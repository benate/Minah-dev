using UnityEngine;
using System.Collections;

public class Tribeman_Melee : Tribeman {

	protected override void OnStart()
	{
		base.OnStart();
		
		fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack1, "Run", "Attack1", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack2, "Run", "Attack1", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack3, "Run", "Attack1", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack4, "Run", "Attack1", data.attackActions[0]));
		fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack5, "Run", "Attack1", data.attackActions[0]));

		fsm.AddState(new FsmUnitTime(fsm, Game.FsmType.Idle, "Idle", Game.FsmIdleTime));
		fsm.AddState(new FsmUnit_Halt(fsm, Game.FsmType.Halt, "Run"));
		fsm.AddState(new FsmUnit_Run(fsm, Game.FsmType.Run, "Run"));
		fsm.AddState(new FsmUnit_Damage(fsm, "Damage"));
		fsm.AddState(new FsmUnit_Death(fsm, "Death"));

		fsm.ChangeState(Game.FsmType.Idle);
	}
}
