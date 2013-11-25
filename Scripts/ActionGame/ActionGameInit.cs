using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class ActionGameInit : MonoBehaviour
{
	public GameInput gameInput;
	public PerformActor user;

	void Awake()
	{
		TextAsset asset = Resources.Load("Table/GameData") as TextAsset;
		Stream stream = new MemoryStream(asset.bytes);
		GameData.Loader.Load(stream);
	}

	void Start ()
	{
		// 게임셋팅을 한다
		// 자기캐릭터를 만든다
		// gameInput.player = user;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject go = GetTouchedEnemy(Input.mousePosition);

			if (go != null)
			{
				PerformActor actor = GetPerformActor(go.transform);
				if (actor != null && actor.fsm.curFsmType != Game.FsmType.Death)
				{
					World.instance.OnMsg(PacketData.OnTargeting.Create(GameEnum.UserIndex, actor));
				}
			}
		}
	}
	
	void FixedUpdate ()
	{
		// 일정 시간마다 적 캐릭터를 스폰한다
	}

	private GameObject GetTouchedEnemy(Vector3 touchPos)
	{
		// 적들을 터치한다
		Ray ray = Camera.mainCamera.ScreenPointToRay(touchPos);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			return hit.collider.gameObject;
		}
		return null;
	}

	private PerformActor GetPerformActor(Transform trans)
	{
		if (trans == null)
			return null;

		PerformActor actor;
		actor = trans.GetComponent<PerformActor>();
		if (actor == null)
		{
			actor = GetPerformActor(trans.parent);
			if (actor == null)
				return null;
		}

		return actor;
	}
}
