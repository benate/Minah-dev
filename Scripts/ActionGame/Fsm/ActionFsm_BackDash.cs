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
		// ��ư�� ������ �ִ� �������� Ȯ���Ѵ�
		// ������ ������ �����ִ´�
		if (translater.moveToPos.IsEnd() && !user.isPressed)
		{
			user.fsm.ChangeState(Game.FsmType.Run);
		}
		// ��ư�� ������ ������ ��� �뽬 �̻��·� ������ �ִ´�
		return base.OnUpdate();
	}
}