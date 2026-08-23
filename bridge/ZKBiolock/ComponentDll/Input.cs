using System.Runtime.InteropServices;

namespace ComponentDll;

[StructLayout(LayoutKind.Explicit)]
internal struct Input
{
	[FieldOffset(0)]
	public int type;

	[FieldOffset(4)]
	public MOUSEINPUT mi;

	[FieldOffset(4)]
	public KEYBDINPUT ki;

	[FieldOffset(4)]
	public HARDWAREINPUT hi;
}
internal class INPUT
{
	public const int MOUSE = 0;

	public const int KEYBOARD = 1;

	public const int HARDWARE = 2;
}
