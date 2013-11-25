using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct FsmFactor
{
	public Game.FsmType type;
	public float dir;
	public float moveDir;

	public void Clear()
	{
		type = Game.FsmType.None;
		dir = Game.DirNone;
		moveDir = Game.DirNone;
	}
}

public class Fsm : BaseBehaviour
{
	public enum Result
	{
		None,
		Done,
	}

	public enum Event
	{
		None,
		EnemyInSight,
		TooFarFromAgent,
		Blocked,
	}
	
	public PerformActor actor { set; get; }

	protected Dictionary<Game.FsmType, FsmUnit> m_units = new Dictionary<Game.FsmType, FsmUnit>();
	
	protected FsmUnit m_fsmUnit = null;

	//--> fsm data
	protected Game.FsmType m_curFsmType = Game.FsmType.None; 
	public Game.FsmType curFsmType { get { return m_curFsmType; } }

	protected float m_paramDir = Game.DirNone; public float paramDir { get { return m_paramDir; } }
	protected float m_paramMoveDir = Game.DirNone; public float paramMoveDir { get { return m_paramMoveDir; } }

	public virtual void Clear()
	{
		m_fsmUnit = null;
	}

	public void AddState(FsmUnit unit)
	{		
		m_units.Add(unit.type, unit);
	}

	public void RepeatState()
	{
		if (null != m_fsmUnit)
		{
			m_fsmUnit.FocusOut();
			m_fsmUnit.FocusIn();
		}
	}

	public void ChangeState(Game.FsmType type)
	{
		if (Game.FsmType.Death == curFsmType)
			return;

		ForceChangeState(type);
	}

	public void ChangeState(ref FsmFactor factor)
	{
		if (Game.FsmType.Death == curFsmType)
			return;

		ForceChangeState(ref factor);
	}

	public void ForceChangeState(ref FsmFactor factor)
	{
		//--> focusout
		if (null != m_fsmUnit)
		{
			m_fsmUnit.FocusOut();
		}

		//--> data
		m_curFsmType	= factor.type;
		m_paramDir		= factor.dir;
		m_paramMoveDir	= factor.moveDir;

		//--> focusin		
		m_fsmUnit = m_units[factor.type];
		m_fsmUnit.FocusIn();		
	}

	public void ForceChangeState(Game.FsmType type)
	{
		//--> focusout
		if (null != m_fsmUnit)
		{
			m_fsmUnit.FocusOut();
		}

		//--> data
		m_curFsmType	= type;

		//--> focusin		
		m_fsmUnit = m_units[type];
		m_fsmUnit.FocusIn();	
	}

	void FixedUpdate()
	{
		if (true == actor.pause)
			return;

		this.OnUpdate();
	}

	public void OnUpdate()
	{
		if (null != m_fsmUnit)
		{
			if (Result.Done == m_fsmUnit.OnUpdate())
			{
				actor.FsmEvent(m_fsmUnit);
			}
		}	
	}

	#region static func..

	public static List<PerformActor> GetAnitActors(GameData.RelationType type)
	{
		return World.instance.GetAntiActors(type);
	}

	public static float GetDirection(Actor from, Actor to)
	{
		float dis = from.pos.x - to.pos.x;
		if (dis > Game.DirNone)
		{
			return Game.DirLeft;
		}

		return Game.DirRight;
	}

	public static float GetEnemyDirection(PerformActor actor)
	{
		Vector3 actorPos = actor.pos;
		Actor enemy = World.instance.GetFrontAntiActor(actor.data.relationType);
		if (null != enemy)
		{
			float dis = actorPos.x - enemy.pos.x;
			if (Game.DirNone < dis)
			{
				return Game.DirLeft;
			}

			if (Game.DirNone > dis)
			{
				return Game.DirRight;
			}
		}

		return Game.DirNone;
	}

	public static Actor GetFrontEnemy(PerformActor actor)
	{
		Actor enemy = World.instance.GetFrontAntiActor(actor.data.relationType);

		return enemy;
	}	

	

	#endregion
}