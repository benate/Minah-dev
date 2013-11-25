using UnityEngine;
using System.Collections;

public class FsmUnit_Flee : FsmUnitTime
{
	public FsmUnit_Flee(Fsm fsm, Game.FsmType type, string animName, float time)
		: base(fsm, type, animName, time)
	{

	}

	public FsmUnit_Flee(Fsm fsm, string animName)
		: base(fsm, Game.FsmType.Flee, animName, Game.FsmRunTime)
	{
	}

	public override void FocusIn() 
	{
		base.FocusIn();

		float dir = Fsm.GetEnemyDirection(actor) * -1.0f;

		actor.TurnDir(dir);
		translater.move.DoTranslate(actor.data.boostSpeed, new Vector2(actor.dir, 0.0f));
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
