using UnityEngine;
using System.Collections;

public class ActionFsm_Dash : FsmUnitTime
{
	protected ActionUserActor user;

	public ActionFsm_Dash(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName, float.MaxValue)
	{
		this.user = fsm.actor as ActionUserActor;
	}

	public override void FocusIn()
	{
		base.FocusIn();
		AnimationPlay();

		float dir = user.target.pos.x - user.pos.x;
		if (dir > 0.0f)
			dir = 1.0f;
		else
			dir = -1.0f;

		user.TurnDir(dir);
		translater.move.DoTranslate(user.dashSpeed, new Vector2(dir, 0.0f));
		translater.SetCurrent(translater.move);
	}

	public override void FocusOut()
	{
		base.FocusOut();
		translater.move.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		if (user.target != null)
		{
			float range = Mathf.Abs(user.pos.x - user.target.pos.x);
			// 사거리 안에 들면 공격!!
			if (range < user.attackDashRange)
			{
				user.fsm.ChangeState(Game.FsmType.AttackDash);
			}
		}
		// 버튼을 누르고 있으면 계속 대쉬 이상태로 가만히 있는다
		return base.OnUpdate();
	}
}