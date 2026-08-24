using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LockSoftware;

public class frmDisplaySet : Form
{
	private IContainer components;

	private ComboBox comboBox1;

	public frmDisplaySet()
	{
		InitializeComponent();
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
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(12, 12);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(121, 20);
		this.comboBox1.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.comboBox1);
		base.MaximizeBox = false;
		base.Name = "frmDisplaySet";
		base.ShowIcon = false;
		base.ResumeLayout(false);
	}
}
