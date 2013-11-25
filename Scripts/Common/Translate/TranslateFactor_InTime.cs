using UnityEngine;
using System.Collections;

public class TranslateFactor_InTime : TranslateFactor
{
	protected override void OnUpdate()
	{
		base.OnUpdate();
	}

	public void DoTranslate(float durationTime)
	{
		base.doTranslate(durationTime);
	}
}