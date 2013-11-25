using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionFsm_JumpAttack : FsmUnitTime
{
	protected ActionUserActor user;

	private AttackAction m_attackAction = null;

	public ActionFsm_JumpAttack(Fsm fsm, Game.FsmType type, string animName, AttackAction attackAction)
		: base(fsm, type, animName, float.MaxValue)
	{
		this.user = fsm.actor as ActionUserActor;

		m_attackAction = attackAction;
	}
	
	public override void FocusIn()
	{
		// 가장 가까운 적을 찾는다
		PerformActor enemy = World.instance.GetFrontAntiActor(user.data.relationType);
		float range = 1.0f;
		float attackRange = 500.0f;
		if (enemy != null)
		{
			range = enemy.pos.x - user.pos.x;
			attackRange = 500.0f;
			if (Mathf.Abs(range) < 500.0f)
			{
				attackRange = range;
			}
		}

		float dir = 1.0f;
		if (range < 0.0f)
		{
			attackRange = -attackRange;
			dir = -1.0f;
		}

		user.TurnDir(dir);

		Vector3 pos = Vector2.zero;
		pos.x += attackRange;
		user.translater.arcNormal.DoTranslate(1.0f, pos, 500.0f, false, 1);
		user.translater.SetCurrent(user.translater.arcNormal);
	}

	public override void FocusOut()
	{
		actor.data.targetPos = Vector3.zero;
		user.translater.arcNormal.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		if (user.translater.IsEnd())
		{
			m_attackAction.Attack();
			return Fsm.Result.Done;
		}
		return Fsm.Result.None;
	}
}