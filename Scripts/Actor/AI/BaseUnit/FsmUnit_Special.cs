using UnityEngine;
using System.Collections;

public class FsmUnit_Special : FsmUnit
{
	public FsmUnit_Special(Fsm fsm) : base(fsm, Game.FsmType.Special) { }

	public override void FocusIn() { }

	public override void FocusOut() { }

	public override Fsm.Result OnUpdate() 
	{
		return Fsm.Result.None;
	}

}
