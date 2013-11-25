using UnityEngine;
using System.Collections;

public class ShakeEffect : BaseEffect
{
	enum State
	{
		Left,
		Right,
		Up,
		Down,
		Max,
	}

	public float moveDistance = 4.0f;
	public float playTime = 0.0f;

	private State m_state = State.Left;

	private Vector3 m_startPos = Vector3.zero;

	void Start () 
	{
	
	}
	
	public override void OnUpdate()
	{
		m_progressTime += Time.deltaTime;

		Vector3 pos = m_startPos;
		switch (m_state)
		{
			case State.Left: pos.x -= moveDistance; pos.y += moveDistance; break;
			case State.Right: pos.x += moveDistance; pos.y -= moveDistance; break;
			case State.Up: pos.x += moveDistance; pos.y += moveDistance; break;
			case State.Down: pos.x -= moveDistance; pos.y -= moveDistance; break;
		}

		m_state++;
		if (m_state >= State.Max)
		{
			m_state = State.Left;
		}

		cachedTransform.localPosition = pos;

		if (m_progressTime >= playTime)
		{
			cachedTransform.localPosition = m_startPos;

			if (m_finishFunction != null)
			{
				m_finishFunction(this);
				m_finishFunction = null;
			}
			m_isPlaying = false;
		}
	}

	public override void Play(bool isReverse = false)
	{
		base.Play(isReverse);
		m_startPos = cachedTransform.localPosition;
		m_state = State.Left;
	}

	public override void Play(OnFinished finished, bool isReverse = false)
	{
		base.Play(finished, isReverse);
		m_startPos = cachedTransform.localPosition;
		m_state = State.Left;
	}
}
