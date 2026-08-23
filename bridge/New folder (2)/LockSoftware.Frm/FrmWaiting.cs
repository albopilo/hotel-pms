using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class FrmWaiting : Form
{
	private IContainer components;

	private PictureBox picWait;

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
		this.picWait = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picWait).BeginInit();
		base.SuspendLayout();
		this.picWait.BackColor = System.Drawing.Color.Transparent;
		this.picWait.Dock = System.Windows.Forms.DockStyle.Fill;
		this.picWait.Image = LockSoftware.Properties.Resources.loadpage;
		this.picWait.InitialImage = null;
		this.picWait.Location = new System.Drawing.Point(0, 0);
		this.picWait.Margin = new System.Windows.Forms.Padding(0);
		this.picWait.Name = "picWait";
		this.picWait.Size = new System.Drawing.Size(50, 50);
		this.picWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.picWait.TabIndex = 13;
		this.picWait.TabStop = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(50, 50);
		base.ControlBox = false;
		base.Controls.Add(this.picWait);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FrmWaiting";
		base.Opacity = 0.0;
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Waiting";
		((System.ComponentModel.ISupportInitialize)this.picWait).EndInit();
		base.ResumeLayout(false);
	}

	public FrmWaiting()
	{
		InitializeComponent();
	}
}
