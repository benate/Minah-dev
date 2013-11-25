using UnityEngine;
using System.Collections;

public class FsmUnit_Halt : FsmUnitTime
{
	public FsmUnit_Halt(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName, Game.FsmRunTime)
	{
	}

	public override void FocusIn()
	{
		base.FocusIn();

		Vector3 from = actor.pos;
		Vector3 to = actor.data.startPos;

		float dir = to.x - from.x;

		actor.TurnDir(dir);

		translater.moveToPos.DoTranslate(ref from, ref to, actor.data.boostSpeed * 2.0f, actor.dir);
		translater.SetCurrent(translater.moveToPos);
	}

	public override void FocusOut()
	{		
		actor.TurnDir(actor.data.dirForward);

		translater.moveToPos.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		if (translater.moveToPos.IsEnd())
		{
			return Fsm.Result.Done;
		}

		return Fsm.Result.None;
	}
}