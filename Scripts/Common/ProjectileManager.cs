using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileManager
{
	public World world { set; protected get; }

	private Dictionary<long, Projectile> m_projectiles = new Dictionary<long, Projectile>();

	public void Add(Projectile projectile)
	{
		if (world.fxParentNode != null)
			projectile.cachedTransform.parent = world.fxParentNode;

		m_projectiles.Add(projectile.index, projectile);
	}

	public Projectile Shot(int tableIndex, Actor owner, Actor target)
	{
		GameData.Projectile projectileData = GameData.Projectile.GetData(tableIndex);
		if (projectileData != null)
		{			
			Projectile projectile = CreateProjectile(projectileData, owner, target);
			if (world.projectileParentNode != null)
				projectile.cachedTransform.parent = world.projectileParentNode;

			m_projectiles.Add(projectile.index, projectile);
			return projectile;
		}

		return null;
	}

	public Projectile Shot(GameObject origin, Actor owner, Actor target)
	{
		Vector3 pos = new Vector3(owner.pos.x, owner.pos.y, 0.0f);

		GameObject go = GameObject.Instantiate(origin, pos, Quaternion.identity) as GameObject;
		Projectile projectile = go.GetComponent<Projectile>();
		projectile.OnCreateData(world, GameEnum.NewIndex);

		if (world.projectileParentNode != null)
			projectile.cachedTransform.parent = world.projectileParentNode;

		m_projectiles.Add(projectile.index, projectile);
		return projectile;
	}

	private Projectile CreateProjectile(GameData.Projectile projectileData, Actor owner, Actor target)
	{
		Vector3 pos = new Vector3(owner.pos.x, owner.pos.y, 0.0f);

		GameObject go = GameObject.Instantiate(Resources.Load(projectileData.ResourcePath), pos, Quaternion.identity) as GameObject;
		Projectile projectile = go.GetComponent<Projectile>();		
		projectile.OnCreateData(world, GameEnum.NewIndex);
		return projectile;
	}

	public void RemoveProjectile(Projectile projectile)
	{
		m_projectiles.Remove(projectile.index);

		GameObject.Destroy(projectile.gameObject);
	}

}
