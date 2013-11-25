using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(SpriteEffect))]
public class SpriteEffectInspector : Editor
{
	private SpriteEffect m_parent = null;

	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(80f);
		m_parent = target as SpriteEffect;
		if (m_parent == null)
			return;

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("isEndHide", GUILayout.Width(76f));
			bool isEndHide = EditorGUILayout.Toggle(m_parent.isEndHide);
			if (isEndHide != m_parent.isEndHide)
			{
				m_parent.isEndHide = isEndHide;
				NGUIEditorTools.RegisterUndo("isEndHide Data Select", m_parent);
				EditorUtility.SetDirty(m_parent);
			}
		}
		GUILayout.EndHorizontal();

		UIWidget widget = EditorGUILayout.ObjectField("Widget", m_parent.applyWidget, typeof(UIWidget), true) as UIWidget;
		if (widget != m_parent.applyWidget)
		{
			NGUIEditorTools.RegisterUndo("Sprite Effect Widget Selection", m_parent);
			m_parent.applyWidget = widget;
			EditorUtility.SetDirty(m_parent);
		}
	//	ComponentSelector.Draw<UIWidget>(m_parent.applyWidget, OnSelectWidget);

		bool isChange = false;
		foreach (SpriteEffect.EffectData data in m_parent.effectDatas)
		{
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("Play Time", GUILayout.Width(76f));
				float playTime = EditorGUILayout.FloatField(data.playTime);
				if (playTime != data.playTime)
				{
					data.playTime = playTime;
					isChange = true;
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("isColor", GUILayout.Width(76f));
				bool isColor = EditorGUILayout.Toggle(data.isColor);
				if (isColor != data.isColor)
				{
					data.isColor = isColor;
					isChange = true;
				}
			}
			GUILayout.EndHorizontal();

			if (data.isColor)
				DrawColor(data, ref isChange);

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("isScale", GUILayout.Width(76f));
				bool isScale = EditorGUILayout.Toggle(data.isScale);
				if (isScale != data.isScale)
				{
					data.isScale = isScale;
					isChange = true;
				}
			}
			GUILayout.EndHorizontal();

			if (data.isScale)
				DrawScale(data, ref isChange);

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("isPosition", GUILayout.Width(76f));
				bool isPosition = EditorGUILayout.Toggle(data.isPosition);
				if (isPosition != data.isPosition)
				{
					data.isPosition = isPosition;
					isChange = true;
				}
			}
			GUILayout.EndHorizontal();

			if (data.isPosition)
				DrawPosition(data, ref isChange);

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("isRotate", GUILayout.Width(76f));
				bool isRotate = EditorGUILayout.Toggle(data.isRotate);
				if (isRotate != data.isRotate)
				{
					data.isRotate = isRotate;
					isChange = true;
				}
			}
			GUILayout.EndHorizontal();

			if (data.isRotate)
				DrawRotate(data, ref isChange);
		}

		GUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Add Effect Data"))
			{
				SpriteEffect.EffectData newData = new SpriteEffect.EffectData();
				m_parent.effectDatas.Add(newData);
				NGUIEditorTools.RegisterUndo("Add Effect Data Select", m_parent);
				EditorUtility.SetDirty(m_parent);
			}
		}
		GUILayout.EndHorizontal();

		if (m_parent.effectDatas.Count > 0)
		{
			GUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Remove Effect Data"))
				{
					m_parent.effectDatas.RemoveAt(m_parent.effectDatas.Count - 1);
					NGUIEditorTools.RegisterUndo("Remove Effect Data Select", m_parent);
					EditorUtility.SetDirty(m_parent);
				}
			}
			GUILayout.EndHorizontal();
		}

		if (isChange)
		{
			NGUIEditorTools.RegisterUndo("SpriteEffect Save", m_parent);
			EditorUtility.SetDirty(m_parent);
		}
	}

	void DrawColor(SpriteEffect.EffectData data, ref bool isChange)
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("Start Color", GUILayout.Width(76f));
			Color startColor = EditorGUILayout.ColorField(data.startColor);
			if (startColor != data.startColor)
			{
				data.startColor = startColor;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("End Color", GUILayout.Width(76f));
			Color endColor = EditorGUILayout.ColorField(data.endColor);
			if (endColor != data.endColor)
			{
				data.endColor = endColor;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();
	}

	void DrawScale(SpriteEffect.EffectData data, ref bool isChange)
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("Start Scale", GUILayout.Width(76f));
			float startScale = EditorGUILayout.FloatField(data.startScale);
			if (startScale != data.startScale)
			{
				data.startScale = startScale;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("End Scale", GUILayout.Width(76f));
			float endScale = EditorGUILayout.FloatField(data.endScale);
			if (endScale != data.endScale)
			{
				data.endScale = endScale;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();
	}

	void DrawPosition(SpriteEffect.EffectData data, ref bool isChange)
	{
		GUILayout.BeginHorizontal();
		{
			Vector3 startPosition = EditorGUILayout.Vector3Field("Start Position", data.startPosition);
			if (startPosition != data.startPosition)
			{
				data.startPosition = startPosition;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		{
			Vector3 endPosition = EditorGUILayout.Vector3Field("End Position", data.endPosition);
			if (endPosition != data.endPosition)
			{
				data.endPosition = endPosition;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();
	}

	void DrawRotate(SpriteEffect.EffectData data, ref bool isChange)
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("Start Rotate", GUILayout.Width(76f));
			float startRotate = EditorGUILayout.FloatField(data.startRotate);
			if (startRotate != data.startRotate)
			{
				data.startRotate = startRotate;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("End Rotate", GUILayout.Width(76f));
			float endRotate = EditorGUILayout.FloatField(data.endRotate);
			if (endRotate != data.endRotate)
			{
				data.endRotate = endRotate;
				isChange = true;
			}
		}
		GUILayout.EndHorizontal();
	}

	void OnSelectWidget(MonoBehaviour obj)
	{
		NGUIEditorTools.RegisterUndo("Sprite Effect Widget Selection", m_parent);
		m_parent.applyWidget = obj as UIWidget;
		EditorUtility.SetDirty(m_parent);
	}
}
