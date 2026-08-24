using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LockSoftware;

[Serializable]
[ToolboxItem(true)]
[XmlSchemaProvider("GetTypedDataSetSchema")]
[HelpKeyword("vs.data.DataSet")]
[DesignerCategory("code")]
[XmlRoot("RPT_DS_HB")]
public class RPT_DS_HB : DataSet
{
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public delegate void D_HotelBasicRowChangeEventHandler(object sender, D_HotelBasicRowChangeEvent e);

	[Serializable]
	[XmlSchemaProvider("GetTypedTableSchema")]
	public class D_HotelBasicDataTable : DataTable, IEnumerable
	{
		private DataColumn columnB_ID;

		private DataColumn columnB_HotelName;

		private DataColumn columnB_HotelWeb;

		private DataColumn columnB_HotelID;

		private DataColumn columnB_Address;

		private DataColumn columnB_BookTel;

		private DataColumn columnB_Fax;

		private DataColumn columnB_Post;

		private DataColumn columnB_StayDay;

		private DataColumn columnB_LevelTime;

		private DataColumn columnB_BackImg;

		private DataColumn columnB_GInfo;

		private DataColumn columnB_MaxGuest;

		private DataColumn columnB_Updatetime;

		private DataColumn columnB_Updator_ID;

		private DataColumn columnB_Updator;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_IDColumn => columnB_ID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_HotelNameColumn => columnB_HotelName;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_HotelWebColumn => columnB_HotelWeb;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_HotelIDColumn => columnB_HotelID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_AddressColumn => columnB_Address;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_BookTelColumn => columnB_BookTel;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_FaxColumn => columnB_Fax;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_PostColumn => columnB_Post;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_StayDayColumn => columnB_StayDay;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_LevelTimeColumn => columnB_LevelTime;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_BackImgColumn => columnB_BackImg;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_GInfoColumn => columnB_GInfo;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_MaxGuestColumn => columnB_MaxGuest;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_UpdatetimeColumn => columnB_Updatetime;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_Updator_IDColumn => columnB_Updator_ID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_UpdatorColumn => columnB_Updator;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		[Browsable(false)]
		public int Count => base.Rows.Count;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public D_HotelBasicRow this[int index] => (D_HotelBasicRow)base.Rows[index];

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event D_HotelBasicRowChangeEventHandler D_HotelBasicRowChanging;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event D_HotelBasicRowChangeEventHandler D_HotelBasicRowChanged;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event D_HotelBasicRowChangeEventHandler D_HotelBasicRowDeleting;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event D_HotelBasicRowChangeEventHandler D_HotelBasicRowDeleted;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public D_HotelBasicDataTable()
		{
			base.TableName = "D_HotelBasic";
			BeginInit();
			InitClass();
			EndInit();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal D_HotelBasicDataTable(DataTable table)
		{
			base.TableName = table.TableName;
			if (table.CaseSensitive != table.DataSet.CaseSensitive)
			{
				base.CaseSensitive = table.CaseSensitive;
			}
			if (table.Locale.ToString() != table.DataSet.Locale.ToString())
			{
				base.Locale = table.Locale;
			}
			if (table.Namespace != table.DataSet.Namespace)
			{
				base.Namespace = table.Namespace;
			}
			base.Prefix = table.Prefix;
			base.MinimumCapacity = table.MinimumCapacity;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected D_HotelBasicDataTable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			InitVars();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void AddD_HotelBasicRow(D_HotelBasicRow row)
		{
			base.Rows.Add(row);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public D_HotelBasicRow AddD_HotelBasicRow(string B_HotelName, string B_HotelWeb, string B_HotelID, string B_Address, string B_BookTel, string B_Fax, string B_Post, int B_StayDay, string B_LevelTime, byte[] B_BackImg, bool B_GInfo, int B_MaxGuest, DateTime B_Updatetime, long B_Updator_ID, string B_Updator)
		{
			D_HotelBasicRow d_HotelBasicRow = (D_HotelBasicRow)NewRow();
			object[] itemArray = new object[16]
			{
				null, B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime,
				B_BackImg, B_GInfo, B_MaxGuest, B_Updatetime, B_Updator_ID, B_Updator
			};
			d_HotelBasicRow.ItemArray = itemArray;
			base.Rows.Add(d_HotelBasicRow);
			return d_HotelBasicRow;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public D_HotelBasicRow FindByB_ID(long B_ID)
		{
			return (D_HotelBasicRow)base.Rows.Find(new object[1] { B_ID });
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public virtual IEnumerator GetEnumerator()
		{
			return base.Rows.GetEnumerator();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public override DataTable Clone()
		{
			D_HotelBasicDataTable d_HotelBasicDataTable = (D_HotelBasicDataTable)base.Clone();
			d_HotelBasicDataTable.InitVars();
			return d_HotelBasicDataTable;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override DataTable CreateInstance()
		{
			return new D_HotelBasicDataTable();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			columnB_ID = base.Columns["B_ID"];
			columnB_HotelName = base.Columns["B_HotelName"];
			columnB_HotelWeb = base.Columns["B_HotelWeb"];
			columnB_HotelID = base.Columns["B_HotelID"];
			columnB_Address = base.Columns["B_Address"];
			columnB_BookTel = base.Columns["B_BookTel"];
			columnB_Fax = base.Columns["B_Fax"];
			columnB_Post = base.Columns["B_Post"];
			columnB_StayDay = base.Columns["B_StayDay"];
			columnB_LevelTime = base.Columns["B_LevelTime"];
			columnB_BackImg = base.Columns["B_BackImg"];
			columnB_GInfo = base.Columns["B_GInfo"];
			columnB_MaxGuest = base.Columns["B_MaxGuest"];
			columnB_Updatetime = base.Columns["B_Updatetime"];
			columnB_Updator_ID = base.Columns["B_Updator_ID"];
			columnB_Updator = base.Columns["B_Updator"];
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			columnB_ID = new DataColumn("B_ID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnB_ID);
			columnB_HotelName = new DataColumn("B_HotelName", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_HotelName);
			columnB_HotelWeb = new DataColumn("B_HotelWeb", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_HotelWeb);
			columnB_HotelID = new DataColumn("B_HotelID", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_HotelID);
			columnB_Address = new DataColumn("B_Address", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_Address);
			columnB_BookTel = new DataColumn("B_BookTel", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_BookTel);
			columnB_Fax = new DataColumn("B_Fax", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_Fax);
			columnB_Post = new DataColumn("B_Post", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_Post);
			columnB_StayDay = new DataColumn("B_StayDay", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnB_StayDay);
			columnB_LevelTime = new DataColumn("B_LevelTime", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_LevelTime);
			columnB_BackImg = new DataColumn("B_BackImg", typeof(byte[]), null, MappingType.Element);
			base.Columns.Add(columnB_BackImg);
			columnB_GInfo = new DataColumn("B_GInfo", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnB_GInfo);
			columnB_MaxGuest = new DataColumn("B_MaxGuest", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnB_MaxGuest);
			columnB_Updatetime = new DataColumn("B_Updatetime", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnB_Updatetime);
			columnB_Updator_ID = new DataColumn("B_Updator_ID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnB_Updator_ID);
			columnB_Updator = new DataColumn("B_Updator", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnB_Updator);
			base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[1] { columnB_ID }, isPrimaryKey: true));
			columnB_ID.AutoIncrement = true;
			columnB_ID.AutoIncrementSeed = -1L;
			columnB_ID.AutoIncrementStep = -1L;
			columnB_ID.AllowDBNull = false;
			columnB_ID.ReadOnly = true;
			columnB_ID.Unique = true;
			columnB_HotelName.MaxLength = 128;
			columnB_HotelWeb.MaxLength = 256;
			columnB_HotelID.MaxLength = 128;
			columnB_Address.MaxLength = 256;
			columnB_BookTel.MaxLength = 50;
			columnB_Fax.MaxLength = 50;
			columnB_Post.MaxLength = 50;
			columnB_LevelTime.MaxLength = 10;
			columnB_Updator.MaxLength = 20;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public D_HotelBasicRow NewD_HotelBasicRow()
		{
			return (D_HotelBasicRow)NewRow();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new D_HotelBasicRow(builder);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override Type GetRowType()
		{
			return typeof(D_HotelBasicRow);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);
			if (D_HotelBasicRowChanged != null)
			{
				D_HotelBasicRowChanged(this, new D_HotelBasicRowChangeEvent((D_HotelBasicRow)e.Row, e.Action));
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override void OnRowChanging(DataRowChangeEventArgs e)
		{
			base.OnRowChanging(e);
			if (D_HotelBasicRowChanging != null)
			{
				D_HotelBasicRowChanging(this, new D_HotelBasicRowChangeEvent((D_HotelBasicRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleted(DataRowChangeEventArgs e)
		{
			base.OnRowDeleted(e);
			if (D_HotelBasicRowDeleted != null)
			{
				D_HotelBasicRowDeleted(this, new D_HotelBasicRowChangeEvent((D_HotelBasicRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleting(DataRowChangeEventArgs e)
		{
			base.OnRowDeleting(e);
			if (D_HotelBasicRowDeleting != null)
			{
				D_HotelBasicRowDeleting(this, new D_HotelBasicRowChangeEvent((D_HotelBasicRow)e.Row, e.Action));
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void RemoveD_HotelBasicRow(D_HotelBasicRow row)
		{
			base.Rows.Remove(row);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			RPT_DS_HB rPT_DS_HB = new RPT_DS_HB();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
			xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
			xmlSchemaAny2.MinOccurs = 1m;
			xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny2);
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "namespace";
			xmlSchemaAttribute.FixedValue = rPT_DS_HB.Namespace;
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "tableTypeName";
			xmlSchemaAttribute2.FixedValue = "D_HotelBasicDataTable";
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = rPT_DS_HB.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					XmlSchema xmlSchema = null;
					schemaSerializable.Write(memoryStream);
					IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
					while (enumerator.MoveNext())
					{
						xmlSchema = (XmlSchema)enumerator.Current;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					memoryStream?.Close();
					memoryStream2?.Close();
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}
	}

	public class D_HotelBasicRow : DataRow
	{
		private D_HotelBasicDataTable tableD_HotelBasic;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long B_ID
		{
			get
			{
				return (long)base[tableD_HotelBasic.B_IDColumn];
			}
			set
			{
				base[tableD_HotelBasic.B_IDColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string B_HotelName
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_HotelNameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_HotelName' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_HotelNameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string B_HotelWeb
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_HotelWebColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_HotelWeb' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_HotelWebColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string B_HotelID
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_HotelIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_HotelID' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_HotelIDColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string B_Address
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_AddressColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_Address' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_AddressColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string B_BookTel
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_BookTelColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_BookTel' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_BookTelColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string B_Fax
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_FaxColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_Fax' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_FaxColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string B_Post
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_PostColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_Post' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_PostColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public int B_StayDay
		{
			get
			{
				try
				{
					return (int)base[tableD_HotelBasic.B_StayDayColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_StayDay' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_StayDayColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string B_LevelTime
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_LevelTimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_LevelTime' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_LevelTimeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public byte[] B_BackImg
		{
			get
			{
				try
				{
					return (byte[])base[tableD_HotelBasic.B_BackImgColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_BackImg' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_BackImgColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool B_GInfo
		{
			get
			{
				try
				{
					return (bool)base[tableD_HotelBasic.B_GInfoColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_GInfo' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_GInfoColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int B_MaxGuest
		{
			get
			{
				try
				{
					return (int)base[tableD_HotelBasic.B_MaxGuestColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_MaxGuest' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_MaxGuestColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DateTime B_Updatetime
		{
			get
			{
				try
				{
					return (DateTime)base[tableD_HotelBasic.B_UpdatetimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_Updatetime' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_UpdatetimeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long B_Updator_ID
		{
			get
			{
				try
				{
					return (long)base[tableD_HotelBasic.B_Updator_IDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_Updator_ID' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_Updator_IDColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string B_Updator
		{
			get
			{
				try
				{
					return (string)base[tableD_HotelBasic.B_UpdatorColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("The value for column 'B_Updator' in table 'D_HotelBasic' is DBNull.", innerException);
				}
			}
			set
			{
				base[tableD_HotelBasic.B_UpdatorColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal D_HotelBasicRow(DataRowBuilder rb)
			: base(rb)
		{
			tableD_HotelBasic = (D_HotelBasicDataTable)base.Table;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_HotelNameNull()
		{
			return IsNull(tableD_HotelBasic.B_HotelNameColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_HotelNameNull()
		{
			base[tableD_HotelBasic.B_HotelNameColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_HotelWebNull()
		{
			return IsNull(tableD_HotelBasic.B_HotelWebColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_HotelWebNull()
		{
			base[tableD_HotelBasic.B_HotelWebColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_HotelIDNull()
		{
			return IsNull(tableD_HotelBasic.B_HotelIDColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_HotelIDNull()
		{
			base[tableD_HotelBasic.B_HotelIDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_AddressNull()
		{
			return IsNull(tableD_HotelBasic.B_AddressColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_AddressNull()
		{
			base[tableD_HotelBasic.B_AddressColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_BookTelNull()
		{
			return IsNull(tableD_HotelBasic.B_BookTelColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_BookTelNull()
		{
			base[tableD_HotelBasic.B_BookTelColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_FaxNull()
		{
			return IsNull(tableD_HotelBasic.B_FaxColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_FaxNull()
		{
			base[tableD_HotelBasic.B_FaxColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_PostNull()
		{
			return IsNull(tableD_HotelBasic.B_PostColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_PostNull()
		{
			base[tableD_HotelBasic.B_PostColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_StayDayNull()
		{
			return IsNull(tableD_HotelBasic.B_StayDayColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_StayDayNull()
		{
			base[tableD_HotelBasic.B_StayDayColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_LevelTimeNull()
		{
			return IsNull(tableD_HotelBasic.B_LevelTimeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_LevelTimeNull()
		{
			base[tableD_HotelBasic.B_LevelTimeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_BackImgNull()
		{
			return IsNull(tableD_HotelBasic.B_BackImgColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_BackImgNull()
		{
			base[tableD_HotelBasic.B_BackImgColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_GInfoNull()
		{
			return IsNull(tableD_HotelBasic.B_GInfoColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_GInfoNull()
		{
			base[tableD_HotelBasic.B_GInfoColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_MaxGuestNull()
		{
			return IsNull(tableD_HotelBasic.B_MaxGuestColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_MaxGuestNull()
		{
			base[tableD_HotelBasic.B_MaxGuestColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_UpdatetimeNull()
		{
			return IsNull(tableD_HotelBasic.B_UpdatetimeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_UpdatetimeNull()
		{
			base[tableD_HotelBasic.B_UpdatetimeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_Updator_IDNull()
		{
			return IsNull(tableD_HotelBasic.B_Updator_IDColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_Updator_IDNull()
		{
			base[tableD_HotelBasic.B_Updator_IDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_UpdatorNull()
		{
			return IsNull(tableD_HotelBasic.B_UpdatorColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_UpdatorNull()
		{
			base[tableD_HotelBasic.B_UpdatorColumn] = Convert.DBNull;
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public class D_HotelBasicRowChangeEvent : EventArgs
	{
		private D_HotelBasicRow eventRow;

		private DataRowAction eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public D_HotelBasicRow Row => eventRow;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataRowAction Action => eventAction;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public D_HotelBasicRowChangeEvent(D_HotelBasicRow row, DataRowAction action)
		{
			eventRow = row;
			eventAction = action;
		}
	}

	private D_HotelBasicDataTable tableD_HotelBasic;

	private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[Browsable(false)]
	public D_HotelBasicDataTable D_HotelBasic => tableD_HotelBasic;

	[Browsable(true)]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	public override SchemaSerializationMode SchemaSerializationMode
	{
		get
		{
			return _schemaSerializationMode;
		}
		set
		{
			_schemaSerializationMode = value;
		}
	}

	[DebuggerNonUserCode]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public new DataTableCollection Tables => base.Tables;

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public new DataRelationCollection Relations => base.Relations;

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public RPT_DS_HB()
	{
		BeginInit();
		InitClass();
		CollectionChangeEventHandler value = SchemaChanged;
		base.Tables.CollectionChanged += value;
		base.Relations.CollectionChanged += value;
		EndInit();
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected RPT_DS_HB(SerializationInfo info, StreamingContext context)
		: base(info, context, ConstructSchema: false)
	{
		if (IsBinarySerialized(info, context))
		{
			InitVars(initTable: false);
			CollectionChangeEventHandler value = SchemaChanged;
			Tables.CollectionChanged += value;
			Relations.CollectionChanged += value;
			return;
		}
		string s = (string)info.GetValue("XmlSchema", typeof(string));
		if (DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
		{
			DataSet dataSet = new DataSet();
			dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
			if (dataSet.Tables["D_HotelBasic"] != null)
			{
				base.Tables.Add(new D_HotelBasicDataTable(dataSet.Tables["D_HotelBasic"]));
			}
			base.DataSetName = dataSet.DataSetName;
			base.Prefix = dataSet.Prefix;
			base.Namespace = dataSet.Namespace;
			base.Locale = dataSet.Locale;
			base.CaseSensitive = dataSet.CaseSensitive;
			base.EnforceConstraints = dataSet.EnforceConstraints;
			Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
			InitVars();
		}
		else
		{
			ReadXmlSchema(new XmlTextReader(new StringReader(s)));
		}
		GetSerializationData(info, context);
		CollectionChangeEventHandler value2 = SchemaChanged;
		base.Tables.CollectionChanged += value2;
		Relations.CollectionChanged += value2;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override void InitializeDerivedDataSet()
	{
		BeginInit();
		InitClass();
		EndInit();
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public override DataSet Clone()
	{
		RPT_DS_HB rPT_DS_HB = (RPT_DS_HB)base.Clone();
		rPT_DS_HB.InitVars();
		rPT_DS_HB.SchemaSerializationMode = SchemaSerializationMode;
		return rPT_DS_HB;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected override bool ShouldSerializeTables()
	{
		return false;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected override bool ShouldSerializeRelations()
	{
		return false;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected override void ReadXmlSerializable(XmlReader reader)
	{
		if (DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
		{
			Reset();
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(reader);
			if (dataSet.Tables["D_HotelBasic"] != null)
			{
				base.Tables.Add(new D_HotelBasicDataTable(dataSet.Tables["D_HotelBasic"]));
			}
			base.DataSetName = dataSet.DataSetName;
			base.Prefix = dataSet.Prefix;
			base.Namespace = dataSet.Namespace;
			base.Locale = dataSet.Locale;
			base.CaseSensitive = dataSet.CaseSensitive;
			base.EnforceConstraints = dataSet.EnforceConstraints;
			Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
			InitVars();
		}
		else
		{
			ReadXml(reader);
			InitVars();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override XmlSchema GetSchemaSerializable()
	{
		MemoryStream memoryStream = new MemoryStream();
		WriteXmlSchema(new XmlTextWriter(memoryStream, null));
		memoryStream.Position = 0L;
		return XmlSchema.Read(new XmlTextReader(memoryStream), null);
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal void InitVars()
	{
		InitVars(initTable: true);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	internal void InitVars(bool initTable)
	{
		tableD_HotelBasic = (D_HotelBasicDataTable)base.Tables["D_HotelBasic"];
		if (initTable && tableD_HotelBasic != null)
		{
			tableD_HotelBasic.InitVars();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private void InitClass()
	{
		base.DataSetName = "RPT_DS_HB";
		base.Prefix = "";
		base.Namespace = "http://tempuri.org/RPT_DS_HB.xsd";
		base.EnforceConstraints = true;
		SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		tableD_HotelBasic = new D_HotelBasicDataTable();
		base.Tables.Add(tableD_HotelBasic);
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	private bool ShouldSerializeD_HotelBasic()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private void SchemaChanged(object sender, CollectionChangeEventArgs e)
	{
		if (e.Action == CollectionChangeAction.Remove)
		{
			InitVars();
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
	{
		RPT_DS_HB rPT_DS_HB = new RPT_DS_HB();
		XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
		XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
		XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
		xmlSchemaAny.Namespace = rPT_DS_HB.Namespace;
		xmlSchemaSequence.Items.Add(xmlSchemaAny);
		xmlSchemaComplexType.Particle = xmlSchemaSequence;
		XmlSchema schemaSerializable = rPT_DS_HB.GetSchemaSerializable();
		if (xs.Contains(schemaSerializable.TargetNamespace))
		{
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = new MemoryStream();
			try
			{
				XmlSchema xmlSchema = null;
				schemaSerializable.Write(memoryStream);
				IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
				while (enumerator.MoveNext())
				{
					xmlSchema = (XmlSchema)enumerator.Current;
					memoryStream2.SetLength(0L);
					xmlSchema.Write(memoryStream2);
					if (memoryStream.Length == memoryStream2.Length)
					{
						memoryStream.Position = 0L;
						memoryStream2.Position = 0L;
						while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
						{
						}
						if (memoryStream.Position == memoryStream.Length)
						{
							return xmlSchemaComplexType;
						}
					}
				}
			}
			finally
			{
				memoryStream?.Close();
				memoryStream2?.Close();
			}
		}
		xs.Add(schemaSerializable);
		return xmlSchemaComplexType;
	}
}
