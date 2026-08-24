using System.Drawing;

namespace Sayes.Controls.Vista.Config;

internal class Color
{
	private System.Drawing.Color _BackColor = System.Drawing.Color.WhiteSmoke;

	private System.Drawing.Color _ActiveBorderColor = System.Drawing.Color.DimGray;

	private System.Drawing.Color _BorderColor = System.Drawing.Color.Gray;

	private System.Drawing.Color _ActiveHeaderColor = System.Drawing.Color.FromArgb(120, System.Drawing.Color.Gray);

	private System.Drawing.Color _HeaderColor = System.Drawing.Color.FromArgb(120, System.Drawing.Color.LightGray);

	private System.Drawing.Color _LinearColor1 = System.Drawing.Color.FromArgb(100, System.Drawing.Color.LightGray);

	private System.Drawing.Color _LinearColor2 = System.Drawing.Color.FromArgb(100, System.Drawing.Color.White);

	public System.Drawing.Color BackColor
	{
		get
		{
			return _BackColor;
		}
		set
		{
			_BackColor = value;
		}
	}

	public System.Drawing.Color ActiveBorderColor
	{
		get
		{
			return _ActiveBorderColor;
		}
		set
		{
			_ActiveBorderColor = value;
		}
	}

	public System.Drawing.Color BorderColor
	{
		get
		{
			return _BorderColor;
		}
		set
		{
			_BorderColor = value;
		}
	}

	public System.Drawing.Color ActiveHeaderColor
	{
		get
		{
			return _ActiveHeaderColor;
		}
		set
		{
			_ActiveHeaderColor = value;
		}
	}

	public System.Drawing.Color HeaderColor
	{
		get
		{
			return _HeaderColor;
		}
		set
		{
			_HeaderColor = value;
		}
	}

	public System.Drawing.Color LinearColor1
	{
		get
		{
			return _LinearColor1;
		}
		set
		{
			_LinearColor1 = value;
		}
	}

	public System.Drawing.Color LinearColor2
	{
		get
		{
			return _LinearColor2;
		}
		set
		{
			_LinearColor2 = value;
		}
	}
}
