using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace PacketData
{
	public enum Category
	{
		None = 0,
		Agent = 1,
		World = 2,
		UI = 3,
	}

	public abstract class Message
	{
		public Int64 ReceiverID;
		public abstract int GetID();
		public abstract Category GetCategory();
		public Int64 GetReceiverID() { return ReceiverID; }
	}

	public class OnPause : Message
	{
		public const int ID = -1280724749;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OnPause.ID; }
		public override Category GetCategory() { return OnPause.CATEGORY; }
		public static OnPause Create(Int64 _ReceiverID)
		{
			OnPause packet = new OnPause();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class OnGameStart : Message
	{
		public const int ID = 1826672863;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OnGameStart.ID; }
		public override Category GetCategory() { return OnGameStart.CATEGORY; }
		public static OnGameStart Create(Int64 _ReceiverID)
		{
			OnGameStart packet = new OnGameStart();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class OnBattle : Message
	{
		public const int ID = -1336675651;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OnBattle.ID; }
		public override Category GetCategory() { return OnBattle.CATEGORY; }
		public static OnBattle Create(Int64 _ReceiverID)
		{
			OnBattle packet = new OnBattle();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class OffBattle : Message
	{
		public const int ID = -360346344;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OffBattle.ID; }
		public override Category GetCategory() { return OffBattle.CATEGORY; }
		public static OffBattle Create(Int64 _ReceiverID)
		{
			OffBattle packet = new OffBattle();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class OnRunRight : Message
	{
		public const int ID = -1980785665;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OnRunRight.ID; }
		public override Category GetCategory() { return OnRunRight.CATEGORY; }
		public static OnRunRight Create(Int64 _ReceiverID)
		{
			OnRunRight packet = new OnRunRight();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class OnDamage : Message
	{
		public const int ID = -1815110711;
		public const Category CATEGORY = Category.Agent;

		public PerformActor Attacker;
		public Factor DamageFactor;

		public override int GetID() { return OnDamage.ID; }
		public override Category GetCategory() { return OnDamage.CATEGORY; }
		public static OnDamage Create(Int64 _ReceiverID, PerformActor _Attacker, Factor _DamageFactor)
		{
			OnDamage packet = new OnDamage();
			packet.ReceiverID = _ReceiverID;
			packet.Attacker = _Attacker;
			packet.DamageFactor = _DamageFactor;
			return packet;
		}
	}

	public class OnTargeting : Message
	{
		public const int ID = -257104154;
		public const Category CATEGORY = Category.Agent;

		public PerformActor Target;

		public override int GetID() { return OnTargeting.ID; }
		public override Category GetCategory() { return OnTargeting.CATEGORY; }
		public static OnTargeting Create(Int64 _ReceiverID, PerformActor _Target)
		{
			OnTargeting packet = new OnTargeting();
			packet.ReceiverID = _ReceiverID;
			packet.Target = _Target;
			return packet;
		}
	}

	public class OnBackDash : Message
	{
		public const int ID = 1627066868;
		public const Category CATEGORY = Category.Agent;

		public bool IsPressed;

		public override int GetID() { return OnBackDash.ID; }
		public override Category GetCategory() { return OnBackDash.CATEGORY; }
		public static OnBackDash Create(Int64 _ReceiverID, bool _IsPressed)
		{
			OnBackDash packet = new OnBackDash();
			packet.ReceiverID = _ReceiverID;
			packet.IsPressed = _IsPressed;
			return packet;
		}
	}

	public class OnJumpAttack : Message
	{
		public const int ID = 508003251;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OnJumpAttack.ID; }
		public override Category GetCategory() { return OnJumpAttack.CATEGORY; }
		public static OnJumpAttack Create(Int64 _ReceiverID)
		{
			OnJumpAttack packet = new OnJumpAttack();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class OnStrongAttack : Message
	{
		public const int ID = -1261086562;
		public const Category CATEGORY = Category.Agent;


		public override int GetID() { return OnStrongAttack.ID; }
		public override Category GetCategory() { return OnStrongAttack.CATEGORY; }
		public static OnStrongAttack Create(Int64 _ReceiverID)
		{
			OnStrongAttack packet = new OnStrongAttack();
			packet.ReceiverID = _ReceiverID;
			return packet;
		}
	}

	public class TestActor : Message
	{
		public const int ID = -338696227;
		public const Category CATEGORY = Category.Agent;

		public PerformActor Actor;

		public override int GetID() { return TestActor.ID; }
		public override Category GetCategory() { return TestActor.CATEGORY; }
		public static TestActor Create(Int64 _ReceiverID, PerformActor _Actor)
		{
			TestActor packet = new TestActor();
			packet.ReceiverID = _ReceiverID;
			packet.Actor = _Actor;
			return packet;
		}
	}

}
