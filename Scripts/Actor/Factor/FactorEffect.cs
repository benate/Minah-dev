using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactorEffect
{
	protected int factorValue = 0;

	public FactorEffect(int value)
	{
		factorValue = value;
	}

	public virtual void OnEffect(Factor factor) { }
	public virtual void OffEffect(Factor factor) { }
}

public class FactorEffect_Damage : FactorEffect
{
	public FactorEffect_Damage(int value) : base(value) { }

	public override void OnEffect(Factor factor) 
	{
		int value = factor.owner.data.defence + factorValue;
		if (Game.FsmType.Defence == factor.owner.fsm.curFsmType)
		{
			value += factor.owner.data.defenceBonus;
		}

		if (value < 0)
		{
			factor.owner.data.curHp += value;

			factor.owner.FactorEvent(GameData.FactorEventType.Damage);
			factor.owner.world.OnDamageEffect(value, factor.owner);
		}
	}

	public override void OffEffect(Factor factor) 
	{ 
	}
}

public class FactorEffect_AttackPower : FactorEffect
{
	public FactorEffect_AttackPower(int value) : base(value)
	{		
	}

	public override void OnEffect(Factor factor)
	{
		factor.owner.data.attackPowerBonus += factorValue;
	}

	public override void OffEffect(Factor factor)
	{
		factor.owner.data.attackPowerBonus -= factorValue;
	}
}

public class FactorEffect_DefensePower : FactorEffect
{
	public FactorEffect_DefensePower(int value) : base(value){ }

	public override void OnEffect(Factor factor)
	{
		factor.owner.data.defenceBonus += factorValue;
	}

	public override void OffEffect(Factor factor)
	{
		factor.owner.data.defenceBonus -= factorValue;
	}
}

public class FactorEffect_Heal : FactorEffect
{
	public FactorEffect_Heal(int value) : base(value) { }

	public override void OnEffect(Factor factor)
	{
		factor.owner.data.curHp += factorValue;
		if (factor.owner.data.maxHp < factor.owner.data.curHp)
		{
			factor.owner.data.curHp = factor.owner.data.maxHp;
		}
	}

	public override void OffEffect(Factor factor)
	{
	}
}

public class FactorEffect_Fever : FactorEffect 
{
	public FactorEffect_Fever() : base(0) { }

	public override void OnEffect(Factor factor)
	{
		factor.owner.data.fever++;
	}

	public override void OffEffect(Factor factor)
	{
		factor.owner.data.fever--;

		if (factor.owner.data.fever < 0)
			factor.owner.data.fever = 0;

	}
}