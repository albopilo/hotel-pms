using System;
using System.Text;

namespace Dev_C_Sharp;

internal class CDesArray
{
	private static CDesArray instance;

	internal static CDesArray Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new CDesArray();
			}
			return instance;
		}
	}

	internal int ChkBuf(byte[] buf)
	{
		if (buf == null || buf.Length < 6)
		{
			return -2;
		}
		if (buf[0] != 170 || buf[buf.Length - 1] != 187 || buf[2] != buf.Length - 5)
		{
			return -3;
		}
		byte b = 0;
		for (int i = 1; i < buf.Length - 2; i++)
		{
			b ^= buf[i];
		}
		if (b != buf[buf.Length - 2])
		{
			return -1;
		}
		return 0;
	}

	internal int Des_S50(byte[] Outbuff, int lens, byte[] cardtype, StringBuilder retstr)
	{
		if (Outbuff == null || Outbuff.Length < 6 || lens < 6 || Outbuff.Length < lens)
		{
			return -2;
		}
		int num;
		for (num = Outbuff.Length - 1; num >= 0; num--)
		{
			if (num < 5)
			{
				return -2;
			}
			if (Outbuff[num] == 187)
			{
				break;
			}
		}
		byte[] array = new byte[num + 1];
		Array.Copy(Outbuff, 0, array, 0, array.Length);
		int num2 = ChkBuf(array);
		if (num2 < 0)
		{
			return num2;
		}
		cardtype[0] = (byte)(array[10] & 0x3F);
		switch (cardtype[0])
		{
		default:
			_ = 255;
			break;
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
			break;
		}
		return 0;
	}
}
