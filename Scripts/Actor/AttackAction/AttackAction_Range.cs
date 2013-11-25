using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAction_Range : AttackAction
{
	public GameData.FactorUpdateType updateType;
	public GameData.FactorEventType eventType;

	public override bool isCollisionContinuous { get { return true; } }

	public override bool Attack()
	{
		List<PerformActor> actors = null;
		if (GameData.RelationType.Friend == relationType)
		{			
			actors = World.instance.enemies;
		}
		else if (GameData.RelationType.Enemy == relationType)
		{
			actors = World.instance.friends;
		}

		if (null == actors)
			return false;

		bool isSuccess = false;
		foreach (PerformActor collidee in actors)
		{
			if (Game.FsmType.Death == collidee.fsm.curFsmType)
				continue;

			if (true == Actor.CheckCollision(collidee.cachedBoxCollider, owner.cachedBoxCollider))
			{
				isSuccess = true;
				if (collisionFxObject != null)
					owner.world.OnFx(collisionFxObject, owner);

				Factor factor = Factor.CreateFactor(updateType, eventType, owner.data.attackPowerInTotal);

				owner.world.OnMsg(PacketData.OnDamage.Create(collidee.index, owner, factor), false);
			}
		}

		return isSuccess;
	}
}