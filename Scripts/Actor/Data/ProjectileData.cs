using UnityEngine;
using System.Collections;

public class ProjectileData : MonoBehaviour
{
	public GameData.ProjectileType projectileType;
	public GameData.FactorUpdateType updateType;
	public GameData.FactorEventType eventType;

	public string projectileName;

	public int level = 1;

	public int attackPower = -2;
	public int attackPowerBouns = 0;
	public int attackPowerInTotal { get { return attackPower + attackPowerBouns; } }

	public Vector2 dir;
	public GameObject fxObject;

	public float lifeTime;

	public float minTimeLength;
	public float maxTimeLength;

	public float minHeight;
	public float maxHeight;
	
	public float speed;
}
