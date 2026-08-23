using System;

namespace ComponentDll;

public class CompressStr
{
	private static string[] strNumber = new string[66]
	{
		"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
		"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
		"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
		"U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
		"e", "f", "g", "h", "i", "j", "k", "l", "m", "n",
		"o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
		"y", "z", "-", "/", "(", ")"
	};

	private static string strStringInString = "";

	private static string strNumberInString = "";

	public static string Compress(string p_string)
	{
		char[] array = Separate(p_string).ToCharArray();
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		for (int num = array.Length - 1; num > -1; num--)
		{
			_ = array[num];
			if (text.Length == 16)
			{
				break;
			}
			text = Convert.ToString(array[num]) + text;
		}
		string text5 = p_string.Substring(0, p_string.Length - text.Length);
		if (text.Length == 16)
		{
			char[] array2 = text5.ToCharArray();
			for (int num2 = array2.Length - 1; num2 > -1; num2--)
			{
				_ = array2[num2];
				if (text2.Length == 16)
				{
					break;
				}
				text2 = Convert.ToString(array[num2]) + text2;
			}
		}
		string text6 = text5.Substring(0, text5.Length - text2.Length);
		if (text2.Length == 16)
		{
			char[] array3 = text6.ToCharArray();
			for (int num3 = array3.Length - 1; num3 > -1; num3--)
			{
				_ = array3[num3];
				if (text3.Length == 16)
				{
					break;
				}
				text3 = Convert.ToString(array3[num3]) + text3;
			}
		}
		string text7 = text6.Substring(0, text6.Length - text3.Length);
		if (text3.Length == 16)
		{
			char[] array4 = text7.ToCharArray();
			for (int num4 = array4.Length - 1; num4 > -1; num4--)
			{
				_ = array4[num4];
				if (text4.Length == 16)
				{
					break;
				}
				text4 = Convert.ToString(array4[num4]) + text4;
			}
		}
		return To64(text4) + To64(text3) + To64(text2) + To64(text);
	}

	private static string To64(string strSource)
	{
		if (strSource.Trim().Length != 0)
		{
			long num = Convert.ToInt64(strSource);
			int num2 = 0;
			long[] array = new long[1000000];
			do
			{
				long num3 = num;
				num = num3 / 64;
				array[num2++] = num3 % 64;
			}
			while (num != 0);
			string text = "";
			for (int num4 = num2 - 1; num4 > -1; num4--)
			{
				text += strNumber[array[num4]];
			}
			return text;
		}
		return "";
	}

	private static string To10(string strSource)
	{
		if (strSource.Length != 0)
		{
			long num = 0L;
			for (int i = 0; i < strSource.Length; i++)
			{
				string text = strSource.Substring(i, 1);
				for (int j = 0; j < 64; j++)
				{
					if (strNumber[j] == text)
					{
						num += j * Convert.ToInt64(Math.Pow(64.0, strSource.Length - i - 1));
					}
				}
			}
			return Convert.ToString(num);
		}
		return "";
	}

	public static string UnCompress(string p_string)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		text = Incise(p_string);
		string text5 = p_string.Substring(0, p_string.Length - text.Length);
		text2 = Incise(text5);
		string text6 = text5.Substring(0, text5.Length - text2.Length);
		text3 = Incise(text6);
		string p_string2 = text6.Substring(0, text6.Length - text3.Length);
		text4 = Incise(p_string2);
		string p_strNumber = To10(text4) + To10(text3) + To10(text2) + To10(text);
		return ConnectString(p_strNumber);
	}

	private static string Incise(string p_string)
	{
		char[] array = p_string.ToCharArray();
		string text = "";
		if (p_string.Length >= 9)
		{
			for (int num = p_string.Length - 1; num > p_string.Length - 10; num--)
			{
				text = array[num] + text;
			}
			return text;
		}
		return p_string;
	}

	private static string Separate(string p_string)
	{
		char[] array = p_string.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] >= '0' && array[i] <= ':')
			{
				strNumberInString += array[i];
			}
			else
			{
				strStringInString = strStringInString + array[i] + To64(i.ToString());
			}
		}
		return strNumberInString;
	}

	private static string ConnectString(string p_strNumber)
	{
		string text = "";
		string text2 = "";
		if (strStringInString.Length != 0)
		{
			char[] array = strStringInString.ToCharArray();
			for (int i = 0; i < strStringInString.Length; i++)
			{
				if (i % 2 == 0)
				{
					text += array[i];
				}
				else
				{
					text2 += array[i];
				}
			}
		}
		if (text.Length != 0)
		{
			text.ToCharArray();
			char[] array2 = text2.ToCharArray();
			int num = 0;
			while (num > array2.Length)
			{
			}
		}
		return p_strNumber;
	}
}
