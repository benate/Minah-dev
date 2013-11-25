using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTrigger : BaseBehaviour
{
	public List<int> emenies;
	public GameObject wallLeft;
	public GameObject wallRight;
	public GameObject wallObject;
	public GameObject playerSpawnPoint;
	public GameObject enemySpawnPoint;
	public bool isEndGame = false;
	public string startTalkString = "";
	public string endTalkString = "";

	public EndPoint endPoint { get; set; }

	private BoxCollider m_cachedBoxCollider;
	public void Start()
	{
		m_cachedBoxCollider = gameObject.GetComponentInChildren<BoxCollider>();

		// 임시
		startTalkString = "전투 시작 전투 시작 전투 시작 전투 시작\n전투 시작 전투 시작 전투 시작\n전투 시작 전투 시작 전투 시작";
		endTalkString = "전투 끝 전투 끝 전투 끝\n전투 끝 전투 끝 전투 끝 전투 끝\n전투 끝 전투 끝 전투 끝 전투 끝 전투 끝";
	}
	
	public void FixedUpdate ()
	{
		foreach (PerformActor collidee in World.instance.friends)
		{
			if (Game.FsmType.Death == collidee.fsm.curFsmType)
				continue;

			if (true == Actor.CheckCollision(collidee.cachedBoxCollider, m_cachedBoxCollider))
			{
				Directing.instance.StartTriggerDirecting(this);
			}
		}
	}

	public void EndTrigger()
	{
		endPoint.Excute();

		World.instance.OnMsg(PacketData.OnRunRight.Create(0), true);
	}

	public void EnterEndTrigger()
	{
		endPoint.enabled = false;
	}

	public void ExcuteTrigger()
	{
		endPoint = gameObject.AddComponent<EndPoint>();

		//GameObject.Instantiate(wallObject, wallLeft.transform.position, Quaternion.identity);
		//GameObject.Instantiate(wallObject, wallRight.transform.position, Quaternion.identity);

		float startX = enemySpawnPoint.transform.position.x;
		float offset = 50.0f;
		for (int i = 0; i < emenies.Count; ++i)
		{
			World.instance.CreateActor(GameEnum.NewIndex, emenies[i], new Vector3(startX + (offset * i), Game.GroundYPos, 0.0f));
		}
	}

	public void StartTrigger()
	{
		this.enabled = false;

		Transform trans;

		float offset = 50.0f;

		trans = playerSpawnPoint.transform;
		foreach (PerformActor actor in World.instance.friends)
		{
			actor.data.startPos = trans.position;
			actor.data.startPos.x -= offset;

			offset += 50.0f;
		}

		offset = 50.0f;

		trans = enemySpawnPoint.transform;
		foreach (PerformActor actor in World.instance.enemies)
		{
			actor.data.startPos = trans.position;
			actor.data.startPos.x += offset;

			offset += 50.0f;
		}
	}
}
