using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FsmUnit
{
	public Fsm fsm { private set; get; }
	public Game.FsmType type { private set; get; }	
	public PerformActor actor { get { return fsm.actor; } }
	public Translater translater { get { return fsm.actor.translater; } }	

	private FsmUnit() { }
	public FsmUnit(Fsm fsm, Game.FsmType type) { this.fsm = fsm; this.type = type; }

	public abstract void FocusIn();
	public abstract void FocusOut();
	public abstract Fsm.Result OnUpdate();
}

public class FsmUnitAnimation : FsmUnit
{
	public string animName { private set; get; }

	public FsmUnitAnimation(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type)
	{
		this.animName = animName;
	}

	public override void FocusIn()
	{
		AnimationPlay();
	}

	public override void FocusOut()
	{
	}

	public override Fsm.Result OnUpdate()
	{
		if (!IsAnimationPlaying())
		{
			return Fsm.Result.Done;
		}

		return Fsm.Result.None;
	}

	public void AnimationPlay()
	{
		if (false == fsm.actor.anim.IsPlaying(animName))
		{
			fsm.actor.anim.Play(animName);
		}
	}

	public void ForceAnimationPlay()
	{
		fsm.actor.anim.Play(animName);
	}

	public float GetAnimationTime()
	{
		return fsm.actor.anim.GetAnimationTime();
	}

	public bool IsAnimationPlaying()
	{
		return fsm.actor.anim.IsPlaying(animName);
	}
}

public class FsmUnitTime : FsmUnitAnimation
{
	public float changeTime { private set; get; }

	private float m_progressTime = 0.0f;

	public FsmUnitTime(Fsm fsm, Game.FsmType type, string animName, float changeTime)
		: base(fsm, type, animName)
	{
		this.changeTime = changeTime;
	}

	public override void FocusIn()
	{
		base.FocusIn();
		m_progressTime = 0.0f;
	}

	public override void FocusOut()
	{
	}

	public override Fsm.Result OnUpdate()
	{
		m_progressTime += Time.deltaTime;
		if (m_progressTime >= changeTime)
		{
			return Fsm.Result.Done;
		}

		return Fsm.Result.None;
	}
}

public class FsmUnitList : FsmUnit
{
	private List<FsmUnit> m_units = new List<FsmUnit>();
	private int m_currentUnitIndex = 0;
	private FsmUnit m_currentUnit = null;

	public FsmUnitList(Fsm fsm, Game.FsmType type, params FsmUnit[] units)
		: base(fsm, type)
	{
		for (int i = 0; i < units.Length; i++)
		{
			m_units.Add(units[i]);
		}
	}

	public override void FocusIn()
	{
		m_currentUnit = null;
		m_currentUnitIndex = -1;
	}

	public override void FocusOut()
	{
	}

	public override Fsm.Result OnUpdate()
	{
		if (m_units.Count == 0 || m_currentUnitIndex >= m_units.Count)
		{
			return Fsm.Result.Done;
		}

		if (null == m_currentUnit)
		{
			NextUnit();
		}

		if (Fsm.Result.Done == m_currentUnit.OnUpdate())
		{			
			NextUnit();
		}

		return Fsm.Result.None;
	}

	void NextUnit()
	{
		++m_currentUnitIndex;
		if (m_currentUnitIndex < m_units.Count)
		{
			if(null != m_currentUnit)
				m_currentUnit.FocusOut();

			m_currentUnit = m_units[m_currentUnitIndex];
			m_currentUnit.FocusIn();
		}
		else
		{
			if (null != m_currentUnit)
				m_currentUnit.FocusOut();

			m_currentUnit = null;
		}
	}
}