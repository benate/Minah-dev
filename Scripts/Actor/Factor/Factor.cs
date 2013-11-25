using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Factor
{
	public PerformActor owner = null;

	public FactorEffect effect = null;
	public FactorUpdater updater = null;

	public bool isEnd { get; set; }

	public virtual void Init(PerformActor owner) 
	{
		this.owner = owner;
		this.isEnd = false;		
		
		this.updater.Init(this);
	}

	public virtual void OnUpdate()
	{
		if (null != updater)
		{
			updater.OnUpdate(this);
		}
	}

	#region static funcs..

	public static Factor CreateFactor(GameData.FactorUpdateType updateType, GameData.FactorEventType eventType, int factorValue = 0, float durationTime = 0.0f)
	{
		Factor factor = new Factor();
		if (null != factor)
		{
			switch (updateType)
			{
				case GameData.FactorUpdateType.OnShot: factor.updater = new FactorUpdater_OnShot(); break;
				case GameData.FactorUpdateType.Dot: factor.updater = new FactorUpdater_Dot(durationTime); break;
				case GameData.FactorUpdateType.Timer: factor.updater = new FactorUpdater_Timer(durationTime); break;
			}

			switch (eventType)
			{
				case GameData.FactorEventType.Damage: factor.effect = new FactorEffect_Damage(factorValue); break;
				case GameData.FactorEventType.Heal: factor.effect = new FactorEffect_Heal(factorValue); break;
				case GameData.FactorEventType.Defence: factor.effect = new FactorEffect_DefensePower(factorValue); break;
				case GameData.FactorEventType.AttackPower: factor.effect = new FactorEffect_AttackPower(factorValue); break;
				case GameData.FactorEventType.Fever: factor.effect = new FactorEffect_Fever(); break;
			}
		}

		return factor;
	}

#endregion
}