using UnityEngine;
using System.Collections;

public class TKAnimated : ActorAnimated.Animated
{
#if tk2d_v2_00
    private tk2dSprite m_tk2dSprite = null;
    private tk2dSpriteAnimator m_tk2dAnimated = null;

    public static tk2dSprite GetSprite(GameObject gameObject)
    {
        tk2dSprite[] contents = gameObject.GetComponentsInChildren<tk2dSprite>() as tk2dSprite[];
        if (contents.Length > 0)
        {
            return contents[0];
        }
        return null;
    }
    public static tk2dSpriteAnimator GetAnimation(GameObject gameObject)
    {
        tk2dSpriteAnimator[] contents = gameObject.GetComponentsInChildren<tk2dSpriteAnimator>() as tk2dSpriteAnimator[];
        if (contents.Length > 0)
            return contents[0];

        return null;
    }

    public TKAnimated(tk2dSprite tk2dSprited, tk2dSpriteAnimator tk2dAnimated)
    {
        m_tk2dSprite = tk2dSprited;
        m_tk2dAnimated = tk2dAnimated;
    }
#else
    private tk2dAnimatedSprite m_tk2dAnimated = null;

	public static tk2dAnimatedSprite GetAnimation(GameObject gameObject)
	{
		tk2dAnimatedSprite[] contents = gameObject.GetComponentsInChildren<tk2dAnimatedSprite>() as tk2dAnimatedSprite[];
		if (contents.Length > 0)
			return contents[0];

		return null;
	}

	public TKAnimated(tk2dAnimatedSprite tk2dAnimated)
	{
		m_tk2dAnimated = tk2dAnimated;
	}
#endif

	public override string GetCurrentAnimationName()
	{
		return m_tk2dAnimated.CurrentClip.name;
	}

	public override float GetAnimationTime()
	{
		return m_tk2dAnimated.ClipTimeSeconds;
	}

#if tk2d_v2_00
    public override void FlipX() { m_tk2dSprite.FlipX = !m_tk2dSprite.FlipX; }
    public override void FlipY() { m_tk2dSprite.FlipY = !m_tk2dSprite.FlipY; }

    public override bool IsFlipX()
    {
        if (m_tk2dSprite.scale.x > 0.0f)
            return false;

        return true;
    }

    public override bool IsFlipY()
    {
        if (m_tk2dSprite.scale.y > 0.0f)
            return false;

        return true;
    }
#else
    public override void FlipX() { m_tk2dAnimated.FlipX(); }
	public override void FlipY() { m_tk2dAnimated.FlipY(); }

	public override bool IsFlipX()
	{
		if (m_tk2dAnimated.scale.x > 0.0f)
			return false;

		return true;
	}

	public override bool IsFlipY()
	{
		if (m_tk2dAnimated.scale.y > 0.0f)
			return false;

		return true;
	}
#endif

	public override bool IsPlaying() { return m_tk2dAnimated.Playing; }

	public override bool IsPlaying(string animationName)
	{
		return m_tk2dAnimated.IsPlaying(animationName);
	}

	public override void Play() { m_tk2dAnimated.Play(); }

	public override void Play(string animationName) { m_tk2dAnimated.Play(animationName); }

	public override void Stop() { m_tk2dAnimated.Stop(); }

	protected void DelegateEvent(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum)
	{
		if (eventDelegate != null)
			eventDelegate(frame.eventInt);
	}
}
