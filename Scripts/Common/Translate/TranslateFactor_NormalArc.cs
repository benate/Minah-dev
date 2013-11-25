using UnityEngine;
using System.Collections;

public class TranslateFactor_NormalArc : TranslateFactor
{
	public Vector2 m_arrivePos = Vector3.zero;
	public int m_jumps = 0;
	public float m_height = 0.0f;
	public bool m_isRotate = false;

	private Vector3 m_startPosition = Vector3.zero;
	private Vector3 m_previousPos = Vector3.zero;

	public void DoTranslate(float durationTime, Vector2 arrivePos, float height, bool isRotate, int jumps)
	{
		base.doTranslate(durationTime);
		m_startPosition = m_previousPos = ownerTransform.position;

		m_arrivePos = arrivePos;
		m_height = height;
		m_isRotate = isRotate;
		m_jumps = jumps;
	}

	protected override void OnInit()
	{
		m_startPosition = m_previousPos = ownerTransform.position;
	}

	protected override void OnUpdate()
	{
		if (true == IsEnd())
			return;

		float lerp = LerpByTime();
		if (lerp >= 1.0f)
		{
			OnEnd();
		}

		ownerTransform.position = GetPosition(lerp, ref m_startPosition, ownerTransform.position, m_previousPos, m_arrivePos, m_height, m_jumps);

		if (m_isRotate)
		{
			ownerTransform.localRotation = GetRotation(ownerTransform.position, m_previousPos);
		}

		m_previousPos = ownerTransform.position;
	}

	public static Vector3 GetPosition(float progressTime, ref Vector3 startPos, Vector3 currentPos, Vector3 previousPos, Vector3 arrivePos, float height, int jumps)
	{
		float frac = (progressTime * jumps) % 1.0f;
		float y = height * 4 * frac * (1 - frac);
		y += arrivePos.y * progressTime;

		float x = arrivePos.x * progressTime;

		Vector3 diff = currentPos - previousPos;
		startPos = diff + startPos;

		return startPos + new Vector3(x, y, 0.0f);
	}

	public static Quaternion GetRotation(Vector3 pos, Vector3 previousPos)
	{
		Vector3 diff = pos - previousPos;
		Quaternion rotate;
		if (diff.x > 0)
		{
			rotate = Quaternion.FromToRotation(Vector3.right, diff);
		}
		else
		{
			rotate = Quaternion.FromToRotation(Vector3.left, diff);
		}

		return rotate;
	}
}