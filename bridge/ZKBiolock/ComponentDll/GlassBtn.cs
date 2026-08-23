using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ComponentDll;

[ToolboxItem(true)]
[Description("Raises an event when the user clicks it.")]
[ToolboxBitmap(typeof(GlassBtn))]
[ToolboxItemFilter("System.Windows.Forms")]
public class GlassBtn : Button
{
	private class TransparentControl : Control
	{
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
		}
	}

	private const int FRAME_DISABLED = 0;

	private const int FRAME_PRESSED = 1;

	private const int FRAME_NORMAL = 2;

	private const int FRAME_ANIMATED = 3;

	private const int animationLength = 300;

	private const int framesCount = 10;

	private TmpClass tc = new TmpClass();

	private string _Key;

	private Color backColor;

	private Color innerBorderColor;

	private Color outerBorderColor;

	private Color shineColor;

	private Color glowColor;

	private bool isHovered;

	private bool isFocused;

	private bool isFocusedByKey;

	private bool isKeyDown;

	private Timer timer;

	private IContainer components;

	private bool isMouseDown;

	private Button imageButton;

	private List<Image> frames;

	private int currentFrame;

	private int direction;

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

	[DefaultValue(typeof(Color), "Black")]
	public new virtual Color BackColor
	{
		get
		{
			return backColor;
		}
		set
		{
			if (!backColor.Equals(value))
			{
				backColor = value;
				UseVisualStyleBackColor = false;
				CreateFrames();
				OnBackColorChanged(EventArgs.Empty);
			}
		}
	}

	[DefaultValue(typeof(Color), "White")]
	public new virtual Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	[Description("The inner border color of the control.")]
	[DefaultValue(typeof(Color), "Black")]
	[Category("Appearance")]
	public virtual Color InnerBorderColor
	{
		get
		{
			return innerBorderColor;
		}
		set
		{
			if (innerBorderColor != value)
			{
				innerBorderColor = value;
				CreateFrames();
				if (base.IsHandleCreated)
				{
					Invalidate();
				}
				OnInnerBorderColorChanged(EventArgs.Empty);
			}
		}
	}

	[DefaultValue(typeof(Color), "White")]
	[Description("The outer border color of the control.")]
	[Category("Appearance")]
	public virtual Color OuterBorderColor
	{
		get
		{
			return outerBorderColor;
		}
		set
		{
			if (outerBorderColor != value)
			{
				outerBorderColor = value;
				CreateFrames();
				if (base.IsHandleCreated)
				{
					Invalidate();
				}
				OnOuterBorderColorChanged(EventArgs.Empty);
			}
		}
	}

	[Description("上半部分颜色控制")]
	[DefaultValue(typeof(Color), "White")]
	[Category("Appearance")]
	public virtual Color ShineColor
	{
		get
		{
			return shineColor;
		}
		set
		{
			if (shineColor != value)
			{
				shineColor = value;
				CreateFrames();
				if (base.IsHandleCreated)
				{
					Invalidate();
				}
				OnShineColorChanged(EventArgs.Empty);
			}
		}
	}

	[Description("边框颜色")]
	[Category("Appearance")]
	[DefaultValue(typeof(Color), "255,141,189,255")]
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

	private bool isPressed
	{
		get
		{
			if (!isKeyDown)
			{
				if (isMouseDown)
				{
					return isHovered;
				}
				return false;
			}
			return true;
		}
	}

	[Browsable(false)]
	public PushButtonState State
	{
		get
		{
			if (!base.Enabled)
			{
				return PushButtonState.Disabled;
			}
			if (isPressed)
			{
				return PushButtonState.Pressed;
			}
			if (isHovered)
			{
				return PushButtonState.Hot;
			}
			if (isFocused || base.IsDefault)
			{
				return PushButtonState.Default;
			}
			return PushButtonState.Normal;
		}
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new FlatButtonAppearance FlatAppearance => base.FlatAppearance;

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new FlatStyle FlatStyle
	{
		get
		{
			return base.FlatStyle;
		}
		set
		{
			base.FlatStyle = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new bool UseVisualStyleBackColor
	{
		get
		{
			return base.UseVisualStyleBackColor;
		}
		set
		{
			base.UseVisualStyleBackColor = value;
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

	[Category("Property Changed")]
	[Description("Event raised when the value of the InnerBorderColor property is changed.")]
	public event EventHandler InnerBorderColorChanged;

	[Category("Property Changed")]
	[Description("Event raised when the value of the OuterBorderColor property is changed.")]
	public event EventHandler OuterBorderColorChanged;

	[Description("Event raised when the value of the ShineColor property is changed.")]
	[Category("Property Changed")]
	public event EventHandler ShineColorChanged;

	[Description("Event raised when the value of the GlowColor property is changed.")]
	[Category("Property Changed")]
	public event EventHandler GlowColorChanged;

	public GlassBtn()
	{
		InitializeComponent();
		_Key = "";
		timer.Interval = 30;
		base.BackColor = Color.Transparent;
		BackColor = Color.Black;
		ForeColor = Color.White;
		OuterBorderColor = Color.White;
		InnerBorderColor = Color.Black;
		ShineColor = Color.White;
		GlowColor = Color.FromArgb(-7488001);
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		SetStyle(ControlStyles.Opaque, value: false);
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

	protected virtual void OnShineColorChanged(EventArgs e)
	{
		if (ShineColorChanged != null)
		{
			ShineColorChanged(this, e);
		}
	}

	protected virtual void OnGlowColorChanged(EventArgs e)
	{
		if (GlowColorChanged != null)
		{
			InnerBorderColorChanged(this, e);
		}
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		CreateFrames();
		base.OnSizeChanged(e);
	}

	protected override void OnClick(EventArgs e)
	{
		isKeyDown = (isMouseDown = false);
		base.OnClick(e);
	}

	protected override void OnEnter(EventArgs e)
	{
		isFocused = (isFocusedByKey = true);
		base.OnEnter(e);
	}

	protected override void OnLeave(EventArgs e)
	{
		base.OnLeave(e);
		isFocused = (isFocusedByKey = (isKeyDown = (isMouseDown = false)));
		Invalidate();
	}

	protected override void OnKeyDown(KeyEventArgs kevent)
	{
		if (kevent.KeyCode == Keys.Space)
		{
			isKeyDown = true;
			Invalidate();
		}
		base.OnKeyDown(kevent);
	}

	protected override void OnKeyUp(KeyEventArgs kevent)
	{
		if (isKeyDown && kevent.KeyCode == Keys.Space)
		{
			isKeyDown = false;
			Invalidate();
		}
		base.OnKeyUp(kevent);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (!isMouseDown && e.Button == MouseButtons.Left)
		{
			isMouseDown = true;
			isFocusedByKey = false;
			Invalidate();
		}
		base.OnMouseDown(e);
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		if (isMouseDown)
		{
			isMouseDown = false;
			Invalidate();
		}
		base.OnMouseUp(e);
	}

	protected override void OnMouseMove(MouseEventArgs mevent)
	{
		base.OnMouseMove(mevent);
		if (mevent.Button == MouseButtons.None)
		{
			return;
		}
		if (!base.ClientRectangle.Contains(mevent.X, mevent.Y))
		{
			if (isHovered)
			{
				isHovered = false;
				Invalidate();
			}
		}
		else if (!isHovered)
		{
			isHovered = true;
			Invalidate();
		}
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		isHovered = true;
		FadeIn();
		Invalidate();
		base.OnMouseEnter(e);
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		isHovered = false;
		FadeOut();
		Invalidate();
		base.OnMouseLeave(e);
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		if (tc.ChkKey(_Key))
		{
			DrawButtonBackgroundFromBuffer(pevent.Graphics);
			DrawForegroundFromButton(pevent);
			DrawButtonForeground(pevent.Graphics);
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
		Image result = new Bitmap(clientRectangle.Width, clientRectangle.Height);
		using Graphics graphics = Graphics.FromImage(result);
		graphics.Clear(Color.Transparent);
		DrawButtonBackground(graphics, clientRectangle, pressed, hovered, animating, enabled, outerBorderColor, backColor, glowColor, shineColor, innerBorderColor, glowOpacity);
		return result;
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

	private void DrawButtonForeground(Graphics g)
	{
		if (Focused && ShowFocusCues)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			clientRectangle.Inflate(-4, -4);
			ControlPaint.DrawFocusRectangle(g, clientRectangle);
		}
	}

	private void DrawForegroundFromButton(PaintEventArgs pevent)
	{
		if (imageButton == null)
		{
			imageButton = new Button();
			imageButton.Parent = new TransparentControl();
			imageButton.BackColor = Color.Transparent;
			imageButton.FlatAppearance.BorderSize = 0;
			imageButton.FlatStyle = FlatStyle.Flat;
		}
		imageButton.AutoEllipsis = base.AutoEllipsis;
		if (base.Enabled)
		{
			imageButton.ForeColor = ForeColor;
		}
		else
		{
			imageButton.ForeColor = Color.FromArgb(3 * ForeColor.R + backColor.R >> 2, 3 * ForeColor.G + backColor.G >> 2, 3 * ForeColor.B + backColor.B >> 2);
		}
		imageButton.Font = Font;
		imageButton.RightToLeft = RightToLeft;
		imageButton.Image = base.Image;
		imageButton.ImageAlign = base.ImageAlign;
		imageButton.ImageIndex = base.ImageIndex;
		imageButton.ImageKey = base.ImageKey;
		imageButton.ImageList = base.ImageList;
		imageButton.Padding = base.Padding;
		imageButton.Size = base.Size;
		imageButton.Text = Text;
		imageButton.TextAlign = TextAlign;
		imageButton.TextImageRelation = base.TextImageRelation;
		imageButton.UseCompatibleTextRendering = base.UseCompatibleTextRendering;
		imageButton.UseMnemonic = base.UseMnemonic;
		InvokePaint(imageButton, pevent);
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
		timer.Enabled = true;
	}

	private void FadeOut()
	{
		direction = -1;
		timer.Enabled = true;
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		if (timer.Enabled)
		{
			Refresh();
			currentFrame += direction;
			if (currentFrame == -1)
			{
				currentFrame = 0;
				timer.Enabled = false;
				direction = 0;
			}
			else if (currentFrame == 10)
			{
				currentFrame = 9;
				timer.Enabled = false;
				direction = 0;
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.timer = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.timer.Enabled = true;
		this.timer.Interval = 30;
		this.timer.Tick += new System.EventHandler(timer_Tick);
		base.ResumeLayout(false);
	}
}
