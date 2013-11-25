using UnityEngine;
using System.Collections;

public class Projectile_MineUpdater : Projectile.Updater
{
	public override void Update()
	{
		long msgIndex = 0;
		bool isCollision = false;
		PerformActor collidee = null;

		switch (owner.relationType)
		{
			case GameData.RelationType.None:
				{
					isCollision = owner.CheckEmemy(ref msgIndex, ref collidee);
					if (!isCollision)
					{
						isCollision = owner.CheckFriend(ref msgIndex, ref collidee);
					}
				}
				break;
			case GameData.RelationType.Friend:
				{
					isCollision = owner.CheckEmemy(ref msgIndex, ref collidee);
				}
				break;
			case GameData.RelationType.Enemy:
				{
					isCollision = owner.CheckFriend(ref msgIndex, ref collidee);
				}
				break;
		}

		if (isCollision)
		{
			owner.AddIgnoreCollidee(collidee.index, collidee.index);

			owner.world.OnFx(owner.projectileData.fxObject, owner);

			int attackPower = owner.projectileData.attackPowerInTotal + owner.owner.data.attackPowerInTotal;

			Factor factor = Factor.CreateFactor(owner.projectileData.updateType, owner.projectileData.eventType, attackPower);
			owner.world.OnMsg(PacketData.OnDamage.Create(collidee.index, owner.owner, factor), false);
		}
	}
}