using UnityEngine;
using System.Collections;

public class ActionFsm_Run : FsmUnitTime
{
	protected ActionUserActor user;

	public ActionFsm_Run(Fsm fsm, Game.FsmType type, string animName)
		: base(fsm, type, animName, Game.FsmRunTime)
	{
		this.user = fsm.actor as ActionUserActor;
	}

	public override void FocusIn()
	{
		base.FocusIn();

		actor.TurnDir(1.0f);
		translater.move.DoTranslate(actor.data.boostSpeed, new Vector2(1.0f, 0.0f));
		translater.SetCurrent(translater.move);
	}

	public override void FocusOut()
	{
		translater.move.Clear();
	}

	public override Fsm.Result OnUpdate()
	{
		// �տ� ���� attackRange�ȿ� �ִٸ� Idle ���·� ������ �ִ´�
		// fsm.ChangeState(Game.FsmType.Idle);

		return base.OnUpdate();
	}
}
