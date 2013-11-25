using UnityEngine;
using System.Collections;

public class BaseAnimated : ActorAnimated.Animated
{
	private Animation m_animaton = null;
	private Transform m_tansform = null;
	private bool m_flipX = false;
	private bool m_flipY = false;

	public static Animation GetAnimation(GameObject gameObject)
	{
		Animation[] contents = gameObject.GetComponentsInChildren<Animation>() as Animation[];
		if (contents.Length > 0)
			return contents[0];

		return null;
	}

	public BaseAnimated(Animation animated)
	{
		m_animaton = animated;
		m_tansform = m_animaton.transform;
	}

	public override string GetCurrentAnimationName()
	{
		return m_animaton.clip.name;
	}

	public override float GetAnimationTime()
	{
		return m_animaton.clip.frameRate;
	}

	public override void FlipX()
	{
		m_flipX = !m_flipX;
		m_tansform.localRotation = Quaternion.Euler(0.0f, m_flipX ? 180.0f : 0.0f, m_flipY ? 180.0f : 0.0f);
	}

	public override void FlipY()
	{
		m_flipY = !m_flipY;
		m_tansform.localRotation = Quaternion.Euler(0.0f, m_flipX ? 180.0f : 0.0f, m_flipY ? 180.0f : 0.0f);
	}

	public override bool IsFlipX()
	{
		return m_flipX;
	}

	public override bool IsFlipY()
	{
		return m_flipY;
	}

	public override bool IsPlaying()
	{
		return m_animaton.IsPlaying(m_animaton.clip.name);
	}

	public override bool IsPlaying(string animationName)
	{
		return m_animaton.IsPlaying(animationName);
	}

	public override void Play()
	{
		m_animaton.Play();
	}
	public override void Play(string animationName)
	{
		m_animaton.Play(animationName);
	}

	public override void Stop()
	{
		m_animaton.Stop();
	}

	protected void DelegateEvent()
	{
		//if (eventDelegate != null)
		//    eventDelegate(frame.eventInt);
	}
}
