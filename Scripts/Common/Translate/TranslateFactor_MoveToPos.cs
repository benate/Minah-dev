using UnityEngine;
using System.Collections;

public class TranslateFactor_MoveToPos : TranslateFactor
{	
	protected Vector3 m_to = Vector3.zero;
	protected Vector3 m_from = Vector3.zero;

	protected float m_speed = 0.0f;
	protected float m_dir = 0.0f;

	protected float m_progressDis = 0.0f;
	public float progressDis { get { return m_progressDis; } }

	public void DoTranslate(ref Vector3 from, ref Vector3 to, float speed, float dir)
	{
		base.OnStart();

		m_from = from;
		m_to = to;		
		
		m_speed = speed;
		m_dir = dir;

		m_progressDis = 0.0f;

		float posDir = to.x - from.x;

		if(Game.DirLeft == dir)
		{
			if( 0 < posDir )
			{
				OnEnd();
			}
		}		
		else if(Game.DirRight == dir)
		{
			if (0 > posDir)
			{
				OnEnd();
			}
		}
		else
		{
			OnEnd();
		}
	}    

	protected override void OnClear()
	{
		base.OnClear();

		m_from = Vector3.zero;
		m_to = Vector3.zero;

		m_progressDis = 0.0f;
	}

	protected override void OnUpdate()
	{
		if (true == IsEnd())
			return;

		float deltaDis = m_speed * Time.deltaTime;
		float compareDis = Mathf.Abs(ownerTransform.position.x - m_to.x);
		if (deltaDis >= compareDis)
		{
			OnEnd();
		}
		else
		{
			ownerTransform.position += Vector3.right * deltaDis * m_dir;
			m_progressDis += deltaDis;
		}
	}
}