using UnityEngine;
using System.Collections;

public class GameInput_Mobile : GameInputUpdater
{	
	bool m_bSlideBegin = false;
	bool m_bSlidePost = false;
	
	Vector2 m_slidePos = Vector2.zero;	

	void Clear()
	{
		m_bSlideBegin	= false;
		m_bSlidePost	= false;		

		m_slidePos = Vector2.zero;	
	}

	public override void Update()
	{
		for (int count = 0; count < Input.touchCount; ++count)
		{
			Touch touch = Input.touches[count];

			if (touch.phase == TouchPhase.Began)
			{				
				touchDown(touch.position);
			}
			if (touch.phase == TouchPhase.Ended
			|| touch.phase == TouchPhase.Canceled)
			{
				touchUp(touch.position);
			}
			if (touch.phase == TouchPhase.Moved)
			{			
				touchSlide(touch.position);
			}
			if (touch.phase == TouchPhase.Stationary)
			{	

			}
		}
	}

	void touchDown(Vector2 pos)
	{
		m_receiver.TouchDown(pos);
	}

	void touchUp(Vector2 pos)
	{
		Clear();

		m_receiver.TouchUp(pos);
	}

	void touchSlide(Vector2 pos)
	{
		if (false == m_bSlidePost)
		{
			if (false == m_bSlideBegin)
			{
				m_bSlideBegin = true;
				m_slidePos = pos;
				Debugging.instance.prevPos = pos;
			}
			else
			{
				Vector2 diff = m_slidePos - pos;
				if (Game.SlideDistance < Mathf.Abs(diff.magnitude))
				{
					m_bSlidePost = true;
					m_receiver.TouchSlideDiff(diff);					
				}
			}
		}
	}
}
