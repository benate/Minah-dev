using UnityEngine;
using System.Collections;

public class TranslateFactor_Move : TranslateFactor
{
	float m_speed = 0.0f;
	Vector2 m_dir = Vector2.zero;

	public void DoTranslate(float speed, Vector2 dir)
	{
		OnStart();

		m_speed = speed;
		m_dir = dir;
	}

	protected override void OnClear()
	{
		m_speed = 0.0f;
		m_dir = Vector2.zero;
	}

	protected override void OnUpdate()
	{
		if (true == IsEnd())
			return;

		float deltaPos = m_speed * Time.deltaTime;

		Vector3 add = m_dir * deltaPos;
		ownerTransform.position += add;

		if (add.y < 0.0f && ownerTransform.position.y < Game.GroundYPos)
		{
			OnEnd();
		}
	}
}