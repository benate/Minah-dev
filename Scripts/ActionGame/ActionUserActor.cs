using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionUserActor : PerformActor
{
	public PerformActor target { get; set; }

	public float attackDashRange = 200.0f;
	public float attackRange = 100.0f;

	public float dashSpeed = 500.0f;
	public float attackDashSpeed = 1000.0f;

	public float backDashRange = 500.0f;

	public int comboCount { get; set; }
	public bool isAttackReady { get; set; }
	public bool isPressed { get; set; }

	public override void FactorEvent(GameData.FactorEventType eventType)
	{
		if (GameData.FactorEventType.Damage == eventType)
		{
			if (0 >= data.curHp)
			{
				fsm.ChangeState(Game.FsmType.Death);
			}
		}
	}

	public override void FsmEvent(FsmUnit unit, Fsm.Event fsmEvent = Fsm.Event.None)
	{
		PerformActor enemy = World.instance.GetNearstAntiActor(data.relationType, pos);
		if (enemy != null)
		{
			if (enemy.pos.x < pos.x || Mathf.Abs(enemy.pos.x - pos.x) < attackRange)
			{
				fsm.ChangeState(Game.FsmType.Idle);
			}
			else
			{
				fsm.ChangeState(Game.FsmType.Run);
			}
		}
		else
		{
			fsm.ChangeState(Game.FsmType.Run);
		}

		fsm.ChangeState(Game.FsmType.Idle);
		if (Game.FsmType.Attack1 == unit.type)
		{
			comboCount = 0;
			isAttackReady = true;
		}
		else if (Game.FsmType.Attack2 == unit.type)
		{
			fsm.ChangeState(Game.FsmType.Run);
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();

		isAttackReady = true;

		index = GameEnum.UserIndex;

		ActionMrUser mr = gameObject.AddComponent<ActionMrUser>();
		mr.actor = this;

		messageReceiver = mr;
	}

	protected override void OnStart()
	{
		base.OnStart();

		//fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack1, "Run", "Attack1", data.attackActions[0]));
		//fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack2, "Run", "Attack1", data.attackActions[0]));
		//fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack3, "Run", "Attack1", data.attackActions[0]));
		//fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack4, "Run", "Attack1", data.attackActions[0]));
		//fsm.AddState(FsmUnit_Attack.CreatePursuitAttack(fsm, Game.FsmType.Attack5, "Run", "Attack1", data.attackActions[0]));

		fsm.AddState(new ActionFsm_Attack(fsm, Game.FsmType.Attack1, "Attack1", data.attackActions[0]));
		fsm.AddState(new ActionFsm_Attack(fsm, Game.FsmType.Attack2, "Attack1", data.attackActions[2]));

		fsm.AddState(new FsmUnitTime(fsm, Game.FsmType.Idle, "Idle", Game.FsmIdleTime));
		fsm.AddState(new FsmUnit_Run(fsm, Game.FsmType.Run, "Run"));
		fsm.AddState(new FsmUnit_Damage(fsm, "Damage"));
		fsm.AddState(new FsmUnit_Death(fsm, "Death"));

		fsm.AddState(new ActionFsm_Dash(fsm, Game.FsmType.Dash, "Run"));
		fsm.AddState(new ActionFsm_AttackDash(fsm, Game.FsmType.AttackDash, "Run"));
		fsm.AddState(new ActionFsm_BackDash(fsm, Game.FsmType.BackDash, "Run"));
		fsm.AddState(new ActionFsm_JumpAttack(fsm, Game.FsmType.JumpAttack, "Attack1", data.attackActions[1]));

		FsmFactor factor = new FsmFactor();

		factor.type = Game.FsmType.Run;
		factor.dir = Game.DirRight;
		factor.moveDir = Game.DirRight;

		fsm.ChangeState(ref factor);
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		// ÇöÀç 
	}

	public void OnTargeting(PerformActor actor)
	{
		target = actor;

		float range = Mathf.Abs(actor.cachedTransform.position.x - cachedTransform.position.x);
		if (range < attackRange)
		{
			if (isAttackReady)
			{
				fsm.ChangeState(Game.FsmType.Attack1);
				isAttackReady = false;
			}
		}
		else if (range < attackDashRange)
		{
			fsm.ChangeState(Game.FsmType.AttackDash);
		}
		else
		{
			fsm.ChangeState(Game.FsmType.Dash);
		}
	}

	public void OnBackDash(bool isPressed)
	{
		this.isPressed = isPressed;
		if (isPressed)
			fsm.ChangeState(Game.FsmType.BackDash);
	}

	public void OnJumpAttack()
	{
		fsm.ChangeState(Game.FsmType.JumpAttack);
	}

	public void OnStrongAttack()
	{
		fsm.ChangeState(Game.FsmType.Attack2);
	}
}
