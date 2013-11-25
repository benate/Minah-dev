using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class GameInit : MonoBehaviour
{
	public bool isNoCreateActor = true;
	public int testEnemyIndex = 2003;

	void Awake()
	{
		TextAsset asset = Resources.Load("Table/GameData") as TextAsset;
		Stream stream = new MemoryStream(asset.bytes);
		GameData.Loader.Load(stream);
	}

	void Start ()
	{
		Application.targetFrameRate = 30;

		if (!isNoCreateActor)
			return;

		// 슬롯 머신에 셋팅한다
		SlotMachine slotMachine = FindObjectOfType(typeof(SlotMachine)) as SlotMachine;
		slotMachine.SetSlots(GameInfo.instance.runeInfos);

		bool isEquip = false;
		foreach (GameInfo.CharacterInfo info in GameInfo.instance.equipCharacterInfos)
		{
			if (info.index != GameEnum.InvalidIndex)
			{
				isEquip = true;
				break;
			}
		}

		if (isEquip)
		{
			createActor(GameEnum.UserIndex, 1002, new Vector3(250.0f, Game.GroundYPos, 0.0f));

			float x = 200.0f;
			foreach (GameInfo.CharacterInfo info in GameInfo.instance.equipCharacterInfos)
			{
				if (info.index != GameEnum.InvalidIndex)
				{
					createActor(GameEnum.NewIndex, info.index, new Vector3(x, Game.GroundYPos, 0.0f));
					x -= 50.0f;
				}
			}
		}
		else
		{
			{
				//--> tribes
				createActor(GameEnum.UserIndex, 1002, new Vector3(250.0f, Game.GroundYPos, 0.0f));
				createActor(GameEnum.NewIndex, 1002, new Vector3(300.0f, Game.GroundYPos, 0.0f));
				createActor(GameEnum.NewIndex, 1003, new Vector3(200.0f, Game.GroundYPos, 0.0f));
				createActor(GameEnum.NewIndex, 1003, new Vector3(150.0f, Game.GroundYPos, 0.0f));
				createActor(GameEnum.NewIndex, 1003, new Vector3(100.0f, Game.GroundYPos, 0.0f));
				createActor(GameEnum.NewIndex, 1003, new Vector3(50.0f, Game.GroundYPos, 0.0f));
			}
		}
	}

	private PerformActor createActor(long index, int tableindex, Vector3 pos)
	{
		return World.instance.CreateActor(index, tableindex, pos);
	}
}