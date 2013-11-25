using UnityEngine;
using System.Collections;

public class BaseCampCamera : BaseBehaviour
{
	delegate void OnUpdate();

	public static readonly float MinCameraPosX = -1000.0f;
	public static readonly float MaxCameraPosX = 800.0f;

	private int m_touchLayer = 0;
	private Vector3 m_touchStartPos = Vector3.zero;
	private Vector3 m_startCameraPos = Vector3.zero;
	private OnUpdate m_updater = null;

	void Awake()
	{
		m_touchLayer = LayerMask.NameToLayer("Game");

		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
		{
			m_updater = OnUpdateTouch;
		}
		else
		{
			m_updater = OnUpdateMouse;
		}
	}

	void Update()
	{
		if (m_updater != null)
			m_updater();
	}

	void OnUpdateMouse()
	{
		if (Input.GetMouseButtonDown(0))
		{
			TouchStart();
		}

		if (Input.GetMouseButtonUp(0))
		{
			TouchEnd();
		}

		if (Input.GetMouseButton(0))
		{
			TouchMoved();
		}
	}

	void OnUpdateTouch()
	{
		for (int count = 0; count < Input.touchCount; ++count)
		{
			Touch touch = Input.touches[count];

			if (touch.phase == TouchPhase.Began)
			{
				TouchStart();
			}
			if (touch.phase == TouchPhase.Ended	|| touch.phase == TouchPhase.Canceled)
			{
				TouchEnd();
			}
			if (touch.phase == TouchPhase.Moved)
			{
				TouchMoved();
			}
			if (touch.phase == TouchPhase.Stationary)
			{
			}
		}
	}

	GameObject GetTouchedObject(Vector3 touchPos)
	{
		Ray ray = Camera.mainCamera.ScreenPointToRay(touchPos);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1 << m_touchLayer))
		{
			return hit.collider.gameObject;
		}
		return null;
	}

	void SetNewPos(Vector3 newPos)
	{
		if (newPos.x > MaxCameraPosX)
			newPos.x = MaxCameraPosX;

		if (newPos.x < MinCameraPosX)
			newPos.x = MinCameraPosX;

		cachedTransform.position = newPos;
	}

	void TouchStart()
	{
		m_touchStartPos = Input.mousePosition;
		m_startCameraPos = cachedTransform.position;
	}

	void TouchEnd()
	{
		// 어떤 건물을 선택했는지 검사!
		// 빌딩이클릭이 됐으면!
		GameObject go = GetTouchedObject(Input.mousePosition);
		if (go != null && go.activeSelf)
			go.SendMessage("OnClickBuilding", SendMessageOptions.DontRequireReceiver);
	}

	void TouchMoved()
	{
		float diffX = m_touchStartPos.x - Input.mousePosition.x;

		SetNewPos(new Vector3(m_startCameraPos.x + diffX, m_startCameraPos.y, m_startCameraPos.z));
	}
}
