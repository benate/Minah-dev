using UnityEngine;
using System.Collections;

public class ComboEffect : SpriteEffect
{
	public NumberEffect numberEffect;

	public void SetCombo(int comboCount)
	{
		if (numberEffect != null)
			numberEffect.SetNumber(comboCount);

		Play();
	}

	protected override void SetCurrentFactor(int current, float factor)
	{
		base.SetCurrentFactor(current, factor);
		if (numberEffect != null)
			numberEffect.SetColor(m_currentColor);

		Debug.Log("SetCurrentFactor = " + m_currentColor);
	}
}
