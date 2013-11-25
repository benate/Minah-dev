using UnityEngine;
using System.Collections;

public class ActorAnimated : MonoBehaviour
{
	public abstract class Animated
	{
		public delegate void OnEventDelegate(int customIndex);

		public OnEventDelegate eventDelegate;

		public abstract string GetCurrentAnimationName();
		public abstract float GetAnimationTime();

		public abstract void FlipX();
		public abstract void FlipY();
		public abstract bool IsFlipX();
		public abstract bool IsFlipY();

		public abstract bool IsPlaying();
		public abstract bool IsPlaying(string animationName);

		public abstract void Play();
		public abstract void Play(string animationName);

		public abstract void Stop();
	}

	private Animated m_animated = null;
	void Awake()
	{
		tk2dAnimatedSprite anim = TKAnimated.GetAnimation(gameObject);
		if (anim != null)
		{
#if tk2d_v2_00
            tk2dSprite sprite = TKAnimated.GetSprite(gameObject);
            m_animated = new TKAnimated(sprite, anim);
#else
            m_animated = new TKAnimated(anim);
#endif
		}
		else
		{
			Animation anim2 = BaseAnimated.GetAnimation(gameObject);
			if (anim2 != null)
			{
				m_animated = new BaseAnimated(anim2);
			}
		}
	}

	public string GetCurrentAnimationName() { return m_animated.GetCurrentAnimationName(); }
	public float GetAnimationTime() { return m_animated.GetAnimationTime(); }

	public void FlipX() { m_animated.FlipX(); }
	public void FlipY() { m_animated.FlipY(); }
	public bool IsFlipX() { return m_animated.IsFlipX(); }
	public bool IsFlipY() { return m_animated.IsFlipY(); }
	public bool IsPlaying() { return m_animated.IsPlaying(); }
	public bool IsPlaying(string animationName) { return m_animated.IsPlaying(animationName); }

	public void Play() { m_animated.Play(); }
	public void Play(string animationName) { m_animated.Play(animationName); }

	public void Stop() { m_animated.Stop(); }
}

