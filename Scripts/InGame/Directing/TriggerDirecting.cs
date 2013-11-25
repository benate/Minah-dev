using UnityEngine;
using System.Collections;

public class TriggerDirecting
{
	public class UnitIdleNTalk : Unit<Directing>
	{
		private float m_progressTime = 0.0f;
		public UnitIdleNTalk(Directing owner)
			: base(owner) { }

		public override void FocusIn()
		{
			// ��� ĳ���Ͱ� Idle ���·� ��ȯ
			// ��ȭ�� �����Ѵ�
			m_progressTime = 0.0f;

			foreach (PerformActor actor in World.instance.friends)
			{
				actor.fsm.ChangeState(Game.FsmType.Halt);
			}

			foreach (PerformActor actor in World.instance.enemies)
			{
				actor.fsm.ChangeState(Game.FsmType.Halt);
			}

			owner.talkLabel.text = owner.trigger.startTalkString;
			owner.talkLabel.gameObject.SetActive(true);
			owner.talkLabel.gameObject.AddComponent<TypewriterEffect>();

			//TweenAlpha tc = TweenAlpha.Begin(owner.talkLabel.gameObject, 5.0f, 0.0f);
			//tc.from = 1.0f;
			//Debug.Log("UnitIdleNTalk FocusIn");
		}

		public override void FocusOut() { }

		public override void OnUpdate()
		{
			// ��ȭ�� ������ ���� ���·�
			m_progressTime += Time.deltaTime;
			if (m_progressTime > 3.0f)
			{
				owner.ChangeState(Directing.Type.CreateWallNEnemies);
			}
		}
	}

	public class UnitCreateWallNEnemies : Unit<Directing>
	{
		private float m_progressTime = 0.0f;

		public UnitCreateWallNEnemies(Directing owner)
			: base(owner) { }

		public override void FocusIn()
		{
			// ���� ���� �����Ѵ�
			owner.trigger.ExcuteTrigger();

		//	Debug.Log("UnitCreateWallNEnemies FocusIn");
		}

		public override void FocusOut() { }

		public override void OnUpdate()
		{
			// ���� �ö󰡰� 
			// ���� �����Ǵ� ����!
			m_progressTime += Time.deltaTime;
			if (m_progressTime > 3.0f)
			{
				owner.ChangeState(Directing.Type.Battle);
			}
		}
	}

	public class UnitBattle : Unit<Directing>
	{
		public UnitBattle(Directing owner)
			: base(owner) { }

		public override void FocusIn()
		{
			//--> ��������!
			World.instance.OnMsg(PacketData.OnBattle.Create(GameEnum.InGameIndex));

			owner.talkLabel.gameObject.SetActive(false);

			TweenPosition.Begin(owner.slotMachine, owner.slotMachineSlideTime, owner.slotMachineOnPosition.localPosition);

			//Debug.Log("UnitBattle FocusIn");
		}

		public override void FocusOut()
		{			
		}

		public override void OnUpdate() { owner.ChangeState(Directing.Type.None); }
	}

	public class UnitEndIdleNTalk : Unit<Directing>
	{
		private float m_progressTime = 0.0f;
		public UnitEndIdleNTalk(Directing owner)
			: base(owner) { }

		public override void FocusIn()
		{
			//--> ������ ��
			World.instance.OnMsg(PacketData.OffBattle.Create(GameEnum.InGameIndex));

			// ��� ĳ���Ͱ� Idle ���·� ��ȯ
			// ��ȭ�� �����Ѵ�
			m_progressTime = 0.0f;
			foreach (PerformActor actor in World.instance.friends)
			{
				actor.fsm.ChangeState(Game.FsmType.Halt);
			}

			owner.talkLabel.text = owner.trigger.endTalkString;
			owner.talkLabel.gameObject.SetActive(true);
			owner.talkLabel.gameObject.AddComponent<TypewriterEffect>();

			TweenPosition.Begin(owner.slotMachine, owner.slotMachineSlideTime, owner.slotMachineOffPosition.localPosition);

			//Debug.Log("UnitEndIdleNTalk FocusIn");
		}

		public override void FocusOut() { }

		public override void OnUpdate()
		{
			// ��ȭ�� ������ ���� ���·�
			m_progressTime += Time.deltaTime;
			if (m_progressTime > 3.0f)
			{
				owner.ChangeState(Directing.Type.EnemiesRemove);
			}
		}
	}

	public class UnitEnemiesRemove : Unit<Directing>
	{
		public UnitEnemiesRemove(Directing owner)
			: base(owner) { }

		public override void FocusIn()
		{
			if (owner.trigger.isEndGame)
			{
				Application.LoadLevel("GameBaseCampScene");
			}
			else
			{
				owner.talkLabel.gameObject.SetActive(false);

				owner.trigger.EndTrigger();

				foreach (PerformActor actor in World.instance.friends)
				{
					actor.fsm.ChangeState(Game.FsmType.Run);
				}

				owner.ChangeState(Directing.Type.None);
			}
		}

		public override void FocusOut() { }

		public override void OnUpdate() { }
	}
}
