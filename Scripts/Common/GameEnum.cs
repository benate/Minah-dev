using System;
using System.Collections;

public class GameEnum
{
	public const int AttackActionCount = 5;
	public const int MaxCharacterSlotCount = 5;

	public const int InvalidIndex = 0;

	public static readonly Int64 UserIndex = 1;	
	public static readonly Int64 Enemy1Index = 2;
	public static readonly Int64 Enemy2Index = 3;
	public static readonly Int64 Enemy3Index = 4;
	public static readonly Int64 Enemy4Index = 5;
	public static readonly Int64 Enemy5Index = 6;

	public static readonly Int64 WorldIndex = (long)Guid.NewGuid().GetHashCode();
	public static readonly Int64 InGameIndex = (long)Guid.NewGuid().GetHashCode();
	public static readonly Int64 UIIndex = (long)Guid.NewGuid().GetHashCode();

	public static Int64 NewIndex { get { return (long)Guid.NewGuid().GetHashCode(); } }

	public enum Direction
	{
		None,
		Left,
		Right,
		Down,
		Up,
	}
}

