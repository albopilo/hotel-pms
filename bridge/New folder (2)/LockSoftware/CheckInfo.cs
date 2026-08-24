using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LockSoftware;

public class CheckInfo
{
	public static bool IsEmail(string source)
	{
		if (source.IndexOf("@") > 0 && source.IndexOf(".") > 0)
		{
			return true;
		}
		return false;
	}

	public static void FloatKeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (!(sender is TextBox textBox))
			{
				return;
			}
			if (e.KeyChar >= '１' && e.KeyChar < '；')
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SNumberInputError"], MessageBoxIcon.Exclamation);
				return;
			}
			if (e.KeyChar > 'Ā')
			{
				e.Handled = true;
				if (textBox.Tag == null)
				{
					Program.MsgCustom((string)Program.m_hPubTab["SFloatInputFormatError"], MessageBoxIcon.Exclamation);
					textBox.Tag = "isshow";
				}
				return;
			}
			bool flag = textBox.Text.Contains(".");
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.' && e.KeyChar != '\r')
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SFloatInputFormatError"], MessageBoxIcon.Exclamation);
				return;
			}
			if (flag && e.KeyChar == '.')
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SFloatInputIsContainsDot"], MessageBoxIcon.Exclamation);
				return;
			}
			if (textBox.Text.Trim().Length == 0 && e.KeyChar == '.')
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SFloatInputDotIsFirst"], MessageBoxIcon.Exclamation);
				return;
			}
			try
			{
				if (e.KeyChar != '\b' && e.KeyChar != '.' && textBox.Text.Trim().Length == 1 && textBox.Text.Trim() == "0" && e.KeyChar != '\r')
				{
					Program.MsgCustom((string)Program.m_hPubTab["SFloatInputDot"], MessageBoxIcon.Exclamation);
					e.Handled = true;
				}
			}
			catch
			{
				e.Handled = true;
			}
		}
		catch
		{
		}
	}

	public static void NumberKeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			NumberKeyPress(sender, e, 8);
		}
		catch
		{
		}
	}

	public static void NumberKeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (sender is TextBox { SelectionStart: 0 } textBox && e.KeyValue == 46 && textBox.TextLength > textBox.SelectionLength)
			{
				textBox.Text = int.Parse(textBox.Text.Substring(1)).ToString();
				textBox.SelectionStart = 0;
				e.Handled = true;
			}
		}
		catch
		{
		}
	}

	public static void MemoKeyPress(object sender, KeyPressEventArgs e, int len)
	{
		try
		{
			MemoEdit val = (MemoEdit)((sender is MemoEdit) ? sender : null);
			if (val == null)
			{
				return;
			}
			int length = GetLength(e.KeyChar.ToString());
			int length2 = GetLength(((TextEdit)val).SelectedText);
			int length3 = GetLength(((Control)(object)val).Text);
			if (length3 + length - length2 <= len || e.KeyChar == '\b' || e.KeyChar == '\r')
			{
				return;
			}
			if (((Control)(object)val).Tag == null || ((Control)(object)val).Tag.ToString().Length > 1)
			{
				if (((Control)(object)val).Tag == null || (DateTime.Now - DateTime.Parse(((Control)(object)val).Tag.ToString())).Milliseconds > 10)
				{
					((Control)(object)val).Tag = 1;
					Program.MsgCustom((string)Program.m_hPubTab["SInputIsOutLen"], MessageBoxIcon.Exclamation);
				}
				((Control)(object)val).Tag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
			}
			e.Handled = true;
		}
		catch
		{
		}
	}

	public static void KeyPress(object sender, KeyPressEventArgs e, bool Letter)
	{
		try
		{
			if (sender is TextBox textBox && e.KeyChar != '\b' && e.KeyChar != '\r' && textBox.SelectionLength == 0 && (e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar < 'A' || e.KeyChar > 'Z') && (e.KeyChar < 'a' || e.KeyChar > 'z'))
			{
				e.Handled = true;
			}
		}
		catch
		{
		}
	}

	public static void KeyPress(object sender, KeyPressEventArgs e)
	{
		KeyPress(sender, e, 24);
	}

	private static int GetLength(string str)
	{
		if (str.Length == 0)
		{
			return 0;
		}
		ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
		int num = 0;
		byte[] bytes = aSCIIEncoding.GetBytes(str);
		for (int i = 0; i < bytes.Length; i++)
		{
			num = ((bytes[i] != 63) ? (num + 1) : (num + 2));
		}
		return num;
	}

	public static void NumberKeyPress(object sender, KeyPressEventArgs e, int len)
	{
		try
		{
			if (!(sender is TextBox textBox))
			{
				return;
			}
			if (e.KeyChar == '\u0003')
			{
				e.Handled = true;
			}
			else if (e.KeyChar >= '１' && e.KeyChar < '；')
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SNumberInputError"], MessageBoxIcon.Exclamation);
			}
			else if (e.KeyChar > 'Ā')
			{
				e.Handled = true;
				if (textBox.Tag == null)
				{
					Program.MsgCustom((string)Program.m_hPubTab["SNumberInputFormatError"], MessageBoxIcon.Exclamation);
					textBox.Tag = "isshow";
				}
			}
			else if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '\r')
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SNumberInputFormatError"], MessageBoxIcon.Exclamation);
			}
			else if (textBox.SelectionLength == 0 && textBox.Text.Length + 1 > len && e.KeyChar != '\b' && e.KeyChar != '\r')
			{
				Program.MsgCustom((string)Program.m_hPubTab["SInputIsOutLen"], MessageBoxIcon.Exclamation);
				e.Handled = true;
			}
		}
		catch
		{
		}
	}

	public static void NumberKeyPress(object sender, KeyPressEventArgs e, int start, long end)
	{
		try
		{
			if (!(sender is TextBox textBox))
			{
				return;
			}
			NumberKeyPress(sender, e, end.ToString().Length);
			if (e.Handled)
			{
				return;
			}
			if (e.KeyChar == '\b' && textBox.SelectionStart == 1 && textBox.TextLength > textBox.SelectionLength)
			{
				textBox.Text = int.Parse(textBox.Text.Substring(1)).ToString();
				textBox.SelectionStart = 0;
				e.Handled = true;
				return;
			}
			if (e.KeyChar == '0' && textBox.SelectionStart == 0 && (start > 0 || textBox.TextLength > textBox.SelectionLength))
			{
				e.Handled = true;
				Program.MsgCustom((string)Program.m_hPubTab["SHeadNumberCannotZero"], MessageBoxIcon.Exclamation);
				return;
			}
			if (textBox.Text == "0" && e.KeyChar != '\b' && e.KeyChar != '\r' && textBox.SelectionLength == 0 && textBox.SelectionStart == 1)
			{
				textBox.Text = e.KeyChar.ToString();
				textBox.SelectionStart = 1;
				e.Handled = true;
				return;
			}
			string mess = string.Format((string)Program.m_hPubTab["SInputIsOutOfLimit"], start, end);
			if (e.KeyChar != '\b' && e.KeyChar != '\r')
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(textBox.Text);
				stringBuilder.Remove(textBox.SelectionStart, textBox.SelectionLength);
				stringBuilder.Insert(textBox.SelectionStart, e.KeyChar);
				long num = long.Parse(stringBuilder.ToString());
				if (num > end)
				{
					e.Handled = true;
					Program.MsgCustom(mess, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(textBox.Text);
				long num2 = long.Parse(stringBuilder2.ToString());
				if (num2 < start)
				{
					Program.MsgCustom(mess, MessageBoxIcon.Exclamation);
				}
			}
		}
		catch
		{
		}
	}

	public static void KeyPress(object sender, KeyPressEventArgs e, int len)
	{
		try
		{
			if (!(sender is TextBox textBox))
			{
				return;
			}
			int length = GetLength(e.KeyChar.ToString());
			int length2 = GetLength(textBox.SelectedText);
			int length3 = GetLength(textBox.Text);
			if (length3 + length - length2 <= len || e.KeyChar == '\b' || e.KeyChar == '\r')
			{
				return;
			}
			if (textBox.Tag == null || textBox.Tag.ToString().Length > 1)
			{
				if (textBox.Tag == null || (DateTime.Now - DateTime.Parse(textBox.Tag.ToString())).Milliseconds > 10)
				{
					textBox.Tag = 1;
					Program.MsgCustom((string)Program.m_hPubTab["SInputIsOutLen"], MessageBoxIcon.Exclamation);
				}
				textBox.Tag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
			}
			e.Handled = true;
		}
		catch
		{
		}
	}

	public static bool IsNumber(string strNumber)
	{
		Regex regex = new Regex("[^0-9.-]");
		Regex regex2 = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
		Regex regex3 = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
		string text = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
		string text2 = "^([-]|[0-9])[0-9]*$";
		Regex regex4 = new Regex("(" + text + ")|(" + text2 + ")");
		if (!regex.IsMatch(strNumber) && !regex2.IsMatch(strNumber) && !regex3.IsMatch(strNumber))
		{
			return regex4.IsMatch(strNumber);
		}
		return false;
	}
}
