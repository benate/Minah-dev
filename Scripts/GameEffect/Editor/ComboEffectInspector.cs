using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ComboEffect))]
public class ComboEffectInspector : SpriteEffectInspector
{
	private ComboEffect m_comboEffect = null;

	public override void OnInspectorGUI()
	{
		EditorGUIUtility.LookLikeControls(80f);
		m_comboEffect = target as ComboEffect;
		if (m_comboEffect == null)
			return;

		NumberEffect numberEffect = EditorGUILayout.ObjectField("NumberEffect", m_comboEffect.numberEffect, typeof(NumberEffect), true) as NumberEffect;
		if (numberEffect != m_comboEffect.numberEffect)
		{
			NGUIEditorTools.RegisterUndo("Sprite Effect Widget Selection", m_comboEffect);
			m_comboEffect.numberEffect = numberEffect;
			EditorUtility.SetDirty(m_comboEffect);
		}

		base.OnInspectorGUI();
	}
}