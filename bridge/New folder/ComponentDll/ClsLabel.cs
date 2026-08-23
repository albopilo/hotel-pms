using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ComponentDll;

public class ClsLabel : Label
{
	private const int FRAME_DISABLED = 0;

	private const int FRAME_PRESSED = 1;

	private const int FRAME_NORMAL = 2;

	private const int FRAME_ANIMATED = 3;

	private const int animationLength = 300;

	private const int framesCount = 10;

	private TmpClass tc = new TmpClass();

	private string _Key;

	private bool isEnter;

	private bool isCheck;

	private bool isDown;

	private bool isbtn;

	private bool reDrawImage;

	private bool reDrawText;

	private Color mouseEnterStartColor;

	private Color mouseEnterEndColor;

	private Color mouseDownStartColor;

	private Color mouseDownEndColor;

	private Color defaultColor;

	private Color defaultBorderColor;

	private Color mouseEnterBorderColor;

	private Color mouseDownBorderColor;

	private Color sColor;

	private Color eColor;

	private Color bColor;

	protected int _Image_Padding;

	protected int _ImageStyle;

	protected Image _Image;

	protected string _Text;

	private int _TextImageLocation;

	private Timer timer;

	private IContainer components;

	private static readonly object EventCheckChanged = new object();

	private Color glowColor;

	private List<Image> frames;

	private int currentFrame;

	private int direction;

	private bool isPressed
	{
		get
		{
			if (isDown)
			{
				return isEnter;
			}
			return false;
		}
	}

	[DefaultValue(typeof(Color), "255,141,189,255")]
	[Category("Appearance")]
	[Description("边框颜色")]
	public virtual Color GlowColor
	{
		get
		{
			return glowColor;
		}
		set
		{
			if (glowColor != value)
			{
				glowColor = value;
				CreateFrames();
				if (base.IsHandleCreated)
				{
					Invalidate();
				}
				OnGlowColorChanged(EventArgs.Empty);
			}
		}
	}

	[Description("Key")]
	public string GuidInfo
	{
		get
		{
			return _Key;
		}
		set
		{
			_Key = value;
		}
	}

	[Description("文本与图像关系: 0 - Overlay,1 - ImageAboveText, 2 - TextAboveImage, 3 - ImageBeforeText, 4 - TextBeforeImage")]
	public int TextImageLocation
	{
		get
		{
			return _TextImageLocation;
		}
		set
		{
			_TextImageLocation = value;
		}
	}

	[Category("Appearance")]
	public string TextNew
	{
		get
		{
			return _Text;
		}
		set
		{
			_Text = value;
		}
	}

	[Category("Appearance")]
	public bool TextRedrawed
	{
		get
		{
			return reDrawText;
		}
		set
		{
			reDrawText = value;
		}
	}

	[Category("Appearance")]
	public Image ImageNew
	{
		get
		{
			return _Image;
		}
		set
		{
			_Image = value;
		}
	}

	[Category("Appearance")]
	public int ImageStyle
	{
		get
		{
			return _ImageStyle;
		}
		set
		{
			_ImageStyle = value;
		}
	}

	[Category("Appearance")]
	public bool ImageRedrawed
	{
		get
		{
			return reDrawImage;
		}
		set
		{
			reDrawImage = value;
		}
	}

	[Category("Appearance")]
	public bool isButton
	{
		get
		{
			return isbtn;
		}
		set
		{
			isbtn = value;
		}
	}

	[Category("Appearance")]
	public Color DefaultColor
	{
		get
		{
			return defaultColor;
		}
		set
		{
			defaultColor = value;
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

	[Category("Appearance")]
	public Color MouseDownBorderColor
	{
		get
		{
			return mouseDownBorderColor;
		}
		set
		{
			mouseDownBorderColor = value;
		}
	}

	[Category("Appearance")]
	public bool Checked
	{
		get
		{
			return isCheck;
		}
		set
		{
			isCheck = value;
			Invalidate();
			CheckChangedEventArgs args = new CheckChangedEventArgs(isCheck);
			OnCheckChanged(args);
		}
	}

	private bool HasAnimationFrames
	{
		get
		{
			if (frames != null)
			{
				return frames.Count > 3;
			}
			return false;
		}
	}

	private bool isAnimating => direction != 0;

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

	[Category("Property Changed")]
	[Description("Event raised when the value of the GlowColor property is changed.")]
	public event EventHandler GlowColorChanged;

	[Category("Property Changed")]
	[Description("Event raised when the value of the InnerBorderColor property is changed.")]
	public event EventHandler InnerBorderColorChanged;

	[Description("Event raised when the value of the OuterBorderColor property is changed.")]
	[Category("Property Changed")]
	public event EventHandler OuterBorderColorChanged;

	public ClsLabel()
	{
		if (timer == null)
		{
			timer = new Timer();
		}
		GlowColor = Color.FromArgb(-7488001);
		timer.Interval = 30;
		_Key = "";
		isEnter = false;
		isCheck = false;
		defaultBorderColor = Color.Transparent;
		defaultColor = Color.Transparent;
		mouseEnterStartColor = Color.White;
		mouseEnterEndColor = Color.FromArgb(164, 254, 85);
		mouseEnterBorderColor = Color.FromArgb(37, 199, 0);
		mouseDownStartColor = Color.White;
		mouseDownEndColor = Color.FromArgb(179, 210, 254);
		mouseDownBorderColor = Color.SteelBlue;
		sColor = defaultColor;
		eColor = defaultColor;
		bColor = defaultColor;
		_Image_Padding = 4;
		_ImageStyle = 0;
		AutoSize = false;
		reDrawImage = true;
		isbtn = true;
		_Text = base.Name.ToString();
		Text = "";
		_TextImageLocation = 0;
		Invalidate();
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	protected virtual void OnCheckChanged(CheckChangedEventArgs args)
	{
		if (base.Events[EventCheckChanged] is EventHandler<CheckChangedEventArgs> eventHandler)
		{
			eventHandler(this, args);
		}
		if (isEnter)
		{
			OnMouseEnter(null);
		}
		else
		{
			OnMouseLeave(null);
		}
	}

	protected virtual void OnGlowColorChanged(EventArgs e)
	{
		if (GlowColorChanged != null)
		{
			InnerBorderColorChanged(this, e);
		}
	}

	protected virtual void OnInnerBorderColorChanged(EventArgs e)
	{
		if (InnerBorderColorChanged != null)
		{
			InnerBorderColorChanged(this, e);
		}
	}

	protected virtual void OnOuterBorderColorChanged(EventArgs e)
	{
		if (OuterBorderColorChanged != null)
		{
			OuterBorderColorChanged(this, e);
		}
	}

	private static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		int left = rectangle.Left;
		int top = rectangle.Top;
		int num = rectangle.Width;
		int num2 = rectangle.Height;
		int num3 = radius << 1;
		graphicsPath.AddArc(left, top, num3, num3, 180f, 90f);
		graphicsPath.AddLine(left + radius, top, left + num - radius, top);
		graphicsPath.AddArc(left + num - num3, top, num3, num3, 270f, 90f);
		graphicsPath.AddLine(left + num, top + radius, left + num, top + num2 - radius);
		graphicsPath.AddArc(left + num - num3, top + num2 - num3, num3, num3, 0f, 90f);
		graphicsPath.AddLine(left + num - radius, top + num2, left + radius, top + num2);
		graphicsPath.AddArc(left, top + num2 - num3, num3, num3, 90f, 90f);
		graphicsPath.AddLine(left, top + num2 - radius, left, top + radius);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	private static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		int left = rectangle.Left;
		int top = rectangle.Top;
		int num = rectangle.Width;
		int num2 = rectangle.Height;
		int num3 = radius << 1;
		graphicsPath.AddArc(left, top, num3, num3, 180f, 90f);
		graphicsPath.AddLine(left + radius, top, left + num - radius, top);
		graphicsPath.AddArc(left + num - num3, top, num3, num3, 270f, 90f);
		graphicsPath.AddLine(left + num, top + radius, left + num, top + num2);
		graphicsPath.AddLine(left + num, top + num2, left, top + num2);
		graphicsPath.AddLine(left, top + num2, left, top + radius);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	private static GraphicsPath CreateBottomRadialPath(Rectangle rectangle)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		RectangleF rect = rectangle;
		rect.X -= rect.Width * 0.35f;
		rect.Y -= rect.Height * 0.15f;
		rect.Width *= 1.7f;
		rect.Height *= 2.3f;
		graphicsPath.AddEllipse(rect);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	private void DrawRectangle(PaintEventArgs pevent, Color sColor, Color eColor, Color bColor, float glowOpacity)
	{
		Color baseColor = Color.FromArgb(-7488001);
		Graphics graphics = pevent.Graphics;
		SmoothingMode smoothingMode = graphics.SmoothingMode;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.Width--;
		clientRectangle.Height--;
		using (GraphicsPath path = CreateRoundRectangle(clientRectangle, 4))
		{
			using Pen pen = new Pen(bColor);
			graphics.DrawPath(pen, path);
		}
		clientRectangle.X++;
		clientRectangle.Y++;
		clientRectangle.Width -= 2;
		clientRectangle.Height -= 2;
		Rectangle rectangle = clientRectangle;
		rectangle.Height >>= 1;
		using (GraphicsPath path2 = CreateRoundRectangle(clientRectangle, 2))
		{
			int alpha = (isDown ? 95 : 127);
			using Brush brush = new SolidBrush(Color.FromArgb(alpha, eColor));
			graphics.FillPath(brush, path2);
		}
		if (isEnter && !isDown)
		{
			using GraphicsPath path3 = CreateRoundRectangle(clientRectangle, 2);
			graphics.SetClip(path3, CombineMode.Intersect);
			using (GraphicsPath graphicsPath = CreateBottomRadialPath(clientRectangle))
			{
				using PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
				int alpha2 = (int)(178f * glowOpacity + 0.5f);
				RectangleF bounds = graphicsPath.GetBounds();
				pathGradientBrush.CenterPoint = new PointF((bounds.Left + bounds.Right) / 2f, (bounds.Top + bounds.Bottom) / 2f);
				pathGradientBrush.CenterColor = Color.FromArgb(alpha2, baseColor);
				pathGradientBrush.SurroundColors = new Color[1] { Color.FromArgb(0, baseColor) };
				graphics.FillPath(pathGradientBrush, graphicsPath);
			}
			graphics.ResetClip();
		}
		if (rectangle.Width > 0 && rectangle.Height > 0)
		{
			rectangle.Height++;
			using (GraphicsPath path4 = CreateTopRoundRectangle(rectangle, 2))
			{
				rectangle.Height++;
				int num = 153;
				if (isDown | !base.Enabled)
				{
					num = (int)(0.5f * (float)num + 0.5f);
				}
				using LinearGradientBrush brush2 = new LinearGradientBrush(rectangle, Color.FromArgb(num, sColor), Color.FromArgb(num / 3, sColor), LinearGradientMode.Vertical);
				graphics.FillPath(brush2, path4);
			}
			rectangle.Height -= 2;
		}
		using (GraphicsPath path5 = CreateRoundRectangle(clientRectangle, 3))
		{
			using Pen pen2 = new Pen(Color.Transparent);
			graphics.DrawPath(pen2, path5);
		}
		graphics.SmoothingMode = smoothingMode;
	}

	private void SetTextAlign(StringFormat format)
	{
		string text = TextAlign.ToString("G");
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

	private void DrawBorder(Graphics g, Color color, Color bordercolor, int x, int y)
	{
		SolidBrush brush = new SolidBrush(color);
		Pen pen = new Pen(brush, 1f);
		BorderStyle = BorderStyle.None;
		BackColor = color;
		pen.Color = Color.White;
		Rectangle rectangle = new Rectangle(0, 0, x, y);
		ControlPaint.DrawBorder(g, rectangle, bordercolor, ButtonBorderStyle.Solid);
		DrawImage(g, rectangle);
	}

	private static void DrawButtonBackground(Graphics g, Rectangle rectangle, bool pressed, bool hovered, bool animating, bool enabled, Color outerBorderColor, Color backColor, Color glowColor, Color shineColor, Color innerBorderColor, float glowOpacity)
	{
		SmoothingMode smoothingMode = g.SmoothingMode;
		g.SmoothingMode = SmoothingMode.AntiAlias;
		Rectangle rectangle2 = rectangle;
		rectangle2.Width--;
		rectangle2.Height--;
		using (GraphicsPath path = CreateRoundRectangle(rectangle2, 4))
		{
			using Pen pen = new Pen(outerBorderColor);
			g.DrawPath(pen, path);
		}
		rectangle2.X++;
		rectangle2.Y++;
		rectangle2.Width -= 2;
		rectangle2.Height -= 2;
		Rectangle rectangle3 = rectangle2;
		rectangle3.Height >>= 1;
		using (GraphicsPath path2 = CreateRoundRectangle(rectangle2, 2))
		{
			int alpha = (pressed ? 204 : 127);
			using Brush brush = new SolidBrush(Color.FromArgb(alpha, backColor));
			g.FillPath(brush, path2);
		}
		if ((hovered || animating) && !pressed)
		{
			using GraphicsPath path3 = CreateRoundRectangle(rectangle2, 2);
			g.SetClip(path3, CombineMode.Intersect);
			using (GraphicsPath graphicsPath = CreateBottomRadialPath(rectangle2))
			{
				using PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
				int alpha2 = (int)(178f * glowOpacity + 0.5f);
				RectangleF bounds = graphicsPath.GetBounds();
				pathGradientBrush.CenterPoint = new PointF((bounds.Left + bounds.Right) / 2f, (bounds.Top + bounds.Bottom) / 2f);
				pathGradientBrush.CenterColor = Color.FromArgb(alpha2, glowColor);
				pathGradientBrush.SurroundColors = new Color[1] { Color.FromArgb(0, glowColor) };
				g.FillPath(pathGradientBrush, graphicsPath);
			}
			g.ResetClip();
		}
		if (rectangle3.Width > 0 && rectangle3.Height > 0)
		{
			rectangle3.Height++;
			using (GraphicsPath path4 = CreateTopRoundRectangle(rectangle3, 2))
			{
				rectangle3.Height++;
				int num = 153;
				if (pressed | !enabled)
				{
					num = (int)(0.4f * (float)num + 0.5f);
				}
				using LinearGradientBrush brush2 = new LinearGradientBrush(rectangle3, Color.FromArgb(num, shineColor), Color.FromArgb(num / 3, shineColor), LinearGradientMode.Vertical);
				g.FillPath(brush2, path4);
			}
			rectangle3.Height -= 2;
		}
		using (GraphicsPath path5 = CreateRoundRectangle(rectangle2, 3))
		{
			using Pen pen2 = new Pen(innerBorderColor);
			g.DrawPath(pen2, path5);
		}
		g.SmoothingMode = smoothingMode;
	}

	public Image CreateBackgroundFrame(bool pressed, bool hovered, bool animating, bool enabled, float glowOpacity)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		if (clientRectangle.Width <= 0)
		{
			clientRectangle.Width = 1;
		}
		if (clientRectangle.Height <= 0)
		{
			clientRectangle.Height = 1;
		}
		Image image = new Bitmap(clientRectangle.Width, clientRectangle.Height);
		using Graphics graphics = Graphics.FromImage(image);
		graphics.Clear(Color.Transparent);
		DrawButtonBackground(graphics, clientRectangle, pressed, hovered, animating, enabled, bColor, sColor, glowColor, bColor, bColor, glowOpacity);
		return image;
	}

	private void CreateFrames()
	{
		CreateFrames(withAnimationFrames: false);
	}

	private void CreateFrames(bool withAnimationFrames)
	{
		DestroyFrames();
		if (!base.IsHandleCreated)
		{
			return;
		}
		if (frames == null)
		{
			frames = new List<Image>();
		}
		frames.Add(CreateBackgroundFrame(pressed: false, hovered: false, animating: false, enabled: false, 0f));
		frames.Add(CreateBackgroundFrame(pressed: true, hovered: true, animating: false, enabled: true, 0f));
		frames.Add(CreateBackgroundFrame(pressed: false, hovered: false, animating: false, enabled: true, 0f));
		if (withAnimationFrames)
		{
			for (int i = 0; i < 10; i++)
			{
				frames.Add(CreateBackgroundFrame(pressed: false, hovered: true, animating: true, enabled: true, (float)i / 9f));
			}
		}
	}

	private void DestroyFrames()
	{
		if (frames != null)
		{
			while (frames.Count > 0)
			{
				frames[frames.Count - 1].Dispose();
				frames.RemoveAt(frames.Count - 1);
			}
		}
	}

	private void FadeIn()
	{
		direction = 1;
		timer.Enabled = false;
	}

	private void FadeOut()
	{
		direction = -1;
		timer.Enabled = false;
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		if (timer.Enabled)
		{
			currentFrame += direction;
			if (currentFrame == -1)
			{
				currentFrame = 0;
				timer.Enabled = false;
				direction = 0;
				Refresh();
			}
			else if (currentFrame == 10)
			{
				currentFrame = 9;
				timer.Enabled = false;
				direction = 0;
				Refresh();
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.timer = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.timer.Interval = 2000;
		this.timer.Tick += new System.EventHandler(timer_Tick);
		base.ResumeLayout(false);
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		if (isbtn)
		{
			FadeIn();
			isEnter = true;
			sColor = mouseEnterStartColor;
			eColor = mouseEnterEndColor;
			bColor = mouseEnterBorderColor;
			if (isCheck)
			{
				sColor = mouseDownStartColor;
				eColor = mouseDownEndColor;
				bColor = mouseDownBorderColor;
			}
			Invalidate();
			base.OnMouseEnter(e);
		}
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		if (isbtn)
		{
			FadeOut();
			isEnter = false;
			eColor = (sColor = defaultColor);
			bColor = defaultBorderColor;
			Invalidate();
			base.OnMouseLeave(e);
		}
	}

	protected override void OnMouseDown(MouseEventArgs mevent)
	{
		if (isbtn)
		{
			isDown = true;
			sColor = mouseDownStartColor;
			eColor = mouseDownEndColor;
			bColor = mouseDownBorderColor;
			Invalidate();
			base.OnMouseDown(mevent);
		}
	}

	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		if (isbtn)
		{
			isDown = false;
			eColor = (sColor = defaultColor);
			bColor = defaultBorderColor;
			if (isEnter)
			{
				sColor = mouseEnterStartColor;
				eColor = mouseEnterEndColor;
				bColor = mouseEnterBorderColor;
			}
			Invalidate();
			base.OnMouseUp(mevent);
		}
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		if (!base.Enabled)
		{
			isCheck = false;
		}
		Invalidate();
		base.OnEnabledChanged(e);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if (tc.ChkKey(_Key))
		{
			base.OnPaint(e);
			if (!base.Enabled)
			{
				isCheck = false;
			}
			if (isCheck)
			{
				sColor = mouseDownStartColor;
				eColor = mouseDownEndColor;
				bColor = mouseDownBorderColor;
			}
			else if (!isEnter && !isDown)
			{
				eColor = (sColor = defaultColor);
			}
			if (!reDrawImage)
			{
				DrawImage(e.Graphics, base.ClientRectangle);
			}
			if (!reDrawText)
			{
				DrawText(e.Graphics, base.ClientRectangle);
			}
			if (!isEnter && !isCheck)
			{
				DrawBorder(e.Graphics, sColor, bColor, base.Width, base.Height);
			}
			else
			{
				DrawRectangle(e, sColor, eColor, bColor, 0f);
			}
			if (reDrawImage)
			{
				DrawImage(e.Graphics, base.ClientRectangle);
			}
			if (reDrawText)
			{
				DrawText(e.Graphics, base.ClientRectangle);
			}
		}
	}

	private void DrawButtonBackgroundFromBuffer(Graphics graphics)
	{
		int index;
		if (!base.Enabled)
		{
			index = 0;
		}
		else if (isPressed)
		{
			index = 1;
		}
		else if (!isAnimating && currentFrame == 0)
		{
			index = 2;
		}
		else
		{
			if (!HasAnimationFrames)
			{
				CreateFrames(withAnimationFrames: true);
			}
			index = 3 + currentFrame;
		}
		if (frames == null)
		{
			CreateFrames();
		}
		graphics.DrawImage(frames[index], Point.Empty);
	}

	private void DrawImage(Graphics g, Rectangle rect)
	{
		if (_Image != null)
		{
			float num2;
			float num3;
			float num = (num2 = (num3 = 0f));
			num = _Image.Width;
			float num4 = _Image.Height;
			if (_ImageStyle != 0)
			{
				num *= (float)rect.Width / num;
				num4 *= (float)rect.Height / num4;
			}
			GetTextSize(g, _Text);
			switch (base.ImageAlign)
			{
			case ContentAlignment.TopLeft:
				num2 = rect.Left + _Image_Padding - 2;
				num3 = rect.Top + _Image_Padding - 2;
				break;
			case ContentAlignment.MiddleLeft:
				num2 = rect.Left + _Image_Padding - 2;
				num3 = ((float)rect.Height - num4) / 2f - 0.5f;
				break;
			case ContentAlignment.BottomLeft:
				num2 = rect.Left + _Image_Padding - 2;
				num3 = (float)rect.Height - num4 - (float)_Image_Padding;
				break;
			case ContentAlignment.TopCenter:
				num2 = ((float)rect.Width - num) / 2f;
				num3 = rect.Top + _Image_Padding - 2;
				break;
			case ContentAlignment.MiddleCenter:
				num2 = ((float)rect.Width - num) / 2f;
				num3 = ((float)rect.Height - num4) / 2f - 0.5f;
				break;
			case ContentAlignment.BottomCenter:
				num2 = ((float)rect.Width - num) / 2f;
				num3 = (float)rect.Height - num4 - (float)_Image_Padding;
				break;
			case ContentAlignment.TopRight:
				num2 = (float)rect.Width - num - (float)_Image_Padding;
				num3 = rect.Top + _Image_Padding - 2;
				break;
			case ContentAlignment.MiddleRight:
				num2 = (float)rect.Width - num - (float)_Image_Padding;
				num3 = ((float)rect.Height - num4) / 2f - 0.5f;
				break;
			case ContentAlignment.BottomRight:
				num2 = (float)rect.Width - num - (float)_Image_Padding;
				num3 = (float)rect.Height - num4 - (float)_Image_Padding;
				break;
			}
			if (base.Enabled)
			{
				base.Image = null;
				g.DrawImage(_Image, num2, num3, num, num4);
			}
			else
			{
				base.Image = _Image;
			}
		}
	}

	private void DrawText(Graphics g, Rectangle rect)
	{
		if (!(_Text != ""))
		{
			return;
		}
		StringFormat stringFormat = new StringFormat();
		SetTextAlign(stringFormat);
		stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
		using Brush brush = new SolidBrush(ForeColor);
		if (rect.Width > 4 && rect.Height > 2)
		{
			rect.Inflate(-2, -2);
			g.DrawString(_Text, Font, base.Enabled ? brush : Brushes.Gray, rect, stringFormat);
		}
	}

	private SizeF GetTextSize(Graphics g, string txt)
	{
		return g.MeasureString(txt, Font);
	}

	private PointF GetImgLocation(SizeF txtSize)
	{
		return default(PointF);
	}
}
