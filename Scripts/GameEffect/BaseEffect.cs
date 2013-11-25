using UnityEngine;
using System.Collections;

public class BaseEffect : MonoBehaviour
{
	public delegate void OnFinished(BaseEffect effect);

	protected Transform m_cachedTransform = null;
	public Transform cachedTransform
	{ get { if (m_cachedTransform == null) m_cachedTransform = transform; return m_cachedTransform; } }

	protected bool m_isPlaying = false;
	public bool isPlaying { get { return m_isPlaying; } }

	protected bool m_isReverse = false;

	protected OnFinished m_finishFunction = null;

	protected float m_progressTime = 0.0f;

	void FixedUpdate()
	{
		if (!m_isPlaying)
			return;

		OnUpdate();
	}

	public virtual void OnUpdate() { }

	public virtual void Play(bool isReverse = false)
	{
		m_isReverse = isReverse;
		m_finishFunction = null;
		m_isPlaying = true;
		m_progressTime = 0.0f;
	}

	public virtual void Play(OnFinished finished, bool isReverse = false)
	{
		m_isReverse = isReverse;
		m_finishFunction = finished;
		m_isPlaying = true;
		m_progressTime = 0.0f;
	}

	public virtual void Stop() { m_isPlaying = false; }
}
