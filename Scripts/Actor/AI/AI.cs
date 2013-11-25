using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI
{
	public abstract class Unit
	{
		public AI ai { private set; get; }
		public int type { private set; get; }
		public PerformActor actor { get { return ai.actor; } }

		private Unit() { }
		public Unit(AI ai, int type) { this.ai = ai; this.type = type; }

		public abstract void FocusIn();
		public abstract void FocusOut();
		public abstract void FsmEvent(FsmUnit unit);
		public abstract void Update();
	}

	public PerformActor actor { protected set; get; }

	protected Dictionary<int, Unit> m_units = new Dictionary<int, Unit>();

	protected Unit m_currentAIUnit = null;

	private AI() { }
	public AI(PerformActor actor) { this.actor = actor; }

	public void AddState(Unit unit)
	{
		m_units.Add(unit.type, unit);
	}

	public void ChangeState(int type)
	{
		if (null != m_currentAIUnit)
		{
			m_currentAIUnit.FocusOut();
		}

		//--> focusin
		Unit unit = null;
		if (m_units.TryGetValue(type, out unit))
		{
			m_currentAIUnit = unit;
			m_currentAIUnit.FocusIn();
		}
	}

	public void FsmEvent(FsmUnit unit)
	{
		if (m_currentAIUnit != null)
			m_currentAIUnit.FsmEvent(unit);
	}

	public void Update()
	{
		if (m_currentAIUnit != null)
			m_currentAIUnit.Update();
	}
}
