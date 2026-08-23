using System;
using System.Runtime.InteropServices;

namespace Urovo;

public class Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public class MSGQUEUEOPTIONS
	{
		public uint dwSize;

		public uint dwFlags;

		public uint dwMaxMessages;

		public uint cbMaxMessage;

		public bool bReadAccess;
	}

	public struct POWER_BROADCAST_POWER_INFO
	{
		public uint dwNumLevels;

		public uint dwBatteryLifeTime;

		public uint dwBatteryFullLifeTime;

		public uint dwBackupBatteryLifeTime;

		public uint dwBackupBatteryFullLifeTime;

		public byte bACLineStatus;

		public byte bBatteryFlag;

		public byte bBatteryLifePercent;

		public byte bBackupBatteryFlag;

		public byte bBackupBatteryLifePercent;
	}

	public struct POWER_BROADCAST
	{
		public uint Message;

		public uint Flags;

		public uint Length;

		public POWER_BROADCAST_POWER_INFO PI;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class SYSTEM_POWER_STATUS_EX2
	{
		public byte ACLineStatus;

		public byte BatteryFlag;

		public byte BatteryLifePercent;

		public byte Reserved1;

		public uint BatteryLifeTime;

		public uint BatteryFullLifeTime;

		public byte Reserved2;

		public byte BackupBatteryFlag;

		public byte BackupBatteryLifePercent;

		public byte Reserved3;

		public uint BackupBatteryLifeTime;

		public uint BackupBatteryFullLifeTime;

		public uint BatteryVoltage;

		public uint BatteryCurrent;

		public uint BatteryAverageCurrent;

		public uint BatteryAverageInterval;

		public uint BatterymAHourConsumed;

		public uint BatteryTemperature;

		public uint BackupBatteryVoltage;

		public byte BatteryChemistry;
	}

	public struct TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
	{
		public int x = x;

		public int y = y;

		public ushort Red = (ushort)(red << 8);

		public ushort Green = (ushort)(green << 8);

		public ushort Blue = (ushort)(blue << 8);

		public ushort Alpha = (ushort)(alpha << 8);
	}

	public struct GRADIENT_RECT(uint ul, uint lr)
	{
		public uint UpperLeft = ul;

		public uint LowerRight = lr;
	}

	public class RECT
	{
		private int left;

		private int top;

		private int right;

		private int bottom;

		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public class KBDLLHOOKSTRUCT
	{
		public uint vkCode;

		public uint scanCode;

		public uint flags;

		public uint time;

		public uint dwExtraInfo;
	}

	public delegate int HOOKPROC(int code, uint wParam, KBDLLHOOKSTRUCT lParam);

	[StructLayout(LayoutKind.Sequential)]
	public class SYSTEMTIME
	{
		public ushort wYear;

		public ushort wMonth;

		public ushort wDayOfWeek;

		public ushort wDay;

		public ushort wHour;

		public ushort wMinute;

		public ushort wSecond;

		public ushort wMilliseconds;

		public SYSTEMTIME(DateTime datetime)
		{
			wYear = (ushort)datetime.Year;
			wMonth = (ushort)datetime.Month;
			wDay = (ushort)datetime.Day;
			wHour = (ushort)datetime.Hour;
			wMinute = (ushort)datetime.Minute;
			wSecond = (ushort)datetime.Second;
			wMilliseconds = (ushort)datetime.Millisecond;
		}
	}

	public const int IDC_WAIT = 32514;

	public const uint WM_KEYFIRST = 256u;

	public const uint WM_KEYDOWN = 256u;

	public const uint WM_KEYUP = 257u;

	public const uint WM_KEYLAST = 264u;

	public const int WM_HOTKEY = 786;

	public const uint MOD_ALT = 1u;

	public const uint MOD_CONTROL = 2u;

	public const uint MOD_KEYUP = 4096u;

	public const uint MOD_SHIFT = 4u;

	public const uint MOD_WIN = 8u;

	public const uint VK_NUMPAD0 = 96u;

	public const uint VK_NUMPAD1 = 97u;

	public const uint VK_NUMPAD2 = 98u;

	public const uint VK_NUMPAD3 = 99u;

	public const uint VK_NUMPAD4 = 100u;

	public const uint VK_NUMPAD5 = 101u;

	public const uint VK_NUMPAD6 = 102u;

	public const uint VK_NUMPAD7 = 103u;

	public const uint VK_NUMPAD8 = 104u;

	public const uint VK_NUMPAD9 = 105u;

	public const uint VK_F1 = 112u;

	public const uint VK_F2 = 113u;

	public const uint VK_F3 = 114u;

	public const uint VK_F4 = 115u;

	public const uint VK_F5 = 116u;

	public const uint VK_F6 = 117u;

	public const uint VK_F7 = 118u;

	public const uint VK_F8 = 119u;

	public const uint VK_F9 = 120u;

	public const uint VK_F23 = 134u;

	public const uint VK_F24 = 135u;

	public const uint VK_ESCAPE = 27u;

	public const uint VK_RETURN = 13u;

	public const uint VK_CAPITAL = 20u;

	public const uint VK_NUMBER = 11u;

	public const uint WAIT_TIMEOUT = 258u;

	public const uint WAIT_FAILED = uint.MaxValue;

	public const uint INFINITE = uint.MaxValue;

	public const uint EVENT_PULSE = 1u;

	public const uint EVENT_RESET = 2u;

	public const uint EVENT_SET = 3u;

	public const uint PBT_TRANSITION = 1u;

	public const uint PBT_RESUME = 2u;

	public const uint PBT_POWERSTATUSCHANGE = 4u;

	public const uint PBT_POWERINFOCHANGE = 8u;

	public const uint POWER_STATE_ON = 65536u;

	public const uint POWER_STATE_OFF = 131072u;

	public const uint POWER_STATE_CRITICAL = 262144u;

	public const uint POWER_STATE_BOOT = 524288u;

	public const uint POWER_STATE_IDLE = 1048576u;

	public const uint POWER_STATE_SUSPEND = 2097152u;

	public const uint POWER_STATE_RESET = 8388608u;

	public const uint POWER_FORCE = 4096u;

	public const uint SRCCOPY = 13369376u;

	public const uint SRCPAINT = 15597702u;

	public const uint SRCAND = 8913094u;

	public const uint SRCINVERT = 6684742u;

	public const uint SRCERASE = 4457256u;

	public const uint NOTSRCCOPY = 3342344u;

	public const uint NOTSRCERASE = 1114278u;

	public const uint MERGECOPY = 12583114u;

	public const uint MERGEPAINT = 12255782u;

	public const uint PATCOPY = 15728673u;

	public const uint PATPAINT = 16452105u;

	public const uint PATINVERT = 5898313u;

	public const uint DSTINVERT = 5570569u;

	public const uint BLACKNESS = 66u;

	public const uint WHITENESS = 16711778u;

	public const int GRADIENT_FILL_RECT_H = 0;

	public const int GRADIENT_FILL_RECT_V = 1;

	public const int WHITE_BRUSH = 0;

	public const int LTGRAY_BRUSH = 1;

	public const int GRAY_BRUSH = 2;

	public const int DKGRAY_BRUSH = 3;

	public const int BLACK_BRUSH = 4;

	public const int NULL_BRUSH = 5;

	public const int HOLLOW_BRUSH = 5;

	public const int WHITE_PEN = 6;

	public const int BLACK_PEN = 7;

	public const int NULL_PEN = 8;

	public const int SYSTEM_FONT = 13;

	public const int DEFAULT_PALETTE = 15;

	public const int BORDERX_PEN = 32;

	public const int BORDERY_PEN = 33;

	public const int PS_SOLID = 0;

	public const int PS_DASH = 1;

	public const int PS_NULL = 5;

	public const int WH_JOURNALRECORD = 0;

	public const int WH_JOURNALPLAYBACK = 1;

	public const int WH_KEYBOARD_LL = 20;

	public const int HC_ACTION = 0;

	public const uint SND_ALIAS = 65536u;

	public const uint SND_FILENAME = 131072u;

	public const uint SND_SYNC = 0u;

	public const uint SND_ASYNC = 1u;

	public const uint SND_NODEFAULT = 2u;

	public const uint SND_MEMORY = 4u;

	public const uint SND_LOOP = 8u;

	public const uint SND_NOSTOP = 16u;

	[DllImport("kernel32.dll")]
	public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

	[DllImport("kernel32.dll")]
	public static extern IntPtr SetCursor(IntPtr hCursor);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, int pvParam, uint fWinIni);

	[DllImport("kernel32.dll")]
	public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPWStr)] string lpClassName, [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

	[DllImport("kernel32.dll")]
	public static extern short GetKeyState(int nVirtKey);

	[DllImport("kernel32.dll")]
	public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

	[DllImport("kernel32.dll")]
	public static extern uint WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, [MarshalAs(UnmanagedType.Bool)] bool fWaitAll, uint dwMilliseconds);

	[DllImport("kernel32.dll")]
	public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, [MarshalAs(UnmanagedType.Bool)] bool bManualReset, [MarshalAs(UnmanagedType.Bool)] bool bInitialState, [MarshalAs(UnmanagedType.LPWStr)] string lpName);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool EventModify(IntPtr hEvent, uint func);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseHandle(IntPtr hObject);

	[DllImport("kernel32.dll")]
	public static extern IntPtr CreateMsgQueue([MarshalAs(UnmanagedType.LPWStr)] string lpszName, MSGQUEUEOPTIONS lpOptions);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseMsgQueue(IntPtr hMsgQ);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ReadMsgQueue(IntPtr hMsgQ, [MarshalAs(UnmanagedType.AsAny)] out object lpBuffer, uint cbBufferSize, out uint lpNumberOfBytesRead, uint dwTimeout, out uint pdwFlags);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ReadMsgQueue(IntPtr hMsgQ, out POWER_BROADCAST BroadCast, uint cbBufferSize, out uint lpNumberOfBytesRead, uint dwTimeout, out uint pdwFlags);

	[DllImport("kernel32.dll")]
	public static extern IntPtr RequestPowerNotifications(IntPtr hMsgQ, uint Flags);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool StopPowerNotifications(IntPtr hMsgQ);

	[DllImport("kernel32.dll")]
	public static extern uint GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 pSystemPowerStatusEx2, uint dwLen, [MarshalAs(UnmanagedType.Bool)] bool fUpdate);

	[DllImport("kernel32.dll")]
	public static extern uint SetSystemPowerState([MarshalAs(UnmanagedType.LPWStr)] string psState, uint StateFlags, uint Options);

	[DllImport("kernel32.dll")]
	public static extern IntPtr GetDC(IntPtr hWnd);

	[DllImport("kernel32.dll")]
	public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

	[DllImport("kernel32.dll")]
	public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DeleteDC(IntPtr hDC);

	[DllImport("kernel32.dll")]
	public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

	[DllImport("kernel32.dll")]
	public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hgdiobj);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DeleteObject(IntPtr hgdiobj);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, uint dwNumVertex, GRADIENT_RECT[] pMesh, uint dwNumMesh, uint dwMode);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidth, int nHeight);

	[DllImport("kernel32.dll")]
	public static extern IntPtr GetStockObject(int fnObject);

	public static uint RGB(byte r, byte g, byte b)
	{
		return (uint)(r | (g << 8) | (b << 16));
	}

	[DllImport("kernel32.dll")]
	public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

	[DllImport("kernel32.dll")]
	public static extern IntPtr CreateSolidBrush(uint crColor);

	[DllImport("kernel32.dll")]
	public static extern int FillRect(IntPtr hDC, RECT lprc, IntPtr hbr);

	[DllImport("kernel32.dll", EntryPoint = "SetWindowsHookExW")]
	public static extern IntPtr SetWindowsHookEx(int idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HOOKPROC lpfn, IntPtr hmod, uint dwThreadId);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool UnhookWindowsHookEx(IntPtr hhk);

	[DllImport("kernel32.dll")]
	public static extern int CallNextHookEx(IntPtr hhk, int nCode, uint wParam, KBDLLHOOKSTRUCT lParam);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool sndPlaySound(byte[] lpszSoundName, uint fuSound);

	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetLocalTime(SYSTEMTIME lpSystemTime);
}
