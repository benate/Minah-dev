using UnityEngine;
using System.Collections;

public class Tribeman_Range : Tribeman
{
	protected override void OnStart()
	{
		base.OnStart();

		fsm.AddState(FsmUnit_Attack.CreateReadyAttack(fsm, Game.FsmType.Attack1, "Attack1", "Idle2", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreateReadyAttack(fsm, Game.FsmType.Attack2, "Attack1", "Idle2", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreateReadyAttack(fsm, Game.FsmType.Attack3, "Attack1", "Idle2", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreateReadyAttack(fsm, Game.FsmType.Attack4, "Attack1", "Idle2", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreateReadyAttack(fsm, Game.FsmType.Attack5, "Attack1", "Idle2", data.attackActions[0]));

		fsm.AddState(new FsmUnitTime(fsm, Game.FsmType.Idle, "Idle", Game.FsmIdleTime));
		fsm.AddState(new FsmUnit_Halt(fsm, Game.FsmType.Halt, "Run"));		
		fsm.AddState(new FsmUnit_Run(fsm, Game.FsmType.Run, "Run"));
		fsm.AddState(new FsmUnit_Damage(fsm, "Damage"));
		fsm.AddState(new FsmUnit_Death(fsm, "Death"));

		fsm.ChangeState(Game.FsmType.Idle);
	}
}
