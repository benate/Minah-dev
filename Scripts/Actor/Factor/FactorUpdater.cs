using UnityEngine;
using System.Collections;

public class FactorUpdater
{
	public virtual void Init(Factor factor) { }
	public virtual void OnUpdate(Factor factor) { }
}

public class FactorUpdater_OnShot : FactorUpdater
{
	public override void OnUpdate(Factor factor)
	{
		factor.effect.OnEffect(factor);
		factor.isEnd = true;
	}
}

public class FactorUpdater_Dot : FactorUpdater
{
	CheckTimer m_dotTimer = new CheckTimer();
	CheckTimer m_lifeTimer = new CheckTimer();

	public FactorUpdater_Dot(float durationTime)
	{
		m_lifeTimer.SetTimer(durationTime);
	}

	public override void Init(Factor factor) 
	{
		m_dotTimer.SetTimer(0.0f);		
	}

	public override void OnUpdate(Factor factor)
	{
		m_dotTimer.OnUpdate();
		if (m_dotTimer.IsEnd())
		{
			m_dotTimer.SetTimer(1.0f);
			factor.effect.OnEffect(factor);
		}

		m_lifeTimer.OnUpdate();
		if (m_lifeTimer.IsEnd())
		{
			factor.isEnd = true;
		}
	}
}

public class FactorUpdater_Timer : FactorUpdater
{
	CheckTimer m_timer = new CheckTimer();

	public FactorUpdater_Timer(float durationTime)
	{
		m_timer.SetTimer(durationTime);
	}

	public override void Init(Factor factor)	
	{
		factor.effect.OnEffect(factor);
	}
	
	public override void OnUpdate(Factor factor)
	{
		m_timer.OnUpdate();
		if (true == m_timer.IsEnd())
		{
			factor.effect.OffEffect(factor);

			factor.isEnd = true;
		}
	}
}