using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameData
{
	public class TableEnumElement
	{
		public string name = "";
		public string description = "";

		public void Read(BinaryReader reader)
		{
			while (true)
			{
				string value = reader.ReadString();

				if (value == "TableEnumElemenetData")
				{
					name = reader.ReadString();
					description = reader.ReadString();
				}
				else if (value == "EndTableEnumElementData")
					return;
			}
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write("StartTableEnumElementData");

			writer.Write("TableEnumElemenetData");
			writer.Write(name);
			writer.Write(description);

			writer.Write("EndTableEnumElementData");
		}
	}

	public class TableEnumData
	{
		public string name = "";
		public string description = "";
		public List<TableEnumElement> elements = new List<TableEnumElement>();

		public void Read(BinaryReader reader)
		{
			while (true)
			{
				string value = reader.ReadString();

				if (value == "TableEnumDataName")
				{
					name = reader.ReadString();
				}
				else if (value == "TableEnumDataDesc")
				{
					description = reader.ReadString();
				}
				else if (value == "TableEnumElementData")
				{
					int count = reader.ReadInt32();
					for (int i = 0; i < count; ++i)
					{
						TableEnumElement elem = new TableEnumElement();
						elem.Read(reader);
						elements.Add(elem);
					}
				}
				else if (value == "EndTableEnumData")
					return;
			}
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write("StartTableEnumData");

			writer.Write("TableEnumDataName");
			writer.Write(name);
			writer.Write("TableEnumDataDesc");
			writer.Write(description);

			writer.Write("TableEnumElementData");
			writer.Write(elements.Count);
			foreach (TableEnumElement obj in elements)
			{
				obj.Write(writer);
			}

			writer.Write("EndTableEnumData");
		}
	}

	public class TableColumnData
	{
		public string name = "";
		public string type = "";
		public string description = "";

		public void Read(BinaryReader reader)
		{
			while (true)
			{
				string value = reader.ReadString();

				if (value == "TableColumnData")
				{
					name = reader.ReadString();
					type = reader.ReadString();
					description = reader.ReadString();
				}
				else if (value == "EndTableColumnData")
					return;
			}
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write("StartTableColumnData");

			writer.Write("TableColumnData");
			writer.Write(name);
			writer.Write(type);
			writer.Write(description);

			writer.Write("EndTableColumnData");
		}
	}

	public class TableRowData
	{
		public List<object> objects = new List<object>();

		public void Read(BinaryReader reader)
		{
			while (true)
			{
				string value = reader.ReadString();

				if (value == "TableRowData")
				{
					int count = reader.ReadInt32();
					for (int i = 0; i < count; ++i)
					{
						object obj = ReadObject(reader);
						objects.Add(obj);
					}
				}
				else if (value == "EndTableRowData")
					return;
			}
		}

		protected object ReadObject(BinaryReader reader)
		{
			string typeName = reader.ReadString();
			switch (typeName)
			{
				case "Boolean": return reader.ReadBoolean();
				case "Char":	return reader.ReadChar();
				case "Int16":	return reader.ReadInt16();
				case "UInt16":	return reader.ReadUInt16();
				case "Int32":	return reader.ReadInt32();
				case "UInt32":	return reader.ReadUInt32();
				case "Int64":	return reader.ReadInt64();
				case "UInt64":	return reader.ReadUInt64();
				case "Single":	return reader.ReadSingle();
				case "Double":	return reader.ReadDouble();
				case "String":	return reader.ReadString();
			}
			return null;
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write("StartTableRowData");

			writer.Write("TableRowData");
			writer.Write(objects.Count);

			foreach (object obj in objects)
			{
				WriteObject(writer, obj);
			}

			writer.Write("EndTableRowData");
		}

		protected void WriteObject(BinaryWriter writer, object obj)
		{
			writer.Write(obj.GetType().Name);
			switch (obj.GetType().Name)
			{
				case "Boolean": writer.Write((Boolean)obj); break;
				case "Char":	writer.Write((Char)obj); break;
				case "Int16":	writer.Write((Int16)obj); break;
				case "UInt16":	writer.Write((UInt16)obj); break;
				case "Int32":	writer.Write((Int32)obj); break;
				case "UInt32":	writer.Write((UInt32)obj); break;
				case "Int64":	writer.Write((UInt64)obj); break;
				case "UInt64":	writer.Write((UInt64)obj); break;
				case "Single":	writer.Write((Single)obj); break;
				case "Double":	writer.Write((Double)obj); break;
				case "String":	writer.Write(obj.ToString()); break;
			}
		}
	}

	public class TableData
	{
		private string m_tableName = "";
		private List<TableEnumData> m_enums = new List<TableEnumData>();
		private List<TableColumnData> m_columns = new List<TableColumnData>();
		private List<TableRowData> m_rows = new List<TableRowData>();

		public string tableName { get { return m_tableName; } }
		public List<TableEnumData> enums { get { return m_enums; } }
		public List<TableColumnData> columns { get { return m_columns; } }
		public List<TableRowData> rows { get { return m_rows; } }

		public TableData(string tableName)
		{
			m_tableName = tableName;
		}

		public void AddEnum(TableEnumData enumData)
		{
			m_enums.Add(enumData);
		}

		public void AddColumn(TableColumnData column)
		{
			m_columns.Add(column);
		}

		public void AddRow(TableRowData row)
		{
			m_rows.Add(row);
		}

		public TableRowData NewRow()
		{
			TableRowData row = new TableRowData();

			return row;
		}

		public void Read(BinaryReader reader)
		{
			while (true)
			{
				string value = reader.ReadString();

				if (value == "EnumData")
				{
					int count = reader.ReadInt32();
					for (int i = 0; i < count; ++i)
					{
						TableEnumData enumData = new TableEnumData();
						enumData.Read(reader);
						m_enums.Add(enumData);
					}
				}
				else if (value == "ColumnData")
				{
					int count = reader.ReadInt32();
					for (int i = 0; i < count; ++i)
					{
						TableColumnData colData = new TableColumnData();
						colData.Read(reader);
						m_columns.Add(colData);
					}
				}
				else if (value == "RowData")
				{
					int count = reader.ReadInt32();
					for (int i = 0; i < count; ++i)
					{
						TableRowData rowData = new TableRowData();
						rowData.Read(reader);
						m_rows.Add(rowData);
					}
				}
				else if (value == "EndTableData")
					return;
			}
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write("StartTableData");

			writer.Write("EnumData");
			writer.Write(m_enums.Count);
			foreach (TableEnumData enumData in m_enums)
			{
				enumData.Write(writer);
			}

			writer.Write("ColumnData");
			writer.Write(m_columns.Count);
			foreach (TableColumnData colData in m_columns)
			{
				colData.Write(writer);
			}

			writer.Write("RowData");
			writer.Write(m_rows.Count);
			foreach (TableRowData rowData in m_rows)
			{
				rowData.Write(writer);
			}

			writer.Write("EndTableData");
		}

		public static Dictionary<string, TableData> Load(Stream stream)
		{
			Dictionary<string, TableData> datas = new Dictionary<string, TableData>();

			BinaryReader reader = new BinaryReader(stream);

			string identity = reader.ReadString();
			if (identity != "Game Data Base File")
				return null;

			string value = reader.ReadString();

			if (value == "StartData")
			{
				int count = reader.ReadInt32();
				for (int i = 0; i < count; ++i)
				{
					string name = reader.ReadString();
					TableData data = new TableData(name);
					data.Read(reader);

					datas[name] = data;
				}
			}

			return datas;
		}

		public static bool Save(Stream stream, Dictionary<string, TableData> tableDatas)
		{
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write("Game Data Base File");

			writer.Write("StartData");
			writer.Write(tableDatas.Count);

			foreach (KeyValuePair<string, TableData> pair in tableDatas)
			{
				writer.Write(pair.Key);
				pair.Value.Write(writer);
			}

			writer.Write("EndData");

			return true;
		}
	}
}