using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Texture Type -> Advance
// Read / Write -> Enabled Check

public class TextureCutter : EditorWindow
{
	class IntRect
	{
		public int left;
		public int right;
		public int top;
		public int bottom;
	}

	class RectData
	{
		public bool bHit;
		public IntRect rc;
	};

	[MenuItem("Assets/Texture Cutter")]
	static public void ExcuteTextureCutter()
	{
		Texture2D tex = Selection.activeObject as Texture2D;

		if (tex != null)
		{
			string fullpath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
			string fileName = Path.GetFileNameWithoutExtension(fullpath);

			string path = EditorUtility.OpenFolderPanel("Save cut textures", "", "");
			if (path.Length == 0)
				return;

			AlphaCutting(tex, path, fileName);
		}
		else
		{
			Debug.LogError("Selection.activeObject is not texture!!");
		}
	}

	//void OnGUI()
	//{
	//    EditorGUIUtility.LookLikeControls(80f);

	//    GUILayout.Space(6f);
	//    GUILayout.BeginHorizontal();

	//    Texture2D tex = Selection.activeObject as Texture2D;

	//    bool make = GUILayout.Button("Make Alpha Cut");

	//    if (tex != null && make)
	//    {
	//        string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
	//        AlphaCutting(tex, path);
	//    }

	//    GUILayout.EndHorizontal();
	//}

	private static bool AlphaCutting(Texture2D texture, string path, string fileName)
	{
		List<IntRect> rects = new List<IntRect>();
		GetTextureUV(rects, texture);

		for (int i = 0; i < rects.Count; ++i)
		{
			IntRect rect = rects[i];

			Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);

			int width = rect.right - rect.left;
			int height = rect.bottom - rect.top;
			tex.Resize(width, height);

			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					Color color = texture.GetPixel(rect.left + x, rect.top + y);
					tex.SetPixel(x, y, color);
				}
			}

			byte[] bytes = tex.EncodeToPNG();
			System.IO.File.WriteAllBytes(path + "/" + fileName + i + ".png", bytes);
			bytes = null;
		}

		Debug.Log("Texture Cutting Complete = " + path);
		return true;
	}

	private static bool GetTextureUV(List<IntRect> rects, Texture2D texture)
	{
		int width = texture.width;
		int height = texture.height;

		List<RectData> rectDatas = new List<RectData>();
		
		for(int y = 0; y < height; ++y)
		{
			// ���ٿ� �ִ� ������ üũ�Ѵ�
			List<IntRect> rowRects = new List<IntRect>();
			IntRect prevRect = new IntRect();

			for(int x = 0; x < width; ++x)
			{
				Color color = texture.GetPixel(x, y);
				if (color.a == 0.0f)
				{
					continue;
				}

				IntRect rc = new IntRect();
				rc.left = x;
				rc.top = y;
				rc.right = x + 1;
				rc.bottom = y + 1;

				// ��������� ���� �ִ´�!!
				if (rowRects.Count == 0)
				{
					rowRects.Add(rc);
					// ù��° ���� ����Ų��
					prevRect = rc;
				}
				else 
				{
					// ���� �ȼ��ϰ� ���Ѵ�
					// �̾����� �ȼ��̸� �������� �̾ �����Ѵ�
					if (prevRect.right == rc.left)
					{
						prevRect.right = rc.right;
					}
					else
					{
						rowRects.Add(rc);
						prevRect = rc;
					}
				}
			}

			CollectRect(rects, rectDatas, rowRects);
		}

		foreach(RectData rcData in rectDatas)
		{
			IntRect rc = new IntRect();
			rc.left = rcData.rc.left;
			rc.top = rcData.rc.top;
			rc.right = rcData.rc.right;
			rc.bottom = rcData.rc.bottom;

			rects.Add(rc);
		}

		return true;
	}

	private static void CollectRect(List<IntRect> rects, List<RectData> rectDatas, List<IntRect> rowRects)
	{
	    // ������ �� �о����� ���������� ����!!
	    // ������ �״�� ����!!

		if (rectDatas.Count == 0)
	    {
			foreach (IntRect rcRow in rowRects)
			{
				RectData rcData = new RectData();
				rcData.bHit = false;
				rcData.rc = rcRow;
				rectDatas.Add(rcData);
			}
			return;
	    }


		for (int i = 0; i < rectDatas.Count; ++i)
		{
			rectDatas[i].bHit = false;
		}

		List<RectData> collisions = new List<RectData>();

	    foreach (IntRect rowRect in rowRects)
	    {
	        collisions.Clear();
	        // ��ĥ���� ���� ������
			for (int i = 0; i < rectDatas.Count; ++i)
			{
	            // ��ĥ���� ������ �������� ���ʺ��� ũ�� �����ʺ��� �۾ƾ� �Ѵ�
				if (rectDatas[i].rc.bottom >= rowRect.top && rectDatas[i].rc.top <= rowRect.bottom &&
					rectDatas[i].rc.right >= rowRect.left && rectDatas[i].rc.left <= rowRect.right)
	            {
					rectDatas[i].bHit = true;
					collisions.Add(rectDatas[i]);
	            }
	        }

			RectData firstData = null;
	        // ������� ��ģ���� ù��°�͸� ������ ���ش�
	        foreach (RectData rectData in collisions)
			{
				if (rectData == collisions[0])
				{
					firstData = rectData;
					// ���� ù ����Ÿ�� Row������ ��ģ��
					if (rectData.rc.left > rowRect.left)
						rectData.rc.left = rowRect.left;

					if (rectData.rc.right < rowRect.right)
						rectData.rc.right = rowRect.right;

					rectData.rc.bottom = rowRect.bottom;
				}
				else
				{
					// ������ ����Ÿ�� ��ģ��
					if (firstData.rc.left > rectData.rc.left)
						firstData.rc.left = rectData.rc.left;

					if (firstData.rc.right < rectData.rc.right)
						firstData.rc.right = rectData.rc.right;

					rectDatas.Remove(rectData);
				}
			}

			if (collisions.Count == 0)
			{
				RectData rcData = new RectData();
				rcData.bHit = true;
				rcData.rc = rowRect;
				rectDatas.Add(rcData);
			}
	    }

		// �������� �������� �״�� ������ �ȴ�
		for (int i = 0; i < rectDatas.Count;)
		{
			RectData rectData = rectDatas[i];
			if (false == rectData.bHit)
			{
				IntRect rc = new IntRect();
				rc.left = rectData.rc.left;
				rc.top = rectData.rc.top;
				rc.right = rectData.rc.right;
				rc.bottom = rectData.rc.bottom;

				rects.Add(rc);

				rectDatas.Remove(rectData);
			}
			else
			{
				++i;
			}
		}
	}
}
