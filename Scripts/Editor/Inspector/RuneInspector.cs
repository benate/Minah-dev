using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Rune))]
public class RuneInspector : Editor
{
	protected Rune m_parent;

	/// <summary>
	/// Atlas selection callback.
	/// </summary>

	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(80f);
		m_parent = target as Rune;

		ComponentSelector.Draw<UIAtlas>(m_parent.atlas, OnSelectAtlas);
		if (m_parent.atlas != null)
		{
			NGUIEditorTools.AdvancedSpriteField(m_parent.atlas, m_parent.spriteName, SelectSprite, false);
		}

		base.OnInspectorGUI();
	}

	void OnSelectAtlas(MonoBehaviour obj)
	{
		if (m_parent != null)
		{
			NGUIEditorTools.RegisterUndo("Rune Atlas Selection", m_parent);
			bool resize = (m_parent.atlas == null);
			m_parent.atlas = obj as UIAtlas;
			EditorUtility.SetDirty(m_parent.gameObject);
		}
	}

	/// <summary>
	/// Sprite selection callback function.
	/// </summary>

	void SelectSprite(string spriteName)
	{
		if (m_parent != null && m_parent.spriteName != spriteName)
		{
			NGUIEditorTools.RegisterUndo("Rune Sprite Change", m_parent);
			m_parent.spriteName = spriteName;
			EditorUtility.SetDirty(m_parent.gameObject);
		}
	}

	//public override bool HasPreviewGUI() { return true; }

	/// <summary>
	/// Draw the sprite preview.
	/// </summary>

	//public override void OnPreviewGUI(Rect rect, GUIStyle background)
	//{
	//    Texture2D tex = m_parent.atlas.texture as Texture2D;
	//    if (tex == null) return;

	//    Rect outer = new Rect(m_parent.atlas.GetAtlasSprite().outer);
	//    Rect inner = new Rect(m_parent.atlas.GetAtlasSprite().inner);
	//    Rect uv = outer;

	//    if (mSprite.atlas.coordinates == UIAtlas.Coordinates.Pixels)
	//    {
	//        uv = NGUIMath.ConvertToTexCoords(outer, tex.width, tex.height);
	//    }
	//    else
	//    {
	//        outer = NGUIMath.ConvertToPixels(outer, tex.width, tex.height, true);
	//        inner = NGUIMath.ConvertToPixels(inner, tex.width, tex.height, true);
	//    }
	//    NGUIEditorTools.DrawSprite(tex, rect, outer, inner, uv, mSprite.color);
	//}
}
