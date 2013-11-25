using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FsmUnit_CronoAttack 
{
	public static FsmUnitList CreateCronoAttack(Fsm fsm, Game.FsmType attackType, string attackAnimation, AttackAction attackAction)
	{
		return new FsmUnitList(fsm, attackType,
							   new FsmUnit_Pursuit(fsm, Game.FsmType.None, "Run"),
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction),
							   new FsmUnit_Halt(fsm, Game.FsmType.None, "Run"));
	}

	public static FsmUnitList CreateCronoAttackTwice(Fsm fsm, Game.FsmType attackType, string attackAnimation, AttackAction attackAction)
	{
		return new FsmUnitList(fsm, attackType,
							   new FsmUnit_Pursuit(fsm, Game.FsmType.None, "Run"),
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction),
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction),
							   new FsmUnit_Halt(fsm, Game.FsmType.None, "Run"));
	}

	public static FsmUnitList CreateCronoAttackProjectile(Fsm fsm, Game.FsmType attackType, string attackAnimation, AttackAction attackAction)
	{
		return new FsmUnitList(fsm, attackType,							   
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction),
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction),
							   new FsmUnit_Attack(fsm, Game.FsmType.None, attackAnimation, attackAction));
	}

}
