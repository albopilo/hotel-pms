using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CommonLib;
using DataBase;
using Dev_C_Sharp;
using LockSoftware.Controls;
using LockSoftware.Frm;
using Microsoft.Win32;

namespace LockSoftware;

internal static class Program
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct IDCardData
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string Name;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		public string Sex;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
		public string Nation;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
		public string Born;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
		public string Address;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
		public string IDCardNo;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string GrantDept;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
		public string UserLifeBegin;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
		public string UserLifeEnd;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
		public string reserved;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
		public string PhotoFileName;
	}

	private delegate IntPtr HookProc(int code, IntPtr wparam, IntPtr lparam);

	public static string m_M_OK = "确定";

	public static string m_M_Cancel = "取消";

	public static string m_M_Abort = "终止";

	public static string m_M_Retry = "重试";

	public static string m_M_Ignore = "忽略";

	public static string m_M_Yes = "是(Y)";

	public static string m_M_No = "否(N)";

	private static IntPtr _nextHookPtr;

	private static HookProc myProc = MyHookProc;

	public static frmLogin fl = null;

	public static frmMain fm = null;

	public static frmPop fpop = null;

	public static bool m_Exit = false;

	public static string m_SqlSN = "";

	public static string m_SqlDN = "";

	public static string m_SqlUN = "";

	public static string m_SqlUPWD = "";

	public static string m_SqlCTO = "60";

	public static int m_opid = 0;

	public static string m_OperName = "";

	public static string m_OperPwd = "";

	public static string m_OperID = "";

	public static int m_DevCOM = 0;

	public static int m_CardSector = 13;

	public static int m_DevBaud = 115200;

	public static string m_regID = "";

	public static string m_regKey = "";

	public static int m_Lan = 0;

	public static string m_defDBPath = "";

	public static string m_AppPath = "";

	public static string m_baseCurrCode = "";

	public static double m_baseCurrRate = 1.0;

	public static int m_baseCurrID = 1;

	public static int m_basMaxGuest = 10;

	public static int m_defDiscount = -1;

	public static Hashtable m_hPubTab = null;

	public static string m_defDay = "1";

	public static string m_bgVal = "R0lGODlhCgAKAJEAAAAAAP///////wAAACH5BAEAAAIALAAAAAAKAAoAAAIIjI+py+0PYysAOw==";

	public static bool m_chkGInfo = true;

	public static string m_defComeTime = "8:00";

	public static string m_defLeaveTime = "12:30";

	public static string m_defHalfDay = "14:30";

	public static string m_defFullDay = "18:00";

	public static string m_defDateFmt = "yyyy-MM-dd";

	public static string m_defDateTimeFmt = "yyyy-MM-dd HH:mm";

	public static string m_currDateFmt = "";

	public static string m_currDateTimeFmt = "";

	public static int m_defClearTime = 10;

	public static int m_defLS = 1;

	public static int m_defHR = 4;

	public static string m_tmpval = "";

	public static DataTable m_lansDt = null;

	public static Mutex mutex;

	public static string m_defDebug = "";

	public static bool showOldMSG = true;

	public static string firstRun = "0";

	public static string sqlstrup = "";

	public static string TaxType = "Tax({0}):";

	public static decimal TaxPercent = 1m;

	public static string pathXml = "DataBase.xml";

	public static XmlDocument xd = new XmlDocument();

	public static string AssemblyTitle
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), inherit: false);
			if (customAttributes.Length > 0)
			{
				AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
				if (assemblyTitleAttribute.Title != "")
				{
					return assemblyTitleAttribute.Title;
				}
			}
			return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
		}
	}

	public static string AssemblyVersionMM
	{
		get
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			return version.Major + "." + version.Minor;
		}
	}

	public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

	public static string AssemblyDescription
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
		}
	}

	public static string AssemblyProduct
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyProductAttribute)customAttributes[0]).Product;
		}
	}

	public static string AssemblyCopyright
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
		}
	}

	public static string AssemblyCompany
	{
		get
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
		}
	}

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_GetCOMBaud(int iComID, ref uint puiBaud);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_SetCOMBaud(int iComID, uint uiCurrBaud, uint uiSetBaud);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_OpenPort(int iPortID);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_ClosePort(int iPortID);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_GetSAMStatus(int iPortID, int iIfOpen);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_ResetSAM(int iPortID, int iIfOpen);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_GetSAMID(int iPortID, ref byte pucSAMID, int iIfOpen);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_GetSAMIDToStr(int iPortID, ref byte pcSAMID, int iIfOpen);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_StartFindIDCard(int iPortID, ref byte pucManaInfo, int iIfOpen);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_SelectIDCard(int iPortID, ref byte pucManaMsg, int iIfOpen);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_ReadMsg(int iPortID, int iIfOpen, ref IDCardData pIDCardData);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern int Syn_SendSound(int iCmdNo);

	[DllImport("Syn_IDCardRead.dll", CharSet = CharSet.Ansi)]
	public static extern void Syn_DelPhotoFile();

	[DllImport("sdtapi.dll")]
	public static extern int SDT_OpenPort(int iPortID);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ClosePort(int iPortID);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_PowerManagerBegin(int iPortID, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_AddSAMUser(int iPortID, string pcUserName, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_SAMLogin(int iPortID, string pcUserName, string pcPasswd, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_SAMLogout(int iPortID, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_UserManagerOK(int iPortID, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ChangeOwnPwd(int iPortID, string pcOldPasswd, string pcNewPasswd, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ChangeOtherPwd(int iPortID, string pcUserName, string pcNewPasswd, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_DeleteSAMUser(int iPortID, string pcUserName, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_StartFindIDCard(int iPortID, ref int pucIIN, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_SelectIDCard(int iPortID, ref int pucSN, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ReadBaseMsg(int iPortID, string pucCHMsg, ref int puiCHMsgLen, string pucPHMsg, ref int puiPHMsgLen, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ReadBaseMsgToFile(int iPortID, string fileName1, ref int puiCHMsgLen, string fileName2, ref int puiPHMsgLen, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_WriteAppMsg(int iPortID, ref byte pucSendData, int uiSendLen, ref byte pucRecvData, ref int puiRecvLen, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_WriteAppMsgOK(int iPortID, ref byte pucData, int uiLen, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_CancelWriteAppMsg(int iPortID, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ReadNewAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ReadAllAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_UsableAppMsg(int iPortID, ref byte ucByte, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_GetUnlockMsg(int iPortID, ref byte strMsg, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_GetSAMID(int iPortID, ref byte StrSAMID, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_SetMaxRFByte(int iPortID, byte ucByte, int iIfOpen);

	[DllImport("sdtapi.dll")]
	public static extern int SDT_ResetSAM(int iPortID, int iIfOpen);

	[DllImport("WltRS.dll")]
	public static extern int GetBmp(string file_name, int intf);

	public static bool ReadICCard(ref IDCardData objEDZ)
	{
		if (m_Lan < 1 || m_Lan > 2)
		{
			return false;
		}
		bool flag = false;
		int num = 0;
		int num2 = 0;
		int pucIIN = 0;
		int pucSN = 0;
		int puiCHMsgLen = 0;
		int puiPHMsgLen = 0;
		int iIfOpen = 0;
		int iPortID = 1;
		for (int i = 1001; i <= 1016; i++)
		{
			num = SDT_OpenPort(i);
			if (num == 144)
			{
				iPortID = i;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			for (int j = 1; j <= 2; j++)
			{
				num = SDT_OpenPort(j);
				if (num == 144)
				{
					iPortID = j;
					flag = false;
					break;
				}
			}
		}
		if (num != 144)
		{
			MessageBox.Show("端口打开失败，请检测相应的端口或者重新连接读卡器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		num2 = SDT_StartFindIDCard(iPortID, ref pucIIN, iIfOpen);
		if (num2 != 159)
		{
			num2 = SDT_StartFindIDCard(iPortID, ref pucIIN, iIfOpen);
			if (num2 != 159)
			{
				num2 = SDT_ClosePort(iPortID);
				MessageBox.Show("未放卡或者卡未放好，请重新放卡！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
		}
		num2 = SDT_SelectIDCard(iPortID, ref pucSN, iIfOpen);
		if (num2 != 144)
		{
			num2 = SDT_SelectIDCard(iPortID, ref pucSN, iIfOpen);
			if (num2 != 144)
			{
				num2 = SDT_ClosePort(iPortID);
				MessageBox.Show("读卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
		}
		FileInfo fileInfo = new FileInfo("wz.txt");
		if (fileInfo.Exists)
		{
			fileInfo.Attributes = FileAttributes.Normal;
			fileInfo.Delete();
		}
		fileInfo = new FileInfo("zp.wlt");
		if (fileInfo.Exists)
		{
			fileInfo.Attributes = FileAttributes.Normal;
			fileInfo.Delete();
		}
		num2 = SDT_ReadBaseMsgToFile(iPortID, "wz.txt", ref puiCHMsgLen, "zp.wlt", ref puiPHMsgLen, iIfOpen);
		if (num2 != 144)
		{
			num2 = SDT_ClosePort(iPortID);
			MessageBox.Show("读卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		num2 = SDT_ClosePort(iPortID);
		FileInfo fileInfo2 = new FileInfo("wz.txt");
		FileStream fileStream = fileInfo2.OpenRead();
		byte[] array = new byte[fileStream.Length];
		fileStream.Read(array, 0, (int)fileStream.Length);
		fileStream.Close();
		objEDZ.Name = Encoding.Unicode.GetString(array, 0, 30).Trim();
		objEDZ.Sex = Encoding.Unicode.GetString(array, 30, 2).Trim();
		objEDZ.Nation = Encoding.Unicode.GetString(array, 32, 4).Trim();
		string text = Encoding.Unicode.GetString(array, 36, 16).Trim();
		objEDZ.Born = text.Substring(0, 4) + "年" + text.Substring(4, 2) + "月" + text.Substring(6) + "日";
		objEDZ.Address = Encoding.Unicode.GetString(array, 52, 70).Trim();
		objEDZ.IDCardNo = Encoding.Unicode.GetString(array, 122, 36).Trim();
		objEDZ.GrantDept = Encoding.Unicode.GetString(array, 158, 30).Trim();
		string text2 = Encoding.Unicode.GetString(array, 188, array.GetLength(0) - 188).Trim();
		objEDZ.UserLifeBegin = text2.Substring(0, 4) + "年" + text2.Substring(4, 2) + "月" + text2.Substring(6, 2) + "日";
		text2 = text2.Substring(8);
		if (text2.Trim() != "长期")
		{
			objEDZ.UserLifeEnd = text2.Substring(0, 4) + "年" + text2.Substring(4, 2) + "月" + text2.Substring(6, 2) + "日";
		}
		else
		{
			objEDZ.UserLifeEnd = "长期";
		}
		return true;
	}

	[DllImport("kernel32.dll")]
	private static extern int GetCurrentThreadId();

	[DllImport("user32.dll")]
	private static extern int GetDlgItem(IntPtr hDlg, int nIDDlgItem);

	[DllImport("user32", EntryPoint = "SetDlgItemText")]
	private static extern int SetDlgItemTextA(IntPtr hDlg, int nIDDlgItem, string lpString);

	[DllImport("user32.dll")]
	private static extern void UnhookWindowsHookEx(IntPtr handle);

	[DllImport("user32.dll")]
	private static extern IntPtr SetWindowsHookEx(int idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HookProc lpfn, IntPtr hInstance, int threadID);

	[DllImport("user32.dll")]
	private static extern IntPtr CallNextHookEx(IntPtr handle, int code, IntPtr wparam, IntPtr lparam);

	private static IntPtr MyHookProc(int code, IntPtr wparam, IntPtr lparam)
	{
		if (code == 5)
		{
			if (m_Lan != 0)
			{
				_ = m_Lan;
			}
		}
		else
		{
			CallNextHookEx(_nextHookPtr, code, wparam, lparam);
		}
		return IntPtr.Zero;
	}

	public static void SetHook()
	{
		if (!(_nextHookPtr != IntPtr.Zero))
		{
			_nextHookPtr = SetWindowsHookEx(5, myProc, IntPtr.Zero, GetCurrentThreadId());
		}
	}

	public static void UnHook()
	{
		if (_nextHookPtr != IntPtr.Zero)
		{
			UnhookWindowsHookEx(_nextHookPtr);
			_nextHookPtr = IntPtr.Zero;
		}
	}

	[STAThread]
	private static void Main()
	{
		bool createdNew = false;
		mutex = new Mutex(initiallyOwned: true, "ZKHotelLock", out createdNew);
		if (!createdNew)
		{
			MessageBox.Show("The Program is running !", "Warning", MessageBoxButtons.OK);
			return;
		}
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		m_AppPath = Application.StartupPath;
		try
		{
			pathXml = AppDomain.CurrentDomain.BaseDirectory + pathXml;
			xd.Load(pathXml);
			StringBuilder stringBuilder = new StringBuilder(256);
			StringBuilder stringBuilder2 = new StringBuilder(256);
			global::Dev_C_Sharp.Dev_C_Sharp.Instance.GetRegInfo(stringBuilder, stringBuilder2);
			m_regID = stringBuilder.ToString();
			m_regKey = stringBuilder2.ToString();
		}
		catch
		{
		}
		sqlstrup += "SET DATEFORMAT YMD\n";
		sqlstrup += "declare @maxid as bigint \nselect @maxid=max(rs_id) from d_roomstatus \nif (@maxid=9) \n";
		sqlstrup += "insert into d_roomstatus values (N'预住可住',N'预住可住',N'预住可住',N'预住可住',0,0,getdate(),1,N'超级管理员',NULL,NULL,NULL)\n";
		sqlstrup += "select @maxid=max(rs_id) from d_roomstatus \nif (@maxid=10) \n";
		sqlstrup += "insert into d_roomstatus values (N'预住过期',N'预住过期',N'预住过期',N'预住过期',0,0,getdate(),1,N'超级管理员',NULL,NULL,NULL)\n";
		sqlstrup += "update v_cardguest set g_deposit=(case isnull(team_id,-1) when -1 then 0 else tr_deposit end) where isnull(a_id,-1)=-1 \n";
		sqlstrup += "update t_guest set r_price=g_singlepaid,g_memo=convert(nvarchar(max),tr_id) where isnull(a_id,-1)=-1 \n";
		sqlstrup += "update t_guest set g_singlepaid=r_price*g_discount where isnull(a_id,-1)=-1 \n";
		sqlstrup += "update t_guest set g_SOTotalDay=convert(bigint,g_stayHour/24),g_stayHour=convert(bigint,g_stayHour)%24 where isnull(a_id,-1)=-1 \n";
		sqlstrup += "update t_guest set a_id=Convert(bigint,(g_actual_S_Hour/12.0)),g_actual_S_Hour=Convert(bigint,g_actual_S_Hour)%12 where isnull(a_id,-1)=-1\n";
		sqlstrup += "update t_rooms set tr_deposit=(case isnull(team_id,-1) when -1 then 0 else tr_deposit end),Tr_sodp=0,Tr_sohour=convert(bigint,Tr_stayhour)%24,Tr_stayhour=convert(bigint,Tr_stayhour/24) where isnull(a_id,-1)=-1\n";
		sqlstrup += "update t_rooms set a_id=Convert(bigint,(Tr_actual_s_hour/12.0)),Tr_actual_s_hour=convert(bigint,Tr_actual_s_hour)%12 where isnull(a_id,-1)=-1\n";
		sqlstrup += "update v_teamdetails set team_totalpaid=team_deposit/curr_rate,team_mainguestid =0 where isnull(team_mainguestid,-1)=-1";
		sqlstrup += "IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[D_Rooms]') AND name = N'PK_D_Rooms_1')\n";
		sqlstrup += "ALTER TABLE [dbo].[D_Rooms] drop  CONSTRAINT [PK_D_Rooms_1]\n";
		sqlstrup += "IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[D_Currency]') AND name = N'PK_D_Currency_1')\n";
		sqlstrup += "ALTER TABLE [dbo].[D_Currency] DROP CONSTRAINT [PK_D_Currency_1]\n";
		sqlstrup += "alter table d_currency alter column curr_code nvarchar(20) not null\n";
		sqlstrup += "alter table d_currency alter column curr_name nvarchar(20)\n";
		sqlstrup += "alter table t_roomgroup alter column rgt_name nvarchar(128)\n";
		sqlstrup += "alter table t_roomgroup alter column r_name nvarchar(50)\n";
		sqlstrup += "alter table t_roomgroup alter column build_name nvarchar(50)\n";
		sqlstrup += "alter table t_roomgroup alter column floor_name nvarchar(50)\n";
		sqlstrup += "alter table t_roomgroup alter column tp_name nvarchar(50)\n";
		sqlstrup += "alter table d_hotelbasic alter column b_hotelname nvarchar(128)\n";
		sqlstrup += "alter table d_hotelbasic alter column b_hotelweb nvarchar(256)\n";
		sqlstrup += "alter table d_hotelbasic alter column b_hotelid nvarchar(128)\n";
		sqlstrup += "alter table d_hotelbasic alter column b_booktel nvarchar(50)\n";
		sqlstrup += "alter table d_hotelbasic alter column B_fax nvarchar(50)\n";
		sqlstrup += "alter table d_hotelbasic alter column B_post nvarchar(50)\n";
		sqlstrup += "alter table userinfo alter column u_rem nvarchar(32)\n";
		sqlstrup += "alter table d_build alter column build_name nvarchar(50)\n";
		sqlstrup += "alter table d_floor alter column floor_name nvarchar(50)\n";
		sqlstrup += "alter table t_cardmanage alter column bl_name nvarchar(50)\n";
		sqlstrup += "alter table t_cardmanage alter column f_name nvarchar(50)\n";
		sqlstrup += "alter table t_cardmanage alter column r_name nvarchar(50)\n";
		sqlstrup += "alter table t_roomgroupcard alter column rgt_n0 nvarchar(128)\n";
		sqlstrup += "alter table t_roomgroupcard alter column rgt_n1 nvarchar(128)\n";
		sqlstrup += "alter table t_roomgroupcard alter column rgt_n2 nvarchar(128)\n";
		sqlstrup += "alter table t_roomgroupcard alter column rgt_n3 nvarchar(128)\n";
		sqlstrup += "alter table t_roomgroupcard alter column rgt_n4 nvarchar(128)\n";
		sqlstrup += "alter table t_roomgroupcard alter column rgt_n5 nvarchar(128)\n";
		sqlstrup += "alter table d_rooms alter column r_name nvarchar(50) not null\n";
		sqlstrup += "alter table t_rooms alter column r_name nvarchar(50)\n";
		sqlstrup += "alter table t_rooms alter column TR_Bascurname nvarchar(20)\n";
		sqlstrup += "alter table t_rooms alter column curr_code nvarchar(20)\n";
		sqlstrup += "alter table t_guest alter column r_name nvarchar(50)\n";
		sqlstrup += "alter table t_guest alter column g_tel nvarchar(50)\n";
		sqlstrup += "alter table D_TraBur alter column tb_tel nvarchar(50)\n";
		sqlstrup += "alter table D_TraBur alter column tb_mail nvarchar(50)\n";
		sqlstrup += "alter table D_TraBur alter column tb_fax nvarchar(128)\n";
		sqlstrup += "alter table D_TraBur alter column TB_othConn nvarchar(128)\n";
		sqlstrup += " ALTER TABLE [dbo].[D_Rooms] ADD  CONSTRAINT [PK_D_Rooms_1] PRIMARY KEY CLUSTERED \n";
		sqlstrup += "(\n";
		sqlstrup += "[R_Name] ASC\n";
		sqlstrup += ")WITH (PAD_INDEX  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]\n";
		sqlstrup += "ALTER TABLE [dbo].[D_Currency] ADD  CONSTRAINT [PK_D_Currency_1] PRIMARY KEY CLUSTERED \n";
		sqlstrup += "(\n";
		sqlstrup += "[curr_code] ASC\n";
		sqlstrup += ")WITH (PAD_INDEX  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]\n";
		sqlstrup += "IF not EXISTS(SELECT * FROM dbo.SysObjects WHERE ID = object_id(N'[T_LockRecords]') AND OBJECTPROPERTY(ID, 'IsTable') = 1)\n";
		sqlstrup += "begin CREATE TABLE [T_LockRecords](\n";
		sqlstrup += "[ID] [bigint] IDENTITY(1,1) NOT NULL,\n";
		sqlstrup += "[B_Code] [varchar](8) NOT NULL,\n";
		sqlstrup += "[F_Code] [varchar](8) NOT NULL,\n";
		sqlstrup += "[R_Code] [varchar](8) NOT NULL,\n";
		sqlstrup += "[R_SubCode] [varchar](8) NOT NULL,\n";
		sqlstrup += "[C_Num] [bigint] NOT NULL,\n";
		sqlstrup += "[OpenTime] [datetime] NOT NULL,\n";
		sqlstrup += "CONSTRAINT [PK_T_LockRecords] PRIMARY KEY CLUSTERED \n";
		sqlstrup += "(\n";
		sqlstrup += "[OpenTime] ASC,\n";
		sqlstrup += "[B_Code] ASC,\n";
		sqlstrup += "[F_Code] ASC,\n";
		sqlstrup += "[R_Code] ASC,\n";
		sqlstrup += "[R_SubCode] ASC,\n";
		sqlstrup += "[C_Num] ASC\n";
		sqlstrup += ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]\n";
		sqlstrup += ") ON [PRIMARY]\nend\n";
		sqlstrup += "if not exists(select * from syscolumns where id=object_id('T_LockRecords')and name=N'Code')\n";
		sqlstrup += "begin alter table [T_LockRecords] add [Code] [int] not null constraint [DF_T_LockRecords_code] default((1))end\n";
		sqlstrup += "if not exists(select * from syscolumns where id=object_id('T_LockRecords')and name=N'AddTime')\n";
		sqlstrup += "begin alter table [T_LockRecords] add [AddTime] [datetime] null constraint [DF_T_LockRecords_addtime] default(getdate())end\n";
		sqlstrup += "IF EXISTS(SELECT * FROM dbo.SysObjects WHERE ID = object_id(N'[T_LockRecords]') AND OBJECTPROPERTY(ID, 'IsTable') = 1)\n";
		sqlstrup += "begin\n";
		sqlstrup += "IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[T_LockRecords]') AND name=(N'PK_T_LockRecords'))\n";
		sqlstrup += "begin\n";
		sqlstrup += "alter table [T_LockRecords] drop constraint [PK_T_LockRecords]\n";
		sqlstrup += "end\n";
		sqlstrup += "alter table [T_LockRecords] add CONSTRAINT [PK_T_LockRecords] PRIMARY KEY CLUSTERED \n";
		sqlstrup += "(\n";
		sqlstrup += "[OpenTime] ASC,\n";
		sqlstrup += "[B_Code] ASC,\n";
		sqlstrup += "[F_Code] ASC,\n";
		sqlstrup += "[R_Code] ASC,\n";
		sqlstrup += "[R_SubCode] ASC,\n";
		sqlstrup += "[C_Num] ASC,\n";
		sqlstrup += "[Code] asc\n";
		sqlstrup += ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]\n";
		sqlstrup += "end\n";
		sqlstrup += "alter table d_rooms alter column R_Size nvarchar(50) not null\n";
		int num = 0;
		while (!m_Exit)
		{
			if (fl == null || fl.Disposing || fl.IsDisposed)
			{
				fl = new frmLogin();
			}
			try
			{
				IsAdministrator();
				fl.ShowDialog();
				num = 0;
			}
			catch
			{
				num++;
				if (num == 10)
				{
					m_Exit = true;
				}
				if (fl != null)
				{
					fl.Close();
					fl.Dispose();
				}
				fl = null;
			}
		}
	}

	private static string GetInnerText(string itemKey, XmlNode node, XmlDocument xd)
	{
		string result = string.Empty;
		node = xd.SelectSingleNode("/SystemConfig/" + itemKey);
		if (node != null)
		{
			result = node.InnerText;
		}
		return result;
	}

	public static string GetConfig()
	{
		XmlNode node = null;
		m_SqlSN = GetInnerText("DataSource", node, xd).Trim();
		m_SqlUN = GetInnerText("UserID", node, xd).Trim();
		m_SqlDN = GetInnerText("InitialCatalog", node, xd).Trim();
		m_SqlUPWD = GetInnerText("Password", node, xd).Trim();
		m_SqlCTO = GetInnerText("ConnectTimeout", node, xd).Trim();
		m_defDBPath = GetInnerText("DBPath", node, xd).Trim();
		m_defDebug = GetInnerText("Debug", node, xd).Trim();
		string text = GetInnerText("DevCom", node, xd).Trim();
		text = ((text.Length == 0) ? "0" : text);
		m_DevCOM = Convert.ToInt32(text);
		text = GetInnerText("CardSec", node, xd).Trim();
		text = ((text.Length == 0) ? "5" : text);
		m_CardSector = Convert.ToInt32(text);
		text = GetInnerText("DevBaud", node, xd).Trim();
		text = ((text.Length == 0) ? "11500" : text);
		m_DevBaud = Convert.ToInt32(text);
		text = GetInnerText("SysLan", node, xd).Trim();
		text = ((text.Length == 0) ? "0" : text);
		m_Lan = Convert.ToInt32(text);
		text = GetInnerText("Discount", node, xd).Trim();
		text = ((text.Length == 0) ? "0" : text);
		m_defDiscount = Convert.ToInt32(text);
		text = GetInnerText("FirstRun", node, xd).Trim();
		text = ((text.Length == 0) ? "0" : text);
		firstRun = text;
		text = GetInnerText("ShowOldMSG", node, xd).Trim();
		text = ((text.Length == 0) ? "1" : text);
		showOldMSG = !(text == "0");
		text = GetInnerText("TaxType", node, xd).Trim();
		text = ((text.Length == 0) ? "Tax({0}):" : text);
		TaxType = text;
		text = GetInnerText("TaxPercent", node, xd).Trim();
		text = ((text.Length == 0) ? "0" : text);
		TaxPercent = decimal.Parse(text);
		string result = GetInnerText("RESTOREFILE", node, xd).Trim();
		node = null;
		return result;
	}

	public static void SaveConfig()
	{
		XmlNode node = null;
		SetItemInnerText(node, ref xd, "DataSource", m_SqlSN);
		SetItemInnerText(node, ref xd, "UserID", m_SqlUN);
		SetItemInnerText(node, ref xd, "Password", m_SqlUPWD);
		SetItemInnerText(node, ref xd, "InitialCatalog", m_SqlDN);
		SetItemInnerText(node, ref xd, "ConnectTimeout", m_SqlCTO);
		SetItemInnerText(node, ref xd, "DevCom", m_DevCOM.ToString());
		SetItemInnerText(node, ref xd, "CardSec", m_CardSector.ToString());
		SetItemInnerText(node, ref xd, "DevBaud", m_DevBaud.ToString());
		SetItemInnerText(node, ref xd, "SysLan", m_Lan.ToString());
		SetItemInnerText(node, ref xd, "DBPath", m_defDBPath);
		SetItemInnerText(node, ref xd, "Discount", m_defDiscount.ToString());
		SetItemInnerText(node, ref xd, "Debug", m_defDebug);
		SetItemInnerText(node, ref xd, "PersistSecurityInfo", "False");
		SetItemInnerText(node, ref xd, "DBAUTO", "D;23:00");
		SetItemInnerText(node, ref xd, "Radio", "");
		SetItemInnerText(node, ref xd, "RESTOREFILE", "");
		SetItemInnerText(node, ref xd, "DBAUTOPATH", "");
		SetItemInnerText(node, ref xd, "FirstRun", firstRun);
		SetItemInnerText(node, ref xd, "ShowOldMSG", showOldMSG ? "1" : "0");
		xd.Save(pathXml);
	}

	public static bool SetSingleItem(string itemKey, string val)
	{
		XmlNode node = null;
		SetItemInnerText(node, ref xd, itemKey, val);
		xd.Save(pathXml);
		return true;
	}

	private static void SetItemInnerText(XmlNode node, ref XmlDocument xd, string itemKey, string val)
	{
		node = xd.SelectSingleNode("/SystemConfig/" + itemKey);
		if (node == null)
		{
			node = xd.SelectSingleNode("/SystemConfig");
			node.AppendChild(xd.CreateNode(XmlNodeType.Element, itemKey, ""));
		}
		node = xd.SelectSingleNode("/SystemConfig/" + itemKey);
		node.InnerText = val;
	}

	private static void SetCtrlItem(ToolStripItemCollection tsic, Hashtable htab)
	{
		for (int i = 0; i < tsic.Count; i++)
		{
			if (ClassFont.Instance.enabled)
			{
				tsic[i].Font = ClassFont.Instance.GetFont(4u);
			}
			if (htab.Contains(tsic[i].Name))
			{
				tsic[i].Text = (string)htab[tsic[i].Name];
			}
			if (tsic[i].GetType() == typeof(ToolStripMenuItem))
			{
				SetCtrlItem(((ToolStripMenuItem)tsic[i]).DropDownItems, htab);
			}
		}
	}

	private static void SetCtrl(Control subctrl, Hashtable htab, Type tmpName)
	{
		if (htab.Contains(subctrl.Name))
		{
			if (tmpName == typeof(GlassBtn))
			{
				subctrl.Text = (string)htab[subctrl.Name];
				if (ClassFont.Instance.enabled)
				{
					subctrl.Font = ClassFont.Instance.GetFont(1u);
				}
			}
			else if (tmpName == typeof(NGlassBtn))
			{
				((NGlassBtn)subctrl).ButtonText = (string)htab[subctrl.Name];
				if (ClassFont.Instance.enabled)
				{
					((NGlassBtn)subctrl).Font = ClassFont.Instance.GetFont(2u);
				}
			}
			else if (tmpName == typeof(ToolsBtn))
			{
				((ToolsBtn)subctrl).TextNew = (string)htab[subctrl.Name];
				if (ClassFont.Instance.enabled)
				{
					((ToolsBtn)subctrl).Font = ClassFont.Instance.GetFont(3u);
				}
			}
			else
			{
				subctrl.Text = (string)htab[subctrl.Name];
				if (ClassFont.Instance.enabled)
				{
					subctrl.Font = ClassFont.Instance.GetFont(0u);
				}
			}
		}
		else if (tmpName == typeof(DataGridView))
		{
			if (ClassFont.Instance.enabled)
			{
				((DataGridView)subctrl).Font = ClassFont.Instance.GetFont(0u);
			}
		}
		else if (tmpName == typeof(GlassBtn))
		{
			if (ClassFont.Instance.enabled)
			{
				subctrl.Font = ClassFont.Instance.GetFont(1u);
			}
		}
		else if (tmpName == typeof(StatusStrip))
		{
			SetCtrlItem(((StatusStrip)subctrl).Items, htab);
		}
		else if (tmpName == typeof(MenuStrip))
		{
			SetCtrlItem(((MenuStrip)subctrl).Items, htab);
		}
		else if (ClassFont.Instance.enabled)
		{
			subctrl.Font = ClassFont.Instance.GetFont(0u);
		}
		if (tmpName == typeof(GroupBox) || tmpName == typeof(Panel) || tmpName == typeof(clsBackPanel) || tmpName == typeof(SplitContainer) || tmpName == typeof(SplitterPanel) || tmpName == typeof(TableLayoutPanel) || tmpName == typeof(StatusStrip) || tmpName == typeof(TabPage) || tmpName == typeof(TabControl) || tmpName == typeof(FlowLayoutPanel) || tmpName == typeof(ToolStripStatusLabel))
		{
			GetSubCtrl(subctrl, htab);
		}
	}

	private static void GetSubCtrl(Control ctrl, Hashtable htab)
	{
		foreach (Control control in ctrl.Controls)
		{
			Type type = control.GetType();
			SetCtrl(control, htab, type);
		}
	}

	public static void InitGUI(Form lanfm, Hashtable htab)
	{
		lanfm.AutoScaleMode = AutoScaleMode.None;
		if (htab.Contains("Text"))
		{
			lanfm.Text = (string)htab["Text"];
		}
		foreach (Control control in lanfm.Controls)
		{
			Type type = control.GetType();
			SetCtrl(control, htab, type);
		}
	}

	public static Hashtable GetControlName(Form lanfrm, string lanfmobjname)
	{
		Hashtable hashtable = new Hashtable();
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(m_lansDt.Rows[m_Lan]["fpath"].ToString());
			if (xmlDocument == null)
			{
				return null;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/Radio/C" + lanfmobjname.ToUpperInvariant());
			if (xmlNode == null)
			{
				return null;
			}
			XmlNodeList childNodes = xmlNode.ChildNodes;
			if (childNodes == null)
			{
				MsgBox("No form's information in database, please close it and try it again !", "System Initialization", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			for (int i = 0; i < childNodes.Count; i++)
			{
				hashtable.Add(childNodes[i].Name.ToString().Trim(), childNodes[i].Attributes["value"].Value.ToString().Trim());
			}
			if (lanfrm != null)
			{
				InitGUI(lanfrm, hashtable);
			}
		}
		catch (Exception ex)
		{
			if (m_Lan == 1)
			{
				MsgBox("初始化窗体错误，请关闭后重试！错误信息：\r\n" + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MsgBox("Error in initializing this form, please close it and try it again ! Error Info:\r\n" + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return hashtable;
	}

	public static bool isValNull(string lab, string val, bool chk)
	{
		try
		{
			if (!chk)
			{
				return false;
			}
			if (!(val.Trim() == ""))
			{
				return false;
			}
			val = string.Format((string)m_hPubTab["InfoChkVal"], lab);
			MsgBox(val, (string)m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MsgBox((string)m_hPubTab["ErrOperWithMess"] + ex.Message, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return true;
	}

	public static void MsgCusErrMess(string mess, string opertxt)
	{
		SetHook();
		string text = string.Format((string)m_hPubTab["MsgCusErrMess"] + mess, opertxt);
		MessageBox.Show(text, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		UnHook();
	}

	public static void MsgCustom(string mess, MessageBoxIcon micon)
	{
		string caption = "Application";
		if (m_hPubTab != null)
		{
			caption = micon switch
			{
				MessageBoxIcon.Hand => (string)m_hPubTab["ErrTitle"], 
				MessageBoxIcon.Exclamation => (string)m_hPubTab["WarnTitle"], 
				_ => (string)m_hPubTab["InfoTitle"], 
			};
		}
		SetHook();
		MessageBox.Show(mess, caption, MessageBoxButtons.OK, micon);
		UnHook();
	}

	public static DialogResult MsgBox(string msg, string title, MessageBoxButtons mbb, MessageBoxIcon mbi, MessageBoxDefaultButton mbdb)
	{
		SetHook();
		DialogResult result = MessageBox.Show(msg, title, mbb, mbi, mbdb);
		UnHook();
		return result;
	}

	public static string GetFormatStringShow(string key)
	{
		XmlNodeList elements = new ClassXml(m_lansDt.Rows[m_Lan]["fpath"].ToString(), "Radio").GetElements("Radio/Info_Public/Info_Show");
		string result = "";
		foreach (XmlNode item in elements)
		{
			if (item.Attributes["Key"].Value == key)
			{
				result = item.Attributes["Value"].Value;
				break;
			}
		}
		return result;
	}

	public static DialogResult MsgBox(string msg, string title, MessageBoxButtons mbb, MessageBoxIcon mbi)
	{
		SetHook();
		DialogResult result = MessageBox.Show(msg, title, mbb, mbi);
		UnHook();
		return result;
	}

	public static double CountDay(DateTime dtComeTime, DateTime dtLeaveTime)
	{
		double num = (dtLeaveTime.Date - dtComeTime.Date).Days;
		if (num == 0.0)
		{
			num = 1.0;
			if (dtComeTime.TimeOfDay < TimeSpan.Parse(m_defComeTime) && dtLeaveTime.TimeOfDay > TimeSpan.Parse(m_defLeaveTime))
			{
				num++;
			}
		}
		else
		{
			if (dtComeTime.TimeOfDay < TimeSpan.Parse(m_defComeTime))
			{
				num++;
			}
			if (dtLeaveTime.TimeOfDay > TimeSpan.Parse(m_defFullDay))
			{
				num++;
			}
			else if (dtLeaveTime.TimeOfDay > TimeSpan.Parse(m_defHalfDay))
			{
				num += 0.5;
			}
		}
		return num;
	}

	public static DateTime GetleaveTime(DateTime dtComeTime, int iStay, bool bHour = false)
	{
		DateTime dateTime = dtComeTime;
		if (bHour)
		{
			return dateTime.AddHours(iStay);
		}
		string text = "";
		text = ((!(dateTime.TimeOfDay >= TimeSpan.Parse(m_defComeTime))) ? GetLocDate(dateTime.AddDays(iStay - 1)) : GetLocDate(dateTime.AddDays(iStay)));
		return DateTime.Parse(text + " " + m_defLeaveTime);
	}

	public static int DBCompExec(string sqlquery, string opertxt)
	{
		string text = "SET XACT_ABORT ON \n ";
		text += "BEGIN TRANSACTION \n ";
		text = text + sqlquery + " \n ";
		text += "COMMIT TRANSACTION \n ";
		text += "SET XACT_ABORT OFF";
		return SQLserver.Data_ExecuteSql(sqlquery);
	}

	public static DataTable DBCompGetDT(string sqlquery, string opertxt)
	{
		string text = "SET XACT_ABORT ON \n ";
		text += "BEGIN TRANSACTION \n ";
		text = text + sqlquery + " \n ";
		text += "COMMIT TRANSACTION \n ";
		text += "SET XACT_ABORT OFF";
		return SQLserver.Data_GetDataTable(sqlquery);
	}

	public static bool ChkNumInput(object sender, KeyPressEventArgs e, bool Integer, bool chkDot)
	{
		string numberDecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
		if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (!Integer && e.KeyChar == numberDecimalSeparator[0]) || e.KeyChar == '\b')
		{
			if (chkDot && (((TextBox)sender).Text.IndexOf(numberDecimalSeparator[0]) >= 0 || ((TextBox)sender).Text.Trim() == "") && e.KeyChar == numberDecimalSeparator[0])
			{
				return true;
			}
			return false;
		}
		return true;
	}

	public static bool ChkPWDInput(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == '%' || e.KeyChar == '&')
		{
			return true;
		}
		return false;
	}

	public static int getMaxNumber(int type, bool showError)
	{
		int num = -1;
		try
		{
			DataTable dataTable = null;
			if (type == 1)
			{
				string sql = "select IsNull(Max(cnum),0) As cnum from(select IsNull(Max(cm_cardid),0) as cnum from T_CardManage union all select IsnULL(Max(Tgs_cardid),0) As cnum from T_RoomGroupCard union all select IsnULL(Max(r_cardnum),0) As cnum from T_Guest) as tmpTab";
				dataTable = SQLserver.Data_GetDataTable(sql);
			}
			else
			{
				_ = 2;
			}
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				if (showError)
				{
					MsgBox((string)m_hPubTab["gMaxNum"], (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return -1;
			}
			num = Convert.ToInt32(dataTable.Rows[0]["cnum"].ToString());
			if (dataTable != null)
			{
				dataTable.Clear();
				dataTable.Dispose();
			}
		}
		catch (Exception ex)
		{
			MsgBox((string)m_hPubTab["gMaxNum"] + "\r\n" + (string)m_hPubTab["ErrOperWithMess"] + ex.Message, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return -1;
		}
		return num;
	}

	public static int RadioWriteCard(int cardtype, int cardnum, string datetime, string carddata, int datalen, bool Buzzer)
	{
		try
		{
			int num = 0;
			num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.WriteCard(cardtype, cardnum, datetime, carddata, datalen, Buzzer);
			if (num < 0)
			{
				MsgBox((string)m_hPubTab["devCardWr"] + num, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return num;
			}
			return num;
		}
		catch (Exception ex)
		{
			MsgBox((string)m_hPubTab["ErrOperWithMess"] + ex.Message, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return -1;
		}
	}

	public static int RadioReadCard(object[] retdata, bool Buzzer, int operType)
	{
		try
		{
			int num = 0;
			string CardData = "";
			num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.ReadCard(out var CardType, ref CardData, Buzzer: false);
			StringBuilder stringBuilder = new StringBuilder(CardData);
			if (num < 0)
			{
				string text = (string)m_hPubTab["devCardRd"] + num;
				if (operType == 4)
				{
					text = text + "\r\n\r\n" + (string)m_hPubTab["infoInput"];
				}
				MsgBox(text, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return num;
			}
			if (Buzzer)
			{
				RadioDevBuzzer(1, 2);
			}
			string[] array = stringBuilder.ToString().Split(';');
			if (array.Length < 3)
			{
				MsgBox((string)m_hPubTab["devCardRdRet"], (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return num;
			}
			retdata[0] = Convert.ToInt32(CardType);
			retdata[1] = Convert.ToInt32(array[0].ToString());
			retdata[2] = GetLocDTime(Convert.ToDateTime(array[1]));
			retdata[3] = array[2].ToString();
			string[] array2 = array[2].Split(',');
			if (array2.Length < 1)
			{
				MsgBox((string)m_hPubTab["devCardRdRetData"], (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return -1;
			}
			num = 3;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != "")
				{
					retdata[3 + i] = array2[i];
					num++;
				}
			}
			string text2 = "";
			string text3 = "";
			if (operType > 0)
			{
				text2 = (string)m_hPubTab["cardinfoTT"];
				text3 = CardType.ToString("D2");
				if (CardType == 0 && array2[0].ToString() == "1")
				{
					text3 += "01";
				}
				text3 = (string)m_hPubTab["devct" + text3];
				if (text3 == "")
				{
					text3 = (string)m_hPubTab["devctUK"];
				}
				text2 += string.Format("\r\n{0}{1}", (string)m_hPubTab["cardinfoTp"], text3);
				text2 += string.Format("\r\n{0}{1}\r\n{2}{3}", (string)m_hPubTab["cardinfoCn"], array[0], (string)m_hPubTab["cardinfoVd"], GetLocDTime(Convert.ToDateTime(array[1])));
				text3 = "";
				string text4 = "";
				DataTable dataTable = null;
				switch (CardType)
				{
				case 0:
					if (array2[0] != "1")
					{
						text3 = "\r\n" + (string)m_hPubTab["devctData"] + " " + (string)m_hPubTab["devct00d" + array2[1]];
						if (array2[1].ToString() == "1")
						{
							text3 = ((!(array2[2] == "0")) ? (text3 + string.Format("\r\n{0}{1}", (string)m_hPubTab["devct00d3"], array2[2])) : (text3 + string.Format("\r\n{0}{1}", (string)m_hPubTab["devct00d3"], (string)m_hPubTab["devct00d2"])));
						}
					}
					break;
				case 3:
				{
					object obj = text3;
					text3 = string.Concat(obj, "\r\n", (string)m_hPubTab["cardinfo03t"], retdata[3]);
					break;
				}
				case 4:
				{
					string text11 = text3;
					text3 = text11 + "\r\n" + (string)m_hPubTab["cardinfo04c"] + array2[0] + "\r\n" + (string)m_hPubTab["cardinfo04" + array2[1]];
					break;
				}
				case 5:
				{
					text4 = "Select * From T_RoomGroupCard Where Tgs_cardid = " + retdata[1];
					for (int k = 2; k < array2.Length; k++)
					{
						string text12 = text4;
						text4 = text12 + " And RGT_C" + (k - 2) + "=" + array2[k];
					}
					dataTable = SQLserver.Data_GetDataTable(text4);
					if (dataTable != null && dataTable.Rows.Count > 0)
					{
						string text13 = text3;
						text3 = text13 + "\r\n" + (string)m_hPubTab["cardinfo05"] + (string)m_hPubTab["cardinfo050" + array2[1]] + (string)m_hPubTab["cardinfo05" + array2[0]];
						if (array2[1] != "1")
						{
							for (int l = 0; l < array2.Length - 2; l++)
							{
								string text14 = text3;
								text3 = text14 + "\r\n" + dataTable.Rows[0]["RGT_N" + l].ToString() + " - " + array2[2 + l];
							}
						}
						break;
					}
					text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoDNull"];
					text3 = text3 + "\r\n" + (string)m_hPubTab["devctData"] + "↓";
					string text15 = text3;
					text3 = text15 + "\r\n" + (string)m_hPubTab["cardinfo05"] + (string)m_hPubTab["cardinfo050" + array2[1]] + (string)m_hPubTab["cardinfo05" + array2[0]] + "\r\n";
					if (array2[1] != "1")
					{
						for (int m = 2; m < array2.Length; m++)
						{
							text3 = text3 + array2[m] + " ";
						}
					}
					break;
				}
				case 2:
				case 10:
				case 11:
				case 12:
				case 13:
					text4 = string.Concat("Select bl_id, bl_code, bl_name, f_id, f_code, f_name,Build_Name,Floor_Name, r_name, cm_user, cm_carddate, cm_carddatest,cm_carddateet,cm_Createtime,r_oplock,r_opkeep,IsNull(cm_reportloss,0) As cm_reportloss, IsNull(cm_logout,0) as cm_logout, cm_logoutdate From v_CardMgr Where cm_cardid=", retdata[1], " and ct_code=", CardType);
					dataTable = SQLserver.Data_GetDataTable(text4);
					if (dataTable != null && dataTable.Rows.Count > 0)
					{
						if (CardType != 10 && CardType != 11)
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["devct02b"] + dataTable.Rows[0]["bl_name"].ToString().Trim();
							if (CardType != 13)
							{
								text3 = text3 + "\r\n" + (string)m_hPubTab["devct02f"] + dataTable.Rows[0]["f_name"].ToString().Trim();
							}
							if (CardType == 2)
							{
								text3 = text3 + "\r\n" + (string)m_hPubTab["devct02r"] + dataTable.Rows[0]["r_name"].ToString().Trim();
							}
						}
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoUser"] + dataTable.Rows[0]["cm_user"].ToString().Trim();
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoCd"] + dataTable.Rows[0]["cm_Createtime"].ToString().Trim();
						if ((bool)dataTable.Rows[0]["cm_logout"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLogout"];
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLogoutD"] + dataTable.Rows[0]["cm_logoutdate"].ToString().Trim();
						}
						if ((bool)dataTable.Rows[0]["cm_reportloss"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLost"];
						}
					}
					else
					{
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoDNull"];
					}
					if (CardType != 2)
					{
						text3 = text3 + "\r\n" + (string)m_hPubTab["devctData"] + "↓";
						string text9 = text3;
						text3 = text9 + "\r\n" + (string)m_hPubTab["devct0Ast"] + array2[0] + ":" + array2[1];
						string text10 = text3;
						text3 = text10 + "\r\n" + (string)m_hPubTab["devct0Aet"] + array2[2] + ":" + array2[3];
						text3 = text3 + "\r\n" + (string)m_hPubTab["devct0ATp"] + (string)m_hPubTab["devct0ATp" + array2[4]];
					}
					break;
				case 6:
				{
					text4 = "Select IsNull(g_teamid,-1) As g_teamid, Build_Name, Floor_Name, r_name, TP_Name, g_name, g_cometime, g_stayHour, g_stand_L_time, IsNull(g_level,0) As g_level, g_actual_L_time, g_level_Card ";
					text4 += ", IsNull(g_loss,0) As g_loss, g_lossdate, IsNull(g_logout,0) As g_logout, g_logoutdate, createtime From v_CardGuest ";
					object obj2 = text4;
					text4 = string.Concat(obj2, " Where b_code='", array2[0], "' And f_code='", array2[1], "' And r_code='", array2[2], "' And r_subcode=", array2[3], " And r_cardnum=", retdata[1]);
					text4 += " Order by g_id desc";
					dataTable = SQLserver.Data_GetDataTable(text4);
					if (dataTable != null && dataTable.Rows.Count > 0)
					{
						if (Convert.ToInt64(dataTable.Rows[0]["g_teamid"]) != -1)
						{
							text4 = "Select TB_name, team_name,team_guide  From v_TeamInfo Where team_id = " + Convert.ToInt64(dataTable.Rows[0]["g_teamid"]);
						}
						text3 = text3 + "\r\n" + (string)m_hPubTab["devct02b"] + dataTable.Rows[0]["Build_Name"].ToString().Trim();
						text3 = text3 + "\r\n" + (string)m_hPubTab["devct02f"] + dataTable.Rows[0]["Floor_Name"].ToString().Trim();
						text3 = text3 + "\r\n" + (string)m_hPubTab["devct02r"] + dataTable.Rows[0]["r_name"].ToString().Trim();
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoRTp"] + dataTable.Rows[0]["TP_Name"].ToString().Trim();
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoCd"] + GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["createtime"]));
						if ((bool)dataTable.Rows[0]["g_logout"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLogout"];
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLogoutD"] + dataTable.Rows[0]["g_logoutdate"].ToString().Trim();
						}
						if ((bool)dataTable.Rows[0]["g_loss"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLost"];
						}
						if ((bool)dataTable.Rows[0]["g_level"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLevel"];
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLevelD"] + dataTable.Rows[0]["g_actual_L_time"].ToString().Trim();
						}
						if (((bool)dataTable.Rows[0]["g_loss"] || (bool)dataTable.Rows[0]["g_level"]) && !(bool)dataTable.Rows[0]["g_logout"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLd"];
						}
					}
					else
					{
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoDNull"];
					}
					break;
				}
				case 9:
				{
					text4 = string.Concat("Select * From v_CardGrp Where ct_code = ", CardType, " And cm_cardid=", retdata[1], " And (RGT_code=", array2[5], " or RGT_code=", array2[6], ")");
					dataTable = SQLserver.Data_GetDataTable(text4);
					if (dataTable != null && dataTable.Rows.Count > 0)
					{
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoUser"] + dataTable.Rows[0]["cm_user"].ToString().Trim();
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoCd"] + dataTable.Rows[0]["cm_Createtime"].ToString().Trim();
						if ((bool)dataTable.Rows[0]["cm_logout"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLogout"];
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLogoutD"] + dataTable.Rows[0]["cm_logoutdate"].ToString().Trim();
						}
						if ((bool)dataTable.Rows[0]["cm_reportloss"])
						{
							text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoLost"];
						}
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfo09"];
						for (int j = 0; j < array2.Length - 5; j++)
						{
							string text5 = text3;
							text3 = text5 + "\r\n" + dataTable.Rows[j]["RGT_name"].ToString() + " - " + array2[5 + j];
						}
					}
					else
					{
						text3 = text3 + "\r\n" + (string)m_hPubTab["cardinfoDNull"];
					}
					text3 = text3 + "\r\n" + (string)m_hPubTab["devctData"] + "↓";
					string text6 = text3;
					text3 = text6 + "\r\n" + (string)m_hPubTab["devct0Ast"] + array2[0] + ":" + array2[1];
					string text7 = text3;
					text3 = text7 + "\r\n" + (string)m_hPubTab["devct0Aet"] + array2[2] + ":" + array2[3];
					if (dataTable == null || dataTable.Rows.Count <= 0)
					{
						string text8 = text3;
						text3 = text8 + "\r\n" + (string)m_hPubTab["cardinfo09"] + " " + array2[5] + " | " + array2[6];
					}
					break;
				}
				case byte.MaxValue:
					text3 = (string)m_hPubTab["devct255"];
					text2 = "";
					break;
				}
				text2 += text3;
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
			}
			switch (operType)
			{
			case 1:
				MsgBox(text2, (string)m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				break;
			case 2:
				if (CardType == byte.MaxValue)
				{
					MsgBox(text2 + "\r\n\r\n" + (string)m_hPubTab["InfoCCL_EM"], (string)m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					return 0;
				}
				if (MsgBox(text2 + "\r\n\r\n" + (string)m_hPubTab["InfoCCL"], (string)m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return 0;
				}
				num = RadioClearCard(1, Buzzer: true, 1, CardType, Convert.ToInt32(array[0].ToString()));
				break;
			case 3:
			case 4:
				if (CardType != 6)
				{
					text2 = text2 + "\r\n\r\n" + (string)m_hPubTab["InfoGCN"];
					MsgBox(text2, (string)m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return -1;
				}
				break;
			}
			return num;
		}
		catch (Exception ex)
		{
			MsgBox((string)m_hPubTab["ErrOperWithMess"] + ex.Message, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return -1;
		}
	}

	public static int RadioDevBuzzer(byte mill, byte num)
	{
		try
		{
			return global::Dev_C_Sharp.Dev_C_Sharp.Instance.DevBuzzer(mill, num);
		}
		catch (Exception ex)
		{
			MsgBox((string)m_hPubTab["ErrOperWithMess"] + ex.Message, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return 0;
	}

	public static int RadioClearCard(int type, bool Buzzer, int otype, int cardtype, int cardnum)
	{
		try
		{
			int num = 0;
			string text = "Update ";
			switch (cardtype)
			{
			case 6:
			{
				string text4 = text;
				text = text4 + " T_Guest Set g_logout = 1, g_logoutdate = GetDate(), Updator_id = " + m_opid + ", Updator = N'" + m_OperName + "', UpdateTime = GetDate()";
				text += " Where r_cardnum = ";
				break;
			}
			case 5:
			{
				string text3 = text;
				text = text3 + " T_RoomGroupCard Set Tgs_logout = 1, Tgs_logoutdate = GetDate(), Tgs_updatorid = " + m_opid + ", Tgs_updator = N'" + m_OperName + "', Tgs_updatetime = GetDate()";
				text += " Where Tgs_cardid = ";
				break;
			}
			default:
			{
				string text2 = text;
				text = text2 + " T_CardManage Set cm_logout = 1, cm_logoutdate = GetDate(), cm_updatorid = " + m_opid + ", cm_updator = N'" + m_OperName + "', cm_updatetime = GetDate()";
				text += " Where cm_cardid = ";
				break;
			}
			case 255:
				break;
			}
			text += cardnum;
			num = SQLserver.Data_ExecuteSql(text);
			if (num != 1)
			{
				text = string.Format((string)m_hPubTab["devCardClearDB"], num, "\r\n");
				if (MsgBox(text, (string)m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return num;
				}
			}
			num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.ClearCard(type, Buzzer: false);
			if (num < 0 && otype == 1)
			{
				MsgBox((string)m_hPubTab["devCardClear"] + num, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return num;
			}
			if (Buzzer)
			{
				RadioDevBuzzer(1, 2);
			}
			return num;
		}
		catch (Exception ex)
		{
			MsgBox((string)m_hPubTab["ErrOperWithMess"] + ex.Message, (string)m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return -1;
		}
	}

	public static void MDIFrm_Center_Room_Refresh(Form[] MdiChildren)
	{
		foreach (Form form in MdiChildren)
		{
			if (form.Name == "frmCenter")
			{
				((frmCenter)form).btnSear_Click(null, null);
			}
		}
	}

	public static void MDIFrm_Center_BFR_Ref(Form[] MdiChildren)
	{
		foreach (Form form in MdiChildren)
		{
			if (form.Name == "frmCenter")
			{
				((frmCenter)form).btnRefresh_Click(null, null);
			}
		}
	}

	public static int ReadLansType()
	{
		try
		{
			string path = m_AppPath + "\\lans";
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			FileInfo[] files = directoryInfo.GetFiles("*.xml");
			if (files.Length <= 0)
			{
				path = "缺少语言文件系统无法运行，系统将强制退出！请向软件供应商获取正确的软件版本！";
				path += "\r\nNo language files found, the system will exit. Please get the right software from the supplier.";
				MessageBox.Show(path, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				m_Exit = true;
				return -1;
			}
			Array.Sort(files, (FileInfo x, FileInfo y) => x.Name.Trim().CompareTo(y.Name.Trim()));
			m_lansDt = new DataTable();
			m_lansDt.Columns.Add("lansName");
			m_lansDt.Columns.Add("fpath");
			string text = "";
			XmlDocument xmlDocument = null;
			XmlNode xmlNode = null;
			for (int num = 0; num < files.Length; num++)
			{
				string[] array = new string[2];
				xmlDocument = new XmlDocument();
				xmlDocument.Load(m_AppPath + "\\lans\\" + files[num].Name.Trim());
				if (xmlDocument != null)
				{
					xmlNode = xmlDocument.SelectSingleNode("/Radio");
					if (xmlNode != null && xmlNode.Attributes["LanType"] != null)
					{
						text = xmlNode.Attributes["LanType"].Value.ToString().Trim();
						array[0] = text;
						text = m_AppPath + "\\lans\\" + files[num].Name.Trim();
						array[1] = m_AppPath + "\\lans\\" + files[num].Name.Trim();
						m_lansDt.Rows.Add(array);
					}
				}
			}
			return files.Length;
		}
		catch (Exception ex)
		{
			string text2 = "";
			text2 = "初始化系统语言错误，系统将强制退出！请向软件供应商获取正确的软件版本！";
			text2 += "\r\nInitialized language error, the system will exit. Please get the right software from the supplier.";
			text2 = text2 + "\r\n\r\nError Message:\r\n" + ex.Message;
			MessageBox.Show(text2, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			m_Exit = true;
			return -1;
		}
	}

	public static int Get_IDCardII_Information(ref IDCardData CardMsg)
	{
		int num = -1;
		try
		{
			byte[] array = new byte[4];
			byte[] array2 = new byte[8];
			int num2 = 0;
			bool flag = false;
			for (num2 = 1001; num2 < 1017; num2++)
			{
				if (Syn_OpenPort(num2) == 0 && Syn_GetSAMStatus(num2, 0) == 0)
				{
					Syn_ClosePort(num2);
					flag = true;
					break;
				}
				Syn_ClosePort(num2);
			}
			if (!flag)
			{
				for (num2 = 1; num2 < 17; num2++)
				{
					if (Syn_OpenPort(num2) == 0 && Syn_GetSAMStatus(num2, 0) == 0)
					{
						Syn_ClosePort(num2);
						flag = true;
						break;
					}
					Syn_ClosePort(num2);
				}
			}
			if (!flag)
			{
				MsgCustom((string)m_hPubTab["InfoCRIIConn"], MessageBoxIcon.Asterisk);
			}
			else
			{
				num = Syn_OpenPort(num2);
				if (num == 0)
				{
					num = Syn_GetSAMStatus(num2, 0);
					num = Syn_StartFindIDCard(num2, ref array[0], 0);
					num = Syn_SelectIDCard(num2, ref array2[0], 0);
					if (Syn_ReadMsg(num2, 0, ref CardMsg) == 0)
					{
						num = 0;
					}
					else
					{
						_ = (string)m_hPubTab["InfoCRIIRead"];
					}
				}
				else
				{
					_ = (string)m_hPubTab["InfoCRIIPort"];
				}
			}
			Syn_ClosePort(num2);
		}
		catch (Exception ex)
		{
			MsgCustom((string)m_hPubTab["IDCardII_Err"] + " " + ex.Message, MessageBoxIcon.Hand);
		}
		return num;
	}

	public static int UpdateDB()
	{
		try
		{
			string sql = "select * from sysobjects where id = object_id(N'T_OtherId')";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count == 0)
			{
				sql = "create table T_OtherID (oth_ID varchar(128) PRIMARY KEY)";
				dataTable = SQLserver.Data_GetDataTable(sql);
				for (int i = 1; i < 20000; i++)
				{
					string text = i.ToString().PadLeft(12, '0');
					sql = "insert into T_OtherID values ('" + text + "')";
					SQLserver.Data_GetDataTable(sql);
				}
			}
		}
		catch (Exception)
		{
			return -1;
		}
		return 0;
	}

	public static string GetRegeditValue(string name, string key)
	{
		string result = string.Empty;
		try
		{
			RegistryKey registryKey = null;
			registryKey = Registry.LocalMachine.OpenSubKey(name, writable: true);
			result = registryKey.GetValue(key).ToString().Trim();
			registryKey.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return result;
	}

	public static void SetRegeditValue(string name, string key, object value)
	{
		try
		{
			RegistryKey registryKey = null;
			registryKey = Registry.LocalMachine.OpenSubKey(name, writable: true);
			registryKey.SetValue(key, value);
			registryKey.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public static bool LoadLogo(string filename, PictureBox picBox)
	{
		return LoadImg("image\\" + filename, picBox);
	}

	public static bool LoadImg(string filePath, PictureBox picBox)
	{
		bool result = false;
		if (File.Exists(filePath))
		{
			if (picBox.Image != null)
			{
				picBox.Image.Dispose();
			}
			picBox.Image = Image.FromFile(filePath);
			result = true;
		}
		return result;
	}

	public static string changeValue(double _d, CultureInfo _c)
	{
		return _d.ToString("F2", _c);
	}

	public static double changeValue(string _s, CultureInfo _c)
	{
		return Convert.ToDouble(_s, _c);
	}

	public static string GetStandDec(double num)
	{
		return changeValue(num, CultureInfo.InvariantCulture);
	}

	public static string GetLocDecStr(string text)
	{
		string text2 = text;
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "0";
		}
		string numberDecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
		if (numberDecimalSeparator != ".")
		{
			text2 = text2.Replace(".", numberDecimalSeparator);
		}
		return text2;
	}

	public static string GetStandDec(string text)
	{
		string text2 = text;
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "0";
		}
		string numberDecimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
		if (numberDecimalSeparator != ".")
		{
			text2 = text2.Replace(numberDecimalSeparator, ".");
		}
		return text2;
	}

	public static double GetRealDisValue(string text)
	{
		if (double.TryParse(text, out var result))
		{
			if (m_defDiscount == 0)
			{
				return 1.0 - result / 100.0;
			}
			return result / 100.0;
		}
		return m_defDiscount;
	}

	public static string GetFaceDisValue()
	{
		return (m_defDiscount * 100).ToString();
	}

	public static string GetFaceDisValue(double val)
	{
		if (m_defDiscount == 0)
		{
			return (100.0 - val * 100.0).ToString();
		}
		return (val * 100.0).ToString();
	}

	public static int FindSubStringCount(string source, string sub, bool ignoreCase = false)
	{
		int num = 0;
		int length = sub.Length;
		for (int i = 0; i < source.Length; i++)
		{
			if ((!ignoreCase) ? (source.Substring(i, length) == sub) : (source.Substring(i, length).ToLower() == sub.ToLower()))
			{
				num++;
				i = i + length - 1;
			}
		}
		return num;
	}

	public static void LocDFmt()
	{
		string text = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
		if (text.Length < 10)
		{
			int num = FindSubStringCount(text, "M");
			if (num == 1)
			{
				int startIndex = text.LastIndexOf("M");
				text = text.Insert(startIndex, "M");
			}
			num = FindSubStringCount(text, "d");
			if (num == 1)
			{
				int startIndex2 = text.LastIndexOf("d");
				text = text.Insert(startIndex2, "d");
			}
		}
		m_currDateFmt = text;
		m_currDateTimeFmt = text + " HH:mm";
	}

	public static string GetLocDate(DateTime dtTime)
	{
		return dtTime.ToString(m_currDateFmt);
	}

	public static string GetLocDTime(DateTime dtTime, string sec = "")
	{
		string text = m_currDateTimeFmt;
		if (!string.IsNullOrEmpty(sec))
		{
			text = text + ":" + sec;
		}
		return dtTime.ToString(text);
	}

	public static string GetStandDate(DateTime dtTime)
	{
		return dtTime.ToString(m_defDateFmt);
	}

	public static string GetStandDTime(DateTime dtTime, string sec = "")
	{
		if (string.IsNullOrEmpty(sec))
		{
			return dtTime.ToString(m_defDateTimeFmt);
		}
		return dtTime.ToString(m_defDateTimeFmt + ":" + sec);
	}

	public static void AddNTFSDirPermissions(string path)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		DirectorySecurity accessControl = directoryInfo.GetAccessControl();
		FileSystemAccessRule rule = new FileSystemAccessRule("Authenticated Users", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
		accessControl.AddAccessRule(rule);
		directoryInfo.SetAccessControl(accessControl);
	}

	public static void DelNTFSDirPermissions(string path)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		DirectorySecurity accessControl = directoryInfo.GetAccessControl();
		FileSystemAccessRule rule = new FileSystemAccessRule("Authenticated Users", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
		accessControl.AddAccessRule(rule);
		directoryInfo.SetAccessControl(accessControl);
	}

	public static bool IsScheduleStatus(DateTime dtDueCome, int iRoomStatus = 1)
	{
		if (iRoomStatus < 3)
		{
			return dtDueCome.AddHours(-1.0) <= DateTime.Now;
		}
		return false;
	}

	public static bool IsCanCheckIn(int iRoomID, DateTime dtCome, DateTime dtLeave)
	{
		string standDTime = GetStandDTime(dtCome, "00");
		string standDTime2 = GetStandDTime(dtLeave.AddMinutes(m_defClearTime), "00");
		bool result = false;
		string sql = "select sch_mob,(g_come_day + ' ' + g_come_time) as ComeTime, case when g_teamid IS NULL then sch_name else g_name end sch_name from T_Schedule where sch_flag = 0 and R_ID = " + iRoomID + " and g_come_day + ' ' + g_come_time < '" + standDTime2 + "' and g_level_day + ' " + m_defLeaveTime + "' > '" + standDTime + "' ";
		DataTable dataTable = SQLserver.Data_GetDataTable(sql);
		if (dataTable == null || dataTable.Rows.Count == 0)
		{
			result = true;
		}
		else if (DateTime.Parse(dataTable.Rows[0]["ComeTime"].ToString()) > dtLeave)
		{
			string msg = (string)m_hPubTab["SchInfo"] + "\n" + (string)m_hPubTab["GuestName"] + ":" + dataTable.Rows[0]["sch_name"].ToString() + "\n" + (string)m_hPubTab["MobileNumber"] + ":" + dataTable.Rows[0]["sch_mob"].ToString() + "\n" + (string)m_hPubTab["GuestInTime"] + ":" + dataTable.Rows[0]["ComeTime"].ToString() + "\n\n" + (string)m_hPubTab["IsContinueGuestIn"];
			if (MsgBox(msg, (string)m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				result = true;
			}
		}
		return result;
	}

	public static void getdat(havedata hd)
	{
		if (hd.isforhour)
		{
			hd.havday0 = 0.0;
			hd.havday1 = 0.0;
			hd.havday2 = 0.0;
			TimeSpan timeSpan;
			switch (hd.ptype)
			{
			case 1:
				timeSpan = ((hd.comedate.TimeOfDay.Minutes * 60 + hd.comedate.TimeOfDay.Seconds <= TimeSpan.Parse(m_defLeaveTime).Minutes * 60 + TimeSpan.Parse(m_defLeaveTime).Seconds) ? new TimeSpan(hd.comedate.TimeOfDay.Hours, TimeSpan.Parse(m_defLeaveTime).Minutes, TimeSpan.Parse(m_defLeaveTime).Seconds) : new TimeSpan(hd.comedate.TimeOfDay.Hours + 1, TimeSpan.Parse(m_defLeaveTime).Minutes, TimeSpan.Parse(m_defLeaveTime).Seconds));
				hd.havhour0 = (int)(hd.dtnow - (hd.comedate.Date + timeSpan)).TotalHours + 1;
				hd.havhour1 = hd.havhour0;
				hd.havhour2 = hd.havhour1 - 1.0;
				if (hd.havhour2 < 0.0)
				{
					hd.havhour2 = 0.0;
				}
				break;
			case 2:
				timeSpan = ((hd.comedate.TimeOfDay.Minutes * 60 + hd.comedate.TimeOfDay.Seconds <= TimeSpan.Parse(m_defLeaveTime).Minutes * 60 + TimeSpan.Parse(m_defLeaveTime).Seconds) ? new TimeSpan(hd.comedate.TimeOfDay.Hours - 1, TimeSpan.Parse(m_defLeaveTime).Minutes, TimeSpan.Parse(m_defLeaveTime).Seconds) : new TimeSpan(hd.comedate.TimeOfDay.Hours, TimeSpan.Parse(m_defLeaveTime).Minutes, TimeSpan.Parse(m_defLeaveTime).Seconds));
				hd.havhour0 = (int)(hd.dtnow - (hd.comedate.Date + timeSpan)).TotalHours + 1;
				hd.havhour1 = hd.havhour0;
				hd.havhour2 = hd.havhour1 - 1.0;
				if (hd.havhour2 < 0.0)
				{
					hd.havhour2 = 0.0;
				}
				break;
			default:
				hd.havhour0 = (int)(hd.dtnow - hd.comedate).TotalHours + 1;
				hd.havhour1 = hd.havhour0;
				hd.havhour2 = hd.havhour1 - 1.0;
				if (hd.havhour2 < 0.0)
				{
					hd.havhour2 = 0.0;
				}
				break;
			}
			if (hd.isfordis || hd.othhavhour >= (double)m_defHR)
			{
				hd.maypay0 = hd.havhour0 * hd.rpstandhour * hd.m_discount;
				hd.maypay1 = hd.havhour1 * hd.rpstandhour * hd.m_discount;
				hd.maypay2 = hd.havhour2 * hd.rpstandhour * hd.m_discount;
				return;
			}
			double num = (double)m_defHR - hd.othhavhour;
			if (hd.havhour0 < num)
			{
				hd.maypay0 = hd.rplesshour * hd.m_discount * hd.havhour0;
			}
			else
			{
				hd.maypay0 = hd.rplesshour * hd.m_discount * num + (hd.havhour0 - num) * hd.m_discount * hd.rpstandhour;
			}
			if (hd.havhour1 < num)
			{
				hd.maypay1 = hd.rplesshour * hd.m_discount * hd.havhour1;
			}
			else
			{
				hd.maypay1 = hd.rplesshour * hd.m_discount * num + (hd.havhour1 - num) * hd.m_discount * hd.rpstandhour;
			}
			if (hd.havhour2 < num)
			{
				hd.maypay2 = hd.rplesshour * hd.m_discount * hd.havhour2;
			}
			else
			{
				hd.maypay2 = hd.rplesshour * hd.m_discount * num + (hd.havhour2 - num) * hd.m_discount * hd.rpstandhour;
			}
			return;
		}
		hd.havhour0 = 0.0;
		hd.havhour1 = 0.0;
		hd.havhour2 = 0.0;
		hd.havday0 = (int)(hd.dtnow.Date - hd.comedate.Date).TotalDays;
		hd.havday1 = (int)(hd.dtnow.Date - hd.comedate.Date).TotalDays;
		hd.havday2 = (int)(hd.dtnow.Date - hd.comedate.Date).TotalDays;
		if (hd.dtnow.Date > hd.comedate.Date)
		{
			if (hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defComeTime))
			{
				hd.havday2--;
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defComeTime) && hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defLeaveTime))
			{
				hd.havday2--;
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defLeaveTime) && hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defHalfDay))
			{
				hd.havday1++;
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defHalfDay) && hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defFullDay))
			{
				hd.havday0 += 0.5;
				hd.havday1++;
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defFullDay))
			{
				hd.havday1++;
				hd.havday0++;
			}
			if (hd.comedate.TimeOfDay < TimeSpan.Parse(m_defComeTime) && (hd.ptype == 2 || hd.ptype == 0 || hd.ptype == -1))
			{
				hd.havday0++;
				hd.havday1++;
				hd.havday2++;
			}
			else if (hd.comedate.TimeOfDay >= TimeSpan.Parse(m_defComeTime) && hd.comedate.TimeOfDay < TimeSpan.Parse(m_defLeaveTime) && hd.ptype == 2)
			{
				hd.havday0++;
				hd.havday1++;
				hd.havday2++;
			}
			else if (hd.comedate.TimeOfDay >= TimeSpan.Parse(m_defLeaveTime) && hd.ptype == 1)
			{
				hd.havday0--;
				hd.havday1--;
				hd.havday2--;
			}
		}
		else if (hd.dtnow.Date == hd.comedate.Date)
		{
			if (hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defComeTime))
			{
				switch (hd.ptype)
				{
				case -1:
				case 0:
				case 2:
					hd.havday1++;
					break;
				}
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defComeTime) && hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defLeaveTime))
			{
				if (hd.comedate.TimeOfDay < TimeSpan.Parse(m_defComeTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1++;
						hd.havday0++;
						break;
					}
				}
				else
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1++;
						break;
					}
				}
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defLeaveTime) && hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defHalfDay))
			{
				if (hd.comedate.TimeOfDay < TimeSpan.Parse(m_defComeTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1 += 2.0;
						hd.havday0++;
						hd.havday2++;
						break;
					case 1:
						hd.havday1++;
						break;
					}
				}
				else if (hd.comedate.TimeOfDay >= TimeSpan.Parse(m_defComeTime) && hd.comedate.TimeOfDay < TimeSpan.Parse(m_defLeaveTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
						hd.havday1++;
						break;
					case 2:
						hd.havday1 += 2.0;
						hd.havday0++;
						hd.havday2++;
						break;
					case 1:
						hd.havday1++;
						break;
					}
				}
				else
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1++;
						break;
					}
				}
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defHalfDay) && hd.dtnow.TimeOfDay < TimeSpan.Parse(m_defFullDay))
			{
				if (hd.comedate.TimeOfDay < TimeSpan.Parse(m_defComeTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1 += 2.0;
						hd.havday0 += 1.5;
						hd.havday2++;
						break;
					case 1:
						hd.havday1++;
						break;
					}
				}
				else if (hd.comedate.TimeOfDay >= TimeSpan.Parse(m_defComeTime) && hd.comedate.TimeOfDay < TimeSpan.Parse(m_defLeaveTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
						hd.havday1++;
						break;
					case 2:
						hd.havday1 += 2.0;
						hd.havday0 += 1.5;
						hd.havday2++;
						break;
					case 1:
						hd.havday1++;
						break;
					}
				}
				else
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1++;
						break;
					}
				}
			}
			else if (hd.dtnow.TimeOfDay >= TimeSpan.Parse(m_defFullDay))
			{
				if (hd.comedate.TimeOfDay < TimeSpan.Parse(m_defComeTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1 += 2.0;
						hd.havday0 += 2.0;
						hd.havday2++;
						break;
					case 1:
						hd.havday1++;
						break;
					}
				}
				else if (hd.comedate.TimeOfDay >= TimeSpan.Parse(m_defComeTime) && hd.comedate.TimeOfDay < TimeSpan.Parse(m_defLeaveTime))
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
						hd.havday1++;
						break;
					case 2:
						hd.havday1 += 2.0;
						hd.havday0 += 2.0;
						hd.havday2++;
						break;
					case 1:
						hd.havday1++;
						break;
					}
				}
				else
				{
					switch (hd.ptype)
					{
					case -1:
					case 0:
					case 2:
						hd.havday1++;
						break;
					}
				}
			}
		}
		if (hd.havday0 < 0.0)
		{
			hd.havday0 = 0.0;
		}
		if (hd.havday1 < 0.0)
		{
			hd.havday1 = 0.0;
		}
		if (hd.havday2 < 0.0)
		{
			hd.havday2 = 0.0;
		}
		hd.maypay0 = hd.havday0 * hd.rp * hd.m_discount;
		hd.maypay1 = hd.havday1 * hd.rp * hd.m_discount;
		hd.maypay2 = hd.havday2 * hd.rp * hd.m_discount;
	}

	public static double GetRemainMoney(byte type, int _id, bool iscontainfuture)
	{
		double num = 0.0;
		try
		{
			string text = "";
			double num2 = 1.0;
			string text2 = "";
			switch (type)
			{
			case 0:
			{
				text = "select g_deposit,curr_rate,g_memo from v_cardguest where tr_level=0 and g_id =" + _id + "\n";
				object obj = text;
				text = string.Concat(obj, "select isnull(sum(othp_mpay),0) from t_otherpaid where a_id =0 and g_id in (select g_id from t_guest where tr_id in (select tr_id from t_guest where g_id=", _id, "))\n");
				DataSet dataSet = SQLserver.Data_GetDataSet(text);
				if (dataSet != null && dataSet.Tables.Count == 2)
				{
					if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count >= 1)
					{
						num = Convert.ToDouble(dataSet.Tables[0].Rows[0]["g_deposit"]);
						num2 = Convert.ToDouble(dataSet.Tables[0].Rows[0]["curr_rate"]);
						text2 = dataSet.Tables[0].Rows[0]["g_memo"].ToString();
						text2 = "(" + text2.Replace("->", ",").Trim(',') + ")";
					}
					if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count == 1)
					{
						num -= Convert.ToDouble(dataSet.Tables[1].Rows[0][0]) / num2;
					}
				}
				text = "select isnull(sum(Tr_mustpay+Tr_sodp),0) from t_rooms where tr_id in " + text2;
				DataTable dataTable = SQLserver.Data_GetDataTable(text);
				if (dataTable != null && dataTable.Rows.Count == 1)
				{
					num -= Convert.ToDouble(dataTable.Rows[0][0]) / num2;
				}
				if (!iscontainfuture)
				{
					text = "select (Tr_stayhour*tp_price+Tr_sohour*tp_pricestandhour)*tr_discount from v_room where tr_id in (select tr_id from t_guest where g_level=0 and g_id=" + _id + ")";
					dataTable = SQLserver.Data_GetDataTable(text);
					if (dataTable != null && dataTable.Rows.Count == 1)
					{
						num -= Convert.ToDouble(dataTable.Rows[0][0]) / num2;
					}
				}
				break;
			}
			}
			return num;
		}
		catch
		{
			return 0.0;
		}
	}

	public static bool IsAdministrator()
	{
		WindowsIdentity current = WindowsIdentity.GetCurrent();
		WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
		return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
	}
}
