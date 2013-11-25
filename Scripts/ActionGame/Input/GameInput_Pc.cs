using UnityEngine;
using System.Collections;

public class GameInput_Pc : GameInputUpdater
{	
	bool m_bSlideBegin = false;
	bool m_bSlidePost = false;
	Vector3 m_slidePos = Vector3.zero;

	void Clear()
	{		
		m_bSlideBegin = false;
		m_bSlidePost = false;

		m_slidePos = Vector3.zero;
	}

	public override void Update()
	{
		//--> down
		if (Input.GetMouseButtonDown(0))
		{
			m_slidePos = Input.mousePosition;
			m_receiver.TouchDown(Input.mousePosition);		
		}

		//--> up
		if (Input.GetMouseButtonUp(0))
		{
			Clear();
			m_receiver.TouchUp(Input.mousePosition);			
		}
		
		if (Input.GetMouseButton(0))
		{
		    if (false == m_bSlidePost)
		    {
		        if (false == m_bSlideBegin)
		        {
		            m_bSlideBegin = true;
					m_slidePos = Input.mousePosition;
		        }
		        else
		        {
					Vector3 diff = m_slidePos - Input.mousePosition;
					if (Game.SlideDistance < Mathf.Abs(diff.magnitude))
		            {
		                m_bSlidePost = true;
		                m_receiver.TouchSlideDiff(diff);
		            }
		        }
		    }
		}
	}
}
