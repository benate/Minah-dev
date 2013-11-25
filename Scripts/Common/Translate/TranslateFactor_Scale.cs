using UnityEngine;
using System.Collections;

public class TranslateFactor_Scale : TranslateFactor_Float
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

		float scale = (m_to - m_from) * lerp;

		ownerTransform.localScale = Vector3.one * scale;
	}
}