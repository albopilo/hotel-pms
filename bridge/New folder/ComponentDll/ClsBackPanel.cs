using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ComponentDll;

public class ClsBackPanel : Panel
{
	public Color _Color1 = default(Color);

	public Color _Color2 = default(Color);

	public float _ColorAngle;

	public Color _BorderColorLeft = default(Color);

	public Color _BorderColorTop = default(Color);

	public Color _BorderColorRight = default(Color);

	public Color _BorderColorBottom = default(Color);

	public bool _Border;

	public int _BorderLW = 1;

	public int _BorderTW = 1;

	public int _BorderRW = 1;

	public int _BorderBW = 1;

	public ButtonBorderStyle _BorderLT;

	public ButtonBorderStyle _BorderTT;

	public ButtonBorderStyle _BorderRT;

	public ButtonBorderStyle _BorderBT;

	public Color Color1
	{
		get
		{
			return _Color1;
		}
		set
		{
			_Color1 = value;
			Invalidate();
		}
	}

	public Color Color2
	{
		get
		{
			return _Color2;
		}
		set
		{
			_Color2 = value;
			Invalidate();
		}
	}

	public float ColorAngle
	{
		get
		{
			return _ColorAngle;
		}
		set
		{
			_ColorAngle = value;
			Invalidate();
		}
	}

	public Color BorderColorLeft
	{
		get
		{
			return _BorderColorLeft;
		}
		set
		{
			_BorderColorLeft = value;
			Invalidate();
		}
	}

	public Color BorderColorTop
	{
		get
		{
			return _BorderColorTop;
		}
		set
		{
			_BorderColorTop = value;
			Invalidate();
		}
	}

	public Color BorderColorRight
	{
		get
		{
			return _BorderColorRight;
		}
		set
		{
			_BorderColorRight = value;
			Invalidate();
		}
	}

	public Color BorderColorBottom
	{
		get
		{
			return _BorderColorBottom;
		}
		set
		{
			_BorderColorBottom = value;
			Invalidate();
		}
	}

	public bool Border
	{
		get
		{
			return _Border;
		}
		set
		{
			_Border = value;
			Invalidate();
		}
	}

	public int BorderLW
	{
		get
		{
			return _BorderLW;
		}
		set
		{
			_BorderLW = value;
			Invalidate();
		}
	}

	public int BorderTW
	{
		get
		{
			return _BorderTW;
		}
		set
		{
			_BorderTW = value;
			Invalidate();
		}
	}

	public int BorderRW
	{
		get
		{
			return _BorderRW;
		}
		set
		{
			_BorderRW = value;
			Invalidate();
		}
	}

	public int BorderBW
	{
		get
		{
			return _BorderBW;
		}
		set
		{
			_BorderBW = value;
			Invalidate();
		}
	}

	public ButtonBorderStyle BorderLT
	{
		get
		{
			return _BorderLT;
		}
		set
		{
			_BorderLT = value;
			Invalidate();
		}
	}

	public ButtonBorderStyle BorderTT
	{
		get
		{
			return _BorderTT;
		}
		set
		{
			_BorderTT = value;
			Invalidate();
		}
	}

	public ButtonBorderStyle BorderRT
	{
		get
		{
			return _BorderRT;
		}
		set
		{
			_BorderRT = value;
			Invalidate();
		}
	}

	public ButtonBorderStyle BorderBT
	{
		get
		{
			return _BorderBT;
		}
		set
		{
			_BorderBT = value;
			Invalidate();
		}
	}

	public ClsBackPanel()
	{
		_Color1 = Color.White;
		_Color2 = Color.Gray;
		_ColorAngle = 90f;
		_BorderColorLeft = Color.Gray;
		_BorderColorTop = Color.Gray;
		_BorderColorRight = Color.Gray;
		_BorderColorBottom = Color.Gray;
		_BorderLT = (_BorderTT = (_BorderRT = (_BorderBT = ButtonBorderStyle.Solid)));
		_Border = false;
		_BorderLW = (_BorderTW = (_BorderRW = (_BorderBW = 1)));
		Invalidate();
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
		Graphics graphics = pevent.Graphics;
		int num = base.Width;
		int num2 = base.Height;
		if (num <= 0)
		{
			num = 1;
		}
		if (num2 <= 0)
		{
			num2 = 1;
		}
		Rectangle rect = new Rectangle(0, 0, num, num2);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, _Color1, _Color2, _ColorAngle);
		graphics.FillRectangle(linearGradientBrush, rect);
		if (Border)
		{
			ControlPaint.DrawBorder(graphics, base.ClientRectangle, _BorderColorLeft, _BorderLW, _BorderLT, _BorderColorTop, _BorderTW, _BorderTT, _BorderColorRight, _BorderRW, _BorderRT, _BorderColorBottom, _BorderBW, _BorderBT);
		}
		linearGradientBrush.Dispose();
	}
}
