using UnityEngine;
using System.Collections;

public class Enemy : PerformActor
{
	public override void FsmEvent(FsmUnit unit, Fsm.Event fsmEvent = Fsm.Event.None)
	{
		if (Game.FsmType.Damage == unit.type)
		{
			fsm.ChangeState(Game.FsmType.Halt);
		}
		else
		{
			fsm.ChangeState(Game.FsmType.Idle);
		}
	}

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

	protected override void OnAwake()
	{
		base.OnAwake();

		MrEnemy mr = gameObject.AddComponent<MrEnemy>();
		mr.actor = this;

		messageReceiver = mr;
	}

	protected override void OnStart()
	{
		base.OnStart();
	}
}