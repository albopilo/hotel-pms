using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ComponentDll;

public class CommonClass
{
	public const int WS_SYSMENU = 524288;

	public const int WS_MINIMIZEBOX = 131072;

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern int GetWindowLong(HandleRef hWnd, int nIndex);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

	public static void SetTaskMenu(Form form)
	{
		int windowLong = GetWindowLong(new HandleRef(form, form.Handle), -16);
		SetWindowLong(new HandleRef(form, form.Handle), -16, windowLong | 0x80000 | 0x20000);
	}
}
