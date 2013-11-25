using UnityEngine;
using System.Collections;

public class GUIListener_Camp : MonoBehaviour
{
	public GameObject characterMenu;
	public GameObject collectMenu;
	public GameObject itemshopMenu;

	public UILabel slotLabel1;
	public UILabel slotLabel2;
	public UILabel slotLabel3;
	public UILabel slotLabel4;
	public UILabel slotLabel5;

	private int m_selectedSlot = GameEnum.InvalidIndex;

	public void Start()
	{
		characterMenu.SetActive(false);
		collectMenu.SetActive(false);
		itemshopMenu.SetActive(false);
	}

	public void Update()
	{
	
	}

	public void OnClickCharacter()
	{
		m_selectedSlot = -1;
		GenerateSlotInfo();
		characterMenu.SetActive(true);
		collectMenu.SetActive(false);
	}

	public void OnClickCharacterClose()
	{
		characterMenu.SetActive(false);
	}

	public void OnCharacterSelected1()
	{
		// 선택된 슬롯에 Archer를 넣는다
		if (m_selectedSlot != -1)
		{
			GameInfo.instance.equipCharacterInfos[m_selectedSlot].index = 1002;
			GenerateSlotInfo();
		}
		collectMenu.SetActive(false);
	}

	public void OnCharacterSelected2()
	{
		// 선택된 슬롯에 Defender를 넣는다
		if (m_selectedSlot != -1)
		{
			GameInfo.instance.equipCharacterInfos[m_selectedSlot].index = 1003;
			GenerateSlotInfo();
		}
		collectMenu.SetActive(false);
	}

	public void OnClickCharacterSlot1()
	{
		m_selectedSlot = 0;
		collectMenu.SetActive(true);
	}

	public void OnClickCharacterSlot2()
	{
		m_selectedSlot = 1;
		collectMenu.SetActive(true);
	}

	public void OnClickCharacterSlot3()
	{
		m_selectedSlot = 2;
		collectMenu.SetActive(true);
	}

	public void OnClickCharacterSlot4()
	{
		m_selectedSlot = 3;
		collectMenu.SetActive(true);
	}

	public void OnClickCharacterSlot5()
	{
		m_selectedSlot = 4;
		collectMenu.SetActive(true);
	}

	public void OnClickCollectMenu()
	{
		collectMenu.SetActive(false);
	}

	public void OnClickItemShop()
	{
		itemshopMenu.SetActive(true);
	}

	public void OnClickItemShopClose()
	{
		itemshopMenu.SetActive(false);
	}

	public void OnClickNextScene()
	{
		// 캐릭터가 셋팅되어 있는지 검사!

		bool isStart = false;
		foreach (GameInfo.CharacterInfo info in GameInfo.instance.equipCharacterInfos)
		{
			if (info.index != GameEnum.InvalidIndex)
			{
				isStart = true;
				break;
			}
		}

		// 안되어 있다면 팝업을 띄운다
		if (isStart)
			Application.LoadLevel("GameSelectedScene");
	}

	private void GenerateSlotInfo()
	{
		int index = 0;
		index = GameInfo.instance.equipCharacterInfos[0].index;
		slotLabel1.text = "" + index;

		index = GameInfo.instance.equipCharacterInfos[1].index;
		slotLabel2.text = "" + index;

		index = GameInfo.instance.equipCharacterInfos[2].index;
		slotLabel3.text = "" + index;

		index = GameInfo.instance.equipCharacterInfos[3].index;
		slotLabel4.text = "" + index;

		index = GameInfo.instance.equipCharacterInfos[4].index;
		slotLabel5.text = "" + index;
	}
}
