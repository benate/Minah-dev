using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(NumberEffect))]
public class NumberInspector : Editor
{
	private NumberEffect m_number = null;
	void Start ()
	{
	}
	
	void Update ()
	{
	
	}

	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(80f);
		m_number = target as NumberEffect;
		if (m_number == null)
			return;

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("cipher", GUILayout.Width(76f));
			int cipher = EditorGUILayout.IntField(m_number.cipher);
			if (cipher != m_number.cipher)
			{
				NGUIEditorTools.RegisterUndo("NumberEffect Cipher Selection", m_number);
				m_number.cipher = cipher;
				EditorUtility.SetDirty(m_number.gameObject);
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("startNumber", GUILayout.Width(76f));
			int startNumber = EditorGUILayout.IntField(m_number.startNumber);
			if (startNumber != m_number.startNumber)
			{
				NGUIEditorTools.RegisterUndo("NumberEffect StartNumber Selection", m_number);
				m_number.startNumber = startNumber;
				EditorUtility.SetDirty(m_number.gameObject);
			}
		}
		GUILayout.EndHorizontal();

		NumberEffect.Aligned aligned = (NumberEffect.Aligned)EditorGUILayout.EnumPopup("Aligned Type", m_number.aligned);
		if (aligned != m_number.aligned)
		{
			NGUIEditorTools.RegisterUndo("NumberEffect Aligned Selection", m_number);
			m_number.aligned = aligned;
			EditorUtility.SetDirty(m_number.gameObject);
		}

		ComponentSelector.Draw<UIAtlas>(m_number.atlas, OnSelectAtlas);
		if (m_number.atlas != null)
		{
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName0, OnSelectSpriteNum0, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName1, OnSelectSpriteNum1, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName2, OnSelectSpriteNum2, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName3, OnSelectSpriteNum3, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName4, OnSelectSpriteNum4, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName5, OnSelectSpriteNum5, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName6, OnSelectSpriteNum6, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName7, OnSelectSpriteNum7, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName8, OnSelectSpriteNum8, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_numOfSpriteName9, OnSelectSpriteNum9, false);
			NGUIEditorTools.AdvancedSpriteField(m_number.atlas, m_number.m_dotOfSpriteName, OnSelectSpriteDot, false);
		}
	}

	void OnSelectAtlas(MonoBehaviour obj)
	{
		NGUIEditorTools.RegisterUndo("NumberEffect Atlas Selection", m_number);
		m_number.atlas = obj as UIAtlas;
		EditorUtility.SetDirty(m_number.gameObject);
	}

	void OnSelectSpriteNum0(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName0, spriteName); }
	void OnSelectSpriteNum1(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName1, spriteName); }
	void OnSelectSpriteNum2(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName2, spriteName); }
	void OnSelectSpriteNum3(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName3, spriteName); }
	void OnSelectSpriteNum4(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName4, spriteName); }
	void OnSelectSpriteNum5(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName5, spriteName); }
	void OnSelectSpriteNum6(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName6, spriteName); }
	void OnSelectSpriteNum7(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName7, spriteName); }
	void OnSelectSpriteNum8(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName8, spriteName); }
	void OnSelectSpriteNum9(string spriteName) { SelectSprite(ref m_number.m_numOfSpriteName9, spriteName); }
	void OnSelectSpriteDot(string spriteName) { SelectSprite(ref m_number.m_dotOfSpriteName, spriteName); }

	void SelectSprite(ref string spriteApplyName, string spriteName)
	{
		if (spriteApplyName != spriteName)
		{
			NGUIEditorTools.RegisterUndo("Sprite Change", m_number);
			spriteApplyName = spriteName;
			EditorUtility.SetDirty(m_number.gameObject);
		}
	}
}
