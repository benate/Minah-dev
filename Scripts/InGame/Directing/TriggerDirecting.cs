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
			// 모든 캐릭터가 Idle 상태로 변환
			// 대화를 시작한다
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
			// 대화가 끝나면 다음 상태로
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
			// 벽과 적을 생성한다
			owner.trigger.ExcuteTrigger();

		//	Debug.Log("UnitCreateWallNEnemies FocusIn");
		}

		public override void FocusOut() { }

		public override void OnUpdate()
		{
			// 벽이 올라가고 
			// 적이 생성되는 연출!
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
			//--> 전투시작!
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
			//--> 전투는 끝
			World.instance.OnMsg(PacketData.OffBattle.Create(GameEnum.InGameIndex));

			// 모든 캐릭터가 Idle 상태로 변환
			// 대화를 시작한다
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
			// 대화가 끝나면 다음 상태로
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
