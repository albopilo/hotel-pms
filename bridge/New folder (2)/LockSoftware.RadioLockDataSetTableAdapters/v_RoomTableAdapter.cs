using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using LockSoftware.Properties;

namespace LockSoftware.RadioLockDataSetTableAdapters;

[HelpKeyword("vs.data.TableAdapter")]
[DesignerCategory("code")]
[ToolboxItem(true)]
[DataObject(true)]
[Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class v_RoomTableAdapter : Component
{
	private SqlDataAdapter _adapter;

	private SqlConnection _connection;

	private SqlTransaction _transaction;

	private SqlCommand[] _commandCollection;

	private bool _clearBeforeFill;

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
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

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public v_RoomTableAdapter()
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
		dataTableMapping.DataSetTable = "v_Room";
		dataTableMapping.ColumnMappings.Add("TR_ID", "TR_ID");
		dataTableMapping.ColumnMappings.Add("TR_guestcount", "TR_guestcount");
		dataTableMapping.ColumnMappings.Add("a_id", "a_id");
		dataTableMapping.ColumnMappings.Add("r_id", "r_id");
		dataTableMapping.ColumnMappings.Add("RS_ID", "RS_ID");
		dataTableMapping.ColumnMappings.Add("r_name", "r_name");
		dataTableMapping.ColumnMappings.Add("r_code", "r_code");
		dataTableMapping.ColumnMappings.Add("r_SubCode", "r_SubCode");
		dataTableMapping.ColumnMappings.Add("r_price", "r_price");
		dataTableMapping.ColumnMappings.Add("TR_discount", "TR_discount");
		dataTableMapping.ColumnMappings.Add("TR_deposit", "TR_deposit");
		dataTableMapping.ColumnMappings.Add("TR_cometime", "TR_cometime");
		dataTableMapping.ColumnMappings.Add("TR_stayhour", "TR_stayhour");
		dataTableMapping.ColumnMappings.Add("TR_stand_L_time", "TR_stand_L_time");
		dataTableMapping.ColumnMappings.Add("TR_stayover", "TR_stayover");
		dataTableMapping.ColumnMappings.Add("TR_Level", "TR_Level");
		dataTableMapping.ColumnMappings.Add("TR_actual_L_time", "TR_actual_L_time");
		dataTableMapping.ColumnMappings.Add("TR_roomprice", "TR_roomprice");
		dataTableMapping.ColumnMappings.Add("TR_othprice", "TR_othprice");
		dataTableMapping.ColumnMappings.Add("TR_othp_ID", "TR_othp_ID");
		dataTableMapping.ColumnMappings.Add("TR_basCurrid", "TR_basCurrid");
		dataTableMapping.ColumnMappings.Add("TR_Bascurname", "TR_Bascurname");
		dataTableMapping.ColumnMappings.Add("TR_basrate", "TR_basrate");
		dataTableMapping.ColumnMappings.Add("curr_code", "curr_code");
		dataTableMapping.ColumnMappings.Add("curr_rate", "curr_rate");
		dataTableMapping.ColumnMappings.Add("TR_mustpay", "TR_mustpay");
		dataTableMapping.ColumnMappings.Add("TR_totalpaid", "TR_totalpaid");
		dataTableMapping.ColumnMappings.Add("TR_getchange", "TR_getchange");
		dataTableMapping.ColumnMappings.Add("TR_memo", "TR_memo");
		dataTableMapping.ColumnMappings.Add("TR_sch", "TR_sch");
		dataTableMapping.ColumnMappings.Add("p_typeID", "p_typeID");
		dataTableMapping.ColumnMappings.Add("team_id", "team_id");
		dataTableMapping.ColumnMappings.Add("Createtime", "Createtime");
		dataTableMapping.ColumnMappings.Add("Creator_id", "Creator_id");
		dataTableMapping.ColumnMappings.Add("Creator", "Creator");
		dataTableMapping.ColumnMappings.Add("updatetime", "updatetime");
		dataTableMapping.ColumnMappings.Add("updator_id", "updator_id");
		dataTableMapping.ColumnMappings.Add("updator", "updator");
		dataTableMapping.ColumnMappings.Add("R_FloorID", "R_FloorID");
		dataTableMapping.ColumnMappings.Add("R_TypeID", "R_TypeID");
		dataTableMapping.ColumnMappings.Add("R_RSID", "R_RSID");
		dataTableMapping.ColumnMappings.Add("R_CurGuestCount", "R_CurGuestCount");
		dataTableMapping.ColumnMappings.Add("R_CurGuestID", "R_CurGuestID");
		dataTableMapping.ColumnMappings.Add("R_MaxCardNum", "R_MaxCardNum");
		dataTableMapping.ColumnMappings.Add("R_BedAdd", "R_BedAdd");
		dataTableMapping.ColumnMappings.Add("R_BedSinglePrice", "R_BedSinglePrice");
		dataTableMapping.ColumnMappings.Add("R_Size", "R_Size");
		dataTableMapping.ColumnMappings.Add("R_TotalGuest", "R_TotalGuest");
		dataTableMapping.ColumnMappings.Add("R_TotalPrice", "R_TotalPrice");
		dataTableMapping.ColumnMappings.Add("R_Memo", "R_Memo");
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
		dataTableMapping.ColumnMappings.Add("Build_ID", "Build_ID");
		dataTableMapping.ColumnMappings.Add("Build_Code", "Build_Code");
		dataTableMapping.ColumnMappings.Add("Build_Name", "Build_Name");
		dataTableMapping.ColumnMappings.Add("Build_Flag", "Build_Flag");
		dataTableMapping.ColumnMappings.Add("Build_Memo", "Build_Memo");
		dataTableMapping.ColumnMappings.Add("Floor_Code", "Floor_Code");
		dataTableMapping.ColumnMappings.Add("Floor_Name", "Floor_Name");
		dataTableMapping.ColumnMappings.Add("Floor_Flag", "Floor_Flag");
		dataTableMapping.ColumnMappings.Add("Floor_Memo", "Floor_Memo");
		dataTableMapping.ColumnMappings.Add("TP_Name", "TP_Name");
		dataTableMapping.ColumnMappings.Add("TP_Price", "TP_Price");
		dataTableMapping.ColumnMappings.Add("TP_BedCount", "TP_BedCount");
		dataTableMapping.ColumnMappings.Add("TP_PricelessHour", "TP_PricelessHour");
		dataTableMapping.ColumnMappings.Add("TP_PriceStandHour", "TP_PriceStandHour");
		dataTableMapping.ColumnMappings.Add("TP_RSize", "TP_RSize");
		dataTableMapping.ColumnMappings.Add("TP_Flag", "TP_Flag");
		dataTableMapping.ColumnMappings.Add("TP_Memo", "TP_Memo");
		dataTableMapping.ColumnMappings.Add("RS_Nameen", "RS_Nameen");
		dataTableMapping.ColumnMappings.Add("RS_Name", "RS_Name");
		dataTableMapping.ColumnMappings.Add("RS_Canused", "RS_Canused");
		dataTableMapping.ColumnMappings.Add("RS_flag", "RS_flag");
		dataTableMapping.ColumnMappings.Add("TP_deposit", "TP_deposit");
		dataTableMapping.ColumnMappings.Add("TR_cardcount", "TR_cardcount");
		dataTableMapping.ColumnMappings.Add("TR_SOhour", "TR_SOhour");
		dataTableMapping.ColumnMappings.Add("TR_SOrp", "TR_SOrp");
		dataTableMapping.ColumnMappings.Add("TR_SOdp", "TR_SOdp");
		dataTableMapping.ColumnMappings.Add("TR_SOLTime", "TR_SOLTime");
		dataTableMapping.ColumnMappings.Add("TR_actual_S_Hour", "TR_actual_S_Hour");
		_adapter.TableMappings.Add(dataTableMapping);
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
		_commandCollection[0].CommandText = "SELECT TR_ID, TR_guestcount, a_id, r_id, RS_ID, r_name, r_code, r_SubCode, r_price, TR_discount, TR_deposit, TR_cometime, TR_stayhour, TR_stand_L_time, TR_stayover, TR_Level, TR_actual_L_time, TR_roomprice, TR_othprice, TR_othp_ID, TR_basCurrid, TR_Bascurname, TR_basrate, curr_code, curr_rate, TR_mustpay, TR_totalpaid, TR_getchange, TR_memo, TR_sch, p_typeID, team_id, Createtime, Creator_id, Creator, updatetime, updator_id, updator, R_FloorID, R_TypeID, R_RSID, R_CurGuestCount, R_CurGuestID, R_MaxCardNum, R_BedAdd, R_BedSinglePrice, R_Size, R_TotalGuest, R_TotalPrice, R_Memo, B_ID, B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime, Build_ID, Build_Code, Build_Name, Build_Flag, Build_Memo, Floor_Code, Floor_Name, Floor_Flag, Floor_Memo, TP_Name, TP_Price, TP_BedCount, TP_PricelessHour, TP_PriceStandHour, TP_RSize, TP_Flag, TP_Memo, RS_Nameen, RS_Name, RS_Canused, RS_flag, TP_deposit, TR_cardcount, TR_SOhour, TR_SOrp, TR_SOdp, TR_SOLTime, TR_actual_S_Hour FROM dbo.v_Room";
		_commandCollection[0].CommandType = CommandType.Text;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	[HelpKeyword("vs.data.TableAdapter")]
	[DataObjectMethod(DataObjectMethodType.Fill, true)]
	public virtual int Fill(RadioLockDataSet.v_RoomDataTable dataTable)
	{
		Adapter.SelectCommand = CommandCollection[0];
		if (ClearBeforeFill)
		{
			dataTable.Clear();
		}
		return Adapter.Fill(dataTable);
	}

	[DataObjectMethod(DataObjectMethodType.Select, true)]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	[HelpKeyword("vs.data.TableAdapter")]
	public virtual RadioLockDataSet.v_RoomDataTable GetData()
	{
		Adapter.SelectCommand = CommandCollection[0];
		RadioLockDataSet.v_RoomDataTable v_RoomDataTable = new RadioLockDataSet.v_RoomDataTable();
		Adapter.Fill(v_RoomDataTable);
		return v_RoomDataTable;
	}
}
