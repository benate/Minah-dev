using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerformActor : Actor
{	
	public Fsm fsm { protected set; get; }
	public ActorData data { protected set; get; }	

	private List<Factor> m_factors = new List<Factor>();

	public void AddFactor(Factor factor)
	{
		if (null == factor)
			return;

		factor.Init(this);

		m_factors.Add(factor);
	}

	public virtual void FactorEvent(GameData.FactorEventType eventType) { }	
	public virtual void FsmEvent(FsmUnit unit, Fsm.Event fsmEvent = Fsm.Event.None) { }	

	protected override void OnAwake()
	{
		base.OnAwake();

		fsm = gameObject.AddComponent<Fsm>();
		fsm.actor = this;

		if (data == null)
			data = gameObject.GetComponent<ActorData>();

		AttackAction[] actions = GetComponentsInChildren<AttackAction>();
		for (int i = 0; i < actions.Length; ++i)
		{
			AttackAction act = actions[i];
			if (act == null)
				break;

			act.Init(this);

			data.attackActions.Add(act);
		}
	}

	protected override void OnStart()
	{
		base.OnStart();

		if (fsm == null)
			Debug.LogError("No fsm = " + this);

		if (data == null)
			Debug.LogError("No ActorData = " + this);

		if (data != null)
			data.startPos = cachedTransform.position;

		if (world == null)
		{
			world = World.instance;
			world.AddActor(index, this);
		}

		HpBar hpBar = GetComponentInChildren<HpBar>();
		if (hpBar != null)
			hpBar.OnInit();
	}

	protected override void OnUpdate()
	{
		if (true == pause)
			return;

		int count = 0;
		while (count < m_factors.Count)
		{
			Factor factor = m_factors[count];
			if (true == factor.isEnd)
			{
				m_factors.Remove(factor);
				continue;
			}
			else
			{
				factor.OnUpdate();
			}

			++count;
		}
	}	
}
