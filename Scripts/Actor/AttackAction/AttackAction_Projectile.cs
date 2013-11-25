using UnityEngine;
using System.Collections;

public class AttackAction_Projectile : AttackAction
{
	public GameObject projectileObject;

	public override bool Attack()
	{
		int count = 0;
		do 
		{			
			ShotProjectile();

			count++;
			if (count > 5)
				break;

		} while (0 < owner.data.fever);

		return false;
	}

	void ShotProjectile()
	{
		Projectile projectile = owner.world.OnShotProjectile(projectileObject, owner, null);
		if (projectile != null)
		{
			if (0.0f < owner.dir)
				projectile.anim.FlipX();

			projectile.relationType = relationType;
			projectile.owner = owner;
		}
	}
}