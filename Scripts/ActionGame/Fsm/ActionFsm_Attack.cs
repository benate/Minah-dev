using UnityEngine;
using System.Collections;

public class ActionFsm_Attack : FsmUnitAnimation
{
	protected ActionUserActor user;

	private AttackAction m_attackAction = null;
	private bool m_isAttack = false;

	public ActionFsm_Attack(Fsm fsm, Game.FsmType type, string animName, AttackAction attackAction)
		: base(fsm, type, animName)
	{
		this.user = fsm.actor as ActionUserActor;

		m_attackAction = attackAction;
	}

	public override void FocusIn()
	{
		m_isAttack = false;
		ForceAnimationPlay();
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
				user.comboCount++;
				user.isAttackReady = true;
				m_isAttack = true;
			}
		}

		return base.OnUpdate();
	}
}