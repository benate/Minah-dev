using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorData : MonoBehaviour
{
	public string actorName = "";

	#region attack delegate

		private List<AttackAction> m_attackActions = new List<AttackAction>();
		public List<AttackAction> attackActions { get { return m_attackActions; } }

	#endregion	

	#region type

		public GameData.RelationType relationType = GameData.RelationType.None;		
		public float dirForward { get {	return GameData.RelationType.Friend == relationType ? Game.DirRight : Game.DirLeft; } }

	#endregion

	#region status

		public int maxHp = 100;
		[HideInInspector]public int curHp = 100;

		public int maxEnergy = 100;
		[HideInInspector]public int curEnergy = 100;

		public int level = 0;

		public int defence = 0;
		public int defenceBonus = 0;

		public int attackPower = 0;
		public int attackPowerBonus = 0;
		public int attackPowerInTotal { get { return attackPower + attackPowerBonus; } }

		public float speed		= 0.0f;//60.0f;
		public float boostSpeed = 0.0f;//120.0f;
		public float jumpPower	= 0.0f;//900.0f;		

		public int fever { set; get; }

	#endregion

	#region Fsm

		[HideInInspector]public Vector3 startPos = Vector3.zero;
		[HideInInspector]public Vector3 targetPos = Vector3.zero;
		[HideInInspector]public string animName = "";		

	#endregion

	void Awake()
	{
		curHp = maxHp;
		curEnergy = maxEnergy;

		fever			= 0;
	}

	void Upgrade()
	{
	}
}