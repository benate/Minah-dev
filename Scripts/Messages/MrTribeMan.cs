using UnityEngine;
using System;
using System.Collections;

public class MrTribeMan : MrPerformActor
{
	public override void OnCreate()
	{
		base.OnCreate();

		RegistMessage<PacketData.OnGameStart>(OnGameStart);
		RegistMessage<PacketData.OnRunRight>(OnRunRight);
	}

	private void OnGameStart(PacketData.OnGameStart packet)
	{
		FsmFactor factor = new FsmFactor();

		factor.type = Game.FsmType.Run;
		factor.dir = Game.DirRight;
		factor.moveDir = Game.DirRight;

		actor.fsm.ChangeState(ref factor);
	}

	private void OnRunRight(PacketData.OnRunRight packet)
	{
		FsmFactor factor = new FsmFactor();

		factor.type = Game.FsmType.Run;
		factor.dir = Game.DirRight;
		factor.moveDir = Game.DirRight;

		actor.fsm.ChangeState(ref factor);
	}
}
