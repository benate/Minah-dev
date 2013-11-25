using UnityEngine;
using System.Collections;

public class ActionFsm_AttackDash : FsmUnitTime
{
	protected ActionUserActor user;

	public ActionFsm_AttackDash(Fsm fsm, Game.FsmType type, string animName)
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
		translater.move.DoTranslate(user.attackDashSpeed, new Vector2(dir, 0.0f));
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
			// ��Ÿ� �ȿ� ��� ����!!
			float range = Mathf.Abs(user.target.pos.x - user.pos.x);
			if (range < user.attackRange)
			{
				user.fsm.ChangeState(Game.FsmType.Attack1);
			}
		}
		// ��ư�� ������ ������ ��� �뽬 �̻��·� ������ �ִ´�
		return base.OnUpdate();
	}
}