using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NumberEffect : MonoBehaviour
{
	public enum Aligned
	{
		Left,
		Center,
		Right,
	}
	
	public string m_numOfSpriteName0;
	public string m_numOfSpriteName1;
	public string m_numOfSpriteName2;
	public string m_numOfSpriteName3;
	public string m_numOfSpriteName4;
	public string m_numOfSpriteName5;
	public string m_numOfSpriteName6;
	public string m_numOfSpriteName7;
	public string m_numOfSpriteName8;
	public string m_numOfSpriteName9;
	public string m_dotOfSpriteName;	

	[HideInInspector][SerializeField] private int m_startNumber;
	[HideInInspector][SerializeField] private int m_cipher;
	[HideInInspector][SerializeField] private UIAtlas m_atlas;
	[HideInInspector][SerializeField] private Aligned m_aligned = Aligned.Right;

	private int m_targetNumber = 0;
	private int m_currentNumber = 0;
	private bool m_isUpdate = false;
	private Vector2 m_size = Vector2.zero;
	private bool m_isDot = false;

	private List<UISprite> m_sprites = new List<UISprite>();
	private List<string> m_numOfSpriteNames = new List<string>();

	public int cipher { get { return m_cipher; } set { m_cipher = value; } }
	public int startNumber { get { return m_startNumber; } set { m_startNumber = value; } }
	public UIAtlas atlas { get { return m_atlas; } set { m_atlas = value; } }
	public Aligned aligned { get { return m_aligned; } set { m_aligned = value; } }

	public int arriveNumber { get { return m_targetNumber; } set { m_targetNumber = value; } }

	void Awake()
	{
		m_numOfSpriteNames.Add(m_numOfSpriteName0);
		m_numOfSpriteNames.Add(m_numOfSpriteName1);
		m_numOfSpriteNames.Add(m_numOfSpriteName2);
		m_numOfSpriteNames.Add(m_numOfSpriteName3);
		m_numOfSpriteNames.Add(m_numOfSpriteName4);
		m_numOfSpriteNames.Add(m_numOfSpriteName5);
		m_numOfSpriteNames.Add(m_numOfSpriteName6);
		m_numOfSpriteNames.Add(m_numOfSpriteName7);
		m_numOfSpriteNames.Add(m_numOfSpriteName8);
		m_numOfSpriteNames.Add(m_numOfSpriteName9);

		for (int i = 0; i < m_cipher; ++i)
		{
			UISprite sprite = NGUITools.AddWidget<UISprite>(this.gameObject);
			sprite.name = sprite.name + " (" + i + ")";
			sprite.atlas = atlas;
			sprite.spriteName = m_numOfSpriteNames[0];
			sprite.pivot = UIWidget.Pivot.Center;
			sprite.MakePixelPerfect();
			sprite.gameObject.SetActive(false);
			m_sprites.Add(sprite);
		}

		if (m_dotOfSpriteName.Length > 0)
		{
			int numOfDot = m_cipher / 3;
			m_isDot = true;
			for (int i = 0; i < numOfDot; ++i)
			{
				UISprite sprite = NGUITools.AddWidget<UISprite>(this.gameObject);
				sprite.name = string.Format("{0} (dot{1})", sprite.name, i);
				sprite.atlas = atlas;
				sprite.spriteName = m_dotOfSpriteName;
				sprite.pivot = UIWidget.Pivot.Center;
				sprite.MakePixelPerfect();
				sprite.gameObject.SetActive(false);
				m_sprites.Add(sprite);
			}
		}

		if (cipher > 0)
		{
			m_size.x = (int)(m_atlas.texture.width * m_sprites[0].outerUV.width);
			m_size.y = (int)(m_atlas.texture.height * m_sprites[0].outerUV.height);
		}

		Debug.Log("Awake = " + this);
	}

	void Start ()
	{
		Transform baseObj = transform.FindChild("Base");
		if (baseObj != null && baseObj.gameObject != null)
			baseObj.gameObject.SetActive(false);

		SetNumber(m_startNumber);
	}
	
	void FixedUpdate ()
	{
		////////////////////////////////////////////////////////////////////////
		// Test
		if (Input.GetKeyDown(KeyCode.F1))
		{
		    StartCount(1234567);
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
		    Pause();
		}
		if (Input.GetKeyDown(KeyCode.F3))
		{
		    Stop();
		}
		////////////////////////////////////////////////////////////////////////

		if (m_isUpdate)
		{
			if (m_currentNumber >= m_targetNumber)
				m_isUpdate = false;

			SetNumber(m_currentNumber);

			int add = 1;
			int gap = Mathf.Abs(m_currentNumber - m_targetNumber);
			if (gap > 10) add = 2;
			if (gap > 100) add = 13;
			if (gap > 1000) add = 222;
			if (gap > 10000) add = 2222;
			if (gap > 100000) add = 13131;
			if (m_currentNumber < m_targetNumber) m_currentNumber += add;
			else m_currentNumber -= add;
		}
	}

	public void SetColor(Color color)
	{
		UISprite sprite = null;
		for (int i = 0; i < m_sprites.Count; ++i)
		{
			sprite = m_sprites[i];
			sprite.color = color;
		}
	}

	public void StartCount(int count)
	{
		m_isUpdate = true;
		m_currentNumber = 0;
		m_targetNumber = count;
	}

	public void StartCount(int startCount, int targetCount)
	{
		m_isUpdate = true;
		m_currentNumber = startCount;
		m_targetNumber = targetCount;
	}

	public void Pause()
	{
		m_isUpdate = false;
	}

	public void Stop()
	{
		m_isUpdate = false;
		SetNumber(m_targetNumber);
	}

	public void SetNumber(int num)
	{
		int numSlice = num;
		int showSpriteCount = 1;
		int curNum = 0;

		for (int i = 0; i < m_sprites.Count; ++i)
		{
			if (m_isDot && i != 0 && (i + 1) % 4 == 0)
			{
				m_sprites[i].spriteName = m_dotOfSpriteName;
			}
			else
			{
				if (numSlice > 0)
				{
					curNum = numSlice % 10;
					numSlice = numSlice / 10;
				}

				m_sprites[i].spriteName = m_numOfSpriteNames[curNum];
			}

			if (numSlice == 0)
				break;

			++showSpriteCount;
		}

		float startX = 0.0f;
		switch (m_aligned)
		{
		    case Aligned.Center: { startX = (showSpriteCount - 1) * m_size.x * 0.5f; } break;
			case Aligned.Left: { startX = (showSpriteCount - 1) * m_size.x; } break;
		}

		float addX = -m_size.x;

		Vector3 p;
		UISprite sprite = null;
		bool isVisible = false;
		for (int i = 0; i < m_sprites.Count; ++i)
		{
			sprite = m_sprites[i];
			p = sprite.cachedTransform.localPosition;

			if (showSpriteCount > i)
				isVisible = true;
			else
				isVisible = false;

			sprite.gameObject.SetActive(isVisible);
			sprite.cachedTransform.localPosition = new Vector3(startX, p.y, p.z);

			startX += addX;
		}
	}
}
