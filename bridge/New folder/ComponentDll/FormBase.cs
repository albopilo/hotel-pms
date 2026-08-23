using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using ComponentDll.Properties;
using Sayes.Controls.Vista.Config;

namespace ComponentDll;

public class FormBase : Form
{
	public bool ismax;

	private Timer timerBackGround;

	private Sayes.Controls.Vista.Config.Color _ColorSetting;

	private Region MinRegion;

	private Region MaxRegion;

	private Region CloseRegion;

	private bool IsMinOn;

	private bool IsMaxOn;

	private bool IsCloseOn;

	private static FormBase FocusForm;

	public Bitmap myImage;

	public Graphics g;

	private IntPtr dc1;

	private GaussianBlur filter;

	private Point StartPoint;

	private bool IsMoveClick;

	private bool IsClick;

	private IContainer components;

	public new double Opacity => 1.0;

	public new Padding Padding => base.Padding;

	internal Sayes.Controls.Vista.Config.Color ColorSetting => _ColorSetting;

	public unsafe static Bitmap KiBlur(Bitmap b)
	{
		if (b == null)
		{
			return null;
		}
		int num = b.Width;
		int num2 = b.Height;
		try
		{
			Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format24bppRgb);
			BitmapData bitmapData = b.LockBits(new Rectangle(0, 0, num, num2), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			BitmapData bitmapData2 = bitmap.LockBits(new Rectangle(0, 0, num, num2), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
			byte* ptr = (byte*)bitmapData.Scan0.ToPointer();
			byte* ptr2 = (byte*)bitmapData2.Scan0.ToPointer();
			int stride = bitmapData.Stride;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (j == 0 || j == num - 1 || i == 0 || i == num2 - 1)
					{
						*ptr2 = *ptr;
						ptr2[1] = ptr[1];
						ptr2[2] = ptr[2];
					}
					else
					{
						byte* ptr3 = ptr - stride - 3;
						int num3 = ptr3[2];
						int num4 = ptr3[1];
						int num5 = *ptr3;
						ptr3 = ptr - stride;
						int num6 = ptr3[2];
						int num7 = ptr3[1];
						int num8 = *ptr3;
						ptr3 = ptr - stride + 3;
						int num9 = ptr3[2];
						int num10 = ptr3[1];
						int num11 = *ptr3;
						ptr3 = ptr - 3;
						int num12 = ptr3[2];
						int num13 = ptr3[1];
						int num14 = *ptr3;
						ptr3 = ptr + 3;
						int num15 = ptr3[2];
						int num16 = ptr3[1];
						int num17 = *ptr3;
						ptr3 = ptr + stride - 3;
						int num18 = ptr3[2];
						int num19 = ptr3[1];
						int num20 = *ptr3;
						ptr3 = ptr + stride;
						int num21 = ptr3[2];
						int num22 = ptr3[1];
						int num23 = *ptr3;
						ptr3 = ptr + stride + 3;
						int num24 = ptr3[2];
						int num25 = ptr3[1];
						int num26 = *ptr3;
						ptr3 = ptr;
						int num27 = ptr3[2];
						int num28 = ptr3[1];
						int num29 = *ptr3;
						float num30 = num3 + num6 + num9 + num12 + num15 + num18 + num21 + num24 + num27;
						float num31 = num4 + num7 + num10 + num13 + num16 + num19 + num22 + num25 + num28;
						float num32 = num5 + num8 + num11 + num14 + num17 + num20 + num23 + num26 + num29;
						num30 /= 9f;
						num31 /= 9f;
						num32 /= 9f;
						*ptr2 = (byte)num32;
						ptr2[1] = (byte)num31;
						ptr2[2] = (byte)num30;
					}
					ptr += 3;
					ptr2 += 3;
				}
				ptr += bitmapData.Stride - num * 3;
				ptr2 += bitmapData.Stride - num * 3;
			}
			b.UnlockBits(bitmapData);
			bitmap.UnlockBits(bitmapData2);
			b.Dispose();
			return bitmap;
		}
		catch
		{
			return null;
		}
	}

	public FormBase()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		_ColorSetting = new Sayes.Controls.Vista.Config.Color();
		MinRegion = new Region();
		MaxRegion = new Region();
		CloseRegion = new Region();
		filter = new GaussianBlur();
		StartPoint = new Point(0, 0);
		base._002Ector();
		InitializeComponent();
		FocusForm = this;
		base.Padding = new Padding(7, 24, 7, 7);
		base.Opacity = 0.99;
		base.FormBorderStyle = FormBorderStyle.None;
		SetStyle(ControlStyles.UserPaint, value: true);
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	private void EnableAllControls()
	{
		if (this == null || base.Controls == null)
		{
			return;
		}
		foreach (Control control in base.Controls)
		{
			control.Enabled = true;
		}
	}

	private void DisableAllControls()
	{
		foreach (Control control in base.Controls)
		{
			control.Enabled = false;
		}
	}

	protected override void OnDeactivate(EventArgs e)
	{
		base.OnDeactivate(e);
		timerBackGround.Start();
		DisableAllControls();
	}

	protected override void OnActivated(EventArgs e)
	{
		base.OnActivated(e);
		FocusForm = this;
		timerBackGround.Stop();
		Invalidate();
		EnableAllControls();
	}

	protected override void OnLocationChanged(EventArgs e)
	{
		base.OnLocationChanged(e);
		Invalidate();
	}

	public virtual Image my(Point px, Point px1, Size r)
	{
		myImage = new Bitmap(r.Width, r.Height);
		g = Graphics.FromImage(myImage);
		g.CopyFromScreen(px, px1, r);
		filter.Size = 7;
		filter.Sigma = 5.0;
		return filter.Apply(myImage);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		e.Graphics.Clear(System.Drawing.Color.White);
		if (!ismax)
		{
			e.Graphics.DrawImage(my(PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(base.Width, 31)), 0, 0, base.Width, 31);
			e.Graphics.DrawImage(my(PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(9, base.Height)), 0, 0, 9, base.Height);
			e.Graphics.DrawImage(my(PointToScreen(new Point(base.Width - 7, 0)), new Point(0, 0), new Size(11, base.Height)), base.Width - 11, 0);
			e.Graphics.DrawImage(my(PointToScreen(new Point(0, base.Height - 11)), new Point(0, 0), new Size(base.Width, 10)), 0, base.Height - 10);
		}
		else
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.hear, 0, 0, base.Width, 30);
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.hearhei, 0, 0, 300, 30);
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.hearhei, 0, 0, base.Width, 30);
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.wi, 0, 30);
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.wi, base.Width - 7, 30);
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.botton, 0, base.Height - 11, base.Width, 11);
		}
		_ = FocusForm;
		e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.topline, 7, 0, base.Width, 5);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.toplef, 0, 1, 7, 7);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.ritop, base.Width - 10, 1, 7, 7);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.leflef, 0, 7, 5, base.Height);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.riri, base.Width - 6, 5, 4, base.Height);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.bottom, 5, base.Height - 5, base.Width, 5);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.lefbottom, 0, base.Height - 7, 6, 6);
		e.Graphics.DrawImage(ComponentDll.Properties.Resources.ribottom, base.Width - 7, base.Height - 7, 7, 7);
		Rectangle rect = new Rectangle(10, 32, base.Width - 23, base.Height - 44);
		e.Graphics.FillRectangle(new SolidBrush(ColorSetting.BackColor), rect);
		if (FocusForm == this)
		{
			e.Graphics.DrawRectangle(new Pen(ColorSetting.ActiveBorderColor), rect);
		}
		else
		{
			e.Graphics.DrawRectangle(new Pen(ColorSetting.BorderColor), rect);
		}
		if (BackgroundImage != null)
		{
			e.Graphics.DrawImage(BackgroundImage, rect);
		}
		if (base.Icon != null)
		{
			e.Graphics.DrawIcon(base.Icon, new Rectangle(10, 10, 16, 16));
		}
		Font font = new Font("ArialBlack", 9f, FontStyle.Bold);
		e.Graphics.DrawString(Text, font, Brushes.White, 21f, 10f);
		e.Graphics.DrawString(Text, font, Brushes.White, 23f, 10f);
		e.Graphics.DrawString(Text, font, Brushes.White, 22f, 9f);
		e.Graphics.DrawString(Text, font, Brushes.White, 22f, 11f);
		e.Graphics.DrawString(Text, font, Brushes.Black, 22f, 10f);
		if (IsMinOn)
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.MinHigh, MinRegion.GetBounds(e.Graphics));
		}
		else
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.Min, MinRegion.GetBounds(e.Graphics));
		}
		if (IsCloseOn)
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.CloseHigh, CloseRegion.GetBounds(e.Graphics));
		}
		else
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.Close, CloseRegion.GetBounds(e.Graphics));
		}
		if (IsMaxOn)
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.MaxHigh, MaxRegion.GetBounds(e.Graphics));
		}
		else
		{
			e.Graphics.DrawImage(ComponentDll.Properties.Resources.Max, MaxRegion.GetBounds(e.Graphics));
		}
		if (ismax)
		{
			base.Opacity = 10.0;
		}
		else
		{
			base.Opacity = 0.99;
		}
		e.Dispose();
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		try
		{
			MinRegion = new Region(new Rectangle(base.Width - 110, 2, 30, 23));
			MaxRegion = new Region(new Rectangle(base.Width - 79, 2, 30, 23));
			CloseRegion = new Region(new Rectangle(base.Width - 49, 2, 40, 23));
		}
		catch
		{
		}
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		StartPoint = new Point(e.X, e.Y);
		if (e.Y < 23)
		{
			if (!MinRegion.IsVisible(new Point(e.X, e.Y)) && !MaxRegion.IsVisible(new Point(e.X, e.Y)) && !CloseRegion.IsVisible(new Point(e.X, e.Y)))
			{
				IsMoveClick = true;
			}
		}
		else
		{
			IsClick = true;
		}
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		Cursor = Cursors.Default;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		Cursor = Cursors.Default;
		if (MinRegion.IsVisible(new Point(e.X, e.Y)) && !IsMinOn)
		{
			IsMinOn = true;
			IsMaxOn = false;
			IsCloseOn = false;
			Cursor = Cursors.Hand;
			Invalidate(MinRegion);
			Invalidate(CloseRegion);
			Invalidate(MaxRegion);
		}
		if (MaxRegion.IsVisible(new Point(e.X, e.Y)) && !IsMaxOn)
		{
			IsMinOn = false;
			IsCloseOn = false;
			IsMaxOn = true;
			Cursor = Cursors.Hand;
			Invalidate(MinRegion);
			Invalidate(MaxRegion);
			Invalidate(CloseRegion);
		}
		if (CloseRegion.IsVisible(new Point(e.X, e.Y)) && !IsCloseOn)
		{
			IsMinOn = false;
			IsMaxOn = false;
			IsCloseOn = true;
			Cursor = Cursors.Hand;
			Invalidate(MinRegion);
			Invalidate(CloseRegion);
			Invalidate(MaxRegion);
		}
		if (IsMoveClick)
		{
			base.Left += e.X - StartPoint.X;
			base.Top += e.Y - StartPoint.Y;
		}
		if (e.X > base.Width - 6 && e.Y >= 23 && e.Y <= base.Height - 6)
		{
			Cursor = Cursors.SizeWE;
			if (IsClick)
			{
				base.Width += e.X - StartPoint.X;
				StartPoint = new Point(e.X, e.Y);
			}
		}
		else if (e.X > base.Width - 6 && e.Y > base.Height - 6)
		{
			Cursor = Cursors.SizeNWSE;
			if (IsClick)
			{
				base.Width += e.X - StartPoint.X;
				base.Height += e.Y - StartPoint.Y;
				StartPoint = new Point(e.X, e.Y);
			}
		}
		else if (e.Y > base.Height - 6)
		{
			Cursor = Cursors.SizeNS;
			if (IsClick)
			{
				base.Height -= StartPoint.Y - e.Y;
				StartPoint = new Point(e.X, e.Y);
			}
		}
		else
		{
			Cursor = Cursors.Default;
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (MinRegion.IsVisible(new Point(e.X, e.Y)) && base.MinimizeBox)
		{
			base.WindowState = FormWindowState.Minimized;
			CommonClass.SetTaskMenu(this);
		}
		if (MaxRegion.IsVisible(new Point(e.X, e.Y)) && base.MaximizeBox)
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				base.WindowState = FormWindowState.Maximized;
				ismax = true;
			}
			else
			{
				base.WindowState = FormWindowState.Normal;
				ismax = false;
			}
		}
		if (CloseRegion.IsVisible(new Point(e.X, e.Y)))
		{
			Close();
		}
		IsClick = false;
		IsMoveClick = false;
		StartPoint = new Point(0, 0);
	}

	protected override void OnControlAdded(ControlEventArgs e)
	{
		base.OnControlRemoved(e);
		e.Control.LocationChanged += Control_SizeChanged;
		e.Control.SizeChanged += Control_SizeChanged;
		Control_SizeChanged(e.Control, null);
	}

	private void Control_SizeChanged(object sender, EventArgs e)
	{
		Control control = (Control)sender;
		if (control.Dock == DockStyle.None)
		{
			if (control.Width > base.Width - 8)
			{
				control.Width = base.Width - 8;
			}
			if (control.Height > base.Height - 8)
			{
				control.Height = base.Height - 8;
			}
			if (control.Left < 3)
			{
				control.Left = 4;
			}
			if (control.Right > base.Width - 3)
			{
				control.Left = base.Width - 4 - control.Width;
			}
			if (control.Top < 23)
			{
				control.Top = 24;
			}
			if (control.Bottom > base.Height - 3)
			{
				control.Top = base.Height - 4 - control.Height;
			}
		}
	}

	private void timerBackGround_Tick(object sender, EventArgs e)
	{
		if (this != FocusForm)
		{
			Invalidate(invalidateChildren: false);
		}
	}

	private void GlassForm_DoubleClick(object sender, EventArgs e)
	{
		if (Control.MousePosition.Y - base.Location.Y < 23 && Control.MousePosition.X - base.Location.X < base.Width - 150)
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				base.WindowState = FormWindowState.Maximized;
				ismax = true;
			}
			else if (base.WindowState == FormWindowState.Maximized)
			{
				base.WindowState = FormWindowState.Normal;
				ismax = false;
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.timerBackGround = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		base.ClientSize = new System.Drawing.Size(549, 281);
		base.Name = "FormBase";
		this.Text = "Form";
		base.ResumeLayout(false);
	}
}
