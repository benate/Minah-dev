using UnityEngine;
using System.Collections;

public class CheckTimer
{
	bool m_bCompleted = true;

	public float progressTime { get { return m_accumTime; } }
	public bool IsEnd() { return m_bCompleted; }

	float m_checkTime = 0.0f;
	float m_accumTime = 0.0f;

	public void Clear()
	{
		m_bCompleted = true;
		m_checkTime = 0.0f;
		m_accumTime = 0.0f;
	}

	public void OnUpdate()
	{
		if (false == m_bCompleted)
		{
			if (m_accumTime >= m_checkTime)
			{
				m_bCompleted = true;
				m_accumTime = 0.0f;
			}
			else
			{
				m_accumTime += Time.deltaTime;
			}
		}
	}

	public void SetTimer(float checkTime)
	{
		m_bCompleted = false;
		m_checkTime = checkTime;
		m_accumTime = 0.0f;
	}
}
