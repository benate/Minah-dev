using UnityEngine;
using System.Collections;

public class TribeTk2dCamera : BaseBehaviour
{
	public static readonly float MaxCameraZoomIn = 1.0f;
	public static readonly float MaxCameraZoomOut = 0.7f;

	public tk2dCamera m_tk2DCamera;

	public bool m_isUpdate = true;

	private Camera m_camera;
	public Camera cachedCamera { get { if (m_camera == null) m_camera = camera; return m_camera; } }

	private float m_cameraMoveSpeed = 200.0f;
	private float m_cameraSizeSpeed = 0.1f;

	private float m_targetPosX = 0.0f;
	private float m_targetSize = 0.0f;

	void Awake()
	{
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			m_tk2DCamera.zoomScale = 0.5f;
			Debug.Log(m_tk2DCamera.forceResolution);
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			m_tk2DCamera.zoomScale = 1.0f;
			Debug.Log(m_tk2DCamera.forceResolution);
		}
		if (Input.GetKeyDown(KeyCode.F3))
		{
			m_tk2DCamera.zoomScale = 3.0f;
			Debug.Log(m_tk2DCamera.forceResolution);
		}
		if (Input.GetKeyDown(KeyCode.F4))
		{
			m_tk2DCamera.zoomScale = 4.0f;
			Debug.Log(m_tk2DCamera.forceResolution);
		}

		if (!m_isUpdate || World.instance == null)
			return;

		float delta = Time.deltaTime;

		float minX = float.MaxValue;
		float maxX = float.MinValue;

		GetMinMax(ref minX, ref maxX);

		float size = Mathf.Abs(maxX - minX);

		//// 적이 시야밖으로 벗어나면
		if (size > 1.0f / MaxCameraZoomOut * m_tk2DCamera.forceResolution.x)
			UpdateUserAligned(delta, minX, maxX);
		else
			UpdateCenter(delta, minX, maxX);

		UpdateCameraLerp(delta);
	}

	void UpdateUserAligned(float delta, float minX, float maxX)
	{
		// 적의 오른쪽에 있는지 왼쪽에 있는지 검사
		float center = minX;

		float changeOffsetX = 1.0f / m_tk2DCamera.zoomScale * m_tk2DCamera.forceResolution.x * 0.5f;
		float originOffsetX = m_tk2DCamera.forceResolution.x * 0.5f;

		m_targetPosX = center + changeOffsetX - originOffsetX;

		m_targetSize = MaxCameraZoomIn;
	}

	private void UpdateCenter(float delta, float minX, float maxX)
	{
		m_targetSize = m_tk2DCamera.forceResolution.x / Mathf.Abs(maxX - minX);

		if (m_targetSize > MaxCameraZoomIn)
			m_targetSize = MaxCameraZoomIn;

		if (m_targetSize < MaxCameraZoomOut)
			m_targetSize = MaxCameraZoomOut;

		float originOffsetX = m_tk2DCamera.forceResolution.x * 0.5f;
		float changeOffsetX = 1.0f / m_tk2DCamera.zoomScale * originOffsetX;

		m_targetPosX = minX + ((maxX - minX) * 0.5f) - originOffsetX;
	}

	private void UpdateCameraLerp(float delta)
	{
		Vector3 pos = cachedTransform.position;

		float diffX = m_targetPosX - pos.x;
		if (10.0f < Mathf.Abs(diffX))
		{
			// 위치를 보정한다
			if (diffX < 0.0f)
				pos.x -= (m_cameraMoveSpeed * delta);
			else
				pos.x += (m_cameraMoveSpeed * delta);
		}

		float diffSize = m_targetSize - m_tk2DCamera.zoomScale;
		if (0.1f < Mathf.Abs(diffSize))
		{
			// 사이즈를 보정한다
			if (diffSize < 0.0f)
				m_tk2DCamera.zoomScale -= (m_cameraSizeSpeed * delta);
			else
				m_tk2DCamera.zoomScale += (m_cameraSizeSpeed * delta);
		}

		float changeOffsetY = 1.0f / m_tk2DCamera.zoomScale * m_tk2DCamera.forceResolution.y * 0.5f;
		float originOffsetY = m_tk2DCamera.forceResolution.y * 0.5f;

		pos.y = changeOffsetY - originOffsetY;
		cachedTransform.position = pos;
	}

	private void GetMinMax(ref float minX, ref float maxX)
	{
		minX = float.MaxValue;
		maxX = float.MinValue;

		foreach (PerformActor actor in World.instance.friends)
		{
			if(Game.FsmType.Death == actor.fsm.curFsmType)
				continue;

			if (minX > actor.cachedTransform.position.x)
				minX = actor.cachedTransform.position.x;

			if (maxX < actor.cachedTransform.position.x)
				maxX = actor.cachedTransform.position.x;
		}

		foreach (PerformActor actor in World.instance.enemies)
		{
			if (Game.FsmType.Death == actor.fsm.curFsmType)
				continue;

			if (minX > actor.cachedTransform.position.x)
				minX = actor.cachedTransform.position.x;

			if (maxX < actor.cachedTransform.position.x)
				maxX = actor.cachedTransform.position.x;
		}
	}
}
