using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ComponentDll;

internal class KeyboardLight : UserControl
{
	private bool on;

	private KeyboardButton assositeButton;

	private static readonly Size SIZE = new Size(16, 16);

	public KeyboardButton AssositeButton
	{
		set
		{
			if (!object.ReferenceEquals(assositeButton, value))
			{
				assositeButton = value;
				assositeButton.CheckChanged += AssositeButtonOnCheckChanged;
			}
		}
	}

	public bool On => on;

	public KeyboardLight()
	{
		base.Size = SIZE;
		on = false;
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		UpdateStyles();
		BackColor = Color.Transparent;
	}

	private void AssositeButtonOnCheckChanged(object sender, CheckChangedEventArgs e)
	{
		on = e.Checked;
		Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		Graphics graphics = e.Graphics;
		graphics.SmoothingMode = SmoothingMode.HighQuality;
		Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
		using (Pen pen = (on ? new Pen(Color.FromArgb(0, 0, 128)) : new Pen(Color.FromArgb(59, 97, 156))))
		{
			graphics.DrawEllipse(pen, rect);
		}
		using Brush brush = (on ? new SolidBrush(Color.Yellow) : new SolidBrush(Color.White));
		if (rect.Width > 3 && rect.Height > 3)
		{
			rect.Inflate(-1, -1);
			graphics.FillEllipse(Brushes.WhiteSmoke, rect);
			rect.Inflate(-2, -2);
			graphics.FillEllipse(brush, rect);
		}
	}
}
