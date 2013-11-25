using UnityEngine;
using System.Collections;

public class Translater : BaseBehaviour
{
	public bool pause { set; get; }

	TranslateFactor_Arc m_arc = new TranslateFactor_Arc();
	public TranslateFactor_Arc arc { get { return m_arc; } }

	TranslateFactor_NormalArc m_arcNormal = new TranslateFactor_NormalArc();
	public TranslateFactor_NormalArc arcNormal { get { return m_arcNormal; } }

	TranslateFactor_InTime m_inTime = new TranslateFactor_InTime();
	public TranslateFactor_InTime inTime { get { return m_inTime; } }

	TranslateFactor_Move m_move = new TranslateFactor_Move();
	public TranslateFactor_Move move { get { return m_move; } }

	TranslateFactor_MoveInTime m_moveInTime = new TranslateFactor_MoveInTime();
	public TranslateFactor_MoveInTime moveInTime { get { return m_moveInTime; } }

	TranslateFactor_MoveToPos m_moveToPos = new TranslateFactor_MoveToPos();
	public TranslateFactor_MoveToPos moveToPos { get { return m_moveToPos; } }

	TranslateFactor_Rotate m_rotate = new TranslateFactor_Rotate();
	public TranslateFactor_Rotate rotate { get { return m_rotate; } }

	TranslateFactor_Scale m_scale = new TranslateFactor_Scale();
	public TranslateFactor_Scale scale { get { return m_scale; } }

	TranslateFactor m_current = null;

	void Awake()
	{
		m_arc.Init(cachedTransform);
		m_arcNormal.Init(cachedTransform);
		m_inTime.Init(cachedTransform);
		m_move.Init(cachedTransform);
		m_moveToPos.Init(cachedTransform);
		m_moveInTime.Init(cachedTransform);
		m_rotate.Init(cachedTransform);
		m_scale.Init(cachedTransform);

		pause = false;
	}

	void FixedUpdate() 
	{
		if (true == pause)
			return;

		if (m_current != null)
		{
			m_current.Update();
		}
	}

	public void Clear()
	{
		m_arc.Clear();
		m_arcNormal.Clear();
		m_inTime.Clear();
		m_move.Clear();
		m_moveToPos.Clear();
		m_moveInTime.Clear();
		m_rotate.Clear();
		m_scale.Clear();
	}

	public void SetCurrent(TranslateFactor current)
	{
		m_current = current;
	}

	public bool IsCurrent()
	{
		return m_current != null;
	}

	public bool IsEnd()
	{
		if (m_current != null)
			return m_current.IsEnd();

		return true;
	}
}
