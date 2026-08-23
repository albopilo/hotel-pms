using System;

namespace ComponentDll;

internal struct KEYBDINPUT
{
	public short wVk;

	public short wScan;

	public int dwFlags;

	public int time;

	public IntPtr dwExtraInfo;
}
