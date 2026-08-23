using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using ComponentDll;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmCenter : Form
{
	private class ListViewGroupSorter : IComparer
	{
		private SortOrder order;

		public ListViewGroupSorter(SortOrder theOrder)
		{
			order = theOrder;
		}

		public int Compare(object x, object y)
		{
			int num = string.Compare(((ListViewGroup)x).Header, ((ListViewGroup)y).Header);
			if (order == SortOrder.Ascending)
			{
				return num;
			}
			return -num;
		}
	}

	private IContainer components;

	private Panel panel1;

	private SplitContainer splitContainer1;

	private ToolsBtn toolsBtn1;

	private TreeView tvList;

	private clsBackPanel clsBackPanel3;

	private Panel clsPlRt;

	private ToolsBtn toolsBtn3;

	private ListView dgvList;

	private ImageList imgTV;

	private ComboBox cobType;

	private ComboBox cobStatus;

	private TextBox txtSRn;

	private ImageList imgRoom;

	private ToolsBtn toolsBtn5;

	private ToolTip ttMsg;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label18;

	private Label label19;

	private Label label20;

	private Label label21;

	private Label label22;

	private Label label23;

	private Label label24;

	private Label label25;

	private Label label13;

	private Label label12;

	private Label label11;

	private Label label10;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private NGlassBtn btnRInfo;

	private NGlassBtn btnRGLevel;

	private TextBox txtRMemo;

	private NGlassBtn btnTGO;

	private Panel plR3;

	private Panel plR2;

	private TextBox txtRn;

	private TextBox txtGn;

	private TextBox txtCernum;

	private ComboBox cobCer;

	private DateTimePicker dtpLevel;

	private NumericUpDown nudDay;

	private Label labArr;

	private CheckBox chkHr;

	private TextBox txtGDepo;

	private ComboBox cobCurrency;

	private LockSoftware.Controls.GlassBtn btnCard;

	private TextBox txtGC;

	private Timer tSync;

	private DateTimePicker dtpTime;

	private Panel plR1;

	private Label label1;

	private Label label27;

	private Label label26;

	private Label label17;

	private Label label30;

	private Label label29;

	private Label label28;

	private Label label32;

	private Label label31;

	private Label label33;

	private clsBackPanel clsBackPanel1;

	private TableLayoutPanel tableLayoutPanel1;

	private PictureBox pictureBox1;

	private Label label38;

	private Label label40;

	private TextBox txtLRn;

	private Label label35;

	private Label label39;

	private Label label42;

	private Label label41;

	private Label label43;

	private Label label44;

	private Label label45;

	private LockSoftware.Controls.GlassBtn btnLN;

	private LockSoftware.Controls.GlassBtn btnLC;

	private LockSoftware.Controls.GlassBtn btnRC;

	private Label label46;

	private Label label52;

	private Label label51;

	private Label label50;

	private Label label49;

	private Label label48;

	private Label label47;

	private Label label37;

	private Label label36;

	private Label label56;

	private Label label55;

	private Label label57;

	private LockSoftware.Controls.GlassBtn btnGCSO;

	private Label label58;

	private TextBox txtTGN;

	private DataGridView dgvTGList;

	private LockSoftware.Controls.GlassBtn btnTGL;

	private Panel panel2;

	private StatusStrip statusStrip1;

	private ToolStripDropDownButton TSDDBtnRead;

	private ToolStripDropDownButton TSDDBtnGetTeam;

	private ToolStripStatusLabel TSSLab01;

	private ToolStripStatusLabel TSSLab02;

	private ToolStripStatusLabel TSSLab03;

	private ToolStripStatusLabel TSSLab04;

	private ToolStripStatusLabel TSSLab05;

	private ToolStripStatusLabel TSSLab06;

	private NGlassBtn btnTS;

	private clsBackPanel clsBackPanel2;

	private TableLayoutPanel tableLayoutPanel2;

	private ToolsBtn toolsBtn2;

	private NGlassBtn btnTGIn;

	private Timer tRefRoom;

	private Label label59;

	private Label label60;

	private Label label61;

	private Label label63;

	private Label label64;

	private ToolsBtn toolsBtn4;

	private ToolsBtn toolsBtn6;

	private ToolsBtn toolsBtn7;

	private ToolsBtn toolsBtn8;

	private ToolsBtn toolsBtn9;

	private ToolsBtn toolsBtn10;

	private ToolsBtn toolsBtn11;

	private clsBackPanel clsBackPanel4;

	private ToolsBtn toolsBtn13;

	private ToolsBtn toolsBtn12;

	private clsBackPanel clsBackPanel5;

	private Label label65;

	private Label label66;

	private Label label67;

	private Label label68;

	private Label label69;

	private Label label70;

	private Label label71;

	private Label label72;

	private Label label73;

	private Label label74;

	private Label label75;

	private Label label76;

	private Label label77;

	private Label label78;

	private Label label79;

	private Label label80;

	private clsBackPanel clsBackPanel6;

	private clsBackPanel clsBackPanel7;

	private clsBackPanel clsBackPanel8;

	private clsBackPanel clsBackPanel9;

	private clsBackPanel clsBackPanel10;

	private clsBackPanel clsBackPanel11;

	private clsBackPanel clsBackPanel12;

	private Label label82;

	private TextBox txtGuide;

	private Label label81;

	private TextBox txtGuideCer;

	private Label label83;

	private TextBox txtGuideCernum;

	private ToolsBtn btnChk;

	private LockSoftware.Controls.GlassBtn btnTGSO;

	private ContextMenuStrip cMSRoom;

	private ToolStripMenuItem TSMIRName;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem TSMIRCard;

	private ToolStripMenuItem TSMITCard;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem TSMIRSCh;

	private ToolStripMenuItem TSMISub01;

	private ToolStripMenuItem TSMISub02;

	private ToolStripMenuItem TSMISub03;

	private ToolStripMenuItem TSMISub04;

	private ToolStripMenuItem TSMISub05;

	private ToolStripMenuItem TSMISub06;

	private ToolStripMenuItem TSMISub07;

	private ToolStripMenuItem TSMISub08;

	private ToolStripMenuItem TSMISub09;

	private ToolStripMenuItem TSMISubRLog;

	private ToolStripMenuItem TSMISubGLog;

	private ToolStripMenuItem TSMIRCh;

	private clsBackPanel clsBackPanel13;

	private TextBox txtTGRn;

	private TextBox txtCurRn;

	private Label label84;

	private Label label85;

	private ToolsBtn btnClCh;

	private FlowLayoutPanel flowLayoutPanel1;

	public LockSoftware.Controls.GlassBtn btnOK;

	private Label label86;

	private Label label87;

	private Label label88;

	private ToolStripMenuItem TSMIEBR;

	private TextBox txtBM;

	private TableLayoutPanel tlpR2;

	private Panel panel3;

	private NGlassBtn btnIDCard;

	private Label label89;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem TSMISubOth;

	private Panel panel5;

	public DateTimePicker dtpCome;

	public CheckBox chkSync;

	public ToolsBtn btnRefresh;

	private Label label92;

	private TextBox txtDiscount;

	private TextBox txtRP;

	private CheckBox chkRepl;

	private Label label54;

	private Label label53;

	private Label label34;

	private Label label90;

	private Label label62;

	public ToolsBtn btnSear;

	public string m_objName = "WFRc";

	public Hashtable m_htab;

	private Label lb_1 = new Label();

	public bool m_chVal;

	public Panel ActivePanel = new Panel();

	private ListViewItem m_SelectItem;

	private double basePrice;

	private bool isRunningXPOrLater = OSFeature.Feature.IsPresent(OSFeature.Themes);

	private Hashtable[] lvGroupTab;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmCenter));
		this.panel1 = new System.Windows.Forms.Panel();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.tvList = new System.Windows.Forms.TreeView();
		this.imgTV = new System.Windows.Forms.ImageList(this.components);
		this.dgvList = new System.Windows.Forms.ListView();
		this.imgRoom = new System.Windows.Forms.ImageList(this.components);
		this.clsPlRt = new System.Windows.Forms.Panel();
		this.plR3 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.dgvTGList = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.TSDDBtnRead = new System.Windows.Forms.ToolStripDropDownButton();
		this.TSDDBtnGetTeam = new System.Windows.Forms.ToolStripDropDownButton();
		this.TSSLab01 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab03 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab04 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab05 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab06 = new System.Windows.Forms.ToolStripStatusLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.label58 = new System.Windows.Forms.Label();
		this.label81 = new System.Windows.Forms.Label();
		this.label82 = new System.Windows.Forms.Label();
		this.txtTGN = new System.Windows.Forms.TextBox();
		this.txtGuide = new System.Windows.Forms.TextBox();
		this.label83 = new System.Windows.Forms.Label();
		this.txtGuideCernum = new System.Windows.Forms.TextBox();
		this.txtGuideCer = new System.Windows.Forms.TextBox();
		this.plR2 = new System.Windows.Forms.Panel();
		this.tlpR2 = new System.Windows.Forms.TableLayoutPanel();
		this.label90 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.txtLRn = new System.Windows.Forms.TextBox();
		this.label44 = new System.Windows.Forms.Label();
		this.label87 = new System.Windows.Forms.Label();
		this.label42 = new System.Windows.Forms.Label();
		this.label41 = new System.Windows.Forms.Label();
		this.label40 = new System.Windows.Forms.Label();
		this.label39 = new System.Windows.Forms.Label();
		this.label38 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.label55 = new System.Windows.Forms.Label();
		this.label88 = new System.Windows.Forms.Label();
		this.label54 = new System.Windows.Forms.Label();
		this.label53 = new System.Windows.Forms.Label();
		this.label52 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.label50 = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.label57 = new System.Windows.Forms.Label();
		this.label89 = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.label56 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.plR1 = new System.Windows.Forms.Panel();
		this.label34 = new System.Windows.Forms.Label();
		this.label92 = new System.Windows.Forms.Label();
		this.txtDiscount = new System.Windows.Forms.TextBox();
		this.txtRP = new System.Windows.Forms.TextBox();
		this.chkRepl = new System.Windows.Forms.CheckBox();
		this.txtGDepo = new System.Windows.Forms.TextBox();
		this.cobCurrency = new System.Windows.Forms.ComboBox();
		this.dtpTime = new System.Windows.Forms.DateTimePicker();
		this.txtGC = new System.Windows.Forms.TextBox();
		this.chkHr = new System.Windows.Forms.CheckBox();
		this.chkSync = new System.Windows.Forms.CheckBox();
		this.dtpLevel = new System.Windows.Forms.DateTimePicker();
		this.txtRn = new System.Windows.Forms.TextBox();
		this.txtGn = new System.Windows.Forms.TextBox();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.txtCernum = new System.Windows.Forms.TextBox();
		this.nudDay = new System.Windows.Forms.NumericUpDown();
		this.dtpCome = new System.Windows.Forms.DateTimePicker();
		this.label30 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.labArr = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.ttMsg = new System.Windows.Forms.ToolTip(this.components);
		this.tSync = new System.Windows.Forms.Timer(this.components);
		this.tRefRoom = new System.Windows.Forms.Timer(this.components);
		this.cMSRoom = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.TSMIRName = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISubRLog = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISubGLog = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIRSCh = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub01 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub02 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub03 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub04 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub05 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub06 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub07 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub08 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISub09 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIRCard = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMITCard = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIRCh = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIEBR = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMISubOth = new System.Windows.Forms.ToolStripMenuItem();
		this.toolsBtn2 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.label62 = new System.Windows.Forms.Label();
		this.label59 = new System.Windows.Forms.Label();
		this.label64 = new System.Windows.Forms.Label();
		this.label63 = new System.Windows.Forms.Label();
		this.btnRefresh = new LockSoftware.Controls.ToolsBtn(this.components);
		this.label60 = new System.Windows.Forms.Label();
		this.clsBackPanel4 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.label61 = new System.Windows.Forms.Label();
		this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.clsBackPanel5 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.toolsBtn4 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn9 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn11 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn12 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn6 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn10 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn7 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn8 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn13 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.label65 = new System.Windows.Forms.Label();
		this.label66 = new System.Windows.Forms.Label();
		this.label67 = new System.Windows.Forms.Label();
		this.label68 = new System.Windows.Forms.Label();
		this.label69 = new System.Windows.Forms.Label();
		this.label70 = new System.Windows.Forms.Label();
		this.label71 = new System.Windows.Forms.Label();
		this.label72 = new System.Windows.Forms.Label();
		this.label73 = new System.Windows.Forms.Label();
		this.label74 = new System.Windows.Forms.Label();
		this.label75 = new System.Windows.Forms.Label();
		this.label76 = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.label78 = new System.Windows.Forms.Label();
		this.label79 = new System.Windows.Forms.Label();
		this.label80 = new System.Windows.Forms.Label();
		this.clsBackPanel6 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel7 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel8 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel9 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel10 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel11 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel12 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel3 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnSear = new LockSoftware.Controls.ToolsBtn(this.components);
		this.txtBM = new System.Windows.Forms.TextBox();
		this.toolsBtn5 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.txtSRn = new System.Windows.Forms.TextBox();
		this.cobStatus = new System.Windows.Forms.ComboBox();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.toolsBtn3 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnRInfo = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnRGLevel = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnTGIn = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnTGL = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnTGSO = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnChk = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnTS = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnTGO = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnLN = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnLC = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnRC = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnIDCard = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnGCSO = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnCard = new LockSoftware.Controls.GlassBtn(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.txtRMemo = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.clsBackPanel13 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnClCh = new LockSoftware.Controls.ToolsBtn(this.components);
		this.label84 = new System.Windows.Forms.Label();
		this.txtCurRn = new System.Windows.Forms.TextBox();
		this.label86 = new System.Windows.Forms.Label();
		this.label85 = new System.Windows.Forms.Label();
		this.txtTGRn = new System.Windows.Forms.TextBox();
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel1.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.clsPlRt.SuspendLayout();
		this.plR3.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTGList).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.panel5.SuspendLayout();
		this.plR2.SuspendLayout();
		this.tlpR2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.plR1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).BeginInit();
		this.cMSRoom.SuspendLayout();
		this.clsBackPanel2.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.clsBackPanel3.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.clsBackPanel13.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.Controls.Add(this.splitContainer1);
		this.panel1.Controls.Add(this.toolsBtn1);
		this.panel1.Controls.Add(this.clsBackPanel1);
		this.panel1.Controls.Add(this.clsBackPanel13);
		this.panel1.Location = new System.Drawing.Point(6, 4);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(973, 588);
		this.panel1.TabIndex = 1;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(0, 38);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.tvList);
		this.splitContainer1.Panel1.Controls.Add(this.toolsBtn2);
		this.splitContainer1.Panel1.Controls.Add(this.clsBackPanel2);
		this.splitContainer1.Panel2.Controls.Add(this.dgvList);
		this.splitContainer1.Panel2.Controls.Add(this.clsBackPanel3);
		this.splitContainer1.Panel2.Controls.Add(this.toolsBtn3);
		this.splitContainer1.Panel2.Controls.Add(this.clsPlRt);
		this.splitContainer1.Size = new System.Drawing.Size(973, 495);
		this.splitContainer1.SplitterDistance = 224;
		this.splitContainer1.TabIndex = 1;
		this.tvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tvList.ImageIndex = 0;
		this.tvList.ImageList = this.imgTV;
		this.tvList.Location = new System.Drawing.Point(0, 0);
		this.tvList.MinimumSize = new System.Drawing.Size(80, 4);
		this.tvList.Name = "tvList";
		this.tvList.SelectedImageIndex = 0;
		this.tvList.Size = new System.Drawing.Size(224, 231);
		this.tvList.TabIndex = 1;
		this.tvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvList_AfterSelect);
		this.imgTV.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgTV.ImageStream");
		this.imgTV.TransparentColor = System.Drawing.Color.Transparent;
		this.imgTV.Images.SetKeyName(0, "OS00.png");
		this.imgTV.Images.SetKeyName(1, "46.png");
		this.imgTV.Images.SetKeyName(2, "ok.png");
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dgvList.ForeColor = System.Drawing.Color.FromArgb(0, 64, 64);
		this.dgvList.LargeImageList = this.imgRoom;
		this.dgvList.Location = new System.Drawing.Point(0, 51);
		this.dgvList.MultiSelect = false;
		this.dgvList.Name = "dgvList";
		this.dgvList.Size = new System.Drawing.Size(485, 444);
		this.dgvList.TabIndex = 1;
		this.dgvList.UseCompatibleStateImageBehavior = false;
		this.dgvList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(dgvList_ItemSelectionChanged);
		this.dgvList.MouseClick += new System.Windows.Forms.MouseEventHandler(dgvList_MouseClick);
		this.imgRoom.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgRoom.ImageStream");
		this.imgRoom.TransparentColor = System.Drawing.Color.Transparent;
		this.imgRoom.Images.SetKeyName(0, "05(1).png");
		this.imgRoom.Images.SetKeyName(1, "trashcan_full.ico");
		this.imgRoom.Images.SetKeyName(2, "synchour.png");
		this.imgRoom.Images.SetKeyName(3, "120px-Vista-Login_Manager.png");
		this.imgRoom.Images.SetKeyName(4, "54.png");
		this.imgRoom.Images.SetKeyName(5, "35(1).png");
		this.imgRoom.Images.SetKeyName(6, "Pic_07.png");
		this.imgRoom.Images.SetKeyName(7, "tt.ico");
		this.imgRoom.Images.SetKeyName(8, "v_stop.png");
		this.imgRoom.Images.SetKeyName(9, "Icon-1.png");
		this.imgRoom.Images.SetKeyName(10, "Icon-2.png");
		this.clsPlRt.Controls.Add(this.btnRInfo);
		this.clsPlRt.Controls.Add(this.btnRGLevel);
		this.clsPlRt.Controls.Add(this.btnTGIn);
		this.clsPlRt.Controls.Add(this.plR3);
		this.clsPlRt.Controls.Add(this.plR2);
		this.clsPlRt.Controls.Add(this.plR1);
		this.clsPlRt.Dock = System.Windows.Forms.DockStyle.Right;
		this.clsPlRt.Location = new System.Drawing.Point(495, 0);
		this.clsPlRt.Margin = new System.Windows.Forms.Padding(0);
		this.clsPlRt.Name = "clsPlRt";
		this.clsPlRt.Size = new System.Drawing.Size(250, 495);
		this.clsPlRt.TabIndex = 3;
		this.plR3.AutoScroll = true;
		this.plR3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.plR3.Controls.Add(this.panel2);
		this.plR3.Controls.Add(this.btnTGL);
		this.plR3.Controls.Add(this.btnTGSO);
		this.plR3.Controls.Add(this.panel5);
		this.plR3.Controls.Add(this.btnTGO);
		this.plR3.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.plR3.Location = new System.Drawing.Point(3, 38);
		this.plR3.Margin = new System.Windows.Forms.Padding(0);
		this.plR3.MinimumSize = new System.Drawing.Size(100, 80);
		this.plR3.Name = "plR3";
		this.plR3.Padding = new System.Windows.Forms.Padding(1);
		this.plR3.Size = new System.Drawing.Size(245, 473);
		this.plR3.TabIndex = 8;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.dgvTGList);
		this.panel2.Controls.Add(this.statusStrip1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(1, 121);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(241, 285);
		this.panel2.TabIndex = 38;
		this.dgvTGList.AllowUserToAddRows = false;
		this.dgvTGList.AllowUserToDeleteRows = false;
		this.dgvTGList.BackgroundColor = System.Drawing.Color.White;
		this.dgvTGList.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dgvTGList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTGList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvTGList.Location = new System.Drawing.Point(0, 0);
		this.dgvTGList.Margin = new System.Windows.Forms.Padding(0);
		this.dgvTGList.Name = "dgvTGList";
		this.dgvTGList.ReadOnly = true;
		this.dgvTGList.RowHeadersVisible = false;
		this.dgvTGList.RowTemplate.Height = 23;
		this.dgvTGList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTGList.Size = new System.Drawing.Size(239, 179);
		this.dgvTGList.TabIndex = 36;
		this.dgvTGList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvTGList_ColumnHeaderMouseClick);
		this.statusStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
		this.statusStrip1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.TSDDBtnRead, this.TSDDBtnGetTeam, this.TSSLab01, this.TSSLab02, this.TSSLab03, this.TSSLab04, this.TSSLab05, this.TSSLab06 });
		this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
		this.statusStrip1.Location = new System.Drawing.Point(0, 179);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(239, 104);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 37;
		this.statusStrip1.Text = "statusStrip1";
		this.TSDDBtnRead.BackColor = System.Drawing.Color.WhiteSmoke;
		this.TSDDBtnRead.Image = LockSoftware.Properties.Resources.SHOW_CARD;
		this.TSDDBtnRead.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSDDBtnRead.Margin = new System.Windows.Forms.Padding(0);
		this.TSDDBtnRead.Name = "TSDDBtnRead";
		this.TSDDBtnRead.ShowDropDownArrow = false;
		this.TSDDBtnRead.Size = new System.Drawing.Size(56, 20);
		this.TSDDBtnRead.Text = "读卡";
		this.TSDDBtnRead.Click += new System.EventHandler(TSDDBtnRead_Click);
		this.TSDDBtnGetTeam.Image = LockSoftware.Properties.Resources.EmployeeQuery;
		this.TSDDBtnGetTeam.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSDDBtnGetTeam.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.TSDDBtnGetTeam.Name = "TSDDBtnGetTeam";
		this.TSDDBtnGetTeam.ShowDropDownArrow = false;
		this.TSDDBtnGetTeam.Size = new System.Drawing.Size(84, 20);
		this.TSDDBtnGetTeam.Text = "查看成员";
		this.TSDDBtnGetTeam.Click += new System.EventHandler(TSDDBtnGetTeam_Click);
		this.TSSLab01.AutoSize = false;
		this.TSSLab01.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab01.Margin = new System.Windows.Forms.Padding(0);
		this.TSSLab01.Name = "TSSLab01";
		this.TSSLab01.Size = new System.Drawing.Size(128, 28);
		this.TSSLab01.Text = "共有客房：";
		this.TSSLab01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab02.AutoSize = false;
		this.TSSLab02.BackColor = System.Drawing.Color.Gold;
		this.TSSLab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab02.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab02.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab02.Margin = new System.Windows.Forms.Padding(0);
		this.TSSLab02.Name = "TSSLab02";
		this.TSSLab02.Size = new System.Drawing.Size(80, 26);
		this.TSSLab02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab03.AutoSize = false;
		this.TSSLab03.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab03.Margin = new System.Windows.Forms.Padding(0);
		this.TSSLab03.Name = "TSSLab03";
		this.TSSLab03.Size = new System.Drawing.Size(128, 28);
		this.TSSLab03.Text = "共有成员：";
		this.TSSLab03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab04.AutoSize = false;
		this.TSSLab04.BackColor = System.Drawing.Color.Gold;
		this.TSSLab04.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab04.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab04.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab04.Margin = new System.Windows.Forms.Padding(0);
		this.TSSLab04.Name = "TSSLab04";
		this.TSSLab04.Size = new System.Drawing.Size(80, 26);
		this.TSSLab04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab05.AutoSize = false;
		this.TSSLab05.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab05.Margin = new System.Windows.Forms.Padding(0);
		this.TSSLab05.Name = "TSSLab05";
		this.TSSLab05.Size = new System.Drawing.Size(128, 28);
		this.TSSLab05.Text = "共有卡片：";
		this.TSSLab05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab06.AutoSize = false;
		this.TSSLab06.BackColor = System.Drawing.Color.Gold;
		this.TSSLab06.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab06.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab06.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab06.Margin = new System.Windows.Forms.Padding(0);
		this.TSSLab06.Name = "TSSLab06";
		this.TSSLab06.Size = new System.Drawing.Size(80, 26);
		this.TSSLab06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel5.Controls.Add(this.label58);
		this.panel5.Controls.Add(this.label81);
		this.panel5.Controls.Add(this.label82);
		this.panel5.Controls.Add(this.txtTGN);
		this.panel5.Controls.Add(this.btnChk);
		this.panel5.Controls.Add(this.txtGuide);
		this.panel5.Controls.Add(this.label83);
		this.panel5.Controls.Add(this.txtGuideCernum);
		this.panel5.Controls.Add(this.btnTS);
		this.panel5.Controls.Add(this.txtGuideCer);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(1, 33);
		this.panel5.Margin = new System.Windows.Forms.Padding(0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(241, 88);
		this.panel5.TabIndex = 50;
		this.label58.Location = new System.Drawing.Point(4, 2);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(90, 28);
		this.label58.TabIndex = 0;
		this.label58.Text = "团队名称：";
		this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label81.Location = new System.Drawing.Point(4, 30);
		this.label81.Name = "label81";
		this.label81.Size = new System.Drawing.Size(90, 28);
		this.label81.TabIndex = 42;
		this.label81.Text = "领队姓名：";
		this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label82.Location = new System.Drawing.Point(4, 58);
		this.label82.Name = "label82";
		this.label82.Size = new System.Drawing.Size(88, 28);
		this.label82.TabIndex = 44;
		this.label82.Text = "领队证件：";
		this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtTGN.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtTGN.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtTGN.Location = new System.Drawing.Point(97, 6);
		this.txtTGN.Name = "txtTGN";
		this.txtTGN.Size = new System.Drawing.Size(107, 21);
		this.txtTGN.TabIndex = 1;
		this.txtGuide.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtGuide.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtGuide.Location = new System.Drawing.Point(97, 33);
		this.txtGuide.Name = "txtGuide";
		this.txtGuide.Size = new System.Drawing.Size(107, 21);
		this.txtGuide.TabIndex = 43;
		this.label83.AutoSize = true;
		this.label83.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label83.ForeColor = System.Drawing.Color.Red;
		this.label83.Location = new System.Drawing.Point(202, 39);
		this.label83.Name = "label83";
		this.label83.Size = new System.Drawing.Size(15, 16);
		this.label83.TabIndex = 46;
		this.label83.Text = "*";
		this.label83.Visible = false;
		this.txtGuideCernum.Location = new System.Drawing.Point(111, 60);
		this.txtGuideCernum.Name = "txtGuideCernum";
		this.txtGuideCernum.Size = new System.Drawing.Size(80, 21);
		this.txtGuideCernum.TabIndex = 47;
		this.txtGuideCernum.Visible = false;
		this.txtGuideCer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtGuideCer.Location = new System.Drawing.Point(97, 60);
		this.txtGuideCer.Name = "txtGuideCer";
		this.txtGuideCer.Size = new System.Drawing.Size(107, 21);
		this.txtGuideCer.TabIndex = 45;
		this.txtGuideCer.TextChanged += new System.EventHandler(txtGuideCer_TextChanged);
		this.plR2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.plR2.Controls.Add(this.btnLN);
		this.plR2.Controls.Add(this.btnLC);
		this.plR2.Controls.Add(this.tlpR2);
		this.plR2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.plR2.Location = new System.Drawing.Point(3, 129);
		this.plR2.Margin = new System.Windows.Forms.Padding(0);
		this.plR2.Name = "plR2";
		this.plR2.Size = new System.Drawing.Size(245, 485);
		this.plR2.TabIndex = 9;
		this.tlpR2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
		this.tlpR2.ColumnCount = 2;
		this.tlpR2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tlpR2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tlpR2.Controls.Add(this.label90, 1, 11);
		this.tlpR2.Controls.Add(this.panel3, 1, 0);
		this.tlpR2.Controls.Add(this.label44, 0, 10);
		this.tlpR2.Controls.Add(this.label87, 0, 9);
		this.tlpR2.Controls.Add(this.label42, 0, 7);
		this.tlpR2.Controls.Add(this.label41, 0, 6);
		this.tlpR2.Controls.Add(this.label40, 0, 5);
		this.tlpR2.Controls.Add(this.label39, 0, 4);
		this.tlpR2.Controls.Add(this.label38, 0, 3);
		this.tlpR2.Controls.Add(this.label37, 0, 2);
		this.tlpR2.Controls.Add(this.label36, 0, 1);
		this.tlpR2.Controls.Add(this.label55, 1, 10);
		this.tlpR2.Controls.Add(this.label88, 1, 9);
		this.tlpR2.Controls.Add(this.label54, 1, 8);
		this.tlpR2.Controls.Add(this.label53, 1, 7);
		this.tlpR2.Controls.Add(this.label52, 1, 6);
		this.tlpR2.Controls.Add(this.label51, 1, 5);
		this.tlpR2.Controls.Add(this.label50, 1, 4);
		this.tlpR2.Controls.Add(this.label49, 1, 3);
		this.tlpR2.Controls.Add(this.label47, 1, 1);
		this.tlpR2.Controls.Add(this.label48, 1, 2);
		this.tlpR2.Controls.Add(this.label46, 0, 13);
		this.tlpR2.Controls.Add(this.label57, 1, 13);
		this.tlpR2.Controls.Add(this.label89, 0, 11);
		this.tlpR2.Controls.Add(this.label45, 0, 12);
		this.tlpR2.Controls.Add(this.label56, 1, 12);
		this.tlpR2.Controls.Add(this.label43, 0, 8);
		this.tlpR2.Controls.Add(this.label35, 0, 0);
		this.tlpR2.Dock = System.Windows.Forms.DockStyle.Top;
		this.tlpR2.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tlpR2.Location = new System.Drawing.Point(0, 0);
		this.tlpR2.Name = "tlpR2";
		this.tlpR2.RowCount = 14;
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tlpR2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpR2.Size = new System.Drawing.Size(243, 410);
		this.tlpR2.TabIndex = 51;
		this.label90.AutoSize = true;
		this.label90.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label90.ForeColor = System.Drawing.Color.Green;
		this.label90.Location = new System.Drawing.Point(88, 320);
		this.label90.Name = "label90";
		this.label90.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label90.Size = new System.Drawing.Size(45, 17);
		this.label90.TabIndex = 52;
		this.label90.Text = "label90";
		this.panel3.Controls.Add(this.txtLRn);
		this.panel3.Controls.Add(this.btnRC);
		this.panel3.Location = new System.Drawing.Point(85, 1);
		this.panel3.Margin = new System.Windows.Forms.Padding(0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(158, 28);
		this.panel3.TabIndex = 53;
		this.txtLRn.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtLRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtLRn.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLRn.ForeColor = System.Drawing.Color.Black;
		this.txtLRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtLRn.Location = new System.Drawing.Point(1, 3);
		this.txtLRn.Margin = new System.Windows.Forms.Padding(0);
		this.txtLRn.Name = "txtLRn";
		this.txtLRn.Size = new System.Drawing.Size(87, 22);
		this.txtLRn.TabIndex = 22;
		this.label44.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label44.Location = new System.Drawing.Point(4, 291);
		this.label44.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(80, 28);
		this.label44.TabIndex = 30;
		this.label44.Text = "客房费用：";
		this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label87.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label87.Location = new System.Drawing.Point(4, 262);
		this.label87.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label87.Name = "label87";
		this.label87.Size = new System.Drawing.Size(80, 28);
		this.label87.TabIndex = 49;
		this.label87.Text = "已住天数：";
		this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label42.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label42.Location = new System.Drawing.Point(4, 204);
		this.label42.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label42.Name = "label42";
		this.label42.Size = new System.Drawing.Size(80, 28);
		this.label42.TabIndex = 28;
		this.label42.Text = "预住天数：";
		this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label41.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label41.Location = new System.Drawing.Point(4, 175);
		this.label41.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(80, 28);
		this.label41.TabIndex = 27;
		this.label41.Text = "入住时间：";
		this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label40.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label40.Location = new System.Drawing.Point(4, 146);
		this.label40.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(80, 28);
		this.label40.TabIndex = 24;
		this.label40.Text = "宾客姓名：";
		this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label39.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label39.Location = new System.Drawing.Point(4, 117);
		this.label39.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(80, 28);
		this.label39.TabIndex = 26;
		this.label39.Text = "入住人数：";
		this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label38.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label38.Location = new System.Drawing.Point(4, 88);
		this.label38.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(80, 28);
		this.label38.TabIndex = 25;
		this.label38.Text = "客房类型：";
		this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label37.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(4, 59);
		this.label37.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(80, 28);
		this.label37.TabIndex = 37;
		this.label37.Text = "层 名 称：";
		this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label36.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label36.Location = new System.Drawing.Point(4, 30);
		this.label36.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(80, 28);
		this.label36.TabIndex = 36;
		this.label36.Text = "楼 名 称：";
		this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label55.AutoSize = true;
		this.label55.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label55.ForeColor = System.Drawing.Color.Green;
		this.label55.Location = new System.Drawing.Point(88, 291);
		this.label55.Name = "label55";
		this.label55.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label55.Size = new System.Drawing.Size(45, 17);
		this.label55.TabIndex = 46;
		this.label55.Text = "label55";
		this.label88.AutoSize = true;
		this.label88.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label88.ForeColor = System.Drawing.Color.Green;
		this.label88.Location = new System.Drawing.Point(88, 262);
		this.label88.Name = "label88";
		this.label88.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label88.Size = new System.Drawing.Size(45, 17);
		this.label88.TabIndex = 50;
		this.label88.Text = "label88";
		this.label54.AutoSize = true;
		this.label54.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label54.Location = new System.Drawing.Point(88, 233);
		this.label54.Name = "label54";
		this.label54.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label54.Size = new System.Drawing.Size(45, 17);
		this.label54.TabIndex = 45;
		this.label54.Text = "label54";
		this.label53.AutoSize = true;
		this.label53.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label53.Location = new System.Drawing.Point(88, 204);
		this.label53.Name = "label53";
		this.label53.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label53.Size = new System.Drawing.Size(45, 17);
		this.label53.TabIndex = 44;
		this.label53.Text = "label53";
		this.label52.AutoSize = true;
		this.label52.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label52.Location = new System.Drawing.Point(88, 175);
		this.label52.Name = "label52";
		this.label52.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label52.Size = new System.Drawing.Size(45, 17);
		this.label52.TabIndex = 43;
		this.label52.Text = "label52";
		this.label51.AutoSize = true;
		this.label51.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label51.Location = new System.Drawing.Point(88, 146);
		this.label51.Name = "label51";
		this.label51.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label51.Size = new System.Drawing.Size(45, 17);
		this.label51.TabIndex = 42;
		this.label51.Text = "label51";
		this.label50.AutoSize = true;
		this.label50.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label50.Location = new System.Drawing.Point(88, 117);
		this.label50.Name = "label50";
		this.label50.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label50.Size = new System.Drawing.Size(45, 17);
		this.label50.TabIndex = 41;
		this.label50.Text = "label50";
		this.label49.AutoSize = true;
		this.label49.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label49.Location = new System.Drawing.Point(88, 88);
		this.label49.Name = "label49";
		this.label49.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label49.Size = new System.Drawing.Size(45, 17);
		this.label49.TabIndex = 40;
		this.label49.Text = "label49";
		this.label47.AutoSize = true;
		this.label47.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label47.Location = new System.Drawing.Point(88, 30);
		this.label47.Name = "label47";
		this.label47.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label47.Size = new System.Drawing.Size(45, 17);
		this.label47.TabIndex = 38;
		this.label47.Text = "label47";
		this.label48.AutoSize = true;
		this.label48.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label48.Location = new System.Drawing.Point(88, 59);
		this.label48.Name = "label48";
		this.label48.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label48.Size = new System.Drawing.Size(45, 17);
		this.label48.TabIndex = 39;
		this.label48.Text = "label48";
		this.label46.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label46.Location = new System.Drawing.Point(4, 378);
		this.label46.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(80, 28);
		this.label46.TabIndex = 32;
		this.label46.Text = "应 找 零：";
		this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label57.AutoSize = true;
		this.label57.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label57.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label57.Location = new System.Drawing.Point(88, 378);
		this.label57.Name = "label57";
		this.label57.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label57.Size = new System.Drawing.Size(45, 17);
		this.label57.TabIndex = 48;
		this.label57.Text = "label57";
		this.label89.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label89.Location = new System.Drawing.Point(4, 320);
		this.label89.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label89.Name = "label89";
		this.label89.Size = new System.Drawing.Size(80, 28);
		this.label89.TabIndex = 54;
		this.label89.Text = "其他费用：";
		this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label45.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label45.Location = new System.Drawing.Point(4, 349);
		this.label45.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(80, 28);
		this.label45.TabIndex = 31;
		this.label45.Text = "已付费用：";
		this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label56.AutoSize = true;
		this.label56.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label56.ForeColor = System.Drawing.Color.Green;
		this.label56.Location = new System.Drawing.Point(88, 349);
		this.label56.Name = "label56";
		this.label56.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label56.Size = new System.Drawing.Size(45, 17);
		this.label56.TabIndex = 47;
		this.label56.Text = "label56";
		this.label43.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label43.Location = new System.Drawing.Point(4, 233);
		this.label43.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(80, 28);
		this.label43.TabIndex = 29;
		this.label43.Text = "预退时间：";
		this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label35.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label35.Location = new System.Drawing.Point(4, 1);
		this.label35.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(80, 28);
		this.label35.TabIndex = 23;
		this.label35.Text = "客房名称：";
		this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.plR1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.plR1.Controls.Add(this.label34);
		this.plR1.Controls.Add(this.label92);
		this.plR1.Controls.Add(this.btnIDCard);
		this.plR1.Controls.Add(this.txtDiscount);
		this.plR1.Controls.Add(this.btnGCSO);
		this.plR1.Controls.Add(this.txtRP);
		this.plR1.Controls.Add(this.chkRepl);
		this.plR1.Controls.Add(this.btnCard);
		this.plR1.Controls.Add(this.txtGDepo);
		this.plR1.Controls.Add(this.cobCurrency);
		this.plR1.Controls.Add(this.dtpTime);
		this.plR1.Controls.Add(this.txtGC);
		this.plR1.Controls.Add(this.chkHr);
		this.plR1.Controls.Add(this.chkSync);
		this.plR1.Controls.Add(this.dtpLevel);
		this.plR1.Controls.Add(this.txtRn);
		this.plR1.Controls.Add(this.txtGn);
		this.plR1.Controls.Add(this.cobCer);
		this.plR1.Controls.Add(this.txtCernum);
		this.plR1.Controls.Add(this.nudDay);
		this.plR1.Controls.Add(this.dtpCome);
		this.plR1.Controls.Add(this.label30);
		this.plR1.Controls.Add(this.label29);
		this.plR1.Controls.Add(this.label28);
		this.plR1.Controls.Add(this.label27);
		this.plR1.Controls.Add(this.label26);
		this.plR1.Controls.Add(this.label17);
		this.plR1.Controls.Add(this.label1);
		this.plR1.Controls.Add(this.labArr);
		this.plR1.Controls.Add(this.label32);
		this.plR1.Controls.Add(this.label31);
		this.plR1.Controls.Add(this.label33);
		this.plR1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.plR1.Location = new System.Drawing.Point(3, 36);
		this.plR1.Margin = new System.Windows.Forms.Padding(0);
		this.plR1.Name = "plR1";
		this.plR1.Size = new System.Drawing.Size(245, 448);
		this.plR1.TabIndex = 10;
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label34.ForeColor = System.Drawing.Color.Red;
		this.label34.Location = new System.Drawing.Point(155, 249);
		this.label34.Margin = new System.Windows.Forms.Padding(3, 12, 0, 0);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(21, 14);
		this.label34.TabIndex = 47;
		this.label34.Text = "%";
		this.label92.Location = new System.Drawing.Point(5, 245);
		this.label92.Name = "label92";
		this.label92.Size = new System.Drawing.Size(80, 28);
		this.label92.TabIndex = 45;
		this.label92.Text = "Discount：";
		this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtDiscount.Location = new System.Drawing.Point(90, 246);
		this.txtDiscount.MaxLength = 5;
		this.txtDiscount.Name = "txtDiscount";
		this.txtDiscount.Size = new System.Drawing.Size(62, 22);
		this.txtDiscount.TabIndex = 46;
		this.txtDiscount.Text = "0";
		this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDiscount.TextChanged += new System.EventHandler(txtDiscount_TextChanged);
		this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiscount_KeyPress);
		this.txtRP.Location = new System.Drawing.Point(90, 295);
		this.txtRP.MaxLength = 12;
		this.txtRP.Name = "txtRP";
		this.txtRP.ReadOnly = true;
		this.txtRP.Size = new System.Drawing.Size(135, 22);
		this.txtRP.TabIndex = 38;
		this.txtRP.Text = "0";
		this.chkRepl.Location = new System.Drawing.Point(90, 269);
		this.chkRepl.Name = "chkRepl";
		this.chkRepl.Size = new System.Drawing.Size(135, 28);
		this.chkRepl.TabIndex = 24;
		this.chkRepl.Text = "Null Card";
		this.chkRepl.UseVisualStyleBackColor = true;
		this.chkRepl.CheckedChanged += new System.EventHandler(chkRepl_CheckedChanged);
		this.txtGDepo.Location = new System.Drawing.Point(90, 348);
		this.txtGDepo.MaxLength = 12;
		this.txtGDepo.Name = "txtGDepo";
		this.txtGDepo.Size = new System.Drawing.Size(62, 22);
		this.txtGDepo.TabIndex = 29;
		this.txtGDepo.Text = "0";
		this.txtGDepo.TextChanged += new System.EventHandler(txtGDepo_TextChanged);
		this.txtGDepo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGDepo_KeyPress);
		this.cobCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCurrency.FormattingEnabled = true;
		this.cobCurrency.Location = new System.Drawing.Point(160, 348);
		this.cobCurrency.Name = "cobCurrency";
		this.cobCurrency.Size = new System.Drawing.Size(65, 22);
		this.cobCurrency.TabIndex = 30;
		this.cobCurrency.SelectedValueChanged += new System.EventHandler(cobCurrency_SelectedValueChanged);
		this.dtpTime.CustomFormat = "HH:mm";
		this.dtpTime.Enabled = false;
		this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpTime.Location = new System.Drawing.Point(90, 219);
		this.dtpTime.Name = "dtpTime";
		this.dtpTime.ShowUpDown = true;
		this.dtpTime.Size = new System.Drawing.Size(135, 22);
		this.dtpTime.TabIndex = 26;
		this.txtGC.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtGC.Location = new System.Drawing.Point(90, 321);
		this.txtGC.MaxLength = 12;
		this.txtGC.Name = "txtGC";
		this.txtGC.ReadOnly = true;
		this.txtGC.Size = new System.Drawing.Size(135, 22);
		this.txtGC.TabIndex = 31;
		this.txtGC.Text = "0";
		this.chkHr.Location = new System.Drawing.Point(146, 166);
		this.chkHr.Name = "chkHr";
		this.chkHr.Size = new System.Drawing.Size(79, 28);
		this.chkHr.TabIndex = 22;
		this.chkHr.Text = "Hour Room";
		this.chkHr.UseVisualStyleBackColor = true;
		this.chkHr.CheckedChanged += new System.EventHandler(chkHr_CheckedChanged);
		this.chkSync.Location = new System.Drawing.Point(90, 138);
		this.chkSync.Name = "chkSync";
		this.chkSync.Size = new System.Drawing.Size(135, 28);
		this.chkSync.TabIndex = 6;
		this.chkSync.Text = "Local System Time";
		this.chkSync.UseVisualStyleBackColor = true;
		this.chkSync.CheckedChanged += new System.EventHandler(chkSync_CheckedChanged);
		this.dtpLevel.CustomFormat = "dd-MM-yyyy";
		this.dtpLevel.Enabled = false;
		this.dtpLevel.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevel.Location = new System.Drawing.Point(90, 194);
		this.dtpLevel.Name = "dtpLevel";
		this.dtpLevel.Size = new System.Drawing.Size(135, 22);
		this.dtpLevel.TabIndex = 9;
		this.txtRn.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRn.ForeColor = System.Drawing.Color.Black;
		this.txtRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtRn.Location = new System.Drawing.Point(90, 4);
		this.txtRn.Name = "txtRn";
		this.txtRn.ReadOnly = true;
		this.txtRn.Size = new System.Drawing.Size(135, 22);
		this.txtRn.TabIndex = 0;
		this.txtRn.TextChanged += new System.EventHandler(txtRn_TextChanged);
		this.txtGn.Location = new System.Drawing.Point(90, 33);
		this.txtGn.MaxLength = 50;
		this.txtGn.Name = "txtGn";
		this.txtGn.Size = new System.Drawing.Size(135, 22);
		this.txtGn.TabIndex = 1;
		this.cobCer.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 150;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(90, 60);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(107, 24);
		this.cobCer.TabIndex = 4;
		this.txtCernum.Location = new System.Drawing.Point(90, 85);
		this.txtCernum.MaxLength = 50;
		this.txtCernum.Name = "txtCernum";
		this.txtCernum.Size = new System.Drawing.Size(135, 22);
		this.txtCernum.TabIndex = 2;
		this.nudDay.Location = new System.Drawing.Point(90, 167);
		this.nudDay.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudDay.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.Name = "nudDay";
		this.nudDay.Size = new System.Drawing.Size(50, 22);
		this.nudDay.TabIndex = 17;
		this.nudDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.nudDay.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.ValueChanged += new System.EventHandler(nudDay_ValueChanged);
		this.dtpCome.CustomFormat = "dd-MM-yyyy HH:mm";
		this.dtpCome.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCome.Location = new System.Drawing.Point(90, 114);
		this.dtpCome.Name = "dtpCome";
		this.dtpCome.Size = new System.Drawing.Size(135, 22);
		this.dtpCome.TabIndex = 5;
		this.dtpCome.ValueChanged += new System.EventHandler(dtpCome_ValueChanged);
		this.label30.Location = new System.Drawing.Point(5, 217);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(80, 28);
		this.label30.TabIndex = 27;
		this.label30.Text = "Level Time:";
		this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label29.Location = new System.Drawing.Point(5, 189);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(80, 28);
		this.label29.TabIndex = 26;
		this.label29.Text = "Level Date:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label28.Location = new System.Drawing.Point(5, 161);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(80, 28);
		this.label28.TabIndex = 25;
		this.label28.Text = "Stay Day:";
		this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label27.Location = new System.Drawing.Point(5, 85);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(80, 28);
		this.label27.TabIndex = 24;
		this.label27.Text = "Number:";
		this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label26.Location = new System.Drawing.Point(5, 57);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(80, 28);
		this.label26.TabIndex = 23;
		this.label26.Text = "Certificate:";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label17.Location = new System.Drawing.Point(5, 29);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(80, 28);
		this.label17.TabIndex = 22;
		this.label17.Text = "Guest Name:";
		this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.Location = new System.Drawing.Point(5, 1);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 28);
		this.label1.TabIndex = 21;
		this.label1.Text = "Room Name:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labArr.Location = new System.Drawing.Point(5, 113);
		this.labArr.Name = "labArr";
		this.labArr.Size = new System.Drawing.Size(80, 28);
		this.labArr.TabIndex = 20;
		this.labArr.Text = "Arrival Date:";
		this.labArr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label32.Location = new System.Drawing.Point(5, 317);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(80, 28);
		this.label32.TabIndex = 34;
		this.label32.Text = "Room Deposit:";
		this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label31.Location = new System.Drawing.Point(5, 289);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(80, 28);
		this.label31.TabIndex = 33;
		this.label31.Text = "Room Price:";
		this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label33.Location = new System.Drawing.Point(5, 346);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(80, 28);
		this.label33.TabIndex = 35;
		this.label33.Text = "Paid Deposit:";
		this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ttMsg.AutomaticDelay = 100;
		this.ttMsg.AutoPopDelay = 5000;
		this.ttMsg.InitialDelay = 100;
		this.ttMsg.IsBalloon = true;
		this.ttMsg.ReshowDelay = 20;
		this.ttMsg.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		this.tSync.Interval = 1000;
		this.tSync.Tick += new System.EventHandler(tSync_Tick);
		this.cMSRoom.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cMSRoom.Items.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.TSMIRName, this.toolStripSeparator1, this.TSMIRCard, this.TSMITCard, this.toolStripSeparator2, this.TSMIRCh, this.TSMIEBR, this.toolStripSeparator4, this.TSMISubOth });
		this.cMSRoom.Name = "cMSRoom";
		this.cMSRoom.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.cMSRoom.Size = new System.Drawing.Size(135, 154);
		this.TSMIRName.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSMISubRLog, this.TSMISubGLog, this.toolStripSeparator3, this.TSMIRSCh });
		this.TSMIRName.Image = LockSoftware.Properties.Resources._05_1_;
		this.TSMIRName.Name = "TSMIRName";
		this.TSMIRName.Size = new System.Drawing.Size(134, 22);
		this.TSMISubRLog.Name = "TSMISubRLog";
		this.TSMISubRLog.Size = new System.Drawing.Size(146, 22);
		this.TSMISubRLog.Text = "客房消费日志";
		this.TSMISubRLog.Click += new System.EventHandler(TSMISubRLog_Click);
		this.TSMISubGLog.Name = "TSMISubGLog";
		this.TSMISubGLog.Size = new System.Drawing.Size(146, 22);
		this.TSMISubGLog.Text = "宾客入住日志";
		this.TSMISubGLog.Click += new System.EventHandler(TSMISubGLog_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
		this.TSMIRSCh.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.TSMISub01, this.TSMISub02, this.TSMISub03, this.TSMISub04, this.TSMISub05, this.TSMISub06, this.TSMISub07, this.TSMISub08, this.TSMISub09 });
		this.TSMIRSCh.Name = "TSMIRSCh";
		this.TSMIRSCh.Size = new System.Drawing.Size(146, 22);
		this.TSMIRSCh.Text = "更改状态";
		this.TSMISub01.Image = LockSoftware.Properties.Resources._05_1_;
		this.TSMISub01.Name = "TSMISub01";
		this.TSMISub01.Size = new System.Drawing.Size(102, 22);
		this.TSMISub01.Text = "空 房";
		this.TSMISub02.Image = LockSoftware.Properties.Resources.trashcan_full1;
		this.TSMISub02.Name = "TSMISub02";
		this.TSMISub02.Size = new System.Drawing.Size(102, 22);
		this.TSMISub02.Text = "空 脏";
		this.TSMISub03.Name = "TSMISub03";
		this.TSMISub03.Size = new System.Drawing.Size(102, 22);
		this.TSMISub03.Visible = false;
		this.TSMISub04.Name = "TSMISub04";
		this.TSMISub04.Size = new System.Drawing.Size(102, 22);
		this.TSMISub04.Visible = false;
		this.TSMISub05.Name = "TSMISub05";
		this.TSMISub05.Size = new System.Drawing.Size(102, 22);
		this.TSMISub05.Visible = false;
		this.TSMISub06.Name = "TSMISub06";
		this.TSMISub06.Size = new System.Drawing.Size(102, 22);
		this.TSMISub06.Visible = false;
		this.TSMISub07.Name = "TSMISub07";
		this.TSMISub07.Size = new System.Drawing.Size(102, 22);
		this.TSMISub07.Visible = false;
		this.TSMISub08.Name = "TSMISub08";
		this.TSMISub08.Size = new System.Drawing.Size(102, 22);
		this.TSMISub08.Visible = false;
		this.TSMISub09.Name = "TSMISub09";
		this.TSMISub09.Size = new System.Drawing.Size(102, 22);
		this.TSMISub09.Visible = false;
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
		this.TSMIRCard.Name = "TSMIRCard";
		this.TSMIRCard.Size = new System.Drawing.Size(134, 22);
		this.TSMIRCard.Text = "客人卡重写";
		this.TSMIRCard.Click += new System.EventHandler(TSMIRCard_Click);
		this.TSMITCard.Name = "TSMITCard";
		this.TSMITCard.Size = new System.Drawing.Size(134, 22);
		this.TSMITCard.Text = "团队卡重写";
		this.TSMITCard.Click += new System.EventHandler(TSMITCard_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(131, 6);
		this.TSMIRCh.Image = LockSoftware.Properties.Resources.Button_Refresh;
		this.TSMIRCh.Name = "TSMIRCh";
		this.TSMIRCh.Size = new System.Drawing.Size(134, 22);
		this.TSMIRCh.Text = "宾客换房";
		this.TSMIRCh.Click += new System.EventHandler(TSMIRCh_Click);
		this.TSMIEBR.Image = LockSoftware.Properties.Resources.synchour;
		this.TSMIEBR.Name = "TSMIEBR";
		this.TSMIEBR.Size = new System.Drawing.Size(134, 22);
		this.TSMIEBR.Text = "快速预订";
		this.TSMIEBR.Visible = false;
		this.TSMIEBR.Click += new System.EventHandler(TSMIEBR_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(131, 6);
		this.TSMISubOth.Name = "TSMISubOth";
		this.TSMISubOth.Size = new System.Drawing.Size(134, 22);
		this.TSMISubOth.Text = "其他消费";
		this.TSMISubOth.Click += new System.EventHandler(TSMISubOth_Click);
		this.toolsBtn2.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Checked = false;
		this.toolsBtn2.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.toolsBtn2.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn2.ImageNew = LockSoftware.Properties.Resources.mini_bottom;
		this.toolsBtn2.ImageRedrawed = true;
		this.toolsBtn2.ImageStyle = 0;
		this.toolsBtn2.isButton = true;
		this.toolsBtn2.Location = new System.Drawing.Point(0, 231);
		this.toolsBtn2.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn2.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn2.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn2.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn2.Name = "toolsBtn2";
		this.toolsBtn2.Size = new System.Drawing.Size(224, 10);
		this.toolsBtn2.TabIndex = 4;
		this.toolsBtn2.TextImageLocation = 0;
		this.toolsBtn2.TextNew = "";
		this.toolsBtn2.TextRedrawed = false;
		this.toolsBtn2.Click += new System.EventHandler(toolsBtn2_Click);
		this.clsBackPanel2.Border = true;
		this.clsBackPanel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderBW = 1;
		this.clsBackPanel2.BorderColorBottom = System.Drawing.SystemColors.GradientInactiveCaption;
		this.clsBackPanel2.BorderColorLeft = System.Drawing.SystemColors.GradientInactiveCaption;
		this.clsBackPanel2.BorderColorRight = System.Drawing.SystemColors.GradientInactiveCaption;
		this.clsBackPanel2.BorderColorTop = System.Drawing.SystemColors.GradientInactiveCaption;
		this.clsBackPanel2.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderLW = 1;
		this.clsBackPanel2.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderRW = 1;
		this.clsBackPanel2.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderTW = 1;
		this.clsBackPanel2.Color1 = System.Drawing.Color.White;
		this.clsBackPanel2.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.label62);
		this.clsBackPanel2.Controls.Add(this.label59);
		this.clsBackPanel2.Controls.Add(this.label64);
		this.clsBackPanel2.Controls.Add(this.label63);
		this.clsBackPanel2.Controls.Add(this.btnRefresh);
		this.clsBackPanel2.Controls.Add(this.label60);
		this.clsBackPanel2.Controls.Add(this.clsBackPanel4);
		this.clsBackPanel2.Controls.Add(this.label61);
		this.clsBackPanel2.Controls.Add(this.tableLayoutPanel2);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 241);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(224, 254);
		this.clsBackPanel2.TabIndex = 2;
		this.label62.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label62.BackColor = System.Drawing.Color.Transparent;
		this.label62.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label62.Location = new System.Drawing.Point(139, 2);
		this.label62.Name = "label62";
		this.label62.Size = new System.Drawing.Size(45, 17);
		this.label62.TabIndex = 22;
		this.label62.Text = "0";
		this.label59.AutoSize = true;
		this.label59.BackColor = System.Drawing.Color.Transparent;
		this.label59.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label59.Location = new System.Drawing.Point(2, 2);
		this.label59.Name = "label59";
		this.label59.Size = new System.Drawing.Size(67, 15);
		this.label59.TabIndex = 7;
		this.label59.Text = "共有客房：";
		this.label64.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label64.BackColor = System.Drawing.Color.Transparent;
		this.label64.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label64.ForeColor = System.Drawing.Color.Green;
		this.label64.Location = new System.Drawing.Point(139, 44);
		this.label64.Name = "label64";
		this.label64.Size = new System.Drawing.Size(45, 17);
		this.label64.TabIndex = 12;
		this.label64.Text = "0";
		this.label63.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label63.BackColor = System.Drawing.Color.Transparent;
		this.label63.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label63.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label63.Location = new System.Drawing.Point(139, 23);
		this.label63.Name = "label63";
		this.label63.Size = new System.Drawing.Size(45, 17);
		this.label63.TabIndex = 11;
		this.label63.Text = "0";
		this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
		this.btnRefresh.Checked = false;
		this.btnRefresh.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRefresh.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRefresh.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRefresh.ImageNew = LockSoftware.Properties.Resources.Button_Refresh;
		this.btnRefresh.ImageRedrawed = true;
		this.btnRefresh.ImageStyle = 0;
		this.btnRefresh.isButton = true;
		this.btnRefresh.Location = new System.Drawing.Point(185, 8);
		this.btnRefresh.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRefresh.MouseDownEndColor = System.Drawing.Color.White;
		this.btnRefresh.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRefresh.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRefresh.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRefresh.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(35, 52);
		this.btnRefresh.TabIndex = 1;
		this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRefresh.TextImageLocation = 0;
		this.btnRefresh.TextNew = "";
		this.btnRefresh.TextRedrawed = false;
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		this.label60.AutoSize = true;
		this.label60.BackColor = System.Drawing.Color.Transparent;
		this.label60.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label60.Location = new System.Drawing.Point(2, 23);
		this.label60.Name = "label60";
		this.label60.Size = new System.Drawing.Size(67, 15);
		this.label60.TabIndex = 8;
		this.label60.Text = "已用客房：";
		this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.clsBackPanel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.clsBackPanel4.Border = false;
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
		this.clsBackPanel4.Color1 = System.Drawing.Color.Black;
		this.clsBackPanel4.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel4.ColorAngle = 45f;
		this.clsBackPanel4.Location = new System.Drawing.Point(4, 65);
		this.clsBackPanel4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.clsBackPanel4.Name = "clsBackPanel4";
		this.clsBackPanel4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.clsBackPanel4.Size = new System.Drawing.Size(217, 1);
		this.clsBackPanel4.TabIndex = 21;
		this.label61.AutoSize = true;
		this.label61.BackColor = System.Drawing.Color.Transparent;
		this.label61.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label61.Location = new System.Drawing.Point(2, 44);
		this.label61.Name = "label61";
		this.label61.Size = new System.Drawing.Size(67, 15);
		this.label61.TabIndex = 9;
		this.label61.Text = "可用客房：";
		this.label61.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
		this.tableLayoutPanel2.ColumnCount = 3;
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50f));
		this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel5, 0, 19);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn4, 2, 4);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn9, 2, 12);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn11, 2, 16);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn12, 2, 18);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn6, 2, 10);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn10, 2, 6);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn7, 2, 14);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn8, 2, 8);
		this.tableLayoutPanel2.Controls.Add(this.toolsBtn13, 2, 19);
		this.tableLayoutPanel2.Controls.Add(this.label65, 0, 4);
		this.tableLayoutPanel2.Controls.Add(this.label66, 0, 6);
		this.tableLayoutPanel2.Controls.Add(this.label67, 0, 8);
		this.tableLayoutPanel2.Controls.Add(this.label68, 0, 10);
		this.tableLayoutPanel2.Controls.Add(this.label69, 0, 12);
		this.tableLayoutPanel2.Controls.Add(this.label70, 0, 14);
		this.tableLayoutPanel2.Controls.Add(this.label71, 0, 16);
		this.tableLayoutPanel2.Controls.Add(this.label72, 0, 18);
		this.tableLayoutPanel2.Controls.Add(this.label73, 1, 4);
		this.tableLayoutPanel2.Controls.Add(this.label74, 1, 6);
		this.tableLayoutPanel2.Controls.Add(this.label75, 1, 8);
		this.tableLayoutPanel2.Controls.Add(this.label76, 1, 10);
		this.tableLayoutPanel2.Controls.Add(this.label77, 1, 12);
		this.tableLayoutPanel2.Controls.Add(this.label78, 1, 14);
		this.tableLayoutPanel2.Controls.Add(this.label79, 1, 16);
		this.tableLayoutPanel2.Controls.Add(this.label80, 1, 18);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel6, 0, 5);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel7, 0, 7);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel8, 0, 9);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel9, 0, 11);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel10, 0, 13);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel11, 0, 15);
		this.tableLayoutPanel2.Controls.Add(this.clsBackPanel12, 0, 17);
		this.tableLayoutPanel2.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 67);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.tableLayoutPanel2.RowCount = 20;
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1f));
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16f));
		this.tableLayoutPanel2.Size = new System.Drawing.Size(236, 280);
		this.tableLayoutPanel2.TabIndex = 0;
		this.clsBackPanel5.Border = false;
		this.clsBackPanel5.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderBW = 1;
		this.clsBackPanel5.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel5.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel5.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel5.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel5.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderLW = 1;
		this.clsBackPanel5.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderRW = 1;
		this.clsBackPanel5.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderTW = 1;
		this.clsBackPanel5.Color1 = System.Drawing.Color.Black;
		this.clsBackPanel5.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel5.ColorAngle = 45f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel5, 2);
		this.clsBackPanel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel5.Location = new System.Drawing.Point(3, 256);
		this.clsBackPanel5.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.clsBackPanel5.Name = "clsBackPanel5";
		this.clsBackPanel5.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
		this.clsBackPanel5.Size = new System.Drawing.Size(120, 1);
		this.clsBackPanel5.TabIndex = 39;
		this.clsBackPanel5.Visible = false;
		this.toolsBtn4.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn4.Checked = false;
		this.toolsBtn4.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn4.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn4.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn4.ImageNew = LockSoftware.Properties.Resources._05_1_;
		this.toolsBtn4.ImageRedrawed = true;
		this.toolsBtn4.ImageStyle = 1;
		this.toolsBtn4.isButton = false;
		this.toolsBtn4.Location = new System.Drawing.Point(129, 14);
		this.toolsBtn4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn4.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn4.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn4.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn4.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn4.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn4.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn4.Name = "toolsBtn4";
		this.toolsBtn4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn4.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn4.TabIndex = 13;
		this.toolsBtn4.TextImageLocation = 0;
		this.toolsBtn4.TextNew = "";
		this.toolsBtn4.TextRedrawed = false;
		this.toolsBtn9.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn9.Checked = false;
		this.toolsBtn9.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn9.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn9.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn9.ImageNew = LockSoftware.Properties.Resources._54;
		this.toolsBtn9.ImageRedrawed = true;
		this.toolsBtn9.ImageStyle = 1;
		this.toolsBtn9.isButton = false;
		this.toolsBtn9.Location = new System.Drawing.Point(129, 134);
		this.toolsBtn9.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn9.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn9.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn9.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn9.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn9.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn9.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn9.Name = "toolsBtn9";
		this.toolsBtn9.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn9.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn9.TabIndex = 17;
		this.toolsBtn9.TextImageLocation = 0;
		this.toolsBtn9.TextNew = "";
		this.toolsBtn9.TextRedrawed = false;
		this.toolsBtn11.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn11.Checked = false;
		this.toolsBtn11.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn11.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn11.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn11.ImageNew = LockSoftware.Properties.Resources.Pic_07;
		this.toolsBtn11.ImageRedrawed = true;
		this.toolsBtn11.ImageStyle = 1;
		this.toolsBtn11.isButton = false;
		this.toolsBtn11.Location = new System.Drawing.Point(129, 194);
		this.toolsBtn11.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn11.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn11.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn11.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn11.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn11.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn11.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn11.Name = "toolsBtn11";
		this.toolsBtn11.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn11.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn11.TabIndex = 19;
		this.toolsBtn11.TextImageLocation = 0;
		this.toolsBtn11.TextNew = "";
		this.toolsBtn11.TextRedrawed = false;
		this.toolsBtn12.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn12.Checked = false;
		this.toolsBtn12.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn12.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn12.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn12.ImageNew = LockSoftware.Properties.Resources.tt;
		this.toolsBtn12.ImageRedrawed = false;
		this.toolsBtn12.ImageStyle = 1;
		this.toolsBtn12.isButton = false;
		this.toolsBtn12.Location = new System.Drawing.Point(129, 224);
		this.toolsBtn12.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn12.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn12.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn12.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn12.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn12.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn12.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn12.Name = "toolsBtn12";
		this.toolsBtn12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn12.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn12.TabIndex = 20;
		this.toolsBtn12.TextImageLocation = 0;
		this.toolsBtn12.TextNew = "";
		this.toolsBtn12.TextRedrawed = false;
		this.toolsBtn6.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn6.Checked = false;
		this.toolsBtn6.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn6.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn6.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn6.ImageNew = LockSoftware.Properties.Resources._120px_Vista_Login_Manager;
		this.toolsBtn6.ImageRedrawed = true;
		this.toolsBtn6.ImageStyle = 1;
		this.toolsBtn6.isButton = false;
		this.toolsBtn6.Location = new System.Drawing.Point(129, 104);
		this.toolsBtn6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn6.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn6.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn6.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn6.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn6.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn6.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn6.Name = "toolsBtn6";
		this.toolsBtn6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn6.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn6.TabIndex = 14;
		this.toolsBtn6.TextImageLocation = 0;
		this.toolsBtn6.TextNew = "";
		this.toolsBtn6.TextRedrawed = false;
		this.toolsBtn10.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn10.Checked = false;
		this.toolsBtn10.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn10.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn10.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn10.ImageIndex = 1;
		this.toolsBtn10.ImageNew = LockSoftware.Properties.Resources.trashcan_full1;
		this.toolsBtn10.ImageRedrawed = true;
		this.toolsBtn10.ImageStyle = 1;
		this.toolsBtn10.isButton = false;
		this.toolsBtn10.Location = new System.Drawing.Point(129, 44);
		this.toolsBtn10.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn10.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn10.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn10.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn10.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn10.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn10.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn10.Name = "toolsBtn10";
		this.toolsBtn10.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn10.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn10.TabIndex = 18;
		this.toolsBtn10.TextImageLocation = 0;
		this.toolsBtn10.TextNew = "";
		this.toolsBtn10.TextRedrawed = false;
		this.toolsBtn7.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn7.Checked = false;
		this.toolsBtn7.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn7.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn7.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn7.ImageNew = LockSoftware.Properties.Resources._35_1_;
		this.toolsBtn7.ImageRedrawed = true;
		this.toolsBtn7.ImageStyle = 1;
		this.toolsBtn7.isButton = false;
		this.toolsBtn7.Location = new System.Drawing.Point(129, 164);
		this.toolsBtn7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn7.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn7.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn7.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn7.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn7.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn7.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn7.Name = "toolsBtn7";
		this.toolsBtn7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn7.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn7.TabIndex = 15;
		this.toolsBtn7.TextImageLocation = 0;
		this.toolsBtn7.TextNew = "";
		this.toolsBtn7.TextRedrawed = false;
		this.toolsBtn8.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn8.Checked = false;
		this.toolsBtn8.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn8.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn8.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn8.ImageIndex = 2;
		this.toolsBtn8.ImageNew = LockSoftware.Properties.Resources.synchour;
		this.toolsBtn8.ImageRedrawed = true;
		this.toolsBtn8.ImageStyle = 1;
		this.toolsBtn8.isButton = false;
		this.toolsBtn8.Location = new System.Drawing.Point(129, 74);
		this.toolsBtn8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.toolsBtn8.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn8.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn8.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn8.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn8.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn8.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn8.Name = "toolsBtn8";
		this.toolsBtn8.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.toolsBtn8.Size = new System.Drawing.Size(24, 24);
		this.toolsBtn8.TabIndex = 16;
		this.toolsBtn8.TextImageLocation = 0;
		this.toolsBtn8.TextNew = "";
		this.toolsBtn8.TextRedrawed = false;
		this.toolsBtn13.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn13.Checked = false;
		this.toolsBtn13.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn13.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn13.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn13.ImageNew = LockSoftware.Properties.Resources.collapse;
		this.toolsBtn13.ImageRedrawed = true;
		this.toolsBtn13.ImageStyle = 0;
		this.toolsBtn13.isButton = true;
		this.toolsBtn13.Location = new System.Drawing.Point(129, 248);
		this.toolsBtn13.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn13.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn13.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn13.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn13.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn13.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn13.Name = "toolsBtn13";
		this.toolsBtn13.Size = new System.Drawing.Size(20, 16);
		this.toolsBtn13.TabIndex = 22;
		this.toolsBtn13.TextImageLocation = 0;
		this.toolsBtn13.TextNew = "";
		this.toolsBtn13.TextRedrawed = false;
		this.toolsBtn13.Visible = false;
		this.label65.AutoSize = true;
		this.label65.Dock = System.Windows.Forms.DockStyle.Left;
		this.label65.ForeColor = System.Drawing.Color.Teal;
		this.label65.Location = new System.Drawing.Point(3, 17);
		this.label65.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label65.Name = "label65";
		this.label65.Size = new System.Drawing.Size(41, 21);
		this.label65.TabIndex = 40;
		this.label65.Text = "label65";
		this.label66.AutoSize = true;
		this.label66.Dock = System.Windows.Forms.DockStyle.Left;
		this.label66.ForeColor = System.Drawing.Color.Teal;
		this.label66.Location = new System.Drawing.Point(3, 47);
		this.label66.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label66.Name = "label66";
		this.label66.Size = new System.Drawing.Size(41, 21);
		this.label66.TabIndex = 41;
		this.label66.Text = "label66";
		this.label67.AutoSize = true;
		this.label67.Dock = System.Windows.Forms.DockStyle.Left;
		this.label67.ForeColor = System.Drawing.Color.Teal;
		this.label67.Location = new System.Drawing.Point(3, 77);
		this.label67.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label67.Name = "label67";
		this.label67.Size = new System.Drawing.Size(41, 21);
		this.label67.TabIndex = 42;
		this.label67.Text = "label67";
		this.label68.AutoSize = true;
		this.label68.Dock = System.Windows.Forms.DockStyle.Left;
		this.label68.ForeColor = System.Drawing.Color.Teal;
		this.label68.Location = new System.Drawing.Point(3, 107);
		this.label68.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label68.Name = "label68";
		this.label68.Size = new System.Drawing.Size(41, 21);
		this.label68.TabIndex = 43;
		this.label68.Text = "label68";
		this.label69.AutoSize = true;
		this.label69.Dock = System.Windows.Forms.DockStyle.Left;
		this.label69.ForeColor = System.Drawing.Color.Teal;
		this.label69.Location = new System.Drawing.Point(3, 137);
		this.label69.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label69.Name = "label69";
		this.label69.Size = new System.Drawing.Size(41, 21);
		this.label69.TabIndex = 44;
		this.label69.Text = "label69";
		this.label70.AutoSize = true;
		this.label70.Dock = System.Windows.Forms.DockStyle.Left;
		this.label70.ForeColor = System.Drawing.Color.Teal;
		this.label70.Location = new System.Drawing.Point(3, 167);
		this.label70.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label70.Name = "label70";
		this.label70.Size = new System.Drawing.Size(41, 21);
		this.label70.TabIndex = 45;
		this.label70.Text = "label70";
		this.label71.AutoSize = true;
		this.label71.Dock = System.Windows.Forms.DockStyle.Left;
		this.label71.ForeColor = System.Drawing.Color.Teal;
		this.label71.Location = new System.Drawing.Point(3, 197);
		this.label71.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label71.Name = "label71";
		this.label71.Size = new System.Drawing.Size(41, 21);
		this.label71.TabIndex = 46;
		this.label71.Text = "label71";
		this.label72.AutoSize = true;
		this.label72.Dock = System.Windows.Forms.DockStyle.Left;
		this.label72.ForeColor = System.Drawing.Color.Teal;
		this.label72.Location = new System.Drawing.Point(3, 227);
		this.label72.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label72.Name = "label72";
		this.label72.Size = new System.Drawing.Size(41, 21);
		this.label72.TabIndex = 47;
		this.label72.Text = "label72";
		this.label73.Dock = System.Windows.Forms.DockStyle.Left;
		this.label73.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label73.ForeColor = System.Drawing.Color.Green;
		this.label73.Location = new System.Drawing.Point(79, 17);
		this.label73.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label73.Name = "label73";
		this.label73.Size = new System.Drawing.Size(44, 21);
		this.label73.TabIndex = 48;
		this.label73.Text = "label73";
		this.label74.Dock = System.Windows.Forms.DockStyle.Left;
		this.label74.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label74.ForeColor = System.Drawing.Color.Green;
		this.label74.Location = new System.Drawing.Point(79, 47);
		this.label74.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label74.Name = "label74";
		this.label74.Size = new System.Drawing.Size(44, 21);
		this.label74.TabIndex = 49;
		this.label74.Text = "label74";
		this.label75.Dock = System.Windows.Forms.DockStyle.Left;
		this.label75.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label75.ForeColor = System.Drawing.Color.Green;
		this.label75.Location = new System.Drawing.Point(79, 77);
		this.label75.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label75.Name = "label75";
		this.label75.Size = new System.Drawing.Size(44, 21);
		this.label75.TabIndex = 50;
		this.label75.Text = "label75";
		this.label76.Dock = System.Windows.Forms.DockStyle.Left;
		this.label76.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label76.ForeColor = System.Drawing.Color.Green;
		this.label76.Location = new System.Drawing.Point(79, 107);
		this.label76.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(44, 21);
		this.label76.TabIndex = 51;
		this.label76.Text = "label76";
		this.label77.Dock = System.Windows.Forms.DockStyle.Left;
		this.label77.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label77.ForeColor = System.Drawing.Color.Green;
		this.label77.Location = new System.Drawing.Point(79, 137);
		this.label77.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(44, 21);
		this.label77.TabIndex = 52;
		this.label77.Text = "label77";
		this.label78.Dock = System.Windows.Forms.DockStyle.Left;
		this.label78.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label78.ForeColor = System.Drawing.Color.Green;
		this.label78.Location = new System.Drawing.Point(79, 167);
		this.label78.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(44, 21);
		this.label78.TabIndex = 53;
		this.label78.Text = "label78";
		this.label79.Dock = System.Windows.Forms.DockStyle.Left;
		this.label79.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label79.ForeColor = System.Drawing.Color.Green;
		this.label79.Location = new System.Drawing.Point(79, 197);
		this.label79.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(44, 21);
		this.label79.TabIndex = 54;
		this.label79.Text = "label79";
		this.label80.Dock = System.Windows.Forms.DockStyle.Left;
		this.label80.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label80.ForeColor = System.Drawing.Color.Green;
		this.label80.Location = new System.Drawing.Point(79, 227);
		this.label80.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label80.Name = "label80";
		this.label80.Size = new System.Drawing.Size(44, 21);
		this.label80.TabIndex = 55;
		this.label80.Text = "label80";
		this.clsBackPanel6.Border = true;
		this.clsBackPanel6.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel6.BorderBW = 1;
		this.clsBackPanel6.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel6.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel6.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderLW = 0;
		this.clsBackPanel6.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderRW = 0;
		this.clsBackPanel6.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel6.BorderTW = 0;
		this.clsBackPanel6.Color1 = System.Drawing.Color.White;
		this.clsBackPanel6.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel6.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel6, 3);
		this.clsBackPanel6.Location = new System.Drawing.Point(3, 41);
		this.clsBackPanel6.Name = "clsBackPanel6";
		this.clsBackPanel6.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel6.TabIndex = 56;
		this.clsBackPanel7.Border = true;
		this.clsBackPanel7.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel7.BorderBW = 1;
		this.clsBackPanel7.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel7.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel7.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel7.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel7.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel7.BorderLW = 0;
		this.clsBackPanel7.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel7.BorderRW = 0;
		this.clsBackPanel7.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel7.BorderTW = 0;
		this.clsBackPanel7.Color1 = System.Drawing.Color.White;
		this.clsBackPanel7.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel7.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel7, 3);
		this.clsBackPanel7.Location = new System.Drawing.Point(3, 71);
		this.clsBackPanel7.Name = "clsBackPanel7";
		this.clsBackPanel7.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel7.TabIndex = 57;
		this.clsBackPanel8.Border = true;
		this.clsBackPanel8.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel8.BorderBW = 1;
		this.clsBackPanel8.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel8.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel8.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel8.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel8.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel8.BorderLW = 0;
		this.clsBackPanel8.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel8.BorderRW = 0;
		this.clsBackPanel8.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel8.BorderTW = 0;
		this.clsBackPanel8.Color1 = System.Drawing.Color.White;
		this.clsBackPanel8.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel8.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel8, 3);
		this.clsBackPanel8.Location = new System.Drawing.Point(3, 101);
		this.clsBackPanel8.Name = "clsBackPanel8";
		this.clsBackPanel8.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel8.TabIndex = 57;
		this.clsBackPanel9.Border = true;
		this.clsBackPanel9.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel9.BorderBW = 1;
		this.clsBackPanel9.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel9.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel9.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel9.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel9.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel9.BorderLW = 0;
		this.clsBackPanel9.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel9.BorderRW = 0;
		this.clsBackPanel9.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel9.BorderTW = 0;
		this.clsBackPanel9.Color1 = System.Drawing.Color.White;
		this.clsBackPanel9.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel9.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel9, 3);
		this.clsBackPanel9.Location = new System.Drawing.Point(3, 131);
		this.clsBackPanel9.Name = "clsBackPanel9";
		this.clsBackPanel9.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel9.TabIndex = 57;
		this.clsBackPanel10.Border = true;
		this.clsBackPanel10.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel10.BorderBW = 1;
		this.clsBackPanel10.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel10.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel10.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel10.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel10.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel10.BorderLW = 0;
		this.clsBackPanel10.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel10.BorderRW = 0;
		this.clsBackPanel10.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel10.BorderTW = 0;
		this.clsBackPanel10.Color1 = System.Drawing.Color.White;
		this.clsBackPanel10.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel10.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel10, 3);
		this.clsBackPanel10.Location = new System.Drawing.Point(3, 161);
		this.clsBackPanel10.Name = "clsBackPanel10";
		this.clsBackPanel10.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel10.TabIndex = 58;
		this.clsBackPanel11.Border = true;
		this.clsBackPanel11.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel11.BorderBW = 1;
		this.clsBackPanel11.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel11.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel11.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel11.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel11.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel11.BorderLW = 0;
		this.clsBackPanel11.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel11.BorderRW = 0;
		this.clsBackPanel11.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel11.BorderTW = 0;
		this.clsBackPanel11.Color1 = System.Drawing.Color.White;
		this.clsBackPanel11.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel11.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel11, 3);
		this.clsBackPanel11.Location = new System.Drawing.Point(3, 191);
		this.clsBackPanel11.Name = "clsBackPanel11";
		this.clsBackPanel11.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel11.TabIndex = 59;
		this.clsBackPanel12.Border = true;
		this.clsBackPanel12.BorderBT = System.Windows.Forms.ButtonBorderStyle.Dashed;
		this.clsBackPanel12.BorderBW = 1;
		this.clsBackPanel12.BorderColorBottom = System.Drawing.Color.FromArgb(0, 64, 64);
		this.clsBackPanel12.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel12.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel12.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel12.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel12.BorderLW = 0;
		this.clsBackPanel12.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel12.BorderRW = 0;
		this.clsBackPanel12.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel12.BorderTW = 0;
		this.clsBackPanel12.Color1 = System.Drawing.Color.White;
		this.clsBackPanel12.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel12.ColorAngle = 90f;
		this.tableLayoutPanel2.SetColumnSpan(this.clsBackPanel12, 3);
		this.clsBackPanel12.Location = new System.Drawing.Point(3, 221);
		this.clsBackPanel12.Name = "clsBackPanel12";
		this.clsBackPanel12.Size = new System.Drawing.Size(160, 1);
		this.clsBackPanel12.TabIndex = 57;
		this.clsBackPanel3.Border = true;
		this.clsBackPanel3.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderBW = 0;
		this.clsBackPanel3.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderLW = 1;
		this.clsBackPanel3.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderRW = 1;
		this.clsBackPanel3.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderTW = 1;
		this.clsBackPanel3.Color1 = System.Drawing.Color.White;
		this.clsBackPanel3.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel3.ColorAngle = 90f;
		this.clsBackPanel3.Controls.Add(this.btnSear);
		this.clsBackPanel3.Controls.Add(this.txtBM);
		this.clsBackPanel3.Controls.Add(this.toolsBtn5);
		this.clsBackPanel3.Controls.Add(this.txtSRn);
		this.clsBackPanel3.Controls.Add(this.cobStatus);
		this.clsBackPanel3.Controls.Add(this.cobType);
		this.clsBackPanel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel3.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel3.Name = "clsBackPanel3";
		this.clsBackPanel3.Size = new System.Drawing.Size(485, 51);
		this.clsBackPanel3.TabIndex = 0;
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
		this.btnSear.Location = new System.Drawing.Point(476, 7);
		this.btnSear.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnSear.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnSear.MouseDownStartColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnSear.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnSear.Name = "btnSear";
		this.btnSear.Size = new System.Drawing.Size(44, 37);
		this.btnSear.TabIndex = 7;
		this.btnSear.TextImageLocation = 0;
		this.btnSear.TextNew = "";
		this.btnSear.TextRedrawed = false;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.txtBM.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBM.ForeColor = System.Drawing.Color.DarkGray;
		this.txtBM.Location = new System.Drawing.Point(370, 16);
		this.txtBM.Name = "txtBM";
		this.txtBM.Size = new System.Drawing.Size(100, 21);
		this.txtBM.TabIndex = 6;
		this.txtBM.Visible = false;
		this.txtBM.Enter += new System.EventHandler(txtBM_Enter);
		this.txtBM.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBM_KeyDown);
		this.txtBM.Leave += new System.EventHandler(txtBM_Leave);
		this.toolsBtn5.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn5.Checked = false;
		this.toolsBtn5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.toolsBtn5.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn5.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn5.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn5.ImageNew = LockSoftware.Properties.Resources.TRoomCenter;
		this.toolsBtn5.ImageRedrawed = true;
		this.toolsBtn5.ImageStyle = 1;
		this.toolsBtn5.isButton = true;
		this.toolsBtn5.Location = new System.Drawing.Point(6, 6);
		this.toolsBtn5.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn5.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn5.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn5.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn5.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn5.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn5.Name = "toolsBtn5";
		this.toolsBtn5.Size = new System.Drawing.Size(40, 40);
		this.toolsBtn5.TabIndex = 5;
		this.toolsBtn5.TextImageLocation = 0;
		this.toolsBtn5.TextNew = "";
		this.toolsBtn5.TextRedrawed = false;
		this.toolsBtn5.Click += new System.EventHandler(toolsBtn5_Click);
		this.toolsBtn5.MouseLeave += new System.EventHandler(toolsBtn5_MouseLeave);
		this.toolsBtn5.MouseMove += new System.Windows.Forms.MouseEventHandler(toolsBtn5_MouseMove);
		this.txtSRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSRn.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSRn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtSRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtSRn.Location = new System.Drawing.Point(274, 16);
		this.txtSRn.Name = "txtSRn";
		this.txtSRn.Size = new System.Drawing.Size(92, 21);
		this.txtSRn.TabIndex = 4;
		this.txtSRn.Text = "ROOM NAME...";
		this.txtSRn.Enter += new System.EventHandler(txtSRn_Enter);
		this.txtSRn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSRn_KeyDown);
		this.txtSRn.Leave += new System.EventHandler(txtSRn_Leave);
		this.cobStatus.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobStatus.DropDownHeight = 130;
		this.cobStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobStatus.DropDownWidth = 180;
		this.cobStatus.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobStatus.FormattingEnabled = true;
		this.cobStatus.IntegralHeight = false;
		this.cobStatus.ItemHeight = 15;
		this.cobStatus.Location = new System.Drawing.Point(171, 16);
		this.cobStatus.MaxDropDownItems = 9;
		this.cobStatus.Name = "cobStatus";
		this.cobStatus.Size = new System.Drawing.Size(97, 23);
		this.cobStatus.TabIndex = 1;
		this.cobStatus.SelectedIndexChanged += new System.EventHandler(cobStatus_SelectedIndexChanged);
		this.cobType.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 180;
		this.cobType.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobType.FormattingEnabled = true;
		this.cobType.ItemHeight = 15;
		this.cobType.Location = new System.Drawing.Point(54, 16);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(111, 23);
		this.cobType.TabIndex = 0;
		this.toolsBtn3.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn3.Checked = false;
		this.toolsBtn3.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn3.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn3.Dock = System.Windows.Forms.DockStyle.Right;
		this.toolsBtn3.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn3.ImageNew = LockSoftware.Properties.Resources.mini_right;
		this.toolsBtn3.ImageRedrawed = true;
		this.toolsBtn3.ImageStyle = 0;
		this.toolsBtn3.isButton = true;
		this.toolsBtn3.Location = new System.Drawing.Point(485, 0);
		this.toolsBtn3.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn3.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn3.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn3.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn3.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn3.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn3.Name = "toolsBtn3";
		this.toolsBtn3.Size = new System.Drawing.Size(10, 495);
		this.toolsBtn3.TabIndex = 2;
		this.toolsBtn3.TextImageLocation = 0;
		this.toolsBtn3.TextNew = "";
		this.toolsBtn3.TextRedrawed = false;
		this.toolsBtn3.Click += new System.EventHandler(toolsBtn3_Click);
		this.btnRInfo.BackColor = System.Drawing.Color.Transparent;
		this.btnRInfo.BaseColor = System.Drawing.Color.White;
		this.btnRInfo.ButtonColor = System.Drawing.Color.FromArgb(224, 85, 50);
		this.btnRInfo.ButtonText = "Guest Check In";
		this.btnRInfo.CornerRadius = 2;
		this.btnRInfo.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnRInfo.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRInfo.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.btnRInfo.GlowColor = System.Drawing.Color.FromArgb(224, 85, 50);
		this.btnRInfo.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRInfo.Image = LockSoftware.Properties.Resources._05_1_;
		this.btnRInfo.Location = new System.Drawing.Point(0, 0);
		this.btnRInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.btnRInfo.Name = "btnRInfo";
		this.btnRInfo.Size = new System.Drawing.Size(250, 32);
		this.btnRInfo.TabIndex = 0;
		this.btnRInfo.Click += new System.EventHandler(btnRInfo_Click);
		this.btnRGLevel.BackColor = System.Drawing.Color.Transparent;
		this.btnRGLevel.BaseColor = System.Drawing.Color.White;
		this.btnRGLevel.ButtonColor = System.Drawing.Color.Orange;
		this.btnRGLevel.ButtonText = "Guest Check Out";
		this.btnRGLevel.CornerRadius = 2;
		this.btnRGLevel.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnRGLevel.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnRGLevel.GlowColor = System.Drawing.Color.Orange;
		this.btnRGLevel.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRGLevel.Image = LockSoftware.Properties.Resources.level;
		this.btnRGLevel.Location = new System.Drawing.Point(0, 32);
		this.btnRGLevel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.btnRGLevel.Name = "btnRGLevel";
		this.btnRGLevel.Size = new System.Drawing.Size(250, 32);
		this.btnRGLevel.TabIndex = 6;
		this.btnRGLevel.Click += new System.EventHandler(btnRGLevel_Click);
		this.btnTGIn.BackColor = System.Drawing.Color.Transparent;
		this.btnTGIn.BaseColor = System.Drawing.Color.White;
		this.btnTGIn.ButtonColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.btnTGIn.ButtonText = "团 队 办 理";
		this.btnTGIn.CornerRadius = 2;
		this.btnTGIn.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnTGIn.ForeColor = System.Drawing.Color.Green;
		this.btnTGIn.GlowColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.btnTGIn.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTGIn.Image = LockSoftware.Properties.Resources._35_1_;
		this.btnTGIn.Location = new System.Drawing.Point(0, 64);
		this.btnTGIn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.btnTGIn.Name = "btnTGIn";
		this.btnTGIn.Size = new System.Drawing.Size(250, 32);
		this.btnTGIn.TabIndex = 11;
		this.btnTGIn.Click += new System.EventHandler(btnTGIn_Click);
		this.btnTGL.BackColor = System.Drawing.Color.LightGray;
		this.btnTGL.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.btnTGL.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnTGL.ForeColor = System.Drawing.Color.Black;
		this.btnTGL.GlowColor = System.Drawing.Color.White;
		this.btnTGL.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTGL.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnTGL.Location = new System.Drawing.Point(1, 406);
		this.btnTGL.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
		this.btnTGL.Name = "btnTGL";
		this.btnTGL.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnTGL.Size = new System.Drawing.Size(241, 32);
		this.btnTGL.TabIndex = 37;
		this.btnTGL.Text = "团 队 退 房";
		this.btnTGL.Click += new System.EventHandler(btnTGL_Click);
		this.btnTGSO.BackColor = System.Drawing.Color.LightGray;
		this.btnTGSO.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.btnTGSO.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnTGSO.ForeColor = System.Drawing.Color.Black;
		this.btnTGSO.GlowColor = System.Drawing.Color.White;
		this.btnTGSO.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTGSO.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnTGSO.Location = new System.Drawing.Point(1, 438);
		this.btnTGSO.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
		this.btnTGSO.Name = "btnTGSO";
		this.btnTGSO.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnTGSO.Size = new System.Drawing.Size(241, 32);
		this.btnTGSO.TabIndex = 49;
		this.btnTGSO.Text = "Tour Group Extension";
		this.btnTGSO.Click += new System.EventHandler(btnTGSO_Click);
		this.btnChk.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnChk.BackColor = System.Drawing.Color.Transparent;
		this.btnChk.Checked = false;
		this.btnChk.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnChk.DefaultColor = System.Drawing.Color.Transparent;
		this.btnChk.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnChk.ImageNew = LockSoftware.Properties.Resources.ok;
		this.btnChk.ImageRedrawed = true;
		this.btnChk.ImageStyle = 0;
		this.btnChk.isButton = true;
		this.btnChk.Location = new System.Drawing.Point(208, 55);
		this.btnChk.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnChk.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnChk.MouseDownStartColor = System.Drawing.Color.White;
		this.btnChk.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.btnChk.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.btnChk.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnChk.Name = "btnChk";
		this.btnChk.Size = new System.Drawing.Size(28, 28);
		this.btnChk.TabIndex = 48;
		this.btnChk.TextImageLocation = 0;
		this.btnChk.TextNew = "";
		this.btnChk.TextRedrawed = false;
		this.btnChk.Click += new System.EventHandler(btnChk_Click);
		this.btnTS.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnTS.BackColor = System.Drawing.Color.Transparent;
		this.btnTS.BaseColor = System.Drawing.Color.White;
		this.btnTS.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnTS.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnTS.ButtonText = null;
		this.btnTS.CornerRadius = 2;
		this.btnTS.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTS.Image = LockSoftware.Properties.Resources.search;
		this.btnTS.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnTS.Location = new System.Drawing.Point(212, 3);
		this.btnTS.Name = "btnTS";
		this.btnTS.Size = new System.Drawing.Size(24, 24);
		this.btnTS.TabIndex = 41;
		this.btnTS.Click += new System.EventHandler(btnTS_Click);
		this.btnTGO.BackColor = System.Drawing.Color.Transparent;
		this.btnTGO.BaseColor = System.Drawing.Color.White;
		this.btnTGO.ButtonColor = System.Drawing.Color.CornflowerBlue;
		this.btnTGO.ButtonText = "Tour Group Check In";
		this.btnTGO.CornerRadius = 2;
		this.btnTGO.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnTGO.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnTGO.ForeColor = System.Drawing.Color.SteelBlue;
		this.btnTGO.GlowColor = System.Drawing.Color.CornflowerBlue;
		this.btnTGO.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTGO.Image = LockSoftware.Properties.Resources.GuestIn;
		this.btnTGO.Location = new System.Drawing.Point(1, 1);
		this.btnTGO.Margin = new System.Windows.Forms.Padding(0);
		this.btnTGO.Name = "btnTGO";
		this.btnTGO.Size = new System.Drawing.Size(241, 32);
		this.btnTGO.TabIndex = 7;
		this.btnTGO.Click += new System.EventHandler(btnTGO_Click);
		this.btnLN.AutoSize = true;
		this.btnLN.BackColor = System.Drawing.Color.LightGray;
		this.btnLN.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLN.ForeColor = System.Drawing.Color.Black;
		this.btnLN.GlowColor = System.Drawing.Color.White;
		this.btnLN.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnLN.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnLN.Location = new System.Drawing.Point(5, 447);
		this.btnLN.Name = "btnLN";
		this.btnLN.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnLN.Size = new System.Drawing.Size(225, 36);
		this.btnLN.TabIndex = 35;
		this.btnLN.Text = "无 卡 退 房";
		this.btnLN.Click += new System.EventHandler(btnLN_Click);
		this.btnLC.AutoSize = true;
		this.btnLC.BackColor = System.Drawing.Color.LightGray;
		this.btnLC.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLC.ForeColor = System.Drawing.Color.Black;
		this.btnLC.GlowColor = System.Drawing.Color.White;
		this.btnLC.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnLC.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnLC.Location = new System.Drawing.Point(5, 411);
		this.btnLC.Name = "btnLC";
		this.btnLC.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnLC.Size = new System.Drawing.Size(225, 36);
		this.btnLC.TabIndex = 34;
		this.btnLC.Text = "有 卡 退 房";
		this.btnLC.Click += new System.EventHandler(btnLC_Click);
		this.btnRC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRC.AutoSize = true;
		this.btnRC.BackColor = System.Drawing.Color.Gainsboro;
		this.btnRC.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRC.ForeColor = System.Drawing.Color.Black;
		this.btnRC.GlowColor = System.Drawing.Color.White;
		this.btnRC.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRC.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnRC.Location = new System.Drawing.Point(90, 0);
		this.btnRC.Margin = new System.Windows.Forms.Padding(0);
		this.btnRC.Name = "btnRC";
		this.btnRC.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnRC.Size = new System.Drawing.Size(67, 28);
		this.btnRC.TabIndex = 33;
		this.btnRC.Text = "读卡";
		this.btnRC.Click += new System.EventHandler(btnRC_Click);
		this.btnIDCard.BackColor = System.Drawing.Color.Transparent;
		this.btnIDCard.BaseColor = System.Drawing.Color.White;
		this.btnIDCard.ButtonColor = System.Drawing.Color.Silver;
		this.btnIDCard.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnIDCard.ButtonText = null;
		this.btnIDCard.CornerRadius = 2;
		this.btnIDCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIDCard.Image = LockSoftware.Properties.Resources.V_Cer;
		this.btnIDCard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnIDCard.Location = new System.Drawing.Point(197, 58);
		this.btnIDCard.Name = "btnIDCard";
		this.btnIDCard.Size = new System.Drawing.Size(28, 22);
		this.btnIDCard.TabIndex = 41;
		this.btnIDCard.Click += new System.EventHandler(btnIDCard_Click);
		this.btnIDCard.MouseLeave += new System.EventHandler(btnIDCard_MouseLeave);
		this.btnIDCard.MouseMove += new System.Windows.Forms.MouseEventHandler(btnIDCard_MouseMove);
		this.btnGCSO.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnGCSO.AutoSize = true;
		this.btnGCSO.BackColor = System.Drawing.Color.LightGray;
		this.btnGCSO.Enabled = false;
		this.btnGCSO.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGCSO.ForeColor = System.Drawing.Color.Black;
		this.btnGCSO.GlowColor = System.Drawing.Color.White;
		this.btnGCSO.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnGCSO.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnGCSO.Location = new System.Drawing.Point(5, 409);
		this.btnGCSO.Name = "btnGCSO";
		this.btnGCSO.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnGCSO.Size = new System.Drawing.Size(225, 36);
		this.btnGCSO.TabIndex = 40;
		this.btnGCSO.Text = "Guest Stay Over";
		this.btnGCSO.Visible = false;
		this.btnGCSO.EnabledChanged += new System.EventHandler(btnCard_EnabledChanged);
		this.btnGCSO.Click += new System.EventHandler(btnCard_Click);
		this.btnCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnCard.AutoSize = true;
		this.btnCard.BackColor = System.Drawing.Color.LightGray;
		this.btnCard.Enabled = false;
		this.btnCard.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCard.ForeColor = System.Drawing.Color.Black;
		this.btnCard.GlowColor = System.Drawing.Color.White;
		this.btnCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCard.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCard.Location = new System.Drawing.Point(5, 373);
		this.btnCard.Name = "btnCard";
		this.btnCard.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnCard.Size = new System.Drawing.Size(225, 36);
		this.btnCard.TabIndex = 32;
		this.btnCard.Text = "Make Card";
		this.btnCard.EnabledChanged += new System.EventHandler(btnCard_EnabledChanged);
		this.btnCard.Click += new System.EventHandler(btnCard_Click);
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = false;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.ImageNew = LockSoftware.Properties.Resources.mini_bottom;
		this.toolsBtn1.ImageRedrawed = true;
		this.toolsBtn1.ImageStyle = 0;
		this.toolsBtn1.isButton = true;
		this.toolsBtn1.Location = new System.Drawing.Point(0, 533);
		this.toolsBtn1.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn1.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn1.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn1.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn1.Name = "toolsBtn1";
		this.toolsBtn1.Size = new System.Drawing.Size(973, 8);
		this.toolsBtn1.TabIndex = 3;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "";
		this.toolsBtn1.TextRedrawed = false;
		this.toolsBtn1.Click += new System.EventHandler(toolsBtn1_Click);
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.tableLayoutPanel1);
		this.clsBackPanel1.Controls.Add(this.pictureBox1);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 541);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(973, 47);
		this.clsBackPanel1.TabIndex = 2;
		this.tableLayoutPanel1.ColumnCount = 12;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
		this.tableLayoutPanel1.Controls.Add(this.txtRMemo, 11, 1);
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.label14, 11, 0);
		this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.label6, 4, 0);
		this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.label21, 5, 0);
		this.tableLayoutPanel1.Controls.Add(this.label22, 3, 1);
		this.tableLayoutPanel1.Controls.Add(this.label23, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.label24, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.label13, 10, 0);
		this.tableLayoutPanel1.Controls.Add(this.label25, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label10, 10, 1);
		this.tableLayoutPanel1.Controls.Add(this.label9, 4, 1);
		this.tableLayoutPanel1.Controls.Add(this.label18, 5, 1);
		this.tableLayoutPanel1.Controls.Add(this.label8, 8, 0);
		this.tableLayoutPanel1.Controls.Add(this.label11, 6, 0);
		this.tableLayoutPanel1.Controls.Add(this.label7, 8, 1);
		this.tableLayoutPanel1.Controls.Add(this.label12, 6, 1);
		this.tableLayoutPanel1.Controls.Add(this.label19, 9, 0);
		this.tableLayoutPanel1.Controls.Add(this.label16, 7, 0);
		this.tableLayoutPanel1.Controls.Add(this.label20, 9, 1);
		this.tableLayoutPanel1.Controls.Add(this.label15, 7, 1);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(48, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2, 5, 2, 2);
		this.tableLayoutPanel1.RowCount = 2;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(925, 47);
		this.tableLayoutPanel1.TabIndex = 6;
		this.txtRMemo.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtRMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtRMemo.Dock = System.Windows.Forms.DockStyle.Left;
		this.txtRMemo.ForeColor = System.Drawing.Color.Teal;
		this.txtRMemo.Location = new System.Drawing.Point(813, 28);
		this.txtRMemo.Multiline = true;
		this.txtRMemo.Name = "txtRMemo";
		this.txtRMemo.ReadOnly = true;
		this.txtRMemo.Size = new System.Drawing.Size(65, 14);
		this.txtRMemo.TabIndex = 24;
		this.txtRMemo.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(5, 5);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label2.Size = new System.Drawing.Size(35, 18);
		this.label2.TabIndex = 0;
		this.label2.Text = "label2";
		this.label14.AutoSize = true;
		this.label14.Dock = System.Windows.Forms.DockStyle.Left;
		this.label14.ForeColor = System.Drawing.Color.Teal;
		this.label14.Location = new System.Drawing.Point(813, 5);
		this.label14.Name = "label14";
		this.label14.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label14.Size = new System.Drawing.Size(41, 20);
		this.label14.TabIndex = 23;
		this.label14.Text = "label14";
		this.label14.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(156, 5);
		this.label4.Name = "label4";
		this.label4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label4.Size = new System.Drawing.Size(35, 18);
		this.label4.TabIndex = 2;
		this.label4.Text = "label4";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(307, 5);
		this.label6.Name = "label6";
		this.label6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label6.Size = new System.Drawing.Size(35, 18);
		this.label6.TabIndex = 4;
		this.label6.Text = "label6";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(5, 25);
		this.label3.Name = "label3";
		this.label3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label3.Size = new System.Drawing.Size(35, 17);
		this.label3.TabIndex = 1;
		this.label3.Text = "label3";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(156, 25);
		this.label5.Name = "label5";
		this.label5.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label5.Size = new System.Drawing.Size(35, 17);
		this.label5.TabIndex = 3;
		this.label5.Text = "label5";
		this.label21.AutoSize = true;
		this.label21.Dock = System.Windows.Forms.DockStyle.Left;
		this.label21.ForeColor = System.Drawing.Color.Teal;
		this.label21.Location = new System.Drawing.Point(348, 5);
		this.label21.Name = "label21";
		this.label21.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label21.Size = new System.Drawing.Size(41, 20);
		this.label21.TabIndex = 16;
		this.label21.Text = "label21";
		this.label22.AutoSize = true;
		this.label22.Dock = System.Windows.Forms.DockStyle.Left;
		this.label22.ForeColor = System.Drawing.Color.Teal;
		this.label22.Location = new System.Drawing.Point(197, 25);
		this.label22.Name = "label22";
		this.label22.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label22.Size = new System.Drawing.Size(41, 20);
		this.label22.TabIndex = 15;
		this.label22.Text = "label22";
		this.label23.AutoSize = true;
		this.label23.Dock = System.Windows.Forms.DockStyle.Left;
		this.label23.ForeColor = System.Drawing.Color.Teal;
		this.label23.Location = new System.Drawing.Point(197, 5);
		this.label23.Name = "label23";
		this.label23.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label23.Size = new System.Drawing.Size(41, 20);
		this.label23.TabIndex = 14;
		this.label23.Text = "label23";
		this.label24.AutoSize = true;
		this.label24.Dock = System.Windows.Forms.DockStyle.Left;
		this.label24.ForeColor = System.Drawing.Color.Teal;
		this.label24.Location = new System.Drawing.Point(46, 25);
		this.label24.Name = "label24";
		this.label24.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label24.Size = new System.Drawing.Size(41, 20);
		this.label24.TabIndex = 13;
		this.label24.Text = "label24";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(766, 5);
		this.label13.Name = "label13";
		this.label13.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label13.Size = new System.Drawing.Size(41, 18);
		this.label13.TabIndex = 11;
		this.label13.Text = "label13";
		this.label13.Visible = false;
		this.label25.AutoSize = true;
		this.label25.Dock = System.Windows.Forms.DockStyle.Left;
		this.label25.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.ForeColor = System.Drawing.Color.Teal;
		this.label25.Location = new System.Drawing.Point(46, 5);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(51, 20);
		this.label25.TabIndex = 12;
		this.label25.Text = "label25";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(766, 25);
		this.label10.Name = "label10";
		this.label10.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label10.Size = new System.Drawing.Size(41, 17);
		this.label10.TabIndex = 8;
		this.label10.Text = "label10";
		this.label10.Visible = false;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(307, 25);
		this.label9.Name = "label9";
		this.label9.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label9.Size = new System.Drawing.Size(35, 17);
		this.label9.TabIndex = 7;
		this.label9.Text = "label9";
		this.label18.AutoSize = true;
		this.label18.Dock = System.Windows.Forms.DockStyle.Left;
		this.label18.ForeColor = System.Drawing.Color.Teal;
		this.label18.Location = new System.Drawing.Point(348, 25);
		this.label18.Name = "label18";
		this.label18.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label18.Size = new System.Drawing.Size(41, 20);
		this.label18.TabIndex = 19;
		this.label18.Text = "label18";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(615, 5);
		this.label8.Name = "label8";
		this.label8.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label8.Size = new System.Drawing.Size(35, 18);
		this.label8.TabIndex = 6;
		this.label8.Text = "label8";
		this.label8.Visible = false;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(458, 5);
		this.label11.Name = "label11";
		this.label11.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label11.Size = new System.Drawing.Size(41, 18);
		this.label11.TabIndex = 9;
		this.label11.Text = "label11";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(615, 25);
		this.label7.Name = "label7";
		this.label7.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label7.Size = new System.Drawing.Size(35, 17);
		this.label7.TabIndex = 5;
		this.label7.Text = "label7";
		this.label7.Visible = false;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(458, 25);
		this.label12.Name = "label12";
		this.label12.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label12.Size = new System.Drawing.Size(41, 17);
		this.label12.TabIndex = 10;
		this.label12.Text = "label12";
		this.label19.AutoSize = true;
		this.label19.Dock = System.Windows.Forms.DockStyle.Left;
		this.label19.ForeColor = System.Drawing.Color.Teal;
		this.label19.Location = new System.Drawing.Point(656, 5);
		this.label19.Name = "label19";
		this.label19.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.label19.Size = new System.Drawing.Size(41, 20);
		this.label19.TabIndex = 18;
		this.label19.Text = "label19";
		this.label19.Visible = false;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.Color.Red;
		this.label16.Location = new System.Drawing.Point(505, 5);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(51, 17);
		this.label16.TabIndex = 21;
		this.label16.Text = "label16";
		this.label20.AutoSize = true;
		this.label20.Dock = System.Windows.Forms.DockStyle.Left;
		this.label20.ForeColor = System.Drawing.Color.Teal;
		this.label20.Location = new System.Drawing.Point(656, 25);
		this.label20.Name = "label20";
		this.label20.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label20.Size = new System.Drawing.Size(41, 20);
		this.label20.TabIndex = 17;
		this.label20.Text = "label20";
		this.label20.Visible = false;
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.Color.Teal;
		this.label15.Location = new System.Drawing.Point(505, 25);
		this.label15.Name = "label15";
		this.label15.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label15.Size = new System.Drawing.Size(41, 17);
		this.label15.TabIndex = 22;
		this.label15.Text = "label15";
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(48, 47);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.clsBackPanel13.AutoSize = true;
		this.clsBackPanel13.Border = true;
		this.clsBackPanel13.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel13.BorderBW = 1;
		this.clsBackPanel13.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel13.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel13.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel13.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel13.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel13.BorderLW = 1;
		this.clsBackPanel13.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel13.BorderRW = 1;
		this.clsBackPanel13.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel13.BorderTW = 1;
		this.clsBackPanel13.Color1 = System.Drawing.Color.White;
		this.clsBackPanel13.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel13.ColorAngle = 90f;
		this.clsBackPanel13.Controls.Add(this.flowLayoutPanel1);
		this.clsBackPanel13.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel13.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clsBackPanel13.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel13.Name = "clsBackPanel13";
		this.clsBackPanel13.Size = new System.Drawing.Size(973, 38);
		this.clsBackPanel13.TabIndex = 4;
		this.clsBackPanel13.Visible = false;
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel1.Controls.Add(this.btnClCh);
		this.flowLayoutPanel1.Controls.Add(this.label84);
		this.flowLayoutPanel1.Controls.Add(this.txtCurRn);
		this.flowLayoutPanel1.Controls.Add(this.label86);
		this.flowLayoutPanel1.Controls.Add(this.label85);
		this.flowLayoutPanel1.Controls.Add(this.txtTGRn);
		this.flowLayoutPanel1.Controls.Add(this.btnOK);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(973, 38);
		this.flowLayoutPanel1.TabIndex = 5;
		this.btnClCh.BackColor = System.Drawing.Color.Transparent;
		this.btnClCh.Checked = false;
		this.btnClCh.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnClCh.DefaultColor = System.Drawing.Color.Transparent;
		this.btnClCh.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClCh.ImageNew = LockSoftware.Properties.Resources.close;
		this.btnClCh.ImageRedrawed = true;
		this.btnClCh.ImageStyle = 0;
		this.btnClCh.isButton = true;
		this.btnClCh.Location = new System.Drawing.Point(8, 8);
		this.btnClCh.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
		this.btnClCh.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnClCh.MouseDownEndColor = System.Drawing.Color.White;
		this.btnClCh.MouseDownStartColor = System.Drawing.Color.White;
		this.btnClCh.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnClCh.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnClCh.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnClCh.Name = "btnClCh";
		this.btnClCh.Size = new System.Drawing.Size(24, 24);
		this.btnClCh.TabIndex = 4;
		this.btnClCh.TextImageLocation = 0;
		this.btnClCh.TextNew = "";
		this.btnClCh.TextRedrawed = false;
		this.btnClCh.Click += new System.EventHandler(btnClCh_Click);
		this.label84.AutoSize = true;
		this.label84.BackColor = System.Drawing.Color.Transparent;
		this.label84.Location = new System.Drawing.Point(38, 5);
		this.label84.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
		this.label84.Name = "label84";
		this.label84.Padding = new System.Windows.Forms.Padding(5, 6, 0, 0);
		this.label84.Size = new System.Drawing.Size(98, 22);
		this.label84.TabIndex = 0;
		this.label84.Text = "Original Room:";
		this.txtCurRn.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtCurRn.Location = new System.Drawing.Point(138, 8);
		this.txtCurRn.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
		this.txtCurRn.Name = "txtCurRn";
		this.txtCurRn.ReadOnly = true;
		this.txtCurRn.Size = new System.Drawing.Size(100, 24);
		this.txtCurRn.TabIndex = 1;
		this.label86.AutoSize = true;
		this.label86.Font = new System.Drawing.Font("Times New Roman", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label86.Location = new System.Drawing.Point(242, 5);
		this.label86.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
		this.label86.Name = "label86";
		this.label86.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.label86.Size = new System.Drawing.Size(30, 25);
		this.label86.TabIndex = 11;
		this.label86.Text = "→";
		this.label85.AutoSize = true;
		this.label85.BackColor = System.Drawing.Color.Transparent;
		this.label85.Location = new System.Drawing.Point(276, 5);
		this.label85.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
		this.label85.Name = "label85";
		this.label85.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label85.Size = new System.Drawing.Size(85, 22);
		this.label85.TabIndex = 3;
		this.label85.Text = "Target Room:";
		this.txtTGRn.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtTGRn.Location = new System.Drawing.Point(363, 8);
		this.txtTGRn.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
		this.txtTGRn.Name = "txtTGRn";
		this.txtTGRn.ReadOnly = true;
		this.txtTGRn.Size = new System.Drawing.Size(100, 24);
		this.txtTGRn.TabIndex = 2;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.btnOK.AutoSize = true;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(469, 5);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(61, 30);
		this.btnOK.TabIndex = 10;
		this.btnOK.Text = "OK";
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.WhiteSmoke;
		base.ClientSize = new System.Drawing.Size(984, 594);
		base.Controls.Add(this.panel1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmCenter";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Room Center";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCenter_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.clsPlRt.ResumeLayout(false);
		this.plR3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTGList).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		this.plR2.ResumeLayout(false);
		this.plR2.PerformLayout();
		this.tlpR2.ResumeLayout(false);
		this.tlpR2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.plR1.ResumeLayout(false);
		this.plR1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).EndInit();
		this.cMSRoom.ResumeLayout(false);
		this.clsBackPanel2.ResumeLayout(false);
		this.clsBackPanel2.PerformLayout();
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		this.clsBackPanel3.ResumeLayout(false);
		this.clsBackPanel3.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.clsBackPanel13.ResumeLayout(false);
		this.clsBackPanel13.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmCenter()
	{
		InitializeComponent();
		base.Controls.Add(lb_1);
		ActivePanel = plR3;
		plR1.AutoScroll = (plR2.AutoScroll = true);
		m_htab = Program.GetControlName(this, m_objName);
		label92.Text = (string)m_htab["dgvTR_discount"];
		label14.Text = (label15.Text = (label16.Text = (txtRMemo.Text = (label18.Text = (label19.Text = (label20.Text = ""))))));
		label21.Text = (label22.Text = (label23.Text = (label24.Text = (label25.Text = ""))));
		label88.Text = (label55.Text = (label90.Text = (label56.Text = (label57.Text = ""))));
		txtDiscount.Text = Program.GetFaceDisValue();
		chkSync.Checked = true;
		tSync.Interval = 10000;
		_ = flowLayoutPanel1.Width;
		_ = 963;
		for (int i = 0; i < 11; i++)
		{
			tlpR2.Controls["label" + (i + 47)].Text = "";
		}
		for (int j = 0; j < 8; j++)
		{
			tableLayoutPanel2.Controls["label" + (j + 73)].Text = "";
		}
	}

	private void toolsBtn3_Click(object sender, EventArgs e)
	{
		if (clsPlRt.Visible)
		{
			toolsBtn3.ImageNew = Resources.mini_left;
			clsPlRt.Visible = false;
		}
		else
		{
			clsPlRt.Visible = true;
			toolsBtn3.ImageNew = Resources.mini_right;
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
			Program.MsgBox((string)m_htab["Err09"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitCerType()
	{
		try
		{
			cobCer.Text = "";
			cobCer.DataSource = null;
			string sql = "Select * FROM D_Cer Where cer_flag = 0";
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
			Program.MsgBox((string)m_htab["Err06"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitStatus()
	{
		try
		{
			cobStatus.DataSource = null;
			string sql = "Select RS_ID, RS_Name000 From  D_RoomStatus Order by RS_ID, RS_Name000";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["RS_ID"] = 0;
				dataRow["RS_Name000"] = (string)m_htab["cobStatus"];
				dataTable.Rows.InsertAt(dataRow, 0);
				cobStatus.DisplayMember = dataTable.Columns["RS_Name000"].ColumnName.ToString().Trim();
				cobStatus.ValueMember = "RS_ID";
				cobStatus.DataSource = dataTable.DefaultView;
				int num = 64;
				for (int i = 1; i < cobStatus.Items.Count - 3; i++)
				{
					tableLayoutPanel2.Controls["label" + (num + i)].Text = ((DataRowView)cobStatus.Items[i]).Row.ItemArray[1].ToString();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err03"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitRImage(bool st)
	{
		Label label = label69;
		Label label2 = label77;
		ToolsBtn toolsBtn = toolsBtn9;
		bool flag = (clsBackPanel10.Visible = st);
		bool flag3 = (toolsBtn.Visible = flag);
		bool visible = (label2.Visible = flag3);
		label.Visible = visible;
		Label label3 = label71;
		Label label4 = label79;
		ToolsBtn toolsBtn2 = toolsBtn11;
		bool flag6 = (clsBackPanel12.Visible = st);
		bool flag8 = (toolsBtn2.Visible = flag6);
		bool visible2 = (label4.Visible = flag8);
		label3.Visible = visible2;
		Label label5 = label72;
		Label label6 = label80;
		bool flag11 = (toolsBtn12.Visible = st);
		bool visible3 = (label6.Visible = flag11);
		label5.Visible = visible3;
		if (st)
		{
			clsBackPanel2.Height = 325;
		}
		else
		{
			clsBackPanel2.Height = 286;
		}
	}

	private void InitRoomList(TreeNode selNode, string sqlStr)
	{
		dgvList.Items.Clear();
		string text = "Select R_Name, R_ID, R_Code, R_SubCode, R_FloorID, R_TypeID, RS_ID, R_BedAdd,R_BedSinglePrice, R_Size, R_Size As R_Memo, Build_Name, Floor_Name, TP_Name , R_CurGuestCount,R_TotalGuest, R_TotalPrice, TP_Price, TP_deposit, TP_PricelessHour, TP_PriceStandHour, RS_Name000 From v_HotelRooms Where IsNull(R_flag,0) = 0 ";
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
		text = text + sqlStr + " Group by TP_Name, R_Name, R_ID, R_Code, R_SubCode, R_FloorID, R_TypeID, RS_ID, R_BedAdd, R_BedSinglePrice, R_Size , Build_Name, Floor_Name, R_CurGuestCount, R_TotalGuest, R_TotalPrice,TP_Price,TP_deposit, RS_Name000, TP_PricelessHour, TP_PriceStandHour Order by TP_Name, Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(text);
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			InitListView(dataTable);
		}
		InitRoomTotalStatus();
	}

	private void InitRoomTotalStatus()
	{
		int num = 73;
		int num2 = 0;
		int num3 = 0;
		Label label = label62;
		Label label2 = label63;
		string text = (label64.Text = "");
		string text3 = (label2.Text = text);
		label.Text = text3;
		for (int i = 0; i < 8; i++)
		{
			tableLayoutPanel2.Controls["label" + (num + i)].Text = "";
		}
		int[] array = new int[11];
		for (int j = 0; j < dgvList.Items.Count; array[num - 1]++, j++)
		{
			num = Convert.ToInt16(dgvList.Items[j].SubItems[6].Text.ToString());
			switch (num)
			{
			case 1:
				num2++;
				continue;
			default:
				if (num != 10 && num != 11)
				{
					continue;
				}
				break;
			case 3:
			case 4:
			case 5:
			case 6:
				break;
			}
			num3++;
		}
		label62.Text = dgvList.Items.Count.ToString();
		label63.Text = num3.ToString();
		label64.Text = num2.ToString();
		num = 73;
		for (int k = 0; k < 8; k++)
		{
			tableLayoutPanel2.Controls["label" + (num + k)].Text = array[k].ToString();
		}
	}

	private void InitTreeList()
	{
		try
		{
			dgvList.Clear();
			TextBox textBox = txtLRn;
			string text = (txtRn.Text = "");
			textBox.Text = text;
			tvList.Nodes.Clear();
			string text3 = "Select B_ID, B_HotelName,Build_ID,Build_Code, Build_Name, Build_Flag, Build_Memo, Floor_ID, Floor_Code, Floor_Name, Floor_Flag, Floor_Memo From v_HotelBF";
			text3 += " Where 1=1";
			text3 += " And Not (Floor_Flag=1 or Build_Flag = 1)";
			text3 += " Order by B_ID, Build_ID, Floor_ID ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text3);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			TreeNode treeNode = null;
			TreeNode treeNode2 = null;
			string text5;
			string text4 = (text5 = "");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (text4 != dataTable.Rows[i]["B_HotelName"].ToString().Trim())
				{
					text4 = dataTable.Rows[i]["B_HotelName"].ToString().Trim();
					treeNode = new TreeNode(text4, 0, 2);
					treeNode.Name = dataTable.Rows[i]["B_ID"].ToString().Trim();
					tvList.Nodes.Add(treeNode);
				}
				if (text5 != dataTable.Rows[i]["Build_Name"].ToString().Trim())
				{
					text5 = dataTable.Rows[i]["Build_Name"].ToString().Trim();
					treeNode2 = new TreeNode(text5, 1, 2);
					treeNode2.Name = dataTable.Rows[i]["Build_ID"].ToString().Trim();
					treeNode.Nodes.Add(treeNode2);
				}
				if (dataTable.Rows[i]["Floor_Name"].ToString().Trim() != "")
				{
					treeNode2?.Nodes.Add(dataTable.Rows[i]["Floor_ID"].ToString().Trim(), dataTable.Rows[i]["Floor_Name"].ToString().Trim(), 1, 2);
				}
			}
			tvList.Select();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private string getSqlStr()
	{
		string text = "";
		if (cobStatus.SelectedIndex > 0)
		{
			text = text + " And R_RSID=" + cobStatus.SelectedValue.ToString();
		}
		if (cobType.SelectedIndex > 0)
		{
			text = text + " And R_TypeID=" + cobType.SelectedValue.ToString();
		}
		if (txtSRn.ForeColor == Color.Black && txtSRn.Text.Trim() != "")
		{
			text = text + " And R_Name like N'%" + txtSRn.Text.Trim() + "%'";
		}
		if (txtBM.Visible && txtBM.ForeColor == Color.Black && txtBM.Text.Trim() != "")
		{
			string text2 = txtBM.Text.Trim();
			string text3 = text;
			text = text3 + " And r_id in ( Select r_id from T_Schedule Where sch_flag = 0 And sch_name like N'%" + text2 + "%' or sch_mob  like N'%" + text2 + "%' or g_name  like N'%" + text2 + "%')";
		}
		return text;
	}

	private void refresh_room(string room, int roomstatus, int gcount)
	{
		try
		{
			try
			{
				if (roomstatus >= 0 && roomstatus < 6)
				{
					int num = -1;
					if (Program.fm != null)
					{
						num = Program.fm.cur_rnList.IndexOf(room);
						if (num >= 0)
						{
							Program.fm.cur_rnList.RemoveAt(num);
						}
					}
					if (Program.fpop != null)
					{
						for (int i = 0; i < Program.fpop.rnlist.Count; i++)
						{
							if (((guestListCls)Program.fpop.rnlist[i]).c_rn == room)
							{
								Program.fpop.rnlist.RemoveAt(i);
								Program.fpop.tCount = 0;
								Program.fpop.labrn.Text = "";
								break;
							}
						}
					}
				}
			}
			catch
			{
			}
			for (int j = 0; j < dgvList.Items.Count; j++)
			{
				if (dgvList.Items[j].Text == room)
				{
					dgvList.Items[j].ImageIndex = roomstatus;
					pictureBox1.Image = imgRoom.Images[roomstatus];
					if (gcount == 0)
					{
						dgvList.Items[j].SubItems[14].Text = "0";
					}
					else
					{
						dgvList.Items[j].SubItems[14].Text = (Convert.ToInt32(dgvList.Items[j].SubItems[14].Text.Trim()) + gcount).ToString();
					}
					DataView dataView = (DataView)cobStatus.DataSource;
					dgvList.Items[j].SubItems[6].Text = (roomstatus + 1).ToString();
					dgvList.Items[j].SubItems[dgvList.Items[j].SubItems.Count - 1].Text = dataView.Table.Rows[roomstatus + 1][1].ToString();
					break;
				}
			}
			InitRoomList(tvList.SelectedNode, getSqlStr());
		}
		catch
		{
		}
	}

	private void frmCenter_Load(object sender, EventArgs e)
	{
		Panel panel = plR1;
		Panel panel2 = plR2;
		bool flag = (plR3.AutoScroll = true);
		bool autoScroll = (panel2.AutoScroll = flag);
		panel.AutoScroll = autoScroll;
		try
		{
			TSMIRCh.Visible = SQLserver.GetUserPermisstion(1026, Program.m_OperID);
			TSMIRCard.Visible = SQLserver.GetUserPermisstion(1036, Program.m_OperID);
			TSMITCard.Visible = SQLserver.GetUserPermisstion(1037, Program.m_OperID);
			btnCard.Visible = SQLserver.GetUserPermisstion(1015, Program.m_OperID);
			btnGCSO.Visible = SQLserver.GetUserPermisstion(1016, Program.m_OperID);
			btnLC.Visible = SQLserver.GetUserPermisstion(1017, Program.m_OperID);
			btnLN.Visible = SQLserver.GetUserPermisstion(1018, Program.m_OperID);
			btnTGL.Visible = SQLserver.GetUserPermisstion(1025, Program.m_OperID);
			btnTGO.Visible = SQLserver.GetUserPermisstion(1024, Program.m_OperID);
			btnTGIn.Enabled = SQLserver.GetUserPermisstion(1022, Program.m_OperID);
			TSMISubGLog.Visible = SQLserver.GetUserPermisstion(1032, Program.m_OperID);
			TSMISubRLog.Visible = SQLserver.GetUserPermisstion(1034, Program.m_OperID);
			if (!TSMIRCard.Visible && !TSMITCard.Visible)
			{
				toolStripSeparator1.Visible = false;
			}
			toolStripSeparator1.Visible = TSMIRCh.Visible;
			SQLserver.GetUserPermisstion(1048, Program.m_OperID);
			ToolStripSeparator toolStripSeparator = toolStripSeparator4;
			bool visible = (TSMISubOth.Visible = false);
			toolStripSeparator.Visible = visible;
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)Program.m_hPubTab["InfoPermission"] + ex.Message, MessageBoxIcon.Hand);
		}
		btnRInfo_Click(null, null);
		InitTreeList();
		InitType();
		InitStatus();
		InitCerType();
		InitCurrency();
		InitRImage(st: false);
		try
		{
			for (int i = 1; i < cobStatus.Items.Count; i++)
			{
				try
				{
					TSMIRSCh.DropDownItems[i - 1].Text = ((DataRowView)cobStatus.Items[i]).Row.ItemArray[1].ToString();
					TSMIRSCh.DropDownItems[i - 1].Click += TSMIRSCH_SUB_Click;
				}
				catch
				{
				}
			}
			TSMIRSCh.Text = (string)m_htab["TSMIRSCh"];
			btnOK.Text = (string)Program.m_hPubTab["btnOK"];
			TSMIRCard.Text = (string)m_htab["TSMIRCard"];
			TSMITCard.Text = (string)m_htab["TSMITCard"];
			TSMIRCh.Text = (string)m_htab["TSMIRCh"];
			TSMISubOth.Text = (string)m_htab["TSMISubOth"];
			TSMISubGLog.Text = (string)Program.m_hPubTab["TSMISGuest"];
			TSMISubRLog.Text = (string)Program.m_hPubTab["TSMISRoom"];
			txtRn.Text.Trim();
			txtSRn.Text = (string)m_htab["txtSRn"];
			txtBM.Text = (string)m_htab["txtBM"];
			dtpCome.CustomFormat = Program.m_currDateTimeFmt;
			dtpLevel.CustomFormat = Program.m_currDateFmt;
			nudDay.Value = nudDay.Minimum;
			dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
			dtpCome.Value = DateTime.Now;
			cobCurrency.Text = Program.m_baseCurrCode;
		}
		catch
		{
		}
		if (Program.m_Lan == 0)
		{
			btnIDCard.Enabled = false;
		}
	}

	private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
	{
		try
		{
			if (e != null && e.Node != null)
			{
				InitRoomList(e.Node, getSqlStr());
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void toolsBtn5_Click(object sender, EventArgs e)
	{
		try
		{
			if (!toolsBtn5.Checked)
			{
				toolsBtn5.Checked = true;
			}
			else
			{
				toolsBtn5.Checked = false;
			}
			btnSear_Click(null, null);
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

	private void txtSRn_Enter(object sender, EventArgs e)
	{
		if (txtSRn.ForeColor == Color.DarkGray)
		{
			txtSRn.Text = "";
			txtSRn.ForeColor = Color.Black;
		}
	}

	public void btnSear_Click(object sender, EventArgs e)
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

	private void txtSRn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnSear_Click(null, null);
		}
	}

	private void btnRInfo_Click(object sender, EventArgs e)
	{
		if (!(ActivePanel.Name == plR1.Name))
		{
			ActivePanel = plR1;
			btnRInfo.Dock = DockStyle.Top;
			btnRGLevel.SendToBack();
			btnRGLevel.Dock = DockStyle.Bottom;
			btnTGIn.SendToBack();
			btnTGIn.Dock = DockStyle.Bottom;
			plR2.SendToBack();
			plR3.SendToBack();
			plR1.BringToFront();
			plR1.Dock = DockStyle.Fill;
		}
	}

	private void btnRGLevel_Click(object sender, EventArgs e)
	{
		if (!(ActivePanel.Name == plR2.Name))
		{
			ActivePanel = plR2;
			btnRGLevel.Dock = DockStyle.Top;
			btnRInfo.SendToBack();
			btnRInfo.Dock = DockStyle.Top;
			btnTGIn.SendToBack();
			btnTGIn.Dock = DockStyle.Bottom;
			plR1.SendToBack();
			plR3.SendToBack();
			plR2.BringToFront();
			plR2.Dock = DockStyle.Fill;
		}
	}

	private void chkSync_CheckedChanged(object sender, EventArgs e)
	{
		tSync.Enabled = chkSync.Checked;
	}

	private void tSync_Tick(object sender, EventArgs e)
	{
		if (chkSync.Checked)
		{
			dtpCome.Value = DateTime.Now;
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
			if (!string.IsNullOrEmpty(txtRn.Text))
			{
				SetleaveTime();
			}
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
			if (!string.IsNullOrEmpty(txtRn.Text))
			{
				SetleaveTime();
				GetPaymentAmount();
			}
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void chkHr_CheckedChanged(object sender, EventArgs e)
	{
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		try
		{
			if (chkHr.Checked)
			{
				if (btnGCSO.Enabled)
				{
					nudDay.Minimum = 1m;
				}
				else
				{
					nudDay.Minimum = Program.m_defHR;
				}
				nudDay.Maximum = 12m;
				nudDay.Value = nudDay.Minimum;
				label28.Text = (string)m_htab["label28_hr"];
			}
			else
			{
				nudDay.Minimum = 1m;
				nudDay.Maximum = 9999m;
				decimal.TryParse(Program.m_defDay, out var result);
				nudDay.Value = result;
				dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
				label28.Text = (string)m_htab["label28"];
			}
			if (!string.IsNullOrEmpty(txtRn.Text))
			{
				SetleaveTime();
				GetPaymentAmount();
			}
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void txtDiscount_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt16(txtDiscount.Text) < 0 || Convert.ToInt16(txtDiscount.Text) > 100)
			{
				txtDiscount.Text = "0";
			}
		}
		catch
		{
			txtDiscount.Text = "0";
		}
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		try
		{
			if (!string.IsNullOrEmpty(txtRn.Text))
			{
				GetPaymentAmount();
			}
		}
		catch
		{
		}
		m_chVal = false;
	}

	private void txtGDepo_TextChanged(object sender, EventArgs e)
	{
		try
		{
			double num = Convert.ToDouble(cobCurrency.SelectedValue);
			double num2 = Convert.ToDouble(txtGDepo.Text.Trim());
			if (num == 0.0)
			{
				num = 1.0;
			}
			basePrice = num2 * num;
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
			double num2 = basePrice;
			if (num == 0.0)
			{
				num = 1.0;
			}
			txtGDepo.Text = (num2 / num).ToString("F2");
		}
		catch
		{
		}
	}

	private bool IsNullValue()
	{
		if (Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtRn.Text.Trim(), chk: true))
		{
			return true;
		}
		if (Program.isValNull(label17.Text.Substring(0, label17.Text.Length - 1), txtGn.Text.Trim(), Program.m_chkGInfo))
		{
			return true;
		}
		if (Program.isValNull(label26.Text.Substring(0, label26.Text.Length - 1), cobCer.Text.Trim(), chk: true))
		{
			return true;
		}
		if (Program.isValNull(label27.Text.Substring(0, label27.Text.Length - 1), txtCernum.Text.Trim(), Program.m_chkGInfo))
		{
			return true;
		}
		if (Program.isValNull(label92.Text.Substring(0, label92.Text.Length - 1), txtDiscount.Text.Trim(), Program.m_chkGInfo))
		{
			return true;
		}
		if (Convert.ToDouble(txtDiscount.Text.Trim()) > 100.0 || Convert.ToDouble(txtDiscount.Text.Trim()) < 0.0)
		{
			MessageBox.Show(m_htab["Err11"].ToString());
			return true;
		}
		return false;
	}

	private void GuestCheckIn(int style)
	{
		try
		{
			if (IsNullValue())
			{
				return;
			}
			string text = txtRn.Text.Trim();
			string text2 = "";
			string sql = "Select R_ID,R_MaxCardNum,Build_Code,Floor_Code,R_Code,R_SubCode,R_RSID,R_SubCodeDai,R_CurGuestCount  From v_HotelRooms Where R_Name=N'" + text + "' And R_flag=0";
			string sql2 = "select top 1 b.tr_id,a.g_id,a.g_stand_l_time from t_guest a,t_rooms b where a.tr_id=b.tr_id and a.g_level=0 and b.r_name =N'" + text + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			DataTable dataTable2 = SQLserver.Data_GetDataTable(sql2);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (style == 1)
			{
				DialogResult dialogResult = Program.MsgBox(string.Format((string)m_htab["Info15_1"], "\r\n\r\n"), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
			}
			int num = int.Parse(dataTable.Rows[0]["R_RSID"].ToString());
			if (Convert.ToInt32(dataTable.Rows[0]["R_CurGuestCount"]) >= Program.m_basMaxGuest)
			{
				text2 = string.Format((string)m_htab["MaxGuest"], dataTable.Rows[0]["R_CurGuestCount"].ToString() + "\r\n");
				Program.MsgBox(text2, (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			int num2 = 1;
			if (chkRepl.Checked)
			{
				num2 = 0;
			}
			long num3 = Convert.ToInt32(dataTable.Rows[0]["R_ID"].ToString());
			int maxNumber = Program.getMaxNumber(1, showError: true);
			if (maxNumber < 0)
			{
				return;
			}
			maxNumber++;
			int num4 = Convert.ToInt32(dataTable.Rows[0]["Build_Code"].ToString());
			int num5 = Convert.ToInt32(dataTable.Rows[0]["Floor_Code"].ToString());
			int num6 = Convert.ToInt32(dataTable.Rows[0]["R_Code"].ToString());
			int num7 = Convert.ToInt32(dataTable.Rows[0]["R_SubCode"].ToString());
			int num8 = Convert.ToInt32(dataTable.Rows[0]["R_SubCodeDai"].ToString());
			string datetime;
			string text3;
			string standDTime;
			switch (style)
			{
			case 0:
				datetime = dtpLevel.Value.ToString("yyyyMMdd") + dtpTime.Value.ToString("HHmm");
				text3 = Program.GetStandDate(dtpLevel.Value) + " " + dtpTime.Value.ToString("HH:mm:00");
				standDTime = Program.GetStandDTime(dtpCome.Value, "00");
				break;
			case 1:
				text3 = Program.GetStandDTime(DateTime.Parse(dataTable2.Rows[0]["g_stand_l_time"].ToString()));
				datetime = DateTime.Parse(text3).ToString("yyyyMMddHHmm");
				standDTime = Program.GetStandDTime(DateTime.Now);
				break;
			default:
				return;
			}
			if (style == 0 && !Program.IsCanCheckIn(int.Parse(num3.ToString()), dtpCome.Value, DateTime.Parse(text3)))
			{
				return;
			}
			if (style == 0)
			{
				num8 += 2;
			}
			text2 = num4.ToString("X2") + num5.ToString("X2") + num6.ToString("X2") + num7.ToString("X2") + ((byte)num8).ToString("X2");
			int num9 = -1;
			if (num2 == 1 && Program.RadioWriteCard(6, maxNumber, datetime, text2, text2.Length, Buzzer: false) != 0)
			{
				return;
			}
			int num10 = 4;
			if (num == 6)
			{
				num10 = num;
			}
			int num11 = Convert.ToInt32(nudDay.Value);
			int num12 = 0;
			int num13 = 0;
			if (chkHr.Checked)
			{
				num12 = ((num11 > Program.m_defHR) ? num11 : Program.m_defHR);
			}
			else
			{
				num13 = num11;
			}
			int num14 = 0;
			string standDec = Program.GetStandDec(txtRP.Text.Trim());
			string standDec2 = Program.GetStandDec(txtGC.Text.Trim());
			string standDec3 = Program.GetStandDec(txtGDepo.Tag.ToString());
			string standDec4 = Program.GetStandDec(txtGDepo.Text.Trim());
			string standDec5 = Program.GetStandDec(Program.GetRealDisValue(txtDiscount.Text.Trim()));
			sql = "declare @_ID As bigint \n declare @g_id as bigint \n";
			if (num == 1 || num == 10)
			{
				object obj = sql;
				sql = string.Concat(obj, "Insert Into T_Rooms Values('',1,", num2.ToString(), ",0,", num3.ToString(), ",", num10.ToString(), ",N'", text, "','", num6.ToString(), "',", num7.ToString(), ",", standDec, ",", standDec5, ",", standDec4, ",'", standDTime, "',", num13, ",'", text3, "',0, NULL,", num12, ", 0, 0,  0, 0, NULL,", standDec2, ",0,'',", Program.m_baseCurrID, ",N'", Program.m_baseCurrCode, "',", Program.GetStandDec(Program.m_baseCurrRate), ",N'", cobCurrency.Text.Trim(), "',", Program.GetStandDec(cobCurrency.SelectedValue.ToString()), ", 0, 0, 0,'',", num14.ToString(), ",NULL,NULL,GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "', NULL, NULL, NULL) \n ");
				sql += "Select @_ID = @@Identity \n ";
			}
			switch (style)
			{
			case 0:
			{
				object obj3 = sql;
				sql = string.Concat(obj3, "Insert Into T_Guest Values(N'", txtGn.Text.Trim(), "',2,", cobCer.SelectedValue, ",N'", txtCernum.Text.Trim(), "','', @_ID,", num3.ToString(), ",'", num4.ToString(), "','", num5.ToString(), "','", num6.ToString(), "',", num7.ToString(), ",", ((byte)num8).ToString(), ",", maxNumber.ToString(), ",N'", text, "',0,", standDec, ",", standDec5, ",", Program.GetStandDec(Program.GetRealDisValue(txtDiscount.Text.Trim()) * Program.changeValue(standDec, CultureInfo.InvariantCulture)), ",", standDec4);
				if (num == 1 || num == 10)
				{
					string text5 = sql;
					sql = text5 + ",'" + standDTime + "'," + num12 + ",'" + text3 + "'";
				}
				object obj4 = sql;
				sql = string.Concat(obj4, ",0,NULL,NULL,", num13, ",0,0,0,NULL,0,0,'',0,0,0,NULL,0,NULL,NULL");
				sql += ",0,NULL";
				object obj5 = sql;
				sql = string.Concat(obj5, ",", num14.ToString(), ",convert(nvarchar(max),@_ID),0,NULL,0,NULL,", num2.ToString(), ",GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "', NULL, NULL, NULL,NULL, ", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(),0) \n ");
				break;
			}
			case 1:
			{
				object obj2 = sql;
				sql = string.Concat(obj2, "insert into t_guest select N'", txtGn.Text.Trim(), "',2,", cobCer.SelectedValue, ",N'", txtCernum.Text.Trim(), "','', tr_id,r_id,b_code,f_code,r_code,r_subcode,r_subdai,", maxNumber.ToString(), ",r_name,a_id,r_price,g_discount,g_singlepaid,g_deposit,g_cometime,g_stayhour,g_stand_l_time,g_stayover,g_softime,g_soltime,g_sototalday,g_sodeposit,g_level,g_actual_s_hour,g_actual_l_time,g_level_card,0,'',g_mustpaid,g_totalpaid,g_getchange,p_typeid,0,NULL,g_teamid,0,NULL,0,g_memo,0,g_lossdate,g_logout,g_logoutdate,", num2, ",GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "', NULL, NULL, NULL,NULL, ", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(),0 from t_guest where g_id=", dataTable2.Rows[0]["g_id"].ToString(), " \n ");
				string text4 = sql;
				sql = text4 + "Update T_Rooms Set TR_guestcount = TR_guestcount + 1, TR_cardcount = TR_cardcount + " + num2 + " Where TR_ID = " + dataTable2.Rows[0]["tr_id"].ToString() + " \n";
				break;
			}
			}
			object obj6 = sql;
			sql = string.Concat(obj6, "Update D_Rooms Set R_RSID=", num10.ToString(), ", R_CurGuestCount=R_CurGuestCount+1,R_MaxCardNum=", maxNumber.ToString(), ", R_SubCodeDai= ", ((byte)num8).ToString(), ", R_TotalGuest=IsNull(R_TotalGuest,0) + 1,R_TotalPrice=Isnull(R_TotalPrice,0) + ", Program.GetStandDec(double.Parse(standDec3, CultureInfo.InvariantCulture) - double.Parse(standDec2, CultureInfo.InvariantCulture)), ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", num3.ToString());
			num9 = Program.DBCompExec(sql, btnCard.Text);
			if (num9 < 0)
			{
				Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			refresh_room(txtRn.Text.Trim(), num10 - 1, 1);
			if (dgvList.SelectedItems != null && dgvList.SelectedItems.Count > 0)
			{
				dgvList.SelectedItems[0].SubItems[15].Text = (Convert.ToInt32(dgvList.SelectedItems[0].SubItems[15].Text) + 1).ToString();
				dgvList.SelectedItems[0].SubItems[16].Text = (Convert.ToDouble("0" + dgvList.SelectedItems[0].SubItems[16].Text) + (Program.changeValue(standDec3, CultureInfo.InvariantCulture) - Program.changeValue(standDec2, CultureInfo.InvariantCulture))).ToString("F2");
			}
			chkHr.Checked = false;
			txtRn.Text = "";
			nudDay.Value = nudDay.Minimum;
			chkRepl.Checked = false;
			if (num2 == 1)
			{
				Program.RadioDevBuzzer(1, 2);
			}
			if (dgvList.SelectedItems != null && dgvList.SelectedItems.Count > 0)
			{
				dgvList.SelectedItems[0].Selected = false;
			}
			InitRoomList(tvList.SelectedNode, getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message + "\n" + ex.StackTrace, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void GuestContinueStay()
	{
		try
		{
			string text = "";
			string[] array = new string[0];
			string text2 = "Select Top 1  Build_Name, Floor_Name, RS_Name000,createtime,curr_code,curr_rate,isnull(a_id,0)/2.0 as a_id,g_stand_L_time, g_level, g_actual_L_time, g_level_Card,IsNull(g_loss,0) As g_loss,r_price,g_id,g_name, g_lossdate,  g_logoutdate,g_SOLTime, g_cometime,IsNull(g_stayOver,0) As g_stayOver,IsNull(g_logout,0) As g_logout,IsNull(g_stayHour,0) As g_stayHour,g_actual_s_hour, TR_ID,(Case TR_stayover When 1 then TR_SOLTime Else TR_stand_L_time End) As TR_stand_L_time,isnull(p_typeid,-1) as ptype, Tr_sohour,tr_cometime,TR_stayHour,TR_RoomPrice,TR_deposit,TR_Bascurname,TR_stayover,r_name,r_id,r_cardnum,R_RSID,R_CurGuestCount,TP_Name,TP_deposit,tp_price,TP_PricelessHour,TP_PriceStandHour From v_CardGuest ";
			string text3 = "select t_rooms.r_price as r_price from t_rooms,t_guest where t_rooms.tr_id=t_guest.tr_id ";
			int num = -1;
			if (chkRepl.Checked)
			{
				text2 = text2 + " Where r_name = N'" + txtRn.Text.Trim() + "' And g_level = 0 \n";
				text3 = text3 + "and t_rooms.r_name = N'" + txtRn.Text.Trim() + "' And t_guest.g_level = 0 \n";
			}
			else
			{
				object[] array2 = new object[256];
				num = Program.RadioReadCard(array2, Buzzer: true, 0);
				if (num < 0)
				{
					return;
				}
				if (Convert.ToInt32(array2[0]) != 6)
				{
					Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				string[] array3 = new string[num - 3];
				for (int i = 3; i < num; i++)
				{
					array3[i - 3] = (string)array2[i];
				}
				object obj = text2;
				text2 = string.Concat(obj, " Where b_code='", array3[0], "' And f_code='", array3[1], "' And r_code='", array3[2], "' And r_cardnum=", array2[1], "\n");
				object obj2 = text3;
				text3 = string.Concat(obj2, "and t_guest.b_code='", array3[0], "' And t_guest.f_code='", array3[1], "' And t_guest.r_code='", array3[2], "' And t_guest.r_cardnum=", array2[1], "\n");
				array = array3;
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text2);
			DataTable dataTable2 = SQLserver.Data_GetDataTable(text3);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				int num2 = Convert.ToInt32(dataTable.Rows[0]["R_RSID"].ToString());
				if (num2 == 6)
				{
					Program.MsgCustom((string)m_htab["Info14"], MessageBoxIcon.Asterisk);
					return;
				}
				Math.Round(Convert.ToDouble(dataTable.Rows[0]["TR_stayHour"]));
				Convert.ToInt32(dataTable.Rows[0]["Tr_sohour"]);
				int cardnum = Convert.ToInt32(dataTable.Rows[0]["r_cardnum"].ToString());
				label54.Text = Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["TR_stand_l_time"].ToString()), "00");
				DateTime now = DateTime.Now;
				Convert.ToDouble(dataTable.Rows[0]["curr_rate"]);
				Convert.ToDouble(dataTable.Rows[0]["r_price"]);
				double num3 = Convert.ToDouble(dataTable2.Rows[0]["r_price"]);
				double num4 = Convert.ToDouble(dataTable.Rows[0]["tp_price"]);
				double num5 = Convert.ToDouble(dataTable.Rows[0]["TP_PricelessHour"]);
				double num6 = Convert.ToDouble(dataTable.Rows[0]["TP_PriceStandHour"]);
				Convert.ToDouble(dataTable.Rows[0]["TR_RoomPrice"]);
				Convert.ToDouble(dataTable.Rows[0]["TR_deposit"]);
				Convert.ToBoolean(dataTable.Rows[0]["TR_stayover"]);
				DateTime comedate = Convert.ToDateTime(dataTable.Rows[0]["tr_cometime"]);
				bool flag = false;
				if (num3 == num5)
				{
					flag = true;
				}
				if (((chkHr.Checked && !flag) || (!chkHr.Checked && flag)) && Program.MsgBox((string)m_htab["Info36"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
				double realDisValue = Program.GetRealDisValue(txtDiscount.Text.Trim());
				int num7 = 0;
				int num8 = 0;
				if (dataTable.Rows[0]["a_id"] != null)
				{
					num7 = Convert.ToInt32(dataTable.Rows[0]["a_id"]);
				}
				if (dataTable.Rows[0]["g_actual_s_hour"] != null)
				{
					num8 = Convert.ToInt32(dataTable.Rows[0]["g_actual_s_hour"]);
				}
				_ = (bool)dataTable.Rows[0]["g_logout"];
				if ((bool)dataTable.Rows[0]["g_level"])
				{
					Program.MsgBox((string)m_htab["Info08"] + "\r\n" + (string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				text = label35.Text + " " + txtLRn.Text;
				string text4;
				for (int j = 0; j < 11; j++)
				{
					text4 = text;
					text = text4 + "\r\n" + tlpR2.Controls["label" + (j + 36)].Text + " " + tlpR2.Controls["label" + (j + 47)].Text;
				}
				int num9 = Convert.ToInt16(nudDay.Value);
				int num10 = 0;
				int num11 = 0;
				if (chkHr.Checked)
				{
					num10 = num9;
				}
				else
				{
					num11 = num9;
				}
				text2 = "Select R_ID,R_MaxCardNum,Build_Code,Floor_Code,R_Code,R_SubCode,R_RSID,R_SubCodeDai From v_HotelRooms ";
				text2 += $" Where R_Name=N'{txtRn.Text}' And R_flag=0";
				if (!chkRepl.Checked)
				{
					string text5 = text2;
					text2 = text5 + " And Build_Code='" + array[0] + "' And Floor_Code='" + array[1] + "' And R_Code='" + array[2] + "' And R_SubCode=" + array[3];
				}
				DataTable dataTable3 = SQLserver.Data_GetDataTable(text2);
				if (dataTable3 == null || dataTable3.Rows.Count <= 0)
				{
					Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				int num12 = Convert.ToInt32(dataTable3.Rows[0]["Build_Code"].ToString());
				int num13 = Convert.ToInt32(dataTable3.Rows[0]["Floor_Code"].ToString());
				int num14 = Convert.ToInt32(dataTable3.Rows[0]["R_Code"].ToString());
				int num15 = Convert.ToInt32(dataTable3.Rows[0]["R_SubCode"].ToString());
				int num16 = Convert.ToInt32(dataTable3.Rows[0]["R_SubCodeDai"].ToString());
				if (num16 > 255)
				{
					num16 = 1;
				}
				text = num12.ToString("X2") + num13.ToString("X2") + num14.ToString("X2") + num15.ToString("X2") + num16.ToString("X2");
				string value = dataTable.Rows[0]["TR_stand_l_time"].ToString();
				int num17 = 0;
				value = (chkHr.Checked ? ((!flag) ? Program.GetStandDTime(now.AddHours(Convert.ToDouble(num10))) : Program.GetStandDTime(Convert.ToDateTime(value).AddHours(Convert.ToDouble(num10)), "00")) : ((!flag) ? Program.GetStandDTime(Convert.ToDateTime(value).AddDays(Convert.ToDouble(num11)), "00") : Program.GetStandDTime(DateTime.Now.Date.AddDays(num11) + TimeSpan.Parse(Program.m_defLeaveTime))));
				string datetime = Convert.ToDateTime(value).ToString("yyyyMMddHHmm");
				if (!chkRepl.Checked)
				{
					num = Program.RadioWriteCard(6, cardnum, datetime, text, text.Length, Buzzer: false);
				}
				bool flag2 = false;
				if (num7 >= Convert.ToInt32(Program.m_defDay) || num8 >= Program.m_defHR)
				{
					flag2 = true;
				}
				int ptype = Convert.ToInt32(dataTable.Rows[0]["ptype"]);
				havedata havedata2 = new havedata();
				havedata2.comedate = comedate;
				havedata2.dtnow = now;
				havedata2.isfordis = flag2;
				havedata2.isforhour = flag;
				havedata2.m_discount = realDisValue;
				havedata2.othhavhour = num8;
				havedata2.ptype = ptype;
				havedata2.rp = num4;
				havedata2.rplesshour = num5;
				havedata2.rpstandhour = num6;
				Program.getdat(havedata2);
				text2 = $"Update T_Rooms Set  TR_stayover=1, TR_SOLTime='{value}'";
				if (!chkHr.Checked)
				{
					text2 += $", TR_stayhour= TR_stayhour+{num11}";
					text2 += string.Format(", TR_SOrp = {0}, TR_deposit = TR_deposit + {1}", Program.GetStandDec(dataTable.Rows[0]["tp_price"].ToString()), Program.GetStandDec(txtGDepo.Text.Trim()));
					if (flag)
					{
						int num18 = (((double)Program.m_defHR > havedata2.havhour0 + (double)num8) ? (Program.m_defHR - num8) : ((int)havedata2.havhour0));
						num17 = (flag2 ? ((int)havedata2.havhour0) : num18);
						int num19 = num17 + num8 - Program.m_defHR;
						double num20 = (flag2 ? (havedata2.havhour0 * num6) : ((double)((num17 + num8 > Program.m_defHR) ? num19 : 0) * num6 + (double)(Program.m_defHR - num8) * num5)) * 1.0;
						text2 += ", TR_SOhour = 0";
						object obj3 = text2;
						text2 = string.Concat(obj3, ",p_typeid=Null,tr_cometime='", Program.GetStandDTime(now), "',r_price=", Program.GetStandDec(num4), ",Tr_actual_s_hour=Tr_actual_s_hour+", num17, ",tr_mustpay=tr_mustpay+", Program.GetStandDec(num20 * realDisValue));
					}
				}
				else
				{
					text2 += $", TR_SOhour = TR_SOhour + {num10}";
					text2 += $", TR_SOrp = {Program.GetStandDec(num6)}, TR_deposit=TR_deposit + {Program.GetStandDec(txtGDepo.Text.Trim())}";
					if (!flag)
					{
						text2 += ", TR_stayhour= 0";
						text4 = text2;
						text2 = text4 + ",p_typeid=NULL,tr_cometime='" + Program.GetStandDTime(now) + "',r_price=" + Program.GetStandDec(num5) + ",a_id=isnull(a_id,0)+" + Program.GetStandDec(havedata2.havday0 * 2.0) + ",tr_mustpay=tr_mustpay+" + Program.GetStandDec(havedata2.havday0 * num4 * realDisValue);
					}
				}
				object obj = text2;
				text2 = string.Concat(obj, ", Updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "' Where TR_ID=", dataTable.Rows[0]["TR_ID"].ToString(), " \n ");
				obj = text2;
				text2 = string.Concat(obj, "Update T_Guest Set g_stayover=1,g_softime = (Case g_stayover When 1 Then g_softime Else GetDate() End), g_soltime =Getdate(),g_SOTotalDay= g_SOTotalDay + ", num11, " ,SOCreator_id=", Program.m_opid, ", SOCreator=N'", Program.m_OperName, "'");
				obj = text2;
				text2 = string.Concat(obj, ",g_stayHour=g_stayHour+", num10, ", g_stand_L_time='", value, "'");
				if (!chkHr.Checked)
				{
					text2 = text2 + ", g_deposit = g_deposit+" + Program.GetStandDec(txtGDepo.Text.Trim());
					if (flag)
					{
						text2 = text2 + ",g_actual_s_hour=g_actual_s_hour+" + num17;
					}
				}
				else
				{
					text2 = text2 + ", g_deposit = g_deposit+" + Program.GetStandDec(txtGDepo.Text.Trim());
					if (!flag)
					{
						text2 = text2 + ",a_id=a_id+" + Program.GetStandDec(havedata2.havday0 * 2.0);
					}
				}
				text2 = text2 + ", r_subDai=" + num16;
				obj = text2;
				text2 = string.Concat(obj, ", Updator = N'", Program.m_OperName, "', Updator_id =", Program.m_opid, ", UpdateTime = GetDate() Where g_id =", dataTable.Rows[0]["g_id"].ToString(), " \n ");
				obj = text2;
				text2 = string.Concat(obj, "Update T_Guest Set g_stayover=1,g_actual_s_hour=g_actual_s_hour+", num17, ",g_softime = (Case g_stayover When 1 Then g_softime Else GetDate() End), g_soltime =Getdate(),g_SOTotalDay= g_SOTotalDay + ", num11, " ,SOCreator_id=", Program.m_opid, ", SOCreator=N'", Program.m_OperName, "'");
				obj = text2;
				text2 = string.Concat(obj, ",a_id=a_id+", Program.GetStandDec(havedata2.havday0 * 2.0), ", g_stayHour=g_stayHour + ", num10, ", g_stand_L_time='", value, "', r_subDai=", num16.ToString());
				obj = text2;
				text2 = string.Concat(obj, ", Updator = N'", Program.m_OperName, "', Updator_id =", Program.m_opid, ", UpdateTime = GetDate() Where TR_ID= ", dataTable.Rows[0]["TR_ID"].ToString(), " And g_id <>", dataTable.Rows[0]["g_id"].ToString(), " And g_level = 0 \n ");
				string standDec = Program.GetStandDec(txtGC.Text.Trim());
				obj = text2;
				text2 = string.Concat(obj, "Update D_Rooms Set R_SubCodeDai= ", num16.ToString(), ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "'");
				text4 = text2;
				text2 = text4 + ",  R_TotalPrice=Isnull(R_TotalPrice,0) + " + Program.GetStandDec(basePrice) + "-" + standDec;
				text2 = text2 + " Where R_ID=" + dataTable.Rows[0]["r_id"].ToString() + " \n ";
				if (Program.DBCompExec(text2, btnGCSO.Text) < 0)
				{
					Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				text = string.Format((string)m_htab["Info21"], txtLRn.Text.Trim() + " - " + label51.Text);
				txtLRn.Text = "";
				nudDay.Value = nudDay.Minimum;
				dataTable3.Clear();
				Program.RadioDevBuzzer(1, 2);
				Program.MsgCustom(text, MessageBoxIcon.Asterisk);
			}
			else
			{
				Program.MsgBox((string)Program.m_hPubTab["GuestInfoDNull"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message + "\n" + ex.StackTrace, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnCard_Click(object sender, EventArgs e)
	{
		try
		{
			switch (int.Parse(m_SelectItem.SubItems[6].Text))
			{
			case 4:
			case 5:
			case 6:
			{
				if ((sender as Button).Name == "btnCard")
				{
					GuestCheckIn(1);
					break;
				}
				string key = "Info34";
				if (chkHr.Checked)
				{
					key = "Info35";
				}
				if (Program.MsgBox(string.Format((string)m_htab[key], nudDay.Value), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{
					GuestContinueStay();
				}
				break;
			}
			case 1:
			case 10:
				GuestCheckIn(0);
				break;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtGDepo_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void SetleaveTime()
	{
		try
		{
			int num = Convert.ToInt16(nudDay.Value);
			DateTime value = dtpCome.Value;
			if (chkHr.Checked)
			{
				value = value.AddHours(num);
			}
			else
			{
				string text = "";
				text = ((!(value.TimeOfDay >= TimeSpan.Parse(Program.m_defComeTime))) ? Program.GetLocDate(value.AddDays(num - 1)) : Program.GetLocDate(value.AddDays(num)));
				value = DateTime.Parse(text + " " + Program.m_defLeaveTime);
			}
			dtpLevel.Value = value.Date;
			dtpTime.Value = value;
		}
		catch
		{
		}
	}

	private void setstation(bool t)
	{
		m_chVal = true;
		try
		{
			if (t)
			{
				TextBox textBox = txtGn;
				bool readOnly = (txtCernum.ReadOnly = !t);
				textBox.ReadOnly = readOnly;
				btnCard.Enabled = t;
				cobCer.Enabled = t;
				return;
			}
			TextBox textBox2 = txtGn;
			TextBox textBox3 = txtCernum;
			bool flag2 = (txtDiscount.ReadOnly = !t);
			bool readOnly2 = (textBox3.ReadOnly = flag2);
			textBox2.ReadOnly = readOnly2;
			ComboBox comboBox = cobCer;
			NGlassBtn nGlassBtn = btnIDCard;
			DateTimePicker dateTimePicker = dtpCome;
			CheckBox checkBox = chkSync;
			LockSoftware.Controls.GlassBtn glassBtn = btnGCSO;
			CheckBox checkBox2 = chkSync;
			ComboBox comboBox2 = cobCurrency;
			bool flag5 = (btnCard.Enabled = t);
			bool flag7 = (comboBox2.Enabled = flag5);
			bool flag9 = (checkBox2.Enabled = flag7);
			bool flag11 = (glassBtn.Enabled = flag9);
			bool flag13 = (checkBox.Checked = flag11);
			bool flag15 = (dateTimePicker.Enabled = flag13);
			bool enabled = (nGlassBtn.Enabled = flag15);
			comboBox.Enabled = enabled;
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void GetContinueInfo()
	{
		try
		{
			int num = int.Parse(m_SelectItem.SubItems[6].Text);
			DateTime value = DateTime.Now;
			double val = 1.0;
			bool flag = false;
			bool flag2 = true;
			switch (num)
			{
			case 4:
			case 5:
			case 6:
			{
				string sql = "Select Top 1 g_name, cer_id, cer_name, g_cernum, r_price, g_singlepaid, g_cometime, g_stand_L_time, g_discount, curr_code, curr_rate,g_WCard,tp_pricelesshour From v_CardGuest Where r_name = N'" + txtRn.Text + "' And g_level = 0 Order by g_id desc";
				DataTable dataTable = SQLserver.Data_GetDataTable(sql);
				if (dataTable == null || dataTable.Rows.Count <= 0)
				{
					break;
				}
				flag2 = false;
				txtGn.Text = dataTable.Rows[0]["g_name"].ToString();
				cobCer.SelectedIndex = cobCer.FindStringExact(dataTable.Rows[0]["cer_name"].ToString());
				txtCernum.Text = dataTable.Rows[0]["g_cernum"].ToString();
				m_chVal = true;
				try
				{
					chkRepl.Checked = dataTable.Rows[0]["g_WCard"].ToString().ToLower() == "false";
				}
				catch
				{
				}
				finally
				{
					m_chVal = false;
				}
				cobCurrency.SelectedIndex = cobCurrency.FindStringExact(dataTable.Rows[0]["curr_code"].ToString());
				value = DateTime.Parse(dataTable.Rows[0]["g_stand_L_time"].ToString());
				val = double.Parse(dataTable.Rows[0]["g_discount"].ToString());
				flag = double.Parse(dataTable.Rows[0]["r_price"].ToString()) == double.Parse(dataTable.Rows[0]["tp_pricelesshour"].ToString());
				break;
			}
			case 1:
			case 10:
				flag2 = true;
				break;
			}
			m_chVal = true;
			try
			{
				TextBox textBox = txtDiscount;
				bool readOnly = (btnGCSO.Enabled = !flag2);
				textBox.ReadOnly = readOnly;
				NGlassBtn nGlassBtn = btnIDCard;
				DateTimePicker dateTimePicker = dtpCome;
				CheckBox checkBox = chkSync;
				CheckBox checkBox2 = chkSync;
				bool flag4 = (cobCurrency.Enabled = flag2);
				bool flag6 = (checkBox2.Enabled = flag4);
				bool flag8 = (checkBox.Checked = flag6);
				bool enabled = (dateTimePicker.Enabled = flag8);
				nGlassBtn.Enabled = enabled;
				if (num == 6)
				{
					btnGCSO.Enabled = false;
				}
				dtpCome.Value = value;
				txtDiscount.Text = Program.GetFaceDisValue(val);
			}
			catch
			{
			}
			finally
			{
				m_chVal = false;
			}
			chkHr.Checked = flag;
			SetleaveTime();
			GetPaymentAmount();
		}
		catch
		{
		}
	}

	private void ScheduleChexkIn()
	{
	}

	private void GetPaymentAmount()
	{
		try
		{
			double num = Convert.ToDouble(nudDay.Value);
			double realDisValue = Program.GetRealDisValue(txtDiscount.Text);
			int num2 = int.Parse(m_SelectItem.SubItems[6].Text);
			double num3 = double.Parse(m_SelectItem.SubItems[20].Text);
			double num4 = 0.0;
			double num5 = 0.0;
			if (!chkHr.Checked)
			{
				txtRP.Text = m_SelectItem.SubItems[17].Text;
				if (num2 > 3 && num2 <= 6)
				{
					txtGC.Text = Program.changeValue(0.0, CultureInfo.CurrentCulture);
				}
				else
				{
					txtGC.Text = m_SelectItem.SubItems[18].Text;
				}
				num4 = Convert.ToDouble(txtRP.Text);
				num5 = num * num4;
			}
			else if (num2 > 3 && num2 <= 6)
			{
				txtRP.Text = m_SelectItem.SubItems[20].Text;
				txtGC.Text = Program.changeValue(0.0, CultureInfo.CurrentCulture);
				num5 = num * num3;
			}
			else
			{
				txtRP.Text = m_SelectItem.SubItems[19].Text;
				num4 = Convert.ToDouble(txtRP.Text);
				txtGC.Text = m_SelectItem.SubItems[18].Text;
				num5 = num4 * (double)Program.m_defHR + (num - (double)Program.m_defHR) * num3;
			}
			double num6 = Convert.ToDouble(cobCurrency.SelectedValue);
			if (num6 == 0.0)
			{
				num6 = 1.0;
			}
			txtGDepo.Tag = (num5 * realDisValue + double.Parse(txtGC.Text)).ToString("F2");
			txtGDepo.Text = ((num5 * realDisValue + double.Parse(txtGC.Text)) / num6).ToString("F2");
		}
		catch
		{
		}
	}

	private DataTable Getv_CardGuestInfos(out GuestDetail gd, string where, string where2)
	{
		gd = default(GuestDetail);
		string text = "Select Build_Name,Floor_Name,RS_Name000,createtime,curr_code,curr_rate,isnull(a_id,0)/2.0 as a_id, g_id,g_name,g_memo,IsNull(g_teamid,-1) As g_teamid,g_cometime, IsNull(g_stayHour,0) As g_stayHour,g_actual_s_hour,g_deposit,g_totalpaid,g_othprice,IsNull(g_loss,0) As g_loss,g_lossdate,IsNull(g_logout,0) As g_logout,g_level,g_logoutdate,g_SOTotalDay,g_actual_L_time, g_level_Card,g_stand_L_time,TR_RoomPrice,TR_stayover,isnull(p_typeid,-1) as ptype,TR_ID,tr_sodp, TR_mustpay, TR_totalpaid,g_singlepaid,TR_othprice,tr_cometime,TR_Bascurname,TR_discount,TR_stayHour,TR_deposit, (Case TR_stayover When 1 then TR_SOLTime Else TR_stand_L_time End) As TR_stand_l_time,r_id,r_price,r_name,R_CurGuestCount,TP_Name,TP_Price,TP_PricelessHour,TP_PriceStandHour From v_CardGuest ";
		string text2 = " Order by g_id Desc";
		string sql = "select t_rooms.r_price as r_price from t_rooms,t_guest " + where2 + " and t_rooms.tr_id=t_guest.tr_id";
		DataTable dataTable = SQLserver.Data_GetDataTable(text + where + text2);
		DataTable dataTable2 = SQLserver.Data_GetDataTable(sql);
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			foreach (ListViewItem item in dgvList.Items)
			{
				if (item.Text.Trim() == dataTable.Rows[0]["r_name"].ToString().Trim())
				{
					item.Selected = true;
					break;
				}
			}
			bool flag = false;
			bool flag2 = false;
			if (dataTable.Rows[0]["a_id"] == null)
			{
				flag = true;
			}
			Convert.ToBoolean(dataTable.Rows[0]["TR_stayover"]);
			int num = Convert.ToInt16(dataTable.Rows[0]["g_stayHour"]);
			int num2 = Convert.ToInt32(dataTable.Rows[0]["g_sototalday"]);
			Convert.ToDouble(dataTable.Rows[0]["r_price"]);
			double num3 = Convert.ToDouble(dataTable2.Rows[0]["r_price"]);
			double rp = Convert.ToDouble(dataTable.Rows[0]["tp_price"]);
			double num4 = Convert.ToDouble(dataTable.Rows[0]["TP_PricelessHour"]);
			double num5 = Convert.ToDouble(dataTable.Rows[0]["TP_PriceStandHour"]);
			Convert.ToDouble(dataTable.Rows[0]["TR_deposit"]);
			double num6 = Convert.ToDouble(dataTable.Rows[0]["TR_discount"]);
			DateTime dateTime = Convert.ToDateTime(dataTable.Rows[0]["g_cometime"]);
			DateTime dtTime = Convert.ToDateTime(dataTable.Rows[0]["g_stand_L_time"]);
			DateTime comedate = Convert.ToDateTime(dataTable.Rows[0]["tr_cometime"]);
			int ptype = Convert.ToInt32(dataTable.Rows[0]["ptype"]);
			double num7 = 0.0;
			double num8 = 0.0;
			if (dataTable.Rows[0]["a_id"] != null)
			{
				num7 = Convert.ToDouble(dataTable.Rows[0]["a_id"]);
			}
			if (dataTable.Rows[0]["g_actual_s_hour"] != null)
			{
				num8 = Convert.ToInt32(dataTable.Rows[0]["g_actual_s_hour"]);
			}
			gd.isdis = false;
			if (num7 >= (double)Convert.ToInt32(Program.m_defDay) || num8 >= (double)Program.m_defHR)
			{
				gd.isdis = true;
			}
			gd.haveday = num7;
			gd.havehour = num8;
			gd.houseids = dataTable.Rows[0]["g_memo"].ToString();
			gd.rate = Convert.ToDouble(dataTable.Rows[0]["curr_rate"]);
			gd.dtnow = DateTime.Now;
			gd.factday = 0.0;
			gd.facthour = 0.0;
			if (m_SelectItem == null)
			{
				return null;
			}
			gd.priceroom = Convert.ToDouble(m_SelectItem.SubItems[17].Text);
			gd.pricehour = Convert.ToDouble(m_SelectItem.SubItems[19].Text);
			gd.pricecontinue = double.Parse(m_SelectItem.SubItems[20].Text);
			if (num3 == num4)
			{
				flag2 = true;
			}
			if (gd.dtnow.CompareTo(dateTime) <= 0)
			{
				Program.MsgBox((string)m_htab["Info33"], (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			txtLRn.Text = dataTable.Rows[0]["r_name"].ToString().Trim();
			label47.Text = dataTable.Rows[0]["Build_Name"].ToString().Trim();
			label48.Text = dataTable.Rows[0]["Floor_Name"].ToString().Trim();
			label49.Text = dataTable.Rows[0]["TP_Name"].ToString().Trim();
			label50.Text = dataTable.Rows[0]["R_CurGuestCount"].ToString().Trim();
			label51.Text = dataTable.Rows[0]["g_name"].ToString().Trim();
			label52.Text = Program.GetLocDTime(dateTime);
			label54.Text = Program.GetLocDTime(dtTime);
			string[] array = gd.houseids.Replace("->", ",").Trim(',').Split(',');
			gd.houseids = "(" + gd.houseids.Replace("->", ",").Trim(',') + ")";
			string sql2 = "select isnull(a_id,0)/2.0 as a_id,Tr_actual_s_hour,tr_id,r_name,tr_sodp,tr_mustpay from t_rooms where tr_id in " + gd.houseids;
			DataTable dataTable3 = SQLserver.Data_GetDataTable(sql2);
			gd.houses = ",";
			gd.extrapay = 0.0;
			gd.tr_mustp = 0.0;
			if (dataTable3 != null)
			{
				string[] array2 = array;
				foreach (string text3 in array2)
				{
					for (int j = 0; j < dataTable3.Rows.Count; j++)
					{
						if (dataTable3.Rows[j]["tr_id"].ToString() == text3)
						{
							gd.houses = gd.houses + dataTable3.Rows[j]["r_name"].ToString() + ",";
							gd.extrapay += Convert.ToDouble(dataTable3.Rows[j]["tr_sodp"]);
							gd.tr_mustp += Convert.ToDouble(dataTable3.Rows[j]["tr_mustpay"]);
							break;
						}
					}
				}
			}
			gd.houses = gd.houses.Trim(',');
			if (!flag)
			{
				havedata havedata2 = new havedata();
				havedata2.comedate = comedate;
				havedata2.dtnow = gd.dtnow;
				havedata2.isfordis = gd.isdis;
				havedata2.isforhour = flag2;
				havedata2.m_discount = num6;
				havedata2.othhavhour = num8;
				havedata2.ptype = ptype;
				havedata2.rp = rp;
				havedata2.rplesshour = num4;
				havedata2.rpstandhour = num5;
				Program.getdat(havedata2);
				if (flag2)
				{
					double num9 = (((double)Program.m_defHR > havedata2.havhour0 + num8) ? ((double)Program.m_defHR - num8) : ((double)(int)havedata2.havhour0));
					double num10 = (gd.isdis ? ((double)(int)havedata2.havhour0) : num9);
					int num11 = (int)(num10 + num8 - (double)Program.m_defHR);
					gd.facthour = num10;
					gd.totalCur = (gd.isdis ? (havedata2.havhour0 * num5) : ((double)((num10 + num8 > (double)Program.m_defHR) ? num11 : 0) * num5 + ((double)Program.m_defHR - num8) * num4)) * 1.0;
				}
				else
				{
					gd.factday = havedata2.havday0;
					gd.totalCur = gd.factday * gd.priceroom;
				}
				label42.Text = (string)m_htab["label28_hr"];
				label87.Text = (string)m_htab["label87_hr"];
				label53.Text = num2 + " " + (string)Program.m_hPubTab["InfoDay"] + " " + num + " " + (string)Program.m_hPubTab["InfoHour"];
				label88.Text = gd.factday + "+" + num7 + " " + (string)Program.m_hPubTab["InfoDay"] + " " + gd.facthour + "+" + num8 + " " + (string)Program.m_hPubTab["InfoHour"];
			}
			gd.othpay_tr = Convert.ToDouble(dataTable.Rows[0]["TR_othprice"]);
			double num12 = 0.0;
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				string sql3 = "select isnull(sum(othp_mpay),0) from t_otherpaid where a_id=0 and g_id=" + dataTable.Rows[k]["g_id"].ToString();
				DataTable dataTable4 = SQLserver.Data_GetDataTable(sql3);
				if (dataTable4.Rows.Count == 1)
				{
					num12 += Convert.ToDouble(dataTable4.Rows[0][0]);
				}
			}
			gd.othpay_g = num12;
			gd.paid = Convert.ToDouble(dataTable.Rows[0]["g_deposit"]);
			gd.totalCur *= num6;
			gd.change = gd.paid * gd.rate - gd.totalCur - gd.othpay_g - gd.extrapay - gd.tr_mustp;
			if (gd.paid * gd.rate < gd.totalCur + gd.othpay_g + gd.extrapay + gd.tr_mustp)
			{
				label57.ForeColor = Color.FromArgb(192, 0, 0);
			}
			else
			{
				label57.ForeColor = Color.Green;
			}
			label55.Text = gd.totalCur.ToString("F2") + " " + dataTable.Rows[0]["TR_Bascurname"].ToString().Trim() + "+" + gd.tr_mustp.ToString("F2") + " " + dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			label90.Text = gd.othpay_g.ToString("F2") + " " + dataTable.Rows[0]["TR_Bascurname"].ToString().Trim() + "+" + gd.extrapay.ToString("F2") + " " + dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			label56.Text = gd.paid.ToString("F2") + " " + dataTable.Rows[0]["curr_code"].ToString().Trim();
			label57.Text = gd.change.ToString("F2") + " " + dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			if ((bool)dataTable.Rows[0]["g_loss"])
			{
				Program.MsgBox((string)Program.m_hPubTab["cardinfoLost"] + "\r\n" + (string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			if ((bool)dataTable.Rows[0]["g_level"])
			{
				Program.MsgBox((string)m_htab["Info08"] + "\r\n" + (string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
		}
		else
		{
			Program.MsgBox((string)Program.m_hPubTab["cardinfoDNull"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return dataTable;
	}

	private void btnRC_Click(object sender, EventArgs e)
	{
		try
		{
			object[] array = new object[256];
			int num = Program.RadioReadCard(array, Buzzer: true, 0);
			if (num < 0)
			{
				return;
			}
			if (Convert.ToInt32(array[0]) != 6)
			{
				Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string[] array2 = new string[num - 3];
			for (int i = 3; i < num; i++)
			{
				array2[i - 3] = (string)array[i];
			}
			string text = "Where b_code='" + array2[0] + "' And f_code='" + array2[1] + "' And r_code='" + array2[2] + "' And r_subcode=" + array2[3] + " and g_level=0";
			string where = "Where t_guest.b_code='" + array2[0] + "' And t_guest.f_code='" + array2[1] + "' And t_guest.r_code='" + array2[2] + "' And t_guest.r_subcode=" + array2[3] + " and t_guest.g_level=0";
			GuestDetail gd = default(GuestDetail);
			DataTable dataTable = Getv_CardGuestInfos(out gd, text, where);
			if (dataTable != null)
			{
				_ = dataTable.Rows.Count;
				_ = 0;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnLC_Click(object sender, EventArgs e)
	{
		frmBill frmBill2 = new frmBill();
		try
		{
			object[] array = new object[256];
			int num = Program.RadioReadCard(array, Buzzer: true, 0);
			bool flag = false;
			if (num < 0)
			{
				return;
			}
			if (Convert.ToInt32(array[0]) != 6)
			{
				Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string[] array2 = new string[num - 3];
			for (int i = 3; i < num; i++)
			{
				array2[i - 3] = (string)array[i];
			}
			string text = "";
			GuestDetail gd = default(GuestDetail);
			DataTable dataTable = Getv_CardGuestInfos(out gd, "Where b_code='" + array2[0] + "' And f_code='" + array2[1] + "' And r_code='" + array2[2] + "' And r_subcode=" + array2[3] + " and g_level=0 And r_subDai=" + array2[4], "Where t_guest.b_code=N'" + array2[0] + "' And t_guest.f_code='" + array2[1] + "' And t_guest.r_code='" + array2[2] + "' And t_guest.r_subcode=" + array2[3] + " and t_guest.g_level=0 And t_guest.r_subDai=" + array2[4]);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			if (Convert.ToInt64(dataTable.Rows[0]["g_teamid"]) != -1)
			{
				flag = true;
			}
			text = label35.Text + " " + txtLRn.Text;
			string text2 = "";
			for (int j = 0; j < 11; j++)
			{
				string text3 = text;
				text = text3 + "\r\n" + tlpR2.Controls["label" + (j + 36)].Text + " " + tlpR2.Controls["label" + (j + 47)].Text;
				if (j + 36 == 43)
				{
					string text4 = text;
					text = text4 + "\r\n" + label87.Text + " " + label88.Text;
				}
			}
			if (flag)
			{
				text = text + "\r\n\r\n" + (string)m_htab["Info12"];
			}
			else
			{
				text = text + "\r\n\r\n" + (string)Program.m_hPubTab["InfoGCL"];
			}
			frmBill2.m_LeaveTime = gd.dtnow;
			if (flag)
			{
				frmBill2.labMsg.Text = (string)m_htab["Info12"];
			}
			else
			{
				frmBill2.labMsg.Text = (string)Program.m_hPubTab["InfoGCL"];
			}
			frmBill2.m_Total = gd.totalCur + gd.othpay_g + gd.extrapay + gd.tr_mustp;
			frmBill2.m_Deposit = gd.paid;
			frmBill2.m_Paid = 0.0;
			frmBill2.totalCur = gd.totalCur;
			frmBill2.m_Change = gd.change;
			frmBill2.m_Rate = gd.rate;
			frmBill2.Extrapay = gd.extrapay;
			frmBill2.houses = gd.houses;
			frmBill2.houseids = gd.houseids;
			frmBill2.isdis = gd.isdis;
			frmBill2.havday = gd.haveday;
			frmBill2.havhour = gd.havehour;
			frmBill2.txt01.Text = dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			frmBill2.txt02.Text = dataTable.Rows[0]["curr_code"].ToString().Trim();
			frmBill2.txt03.Text = dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			frmBill2.txt04.Text = dataTable.Rows[0]["curr_code"].ToString().Trim();
			frmBill2.txtTotal.Text = frmBill2.m_Total.ToString("F2");
			frmBill2.txtDep.Text = gd.paid.ToString("F2");
			frmBill2.txtPaid.Text = Program.GetLocDecStr("0.0");
			frmBill2.txtChange.Text = gd.change.ToString("F2");
			frmBill2.m_trid = Convert.ToInt32(dataTable.Rows[0]["TR_ID"]);
			frmBill2.m_gid = Convert.ToInt32(dataTable.Rows[0]["g_id"]);
			frmBill2.m_chkIn = label52.Text;
			frmBill2.m_chkOut = Program.GetLocDTime(DateTime.Now);
			frmBill2.m_FactDay = gd.factday;
			frmBill2.m_FactHour = gd.facthour;
			frmBill2.m_RoomPrice = gd.priceroom;
			frmBill2.m_HourPrice = gd.pricehour;
			frmBill2.m_AddHourPrice = gd.pricecontinue;
			frmBill2.m_ChangeRoom = TSMIRCh.Text;
			frmBill2.m_OtherPaid = gd.othpay_g;
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				frmBill2.guestsName.Add(dataTable.Rows[k]["g_name"].ToString().Trim());
				frmBill2.guestsInfoDT.Rows.Add(dataTable.Rows[k]["g_id"], dataTable.Rows[k]["g_name"]);
			}
			if (frmBill2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			gd.change = frmBill2.m_Change;
			gd.paid = frmBill2.m_Paid + frmBill2.m_Deposit;
			text2 = "Update T_Rooms Set TR_mustpay =  " + Program.GetStandDec(gd.totalCur + gd.extrapay) + ", TR_totalpaid=" + Program.GetStandDec(gd.paid) + ", TR_getchange=" + Program.GetStandDec(gd.change.ToString("F2"));
			object obj = text2;
			text2 = string.Concat(obj, ", TR_Level=1,a_id=a_id+", Program.GetStandDec(gd.factday * 2.0), ",TR_actual_S_Hour=TR_actual_S_Hour+", Program.GetStandDec(gd.facthour), ", TR_actual_l_time=GetDate(), Updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "' Where TR_ID=", dataTable.Rows[0]["TR_ID"].ToString(), " \n ");
			text2 += "Update D_Rooms Set R_RSID = 2, R_CurGuestCount = 0";
			object obj2 = text2;
			text2 = string.Concat(obj2, ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", dataTable.Rows[0]["r_id"].ToString(), " \n ");
			object obj3 = text2;
			text2 = string.Concat(obj3, "Update T_Guest Set g_level=1,a_id=a_id+", Program.GetStandDec(gd.factday * 2.0), ",g_actual_S_Hour=g_actual_S_Hour+", Program.GetStandDec(gd.facthour), ",g_mustpaid=", Program.GetStandDec(frmBill2.m_Total), ",g_actual_l_time = getdate(), g_level_card = 1,LevelCreator_id=", Program.m_opid, ", LevelCreator=N'", Program.m_OperName, "', Updator = N'", Program.m_OperName, "', Updator_id =", Program.m_opid, ", UpdateTime = GetDate()  Where TR_ID=", dataTable.Rows[0]["TR_ID"].ToString(), " And g_level=0  \n");
			if (Program.DBCompExec(text2, btnLC.Text) < 0)
			{
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			try
			{
				if (frmBill2.chkPB.Checked)
				{
					frmBill2.rptbill.PrintDialog();
				}
			}
			catch
			{
			}
			refresh_room(txtLRn.Text.Trim(), Program.m_defLS, 0);
			txtLRn.Text = "";
			if ((num = Program.RadioClearCard(1, Buzzer: false, 0, 6, Convert.ToInt32(array[1].ToString()))) < 0)
			{
				Program.MsgBox((string)m_htab["Err07"] + num + "\r\n" + (string)m_htab["Err08"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Program.RadioDevBuzzer(1, 2);
			if (dgvList.SelectedItems.Count > 0 && dgvList.SelectedItems[0] != null)
			{
				dgvList.SelectedItems[0].Selected = false;
			}
			InitRoomList(tvList.SelectedNode, getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			if (frmBill2 != null && !frmBill2.IsDisposed)
			{
				frmBill2.rptbill.Dispose();
				frmBill2.Dispose();
			}
		}
	}

	private void btnLN_Click(object sender, EventArgs e)
	{
		frmBill frmBill2 = new frmBill();
		try
		{
			if (txtLRn.Text.Trim() == "")
			{
				return;
			}
			string text = "";
			GuestDetail gd = default(GuestDetail);
			DataTable dataTable = Getv_CardGuestInfos(out gd, "Where r_name=N'" + txtLRn.Text.Trim() + "' And g_level = 0", "Where t_guest.r_name=N'" + txtLRn.Text.Trim() + "' And t_guest.g_level = 0");
			bool flag = false;
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			if (Convert.ToInt64(dataTable.Rows[0]["g_teamid"]) != -1)
			{
				flag = true;
			}
			text = label35.Text + " " + txtLRn.Text;
			string text2 = "";
			for (int i = 0; i < 11; i++)
			{
				string text3 = text;
				text = text3 + "\r\n" + tlpR2.Controls["label" + (i + 36)].Text + " " + tlpR2.Controls["label" + (i + 47)].Text;
				if (i + 36 == 43)
				{
					string text4 = text;
					text = text4 + "\r\n" + label87.Text + " " + label88.Text;
				}
			}
			if (flag)
			{
				text = text + "\r\n\r\n" + (string)m_htab["Info12"];
			}
			else
			{
				text = text + "\r\n\r\n" + (string)Program.m_hPubTab["InfoGCLN"];
			}
			frmBill2.m_LeaveTime = gd.dtnow;
			if (flag)
			{
				frmBill2.labMsg.Text = (string)m_htab["Info12"];
			}
			else
			{
				frmBill2.labMsg.Text = (string)Program.m_hPubTab["InfoGCL"];
			}
			frmBill2.m_Total = gd.totalCur + gd.othpay_g + gd.extrapay + gd.tr_mustp;
			frmBill2.m_Deposit = gd.paid;
			frmBill2.m_Paid = 0.0;
			frmBill2.totalCur = gd.totalCur;
			frmBill2.m_Change = gd.change;
			frmBill2.m_Rate = gd.rate;
			frmBill2.Extrapay = gd.extrapay;
			frmBill2.houses = gd.houses;
			frmBill2.houseids = gd.houseids;
			frmBill2.isdis = gd.isdis;
			frmBill2.havday = gd.haveday;
			frmBill2.havhour = gd.havehour;
			frmBill2.txt01.Text = dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			frmBill2.txt02.Text = dataTable.Rows[0]["curr_code"].ToString().Trim();
			frmBill2.txt03.Text = dataTable.Rows[0]["TR_Bascurname"].ToString().Trim();
			frmBill2.txt04.Text = dataTable.Rows[0]["curr_code"].ToString().Trim();
			frmBill2.txtTotal.Text = frmBill2.m_Total.ToString("F2");
			frmBill2.txtDep.Text = gd.paid.ToString("F2");
			frmBill2.txtPaid.Text = Program.GetLocDecStr("0.0");
			frmBill2.txtChange.Text = gd.change.ToString("F2");
			frmBill2.m_trid = Convert.ToInt32(dataTable.Rows[0]["TR_ID"]);
			frmBill2.m_gid = Convert.ToInt32(dataTable.Rows[0]["g_id"]);
			frmBill2.m_chkIn = label52.Text;
			frmBill2.m_chkOut = Program.GetLocDTime(DateTime.Now);
			frmBill2.m_FactDay = gd.factday;
			frmBill2.m_FactHour = gd.facthour;
			frmBill2.m_RoomPrice = gd.priceroom;
			frmBill2.m_HourPrice = gd.pricehour;
			frmBill2.m_AddHourPrice = gd.pricecontinue;
			frmBill2.m_ChangeRoom = TSMIRCh.Text;
			frmBill2.m_OtherPaid = gd.othpay_g;
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				frmBill2.guestsName.Add(dataTable.Rows[j]["g_name"].ToString().Trim());
				frmBill2.guestsInfoDT.Rows.Add(dataTable.Rows[j]["g_id"], dataTable.Rows[j]["g_name"]);
			}
			if (frmBill2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			gd.change = frmBill2.m_Change;
			gd.paid = frmBill2.m_Paid + frmBill2.m_Deposit;
			text2 = "Update T_Rooms Set TR_mustpay =  " + Program.GetStandDec(gd.totalCur + gd.extrapay) + ", TR_totalpaid=" + Program.GetStandDec(gd.paid) + ", TR_getchange=" + Program.GetStandDec(gd.change.ToString("F2")) + ", TR_Level=1,a_id=a_id+" + Program.GetStandDec(gd.factday * 2.0) + ",TR_actual_S_Hour=TR_actual_S_Hour+" + Program.GetStandDec(gd.facthour) + ", TR_actual_l_time=GetDate(),Updatetime=GetDate(),updator_id=" + Program.m_opid + ", updator=N'" + Program.m_OperName + "' Where TR_ID=" + dataTable.Rows[0]["TR_ID"].ToString() + " And TR_Level=0 \n ";
			text2 += "Update D_Rooms Set R_RSID = 2, R_CurGuestCount = 0";
			object obj = text2;
			text2 = string.Concat(obj, ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", dataTable.Rows[0]["r_id"].ToString(), " \n ");
			object obj2 = text2;
			text2 = string.Concat(obj2, "Update T_Guest Set g_level=1,a_id=a_id+", Program.GetStandDec(gd.factday * 2.0), ",g_actual_S_Hour=g_actual_S_Hour+", Program.GetStandDec(gd.facthour), ",g_mustpaid=", Program.GetStandDec(frmBill2.m_Total), ",g_actual_l_time = getdate(), g_level_card = 0,LevelCreator_id=", Program.m_opid, ", LevelCreator=N'", Program.m_OperName, "', Updator = N'", Program.m_OperName, "', Updator_id =", Program.m_opid, ", UpdateTime = GetDate() Where  TR_ID=", dataTable.Rows[0]["TR_ID"].ToString(), " And g_level=0 \n ");
			if (Program.DBCompExec(text2, btnLN.Text) < 0)
			{
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			try
			{
				if (frmBill2.chkPB.Checked)
				{
					frmBill2.rptbill.PrintDialog();
				}
			}
			catch
			{
			}
			refresh_room(txtLRn.Text.Trim(), Program.m_defLS, 0);
			Program.MsgBox(txtLRn.Text + "\r\n" + (string)m_htab["Info09"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtLRn.Text = "";
			if (dgvList.SelectedItems.Count > 0 && dgvList.SelectedItems[0] != null)
			{
				dgvList.SelectedItems[0].Selected = false;
			}
			InitRoomList(tvList.SelectedNode, getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		finally
		{
			if (frmBill2 != null && !frmBill2.IsDisposed)
			{
				frmBill2.rptbill.Dispose();
				frmBill2.Dispose();
			}
		}
	}

	private void toolsBtn1_Click(object sender, EventArgs e)
	{
		if (clsBackPanel1.Visible)
		{
			toolsBtn1.ImageNew = Resources.mini_top;
			clsBackPanel1.Visible = false;
		}
		else
		{
			clsBackPanel1.Visible = true;
			toolsBtn1.ImageNew = Resources.mini_bottom;
		}
	}

	private void InitDgvTGList(long tid)
	{
		string text = "Select TR_ID, g_teamid, R_Name, Count(g_id) As gCount, TR_cardcount,TR_Level, TR_mustpay, TR_totalpaid, Build_Name, Floor_Name, TP_Name, TB_name, team_name, team_guide,Team_cername, team_cernum, curr_rate, TR_Bascurname, curr_code, TR_cometime, TP_Price, TR_deposit,";
		text += "TR_actual_L_time, TR_roomprice,";
		text = ((Program.m_defDiscount != 1) ? (text + "(1-TR_discount)as TR_discount,") : (text + "TR_discount,"));
		text = text + "TR_stayhour,Tr_sodp  From v_TeamDetails Where tr_level=0 and g_teamid = " + tid + " Group by  TR_ID, g_teamid, r_name, build_name, floor_name, TP_Name, TB_name, team_name, team_guide,Team_cername, team_cernum, TR_mustpay,Tr_sodp,TR_totalpaid, TR_cardcount, TR_Level, curr_rate, TR_Bascurname, curr_code,TR_cometime,TP_Price, TR_deposit,TR_actual_L_time, TR_roomprice, TR_discount, TR_stayhour Order by TR_Level, build_name, floor_name, TP_Name, r_name";
		dgvTGList.DataSource = null;
		DataTable dataTable = SQLserver.Data_GetDataTable(text);
		if (dataTable != null)
		{
			dgvTGList.DataSource = dataTable.DefaultView;
			for (int i = 1; i < dgvTGList.Columns.Count; i++)
			{
				dgvTGList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvTGList.Columns[i].Name];
			}
			DataGridViewColumn dataGridViewColumn = dgvTGList.Columns["TP_Price"];
			DataGridViewColumn dataGridViewColumn2 = dgvTGList.Columns["TR_discount"];
			DataGridViewColumn dataGridViewColumn3 = dgvTGList.Columns["curr_rate"];
			bool flag = (dgvTGList.Columns["g_teamid"].Visible = false);
			bool flag3 = (dataGridViewColumn3.Visible = flag);
			bool visible = (dataGridViewColumn2.Visible = flag3);
			dataGridViewColumn.Visible = visible;
			dgvTGList.Columns["TR_discount"].Visible = true;
			DataGridViewColumn dataGridViewColumn4 = dgvTGList.Columns["TR_cometime"];
			DataGridViewColumn dataGridViewColumn5 = dgvTGList.Columns["TR_actual_L_time"];
			DataGridViewColumn dataGridViewColumn6 = dgvTGList.Columns["TR_Bascurname"];
			bool flag6 = (dgvTGList.Columns["curr_code"].Visible = false);
			bool flag8 = (dataGridViewColumn6.Visible = flag6);
			bool visible2 = (dataGridViewColumn5.Visible = flag8);
			dataGridViewColumn4.Visible = visible2;
			DataGridViewColumn dataGridViewColumn7 = dgvTGList.Columns["TR_ID"];
			DataGridViewColumn dataGridViewColumn8 = dgvTGList.Columns["TR_roomprice"];
			bool flag11 = (dgvTGList.Columns["TR_stayhour"].Visible = false);
			bool visible3 = (dataGridViewColumn8.Visible = flag11);
			dataGridViewColumn7.Visible = visible3;
			dgvTGList.Columns["Tr_sodp"].Visible = false;
			dgvTGList.AutoResizeColumns();
			TSSLab02.Text = dgvTGList.Rows.Count.ToString();
			tid = 0L;
			int num = 0;
			for (int j = 0; j < dgvTGList.Rows.Count; j++)
			{
				if (Convert.ToBoolean(dgvTGList.Rows[j].Cells["TR_Level"].Value))
				{
					dgvTGList.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(224, 85, 50);
					dgvTGList.Rows[j].DefaultCellStyle.ForeColor = Color.White;
				}
				else
				{
					tid += Convert.ToInt64(dgvTGList.Rows[j].Cells["gCount"].Value);
					num += Convert.ToInt32(dgvTGList.Rows[j].Cells["TR_cardcount"].Value);
				}
			}
			TSSLab04.Text = tid.ToString();
			TSSLab06.Text = num.ToString();
		}
		btnTGL.Select();
	}

	private void TSDDBtnGetTeam_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvTGList.DataSource != null && dgvTGList.Rows.Count > 0)
			{
				long num = Convert.ToInt64(dgvTGList["g_teamid", 0].Value);
				frmSTour frmSTour2 = new frmSTour();
				frmSTour2.StartPosition = FormStartPosition.CenterScreen;
				frmSTour2.m_initctrl = false;
				string text = "Select TB_name, team_name, team_guide, Team_cername, team_cernum, g_name,  cer_name, g_cernum, r_name, build_name, floor_name, TP_Name";
				text += ", g_cometime, cast(g_sototalday As Integer) As g_stayDay, g_stand_L_time, g_stayover, g_softime, g_soltime";
				text += ", g_sototalday, g_level, g_actual_l_time, g_level_card, g_othprice";
				text += " From v_TeamDetails Where 1 = 1 ";
				text = text + " And Team_Id = " + num;
				text += " Order by g_id desc";
				frmSTour2.m_extstr = text;
				frmSTour2.m_sum = (frmSTour2.m_pars = false);
				frmSTour2.Text = dgvTGList["team_name", 0].Value.ToString();
				frmSTour2.ShowDialog();
			}
		}
		catch
		{
		}
	}

	private void TSDDBtnRead_Click(object sender, EventArgs e)
	{
		try
		{
			btnChk.Checked = false;
			TextBox textBox = txtGuideCernum;
			TextBox textBox2 = txtTGN;
			TextBox textBox3 = txtGuide;
			string text = (txtGuideCer.Text = "");
			string text3 = (textBox3.Text = text);
			string text5 = (textBox2.Text = text3);
			textBox.Text = text5;
			dgvTGList.DataSource = null;
			ToolStripStatusLabel tSSLab = TSSLab04;
			string text7 = (TSSLab02.Text = "");
			tSSLab.Text = text7;
			object[] array = new object[256];
			string text9 = "";
			int num = Program.RadioReadCard(array, Buzzer: true, 0);
			if (num < 0)
			{
				return;
			}
			if (Convert.ToInt32(array[0]) != 6)
			{
				Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string[] array2 = new string[num - 3];
			for (int i = 3; i < num; i++)
			{
				array2[i - 3] = (string)array[i];
			}
			string text10 = "Select TB_name, team_name, team_guide, Team_cername, team_cernum, g_id, g_name,  cer_name, g_cernum, r_name, build_name, floor_name, TP_Name";
			text10 += ", g_cometime, g_stayHour As g_stayDay, g_stand_L_time, g_stayover, g_softime, g_soltime";
			text10 += ", g_logout, g_loss, g_sototalday, g_level, g_actual_l_time, g_level_card, r_price, TR_Bascurname, TR_Basrate";
			text10 += ", g_deposit, curr_code, curr_rate,  g_teamid";
			text10 += " From v_TeamDetails ";
			object obj = text10;
			text10 = string.Concat(obj, " Where b_code='", array2[0], "' And f_code='", array2[1], "' And r_code='", array2[2], "' And r_subcode=", array2[3], " And r_cardnum=", array[1], " And r_subDai=", array2[4]);
			DataTable dataTable = SQLserver.Data_GetDataTable(text10);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				text9 = (string)m_htab["dgvTB_name"] + "：" + dataTable.Rows[0]["TB_name"].ToString().Trim();
				string text11 = text9;
				text9 = text11 + "\r\n" + label58.Text + " " + dataTable.Rows[0]["team_name"].ToString().Trim();
				string text12 = text9;
				text9 = text12 + "\r\n" + (string)m_htab["dgvteam_guide"] + "：" + dataTable.Rows[0]["team_guide"].ToString().Trim();
				string text13 = text9;
				text9 = text13 + "\r\n" + label35.Text + " " + dataTable.Rows[0]["r_name"].ToString().Trim();
				string text14 = text9;
				text9 = text14 + "\r\n" + label36.Text + " " + dataTable.Rows[0]["Build_Name"].ToString().Trim();
				string text15 = text9;
				text9 = text15 + "\r\n" + label37.Text + " " + dataTable.Rows[0]["Floor_Name"].ToString().Trim();
				string text16 = text9;
				text9 = text16 + "\r\n" + label38.Text + " " + dataTable.Rows[0]["TP_Name"].ToString().Trim();
				string text17 = text9;
				text9 = text17 + "\r\n" + label40.Text + " " + dataTable.Rows[0]["g_name"].ToString().Trim();
				string text18 = text9;
				text9 = text18 + "\r\n" + (string)m_htab["label26"] + " " + dataTable.Rows[0]["cer_name"].ToString().Trim();
				string text19 = text9;
				text9 = text19 + "\r\n" + (string)m_htab["label27"] + " " + dataTable.Rows[0]["g_cernum"].ToString().Trim();
				text9 = text9 + "\r\n" + label41.Text + dataTable.Rows[0]["g_cometime"].ToString().Trim();
				_ = (bool)dataTable.Rows[0]["g_logout"];
				if ((bool)dataTable.Rows[0]["g_loss"])
				{
					text9 = text9 + "\r\n\r\n" + (string)Program.m_hPubTab["cardinfoLost"];
					Program.MsgBox(text9 + "\r\n" + (string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if ((bool)dataTable.Rows[0]["g_level"])
				{
					text9 = text9 + "\r\n" + label29.Text + dataTable.Rows[0]["g_actual_l_time"].ToString().Trim();
					text9 = text9 + "\r\n\r\n" + (string)m_htab["Info08"];
					Program.MsgBox(text9 + "\r\n" + (string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				long tid = Convert.ToInt64(dataTable.Rows[0]["g_teamid"]);
				txtTGN.Text = dataTable.Rows[0]["team_name"].ToString().Trim();
				txtGuide.Text = dataTable.Rows[0]["team_guide"].ToString().Trim();
				TextBox textBox4 = txtGuideCer;
				string text20 = (txtGuideCernum.Text = dataTable.Rows[0]["team_cernum"].ToString().Trim());
				textBox4.Text = text20;
				InitDgvTGList(tid);
				dataTable.Clear();
			}
			else
			{
				Program.MsgBox((string)Program.m_hPubTab["cardinfoDNull"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnTS_Click(object sender, EventArgs e)
	{
		try
		{
			btnChk.Checked = false;
			TextBox textBox = txtGuideCernum;
			TextBox textBox2 = txtTGN;
			TextBox textBox3 = txtGuide;
			string text = (txtGuideCer.Text = "");
			string text3 = (textBox3.Text = text);
			string text5 = (textBox2.Text = text3);
			textBox.Text = text5;
			dgvTGList.DataSource = null;
			ToolStripStatusLabel tSSLab = TSSLab04;
			string text7 = (TSSLab02.Text = "");
			tSSLab.Text = text7;
			frmSTB frmSTB2 = new frmSTB();
			frmSTB2.StartPosition = FormStartPosition.CenterScreen;
			frmSTB2.clsBackPanel1.Visible = true;
			frmSTB2.m_sqlstr = "And Team_leveltime is null and team_sch = 0 ";
			frmSTB2.Text = "";
			LockSoftware.Controls.GlassBtn btnClose = frmSTB2.btnClose;
			Label label = frmSTB2.label5;
			Label label2 = frmSTB2.label29;
			DateTimePicker dtpLevelS = frmSTB2.dtpLevelS;
			bool flag = (frmSTB2.dtpLevelE.Visible = false);
			bool flag3 = (dtpLevelS.Visible = flag);
			bool flag5 = (label2.Visible = flag3);
			bool visible = (label.Visible = flag5);
			btnClose.Visible = visible;
			if (frmSTB2.ShowDialog() != DialogResult.Cancel)
			{
				long tid = frmSTB2.m_tid;
				string text9 = frmSTB2.Text;
				txtTGN.Text = text9;
				txtGuide.Text = frmSTB2.m_guide;
				TextBox textBox4 = txtGuideCer;
				string text10 = (txtGuideCernum.Text = frmSTB2.m_gcer);
				textBox4.Text = text10;
				InitDgvTGList(tid);
				btnChk_Click(null, null);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom(ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void toolsBtn2_Click(object sender, EventArgs e)
	{
		if (clsBackPanel2.Visible)
		{
			toolsBtn2.ImageNew = Resources.mini_top;
			clsBackPanel2.Visible = false;
		}
		else
		{
			clsBackPanel2.Visible = true;
			toolsBtn2.ImageNew = Resources.mini_bottom;
		}
	}

	private void btnTGIn_Click(object sender, EventArgs e)
	{
		if (!(ActivePanel.Name == plR3.Name))
		{
			ActivePanel = plR3;
			btnTGIn.Dock = DockStyle.Top;
			btnRGLevel.SendToBack();
			btnRGLevel.Dock = DockStyle.Top;
			btnRInfo.SendToBack();
			btnRInfo.Dock = DockStyle.Top;
			plR1.SendToBack();
			plR2.SendToBack();
			plR3.BringToFront();
			plR3.Dock = DockStyle.Fill;
		}
	}

	private void btnTGO_Click(object sender, EventArgs e)
	{
		frmTeam frmTeam2 = new frmTeam();
		if (Program.fm.OpenFrm(frmTeam2))
		{
			frmTeam2.MdiParent = Program.fm;
			frmTeam2.Show();
		}
	}

	private void btnChk_Click(object sender, EventArgs e)
	{
		if (txtGuideCer.Text.Trim() == txtGuideCernum.Text.Trim())
		{
			btnChk.Checked = true;
			return;
		}
		Program.MsgCustom((string)m_htab["Info16"], MessageBoxIcon.Asterisk);
		txtGuideCer.Select();
		btnChk.Checked = false;
	}

	private void txtGuideCer_TextChanged(object sender, EventArgs e)
	{
		btnChk.Checked = false;
	}

	private void btnTGL_Click(object sender, EventArgs e)
	{
		frmBill frmBill2 = new frmBill();
		try
		{
			if (!btnChk.Checked)
			{
				Program.MsgCustom((string)m_htab["Info18"], MessageBoxIcon.Asterisk);
				btnChk.Select();
				return;
			}
			if (Program.isValNull(label82.Text.Substring(0, label82.Text.Length - 1), txtGuideCer.Text, chk: true))
			{
				txtGuideCer.Select();
				return;
			}
			if (txtGuideCer.Text.Trim() != txtGuideCernum.Text.Trim())
			{
				Program.MsgCustom((string)m_htab["Info16"], MessageBoxIcon.Asterisk);
				txtGuideCer.Select();
				return;
			}
			if (dgvTGList.DataSource == null || dgvTGList.Rows.Count <= 0)
			{
				Program.MsgCustom((string)m_htab["Info13"], MessageBoxIcon.Asterisk);
				return;
			}
			string text = label58.Text + " " + txtTGN.Text.Trim();
			string text2 = text;
			text = text2 + "\r\n" + label81.Text + " " + txtGuide.Text.Trim();
			string text3 = text;
			text = text3 + "\r\n" + label82.Text + " " + txtGuideCer.Text.Trim();
			string text4 = text;
			text = text4 + "\r\n" + TSSLab01.Text + " " + TSSLab02.Text;
			string text5 = text;
			text = text5 + "\r\n" + TSSLab03.Text + " " + TSSLab04.Text;
			string text6 = text;
			text = text6 + "\r\n" + TSSLab05.Text + " " + TSSLab06.Text;
			long num = Convert.ToInt64(dgvTGList.Rows[0].Cells["g_teamid"].Value.ToString());
			string sql = "Select * From T_Team Where Team_id=" + num;
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count != 1)
			{
				Program.MsgCustom((string)m_htab["Info19"], MessageBoxIcon.Asterisk);
				return;
			}
			string text7 = text;
			text = text7 + "\r\n" + label41.Text + " " + dataTable.Rows[0]["team_cometime"].ToString();
			string text8 = text;
			text = text8 + "\r\n" + label42.Text + " " + Convert.ToInt32(dataTable.Rows[0]["team_stayHour"]);
			string text9 = text;
			text = text9 + "\r\n" + label43.Text + " " + dataTable.Rows[0]["team_stand_L_time"].ToString();
			string text10 = text;
			text = text10 + "\r\n" + label44.Text + " " + dataTable.Rows[0]["team_roomprice"].ToString();
			string text11 = text;
			text = text11 + "\r\n" + label45.Text + " " + dataTable.Rows[0]["team_deposit"].ToString();
			text = text + "\r\n" + (string)m_htab["Info17"];
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			string sql2 = "select sum(Tr_sodp),sum(Tr_mustpay)  from t_rooms where team_id=" + num;
			num6 = Convert.ToDouble(SQLserver.Data_GetDataTable(sql2).Rows[0][0]);
			num7 = Convert.ToDouble(SQLserver.Data_GetDataTable(sql2).Rows[0][1]);
			num9 = Program.GetRealDisValue((Convert.ToDouble(dataTable.Rows[0]["team_discount"]) * 100.0).ToString());
			double num10 = Convert.ToDouble(dgvTGList.Rows[0].Cells["curr_rate"].Value);
			string text12 = dgvTGList.Rows[0].Cells["curr_code"].Value.ToString();
			string text13 = "";
			num3 = Convert.ToDouble(dataTable.Rows[0]["team_totalpaid"]);
			DateTime now = DateTime.Now;
			Dictionary<object, double> dictionary = new Dictionary<object, double>();
			dictionary.Add(0, 0.0);
			for (int i = 0; i < dgvTGList.Rows.Count; i++)
			{
				text = "";
				DateTime dtComeTime = Convert.ToDateTime(dgvTGList.Rows[i].Cells["TR_cometime"].Value);
				text = dgvTGList.Rows[i].Cells["r_name"].Value.ToString().Trim();
				if (!Convert.ToBoolean(dgvTGList.Rows[i].Cells["TR_Level"].Value))
				{
					num8 = Program.CountDay(dtComeTime, now);
					dictionary.Add(dgvTGList.Rows[i].Cells["tr_id"].Value, num8);
					dictionary[0] = ((dictionary[0] < num8) ? num8 : dictionary[0]);
					num9 = Program.GetRealDisValue((Convert.ToDouble(dgvTGList.Rows[i].Cells["TR_discount"].Value) * 100.0).ToString());
					dgvTGList.Rows[i].Cells["TR_stayhour"].Value = num8;
					dgvTGList.Rows[i].Cells["TR_roomprice"].Value = num8 * Convert.ToDouble(dgvTGList.Rows[i].Cells["TP_Price"].Value) * num9;
					num2 += Convert.ToDouble(dgvTGList.Rows[i].Cells["TR_roomprice"].Value);
					string sql3 = string.Concat("select g_memo from t_guest a,t_rooms b where a.tr_id=b.tr_id and b.tr_id=", dgvTGList.Rows[i].Cells["TR_ID"].Value, " order by a.g_id asc");
					DataTable dataTable2 = SQLserver.Data_GetDataTable(sql3);
					string text14 = dataTable2.Rows[0]["g_memo"].ToString().Trim().Replace("->", ",");
					text13 += ((text14.Split(',').Length > 1) ? (text14 + "\n") : "");
				}
			}
			sql = "Select IsNull(Sum(othp_apaid),0) As OthPrice From T_Otherpaid Where team_id = " + num + "and a_id=-1";
			DataTable dataTable3 = SQLserver.Data_GetDataTable(sql);
			if (dataTable3 == null)
			{
				Program.MsgCusErrMess("Null", TSMISubOth.Text.Trim());
				return;
			}
			if (dataTable3.Rows.Count > 0)
			{
				num4 = Convert.ToDouble(dataTable3.Rows[0]["OthPrice"]);
			}
			dataTable3.Clear();
			dataTable3.Dispose();
			num5 = num3 * num10 - num2 - num4 - num7 - num6;
			frmBill2.m_LeaveTime = now;
			frmBill2.labMsg.Text = (string)m_htab["Info29"];
			frmBill2.m_team = true;
			frmBill2.m_Total = num2 + num4 + num7 + num6;
			frmBill2.m_Deposit = num3;
			frmBill2.m_Paid = 0.0;
			frmBill2.m_Change = num5;
			frmBill2.m_Rate = num10;
			frmBill2.Extrapay = num6;
			frmBill2.houses = text13;
			frmBill2.houseids = "";
			frmBill2.txt01.Text = Program.m_baseCurrCode;
			frmBill2.txt02.Text = text12;
			frmBill2.txt03.Text = Program.m_baseCurrCode;
			frmBill2.txt04.Text = text12;
			frmBill2.txtTotal.Text = frmBill2.m_Total.ToString("F2");
			frmBill2.txtDep.Text = num3.ToString("F2");
			frmBill2.txtPaid.Text = Program.GetLocDecStr("0.0");
			frmBill2.txtChange.Text = num5.ToString("F2");
			frmBill2.m_trid = Convert.ToInt32(dataTable.Rows[0]["team_id"]);
			frmBill2.m_gid = Convert.ToInt32(dataTable.Rows[0]["team_id"]);
			frmBill2.m_chkIn = dataTable.Rows[0]["team_cometime"].ToString();
			frmBill2.m_chkOut = DateTime.Now.ToString();
			frmBill2.m_FactDay = 0.0;
			frmBill2.m_ChangeRoom = TSMIRCh.Text;
			frmBill2.m_OtherPaid = 0.0;
			frmBill2.guestsName.Add(dgvTGList.Rows[0].Cells["team_guide"].Value.ToString());
			frmBill2.guestsInfoDT.Rows.Add(frmBill2.m_gid, dgvTGList.Rows[0].Cells["team_guide"].Value);
			if (frmBill2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			num5 = frmBill2.m_Change;
			num3 = frmBill2.m_Paid + frmBill2.m_Deposit;
			sql = "Update D_Rooms Set R_RSID = " + (Program.m_defLS + 1).ToString() + ", R_CurGuestCount = 0, R_SubCodeDai= 0,R_MaxCardNum = R_MaxCardNum+1,R_Updatetime=GetDate(),R_Updator_ID=" + Program.m_opid + ", R_Updator=N'" + Program.m_OperName + "' Where r_id in (select r_id From T_Rooms Where team_id=" + num + " And TR_Level=0) \n ";
			object obj;
			for (int j = 0; j < dgvTGList.Rows.Count; j++)
			{
				if (!Convert.ToBoolean(dgvTGList.Rows[j].Cells["TR_Level"].Value))
				{
					obj = sql;
					sql = string.Concat(obj, "Update T_Rooms Set  TR_Level=1, TR_actual_l_time=GetDate(), a_id = isnull(a_id,0)+", Program.GetStandDec(num8 * 2.0), ", TR_mustpay = TR_mustpay + ", Program.GetStandDec(Convert.ToDouble(dgvTGList.Rows[j].Cells["Tr_sodp"].Value) + Convert.ToDouble(dgvTGList.Rows[j].Cells["TR_roomprice"].Value)), ",TR_getchange = 0, Updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "' Where TR_id=", dgvTGList.Rows[j].Cells["TR_ID"].Value.ToString(), " And TR_Level=0 \n ");
				}
			}
			string text15 = sql;
			sql = text15 + "Update T_Team Set team_act_sh = " + Program.GetStandDec(dictionary[0] * 2.0) + ", team_roomprice = " + Program.GetStandDec(num2 + num6 + num7) + ", team_totalprice=" + Program.GetStandDec(frmBill2.m_Total);
			obj = sql;
			sql = string.Concat(obj, ", team_totalpaid=", Program.GetStandDec(num3), ", team_getchange=", Program.GetStandDec(num5), ", team_leveltime = GetDate(), updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "' Where team_id=", num, " \n ");
			foreach (KeyValuePair<object, double> item in dictionary)
			{
				if (Convert.ToInt64(item.Key) != 0)
				{
					obj = sql;
					sql = string.Concat(obj, "Update T_Guest Set g_level=1, a_id=isnull(a_id,0)+", Program.GetStandDec(item.Value * 2.0), ", g_actual_l_time = getdate(), g_level_card = 0,LevelCreator_id=", Program.m_opid, ", LevelCreator=N'", Program.m_OperName, "', Updator = N'", Program.m_OperName, "', Updator_id =", Program.m_opid, ", UpdateTime = GetDate()  Where  g_teamid=", num, " And tr_id=", item.Key, "and g_level=0\n ");
				}
			}
			if (Program.DBCompExec(sql, btnTGL.Text) < 0)
			{
				Program.MsgCustom((string)m_htab["Info07"], MessageBoxIcon.Hand);
				return;
			}
			try
			{
				if (frmBill2.chkPB.Checked)
				{
					frmBill2.rptbill.PrintDialog();
				}
			}
			catch
			{
			}
			for (int k = 0; k < dgvTGList.Rows.Count; k++)
			{
				refresh_room(dgvTGList.Rows[k].Cells["R_Name"].Value.ToString().Trim(), Program.m_defLS, 0);
			}
			dgvTGList.DataSource = null;
			text = string.Format((string)m_htab["Info25"], txtTGN.Text);
			txtTGN.Text = "";
			ToolStripStatusLabel tSSLab = TSSLab02;
			ToolStripStatusLabel tSSLab2 = TSSLab04;
			text2 = (TSSLab06.Text = "");
			text2 = (tSSLab2.Text = text2);
			tSSLab.Text = text2;
			btnChk.Checked = false;
			Program.MsgCustom(text, MessageBoxIcon.Asterisk);
			InitRoomList(tvList.SelectedNode, getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnTGL.Text);
		}
		finally
		{
			if (frmBill2 != null && !frmBill2.IsDisposed)
			{
				frmBill2.rptbill.Dispose();
				frmBill2.Dispose();
			}
		}
	}

	private void btnTGSO_Click(object sender, EventArgs e)
	{
		try
		{
			if (!btnChk.Checked)
			{
				Program.MsgCustom((string)m_htab["Info18"], MessageBoxIcon.Asterisk);
				btnChk.Select();
				return;
			}
			if (Program.isValNull(label82.Text.Substring(0, label82.Text.Length - 1), txtGuideCer.Text, chk: true))
			{
				txtGuideCer.Select();
				return;
			}
			if (txtGuideCer.Text.Trim() != txtGuideCernum.Text.Trim())
			{
				Program.MsgCustom((string)m_htab["Info16"], MessageBoxIcon.Asterisk);
				txtGuideCer.Select();
				return;
			}
			if (dgvTGList.DataSource == null || dgvTGList.Rows.Count <= 0)
			{
				Program.MsgCustom((string)m_htab["Info13"], MessageBoxIcon.Asterisk);
				return;
			}
			string text = label58.Text + " " + txtTGN.Text.Trim();
			string text2 = text;
			text = text2 + "\r\n" + label81.Text + " " + txtGuide.Text.Trim();
			string text3 = text;
			text = text3 + "\r\n" + label82.Text + " " + txtGuideCer.Text.Trim();
			string text4 = text;
			text = text4 + "\r\n" + TSSLab01.Text + " " + TSSLab02.Text;
			string text5 = text;
			text = text5 + "\r\n" + TSSLab03.Text + " " + TSSLab04.Text;
			string text6 = text;
			text = text6 + "\r\n" + TSSLab05.Text + " " + TSSLab06.Text;
			long num = Convert.ToInt64(dgvTGList.Rows[0].Cells["g_teamid"].Value.ToString());
			string sql = "Select team_leveltime,team_cometime,team_stayHour,team_stand_L_time,team_totalpaid From T_Team Where Team_id=" + num;
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count != 1)
			{
				Program.MsgCustom((string)m_htab["Info19"], MessageBoxIcon.Asterisk);
				return;
			}
			string locDTime = Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["team_stand_L_time"].ToString()), "00");
			string text7 = text;
			text = text7 + "\r\n" + label41.Text + " " + Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["team_cometime"].ToString()), "00");
			string text8 = text;
			text = text8 + "\r\n" + label42.Text + " " + Convert.ToInt32(dataTable.Rows[0]["team_stayHour"]);
			string text9 = text;
			text = text9 + "\r\n" + label43.Text + " " + locDTime;
			double num2 = Convert.ToDouble(dgvTGList.Rows[0].Cells["curr_rate"].Value);
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = Convert.ToDouble(dataTable.Rows[0]["team_totalpaid"]);
			string text10 = text;
			text = text10 + "\r\n" + label45.Text + " " + num5.ToString("F2") + " " + dgvTGList.Rows[0].Cells["curr_code"].Value.ToString();
			if (dataTable.Rows[0]["team_leveltime"].ToString() != "")
			{
				Program.MsgBox((string)m_htab["Info20"] + "\r\n" + (string)m_htab["label29"] + " " + dataTable.Rows[0]["team_leveltime"].ToString(), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			locDTime = Program.GetLocDTime(Convert.ToDateTime(locDTime).AddDays(1.0), "00");
			frmGCSO frmGCSO2 = new frmGCSO();
			frmGCSO2.label1.Text = (string)m_htab["label28"];
			frmGCSO2.label2.Text = (string)m_htab["label29"];
			frmGCSO2.label3.Text = (string)m_htab["Info11"];
			frmGCSO2.label4.Text = (string)m_htab["Info10"];
			frmGCSO2.Text = btnTGSO.Text;
			frmGCSO2.txtMsg.Text = text;
			DateTimePicker dateTimePicker = frmGCSO2.dtpLevel;
			DateTime value = (frmGCSO2.dtpTime.Value = Convert.ToDateTime(locDTime));
			dateTimePicker.Value = value;
			frmGCSO2.m_oldLT = dataTable.Rows[0]["team_stand_l_time"].ToString();
			if (frmGCSO2.ShowDialog() == DialogResult.Cancel)
			{
				frmGCSO2.Dispose();
				return;
			}
			locDTime = Program.GetStandDate(frmGCSO2.dtpLevel.Value) + " " + frmGCSO2.dtpTime.Value.ToString("HH:mm:ss");
			double num6;
			try
			{
				num6 = Convert.ToDouble(frmGCSO2.textBox1.Text);
				if (num6 > 200.0)
				{
					frmGCSO2.Dispose();
					Program.MsgBox((string)m_htab["Info30"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message.ToString());
				Program.MsgBox((string)m_htab["Info31"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			frmGCSO2.Dispose();
			sql = "Update T_Rooms Set  TR_stayover=1, TR_SOLTime='" + locDTime + "', TR_SOhour = 0";
			sql = sql + ", TR_stayhour= TR_stayhour+" + Program.GetStandDec(num6);
			sql += ", TR_SOrp = r_price";
			object obj = sql;
			sql = string.Concat(obj, ", Updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "' Where team_id=", num.ToString(), " And TR_level=0 \n ");
			object obj2 = sql;
			sql = string.Concat(obj2, "Update T_Guest Set g_stayover=1, g_softime = (Case g_stayover When 1 Then g_softime Else GetDate() End), g_soltime =Getdate(),g_SOTotalDay= g_SOTotalDay + ", Program.GetStandDec(num6), ",SOCreator_id=", Program.m_opid, ", SOCreator=N'", Program.m_OperName, "'");
			sql = sql + ", g_stand_L_time='" + locDTime + "'";
			object obj3 = sql;
			sql = string.Concat(obj3, ", Updator = N'", Program.m_OperName, "', Updator_id =", Program.m_opid, ", UpdateTime = GetDate() Where g_teamid =", num.ToString(), " And g_level=0 \n ");
			object obj4 = sql;
			sql = string.Concat(obj4, "Update D_Rooms Set R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "'");
			sql = sql + ", R_TotalPrice=Isnull(R_TotalPrice,0) + (Select TP_Price From D_RoomType Where TP_ID = D_Rooms.R_TypeID) *" + Program.GetStandDec(num6);
			sql = sql + " Where R_ID in (Select r_id from T_Rooms Where team_id = " + num + ") \n ";
			sql += "Declare @_RP As Numeric(18,2) \n ";
			sql += "Declare @_DP As Numeric(18,2) \n ";
			string text11 = sql;
			sql = text11 + "Select @_RP = Sum(TR_RoomPrice), @_DP = Sum(r_price * " + Program.GetStandDec(num6) + ") From T_Rooms Where team_id=" + num + " \n ";
			object obj5 = sql;
			sql = string.Concat(obj5, "Update T_Team Set team_deposit = team_deposit + ", 0, ",team_stayHour=team_stayHour+", Program.GetStandDec(num6), ", team_stand_L_time = '", locDTime, "'");
			object obj6 = sql;
			sql = string.Concat(obj6, ", Updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "'");
			sql = sql + " Where team_id=" + num;
			if (Program.DBCompExec(sql, btnGCSO.Text) < 0)
			{
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			for (int i = 0; i < dgvTGList.Rows.Count; i++)
			{
				if (!Convert.ToBoolean(dgvTGList.Rows[i].Cells["TR_Level"].Value))
				{
					refresh_room(dgvTGList.Rows[i].Cells["R_Name"].Value.ToString().Trim(), 5, Convert.ToInt32(dgvTGList.Rows[i].Cells["gCount"].Value));
				}
			}
			InitDgvTGList(num);
			Program.MsgCustom((string)m_htab["Info22"], MessageBoxIcon.Asterisk);
			frmCardRewrite frmCardRewrite2 = new frmCardRewrite();
			frmCardRewrite2.m_tmpID = num;
			frmCardRewrite2.m_rtype = 1;
			frmCardRewrite2.btnTxt = txtTGN.Text.Trim();
			frmCardRewrite2.Text = TSMITCard.Text;
			frmCardRewrite2.ShowDialog();
			dgvTGList.DataSource = null;
			txtTGN.Text = "";
			ToolStripStatusLabel tSSLab = TSSLab02;
			ToolStripStatusLabel tSSLab2 = TSSLab04;
			string text12 = (TSSLab06.Text = "");
			string text14 = (tSSLab2.Text = text12);
			tSSLab.Text = text14;
			btnChk.Checked = false;
		}
		catch (Exception ex2)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		try
		{
			TextBox textBox = txtLRn;
			string text = (txtRn.Text = "");
			textBox.Text = text;
			Label label = label14;
			Label label2 = label15;
			Label label3 = label16;
			TextBox textBox2 = txtRMemo;
			Label label4 = label18;
			Label label5 = label19;
			string text3 = (label20.Text = "");
			string text5 = (label5.Text = text3);
			string text7 = (label4.Text = text5);
			string text9 = (textBox2.Text = text7);
			string text11 = (label3.Text = text9);
			string text13 = (label2.Text = text11);
			label.Text = text13;
			Label label6 = label21;
			Label label7 = label22;
			Label label8 = label23;
			string text15 = (label24.Text = "");
			string text17 = (label8.Text = text15);
			string text19 = (label7.Text = text17);
			label6.Text = text19;
			Label label9 = label88;
			string text21 = (txtGn.Text = "");
			label9.Text = text21;
			for (int i = 0; i < 11; i++)
			{
				tlpR2.Controls["label" + (i + 47)].Text = "";
			}
			label90.Text = "";
			if (!e.IsSelected)
			{
				dgvList.ContextMenuStrip = null;
				return;
			}
			label25.Text = "";
			if (e.ItemIndex < 0 || e.Item == null)
			{
				dgvList.ContextMenuStrip = null;
				return;
			}
			m_SelectItem = e.Item;
			pictureBox1.Image = imgRoom.Images[m_SelectItem.ImageIndex];
			int num = Convert.ToInt32(m_SelectItem.SubItems[6].Text.Trim());
			if (num == 1)
			{
				if (txtCurRn.Text.Trim() != m_SelectItem.Text.Trim())
				{
					txtTGRn.Text = m_SelectItem.Text.Trim();
				}
			}
			else
			{
				txtTGRn.Text = "";
			}
			int index = int.Parse(m_SelectItem.SubItems[6].Text.Trim());
			txtRn.Text = m_SelectItem.Text.Trim();
			label25.Text = m_SelectItem.Text.Trim();
			label24.Text = m_SelectItem.SubItems[11].Text.Trim();
			label23.Text = m_SelectItem.SubItems[12].Text.Trim();
			label22.Text = m_SelectItem.SubItems[13].Text.Trim();
			label21.Text = ((DataRowView)cobStatus.Items[index]).Row.ItemArray[1].ToString();
			label20.Text = m_SelectItem.SubItems[7].Text.Trim();
			label19.Text = m_SelectItem.SubItems[8].Text.Trim();
			label18.Text = m_SelectItem.SubItems[9].Text.Trim();
			txtRMemo.Text = m_SelectItem.SubItems[10].Text.Trim();
			label16.Text = m_SelectItem.SubItems[14].Text.Trim();
			label15.Text = m_SelectItem.SubItems[15].Text.Trim();
			label14.Text = m_SelectItem.SubItems[16].Text.Trim();
			int num2 = Convert.ToInt16("0" + m_SelectItem.SubItems[14].Text.Trim());
			if (num2 > 0)
			{
				txtLRn.Text = m_SelectItem.Text.Trim();
				label47.Text = label24.Text;
				label48.Text = label23.Text;
				label49.Text = label22.Text;
				label50.Text = num2.ToString();
				btnLN.Enabled = true;
			}
			else
			{
				txtLRn.Text = "";
				btnLN.Enabled = false;
			}
			cobCer.SelectedIndex = 0;
			txtCernum.Clear();
			chkRepl.Checked = false;
			cobCurrency.SelectedIndex = cobCurrency.FindStringExact(Program.m_baseCurrCode);
			switch (num)
			{
			case 3:
			case 11:
				setstation(t: false);
				ScheduleChexkIn();
				break;
			case 1:
			case 4:
			case 5:
			case 6:
			case 10:
				setstation(t: true);
				GetContinueInfo();
				break;
			default:
				setstation(t: false);
				break;
			}
		}
		catch (Exception ex)
		{
			Console.Write(ex.StackTrace.ToString());
		}
	}

	private void dgvList_MouseClick(object sender, MouseEventArgs e)
	{
		try
		{
			if (e.Button != MouseButtons.Right || dgvList.SelectedItems == null)
			{
				return;
			}
			ToolStripMenuItem tSMISubOth = TSMISubOth;
			ToolStripMenuItem tSMIRCh = TSMIRCh;
			ToolStripMenuItem tSMIRCard = TSMIRCard;
			ToolStripMenuItem tSMITCard = TSMITCard;
			bool flag = (TSMIEBR.Enabled = false);
			bool flag3 = (tSMITCard.Enabled = flag);
			bool flag5 = (tSMIRCard.Enabled = flag3);
			bool enabled = (tSMIRCh.Enabled = flag5);
			tSMISubOth.Enabled = enabled;
			ListViewItem listViewItem = dgvList.Items[dgvList.SelectedIndices[0]];
			TSMIRName.Text = listViewItem.Text.Trim();
			int num = Convert.ToInt32(listViewItem.SubItems[6].Text);
			TSMIRName.Image = imgRoom.Images[num - 1];
			if (num > 3 && num < 7)
			{
				ToolStripMenuItem tSMISubOth2 = TSMISubOth;
				ToolStripMenuItem tSMIRCard2 = TSMIRCard;
				bool flag8 = (TSMIRCh.Enabled = true);
				bool enabled2 = (tSMIRCard2.Enabled = flag8);
				tSMISubOth2.Enabled = enabled2;
			}
			if (num == 6)
			{
				ToolStripMenuItem tSMITCard2 = TSMITCard;
				bool enabled3 = (TSMIRCh.Enabled = true);
				tSMITCard2.Enabled = enabled3;
			}
			if (num == 1)
			{
				TSMIEBR.Enabled = true;
			}
			if (num == 1 || num == 2)
			{
				TSMIRSCh.Enabled = true;
				for (int i = 0; i < TSMIRSCh.DropDownItems.Count; i++)
				{
					TSMIRSCh.DropDownItems[i].Enabled = true;
				}
				TSMIRSCh.DropDownItems[num - 1].Enabled = false;
			}
			else
			{
				TSMIRSCh.Enabled = false;
			}
			dgvList.ContextMenuStrip = cMSRoom;
			Type type = dgvList.GetType();
			MethodInfo method = type.GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
			method.Invoke(dgvList, null);
		}
		catch
		{
		}
	}

	private void TSMIRCard_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.SelectedItems != null)
			{
				ListViewItem listViewItem = dgvList.Items[dgvList.SelectedIndices[0]];
				frmCardRewrite frmCardRewrite2 = new frmCardRewrite();
				frmCardRewrite2.Text = TSMIRCard.Text;
				Convert.ToInt64(listViewItem.SubItems[1].Text.Trim());
				string sql = "Select Top 1 TR_ID, team_id From T_Rooms Where TR_Level = 0 And r_name = N'" + listViewItem.Text + "' Order by TR_ID Desc";
				DataTable dataTable = SQLserver.Data_GetDataTable(sql);
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					frmCardRewrite2.m_tmpID = Convert.ToInt64(dataTable.Rows[0]["TR_ID"].ToString());
					frmCardRewrite2.m_rtype = 0;
					frmCardRewrite2.btnTxt = listViewItem.Text;
					frmCardRewrite2.ShowDialog();
				}
			}
		}
		catch
		{
		}
	}

	private void TSMITCard_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.SelectedItems != null)
			{
				ListViewItem listViewItem = dgvList.Items[dgvList.SelectedIndices[0]];
				frmCardRewrite frmCardRewrite2 = new frmCardRewrite();
				frmCardRewrite2.Text = TSMITCard.Text;
				Convert.ToInt64(listViewItem.SubItems[1].Text.Trim());
				string sql = "Select Top 1 TR_ID, team_id From T_Rooms Where TR_Level = 0 And r_name = N'" + listViewItem.Text + "' Order by TR_ID Desc";
				DataTable dataTable = SQLserver.Data_GetDataTable(sql);
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					frmCardRewrite2.m_tmpID = Convert.ToInt64(dataTable.Rows[0]["team_id"].ToString());
					frmCardRewrite2.m_rtype = 1;
					frmCardRewrite2.btnTxt = listViewItem.Text;
					frmCardRewrite2.ShowDialog();
				}
			}
		}
		catch
		{
		}
	}

	private void TSMISubRLog_Click(object sender, EventArgs e)
	{
		try
		{
			frmSRoom frmSRoom2 = new frmSRoom();
			frmSRoom2.StartPosition = FormStartPosition.CenterScreen;
			frmSRoom2.m_tmpVal = TSMIRName.Text;
			frmSRoom2.ShowDialog();
		}
		catch
		{
		}
	}

	private void TSMISubGLog_Click(object sender, EventArgs e)
	{
		try
		{
			frmSGuest frmSGuest2 = new frmSGuest();
			frmSGuest2.StartPosition = FormStartPosition.CenterScreen;
			frmSGuest2.m_tmpVal = TSMIRName.Text;
			frmSGuest2.ShowDialog();
		}
		catch
		{
		}
	}

	public void btnRefresh_Click(object sender, EventArgs e)
	{
		btnRInfo_Click(null, null);
		InitTreeList();
		InitType();
		InitStatus();
		InitCerType();
		InitCurrency();
		InitRImage(st: false);
		try
		{
			TSMIRCard.Text = (string)m_htab["TSMIRCard"];
			TSMITCard.Text = (string)m_htab["TSMITCard"];
			txtRn.Text.Trim();
			if (txtSRn.Text.Length == 0)
			{
				txtSRn.Text = (string)m_htab["txtSRn"];
			}
			nudDay.Value = nudDay.Minimum;
			dtpTime.Value = Convert.ToDateTime(Program.m_defLeaveTime + ":00");
			dtpCome.Value = DateTime.Now;
			cobCurrency.Text = Program.m_baseCurrCode;
		}
		catch
		{
		}
		InitRoomList(tvList.SelectedNode, getSqlStr());
	}

	public void refreshRoomList()
	{
		nudDay.Value = nudDay.Minimum;
		btnRefresh_Click(null, null);
	}

	private void dgvTGList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		try
		{
			for (int i = 0; i < dgvTGList.Rows.Count; i++)
			{
				if (Convert.ToBoolean(dgvTGList.Rows[i].Cells["TR_Level"].Value))
				{
					dgvTGList.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(224, 85, 50);
					dgvTGList.Rows[i].DefaultCellStyle.ForeColor = Color.White;
				}
			}
		}
		catch
		{
		}
	}

	private void TSMIRCh_Click(object sender, EventArgs e)
	{
		try
		{
			clsBackPanel13.Visible = true;
			clsPlRt.Enabled = false;
			txtCurRn.Text = TSMIRName.Text;
		}
		catch
		{
			clsPlRt.Enabled = true;
		}
	}

	private void btnClCh_Click(object sender, EventArgs e)
	{
		TextBox textBox = txtCurRn;
		string text = (txtTGRn.Text = "");
		textBox.Text = text;
		clsBackPanel13.Visible = false;
		clsPlRt.Enabled = true;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (Program.isValNull(label84.Text.Substring(0, label84.Text.Trim().Length - 1), txtCurRn.Text.Trim(), chk: true) || Program.isValNull(label85.Text.Substring(0, label85.Text.Trim().Length - 1), txtTGRn.Text.Trim(), chk: true))
			{
				return;
			}
			string text = txtCurRn.Text.Trim();
			string text2 = txtTGRn.Text.Trim();
			string sql = "Select Top 1 *,isnull(team_id,-1) as teamid,isnull(p_typeid,-1) as ptype,(Case TR_stayover When 1 then TR_SOLTime Else TR_stand_L_time End) As CurLT From v_Room Where R_Name = N'" + text + "' And TR_Level = 0 Order by TR_ID Desc";
			string sql2 = "select tr_cometime,isnull(a_id,0)/2.0 as a_id,g_actual_s_hour from v_CardGuest Where R_Name = N'" + text + "' And TR_Level = 0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			DataTable dataTable2 = SQLserver.Data_GetDataTable(sql2);
			dataTable.Rows[0]["tr_cometime"] = ((DateTime.Parse(dataTable2.Rows[0]["tr_cometime"].ToString()) > DateTime.Parse(dataTable.Rows[0]["tr_cometime"].ToString())) ? dataTable2.Rows[0]["tr_cometime"] : dataTable.Rows[0]["tr_cometime"]);
			bool isfordis = false;
			int num = 0;
			if (Convert.ToDouble(dataTable2.Rows[0]["a_id"]) >= (double)Convert.ToInt32(Program.m_defDay) || Convert.ToDouble(dataTable2.Rows[0]["g_actual_s_hour"]) >= (double)Program.m_defHR)
			{
				isfordis = true;
			}
			num = Convert.ToInt32(dataTable2.Rows[0]["g_actual_s_hour"]);
			dataTable2.Dispose();
			if (dataTable == null || dataTable.Rows.Count < 1)
			{
				Program.MsgCustom(text + "\r\n" + (string)m_htab["Err05"], MessageBoxIcon.Hand);
				return;
			}
			string text3 = dataTable.Rows[0]["tr_bascurname"].ToString();
			string text4 = dataTable.Rows[0]["curr_code"].ToString().Trim();
			double num2 = Convert.ToDouble(dataTable.Rows[0]["curr_rate"]);
			double stayday = Convert.ToDouble(dataTable.Rows[0]["TR_stayhour"]);
			int stayhour = Convert.ToInt32(dataTable.Rows[0]["Tr_sohour"]);
			double num3 = 0.0;
			num3 = Convert.ToDouble(dataTable.Rows[0]["TR_discount"]);
			bool flag = false;
			if (dataTable.Rows[0]["r_price"].Equals(dataTable.Rows[0]["TP_PricelessHour"]))
			{
				flag = true;
			}
			sql = "Select * From v_HotelRooms Where R_Name = N'" + text2 + "' And RS_Canused = 1";
			DataTable dataTable3 = SQLserver.Data_GetDataTable(sql);
			if (dataTable3 == null || dataTable3.Rows.Count < 1)
			{
				Program.MsgCustom(text2 + "\r\n" + (string)m_htab["Err05"], MessageBoxIcon.Hand);
				return;
			}
			double num4 = Convert.ToDouble(dataTable3.Rows[0]["TP_deposit"]);
			double num5 = Convert.ToDouble(dataTable3.Rows[0]["TP_Price"]);
			double num6 = Convert.ToDouble(dataTable3.Rows[0]["TP_PricelessHour"]);
			string text5 = label84.Text + " " + dataTable.Rows[0]["R_Name"].ToString();
			string text6 = text5;
			text5 = text6 + "\r\n" + label38.Text + " " + dataTable.Rows[0]["TP_Name"].ToString();
			string text7 = text5;
			text5 = text7 + "\r\n" + label39.Text + " " + dataTable.Rows[0]["R_CurGuestCount"].ToString();
			if (flag)
			{
				string text8 = text5;
				text5 = text8 + "\r\n" + label44.Text + "(" + Program.GetFaceDisValue(num3) + "%) " + (Convert.ToDouble(dataTable.Rows[0]["TP_PricelessHour"]) * num3).ToString("F2") + text3 + "-->" + (Convert.ToDouble(dataTable.Rows[0]["TP_PricelessHour"]) * num3 / num2).ToString("F2") + text4;
				string text9 = text5;
				text5 = text9 + "\r\n" + (string)m_htab["dgvTP_Price"] + " " + dataTable.Rows[0]["TP_PricelessHour"].ToString() + text3 + "-->" + (Convert.ToDouble(dataTable.Rows[0]["TP_PricelessHour"]) / num2).ToString("F2") + text4;
				string text10 = text5;
				text5 = text10 + "\r\n" + label32.Text + " " + dataTable.Rows[0]["TP_deposit"].ToString() + text3 + "-->" + (Convert.ToDouble(dataTable.Rows[0]["TP_deposit"]) / num2).ToString("F2") + text4;
				string text11 = text5;
				text5 = text11 + "\r\n" + label41.Text + " " + Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["TR_cometime"].ToString()));
				string text12 = text5;
				text5 = text12 + "\r\n" + (string)m_htab["label28_hr"] + " " + stayhour;
				string text13 = text5;
				text5 = text13 + "\r\n" + label43.Text + " " + Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["CurLT"].ToString()));
				text5 += "\r\n--------------------------";
				string text14 = text5;
				text5 = text14 + "\r\n" + label85.Text + " " + dataTable3.Rows[0]["R_Name"].ToString();
				string text15 = text5;
				text5 = text15 + "\r\n" + label38.Text + " " + dataTable3.Rows[0]["TP_Name"].ToString();
				string text16 = text5;
				text5 = text16 + "\r\n" + label39.Text + " " + dataTable3.Rows[0]["R_CurGuestCount"].ToString();
				string text17 = text5;
				text5 = text17 + "\r\n" + (string)m_htab["dgvTR_discount"] + " " + Program.GetFaceDisValue(num3) + "%";
				string text18 = text5;
				text5 = text18 + "\r\n" + label44.Text + "(" + Program.GetFaceDisValue(num3) + "%) " + (num6 * num3).ToString("F2") + text3 + "-->" + (num6 * num3 / num2).ToString("F2") + text4;
				string text19 = text5;
				text5 = text19 + "\r\n" + (string)m_htab["dgvTP_Price"] + " " + num6.ToString("F2") + text3 + "-->" + (num6 / num2).ToString("F2") + text4;
				string text20 = text5;
				text5 = text20 + "\r\n" + label32.Text + " " + num4.ToString("F2") + text3 + "-->" + (num4 / num2).ToString("F2") + text4;
			}
			else
			{
				string text21 = text5;
				text5 = text21 + "\r\n" + label44.Text + "(" + Program.GetFaceDisValue(num3) + "%) " + (Convert.ToDouble(dataTable.Rows[0]["TP_Price"]) * num3).ToString("F2") + text3 + "-->" + (Convert.ToDouble(dataTable.Rows[0]["TP_Price"]) * num3 / num2).ToString("F2") + text4;
				text6 = text5;
				text5 = text6 + "\r\n" + (string)m_htab["dgvTP_Price"] + " " + dataTable.Rows[0]["TP_Price"].ToString() + text3 + "-->" + (Convert.ToDouble(dataTable.Rows[0]["TP_Price"]) / num2).ToString("F2") + text4;
				text6 = text5;
				text5 = text6 + "\r\n" + label32.Text + " " + dataTable.Rows[0]["TP_deposit"].ToString() + text3 + "-->" + (Convert.ToDouble(dataTable.Rows[0]["TP_deposit"]) / num2).ToString("F2") + text4;
				text6 = text5;
				text5 = text6 + "\r\n" + label41.Text + " " + Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["TR_cometime"].ToString()));
				text6 = text5;
				text5 = text6 + "\r\n" + label42.Text + " " + stayday;
				text6 = text5;
				text5 = text6 + "\r\n" + label43.Text + " " + Program.GetLocDTime(Convert.ToDateTime(dataTable.Rows[0]["CurLT"].ToString()));
				text5 += "\r\n--------------------------";
				text6 = text5;
				text5 = text6 + "\r\n" + label85.Text + " " + dataTable3.Rows[0]["R_Name"].ToString();
				text6 = text5;
				text5 = text6 + "\r\n" + label38.Text + " " + dataTable3.Rows[0]["TP_Name"].ToString();
				text6 = text5;
				text5 = text6 + "\r\n" + label39.Text + " " + dataTable3.Rows[0]["R_CurGuestCount"].ToString();
				text6 = text5;
				text5 = text6 + "\r\n" + (string)m_htab["dgvTR_discount"] + " " + Program.GetFaceDisValue(num3) + "%";
				text6 = text5;
				text5 = text6 + "\r\n" + label44.Text + "(" + Program.GetFaceDisValue(num3) + "%) " + (num5 * num3).ToString("F2") + text3 + "-->" + (num5 * num3 / num2).ToString("F2") + text4;
				text6 = text5;
				text5 = text6 + "\r\n" + (string)m_htab["dgvTP_Price"] + " " + num5.ToString("F2") + text3 + "-->" + (num5 / num2).ToString("F2") + text4;
				text6 = text5;
				text5 = text6 + "\r\n" + label32.Text + " " + num4.ToString("F2") + text3 + "-->" + (num4 / num2).ToString("F2") + text4;
			}
			frmGCR frmGCR2 = new frmGCR();
			frmGCR2.isfordis = isfordis;
			frmGCR2.ptype = Convert.ToInt32(dataTable.Rows[0]["ptype"]);
			frmGCR2.othhavhour = num;
			frmGCR2.labTxtMsg.Text = text5;
			frmGCR2.dt = dataTable;
			frmGCR2.ndt = dataTable3;
			frmGCR2.stayday = stayday;
			frmGCR2.stayhour = stayhour;
			frmGCR2.m_discount = num3;
			frmGCR2.currrate = num2;
			frmGCR2.ndp = num4;
			frmGCR2.nrp = num5;
			frmGCR2.nrples = num6;
			frmGCR2.isforhour = flag;
			frmGCR2.basesurname = text3;
			frmGCR2.Text = TSMIRCh.Text;
			frmGCR2.ShowDialog();
			if (frmGCR2.m_retst == 0)
			{
				refresh_room(txtCurRn.Text.Trim(), 0, 0);
				refresh_room(txtTGRn.Text.Trim(), Convert.ToInt32(dataTable.Rows[0]["RS_ID"]) - 1, Convert.ToInt32(dataTable.Rows[0]["R_CurGuestCount"]));
			}
			frmGCR2.Dispose();
			dataTable.Clear();
			dataTable3.Clear();
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, TSMIRCh.Text);
		}
	}

	private void InitListView(DataTable dt)
	{
		if (dt == null)
		{
			return;
		}
		if (Program.fm.TSMIView02.Checked)
		{
			dgvList.Sorting = SortOrder.Ascending;
		}
		else
		{
			dgvList.Sorting = SortOrder.Descending;
		}
		dgvList.Groups.Clear();
		dgvList.Items.Clear();
		dgvList.Columns.Clear();
		for (int i = 0; i < dt.Columns.Count; i++)
		{
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Text = dt.Columns[i].Caption.ToString().Trim();
			dgvList.Columns.Add(columnHeader);
		}
		if (dt.Rows.Count <= 0)
		{
			return;
		}
		ListViewItem[] array = new ListViewItem[dt.Rows.Count];
		for (int j = 0; j < dt.Rows.Count; j++)
		{
			string[] array2 = new string[dt.Columns.Count];
			for (int k = 0; k < dt.Columns.Count; k++)
			{
				array2[k] = dt.Rows[j][k].ToString().Trim();
			}
			array[j] = new ListViewItem(array2);
			array[j].ImageIndex = Convert.ToInt16(dt.Rows[j]["RS_ID"].ToString()) - 1;
		}
		dgvList.Items.AddRange(array);
		if (isRunningXPOrLater && !toolsBtn5.Checked)
		{
			lvGroupTab = new Hashtable[dgvList.Columns.Count];
			for (int l = 0; l < dgvList.Columns.Count; l++)
			{
				lvGroupTab[l] = CreateGroupsTable(l);
			}
			lvGroupSetGroups(13);
		}
	}

	private Hashtable CreateGroupsTable(int column)
	{
		Hashtable hashtable = new Hashtable();
		foreach (ListViewItem item in dgvList.Items)
		{
			string text = item.SubItems[column].Text;
			if (!hashtable.Contains(text))
			{
				hashtable.Add(text, new ListViewGroup(text, HorizontalAlignment.Left));
			}
		}
		return hashtable;
	}

	private void lvGroupSetGroups(int column)
	{
		dgvList.Groups.Clear();
		Hashtable hashtable = lvGroupTab[column];
		ListViewGroup[] array = new ListViewGroup[hashtable.Count];
		hashtable.Values.CopyTo(array, 0);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Items.Clear();
		}
		Array.Sort(array, new ListViewGroupSorter(dgvList.Sorting));
		dgvList.Groups.AddRange(array);
		foreach (ListViewItem item in dgvList.Items)
		{
			if (item.Group != null)
			{
				item.Group = null;
			}
			string key = item.SubItems[column].Text;
			if ((ListViewGroup)hashtable[key] != null)
			{
				item.Group = (ListViewGroup)hashtable[key];
			}
			else if ((ListViewGroup)hashtable[key] == null)
			{
				dgvList.Items.Remove(item);
			}
		}
	}

	private void TSMIEBR_Click(object sender, EventArgs e)
	{
	}

	private void txtBM_Enter(object sender, EventArgs e)
	{
		ttMsg.SetToolTip(txtBM, (string)m_htab["txtBM-ttMsg"]);
		if (txtBM.ForeColor == Color.DarkGray)
		{
			txtBM.Text = "";
			txtBM.ForeColor = Color.Black;
		}
	}

	private void txtBM_KeyDown(object sender, KeyEventArgs e)
	{
		ttMsg.RemoveAll();
		if (e.KeyCode == Keys.Return)
		{
			btnSear_Click(null, null);
		}
	}

	private void txtBM_Leave(object sender, EventArgs e)
	{
		if (txtBM.Text.Trim() == "" || txtBM.ForeColor == Color.DarkGray)
		{
			txtBM.Text = (string)m_htab["txtBM"];
			txtBM.ForeColor = Color.DarkGray;
		}
	}

	private void cobStatus_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cobStatus.SelectedIndex > 0 && (int)cobStatus.SelectedValue == 3)
			{
				txtBM.Visible = true;
				btnSear.Left = txtBM.Left + txtBM.Width + 5;
			}
			else
			{
				txtBM.Visible = false;
				btnSear.Left = txtBM.Left;
			}
		}
		catch
		{
		}
	}

	private void btnIDCard_Click(object sender, EventArgs e)
	{
		try
		{
			TextBox textBox = txtCernum;
			string text = (txtGn.Text = "");
			textBox.Text = text;
			Program.IDCardData objEDZ = default(Program.IDCardData);
			Program.ReadICCard(ref objEDZ);
			txtGn.Text = objEDZ.Name.Trim();
			txtCernum.Text = objEDZ.IDCardNo;
		}
		catch
		{
		}
	}

	private void chkRepl_CheckedChanged(object sender, EventArgs e)
	{
		if (m_chVal)
		{
			return;
		}
		m_chVal = true;
		try
		{
			if (chkRepl.Checked)
			{
				if (btnCard.Enabled)
				{
					btnCard.ForeColor = Color.Red;
				}
				else
				{
					btnCard.ForeColor = Color.Black;
				}
				if (btnGCSO.Enabled)
				{
					btnGCSO.ForeColor = Color.Red;
				}
				else
				{
					btnGCSO.ForeColor = Color.Black;
				}
			}
			else
			{
				btnCard.ForeColor = Color.Black;
				btnGCSO.ForeColor = Color.Black;
			}
		}
		catch
		{
		}
		finally
		{
			m_chVal = false;
		}
	}

	private void TSMIRSCH_SUB_Click(object sender, EventArgs e)
	{
		try
		{
			string name = ((ToolStripMenuItem)sender).Name;
			name = name.Replace("TSMISub", "");
			int num = Convert.ToInt32(name);
			name = TSMIRName.Text.Trim();
			string sqlstr = "Update D_Rooms Set R_RSID = " + num.ToString() + ", R_Updatetime = GetDate(), R_Updator_ID=" + Program.m_opid + ", R_Updator=N'" + Program.m_OperName + "' Where R_Name = N'" + name + "'";
			if (SQLserver.Data_ExecuteSql(sqlstr) > 0)
			{
				refresh_room(name, num - 1, 0);
			}
		}
		catch
		{
		}
	}

	private void TSMISubOth_Click(object sender, EventArgs e)
	{
		frmOther frmOther2 = new frmOther();
		frmOther2.Text = TSMISubOth.Text;
		frmOther2.txtRoom.Text = TSMIRName.Text;
		frmOther2.ShowDialog();
	}

	private void btnIDCard_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(plR1.Location.X + clsPlRt.Location.X + splitContainer1.Location.X + panel1.Location.X + btnIDCard.Location.X + 60 + 60, plR1.Location.Y + clsPlRt.Location.Y + splitContainer1.Location.Y + panel1.Location.Y + btnIDCard.Location.Y - 12);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_identity"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnIDCard_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void toolsBtn5_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(clsBackPanel3.Location.X + splitContainer1.Location.X + panel1.Location.X + toolsBtn5.Location.X + 150, clsBackPanel3.Location.Y + splitContainer1.Location.Y + panel1.Location.Y + toolsBtn5.Location.Y - 10);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_shift"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void toolsBtn5_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void SetSize()
	{
		flowLayoutPanel1.Width = 963;
		flowLayoutPanel1.Height = 38;
		tvList.Width = 176;
		tvList.Height = 317;
		clsBackPanel3.Width = 523;
		clsBackPanel3.Height = 51;
		plR1.Width = 245;
		plR1.Height = 469;
		dgvList.Width = 523;
		dgvList.Height = 592;
		clsBackPanel2.Width = 176;
		clsBackPanel2.Height = 318;
		tableLayoutPanel1.Width = 915;
		tableLayoutPanel1.Height = 47;
		clsPlRt.Width = 250;
		clsPlRt.Height = 643;
		plR3.Width = 254;
		plR3.Height = 280;
		btnCard.Width = 238;
		btnCard.Height = 46;
		btnGCSO.Width = 238;
		btnGCSO.Height = 46;
		btnTGL.Width = 238;
		btnTGL.Height = 46;
		btnTGSO.Width = 238;
		btnTGSO.Height = 46;
		btnRGLevel.Width = 250;
		btnRGLevel.Height = 37;
		btnTGIn.Width = 250;
		btnTGIn.Height = 37;
		toolsBtn1.Width = 963;
		toolsBtn1.Height = 8;
		toolsBtn2.Width = 176;
		toolsBtn2.Height = 8;
		pictureBox1.Width = 48;
		pictureBox1.Height = 47;
		toolsBtn5.Width = 40;
		toolsBtn5.Height = 40;
	}

	private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0])
		{
			e.Handled = true;
		}
		else
		{
			e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
		}
	}

	private void btnCard_EnabledChanged(object sender, EventArgs e)
	{
		LockSoftware.Controls.GlassBtn glassBtn = sender as LockSoftware.Controls.GlassBtn;
		if (glassBtn.Enabled)
		{
			glassBtn.BackColor = Color.Green;
			if (chkRepl.Checked)
			{
				glassBtn.ForeColor = Color.Red;
			}
		}
		else
		{
			glassBtn.BackColor = Color.LightGray;
			glassBtn.ForeColor = Color.Black;
		}
	}

	private void txtRn_TextChanged(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(txtRn.Text))
		{
			LockSoftware.Controls.GlassBtn glassBtn = btnCard;
			bool enabled = (btnGCSO.Enabled = false);
			glassBtn.Enabled = enabled;
		}
	}
}
