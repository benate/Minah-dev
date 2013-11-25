using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : Actor
{
	public class Updater
	{
		public Projectile owner = null;

		public virtual void Update() { }
	}

	public int attackPower { set; get; }
	public PerformActor owner { set; get; }
	public GameData.RelationType relationType { set; get; }
	public ProjectileData projectileData { set; get; }

	protected Dictionary<long, long> m_ignoreCollidee = new Dictionary<long, long>();

	protected Updater m_updater = null;

	public static Quaternion GetStartAngleGunRotate(Vector3 currentPos, Vector2 arrivePos, float height)
    {
        Vector3 pos = TranslateFactor_NormalArc.GetPosition(0.01f, ref currentPos, currentPos, currentPos, arrivePos, height, 1);

        return TranslateFactor_NormalArc.GetRotation(pos, currentPos);
    }

	protected override void OnAwake()
	{
		base.OnAwake();
		if (projectileData == null)
			projectileData = GetComponent<ProjectileData>();

	}

	protected override void OnStart()
	{
		base.OnStart();

		if (world == null)
		{
			index = GameEnum.NewIndex;
			world = World.instance;
			world.AddProjectile(this);
		}

		float dir = 1.0f;
		if (owner != null)
		{
			dir = owner.dir;
		}

		switch (projectileData.projectileType)
		{
			case GameData.ProjectileType.Arc:
				{
					Vector3 arrivePos;

					if (Vector3.zero != owner.data.targetPos)
					{
						arrivePos = owner.data.targetPos - owner.pos;
					}
					else
					{
						float startX = Random.Range(400.0f, 800.0f);
						arrivePos = new Vector2(startX * dir, 0.0f);
					}

					float height = Random.Range(projectileData.minHeight, projectileData.maxHeight);
					float time = Random.Range(projectileData.minTimeLength, projectileData.maxTimeLength);
					Quaternion rot = GetStartAngleGunRotate(cachedTransform.position, arrivePos, height);
					cachedTransform.localRotation = rot;
					translater.arcNormal.DoTranslate(time, arrivePos, height, true, 1);
					translater.SetCurrent(translater.arcNormal);
				}
				break;

			case GameData.ProjectileType.Mine:
				{
					translater.inTime.DoTranslate(projectileData.lifeTime);
					translater.SetCurrent(translater.inTime);
					m_updater = new Projectile_MineUpdater();
				}
				break;

			case GameData.ProjectileType.Straight:
				{
					Vector2 projectileDir = projectileData.dir * dir;
					translater.move.DoTranslate(projectileData.speed, projectileDir);
					translater.SetCurrent(translater.move);
				}
				break;
		}

		if (m_updater == null)
		{
			m_updater = new Projectile_BaseUpdater();
		}
		m_updater.owner = this;
	}

	protected override void OnUpdate()
	{
		if (translater.IsCurrent())
		{
			if (translater.IsEnd())
			{
				world.OnFx(projectileData.fxObject, this);

				world.RemoveProjectile(this);
			}
			else
			{
				if (m_updater != null)
				{
					m_updater.Update();
				}
			}
		}
	}

	public void AddIgnoreCollidee(long index1, long index2)
	{
		m_ignoreCollidee.Add(index1, index2);
	}

	#region Check Collision...

	public bool CheckEmemy(ref long emenyIndex, ref PerformActor collidee)
	{
		collidee = CheckCollision(world.enemies);
		if (collidee != null)
		{
			emenyIndex = GameEnum.Enemy1Index;
			return true;
		}

		return false;
	}

	public bool CheckFriend(ref long emenyIndex, ref PerformActor collidee)
	{
		collidee = CheckCollision(world.friends);
		if (collidee != null)
		{
			emenyIndex = GameEnum.UserIndex;
			return true;
		}

		return false;
	}

	public PerformActor CheckCollision(List<PerformActor> actors)
	{
		foreach (PerformActor collidee in actors)
		{
			if (Game.FsmType.Death == collidee.fsm.curFsmType)
				continue;

			if (true == m_ignoreCollidee.ContainsKey(collidee.index))
				continue;

			if (true == Actor.CheckCollision(collidee.cachedBoxCollider, cachedBoxCollider))
			{
				return collidee;
			}
		}
		return null;
	}

	#endregion	
}
