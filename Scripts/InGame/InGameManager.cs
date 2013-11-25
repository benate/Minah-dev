using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGameManager : MonoBehaviour
{
	public class Buff
	{
		public int factor = 0;
		public float durationTime = 0.0f;
		public UIAtlas atlas = null;
		public string spriteName = "";

		public float progressTime = 0.0f;
	}

	public World world;
	public List<UISprite> buffSprites = new List<UISprite>();
	public List<UIFilledSprite> buffFilledSprites = new List<UIFilledSprite>();

	List<Buff> m_buffs = new List<Buff>();

	Dictionary<GameData.RelationType, Unit<InGameManager>> m_units = new Dictionary<GameData.RelationType, Unit<InGameManager>>();

	Unit<InGameManager> m_currentUnit = null;
	GameData.RelationType m_curRelatioinType = GameData.RelationType.None; public GameData.RelationType curRelationType { get { return m_curRelatioinType; } }

	class FriendTurn : Unit<InGameManager>
	{
		CheckTimer timer = new CheckTimer();
		public FriendTurn(InGameManager mgr) : base(mgr) { }

		public override void FocusIn()
		{
			int attackValue = Random.Range(0, 10);
			Game.FsmType attackType = Game.FsmType.None;
			if (8 < attackValue)		attackType = Game.FsmType.Attack5;
			else if (6 < attackValue)	attackType = Game.FsmType.Attack4;
			else if (4 < attackValue)	attackType = Game.FsmType.Attack3;
			else if (2 < attackValue)	attackType = Game.FsmType.Attack2;
			else						attackType = Game.FsmType.Attack1;

			foreach (PerformActor actor in owner.world.friends)
			{
				if (Game.FsmType.Death == actor.fsm.curFsmType)
					continue;

				actor.fsm.ChangeState(attackType);
			}

			timer.SetTimer(1.0f);
		}

		public override void FocusOut()
		{
			timer.Clear();
		}

		public override void OnUpdate()
		{
			timer.OnUpdate();
			if (timer.IsEnd())
			{
				bool nextTurn = true;
				foreach (PerformActor actor in owner.world.friends)
				{
					if (Game.FsmType.Death == actor.fsm.curFsmType)
						continue;

					if (Game.FsmType.Idle != actor.fsm.curFsmType)
					{
						nextTurn = false;
						break;
					}
				}

				if (nextTurn)
				{
					owner.ChangeTurn(GameData.RelationType.Enemy);
				}
				else
				{
					timer.SetTimer(1.0f);
				}
			}
		}
	}

	class EnemyTurn : Unit<InGameManager>
	{
		CheckTimer timer = new CheckTimer();

		public EnemyTurn(InGameManager mgr) : base(mgr) { }

		public override void FocusIn()
		{
			int attackValue = Random.Range(0, 10);
			Game.FsmType attackType = Game.FsmType.None;
			if (8 < attackValue)		attackType = Game.FsmType.Attack5;
			else if (6 < attackValue)	attackType = Game.FsmType.Attack4;
			else if (4 < attackValue)	attackType = Game.FsmType.Attack3;
			else if (2 < attackValue)	attackType = Game.FsmType.Attack2;
			else						attackType = Game.FsmType.Attack1;

			foreach (PerformActor actor in owner.world.enemies)
			{
				if (Game.FsmType.Death == actor.fsm.curFsmType)
					continue;

				actor.fsm.ChangeState(attackType);
			}

			timer.SetTimer(1.0f);
		}

		public override void FocusOut()
		{
			timer.Clear();
		}

		public override void OnUpdate()
		{
			timer.OnUpdate();
			if (timer.IsEnd())
			{
				bool nextTurn = true;
				foreach (PerformActor actor in owner.world.enemies)
				{
					if (Game.FsmType.Death == actor.fsm.curFsmType)
						continue;

					if (Game.FsmType.Idle != actor.fsm.curFsmType)
					{
						nextTurn = false;
						break;
					}
				}

				if (nextTurn)
				{
					owner.ChangeTurn(GameData.RelationType.Friend);
				}
				else
				{
					timer.SetTimer(1.0f);
				}
			}
		}

	}

	void Awake()
	{
		m_units[GameData.RelationType.None] = null;
		m_units[GameData.RelationType.Friend] = new FriendTurn(this);
		m_units[GameData.RelationType.Enemy] = new EnemyTurn(this);
	}

	void Start ()
	{
		MrInGame mrInGame = gameObject.AddComponent<MrInGame>();
		mrInGame.m_owner = this;

		world.AddMessageReceiver(GameEnum.InGameIndex, mrInGame);

		ArrageBuff();
	}
	
	void FixedUpdate ()
	{
		float deltaTime = Time.deltaTime;
		if(null != m_currentUnit)
		{
			m_currentUnit.OnUpdate();
		}

		bool isArrage = false;
		for (int i = 0; i < m_buffs.Count;)
		{
			Buff buff = m_buffs[i];
			buff.progressTime += deltaTime;
			if (buff.progressTime >= buff.durationTime)
			{
				isArrage = true;
				m_buffs.Remove(buff);
			}
			else
			{
				if (buffFilledSprites.Count > i)
				{
					buffFilledSprites[i].fillAmount = 1.0f - buff.progressTime / buff.durationTime;
				}
				++i;
			}
		}

		if (isArrage)
		{
			ArrageBuff();
		}
	}

	public void ChangeTurn(GameData.RelationType type)
	{
		if (m_curRelatioinType == type)
			return;

		if (null != m_currentUnit)
			m_currentUnit.FocusOut();

		m_currentUnit = m_units[type];
		m_curRelatioinType = type;

		if (null != m_currentUnit)
			m_currentUnit.FocusIn();
	}

	public void SetRune(Rune rune)
	{
		foreach(int index in rune.factorIndices)
		{
			GameData.Factor factorData = GameData.Factor.GetData(index);
			if (null != factorData)
			{
				foreach (PerformActor actor in World.instance.friends)
				{					
					actor.AddFactor(Factor.CreateFactor(factorData.UpdateType, factorData.EventType, factorData.Value, factorData.DurationTime));
				}

				if (factorData.DurationTime > 0.0f)
				{
					Buff buff = new Buff();
					buff.durationTime = factorData.DurationTime;
					buff.factor = index;
					UIAtlas atlas = Resources.Load(factorData.AtlasResource, typeof(UIAtlas)) as UIAtlas;
					if (atlas != null)
					{
						buff.atlas = atlas;
					}
					buff.spriteName = factorData.SpriteName;
					m_buffs.Add(buff);
				}
			}
		}

		ArrageBuff();
	}

	public void ArrageBuff()
	{
		for (int i = 0; i < buffSprites.Count; ++i)
		{
			if (m_buffs.Count > i)
			{
				Buff buff = m_buffs[i];
				buffSprites[i].atlas = buff.atlas;
				buffSprites[i].spriteName = buff.spriteName;
				buffSprites[i].MakePixelPerfect();

				buffFilledSprites[i].atlas = buff.atlas;
				buffFilledSprites[i].spriteName = buff.spriteName;
				buffFilledSprites[i].fillAmount = 1.0f;
				buffFilledSprites[i].MakePixelPerfect();
			}
			else
			{
				buffSprites[i].atlas = null;
				buffFilledSprites[i].atlas = null;
			}
		}
	}
}
