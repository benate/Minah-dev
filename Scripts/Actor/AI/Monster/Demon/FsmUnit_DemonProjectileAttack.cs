using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FsmUnit_DemonProjectileAttack
{
	//-->
	public static FsmUnitList Create(Fsm fsm, Game.FsmType type, string anim1, string anim2, AttackAction attackAction)
	{
		FsmUnit step1 = new FsmUnitTime(fsm, Game.FsmType.None, anim1, 2.0f);
		FsmUnit step2 = new FsmUnit_Attack(fsm, Game.FsmType.None, anim2, attackAction);

		return new FsmUnitList(fsm, type, step1, step2);
	}
}