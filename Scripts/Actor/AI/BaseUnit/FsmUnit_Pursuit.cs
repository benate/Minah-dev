using UnityEngine;
using System.Collections;

public class FsmUnit_Pursuit : FsmUnitAnimation
{
	public FsmUnit_Pursuit(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName)
	{
	}

	public override void FocusIn() 
	{
		base.FocusIn();

		Actor enemy = Fsm.GetFrontEnemy(actor);
		if (enemy != null)
		{
			Vector3 fromPos = actor.pos;
			Vector3 toPos = enemy.pos;

			actor.TurnDir(toPos.x - fromPos.x);

			toPos.x += -(50.0f * actor.dir);

			translater.moveToPos.DoTranslate(ref fromPos, ref toPos, actor.data.boostSpeed * 2.0f, actor.dir);
			translater.SetCurrent(translater.moveToPos);
		}
	}

	public override void FocusOut() 
	{
		translater.moveToPos.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		if (translater.IsEnd())
		{
			return Fsm.Result.Done;
		}

		return Fsm.Result.None;
	}
}
