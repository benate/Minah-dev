using UnityEngine;
using System.Collections;

public class FsmUnit_Run : FsmUnitTime
{
	public FsmUnit_Run(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName, Game.FsmRunTime)
	{
	}

	public override void FocusIn() 
	{
		base.FocusIn();

		actor.TurnDir(fsm.paramDir);
		translater.move.DoTranslate(actor.data.boostSpeed, new Vector2(fsm.paramMoveDir, 0.0f));
		translater.SetCurrent(translater.move);
	}

	public override void FocusOut() 
	{
		translater.move.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		return base.OnUpdate();
	}
}
