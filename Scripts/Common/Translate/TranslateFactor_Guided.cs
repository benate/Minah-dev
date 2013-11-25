using UnityEngine;
using System.Collections;

public class TranslateFactor_Guided : TranslateFactor
{
	private World m_world = null;
	private float m_maxAngle = 20.0f;
	private long m_targetID = 0;
	private float m_speed = 1.0f;

	private Vector3 m_lastGuidedTargetPos = Vector3.zero;
	private Vector3 m_moveDir = Vector3.right;
	private bool m_isTargetDead = false;

	public void DoTranslate(World world, float speed, float upForce, float dir)
	{
		OnStart();
	}

	protected override void OnInit()
	{
	}

	protected override void OnClear()
	{
		m_maxAngle = 20.0f;
		m_targetID = 0;
		m_speed = 1.0f;
	}

	protected override void OnUpdate()
	{
		if (true == IsEnd())
			return;

		float delta = Time.deltaTime;
	}

	void UpdateGuidedProjectile(float deltaTime)
	{
		if (m_targetID != 0)
		{
			PerformActor target = m_world.GetActor(m_targetID);
			if (target.fsm.curFsmType != Game.FsmType.Death && !m_isTargetDead)
			{
			    m_lastGuidedTargetPos = target.cachedTransform.position;
			}
			else
			{
			    m_isTargetDead = true;
			}
		}
		else
		{
			if (Vector3.Distance(m_lastGuidedTargetPos, Vector3.zero) < 0.1f)
			{
				OnEnd();
			}
		}

		if (Vector3.Distance(m_lastGuidedTargetPos, ownerTransform.position) < 0.1f)
		{
			OnEnd();
		}


		Vector3 guidedDir = GetGuidedDirection();
		Vector3 currPos = ownerTransform.position + guidedDir * m_speed * deltaTime;

		m_moveDir = guidedDir;
		ownerTransform.position = currPos;
	}

	Vector3 GetGuidedDirection()
	{
		Vector3 targetDir = m_lastGuidedTargetPos - ownerTransform.position;
		targetDir.Normalize();

		Vector3 ownerDir = m_moveDir;
		ownerDir.Normalize();

		float ownerAngle = Mathf.Atan2(ownerDir.x, ownerDir.y);
		if (ownerAngle < 0.0f)
		{
			ownerAngle += Mathf.PI * 2.0f;
		}
		float targetAngle = Mathf.Atan2(targetDir.x, targetDir.y);
		if (targetAngle < 0.0f)
		{
			targetAngle += Mathf.PI * 2.0f;
		}

		float changeAngle = ownerAngle - targetAngle;
		float absChangeAngle = Mathf.Abs(changeAngle);

		if (absChangeAngle > m_maxAngle)
		{
			if (absChangeAngle > Mathf.PI)
			{
				if (changeAngle > 0.0f)
					changeAngle -= Mathf.PI * 2.0f;
				else
					changeAngle += Mathf.PI * 2.0f;
			}

			if (changeAngle < 0.0f)
				changeAngle = -m_maxAngle;
			else
				changeAngle = m_maxAngle;

			ownerDir = Quaternion.Euler(0.0f, 0.0f, changeAngle * Mathf.Rad2Deg) * ownerDir;
		}
		else
		{
			ownerDir = targetDir;
		}

		return ownerDir;
	}
}
