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
[DesignerCategory("code")]
[ToolboxItem(true)]
[HelpKeyword("vs.data.DataSet")]
[XmlSchemaProvider("GetTypedDataSetSchema")]
[XmlRoot("RadioLockDataSet")]
public class RadioLockDataSet : DataSet
{
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public delegate void v_RoomRowChangeEventHandler(object sender, v_RoomRowChangeEvent e);

	[Serializable]
	[XmlSchemaProvider("GetTypedTableSchema")]
	public class v_RoomDataTable : DataTable, IEnumerable
	{
		private DataColumn columnTR_ID;

		private DataColumn columnTR_guestcount;

		private DataColumn columna_id;

		private DataColumn columnr_id;

		private DataColumn columnRS_ID;

		private DataColumn columnr_name;

		private DataColumn columnr_code;

		private DataColumn columnr_SubCode;

		private DataColumn columnr_price;

		private DataColumn columnTR_discount;

		private DataColumn columnTR_deposit;

		private DataColumn columnTR_cometime;

		private DataColumn columnTR_stayhour;

		private DataColumn columnTR_stand_L_time;

		private DataColumn columnTR_stayover;

		private DataColumn columnTR_Level;

		private DataColumn columnTR_actual_L_time;

		private DataColumn columnTR_roomprice;

		private DataColumn columnTR_othprice;

		private DataColumn columnTR_othp_ID;

		private DataColumn columnTR_basCurrid;

		private DataColumn columnTR_Bascurname;

		private DataColumn columnTR_basrate;

		private DataColumn columncurr_code;

		private DataColumn columncurr_rate;

		private DataColumn columnTR_mustpay;

		private DataColumn columnTR_totalpaid;

		private DataColumn columnTR_getchange;

		private DataColumn columnTR_memo;

		private DataColumn columnTR_sch;

		private DataColumn columnp_typeID;

		private DataColumn columnteam_id;

		private DataColumn columnCreatetime;

		private DataColumn columnCreator_id;

		private DataColumn columnCreator;

		private DataColumn columnupdatetime;

		private DataColumn columnupdator_id;

		private DataColumn columnupdator;

		private DataColumn columnR_FloorID;

		private DataColumn columnR_TypeID;

		private DataColumn columnR_RSID;

		private DataColumn columnR_CurGuestCount;

		private DataColumn columnR_CurGuestID;

		private DataColumn columnR_MaxCardNum;

		private DataColumn columnR_BedAdd;

		private DataColumn columnR_BedSinglePrice;

		private DataColumn columnR_Size;

		private DataColumn columnR_TotalGuest;

		private DataColumn columnR_TotalPrice;

		private DataColumn columnR_Memo;

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

		private DataColumn columnBuild_ID;

		private DataColumn columnBuild_Code;

		private DataColumn columnBuild_Name;

		private DataColumn columnBuild_Flag;

		private DataColumn columnBuild_Memo;

		private DataColumn columnFloor_Code;

		private DataColumn columnFloor_Name;

		private DataColumn columnFloor_Flag;

		private DataColumn columnFloor_Memo;

		private DataColumn columnTP_Name;

		private DataColumn columnTP_Price;

		private DataColumn columnTP_BedCount;

		private DataColumn columnTP_PricelessHour;

		private DataColumn columnTP_PriceStandHour;

		private DataColumn columnTP_RSize;

		private DataColumn columnTP_Flag;

		private DataColumn columnTP_Memo;

		private DataColumn columnRS_Nameen;

		private DataColumn columnRS_Name;

		private DataColumn columnRS_Canused;

		private DataColumn columnRS_flag;

		private DataColumn columnTP_deposit;

		private DataColumn columnTR_cardcount;

		private DataColumn columnTR_SOhour;

		private DataColumn columnTR_SOrp;

		private DataColumn columnTR_SOdp;

		private DataColumn columnTR_SOLTime;

		private DataColumn columnTR_actual_S_Hour;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_IDColumn => columnTR_ID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_guestcountColumn => columnTR_guestcount;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn a_idColumn => columna_id;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn r_idColumn => columnr_id;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn RS_IDColumn => columnRS_ID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn r_nameColumn => columnr_name;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn r_codeColumn => columnr_code;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn r_SubCodeColumn => columnr_SubCode;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn r_priceColumn => columnr_price;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_discountColumn => columnTR_discount;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_depositColumn => columnTR_deposit;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_cometimeColumn => columnTR_cometime;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_stayhourColumn => columnTR_stayhour;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_stand_L_timeColumn => columnTR_stand_L_time;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_stayoverColumn => columnTR_stayover;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_LevelColumn => columnTR_Level;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_actual_L_timeColumn => columnTR_actual_L_time;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_roompriceColumn => columnTR_roomprice;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_othpriceColumn => columnTR_othprice;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_othp_IDColumn => columnTR_othp_ID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_basCurridColumn => columnTR_basCurrid;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_BascurnameColumn => columnTR_Bascurname;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_basrateColumn => columnTR_basrate;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn curr_codeColumn => columncurr_code;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn curr_rateColumn => columncurr_rate;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_mustpayColumn => columnTR_mustpay;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_totalpaidColumn => columnTR_totalpaid;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_getchangeColumn => columnTR_getchange;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_memoColumn => columnTR_memo;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_schColumn => columnTR_sch;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn p_typeIDColumn => columnp_typeID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn team_idColumn => columnteam_id;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn CreatetimeColumn => columnCreatetime;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn Creator_idColumn => columnCreator_id;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn CreatorColumn => columnCreator;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn updatetimeColumn => columnupdatetime;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn updator_idColumn => columnupdator_id;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn updatorColumn => columnupdator;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn R_FloorIDColumn => columnR_FloorID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_TypeIDColumn => columnR_TypeID;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn R_RSIDColumn => columnR_RSID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_CurGuestCountColumn => columnR_CurGuestCount;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn R_CurGuestIDColumn => columnR_CurGuestID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_MaxCardNumColumn => columnR_MaxCardNum;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_BedAddColumn => columnR_BedAdd;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn R_BedSinglePriceColumn => columnR_BedSinglePrice;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_SizeColumn => columnR_Size;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_TotalGuestColumn => columnR_TotalGuest;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn R_TotalPriceColumn => columnR_TotalPrice;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn R_MemoColumn => columnR_Memo;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_IDColumn => columnB_ID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_HotelNameColumn => columnB_HotelName;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn B_HotelWebColumn => columnB_HotelWeb;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn B_HotelIDColumn => columnB_HotelID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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
		public DataColumn Build_IDColumn => columnBuild_ID;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn Build_CodeColumn => columnBuild_Code;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn Build_NameColumn => columnBuild_Name;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn Build_FlagColumn => columnBuild_Flag;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn Build_MemoColumn => columnBuild_Memo;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn Floor_CodeColumn => columnFloor_Code;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn Floor_NameColumn => columnFloor_Name;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn Floor_FlagColumn => columnFloor_Flag;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn Floor_MemoColumn => columnFloor_Memo;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TP_NameColumn => columnTP_Name;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TP_PriceColumn => columnTP_Price;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TP_BedCountColumn => columnTP_BedCount;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TP_PricelessHourColumn => columnTP_PricelessHour;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TP_PriceStandHourColumn => columnTP_PriceStandHour;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TP_RSizeColumn => columnTP_RSize;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TP_FlagColumn => columnTP_Flag;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TP_MemoColumn => columnTP_Memo;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn RS_NameenColumn => columnRS_Nameen;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn RS_NameColumn => columnRS_Name;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn RS_CanusedColumn => columnRS_Canused;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn RS_flagColumn => columnRS_flag;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TP_depositColumn => columnTP_deposit;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_cardcountColumn => columnTR_cardcount;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_SOhourColumn => columnTR_SOhour;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_SOrpColumn => columnTR_SOrp;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataColumn TR_SOdpColumn => columnTR_SOdp;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_SOLTimeColumn => columnTR_SOLTime;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn TR_actual_S_HourColumn => columnTR_actual_S_Hour;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		[Browsable(false)]
		public int Count => base.Rows.Count;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public v_RoomRow this[int index] => (v_RoomRow)base.Rows[index];

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event v_RoomRowChangeEventHandler v_RoomRowChanging;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event v_RoomRowChangeEventHandler v_RoomRowChanged;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event v_RoomRowChangeEventHandler v_RoomRowDeleting;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event v_RoomRowChangeEventHandler v_RoomRowDeleted;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public v_RoomDataTable()
		{
			base.TableName = "v_Room";
			BeginInit();
			InitClass();
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal v_RoomDataTable(DataTable table)
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
		protected v_RoomDataTable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			InitVars();
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Addv_RoomRow(v_RoomRow row)
		{
			base.Rows.Add(row);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public v_RoomRow Addv_RoomRow(long TR_ID, int TR_guestcount, long a_id, long r_id, int RS_ID, string r_name, string r_code, int r_SubCode, decimal r_price, decimal TR_discount, decimal TR_deposit, DateTime TR_cometime, decimal TR_stayhour, DateTime TR_stand_L_time, bool TR_stayover, bool TR_Level, DateTime TR_actual_L_time, decimal TR_roomprice, decimal TR_othprice, string TR_othp_ID, int TR_basCurrid, string TR_Bascurname, decimal TR_basrate, string curr_code, decimal curr_rate, decimal TR_mustpay, decimal TR_totalpaid, decimal TR_getchange, string TR_memo, bool TR_sch, long p_typeID, long team_id, DateTime Createtime, long Creator_id, string Creator, DateTime updatetime, long updator_id, string updator, long R_FloorID, long R_TypeID, int R_RSID, int R_CurGuestCount, long R_CurGuestID, long R_MaxCardNum, int R_BedAdd, decimal R_BedSinglePrice, string R_Size, long R_TotalGuest, decimal R_TotalPrice, string R_Memo, long B_ID, string B_HotelName, string B_HotelWeb, string B_HotelID, string B_Address, string B_BookTel, string B_Fax, string B_Post, int B_StayDay, string B_LevelTime, long Build_ID, string Build_Code, string Build_Name, bool Build_Flag, string Build_Memo, string Floor_Code, string Floor_Name, bool Floor_Flag, string Floor_Memo, string TP_Name, decimal TP_Price, int TP_BedCount, decimal TP_PricelessHour, decimal TP_PriceStandHour, string TP_RSize, bool TP_Flag, string TP_Memo, string RS_Nameen, string RS_Name, bool RS_Canused, bool RS_flag, decimal TP_deposit, int TR_cardcount, decimal TR_SOhour, decimal TR_SOrp, decimal TR_SOdp, DateTime TR_SOLTime, decimal TR_actual_S_Hour)
		{
			v_RoomRow v_RoomRow2 = (v_RoomRow)NewRow();
			object[] itemArray = new object[88]
			{
				TR_ID, TR_guestcount, a_id, r_id, RS_ID, r_name, r_code, r_SubCode, r_price, TR_discount,
				TR_deposit, TR_cometime, TR_stayhour, TR_stand_L_time, TR_stayover, TR_Level, TR_actual_L_time, TR_roomprice, TR_othprice, TR_othp_ID,
				TR_basCurrid, TR_Bascurname, TR_basrate, curr_code, curr_rate, TR_mustpay, TR_totalpaid, TR_getchange, TR_memo, TR_sch,
				p_typeID, team_id, Createtime, Creator_id, Creator, updatetime, updator_id, updator, R_FloorID, R_TypeID,
				R_RSID, R_CurGuestCount, R_CurGuestID, R_MaxCardNum, R_BedAdd, R_BedSinglePrice, R_Size, R_TotalGuest, R_TotalPrice, R_Memo,
				B_ID, B_HotelName, B_HotelWeb, B_HotelID, B_Address, B_BookTel, B_Fax, B_Post, B_StayDay, B_LevelTime,
				Build_ID, Build_Code, Build_Name, Build_Flag, Build_Memo, Floor_Code, Floor_Name, Floor_Flag, Floor_Memo, TP_Name,
				TP_Price, TP_BedCount, TP_PricelessHour, TP_PriceStandHour, TP_RSize, TP_Flag, TP_Memo, RS_Nameen, RS_Name, RS_Canused,
				RS_flag, TP_deposit, TR_cardcount, TR_SOhour, TR_SOrp, TR_SOdp, TR_SOLTime, TR_actual_S_Hour
			};
			v_RoomRow2.ItemArray = itemArray;
			base.Rows.Add(v_RoomRow2);
			return v_RoomRow2;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public virtual IEnumerator GetEnumerator()
		{
			return base.Rows.GetEnumerator();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override DataTable Clone()
		{
			v_RoomDataTable v_RoomDataTable2 = (v_RoomDataTable)base.Clone();
			v_RoomDataTable2.InitVars();
			return v_RoomDataTable2;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataTable CreateInstance()
		{
			return new v_RoomDataTable();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			columnTR_ID = base.Columns["TR_ID"];
			columnTR_guestcount = base.Columns["TR_guestcount"];
			columna_id = base.Columns["a_id"];
			columnr_id = base.Columns["r_id"];
			columnRS_ID = base.Columns["RS_ID"];
			columnr_name = base.Columns["r_name"];
			columnr_code = base.Columns["r_code"];
			columnr_SubCode = base.Columns["r_SubCode"];
			columnr_price = base.Columns["r_price"];
			columnTR_discount = base.Columns["TR_discount"];
			columnTR_deposit = base.Columns["TR_deposit"];
			columnTR_cometime = base.Columns["TR_cometime"];
			columnTR_stayhour = base.Columns["TR_stayhour"];
			columnTR_stand_L_time = base.Columns["TR_stand_L_time"];
			columnTR_stayover = base.Columns["TR_stayover"];
			columnTR_Level = base.Columns["TR_Level"];
			columnTR_actual_L_time = base.Columns["TR_actual_L_time"];
			columnTR_roomprice = base.Columns["TR_roomprice"];
			columnTR_othprice = base.Columns["TR_othprice"];
			columnTR_othp_ID = base.Columns["TR_othp_ID"];
			columnTR_basCurrid = base.Columns["TR_basCurrid"];
			columnTR_Bascurname = base.Columns["TR_Bascurname"];
			columnTR_basrate = base.Columns["TR_basrate"];
			columncurr_code = base.Columns["curr_code"];
			columncurr_rate = base.Columns["curr_rate"];
			columnTR_mustpay = base.Columns["TR_mustpay"];
			columnTR_totalpaid = base.Columns["TR_totalpaid"];
			columnTR_getchange = base.Columns["TR_getchange"];
			columnTR_memo = base.Columns["TR_memo"];
			columnTR_sch = base.Columns["TR_sch"];
			columnp_typeID = base.Columns["p_typeID"];
			columnteam_id = base.Columns["team_id"];
			columnCreatetime = base.Columns["Createtime"];
			columnCreator_id = base.Columns["Creator_id"];
			columnCreator = base.Columns["Creator"];
			columnupdatetime = base.Columns["updatetime"];
			columnupdator_id = base.Columns["updator_id"];
			columnupdator = base.Columns["updator"];
			columnR_FloorID = base.Columns["R_FloorID"];
			columnR_TypeID = base.Columns["R_TypeID"];
			columnR_RSID = base.Columns["R_RSID"];
			columnR_CurGuestCount = base.Columns["R_CurGuestCount"];
			columnR_CurGuestID = base.Columns["R_CurGuestID"];
			columnR_MaxCardNum = base.Columns["R_MaxCardNum"];
			columnR_BedAdd = base.Columns["R_BedAdd"];
			columnR_BedSinglePrice = base.Columns["R_BedSinglePrice"];
			columnR_Size = base.Columns["R_Size"];
			columnR_TotalGuest = base.Columns["R_TotalGuest"];
			columnR_TotalPrice = base.Columns["R_TotalPrice"];
			columnR_Memo = base.Columns["R_Memo"];
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
			columnBuild_ID = base.Columns["Build_ID"];
			columnBuild_Code = base.Columns["Build_Code"];
			columnBuild_Name = base.Columns["Build_Name"];
			columnBuild_Flag = base.Columns["Build_Flag"];
			columnBuild_Memo = base.Columns["Build_Memo"];
			columnFloor_Code = base.Columns["Floor_Code"];
			columnFloor_Name = base.Columns["Floor_Name"];
			columnFloor_Flag = base.Columns["Floor_Flag"];
			columnFloor_Memo = base.Columns["Floor_Memo"];
			columnTP_Name = base.Columns["TP_Name"];
			columnTP_Price = base.Columns["TP_Price"];
			columnTP_BedCount = base.Columns["TP_BedCount"];
			columnTP_PricelessHour = base.Columns["TP_PricelessHour"];
			columnTP_PriceStandHour = base.Columns["TP_PriceStandHour"];
			columnTP_RSize = base.Columns["TP_RSize"];
			columnTP_Flag = base.Columns["TP_Flag"];
			columnTP_Memo = base.Columns["TP_Memo"];
			columnRS_Nameen = base.Columns["RS_Nameen"];
			columnRS_Name = base.Columns["RS_Name"];
			columnRS_Canused = base.Columns["RS_Canused"];
			columnRS_flag = base.Columns["RS_flag"];
			columnTP_deposit = base.Columns["TP_deposit"];
			columnTR_cardcount = base.Columns["TR_cardcount"];
			columnTR_SOhour = base.Columns["TR_SOhour"];
			columnTR_SOrp = base.Columns["TR_SOrp"];
			columnTR_SOdp = base.Columns["TR_SOdp"];
			columnTR_SOLTime = base.Columns["TR_SOLTime"];
			columnTR_actual_S_Hour = base.Columns["TR_actual_S_Hour"];
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			columnTR_ID = new DataColumn("TR_ID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnTR_ID);
			columnTR_guestcount = new DataColumn("TR_guestcount", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnTR_guestcount);
			columna_id = new DataColumn("a_id", typeof(long), null, MappingType.Element);
			base.Columns.Add(columna_id);
			columnr_id = new DataColumn("r_id", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnr_id);
			columnRS_ID = new DataColumn("RS_ID", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnRS_ID);
			columnr_name = new DataColumn("r_name", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnr_name);
			columnr_code = new DataColumn("r_code", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnr_code);
			columnr_SubCode = new DataColumn("r_SubCode", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnr_SubCode);
			columnr_price = new DataColumn("r_price", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnr_price);
			columnTR_discount = new DataColumn("TR_discount", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_discount);
			columnTR_deposit = new DataColumn("TR_deposit", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_deposit);
			columnTR_cometime = new DataColumn("TR_cometime", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnTR_cometime);
			columnTR_stayhour = new DataColumn("TR_stayhour", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_stayhour);
			columnTR_stand_L_time = new DataColumn("TR_stand_L_time", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnTR_stand_L_time);
			columnTR_stayover = new DataColumn("TR_stayover", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnTR_stayover);
			columnTR_Level = new DataColumn("TR_Level", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnTR_Level);
			columnTR_actual_L_time = new DataColumn("TR_actual_L_time", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnTR_actual_L_time);
			columnTR_roomprice = new DataColumn("TR_roomprice", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_roomprice);
			columnTR_othprice = new DataColumn("TR_othprice", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_othprice);
			columnTR_othp_ID = new DataColumn("TR_othp_ID", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnTR_othp_ID);
			columnTR_basCurrid = new DataColumn("TR_basCurrid", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnTR_basCurrid);
			columnTR_Bascurname = new DataColumn("TR_Bascurname", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnTR_Bascurname);
			columnTR_basrate = new DataColumn("TR_basrate", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_basrate);
			columncurr_code = new DataColumn("curr_code", typeof(string), null, MappingType.Element);
			base.Columns.Add(columncurr_code);
			columncurr_rate = new DataColumn("curr_rate", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columncurr_rate);
			columnTR_mustpay = new DataColumn("TR_mustpay", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_mustpay);
			columnTR_totalpaid = new DataColumn("TR_totalpaid", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_totalpaid);
			columnTR_getchange = new DataColumn("TR_getchange", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_getchange);
			columnTR_memo = new DataColumn("TR_memo", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnTR_memo);
			columnTR_sch = new DataColumn("TR_sch", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnTR_sch);
			columnp_typeID = new DataColumn("p_typeID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnp_typeID);
			columnteam_id = new DataColumn("team_id", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnteam_id);
			columnCreatetime = new DataColumn("Createtime", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnCreatetime);
			columnCreator_id = new DataColumn("Creator_id", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnCreator_id);
			columnCreator = new DataColumn("Creator", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnCreator);
			columnupdatetime = new DataColumn("updatetime", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnupdatetime);
			columnupdator_id = new DataColumn("updator_id", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnupdator_id);
			columnupdator = new DataColumn("updator", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnupdator);
			columnR_FloorID = new DataColumn("R_FloorID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnR_FloorID);
			columnR_TypeID = new DataColumn("R_TypeID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnR_TypeID);
			columnR_RSID = new DataColumn("R_RSID", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnR_RSID);
			columnR_CurGuestCount = new DataColumn("R_CurGuestCount", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnR_CurGuestCount);
			columnR_CurGuestID = new DataColumn("R_CurGuestID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnR_CurGuestID);
			columnR_MaxCardNum = new DataColumn("R_MaxCardNum", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnR_MaxCardNum);
			columnR_BedAdd = new DataColumn("R_BedAdd", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnR_BedAdd);
			columnR_BedSinglePrice = new DataColumn("R_BedSinglePrice", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnR_BedSinglePrice);
			columnR_Size = new DataColumn("R_Size", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnR_Size);
			columnR_TotalGuest = new DataColumn("R_TotalGuest", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnR_TotalGuest);
			columnR_TotalPrice = new DataColumn("R_TotalPrice", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnR_TotalPrice);
			columnR_Memo = new DataColumn("R_Memo", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnR_Memo);
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
			columnBuild_ID = new DataColumn("Build_ID", typeof(long), null, MappingType.Element);
			base.Columns.Add(columnBuild_ID);
			columnBuild_Code = new DataColumn("Build_Code", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnBuild_Code);
			columnBuild_Name = new DataColumn("Build_Name", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnBuild_Name);
			columnBuild_Flag = new DataColumn("Build_Flag", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnBuild_Flag);
			columnBuild_Memo = new DataColumn("Build_Memo", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnBuild_Memo);
			columnFloor_Code = new DataColumn("Floor_Code", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnFloor_Code);
			columnFloor_Name = new DataColumn("Floor_Name", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnFloor_Name);
			columnFloor_Flag = new DataColumn("Floor_Flag", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnFloor_Flag);
			columnFloor_Memo = new DataColumn("Floor_Memo", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnFloor_Memo);
			columnTP_Name = new DataColumn("TP_Name", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnTP_Name);
			columnTP_Price = new DataColumn("TP_Price", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTP_Price);
			columnTP_BedCount = new DataColumn("TP_BedCount", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnTP_BedCount);
			columnTP_PricelessHour = new DataColumn("TP_PricelessHour", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTP_PricelessHour);
			columnTP_PriceStandHour = new DataColumn("TP_PriceStandHour", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTP_PriceStandHour);
			columnTP_RSize = new DataColumn("TP_RSize", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnTP_RSize);
			columnTP_Flag = new DataColumn("TP_Flag", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnTP_Flag);
			columnTP_Memo = new DataColumn("TP_Memo", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnTP_Memo);
			columnRS_Nameen = new DataColumn("RS_Nameen", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnRS_Nameen);
			columnRS_Name = new DataColumn("RS_Name", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnRS_Name);
			columnRS_Canused = new DataColumn("RS_Canused", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnRS_Canused);
			columnRS_flag = new DataColumn("RS_flag", typeof(bool), null, MappingType.Element);
			base.Columns.Add(columnRS_flag);
			columnTP_deposit = new DataColumn("TP_deposit", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTP_deposit);
			columnTR_cardcount = new DataColumn("TR_cardcount", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnTR_cardcount);
			columnTR_SOhour = new DataColumn("TR_SOhour", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_SOhour);
			columnTR_SOrp = new DataColumn("TR_SOrp", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_SOrp);
			columnTR_SOdp = new DataColumn("TR_SOdp", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_SOdp);
			columnTR_SOLTime = new DataColumn("TR_SOLTime", typeof(DateTime), null, MappingType.Element);
			base.Columns.Add(columnTR_SOLTime);
			columnTR_actual_S_Hour = new DataColumn("TR_actual_S_Hour", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnTR_actual_S_Hour);
			columnTR_ID.AllowDBNull = false;
			columnr_name.MaxLength = 50;
			columnr_code.MaxLength = 8;
			columnTR_othp_ID.MaxLength = int.MaxValue;
			columnTR_Bascurname.MaxLength = 20;
			columncurr_code.MaxLength = 20;
			columnTR_memo.MaxLength = int.MaxValue;
			columnCreator.MaxLength = 20;
			columnupdator.MaxLength = 20;
			columnR_Size.MaxLength = 50;
			columnR_Memo.MaxLength = int.MaxValue;
			columnB_ID.AllowDBNull = false;
			columnB_HotelName.MaxLength = 128;
			columnB_HotelWeb.MaxLength = 256;
			columnB_HotelID.MaxLength = 128;
			columnB_Address.MaxLength = 256;
			columnB_BookTel.MaxLength = 50;
			columnB_Fax.MaxLength = 50;
			columnB_Post.MaxLength = 50;
			columnB_LevelTime.MaxLength = 10;
			columnBuild_Code.MaxLength = 8;
			columnBuild_Name.MaxLength = 50;
			columnBuild_Memo.MaxLength = int.MaxValue;
			columnFloor_Code.MaxLength = 8;
			columnFloor_Name.MaxLength = 50;
			columnFloor_Memo.MaxLength = int.MaxValue;
			columnTP_Name.MaxLength = 50;
			columnTP_RSize.MaxLength = 50;
			columnTP_Memo.MaxLength = int.MaxValue;
			columnRS_Nameen.MaxLength = 100;
			columnRS_Name.MaxLength = 50;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public v_RoomRow Newv_RoomRow()
		{
			return (v_RoomRow)NewRow();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new v_RoomRow(builder);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override Type GetRowType()
		{
			return typeof(v_RoomRow);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);
			if (v_RoomRowChanged != null)
			{
				v_RoomRowChanged(this, new v_RoomRowChangeEvent((v_RoomRow)e.Row, e.Action));
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override void OnRowChanging(DataRowChangeEventArgs e)
		{
			base.OnRowChanging(e);
			if (v_RoomRowChanging != null)
			{
				v_RoomRowChanging(this, new v_RoomRowChangeEvent((v_RoomRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleted(DataRowChangeEventArgs e)
		{
			base.OnRowDeleted(e);
			if (v_RoomRowDeleted != null)
			{
				v_RoomRowDeleted(this, new v_RoomRowChangeEvent((v_RoomRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleting(DataRowChangeEventArgs e)
		{
			base.OnRowDeleting(e);
			if (v_RoomRowDeleting != null)
			{
				v_RoomRowDeleting(this, new v_RoomRowChangeEvent((v_RoomRow)e.Row, e.Action));
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Removev_RoomRow(v_RoomRow row)
		{
			base.Rows.Remove(row);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			RadioLockDataSet radioLockDataSet = new RadioLockDataSet();
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
			xmlSchemaAttribute.FixedValue = radioLockDataSet.Namespace;
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "tableTypeName";
			xmlSchemaAttribute2.FixedValue = "v_RoomDataTable";
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = radioLockDataSet.GetSchemaSerializable();
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

	public class v_RoomRow : DataRow
	{
		private v_RoomDataTable tablev_Room;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long TR_ID
		{
			get
			{
				return (long)base[tablev_Room.TR_IDColumn];
			}
			set
			{
				base[tablev_Room.TR_IDColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int TR_guestcount
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.TR_guestcountColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_guestcount”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_guestcountColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long a_id
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.a_idColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“a_id”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.a_idColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long r_id
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.r_idColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“r_id”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.r_idColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int RS_ID
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.RS_IDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“RS_ID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.RS_IDColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string r_name
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.r_nameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“r_name”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.r_nameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string r_code
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.r_codeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“r_code”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.r_codeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int r_SubCode
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.r_SubCodeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“r_SubCode”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.r_SubCodeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal r_price
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.r_priceColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“r_price”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.r_priceColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal TR_discount
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_discountColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_discount”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_discountColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_deposit
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_depositColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_deposit”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_depositColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DateTime TR_cometime
		{
			get
			{
				try
				{
					return (DateTime)base[tablev_Room.TR_cometimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_cometime”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_cometimeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_stayhour
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_stayhourColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_stayhour”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_stayhourColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DateTime TR_stand_L_time
		{
			get
			{
				try
				{
					return (DateTime)base[tablev_Room.TR_stand_L_timeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_stand_L_time”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_stand_L_timeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool TR_stayover
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.TR_stayoverColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_stayover”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_stayoverColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool TR_Level
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.TR_LevelColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_Level”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_LevelColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DateTime TR_actual_L_time
		{
			get
			{
				try
				{
					return (DateTime)base[tablev_Room.TR_actual_L_timeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_actual_L_time”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_actual_L_timeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal TR_roomprice
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_roompriceColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_roomprice”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_roompriceColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_othprice
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_othpriceColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_othprice”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_othpriceColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string TR_othp_ID
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.TR_othp_IDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_othp_ID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_othp_IDColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public int TR_basCurrid
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.TR_basCurridColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_basCurrid”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_basCurridColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string TR_Bascurname
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.TR_BascurnameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_Bascurname”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_BascurnameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal TR_basrate
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_basrateColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_basrate”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_basrateColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string curr_code
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.curr_codeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“curr_code”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.curr_codeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal curr_rate
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.curr_rateColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“curr_rate”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.curr_rateColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_mustpay
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_mustpayColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_mustpay”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_mustpayColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_totalpaid
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_totalpaidColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_totalpaid”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_totalpaidColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_getchange
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_getchangeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_getchange”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_getchangeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string TR_memo
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.TR_memoColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_memo”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_memoColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool TR_sch
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.TR_schColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_sch”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_schColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long p_typeID
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.p_typeIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“p_typeID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.p_typeIDColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long team_id
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.team_idColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“team_id”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.team_idColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DateTime Createtime
		{
			get
			{
				try
				{
					return (DateTime)base[tablev_Room.CreatetimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Createtime”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.CreatetimeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long Creator_id
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.Creator_idColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Creator_id”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Creator_idColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Creator
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.CreatorColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Creator”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.CreatorColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DateTime updatetime
		{
			get
			{
				try
				{
					return (DateTime)base[tablev_Room.updatetimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“updatetime”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.updatetimeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long updator_id
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.updator_idColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“updator_id”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.updator_idColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string updator
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.updatorColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“updator”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.updatorColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long R_FloorID
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.R_FloorIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_FloorID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_FloorIDColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long R_TypeID
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.R_TypeIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_TypeID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_TypeIDColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int R_RSID
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.R_RSIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_RSID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_RSIDColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public int R_CurGuestCount
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.R_CurGuestCountColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_CurGuestCount”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_CurGuestCountColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long R_CurGuestID
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.R_CurGuestIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_CurGuestID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_CurGuestIDColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long R_MaxCardNum
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.R_MaxCardNumColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_MaxCardNum”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_MaxCardNumColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int R_BedAdd
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.R_BedAddColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_BedAdd”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_BedAddColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal R_BedSinglePrice
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.R_BedSinglePriceColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_BedSinglePrice”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_BedSinglePriceColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string R_Size
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.R_SizeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_Size”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_SizeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long R_TotalGuest
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.R_TotalGuestColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_TotalGuest”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_TotalGuestColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal R_TotalPrice
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.R_TotalPriceColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_TotalPrice”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_TotalPriceColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string R_Memo
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.R_MemoColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“R_Memo”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.R_MemoColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public long B_ID
		{
			get
			{
				return (long)base[tablev_Room.B_IDColumn];
			}
			set
			{
				base[tablev_Room.B_IDColumn] = value;
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
					return (string)base[tablev_Room.B_HotelNameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_HotelName”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_HotelNameColumn] = value;
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
					return (string)base[tablev_Room.B_HotelWebColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_HotelWeb”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_HotelWebColumn] = value;
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
					return (string)base[tablev_Room.B_HotelIDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_HotelID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_HotelIDColumn] = value;
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
					return (string)base[tablev_Room.B_AddressColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_Address”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_AddressColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string B_BookTel
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.B_BookTelColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_BookTel”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_BookTelColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string B_Fax
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.B_FaxColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_Fax”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_FaxColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string B_Post
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.B_PostColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_Post”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_PostColumn] = value;
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
					return (int)base[tablev_Room.B_StayDayColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_StayDay”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_StayDayColumn] = value;
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
					return (string)base[tablev_Room.B_LevelTimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“B_LevelTime”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.B_LevelTimeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public long Build_ID
		{
			get
			{
				try
				{
					return (long)base[tablev_Room.Build_IDColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Build_ID”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Build_IDColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string Build_Code
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.Build_CodeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Build_Code”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Build_CodeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Build_Name
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.Build_NameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Build_Name”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Build_NameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Build_Flag
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.Build_FlagColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Build_Flag”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Build_FlagColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string Build_Memo
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.Build_MemoColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Build_Memo”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Build_MemoColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string Floor_Code
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.Floor_CodeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Floor_Code”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Floor_CodeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Floor_Name
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.Floor_NameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Floor_Name”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Floor_NameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Floor_Flag
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.Floor_FlagColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Floor_Flag”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Floor_FlagColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string Floor_Memo
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.Floor_MemoColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“Floor_Memo”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.Floor_MemoColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string TP_Name
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.TP_NameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_Name”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_NameColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TP_Price
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TP_PriceColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_Price”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_PriceColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public int TP_BedCount
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.TP_BedCountColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_BedCount”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_BedCountColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TP_PricelessHour
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TP_PricelessHourColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_PricelessHour”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_PricelessHourColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TP_PriceStandHour
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TP_PriceStandHourColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_PriceStandHour”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_PriceStandHourColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string TP_RSize
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.TP_RSizeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_RSize”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_RSizeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool TP_Flag
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.TP_FlagColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_Flag”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_FlagColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string TP_Memo
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.TP_MemoColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_Memo”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_MemoColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string RS_Nameen
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.RS_NameenColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“RS_Nameen”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.RS_NameenColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public string RS_Name
		{
			get
			{
				try
				{
					return (string)base[tablev_Room.RS_NameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“RS_Name”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.RS_NameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool RS_Canused
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.RS_CanusedColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“RS_Canused”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.RS_CanusedColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool RS_flag
		{
			get
			{
				try
				{
					return (bool)base[tablev_Room.RS_flagColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“RS_flag”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.RS_flagColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal TP_deposit
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TP_depositColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TP_deposit”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TP_depositColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public int TR_cardcount
		{
			get
			{
				try
				{
					return (int)base[tablev_Room.TR_cardcountColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_cardcount”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_cardcountColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal TR_SOhour
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_SOhourColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_SOhour”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_SOhourColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_SOrp
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_SOrpColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_SOrp”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_SOrpColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_SOdp
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_SOdpColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_SOdp”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_SOdpColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DateTime TR_SOLTime
		{
			get
			{
				try
				{
					return (DateTime)base[tablev_Room.TR_SOLTimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_SOLTime”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_SOLTimeColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public decimal TR_actual_S_Hour
		{
			get
			{
				try
				{
					return (decimal)base[tablev_Room.TR_actual_S_HourColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“v_Room”中列“TR_actual_S_Hour”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablev_Room.TR_actual_S_HourColumn] = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal v_RoomRow(DataRowBuilder rb)
			: base(rb)
		{
			tablev_Room = (v_RoomDataTable)base.Table;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_guestcountNull()
		{
			return IsNull(tablev_Room.TR_guestcountColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_guestcountNull()
		{
			base[tablev_Room.TR_guestcountColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool Isa_idNull()
		{
			return IsNull(tablev_Room.a_idColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void Seta_idNull()
		{
			base[tablev_Room.a_idColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool Isr_idNull()
		{
			return IsNull(tablev_Room.r_idColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Setr_idNull()
		{
			base[tablev_Room.r_idColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsRS_IDNull()
		{
			return IsNull(tablev_Room.RS_IDColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetRS_IDNull()
		{
			base[tablev_Room.RS_IDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool Isr_nameNull()
		{
			return IsNull(tablev_Room.r_nameColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void Setr_nameNull()
		{
			base[tablev_Room.r_nameColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool Isr_codeNull()
		{
			return IsNull(tablev_Room.r_codeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void Setr_codeNull()
		{
			base[tablev_Room.r_codeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool Isr_SubCodeNull()
		{
			return IsNull(tablev_Room.r_SubCodeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void Setr_SubCodeNull()
		{
			base[tablev_Room.r_SubCodeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Isr_priceNull()
		{
			return IsNull(tablev_Room.r_priceColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void Setr_priceNull()
		{
			base[tablev_Room.r_priceColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_discountNull()
		{
			return IsNull(tablev_Room.TR_discountColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_discountNull()
		{
			base[tablev_Room.TR_discountColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_depositNull()
		{
			return IsNull(tablev_Room.TR_depositColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_depositNull()
		{
			base[tablev_Room.TR_depositColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_cometimeNull()
		{
			return IsNull(tablev_Room.TR_cometimeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_cometimeNull()
		{
			base[tablev_Room.TR_cometimeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_stayhourNull()
		{
			return IsNull(tablev_Room.TR_stayhourColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_stayhourNull()
		{
			base[tablev_Room.TR_stayhourColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_stand_L_timeNull()
		{
			return IsNull(tablev_Room.TR_stand_L_timeColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_stand_L_timeNull()
		{
			base[tablev_Room.TR_stand_L_timeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_stayoverNull()
		{
			return IsNull(tablev_Room.TR_stayoverColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_stayoverNull()
		{
			base[tablev_Room.TR_stayoverColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_LevelNull()
		{
			return IsNull(tablev_Room.TR_LevelColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_LevelNull()
		{
			base[tablev_Room.TR_LevelColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_actual_L_timeNull()
		{
			return IsNull(tablev_Room.TR_actual_L_timeColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_actual_L_timeNull()
		{
			base[tablev_Room.TR_actual_L_timeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_roompriceNull()
		{
			return IsNull(tablev_Room.TR_roompriceColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_roompriceNull()
		{
			base[tablev_Room.TR_roompriceColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_othpriceNull()
		{
			return IsNull(tablev_Room.TR_othpriceColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_othpriceNull()
		{
			base[tablev_Room.TR_othpriceColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_othp_IDNull()
		{
			return IsNull(tablev_Room.TR_othp_IDColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_othp_IDNull()
		{
			base[tablev_Room.TR_othp_IDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_basCurridNull()
		{
			return IsNull(tablev_Room.TR_basCurridColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_basCurridNull()
		{
			base[tablev_Room.TR_basCurridColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_BascurnameNull()
		{
			return IsNull(tablev_Room.TR_BascurnameColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_BascurnameNull()
		{
			base[tablev_Room.TR_BascurnameColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_basrateNull()
		{
			return IsNull(tablev_Room.TR_basrateColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_basrateNull()
		{
			base[tablev_Room.TR_basrateColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Iscurr_codeNull()
		{
			return IsNull(tablev_Room.curr_codeColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Setcurr_codeNull()
		{
			base[tablev_Room.curr_codeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Iscurr_rateNull()
		{
			return IsNull(tablev_Room.curr_rateColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Setcurr_rateNull()
		{
			base[tablev_Room.curr_rateColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_mustpayNull()
		{
			return IsNull(tablev_Room.TR_mustpayColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_mustpayNull()
		{
			base[tablev_Room.TR_mustpayColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_totalpaidNull()
		{
			return IsNull(tablev_Room.TR_totalpaidColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_totalpaidNull()
		{
			base[tablev_Room.TR_totalpaidColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_getchangeNull()
		{
			return IsNull(tablev_Room.TR_getchangeColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_getchangeNull()
		{
			base[tablev_Room.TR_getchangeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_memoNull()
		{
			return IsNull(tablev_Room.TR_memoColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_memoNull()
		{
			base[tablev_Room.TR_memoColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_schNull()
		{
			return IsNull(tablev_Room.TR_schColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_schNull()
		{
			base[tablev_Room.TR_schColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Isp_typeIDNull()
		{
			return IsNull(tablev_Room.p_typeIDColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Setp_typeIDNull()
		{
			base[tablev_Room.p_typeIDColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Isteam_idNull()
		{
			return IsNull(tablev_Room.team_idColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void Setteam_idNull()
		{
			base[tablev_Room.team_idColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsCreatetimeNull()
		{
			return IsNull(tablev_Room.CreatetimeColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetCreatetimeNull()
		{
			base[tablev_Room.CreatetimeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsCreator_idNull()
		{
			return IsNull(tablev_Room.Creator_idColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetCreator_idNull()
		{
			base[tablev_Room.Creator_idColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsCreatorNull()
		{
			return IsNull(tablev_Room.CreatorColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetCreatorNull()
		{
			base[tablev_Room.CreatorColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsupdatetimeNull()
		{
			return IsNull(tablev_Room.updatetimeColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetupdatetimeNull()
		{
			base[tablev_Room.updatetimeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool Isupdator_idNull()
		{
			return IsNull(tablev_Room.updator_idColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void Setupdator_idNull()
		{
			base[tablev_Room.updator_idColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsupdatorNull()
		{
			return IsNull(tablev_Room.updatorColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetupdatorNull()
		{
			base[tablev_Room.updatorColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsR_FloorIDNull()
		{
			return IsNull(tablev_Room.R_FloorIDColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_FloorIDNull()
		{
			base[tablev_Room.R_FloorIDColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_TypeIDNull()
		{
			return IsNull(tablev_Room.R_TypeIDColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_TypeIDNull()
		{
			base[tablev_Room.R_TypeIDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsR_RSIDNull()
		{
			return IsNull(tablev_Room.R_RSIDColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_RSIDNull()
		{
			base[tablev_Room.R_RSIDColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_CurGuestCountNull()
		{
			return IsNull(tablev_Room.R_CurGuestCountColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_CurGuestCountNull()
		{
			base[tablev_Room.R_CurGuestCountColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_CurGuestIDNull()
		{
			return IsNull(tablev_Room.R_CurGuestIDColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_CurGuestIDNull()
		{
			base[tablev_Room.R_CurGuestIDColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_MaxCardNumNull()
		{
			return IsNull(tablev_Room.R_MaxCardNumColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_MaxCardNumNull()
		{
			base[tablev_Room.R_MaxCardNumColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsR_BedAddNull()
		{
			return IsNull(tablev_Room.R_BedAddColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_BedAddNull()
		{
			base[tablev_Room.R_BedAddColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_BedSinglePriceNull()
		{
			return IsNull(tablev_Room.R_BedSinglePriceColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_BedSinglePriceNull()
		{
			base[tablev_Room.R_BedSinglePriceColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_SizeNull()
		{
			return IsNull(tablev_Room.R_SizeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_SizeNull()
		{
			base[tablev_Room.R_SizeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_TotalGuestNull()
		{
			return IsNull(tablev_Room.R_TotalGuestColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_TotalGuestNull()
		{
			base[tablev_Room.R_TotalGuestColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_TotalPriceNull()
		{
			return IsNull(tablev_Room.R_TotalPriceColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_TotalPriceNull()
		{
			base[tablev_Room.R_TotalPriceColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsR_MemoNull()
		{
			return IsNull(tablev_Room.R_MemoColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetR_MemoNull()
		{
			base[tablev_Room.R_MemoColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_HotelNameNull()
		{
			return IsNull(tablev_Room.B_HotelNameColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_HotelNameNull()
		{
			base[tablev_Room.B_HotelNameColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_HotelWebNull()
		{
			return IsNull(tablev_Room.B_HotelWebColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_HotelWebNull()
		{
			base[tablev_Room.B_HotelWebColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_HotelIDNull()
		{
			return IsNull(tablev_Room.B_HotelIDColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_HotelIDNull()
		{
			base[tablev_Room.B_HotelIDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_AddressNull()
		{
			return IsNull(tablev_Room.B_AddressColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_AddressNull()
		{
			base[tablev_Room.B_AddressColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_BookTelNull()
		{
			return IsNull(tablev_Room.B_BookTelColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetB_BookTelNull()
		{
			base[tablev_Room.B_BookTelColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsB_FaxNull()
		{
			return IsNull(tablev_Room.B_FaxColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_FaxNull()
		{
			base[tablev_Room.B_FaxColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_PostNull()
		{
			return IsNull(tablev_Room.B_PostColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_PostNull()
		{
			base[tablev_Room.B_PostColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_StayDayNull()
		{
			return IsNull(tablev_Room.B_StayDayColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_StayDayNull()
		{
			base[tablev_Room.B_StayDayColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsB_LevelTimeNull()
		{
			return IsNull(tablev_Room.B_LevelTimeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetB_LevelTimeNull()
		{
			base[tablev_Room.B_LevelTimeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsBuild_IDNull()
		{
			return IsNull(tablev_Room.Build_IDColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetBuild_IDNull()
		{
			base[tablev_Room.Build_IDColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsBuild_CodeNull()
		{
			return IsNull(tablev_Room.Build_CodeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetBuild_CodeNull()
		{
			base[tablev_Room.Build_CodeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsBuild_NameNull()
		{
			return IsNull(tablev_Room.Build_NameColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetBuild_NameNull()
		{
			base[tablev_Room.Build_NameColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsBuild_FlagNull()
		{
			return IsNull(tablev_Room.Build_FlagColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetBuild_FlagNull()
		{
			base[tablev_Room.Build_FlagColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsBuild_MemoNull()
		{
			return IsNull(tablev_Room.Build_MemoColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetBuild_MemoNull()
		{
			base[tablev_Room.Build_MemoColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsFloor_CodeNull()
		{
			return IsNull(tablev_Room.Floor_CodeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetFloor_CodeNull()
		{
			base[tablev_Room.Floor_CodeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsFloor_NameNull()
		{
			return IsNull(tablev_Room.Floor_NameColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetFloor_NameNull()
		{
			base[tablev_Room.Floor_NameColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsFloor_FlagNull()
		{
			return IsNull(tablev_Room.Floor_FlagColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetFloor_FlagNull()
		{
			base[tablev_Room.Floor_FlagColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsFloor_MemoNull()
		{
			return IsNull(tablev_Room.Floor_MemoColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetFloor_MemoNull()
		{
			base[tablev_Room.Floor_MemoColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTP_NameNull()
		{
			return IsNull(tablev_Room.TP_NameColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTP_NameNull()
		{
			base[tablev_Room.TP_NameColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTP_PriceNull()
		{
			return IsNull(tablev_Room.TP_PriceColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTP_PriceNull()
		{
			base[tablev_Room.TP_PriceColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTP_BedCountNull()
		{
			return IsNull(tablev_Room.TP_BedCountColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTP_BedCountNull()
		{
			base[tablev_Room.TP_BedCountColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTP_PricelessHourNull()
		{
			return IsNull(tablev_Room.TP_PricelessHourColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTP_PricelessHourNull()
		{
			base[tablev_Room.TP_PricelessHourColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTP_PriceStandHourNull()
		{
			return IsNull(tablev_Room.TP_PriceStandHourColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTP_PriceStandHourNull()
		{
			base[tablev_Room.TP_PriceStandHourColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTP_RSizeNull()
		{
			return IsNull(tablev_Room.TP_RSizeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTP_RSizeNull()
		{
			base[tablev_Room.TP_RSizeColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTP_FlagNull()
		{
			return IsNull(tablev_Room.TP_FlagColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTP_FlagNull()
		{
			base[tablev_Room.TP_FlagColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTP_MemoNull()
		{
			return IsNull(tablev_Room.TP_MemoColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTP_MemoNull()
		{
			base[tablev_Room.TP_MemoColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsRS_NameenNull()
		{
			return IsNull(tablev_Room.RS_NameenColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetRS_NameenNull()
		{
			base[tablev_Room.RS_NameenColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsRS_NameNull()
		{
			return IsNull(tablev_Room.RS_NameColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetRS_NameNull()
		{
			base[tablev_Room.RS_NameColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsRS_CanusedNull()
		{
			return IsNull(tablev_Room.RS_CanusedColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetRS_CanusedNull()
		{
			base[tablev_Room.RS_CanusedColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsRS_flagNull()
		{
			return IsNull(tablev_Room.RS_flagColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetRS_flagNull()
		{
			base[tablev_Room.RS_flagColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTP_depositNull()
		{
			return IsNull(tablev_Room.TP_depositColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTP_depositNull()
		{
			base[tablev_Room.TP_depositColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_cardcountNull()
		{
			return IsNull(tablev_Room.TR_cardcountColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_cardcountNull()
		{
			base[tablev_Room.TR_cardcountColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_SOhourNull()
		{
			return IsNull(tablev_Room.TR_SOhourColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_SOhourNull()
		{
			base[tablev_Room.TR_SOhourColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_SOrpNull()
		{
			return IsNull(tablev_Room.TR_SOrpColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_SOrpNull()
		{
			base[tablev_Room.TR_SOrpColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_SOdpNull()
		{
			return IsNull(tablev_Room.TR_SOdpColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_SOdpNull()
		{
			base[tablev_Room.TR_SOdpColumn] = Convert.DBNull;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public bool IsTR_SOLTimeNull()
		{
			return IsNull(tablev_Room.TR_SOLTimeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetTR_SOLTimeNull()
		{
			base[tablev_Room.TR_SOLTimeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsTR_actual_S_HourNull()
		{
			return IsNull(tablev_Room.TR_actual_S_HourColumn);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public void SetTR_actual_S_HourNull()
		{
			base[tablev_Room.TR_actual_S_HourColumn] = Convert.DBNull;
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public class v_RoomRowChangeEvent : EventArgs
	{
		private v_RoomRow eventRow;

		private DataRowAction eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public v_RoomRow Row => eventRow;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public DataRowAction Action => eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public v_RoomRowChangeEvent(v_RoomRow row, DataRowAction action)
		{
			eventRow = row;
			eventAction = action;
		}
	}

	private v_RoomDataTable tablev_Room;

	private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[DebuggerNonUserCode]
	public v_RoomDataTable v_Room => tablev_Room;

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[DebuggerNonUserCode]
	[Browsable(true)]
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

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public new DataTableCollection Tables => base.Tables;

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DebuggerNonUserCode]
	public new DataRelationCollection Relations => base.Relations;

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	public RadioLockDataSet()
	{
		BeginInit();
		InitClass();
		CollectionChangeEventHandler value = SchemaChanged;
		base.Tables.CollectionChanged += value;
		base.Relations.CollectionChanged += value;
		EndInit();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected RadioLockDataSet(SerializationInfo info, StreamingContext context)
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
			if (dataSet.Tables["v_Room"] != null)
			{
				base.Tables.Add(new v_RoomDataTable(dataSet.Tables["v_Room"]));
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

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
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
		RadioLockDataSet radioLockDataSet = (RadioLockDataSet)base.Clone();
		radioLockDataSet.InitVars();
		radioLockDataSet.SchemaSerializationMode = SchemaSerializationMode;
		return radioLockDataSet;
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected override bool ShouldSerializeTables()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override bool ShouldSerializeRelations()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override void ReadXmlSerializable(XmlReader reader)
	{
		if (DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
		{
			Reset();
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(reader);
			if (dataSet.Tables["v_Room"] != null)
			{
				base.Tables.Add(new v_RoomDataTable(dataSet.Tables["v_Room"]));
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

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DebuggerNonUserCode]
	protected override XmlSchema GetSchemaSerializable()
	{
		MemoryStream memoryStream = new MemoryStream();
		WriteXmlSchema(new XmlTextWriter(memoryStream, null));
		memoryStream.Position = 0L;
		return XmlSchema.Read(new XmlTextReader(memoryStream), null);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	internal void InitVars()
	{
		InitVars(initTable: true);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	internal void InitVars(bool initTable)
	{
		tablev_Room = (v_RoomDataTable)base.Tables["v_Room"];
		if (initTable && tablev_Room != null)
		{
			tablev_Room.InitVars();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private void InitClass()
	{
		base.DataSetName = "RadioLockDataSet";
		base.Prefix = "";
		base.Namespace = "http://tempuri.org/RadioLockDataSet.xsd";
		base.EnforceConstraints = true;
		SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		tablev_Room = new v_RoomDataTable();
		base.Tables.Add(tablev_Room);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private bool ShouldSerializev_Room()
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

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
	{
		RadioLockDataSet radioLockDataSet = new RadioLockDataSet();
		XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
		XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
		XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
		xmlSchemaAny.Namespace = radioLockDataSet.Namespace;
		xmlSchemaSequence.Items.Add(xmlSchemaAny);
		xmlSchemaComplexType.Particle = xmlSchemaSequence;
		XmlSchema schemaSerializable = radioLockDataSet.GetSchemaSerializable();
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
