using UnityEngine;
using System.Collections;

public class Game
{
	public enum FsmType
	{
		None,
		Attack1,
		Attack2,
		Attack3,
		Attack4,
		Attack5,
		AttackDash,
		BackDash,
		Dash,
		Damage,
		Defence,
		Death,		
		Flee,
		Halt,
		Hover,
		Idle,
		JumpAttack,
		Run,
		Pursuit,
		Special,
	}

	//--> time	
	public static float FsmIdleTime = float.MaxValue;
	public static float FsmRunTime = float.MaxValue;

	//--> touch
	public static float SlideDistance = 100.0f;	

	//-- range
	public static float RestrictRange = 500.0f;
	public static float CombatRange = 100.0f;
	public static float MoveRange 	= 50.0f;

	//-->
	public static float GroundYPos	= 204.0f;
	public static float Gravity		= 3000.0f;

	//--> dir
	public static float DirNone 	= 0.0f;
	public static float DirLeft 	= -1.0f;
	public static float DirRight 	= 1.0f;

	public const int MaxCardSlot = 5;
}

