using UnityEngine;
using System.Collections;

public class TranslateFactor
{
	public Transform ownerTransform { protected set; get; }

	protected bool m_isPlay = false;

	protected float m_durationTime = float.MaxValue;
	protected float m_progressTime = 0.0f;

	public void Init(Transform transform)
	{
		ownerTransform = transform;
		OnInit();
	}

	public void Clear()
	{
		OnEnd();

		m_durationTime = float.MaxValue;
		m_progressTime = 0.0f;

		OnClear();
	}

	public bool IsEnd()
	{
		return !m_isPlay;
	}

	public void Update()
	{
		OnUpdate();
	}

	protected virtual void OnInit() { }
	protected virtual void OnClear() { }
	protected virtual void OnUpdate()
	{
		if (!IsEnd())
		{
			float lerp = LerpByTime();
			if (lerp >= 1.0f)
				OnEnd();
		}
	}

	protected void doTranslate(float durationTime)
	{
		m_durationTime = durationTime;
		m_progressTime = 0.0f;	

		OnStart();	
	}

	protected void OnStart()
	{
		m_isPlay = true;
	}

	protected void OnEnd()
	{
		m_isPlay = false;
	}

	protected float LerpByTime()
	{
		if (0.0f == m_durationTime)
			return 0.0f;

		m_progressTime += Time.deltaTime;

		float lerp = m_progressTime / m_durationTime;
		if (1.0f <= lerp)
		{
			lerp = 1.0f;
		}

		return lerp;
	}
};

public class TranslateFactor_Float : TranslateFactor
{
	protected float m_from = 0.0f;
	protected float m_to = 0.0f;

	public void DoTranslate(float from, float to, float durationTime)
	{
		base.doTranslate(durationTime);

		m_from = from;
		m_to = to;
	}

	protected override void OnClear()
	{
		base.OnClear();

		m_from = 0.0f;
		m_to = 0.0f;
	}
}

public class TranslateFactor_Vector3 : TranslateFactor
{
	protected Vector3 m_from = Vector3.zero;
	protected Vector3 m_to = Vector3.zero;

	public void DoTranslate(ref Vector3 from, ref Vector3 to, float durationTime)
	{
		base.doTranslate(durationTime);

		m_from = from;
		m_to = to;
	}
	
	protected override void OnClear()
	{
		base.OnClear();

		m_from = Vector3.zero;
		m_to = Vector3.zero;
	}
}