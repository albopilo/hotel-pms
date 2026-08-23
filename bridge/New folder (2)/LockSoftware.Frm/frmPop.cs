using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmPop : Form
{
	private IContainer components;

	private Label label1;

	private clsBackPanel panel1;

	private clsBackPanel panel2;

	private ToolsBtn btnUp;

	private ToolsBtn btnNext;

	private TextBox txtMess;

	private Panel panel3;

	private clsBackPanel plMain;

	private clsBackPanel panel5;

	private PictureBox pictureBox1;

	private ToolsBtn btnClose;

	private Timer timerChkPo;

	private Label label3;

	public LinkLabel labrn;

	private CheckBox chkPop;

	private ToolTip tTipPO;

	private ToolTip tTipMsg;

	public Timer timer1;

	private FlowLayoutPanel flowLayoutPanel1;

	private Panel panel4;

	private FlowLayoutPanel flowLayoutPanel2;

	private Label labTitle;

	private clsBackPanel clsBackPanel1;

	public string m_objName = "WFrp";

	public Hashtable m_htab;

	private Screen cur_Screen;

	private Rectangle rect;

	public bool clClick;

	public bool hide;

	public int curInd;

	public int tCount;

	public ArrayList rnlist = new ArrayList();

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmPop));
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.timerChkPo = new System.Windows.Forms.Timer(this.components);
		this.tTipPO = new System.Windows.Forms.ToolTip(this.components);
		this.tTipMsg = new System.Windows.Forms.ToolTip(this.components);
		this.plMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.chkPop = new System.Windows.Forms.CheckBox();
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.labrn = new System.Windows.Forms.LinkLabel();
		this.btnUp = new LockSoftware.Controls.ToolsBtn(this.components);
		this.panel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnNext = new LockSoftware.Controls.ToolsBtn(this.components);
		this.txtMess = new System.Windows.Forms.TextBox();
		this.panel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.panel5 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.labTitle = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.btnClose = new LockSoftware.Controls.ToolsBtn(this.components);
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.plMain.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		this.flowLayoutPanel2.SuspendLayout();
		this.panel5.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.timer1.Interval = 25;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.timerChkPo.Interval = 500;
		this.timerChkPo.Tick += new System.EventHandler(timerChkPo_Tick);
		this.tTipPO.AutoPopDelay = 60000;
		this.tTipPO.InitialDelay = 200;
		this.tTipPO.IsBalloon = true;
		this.tTipPO.ReshowDelay = 100;
		this.tTipPO.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		this.tTipMsg.AutoPopDelay = 1500;
		this.tTipMsg.InitialDelay = 500;
		this.tTipMsg.ReshowDelay = 100;
		this.tTipMsg.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		this.plMain.Border = true;
		this.plMain.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderBW = 1;
		this.plMain.BorderColorBottom = System.Drawing.Color.DimGray;
		this.plMain.BorderColorLeft = System.Drawing.Color.DimGray;
		this.plMain.BorderColorRight = System.Drawing.Color.DimGray;
		this.plMain.BorderColorTop = System.Drawing.Color.DimGray;
		this.plMain.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderLW = 1;
		this.plMain.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderRW = 1;
		this.plMain.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderTW = 1;
		this.plMain.Color1 = System.Drawing.Color.White;
		this.plMain.Color2 = System.Drawing.Color.WhiteSmoke;
		this.plMain.ColorAngle = 45f;
		this.plMain.Controls.Add(this.panel4);
		this.plMain.Controls.Add(this.panel5);
		this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.plMain.Location = new System.Drawing.Point(0, 0);
		this.plMain.Name = "plMain";
		this.plMain.Size = new System.Drawing.Size(360, 280);
		this.plMain.TabIndex = 11;
		this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel4.BackColor = System.Drawing.Color.Transparent;
		this.panel4.Controls.Add(this.panel3);
		this.panel4.Location = new System.Drawing.Point(3, 33);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(354, 244);
		this.panel4.TabIndex = 12;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(249, 251, 252);
		this.panel3.Controls.Add(this.clsBackPanel1);
		this.panel3.Controls.Add(this.chkPop);
		this.panel3.Controls.Add(this.flowLayoutPanel2);
		this.panel3.Controls.Add(this.btnUp);
		this.panel3.Controls.Add(this.panel1);
		this.panel3.Controls.Add(this.btnNext);
		this.panel3.Controls.Add(this.txtMess);
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(354, 244);
		this.panel3.TabIndex = 10;
		this.clsBackPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.SlateGray;
		this.clsBackPanel1.ColorAngle = 180f;
		this.clsBackPanel1.Location = new System.Drawing.Point(6, 207);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(340, 1);
		this.clsBackPanel1.TabIndex = 12;
		this.chkPop.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkPop.AutoSize = true;
		this.chkPop.BackColor = System.Drawing.Color.Transparent;
		this.chkPop.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkPop.Location = new System.Drawing.Point(9, 217);
		this.chkPop.Name = "chkPop";
		this.chkPop.Size = new System.Drawing.Size(86, 18);
		this.chkPop.TabIndex = 9;
		this.chkPop.Text = "不重复提示";
		this.chkPop.UseVisualStyleBackColor = false;
		this.chkPop.CheckedChanged += new System.EventHandler(chkPop_CheckedChanged);
		this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel2.Controls.Add(this.label1);
		this.flowLayoutPanel2.Controls.Add(this.labrn);
		this.flowLayoutPanel2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.flowLayoutPanel2.Location = new System.Drawing.Point(5, 3);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.flowLayoutPanel2.Size = new System.Drawing.Size(337, 25);
		this.flowLayoutPanel2.TabIndex = 11;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label1.Location = new System.Drawing.Point(3, 3);
		this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(57, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "客房：";
		this.labrn.ActiveLinkColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.labrn.AutoSize = true;
		this.labrn.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labrn.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.labrn.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.labrn.LinkColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.labrn.Location = new System.Drawing.Point(61, 3);
		this.labrn.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
		this.labrn.Name = "labrn";
		this.labrn.Size = new System.Drawing.Size(108, 19);
		this.labrn.TabIndex = 10;
		this.labrn.TabStop = true;
		this.labrn.Text = "Room Name";
		this.labrn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(labrn_LinkClicked);
		this.labrn.Click += new System.EventHandler(labPO_Click);
		this.labrn.MouseEnter += new System.EventHandler(labrn_MouseEnter);
		this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnUp.BackColor = System.Drawing.Color.Transparent;
		this.btnUp.Checked = false;
		this.btnUp.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnUp.DefaultColor = System.Drawing.Color.Transparent;
		this.btnUp.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnUp.ImageNew = LockSoftware.Properties.Resources.GuideUp;
		this.btnUp.ImageRedrawed = true;
		this.btnUp.ImageStyle = 1;
		this.btnUp.isButton = true;
		this.btnUp.Location = new System.Drawing.Point(279, 214);
		this.btnUp.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnUp.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnUp.MouseDownStartColor = System.Drawing.Color.White;
		this.btnUp.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.btnUp.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.btnUp.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnUp.Name = "btnUp";
		this.btnUp.Size = new System.Drawing.Size(24, 24);
		this.btnUp.TabIndex = 6;
		this.btnUp.TextImageLocation = 0;
		this.btnUp.TextNew = "";
		this.btnUp.TextRedrawed = false;
		this.btnUp.Click += new System.EventHandler(btnUp_Click);
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.Border = false;
		this.panel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel1.BorderBW = 1;
		this.panel1.BorderColorBottom = System.Drawing.Color.Gray;
		this.panel1.BorderColorLeft = System.Drawing.Color.Gray;
		this.panel1.BorderColorRight = System.Drawing.Color.Gray;
		this.panel1.BorderColorTop = System.Drawing.Color.Gray;
		this.panel1.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel1.BorderLW = 1;
		this.panel1.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel1.BorderRW = 1;
		this.panel1.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel1.BorderTW = 1;
		this.panel1.Color1 = System.Drawing.Color.FromArgb(249, 251, 252);
		this.panel1.Color2 = System.Drawing.Color.SlateGray;
		this.panel1.ColorAngle = 180f;
		this.panel1.Location = new System.Drawing.Point(6, 30);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(340, 1);
		this.panel1.TabIndex = 2;
		this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnNext.BackColor = System.Drawing.Color.Transparent;
		this.btnNext.Checked = false;
		this.btnNext.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnNext.DefaultColor = System.Drawing.Color.Transparent;
		this.btnNext.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNext.ImageNew = (System.Drawing.Image)resources.GetObject("btnNext.ImageNew");
		this.btnNext.ImageRedrawed = true;
		this.btnNext.ImageStyle = 1;
		this.btnNext.isButton = true;
		this.btnNext.Location = new System.Drawing.Point(318, 214);
		this.btnNext.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnNext.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnNext.MouseDownStartColor = System.Drawing.Color.White;
		this.btnNext.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.btnNext.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.btnNext.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnNext.Name = "btnNext";
		this.btnNext.Size = new System.Drawing.Size(24, 24);
		this.btnNext.TabIndex = 7;
		this.btnNext.TextImageLocation = 0;
		this.btnNext.TextNew = "";
		this.btnNext.TextRedrawed = false;
		this.btnNext.Click += new System.EventHandler(btnNext_Click);
		this.txtMess.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtMess.BackColor = System.Drawing.Color.FromArgb(249, 251, 252);
		this.txtMess.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtMess.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtMess.ForeColor = System.Drawing.Color.DimGray;
		this.txtMess.Location = new System.Drawing.Point(9, 32);
		this.txtMess.Multiline = true;
		this.txtMess.Name = "txtMess";
		this.txtMess.ReadOnly = true;
		this.txtMess.Size = new System.Drawing.Size(336, 169);
		this.txtMess.TabIndex = 9;
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel2.Border = false;
		this.panel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel2.BorderBW = 1;
		this.panel2.BorderColorBottom = System.Drawing.Color.Gray;
		this.panel2.BorderColorLeft = System.Drawing.Color.Gray;
		this.panel2.BorderColorRight = System.Drawing.Color.Gray;
		this.panel2.BorderColorTop = System.Drawing.Color.Gray;
		this.panel2.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel2.BorderLW = 1;
		this.panel2.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel2.BorderRW = 1;
		this.panel2.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel2.BorderTW = 1;
		this.panel2.Color1 = System.Drawing.Color.FromArgb(249, 251, 252);
		this.panel2.Color2 = System.Drawing.Color.White;
		this.panel2.ColorAngle = 180f;
		this.panel2.Location = new System.Drawing.Point(6, 61);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(336, 1);
		this.panel2.TabIndex = 3;
		this.panel5.Border = true;
		this.panel5.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel5.BorderBW = 1;
		this.panel5.BorderColorBottom = System.Drawing.Color.Gray;
		this.panel5.BorderColorLeft = System.Drawing.Color.Gray;
		this.panel5.BorderColorRight = System.Drawing.Color.Gray;
		this.panel5.BorderColorTop = System.Drawing.Color.Gray;
		this.panel5.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel5.BorderLW = 1;
		this.panel5.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel5.BorderRW = 1;
		this.panel5.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.panel5.BorderTW = 1;
		this.panel5.Color1 = System.Drawing.Color.CadetBlue;
		this.panel5.Color2 = System.Drawing.Color.FromArgb(249, 251, 252);
		this.panel5.ColorAngle = 90f;
		this.panel5.Controls.Add(this.flowLayoutPanel1);
		this.panel5.Controls.Add(this.btnClose);
		this.panel5.Controls.Add(this.pictureBox1);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(360, 32);
		this.panel5.TabIndex = 11;
		this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel1.Controls.Add(this.labTitle);
		this.flowLayoutPanel1.Controls.Add(this.label3);
		this.flowLayoutPanel1.Location = new System.Drawing.Point(36, 6);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(279, 23);
		this.flowLayoutPanel1.TabIndex = 11;
		this.labTitle.AutoSize = true;
		this.labTitle.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labTitle.ForeColor = System.Drawing.Color.Teal;
		this.labTitle.Location = new System.Drawing.Point(3, 0);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(0, 19);
		this.labTitle.TabIndex = 4;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Teal;
		this.label3.Location = new System.Drawing.Point(9, 2);
		this.label3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(34, 17);
		this.label3.TabIndex = 3;
		this.label3.Text = "0/0";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.Checked = false;
		this.btnClose.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnClose.DefaultColor = System.Drawing.Color.Transparent;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.ImageNew = LockSoftware.Properties.Resources.Hide;
		this.btnClose.ImageRedrawed = true;
		this.btnClose.ImageStyle = 1;
		this.btnClose.isButton = true;
		this.btnClose.Location = new System.Drawing.Point(321, 3);
		this.btnClose.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnClose.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnClose.MouseDownStartColor = System.Drawing.Color.White;
		this.btnClose.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.btnClose.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.btnClose.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(26, 26);
		this.btnClose.TabIndex = 2;
		this.btnClose.TextImageLocation = 0;
		this.btnClose.TextNew = "";
		this.btnClose.TextRedrawed = false;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.BackgroundImage = LockSoftware.Properties.Resources.iGoLogo;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Location = new System.Drawing.Point(6, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(24, 24);
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(360, 280);
		base.Controls.Add(this.plMain);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmPop";
		base.Opacity = 0.49;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "新单提醒";
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmPop_FormClosed);
		base.Load += new System.EventHandler(frmPop_Load);
		base.Shown += new System.EventHandler(frmPop_Shown);
		this.plMain.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.flowLayoutPanel2.ResumeLayout(false);
		this.flowLayoutPanel2.PerformLayout();
		this.panel5.ResumeLayout(false);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public frmPop()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		labrn.Text = "";
		cur_Screen = Screen.PrimaryScreen;
		rect = Screen.GetWorkingArea(this);
		base.Left = (rect.Width - base.Width) / 2;
		base.Top = rect.Height;
		clClick = false;
		plMain.Enabled = false;
		timer1.Enabled = true;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (!clClick)
		{
			if (cur_Screen != null && base.Top > rect.Height - base.Height)
			{
				base.Top -= 20;
			}
			if (base.Opacity < 0.99)
			{
				base.Opacity += 0.05;
			}
			if (base.Top <= rect.Height - base.Height)
			{
				timer1.Enabled = false;
				base.Top = rect.Height - base.Height;
				base.Opacity = 100.0;
				plMain.Enabled = true;
				timerChkPo.Enabled = true;
				hide = false;
			}
		}
		else
		{
			if (cur_Screen != null && base.Top < rect.Height)
			{
				base.Top += 20;
			}
			if (base.Opacity > 0.0)
			{
				base.Opacity -= 0.1;
			}
			if (base.Top >= rect.Height)
			{
				timer1.Enabled = false;
				timerChkPo.Enabled = false;
				hide = true;
				Hide();
			}
		}
		Refresh();
	}

	private void frmPop_FormClosed(object sender, FormClosedEventArgs e)
	{
		timerChkPo.Enabled = false;
		timer1.Enabled = false;
		rnlist.Clear();
		m_htab.Clear();
		Dispose();
	}

	private void frmPop_Load(object sender, EventArgs e)
	{
		chkPop.Checked = false;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		timer1.Interval = 20;
		plMain.Enabled = false;
		timer1.Enabled = (clClick = true);
	}

	private void timerChkPo_Tick(object sender, EventArgs e)
	{
		try
		{
			timerChkPo.Enabled = false;
			if (rnlist.Count == 0)
			{
				btnClose_Click(null, null);
				return;
			}
			if (tCount < rnlist.Count)
			{
				if (labrn.Text == "")
				{
					guestListCls guestListCls2 = (guestListCls)rnlist[0];
					labrn.Text = guestListCls2.c_rn.Trim();
					label3.Text = "1";
					curInd = 0;
					txtMess.Text = (string)m_htab["lab01"] + guestListCls2.c_comedate;
					TextBox textBox = txtMess;
					textBox.Text = textBox.Text + "\r\n" + (string)m_htab["lab02"] + guestListCls2.c_leveldate;
					TextBox textBox2 = txtMess;
					textBox2.Text = textBox2.Text + "\r\n" + (string)m_htab["lab03"] + guestListCls2.c_gsd;
					if (guestListCls2.c_hr)
					{
						TextBox textBox3 = txtMess;
						textBox3.Text = textBox3.Text + " " + (string)Program.m_hPubTab["InfoHour"];
					}
					else
					{
						TextBox textBox4 = txtMess;
						textBox4.Text = textBox4.Text + " " + (string)Program.m_hPubTab["InfoDay"];
					}
					TextBox textBox5 = txtMess;
					string text = textBox5.Text;
					textBox5.Text = text + "\r\n" + (string)m_htab["lab04"] + guestListCls2.c_tcc + " " + guestListCls2.c_total.ToString("F2");
					TextBox textBox6 = txtMess;
					string text2 = textBox6.Text;
					textBox6.Text = text2 + "\r\n" + (string)m_htab["lab05"] + guestListCls2.c_pcc + " " + guestListCls2.c_paid.ToString("F2");
					if (guestListCls2.c_team)
					{
						TextBox textBox7 = txtMess;
						textBox7.Text = textBox7.Text + "\r\n" + (string)m_htab["lab06"] + guestListCls2.c_teamname;
						TextBox textBox8 = txtMess;
						textBox8.Text = textBox8.Text + "\r\n" + (string)m_htab["lab07"] + guestListCls2.c_teamguide;
						TextBox textBox9 = txtMess;
						textBox9.Text = textBox9.Text + "\r\n" + (string)m_htab["lab08"] + guestListCls2.c_teamtel;
					}
				}
				else
				{
					label3.Text = (curInd + 1).ToString();
				}
				tCount = rnlist.Count;
				Label label = label3;
				label.Text = label.Text + "/" + tCount;
				Refresh();
			}
		}
		catch (Exception ex)
		{
			Console.Write(ex.Message.ToString());
		}
		timerChkPo.Enabled = true;
	}

	private void btnUp_Click(object sender, EventArgs e)
	{
		try
		{
			chkPop.CheckedChanged -= chkPop_CheckedChanged;
			chkPop.Checked = false;
			chkPop.CheckedChanged -= chkPop_CheckedChanged;
			if (curInd >= 1)
			{
				curInd--;
				guestListCls guestListCls2 = (guestListCls)rnlist[curInd];
				labrn.Text = guestListCls2.c_rn.Trim();
				label3.Text = curInd + 1 + "/" + tCount;
				txtMess.Text = (string)m_htab["lab01"] + guestListCls2.c_comedate;
				TextBox textBox = txtMess;
				textBox.Text = textBox.Text + "\r\n" + (string)m_htab["lab02"] + guestListCls2.c_leveldate;
				TextBox textBox2 = txtMess;
				textBox2.Text = textBox2.Text + "\r\n" + (string)m_htab["lab03"] + guestListCls2.c_gsd;
				if (guestListCls2.c_hr)
				{
					TextBox textBox3 = txtMess;
					textBox3.Text = textBox3.Text + " " + (string)Program.m_hPubTab["InfoHour"];
				}
				else
				{
					TextBox textBox4 = txtMess;
					textBox4.Text = textBox4.Text + " " + (string)Program.m_hPubTab["InfoDay"];
				}
				TextBox textBox5 = txtMess;
				string text = textBox5.Text;
				textBox5.Text = text + "\r\n" + (string)m_htab["lab04"] + guestListCls2.c_tcc + " " + guestListCls2.c_total.ToString("F2");
				TextBox textBox6 = txtMess;
				string text2 = textBox6.Text;
				textBox6.Text = text2 + "\r\n" + (string)m_htab["lab05"] + guestListCls2.c_pcc + " " + guestListCls2.c_paid.ToString("F2");
				if (guestListCls2.c_team)
				{
					TextBox textBox7 = txtMess;
					textBox7.Text = textBox7.Text + "\r\n" + (string)m_htab["lab06"] + guestListCls2.c_teamname;
					TextBox textBox8 = txtMess;
					textBox8.Text = textBox8.Text + "\r\n" + (string)m_htab["lab07"] + guestListCls2.c_teamguide;
					TextBox textBox9 = txtMess;
					textBox9.Text = textBox9.Text + "\r\n" + (string)m_htab["lab08"] + guestListCls2.c_teamtel;
				}
			}
		}
		catch
		{
		}
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		try
		{
			chkPop.CheckedChanged -= chkPop_CheckedChanged;
			chkPop.Checked = false;
			chkPop.CheckedChanged -= chkPop_CheckedChanged;
			if (curInd < tCount - 1)
			{
				curInd++;
				guestListCls guestListCls2 = (guestListCls)rnlist[curInd];
				labrn.Text = guestListCls2.c_rn.Trim();
				label3.Text = curInd + 1 + "/" + tCount;
				txtMess.Text = (string)m_htab["lab01"] + guestListCls2.c_comedate;
				TextBox textBox = txtMess;
				textBox.Text = textBox.Text + "\r\n" + (string)m_htab["lab02"] + guestListCls2.c_leveldate;
				TextBox textBox2 = txtMess;
				textBox2.Text = textBox2.Text + "\r\n" + (string)m_htab["lab03"] + guestListCls2.c_gsd;
				if (guestListCls2.c_hr)
				{
					TextBox textBox3 = txtMess;
					textBox3.Text = textBox3.Text + " " + (string)Program.m_hPubTab["InfoHour"];
				}
				else
				{
					TextBox textBox4 = txtMess;
					textBox4.Text = textBox4.Text + " " + (string)Program.m_hPubTab["InfoDay"];
				}
				TextBox textBox5 = txtMess;
				string text = textBox5.Text;
				textBox5.Text = text + "\r\n" + (string)m_htab["lab04"] + guestListCls2.c_tcc + " " + guestListCls2.c_total.ToString("F2");
				TextBox textBox6 = txtMess;
				string text2 = textBox6.Text;
				textBox6.Text = text2 + "\r\n" + (string)m_htab["lab05"] + guestListCls2.c_pcc + " " + guestListCls2.c_paid.ToString("F2");
				if (guestListCls2.c_team)
				{
					TextBox textBox7 = txtMess;
					textBox7.Text = textBox7.Text + "\r\n" + (string)m_htab["lab06"] + guestListCls2.c_teamname;
					TextBox textBox8 = txtMess;
					textBox8.Text = textBox8.Text + "\r\n" + (string)m_htab["lab07"] + guestListCls2.c_teamguide;
					TextBox textBox9 = txtMess;
					textBox9.Text = textBox9.Text + "\r\n" + (string)m_htab["lab08"] + guestListCls2.c_teamtel;
				}
			}
		}
		catch
		{
		}
	}

	private void chkPop_CheckedChanged(object sender, EventArgs e)
	{
		Program.fm.m_PopOldMess = !chkPop.Checked;
		if (chkPop.Checked)
		{
			Program.fm.cur_rnList.Add(labrn.Text);
		}
		else
		{
			Program.fm.cur_rnList.Remove(labrn.Text);
		}
	}

	private void labPO_Click(object sender, EventArgs e)
	{
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
	}

	private void frmPop_Shown(object sender, EventArgs e)
	{
	}

	private void labrn_MouseEnter(object sender, EventArgs e)
	{
		try
		{
			tTipPO.ToolTipTitle = (string)m_htab["tipT"];
			tTipPO.SetToolTip(labrn, (string)m_htab["tipM"]);
		}
		catch
		{
		}
	}

	private void labrn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		try
		{
			if (!(labrn.Text.Trim() == ""))
			{
				frmSGuest frmSGuest2 = new frmSGuest();
				frmSGuest2.StartPosition = FormStartPosition.CenterScreen;
				frmSGuest2.Text = (frmSGuest2.m_tmpVal = labrn.Text.Trim());
				frmSGuest2.m_tmpCon = " And TR_Level = 0 ";
				frmSGuest2.clsBackPanel1.Visible = false;
				frmSGuest2.TopMost = true;
				frmSGuest2.ShowDialog();
			}
		}
		catch
		{
		}
	}
}
