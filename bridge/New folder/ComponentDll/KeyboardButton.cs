using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ComponentDll;

public class KeyboardButton : Button
{
	private Color defaultStartColor;

	private Color defaultEndColor;

	private Color mouseEnterStartColor;

	private Color mouseEnterEndColor;

	private Color mouseDownStartColor;

	private Color mouseDownEndColor;

	private Color defaultBorderColor;

	private Color mouseEnterBorderColor;

	private Color currentStartColor;

	private Color currentEndColor;

	private Color currentBorderColor;

	private bool antialias;

	private bool isChecked;

	private bool showFocusRectangle;

	private short vkCode;

	protected float _ColorAngle;

	protected int _ColorType = 2;

	protected bool _Ellipse;

	protected float _EllipseValue;

	protected int _Image_Padding = 2;

	private static readonly object EventCheckChanged = new object();

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

	[Category("Appearance")]
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

	[Category("Data")]
	public short VKCode
	{
		get
		{
			return vkCode;
		}
		set
		{
			vkCode = value;
		}
	}

	[Category("Appearance")]
	public Color DefaultStartColor
	{
		get
		{
			return defaultStartColor;
		}
		set
		{
			defaultStartColor = value;
		}
	}

	[Category("Appearance")]
	public Color DefautEndColor
	{
		get
		{
			return defaultEndColor;
		}
		set
		{
			defaultEndColor = value;
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

	[Category("Appearance")]
	public Color DefaultBorderColor
	{
		get
		{
			return defaultBorderColor;
		}
		set
		{
			defaultBorderColor = value;
		}
	}

	[Category("Appearance")]
	public Color MouseEnterBorderColor
	{
		get
		{
			return mouseEnterBorderColor;
		}
		set
		{
			mouseEnterBorderColor = value;
		}
	}

	[Browsable(false)]
	public Color CurrentBorderColor => currentBorderColor;

	[Category("Appearance")]
	public bool AntiAlias
	{
		get
		{
			return antialias;
		}
		set
		{
			antialias = value;
			Invalidate();
		}
	}

	[Category("Appearance")]
	public bool ShowFocusRectangle
	{
		get
		{
			return showFocusRectangle;
		}
		set
		{
			showFocusRectangle = value;
		}
	}

	[Category("Appearance")]
	public bool Checked
	{
		get
		{
			return isChecked;
		}
		set
		{
			isChecked = value;
			Invalidate();
			CheckChangedEventArgs args = new CheckChangedEventArgs(isChecked);
			OnCheckChanged(args);
		}
	}

	public event EventHandler<CheckChangedEventArgs> CheckChanged
	{
		add
		{
			base.Events.AddHandler(EventCheckChanged, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventCheckChanged, value);
		}
	}

	public KeyboardButton()
	{
		base.Size = new Size(107, 31);
		base.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
		defaultStartColor = Color.FromArgb(255, 255, 255);
		defaultEndColor = Color.Silver;
		mouseEnterStartColor = Color.White;
		mouseEnterEndColor = Color.FromArgb(224, 224, 224);
		mouseDownStartColor = Color.FromArgb(224, 224, 224);
		mouseDownEndColor = Color.White;
		defaultBorderColor = Color.FromArgb(59, 97, 156);
		mouseEnterBorderColor = Color.Gray;
		currentStartColor = defaultStartColor;
		currentEndColor = defaultEndColor;
		currentBorderColor = defaultBorderColor;
		antialias = true;
		isChecked = false;
		showFocusRectangle = false;
		Invalidate();
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		LifeButton_Paint(null, e);
	}

	protected virtual void OnCheckChanged(CheckChangedEventArgs args)
	{
		if (base.Events[EventCheckChanged] is EventHandler<CheckChangedEventArgs> eventHandler)
		{
			eventHandler(this, args);
		}
	}

	private void LifeButton_Paint(object sender, PaintEventArgs pevent)
	{
		if (base.ClientSize.Width <= 3 || base.ClientSize.Height <= 3)
		{
			return;
		}
		if (_ColorType != 1 && _ColorType != 2)
		{
			_ColorType = 2;
		}
		_ = base.Width;
		Graphics graphics = pevent.Graphics;
		if (antialias)
		{
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		}
		Rectangle rectangle;
		if (_ColorType == 2)
		{
			rectangle = new Rectangle(0, 0, base.ClientSize.Width - 1, base.ClientSize.Height / 2 + 3);
			Brush brush = (isChecked ? new LinearGradientBrush(rectangle, mouseDownStartColor, mouseDownEndColor, LinearGradientMode.Vertical) : new LinearGradientBrush(rectangle, currentStartColor, currentEndColor, LinearGradientMode.Vertical));
			graphics.FillRectangle(brush, rectangle);
			rectangle = new Rectangle(0, base.ClientSize.Height / 2 - 1, base.ClientSize.Width - 1, base.ClientSize.Height / 2 + 1);
			brush = (isChecked ? new LinearGradientBrush(rectangle, mouseDownEndColor, mouseDownStartColor, LinearGradientMode.Vertical) : new LinearGradientBrush(rectangle, currentEndColor, currentStartColor, LinearGradientMode.Vertical));
			graphics.FillRectangle(brush, rectangle);
			brush.Dispose();
		}
		else
		{
			rectangle = new Rectangle(0, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
			Brush brush2 = (isChecked ? new LinearGradientBrush(base.ClientRectangle, mouseDownStartColor, mouseDownEndColor, LinearGradientMode.Vertical) : new LinearGradientBrush(base.ClientRectangle, currentStartColor, currentEndColor, LinearGradientMode.Vertical));
			graphics.FillRectangle(brush2, base.ClientRectangle);
			brush2.Dispose();
		}
		rectangle = new Rectangle(0, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
		if (BackgroundImage != null)
		{
			if (base.Enabled)
			{
				graphics.DrawImage(base.BackgroundImage, rectangle);
			}
			else
			{
				Image image = ImageProcessHelper.CreateDisabledImage(base.BackgroundImage);
				graphics.DrawImage(image, rectangle);
				image.Dispose();
			}
		}
		if (base.Image != null)
		{
			float num2;
			float num = (num2 = 0f);
			switch (base.ImageAlign)
			{
			case ContentAlignment.TopLeft:
				num = rectangle.Left + _Image_Padding;
				num2 = rectangle.Top + _Image_Padding;
				break;
			case ContentAlignment.MiddleLeft:
				num = rectangle.Left + _Image_Padding;
				num2 = (rectangle.Height - base.Image.Height) / 2;
				break;
			case ContentAlignment.BottomLeft:
				num = rectangle.Left + _Image_Padding;
				num2 = rectangle.Height - base.Image.Height - _Image_Padding;
				break;
			case ContentAlignment.TopCenter:
				num = (rectangle.Width - base.Image.Width) / 2;
				num2 = rectangle.Top + _Image_Padding;
				break;
			case ContentAlignment.MiddleCenter:
				num = (rectangle.Width - base.Image.Width) / 2;
				num2 = (rectangle.Height - base.Image.Height) / 2;
				break;
			case ContentAlignment.BottomCenter:
				num = (rectangle.Width - base.Image.Width) / 2;
				num2 = rectangle.Height - base.Image.Height - _Image_Padding;
				break;
			case ContentAlignment.TopRight:
				num = rectangle.Width - base.Image.Width - _Image_Padding;
				num2 = rectangle.Top + _Image_Padding;
				break;
			case ContentAlignment.MiddleRight:
				num = rectangle.Width - base.Image.Width - _Image_Padding;
				num2 = (rectangle.Height - base.Image.Height) / 2;
				break;
			case ContentAlignment.BottomRight:
				num = rectangle.Width - base.Image.Width - _Image_Padding;
				num2 = rectangle.Height - base.Image.Height - _Image_Padding;
				break;
			}
			if (base.Enabled)
			{
				graphics.DrawImage(base.Image, num, num2, base.Image.Width, base.Image.Height);
			}
		}
		using (Pen pen = new Pen(isChecked ? mouseEnterBorderColor : currentBorderColor, 1f))
		{
			graphics.DrawRectangle(pen, rectangle);
			if (base.Focused && showFocusRectangle)
			{
				pen.Color = defaultBorderColor;
				pen.DashStyle = DashStyle.Dot;
				rectangle.Inflate(-2, -2);
				graphics.DrawRectangle(pen, rectangle);
			}
		}
		StringFormat stringFormat = new StringFormat();
		SetTextAlign(stringFormat);
		stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
		using Brush brush3 = new SolidBrush(base.ForeColor);
		if (rectangle.Width > 4 && rectangle.Height > 2)
		{
			rectangle.Inflate(-4, -2);
			graphics.DrawString(base.Text, base.Font, base.Enabled ? brush3 : Brushes.Gray, rectangle, stringFormat);
		}
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		currentStartColor = mouseEnterStartColor;
		currentEndColor = mouseEnterEndColor;
		currentBorderColor = mouseEnterBorderColor;
		Invalidate();
		base.OnMouseEnter(e);
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		currentStartColor = defaultStartColor;
		currentEndColor = defaultEndColor;
		currentBorderColor = defaultBorderColor;
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

	private void SetTextAlign(StringFormat format)
	{
		string text = base.TextAlign.ToString("G");
		if (text.IndexOf("Right") >= 0)
		{
			format.Alignment = StringAlignment.Far;
		}
		else if (text.IndexOf("Center") >= 0)
		{
			format.Alignment = StringAlignment.Center;
		}
		else if (text.IndexOf("Left") >= 0)
		{
			format.Alignment = StringAlignment.Near;
		}
		if (text.IndexOf("Bottom") >= 0)
		{
			format.LineAlignment = StringAlignment.Far;
		}
		else if (text.IndexOf("Middle") >= 0)
		{
			format.LineAlignment = StringAlignment.Center;
		}
		else if (text.IndexOf("Top") >= 0)
		{
			format.LineAlignment = StringAlignment.Near;
		}
	}
}
