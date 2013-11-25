using UnityEngine;
using System.Collections;

public class FsmUnit_Hover : FsmUnitAnimation
{
	CheckTimer m_timer = new CheckTimer();

	public FsmUnit_Hover(Fsm fsm, string animName)
		: base(fsm, Game.FsmType.Hover, animName)
	{ }

	public override void FocusIn()
	{
		int wait = Random.Range(0, 10);
		if (0 == wait % 3)
		{
			m_timer.Clear();
			m_timer.SetTimer(Random.Range(2.0f, 4.0f));			

			setMove();
			AnimationPlay();
		}
	}

	public override void FocusOut() 
	{
		m_timer.Clear();
		translater.move.Clear();
	}

	public override Fsm.Result OnUpdate() 
	{
		m_timer.OnUpdate();
		if (m_timer.IsEnd())
		{
			return Fsm.Result.Done;
		}

		return Fsm.Result.None;
	}
	
	void setMove()
	{
		float dis = fsm.actor.data.startPos.x - actor.pos.x;
		if (Game.MoveRange <= Mathf.Abs(dis))
		{
			actor.TurnDir(dis);
		}

		translater.move.DoTranslate(actor.data.speed, new Vector2(actor.dir, 0.0f));
		translater.SetCurrent(translater.move);
	}
}
