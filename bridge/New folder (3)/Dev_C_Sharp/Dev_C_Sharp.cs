using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace Dev_C_Sharp;

[ClassInterface(ClassInterfaceType.None)]
[Guid("2E3C7BAD-1051-4622-9C4C-0A68AB79470B")]
[ComSourceInterfaces(typeof(MyCom_Events))]
public class Dev_C_Sharp : Dev_Sharp
{
	private bool timeOut;

	private bool canUse = true;

	private bool remind = true;

	private string stringRemind = "";

	private static Dev_C_Sharp instance;

	private bool isusb;

	private bool opened;

	private System.Timers.Timer t = new System.Timers.Timer();

	public bool Remind => remind;

	public string StringRemind => stringRemind;

	public static Dev_C_Sharp Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Dev_C_Sharp();
			}
			return instance;
		}
	}

	[DllImport("generalHID.dll", EntryPoint = "hidCreate")]
	private static extern int hidOpenDev();

	[DllImport("generalHID.dll", EntryPoint = "hidClose")]
	private static extern int hidCloseDev();

	[DllImport("generalHID.dll", EntryPoint = "hidWriteData")]
	private static extern int Hid_WriteData(int a, int b, byte[] data);

	[DllImport("generalHID.dll", EntryPoint = "hidReadData")]
	private static extern int Hid_ReadData(int a, int b, byte[] data);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Port_Open(int portnum, int baud, int DTR_State, int RTS_State);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Port_Close(int portnum);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Dev_Buzzer(int devType, int devAddr, int mill, int buzzernum);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Card_Write(int devType, int devAddr, int cardtype, int cardnum, string datetime, string carddata, int datalen, byte[] retbuff);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Card_Read_Str(int devType, int devAddr, byte[] cardtype, StringBuilder retstr);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Card_Read_Str_HID(int devType, int devAddr, byte[] Outbuff, int lens, byte[] cardtype, StringBuilder retstr);

	[DllImport("RadioDev.dll", EntryPoint = "Radio_Dev_ReadS70_Str")]
	private static extern int Radio_ReadS70_Str(int devAddr, int lockRecs, StringBuilder lockInfo, StringBuilder recStr);

	[DllImport("RadioDev.dll", EntryPoint = "Radio_HID_ReadS70_Str")]
	private static extern int Radio_ReadS70_Str_HID(StringBuilder lockInfo, StringBuilder recStr);

	[DllImport("RadioDev.dll")]
	private static extern void Radio_HID_RecsCache(byte[] databuff, int index, int lens);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Card_Clear(int devType, int devAddr, int reset, byte[] retbuff);

	[DllImport("reg.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern long GETREG();

	[DllImport("RadioDev.dll", EntryPoint = "GETREGINFO")]
	private static extern int _GINFO(StringBuilder regid, StringBuilder regkey);

	private static int _GetRegInfo(StringBuilder regid, StringBuilder regkey)
	{
		GETREG();
		return _GINFO(regid, regkey);
	}

	[DllImport("RadioDev.dll")]
	private static extern int WRT_KEY(string regkey);

	[DllImport("RadioDev.dll", EntryPoint = "CHKREG")]
	private static extern int _ChkReg(string regid, string regkey, bool chkid);

	[DllImport("RadioDev.dll", EntryPoint = "GetRadioDevParms")]
	private static extern void _GetRadioDevParms(byte[] skver, byte[] initpwd, ref int saler, ref int hotelid);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Card_ReadDefault(int devType, int devAddr, int cardtype, byte[] retbuff);

	[DllImport("RadioDev.dll", EntryPoint = "Radio_Get_CMD_Data_Hex")]
	private static extern int RadioGetCMD(int devAddr, byte cmd, byte[] databuff, int datalens, byte[] retbuff);

	private static int GetBuf(int devAddr, byte cmd, byte[] databuff, int datalens, byte[] retbuff)
	{
		retbuff = new byte[datalens + 6];
		retbuff[0] = 170;
		retbuff[1] = (byte)devAddr;
		retbuff[2] = (byte)(datalens + 1);
		retbuff[3] = cmd;
		byte b = (byte)(retbuff[1] ^ retbuff[2] ^ retbuff[3]);
		for (int i = 4; i - 4 < datalens; i++)
		{
			retbuff[i] = databuff[i - 4];
			b ^= retbuff[i];
		}
		retbuff[retbuff.Length - 2] = b;
		retbuff[retbuff.Length - 1] = 187;
		return retbuff.Length;
	}

	[DllImport("RadioDev.dll")]
	private static extern int RDev_Init_Config(byte ver, int agentid, int hotelid, byte[] hotelpwd);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Init_Sector(int sector);

	[DllImport("RadioDev.dll")]
	private static extern int Radio_Set_Pwd(string pwd);

	[DllImport("RadioDev.dll")]
	private static extern bool HexToAsc(byte[] uca_Hex, StringBuilder uca_Ascii, int uc_Length);

	public Dev_C_Sharp(bool _remind)
	{
		remind = _remind;
		StringBuilder stringBuilder = new StringBuilder(256);
		StringBuilder stringBuilder2 = new StringBuilder(256);
		_GetRegInfo(stringBuilder, stringBuilder2);
		int num = _ChkReg(stringBuilder.ToString(), stringBuilder2.ToString(), chkid: true);
		byte[] array = new byte[1];
		byte[] array2 = new byte[2];
		int saler = 0;
		int hotelid = 0;
		_GetRadioDevParms(array, array2, ref saler, ref hotelid);
		if (array[0] == 0 && array2[0] == 0 && array2[1] == 0 && saler == 0 && hotelid == 0)
		{
			canUse = false;
			stringRemind = "Please contact the vendor to get a key! You should provide your register ID.";
			if (Remind)
			{
				MessageBox.Show(stringRemind);
			}
		}
		else if (num < 0)
		{
			timeOut = true;
			canUse = false;
			stringRemind = "Please contact the vendor to get a new key! You should provide your current key and your register ID.";
			if (Remind)
			{
				MessageBox.Show(stringRemind);
			}
		}
		RDev_Init_Config(array[0], saler, hotelid, array2);
		t.Interval = (new TimeSpan(24, 0, 0).TotalSeconds - DateTime.Now.TimeOfDay.TotalSeconds) * 1000.0;
		t.Elapsed += t_Elapsed;
		t.Start();
	}

	public Dev_C_Sharp()
		: this(_remind: false)
	{
	}

	public int OpenPort(int portnum, int baud, bool buzzer)
	{
		if (portnum <= 0)
		{
			portnum = 0;
			isusb = true;
		}
		else
		{
			isusb = false;
		}
		int num = Radio_Port_Open(portnum, baud, 1, 0);
		if ((num == 0 && !isusb) || isusb)
		{
			byte[] retval = new byte[50];
			num = HIDGetVersion(CHK: false, 22, ref retval);
			if (num == 22)
			{
				opened = true;
				if (buzzer)
				{
					DevBuzzer(1, 2);
				}
			}
			else if (isusb)
			{
				return -800;
			}
			return num;
		}
		return num;
	}

	public int ClosePort(int portnum)
	{
		opened = false;
		return Radio_Port_Close(portnum);
	}

	public int DevBuzzer(byte mill, byte num)
	{
		if (opened)
		{
			if (isusb)
			{
				int num2 = 0;
				byte[] array = new byte[256];
				byte[] databuff = new byte[2] { mill, num };
				RadioGetCMD(0, 137, databuff, 2, array);
				num2 = Hid_WriteData(0, 8, array);
				if (num2 != 8)
				{
					return -601;
				}
				byte[] array2 = new byte[50];
				num2 = Hid_ReadData(0, 7, array2);
				if (num2 != 7)
				{
					return -array2[4];
				}
				return num2;
			}
			return Radio_Dev_Buzzer(1, 0, 1, 2);
		}
		return -802;
	}

	public int WriteCard(int cardtype, int cardnum, string datetime, string carddata, int datalen, bool Buzzer)
	{
		if (canUse)
		{
			if (opened)
			{
				int num = 0;
				byte[] array = new byte[256];
				num = 0;
				num = Radio_Card_Write(0, 0, cardtype, cardnum, datetime, carddata, datalen, array);
				if (num < 0)
				{
					return num;
				}
				if (isusb)
				{
					int num2 = Hid_WriteData(0, num, array);
					if (num2 != num)
					{
						return -601;
					}
					byte[] array2 = new byte[256];
					num = Hid_ReadData(0, 7, array2);
					if (num <= 0 || array2[3] != 0)
					{
						return -array2[4];
					}
					num = 0;
				}
				if (Buzzer)
				{
					DevBuzzer(1, 2);
				}
				return num;
			}
			return -802;
		}
		if (timeOut)
		{
			return -2001;
		}
		return -2000;
	}

	public int ReadCard(out byte CardType, ref string CardData, bool Buzzer)
	{
		CardType = 0;
		if (canUse)
		{
			if (opened)
			{
				int num = 0;
				byte[] array = new byte[256];
				byte[] array2 = new byte[1];
				StringBuilder stringBuilder = new StringBuilder(256);
				if (isusb)
				{
					byte[] array3 = new byte[256];
					num = Radio_Card_ReadDefault(1, 0, 0, array3);
					if (num > 0)
					{
						int num2 = num;
						num = Hid_WriteData(0, num2, array3);
						if (num != num2)
						{
							return -601;
						}
						num = Hid_ReadData(0, 64, array);
						if (num < 64)
						{
							return -array[4];
						}
						num = Radio_Card_Read_Str_HID(1, 0, array, num, array2, stringBuilder);
					}
				}
				else
				{
					num = Radio_Card_Read_Str(1, 0, array2, stringBuilder);
				}
				if (num < 0)
				{
					return num;
				}
				if (Buzzer)
				{
					DevBuzzer(1, 2);
				}
				CardType = array2[0];
				CardData = stringBuilder.ToString();
				return num;
			}
			return -802;
		}
		if (timeOut)
		{
			return -2001;
		}
		return -2000;
	}

	public int ReadCard(ref string CardData, bool Buzzer)
	{
		byte CardType = 0;
		return ReadCard(out CardType, ref CardData, Buzzer);
	}

	public int ReadCardS70(StringBuilder lockInfo, StringBuilder recStr, bool Buzzer)
	{
		if (canUse)
		{
			if (opened)
			{
				int num = -1;
				if (isusb)
				{
					byte[] array = new byte[64];
					byte[] array2 = new byte[64];
					array2[0] = 170;
					array2[1] = 0;
					array2[2] = 9;
					array2[3] = 96;
					array2[4] = 1;
					array2[5] = 0;
					byte b;
					array2[11] = (b = byte.MaxValue);
					array2[6] = (array2[7] = (array2[8] = (array2[9] = (array2[10] = b))));
					array2[12] = array2[1];
					for (int i = 2; i < 12; i++)
					{
						array2[12] ^= array2[i];
					}
					array2[13] = 187;
					int b2 = 54;
					byte[] data = new byte[8] { 170, 0, 3, 96, 221, 102, 216, 187 };
					num = 14;
					int num2 = Hid_WriteData(0, num, array2);
					if (num2 != num)
					{
						return -601;
					}
					num = 0;
					for (int j = 0; j < 64; j++)
					{
						byte[] array3 = new byte[64];
						if (j > 0)
						{
							num2 = Hid_WriteData(0, 8, data);
							if (num2 != 8)
							{
								continue;
							}
						}
						num2 = Hid_ReadData(0, b2, array3);
						if (num2 > 0)
						{
							if (array3[3] != 0)
							{
								return -array3[4];
							}
							if (array3[0] == 170 && array3[1] == 0 && array3[2] == 3 && array3[3] == 0 && array3[4] == 204 && array3[5] == 85 && array3[6] == 154 && array3[7] == 187)
							{
								break;
							}
							num2 = 48;
							for (int k = 0; k < num2; k++)
							{
								array[k] = array3[k + 4];
							}
							num++;
							Radio_HID_RecsCache(array, num - 1, num);
						}
					}
					if (num != 41)
					{
						return -602;
					}
					num = Radio_ReadS70_Str_HID(lockInfo, recStr);
				}
				else
				{
					num = Radio_ReadS70_Str(0, 256, lockInfo, recStr);
				}
				if (Buzzer)
				{
					DevBuzzer(1, 2);
				}
				return num;
			}
			return -802;
		}
		if (timeOut)
		{
			return -2001;
		}
		return -2000;
	}

	public int ClearCard(int type, bool Buzzer)
	{
		if (canUse)
		{
			if (opened)
			{
				int num = 0;
				byte[] array = new byte[256];
				num = Radio_Card_Clear(1, 0, type, array);
				if (isusb)
				{
					int num2 = Hid_WriteData(0, num, array);
					if (num2 != num)
					{
						return -601;
					}
					byte[] array2 = new byte[256];
					num = Hid_ReadData(0, 7, array2);
					if (num <= 0 || array2[3] != 0)
					{
						return -array2[4];
					}
				}
				if (Buzzer)
				{
					DevBuzzer(1, 2);
				}
				return num;
			}
			return -802;
		}
		if (timeOut)
		{
			return -2001;
		}
		return -2000;
	}

	public int GetRegInfo(StringBuilder regid, StringBuilder regkey)
	{
		return _GetRegInfo(regid, regkey);
	}

	public int WriteKey(string regkey)
	{
		return WRT_KEY(regkey);
	}

	public int ChkReg(string regid, string regkey, bool chkid)
	{
		if (!canUse && !timeOut)
		{
			return -1;
		}
		return _ChkReg(regid, regkey, chkid);
	}

	public void GetDevParms(byte[] ver, byte[] initpwd, ref int saler, ref int hotelid)
	{
		_GetRadioDevParms(ver, initpwd, ref saler, ref hotelid);
	}

	internal int HIDGetVersion(bool CHK, int len, ref byte[] retval)
	{
		if (opened || !CHK)
		{
			int num = 0;
			num = Hid_WriteData(0, 6, new byte[10] { 170, 0, 1, 134, 135, 187, 0, 0, 0, 0 });
			if (6 != num)
			{
				return -601;
			}
			num = Hid_ReadData(0, len, retval);
			if (num <= 0 || retval[3] != 0)
			{
				return -retval[4];
			}
			return num;
		}
		return -802;
	}

	public int GetVersion(int len, ref string ver)
	{
		byte[] retval = new byte[len + 10];
		int num = HIDGetVersion(CHK: true, len, ref retval);
		if (num == len)
		{
			int num2 = retval[2];
			for (int i = 0; i < num2 - 1; i++)
			{
				ver = ver + retval[4 + i] + " ";
			}
		}
		return num;
	}

	private void t_Elapsed(object sender, ElapsedEventArgs e)
	{
		if (t.Interval != new TimeSpan(24, 0, 0).TotalSeconds * 1000.0)
		{
			t.Interval = new TimeSpan(24, 0, 0).TotalSeconds * 1000.0;
		}
		try
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			StringBuilder stringBuilder2 = new StringBuilder(256);
			_GetRegInfo(stringBuilder, stringBuilder2);
			int num = _ChkReg(stringBuilder.ToString(), stringBuilder2.ToString(), chkid: true);
			byte[] array = new byte[1];
			byte[] array2 = new byte[2];
			int saler = 0;
			int hotelid = 0;
			_GetRadioDevParms(array, array2, ref saler, ref hotelid);
			if (array[0] == 0 && array2[0] == 0 && array2[1] == 0 && saler == 0 && hotelid == 0)
			{
				canUse = false;
				stringRemind = "Please contact the vendor to get a key! You should provide your register ID.";
			}
			else if (num < 0)
			{
				timeOut = true;
				canUse = false;
				stringRemind = "Please contact the vendor to get a new key! You should provide your current key and your register ID.";
			}
			RDev_Init_Config(array[0], saler, hotelid, array2);
		}
		catch
		{
		}
	}
}
