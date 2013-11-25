using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionEnemyActor : PerformActor
{
	public float attackRange = 100.0f;

	public override void FactorEvent(GameData.FactorEventType eventType)
	{
		if (GameData.FactorEventType.Damage == eventType)
		{
			if (0 >= data.curHp)
			{
				fsm.ChangeState(Game.FsmType.Death);
			}
			else
			{
				fsm.ChangeState(Game.FsmType.Damage);
			}
		}
	}

	public override void FsmEvent(FsmUnit unit, Fsm.Event fsmEvent = Fsm.Event.None)
	{
		// 적과의 거리가 가까우면 무조건 공격!
		PerformActor actor = World.instance.GetFrontAntiActor(data.relationType);
		if (actor != null && Mathf.Abs(actor.pos.x - pos.x) < attackRange)
		{
			fsm.ChangeState(Game.FsmType.Attack1);
		}
		else
		{
			fsm.ChangeState(Game.FsmType.Idle);
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();

		index = GameEnum.NewIndex;

		MrPerformActor mr = gameObject.AddComponent<MrPerformActor>();
		mr.actor = this;

		messageReceiver = mr;
	}

	protected override void OnStart()
	{
		base.OnStart();

		fsm.AddState(new ActionEnemyFsm_Attack(fsm, Game.FsmType.Attack1, "Attack1", data.attackActions[0]));

		fsm.AddState(new ActionEnemyFsm_Idle(fsm, Game.FsmType.Idle, "Idle"));
		fsm.AddState(new FsmUnit_Damage(fsm, "Damage"));
		fsm.AddState(new FsmUnit_Death(fsm, "Death"));

		fsm.ChangeState(Game.FsmType.Idle);
	}
}
