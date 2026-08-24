using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ComponentDll;

public class ClsForm : Form
{
	private IContainer components;

	public Color _Color1 = default(Color);

	public Color _Color2 = default(Color);

	public float _ColorAngle;

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
		base.SuspendLayout();
		base.ClientSize = new System.Drawing.Size(412, 331);
		base.Name = "ClsForm";
		base.ResumeLayout(false);
	}

	public ClsForm()
	{
		_Color1 = Color.White;
		_Color2 = Color.Gray;
		_ColorAngle = 90f;
		InitializeComponent();
		Invalidate();
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
		Graphics graphics = pevent.Graphics;
		Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, _Color1, _Color2, _ColorAngle);
		graphics.FillRectangle(linearGradientBrush, rect);
		linearGradientBrush.Dispose();
	}
}
