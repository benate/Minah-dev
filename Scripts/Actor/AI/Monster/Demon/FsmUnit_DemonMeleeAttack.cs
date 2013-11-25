using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FsmUnit_DemonMeleeAttack
{
	//-->
	public static FsmUnitList Create(Fsm fsm, Game.FsmType type, string anim1, string anim2, string anim3, AttackAction attackAction)
	{
		FsmUnit step1 = new FsmUnit_Pursuit(fsm,Game.FsmType.None, anim1);
		FsmUnit step2 = new FsmUnitTime(fsm, Game.FsmType.None, anim2, 2.0f);
		FsmUnit step3 = new FsmUnit_Attack(fsm, Game.FsmType.None, anim3, attackAction);

		return new FsmUnitList(fsm, type, step1, step2, step3);
	}
}