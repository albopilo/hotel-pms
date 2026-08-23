using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ComponentDll;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmMCMgr : Form
{
	private IContainer components;

	private TabControl tabMain;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Label label2;

	private DateTimePicker dtpCDate;

	private Panel panConfirm;

	private CheckBox chkWK;

	private Label label3;

	private TextBox txtWN;

	private CheckBox chkLW;

	private Label label4;

	private TextBox txtCerNum;

	private ComboBox cobCer;

	private TextBox txtUser;

	private Label label1;

	private ComboBox cobCT;

	private Label label5;

	private Label label6;

	private clsBackPanel cbpline01;

	private Label label8;

	private clsBackPanel clsBackPanel2;

	private Label label7;

	private clsBackPanel clsBackPanel1;

	private TextBox txtLC;

	private Label label11;

	private DateTimePicker dtpNew;

	private Label label10;

	private LockSoftware.Controls.GlassBtn btnClose;

	private LockSoftware.Controls.GlassBtn btnCard;

	private clsBackPanel clsBackPanel3;

	private Label label9;

	private CheckBox chkSync;

	private Timer tSync;

	private SplitContainer splitContainer1;

	private clsBackPanel clsBackPanel4;

	private TreeView tvList;

	private DataGridView dgvList;

	private TextBox txtSRn;

	private ComboBox cobType;

	private ToolsBtn btnSear;

	private ImageList imgListTV;

	private NGlassBtn btnChoAll;

	private Label labCTInfo;

	private LockSoftware.Controls.GlassBtn btnGetMaxNum;

	private RadioButton rbAll;

	private RadioButton rbSingle;

	private Panel panDGV;

	private clsBackPanel clsBackPanel5;

	private ListView lvGrp;

	private FlowLayoutPanel flowLayoutPanel1;

	private NGlassBtn btnClear;

	private NGlassBtn btnDel;

	private CheckBox chkSG;

	private CheckBox chkAG;

	private DataGridView dgvGrp;

	private Label label12;

	private clsBackPanel clsBackPanel6;

	private FlowLayoutPanel flowLayoutPanel2;

	private FlowLayoutPanel flowLayoutPanel3;

	private FlowLayoutPanel flowLayoutPanel4;

	private Panel panel4;

	private FlowLayoutPanel flowLayoutPanel5;

	private LockSoftware.Controls.GlassBtn btnRead;

	private NGlassBtn btnIDCard;

	private SplitContainer splitContainer2;

	private Panel pan_0;

	private Panel pan_3;

	private Panel pan_2;

	private Panel pan_4;

	private Panel pan_5;

	private Panel panel1;

	public string m_objName = "WFmc";

	public Hashtable m_htab;

	private Label lb_1 = new Label();

	private bool cursel;

	private int m_ct = -1;

	private int temHeigh;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmMCMgr));
		this.tabMain = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.clsBackPanel3 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
		this.label11 = new System.Windows.Forms.Label();
		this.txtLC = new System.Windows.Forms.TextBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.rbSingle = new System.Windows.Forms.RadioButton();
		this.rbAll = new System.Windows.Forms.RadioButton();
		this.btnGetMaxNum = new LockSoftware.Controls.GlassBtn(this.components);
		this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
		this.label10 = new System.Windows.Forms.Label();
		this.dtpNew = new System.Windows.Forms.DateTimePicker();
		this.chkSync = new System.Windows.Forms.CheckBox();
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.chkLW = new System.Windows.Forms.CheckBox();
		this.txtWN = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.chkWK = new System.Windows.Forms.CheckBox();
		this.panDGV = new System.Windows.Forms.Panel();
		this.clsBackPanel5 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.lvGrp = new System.Windows.Forms.ListView();
		this.imgListTV = new System.Windows.Forms.ImageList(this.components);
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnClear = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDel = new LockSoftware.Controls.NGlassBtn(this.components);
		this.chkSG = new System.Windows.Forms.CheckBox();
		this.chkAG = new System.Windows.Forms.CheckBox();
		this.dgvGrp = new System.Windows.Forms.DataGridView();
		this.label12 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.tvList = new System.Windows.Forms.TreeView();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.clsBackPanel4 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnChoAll = new LockSoftware.Controls.NGlassBtn(this.components);
		this.txtSRn = new System.Windows.Forms.TextBox();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.btnSear = new LockSoftware.Controls.ToolsBtn(this.components);
		this.cobCT = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
		this.labCTInfo = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCerNum = new System.Windows.Forms.TextBox();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.txtUser = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpCDate = new System.Windows.Forms.DateTimePicker();
		this.panConfirm = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnIDCard = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnRead = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnCard = new LockSoftware.Controls.GlassBtn(this.components);
		this.tSync = new System.Windows.Forms.Timer(this.components);
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.pan_5 = new System.Windows.Forms.Panel();
		this.clsBackPanel6 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.pan_2 = new System.Windows.Forms.Panel();
		this.pan_4 = new System.Windows.Forms.Panel();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.pan_3 = new System.Windows.Forms.Panel();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.pan_0 = new System.Windows.Forms.Panel();
		this.cbpline01 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.tabMain.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.flowLayoutPanel4.SuspendLayout();
		this.panel4.SuspendLayout();
		this.flowLayoutPanel3.SuspendLayout();
		this.flowLayoutPanel2.SuspendLayout();
		this.panDGV.SuspendLayout();
		this.clsBackPanel5.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvGrp).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.clsBackPanel4.SuspendLayout();
		this.flowLayoutPanel5.SuspendLayout();
		this.panConfirm.SuspendLayout();
		this.panel1.SuspendLayout();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		this.pan_5.SuspendLayout();
		this.pan_2.SuspendLayout();
		this.pan_4.SuspendLayout();
		this.pan_3.SuspendLayout();
		this.pan_0.SuspendLayout();
		base.SuspendLayout();
		this.tabMain.Controls.Add(this.tabPage1);
		this.tabMain.Controls.Add(this.tabPage2);
		this.tabMain.HotTrack = true;
		this.tabMain.Location = new System.Drawing.Point(746, 10);
		this.tabMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tabMain.Multiline = true;
		this.tabMain.Name = "tabMain";
		this.tabMain.SelectedIndex = 0;
		this.tabMain.Size = new System.Drawing.Size(320, 87);
		this.tabMain.TabIndex = 2;
		this.tabMain.Visible = false;
		this.tabMain.Click += new System.EventHandler(tabMain_Click);
		this.tabPage1.AutoScroll = true;
		this.tabPage1.Controls.Add(this.clsBackPanel3);
		this.tabPage1.Location = new System.Drawing.Point(4, 44);
		this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tabPage1.Size = new System.Drawing.Size(312, 39);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "授权卡 | 封锁卡 | 读记录卡 | 时间卡 | 挂失卡 | 组号设置卡";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.clsBackPanel3.AutoScroll = true;
		this.clsBackPanel3.Border = true;
		this.clsBackPanel3.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderBW = 1;
		this.clsBackPanel3.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderLW = 0;
		this.clsBackPanel3.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderRW = 1;
		this.clsBackPanel3.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderTW = 0;
		this.clsBackPanel3.Color1 = System.Drawing.Color.White;
		this.clsBackPanel3.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel3.ColorAngle = 90f;
		this.clsBackPanel3.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.clsBackPanel3.Location = new System.Drawing.Point(6, 8);
		this.clsBackPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel3.Name = "clsBackPanel3";
		this.clsBackPanel3.Size = new System.Drawing.Size(206, 27);
		this.clsBackPanel3.TabIndex = 49;
		this.tabPage2.Location = new System.Drawing.Point(4, 40);
		this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tabPage2.Size = new System.Drawing.Size(312, 43);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "房号卡";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.flowLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel4.AutoScroll = true;
		this.flowLayoutPanel4.Controls.Add(this.label11);
		this.flowLayoutPanel4.Controls.Add(this.txtLC);
		this.flowLayoutPanel4.Controls.Add(this.panel4);
		this.flowLayoutPanel4.Controls.Add(this.btnGetMaxNum);
		this.flowLayoutPanel4.Location = new System.Drawing.Point(26, 48);
		this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.flowLayoutPanel4.Name = "flowLayoutPanel4";
		this.flowLayoutPanel4.Size = new System.Drawing.Size(568, 70);
		this.flowLayoutPanel4.TabIndex = 57;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label11.Location = new System.Drawing.Point(3, 0);
		this.label11.Name = "label11";
		this.label11.Padding = new System.Windows.Forms.Padding(0, 21, 0, 0);
		this.label11.Size = new System.Drawing.Size(78, 37);
		this.label11.TabIndex = 46;
		this.label11.Text = "挂失卡号：";
		this.txtLC.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtLC.Location = new System.Drawing.Point(87, 15);
		this.txtLC.Margin = new System.Windows.Forms.Padding(3, 15, 3, 4);
		this.txtLC.MaxLength = 8;
		this.txtLC.Name = "txtLC";
		this.txtLC.Size = new System.Drawing.Size(94, 24);
		this.txtLC.TabIndex = 47;
		this.txtLC.Text = "1";
		this.txtLC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLC_KeyPress);
		this.panel4.AutoSize = true;
		this.panel4.Controls.Add(this.rbSingle);
		this.panel4.Controls.Add(this.rbAll);
		this.panel4.Location = new System.Drawing.Point(187, 4);
		this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(204, 54);
		this.panel4.TabIndex = 58;
		this.rbSingle.AutoSize = true;
		this.rbSingle.Checked = true;
		this.rbSingle.Location = new System.Drawing.Point(3, 4);
		this.rbSingle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.rbSingle.Name = "rbSingle";
		this.rbSingle.Size = new System.Drawing.Size(123, 19);
		this.rbSingle.TabIndex = 49;
		this.rbSingle.TabStop = true;
		this.rbSingle.Text = "Lost current number";
		this.rbSingle.UseVisualStyleBackColor = true;
		this.rbAll.AutoSize = true;
		this.rbAll.Location = new System.Drawing.Point(3, 31);
		this.rbAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.rbAll.Name = "rbAll";
		this.rbAll.Size = new System.Drawing.Size(198, 19);
		this.rbAll.TabIndex = 50;
		this.rbAll.Text = "Lost all smaller than current number";
		this.rbAll.UseVisualStyleBackColor = true;
		this.btnGetMaxNum.BackColor = System.Drawing.Color.Gainsboro;
		this.btnGetMaxNum.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnGetMaxNum.ForeColor = System.Drawing.Color.Black;
		this.btnGetMaxNum.GlowColor = System.Drawing.Color.White;
		this.btnGetMaxNum.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnGetMaxNum.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnGetMaxNum.Location = new System.Drawing.Point(397, 6);
		this.btnGetMaxNum.Margin = new System.Windows.Forms.Padding(3, 6, 3, 4);
		this.btnGetMaxNum.Name = "btnGetMaxNum";
		this.btnGetMaxNum.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnGetMaxNum.Size = new System.Drawing.Size(131, 48);
		this.btnGetMaxNum.TabIndex = 51;
		this.btnGetMaxNum.Text = "Max Number";
		this.btnGetMaxNum.Click += new System.EventHandler(btnGetMaxNum_Click);
		this.flowLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel3.AutoScroll = true;
		this.flowLayoutPanel3.Controls.Add(this.label10);
		this.flowLayoutPanel3.Controls.Add(this.dtpNew);
		this.flowLayoutPanel3.Controls.Add(this.chkSync);
		this.flowLayoutPanel3.Location = new System.Drawing.Point(26, 48);
		this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.flowLayoutPanel3.Name = "flowLayoutPanel3";
		this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		this.flowLayoutPanel3.Size = new System.Drawing.Size(671, 50);
		this.flowLayoutPanel3.TabIndex = 56;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label10.Location = new System.Drawing.Point(3, 4);
		this.label10.Name = "label10";
		this.label10.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
		this.label10.Size = new System.Drawing.Size(78, 26);
		this.label10.TabIndex = 44;
		this.label10.Text = "新锁时间：";
		this.dtpNew.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpNew.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.dtpNew.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpNew.Location = new System.Drawing.Point(87, 8);
		this.dtpNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpNew.Name = "dtpNew";
		this.dtpNew.Size = new System.Drawing.Size(163, 24);
		this.dtpNew.TabIndex = 45;
		this.chkSync.AutoSize = true;
		this.chkSync.Location = new System.Drawing.Point(256, 8);
		this.chkSync.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.chkSync.Name = "chkSync";
		this.chkSync.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.chkSync.Size = new System.Drawing.Size(98, 24);
		this.chkSync.TabIndex = 48;
		this.chkSync.Text = "同步系统时间";
		this.chkSync.UseVisualStyleBackColor = true;
		this.chkSync.CheckedChanged += new System.EventHandler(chkSync_CheckedChanged);
		this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel2.AutoScroll = true;
		this.flowLayoutPanel2.Controls.Add(this.chkLW);
		this.flowLayoutPanel2.Controls.Add(this.txtWN);
		this.flowLayoutPanel2.Controls.Add(this.label3);
		this.flowLayoutPanel2.Controls.Add(this.chkWK);
		this.flowLayoutPanel2.Location = new System.Drawing.Point(26, 48);
		this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		this.flowLayoutPanel2.Size = new System.Drawing.Size(666, 48);
		this.flowLayoutPanel2.TabIndex = 55;
		this.chkLW.AutoSize = true;
		this.chkLW.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.chkLW.Location = new System.Drawing.Point(3, 8);
		this.chkLW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.chkLW.Name = "chkLW";
		this.chkLW.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.chkLW.Size = new System.Drawing.Size(97, 25);
		this.chkLW.TabIndex = 0;
		this.chkLW.Text = "假锁报警→";
		this.chkLW.UseVisualStyleBackColor = true;
		this.chkLW.CheckedChanged += new System.EventHandler(chkLW_CheckedChanged);
		this.txtWN.Enabled = false;
		this.txtWN.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtWN.Location = new System.Drawing.Point(106, 8);
		this.txtWN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtWN.MaxLength = 2;
		this.txtWN.Name = "txtWN";
		this.txtWN.Size = new System.Drawing.Size(72, 24);
		this.txtWN.TabIndex = 1;
		this.txtWN.Text = "1";
		this.txtWN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtWN_KeyPress);
		this.txtWN.Leave += new System.EventHandler(txtWN_Leave);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label3.Location = new System.Drawing.Point(184, 4);
		this.label3.Name = "label3";
		this.label3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
		this.label3.Size = new System.Drawing.Size(22, 26);
		this.label3.TabIndex = 2;
		this.label3.Text = "次";
		this.chkWK.AutoSize = true;
		this.chkWK.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.chkWK.Location = new System.Drawing.Point(212, 8);
		this.chkWK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.chkWK.Name = "chkWK";
		this.chkWK.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.chkWK.Size = new System.Drawing.Size(83, 25);
		this.chkWK.TabIndex = 3;
		this.chkWK.Text = "一直报警";
		this.chkWK.UseVisualStyleBackColor = true;
		this.panDGV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panDGV.Controls.Add(this.clsBackPanel5);
		this.panDGV.Controls.Add(this.dgvGrp);
		this.panDGV.Location = new System.Drawing.Point(26, 36);
		this.panDGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.panDGV.Name = "panDGV";
		this.panDGV.Size = new System.Drawing.Size(591, 17);
		this.panDGV.TabIndex = 54;
		this.clsBackPanel5.Border = true;
		this.clsBackPanel5.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderBW = 1;
		this.clsBackPanel5.BorderColorBottom = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderColorLeft = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderColorRight = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderColorTop = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderLW = 0;
		this.clsBackPanel5.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderRW = 1;
		this.clsBackPanel5.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderTW = 1;
		this.clsBackPanel5.Color1 = System.Drawing.Color.White;
		this.clsBackPanel5.Color2 = System.Drawing.Color.Beige;
		this.clsBackPanel5.ColorAngle = 90f;
		this.clsBackPanel5.Controls.Add(this.lvGrp);
		this.clsBackPanel5.Controls.Add(this.flowLayoutPanel1);
		this.clsBackPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.clsBackPanel5.Location = new System.Drawing.Point(331, 0);
		this.clsBackPanel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel5.Name = "clsBackPanel5";
		this.clsBackPanel5.Padding = new System.Windows.Forms.Padding(1);
		this.clsBackPanel5.Size = new System.Drawing.Size(260, 17);
		this.clsBackPanel5.TabIndex = 11;
		this.lvGrp.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lvGrp.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvGrp.LargeImageList = this.imgListTV;
		this.lvGrp.Location = new System.Drawing.Point(1, 1);
		this.lvGrp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.lvGrp.Name = "lvGrp";
		this.lvGrp.Size = new System.Drawing.Size(258, 0);
		this.lvGrp.SmallImageList = this.imgListTV;
		this.lvGrp.TabIndex = 3;
		this.lvGrp.UseCompatibleStateImageBehavior = false;
		this.imgListTV.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgListTV.ImageStream");
		this.imgListTV.TransparentColor = System.Drawing.Color.Transparent;
		this.imgListTV.Images.SetKeyName(0, "OS00.png");
		this.imgListTV.Images.SetKeyName(1, "46.png");
		this.imgListTV.Images.SetKeyName(2, "ok.png");
		this.imgListTV.Images.SetKeyName(3, "SNOW E AQUA PUBLIC.png");
		this.flowLayoutPanel1.Controls.Add(this.btnClear);
		this.flowLayoutPanel1.Controls.Add(this.btnDel);
		this.flowLayoutPanel1.Controls.Add(this.chkSG);
		this.flowLayoutPanel1.Controls.Add(this.chkAG);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(1, -24);
		this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(6, 2, 6, 6);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(258, 40);
		this.flowLayoutPanel1.TabIndex = 0;
		this.btnClear.BackColor = System.Drawing.Color.Transparent;
		this.btnClear.BaseColor = System.Drawing.Color.White;
		this.btnClear.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnClear.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnClear.ButtonText = null;
		this.btnClear.CornerRadius = 2;
		this.btnClear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClear.Image = LockSoftware.Properties.Resources.clear;
		this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnClear.ImageSize = new System.Drawing.Size(16, 16);
		this.btnClear.Location = new System.Drawing.Point(217, 3);
		this.btnClear.Margin = new System.Windows.Forms.Padding(1);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(28, 30);
		this.btnClear.TabIndex = 8;
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		this.btnClear.MouseLeave += new System.EventHandler(btnClear_MouseLeave);
		this.btnClear.MouseMove += new System.Windows.Forms.MouseEventHandler(btnClear_MouseMove);
		this.btnDel.BackColor = System.Drawing.Color.Transparent;
		this.btnDel.BaseColor = System.Drawing.Color.White;
		this.btnDel.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnDel.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnDel.ButtonText = null;
		this.btnDel.CornerRadius = 2;
		this.btnDel.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDel.Image = LockSoftware.Properties.Resources.delete;
		this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnDel.ImageSize = new System.Drawing.Size(16, 16);
		this.btnDel.Location = new System.Drawing.Point(187, 3);
		this.btnDel.Margin = new System.Windows.Forms.Padding(1);
		this.btnDel.Name = "btnDel";
		this.btnDel.Size = new System.Drawing.Size(28, 30);
		this.btnDel.TabIndex = 9;
		this.btnDel.Click += new System.EventHandler(btnDel_Click);
		this.btnDel.MouseLeave += new System.EventHandler(btnDel_MouseLeave);
		this.btnDel.MouseMove += new System.Windows.Forms.MouseEventHandler(btnDel_MouseMove);
		this.chkSG.AutoSize = true;
		this.chkSG.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.chkSG.Location = new System.Drawing.Point(98, 6);
		this.chkSG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.chkSG.Name = "chkSG";
		this.chkSG.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
		this.chkSG.Size = new System.Drawing.Size(85, 21);
		this.chkSG.TabIndex = 1;
		this.chkSG.Text = "Set Group";
		this.chkSG.UseVisualStyleBackColor = true;
		this.chkAG.AutoSize = true;
		this.chkAG.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.chkAG.Location = new System.Drawing.Point(4, 6);
		this.chkAG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.chkAG.Name = "chkAG";
		this.chkAG.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
		this.chkAG.Size = new System.Drawing.Size(88, 21);
		this.chkAG.TabIndex = 0;
		this.chkAG.Text = "All Groups";
		this.chkAG.UseVisualStyleBackColor = true;
		this.dgvGrp.AllowUserToAddRows = false;
		this.dgvGrp.AllowUserToDeleteRows = false;
		this.dgvGrp.BackgroundColor = System.Drawing.Color.White;
		this.dgvGrp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvGrp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvGrp.Dock = System.Windows.Forms.DockStyle.Left;
		this.dgvGrp.Location = new System.Drawing.Point(0, 0);
		this.dgvGrp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dgvGrp.MultiSelect = false;
		this.dgvGrp.Name = "dgvGrp";
		this.dgvGrp.ReadOnly = true;
		this.dgvGrp.RowHeadersWidth = 25;
		this.dgvGrp.RowTemplate.Height = 23;
		this.dgvGrp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvGrp.Size = new System.Drawing.Size(331, 17);
		this.dgvGrp.TabIndex = 10;
		this.dgvGrp.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvGrp_CellDoubleClick);
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label12.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label12.ForeColor = System.Drawing.Color.Green;
		this.label12.Location = new System.Drawing.Point(22, 7);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(595, 24);
		this.label12.TabIndex = 53;
		this.label12.Text = "组号设置卡";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label6.ForeColor = System.Drawing.Color.Green;
		this.label6.Location = new System.Drawing.Point(22, 7);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(670, 36);
		this.label6.TabIndex = 37;
		this.label6.Text = "授权卡";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label8.ForeColor = System.Drawing.Color.Green;
		this.label8.Location = new System.Drawing.Point(22, 7);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(572, 36);
		this.label8.TabIndex = 41;
		this.label8.Text = "挂失卡";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label7.ForeColor = System.Drawing.Color.Green;
		this.label7.Location = new System.Drawing.Point(22, 7);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(675, 36);
		this.label7.TabIndex = 39;
		this.label7.Text = "时间卡";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(20, 0);
		this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.tvList);
		this.splitContainer1.Panel1MinSize = 100;
		this.splitContainer1.Panel2.Controls.Add(this.dgvList);
		this.splitContainer1.Panel2.Controls.Add(this.clsBackPanel4);
		this.splitContainer1.Panel2MinSize = 100;
		this.splitContainer1.Size = new System.Drawing.Size(559, 102);
		this.splitContainer1.SplitterDistance = 200;
		this.splitContainer1.TabIndex = 12;
		this.tvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tvList.ImageIndex = 0;
		this.tvList.ImageList = this.imgListTV;
		this.tvList.Location = new System.Drawing.Point(0, 0);
		this.tvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.tvList.Name = "tvList";
		this.tvList.SelectedImageIndex = 0;
		this.tvList.Size = new System.Drawing.Size(200, 102);
		this.tvList.TabIndex = 0;
		this.tvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvList_AfterSelect);
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 0);
		this.dgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dgvList.Name = "dgvList";
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvList.Size = new System.Drawing.Size(355, 44);
		this.dgvList.TabIndex = 0;
		this.clsBackPanel4.Border = true;
		this.clsBackPanel4.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel4.BorderBW = 1;
		this.clsBackPanel4.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel4.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel4.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel4.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel4.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel4.BorderLW = 1;
		this.clsBackPanel4.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel4.BorderRW = 1;
		this.clsBackPanel4.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel4.BorderTW = 1;
		this.clsBackPanel4.Color1 = System.Drawing.Color.White;
		this.clsBackPanel4.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel4.ColorAngle = 90f;
		this.clsBackPanel4.Controls.Add(this.btnChoAll);
		this.clsBackPanel4.Controls.Add(this.txtSRn);
		this.clsBackPanel4.Controls.Add(this.cobType);
		this.clsBackPanel4.Controls.Add(this.btnSear);
		this.clsBackPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel4.Location = new System.Drawing.Point(0, 44);
		this.clsBackPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel4.Name = "clsBackPanel4";
		this.clsBackPanel4.Size = new System.Drawing.Size(355, 58);
		this.clsBackPanel4.TabIndex = 10;
		this.btnChoAll.BackColor = System.Drawing.Color.Transparent;
		this.btnChoAll.BaseColor = System.Drawing.Color.White;
		this.btnChoAll.ButtonColor = System.Drawing.Color.Green;
		this.btnChoAll.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnChoAll.ButtonText = "Choose All";
		this.btnChoAll.CornerRadius = 2;
		this.btnChoAll.ForeColor = System.Drawing.Color.Black;
		this.btnChoAll.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnChoAll.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.btnChoAll.ImageSize = new System.Drawing.Size(16, 16);
		this.btnChoAll.Location = new System.Drawing.Point(8, 10);
		this.btnChoAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnChoAll.Name = "btnChoAll";
		this.btnChoAll.Size = new System.Drawing.Size(120, 38);
		this.btnChoAll.TabIndex = 9;
		this.btnChoAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnChoAll.Click += new System.EventHandler(btnChoAll_Click);
		this.txtSRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSRn.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtSRn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtSRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtSRn.Location = new System.Drawing.Point(293, 15);
		this.txtSRn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtSRn.Name = "txtSRn";
		this.txtSRn.Size = new System.Drawing.Size(102, 24);
		this.txtSRn.TabIndex = 8;
		this.txtSRn.Text = "ROOM NAME...";
		this.txtSRn.Enter += new System.EventHandler(txtSRn_Enter);
		this.txtSRn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSRn_KeyDown);
		this.txtSRn.Leave += new System.EventHandler(txtSRn_Leave);
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 180;
		this.cobType.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(147, 15);
		this.cobType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(138, 24);
		this.cobType.TabIndex = 5;
		this.btnSear.BackColor = System.Drawing.Color.Transparent;
		this.btnSear.Checked = false;
		this.btnSear.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnSear.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnSear.DefaultColor = System.Drawing.Color.Transparent;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSear.ImageNew = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSear.ImageRedrawed = true;
		this.btnSear.ImageStyle = 0;
		this.btnSear.isButton = true;
		this.btnSear.Location = new System.Drawing.Point(141, 6);
		this.btnSear.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnSear.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnSear.MouseDownStartColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnSear.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnSear.Name = "btnSear";
		this.btnSear.Size = new System.Drawing.Size(301, 45);
		this.btnSear.TabIndex = 7;
		this.btnSear.TextImageLocation = 0;
		this.btnSear.TextNew = "";
		this.btnSear.TextRedrawed = false;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.cobCT.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.cobCT.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobCT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCT.DropDownWidth = 220;
		this.cobCT.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobCT.FormattingEnabled = true;
		this.cobCT.Items.AddRange(new object[8] { "授权卡", "时间卡", "挂失卡", "读记录卡", "封锁卡", "退房卡", "组号设置卡", "房号卡" });
		this.cobCT.Location = new System.Drawing.Point(112, 15);
		this.cobCT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobCT.Name = "cobCT";
		this.cobCT.Size = new System.Drawing.Size(185, 24);
		this.cobCT.TabIndex = 5;
		this.cobCT.SelectedIndexChanged += new System.EventHandler(cobCT_SelectedIndexChanged);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label5.Location = new System.Drawing.Point(6, 3);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(100, 48);
		this.label5.TabIndex = 4;
		this.label5.Text = "卡片类型：";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.flowLayoutPanel5.Controls.Add(this.label5);
		this.flowLayoutPanel5.Controls.Add(this.cobCT);
		this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.flowLayoutPanel5.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.flowLayoutPanel5.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel5.Name = "flowLayoutPanel5";
		this.flowLayoutPanel5.Padding = new System.Windows.Forms.Padding(3);
		this.flowLayoutPanel5.Size = new System.Drawing.Size(310, 60);
		this.flowLayoutPanel5.TabIndex = 51;
		this.labCTInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labCTInfo.ForeColor = System.Drawing.Color.DarkRed;
		this.labCTInfo.Location = new System.Drawing.Point(361, -2);
		this.labCTInfo.Name = "labCTInfo";
		this.labCTInfo.Size = new System.Drawing.Size(897, 64);
		this.labCTInfo.TabIndex = 50;
		this.labCTInfo.Text = "content";
		this.labCTInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.ForeColor = System.Drawing.Color.DarkRed;
		this.label9.Location = new System.Drawing.Point(310, 2);
		this.label9.Margin = new System.Windows.Forms.Padding(310, 2, 0, 2);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(48, 58);
		this.label9.TabIndex = 48;
		this.label9.Text = "注:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label2.Location = new System.Drawing.Point(377, 7);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(119, 32);
		this.label2.TabIndex = 3;
		this.label2.Text = "卡片有效期：";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label4.Location = new System.Drawing.Point(5, 49);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(154, 32);
		this.label4.TabIndex = 9;
		this.label4.Text = "证件类型：";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCerNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtCerNum.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtCerNum.Location = new System.Drawing.Point(254, 55);
		this.txtCerNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtCerNum.MaxLength = 50;
		this.txtCerNum.Name = "txtCerNum";
		this.txtCerNum.Size = new System.Drawing.Size(108, 24);
		this.txtCerNum.TabIndex = 8;
		this.cobCer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 150;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(167, 54);
		this.cobCer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(81, 24);
		this.cobCer.TabIndex = 7;
		this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtUser.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtUser.Location = new System.Drawing.Point(173, 17);
		this.txtUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtUser.MaxLength = 50;
		this.txtUser.Name = "txtUser";
		this.txtUser.Size = new System.Drawing.Size(154, 24);
		this.txtUser.TabIndex = 6;
		this.txtUser.TextChanged += new System.EventHandler(txtUser_TextChanged);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label1.Location = new System.Drawing.Point(5, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(154, 32);
		this.label1.TabIndex = 5;
		this.label1.Text = "持卡人：";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpCDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpCDate.CustomFormat = "yyyy-MM-dd HH:ss";
		this.dtpCDate.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.dtpCDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCDate.Location = new System.Drawing.Point(499, 11);
		this.dtpCDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpCDate.Name = "dtpCDate";
		this.dtpCDate.Size = new System.Drawing.Size(191, 24);
		this.dtpCDate.TabIndex = 4;
		this.dtpCDate.Value = new System.DateTime(2014, 6, 13, 0, 0, 0, 0);
		this.dtpCDate.ValueChanged += new System.EventHandler(dtpCDate_ValueChanged);
		this.panConfirm.Controls.Add(this.panel1);
		this.panConfirm.Controls.Add(this.btnRead);
		this.panConfirm.Controls.Add(this.dtpCDate);
		this.panConfirm.Controls.Add(this.label2);
		this.panConfirm.Controls.Add(this.btnClose);
		this.panConfirm.Controls.Add(this.btnCard);
		this.panConfirm.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panConfirm.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.panConfirm.Location = new System.Drawing.Point(0, 297);
		this.panConfirm.Margin = new System.Windows.Forms.Padding(0);
		this.panConfirm.Name = "panConfirm";
		this.panConfirm.Size = new System.Drawing.Size(704, 98);
		this.panConfirm.TabIndex = 5;
		this.panel1.Controls.Add(this.btnIDCard);
		this.panel1.Controls.Add(this.txtUser);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.cobCer);
		this.panel1.Controls.Add(this.txtCerNum);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(371, 98);
		this.panel1.TabIndex = 60;
		this.btnIDCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnIDCard.BackColor = System.Drawing.Color.Transparent;
		this.btnIDCard.BaseColor = System.Drawing.Color.White;
		this.btnIDCard.ButtonColor = System.Drawing.Color.Silver;
		this.btnIDCard.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnIDCard.ButtonText = null;
		this.btnIDCard.CornerRadius = 2;
		this.btnIDCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIDCard.Image = LockSoftware.Properties.Resources.V_Cer;
		this.btnIDCard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnIDCard.Location = new System.Drawing.Point(327, 12);
		this.btnIDCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnIDCard.Name = "btnIDCard";
		this.btnIDCard.Size = new System.Drawing.Size(35, 32);
		this.btnIDCard.TabIndex = 59;
		this.btnIDCard.Click += new System.EventHandler(btnIDCard_Click);
		this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRead.AutoSize = true;
		this.btnRead.BackColor = System.Drawing.Color.LightGray;
		this.btnRead.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnRead.ForeColor = System.Drawing.Color.Black;
		this.btnRead.GlowColor = System.Drawing.Color.White;
		this.btnRead.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRead.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnRead.Location = new System.Drawing.Point(393, 48);
		this.btnRead.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnRead.Name = "btnRead";
		this.btnRead.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnRead.Size = new System.Drawing.Size(98, 40);
		this.btnRead.TabIndex = 58;
		this.btnRead.Text = "读 卡";
		this.btnRead.Click += new System.EventHandler(btnRead_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(603, 48);
		this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(87, 40);
		this.btnClose.TabIndex = 5;
		this.btnClose.Text = "关 闭";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCard.AutoSize = true;
		this.btnCard.BackColor = System.Drawing.Color.LightGray;
		this.btnCard.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnCard.ForeColor = System.Drawing.Color.Black;
		this.btnCard.GlowColor = System.Drawing.Color.White;
		this.btnCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCard.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCard.Location = new System.Drawing.Point(498, 48);
		this.btnCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnCard.Name = "btnCard";
		this.btnCard.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCard.Size = new System.Drawing.Size(98, 40);
		this.btnCard.TabIndex = 4;
		this.btnCard.Text = "写 卡";
		this.btnCard.Click += new System.EventHandler(btnCard_Click);
		this.tSync.Interval = 500;
		this.tSync.Tick += new System.EventHandler(tSync_Tick);
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer2.IsSplitterFixed = true;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.label9);
		this.splitContainer2.Panel1.Controls.Add(this.labCTInfo);
		this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel5);
		this.splitContainer2.Panel1MinSize = 60;
		this.splitContainer2.Panel2.Controls.Add(this.pan_5);
		this.splitContainer2.Panel2.Controls.Add(this.pan_2);
		this.splitContainer2.Panel2.Controls.Add(this.pan_4);
		this.splitContainer2.Panel2.Controls.Add(this.pan_3);
		this.splitContainer2.Panel2.Controls.Add(this.pan_0);
		this.splitContainer2.Panel2.Controls.Add(this.tabMain);
		this.splitContainer2.Panel2MinSize = 20;
		this.splitContainer2.Size = new System.Drawing.Size(704, 297);
		this.splitContainer2.SplitterDistance = 60;
		this.splitContainer2.TabIndex = 6;
		this.pan_5.Controls.Add(this.label12);
		this.pan_5.Controls.Add(this.clsBackPanel6);
		this.pan_5.Controls.Add(this.panDGV);
		this.pan_5.Location = new System.Drawing.Point(5, 269);
		this.pan_5.Name = "pan_5";
		this.pan_5.Size = new System.Drawing.Size(637, 60);
		this.pan_5.TabIndex = 59;
		this.pan_5.Visible = false;
		this.clsBackPanel6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.clsBackPanel6.Border = false;
		this.clsBackPanel6.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderBW = 1;
		this.clsBackPanel6.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderLW = 1;
		this.clsBackPanel6.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderRW = 1;
		this.clsBackPanel6.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderTW = 1;
		this.clsBackPanel6.Color1 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel6.Color2 = System.Drawing.Color.Black;
		this.clsBackPanel6.ColorAngle = 135f;
		this.clsBackPanel6.Location = new System.Drawing.Point(26, 33);
		this.clsBackPanel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel6.Name = "clsBackPanel6";
		this.clsBackPanel6.Size = new System.Drawing.Size(591, 1);
		this.clsBackPanel6.TabIndex = 52;
		this.pan_2.Controls.Add(this.splitContainer1);
		this.pan_2.Location = new System.Drawing.Point(9, 62);
		this.pan_2.Name = "pan_2";
		this.pan_2.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
		this.pan_2.Size = new System.Drawing.Size(599, 102);
		this.pan_2.TabIndex = 58;
		this.pan_2.Visible = false;
		this.pan_4.Controls.Add(this.label8);
		this.pan_4.Controls.Add(this.flowLayoutPanel4);
		this.pan_4.Controls.Add(this.clsBackPanel2);
		this.pan_4.Location = new System.Drawing.Point(12, 219);
		this.pan_4.Name = "pan_4";
		this.pan_4.Size = new System.Drawing.Size(616, 28);
		this.pan_4.TabIndex = 57;
		this.pan_4.Visible = false;
		this.clsBackPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.clsBackPanel2.Border = false;
		this.clsBackPanel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderBW = 1;
		this.clsBackPanel2.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderLW = 1;
		this.clsBackPanel2.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderRW = 1;
		this.clsBackPanel2.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderTW = 1;
		this.clsBackPanel2.Color1 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel2.Color2 = System.Drawing.Color.Black;
		this.clsBackPanel2.ColorAngle = 135f;
		this.clsBackPanel2.Location = new System.Drawing.Point(26, 45);
		this.clsBackPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(568, 1);
		this.clsBackPanel2.TabIndex = 40;
		this.pan_3.Controls.Add(this.label7);
		this.pan_3.Controls.Add(this.clsBackPanel1);
		this.pan_3.Controls.Add(this.flowLayoutPanel3);
		this.pan_3.Location = new System.Drawing.Point(12, 169);
		this.pan_3.Name = "pan_3";
		this.pan_3.Size = new System.Drawing.Size(720, 25);
		this.pan_3.TabIndex = 4;
		this.pan_3.Visible = false;
		this.clsBackPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
		this.clsBackPanel1.Color1 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel1.Color2 = System.Drawing.Color.Black;
		this.clsBackPanel1.ColorAngle = 135f;
		this.clsBackPanel1.Location = new System.Drawing.Point(26, 45);
		this.clsBackPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(671, 1);
		this.clsBackPanel1.TabIndex = 38;
		this.pan_0.Controls.Add(this.label6);
		this.pan_0.Controls.Add(this.cbpline01);
		this.pan_0.Controls.Add(this.flowLayoutPanel2);
		this.pan_0.Location = new System.Drawing.Point(5, 3);
		this.pan_0.Name = "pan_0";
		this.pan_0.Size = new System.Drawing.Size(720, 34);
		this.pan_0.TabIndex = 3;
		this.pan_0.Visible = false;
		this.cbpline01.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbpline01.Border = false;
		this.cbpline01.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderBW = 1;
		this.cbpline01.BorderColorBottom = System.Drawing.Color.Gray;
		this.cbpline01.BorderColorLeft = System.Drawing.Color.Gray;
		this.cbpline01.BorderColorRight = System.Drawing.Color.Gray;
		this.cbpline01.BorderColorTop = System.Drawing.Color.Gray;
		this.cbpline01.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderLW = 1;
		this.cbpline01.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderRW = 1;
		this.cbpline01.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderTW = 1;
		this.cbpline01.Color1 = System.Drawing.Color.WhiteSmoke;
		this.cbpline01.Color2 = System.Drawing.Color.Black;
		this.cbpline01.ColorAngle = 135f;
		this.cbpline01.Location = new System.Drawing.Point(26, 45);
		this.cbpline01.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cbpline01.Name = "cbpline01";
		this.cbpline01.Size = new System.Drawing.Size(666, 1);
		this.cbpline01.TabIndex = 36;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlLight;
		base.ClientSize = new System.Drawing.Size(704, 395);
		base.Controls.Add(this.splitContainer2);
		base.Controls.Add(this.panConfirm);
		this.Font = new System.Drawing.Font("Times New Roman", 9f);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MinimumSize = new System.Drawing.Size(720, 200);
		base.Name = "frmMCMgr";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "酒店设置卡";
		base.Load += new System.EventHandler(frmMCMgr_Load);
		this.tabMain.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.flowLayoutPanel4.ResumeLayout(false);
		this.flowLayoutPanel4.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.flowLayoutPanel3.ResumeLayout(false);
		this.flowLayoutPanel3.PerformLayout();
		this.flowLayoutPanel2.ResumeLayout(false);
		this.flowLayoutPanel2.PerformLayout();
		this.panDGV.ResumeLayout(false);
		this.clsBackPanel5.ResumeLayout(false);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvGrp).EndInit();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.clsBackPanel4.ResumeLayout(false);
		this.clsBackPanel4.PerformLayout();
		this.flowLayoutPanel5.ResumeLayout(false);
		this.panConfirm.ResumeLayout(false);
		this.panConfirm.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.ResumeLayout(false);
		this.pan_5.ResumeLayout(false);
		this.pan_2.ResumeLayout(false);
		this.pan_4.ResumeLayout(false);
		this.pan_3.ResumeLayout(false);
		this.pan_0.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public frmMCMgr()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		base.Controls.Add(lb_1);
	}

	private void InitCerType()
	{
		try
		{
			cobCer.Text = "";
			cobCer.DataSource = null;
			string sql = "Select * FROM D_Cer  Where cer_flag = 0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				cobCer.DisplayMember = "cer_name";
				cobCer.ValueMember = "cer_id";
				cobCer.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void InitCardType()
	{
		cobCT.Items.Clear();
		for (int i = 0; i < 8; i++)
		{
			try
			{
				cobCT.Items.Add((string)m_htab["CT" + i.ToString("D2")]);
			}
			catch
			{
			}
		}
	}

	private void InitType()
	{
		try
		{
			cobType.Items.Clear();
			cobType.DataSource = null;
			string sql = "Select TP_ID, TP_Name From D_RoomType Where TP_Flag = 0 Order by TP_ID, TP_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["TP_ID"] = 0;
				dataRow["TP_Name"] = (string)m_htab["cobType"];
				dataTable.Rows.InsertAt(dataRow, 0);
				cobType.DisplayMember = "TP_Name";
				cobType.ValueMember = "TP_ID";
				cobType.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitTreeList()
	{
		try
		{
			tvList.Nodes.Clear();
			string text = "Select B_ID, B_HotelName,Build_ID,Build_Code, Build_Name, Build_Flag, Build_Memo, Floor_ID, Floor_Code, Floor_Name, Floor_Flag, Floor_Memo From v_HotelBF";
			text += " Where 1=1 And  IsNull(Floor_Flag,0)=0 And IsNull(Build_Flag,0) = 0";
			text += " Order by B_ID, Build_ID, Floor_ID ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			TreeNode treeNode = null;
			TreeNode treeNode2 = null;
			string text3;
			string text2 = (text3 = "");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (text2 != dataTable.Rows[i]["B_HotelName"].ToString().Trim())
				{
					text2 = dataTable.Rows[i]["B_HotelName"].ToString().Trim();
					treeNode = new TreeNode(text2, 0, 2);
					treeNode.Name = dataTable.Rows[i]["B_ID"].ToString().Trim();
					tvList.Nodes.Add(treeNode);
				}
				if (text3 != dataTable.Rows[i]["Build_Name"].ToString().Trim())
				{
					text3 = dataTable.Rows[i]["Build_Name"].ToString().Trim();
					treeNode2 = new TreeNode(text3, 1, 2);
					treeNode2.Name = dataTable.Rows[i]["Build_ID"].ToString().Trim();
					treeNode.Nodes.Add(treeNode2);
				}
				if (dataTable.Rows[i]["Floor_Name"].ToString().Trim() != "")
				{
					treeNode2?.Nodes.Add(dataTable.Rows[i]["Floor_ID"].ToString().Trim(), dataTable.Rows[i]["Floor_Name"].ToString().Trim(), 1, 2);
				}
			}
			tvList.ExpandAll();
			tvList.Select();
			dataTable.Clear();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitRoomList(TreeNode selNode, string sqlStr)
	{
		dgvList.DataSource = null;
		string text = "Select Cast(0 As bit) R_Cho, R_ID, R_Name, R_Code, R_SubCode, R_FloorID";
		text += ", Build_Name, Floor_Name, TP_Name, Floor_Code, Build_Code,Build_ID From v_HotelRooms Where 1=1 And R_flag=0";
		if (selNode != null)
		{
			if (selNode.Level == 2)
			{
				text = text + " And  R_FloorID=" + selNode.Name.ToString().Trim();
			}
			else if (selNode.Level == 1)
			{
				text = text + " And  Build_ID=" + selNode.Name.ToString().Trim();
			}
			else if (selNode.Level == 0)
			{
				text = text + " And B_ID=" + selNode.Name.ToString().Trim();
			}
		}
		text += sqlStr;
		text += " Order by Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(text);
		if (dataTable == null || dataTable.Rows.Count <= 0)
		{
			return;
		}
		dgvList.DataSource = dataTable.DefaultView;
		if (dgvList.DataSource != null && dgvList.Columns["R_Code"] != null)
		{
			DataGridViewColumn dataGridViewColumn = dgvList.Columns["R_Code"];
			DataGridViewColumn dataGridViewColumn2 = dgvList.Columns["R_SubCode"];
			DataGridViewColumn dataGridViewColumn3 = dgvList.Columns["R_FloorID"];
			DataGridViewColumn dataGridViewColumn4 = dgvList.Columns["Build_ID"];
			DataGridViewColumn dataGridViewColumn5 = dgvList.Columns["Floor_Code"];
			DataGridViewColumn dataGridViewColumn6 = dgvList.Columns["Build_Code"];
			bool flag = (dgvList.Columns["R_ID"].Visible = false);
			bool flag3 = (dataGridViewColumn6.Visible = flag);
			bool flag5 = (dataGridViewColumn5.Visible = flag3);
			bool flag7 = (dataGridViewColumn4.Visible = flag5);
			bool flag9 = (dataGridViewColumn3.Visible = flag7);
			bool visible = (dataGridViewColumn2.Visible = flag9);
			dataGridViewColumn.Visible = visible;
			DataGridViewColumn dataGridViewColumn7 = dgvList.Columns["R_Name"];
			DataGridViewColumn dataGridViewColumn8 = dgvList.Columns["TP_Name"];
			DataGridViewColumn dataGridViewColumn9 = dgvList.Columns["Build_Name"];
			bool flag12 = (dgvList.Columns["Floor_Name"].ReadOnly = true);
			bool flag14 = (dataGridViewColumn9.ReadOnly = flag12);
			bool readOnly = (dataGridViewColumn8.ReadOnly = flag14);
			dataGridViewColumn7.ReadOnly = readOnly;
			dgvList.Columns["R_Cho"].ReadOnly = false;
			for (int i = 0; i < dgvList.Columns.Count; i++)
			{
				dgvList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvList.Columns[i].Name];
			}
			dgvList.AutoResizeColumns();
		}
	}

	private void InitGroup()
	{
		try
		{
			dgvGrp.DataSource = null;
			string sql = "Select  RGT_id, RGT_name, RGT_code, createtime FROM D_RoomGroupType Where RGT_flag=0 Order by RGT_name ";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				dgvGrp.DataSource = dataTable.DefaultView;
				DataGridViewColumn dataGridViewColumn = dgvGrp.Columns["RGT_id"];
				bool visible = (dgvGrp.Columns["RGT_code"].Visible = false);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvGrp.Columns.Count; i++)
				{
					dgvGrp.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvGrp.Columns[i].Name];
				}
				dgvGrp.AutoResizeColumns();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err06"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void frmMCMgr_Load(object sender, EventArgs e)
	{
		InitCardType();
		InitCerType();
		InitType();
		InitTreeList();
		chkSG.Checked = true;
		cobCT.SelectedIndex = 0;
		dtpCDate.Value = DateTime.Now.AddDays(1.0);
		chkSync.Checked = true;
		txtSRn.Text = (string)m_htab["txtSRn"];
		btnChoAll.ButtonText = (string)m_htab["btnChoAll"];
		if (Program.m_Lan == 0)
		{
			btnIDCard.Enabled = false;
		}
	}

	private void btnCard_Click(object sender, EventArgs e)
	{
		try
		{
			if (m_ct == -1)
			{
				return;
			}
			string text = "";
			string text2 = "";
			string text3 = "";
			int num = 0;
			int num2 = -1;
			num = Program.getMaxNumber(1, showError: true);
			if (num < 0)
			{
				return;
			}
			text2 = dtpCDate.Value.ToString("yyyyMMddHHmm");
			object obj = ((cobCer.SelectedValue != null && Convert.ToInt32(cobCer.SelectedValue) >= 0) ? cobCer.SelectedValue : ((object)0));
			switch (m_ct)
			{
			case 0:
			case 1000:
				if (cobCT.SelectedIndex == 4)
				{
					text = "0100";
					break;
				}
				text = ((!chkLW.Checked) ? "00" : ((!chkWK.Checked) ? ((Convert.ToInt16(txtWN.Text.Trim()) & 0x7F) + 128).ToString("X2") : "80"));
				text = "00" + text;
				break;
			case 1:
				text = "FF";
				break;
			case 2:
			{
				if (dgvList.Rows.Count <= 0)
				{
					Program.MsgBox((string)m_htab["dgvSel"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				text = "";
				for (int i = 0; i < dgvList.Rows.Count; i++)
				{
					if ((bool)dgvList.Rows[i].Cells["R_Cho"].Value)
					{
						text = string.Format((string)m_htab["Info01"], dgvList.Rows[i].Cells["R_Name"].Value.ToString(), "\r\n", "\r\n");
						if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
						{
							break;
						}
						text = Convert.ToInt32(dgvList.Rows[i].Cells["Build_Code"].Value).ToString("X2") + Convert.ToInt32(dgvList.Rows[i].Cells["Floor_Code"].Value).ToString("X2");
						text = text + Convert.ToInt32(dgvList.Rows[i].Cells["R_Code"].Value).ToString("X2") + Convert.ToInt32(dgvList.Rows[i].Cells["R_SubCode"].Value).ToString("X2");
						num++;
						if (Program.RadioWriteCard(m_ct, num, text2, text, text.Length, Buzzer: false) != 0)
						{
							break;
						}
						text3 = string.Concat("Insert Into  T_CardManage Values(", m_ct.ToString(), ",", num.ToString(), ",N'", txtUser.Text.Trim(), "',2,", obj, ",N'", txtCerNum.Text.Trim(), "',", dgvList.Rows[i].Cells["Build_ID"].Value.ToString(), ", '", dgvList.Rows[i].Cells["Build_Code"].Value.ToString(), "', N'", dgvList.Rows[i].Cells["Build_Name"].Value.ToString(), "'");
						string text6 = text3;
						text3 = text6 + "," + dgvList.Rows[i].Cells["R_FloorID"].Value.ToString() + ", '" + dgvList.Rows[i].Cells["Floor_Code"].Value.ToString() + "', N'" + dgvList.Rows[i].Cells["Floor_Name"].Value.ToString() + "'";
						string text7 = text3;
						text3 = text7 + "," + dgvList.Rows[i].Cells["R_ID"].Value.ToString() + ",N'" + dgvList.Rows[i].Cells["R_Name"].Value.ToString() + "', '" + dgvList.Rows[i].Cells["R_Code"].Value.ToString() + "', ";
						object obj2 = text3;
						text3 = string.Concat(obj2, dgvList.Rows[i].Cells["R_SubCode"].Value.ToString(), ", 0, 0,'", text2, "',0,NUll,NULL,GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "'");
						object obj3 = text3;
						text3 = string.Concat(obj3, ",0,NULL, ", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(),'", text, "','')");
						if (SQLserver.Data_ExecuteSql(text3) <= 0)
						{
							Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
							break;
						}
						dgvList.Rows[i].Cells["R_Cho"].Value = false;
						Program.RadioDevBuzzer(1, 2);
					}
				}
				return;
			}
			case 3:
				text = dtpNew.Value.ToString("yyyyMMddHHmm");
				break;
			case 4:
				text = Convert.ToInt32(txtLC.Text.Trim()).ToString("X6");
				text = ((!rbAll.Checked) ? (text + "00") : (text + "01"));
				break;
			case 5:
				num2 = 0;
				if (!chkAG.Checked && lvGrp.Items.Count <= 0)
				{
					Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				if (chkSG.Checked)
				{
					num2++;
				}
				if (chkAG.Checked)
				{
					num2 += 2;
				}
				text = num2.ToString("X2");
				text3 = "";
				if (chkAG.Checked)
				{
					for (num2 = 0; num2 < 6; num2++)
					{
						text += "00";
						text3 += ",Null, '', 0";
					}
					break;
				}
				for (num2 = 0; num2 < lvGrp.Items.Count; num2++)
				{
					text += lvGrp.Items[num2].SubItems[1].Text.ToString();
					string text4 = text3;
					text3 = text4 + "," + lvGrp.Items[num2].SubItems[2].Text.ToString() + ",'" + lvGrp.Items[num2].SubItems[0].Text.ToString() + "'," + lvGrp.Items[num2].SubItems[3].Text.ToString();
				}
				for (; num2 < 6; num2++)
				{
					text += lvGrp.Items[lvGrp.Items.Count - 1].SubItems[1].Text.ToString();
					string text5 = text3;
					text3 = text5 + "," + lvGrp.Items[lvGrp.Items.Count - 1].SubItems[2].Text.ToString() + ",'" + lvGrp.Items[lvGrp.Items.Count - 1].SubItems[0].Text.ToString() + "'," + lvGrp.Items[lvGrp.Items.Count - 1].SubItems[3].Text.ToString();
				}
				break;
			case 7:
				text = "";
				break;
			default:
				return;
			}
			num2 = -1;
			num++;
			int num3 = m_ct;
			if (num3 == 1000)
			{
				num3 = 0;
			}
			if (Program.RadioWriteCard(num3, num, text2, text, text.Length, Buzzer: false) == 0)
			{
				text2 = Program.GetStandDate(dtpCDate.Value);
				if (m_ct == 5)
				{
					text3 = string.Concat("Insert Into  T_RoomGroupCard Values(", num.ToString(), ",N'", txtUser.Text.Trim(), "',2,", obj, ",N'", txtCerNum.Text.Trim(), "', ", Convert.ToInt16(chkSG.Checked).ToString(), ",", Convert.ToInt16(chkAG.Checked).ToString(), text3);
					object obj4 = text3;
					text3 = string.Concat(obj4, ",'", text2, "',0,GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "'");
					object obj5 = text3;
					text3 = string.Concat(obj5, ",0,Null,", Program.m_opid, ",N'", Program.m_OperName, "',GetDate(),'", text, "','')");
				}
				else
				{
					text3 = string.Concat("Insert Into  T_CardManage Values(", m_ct.ToString(), ",", num.ToString(), ",N'", txtUser.Text.Trim(), "',2,", obj, ",N'", txtCerNum.Text.Trim(), "'");
					text3 += ",Null, '', '',Null, '', ''";
					object obj6 = text3;
					text3 = string.Concat(obj6, ",Null, '', '', NUll, 0, 0,'", text2, "',0,NUll,NULL,GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "'");
					object obj7 = text3;
					text3 = string.Concat(obj7, ",0,NULL, ", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(),'", text, "','')");
				}
				if (SQLserver.Data_ExecuteSql(text3) <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					Program.RadioDevBuzzer(1, 2);
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void tSync_Tick(object sender, EventArgs e)
	{
		if (chkSync.Checked)
		{
			dtpNew.Value = DateTime.Now;
		}
	}

	private void chkSync_CheckedChanged(object sender, EventArgs e)
	{
		tSync.Enabled = chkSync.Checked;
	}

	private void cobCT_SelectedIndexChanged(object sender, EventArgs e)
	{
		string text = "";
		switch (cobCT.SelectedIndex)
		{
		case 0:
			chkLW.Focus();
			m_ct = 0;
			text = (string)m_htab["InfoCd00"];
			break;
		case 1:
			m_ct = 3;
			text = (string)m_htab["InfoCd03"];
			dtpNew.Focus();
			break;
		case 2:
			m_ct = 4;
			text = (string)m_htab["InfoCd04"] + "\r\n" + (string)m_htab["InfoCd0401"] + "\r\n" + (string)m_htab["InfoCd0402"];
			txtLC.Focus();
			break;
		case 3:
			m_ct = 1;
			text = (string)m_htab["InfoCdNL"];
			break;
		case 4:
			m_ct = 1000;
			text = (string)m_htab["InfoCdNL"];
			break;
		case 5:
			m_ct = 7;
			text = (string)m_htab["InfoCdNL"];
			break;
		case 6:
			m_ct = 5;
			text = (string)m_htab["InfoCd05"] + "\r\n" + (string)m_htab["InfoCd0501"] + "\r\n" + (string)m_htab["InfoCd0502"];
			InitGroup();
			dgvGrp.Select();
			break;
		case 7:
			m_ct = 2;
			break;
		}
		labCTInfo.Text = text;
		setPanVisible(m_ct);
	}

	private void setPanVisible(int num)
	{
		foreach (Control control3 in splitContainer2.Panel2.Controls)
		{
			control3.Visible = false;
		}
		try
		{
			Control[] array = splitContainer2.Panel2.Controls.Find("pan_" + num, searchAllChildren: false);
			Control control2 = ((array.Length > 0) ? array[0] : null);
			if (control2 != null)
			{
				control2.Dock = DockStyle.Fill;
				control2.Visible = true;
				base.Height += temHeigh;
				temHeigh = 0;
			}
			else
			{
				temHeigh = splitContainer2.Panel2.Height;
				base.Height -= splitContainer2.Panel2.Height;
			}
		}
		catch
		{
		}
	}

	private void chkLW_CheckedChanged(object sender, EventArgs e)
	{
		txtWN.Enabled = chkLW.Checked;
	}

	private void txtWN_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtWN_Leave(object sender, EventArgs e)
	{
		if (txtWN.Text.Trim() == "")
		{
			txtWN.Text = "0";
			txtWN.SelectionStart = 1;
		}
	}

	private void txtLC_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private string getSqlStr()
	{
		string text = "";
		if (cobType.SelectedIndex > 0)
		{
			text = text + " And R_TypeID=" + cobType.SelectedValue.ToString();
		}
		if (txtSRn.ForeColor == Color.Black && txtSRn.Text.Trim() != "")
		{
			text = text + " And R_Name like '" + txtSRn.Text.Trim() + "%'";
		}
		return text;
	}

	private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
	{
		try
		{
			InitRoomList(e.Node, getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void tabMain_Click(object sender, EventArgs e)
	{
		try
		{
			switch (tabMain.SelectedIndex)
			{
			case 0:
			{
				int selectedIndex = cobCT.SelectedIndex;
				cobCT.SelectedIndex = -1;
				cobCT.SelectedIndex = selectedIndex;
				break;
			}
			case 1:
				m_ct = 2;
				break;
			}
		}
		catch
		{
		}
	}

	private void txtSRn_Leave(object sender, EventArgs e)
	{
		if (txtSRn.Text.Trim() == "" || txtSRn.ForeColor == Color.DarkGray)
		{
			txtSRn.Text = (string)m_htab["txtSRn"];
			txtSRn.ForeColor = Color.DarkGray;
		}
	}

	private void txtSRn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnSear_Click(null, null);
		}
	}

	private void btnSear_Click(object sender, EventArgs e)
	{
		try
		{
			InitRoomList(tvList.SelectedNode, getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtSRn_Enter(object sender, EventArgs e)
	{
		if (txtSRn.ForeColor == Color.DarkGray)
		{
			txtSRn.Text = "";
			txtSRn.ForeColor = Color.Black;
		}
	}

	private void btnChoAll_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.DataSource != null)
			{
				cursel = !cursel;
				for (int i = 0; i < dgvList.Rows.Count; i++)
				{
					dgvList.Rows[i].Cells["R_Cho"].Value = cursel;
				}
			}
		}
		catch
		{
		}
	}

	private void btnGetMaxNum_Click(object sender, EventArgs e)
	{
		int num = Program.getMaxNumber(1, showError: true);
		if (num < 0)
		{
			num = 0;
		}
		txtLC.Text = num.ToString();
	}

	private void dgvGrp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (lvGrp.Items.Count < 6 && e.RowIndex != -1)
			{
				string[] items = new string[4]
				{
					dgvGrp.Rows[e.RowIndex].Cells[1].Value.ToString(),
					Convert.ToInt32(dgvGrp.Rows[e.RowIndex].Cells[2].Value).ToString("X2"),
					dgvGrp.Rows[e.RowIndex].Cells[0].Value.ToString(),
					dgvGrp.Rows[e.RowIndex].Cells[2].Value.ToString()
				};
				lvGrp.Items.Add(new ListViewItem(items, 3));
			}
		}
		catch
		{
		}
	}

	private void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			for (int num = lvGrp.SelectedItems.Count - 1; num >= 0; num--)
			{
				lvGrp.SelectedItems[num].Remove();
			}
		}
		catch
		{
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		try
		{
			lvGrp.Items.Clear();
		}
		catch
		{
		}
	}

	private void btnRead_Click(object sender, EventArgs e)
	{
		object[] retdata = new object[256];
		Program.RadioReadCard(retdata, Buzzer: true, 1);
	}

	private void btnIDCard_Click(object sender, EventArgs e)
	{
		try
		{
			TextBox textBox = txtCerNum;
			string text = (txtUser.Text = "");
			textBox.Text = text;
			Program.IDCardData CardMsg = default(Program.IDCardData);
			if (Program.Get_IDCardII_Information(ref CardMsg) >= 0)
			{
				txtUser.Text = CardMsg.Name.Trim();
				txtCerNum.Text = CardMsg.IDCardNo;
			}
		}
		catch
		{
		}
	}

	private void btnDel_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(btnDel.Location.X + tabMain.Location.X + clsBackPanel3.Location.X + clsBackPanel5.Location.X + panDGV.Location.X + flowLayoutPanel1.Location.X, btnDel.Location.Y + tabMain.Location.Y + clsBackPanel3.Location.Y + clsBackPanel5.Location.Y + panDGV.Location.Y + flowLayoutPanel1.Location.Y);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_1_delete"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnDel_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void btnClear_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(btnClear.Location.X + tabMain.Location.X + clsBackPanel3.Location.X + clsBackPanel5.Location.X + panDGV.Location.X + flowLayoutPanel1.Location.X, btnClear.Location.Y + tabMain.Location.Y + clsBackPanel3.Location.Y + clsBackPanel5.Location.Y + panDGV.Location.Y + flowLayoutPanel1.Location.Y);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_1_clear"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnClear_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void dtpCDate_ValueChanged(object sender, EventArgs e)
	{
		if (dtpCDate.Value.TimeOfDay.Ticks != 0)
		{
			dtpCDate.Value = dtpCDate.Value.Date;
		}
	}

	private void txtUser_TextChanged(object sender, EventArgs e)
	{
		if (txtUser.Text.Trim().Length > 50)
		{
			Program.MsgBox(string.Format(Program.GetFormatStringShow("MaxInputN"), 50), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtUser.Text = txtUser.Text.Trim().Substring(0, 50);
		}
	}
}
