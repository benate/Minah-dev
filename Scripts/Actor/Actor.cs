using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : BaseBehaviour
{
	public enum Type
	{
		None,
		Tribe,
		Enemy,
		Creature,
	}

	public long index { protected set; get; }
	public Type type { protected set; get; }
	public World world { protected set; get; }

	public float dir { get { return anim.IsFlipX() ? 1.0f : -1.0f; } }
	public Vector3 pos { get { return cachedTransform.position; } }

	public ActorAnimated anim { protected set; get; }
	public Translater translater { protected set; get; }
	public MessageReceiver messageReceiver { protected set; get; }

	public Transform fxTransform { protected set; get; }

	private bool m_pause = false;
	public bool pause
	{
		set
		{
			m_pause = value;
			translater.pause = m_pause;
		}

		get
		{
			return m_pause;
		}
	}

	protected BoxCollider m_boxCollider;
	public BoxCollider cachedBoxCollider
	{
		get
		{
			if (null == m_boxCollider)
			{
				m_boxCollider = gameObject.GetComponentInChildren<BoxCollider>();
			}

			return m_boxCollider;
		}
	}

	public void Awake()
	{
		anim = gameObject.AddComponent<ActorAnimated>();
		translater = gameObject.AddComponent<Translater>();
		fxTransform = cachedTransform.FindChild("Fx");

		OnAwake();
	}

	public void Start()
	{
		if (anim == null)
			Debug.LogError("No ActorAnimated = " + this);

		if (translater == null)
			Debug.LogError("No Translater = " + this);

		this.pause = false;
		
		OnStart();
	}

	public void FixedUpdate()
	{
		OnUpdate();
	}

	public void OnCreateData(World world, long actorIndex)
	{
		this.world = world;
		this.index = actorIndex;
	}

	public void TurnDir(float dir)
	{
		if (dir > 0.0f)
		{
			TurnRight();
		}
		else if (dir < 0.0f)
		{
			TurnLeft();
		}
	}

	public void TurnLeft()
	{
		if (anim.IsFlipX())
		{
			anim.FlipX();
		}
	}

	public void TurnRight()
	{
		if (!anim.IsFlipX())
		{
			anim.FlipX();
		}
	}

	//--> static func
	public static bool CheckCollision(BoxCollider collider, BoxCollider collidee)
	{
		if (null == collider)
			return false;

		if (null == collidee)
			return false;

		return collider.bounds.Intersects(collidee.bounds);
	}

	protected virtual void OnAwake() { }
	protected virtual void OnStart() { }
	protected virtual void OnUpdate() {	}
}
