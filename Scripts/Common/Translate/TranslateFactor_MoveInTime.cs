using UnityEngine;
using System.Collections;

public class TranslateFactor_MoveInTime : TranslateFactor_Vector3
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

		Vector3 move = Vector3.Lerp(m_from, m_to, lerp);

		move.y = ownerTransform.position.y;

		ownerTransform.position = move;
	}
}