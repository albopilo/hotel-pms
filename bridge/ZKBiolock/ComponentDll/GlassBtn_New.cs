using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ComponentDll;

[DefaultEvent("Click")]
public class GlassBtn_New : UserControl
{
	private enum State
	{
		None,
		Hover,
		Pressed
	}

	public enum Style
	{
		Default,
		Flat
	}

	private TmpClass tc = new TmpClass();

	private string _Key;

	private Container components;

	private bool calledbykey;

	private State mButtonState;

	private Timer mFadeIn = new Timer();

	private Timer mFadeOut = new Timer();

	private int mGlowAlpha;

	private string mText;

	private Color mForeColor = Color.White;

	private ContentAlignment mTextAlign = ContentAlignment.MiddleCenter;

	private Image mImage;

	private ContentAlignment mImageAlign = ContentAlignment.MiddleLeft;

	private Size mImageSize = new Size(24, 24);

	private Style mButtonStyle;

	private int mCornerRadius = 8;

	private Color mHighlightColor = Color.White;

	private Color mButtonColor = Color.Black;

	private Color mGlowColor = Color.FromArgb(141, 189, 255);

	private Image mBackImage;

	private Color mBaseColor = Color.Black;

	[Category("Text")]
	[Description("The text that is displayed on the button.")]
	public string ButtonText
	{
		get
		{
			return mText;
		}
		set
		{
			mText = value;
			Invalidate();
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

	[DefaultValue(typeof(Color), "White")]
	[Description("The color with which the text is drawn.")]
	[Category("Text")]
	[Browsable(true)]
	public override Color ForeColor
	{
		get
		{
			return mForeColor;
		}
		set
		{
			mForeColor = value;
			Invalidate();
		}
	}

	[DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
	[Description("The alignment of the button text that is displayed on the control.")]
	[Category("Text")]
	public ContentAlignment TextAlign
	{
		get
		{
			return mTextAlign;
		}
		set
		{
			mTextAlign = value;
			Invalidate();
		}
	}

	[Category("Image")]
	[DefaultValue(null)]
	[Description("The image displayed on the button that is used to help the user identifyit's function if the text is ambiguous.")]
	public Image Image
	{
		get
		{
			return mImage;
		}
		set
		{
			mImage = value;
			Invalidate();
		}
	}

	[DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
	[Category("Image")]
	[Description("The alignment of the image in relation to the button.")]
	public ContentAlignment ImageAlign
	{
		get
		{
			return mImageAlign;
		}
		set
		{
			mImageAlign = value;
			Invalidate();
		}
	}

	[Category("Image")]
	[DefaultValue(typeof(Size), "24, 24")]
	[Description("The size of the image to be displayed on thebutton. This property defaults to 24x24.")]
	public Size ImageSize
	{
		get
		{
			return mImageSize;
		}
		set
		{
			mImageSize = value;
			Invalidate();
		}
	}

	[Category("Appearance")]
	[DefaultValue(typeof(Style), "Default")]
	[Description("Sets whether the button background is drawn while the mouse is outside of the client area.")]
	public Style ButtonStyle
	{
		get
		{
			return mButtonStyle;
		}
		set
		{
			mButtonStyle = value;
			Invalidate();
		}
	}

	[Category("Appearance")]
	[DefaultValue(8)]
	[Description("The radius for the button corners. The greater this value is, the more 'smooth' the corners are. This property should not be greater than half of the controls height.")]
	public int CornerRadius
	{
		get
		{
			return mCornerRadius;
		}
		set
		{
			mCornerRadius = value;
			Invalidate();
		}
	}

	[Description("The colour of the highlight on the top of the button.")]
	[DefaultValue(typeof(Color), "White")]
	[Category("Appearance")]
	public Color HighlightColor
	{
		get
		{
			return mHighlightColor;
		}
		set
		{
			mHighlightColor = value;
			Invalidate();
		}
	}

	[DefaultValue(typeof(Color), "Black")]
	[Description("The bottom color of the button that will be drawn over the base color.")]
	[Category("Appearance")]
	public Color ButtonColor
	{
		get
		{
			return mButtonColor;
		}
		set
		{
			mButtonColor = value;
			Invalidate();
		}
	}

	[Description("The colour that the button glows when the mouse is inside the client area.")]
	[DefaultValue(typeof(Color), "141,189,255")]
	[Category("Appearance")]
	public Color GlowColor
	{
		get
		{
			return mGlowColor;
		}
		set
		{
			mGlowColor = value;
			Invalidate();
		}
	}

	[DefaultValue(null)]
	[Description("The background image for the button, this image is drawn over the base color of the button.")]
	[Category("Appearance")]
	public Image BackImage
	{
		get
		{
			return mBackImage;
		}
		set
		{
			mBackImage = value;
			Invalidate();
		}
	}

	[DefaultValue(typeof(Color), "Black")]
	[Description("The backing color that the rest ofthe button is drawn. For a glassier effect set this property to Transparent.")]
	[Category("Appearance")]
	public Color BaseColor
	{
		get
		{
			return mBaseColor;
		}
		set
		{
			mBaseColor = value;
			Invalidate();
		}
	}

	public GlassBtn_New()
	{
		InitializeComponent();
		_Key = "";
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.DoubleBuffer, value: true);
		SetStyle(ControlStyles.ResizeRedraw, value: true);
		SetStyle(ControlStyles.Selectable, value: true);
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		SetStyle(ControlStyles.UserPaint, value: true);
		BackColor = Color.Transparent;
		mFadeIn.Interval = 30;
		mFadeOut.Interval = 30;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.mFadeIn = new System.Windows.Forms.Timer();
		this.mFadeOut = new System.Windows.Forms.Timer();
		base.Name = "GlassButton";
		base.Size = new System.Drawing.Size(100, 32);
		base.Paint += new System.Windows.Forms.PaintEventHandler(VistaButton_Paint);
		base.KeyUp += new System.Windows.Forms.KeyEventHandler(VistaButton_KeyUp);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(VistaButton_KeyDown);
		base.MouseEnter += new System.EventHandler(VistaButton_MouseEnter);
		base.MouseLeave += new System.EventHandler(VistaButton_MouseLeave);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(VistaButton_MouseUp);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(VistaButton_MouseDown);
		base.GotFocus += new System.EventHandler(VistaButton_MouseEnter);
		base.LostFocus += new System.EventHandler(VistaButton_MouseLeave);
		this.mFadeIn.Tick += new System.EventHandler(mFadeIn_Tick);
		this.mFadeOut.Tick += new System.EventHandler(mFadeOut_Tick);
		base.Resize += new System.EventHandler(VistaButton_Resize);
	}

	private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
	{
		float num = r.X;
		float num2 = r.Y;
		float num3 = r.Width;
		float num4 = r.Height;
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddBezier(num, num2 + r1, num, num2, num + r1, num2, num + r1, num2);
		graphicsPath.AddLine(num + r1, num2, num + num3 - r2, num2);
		graphicsPath.AddBezier(num + num3 - r2, num2, num + num3, num2, num + num3, num2 + r2, num + num3, num2 + r2);
		graphicsPath.AddLine(num + num3, num2 + r2, num + num3, num2 + num4 - r3);
		graphicsPath.AddBezier(num + num3, num2 + num4 - r3, num + num3, num2 + num4, num + num3 - r3, num2 + num4, num + num3 - r3, num2 + num4);
		graphicsPath.AddLine(num + num3 - r3, num2 + num4, num + r4, num2 + num4);
		graphicsPath.AddBezier(num + r4, num2 + num4, num, num2 + num4, num, num2 + num4 - r4, num, num2 + num4 - r4);
		graphicsPath.AddLine(num, num2 + num4 - r4, num, num2 + r1);
		return graphicsPath;
	}

	private StringFormat StringFormatAlignment(ContentAlignment textalign)
	{
		StringFormat stringFormat = new StringFormat();
		switch (textalign)
		{
		case ContentAlignment.TopLeft:
		case ContentAlignment.TopCenter:
		case ContentAlignment.TopRight:
			stringFormat.LineAlignment = StringAlignment.Near;
			break;
		case ContentAlignment.MiddleLeft:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.MiddleRight:
			stringFormat.LineAlignment = StringAlignment.Center;
			break;
		case ContentAlignment.BottomLeft:
		case ContentAlignment.BottomCenter:
		case ContentAlignment.BottomRight:
			stringFormat.LineAlignment = StringAlignment.Far;
			break;
		}
		switch (textalign)
		{
		case ContentAlignment.TopLeft:
		case ContentAlignment.MiddleLeft:
		case ContentAlignment.BottomLeft:
			stringFormat.Alignment = StringAlignment.Near;
			break;
		case ContentAlignment.TopCenter:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.BottomCenter:
			stringFormat.Alignment = StringAlignment.Center;
			break;
		case ContentAlignment.TopRight:
		case ContentAlignment.MiddleRight:
		case ContentAlignment.BottomRight:
			stringFormat.Alignment = StringAlignment.Far;
			break;
		}
		return stringFormat;
	}

	private void DrawOuterStroke(Graphics g)
	{
		if (ButtonStyle == Style.Flat && mButtonState == State.None)
		{
			return;
		}
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.Width--;
		clientRectangle.Height--;
		using GraphicsPath path = RoundRect(clientRectangle, CornerRadius, CornerRadius, CornerRadius, CornerRadius);
		using Pen pen = new Pen(ButtonColor);
		g.DrawPath(pen, path);
	}

	private void DrawInnerStroke(Graphics g)
	{
		if (ButtonStyle == Style.Flat && mButtonState == State.None)
		{
			return;
		}
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.X++;
		clientRectangle.Y++;
		clientRectangle.Width -= 3;
		clientRectangle.Height -= 3;
		using GraphicsPath path = RoundRect(clientRectangle, CornerRadius, CornerRadius, CornerRadius, CornerRadius);
		using Pen pen = new Pen(HighlightColor);
		g.DrawPath(pen, path);
	}

	private void DrawBackground(Graphics g)
	{
		if (ButtonStyle == Style.Flat && mButtonState == State.None)
		{
			return;
		}
		int alpha = ((mButtonState == State.Pressed) ? 204 : 127);
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.Width--;
		clientRectangle.Height--;
		using GraphicsPath path = RoundRect(clientRectangle, CornerRadius, CornerRadius, CornerRadius, CornerRadius);
		using (SolidBrush brush = new SolidBrush(BaseColor))
		{
			g.FillPath(brush, path);
		}
		SetClip(g);
		if (BackImage != null)
		{
			g.DrawImage(BackImage, base.ClientRectangle);
		}
		g.ResetClip();
		using SolidBrush brush2 = new SolidBrush(Color.FromArgb(alpha, ButtonColor));
		g.FillPath(brush2, path);
	}

	private void DrawHighlight(Graphics g)
	{
		if (ButtonStyle == Style.Flat && mButtonState == State.None)
		{
			return;
		}
		int num = ((mButtonState == State.Pressed) ? 60 : 150);
		Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height / 2);
		using GraphicsPath graphicsPath = RoundRect(rectangle, CornerRadius, CornerRadius, 0f, 0f);
		using LinearGradientBrush brush = new LinearGradientBrush(graphicsPath.GetBounds(), Color.FromArgb(num, HighlightColor), Color.FromArgb(num / 3, HighlightColor), LinearGradientMode.Vertical);
		g.FillPath(brush, graphicsPath);
	}

	private void DrawGlow(Graphics g)
	{
		if (mButtonState == State.Pressed)
		{
			return;
		}
		SetClip(g);
		using (GraphicsPath graphicsPath = new GraphicsPath())
		{
			int num = base.Height;
			num = ((num >= 30) ? 25 : 10);
			graphicsPath.AddEllipse(-5, base.Height / 2 - num, base.Width + 11, base.Height + 11);
			using PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
			pathGradientBrush.CenterColor = Color.FromArgb(mGlowAlpha, GlowColor);
			pathGradientBrush.SurroundColors = new Color[1] { Color.FromArgb(0, GlowColor) };
			g.FillPath(pathGradientBrush, graphicsPath);
		}
		g.ResetClip();
	}

	private void DrawText(Graphics g)
	{
		StringFormat format = StringFormatAlignment(TextAlign);
		g.DrawString(layoutRectangle: new Rectangle(8, 8, base.Width - 17, base.Height - 17), s: ButtonText, font: Font, brush: new SolidBrush(ForeColor), format: format);
	}

	private void DrawImage(Graphics g)
	{
		if (Image != null)
		{
			Rectangle rect = new Rectangle(8, 8, ImageSize.Width, ImageSize.Height);
			switch (ImageAlign)
			{
			case ContentAlignment.TopCenter:
				rect = new Rectangle(base.Width / 2 - ImageSize.Width / 2, 8, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.TopRight:
				rect = new Rectangle(base.Width - 8 - ImageSize.Width, 8, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.MiddleLeft:
				rect = new Rectangle(8, base.Height / 2 - ImageSize.Height / 2, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.MiddleCenter:
				rect = new Rectangle(base.Width / 2 - ImageSize.Width / 2, base.Height / 2 - ImageSize.Height / 2, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.MiddleRight:
				rect = new Rectangle(base.Width - 8 - ImageSize.Width, base.Height / 2 - ImageSize.Height / 2, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.BottomLeft:
				rect = new Rectangle(8, base.Height - 8 - ImageSize.Height, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.BottomCenter:
				rect = new Rectangle(base.Width / 2 - ImageSize.Width / 2, base.Height - 8 - ImageSize.Height, ImageSize.Width, ImageSize.Height);
				break;
			case ContentAlignment.BottomRight:
				rect = new Rectangle(base.Width - 8 - ImageSize.Width, base.Height - 8 - ImageSize.Height, ImageSize.Width, ImageSize.Height);
				break;
			}
			g.DrawImage(Image, rect);
		}
	}

	private void SetClip(Graphics g)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.X++;
		clientRectangle.Y++;
		clientRectangle.Width -= 3;
		clientRectangle.Height -= 3;
		using GraphicsPath clip = RoundRect(clientRectangle, CornerRadius, CornerRadius, CornerRadius, CornerRadius);
		g.SetClip(clip);
	}

	private void VistaButton_Paint(object sender, PaintEventArgs e)
	{
		if (tc.ChkKey(_Key))
		{
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			DrawBackground(e.Graphics);
			DrawHighlight(e.Graphics);
			DrawGlow(e.Graphics);
			DrawImage(e.Graphics);
			DrawText(e.Graphics);
			DrawOuterStroke(e.Graphics);
			DrawInnerStroke(e.Graphics);
		}
	}

	private void VistaButton_Resize(object sender, EventArgs e)
	{
		Rectangle clientRectangle = base.ClientRectangle;
		clientRectangle.X--;
		clientRectangle.Y--;
		clientRectangle.Width += 2;
		clientRectangle.Height += 2;
		using GraphicsPath path = RoundRect(clientRectangle, CornerRadius, CornerRadius, CornerRadius, CornerRadius);
		base.Region = new Region(path);
	}

	private void VistaButton_MouseEnter(object sender, EventArgs e)
	{
		mButtonState = State.Hover;
		mFadeOut.Stop();
		mFadeIn.Start();
	}

	private void VistaButton_MouseLeave(object sender, EventArgs e)
	{
		mButtonState = State.None;
		if (mButtonStyle == Style.Flat)
		{
			mGlowAlpha = 0;
		}
		mFadeIn.Stop();
		mFadeOut.Start();
	}

	private void VistaButton_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mButtonState = State.Pressed;
			if (mButtonStyle != Style.Flat)
			{
				mGlowAlpha = 255;
			}
			mFadeIn.Stop();
			mFadeOut.Stop();
			Invalidate();
		}
	}

	private void mFadeIn_Tick(object sender, EventArgs e)
	{
		if (ButtonStyle == Style.Flat)
		{
			mGlowAlpha = 0;
		}
		if (mGlowAlpha + 40 >= 255)
		{
			mGlowAlpha = 255;
			mFadeIn.Stop();
		}
		else
		{
			mGlowAlpha += 40;
		}
		Invalidate();
	}

	private void mFadeOut_Tick(object sender, EventArgs e)
	{
		if (ButtonStyle == Style.Flat)
		{
			mGlowAlpha = 0;
		}
		if (mGlowAlpha - 40 <= 0)
		{
			mGlowAlpha = 0;
			mFadeOut.Stop();
		}
		else
		{
			mGlowAlpha -= 40;
		}
		Invalidate();
	}

	private void VistaButton_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Space)
		{
			MouseEventArgs e2 = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
			VistaButton_MouseDown(sender, e2);
		}
	}

	private void VistaButton_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Space)
		{
			MouseEventArgs e2 = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
			calledbykey = true;
			VistaButton_MouseUp(sender, e2);
		}
	}

	private void VistaButton_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mButtonState = State.Hover;
			mFadeIn.Stop();
			mFadeOut.Stop();
			Invalidate();
			if (calledbykey)
			{
				OnClick(EventArgs.Empty);
				calledbykey = false;
			}
		}
	}
}
