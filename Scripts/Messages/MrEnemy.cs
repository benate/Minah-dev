using UnityEngine;
using System;
using System.Collections;

public class MrEnemy : MrPerformActor
{
	public override void OnCreate()
	{
		base.OnCreate();

		RegistMessage<PacketData.OnGameStart>(OnGameStart);
	}

	private void OnGameStart(PacketData.OnGameStart packet)
	{
		FsmFactor factor = new FsmFactor();

		factor.type = Game.FsmType.Run;
		factor.dir = Game.DirLeft;
		factor.moveDir = Game.DirLeft;

		actor.fsm.ChangeState(ref factor);
	}
}
