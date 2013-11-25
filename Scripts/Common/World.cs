using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class World : BaseBehaviour
{
	public Transform characterParentNode;
	public Transform projectileParentNode;
	public Transform fxParentNode;
	public GameObject hudObject;
	public List<DamageNumberEffect> damageEffects;

	protected static World ms_instance = null;
	public static World instance { get { return ms_instance; } }

	private MessageDispatcher m_msgDispatcher = new MessageDispatcher();

	private ActorCreator m_creator = new ActorCreator();
	private ProjectileManager m_projectileManager = new ProjectileManager();
	private FxManager m_fxManager = new FxManager();

	private Dictionary<long, PerformActor> m_actors = new Dictionary<long, PerformActor>(); public Dictionary<long, PerformActor> actors { get { return m_actors; } }
	private List<PerformActor> m_friendList = new List<PerformActor>(); public List<PerformActor> friends { get { return m_friendList; } }
	private List<PerformActor> m_enemyList = new List<PerformActor>();  public List<PerformActor> enemies { get { return m_enemyList; } }

	void Awake()
	{
		ms_instance = this;

		m_creator.world = this;
		m_projectileManager.world = this;
		m_fxManager.world = this;

		MrWorld reciver = gameObject.AddComponent<MrWorld>();

		if (reciver != null)
			m_msgDispatcher.AddMessageReceiver(GameEnum.WorldIndex, reciver);

		DontDestroyOnLoad(this);
	}

	void Start()
	{
	}

	void Update()
	{
		m_msgDispatcher.UpdateMessages();
	}

	public void AddActor(long actorIndex, PerformActor actor)
	{
		m_actors[actorIndex] = actor;
		if (actor.messageReceiver != null)
		{
			m_msgDispatcher.AddMessageReceiver(actorIndex, actor.messageReceiver);
		}

		if (GameData.RelationType.Friend == actor.data.relationType)
		{
			m_friendList.Add(actor);
		}

		if (GameData.RelationType.Enemy == actor.data.relationType)
		{
			m_enemyList.Add(actor);
		}
	}

	public void AddMessageReceiver(long index, MessageReceiver receiver)
	{
		m_msgDispatcher.AddMessageReceiver(index, receiver);
	}

	public GameObject AddHPBar(GameObject origin)
	{
		return NGUITools.AddChild(hudObject, origin);
	}

	public void AddProjectile(Projectile projectile)
	{
		m_projectileManager.Add(projectile);
	}

	public PerformActor CreateActor(long actorIndex, int tableIndex, Vector3 pos)
	{
		PerformActor actor = m_creator.CreateActor(actorIndex, tableIndex, pos);
		if (actor != null)
		{
			this.AddActor(actor.index, actor);
		}

		return actor;
	}

	public PerformActor GetActor(long actorIndex)
	{
		return this.getActor(m_actors, actorIndex);
	}

	public List<PerformActor> GetAntiActors(GameData.RelationType type)
	{
		if (GameData.RelationType.Enemy == type)
		{
			return m_friendList;
		}
		else if (GameData.RelationType.Friend == type)
		{
			return m_enemyList;
		}

		return null;
	}

	public PerformActor GetNearstAntiActor(GameData.RelationType type, Vector3 pos)
	{
		List<PerformActor> actors = null;
		if (GameData.RelationType.Enemy == type)
		{
			actors = m_friendList;
		}
		else if (GameData.RelationType.Friend == type)
		{
			actors = m_enemyList;
		}

		float dis = float.MaxValue;
		PerformActor ret = null;
		foreach (PerformActor actor in actors)
		{
			float diff = Mathf.Abs(actor.pos.x - pos.x);
			if (dis > diff)
			{
				dis = diff;
				ret = actor;
			}
		}

		return ret;
	}

	public PerformActor GetNearstAntiActor(GameData.RelationType type, float finderDir, ref Vector3 finderPos)
	{
		GameData.RelationType findType = GameData.RelationType.None;

		findType = type == GameData.RelationType.Friend ? GameData.RelationType.Enemy : GameData.RelationType.Friend;

		if (GameData.RelationType.None != findType)
		{
			if (GameData.RelationType.Friend == findType)
			{
				return this.getNearstActor(ref finderPos, finderDir, m_friendList);
			}
			else if (GameData.RelationType.Enemy == findType)
			{			
				return this.getNearstActor(ref finderPos, finderDir, m_enemyList);
			}
		}

		return null;
	}

	public PerformActor GetFrontAntiActor(GameData.RelationType type)
	{
		GameData.RelationType findType = GameData.RelationType.None;

		findType = type == GameData.RelationType.Friend ? GameData.RelationType.Enemy : GameData.RelationType.Friend;

		if (GameData.RelationType.None != findType)
		{
			if (GameData.RelationType.Friend == findType)
			{
				return this.getFrontActor(Game.DirLeft, float.MinValue, friends);
			}
			else if (GameData.RelationType.Enemy == findType)
			{
				return this.getFrontActor(Game.DirRight, float.MaxValue, enemies);
			}
		}

		return null;
	}

	public void OnMsg(PacketData.Message msg, bool isBroadcast = false)
	{
		if (isBroadcast)
		{
			m_msgDispatcher.OnMessageBroadCast(msg);
		}
		else
		{
			m_msgDispatcher.OnMessage(msg);
		}
	}

	public Fx OnFx(GameObject origin, Actor trigger)
	{
		return m_fxManager.CreateFx(origin, trigger);
	}

	public void OnDamageEffect(int damage, PerformActor trigger)
	{
		GameObject go = GameObject.Instantiate(Resources.Load("Fx/DamageNumberEffect")) as GameObject;
		DamageNumberEffect effect = go.GetComponent<DamageNumberEffect>();
		effect.cachedTransform.parent = hudObject.transform;
		effect.cachedTransform.localPosition = Vector3.zero;
		effect.cachedTransform.localScale = Vector3.one;
		effect.Play(trigger.fxTransform, damage);
	}

	public Projectile OnShotProjectile(int tableIndex, Actor owner, Actor target)
	{
		return m_projectileManager.Shot(tableIndex, owner, target);
	}

	public Projectile OnShotProjectile(GameObject origin, Actor owner, Actor target)
	{
		return m_projectileManager.Shot(origin, owner, target);
	}

	public void RemoveActor(long actorIndex)
	{
		this.removeActor(m_actors, actorIndex);
	}

	public void RemoveProjectile(Projectile obj)
	{
		m_projectileManager.RemoveProjectile(obj);
	}

	public void RemoveFx(Fx fx)
	{
		m_fxManager.RemoveFx(fx);
	}	

	private PerformActor getActor(Dictionary<long, PerformActor> dic, long actorIndex)
	{
		if (null != dic)
		{
			PerformActor outActor = null;
			if (dic.TryGetValue(actorIndex, out outActor))
			{
				return outActor;
			}
		}

		return null;
	}

	private PerformActor getFrontActor(float dir, float dis, List<PerformActor> actors)
	{
		PerformActor resultActor = null;
		foreach (PerformActor actor in actors)
		{
			if (Game.FsmType.Death == actor.fsm.curFsmType)
				continue;

			if (Game.DirLeft == dir)
			{
				if (dis < actor.pos.x)
				{
					dis = actor.pos.x;
					resultActor = actor;
				}
			}
			else if (Game.DirRight == dir)
			{
				if (dis > actor.pos.x)
				{
					dis = actor.pos.x;
					resultActor = actor;
				}
			}
		}

		return resultActor;
	}

	private PerformActor getNearstActor(ref Vector3 finderPos, float finderDir, List<PerformActor> actors)
	{
		float dis = float.MaxValue;
		float compareDis = 0.0f;
		PerformActor resultActor = null;
		foreach (PerformActor actor in actors)
		{
			if (Game.FsmType.Death == actor.fsm.curFsmType)
				continue;

			if (Game.DirLeft == finderDir)
			{
				if (0.0f > finderPos.x - actor.pos.x)
				{
					continue;
				}
			}

			if (Game.DirRight == finderDir)
			{
				if (0.0f < finderPos.x - actor.pos.x)
				{
					continue;
				}
			}

			compareDis = (finderPos - actor.pos).magnitude;
			if (dis >= compareDis)
			{
				dis = compareDis;
				resultActor = actor;
			}
		}

		return resultActor;
	}

	private void removeActor(Dictionary<long, PerformActor> dic, long actorIndex)
	{
		PerformActor actor = getActor(dic, actorIndex);
		if (actor != null)
		{
			if (GameData.RelationType.Friend == actor.data.relationType)
			{
				m_friendList.Remove(actor);
			}

			if (GameData.RelationType.Enemy == actor.data.relationType)
			{
				m_enemyList.Remove(actor);
			}

			if (actor.messageReceiver != null)
			{
				m_msgDispatcher.RemoveMessageReceiver(actorIndex);
			}

			dic.Remove(actorIndex);
			Destroy(actor.gameObject);
		}
	}
}
