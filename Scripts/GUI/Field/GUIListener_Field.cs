using UnityEngine;
using System.Collections;

public class GUIListener_Field : MonoBehaviour
{
	enum Step
	{
		None,
		GameStart,
		Start,
		Ready,
		End,
	}

	public SpriteEffect readyEffect;
	public SpriteEffect startEffect;
	public SpriteEffect runeEffect;
	public UISprite runeSprite;
	public SlotMachine slotMachine;

	public InGameManager inGameManager;

	private Step m_step = Step.None;
	private float m_startTime = 0;

	public void Start()
	{
		readyEffect.gameObject.SetActive(false);
		startEffect.gameObject.SetActive(false);
		runeEffect.gameObject.SetActive(false);

		slotMachine.complete = OnCompleteSlotMachine;
	}

	public void FixedUpdate()
	{
		switch (m_step)
		{
			case Step.None:
				if (m_startTime + 0.3f < Time.time)
				{
					readyEffect.Play();
					m_step = Step.Ready;
				}
				break;

			case Step.Ready:
				if (!readyEffect.isPlaying)
				{
					startEffect.Play();
					m_step = Step.Start;
				}
				break;
			case Step.Start:
				if (!startEffect.isPlaying)
				{					
					World.instance.OnMsg(PacketData.OnGameStart.Create(0), true);
					m_step = Step.End;
				}
				break;
		}
	}

	private void OnCompleteSlotMachine(int runeIndex)
	{
		if (0 != runeIndex)
		{
			Rune rune = GameInfo.instance.GetRune(runeIndex);
			runeSprite.atlas = rune.atlas;
			runeSprite.spriteName = rune.spriteName;
			runeSprite.MakePixelPerfect();

			runeEffect.Play();

			inGameManager.SetRune(rune);
		}
	}
}
