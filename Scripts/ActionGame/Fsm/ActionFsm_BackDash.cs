using UnityEngine;
using System.Collections;

public class ActionFsm_BackDash : FsmUnitTime
{
	protected ActionUserActor user;

	public ActionFsm_BackDash(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName, float.MaxValue)
	{
		this.user = fsm.actor as ActionUserActor;
	}

	public override void FocusIn()
	{
		base.FocusIn();
		AnimationPlay();

		Vector3 from = user.pos;
		Vector3 to = new Vector3(from.x - user.backDashRange, from.y, from.z);
		actor.TurnDir(fsm.paramDir);
		translater.moveToPos.DoTranslate(ref from, ref to, user.attackDashSpeed, -fsm.paramDir);
		translater.SetCurrent(translater.moveToPos);
	}

	public override void FocusOut()
	{
		base.FocusOut();
		translater.moveToPos.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		// 버튼을 누르고 있는 상태인지 확인한다
		// 누르고 있으면 멈취있는다
		if (translater.moveToPos.IsEnd() && !user.isPressed)
		{
			user.fsm.ChangeState(Game.FsmType.Run);
		}
		// 버튼을 누르고 있으면 계속 대쉬 이상태로 가만히 있는다
		return base.OnUpdate();
	}
}