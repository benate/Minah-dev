using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
	class SlotItem
	{
		public int index = 0;
		public UISprite sprite = null;

		public static SlotItem Create(int index, UISprite sprite)
		{
			SlotItem item = new SlotItem();
			item.index = index;
			item.sprite = sprite;
			return item;
		}
	}

	class Slot
	{
		public List<SlotItem> items = new List<SlotItem>();
	}

	public const int SlotCount = 3;

	public delegate void OnComplete(int cardIndex);

	public UIButtonMessage slotButton;
	public UIPanel panel;

	public float slotSpeedPerSecond = 1000.0f;
	public float stopTime = 1.0f;

	public Transform slot1;
	public Transform slot2;
	public Transform slot3;

	private int m_isCompleteCount = 0;
	private bool m_isPlaying = false;
	private int m_cardCompleteIndex = 0;
	private float m_height = 0;
	private float m_progressTime = 0;
	private int[] m_resultCards = new int[SlotCount] { 0, 0, 0 };

	private Slot[] m_slots = new Slot[SlotCount] { new Slot(), new Slot(), new Slot() };

	private OnComplete m_complete;
	public OnComplete complete { set { m_complete = value; } }

	private Vector3 m_startPosition;
	public Vector3 startPosition { get { return m_startPosition; } }

	public void Start()
	{
		m_startPosition = transform.localPosition;
	}

	public void FixedUpdate()
	{
		if (m_isPlaying)
		{
			if (m_isCompleteCount == 0)
			{
				AddYPos(0);
			}

			if (m_isCompleteCount <= 1)
			{
				AddYPos(1);
			}

			if (m_isCompleteCount <= 2)
			{
				AddYPos(2);
			}

			m_progressTime += Time.deltaTime;
			if (m_progressTime > stopTime)
			{
				// 해당카드에 맛춰진다
				bool isComplete = false;
				int currentSlotIndex = 0;
				for (int i = 0; i < m_slots[m_isCompleteCount].items.Count; ++i)
				{
					SlotItem rune = m_slots[m_isCompleteCount].items[i];
					if (rune.index == m_resultCards[m_isCompleteCount] &&
						Mathf.Abs(rune.sprite.cachedTransform.localPosition.y) < 10.0f)
					{
						isComplete = true;
						currentSlotIndex = i;
						break;
					}
				}

				if (isComplete)
				{
					// 위치 정렬
					for (int i = 0; i < m_slots[m_isCompleteCount].items.Count; ++i)
					{
						SlotItem item = m_slots[m_isCompleteCount].items[i];
						Vector3 pos = item.sprite.cachedTransform.localPosition;
						int count = Mathf.RoundToInt(pos.y / m_height);
						pos.y = count* m_height;
						item.sprite.cachedTransform.localPosition = pos;
					}

					++m_isCompleteCount;
					m_progressTime = 0.0f;
				}
			}

			if (SlotCount == m_isCompleteCount)
			{
				if (m_complete != null)
					m_complete(m_cardCompleteIndex);

				m_isPlaying = false;
				slotButton.enabled = true;
			}
		}
	}

	public void SetSlots(List<Rune> runes)
	{
		Rune rune = runes[0];
		UIAtlas.Sprite rect = rune.atlas.GetSprite(rune.spriteName);
		m_height = rect.inner.height;
		for (int i = 0; i < runes.Count; ++i)
		{
			rune = runes[i];

			float posY = m_height * (i - runes.Count + 2);

			UISprite sprite1 = NGUITools.AddWidget<UISprite>(gameObject);
			sprite1.cachedTransform.localPosition = new UnityEngine.Vector3(slot1.localPosition.x, posY, slot1.localPosition.z);
			sprite1.gameObject.name = "slot1" + i;
			sprite1.atlas = rune.atlas;
			sprite1.spriteName = rune.spriteName;
			sprite1.MakePixelPerfect();

			UISprite sprite2 = NGUITools.AddWidget<UISprite>(gameObject);
			sprite2.cachedTransform.localPosition = new UnityEngine.Vector3(slot2.localPosition.x, posY, slot2.localPosition.z);
			sprite2.gameObject.name = "slot2" + i;
			sprite2.atlas = rune.atlas;
			sprite2.spriteName = rune.spriteName;
			sprite2.MakePixelPerfect();

			UISprite sprite3 = NGUITools.AddWidget<UISprite>(gameObject);
			sprite3.cachedTransform.localPosition = new UnityEngine.Vector3(slot3.localPosition.x, posY, slot3.localPosition.z);
			sprite3.gameObject.name = "slot3" + i;
			sprite3.atlas = rune.atlas;
			sprite3.spriteName = rune.spriteName;
			sprite3.MakePixelPerfect();

			m_slots[0].items.Add(SlotItem.Create(rune.index, sprite1));
			m_slots[1].items.Add(SlotItem.Create(rune.index, sprite2));
			m_slots[2].items.Add(SlotItem.Create(rune.index, sprite3));
		}
	}

	public void OnClickShot()
	{
		slotButton.enabled = false;
		m_isPlaying = true;
		m_progressTime = 0.0f;
		m_isCompleteCount = 0;

		int ran = Random.Range(0, 10);
		// 70%의 확율로 카드가 맞춰진다
		if (ran < 7)
		{
			m_cardCompleteIndex = Random.Range(1, 4);
			m_resultCards[0] = m_cardCompleteIndex;
			m_resultCards[1] = m_cardCompleteIndex;
			m_resultCards[2] = m_cardCompleteIndex;
		}
		else
		{
			m_cardCompleteIndex = 0;

			while (true)
			{
				m_resultCards[0] = Random.Range(1, 4);
				m_resultCards[1] = Random.Range(1, 4);
				m_resultCards[2] = Random.Range(1, 4);

				// 세개가 같으면 안된다
				if (m_resultCards[0] != m_resultCards[1] || 
					m_resultCards[1] != m_resultCards[2] ||
					m_resultCards[0] != m_resultCards[2])
				{
					break;
				}
			}
		}
	}

	private void AddYPos(int slot)
	{
		bool isChange = false;
		float minY = float.MaxValue, maxY = float.MinValue;
		Transform minTrans = null;
		foreach (SlotItem item in m_slots[slot].items)
		{
			Vector3 pos = item.sprite.cachedTransform.localPosition;
			pos.y -= slotSpeedPerSecond * Time.deltaTime;

			item.sprite.cachedTransform.localPosition = pos;

			if (-panel.clipRange.w * 0.5f > pos.y + item.sprite.cachedTransform.localScale.y * 0.5f)
			{
				isChange = true;
			}

			if (minY > item.sprite.cachedTransform.localPosition.y)
			{
				minY = item.sprite.cachedTransform.localPosition.y;
				minTrans = item.sprite.cachedTransform;
			}

			if (maxY < item.sprite.cachedTransform.localPosition.y)
			{
				maxY = item.sprite.cachedTransform.localPosition.y;
			}
		}

		if (isChange)
		{
			Vector3 pos = minTrans.localPosition;
			pos.y = maxY + m_height;
			minTrans.localPosition = pos;
		}
	}
}
