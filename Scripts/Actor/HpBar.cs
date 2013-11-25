using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour
{
	public GameObject originHpbar;

	private PerformActor m_actor = null;
	private UISlider m_hpbar = null;
	private int m_prevHP = 0;

	void OnDestroy()
	{
	//	GameObject.Destroy(m_hpbar.gameObject);
	}
	
	void FixedUpdate ()
	{
		if (m_prevHP != m_actor.data.curHp)
		{
			m_hpbar.sliderValue = (float)m_actor.data.curHp / (float)m_actor.data.maxHp;

			if (m_actor.data.curHp <= 0)
			{
				m_hpbar.gameObject.SetActive(false);
			}

			m_prevHP = m_actor.data.curHp;
		}
	}

	public void OnInit()
	{
		m_actor = transform.parent.GetComponent<PerformActor>();

		GameObject hpbar = m_actor.world.AddHPBar(originHpbar);
		hpbar.transform.localScale = new UnityEngine.Vector3(0.3f, 0.5f, 0.0f);
		m_hpbar = hpbar.GetComponent<UISlider>();
		UIFollowTarget target = hpbar.GetComponent<UIFollowTarget>();
		target.target = transform;

		m_hpbar.sliderValue = 1.0f;

		m_prevHP = m_actor.data.curHp;
	}
}
