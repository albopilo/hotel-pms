using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ComponentDll;

public class ClsButton : Button
{
	private Color mouseEnterStartColor;

	private Color mouseEnterEndColor;

	private Color mouseDownStartColor;

	private Color mouseDownEndColor;

	private Color currentStartColor;

	private Color currentEndColor;

	protected Color _Color1 = default(Color);

	protected Color _Color2 = default(Color);

	protected float _ColorAngle;

	protected int _ColorType = 2;

	protected bool _Ellipse;

	protected float _EllipseValue;

	protected int _alignment = 1;

	protected int _alignmetnLine = 1;

	protected bool _drawText = true;

	protected int _Image_Padding = 2;

	private string _Key = "Manson20090428";

	private string MyKey = "Manson20090428";

	private string MyDt = "2009-04-28 11:00:00";

	[Description("第一个显示的颜色。")]
	public Color C_Color1
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

	[Description("第二个显示的颜色。")]
	public Color C_Color2
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

	[Description("颜色渐变度")]
	public float C_ColorAngle
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

	[Description("1为不分段显示,2为分三段显示.注:仅处理由上到下.")]
	public int C_ColorType
	{
		get
		{
			return _ColorType;
		}
		set
		{
			_ColorType = value;
		}
	}

	[Description("是否绘制圆角矩形")]
	public bool C_Ellipse
	{
		get
		{
			return _Ellipse;
		}
		set
		{
			_Ellipse = value;
		}
	}

	[Description("圆角值")]
	public float C_EllipseValue
	{
		get
		{
			return _EllipseValue;
		}
		set
		{
			_EllipseValue = value;
		}
	}

	[Description("Image左右上下边距值")]
	public int C_Image_Padding
	{
		get
		{
			return _Image_Padding;
		}
		set
		{
			_Image_Padding = value;
		}
	}

	[Description("文本垂直对齐方式 2左对齐; 1居中对齐; 0右对齐")]
	public int C_Alignment
	{
		get
		{
			return _alignment;
		}
		set
		{
			_alignment = value;
		}
	}

	[Description("文本水平对齐方式 2上对齐; 1居中对齐; 0下对齐")]
	public int C_AlignmentLine
	{
		get
		{
			return _alignmetnLine;
		}
		set
		{
			_alignmetnLine = value;
		}
	}

	[Description("是否绘制文本")]
	public bool C_DrawText
	{
		get
		{
			return _drawText;
		}
		set
		{
			_drawText = value;
		}
	}

	[Category("Appearance")]
	public Color MouseEnterStartColor
	{
		get
		{
			return mouseEnterStartColor;
		}
		set
		{
			mouseEnterStartColor = value;
		}
	}

	[Category("Appearance")]
	public Color MouseEnterEndColor
	{
		get
		{
			return mouseEnterEndColor;
		}
		set
		{
			mouseEnterEndColor = value;
		}
	}

	[Category("Appearance")]
	public Color MouseDownStartColor
	{
		get
		{
			return mouseDownStartColor;
		}
		set
		{
			mouseDownStartColor = value;
		}
	}

	[Category("Appearance")]
	public Color MouseDownEndColor
	{
		get
		{
			return mouseDownEndColor;
		}
		set
		{
			mouseDownEndColor = value;
		}
	}

	public string AutoUsedStr
	{
		set
		{
			_Key = value;
		}
	}

	public ClsButton()
	{
		Text = "";
		base.Height = 24;
		_Color1 = Color.White;
		_Color2 = Color.Silver;
		_ColorAngle = 90f;
		mouseEnterStartColor = Color.FromArgb(255, 240, 197);
		mouseEnterEndColor = Color.FromArgb(255, 213, 152);
		mouseDownStartColor = Color.FromArgb(254, 151, 84);
		mouseDownEndColor = Color.FromArgb(255, 199, 131);
		Invalidate();
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if (_Key == MyKey && DateTime.Now.Year <= 2010 && DateTime.Now.CompareTo(Convert.ToDateTime(MyDt)) >= 0)
		{
			if (_ColorType != 1 && _ColorType != 2)
			{
				_ColorType = 2;
			}
			base.OnPaint(e);
			if (_Ellipse)
			{
				DrawEllipse(e);
			}
			DrawRectangle(e);
		}
	}

	private void DrawText(Graphics g)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		StringFormat stringFormat = new StringFormat();
		switch (_alignment)
		{
		case 0:
			stringFormat.Alignment = StringAlignment.Far;
			break;
		case 1:
			stringFormat.Alignment = StringAlignment.Center;
			break;
		case 2:
			stringFormat.Alignment = StringAlignment.Near;
			break;
		default:
			stringFormat.Alignment = StringAlignment.Center;
			break;
		}
		switch (_alignmetnLine)
		{
		case 0:
			stringFormat.LineAlignment = StringAlignment.Far;
			break;
		case 1:
			stringFormat.LineAlignment = StringAlignment.Center;
			break;
		case 2:
			stringFormat.LineAlignment = StringAlignment.Near;
			break;
		default:
			stringFormat.LineAlignment = StringAlignment.Center;
			break;
		}
		Font font = Font;
		g.DrawString(Text, font, new SolidBrush(Color.FromArgb(255, ForeColor)), clientRectangle, stringFormat);
	}

	private GraphicsPath GetGraphicsPath(Rectangle rect)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		if (rect.Width <= 0)
		{
			rect.Width = 1;
		}
		if (rect.Height <= 0)
		{
			rect.Height = 1;
		}
		graphicsPath.AddArc(rect.Left, rect.Top, rect.Height, rect.Height, 90f, 180f);
		graphicsPath.AddArc(rect.Right - rect.Height, rect.Top, rect.Height, rect.Height, 270f, 180f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	private GraphicsPath GetGraphicsPath1(Rectangle rect)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		if (rect.Width <= 0)
		{
			rect.Width = 1;
		}
		if (rect.Height <= 0)
		{
			rect.Height = 1;
		}
		graphicsPath.AddArc(rect.Left, rect.Top, rect.Height, rect.Height, 90f, 180f);
		graphicsPath.AddArc(rect.Right - rect.Height, rect.Top, rect.Height, rect.Height, 270f, 180f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	private void DrawGaoLiang(Graphics g)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		GraphicsPath graphicsPath = GetGraphicsPath1(clientRectangle);
		RectangleF bounds = graphicsPath.GetBounds();
		bounds.Height++;
		g.FillPath(new LinearGradientBrush(bounds, _Color1, _Color2, _ColorAngle), graphicsPath);
	}

	private void DrawRectangle(PaintEventArgs e)
	{
		if (_ColorType != 1 && _ColorType != 2)
		{
			_ColorType = 2;
		}
		int num2;
		int num = (num2 = 1);
		int num3 = base.Width - 4;
		Graphics graphics = e.Graphics;
		e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
		e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
		LinearGradientBrush linearGradientBrush = null;
		Rectangle rect;
		if (_ColorType == 2)
		{
			int num4;
			if (_Ellipse)
			{
				num = 0;
				num2 = -2;
				num3 = base.Width;
				num4 = base.Height / _ColorType + 4;
			}
			else
			{
				num3 = base.Width - 4;
				num4 = base.Height / _ColorType;
			}
			rect = new Rectangle(num, num2, num3, num4);
			linearGradientBrush = new LinearGradientBrush(rect, _Color1, _Color2, _ColorAngle);
			graphics.FillRectangle(linearGradientBrush, rect);
			if (!_Ellipse)
			{
				num = 1;
				num2 = base.Height / _ColorType;
				num3 = base.Width - 4;
				num4 = base.Height / _ColorType - 2;
			}
			else
			{
				num = 0;
				num2 = base.Height / _ColorType - 1;
				num3 = base.Width;
				num4 = base.Height / _ColorType + 2;
			}
			rect = new Rectangle(num, num2, num3, num4);
			linearGradientBrush = new LinearGradientBrush(rect, _Color2, _Color1, _ColorAngle);
			graphics.FillRectangle(linearGradientBrush, rect);
		}
		else
		{
			int num4;
			if (_Ellipse)
			{
				num = (num2 = 0);
				num3 = base.Width;
				num4 = base.Height;
			}
			else
			{
				num4 = base.Height - 4;
			}
			rect = new Rectangle(num, num2, num3, num4);
			linearGradientBrush = new LinearGradientBrush(rect, _Color1, _Color2, _ColorAngle);
			graphics.FillRectangle(linearGradientBrush, rect);
		}
		if (base.Image != null)
		{
			Rectangle rectangle = rect;
			float num6;
			float num5 = (num6 = 0f);
			switch (base.ImageAlign)
			{
			case ContentAlignment.TopLeft:
				num5 = rectangle.Left + _Image_Padding;
				num6 = rectangle.Top + _Image_Padding;
				break;
			case ContentAlignment.MiddleLeft:
				num5 = rectangle.Left + _Image_Padding;
				num6 = (rectangle.Height - base.Image.Height) / 2;
				break;
			case ContentAlignment.BottomLeft:
				num5 = rectangle.Left + _Image_Padding;
				num6 = rectangle.Height - base.Image.Height - _Image_Padding;
				break;
			case ContentAlignment.TopCenter:
				num5 = (rectangle.Width - base.Image.Width) / 2;
				num6 = rectangle.Top + _Image_Padding;
				break;
			case ContentAlignment.MiddleCenter:
				num5 = (rectangle.Width - base.Image.Width) / 2;
				num6 = (rectangle.Height - base.Image.Height) / 2;
				break;
			case ContentAlignment.BottomCenter:
				num5 = (rectangle.Width - base.Image.Width) / 2;
				num6 = rectangle.Height - base.Image.Height - _Image_Padding;
				break;
			case ContentAlignment.TopRight:
				num5 = rectangle.Width - base.Image.Width - _Image_Padding;
				num6 = rectangle.Top + _Image_Padding;
				break;
			case ContentAlignment.MiddleRight:
				num5 = rectangle.Width - base.Image.Width - _Image_Padding;
				num6 = (rectangle.Height - base.Image.Height) / 2;
				break;
			case ContentAlignment.BottomRight:
				num5 = rectangle.Width - base.Image.Width - _Image_Padding;
				num6 = rectangle.Height - base.Image.Height - _Image_Padding;
				break;
			}
			if (base.Enabled)
			{
				graphics.DrawImage(base.Image, num5, num6, base.Image.Width, base.Image.Height);
			}
		}
		if (_drawText)
		{
			DrawText(graphics);
		}
		linearGradientBrush?.Dispose();
	}

	private void DrawEllipse(PaintEventArgs e)
	{
		e.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, base.Width, base.Height);
		e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
		e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
		Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
		GraphicsPath graphicsPath = GetGraphicsPath(rect);
		e.Graphics.FillPath(new SolidBrush(BackColor), graphicsPath);
		base.Region = new Region(graphicsPath);
		DrawGaoLiang(e.Graphics);
		e.Graphics.DrawPath(new Pen(BackColor, 3f), graphicsPath);
		Pen pen = new Pen(mouseDownEndColor, 5f);
		e.Graphics.DrawRectangle(pen, rect);
		pen.Color = mouseDownEndColor;
		pen.DashStyle = DashStyle.Dot;
		rect.Inflate(-2, -2);
		e.Graphics.DrawRectangle(pen, rect);
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		currentStartColor = mouseEnterStartColor;
		currentEndColor = mouseEnterEndColor;
		Invalidate();
		base.OnMouseEnter(e);
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		currentStartColor = _Color1;
		currentEndColor = _Color2;
		Invalidate();
		base.OnMouseLeave(e);
	}

	protected override void OnMouseDown(MouseEventArgs mevent)
	{
		currentStartColor = mouseDownStartColor;
		currentEndColor = mouseDownEndColor;
		Invalidate();
		base.OnMouseDown(mevent);
	}

	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		currentStartColor = mouseEnterStartColor;
		currentEndColor = mouseEnterEndColor;
		base.OnMouseUp(mevent);
	}
}
