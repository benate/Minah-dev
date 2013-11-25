using UnityEngine;
using System.Collections;

public class Tribeman : PerformActor
{
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
		if (Game.FsmType.Damage == unit.type)
		{
			fsm.ChangeState(Game.FsmType.Halt);
		}
		else
		{
			fsm.ChangeState(Game.FsmType.Idle);
		}		
	}

	protected override void OnAwake()
	{
		base.OnAwake();

		MrTribeMan mr = gameObject.AddComponent<MrTribeMan>();
		mr.actor = this;

		messageReceiver = mr;
	}

	protected override void OnStart()
	{
		base.OnStart();
	}
}