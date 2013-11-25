using UnityEngine;
using System.Collections;

public class TranslateFactor_Rotate : TranslateFactor_Float
{
	protected override void OnUpdate()
	{
		if (true == IsEnd())
			return;

		float lerp = LerpByTime();
		if (lerp >= 1.0f)
		{
			OnEnd();
		}		

		float rotate = (m_to - m_from) * lerp;

		ownerTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotate);
	}
}