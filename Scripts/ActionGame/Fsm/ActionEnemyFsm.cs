using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionEnemyFsm_Attack : FsmUnitAnimation
{
	private AttackAction m_attackAction = null;
	private bool m_isAttack = false;

	private ActionEnemyActor enemy = null;

	public ActionEnemyFsm_Attack(Fsm fsm, Game.FsmType type, string animName, AttackAction attackAction)
		: base(fsm, type, animName)
	{
		m_attackAction = attackAction;

		enemy = actor as ActionEnemyActor;
	}

	public override void FocusIn()
	{
		m_isAttack = false;
		AnimationPlay();
	}

	public override void FocusOut() 
	{
		actor.data.targetPos = Vector3.zero;
	}

	public override Fsm.Result OnUpdate() 
	{
		if (!m_isAttack && GetAnimationTime() >= m_attackAction.attackTime)
		{
			if (!m_attackAction.isCollisionContinuous)
				m_isAttack = true;

			if (m_attackAction.Attack())
			{
				m_isAttack = true;
			}

			SetTarget();
		}

		return base.OnUpdate();
	}

	private void SetTarget()
	{
		PerformActor target = World.instance.GetFrontAntiActor(actor.data.relationType);
		if (target != null)
			actor.data.targetPos = target.pos;
	}
}

public class ActionEnemyFsm_Idle : FsmUnitTime
{
	private ActionEnemyActor owner = null;

	public ActionEnemyFsm_Idle(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName, float.MaxValue)
	{
		owner = actor as ActionEnemyActor;
	}

	public override void FocusIn()
	{
		AnimationPlay();
	}

	public override void FocusOut()
	{
		actor.data.targetPos = Vector3.zero;
	}

	public override Fsm.Result OnUpdate()
	{
		PerformActor actor = World.instance.GetFrontAntiActor(owner.data.relationType);
		if (actor != null && Mathf.Abs(actor.pos.x - owner.pos.x) < owner.attackRange)
		{
			fsm.ChangeState(Game.FsmType.Attack1);
		}
		return base.OnUpdate();
	}
}