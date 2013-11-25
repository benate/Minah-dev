using UnityEngine;
using System.Collections;

public class FsmUnit_Death : FsmUnitAnimation
{
	public FsmUnit_Death(Fsm fsm, string animName)
		: base(fsm, Game.FsmType.Death, animName) { }

	public override void FocusIn() 
	{
		translater.move.Clear();
		translater.moveInTime.Clear();
		translater.moveToPos.Clear();

		AnimationPlay();
	}

	public override void FocusOut() 
	{

	}
}