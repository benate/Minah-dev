using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Directing : BaseBehaviour
{
	public UILabel talkLabel;
	public GameObject slotMachine;
	public Transform slotMachineOnPosition;
	public Transform slotMachineOffPosition;
	public float slotMachineSlideTime = 0.5f;

	public enum Type
	{
		None,
		IdleNTalk,
		CreateWallNEnemies,
		Battle,
		EndIdleNTalk,
		EnemiesRemove,
	}

	protected Dictionary<Type, Unit<Directing>> m_units = new Dictionary<Type, Unit<Directing>>();

	private Unit<Directing> m_currentUnit = null;
	private GameTrigger m_trigger = null;
	public GameTrigger trigger { get { return m_trigger; } }

	private static Directing m_instance;
	public static Directing instance { get { return m_instance;	} }

	public void Awake()
	{
		m_instance = this;

		m_units[Type.IdleNTalk] = new TriggerDirecting.UnitIdleNTalk(this);
		m_units[Type.CreateWallNEnemies] = new TriggerDirecting.UnitCreateWallNEnemies(this);
		m_units[Type.Battle] = new TriggerDirecting.UnitBattle(this);

		m_units[Type.EndIdleNTalk] = new TriggerDirecting.UnitEndIdleNTalk(this);
		m_units[Type.EnemiesRemove] = new TriggerDirecting.UnitEnemiesRemove(this);
	}

	public void Start()
	{
		talkLabel.gameObject.SetActive(false);
	}

	public void Update()
	{
		// 모든 플레이어를 
		if (m_currentUnit != null)
		{
			m_currentUnit.OnUpdate();
		}
	}

	public void ChangeState(Type type)
	{
		//--> focusout
		if (null != m_currentUnit)
		{
			m_currentUnit.FocusOut();
		}

		//--> focusin
		Unit<Directing> currentUnit;
		if (m_units.TryGetValue(type, out currentUnit))
		{
			m_currentUnit = currentUnit;
			m_currentUnit.FocusIn();
		}
		else
		{
			m_currentUnit = null;
		}
	}

	public void StartTriggerDirecting(GameTrigger trigger)
	{
		m_trigger = trigger;
		m_trigger.StartTrigger();
		ChangeState(Type.IdleNTalk);
	}

	public void StartEndDirecting(EndPoint endPoint)
	{
		m_trigger.EnterEndTrigger();
		ChangeState(Type.EndIdleNTalk);
	}
}
