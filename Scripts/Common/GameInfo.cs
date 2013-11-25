using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour
{
	private static GameInfo m_instance = null;
	public static GameInfo instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = Object.FindObjectOfType(typeof(GameInfo)) as GameInfo;

				if (m_instance == null)
				{
					GameObject go = new GameObject("GameInfo");
					DontDestroyOnLoad(go);
					m_instance = go.AddComponent<GameInfo>();
				}
			}
			return m_instance;
		}
	}

	public class CharacterInfo
	{
		public int index = GameEnum.InvalidIndex;
	}

	public class ItemInfo
	{
		public int index = GameEnum.InvalidIndex;
	}

	private List<CharacterInfo> m_haveCharacterInfos = new List<CharacterInfo>();
	public List<CharacterInfo> haveCharacterInfos { get { return m_haveCharacterInfos; } }

	private List<CharacterInfo> m_equipCharacterInfos = new List<CharacterInfo>();
	public List<CharacterInfo> equipCharacterInfos { get { return m_equipCharacterInfos; } }

	private List<ItemInfo> m_itemInfos = new List<ItemInfo>();
	public List<ItemInfo> itemInfos { get { return m_itemInfos; } }

	private List<Rune> m_runeInfos = new List<Rune>();
	public List<Rune> runeInfos { get { return m_runeInfos; } }

	void Awake()
	{
		for (int i = 0; i < GameEnum.MaxCharacterSlotCount; ++i)
			equipCharacterInfos.Add(new CharacterInfo());

		// 임시
		GameData.Rune runeData;
		Rune rune;

		runeData = GameData.Rune.GetData(1);
		rune = Resources.Load(runeData.ResourcePath, typeof(Rune)) as Rune;
		rune.index = runeData.Index;
		m_runeInfos.Add(rune);

		runeData = GameData.Rune.GetData(2);
		rune = Resources.Load(runeData.ResourcePath, typeof(Rune)) as Rune;
		rune.index = runeData.Index;
		m_runeInfos.Add(rune);

		runeData = GameData.Rune.GetData(3);
		rune = Resources.Load(runeData.ResourcePath, typeof(Rune)) as Rune;
		rune.index = runeData.Index;
		m_runeInfos.Add(rune);
	}

	void Start ()
	{
		// 저장파일을 가져온다
	}

	void OnDestroy()
	{
		// 저장!
	}

	public Rune GetRune(int index)
	{
		foreach (Rune rune in m_runeInfos)
		{
			if (rune.index == index)
				return rune;
		}
		return null;
	}
}
