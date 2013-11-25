using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace GameData
{
	public enum AttackType
	{
		None,
		Range,
		Projectile,
	}
	public enum RelationType
	{
		None,
		Friend,
		Enemy,
	}

	public enum FactorUpdateType
	{
		OnShot,
		Dot,
		Timer,
	}
	public enum FactorEventType
	{
		Damage,
		Heal,
		Defence,
		AttackPower,
		Fever,
	}

	public enum ProjectileType
	{
		None,
		Straight,
		Arc,
		Guided,
		Mine,
	}



	public class PerformActor
	{
		public Int32 Index; //
		public String Name; //
		public String ResourcePath; //

		private static Dictionary<Int32, PerformActor> m_datas = new Dictionary<Int32, PerformActor>();

		public static PerformActor GetData(Int32 key)
		{
			PerformActor value;
			if (m_datas.TryGetValue(key, out value))
				return value;
			return null;
		}

		public static void FillData(TableData data)
		{
			for (int i = 0; i < data.rows.Count; ++i)
			{
				PerformActor addData = new PerformActor();
				TableRowData row = data.rows[i];
				addData.Index = (Int32)row.objects[0];
				addData.Name = (String)row.objects[1];
				addData.ResourcePath = (String)row.objects[2];
				m_datas[addData.Index] = addData;
			}
		}
	}

	public class Factor
	{
		public Int32 Index; //
		public String Name; //
		public FactorUpdateType UpdateType; //
		public FactorEventType EventType; //
		public String FxName; //
		public Int32 Value; //
		public Single DurationTime; //
		public String AtlasResource; //
		public String SpriteName; //

		private static Dictionary<Int32, Factor> m_datas = new Dictionary<Int32, Factor>();

		public static Factor GetData(Int32 key)
		{
			Factor value;
			if (m_datas.TryGetValue(key, out value))
				return value;
			return null;
		}

		public static void FillData(TableData data)
		{
			for (int i = 0; i < data.rows.Count; ++i)
			{
				Factor addData = new Factor();
				TableRowData row = data.rows[i];
				addData.Index = (Int32)row.objects[0];
				addData.Name = (String)row.objects[1];
				addData.UpdateType = (FactorUpdateType)row.objects[2];
				addData.EventType = (FactorEventType)row.objects[3];
				addData.FxName = (String)row.objects[4];
				addData.Value = (Int32)row.objects[5];
				addData.DurationTime = (Single)row.objects[6];
				addData.AtlasResource = (String)row.objects[7];
				addData.SpriteName = (String)row.objects[8];
				m_datas[addData.Index] = addData;
			}
		}
	}

	public class Projectile
	{
		public Int32 Index; //
		public String Name; //
		public String ResourcePath; //

		private static Dictionary<Int32, Projectile> m_datas = new Dictionary<Int32, Projectile>();

		public static Projectile GetData(Int32 key)
		{
			Projectile value;
			if (m_datas.TryGetValue(key, out value))
				return value;
			return null;
		}

		public static void FillData(TableData data)
		{
			for (int i = 0; i < data.rows.Count; ++i)
			{
				Projectile addData = new Projectile();
				TableRowData row = data.rows[i];
				addData.Index = (Int32)row.objects[0];
				addData.Name = (String)row.objects[1];
				addData.ResourcePath = (String)row.objects[2];
				m_datas[addData.Index] = addData;
			}
		}
	}

	public class Fx
	{
		public Int32 Index; //
		public String ResourcePath; //

		private static Dictionary<Int32, Fx> m_datas = new Dictionary<Int32, Fx>();

		public static Fx GetData(Int32 key)
		{
			Fx value;
			if (m_datas.TryGetValue(key, out value))
				return value;
			return null;
		}

		public static void FillData(TableData data)
		{
			for (int i = 0; i < data.rows.Count; ++i)
			{
				Fx addData = new Fx();
				TableRowData row = data.rows[i];
				addData.Index = (Int32)row.objects[0];
				addData.ResourcePath = (String)row.objects[1];
				m_datas[addData.Index] = addData;
			}
		}
	}

	public class Rune
	{
		public Int32 Index; //
		public String Name; //
		public String ResourcePath; //

		private static Dictionary<Int32, Rune> m_datas = new Dictionary<Int32, Rune>();

		public static Rune GetData(Int32 key)
		{
			Rune value;
			if (m_datas.TryGetValue(key, out value))
				return value;
			return null;
		}

		public static void FillData(TableData data)
		{
			for (int i = 0; i < data.rows.Count; ++i)
			{
				Rune addData = new Rune();
				TableRowData row = data.rows[i];
				addData.Index = (Int32)row.objects[0];
				addData.Name = (String)row.objects[1];
				addData.ResourcePath = (String)row.objects[2];
				m_datas[addData.Index] = addData;
			}
		}
	}

	public class Loader
	{
		public static void Load(Stream stream)
		{
			Dictionary<string, TableData> datas = TableData.Load(stream);
			PerformActor.FillData(datas["PerformActor"]);
			Factor.FillData(datas["Factor"]);
			Projectile.FillData(datas["Projectile"]);
			Fx.FillData(datas["Fx"]);
			Rune.FillData(datas["Rune"]);
		}
	}

}
