using UnityEngine;
using System.Collections;

public class FsmUnit_Damage : FsmUnitAnimation
{
	public FsmUnit_Damage(Fsm fsm, string animName)
		: base(fsm, Game.FsmType.Damage, animName) { }

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