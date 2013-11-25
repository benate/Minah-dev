using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAction : MonoBehaviour
{
	public float attackTime = 0.0f;

	public GameObject collisionFxObject;

	public PerformActor owner { protected set; get; }
	public GameData.RelationType relationType { protected set; get; }

	public virtual bool isCollisionContinuous { get { return false; } }

	public void Init(PerformActor owner)
	{
		this.owner = owner;
		this.relationType = owner.data.relationType;
	}

	public virtual bool Attack() { return false; }
}