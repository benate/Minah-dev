using UnityEngine;
using System.Collections;

public class TranslateFactor_Arc : TranslateFactor
{	
	protected float m_upForce = 0.0f;
	protected float m_speed = 0.0f;
	protected float m_dir = 0.0f;

	public void DoTranslate(float speed, float upForce, float dir)
	{
		OnStart();
		m_speed = speed;
		m_upForce = upForce;
		m_dir = dir;
	}

	protected override void OnClear()
	{
		base.OnClear();

		m_upForce = 0.0f;
		m_speed = 0.0f;
		m_dir = 0.0f;
	}

	protected override void OnUpdate()
	{
		if (true == IsEnd())
			return;
		
		float lerp = LerpByTime();
		if (lerp >= 1.0f)
		{
			OnEnd();
		}		

		Vector3 pos = ownerTransform.position;
		//--> x axis
		float deltaPos = m_speed * Time.deltaTime;

		pos.x += deltaPos * m_dir;

		//--> y axis
		m_upForce -= Game.Gravity * Time.deltaTime;

		pos += Vector3.up * ((m_upForce * Time.deltaTime) + (Game.Gravity * Time.deltaTime * Time.deltaTime) * 0.5f);

		if (Game.GroundYPos > pos.y)
		{
			OnEnd();
			pos += ((Vector3.up * (Game.GroundYPos - pos.y)));
		}
		ownerTransform.position = pos;
	}
}