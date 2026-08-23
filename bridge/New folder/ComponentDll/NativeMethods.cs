using System;
using System.Runtime.InteropServices;

namespace ComponentDll;

internal static class NativeMethods
{
	[DllImport("User32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("User32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	[DllImport("User32.dll", CharSet = CharSet.Auto)]
	internal static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

	[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
	internal static extern int GetTickCount();

	[DllImport("User32.dll", CharSet = CharSet.Auto)]
	internal static extern short GetKeyState(int nVirtKey);

	[DllImport("User32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
}
