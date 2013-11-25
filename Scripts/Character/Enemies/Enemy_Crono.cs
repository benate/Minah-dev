using UnityEngine;
using System.Collections;

public class Enemy_Crono : Enemy {

	protected override void OnStart()
	{
		base.OnStart();

		fsm.AddState(FsmUnit_CronoAttack.CreateCronoAttack(fsm, Game.FsmType.Attack1, "Attack1", data.attackActions[0]));
		fsm.AddState(FsmUnit_CronoAttack.CreateCronoAttackTwice(fsm, Game.FsmType.Attack2, "Attack1", data.attackActions[0]));		
		fsm.AddState(FsmUnit_CronoAttack.CreateCronoAttackProjectile(fsm, Game.FsmType.Attack3, "Attack3", data.attackActions[2]));		
		fsm.AddState(FsmUnit_CronoAttack.CreateCronoAttack(fsm, Game.FsmType.Attack4, "Attack4", data.attackActions[3]));
		fsm.AddState(FsmUnit_CronoAttack.CreateCronoAttack(fsm, Game.FsmType.Attack5, "Attack1", data.attackActions[0]));

		fsm.AddState(new FsmUnitTime(fsm, Game.FsmType.Idle, "Idle", Game.FsmIdleTime));
		fsm.AddState(new FsmUnit_Halt(fsm, Game.FsmType.Halt, "Run"));		
		fsm.AddState(new FsmUnit_Run(fsm, Game.FsmType.Run, "Run"));
		fsm.AddState(new FsmUnit_Damage(fsm, "Damage"));
		fsm.AddState(new FsmUnit_Death(fsm, "Death"));		

		fsm.ChangeState(Game.FsmType.Idle);
	}
}
