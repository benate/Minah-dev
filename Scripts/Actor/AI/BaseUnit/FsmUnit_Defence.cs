using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class FsmUnit_DefenceStep2 : FsmUnitTime
{
	public FsmUnit_DefenceStep2(Fsm fsm, string animName)
		: base(fsm, Game.FsmType.None, animName, 3.0f)
	{ }

	public override void FocusIn()
	{
		base.FocusIn();

		actor.TurnDir(fsm.paramDir);
	}

	public override void FocusOut()
	{
	}
}

class FsmUnit_Defence
{
	public static FsmUnitList Create(Fsm fsm, Game.FsmType attackType, string readyAnimName, string defenceAnimName)
	{
		return new FsmUnitList(fsm, attackType,
							   new FsmUnit_Pursuit(fsm, Game.FsmType.None, readyAnimName),
							   new FsmUnit_DefenceStep2(fsm, defenceAnimName));
	}
}
