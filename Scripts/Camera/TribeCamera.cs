using UnityEngine;
using System.Collections;

public class TribeCamera : BaseBehaviour
{
	public static readonly float MaxCameraOthoSize = 7.0f;
	public static readonly float MinCameraOthoSize = 5.0f;

	public bool m_isUpdate = true;	

	private Camera m_camera;
	public Camera cachedCamera { get { if (m_camera == null) m_camera = camera; return m_camera; } }

	private float m_cameraMoveSpeed = 3.0f;
	private float m_cameraSizeSpeed = 1.0f;

	private float m_targetPosX = 0.0f;
	private float m_targetSize = 0.0f;

	void Awake()
	{
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera currentCamera in allCameras)
		{
			float newAspectRatio = 16.0f / 9.0f;
			//무조건 16:9 화면 비율로 맞춘다
			var variance = newAspectRatio / currentCamera.aspect;
			if (variance < 1.0f)
				currentCamera.rect = new Rect((1.0f - variance) / 2.0f, 0, variance, 1.0f);
			else
			{
				variance = 1.0f / variance;

				currentCamera.rect = new Rect(0, (1.0f - variance) / 2.0f, 1.0f, variance);
			}
		}
	}

	void Update()
	{
		if (!m_isUpdate)
		    return;

		float delta = Time.deltaTime;
		float size = 0;

		// 적이 시야밖으로 벗어나면
		if (size > MaxCameraOthoSize)
			UpdateAgentLeft(delta);
		else
			UpdateCenter(delta);

		UpdateCameraLerp(delta);
	}

	void UpdateAgentLeft(float delta)
	{
		float screenRatio = (float)Screen.width / (float)Screen.height;

		float left = float.MaxValue;

		float orthWidth = cachedCamera.orthographicSize * screenRatio;

		m_targetPosX = left + orthWidth; // 센터
		m_targetSize = MinCameraOthoSize;
	}

	void UpdateCenter(float delta)
	{
		float minX = float.MaxValue, maxX = float.MinValue;

		float screenRatio = (float)Screen.width / (float)Screen.height;
		float screenInvRatio = (float)Screen.height / (float)Screen.width;
		m_targetSize = Mathf.Abs(maxX - minX) * 0.5f * screenInvRatio;

		if (m_targetSize > MaxCameraOthoSize)
			m_targetSize = MaxCameraOthoSize;

		if (m_targetSize < MinCameraOthoSize)
			m_targetSize = MinCameraOthoSize;

		// 왼쪽을 기준으로 한다
		m_targetPosX = minX + m_targetSize * screenRatio;
	}

	void UpdateCameraLerp(float delta)
	{
		Vector3 pos = cachedTransform.position;

		float diffX = m_targetPosX - pos.x;
		if (0.1f < Mathf.Abs(diffX))
		{
			// 위치를 보정한다
			if (diffX < 0.0f)
				pos.x -= (m_cameraMoveSpeed * delta);
			else
				pos.x += (m_cameraMoveSpeed * delta);
		}

		float diffSize = m_targetSize - cachedCamera.orthographicSize;
		if (0.1f < Mathf.Abs(diffSize))
		{
			// 사이즈를 보정한다
			if (diffSize < 0.0f)
				cachedCamera.orthographicSize -= (m_cameraSizeSpeed * delta);
			else
				cachedCamera.orthographicSize += (m_cameraSizeSpeed * delta);
		}

		pos.y = cachedCamera.orthographicSize;
		cachedTransform.position = pos;
	}
}
