using UnityEngine;
using System.Collections;

public class FsmUnit_JumpAttackStep1 : FsmUnitAnimation
{
	public FsmUnit_JumpAttackStep1(Fsm fsm, string animName)
		: base(fsm, Game.FsmType.None, animName)
	{
	}

	public override void FocusIn()
	{
		actor.TurnDir(fsm.paramDir);
		AnimationPlay();
	}

	public override void FocusOut()
	{
	}
}

public class FsmUnit_JumpAttackStep2 : FsmUnitAnimation
{
	private AttackAction m_attackAction = null;

	public FsmUnit_JumpAttackStep2(Fsm fsm, string animName, AttackAction attackAction)
		: base(fsm, Game.FsmType.None, animName)
	{
		m_attackAction = attackAction;
	}

	public override void FocusIn()
	{
		actor.translater.arc.DoTranslate(actor.data.boostSpeed, actor.data.jumpPower, actor.dir);
		actor.translater.SetCurrent(actor.translater.arc);
	}

	public override void FocusOut()
	{
	}

	public override Fsm.Result OnUpdate()
	{
		if (actor.translater.IsEnd())
		{
			m_attackAction.Attack();
			return Fsm.Result.Done;
		}

		return Fsm.Result.None;
	}
}

public class FsmUnit_JumpAttack
{
	public static FsmUnitList Create(Fsm fsm, Game.FsmType type, string readyAnimName, string jumpAnimName, AttackAction attackAction)
	{
		return new FsmUnitList(fsm, type
			, new FsmUnit_JumpAttackStep1(fsm, readyAnimName)
			, new FsmUnit_JumpAttackStep2(fsm, jumpAnimName, attackAction));
	}
}
