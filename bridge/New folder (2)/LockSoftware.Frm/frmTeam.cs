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

public class frmTeam : Form
{
	private IContainer components;

	private Label label1;

	private TextBox txtNTM;

	private Label label2;

	private TextBox txtNGuide;

	private Label label3;

	private ComboBox cobCer;

	private Label label4;

	private TextBox txtNCernum;

	private Label label5;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label6;

	private TextBox txtNTel;

	private TextBox txtNFax;

	private Label label7;

	private Label label8;

	private TextBox txtNMail;

	private TextBox txtNOth;

	private LockSoftware.Controls.GlassBtn btnSTB;

	private LockSoftware.Controls.GlassBtn btnNHide;

	private Label label9;

	private Label label10;

	private TextBox txtNMemo;

	private TextBox txtTBN;

	private Label label11;

	private TextBox txtCPer;

	private Label label12;

	private TextBox txtNAddr;

	private Panel panel1;

	private FlowLayoutPanel flowLayoutPanel1;

	private Label label13;

	private ComboBox cobTB;

	private NGlassBtn btnNTB;

	private DataGridView dgvTBHis;

	private LockSoftware.Controls.GlassBtn btnTBHis;

	private Panel panel2;

	private NGlassBtn btnETB;

	private NGlassBtn btnDTB;

	private Panel panel3;

	private Label label14;

	private TextBox txtTel;

	private Label label15;

	private TextBox txtFax;

	private Label label16;

	private TextBox txtMail;

	private Label label17;

	private TextBox txtOth;

	private Label label18;

	private TextBox txtMemo;

	private LockSoftware.Controls.GlassBtn btnTDel;

	private Panel cplMain;

	private Panel panel4;

	private DataGridView dgvRList;

	private ComboBox cobFD;

	private ComboBox cobBD;

	private TextBox txtSRn;

	private Panel panel6;

	private ListView lvRoom;

	private LockSoftware.Controls.GlassBtn btnSear;

	private FlowLayoutPanel flowLayoutPanel3;

	private ComboBox cobType;

	private LockSoftware.Controls.GlassBtn btnClose;

	private SplitContainer splitContainer1;

	private ToolsBtn toolsBtn3;

	private Label label19;

	private TextBox txtERn;

	private StatusStrip sstTB;

	private StatusStrip sstLR;

	private StatusStrip sstDR;

	private ToolStripStatusLabel TSSLab01;

	private ToolStripStatusLabel TSSLab02;

	private ToolStripStatusLabel TSSLab03;

	private ToolStripStatusLabel TSSLab04;

	private ToolStripStatusLabel TSSLab06;

	private ToolStripStatusLabel TSSLab05;

	private ToolStripDropDownButton TSSBtnDel;

	private ToolStripDropDownButton TSSBtnRest;

	private ToolStripStatusLabel TSSLab07;

	private ToolStripStatusLabel TSSLab08;

	private TableLayoutPanel tableLayoutPanel2;

	private Panel cplTop;

	private Panel panel5;

	private Label label20;

	private clsBackPanel cbpline01;

	private CheckBox chkInputGI;

	private FlowLayoutPanel flowLayoutPanel2;

	private ImageList imageList1;

	private Panel panel7;

	private Timer tSync;

	private TextBox txtDP;

	private TextBox txtRP;

	private Label label23;

	private TextBox txtPerCount;

	private LockSoftware.Controls.GlassBtn btnGT;

	private CheckBox chkBM;

	private CheckBox chkRW;

	private LockSoftware.Controls.GlassBtn btnGM;

	private ToolStripDropDownButton TSSBtnBR;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private Panel panel8;

	private NGlassBtn btnIDCard;

	private Panel panel9;

	private ToolsBtn btnRef;

	private TableLayoutPanel tableLayoutPanel3;

	private TextBox txtGC;

	private TextBox txtMP;

	private Label label21;

	private DateTimePicker dtpTime;

	private TextBox txtGDepo;

	private ComboBox cobCurrency;

	private DateTimePicker dtpCome;

	private Label label30;

	private NumericUpDown nudDay;

	private DateTimePicker dtpLevel;

	private Label label28;

	private Label label29;

	private Label label22;

	private CheckBox chkSync;

	private LockSoftware.Controls.GlassBtn btnMC;

	private Label labDc;

	private TextBox txtDC;

	private Panel panel10;

	private Label label34;

	private ImageList imgList;

	public string m_objName = "WFT";

	public Hashtable m_htab;

	public bool m_TBNew = true;

	public long m_TBID = -1L;

	public long m_TID = -1L;

	public bool m_Init;

	public bool m_Del;

	private Label lb_1 = new Label();

	public bool m_chVal;

	private frmTmpDlg fdlg;

	public int m_RIndex = -1;

	private double basepaid;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmTeam));
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.lvRoom = new System.Windows.Forms.ListView();
		this.imgList = new System.Windows.Forms.ImageList(this.components);
		this.sstLR = new System.Windows.Forms.StatusStrip();
		this.TSSLab03 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab04 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab05 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab06 = new System.Windows.Forms.ToolStripStatusLabel();
		this.dgvRList = new System.Windows.Forms.DataGridView();
		this.sstDR = new System.Windows.Forms.StatusStrip();
		this.TSSLab07 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab08 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSBtnBR = new System.Windows.Forms.ToolStripDropDownButton();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSBtnDel = new System.Windows.Forms.ToolStripDropDownButton();
		this.TSSBtnRest = new System.Windows.Forms.ToolStripDropDownButton();
		this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
		this.cobBD = new System.Windows.Forms.ComboBox();
		this.cobFD = new System.Windows.Forms.ComboBox();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.txtSRn = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtERn = new System.Windows.Forms.TextBox();
		this.btnSear = new LockSoftware.Controls.GlassBtn(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel6 = new System.Windows.Forms.Panel();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.chkBM = new System.Windows.Forms.CheckBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtNGuide = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtNTM = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel8 = new System.Windows.Forms.Panel();
		this.txtNCernum = new System.Windows.Forms.TextBox();
		this.panel9 = new System.Windows.Forms.Panel();
		this.btnIDCard = new LockSoftware.Controls.NGlassBtn(this.components);
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtTel = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtOth = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txtMail = new System.Windows.Forms.TextBox();
		this.txtFax = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtMemo = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.txtPerCount = new System.Windows.Forms.TextBox();
		this.btnGT = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnGM = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnTDel = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel5 = new System.Windows.Forms.Panel();
		this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
		this.txtGC = new System.Windows.Forms.TextBox();
		this.label21 = new System.Windows.Forms.Label();
		this.dtpTime = new System.Windows.Forms.DateTimePicker();
		this.txtGDepo = new System.Windows.Forms.TextBox();
		this.cobCurrency = new System.Windows.Forms.ComboBox();
		this.dtpCome = new System.Windows.Forms.DateTimePicker();
		this.label30 = new System.Windows.Forms.Label();
		this.nudDay = new System.Windows.Forms.NumericUpDown();
		this.dtpLevel = new System.Windows.Forms.DateTimePicker();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.chkSync = new System.Windows.Forms.CheckBox();
		this.labDc = new System.Windows.Forms.Label();
		this.txtMP = new System.Windows.Forms.TextBox();
		this.panel10 = new System.Windows.Forms.Panel();
		this.label34 = new System.Windows.Forms.Label();
		this.txtDC = new System.Windows.Forms.TextBox();
		this.btnMC = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.cbpline01 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.label20 = new System.Windows.Forms.Label();
		this.chkInputGI = new System.Windows.Forms.CheckBox();
		this.txtDP = new System.Windows.Forms.TextBox();
		this.txtRP = new System.Windows.Forms.TextBox();
		this.chkRW = new System.Windows.Forms.CheckBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.dgvTBHis = new System.Windows.Forms.DataGridView();
		this.sstTB = new System.Windows.Forms.StatusStrip();
		this.TSSLab01 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnTBHis = new LockSoftware.Controls.GlassBtn(this.components);
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.label13 = new System.Windows.Forms.Label();
		this.cobTB = new System.Windows.Forms.ComboBox();
		this.btnNTB = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnETB = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDTB = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnRef = new LockSoftware.Controls.ToolsBtn(this.components);
		this.cplMain = new System.Windows.Forms.Panel();
		this.toolsBtn3 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.label9 = new System.Windows.Forms.Label();
		this.txtTBN = new System.Windows.Forms.TextBox();
		this.txtNMail = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNTel = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtCPer = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtNFax = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtNAddr = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtNOth = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtNMemo = new System.Windows.Forms.TextBox();
		this.btnSTB = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnNHide = new LockSoftware.Controls.GlassBtn(this.components);
		this.cplTop = new System.Windows.Forms.Panel();
		this.tSync = new System.Windows.Forms.Timer(this.components);
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.sstLR.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRList).BeginInit();
		this.sstDR.SuspendLayout();
		this.flowLayoutPanel3.SuspendLayout();
		this.panel6.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.panel8.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel5.SuspendLayout();
		this.tableLayoutPanel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).BeginInit();
		this.panel10.SuspendLayout();
		this.flowLayoutPanel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTBHis).BeginInit();
		this.sstTB.SuspendLayout();
		this.panel3.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.cplMain.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.cplTop.SuspendLayout();
		base.SuspendLayout();
		this.panel2.AutoScroll = true;
		this.panel2.BackColor = System.Drawing.Color.Transparent;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.panel6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(331, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(671, 509);
		this.panel2.TabIndex = 9;
		this.panel4.BackColor = System.Drawing.Color.Transparent;
		this.panel4.Controls.Add(this.splitContainer1);
		this.panel4.Controls.Add(this.flowLayoutPanel3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Padding = new System.Windows.Forms.Padding(3);
		this.panel4.Size = new System.Drawing.Size(667, 293);
		this.panel4.TabIndex = 16;
		this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splitContainer1.Location = new System.Drawing.Point(3, 77);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.lvRoom);
		this.splitContainer1.Panel1.Controls.Add(this.sstLR);
		this.splitContainer1.Panel1MinSize = 60;
		this.splitContainer1.Panel2.Controls.Add(this.dgvRList);
		this.splitContainer1.Panel2.Controls.Add(this.sstDR);
		this.splitContainer1.Panel2MinSize = 100;
		this.splitContainer1.Size = new System.Drawing.Size(661, 213);
		this.splitContainer1.SplitterDistance = 206;
		this.splitContainer1.TabIndex = 0;
		this.lvRoom.CheckBoxes = true;
		this.lvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvRoom.FullRowSelect = true;
		this.lvRoom.GridLines = true;
		this.lvRoom.LargeImageList = this.imgList;
		this.lvRoom.Location = new System.Drawing.Point(0, 0);
		this.lvRoom.Name = "lvRoom";
		this.lvRoom.Size = new System.Drawing.Size(202, 179);
		this.lvRoom.TabIndex = 18;
		this.lvRoom.UseCompatibleStateImageBehavior = false;
		this.lvRoom.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(lvRoom_ItemChecked);
		this.imgList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgList.ImageStream");
		this.imgList.TransparentColor = System.Drawing.Color.Transparent;
		this.imgList.Images.SetKeyName(0, "05(1).png");
		this.imgList.Images.SetKeyName(1, "trashcan_full.ico");
		this.imgList.Images.SetKeyName(2, "synchour.png");
		this.imgList.Images.SetKeyName(3, "120px-Vista-Login_Manager.png");
		this.imgList.Images.SetKeyName(4, "54.png");
		this.imgList.Images.SetKeyName(5, "35(1).png");
		this.imgList.Images.SetKeyName(6, "Pic_07.png");
		this.imgList.Images.SetKeyName(7, "tt.ico");
		this.imgList.Images.SetKeyName(8, "v_stop.png");
		this.imgList.Images.SetKeyName(9, "Icon-1.png");
		this.imgList.Images.SetKeyName(10, "Icon-2.png");
		this.sstLR.AutoSize = false;
		this.sstLR.BackColor = System.Drawing.Color.Transparent;
		this.sstLR.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstLR.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSSLab03, this.TSSLab04, this.TSSLab05, this.TSSLab06 });
		this.sstLR.Location = new System.Drawing.Point(0, 179);
		this.sstLR.Name = "sstLR";
		this.sstLR.Size = new System.Drawing.Size(202, 30);
		this.sstLR.SizingGrip = false;
		this.sstLR.TabIndex = 19;
		this.sstLR.Text = "statusStrip1";
		this.TSSLab03.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab03.Name = "TSSLab03";
		this.TSSLab03.Size = new System.Drawing.Size(43, 25);
		this.TSSLab03.Text = "Total:";
		this.TSSLab04.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab04.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab04.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab04.Name = "TSSLab04";
		this.TSSLab04.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab04.Size = new System.Drawing.Size(41, 25);
		this.TSSLab04.Spring = true;
		this.TSSLab04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab05.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab05.Name = "TSSLab05";
		this.TSSLab05.Size = new System.Drawing.Size(62, 25);
		this.TSSLab05.Text = "Selected:";
		this.TSSLab06.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab06.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab06.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab06.Name = "TSSLab06";
		this.TSSLab06.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab06.Size = new System.Drawing.Size(41, 25);
		this.TSSLab06.Spring = true;
		this.TSSLab06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.dgvRList.AllowUserToAddRows = false;
		this.dgvRList.AllowUserToDeleteRows = false;
		this.dgvRList.BackgroundColor = System.Drawing.Color.White;
		this.dgvRList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvRList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvRList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvRList.Location = new System.Drawing.Point(0, 0);
		this.dgvRList.Name = "dgvRList";
		this.dgvRList.ReadOnly = true;
		this.dgvRList.RowHeadersWidth = 25;
		this.dgvRList.RowTemplate.Height = 23;
		this.dgvRList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRList.Size = new System.Drawing.Size(447, 179);
		this.dgvRList.TabIndex = 19;
		this.sstDR.AutoSize = false;
		this.sstDR.BackColor = System.Drawing.Color.Transparent;
		this.sstDR.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstDR.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.TSSLab07, this.TSSLab08, this.TSSBtnBR, this.toolStripStatusLabel1, this.TSSBtnDel, this.TSSBtnRest });
		this.sstDR.Location = new System.Drawing.Point(0, 179);
		this.sstDR.Name = "sstDR";
		this.sstDR.Size = new System.Drawing.Size(447, 30);
		this.sstDR.SizingGrip = false;
		this.sstDR.TabIndex = 15;
		this.sstDR.Text = "statusStrip2";
		this.TSSLab07.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab07.Name = "TSSLab07";
		this.TSSLab07.Size = new System.Drawing.Size(43, 25);
		this.TSSLab07.Text = "Total:";
		this.TSSLab08.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab08.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab08.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab08.Name = "TSSLab08";
		this.TSSLab08.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab08.Size = new System.Drawing.Size(190, 25);
		this.TSSLab08.Spring = true;
		this.TSSLab08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSBtnBR.Image = LockSoftware.Properties.Resources.synchour;
		this.TSSBtnBR.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSBtnBR.Name = "TSSBtnBR";
		this.TSSBtnBR.ShowDropDownArrow = false;
		this.TSSBtnBR.Size = new System.Drawing.Size(73, 28);
		this.TSSBtnBR.Text = "Reserve";
		this.TSSBtnBR.Click += new System.EventHandler(TSSBtnBR_Click);
		this.toolStripStatusLabel1.AutoSize = false;
		this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
		this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 25);
		this.TSSBtnDel.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSSBtnDel.Image = LockSoftware.Properties.Resources.delete;
		this.TSSBtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSBtnDel.Name = "TSSBtnDel";
		this.TSSBtnDel.ShowDropDownArrow = false;
		this.TSSBtnDel.Size = new System.Drawing.Size(63, 28);
		this.TSSBtnDel.Text = "Delete";
		this.TSSBtnDel.Click += new System.EventHandler(TSSBtnDel_Click);
		this.TSSBtnRest.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSSBtnRest.Image = LockSoftware.Properties.Resources.clear;
		this.TSSBtnRest.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSBtnRest.Name = "TSSBtnRest";
		this.TSSBtnRest.ShowDropDownArrow = false;
		this.TSSBtnRest.Size = new System.Drawing.Size(59, 28);
		this.TSSBtnRest.Text = "Reset";
		this.TSSBtnRest.Click += new System.EventHandler(TSSBtnRest_Click);
		this.flowLayoutPanel3.AutoSize = true;
		this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel3.Controls.Add(this.cobBD);
		this.flowLayoutPanel3.Controls.Add(this.cobFD);
		this.flowLayoutPanel3.Controls.Add(this.cobType);
		this.flowLayoutPanel3.Controls.Add(this.txtSRn);
		this.flowLayoutPanel3.Controls.Add(this.label19);
		this.flowLayoutPanel3.Controls.Add(this.txtERn);
		this.flowLayoutPanel3.Controls.Add(this.btnSear);
		this.flowLayoutPanel3.Controls.Add(this.btnClose);
		this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel3.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
		this.flowLayoutPanel3.Name = "flowLayoutPanel3";
		this.flowLayoutPanel3.Size = new System.Drawing.Size(661, 74);
		this.flowLayoutPanel3.TabIndex = 0;
		this.cobBD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBD.DropDownWidth = 180;
		this.cobBD.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobBD.FormattingEnabled = true;
		this.cobBD.Location = new System.Drawing.Point(3, 8);
		this.cobBD.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.cobBD.Name = "cobBD";
		this.cobBD.Size = new System.Drawing.Size(90, 22);
		this.cobBD.TabIndex = 1;
		this.cobBD.SelectedIndexChanged += new System.EventHandler(cobBD_SelectedIndexChanged);
		this.cobFD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFD.DropDownWidth = 180;
		this.cobFD.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobFD.FormattingEnabled = true;
		this.cobFD.Location = new System.Drawing.Point(99, 8);
		this.cobFD.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.cobFD.Name = "cobFD";
		this.cobFD.Size = new System.Drawing.Size(90, 22);
		this.cobFD.TabIndex = 2;
		this.cobType.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 180;
		this.cobType.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(195, 8);
		this.cobType.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(90, 22);
		this.cobType.TabIndex = 3;
		this.txtSRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSRn.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSRn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtSRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtSRn.Location = new System.Drawing.Point(291, 8);
		this.txtSRn.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txtSRn.Name = "txtSRn";
		this.txtSRn.Size = new System.Drawing.Size(90, 22);
		this.txtSRn.TabIndex = 4;
		this.txtSRn.Text = "ROOM NAME...";
		this.txtSRn.Enter += new System.EventHandler(txtSRn_Enter);
		this.txtSRn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSRn_KeyDown);
		this.txtSRn.Leave += new System.EventHandler(txtSRn_Leave);
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(387, 10);
		this.label19.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(16, 14);
		this.label19.TabIndex = 51;
		this.label19.Text = "→";
		this.txtERn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtERn.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtERn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtERn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtERn.Location = new System.Drawing.Point(409, 8);
		this.txtERn.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txtERn.Name = "txtERn";
		this.txtERn.Size = new System.Drawing.Size(90, 22);
		this.txtERn.TabIndex = 5;
		this.txtERn.Text = "ROOM NAME...";
		this.txtERn.Enter += new System.EventHandler(txtERn_Enter);
		this.txtERn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtERn_KeyDown);
		this.txtERn.Leave += new System.EventHandler(txtERn_Leave);
		this.btnSear.AutoSize = true;
		this.btnSear.BackColor = System.Drawing.Color.LightGray;
		this.btnSear.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSear.ForeColor = System.Drawing.Color.Black;
		this.btnSear.GlowColor = System.Drawing.Color.White;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSear.ImageIndex = 1;
		this.btnSear.ImageList = this.imageList1;
		this.btnSear.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSear.Location = new System.Drawing.Point(505, 3);
		this.btnSear.Name = "btnSear";
		this.btnSear.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSear.Size = new System.Drawing.Size(89, 30);
		this.btnSear.TabIndex = 6;
		this.btnSear.Text = "Search";
		this.btnSear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Date & Time (wormhole).ico");
		this.imageList1.Images.SetKeyName(1, "Toolbar _ Find.ico");
		this.imageList1.Images.SetKeyName(2, "search.ico");
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(3, 39);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(68, 30);
		this.btnClose.TabIndex = 7;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panel6.BackColor = System.Drawing.Color.Transparent;
		this.panel6.Controls.Add(this.tableLayoutPanel2);
		this.panel6.Controls.Add(this.panel5);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 293);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(667, 212);
		this.panel6.TabIndex = 17;
		this.tableLayoutPanel2.AutoScroll = true;
		this.tableLayoutPanel2.ColumnCount = 4;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel2.Controls.Add(this.chkBM, 2, 5);
		this.tableLayoutPanel2.Controls.Add(this.label4, 2, 1);
		this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
		this.tableLayoutPanel2.Controls.Add(this.txtNGuide, 1, 1);
		this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
		this.tableLayoutPanel2.Controls.Add(this.txtNTM, 1, 0);
		this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.panel8, 3, 1);
		this.tableLayoutPanel2.Controls.Add(this.panel9, 3, 0);
		this.tableLayoutPanel2.Controls.Add(this.label14, 0, 2);
		this.tableLayoutPanel2.Controls.Add(this.txtTel, 1, 2);
		this.tableLayoutPanel2.Controls.Add(this.label17, 2, 3);
		this.tableLayoutPanel2.Controls.Add(this.txtOth, 3, 3);
		this.tableLayoutPanel2.Controls.Add(this.label16, 2, 2);
		this.tableLayoutPanel2.Controls.Add(this.label15, 0, 3);
		this.tableLayoutPanel2.Controls.Add(this.txtMail, 3, 2);
		this.tableLayoutPanel2.Controls.Add(this.txtFax, 1, 3);
		this.tableLayoutPanel2.Controls.Add(this.label18, 0, 5);
		this.tableLayoutPanel2.Controls.Add(this.txtMemo, 1, 5);
		this.tableLayoutPanel2.Controls.Add(this.label23, 0, 4);
		this.tableLayoutPanel2.Controls.Add(this.txtPerCount, 1, 4);
		this.tableLayoutPanel2.Controls.Add(this.btnGT, 3, 5);
		this.tableLayoutPanel2.Controls.Add(this.btnGM, 3, 4);
		this.tableLayoutPanel2.Controls.Add(this.btnTDel, 2, 4);
		this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel2.Font = new System.Drawing.Font("Verdana", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
		this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 6;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(238, 212);
		this.tableLayoutPanel2.TabIndex = 9;
		this.chkBM.AutoSize = true;
		this.chkBM.Enabled = false;
		this.chkBM.Location = new System.Drawing.Point(258, 163);
		this.chkBM.Name = "chkBM";
		this.chkBM.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.chkBM.Size = new System.Drawing.Size(107, 23);
		this.chkBM.TabIndex = 15;
		this.chkBM.Text = "Break Make";
		this.chkBM.UseVisualStyleBackColor = true;
		this.chkBM.CheckedChanged += new System.EventHandler(chkBM_CheckedChanged);
		this.chkBM.TextChanged += new System.EventHandler(chkBM_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(258, 33);
		this.label4.Name = "label4";
		this.label4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label4.Size = new System.Drawing.Size(62, 25);
		this.label4.TabIndex = 6;
		this.label4.Text = "Number:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(258, 0);
		this.label3.Name = "label3";
		this.label3.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label3.Size = new System.Drawing.Size(79, 25);
		this.label3.TabIndex = 4;
		this.label3.Text = "Certificate:";
		this.txtNGuide.Location = new System.Drawing.Point(132, 38);
		this.txtNGuide.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNGuide.MaxLength = 50;
		this.txtNGuide.Name = "txtNGuide";
		this.txtNGuide.Size = new System.Drawing.Size(120, 25);
		this.txtNGuide.TabIndex = 4;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(3, 33);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label2.Size = new System.Drawing.Size(123, 25);
		this.label2.TabIndex = 2;
		this.label2.Text = "Tour Group Guide:";
		this.txtNTM.Location = new System.Drawing.Point(132, 5);
		this.txtNTM.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNTM.MaxLength = 50;
		this.txtNTM.Name = "txtNTM";
		this.txtNTM.Size = new System.Drawing.Size(120, 25);
		this.txtNTM.TabIndex = 1;
		this.txtNTM.TextChanged += new System.EventHandler(txtNTM_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label1.Size = new System.Drawing.Size(122, 25);
		this.label1.TabIndex = 0;
		this.label1.Text = "Tour Group Name:";
		this.panel8.Controls.Add(this.txtNCernum);
		this.panel8.Location = new System.Drawing.Point(368, 33);
		this.panel8.Margin = new System.Windows.Forms.Padding(0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(133, 33);
		this.panel8.TabIndex = 5;
		this.txtNCernum.Location = new System.Drawing.Point(3, 5);
		this.txtNCernum.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNCernum.MaxLength = 50;
		this.txtNCernum.Name = "txtNCernum";
		this.txtNCernum.Size = new System.Drawing.Size(120, 25);
		this.txtNCernum.TabIndex = 5;
		this.panel9.Controls.Add(this.btnIDCard);
		this.panel9.Controls.Add(this.cobCer);
		this.panel9.Location = new System.Drawing.Point(371, 3);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(130, 27);
		this.panel9.TabIndex = 2;
		this.btnIDCard.BackColor = System.Drawing.Color.Transparent;
		this.btnIDCard.BaseColor = System.Drawing.Color.White;
		this.btnIDCard.ButtonColor = System.Drawing.Color.Silver;
		this.btnIDCard.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnIDCard.ButtonText = null;
		this.btnIDCard.CornerRadius = 2;
		this.btnIDCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIDCard.Image = LockSoftware.Properties.Resources.V_Cer;
		this.btnIDCard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnIDCard.Location = new System.Drawing.Point(91, -1);
		this.btnIDCard.Name = "btnIDCard";
		this.btnIDCard.Size = new System.Drawing.Size(30, 26);
		this.btnIDCard.TabIndex = 3;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 160;
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(0, 1);
		this.cobCer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(87, 25);
		this.cobCer.TabIndex = 2;
		this.label14.AutoSize = true;
		this.label14.ForeColor = System.Drawing.Color.Green;
		this.label14.Location = new System.Drawing.Point(3, 66);
		this.label14.Name = "label14";
		this.label14.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label14.Size = new System.Drawing.Size(84, 25);
		this.label14.TabIndex = 8;
		this.label14.Text = "Telephone:";
		this.txtTel.Location = new System.Drawing.Point(132, 69);
		this.txtTel.MaxLength = 50;
		this.txtTel.Name = "txtTel";
		this.txtTel.Size = new System.Drawing.Size(120, 25);
		this.txtTel.TabIndex = 6;
		this.label17.AutoSize = true;
		this.label17.ForeColor = System.Drawing.Color.Green;
		this.label17.Location = new System.Drawing.Point(258, 97);
		this.label17.Name = "label17";
		this.label17.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label17.Size = new System.Drawing.Size(57, 25);
		this.label17.TabIndex = 14;
		this.label17.Text = "label17";
		this.txtOth.Location = new System.Drawing.Point(371, 100);
		this.txtOth.Name = "txtOth";
		this.txtOth.Size = new System.Drawing.Size(120, 25);
		this.txtOth.TabIndex = 9;
		this.label16.AutoSize = true;
		this.label16.ForeColor = System.Drawing.Color.Green;
		this.label16.Location = new System.Drawing.Point(258, 66);
		this.label16.Name = "label16";
		this.label16.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label16.Size = new System.Drawing.Size(57, 25);
		this.label16.TabIndex = 12;
		this.label16.Text = "label16";
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.Color.Green;
		this.label15.Location = new System.Drawing.Point(3, 97);
		this.label15.Name = "label15";
		this.label15.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label15.Size = new System.Drawing.Size(38, 25);
		this.label15.TabIndex = 10;
		this.label15.Text = "Fax:";
		this.txtMail.Location = new System.Drawing.Point(371, 69);
		this.txtMail.MaxLength = 50;
		this.txtMail.Name = "txtMail";
		this.txtMail.Size = new System.Drawing.Size(120, 25);
		this.txtMail.TabIndex = 7;
		this.txtFax.Location = new System.Drawing.Point(132, 100);
		this.txtFax.MaxLength = 50;
		this.txtFax.Name = "txtFax";
		this.txtFax.Size = new System.Drawing.Size(120, 25);
		this.txtFax.TabIndex = 8;
		this.label18.AutoSize = true;
		this.label18.ForeColor = System.Drawing.Color.Green;
		this.label18.Location = new System.Drawing.Point(3, 160);
		this.label18.Name = "label18";
		this.label18.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label18.Size = new System.Drawing.Size(57, 25);
		this.label18.TabIndex = 16;
		this.label18.Text = "label18";
		this.txtMemo.Location = new System.Drawing.Point(132, 163);
		this.txtMemo.Multiline = true;
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.Size = new System.Drawing.Size(120, 23);
		this.txtMemo.TabIndex = 11;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Verdana", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.ForeColor = System.Drawing.Color.Green;
		this.label23.Location = new System.Drawing.Point(3, 128);
		this.label23.Name = "label23";
		this.label23.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label23.Size = new System.Drawing.Size(64, 25);
		this.label23.TabIndex = 36;
		this.label23.Text = "label23";
		this.txtPerCount.Location = new System.Drawing.Point(132, 131);
		this.txtPerCount.MaxLength = 8;
		this.txtPerCount.Name = "txtPerCount";
		this.txtPerCount.Size = new System.Drawing.Size(120, 25);
		this.txtPerCount.TabIndex = 10;
		this.txtPerCount.Text = "1";
		this.txtPerCount.TextChanged += new System.EventHandler(txtPerCount_TextChanged);
		this.txtPerCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPerCount_KeyPress);
		this.txtPerCount.Leave += new System.EventHandler(txtPerCount_Leave);
		this.btnGT.AutoSize = true;
		this.btnGT.BackColor = System.Drawing.Color.LightGray;
		this.btnGT.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGT.ForeColor = System.Drawing.Color.Black;
		this.btnGT.GlowColor = System.Drawing.Color.White;
		this.btnGT.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnGT.Image = LockSoftware.Properties.Resources.EmployeeQuery;
		this.btnGT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGT.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnGT.Location = new System.Drawing.Point(371, 163);
		this.btnGT.Name = "btnGT";
		this.btnGT.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnGT.Size = new System.Drawing.Size(120, 26);
		this.btnGT.TabIndex = 13;
		this.btnGT.Text = "Group Team";
		this.btnGT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGT.Click += new System.EventHandler(btnGT_Click);
		this.btnGM.AutoSize = true;
		this.btnGM.BackColor = System.Drawing.Color.LightGray;
		this.btnGM.Enabled = false;
		this.btnGM.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGM.ForeColor = System.Drawing.Color.Black;
		this.btnGM.GlowColor = System.Drawing.Color.White;
		this.btnGM.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnGM.Image = LockSoftware.Properties.Resources.table_save;
		this.btnGM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGM.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnGM.Location = new System.Drawing.Point(371, 131);
		this.btnGM.Name = "btnGM";
		this.btnGM.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnGM.Size = new System.Drawing.Size(120, 26);
		this.btnGM.TabIndex = 12;
		this.btnGM.Text = "Modify";
		this.btnGM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGM.Click += new System.EventHandler(btnGM_Click);
		this.btnTDel.AutoSize = true;
		this.btnTDel.BackColor = System.Drawing.Color.LightGray;
		this.btnTDel.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnTDel.ForeColor = System.Drawing.Color.Black;
		this.btnTDel.GlowColor = System.Drawing.Color.White;
		this.btnTDel.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTDel.Image = LockSoftware.Properties.Resources.delete;
		this.btnTDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTDel.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnTDel.Location = new System.Drawing.Point(258, 131);
		this.btnTDel.Name = "btnTDel";
		this.btnTDel.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnTDel.Size = new System.Drawing.Size(94, 26);
		this.btnTDel.TabIndex = 14;
		this.btnTDel.Text = "Delete";
		this.btnTDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnTDel.Click += new System.EventHandler(btnTDel_Click);
		this.panel5.AutoScroll = true;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.tableLayoutPanel3);
		this.panel5.Controls.Add(this.panel7);
		this.panel5.Controls.Add(this.cbpline01);
		this.panel5.Controls.Add(this.flowLayoutPanel2);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel5.Location = new System.Drawing.Point(238, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(429, 212);
		this.panel5.TabIndex = 10;
		this.tableLayoutPanel3.ColumnCount = 4;
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.Controls.Add(this.txtGC, 1, 3);
		this.tableLayoutPanel3.Controls.Add(this.label21, 0, 2);
		this.tableLayoutPanel3.Controls.Add(this.dtpTime, 3, 1);
		this.tableLayoutPanel3.Controls.Add(this.txtGDepo, 2, 3);
		this.tableLayoutPanel3.Controls.Add(this.cobCurrency, 3, 3);
		this.tableLayoutPanel3.Controls.Add(this.dtpCome, 1, 0);
		this.tableLayoutPanel3.Controls.Add(this.label30, 2, 1);
		this.tableLayoutPanel3.Controls.Add(this.nudDay, 3, 0);
		this.tableLayoutPanel3.Controls.Add(this.dtpLevel, 1, 1);
		this.tableLayoutPanel3.Controls.Add(this.label28, 2, 0);
		this.tableLayoutPanel3.Controls.Add(this.label29, 0, 1);
		this.tableLayoutPanel3.Controls.Add(this.label22, 0, 3);
		this.tableLayoutPanel3.Controls.Add(this.chkSync, 0, 0);
		this.tableLayoutPanel3.Controls.Add(this.labDc, 2, 2);
		this.tableLayoutPanel3.Controls.Add(this.txtMP, 1, 2);
		this.tableLayoutPanel3.Controls.Add(this.panel10, 3, 2);
		this.tableLayoutPanel3.Controls.Add(this.btnMC, 1, 4);
		this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel3.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 29);
		this.tableLayoutPanel3.Name = "tableLayoutPanel3";
		this.tableLayoutPanel3.RowCount = 5;
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel3.Size = new System.Drawing.Size(427, 181);
		this.tableLayoutPanel3.TabIndex = 45;
		this.txtGC.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtGC.Location = new System.Drawing.Point(114, 84);
		this.txtGC.Name = "txtGC";
		this.txtGC.ReadOnly = true;
		this.txtGC.Size = new System.Drawing.Size(124, 21);
		this.txtGC.TabIndex = 10;
		this.txtGC.TextChanged += new System.EventHandler(txtGC_TextChanged);
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(3, 54);
		this.label21.Name = "label21";
		this.label21.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label21.Size = new System.Drawing.Size(41, 20);
		this.label21.TabIndex = 45;
		this.label21.Text = "label21";
		this.dtpTime.CustomFormat = "HH:mm";
		this.dtpTime.Enabled = false;
		this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpTime.Location = new System.Drawing.Point(332, 30);
		this.dtpTime.Name = "dtpTime";
		this.dtpTime.ShowUpDown = true;
		this.dtpTime.Size = new System.Drawing.Size(63, 21);
		this.dtpTime.TabIndex = 4;
		this.txtGDepo.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.txtGDepo.Location = new System.Drawing.Point(244, 86);
		this.txtGDepo.Name = "txtGDepo";
		this.txtGDepo.Size = new System.Drawing.Size(82, 21);
		this.txtGDepo.TabIndex = 6;
		this.txtGDepo.TextChanged += new System.EventHandler(txtGDepo_TextChanged);
		this.txtGDepo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGDepo_KeyPress);
		this.cobCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCurrency.FormattingEnabled = true;
		this.cobCurrency.Location = new System.Drawing.Point(332, 84);
		this.cobCurrency.Name = "cobCurrency";
		this.cobCurrency.Size = new System.Drawing.Size(68, 23);
		this.cobCurrency.TabIndex = 7;
		this.cobCurrency.SelectedValueChanged += new System.EventHandler(cobCurrency_SelectedValueChanged);
		this.dtpCome.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCome.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCome.Location = new System.Drawing.Point(114, 3);
		this.dtpCome.Name = "dtpCome";
		this.dtpCome.Size = new System.Drawing.Size(124, 21);
		this.dtpCome.TabIndex = 1;
		this.dtpCome.ValueChanged += new System.EventHandler(dtpCome_ValueChanged);
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(244, 27);
		this.label30.Name = "label30";
		this.label30.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label30.Size = new System.Drawing.Size(64, 20);
		this.label30.TabIndex = 44;
		this.label30.Text = "Level Time:";
		this.nudDay.Location = new System.Drawing.Point(332, 3);
		this.nudDay.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudDay.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.Name = "nudDay";
		this.nudDay.Size = new System.Drawing.Size(63, 21);
		this.nudDay.TabIndex = 3;
		this.nudDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.nudDay.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.ValueChanged += new System.EventHandler(nudDay_ValueChanged);
		this.dtpLevel.CustomFormat = "yyyy-MM-dd";
		this.dtpLevel.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevel.Location = new System.Drawing.Point(114, 30);
		this.dtpLevel.Name = "dtpLevel";
		this.dtpLevel.Size = new System.Drawing.Size(124, 21);
		this.dtpLevel.TabIndex = 2;
		this.dtpLevel.ValueChanged += new System.EventHandler(dtpLevel_ValueChanged);
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(244, 0);
		this.label28.Name = "label28";
		this.label28.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label28.Size = new System.Drawing.Size(56, 20);
		this.label28.TabIndex = 41;
		this.label28.Text = "Stay Day:";
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(3, 27);
		this.label29.Name = "label29";
		this.label29.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label29.Size = new System.Drawing.Size(62, 20);
		this.label29.TabIndex = 43;
		this.label29.Text = "Level Date:";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(3, 81);
		this.label22.Name = "label22";
		this.label22.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label22.Size = new System.Drawing.Size(41, 20);
		this.label22.TabIndex = 47;
		this.label22.Text = "label22";
		this.chkSync.AutoSize = true;
		this.chkSync.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkSync.Location = new System.Drawing.Point(3, 5);
		this.chkSync.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.chkSync.Name = "chkSync";
		this.chkSync.Size = new System.Drawing.Size(105, 18);
		this.chkSync.TabIndex = 52;
		this.chkSync.Text = "System Time";
		this.chkSync.UseVisualStyleBackColor = true;
		this.chkSync.CheckedChanged += new System.EventHandler(chkSync_CheckedChanged);
		this.labDc.AutoSize = true;
		this.labDc.Location = new System.Drawing.Point(244, 54);
		this.labDc.Name = "labDc";
		this.labDc.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.labDc.Size = new System.Drawing.Size(54, 20);
		this.labDc.TabIndex = 53;
		this.labDc.Text = "Discount:";
		this.txtMP.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtMP.Location = new System.Drawing.Point(114, 57);
		this.txtMP.Name = "txtMP";
		this.txtMP.ReadOnly = true;
		this.txtMP.Size = new System.Drawing.Size(124, 21);
		this.txtMP.TabIndex = 9;
		this.panel10.Controls.Add(this.label34);
		this.panel10.Controls.Add(this.txtDC);
		this.panel10.Location = new System.Drawing.Point(332, 57);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(68, 21);
		this.panel10.TabIndex = 54;
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label34.ForeColor = System.Drawing.Color.Red;
		this.label34.Location = new System.Drawing.Point(45, 3);
		this.label34.Margin = new System.Windows.Forms.Padding(3, 12, 0, 0);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(21, 14);
		this.label34.TabIndex = 48;
		this.label34.Text = "%";
		this.txtDC.Location = new System.Drawing.Point(0, 0);
		this.txtDC.MaxLength = 5;
		this.txtDC.Name = "txtDC";
		this.txtDC.Size = new System.Drawing.Size(42, 21);
		this.txtDC.TabIndex = 5;
		this.txtDC.Text = "0";
		this.txtDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDC.TextChanged += new System.EventHandler(txtDC_TextChanged);
		this.txtDC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDC_KeyPress);
		this.txtDC.Leave += new System.EventHandler(txtDC_Leave);
		this.btnMC.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.btnMC.BackColor = System.Drawing.Color.LightGray;
		this.tableLayoutPanel3.SetColumnSpan(this.btnMC, 4);
		this.btnMC.Font = new System.Drawing.Font("Times New Roman", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnMC.ForeColor = System.Drawing.Color.Black;
		this.btnMC.GlowColor = System.Drawing.Color.White;
		this.btnMC.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnMC.Image = LockSoftware.Properties.Resources.GuestIn;
		this.btnMC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnMC.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnMC.Location = new System.Drawing.Point(53, 113);
		this.btnMC.Name = "btnMC";
		this.btnMC.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnMC.Size = new System.Drawing.Size(320, 41);
		this.btnMC.TabIndex = 8;
		this.btnMC.Text = "Tour Group Check In";
		this.btnMC.Click += new System.EventHandler(btnMC_Click);
		this.panel7.AutoSize = true;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 210);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(427, 0);
		this.panel7.TabIndex = 54;
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
		this.cbpline01.Dock = System.Windows.Forms.DockStyle.Top;
		this.cbpline01.Location = new System.Drawing.Point(0, 28);
		this.cbpline01.Name = "cbpline01";
		this.cbpline01.Size = new System.Drawing.Size(427, 1);
		this.cbpline01.TabIndex = 36;
		this.flowLayoutPanel2.AutoSize = true;
		this.flowLayoutPanel2.Controls.Add(this.label20);
		this.flowLayoutPanel2.Controls.Add(this.chkInputGI);
		this.flowLayoutPanel2.Controls.Add(this.txtDP);
		this.flowLayoutPanel2.Controls.Add(this.txtRP);
		this.flowLayoutPanel2.Controls.Add(this.chkRW);
		this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel2.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Size = new System.Drawing.Size(427, 28);
		this.flowLayoutPanel2.TabIndex = 53;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label20.Location = new System.Drawing.Point(3, 0);
		this.label20.Name = "label20";
		this.label20.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
		this.label20.Size = new System.Drawing.Size(56, 22);
		this.label20.TabIndex = 0;
		this.label20.Text = "label20";
		this.chkInputGI.AutoSize = true;
		this.chkInputGI.Checked = true;
		this.chkInputGI.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkInputGI.Location = new System.Drawing.Point(65, 3);
		this.chkInputGI.Name = "chkInputGI";
		this.chkInputGI.Size = new System.Drawing.Size(178, 18);
		this.chkInputGI.TabIndex = 52;
		this.chkInputGI.Text = "Input Guest Information";
		this.chkInputGI.UseVisualStyleBackColor = true;
		this.chkInputGI.Visible = false;
		this.txtDP.Location = new System.Drawing.Point(249, 3);
		this.txtDP.Name = "txtDP";
		this.txtDP.Size = new System.Drawing.Size(31, 22);
		this.txtDP.TabIndex = 42;
		this.txtDP.Text = "0";
		this.txtDP.Visible = false;
		this.txtRP.Location = new System.Drawing.Point(286, 3);
		this.txtRP.Name = "txtRP";
		this.txtRP.Size = new System.Drawing.Size(34, 22);
		this.txtRP.TabIndex = 41;
		this.txtRP.Text = "0";
		this.txtRP.Visible = false;
		this.chkRW.AutoSize = true;
		this.chkRW.Location = new System.Drawing.Point(326, 3);
		this.chkRW.Name = "chkRW";
		this.chkRW.Size = new System.Drawing.Size(74, 18);
		this.chkRW.TabIndex = 53;
		this.chkRW.Text = "Rewrite";
		this.chkRW.UseVisualStyleBackColor = true;
		this.chkRW.Visible = false;
		this.panel1.BackColor = System.Drawing.Color.Transparent;
		this.panel1.Controls.Add(this.dgvTBHis);
		this.panel1.Controls.Add(this.sstTB);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.flowLayoutPanel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Font = new System.Drawing.Font("Verdana", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(321, 509);
		this.panel1.TabIndex = 8;
		this.dgvTBHis.AllowUserToAddRows = false;
		this.dgvTBHis.AllowUserToDeleteRows = false;
		this.dgvTBHis.BackgroundColor = System.Drawing.Color.White;
		this.dgvTBHis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvTBHis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTBHis.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvTBHis.Location = new System.Drawing.Point(0, 75);
		this.dgvTBHis.Name = "dgvTBHis";
		this.dgvTBHis.ReadOnly = true;
		this.dgvTBHis.RowHeadersVisible = false;
		this.dgvTBHis.RowHeadersWidth = 25;
		this.dgvTBHis.RowTemplate.Height = 23;
		this.dgvTBHis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTBHis.Size = new System.Drawing.Size(321, 404);
		this.dgvTBHis.TabIndex = 13;
		this.dgvTBHis.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTBHis_CellDoubleClick);
		this.dgvTBHis.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvTBHis_ColumnHeaderMouseClick);
		this.sstTB.AutoSize = false;
		this.sstTB.BackColor = System.Drawing.Color.Transparent;
		this.sstTB.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstTB.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.TSSLab01, this.TSSLab02 });
		this.sstTB.Location = new System.Drawing.Point(0, 479);
		this.sstTB.Name = "sstTB";
		this.sstTB.Size = new System.Drawing.Size(321, 30);
		this.sstTB.SizingGrip = false;
		this.sstTB.TabIndex = 9;
		this.TSSLab01.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab01.Name = "TSSLab01";
		this.TSSLab01.Size = new System.Drawing.Size(43, 25);
		this.TSSLab01.Text = "Total:";
		this.TSSLab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab02.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab02.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab02.Name = "TSSLab02";
		this.TSSLab02.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab02.Size = new System.Drawing.Size(263, 25);
		this.TSSLab02.Spring = true;
		this.TSSLab02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel3.Controls.Add(this.btnTBHis);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 31);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(321, 44);
		this.panel3.TabIndex = 8;
		this.btnTBHis.AutoSize = true;
		this.btnTBHis.BackColor = System.Drawing.Color.SteelBlue;
		this.btnTBHis.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnTBHis.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnTBHis.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTBHis.Image = LockSoftware.Properties.Resources.history;
		this.btnTBHis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTBHis.InnerBorderColor = System.Drawing.Color.WhiteSmoke;
		this.btnTBHis.Location = new System.Drawing.Point(0, 0);
		this.btnTBHis.Name = "btnTBHis";
		this.btnTBHis.OuterBorderColor = System.Drawing.Color.SteelBlue;
		this.btnTBHis.Size = new System.Drawing.Size(321, 44);
		this.btnTBHis.TabIndex = 12;
		this.btnTBHis.Text = "  History Tour Group ";
		this.btnTBHis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTBHis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnTBHis.Click += new System.EventHandler(btnTBHis_Click);
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.Controls.Add(this.label13);
		this.flowLayoutPanel1.Controls.Add(this.cobTB);
		this.flowLayoutPanel1.Controls.Add(this.btnNTB);
		this.flowLayoutPanel1.Controls.Add(this.btnETB);
		this.flowLayoutPanel1.Controls.Add(this.btnDTB);
		this.flowLayoutPanel1.Controls.Add(this.btnRef);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(321, 31);
		this.flowLayoutPanel1.TabIndex = 0;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(3, 0);
		this.label13.Margin = new System.Windows.Forms.Padding(3, 0, 2, 0);
		this.label13.Name = "label13";
		this.label13.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label13.Size = new System.Drawing.Size(100, 22);
		this.label13.TabIndex = 37;
		this.label13.Text = "Travel Bureau:";
		this.cobTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTB.DropDownWidth = 180;
		this.cobTB.FormattingEnabled = true;
		this.cobTB.Location = new System.Drawing.Point(105, 3);
		this.cobTB.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		this.cobTB.Name = "cobTB";
		this.cobTB.Size = new System.Drawing.Size(89, 25);
		this.cobTB.TabIndex = 11;
		this.btnNTB.BackColor = System.Drawing.Color.Transparent;
		this.btnNTB.BaseColor = System.Drawing.Color.White;
		this.btnNTB.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnNTB.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnNTB.ButtonText = null;
		this.btnNTB.CornerRadius = 2;
		this.btnNTB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNTB.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNTB.Image = LockSoftware.Properties.Resources.Add;
		this.btnNTB.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnNTB.ImageSize = new System.Drawing.Size(16, 16);
		this.btnNTB.Location = new System.Drawing.Point(200, 3);
		this.btnNTB.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
		this.btnNTB.Name = "btnNTB";
		this.btnNTB.Size = new System.Drawing.Size(24, 24);
		this.btnNTB.TabIndex = 14;
		this.btnNTB.Click += new System.EventHandler(btnNTB_Click);
		this.btnNTB.MouseLeave += new System.EventHandler(btnNTB_MouseLeave);
		this.btnNTB.MouseMove += new System.Windows.Forms.MouseEventHandler(btnNTB_MouseMove);
		this.btnETB.BackColor = System.Drawing.Color.Transparent;
		this.btnETB.BaseColor = System.Drawing.Color.White;
		this.btnETB.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnETB.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnETB.ButtonText = null;
		this.btnETB.CornerRadius = 2;
		this.btnETB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnETB.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnETB.Image = LockSoftware.Properties.Resources.table_save;
		this.btnETB.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnETB.ImageSize = new System.Drawing.Size(16, 16);
		this.btnETB.Location = new System.Drawing.Point(229, 3);
		this.btnETB.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
		this.btnETB.Name = "btnETB";
		this.btnETB.Size = new System.Drawing.Size(24, 24);
		this.btnETB.TabIndex = 15;
		this.btnETB.Click += new System.EventHandler(btnETB_Click);
		this.btnETB.MouseLeave += new System.EventHandler(btnETB_MouseLeave);
		this.btnETB.MouseMove += new System.Windows.Forms.MouseEventHandler(btnETB_MouseMove);
		this.btnDTB.BackColor = System.Drawing.Color.Transparent;
		this.btnDTB.BaseColor = System.Drawing.Color.White;
		this.btnDTB.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnDTB.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnDTB.ButtonText = null;
		this.btnDTB.CornerRadius = 2;
		this.btnDTB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnDTB.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDTB.Image = LockSoftware.Properties.Resources.delete;
		this.btnDTB.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnDTB.ImageSize = new System.Drawing.Size(16, 16);
		this.btnDTB.Location = new System.Drawing.Point(258, 3);
		this.btnDTB.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
		this.btnDTB.Name = "btnDTB";
		this.btnDTB.Size = new System.Drawing.Size(24, 24);
		this.btnDTB.TabIndex = 16;
		this.btnDTB.Click += new System.EventHandler(btnDTB_Click);
		this.btnDTB.MouseLeave += new System.EventHandler(btnDTB_MouseLeave);
		this.btnDTB.MouseMove += new System.Windows.Forms.MouseEventHandler(btnDTB_MouseMove);
		this.btnRef.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRef.BackColor = System.Drawing.Color.Transparent;
		this.btnRef.Checked = false;
		this.btnRef.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnRef.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRef.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRef.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRef.ImageNew = LockSoftware.Properties.Resources.Button_Refresh;
		this.btnRef.ImageRedrawed = true;
		this.btnRef.ImageStyle = 0;
		this.btnRef.isButton = true;
		this.btnRef.Location = new System.Drawing.Point(287, 3);
		this.btnRef.Margin = new System.Windows.Forms.Padding(3, 3, 1, 0);
		this.btnRef.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRef.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnRef.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRef.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRef.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRef.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRef.Name = "btnRef";
		this.btnRef.Size = new System.Drawing.Size(29, 24);
		this.btnRef.TabIndex = 17;
		this.btnRef.TextImageLocation = 0;
		this.btnRef.TextNew = "";
		this.btnRef.TextRedrawed = false;
		this.btnRef.Click += new System.EventHandler(btnRef_Click);
		this.btnRef.MouseLeave += new System.EventHandler(btnRef_MouseLeave);
		this.btnRef.MouseMove += new System.Windows.Forms.MouseEventHandler(btnRef_MouseMove);
		this.cplMain.Controls.Add(this.panel2);
		this.cplMain.Controls.Add(this.toolsBtn3);
		this.cplMain.Controls.Add(this.panel1);
		this.cplMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cplMain.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cplMain.Location = new System.Drawing.Point(3, 103);
		this.cplMain.Name = "cplMain";
		this.cplMain.Size = new System.Drawing.Size(1002, 509);
		this.cplMain.TabIndex = 2;
		this.toolsBtn3.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn3.Checked = false;
		this.toolsBtn3.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn3.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn3.Dock = System.Windows.Forms.DockStyle.Left;
		this.toolsBtn3.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn3.ImageNew = LockSoftware.Properties.Resources.mini_left;
		this.toolsBtn3.ImageRedrawed = true;
		this.toolsBtn3.ImageStyle = 0;
		this.toolsBtn3.isButton = true;
		this.toolsBtn3.Location = new System.Drawing.Point(321, 0);
		this.toolsBtn3.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn3.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn3.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn3.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn3.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn3.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn3.Name = "toolsBtn3";
		this.toolsBtn3.Size = new System.Drawing.Size(10, 509);
		this.toolsBtn3.TabIndex = 5;
		this.toolsBtn3.TextImageLocation = 0;
		this.toolsBtn3.TextNew = "";
		this.toolsBtn3.TextRedrawed = false;
		this.toolsBtn3.Click += new System.EventHandler(toolsBtn3_Click);
		this.tableLayoutPanel1.AutoScroll = true;
		this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.tableLayoutPanel1.ColumnCount = 9;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtTBN, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtNMail, 5, 0);
		this.tableLayoutPanel1.Controls.Add(this.label7, 4, 0);
		this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtNTel, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.label11, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtCPer, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtNFax, 3, 1);
		this.tableLayoutPanel1.Controls.Add(this.label12, 6, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtNAddr, 7, 0);
		this.tableLayoutPanel1.Controls.Add(this.label8, 4, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtNOth, 5, 1);
		this.tableLayoutPanel1.Controls.Add(this.label10, 6, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtNMemo, 7, 1);
		this.tableLayoutPanel1.Controls.Add(this.btnSTB, 8, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnNHide, 8, 1);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Font = new System.Drawing.Font("Verdana", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 5);
		this.tableLayoutPanel1.RowCount = 2;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(998, 96);
		this.tableLayoutPanel1.TabIndex = 1;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(8, 6);
		this.label9.Name = "label9";
		this.label9.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label9.Size = new System.Drawing.Size(110, 25);
		this.label9.TabIndex = 36;
		this.label9.Text = "Travel Bureau:";
		this.txtTBN.Location = new System.Drawing.Point(136, 11);
		this.txtTBN.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtTBN.MaxLength = 50;
		this.txtTBN.Name = "txtTBN";
		this.txtTBN.Size = new System.Drawing.Size(100, 25);
		this.txtTBN.TabIndex = 1;
		this.txtNMail.Location = new System.Drawing.Point(476, 11);
		this.txtNMail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNMail.MaxLength = 50;
		this.txtNMail.Name = "txtNMail";
		this.txtNMail.Size = new System.Drawing.Size(100, 25);
		this.txtNMail.TabIndex = 5;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(422, 6);
		this.label7.Name = "label7";
		this.label7.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label7.Size = new System.Drawing.Size(48, 24);
		this.label7.TabIndex = 12;
		this.label7.Text = "E-Mail:";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(242, 6);
		this.label5.Name = "label5";
		this.label5.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label5.Size = new System.Drawing.Size(68, 24);
		this.label5.TabIndex = 8;
		this.label5.Text = "Telephone:";
		this.txtNTel.Location = new System.Drawing.Point(316, 11);
		this.txtNTel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNTel.MaxLength = 50;
		this.txtNTel.Name = "txtNTel";
		this.txtNTel.Size = new System.Drawing.Size(100, 25);
		this.txtNTel.TabIndex = 3;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(8, 40);
		this.label11.Name = "label11";
		this.label11.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label11.Size = new System.Drawing.Size(122, 25);
		this.label11.TabIndex = 42;
		this.label11.Text = "Contact Person:";
		this.txtCPer.Location = new System.Drawing.Point(136, 45);
		this.txtCPer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtCPer.MaxLength = 50;
		this.txtCPer.Name = "txtCPer";
		this.txtCPer.Size = new System.Drawing.Size(100, 25);
		this.txtCPer.TabIndex = 2;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(242, 40);
		this.label6.Name = "label6";
		this.label6.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label6.Size = new System.Drawing.Size(32, 24);
		this.label6.TabIndex = 9;
		this.label6.Text = "Fax:";
		this.txtNFax.Location = new System.Drawing.Point(316, 45);
		this.txtNFax.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNFax.MaxLength = 50;
		this.txtNFax.Name = "txtNFax";
		this.txtNFax.Size = new System.Drawing.Size(100, 25);
		this.txtNFax.TabIndex = 4;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(582, 6);
		this.label12.Name = "label12";
		this.label12.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label12.Size = new System.Drawing.Size(72, 25);
		this.label12.TabIndex = 44;
		this.label12.Text = "Address:";
		this.txtNAddr.Location = new System.Drawing.Point(660, 9);
		this.txtNAddr.MaxLength = 50;
		this.txtNAddr.Name = "txtNAddr";
		this.txtNAddr.Size = new System.Drawing.Size(135, 25);
		this.txtNAddr.TabIndex = 7;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(422, 40);
		this.label8.Name = "label8";
		this.label8.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label8.Size = new System.Drawing.Size(43, 24);
		this.label8.TabIndex = 13;
		this.label8.Text = "Other:";
		this.txtNOth.Location = new System.Drawing.Point(476, 45);
		this.txtNOth.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNOth.MaxLength = 50;
		this.txtNOth.Name = "txtNOth";
		this.txtNOth.Size = new System.Drawing.Size(100, 25);
		this.txtNOth.TabIndex = 6;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(582, 40);
		this.label10.Name = "label10";
		this.label10.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label10.Size = new System.Drawing.Size(55, 25);
		this.label10.TabIndex = 38;
		this.label10.Text = "Memo:";
		this.txtNMemo.Location = new System.Drawing.Point(660, 45);
		this.txtNMemo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNMemo.Name = "txtNMemo";
		this.txtNMemo.Size = new System.Drawing.Size(135, 25);
		this.txtNMemo.TabIndex = 8;
		this.btnSTB.AutoSize = true;
		this.btnSTB.BackColor = System.Drawing.Color.Silver;
		this.btnSTB.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSTB.ForeColor = System.Drawing.Color.Black;
		this.btnSTB.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSTB.Image = LockSoftware.Properties.Resources.save;
		this.btnSTB.InnerBorderColor = System.Drawing.Color.White;
		this.btnSTB.Location = new System.Drawing.Point(801, 9);
		this.btnSTB.Name = "btnSTB";
		this.btnSTB.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnSTB.Size = new System.Drawing.Size(68, 28);
		this.btnSTB.TabIndex = 9;
		this.btnSTB.Text = "Save";
		this.btnSTB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSTB.Click += new System.EventHandler(btnSTB_Click);
		this.btnNHide.AutoSize = true;
		this.btnNHide.BackColor = System.Drawing.Color.Silver;
		this.btnNHide.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNHide.ForeColor = System.Drawing.Color.Black;
		this.btnNHide.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNHide.Image = LockSoftware.Properties.Resources.close;
		this.btnNHide.InnerBorderColor = System.Drawing.Color.White;
		this.btnNHide.Location = new System.Drawing.Point(801, 43);
		this.btnNHide.Name = "btnNHide";
		this.btnNHide.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnNHide.Size = new System.Drawing.Size(68, 28);
		this.btnNHide.TabIndex = 10;
		this.btnNHide.Text = "Close";
		this.btnNHide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNHide.Click += new System.EventHandler(btnNHide_Click);
		this.cplTop.BackColor = System.Drawing.Color.LightSteelBlue;
		this.cplTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.cplTop.Controls.Add(this.tableLayoutPanel1);
		this.cplTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.cplTop.Location = new System.Drawing.Point(3, 3);
		this.cplTop.Name = "cplTop";
		this.cplTop.Size = new System.Drawing.Size(1002, 100);
		this.cplTop.TabIndex = 11;
		this.cplTop.Visible = false;
		this.tSync.Interval = 500;
		this.tSync.Tick += new System.EventHandler(tSync_Tick);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.WhiteSmoke;
		base.ClientSize = new System.Drawing.Size(1008, 615);
		base.Controls.Add(this.cplMain);
		base.Controls.Add(this.cplTop);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmTeam";
		base.Padding = new System.Windows.Forms.Padding(3);
		this.Text = "团队管理";
		base.Load += new System.EventHandler(frmTeam_Load);
		this.panel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.sstLR.ResumeLayout(false);
		this.sstLR.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRList).EndInit();
		this.sstDR.ResumeLayout(false);
		this.sstDR.PerformLayout();
		this.flowLayoutPanel3.ResumeLayout(false);
		this.flowLayoutPanel3.PerformLayout();
		this.panel6.ResumeLayout(false);
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		this.panel8.ResumeLayout(false);
		this.panel8.PerformLayout();
		this.panel9.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		this.tableLayoutPanel3.ResumeLayout(false);
		this.tableLayoutPanel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).EndInit();
		this.panel10.ResumeLayout(false);
		this.panel10.PerformLayout();
		this.flowLayoutPanel2.ResumeLayout(false);
		this.flowLayoutPanel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTBHis).EndInit();
		this.sstTB.ResumeLayout(false);
		this.sstTB.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.cplMain.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.cplTop.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public frmTeam()
	{
		InitializeComponent();
		base.Controls.Add(lb_1);
		m_htab = Program.GetControlName(this, m_objName);
		txtDC.Text = Program.GetFaceDisValue();
	}

	private void InitTB()
	{
		try
		{
			panel2.Enabled = false;
			TSSLab02.Text = (string)m_htab["TSSLab02"];
			string sql = "Select * From D_TraBur Where TB_flag = 0 order by TB_name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				cobTB.DisplayMember = "TB_name";
				cobTB.ValueMember = "TB_id";
				cobTB.DataSource = dataTable.DefaultView;
				if (cobTB.Items.Count > 0)
				{
					cobTB.SelectedIndex = 0;
					panel2.Enabled = true;
					TSSLab02.Text = "";
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, label13.Text.Substring(0, label13.Text.Length - 1));
		}
	}

	private void InitCerType()
	{
		try
		{
			cobCer.Text = "";
			cobCer.DataSource = null;
			string sql = "Select * FROM D_Cer  Where cer_flag = 0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				cobCer.DisplayMember = "cer_name";
				cobCer.ValueMember = "cer_id";
				cobCer.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, label3.Text.Substring(0, label3.Text.Length - 1));
		}
	}

	private void InitCurrency()
	{
		string sql = "Select * From D_Currency Order by curr_id";
		DataTable dataTable = null;
		try
		{
			cobCurrency.Text = "";
			cobCurrency.DataSource = null;
			dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				cobCurrency.DisplayMember = "curr_code";
				cobCurrency.ValueMember = "curr_rate";
				cobCurrency.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			if (dataTable != null)
			{
				dataTable.Clear();
				dataTable.Dispose();
			}
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcolcurr_code"]);
		}
	}

	private void InitType()
	{
		try
		{
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
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcolTP_Name"]);
		}
	}

	private void InitBuild()
	{
		try
		{
			cobBD.DataSource = null;
			string sql = "Select  Build_ID, Build_Name FROM D_Build Where Build_Flag=0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Build_ID"] = 0;
				dataRow["Build_Name"] = "";
				dataTable.Rows.InsertAt(dataRow, 0);
				cobBD.DisplayMember = "Build_Name";
				cobBD.ValueMember = "Build_ID";
				cobBD.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcolBuild_Name"]);
		}
	}

	private void InitFloor(int bid)
	{
		try
		{
			cobFD.DataSource = null;
			string text = "Select * From D_Floor ";
			if (bid > 0)
			{
				text = text + " Where Build_ID=" + bid + " And Floor_Flag = 0";
			}
			text += " Order by Build_ID, Floor_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Floor_ID"] = 0;
				dataRow["Floor_Name"] = "";
				dataTable.Rows.InsertAt(dataRow, 0);
				cobFD.DisplayMember = "Floor_Name";
				cobFD.ValueMember = "Floor_ID";
				cobFD.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcolFloor_Name"]);
		}
	}

	private string getSqlStr()
	{
		string text = "";
		int num = 0;
		int num2 = 0;
		num = ((cobBD.DataSource != null) ? Convert.ToInt32(cobBD.SelectedValue) : 0);
		num2 = ((cobFD.DataSource != null) ? Convert.ToInt32(cobFD.SelectedValue) : 0);
		if (num == 0)
		{
			num2 = 0;
		}
		text = ((!chkBM.Checked) ? (" r_flag!=1 and R_RSID!=8 and R_RSID!=9 and R_RSID!=7 and R_RSID!=2 and R_ID not in (select r_id from T_Rooms where TR_Level = 0) and R_ID not in (select r_id from T_Schedule where sch_flag = 0) " + text) : (text + " r_id in (Select r_id from T_rooms Where team_id = " + m_TID + " And TR_Level = 0)"));
		if (num2 > 0)
		{
			text = text + " And  R_FloorID=" + num2;
		}
		if (num > 0)
		{
			text = text + " And  Build_ID=" + num;
		}
		if (cobType.SelectedIndex > 0)
		{
			text = text + " And R_TypeID=" + cobType.SelectedValue.ToString();
		}
		if (txtSRn.ForeColor == Color.Black && txtSRn.Text.Trim() != "")
		{
			text = ((!(txtERn.ForeColor == Color.Black) || !(txtERn.Text.Trim() != "")) ? (text + " And R_Name like '%" + txtSRn.Text.Trim() + "%'") : (text + " And R_Name >= '" + txtSRn.Text.Trim() + "'"));
		}
		if (txtERn.ForeColor == Color.Black && txtERn.Text.Trim() != "")
		{
			text = ((!(txtSRn.ForeColor == Color.Black) || !(txtSRn.Text.Trim() != "")) ? (text + " And R_Name like '%" + txtERn.Text.Trim() + "%'") : (text + " And R_Name <= '" + txtERn.Text.Trim() + "'"));
		}
		return text;
	}

	private void InitRoomList(string sqlStr)
	{
		lvRoom.Items.Clear();
		DateTime.Parse(Program.GetStandDTime(dtpCome.Value, "00")).AddMinutes(-Program.m_defClearTime);
		DateTime.Parse(Program.GetStandDate(dtpLevel.Value) + " " + Program.m_defLeaveTime).AddMinutes(Program.m_defClearTime);
		TSSLab04.Text = "";
		string sql = "Select R_Name, R_ID, R_Code, R_SubCode, R_FloorID, R_TypeID, R_RSID, R_BedAdd, R_BedSinglePrice,R_Size, R_Memo, build_ID, Build_Name, Floor_Name, TP_Name , R_CurGuestCount, R_TotalGuest, R_TotalPrice,TP_Price,TP_deposit, RS_Name000, R_MaxCardNum,Build_Code,Floor_Code, TP_BedCount From v_HotelRooms Where " + sqlStr + " Order by Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(sql);
		if (dataTable == null || dataTable.Rows.Count <= 0)
		{
			return;
		}
		ListViewItem[] array = new ListViewItem[dataTable.Rows.Count];
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			string[] array2 = new string[dataTable.Columns.Count];
			for (int j = 0; j < dataTable.Columns.Count; j++)
			{
				array2[j] = dataTable.Rows[i][j].ToString().Trim();
			}
			array[i] = new ListViewItem(array2);
		}
		lvRoom.Items.AddRange(array);
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			array[k].ImageIndex = Convert.ToInt16(dataTable.Rows[k]["R_RSID"].ToString()) - 1;
			if (array[k].ImageIndex == 2)
			{
				lvRoom.Items[k].Checked = true;
			}
		}
		TSSLab04.Text = lvRoom.Items.Count.ToString();
	}

	private void InitDgvListColumn()
	{
		try
		{
			dgvRList.Rows.Clear();
			dgvRList.Columns.Clear();
			dgvRList.Columns.Add("R_ID", "");
			dgvRList.Columns.Add("R_Name", (string)m_htab["dgvcolR_Name"]);
			dgvRList.Columns.Add("build_ID", "");
			dgvRList.Columns.Add("Build_Name", (string)m_htab["dgvcolBuild_Name"]);
			dgvRList.Columns.Add("floor_id", "");
			dgvRList.Columns.Add("Floor_Name", (string)m_htab["dgvcolFloor_Name"]);
			dgvRList.Columns.Add("TP_Name", (string)m_htab["dgvcolTP_Name"]);
			dgvRList.Columns.Add("TP_BedCount", (string)m_htab["dgvcolTP_BedCount"]);
			dgvRList.Columns.Add("R_CurGuestCount", (string)m_htab["dgvcolR_CurGuestCount"]);
			dgvRList.Columns.Add("TP_Price", (string)m_htab["TP_Price"]);
			dgvRList.Columns.Add("TP_deposit", (string)m_htab["TP_deposit"]);
			dgvRList.Columns.Add("R_BedAdd", (string)m_htab["R_BedAdd"]);
			dgvRList.Columns.Add("R_BedSinglePrice", (string)m_htab["R_BedSinglePrice"]);
			dgvRList.Columns.Add("R_Code", "");
			dgvRList.Columns.Add("R_SubCode", "");
			dgvRList.Columns.Add("Build_Code", "");
			dgvRList.Columns.Add("Floor_Code", "");
			dgvRList.Columns.Add("R_MaxCardNum", "");
			dgvRList.Columns.Add("R_RSID", "");
			DataGridViewColumn dataGridViewColumn = dgvRList.Columns["r_id"];
			DataGridViewColumn dataGridViewColumn2 = dgvRList.Columns["floor_id"];
			bool flag = (dgvRList.Columns["build_ID"].Visible = false);
			bool visible = (dataGridViewColumn2.Visible = flag);
			dataGridViewColumn.Visible = visible;
			DataGridViewColumn dataGridViewColumn3 = dgvRList.Columns["R_Code"];
			DataGridViewColumn dataGridViewColumn4 = dgvRList.Columns["floor_id"];
			bool flag4 = (dgvRList.Columns["R_SubCode"].Visible = false);
			bool visible2 = (dataGridViewColumn4.Visible = flag4);
			dataGridViewColumn3.Visible = visible2;
			DataGridViewColumn dataGridViewColumn5 = dgvRList.Columns["Build_Code"];
			DataGridViewColumn dataGridViewColumn6 = dgvRList.Columns["floor_id"];
			bool flag7 = (dgvRList.Columns["Floor_Code"].Visible = false);
			bool visible3 = (dataGridViewColumn6.Visible = flag7);
			dataGridViewColumn5.Visible = visible3;
			DataGridViewColumn dataGridViewColumn7 = dgvRList.Columns["R_RSID"];
			DataGridViewColumn dataGridViewColumn8 = dgvRList.Columns["R_MaxCardNum"];
			DataGridViewColumn dataGridViewColumn9 = dgvRList.Columns["R_BedAdd"];
			bool flag10 = (dgvRList.Columns["R_BedSinglePrice"].Visible = false);
			bool flag12 = (dataGridViewColumn9.Visible = flag10);
			bool visible4 = (dataGridViewColumn8.Visible = flag12);
			dataGridViewColumn7.Visible = visible4;
			for (int i = 0; i < dgvRList.Columns.Count; i++)
			{
				dgvRList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvRList.Columns[i].Name];
			}
			dgvRList.AutoResizeColumns();
		}
		catch
		{
		}
	}

	private void btnNHide_Click(object sender, EventArgs e)
	{
		cplTop.Visible = false;
		cplMain.Enabled = true;
	}

	private void btnNTB_Click(object sender, EventArgs e)
	{
		cplTop.Visible = true;
		cplMain.Enabled = false;
		m_TBNew = true;
		txtTBN.ReadOnly = false;
		txtTBN.Text = "";
		txtTBN.BackColor = Color.White;
		TextBox textBox = txtTBN;
		TextBox textBox2 = txtCPer;
		TextBox textBox3 = txtNTel;
		TextBox textBox4 = txtNFax;
		TextBox textBox5 = txtNMail;
		TextBox textBox6 = txtNOth;
		TextBox textBox7 = txtNAddr;
		string text = (txtNMemo.Text = "");
		string text3 = (textBox7.Text = text);
		string text5 = (textBox6.Text = text3);
		string text7 = (textBox5.Text = text5);
		string text9 = (textBox4.Text = text7);
		string text11 = (textBox3.Text = text9);
		string text13 = (textBox2.Text = text11);
		textBox.Text = text13;
	}

	private void btnETB_Click(object sender, EventArgs e)
	{
		if (cobTB.SelectedItem != null)
		{
			cplTop.Visible = true;
			cplMain.Enabled = false;
			m_TBNew = false;
			m_TBID = (long)((DataRowView)cobTB.SelectedItem).Row.ItemArray[0];
			txtTBN.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[1].ToString();
			txtCPer.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[2].ToString();
			txtNTel.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[3].ToString();
			txtNFax.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[4].ToString();
			txtNMail.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[6].ToString();
			txtNOth.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[7].ToString();
			txtNAddr.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[8].ToString();
			txtNMemo.Text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[9].ToString();
			txtTBN.ReadOnly = true;
			txtTBN.BackColor = Color.FromArgb(205, 229, 245);
		}
	}

	private void btnDTB_Click(object sender, EventArgs e)
	{
		try
		{
			if (cobTB.SelectedItem == null)
			{
				return;
			}
			long num = (long)((DataRowView)cobTB.SelectedItem).Row.ItemArray[0];
			string text = ((DataRowView)cobTB.SelectedItem).Row.ItemArray[1].ToString();
			if (Program.MsgBox(label13.Text + " " + text + "\r\n\r\n" + (string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				string text2 = "Update D_TraBur Set TB_flag = 1";
				string text3 = text2;
				text2 = text3 + ", updatetime=GetDate(), updator_id=" + Program.m_opid + ", updator=N'" + Program.m_OperName + "'";
				text2 = text2 + " Where TB_id=" + num;
				int num2 = SQLserver.Data_ExecuteSql(text2);
				if (num2 != 1)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num2, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				Program.MsgBox((string)Program.m_hPubTab["InfoDBOper"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				InitTB();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnDTB.Text);
		}
	}

	private void frmTeam_Load(object sender, EventArgs e)
	{
		btnRef_Click(null, null);
		dtpCome.CustomFormat = Program.m_currDateTimeFmt;
		dtpLevel.CustomFormat = Program.m_currDateFmt;
		if (Program.m_Lan == 0)
		{
			btnIDCard.Enabled = false;
		}
		dtpLevel.MaxDate = DateTime.Now.AddDays(9999.0);
	}

	private void btnSTB_Click(object sender, EventArgs e)
	{
		try
		{
			if (Program.isValNull(label9.Text.Substring(0, label9.Text.Length - 1), txtTBN.Text.Trim(), chk: true) || Program.isValNull(label11.Text.Substring(0, label11.Text.Length - 1), txtCPer.Text.Trim(), chk: true))
			{
				return;
			}
			string text = "";
			if (m_TBNew)
			{
				text = "Select TB_id From D_TraBur Where TB_name = N'" + txtTBN.Text.Trim().Replace("'", "''") + "' And TB_flag = 0 ";
				DataTable dataTable = SQLserver.Data_GetDataTable(text);
				if (dataTable == null)
				{
					Program.MsgCusErrMess("Data Null", label13.Text.Substring(0, label13.Text.Length - 1));
					return;
				}
				if (dataTable.Rows.Count > 0)
				{
					text = string.Format((string)m_htab["Info01"], txtTBN.Text.Trim(), "\r\n");
					Program.MsgCustom(text, MessageBoxIcon.Asterisk);
					dataTable.Dispose();
					return;
				}
				text = "Insert into D_TraBur Values( N'" + txtTBN.Text.Trim().Replace("'", "''") + "', N'" + txtCPer.Text.Trim().Replace("'", "''") + "', N'" + txtNTel.Text.Trim().Replace("'", "''") + "', N'" + txtNFax.Text.Trim().Replace("'", "''") + "', 0";
				string text2 = text;
				text = text2 + ", N'" + txtNMail.Text.Trim().Replace("'", "''") + "', N'" + txtNOth.Text.Trim().Replace("'", "''") + "', N'" + txtNAddr.Text.Trim().Replace("'", "''") + "', N'" + txtNMemo.Text.Trim().Replace("'", "''") + "'";
				string text3 = text;
				text = text3 + ", GetDate(), " + Program.m_opid + ", N'" + Program.m_OperName + "', NULL, NULL, NULL)";
			}
			else
			{
				text = "Update D_TraBur Set TB_Conn = N'" + txtCPer.Text.Trim().Replace("'", "''") + "', TB_tel = N'" + txtNTel.Text.Trim().Replace("'", "''") + "', TB_fax =N'" + txtNFax.Text.Trim().Replace("'", "''") + "'";
				string text4 = text;
				text = text4 + ", TB_mail = N'" + txtNMail.Text.Trim().Replace("'", "''") + "', TB_othConn=N'" + txtNOth.Text.Trim().Replace("'", "''") + "', TB_addr=N'" + txtNAddr.Text.Trim().Replace("'", "''") + "', TB_memo=N'" + txtNMemo.Text.Trim().Replace("'", "''") + "'";
				string text5 = text;
				text = text5 + ", updatetime=GetDate(), updator_id=" + Program.m_opid + ", updator=N'" + Program.m_OperName + "'";
				text = text + " Where TB_id=" + m_TBID;
			}
			int num = SQLserver.Data_ExecuteSql(text);
			if (num != 1)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			Program.MsgBox((string)Program.m_hPubTab["InfoDBOper"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			InitTB();
			if (!m_TBNew)
			{
				cobTB.SelectedValue = m_TBID.ToString();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnSTB.Text);
		}
	}

	private void btnTBHis_Click(object sender, EventArgs e)
	{
		try
		{
			m_TID = -1L;
			TextBox textBox = txtNTM;
			TextBox textBox2 = txtNGuide;
			TextBox textBox3 = txtNCernum;
			TextBox textBox4 = txtTel;
			TextBox textBox5 = txtFax;
			TextBox textBox6 = txtMail;
			TextBox textBox7 = txtOth;
			string text = (cobCer.Text = "");
			string text3 = (textBox7.Text = text);
			string text5 = (textBox6.Text = text3);
			string text7 = (textBox5.Text = text5);
			string text9 = (textBox4.Text = text7);
			string text11 = (textBox3.Text = text9);
			string text13 = (textBox2.Text = text11);
			textBox.Text = text13;
			txtPerCount.Text = "0";
			NumericUpDown numericUpDown = nudDay;
			DateTimePicker dateTimePicker = dtpCome;
			DateTimePicker dateTimePicker2 = dtpLevel;
			bool flag = (dtpTime.Enabled = true);
			bool flag3 = (dateTimePicker2.Enabled = flag);
			bool enabled = (dateTimePicker.Enabled = flag3);
			numericUpDown.Enabled = enabled;
			LockSoftware.Controls.GlassBtn glassBtn = btnGM;
			CheckBox checkBox = chkBM;
			bool flag6 = (chkBM.Enabled = false);
			bool enabled2 = (checkBox.Checked = flag6);
			glassBtn.Enabled = enabled2;
			nudDay.Value = Convert.ToDecimal(Program.m_defDay);
			dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
			dtpCome.Value = Convert.ToDateTime(Program.GetLocDate(DateTime.Now) + " " + Program.m_defComeTime + ":00");
			cobCurrency.Text = Program.m_baseCurrCode;
			if (cobTB.SelectedItem == null)
			{
				return;
			}
			TSSLab02.Text = "";
			long num = (long)((DataRowView)cobTB.SelectedItem).Row.ItemArray[0];
			string text15 = "Select Top 20 TB_id, TB_name, Team_id, Team_name, Team_guide, cer_name, team_cernum, Team_cometime, Team_leveltime, team_percount, team_tel, team_fax, team_mail, team_othConn, team_stand_L_time, team_discount ";
			text15 = text15 + " From v_TeamInfo Where TB_flag = 0 And team_flag=0 And TB_id=" + num;
			text15 += " Order by Team_cometime desc, TB_name, Team_name, Team_guide ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text15);
			if (dataTable == null)
			{
				return;
			}
			dgvTBHis.DataSource = dataTable.DefaultView;
			for (int i = 1; i < dgvTBHis.Columns.Count; i++)
			{
				dgvTBHis.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvTBHis.Columns[i].Name];
			}
			DataGridViewColumn dataGridViewColumn = dgvTBHis.Columns["team_stand_L_time"];
			DataGridViewColumn dataGridViewColumn2 = dgvTBHis.Columns["TB_id"];
			DataGridViewColumn dataGridViewColumn3 = dgvTBHis.Columns["TB_name"];
			bool flag9 = (dgvTBHis.Columns["Team_id"].Visible = false);
			bool flag11 = (dataGridViewColumn3.Visible = flag9);
			bool visible = (dataGridViewColumn2.Visible = flag11);
			dataGridViewColumn.Visible = visible;
			dgvTBHis.AutoResizeColumns();
			for (int j = 0; j < dgvTBHis.Rows.Count; j++)
			{
				if (dgvTBHis.Rows[j].Cells["Team_leveltime"].Value.ToString() != "")
				{
					dgvTBHis.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(224, 85, 50);
					dgvTBHis.Rows[j].DefaultCellStyle.ForeColor = Color.White;
				}
			}
			TSSLab02.Text = dgvTBHis.Rows.Count.ToString();
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnTBHis.Text);
		}
	}

	private void cobBD_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cobBD.DataSource != null)
			{
				InitFloor(Convert.ToInt32(cobBD.SelectedValue));
			}
		}
		catch
		{
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

	private void txtSRn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtERn.Select();
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

	private void btnSear_Click(object sender, EventArgs e)
	{
		m_Init = true;
		try
		{
			InitRoomList(getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnSear.Text);
		}
		m_Init = false;
	}

	private void toolsBtn3_Click(object sender, EventArgs e)
	{
		if (panel1.Visible)
		{
			toolsBtn3.ImageNew = Resources.mini_right;
			panel1.Visible = false;
		}
		else
		{
			panel1.Visible = true;
			toolsBtn3.ImageNew = Resources.mini_left;
		}
	}

	private void txtERn_Enter(object sender, EventArgs e)
	{
		if (txtERn.ForeColor == Color.DarkGray)
		{
			txtERn.Text = "";
			txtERn.ForeColor = Color.Black;
		}
	}

	private void txtERn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnSear_Click(null, null);
		}
	}

	private void txtERn_Leave(object sender, EventArgs e)
	{
		if (txtERn.Text.Trim() == "" || txtERn.ForeColor == Color.DarkGray)
		{
			txtERn.Text = (string)m_htab["txtSRn"];
			txtERn.ForeColor = Color.DarkGray;
		}
	}

	private void lvRoom_ItemChecked(object sender, ItemCheckedEventArgs e)
	{
		try
		{
			if (m_Init || m_Del || e.Item == null)
			{
				return;
			}
			if (e.Item.Checked)
			{
				for (int i = 0; i < dgvRList.Rows.Count; i++)
				{
					if (e.Item.SubItems[0].Text.Trim() == dgvRList.Rows[i].Cells["R_Name"].Value.ToString().Trim())
					{
						return;
					}
				}
				object[] values = new object[19]
				{
					e.Item.SubItems[1].Text.Trim(),
					e.Item.SubItems[0].Text.Trim(),
					e.Item.SubItems[11].Text.Trim(),
					e.Item.SubItems[12].Text.Trim(),
					e.Item.SubItems[4].Text.Trim(),
					e.Item.SubItems[13].Text.Trim(),
					e.Item.SubItems[14].Text.Trim(),
					e.Item.SubItems[24].Text.Trim(),
					e.Item.SubItems[15].Text.Trim(),
					e.Item.SubItems[18].Text.Trim(),
					e.Item.SubItems[19].Text.Trim(),
					e.Item.SubItems[7].Text.Trim(),
					e.Item.SubItems[8].Text.Trim(),
					e.Item.SubItems[2].Text.Trim(),
					e.Item.SubItems[3].Text.Trim(),
					e.Item.SubItems[22].Text.Trim(),
					e.Item.SubItems[23].Text.Trim(),
					e.Item.SubItems[21].Text.Trim(),
					e.Item.SubItems[6].Text.Trim()
				};
				dgvRList.Rows.Insert(0, values);
				dgvRList.Rows[0].DefaultCellStyle.BackColor = Color.Beige;
			}
			else
			{
				for (int j = 0; j < dgvRList.Rows.Count; j++)
				{
					if (e.Item.SubItems[0].Text.Trim() == dgvRList.Rows[j].Cells["R_Name"].Value.ToString().Trim())
					{
						dgvRList.Rows.RemoveAt(j);
						break;
					}
				}
			}
			TSSLab06.Text = lvRoom.CheckedItems.Count.ToString();
			TSSLab08.Text = dgvRList.Rows.Count.ToString();
			double num = 0.0;
			double num2 = 0.0;
			for (int k = 0; k < dgvRList.Rows.Count; k++)
			{
				if (Convert.ToInt32(dgvRList.Rows[k].Cells["R_CurGuestCount"].Value.ToString()) <= 0)
				{
					num += Convert.ToDouble(dgvRList.Rows[k].Cells["TP_Price"].Value.ToString());
					num2 += Convert.ToDouble(dgvRList.Rows[k].Cells["TP_deposit"].Value.ToString());
				}
			}
			txtRP.Text = num.ToString("F2");
			txtDP.Text = num2.ToString("F2");
			txtMP.Text = num.ToString("F2");
			txtGC.Text = num2.ToString("F2");
			double num3 = Convert.ToDouble(cobCurrency.SelectedValue);
			if (num3 == 0.0)
			{
				num3 = 1.0;
			}
			double realDisValue = Program.GetRealDisValue(txtDC.Text.Trim());
			txtGDepo.Text = ((num * Convert.ToDouble(nudDay.Value) * realDisValue + num2) / num3).ToString("F2");
		}
		catch
		{
		}
	}

	private void DelRow()
	{
		if (dgvRList.Rows.Count <= 0)
		{
			return;
		}
		string text = "";
		for (int num = dgvRList.SelectedRows.Count - 1; num >= 0; num--)
		{
			text = dgvRList.SelectedRows[num].Cells[1].Value.ToString().Trim();
			ListViewItem listViewItem = lvRoom.FindItemWithText(text, includeSubItemsInSearch: false, 0);
			if (listViewItem != null)
			{
				listViewItem.Checked = false;
			}
			dgvRList.Rows.RemoveAt(dgvRList.SelectedRows[num].Index);
		}
		TSSLab06.Text = lvRoom.CheckedItems.Count.ToString();
		TSSLab08.Text = dgvRList.Rows.Count.ToString();
		double num2 = 0.0;
		double num3 = 0.0;
		for (int i = 0; i < dgvRList.Rows.Count; i++)
		{
			if (Convert.ToInt32(dgvRList.Rows[i].Cells["R_CurGuestCount"].Value) <= 0)
			{
				num2 += Convert.ToDouble(dgvRList.Rows[i].Cells["TP_Price"].Value.ToString());
				num3 += Convert.ToDouble(dgvRList.Rows[i].Cells["TP_deposit"].Value.ToString());
			}
		}
		txtRP.Text = num2.ToString("F2");
		txtDP.Text = num3.ToString("F2");
		txtMP.Text = num2.ToString("F2");
		txtGC.Text = num3.ToString("F2");
		double num4 = Convert.ToDouble(cobCurrency.SelectedValue);
		if (num4 == 0.0)
		{
			num4 = 1.0;
		}
		double realDisValue = Program.GetRealDisValue(txtDC.Text.Trim());
		txtGDepo.Text = ((num2 * Convert.ToDouble(nudDay.Value) * realDisValue + num3) / num4).ToString("F2");
	}

	private void TSSBtnDel_Click(object sender, EventArgs e)
	{
		m_Del = true;
		try
		{
			DelRow();
		}
		catch
		{
		}
		m_Del = false;
	}

	private void TSSBtnRest_Click(object sender, EventArgs e)
	{
		m_Del = true;
		try
		{
			dgvRList.SelectAll();
			DelRow();
		}
		catch
		{
		}
		m_Del = false;
	}

	private void tSync_Tick(object sender, EventArgs e)
	{
		if (chkSync.Checked && dtpCome.Enabled)
		{
			dtpCome.Value = DateTime.Now;
		}
	}

	private void chkSync_CheckedChanged(object sender, EventArgs e)
	{
		tSync.Enabled = chkSync.Checked;
	}

	private void chRoomPrice()
	{
		try
		{
			double num = Convert.ToDouble(txtRP.Text);
			double num2 = Convert.ToDouble(txtDP.Text);
			txtMP.Text = num.ToString("F2");
			txtGC.Text = num2.ToString("F2");
			double num3 = Convert.ToDouble(cobCurrency.SelectedValue);
			if (num3 == 0.0)
			{
				num3 = 1.0;
			}
			double realDisValue = Program.GetRealDisValue(txtDC.Text.Trim());
			txtGDepo.Text = ((num * Convert.ToDouble(nudDay.Value) * realDisValue + num2) / num3).ToString("F2");
		}
		catch
		{
		}
	}

	private void dtpCome_ValueChanged(object sender, EventArgs e)
	{
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		try
		{
			dtpLevel.MaxDate = dtpCome.Value.AddDays(9999.0);
			double num = Convert.ToDouble(nudDay.Value);
			if (dtpCome.Value.ToString("HH:mm:ss").CompareTo(Program.m_defComeTime) < 0)
			{
				num--;
			}
			dtpLevel.Value = dtpCome.Value.AddDays(num);
			chRoomPrice();
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void dtpLevel_ValueChanged(object sender, EventArgs e)
	{
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		try
		{
			TimeSpan timeSpan = new TimeSpan(Convert.ToDateTime(Program.GetLocDate(dtpLevel.Value) + " " + dtpTime.Value.ToString("HH:mm:ss")).Ticks - dtpCome.Value.Ticks);
			double num = Convert.ToInt32(timeSpan.TotalDays);
			if (num < 0.0)
			{
				nudDay.Value = 1m;
				dtpLevel.Value = dtpCome.Value;
			}
			else
			{
				if (num == 0.0)
				{
					num = 1.0;
				}
				else if (num > 0.0 && dtpCome.Value.ToString("HH:mm:ss").CompareTo(Program.m_defComeTime) < 0)
				{
					num++;
				}
				nudDay.Value = Convert.ToDecimal(num);
			}
			chRoomPrice();
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void nudDay_ValueChanged(object sender, EventArgs e)
	{
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		try
		{
			double num = Convert.ToDouble(nudDay.Value);
			if (dtpCome.Value.ToString("HH:mm:ss").CompareTo(Program.m_defComeTime) < 0)
			{
				num--;
			}
			dtpLevel.Value = dtpCome.Value.AddDays(num);
			chRoomPrice();
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void txtGC_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtGDepo_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void btnMC_Click(object sender, EventArgs e)
	{
		try
		{
			if (cobTB.SelectedItem == null)
			{
				return;
			}
			if (cobCer.Items.Count <= 0)
			{
				Program.MsgCustom(string.Format((string)m_htab["Info03"], label3.Text.Substring(0, label3.Text.Length - 1)), MessageBoxIcon.Asterisk);
			}
			else
			{
				if (Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtNTM.Text.Trim(), chk: true) || Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtNGuide.Text.Trim(), chk: true) || Program.isValNull(label3.Text.Substring(0, label3.Text.Length - 1), cobCer.Text.Trim(), chk: true) || Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtNCernum.Text.Trim(), chk: true) || Program.isValNull(label23.Text.Substring(0, label23.Text.Length - 1), txtPerCount.Text.Trim(), chk: true))
				{
					return;
				}
				int num = Convert.ToInt32("0" + txtPerCount.Text.Trim());
				if (num <= 0)
				{
					Program.MsgCustom(string.Format((string)m_htab["Info03"], label23.Text.Substring(0, label23.Text.Length - 1)), MessageBoxIcon.Asterisk);
					return;
				}
				if (dgvRList.Rows.Count <= 0)
				{
					Program.MsgCustom(string.Format((string)m_htab["Info03"], (string)m_htab["dgvcolR_Name"]), MessageBoxIcon.Asterisk);
					return;
				}
				string text = label1.Text + txtNTM.Text.Trim() + "\r\n\r\n";
				text = text + label2.Text + txtNGuide.Text.Trim() + "\r\n\r\n";
				text = text + label23.Text + txtPerCount.Text.Trim() + "\r\n\r\n";
				text = (chkBM.Checked ? (text + (string)m_htab["Info15"]) : (text + (string)m_htab["Info06"]));
				if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}
				long num2 = (long)((DataRowView)cobTB.SelectedItem).Row.ItemArray[0];
				string text2 = (string)((DataRowView)cobTB.SelectedItem).Row.ItemArray[1];
				double realDisValue = Program.GetRealDisValue(txtDC.Text.Trim());
				int num3 = Convert.ToInt32(nudDay.Value);
				DataTable dataTable = null;
				if (!chkRW.Checked && !chkBM.Checked)
				{
					text = "Insert into T_Team Values(N'" + txtNTM.Text.Trim() + "'," + num2.ToString() + ",N'" + text2.ToString() + "',N'" + txtNGuide.Text.Trim() + "',2," + cobCer.SelectedValue.ToString() + ",N'" + txtNCernum.Text.Trim() + "',N'" + txtTel.Text.Trim() + "',N'" + txtFax.Text.Trim() + "',0,'" + txtMail.Text.Trim() + "',N'" + txtOth.Text.Trim() + "', 0, Null," + txtPerCount.Text.Trim() + ",'" + Program.GetStandDTime(dtpCome.Value, "00") + "', " + num3.ToString() + ",'" + Program.GetStandDate(dtpLevel.Value) + " " + dtpTime.Value.ToString("HH:mm:00") + "',0, 0, 0, " + Program.GetStandDec(realDisValue) + "," + Program.GetStandDec(double.Parse(txtGDepo.Text)) + ", 0, 0,N'" + txtMemo.Text.Trim() + "', 0,GetDate()," + Program.m_opid + ",N'" + Program.m_OperName + "', NULL, NULL, NULL) \n Select  @@Identity As Insert_ID";
					dataTable = Program.DBCompGetDT(text, btnMC.Text);
					if (dataTable == null || dataTable.Rows.Count <= 0)
					{
						Program.MsgCustom((string)m_htab["Err01"], MessageBoxIcon.Hand);
						return;
					}
					m_TID = Convert.ToInt64(dataTable.Rows[0]["Insert_ID"].ToString());
				}
				if (m_TID <= 0)
				{
					text = string.Format((string)m_htab["Err02"], cobTB.Text.Trim() + "\r\n", txtNTM.Text.Trim() + "\r\n");
					Program.MsgCustom(text, MessageBoxIcon.Hand);
					return;
				}
				long tID = m_TID;
				dataTable?.Clear();
				for (int num4 = dgvRList.Rows.Count - 1; num4 >= 0; num4--)
				{
					fdlg = new frmTmpDlg();
					double num5 = 0.0;
					DateTime tGComeTime = DateTime.Now;
					if (chkBM.Checked)
					{
						text = "Select * From T_Team Where team_id = " + m_TID;
						DataTable dataTable2 = SQLserver.Data_GetDataTable(text);
						if (dataTable2 == null || dataTable2.Rows.Count <= 0)
						{
							text = string.Format((string)m_htab["Err02"], cobTB.Text.Trim() + "\r\n", txtNTM.Text.Trim() + "\r\n");
							return;
						}
						if (Convert.ToDateTime(dataTable2.Rows[0]["team_stand_L_time"].ToString()).CompareTo(DateTime.Now) < 0)
						{
							Program.MsgCustom((string)m_htab["Info17"], MessageBoxIcon.Exclamation);
							return;
						}
						if (Convert.ToDateTime(dataTable2.Rows[0]["team_cometime"].ToString()).CompareTo(DateTime.Now) > 0)
						{
							tGComeTime = Convert.ToDateTime(dataTable2.Rows[0]["team_cometime"].ToString());
						}
						num5 = Convert.ToDouble(dataTable2.Rows[0]["team_stayHour"]) / 24.0;
						num5 -= Program.CountDay(Convert.ToDateTime(dataTable2.Rows[0]["team_cometime"].ToString()), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd ") + Convert.ToDateTime(dataTable2.Rows[0]["team_stand_L_time"].ToString()).ToString("HH:mm") + ":00").AddHours(-1.0)) - 1.0;
						fdlg.m_stayday = num5;
						fdlg.m_TGComeTime = tGComeTime;
					}
					else
					{
						fdlg.m_stayday = Convert.ToDouble(nudDay.Value);
						fdlg.m_TGComeTime = dtpCome.Value;
					}
					fdlg.m_userate = double.Parse(cobCurrency.SelectedValue.ToString());
					fdlg.m_tmpVal = tID;
					fdlg.tlpCtls.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
					fdlg.tlpCtls.AutoScroll = false;
					int num6 = 4;
					int num7 = 31;
					int num8 = 2;
					int num9 = 20;
					int[] array = new int[num8];
					array[0] = 120;
					array[1] = 150;
					fdlg.tlpCtls.ColumnCount = num8;
					fdlg.tlpCtls.RowCount = num6;
					fdlg.tlpCtls.Refresh();
					fdlg.tlpCtls.ColumnStyles[0].SizeType = SizeType.AutoSize;
					num9 += Convert.ToInt32(fdlg.tlpCtls.ColumnStyles[0].Width);
					fdlg.tlpCtls.ColumnStyles[1].SizeType = SizeType.Absolute;
					fdlg.tlpCtls.ColumnStyles[1].Width = array[1];
					num9 += Convert.ToInt32(fdlg.tlpCtls.ColumnStyles[1].Width);
					for (int i = 0; i < 100; i++)
					{
						fdlg.lab[i] = new Label();
						fdlg.lab[i].Name = "lab" + (i + 1).ToString("D3");
						fdlg.lab[i].AutoSize = false;
						fdlg.lab[i].TextAlign = ContentAlignment.MiddleRight;
						fdlg.lab[i].Dock = DockStyle.Fill;
						fdlg.txtCtrl[i] = new TextBox();
						fdlg.txtCtrl[i].Name = "txt" + (i + 1).ToString("D3");
						fdlg.txtCtrl[i].Dock = DockStyle.Bottom;
						fdlg.txtCtrl[i].MaxLength = 50;
					}
					int num10 = Convert.ToInt32(dgvRList.Rows[num4].Cells["TP_BedCount"].Value);
					int num11 = Convert.ToInt32(dgvRList.Rows[num4].Cells["R_CurGuestCount"].Value.ToString());
					fdlg.Width = num9;
					for (int j = 0; j < num6 - 1; j++)
					{
						fdlg.tlpCtls.RowStyles[j].SizeType = SizeType.Absolute;
						fdlg.tlpCtls.RowStyles[j].Height = num7;
						fdlg.tlpCtls.Controls.Add(fdlg.lab[j]);
						fdlg.tlpCtls.Controls.Add(fdlg.txtCtrl[j]);
						fdlg.Height += num7;
					}
					fdlg.tlpCtls.RowStyles[num6 - 1].SizeType = SizeType.Absolute;
					fdlg.tlpCtls.RowStyles[num6 - 1].Height = num7;
					if (num11 >= num10)
					{
						num10 = num11;
					}
					fdlg.nudGC.Minimum = ((num11 <= 0) ? 1 : num11);
					fdlg.nudGC.Maximum = 20m;
					fdlg.m_tmpVal02 = num10;
					fdlg.nudGC.Value = Convert.ToDecimal(num10);
					try
					{
						string sql = "Select top 1 * From D_HotelBasic Order by B_ID desc";
						DataTable dataTable3 = null;
						dataTable3 = SQLserver.Data_GetDataTable(sql);
						if (dataTable3 != null && dataTable3.Rows.Count > 0)
						{
							fdlg.nudGC.Maximum = int.Parse(dataTable3.Rows[0]["B_MaxGuest"].ToString().Trim());
						}
					}
					catch (Exception ex)
					{
						Console.Write(ex.Message.ToString());
					}
					fdlg.nudGC.DecimalPlaces = 0;
					fdlg.nudGC.Width = 80;
					fdlg.tlpCtls.Controls.Add(fdlg.lab[num6 - 1]);
					fdlg.tlpCtls.Controls.Add(fdlg.nudGC);
					fdlg.Height += num7;
					fdlg.m_htab = m_htab;
					TextBox obj = fdlg.txtCtrl[2];
					TextBox obj2 = fdlg.txtCtrl[1];
					bool flag = (fdlg.txtCtrl[0].ReadOnly = true);
					bool readOnly = (obj2.ReadOnly = flag);
					obj.ReadOnly = readOnly;
					TextBox obj3 = fdlg.txtCtrl[3];
					TextBox obj4 = fdlg.txtCtrl[2];
					TextBox obj5 = fdlg.txtCtrl[1];
					Color color = (fdlg.txtCtrl[0].BackColor = Color.FromArgb(205, 229, 245));
					Color color3 = (obj5.BackColor = color);
					Color backColor = (obj4.BackColor = color3);
					obj3.BackColor = backColor;
					fdlg.txtCtrl[0].Text = dgvRList.Rows[num4].Cells["R_Name"].Value.ToString().Trim();
					fdlg.txtCtrl[1].Text = dgvRList.Rows[num4].Cells["TP_Name"].Value.ToString().Trim();
					fdlg.txtCtrl[2].Text = dgvRList.Rows[num4].Cells["TP_Price"].Value.ToString().Trim();
					if (num11 > 0)
					{
						dataTable = SQLserver.Data_GetDataTable("Select TR_ID, g_name, cer_id, g_cernum From v_TeamDetails Where g_teamid = " + tID + " And r_id =" + dgvRList.Rows[num4].Cells["R_ID"].Value.ToString() + " And g_level = 0 Order by g_id Desc");
						if (dataTable != null && dataTable.Rows.Count > 0)
						{
							fdlg.m_tmpVal01 = Convert.ToInt64(dataTable.Rows[0]["TR_ID"].ToString());
						}
					}
					int num12;
					for (num12 = 4; num12 < num10 * 4 + 4; num12++)
					{
						num6++;
						fdlg.tlpCtls.RowCount = num6;
						fdlg.tlpCtls.RowStyles[num12].SizeType = SizeType.Absolute;
						fdlg.tlpCtls.RowStyles[num12].Height = num7;
						fdlg.lab[num12].Text = string.Format((string)m_htab["dlgLabGN"], (num10 >= 1) ? (num12 / 4).ToString() : "");
						fdlg.tlpCtls.Controls.Add(fdlg.lab[num12]);
						fdlg.tlpCtls.Controls.Add(fdlg.txtCtrl[num12]);
						fdlg.Height += num7;
						num6++;
						num12++;
						fdlg.tlpCtls.RowCount = num6;
						fdlg.tlpCtls.RowStyles[num12].SizeType = SizeType.Absolute;
						fdlg.tlpCtls.RowStyles[num12].Height = num7;
						fdlg.tlpCtls.Controls.Add(fdlg.lab[num12]);
						fdlg.lab[num12].Text = string.Format((string)m_htab["dlgLabGCer"], "");
						Panel panel = new Panel();
						panel.Name = "PL" + (num12 + 1).ToString("D3");
						NGlassBtn nGlassBtn = new NGlassBtn();
						nGlassBtn.BackColor = Color.Transparent;
						nGlassBtn.BaseColor = Color.White;
						nGlassBtn.ButtonColor = Color.GhostWhite;
						nGlassBtn.ButtonStyle = GlassBtn_New.Style.Flat;
						nGlassBtn.ButtonText = null;
						nGlassBtn.CornerRadius = 2;
						nGlassBtn.GuidInfo = "&56~01'][Manson]v%#@";
						nGlassBtn.Image = Resources.V_Cer;
						nGlassBtn.ImageAlign = ContentAlignment.MiddleCenter;
						nGlassBtn.Name = "TIDC" + (num12 + 1).ToString("D3");
						nGlassBtn.Size = new Size(30, 26);
						if (Program.m_Lan == 0)
						{
							nGlassBtn.Enabled = false;
						}
						nGlassBtn.Click += tmpbtnIDCard_Click;
						ComboBox comboBox = new ComboBox();
						comboBox.Name = "cob" + (num12 + 1).ToString("D3");
						comboBox.DisplayMember = "cer_name";
						comboBox.ValueMember = "cer_id";
						DataTable dataTable4 = null;
						try
						{
							text = "Select * FROM D_Cer  Where cer_flag = 0";
							dataTable4 = SQLserver.Data_GetDataTable(text);
							if (dataTable4 != null)
							{
								comboBox.DisplayMember = "cer_name";
								comboBox.ValueMember = "cer_id";
								comboBox.DataSource = dataTable4.DefaultView;
							}
						}
						catch
						{
						}
						comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
						comboBox.Margin = new Padding(0, 0, 3, 0);
						nGlassBtn.Padding = new Padding(0, 3, 0, 0);
						panel.Controls.Add(comboBox);
						panel.Controls.Add(nGlassBtn);
						comboBox.Dock = DockStyle.Fill;
						nGlassBtn.Dock = DockStyle.Right;
						panel.Dock = DockStyle.Fill;
						fdlg.tlpCtls.Controls.Add(panel);
						fdlg.Height += num7;
						fdlg.ctrlcob.Add(comboBox);
						num6++;
						num12++;
						fdlg.tlpCtls.RowCount = num6;
						fdlg.tlpCtls.RowStyles[num12].SizeType = SizeType.Absolute;
						fdlg.tlpCtls.RowStyles[num12].Height = num7;
						fdlg.tlpCtls.Controls.Add(fdlg.lab[num12]);
						fdlg.lab[num12].Text = string.Format((string)m_htab["dlgLabGCNum"], "");
						fdlg.tlpCtls.Controls.Add(fdlg.txtCtrl[num12]);
						fdlg.Height += num7;
						if (num11 > 0 && dataTable != null && dataTable.Rows.Count > 0)
						{
							fdlg.txtCtrl[num12 - 2].Text = dataTable.Rows[num11 - 1]["g_name"].ToString().Trim();
							comboBox.SelectedValue = Convert.ToInt32(dataTable.Rows[num11 - 1]["cer_id"].ToString());
							fdlg.txtCtrl[num12].Text = dataTable.Rows[num11 - 1]["g_cernum"].ToString().Trim();
							comboBox.Enabled = false;
							TextBox obj7 = fdlg.txtCtrl[num12 - 2];
							bool readOnly2 = (fdlg.txtCtrl[num12].ReadOnly = true);
							obj7.ReadOnly = readOnly2;
							nGlassBtn.Enabled = false;
							TextBox obj8 = fdlg.txtCtrl[num12 - 2];
							Color color6 = (fdlg.txtCtrl[num12].BackColor = Color.FromArgb(205, 229, 245));
							Color backColor2 = (obj8.BackColor = color6);
							comboBox.BackColor = backColor2;
							num11--;
						}
						num6++;
						num12++;
						fdlg.tlpCtls.RowCount = num6;
						fdlg.tlpCtls.RowStyles[num12].SizeType = SizeType.Absolute;
						fdlg.tlpCtls.RowStyles[num12].Height = num7 - 5;
						fdlg.tlpCtls.Controls.Add(new Label());
						CheckBox checkBox = new CheckBox();
						checkBox.Name = "chkGC" + (num12 + 1).ToString("D3");
						checkBox.Text = (string)m_htab["dlgChkGC"];
						checkBox.Checked = num12 == 7;
						comboBox.Dock = DockStyle.Bottom;
						fdlg.tlpCtls.Controls.Add(checkBox);
						fdlg.Height += num7 - 5;
						fdlg.ctrlchk.Add(checkBox);
					}
					if (fdlg.Height > 600)
					{
						fdlg.Height = 600;
						fdlg.tlpCtls.AutoScroll = true;
						fdlg.Width += 10;
					}
					fdlg.tlpCtls.AutoScroll = true;
					fdlg.txtCtrl[4].Select();
					fdlg.btnOK.Click += dlgbtnOK_Click;
					fdlg.btnCl.Click += dlgbtnCl_Click;
					fdlg.btnSkip.Click += dlgbtnSkip_Click;
					fdlg.btnSkip.Visible = true;
					fdlg.nudGC.ValueChanged += nudGC_ValueChanged;
					switch (fdlg.ShowDialog())
					{
					case DialogResult.Cancel:
						break;
					case DialogResult.Ignore:
						fdlg.Dispose();
						fdlg = null;
						continue;
					default:
						dgvRList.Rows.Remove(dgvRList.Rows[num4]);
						continue;
					}
					fdlg.Dispose();
					fdlg = null;
					if (Program.fm != null)
					{
						Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
					}
					break;
				}
				frmSTour frmSTour2 = new frmSTour();
				text = "Select TB_name, team_name, team_guide, Team_cername, team_cernum, g_name,  cer_name, g_cernum, r_name, build_name, floor_name, TP_Name";
				text += ", g_cometime,g_sototalday As g_stayDay, g_stand_L_time, g_stayover, g_softime, g_soltime";
				text += ", g_sototalday, g_level, g_actual_l_time, g_level_card, g_othprice";
				text += " From v_TeamDetails Where ";
				text = text + "Team_Id=" + tID;
				text += " Order by g_id desc";
				frmSTour2.m_initctrl = (frmSTour2.m_pars = (frmSTour2.m_sum = false));
				frmSTour2.m_extstr = text;
				frmSTour2.Text = txtNTM.Text.Trim();
				frmSTour2.StartPosition = FormStartPosition.CenterScreen;
				frmSTour2.ShowDialog();
				if (dgvRList.Rows.Count <= 0)
				{
					nudDay.Value = Convert.ToDecimal(Program.m_defDay);
					TextBox textBox = txtDP;
					TextBox textBox2 = txtRP;
					TextBox textBox3 = txtMP;
					string text3 = (txtGC.Text = Program.GetLocDecStr("0.00"));
					string text4 = (textBox3.Text = text3);
					string text6 = (textBox2.Text = text4);
					textBox.Text = text6;
				}
				if (Program.fm != null)
				{
					Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
				}
				btnNHide_Click(null, null);
				btnSear_Click(null, null);
			}
		}
		catch (Exception ex2)
		{
			if (fdlg != null)
			{
				fdlg.Dispose();
				fdlg = null;
			}
			Program.MsgCusErrMess(ex2.Message, btnMC.Text);
		}
	}

	private void nudGC_ValueChanged(object sender, EventArgs e)
	{
		int num = 0;
		try
		{
			int num2 = Convert.ToInt32(fdlg.nudGC.Value);
			if (num2 == fdlg.m_tmpVal02)
			{
				return;
			}
			bool flag = true;
			if (num2 < fdlg.m_tmpVal02)
			{
				flag = false;
			}
			if (!flag)
			{
				num2 = fdlg.m_tmpVal02 - num2;
				for (int i = 0; i < num2; i++)
				{
					int num3 = fdlg.tlpCtls.RowCount * 2 - 1;
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.Controls.RemoveAt(num3--);
					fdlg.tlpCtls.RowStyles.RemoveAt(--fdlg.tlpCtls.RowCount);
					fdlg.tlpCtls.RowStyles.RemoveAt(--fdlg.tlpCtls.RowCount);
					fdlg.tlpCtls.RowStyles.RemoveAt(--fdlg.tlpCtls.RowCount);
					fdlg.tlpCtls.RowStyles.RemoveAt(--fdlg.tlpCtls.RowCount);
					fdlg.ctrlcob.RemoveAt(fdlg.ctrlcob.Count - 1);
					fdlg.ctrlchk.RemoveAt(fdlg.ctrlchk.Count - 1);
				}
				fdlg.tlpCtls.AutoScroll = false;
				fdlg.tlpCtls.AutoScroll = true;
			}
			else
			{
				num2 -= fdlg.m_tmpVal02;
				int num4 = fdlg.tlpCtls.RowCount;
				int num5 = 31;
				int tmpVal = fdlg.m_tmpVal02;
				int num6 = num4;
				RowStyle rowStyle = null;
				int num7;
				for (num7 = num6; num7 < num2 * 4 + num6; num7++)
				{
					num4++;
					fdlg.tlpCtls.RowCount = num4;
					rowStyle = new RowStyle(SizeType.Absolute, num5);
					fdlg.tlpCtls.RowStyles.Insert(num7, rowStyle);
					fdlg.lab[num7].Text = string.Format((string)m_htab["dlgLabGN"], (tmpVal >= 1) ? (num7 / 4).ToString() : "");
					fdlg.tlpCtls.Controls.Add(fdlg.lab[num7]);
					fdlg.tlpCtls.Controls.Add(fdlg.txtCtrl[num7]);
					fdlg.Height += num5;
					num = 1;
					num4++;
					num7++;
					fdlg.tlpCtls.RowCount = num4;
					rowStyle = new RowStyle(SizeType.Absolute, num5);
					fdlg.tlpCtls.RowStyles.Insert(num7, rowStyle);
					fdlg.tlpCtls.Controls.Add(fdlg.lab[num7]);
					fdlg.lab[num7].Text = string.Format((string)m_htab["dlgLabGCer"], "");
					num = 2;
					Panel panel = new Panel();
					panel.Name = "PL" + (num7 + 1).ToString("D3");
					NGlassBtn nGlassBtn = new NGlassBtn();
					nGlassBtn.BackColor = Color.Transparent;
					nGlassBtn.BaseColor = Color.White;
					nGlassBtn.ButtonColor = Color.Silver;
					nGlassBtn.ButtonStyle = GlassBtn_New.Style.Flat;
					nGlassBtn.ButtonText = null;
					nGlassBtn.CornerRadius = 2;
					nGlassBtn.GuidInfo = "&56~01'][Manson]v%#@";
					nGlassBtn.Image = Resources.V_Cer;
					nGlassBtn.ImageAlign = ContentAlignment.MiddleCenter;
					nGlassBtn.Name = "TIDC" + (num7 + 1).ToString("D3");
					nGlassBtn.Size = new Size(30, 26);
					nGlassBtn.Click += tmpbtnIDCard_Click;
					num = 3;
					ComboBox comboBox = new ComboBox();
					comboBox.Name = "cob" + (num7 + 1).ToString("D3");
					comboBox.DisplayMember = "cer_name";
					comboBox.ValueMember = "cer_id";
					comboBox.Width = 30;
					num = 4;
					DataTable dataTable = null;
					try
					{
						string sql = "Select * FROM D_Cer  Where cer_flag = 0";
						dataTable = SQLserver.Data_GetDataTable(sql);
						if (dataTable != null)
						{
							comboBox.DisplayMember = "cer_name";
							comboBox.ValueMember = "cer_id";
							comboBox.DataSource = dataTable.DefaultView;
						}
					}
					catch
					{
					}
					num = 5;
					comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
					comboBox.Dock = DockStyle.Fill;
					nGlassBtn.Dock = DockStyle.Right;
					panel.Controls.Add(comboBox);
					panel.Controls.Add(nGlassBtn);
					comboBox.Margin = new Padding(0, 0, 3, 0);
					nGlassBtn.Padding = new Padding(0, 3, 0, 0);
					panel.Dock = DockStyle.Fill;
					fdlg.tlpCtls.Controls.Add(panel);
					fdlg.Height += num5;
					fdlg.ctrlcob.Add(comboBox);
					num = 6;
					num4++;
					num7++;
					fdlg.tlpCtls.RowCount = num4;
					rowStyle = new RowStyle(SizeType.Absolute, num5);
					fdlg.tlpCtls.RowStyles.Insert(num7, rowStyle);
					fdlg.tlpCtls.Controls.Add(fdlg.lab[num7]);
					fdlg.lab[num7].Text = string.Format((string)m_htab["dlgLabGCNum"], "");
					fdlg.tlpCtls.Controls.Add(fdlg.txtCtrl[num7]);
					fdlg.Height += num5;
					num = 7;
					num4++;
					num7++;
					fdlg.tlpCtls.RowCount = num4;
					rowStyle = new RowStyle(SizeType.Absolute, num5);
					fdlg.tlpCtls.RowStyles.Insert(num7, rowStyle);
					fdlg.tlpCtls.Controls.Add(new Label());
					num = 8;
					CheckBox checkBox = new CheckBox();
					checkBox.Name = "chkGC" + (num7 + 1).ToString("D3");
					checkBox.Text = (string)m_htab["dlgChkGC"];
					checkBox.Checked = num7 == 7;
					comboBox.Dock = DockStyle.Bottom;
					num = 9;
					fdlg.tlpCtls.Controls.Add(checkBox);
					fdlg.Height += num5;
					if (fdlg.Height > 600)
					{
						fdlg.Height = 600;
						fdlg.tlpCtls.AutoScroll = true;
						fdlg.Width += 10;
					}
					fdlg.ctrlchk.Add(checkBox);
				}
			}
			fdlg.m_tmpVal02 = Convert.ToInt32(fdlg.nudGC.Value);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message + "\r\nDebug = " + num);
		}
	}

	private void tmpbtnIDCard_Click(object sender, EventArgs e)
	{
		try
		{
			string name = ((NGlassBtn)sender).Name;
			int num = Convert.ToInt32(name.Replace("TIDC", ""));
			if (num >= 0)
			{
				TextBox obj = fdlg.txtCtrl[num - 2];
				string text = (fdlg.txtCtrl[num].Text = "");
				obj.Text = text;
				Program.IDCardData CardMsg = default(Program.IDCardData);
				if (Program.Get_IDCardII_Information(ref CardMsg) >= 0)
				{
					fdlg.txtCtrl[num - 2].Text = CardMsg.Name.Trim();
					fdlg.txtCtrl[num].Text = CardMsg.IDCardNo;
				}
			}
		}
		catch
		{
		}
	}

	private void dlgbtnSkip_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
		{
			fdlg.m_close = false;
		}
	}

	private void dlgbtnCl_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
		{
			fdlg.m_close = false;
		}
	}

	private void dlgbtnOK_Click(object sender, EventArgs e)
	{
		try
		{
			fdlg.m_close = false;
			Control.ControlCollection controls = fdlg.tlpCtls.Controls;
			for (int i = 4; i < controls.Count; i++)
			{
				if (controls[i].GetType() == typeof(TextBox))
				{
					if (!((TextBox)controls[i]).ReadOnly && Program.m_chkGInfo && Program.isValNull(controls[i - 1].Text.Substring(0, controls[i - 1].Text.Length - 1), controls[i].Text.Trim(), Program.m_chkGInfo))
					{
						controls[i].Select();
						return;
					}
				}
				else if (controls[i].GetType() == typeof(ComboBox) && Program.isValNull(controls[i - 1].Text.Substring(0, controls[i - 1].Text.Length - 1), controls[i].Text.Trim(), chk: true))
				{
					return;
				}
			}
			int num = 4;
			int num2 = Convert.ToInt32(fdlg.nudGC.Value);
			if (num2 > fdlg.ctrlchk.Count)
			{
				Program.MsgCustom((string)m_htab["Info04"], MessageBoxIcon.Exclamation);
				fdlg.m_close = false;
				return;
			}
			string text = fdlg.txtCtrl[0].Text.Trim();
			if (Program.isValNull(fdlg.lab[0].Text.Substring(0, fdlg.lab[0].Text.Length - 1), text, chk: true))
			{
				return;
			}
			string text2 = "Select R_ID,R_MaxCardNum,Build_Code,Floor_Code,R_Code,R_SubCode,R_SubCodeDai, R_RSID, TP_Price, TP_deposit";
			text2 = text2 + " From v_HotelRooms Where R_Name=N'" + text + "' And R_flag=0";
			DataTable dataTable = SQLserver.Data_GetDataTable(text2);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				Program.MsgCusErrMess("Null", text);
				return;
			}
			int num3 = Program.getMaxNumber(1, showError: true);
			if (num3 < 0)
			{
				return;
			}
			int num4 = Convert.ToInt32(dataTable.Rows[0]["Build_Code"].ToString());
			int num5 = Convert.ToInt32(dataTable.Rows[0]["Floor_Code"].ToString());
			int num6 = Convert.ToInt32(dataTable.Rows[0]["R_Code"].ToString());
			int num7 = Convert.ToInt32(dataTable.Rows[0]["R_SubCode"].ToString());
			int num8 = Convert.ToInt32(dataTable.Rows[0]["R_SubCodeDai"].ToString());
			int num9 = Convert.ToInt32(dataTable.Rows[0]["R_RSID"].ToString());
			if (num8 >= 256)
			{
				num8 = 1;
			}
			num8 += 2;
			long num10 = Convert.ToInt32(dataTable.Rows[0]["R_ID"].ToString());
			int num11 = 6;
			double num12 = Convert.ToDouble(fdlg.m_stayday);
			double result = num12;
			if (chkBM.Checked)
			{
				DateTime dtLeaveTime = DateTime.Parse(Program.GetStandDate(dtpLevel.Value) + " " + dtpTime.Value.ToString("HH:mm:ss"));
				num12 = Program.CountDay(fdlg.m_TGComeTime, dtLeaveTime);
			}
			double num13 = (Convert.ToDouble(nudDay.Value) - num12) * 24.0;
			long num14 = -1L;
			double num15 = Convert.ToDouble(dataTable.Rows[0]["TP_Price"]);
			double num16 = Convert.ToDouble(dataTable.Rows[0]["TP_deposit"]);
			double num17 = num15 * num12;
			_ = fdlg.m_userate;
			num12 *= 24.0;
			bool flag = false;
			string datetime = dtpLevel.Value.ToString("yyyyMMdd") + dtpTime.Value.ToString("HHmm");
			string text3 = num4.ToString("X2") + num5.ToString("X2") + num6.ToString("X2") + num7.ToString("X2") + num8.ToString("X2");
			double realDisValue = Program.GetRealDisValue(txtDC.Text.Trim());
			for (int j = 0; j < num2; j++)
			{
				if (fdlg.txtCtrl[num].ReadOnly)
				{
					num += 4;
					continue;
				}
				num3++;
				text2 = "";
				int num18 = -1;
				int num19 = 0;
				if (((CheckBox)fdlg.ctrlchk[j]).Checked)
				{
					num19 = 1;
				}
				if (num19 > 0 && Program.RadioWriteCard(6, num3, datetime, text3, text3.Length, Buzzer: true) != 0)
				{
					return;
				}
				if (fdlg.m_tmpVal01 > 0)
				{
					num14 = fdlg.m_tmpVal01;
					flag = true;
				}
				if (!flag)
				{
					text2 = "Insert Into T_Rooms Values(''," + num2.ToString() + ", " + num19.ToString() + ", 0," + num10.ToString() + "," + num11.ToString() + ",N'" + text + "','" + num6.ToString() + "'," + num7.ToString() + "," + Program.GetStandDec(num15) + ", " + Program.GetStandDec(realDisValue) + ", 0,'" + Program.GetStandDTime(fdlg.m_TGComeTime, "00") + "'" + $",{result:F0},'{Program.GetStandDate(dtpLevel.Value)} {dtpTime.Value:HH:mm:00}'" + ",0,NULL, 0, 0, 0, 0, 0, NULL," + Program.GetStandDec(num16) + ",0,''," + Program.m_baseCurrID + ",N'" + Program.m_baseCurrCode + "'," + Program.GetStandDec(Program.m_baseCurrRate) + ",N'" + cobCurrency.Text.Trim() + "'," + Program.GetStandDec(cobCurrency.SelectedValue.ToString()) + ", 0, 0, 0,'',0,NULL," + fdlg.m_tmpVal.ToString() + ",GetDate()," + Program.m_opid + ",N'" + Program.m_OperName + "', NULL, NULL, NULL) \n Select  @@Identity As Insert_ID";
					dataTable.Clear();
					dataTable = Program.DBCompGetDT(text2, btnMC.Text);
					if (dataTable == null || dataTable.Rows.Count <= 0)
					{
						Program.MsgCustom((string)m_htab["Info09"], MessageBoxIcon.Hand);
						return;
					}
					num14 = Convert.ToInt64(dataTable.Rows[0]["Insert_ID"].ToString());
					text2 = "";
					flag = true;
				}
				else
				{
					if (num19 > 0)
					{
						text2 = "Update T_Rooms Set TR_cardcount = TR_cardcount + 1, TR_guestcount = TR_guestcount + 1 Where TR_ID = " + num14 + " \n";
					}
					string sql = "select top 1 b.tr_id,a.g_id,a.g_stand_l_time,a.g_SOTotalDay from t_guest a,t_rooms b where a.tr_id=b.tr_id and a.g_level=0 and b.r_id =" + num10;
					DataTable dataTable2 = SQLserver.Data_GetDataTable(sql);
					double.TryParse(dataTable2.Rows[0]["g_SOTotalDay"].ToString(), out result);
				}
				string text4 = text2;
				text2 = text4 + "Insert Into T_Guest Values(N'" + fdlg.txtCtrl[num].Text.Trim() + "',2," + ((ComboBox)fdlg.ctrlcob[j]).SelectedValue.ToString() + ",N'" + fdlg.txtCtrl[num + 2].Text.Trim() + "',''," + num14 + "," + num10 + ",'" + num4 + "','" + num5 + "','" + num6 + "'," + num7 + "," + num8 + "," + num3 + ",N'" + text + "',0,";
				object obj = text2;
				text2 = string.Concat(obj, Program.GetStandDec(num15), ",", Program.GetStandDec(realDisValue), ",", Program.GetStandDec(num15 * realDisValue), ",", 0);
				object obj2 = text2;
				text2 = string.Concat(obj2, ",'", Program.GetStandDTime(fdlg.m_TGComeTime, "00"), "',0,'", Program.GetStandDate(dtpLevel.Value), " ", dtpTime.Value.ToString("HH:mm:00"), "',0,NULL,NULL,", result, ",0,0,0,NULL,0,0,'',0,0,0,NULL,0,NULL,", fdlg.m_tmpVal.ToString(), ",0,NULL,0,convert(nvarchar(max),", num14, "),0,NULL,0,NULL,", num19.ToString(), ",GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "', NULL, NULL, NULL,NULL,", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(),", Program.GetStandDec(num13.ToString("F0")), ")");
				if (Program.DBCompExec(text2, "") <= 0)
				{
					Program.MsgBox((string)m_htab["Info10"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				text2 = fdlg.lab[num].Text + fdlg.txtCtrl[num].Text.Trim() + "\r\n\r\n";
				text2 = text2 + fdlg.lab[num + 1].Text + ((DataRowView)((ComboBox)fdlg.ctrlcob[j]).SelectedItem).Row.ItemArray[1].ToString() + "\r\n\r\n";
				text2 = text2 + fdlg.lab[num + 2].Text + fdlg.txtCtrl[num + 2].Text.Trim() + "\r\n\r\n";
				text2 += (string)m_htab["Info08"];
				Program.MsgCustom(text2, MessageBoxIcon.Asterisk);
				num += 4;
			}
			text2 = "Update D_Rooms Set R_RSID=" + num11.ToString() + ", R_CurGuestCount=" + num2.ToString() + ",R_MaxCardNum=" + num3.ToString() + ", R_SubCodeDai= " + num8;
			string text5 = text2;
			text2 = text5 + ", R_TotalGuest=IsNull(R_TotalGuest,0) + " + num2 + ", R_TotalPrice=Isnull(R_TotalPrice,0) + " + Program.GetStandDec(num17 * realDisValue);
			object obj3 = text2;
			text2 = string.Concat(obj3, ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", num10.ToString());
			if (fdlg.m_tmpVal01 <= 0 && !chkBM.Checked)
			{
				num2 = 0;
			}
			if (num9 == 3)
			{
				string text6 = text2;
				text2 = text6 + "\n Update T_Schedule Set sch_flag = 1, sch_memo = N'" + btnMC.Text + "' Where sch_flag=0 And r_id=" + num10 + " And g_teamid = " + fdlg.m_tmpVal;
			}
			if (Program.DBCompExec(text2, "") <= 0)
			{
				Program.MsgBox((string)m_htab["Info11"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				fdlg.m_close = true;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtPerCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void btnGT_Click(object sender, EventArgs e)
	{
		frmSTour frmSTour2 = new frmSTour();
		frmSTour2.StartPosition = FormStartPosition.CenterScreen;
		frmSTour2.m_initctrl = false;
		string text = "Select TB_name, team_name, team_guide, Team_cername, team_cernum, g_name,  cer_name, g_cernum, r_name, build_name, floor_name, TP_Name";
		text += ", g_cometime,g_SOTotalDay As g_stayDay, g_stand_L_time, g_stayover, g_softime, g_soltime";
		text += ", g_sototalday, g_level, g_actual_l_time, g_level_card";
		text = text + " From v_TeamDetails Where 1 = 1 And Team_id = " + m_TID;
		text += " Order by g_id desc";
		frmSTour2.m_pars = false;
		frmSTour2.m_sum = false;
		frmSTour2.m_extstr = text;
		frmSTour2.Text = txtNTM.Text.Trim();
		frmSTour2.ShowDialog();
	}

	private void dgvTBHis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			string text = "";
			if (dgvRList.Rows.Count > 0 || lvRoom.Items.Count > 0)
			{
				text = string.Format((string)m_htab["Info12"], dgvTBHis.Rows[e.RowIndex].Cells["Team_name"].Value.ToString());
				if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}
				ToolStripStatusLabel tSSLab = TSSLab04;
				ToolStripStatusLabel tSSLab2 = TSSLab06;
				string text2 = (TSSLab08.Text = "");
				string text4 = (tSSLab2.Text = text2);
				tSSLab.Text = text4;
				dgvRList.Rows.Clear();
				lvRoom.Items.Clear();
			}
			chkBM.Checked = false;
			chkBM.Enabled = false;
			int rowIndex = e.RowIndex;
			m_TID = Convert.ToInt64(dgvTBHis.Rows[rowIndex].Cells["Team_id"].Value);
			txtNTM.Text = dgvTBHis.Rows[rowIndex].Cells["Team_name"].Value.ToString();
			txtNGuide.Text = dgvTBHis.Rows[rowIndex].Cells["Team_guide"].Value.ToString();
			txtNCernum.Text = dgvTBHis.Rows[rowIndex].Cells["team_cernum"].Value.ToString();
			txtTel.Text = dgvTBHis.Rows[rowIndex].Cells["team_tel"].Value.ToString();
			txtFax.Text = dgvTBHis.Rows[rowIndex].Cells["team_fax"].Value.ToString();
			txtMail.Text = dgvTBHis.Rows[rowIndex].Cells["team_mail"].Value.ToString();
			txtOth.Text = dgvTBHis.Rows[rowIndex].Cells["team_othConn"].Value.ToString();
			cobCer.Text = dgvTBHis.Rows[rowIndex].Cells["cer_name"].Value.ToString();
			text = dgvTBHis.Rows[rowIndex].Cells["Team_Leveltime"].Value.ToString();
			if (text == "")
			{
				LockSoftware.Controls.GlassBtn glassBtn = btnGM;
				bool enabled = (chkBM.Enabled = true);
				glassBtn.Enabled = enabled;
				NumericUpDown numericUpDown = nudDay;
				DateTimePicker dateTimePicker = dtpCome;
				DateTimePicker dateTimePicker2 = dtpLevel;
				bool flag2 = (dtpTime.Enabled = false);
				bool flag4 = (dateTimePicker2.Enabled = flag2);
				bool enabled2 = (dateTimePicker.Enabled = flag4);
				numericUpDown.Enabled = enabled2;
				dtpCome.Value = Convert.ToDateTime(dgvTBHis.Rows[rowIndex].Cells["Team_cometime"].Value);
				DateTimePicker dateTimePicker3 = dtpLevel;
				DateTime value = (dtpTime.Value = Convert.ToDateTime(dgvTBHis.Rows[rowIndex].Cells["team_stand_L_time"].Value));
				dateTimePicker3.Value = value;
				txtPerCount.Text = dgvTBHis.Rows[rowIndex].Cells["team_percount"].Value.ToString();
				chkBM.Checked = true;
			}
			else
			{
				NumericUpDown numericUpDown2 = nudDay;
				DateTimePicker dateTimePicker4 = dtpCome;
				DateTimePicker dateTimePicker5 = dtpLevel;
				bool flag7 = (dtpTime.Enabled = true);
				bool flag9 = (dateTimePicker5.Enabled = flag7);
				bool enabled3 = (dateTimePicker4.Enabled = flag9);
				numericUpDown2.Enabled = enabled3;
				LockSoftware.Controls.GlassBtn glassBtn2 = btnGM;
				CheckBox checkBox = chkBM;
				bool flag12 = (chkBM.Enabled = false);
				bool enabled4 = (checkBox.Checked = flag12);
				glassBtn2.Enabled = enabled4;
				nudDay.Value = Convert.ToDecimal(Program.m_defDay);
				dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
				dtpCome.Value = DateTime.Now;
				cobCurrency.Text = Program.m_baseCurrCode;
			}
		}
		catch
		{
		}
	}

	private void chkBM_TextChanged(object sender, EventArgs e)
	{
		chkBM.Enabled = false;
	}

	private void txtNTM_TextChanged(object sender, EventArgs e)
	{
		chkBM.Enabled = false;
	}

	private void btnGM_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			if (Program.isValNull(label23.Text, txtPerCount.Text.Trim(), chk: true))
			{
				return;
			}
			if (m_TID <= 0)
			{
				text = string.Format((string)m_htab["Err02"], cobTB.Text.Trim() + "\r\n", txtNTM.Text.Trim() + "\r\n");
				Program.MsgCustom(text, MessageBoxIcon.Hand);
			}
			else if (Program.MsgBox((string)m_htab["Info13"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				string text2 = "Update T_Team Set team_tel = N'" + txtTel.Text.Trim() + "', team_fax = N'" + txtFax.Text.Trim() + "'";
				string text3 = text2;
				text2 = text3 + ", team_mail = N'" + txtMail.Text.Trim() + "', team_othConn = N'" + txtOth.Text.Trim() + "',team_perCount = " + txtPerCount.Text.Trim();
				string text4 = text2;
				text2 = text4 + ", team_memo = N'" + txtMemo.Text.Trim() + "' Where Team_id = " + m_TID;
				int num = SQLserver.Data_ExecuteSql(text2);
				if (num <= 0)
				{
					Program.MsgCustom((string)m_htab["Err01"], MessageBoxIcon.Hand);
				}
				btnTBHis_Click(null, null);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnGM.Text);
		}
	}

	private void dgvTBHis_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		try
		{
			for (int i = 0; i < dgvTBHis.Rows.Count; i++)
			{
				if (dgvTBHis.Rows[i].Cells["Team_leveltime"].Value.ToString() != "")
				{
					dgvTBHis.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(224, 85, 50);
					dgvTBHis.Rows[i].DefaultCellStyle.ForeColor = Color.White;
				}
			}
		}
		catch
		{
		}
	}

	private void TSSBtnBR_Click(object sender, EventArgs e)
	{
		try
		{
			if (cobTB.SelectedItem == null)
			{
				return;
			}
			if (cobCer.Items.Count <= 0)
			{
				Program.MsgCustom(string.Format((string)m_htab["Info03"], label3.Text.Substring(0, label3.Text.Length - 1)), MessageBoxIcon.Asterisk);
			}
			else
			{
				if (Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtNTM.Text.Trim(), chk: true) || Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtNGuide.Text.Trim(), chk: true) || Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtNCernum.Text.Trim(), chk: true) || Program.isValNull(label23.Text.Substring(0, label23.Text.Length - 1), txtPerCount.Text.Trim(), chk: true))
				{
					return;
				}
				int num = Convert.ToInt32("0" + txtPerCount.Text.Trim());
				if (num <= 0)
				{
					Program.MsgCustom(string.Format((string)m_htab["Info03"], label23.Text.Substring(0, label23.Text.Length - 1)), MessageBoxIcon.Asterisk);
					return;
				}
				if (dgvRList.Rows.Count <= 0)
				{
					Program.MsgCustom(string.Format((string)m_htab["Info03"], (string)m_htab["dgvcolR_Name"]), MessageBoxIcon.Asterisk);
					return;
				}
				string text = label1.Text + txtNTM.Text.Trim() + "\r\n\r\n";
				text = text + label2.Text + txtNGuide.Text.Trim() + "\r\n\r\n";
				text = text + label23.Text + txtPerCount.Text.Trim() + "\r\n\r\n";
				string text2 = text;
				text = text2 + TSSLab05.Text + " " + TSSLab08.Text + "\r\n\r\n";
				text = (chkBM.Checked ? (text + (string)m_htab["Info15"]) : (text + (string)m_htab["Info14"]));
				if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}
				long num2 = (long)((DataRowView)cobTB.SelectedItem).Row.ItemArray[0];
				string text3 = (string)((DataRowView)cobTB.SelectedItem).Row.ItemArray[1];
				double realDisValue = Program.GetRealDisValue(txtDC.Text.Trim());
				int num3 = Convert.ToInt32(nudDay.Value);
				num3 *= 24;
				DataTable dataTable = null;
				if (!chkBM.Checked)
				{
					text = "Insert into T_Team Values(N'" + txtNTM.Text.Trim() + "'," + num2.ToString() + ",N'" + text3.ToString() + "',N'" + txtNGuide.Text.Trim() + "',2, " + cobCer.SelectedValue.ToString() + ",N'" + txtNCernum.Text.Trim() + "',N'" + txtTel.Text.Trim() + "',N'" + txtFax.Text.Trim() + "',0,N'" + txtMail.Text.Trim() + "',N'" + txtOth.Text.Trim() + "', 0, Null," + txtPerCount.Text.Trim() + ",'" + Program.GetStandDTime(dtpCome.Value, "00") + "', " + num3.ToString() + ",'" + Program.GetStandDate(dtpLevel.Value) + " " + dtpTime.Value.ToString("HH:mm:00") + "',0,0,0," + Program.GetStandDec(realDisValue) + ", 0, 0, 0,N'" + txtMemo.Text.Trim() + "',1,GetDate()," + Program.m_opid + ",N'" + Program.m_OperName + "', NULL, NULL, NULL) \n Select  @@Identity As Insert_ID";
					dataTable = Program.DBCompGetDT(text, btnMC.Text);
					if (dataTable == null || dataTable.Rows.Count <= 0)
					{
						Program.MsgCustom((string)m_htab["Err01"], MessageBoxIcon.Hand);
						return;
					}
					m_TID = Convert.ToInt64(dataTable.Rows[0]["Insert_ID"].ToString());
				}
				if (m_TID <= 0)
				{
					text = string.Format((string)m_htab["Err02"], cobTB.Text.Trim() + "\r\n", txtNTM.Text.Trim() + "\r\n");
					Program.MsgCustom(text, MessageBoxIcon.Hand);
					return;
				}
				long tID = m_TID;
				dataTable?.Clear();
				for (int num4 = dgvRList.Rows.Count - 1; num4 >= 0; num4--)
				{
					if (Convert.ToInt32(dgvRList.Rows[num4].Cells["R_RSID"].Value) == 1)
					{
						long num5 = Convert.ToInt64(dgvRList.Rows[num4].Cells["R_ID"].Value);
						int iRoomStatus = Convert.ToInt32(dgvRList.Rows[num4].Cells["R_RSID"].Value);
						text = "Insert Into T_schedule Values(N'" + txtNTM.Text.Trim() + "', N'" + txtTel.Text.Trim() + "', '', N'" + txtMail.Text.Trim() + "', N'" + txtNGuide.Text.Trim() + "', 2, " + cobCer.SelectedValue.ToString() + ", N'" + txtNCernum.Text.Trim() + "',  NULL, " + num5.ToString() + ", '" + Program.GetStandDate(dtpCome.Value) + "', '" + dtpCome.Value.ToString("HH:mm") + "', '" + Program.GetStandDate(dtpLevel.Value) + "', " + tID.ToString() + ", GetDate(), " + Program.m_opid + ",N'" + Program.m_OperName + "', '', 0) \n";
						if (Program.IsScheduleStatus(dtpCome.Value, iRoomStatus))
						{
							text = text + " Update D_Rooms Set R_RSID = 3 Where R_ID = " + num5;
						}
						if (Program.DBCompExec(text, Text) < 0)
						{
							text = txtNTM.Text.Trim() + "\r\n" + dgvRList.Rows[num4].Cells["r_name"].Value.ToString();
							Program.MsgCustom(text + "\r\n" + (string)m_htab["Err01"], MessageBoxIcon.Hand);
							return;
						}
					}
					dgvRList.Rows.Remove(dgvRList.Rows[num4]);
				}
				if (Program.fm != null)
				{
					Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
				}
				if (dgvRList.Rows.Count <= 0)
				{
					nudDay.Value = Convert.ToDecimal(Program.m_defDay);
					TextBox textBox = txtDP;
					TextBox textBox2 = txtRP;
					TextBox textBox3 = txtMP;
					string text4 = (txtGC.Text = Program.GetLocDecStr("0.00"));
					string text5 = (textBox3.Text = text4);
					string text7 = (textBox2.Text = text5);
					textBox.Text = text7;
				}
				btnTBHis_Click(null, null);
				btnSear_Click(null, null);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, TSSBtnBR.Text);
		}
	}

	private void chkBM_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (!chkBM.Checked)
			{
				dgvRList.Rows.Clear();
				btnMC.ForeColor = Color.Black;
				btnGM.Enabled = false;
				NumericUpDown numericUpDown = nudDay;
				DateTimePicker dateTimePicker = dtpCome;
				DateTimePicker dateTimePicker2 = dtpLevel;
				bool flag = (dtpTime.Enabled = true);
				bool flag3 = (dateTimePicker2.Enabled = flag);
				bool enabled = (dateTimePicker.Enabled = flag3);
				numericUpDown.Enabled = enabled;
				nudDay.Value = Convert.ToDecimal(Program.m_defDay);
				dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
				dtpCome.Value = DateTime.Now;
				cobCurrency.Text = Program.m_baseCurrCode;
				txtPerCount.Text = "0";
				txtDC.Text = Program.GetFaceDisValue();
				txtDC.Enabled = true;
				chkSync.Enabled = true;
				txtGDepo.Enabled = true;
			}
			else
			{
				btnMC.ForeColor = Color.Red;
				if (dgvTBHis.CurrentRow != null)
				{
					btnGM.Enabled = true;
					NumericUpDown numericUpDown2 = nudDay;
					DateTimePicker dateTimePicker3 = dtpCome;
					DateTimePicker dateTimePicker4 = dtpLevel;
					bool flag6 = (dtpTime.Enabled = false);
					bool flag8 = (dateTimePicker4.Enabled = flag6);
					bool enabled2 = (dateTimePicker3.Enabled = flag8);
					numericUpDown2.Enabled = enabled2;
					dtpCome.Value = Convert.ToDateTime(dgvTBHis.CurrentRow.Cells["Team_cometime"].Value);
					DateTimePicker dateTimePicker5 = dtpLevel;
					DateTime value = (dtpTime.Value = Convert.ToDateTime(dgvTBHis.CurrentRow.Cells["team_stand_L_time"].Value));
					dateTimePicker5.Value = value;
					txtPerCount.Text = dgvTBHis.CurrentRow.Cells["team_percount"].Value.ToString();
					txtDC.Text = Program.GetFaceDisValue(Convert.ToDouble(dgvTBHis.CurrentRow.Cells["team_discount"].Value));
					txtDC.Enabled = false;
					chkSync.Enabled = false;
					txtGDepo.Text = "0";
					txtGDepo.Enabled = false;
				}
			}
			btnSear_Click(null, null);
		}
		catch
		{
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnTDel_Click(object sender, EventArgs e)
	{
		try
		{
			if (Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtNTM.Text.Trim(), chk: true) || Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtNGuide.Text.Trim(), chk: true) || Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtNCernum.Text.Trim(), chk: true))
			{
				return;
			}
			string text = "";
			if (m_TID <= 0)
			{
				text = string.Format((string)m_htab["Err02"], cobTB.Text.Trim() + "\r\n", txtNTM.Text.Trim() + "\r\n");
				Program.MsgCustom(text, MessageBoxIcon.Hand);
				return;
			}
			text = string.Format((string)m_htab["Info16"], label1.Text + txtNTM.Text.Trim() + "\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			string sql = "Select * From T_Rooms Where Team_id = " + m_TID;
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null)
			{
				Program.MsgCusErrMess("Is Null", btnTDel.Text + " " + txtNTM.Text.Trim());
				return;
			}
			if (dataTable.Rows.Count > 0)
			{
				text = string.Format((string)m_htab["Err03"], label1.Text + txtNTM.Text.Trim() + "\r\n");
				Program.MsgCustom(text, MessageBoxIcon.Exclamation);
				return;
			}
			sql = "Update T_Team Set team_flag = 1, updatetime = GetDate(), updator_id = " + Program.m_opid + ", updator = N'" + Program.m_OperName + "' where team_id = " + m_TID.ToString() + " \n";
			sql = sql + " Update D_Rooms Set R_RSID = 1 Where R_ID in (Select r_id from T_Schedule Where g_teamid = " + m_TID + ") \n";
			string text2 = sql;
			sql = text2 + " Update T_Schedule Set sch_flag = 1, sch_memo = N'" + Text + "-" + btnTDel.Text + "' where g_teamid = " + m_TID + " \n";
			if (Program.DBCompExec(sql, btnTDel.Text) < 0)
			{
				Program.MsgBox((string)m_htab["Err04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Program.fm != null)
			{
				Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
			}
			btnTBHis_Click(null, null);
		}
		catch (Exception ex)
		{
			string text3 = btnTDel.Text + " " + txtNTM.Text.Trim() + "\r\n";
			Program.MsgCustom(text3 + ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void txtDC_Leave(object sender, EventArgs e)
	{
	}

	private void txtDC_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\b' && (e.KeyChar > '9' || e.KeyChar < '0'))
		{
			e.Handled = true;
		}
	}

	private void txtDC_TextChanged(object sender, EventArgs e)
	{
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		int result = 0;
		int.TryParse(txtDC.Text.Trim(), out result);
		if (result < 0)
		{
			txtDC.Text = "0";
		}
		else if (result > 100)
		{
			txtDC.Text = "100";
		}
		try
		{
			chRoomPrice();
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void btnRef_Click(object sender, EventArgs e)
	{
		try
		{
			txtERn.Text = (string)m_htab["txtSRn"];
			InitDgvListColumn();
			InitTB();
			InitCerType();
			InitBuild();
			InitType();
			InitCurrency();
			chkInputGI.Enabled = !Program.m_chkGInfo;
			try
			{
				nudDay.Value = Convert.ToDecimal(Program.m_defDay);
				dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
				dtpCome.Value = Convert.ToDateTime(Program.GetLocDate(DateTime.Now) + " " + Program.m_defComeTime + ":00");
				cobCurrency.Text = Program.m_baseCurrCode;
			}
			catch
			{
			}
		}
		catch
		{
		}
	}

	private void btnNTB_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(flowLayoutPanel1.Location.X + panel1.Location.X + cplMain.Location.X + btnNTB.Location.X - 20, flowLayoutPanel1.Location.Y + panel1.Location.Y + cplMain.Location.Y + btnNTB.Location.Y - 3);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_add"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnNTB_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void btnETB_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(flowLayoutPanel1.Location.X + panel1.Location.X + cplMain.Location.X + btnETB.Location.X - 20, flowLayoutPanel1.Location.Y + panel1.Location.Y + cplMain.Location.Y + btnETB.Location.Y - 3);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_edit"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnETB_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void btnDTB_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(flowLayoutPanel1.Location.X + panel1.Location.X + cplMain.Location.X + btnDTB.Location.X - 20, flowLayoutPanel1.Location.Y + panel1.Location.Y + cplMain.Location.Y + btnDTB.Location.Y - 3);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_delete"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnDTB_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void btnRef_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(flowLayoutPanel1.Location.X + panel1.Location.X + cplMain.Location.X + btnRef.Location.X - 20, flowLayoutPanel1.Location.Y + panel1.Location.Y + cplMain.Location.Y + btnRef.Location.Y - 3);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_refresh"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnRef_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void txtPerCount_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtPerCount_Leave(object sender, EventArgs e)
	{
		try
		{
			int num = int.Parse(txtPerCount.Text);
			txtPerCount.Clear();
			txtPerCount.Text = num.ToString();
		}
		catch (Exception ex)
		{
			txtPerCount.Text = "1";
			Console.Write(ex.Message.ToString());
		}
	}

	private void txtGDepo_TextChanged(object sender, EventArgs e)
	{
		try
		{
			basepaid = Convert.ToDouble(txtGDepo.Text) * Convert.ToDouble(cobCurrency.SelectedValue);
		}
		catch
		{
		}
	}

	private void cobCurrency_SelectedValueChanged(object sender, EventArgs e)
	{
		try
		{
			double num = Convert.ToDouble(cobCurrency.SelectedValue);
			txtGDepo.Text = (basepaid / num).ToString("F2");
		}
		catch
		{
		}
	}
}
