using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LockSoftware.Controls;

namespace LockSoftware.Frm;

public class frmCols : Form
{
	private IContainer components;

	private clsBackPanel clsBackPanel1;

	private ToolsBtn toolsBtn1;

	private ToolsBtn toolsBtn2;

	private CheckedListBox checkedListBox1;

	public bool ok;

	public string selCol = "";

	public string selColName = "";

	public string selIndex = "";

	public int colType;

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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmCols));
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn2 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.clsBackPanel1.Border = false;
		this.clsBackPanel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderBW = 1;
		this.clsBackPanel1.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel1.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel1.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel1.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel1.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderLW = 1;
		this.clsBackPanel1.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderRW = 1;
		this.clsBackPanel1.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderTW = 1;
		this.clsBackPanel1.Color1 = System.Drawing.Color.White;
		this.clsBackPanel1.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.toolsBtn2);
		this.clsBackPanel1.Controls.Add(this.toolsBtn1);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(224, 38);
		this.clsBackPanel1.TabIndex = 0;
		this.toolsBtn1.AutoSize = true;
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = false;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.ImageNew = null;
		this.toolsBtn1.ImageRedrawed = true;
		this.toolsBtn1.ImageStyle = 0;
		this.toolsBtn1.isButton = true;
		this.toolsBtn1.Location = new System.Drawing.Point(10, 16);
		this.toolsBtn1.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn1.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn1.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn1.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn1.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn1.Name = "toolsBtn1";
		this.toolsBtn1.Size = new System.Drawing.Size(59, 12);
		this.toolsBtn1.TabIndex = 0;
		this.toolsBtn1.Text = "toolsBtn1";
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "";
		this.toolsBtn1.TextRedrawed = false;
		this.toolsBtn2.AutoSize = true;
		this.toolsBtn2.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Checked = false;
		this.toolsBtn2.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn2.ImageNew = null;
		this.toolsBtn2.ImageRedrawed = true;
		this.toolsBtn2.ImageStyle = 0;
		this.toolsBtn2.isButton = true;
		this.toolsBtn2.Location = new System.Drawing.Point(113, 16);
		this.toolsBtn2.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn2.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn2.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn2.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn2.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn2.Name = "toolsBtn2";
		this.toolsBtn2.Size = new System.Drawing.Size(59, 12);
		this.toolsBtn2.TabIndex = 1;
		this.toolsBtn2.Text = "toolsBtn2";
		this.toolsBtn2.TextImageLocation = 0;
		this.toolsBtn2.TextNew = "";
		this.toolsBtn2.TextRedrawed = false;
		this.checkedListBox1.FormattingEnabled = true;
		this.checkedListBox1.Location = new System.Drawing.Point(0, 38);
		this.checkedListBox1.Name = "checkedListBox1";
		this.checkedListBox1.Size = new System.Drawing.Size(224, 356);
		this.checkedListBox1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(224, 391);
		base.Controls.Add(this.checkedListBox1);
		base.Controls.Add(this.clsBackPanel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCols";
		this.Text = "frmCols";
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmCols()
	{
		InitializeComponent();
	}
}
