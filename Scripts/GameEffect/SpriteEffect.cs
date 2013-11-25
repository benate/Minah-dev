using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteEffect : BaseEffect
{ 
	[System.Serializable]
	public class EffectData
	{
		public float playTime = 1.0f;

		public bool isScale = false;
		public float startScale = 1.0f;
		public float endScale = 1.0f;

		public bool isColor = false;
		public Color startColor = Color.white;
		public Color endColor = Color.white;

		public bool isPosition = false;
		public Vector3 startPosition = Vector3.zero;
		public Vector3 endPosition = Vector3.zero;

		public bool isRotate = false;
		public float startRotate = 0.0f;
		public float endRotate = 0.0f;
	}

	[HideInInspector][SerializeField] private UIWidget m_applyWidget;
	public UIWidget applyWidget { get { return m_applyWidget; } set { m_applyWidget = value; } }

	[HideInInspector][SerializeField] private List<EffectData> m_effectDatas = new List<EffectData>();
	public List<EffectData> effectDatas { get { return m_effectDatas; } set { m_effectDatas = value; } }

	[HideInInspector][SerializeField] private bool m_isEndHide = true;
	public bool isEndHide { get { return m_isEndHide; } set { m_isEndHide = value; } }

	protected int m_currentIndex = 0;
	protected Color m_currentColor = Color.white;
	protected Vector3 m_currentPosition = Vector3.zero;
	protected float m_currentRotate = 0.0f;
	protected float m_currentScale = 1.0f;
	protected float m_totalTime = 0.0f;

	private float m_lastTime = 0.0f;

	void Awake()
	{
	}

	void Start()
	{
		SetCurrentFactor(0, 0.0f);
	}

	public override void OnUpdate ()
	{
		if (m_isPlaying && m_effectDatas.Count > 0)
		{
			EffectData data = m_effectDatas[m_currentIndex];

			if (!m_isReverse)
			{
				m_progressTime += Time.deltaTime;

				SetCurrentFactor(m_currentIndex, (m_progressTime - m_lastTime) / data.playTime);

				if (m_lastTime + data.playTime <= m_progressTime)
				{
					m_lastTime += data.playTime;
					++m_currentIndex;
				}

				if (m_totalTime <= m_progressTime)
				{
					if (m_finishFunction != null)
					{
						m_finishFunction(this);
						m_finishFunction = null;
					}
					m_isPlaying = false;

					if (isEndHide)
					{
						gameObject.SetActive(false);
					}
				}
			}
			else
			{
				m_progressTime -= Time.deltaTime;

				SetCurrentFactor(m_currentIndex, 1.0f - (m_lastTime - m_progressTime) / data.playTime);

				if (m_lastTime - data.playTime > m_progressTime)
				{
					m_lastTime -= data.playTime;
					--m_currentIndex;
				}

				if (m_progressTime <= 0.0f)
				{
					if (m_finishFunction != null)
					{
						m_finishFunction(this);
						m_finishFunction = null;
					}
					m_isPlaying = false;

					if (isEndHide)
					{
						gameObject.SetActive(false);
					}
				}
			}
		}
	}

	public override void Play(bool isReverse = false)
	{
		m_isReverse = isReverse;
		m_isPlaying = true;
		m_progressTime = 0.0f;

		gameObject.SetActive(true);

		m_totalTime = 0.0f;
		foreach (EffectData data in m_effectDatas)
			m_totalTime += data.playTime;

		if (isReverse)
		{
			m_currentIndex = m_effectDatas.Count - 1;
			m_lastTime = m_totalTime;
			m_progressTime = m_totalTime;
			SetCurrentFactor(m_currentIndex, 1.0f);
		}
		else
		{
			m_currentIndex = 0;
			m_lastTime = 0.0f;
			m_progressTime = 0.0f;
			SetCurrentFactor(0, 0.0f);
		}
	}

	public override void Play(OnFinished finished, bool isReverse = false)
	{
		m_finishFunction = finished;
		Play(isReverse);
	}

	public override void Stop()
	{
		base.Stop();
		if (m_isReverse)
		{
			SetCurrentFactor(m_effectDatas.Count - 1, 1.0f);
		}
		else
		{
			SetCurrentFactor(0, 0.0f);
		}
		if (isEndHide)
		{
			gameObject.SetActive(false);
		}
	}

	protected virtual void SetCurrentFactor(int current, float factor)
	{
		if (m_effectDatas.Count == 0 || m_effectDatas.Count <= current)
			return;

 		EffectData data = m_effectDatas[current];

		if (data.isScale)
		{
			m_currentScale = Mathf.Lerp(data.startScale, data.endScale, factor);
			cachedTransform.localScale = new Vector3(m_currentScale, m_currentScale);
		}

		if (data.isPosition)
		{
			m_currentPosition = Vector3.Lerp(data.startPosition, data.endPosition, factor);
			cachedTransform.localPosition = m_currentPosition;
		}

		if (data.isRotate)
		{
			m_currentRotate = Mathf.Lerp(data.startRotate, data.endRotate, factor);
			cachedTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_currentRotate);
		}

		if (data.isColor)
		{
			m_currentColor = Color.Lerp(data.startColor, data.endColor, factor);
			if (applyWidget != null)
			{
				applyWidget.color = m_currentColor;
			}
		}
	}
}
