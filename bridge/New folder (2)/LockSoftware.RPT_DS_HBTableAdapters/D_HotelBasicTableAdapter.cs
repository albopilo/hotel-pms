using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using LockSoftware.Properties;

namespace LockSoftware.RPT_DS_HBTableAdapters;

[ToolboxItem(true)]
[DataObject(true)]
[Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DesignerCategory("code")]
[HelpKeyword("vs.data.TableAdapter")]
public class D_HotelBasicTableAdapter : Component
{
	private SqlDataAdapter _adapter;

	private SqlConnection _connection;

	private SqlTransaction _transaction;

	private SqlCommand[] _commandCollection;

	private bool _clearBeforeFill;

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected internal SqlDataAdapter Adapter
	{
		get
		{
			if (_adapter == null)
			{
				InitAdapter();
			}
			return _adapter;
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal SqlConnection Connection
	{
		get
		{
			if (_connection == null)
			{
				InitConnection();
			}
			return _connection;
		}
		set
		{
			_connection = value;
			if (Adapter.InsertCommand != null)
			{
				Adapter.InsertCommand.Connection = value;
			}
			if (Adapter.DeleteCommand != null)
			{
				Adapter.DeleteCommand.Connection = value;
			}
			if (Adapter.UpdateCommand != null)
			{
				Adapter.UpdateCommand.Connection = value;
			}
			for (int i = 0; i < CommandCollection.Length; i++)
			{
				if (CommandCollection[i] != null)
				{
					CommandCollection[i].Connection = value;
				}
			}
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal SqlTransaction Transaction
	{
		get
		{
			return _transaction;
		}
		set
		{
			_transaction = value;
			for (int i = 0; i < CommandCollection.Length; i++)
			{
				CommandCollection[i].Transaction = _transaction;
			}
			if (Adapter != null && Adapter.DeleteCommand != null)
			{
				Adapter.DeleteCommand.Transaction = _transaction;
			}
			if (Adapter != null && Adapter.InsertCommand != null)
			{
				Adapter.InsertCommand.Transaction = _transaction;
			}
			if (Adapter != null && Adapter.UpdateCommand != null)
			{
				Adapter.UpdateCommand.Transaction = _transaction;
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected SqlCommand[] CommandCollection
	{
		get
		{
			if (_commandCollection == null)
			{
				InitCommandCollection();
			}
			return _commandCollection;
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public bool ClearBeforeFill
	{
		get
		{
			return _clearBeforeFill;
		}
		set
		{
			_clearBeforeFill = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public D_HotelBasicTableAdapter()
	{
		ClearBeforeFill = true;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	private void InitAdapter()
	{
		_adapter = new SqlDataAdapter();
		DataTableMapping dataTableMapping = new DataTableMapping();
		dataTableMapping.SourceTable = "Table";
		dataTableMapping.DataSetTable = "D_HotelBasic";
		dataTableMapping.ColumnMappings.Add("B_ID", "B_ID");
		dataTableMapping.ColumnMappings.Add("B_HotelName", "B_HotelName");
		dataTableMapping.ColumnMappings.Add("B_HotelWeb", "B_HotelWeb");
		dataTableMapping.ColumnMappings.Add("B_HotelID", "B_HotelID");
		dataTableMapping.ColumnMappings.Add("B_Address", "B_Address");
		dataTableMapping.ColumnMappings.Add("B_BookTel", "B_BookTel");
		dataTableMapping.ColumnMappings.Add("B_Fax", "B_Fax");
		dataTableMapping.ColumnMappings.Add("B_Post", "B_Post");
		dataTableMapping.ColumnMappings.Add("B_StayDay", "B_StayDay");
		dataTableMapping.ColumnMappings.Add("B_LevelTime", "B_LevelTime");
		dataTableMapping.ColumnMappings.Add("B_BackImg", "B_BackImg");
		dataTableMapping.ColumnMappings.Add("B_GInfo", "B_GInfo");
		dataTableMapping.ColumnMappings.Add("B_MaxGuest", "B_MaxGuest");
		dataTableMapping.ColumnMappings.Add("B_Updatetime", "B_Updatetime");
		dataTableMapping.ColumnMappings.Add("B_Updator_ID", "B_Updator_ID");
		dataTableMapping.ColumnMappings.Add("B_Updator", "B_Updator");
		_adapter.TableMappings.Add(dataTableMapping);
		_adapter.DeleteCommand = new SqlCommand();
		_adapter.DeleteCommand.Connection = Connection;
		_adapter.DeleteCommand.CommandText = "DELETE FROM [dbo].[D_HotelBasic] WHERE (([B_ID] = @Original_B_ID) AND ((@IsNull_B_HotelName = 1 AND [B_HotelName] IS NULL) OR ([B_HotelName] = @Original_B_HotelName)) AND ((@IsNull_B_HotelWeb = 1 AND [B_HotelWeb] IS NULL) OR ([B_HotelWeb] = @Original_B_HotelWeb)) AND ((@IsNull_B_HotelID = 1 AND [B_HotelID] IS NULL) OR ([B_HotelID] = @Original_B_HotelID)) AND ((@IsNull_B_Address = 1 AND [B_Address] IS NULL) OR ([B_Address] = @Original_B_Address)) AND ((@IsNull_B_BookTel = 1 AND [B_BookTel] IS NULL) OR ([B_BookTel] = @Original_B_BookTel)) AND ((@IsNull_B_Fax = 1 AND [B_Fax] IS NULL) OR ([B_Fax] = @Original_B_Fax)) AND ((@IsNull_B_Post = 1 AND [B_Post] IS NULL) OR ([B_Post] = @Original_B_Post)) AND ((@IsNull_B_StayDay = 1 AND [B_StayDay] IS NULL) OR ([B_StayDay] = @Original_B_StayDay)) AND ((@IsNull_B_LevelTime = 1 AND [B_LevelTime] IS NULL) OR ([B_LevelTime] = @Original_B_LevelTime)) AND ((@IsNull_B_GInfo = 1 AND [B_GInfo] IS NULL) OR ([B_GInfo] = @Original_B_GInfo)) AND ((@IsNull_B_MaxGuest = 1 AND [B_MaxGuest] IS NULL) OR ([B_MaxGuest] = @Original_B_MaxGuest)) AND ((@IsNull_B_Updatetime = 1 AND [B_Updatetime] IS NULL) OR ([B_Updatetime] = @Original_B_Updatetime)) AND ((@IsNull_B_Updator_ID = 1 AND [B_Updator_ID] IS NULL) OR ([B_Updator_ID] = @Original_B_Updator_ID)) AND ((@IsNull_B_Updator = 1 AND [B_Updator] IS NULL) OR ([B_Updator] = @Original_B_Updator)))";
		_adapter.DeleteCommand.CommandType = CommandType.Text;
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_ID", SqlDbType.BigInt, 0, ParameterDirection.Input, 0, 0, "B_ID", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_HotelName", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_HotelName", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_HotelName", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelName", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_HotelWeb", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_HotelWeb", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_HotelWeb", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelWeb", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_HotelID", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_HotelID", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_HotelID", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelID", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_Address", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Address", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_Address", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Address", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_BookTel", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_BookTel", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_BookTel", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_BookTel", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_Fax", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Fax", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_Fax", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Fax", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_Post", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Post", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_Post", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Post", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_StayDay", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_StayDay", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_StayDay", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_StayDay", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_LevelTime", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_LevelTime", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_LevelTime", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_LevelTime", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_GInfo", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_GInfo", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_GInfo", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "B_GInfo", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_MaxGuest", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_MaxGuest", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_MaxGuest", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_MaxGuest", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_Updatetime", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Updatetime", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_Updatetime", SqlDbType.DateTime, 0, ParameterDirection.Input, 0, 0, "B_Updatetime", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_Updator_ID", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Updator_ID", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_Updator_ID", SqlDbType.BigInt, 0, ParameterDirection.Input, 0, 0, "B_Updator_ID", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@IsNull_B_Updator", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Updator", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.DeleteCommand.Parameters.Add(new SqlParameter("@Original_B_Updator", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Updator", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand = new SqlCommand();
		_adapter.InsertCommand.Connection = Connection;
		_adapter.InsertCommand.CommandText = "INSERT INTO [dbo].[D_HotelBasic] ([B_HotelName], [B_HotelWeb], [B_HotelID], [B_Address], [B_BookTel], [B_Fax], [B_Post], [B_StayDay], [B_LevelTime], [B_BackImg], [B_GInfo], [B_MaxGuest], [B_Updatetime], [B_Updator_ID], [B_Updator]) VALUES (@B_HotelName, @B_HotelWeb, @B_HotelID, @B_Address, @B_BookTel, @B_Fax, @B_Post, @B_StayDay, @B_LevelTime, @B_BackImg, @B_GInfo, @B_MaxGuest, @B_Updatetime, @B_Updator_ID, @B_Updator);\r\nSELECT B_ID, B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime, B_BackImg, B_GInfo, B_MaxGuest, B_Updatetime, B_Updator_ID, B_Updator FROM D_HotelBasic WHERE (B_ID = SCOPE_IDENTITY())";
		_adapter.InsertCommand.CommandType = CommandType.Text;
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_HotelName", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelName", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_HotelWeb", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelWeb", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_HotelID", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelID", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_Address", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Address", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_BookTel", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_BookTel", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_Fax", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Fax", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_Post", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Post", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_StayDay", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_StayDay", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_LevelTime", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_LevelTime", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_BackImg", SqlDbType.Image, 0, ParameterDirection.Input, 0, 0, "B_BackImg", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_GInfo", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "B_GInfo", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_MaxGuest", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_MaxGuest", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_Updatetime", SqlDbType.DateTime, 0, ParameterDirection.Input, 0, 0, "B_Updatetime", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_Updator_ID", SqlDbType.BigInt, 0, ParameterDirection.Input, 0, 0, "B_Updator_ID", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.InsertCommand.Parameters.Add(new SqlParameter("@B_Updator", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Updator", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand = new SqlCommand();
		_adapter.UpdateCommand.Connection = Connection;
		_adapter.UpdateCommand.CommandText = "UPDATE [dbo].[D_HotelBasic] SET [B_HotelName] = @B_HotelName, [B_HotelWeb] = @B_HotelWeb, [B_HotelID] = @B_HotelID, [B_Address] = @B_Address, [B_BookTel] = @B_BookTel, [B_Fax] = @B_Fax, [B_Post] = @B_Post, [B_StayDay] = @B_StayDay, [B_LevelTime] = @B_LevelTime, [B_BackImg] = @B_BackImg, [B_GInfo] = @B_GInfo, [B_MaxGuest] = @B_MaxGuest, [B_Updatetime] = @B_Updatetime, [B_Updator_ID] = @B_Updator_ID, [B_Updator] = @B_Updator WHERE (([B_ID] = @Original_B_ID) AND ((@IsNull_B_HotelName = 1 AND [B_HotelName] IS NULL) OR ([B_HotelName] = @Original_B_HotelName)) AND ((@IsNull_B_HotelWeb = 1 AND [B_HotelWeb] IS NULL) OR ([B_HotelWeb] = @Original_B_HotelWeb)) AND ((@IsNull_B_HotelID = 1 AND [B_HotelID] IS NULL) OR ([B_HotelID] = @Original_B_HotelID)) AND ((@IsNull_B_Address = 1 AND [B_Address] IS NULL) OR ([B_Address] = @Original_B_Address)) AND ((@IsNull_B_BookTel = 1 AND [B_BookTel] IS NULL) OR ([B_BookTel] = @Original_B_BookTel)) AND ((@IsNull_B_Fax = 1 AND [B_Fax] IS NULL) OR ([B_Fax] = @Original_B_Fax)) AND ((@IsNull_B_Post = 1 AND [B_Post] IS NULL) OR ([B_Post] = @Original_B_Post)) AND ((@IsNull_B_StayDay = 1 AND [B_StayDay] IS NULL) OR ([B_StayDay] = @Original_B_StayDay)) AND ((@IsNull_B_LevelTime = 1 AND [B_LevelTime] IS NULL) OR ([B_LevelTime] = @Original_B_LevelTime)) AND ((@IsNull_B_GInfo = 1 AND [B_GInfo] IS NULL) OR ([B_GInfo] = @Original_B_GInfo)) AND ((@IsNull_B_MaxGuest = 1 AND [B_MaxGuest] IS NULL) OR ([B_MaxGuest] = @Original_B_MaxGuest)) AND ((@IsNull_B_Updatetime = 1 AND [B_Updatetime] IS NULL) OR ([B_Updatetime] = @Original_B_Updatetime)) AND ((@IsNull_B_Updator_ID = 1 AND [B_Updator_ID] IS NULL) OR ([B_Updator_ID] = @Original_B_Updator_ID)) AND ((@IsNull_B_Updator = 1 AND [B_Updator] IS NULL) OR ([B_Updator] = @Original_B_Updator)));\r\nSELECT B_ID, B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime, B_BackImg, B_GInfo, B_MaxGuest, B_Updatetime, B_Updator_ID, B_Updator FROM D_HotelBasic WHERE (B_ID = @B_ID)";
		_adapter.UpdateCommand.CommandType = CommandType.Text;
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_HotelName", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelName", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_HotelWeb", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelWeb", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_HotelID", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelID", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_Address", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Address", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_BookTel", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_BookTel", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_Fax", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Fax", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_Post", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Post", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_StayDay", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_StayDay", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_LevelTime", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_LevelTime", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_BackImg", SqlDbType.Image, 0, ParameterDirection.Input, 0, 0, "B_BackImg", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_GInfo", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "B_GInfo", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_MaxGuest", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_MaxGuest", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_Updatetime", SqlDbType.DateTime, 0, ParameterDirection.Input, 0, 0, "B_Updatetime", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_Updator_ID", SqlDbType.BigInt, 0, ParameterDirection.Input, 0, 0, "B_Updator_ID", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_Updator", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Updator", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_ID", SqlDbType.BigInt, 0, ParameterDirection.Input, 0, 0, "B_ID", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_HotelName", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_HotelName", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_HotelName", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelName", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_HotelWeb", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_HotelWeb", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_HotelWeb", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelWeb", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_HotelID", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_HotelID", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_HotelID", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_HotelID", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_Address", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Address", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_Address", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Address", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_BookTel", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_BookTel", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_BookTel", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_BookTel", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_Fax", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Fax", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_Fax", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Fax", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_Post", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Post", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_Post", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_Post", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_StayDay", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_StayDay", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_StayDay", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_StayDay", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_LevelTime", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_LevelTime", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_LevelTime", SqlDbType.VarChar, 0, ParameterDirection.Input, 0, 0, "B_LevelTime", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_GInfo", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_GInfo", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_GInfo", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "B_GInfo", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_MaxGuest", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_MaxGuest", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_MaxGuest", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_MaxGuest", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_Updatetime", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Updatetime", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_Updatetime", SqlDbType.DateTime, 0, ParameterDirection.Input, 0, 0, "B_Updatetime", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_Updator_ID", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Updator_ID", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_Updator_ID", SqlDbType.BigInt, 0, ParameterDirection.Input, 0, 0, "B_Updator_ID", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IsNull_B_Updator", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "B_Updator", DataRowVersion.Original, sourceColumnNullMapping: true, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Original_B_Updator", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "B_Updator", DataRowVersion.Original, sourceColumnNullMapping: false, null, "", "", ""));
		_adapter.UpdateCommand.Parameters.Add(new SqlParameter("@B_ID", SqlDbType.BigInt, 8, ParameterDirection.Input, 0, 0, "B_ID", DataRowVersion.Current, sourceColumnNullMapping: false, null, "", "", ""));
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private void InitConnection()
	{
		_connection = new SqlConnection();
		_connection.ConnectionString = Settings.Default.RadioLockConnStr;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	private void InitCommandCollection()
	{
		_commandCollection = new SqlCommand[1];
		_commandCollection[0] = new SqlCommand();
		_commandCollection[0].Connection = Connection;
		_commandCollection[0].CommandText = "SELECT B_ID, B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime, B_BackImg, B_GInfo, B_MaxGuest, B_Updatetime, B_Updator_ID, B_Updator FROM dbo.D_HotelBasic";
		_commandCollection[0].CommandType = CommandType.Text;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[HelpKeyword("vs.data.TableAdapter")]
	[DataObjectMethod(DataObjectMethodType.Fill, true)]
	[DebuggerNonUserCode]
	public virtual int Fill(RPT_DS_HB.D_HotelBasicDataTable dataTable)
	{
		Adapter.SelectCommand = CommandCollection[0];
		if (ClearBeforeFill)
		{
			dataTable.Clear();
		}
		return Adapter.Fill(dataTable);
	}

	[HelpKeyword("vs.data.TableAdapter")]
	[DataObjectMethod(DataObjectMethodType.Select, true)]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public virtual RPT_DS_HB.D_HotelBasicDataTable GetData()
	{
		Adapter.SelectCommand = CommandCollection[0];
		RPT_DS_HB.D_HotelBasicDataTable d_HotelBasicDataTable = new RPT_DS_HB.D_HotelBasicDataTable();
		Adapter.Fill(d_HotelBasicDataTable);
		return d_HotelBasicDataTable;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[HelpKeyword("vs.data.TableAdapter")]
	public virtual int Update(RPT_DS_HB.D_HotelBasicDataTable dataTable)
	{
		return Adapter.Update(dataTable);
	}

	[HelpKeyword("vs.data.TableAdapter")]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public virtual int Update(RPT_DS_HB dataSet)
	{
		return Adapter.Update(dataSet, "D_HotelBasic");
	}

	[DebuggerNonUserCode]
	[HelpKeyword("vs.data.TableAdapter")]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public virtual int Update(DataRow dataRow)
	{
		return Adapter.Update(new DataRow[1] { dataRow });
	}

	[HelpKeyword("vs.data.TableAdapter")]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public virtual int Update(DataRow[] dataRows)
	{
		return Adapter.Update(dataRows);
	}

	[DebuggerNonUserCode]
	[DataObjectMethod(DataObjectMethodType.Delete, true)]
	[HelpKeyword("vs.data.TableAdapter")]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public virtual int Delete(long Original_B_ID, string Original_B_HotelName, string Original_B_HotelWeb, string Original_B_HotelID, string Original_B_Address, string Original_B_BookTel, string Original_B_Fax, string Original_B_Post, int? Original_B_StayDay, string Original_B_LevelTime, bool? Original_B_GInfo, int? Original_B_MaxGuest, DateTime? Original_B_Updatetime, long? Original_B_Updator_ID, string Original_B_Updator)
	{
		Adapter.DeleteCommand.Parameters[0].Value = Original_B_ID;
		if (Original_B_HotelName == null)
		{
			Adapter.DeleteCommand.Parameters[1].Value = 1;
			Adapter.DeleteCommand.Parameters[2].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[1].Value = 0;
			Adapter.DeleteCommand.Parameters[2].Value = Original_B_HotelName;
		}
		if (Original_B_HotelWeb == null)
		{
			Adapter.DeleteCommand.Parameters[3].Value = 1;
			Adapter.DeleteCommand.Parameters[4].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[3].Value = 0;
			Adapter.DeleteCommand.Parameters[4].Value = Original_B_HotelWeb;
		}
		if (Original_B_HotelID == null)
		{
			Adapter.DeleteCommand.Parameters[5].Value = 1;
			Adapter.DeleteCommand.Parameters[6].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[5].Value = 0;
			Adapter.DeleteCommand.Parameters[6].Value = Original_B_HotelID;
		}
		if (Original_B_Address == null)
		{
			Adapter.DeleteCommand.Parameters[7].Value = 1;
			Adapter.DeleteCommand.Parameters[8].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[7].Value = 0;
			Adapter.DeleteCommand.Parameters[8].Value = Original_B_Address;
		}
		if (Original_B_BookTel == null)
		{
			Adapter.DeleteCommand.Parameters[9].Value = 1;
			Adapter.DeleteCommand.Parameters[10].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[9].Value = 0;
			Adapter.DeleteCommand.Parameters[10].Value = Original_B_BookTel;
		}
		if (Original_B_Fax == null)
		{
			Adapter.DeleteCommand.Parameters[11].Value = 1;
			Adapter.DeleteCommand.Parameters[12].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[11].Value = 0;
			Adapter.DeleteCommand.Parameters[12].Value = Original_B_Fax;
		}
		if (Original_B_Post == null)
		{
			Adapter.DeleteCommand.Parameters[13].Value = 1;
			Adapter.DeleteCommand.Parameters[14].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[13].Value = 0;
			Adapter.DeleteCommand.Parameters[14].Value = Original_B_Post;
		}
		if (Original_B_StayDay.HasValue)
		{
			Adapter.DeleteCommand.Parameters[15].Value = 0;
			Adapter.DeleteCommand.Parameters[16].Value = Original_B_StayDay.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[15].Value = 1;
			Adapter.DeleteCommand.Parameters[16].Value = DBNull.Value;
		}
		if (Original_B_LevelTime == null)
		{
			Adapter.DeleteCommand.Parameters[17].Value = 1;
			Adapter.DeleteCommand.Parameters[18].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[17].Value = 0;
			Adapter.DeleteCommand.Parameters[18].Value = Original_B_LevelTime;
		}
		if (Original_B_GInfo.HasValue)
		{
			Adapter.DeleteCommand.Parameters[19].Value = 0;
			Adapter.DeleteCommand.Parameters[20].Value = Original_B_GInfo.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[19].Value = 1;
			Adapter.DeleteCommand.Parameters[20].Value = DBNull.Value;
		}
		if (Original_B_MaxGuest.HasValue)
		{
			Adapter.DeleteCommand.Parameters[21].Value = 0;
			Adapter.DeleteCommand.Parameters[22].Value = Original_B_MaxGuest.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[21].Value = 1;
			Adapter.DeleteCommand.Parameters[22].Value = DBNull.Value;
		}
		if (Original_B_Updatetime.HasValue)
		{
			Adapter.DeleteCommand.Parameters[23].Value = 0;
			Adapter.DeleteCommand.Parameters[24].Value = Original_B_Updatetime.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[23].Value = 1;
			Adapter.DeleteCommand.Parameters[24].Value = DBNull.Value;
		}
		if (Original_B_Updator_ID.HasValue)
		{
			Adapter.DeleteCommand.Parameters[25].Value = 0;
			Adapter.DeleteCommand.Parameters[26].Value = Original_B_Updator_ID.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[25].Value = 1;
			Adapter.DeleteCommand.Parameters[26].Value = DBNull.Value;
		}
		if (Original_B_Updator == null)
		{
			Adapter.DeleteCommand.Parameters[27].Value = 1;
			Adapter.DeleteCommand.Parameters[28].Value = DBNull.Value;
		}
		else
		{
			Adapter.DeleteCommand.Parameters[27].Value = 0;
			Adapter.DeleteCommand.Parameters[28].Value = Original_B_Updator;
		}
		ConnectionState state = Adapter.DeleteCommand.Connection.State;
		if ((Adapter.DeleteCommand.Connection.State & ConnectionState.Open) != ConnectionState.Open)
		{
			Adapter.DeleteCommand.Connection.Open();
		}
		try
		{
			return Adapter.DeleteCommand.ExecuteNonQuery();
		}
		finally
		{
			if (state == ConnectionState.Closed)
			{
				Adapter.DeleteCommand.Connection.Close();
			}
		}
	}

	[DebuggerNonUserCode]
	[HelpKeyword("vs.data.TableAdapter")]
	[DataObjectMethod(DataObjectMethodType.Insert, true)]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public virtual int Insert(string B_HotelName, string B_HotelWeb, string B_HotelID, string B_Address, string B_BookTel, string B_Fax, string B_Post, int? B_StayDay, string B_LevelTime, byte[] B_BackImg, bool? B_GInfo, int? B_MaxGuest, DateTime? B_Updatetime, long? B_Updator_ID, string B_Updator)
	{
		if (B_HotelName == null)
		{
			Adapter.InsertCommand.Parameters[0].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[0].Value = B_HotelName;
		}
		if (B_HotelWeb == null)
		{
			Adapter.InsertCommand.Parameters[1].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[1].Value = B_HotelWeb;
		}
		if (B_HotelID == null)
		{
			Adapter.InsertCommand.Parameters[2].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[2].Value = B_HotelID;
		}
		if (B_Address == null)
		{
			Adapter.InsertCommand.Parameters[3].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[3].Value = B_Address;
		}
		if (B_BookTel == null)
		{
			Adapter.InsertCommand.Parameters[4].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[4].Value = B_BookTel;
		}
		if (B_Fax == null)
		{
			Adapter.InsertCommand.Parameters[5].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[5].Value = B_Fax;
		}
		if (B_Post == null)
		{
			Adapter.InsertCommand.Parameters[6].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[6].Value = B_Post;
		}
		if (B_StayDay.HasValue)
		{
			Adapter.InsertCommand.Parameters[7].Value = B_StayDay.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[7].Value = DBNull.Value;
		}
		if (B_LevelTime == null)
		{
			Adapter.InsertCommand.Parameters[8].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[8].Value = B_LevelTime;
		}
		if (B_BackImg == null)
		{
			Adapter.InsertCommand.Parameters[9].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[9].Value = B_BackImg;
		}
		if (B_GInfo.HasValue)
		{
			Adapter.InsertCommand.Parameters[10].Value = B_GInfo.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[10].Value = DBNull.Value;
		}
		if (B_MaxGuest.HasValue)
		{
			Adapter.InsertCommand.Parameters[11].Value = B_MaxGuest.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[11].Value = DBNull.Value;
		}
		if (B_Updatetime.HasValue)
		{
			Adapter.InsertCommand.Parameters[12].Value = B_Updatetime.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[12].Value = DBNull.Value;
		}
		if (B_Updator_ID.HasValue)
		{
			Adapter.InsertCommand.Parameters[13].Value = B_Updator_ID.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[13].Value = DBNull.Value;
		}
		if (B_Updator == null)
		{
			Adapter.InsertCommand.Parameters[14].Value = DBNull.Value;
		}
		else
		{
			Adapter.InsertCommand.Parameters[14].Value = B_Updator;
		}
		ConnectionState state = Adapter.InsertCommand.Connection.State;
		if ((Adapter.InsertCommand.Connection.State & ConnectionState.Open) != ConnectionState.Open)
		{
			Adapter.InsertCommand.Connection.Open();
		}
		try
		{
			return Adapter.InsertCommand.ExecuteNonQuery();
		}
		finally
		{
			if (state == ConnectionState.Closed)
			{
				Adapter.InsertCommand.Connection.Close();
			}
		}
	}

	[DebuggerNonUserCode]
	[DataObjectMethod(DataObjectMethodType.Update, true)]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[HelpKeyword("vs.data.TableAdapter")]
	public virtual int Update(string B_HotelName, string B_HotelWeb, string B_HotelID, string B_Address, string B_BookTel, string B_Fax, string B_Post, int? B_StayDay, string B_LevelTime, byte[] B_BackImg, bool? B_GInfo, int? B_MaxGuest, DateTime? B_Updatetime, long? B_Updator_ID, string B_Updator, long Original_B_ID, string Original_B_HotelName, string Original_B_HotelWeb, string Original_B_HotelID, string Original_B_Address, string Original_B_BookTel, string Original_B_Fax, string Original_B_Post, int? Original_B_StayDay, string Original_B_LevelTime, bool? Original_B_GInfo, int? Original_B_MaxGuest, DateTime? Original_B_Updatetime, long? Original_B_Updator_ID, string Original_B_Updator, long B_ID)
	{
		if (B_HotelName == null)
		{
			Adapter.UpdateCommand.Parameters[0].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[0].Value = B_HotelName;
		}
		if (B_HotelWeb == null)
		{
			Adapter.UpdateCommand.Parameters[1].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[1].Value = B_HotelWeb;
		}
		if (B_HotelID == null)
		{
			Adapter.UpdateCommand.Parameters[2].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[2].Value = B_HotelID;
		}
		if (B_Address == null)
		{
			Adapter.UpdateCommand.Parameters[3].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[3].Value = B_Address;
		}
		if (B_BookTel == null)
		{
			Adapter.UpdateCommand.Parameters[4].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[4].Value = B_BookTel;
		}
		if (B_Fax == null)
		{
			Adapter.UpdateCommand.Parameters[5].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[5].Value = B_Fax;
		}
		if (B_Post == null)
		{
			Adapter.UpdateCommand.Parameters[6].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[6].Value = B_Post;
		}
		if (B_StayDay.HasValue)
		{
			Adapter.UpdateCommand.Parameters[7].Value = B_StayDay.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[7].Value = DBNull.Value;
		}
		if (B_LevelTime == null)
		{
			Adapter.UpdateCommand.Parameters[8].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[8].Value = B_LevelTime;
		}
		if (B_BackImg == null)
		{
			Adapter.UpdateCommand.Parameters[9].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[9].Value = B_BackImg;
		}
		if (B_GInfo.HasValue)
		{
			Adapter.UpdateCommand.Parameters[10].Value = B_GInfo.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[10].Value = DBNull.Value;
		}
		if (B_MaxGuest.HasValue)
		{
			Adapter.UpdateCommand.Parameters[11].Value = B_MaxGuest.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[11].Value = DBNull.Value;
		}
		if (B_Updatetime.HasValue)
		{
			Adapter.UpdateCommand.Parameters[12].Value = B_Updatetime.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[12].Value = DBNull.Value;
		}
		if (B_Updator_ID.HasValue)
		{
			Adapter.UpdateCommand.Parameters[13].Value = B_Updator_ID.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[13].Value = DBNull.Value;
		}
		if (B_Updator == null)
		{
			Adapter.UpdateCommand.Parameters[14].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[14].Value = B_Updator;
		}
		Adapter.UpdateCommand.Parameters[15].Value = Original_B_ID;
		if (Original_B_HotelName == null)
		{
			Adapter.UpdateCommand.Parameters[16].Value = 1;
			Adapter.UpdateCommand.Parameters[17].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[16].Value = 0;
			Adapter.UpdateCommand.Parameters[17].Value = Original_B_HotelName;
		}
		if (Original_B_HotelWeb == null)
		{
			Adapter.UpdateCommand.Parameters[18].Value = 1;
			Adapter.UpdateCommand.Parameters[19].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[18].Value = 0;
			Adapter.UpdateCommand.Parameters[19].Value = Original_B_HotelWeb;
		}
		if (Original_B_HotelID == null)
		{
			Adapter.UpdateCommand.Parameters[20].Value = 1;
			Adapter.UpdateCommand.Parameters[21].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[20].Value = 0;
			Adapter.UpdateCommand.Parameters[21].Value = Original_B_HotelID;
		}
		if (Original_B_Address == null)
		{
			Adapter.UpdateCommand.Parameters[22].Value = 1;
			Adapter.UpdateCommand.Parameters[23].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[22].Value = 0;
			Adapter.UpdateCommand.Parameters[23].Value = Original_B_Address;
		}
		if (Original_B_BookTel == null)
		{
			Adapter.UpdateCommand.Parameters[24].Value = 1;
			Adapter.UpdateCommand.Parameters[25].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[24].Value = 0;
			Adapter.UpdateCommand.Parameters[25].Value = Original_B_BookTel;
		}
		if (Original_B_Fax == null)
		{
			Adapter.UpdateCommand.Parameters[26].Value = 1;
			Adapter.UpdateCommand.Parameters[27].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[26].Value = 0;
			Adapter.UpdateCommand.Parameters[27].Value = Original_B_Fax;
		}
		if (Original_B_Post == null)
		{
			Adapter.UpdateCommand.Parameters[28].Value = 1;
			Adapter.UpdateCommand.Parameters[29].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[28].Value = 0;
			Adapter.UpdateCommand.Parameters[29].Value = Original_B_Post;
		}
		if (Original_B_StayDay.HasValue)
		{
			Adapter.UpdateCommand.Parameters[30].Value = 0;
			Adapter.UpdateCommand.Parameters[31].Value = Original_B_StayDay.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[30].Value = 1;
			Adapter.UpdateCommand.Parameters[31].Value = DBNull.Value;
		}
		if (Original_B_LevelTime == null)
		{
			Adapter.UpdateCommand.Parameters[32].Value = 1;
			Adapter.UpdateCommand.Parameters[33].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[32].Value = 0;
			Adapter.UpdateCommand.Parameters[33].Value = Original_B_LevelTime;
		}
		if (Original_B_GInfo.HasValue)
		{
			Adapter.UpdateCommand.Parameters[34].Value = 0;
			Adapter.UpdateCommand.Parameters[35].Value = Original_B_GInfo.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[34].Value = 1;
			Adapter.UpdateCommand.Parameters[35].Value = DBNull.Value;
		}
		if (Original_B_MaxGuest.HasValue)
		{
			Adapter.UpdateCommand.Parameters[36].Value = 0;
			Adapter.UpdateCommand.Parameters[37].Value = Original_B_MaxGuest.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[36].Value = 1;
			Adapter.UpdateCommand.Parameters[37].Value = DBNull.Value;
		}
		if (Original_B_Updatetime.HasValue)
		{
			Adapter.UpdateCommand.Parameters[38].Value = 0;
			Adapter.UpdateCommand.Parameters[39].Value = Original_B_Updatetime.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[38].Value = 1;
			Adapter.UpdateCommand.Parameters[39].Value = DBNull.Value;
		}
		if (Original_B_Updator_ID.HasValue)
		{
			Adapter.UpdateCommand.Parameters[40].Value = 0;
			Adapter.UpdateCommand.Parameters[41].Value = Original_B_Updator_ID.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[40].Value = 1;
			Adapter.UpdateCommand.Parameters[41].Value = DBNull.Value;
		}
		if (Original_B_Updator == null)
		{
			Adapter.UpdateCommand.Parameters[42].Value = 1;
			Adapter.UpdateCommand.Parameters[43].Value = DBNull.Value;
		}
		else
		{
			Adapter.UpdateCommand.Parameters[42].Value = 0;
			Adapter.UpdateCommand.Parameters[43].Value = Original_B_Updator;
		}
		Adapter.UpdateCommand.Parameters[44].Value = B_ID;
		ConnectionState state = Adapter.UpdateCommand.Connection.State;
		if ((Adapter.UpdateCommand.Connection.State & ConnectionState.Open) != ConnectionState.Open)
		{
			Adapter.UpdateCommand.Connection.Open();
		}
		try
		{
			return Adapter.UpdateCommand.ExecuteNonQuery();
		}
		finally
		{
			if (state == ConnectionState.Closed)
			{
				Adapter.UpdateCommand.Connection.Close();
			}
		}
	}

	[DataObjectMethod(DataObjectMethodType.Update, true)]
	[HelpKeyword("vs.data.TableAdapter")]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public virtual int Update(string B_HotelName, string B_HotelWeb, string B_HotelID, string B_Address, string B_BookTel, string B_Fax, string B_Post, int? B_StayDay, string B_LevelTime, byte[] B_BackImg, bool? B_GInfo, int? B_MaxGuest, DateTime? B_Updatetime, long? B_Updator_ID, string B_Updator, long Original_B_ID, string Original_B_HotelName, string Original_B_HotelWeb, string Original_B_HotelID, string Original_B_Address, string Original_B_BookTel, string Original_B_Fax, string Original_B_Post, int? Original_B_StayDay, string Original_B_LevelTime, bool? Original_B_GInfo, int? Original_B_MaxGuest, DateTime? Original_B_Updatetime, long? Original_B_Updator_ID, string Original_B_Updator)
	{
		return Update(B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime, B_BackImg, B_GInfo, B_MaxGuest, B_Updatetime, B_Updator_ID, B_Updator, Original_B_ID, Original_B_HotelName, Original_B_HotelWeb, Original_B_HotelID, Original_B_Address, Original_B_BookTel, Original_B_Fax, Original_B_Post, Original_B_StayDay, Original_B_LevelTime, Original_B_GInfo, Original_B_MaxGuest, Original_B_Updatetime, Original_B_Updator_ID, Original_B_Updator, Original_B_ID);
	}
}
