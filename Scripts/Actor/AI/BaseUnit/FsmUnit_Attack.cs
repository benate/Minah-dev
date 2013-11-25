using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FsmUnit_Attack : FsmUnitAnimation
{
	private AttackAction m_attackAction = null;
	private bool m_isAttack = false;

	List<PerformActor> m_aliveActors = new List<PerformActor>();

	public FsmUnit_Attack(Fsm fsm, Game.FsmType type, string animName, AttackAction attackAction)
		: base(fsm, type, animName)
	{
		m_attackAction = attackAction;
	}

	public override void FocusIn()
	{
		m_isAttack = false;
		m_aliveActors.Clear();

		float dir = Fsm.GetEnemyDirection(actor);
		actor.TurnDir(dir);

		AnimationPlay();
	}

	public override void FocusOut() 
	{
		actor.data.targetPos = Vector3.zero;
		m_aliveActors.Clear();
	}

	public override Fsm.Result OnUpdate() 
	{
		if (!m_isAttack && GetAnimationTime() >= m_attackAction.attackTime)
		{
			SetTarget();

			m_isAttack = true;
			m_attackAction.Attack();
		}

		return base.OnUpdate();
	}

	void SetTarget()
	{
		List<PerformActor> actors = Fsm.GetAnitActors(actor.data.relationType);

		foreach (PerformActor targetActor in actors)
		{
			if (Game.FsmType.Death == targetActor.fsm.curFsmType)
				continue;

			m_aliveActors.Add(targetActor);
		}

		if (0 < m_aliveActors.Count)
		{
			int targetNumber = Random.Range(0, m_aliveActors.Count);

			PerformActor targetActor = m_aliveActors[targetNumber];

		    actor.data.targetPos = targetActor.pos;
		}
	}

#region static funcs..

	public static FsmUnitList CreateReadyAttack(Fsm fsm, Game.FsmType attackType, string attackAnimation, string readyAnimName, AttackAction attackAction)
	{
		return new FsmUnitList(fsm, attackType,
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction),
							   new FsmUnitTime(fsm, Game.FsmType.None, readyAnimName, 2.0f));
	}

	public static FsmUnitList CreatePursuitAttack(Fsm fsm, Game.FsmType attackType, string pursuitAnimName, string attackAnimName, AttackAction attackAction)
	{
		return new FsmUnitList(fsm, attackType,
							   new FsmUnit_Pursuit(fsm, Game.FsmType.None, pursuitAnimName),
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimName, attackAction),
							   new FsmUnit_Halt(fsm, Game.FsmType.None, pursuitAnimName));
	}

#endregion
}
