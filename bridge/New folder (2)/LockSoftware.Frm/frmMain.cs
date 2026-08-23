using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CommonLib;
using DataBase;
using Dev_C_Sharp;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmMain : Form
{
	private IContainer components;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel TSSLReader;

	private ToolTip toolTip;

	private clsBackPanel cbpTop;

	private ToolStrip ToolMain;

	private ToolStripButton tBtnMain;

	private ToolStripMenuItem TSMI_SM;

	private clsBackPanel cbpline01;

	private ToolStripMenuItem TSMIHInfo;

	private ToolStripMenuItem TSMIExit;

	private ToolStripMenuItem TSMIAbout;

	private ToolStripMenuItem TSMILogout;

	private ToolStripMenuItem TSMI_RM;

	private ToolStripMenuItem TSMI_CM;

	private ToolStripMenuItem TSMI_DM;

	private ToolStripMenuItem TSMI_UM;

	private ToolStripMenuItem TSMICer;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem TSMIPaid;

	private ToolStripMenuItem TSMIOther;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem TSMIBF;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem TSMIRoomType;

	private ToolStripMenuItem TSMIRoomSta;

	private ToolStripMenuItem TSMICurrType;

	private ToolStripMenuItem TSMIRooms;

	private ToolStripButton tBtnTeam;

	private ToolStripDropDownButton TSSLRSt;

	private ToolStripMenuItem TSMICardMgr;

	private ToolStripMenuItem TSMICardEmp;

	private ToolStripMenuItem TSMI_HC;

	private ToolStripMenuItem TSMIRCenter;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton tBtnReadCard;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripMenuItem TSMICardRead;

	private ToolStripMenuItem TSMICardLogout;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripButton tBtnLogout;

	private ToolStripMenuItem TSMISGuest;

	private ToolStripMenuItem TSMIUPWD;

	private ToolStripMenuItem TSMISRoom;

	private ToolStripButton tBtnSGuest;

	private ToolStripButton tBtnSRoom;

	private ToolStripSeparator toolStripSeparator7;

	private Timer tmSys;

	private ToolStripStatusLabel TSSLab02;

	private ToolStripStatusLabel TSSLSystime;

	private ToolStripStatusLabel TSSLab04;

	private ToolStripStatusLabel TSSLUser;

	private ToolStripStatusLabel TSSLRState;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripMenuItem TSMIGrp;

	private ToolStripMenuItem TSMITeamMgr;

	private ToolStripMenuItem TSMITeamCI;

	private ToolStripMenuItem TSMITeam;

	private ToolStripSeparator toolStripSeparator9;

	private ToolStripMenuItem TSMIView;

	private ToolStripMenuItem TSMIView01;

	private ToolStripSeparator TSSViewLine;

	public MenuStrip MenuMain;

	public ToolStripMenuItem TSMIView02;

	private ToolStripButton tBtnBook;

	private ToolStripMenuItem TSMIBR;

	private ToolStripMenuItem TSMIBMSingle;

	private ToolStripMenuItem TSMIBRCancel;

	private ToolStripMenuItem TSMIUMgr;

	private ToolStripMenuItem TSMIUGMgr;

	private ToolStripMenuItem TSMIUPMgr;

	private ToolStripMenuItem TSMIUPGMgr;

	private ToolStripSeparator toolStripSeparator10;

	private ToolStripSeparator toolStripSeparator11;

	private ToolStripSeparator toolStripSeparator12;

	private ToolStripMenuItem TSMILL;

	private ToolStripSeparator toolStripSeparator13;

	private ToolStripMenuItem TSMIDB;

	private ToolStripMenuItem TSMISMCard;

	private ToolStripMenuItem TSMISGSCard;

	private ToolStripSeparator toolStripSeparator14;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private ToolStripMenuItem TSMILanMgr;

	private Timer tmChkGuest;

	private ToolStripMenuItem TSMIRCHK;

	private Timer tmChkRS;

	private ToolStripMenuItem TSMIRC_GC;

	private ToolStripMenuItem TSMIRC_CC;

	private ToolStripMenuItem TSMI_HELP;

	private ToolStripMenuItem TSMISoftIns;

	private ToolStripSeparator toolStripSeparator15;

	private ToolStripMenuItem TSMIOtherList;

	private ToolStripSeparator toolStripSeparator16;

	private ToolStripMenuItem TSMITGLog;

	private ToolStripMenuItem TSMIHR;

	private Timer tmChkReg;

	private ToolStripMenuItem TSMITeamInfo;

	private ToolStripSeparator tsbEmpty;

	private PictureBox pictureBox1;

	private ToolStripLabel tslEmpty;

	private ToolStripMenuItem TSMIBRCheckIn;

	private ToolStripMenuItem TSMI_Item;

	private ToolStripMenuItem TSMISItem;

	private ToolStripMenuItem TSMIShop;

	private clsBackPanel clsBackPanel1;

	private ToolStripMenuItem TSMIExportParameters;

	private ToolStripMenuItem TSMIExportRooms;

	private ToolStripMenuItem TSMIExportGroups;

	private ToolStripMenuItem TSMIImportRooms;

	private ToolStripSeparator toolStripSeparator17;

	private ToolStripMenuItem TSMIDisplaySet;

	private int childFormNumber;

	public string m_objName = "Pub";

	public ArrayList cur_rnList = new ArrayList();

	public bool m_PopOldMess = true;

	public bool m_RChkRun;

	private bool m_reginfo = true;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmMain));
		this.toolTip = new System.Windows.Forms.ToolTip(this.components);
		this.tmSys = new System.Windows.Forms.Timer(this.components);
		this.tmChkGuest = new System.Windows.Forms.Timer(this.components);
		this.tmChkRS = new System.Windows.Forms.Timer(this.components);
		this.tmChkReg = new System.Windows.Forms.Timer(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.TSSLab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLSystime = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab04 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLUser = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLReader = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLRState = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLRSt = new System.Windows.Forms.ToolStripDropDownButton();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.ToolMain = new System.Windows.Forms.ToolStrip();
		this.tslEmpty = new System.Windows.Forms.ToolStripLabel();
		this.tsbEmpty = new System.Windows.Forms.ToolStripSeparator();
		this.tBtnMain = new System.Windows.Forms.ToolStripButton();
		this.tBtnTeam = new System.Windows.Forms.ToolStripButton();
		this.tBtnBook = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.tBtnSGuest = new System.Windows.Forms.ToolStripButton();
		this.tBtnSRoom = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.tBtnReadCard = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.tBtnLogout = new System.Windows.Forms.ToolStripButton();
		this.cbpTop = new LockSoftware.Controls.clsBackPanel(this.components);
		this.cbpline01 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.MenuMain = new System.Windows.Forms.MenuStrip();
		this.TSMI_SM = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIHInfo = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMICer = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIPaid = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMICurrType = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIOther = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIExportParameters = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIExportRooms = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIExportGroups = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIImportRooms = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMILogout = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIExit = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_RM = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIBF = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIRoomType = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIRoomSta = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIRooms = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIGrp = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_CM = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMICardMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMICardEmp = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMICardRead = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMICardLogout = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_HC = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIRCenter = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMITeamMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMITeamCI = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMITeamInfo = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMITGLog = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMITeam = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIHR = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIBR = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIBMSingle = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIBRCancel = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIBRCheckIn = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIRCHK = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIRC_GC = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIRC_CC = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIView = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIView01 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSSViewLine = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIView02 = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_DM = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISGuest = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISRoom = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIOtherList = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMISMCard = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISGSCard = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMILL = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIDB = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMILanMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_UM = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIUGMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIUMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIUPGMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIUPMgr = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIUPWD = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_Item = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISItem = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIShop = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMI_HELP = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMISoftIns = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
		this.TSMIAbout = new System.Windows.Forms.ToolStripMenuItem();
		this.TSMIDisplaySet = new System.Windows.Forms.ToolStripMenuItem();
		this.clsBackPanel1.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.ToolMain.SuspendLayout();
		this.cbpTop.SuspendLayout();
		this.MenuMain.SuspendLayout();
		base.SuspendLayout();
		this.tmSys.Enabled = true;
		this.tmSys.Interval = 500;
		this.tmSys.Tick += new System.EventHandler(tmSys_Tick);
		this.tmChkGuest.Interval = 1000;
		this.tmChkGuest.Tick += new System.EventHandler(tmChkGuest_Tick);
		this.tmChkRS.Interval = 1000;
		this.tmChkRS.Tick += new System.EventHandler(tmChkRS_Tick);
		this.tmChkReg.Enabled = true;
		this.tmChkReg.Interval = 1000;
		this.tmChkReg.Tick += new System.EventHandler(tmChkReg_Tick);
		this.clsBackPanel1.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.Lavender;
		this.clsBackPanel1.ColorAngle = 180f;
		this.clsBackPanel1.Controls.Add(this.statusStrip1);
		this.clsBackPanel1.Controls.Add(this.pictureBox1);
		this.clsBackPanel1.Controls.Add(this.ToolMain);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 25);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(784, 48);
		this.clsBackPanel1.TabIndex = 7;
		this.statusStrip1.AutoSize = false;
		this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
		this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.TSSLab02, this.toolStripStatusLabel1, this.TSSLSystime, this.TSSLab04, this.TSSLUser, this.TSSLReader, this.TSSLRState, this.TSSLRSt });
		this.statusStrip1.Location = new System.Drawing.Point(379, 0);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 15, 0);
		this.statusStrip1.Size = new System.Drawing.Size(405, 48);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 2;
		this.statusStrip1.Text = "StatusStrip";
		this.TSSLab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab02.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.TSSLab02.Name = "TSSLab02";
		this.TSSLab02.Size = new System.Drawing.Size(4, 43);
		this.TSSLab02.Visible = false;
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(183, 43);
		this.toolStripStatusLabel1.Spring = true;
		this.TSSLSystime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLSystime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLSystime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold);
		this.TSSLSystime.ForeColor = System.Drawing.Color.Green;
		this.TSSLSystime.Name = "TSSLSystime";
		this.TSSLSystime.Size = new System.Drawing.Size(4, 43);
		this.TSSLSystime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab04.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab04.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.TSSLab04.Name = "TSSLab04";
		this.TSSLab04.Size = new System.Drawing.Size(96, 43);
		this.TSSLab04.Text = "当前操作员：";
		this.TSSLUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLUser.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLUser.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.TSSLUser.ForeColor = System.Drawing.Color.Green;
		this.TSSLUser.Name = "TSSLUser";
		this.TSSLUser.Size = new System.Drawing.Size(4, 43);
		this.TSSLReader.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLReader.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.TSSLReader.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.TSSLReader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLReader.Name = "TSSLReader";
		this.TSSLReader.Size = new System.Drawing.Size(47, 43);
		this.TSSLReader.Text = "Reader:";
		this.TSSLRState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLRState.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
		this.TSSLRState.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.TSSLRState.Image = LockSoftware.Properties.Resources.v_break;
		this.TSSLRState.IsLink = true;
		this.TSSLRState.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.TSSLRState.LinkColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.TSSLRState.Name = "TSSLRState";
		this.TSSLRState.Size = new System.Drawing.Size(55, 43);
		this.TSSLRState.Text = "Break";
		this.TSSLRState.Click += new System.EventHandler(TSSLRState_Click);
		this.TSSLRState.MouseEnter += new System.EventHandler(TSSLRState_MouseEnter);
		this.TSSLRState.MouseLeave += new System.EventHandler(TSSLRState_MouseLeave);
		this.TSSLRSt.Image = LockSoftware.Properties.Resources.delete;
		this.TSSLRSt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSLRSt.Name = "TSSLRSt";
		this.TSSLRSt.ShowDropDownArrow = false;
		this.TSSLRSt.Size = new System.Drawing.Size(62, 46);
		this.TSSLRSt.Text = "Break";
		this.TSSLRSt.Visible = false;
		this.TSSLRSt.Click += new System.EventHandler(TSSLRSt_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Location = new System.Drawing.Point(3, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(100, 42);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 38;
		this.pictureBox1.TabStop = false;
		this.ToolMain.BackColor = System.Drawing.Color.Transparent;
		this.ToolMain.Dock = System.Windows.Forms.DockStyle.Left;
		this.ToolMain.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.ToolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.tslEmpty, this.tsbEmpty, this.tBtnMain, this.tBtnTeam, this.tBtnBook, this.toolStripSeparator4, this.tBtnSGuest, this.tBtnSRoom, this.toolStripSeparator7, this.tBtnReadCard,
			this.toolStripSeparator6, this.tBtnLogout
		});
		this.ToolMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
		this.ToolMain.Location = new System.Drawing.Point(0, 0);
		this.ToolMain.Name = "ToolMain";
		this.ToolMain.Padding = new System.Windows.Forms.Padding(3);
		this.ToolMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.ToolMain.Size = new System.Drawing.Size(379, 48);
		this.ToolMain.TabIndex = 5;
		this.ToolMain.Text = "tool";
		this.tslEmpty.AutoSize = false;
		this.tslEmpty.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.tslEmpty.Name = "tslEmpty";
		this.tslEmpty.Size = new System.Drawing.Size(97, 39);
		this.tsbEmpty.AutoSize = false;
		this.tsbEmpty.Name = "tsbEmpty";
		this.tsbEmpty.Size = new System.Drawing.Size(6, 40);
		this.tBtnMain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnMain.Image = LockSoftware.Properties.Resources.TMenu;
		this.tBtnMain.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnMain.Name = "tBtnMain";
		this.tBtnMain.Size = new System.Drawing.Size(36, 36);
		this.tBtnMain.Text = "客房中心";
		this.tBtnMain.Click += new System.EventHandler(tBtnMain_Click);
		this.tBtnTeam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnTeam.Image = LockSoftware.Properties.Resources.GuestIn;
		this.tBtnTeam.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnTeam.Name = "tBtnTeam";
		this.tBtnTeam.Size = new System.Drawing.Size(36, 36);
		this.tBtnTeam.Text = "团队入住";
		this.tBtnTeam.Click += new System.EventHandler(tBtnTeam_Click);
		this.tBtnBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnBook.Image = LockSoftware.Properties.Resources.synchour;
		this.tBtnBook.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnBook.Name = "tBtnBook";
		this.tBtnBook.Size = new System.Drawing.Size(36, 36);
		this.tBtnBook.Text = "客房预订";
		this.tBtnBook.Click += new System.EventHandler(tBtnBook_Click);
		this.toolStripSeparator4.AutoSize = false;
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
		this.tBtnSGuest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnSGuest.Image = LockSoftware.Properties.Resources._46;
		this.tBtnSGuest.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnSGuest.Name = "tBtnSGuest";
		this.tBtnSGuest.Size = new System.Drawing.Size(36, 36);
		this.tBtnSGuest.Text = "宾客查询";
		this.tBtnSGuest.Click += new System.EventHandler(tBtnSGuest_Click);
		this.tBtnSRoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnSRoom.Image = LockSoftware.Properties.Resources.OS01;
		this.tBtnSRoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnSRoom.Name = "tBtnSRoom";
		this.tBtnSRoom.Size = new System.Drawing.Size(36, 36);
		this.tBtnSRoom.Text = "客房消费查询";
		this.tBtnSRoom.Click += new System.EventHandler(tBtnSRoom_Click);
		this.toolStripSeparator7.AutoSize = false;
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 40);
		this.tBtnReadCard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnReadCard.Image = LockSoftware.Properties.Resources.SHOW_CARD;
		this.tBtnReadCard.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnReadCard.Name = "tBtnReadCard";
		this.tBtnReadCard.Size = new System.Drawing.Size(36, 36);
		this.tBtnReadCard.Text = "查询卡片";
		this.tBtnReadCard.Click += new System.EventHandler(tBtnReadCard_Click);
		this.toolStripSeparator6.AutoSize = false;
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(6, 40);
		this.tBtnLogout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tBtnLogout.Image = LockSoftware.Properties.Resources._120px_Vista_logout;
		this.tBtnLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tBtnLogout.Name = "tBtnLogout";
		this.tBtnLogout.Size = new System.Drawing.Size(36, 36);
		this.tBtnLogout.Text = "退出系统";
		this.tBtnLogout.Click += new System.EventHandler(tBtnLogout_Click);
		this.cbpTop.AutoSize = true;
		this.cbpTop.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.cbpTop.Border = false;
		this.cbpTop.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpTop.BorderBW = 1;
		this.cbpTop.BorderColorBottom = System.Drawing.Color.Gray;
		this.cbpTop.BorderColorLeft = System.Drawing.Color.Gray;
		this.cbpTop.BorderColorRight = System.Drawing.Color.Gray;
		this.cbpTop.BorderColorTop = System.Drawing.Color.Gray;
		this.cbpTop.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpTop.BorderLW = 1;
		this.cbpTop.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpTop.BorderRW = 1;
		this.cbpTop.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpTop.BorderTW = 1;
		this.cbpTop.Color1 = System.Drawing.Color.White;
		this.cbpTop.Color2 = System.Drawing.Color.Lavender;
		this.cbpTop.ColorAngle = 180f;
		this.cbpTop.Controls.Add(this.cbpline01);
		this.cbpTop.Controls.Add(this.MenuMain);
		this.cbpTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.cbpTop.Location = new System.Drawing.Point(0, 0);
		this.cbpTop.Name = "cbpTop";
		this.cbpTop.Size = new System.Drawing.Size(784, 25);
		this.cbpTop.TabIndex = 5;
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
		this.cbpline01.Color1 = System.Drawing.Color.White;
		this.cbpline01.Color2 = System.Drawing.Color.Black;
		this.cbpline01.ColorAngle = 135f;
		this.cbpline01.Dock = System.Windows.Forms.DockStyle.Top;
		this.cbpline01.Location = new System.Drawing.Point(0, 24);
		this.cbpline01.Name = "cbpline01";
		this.cbpline01.Size = new System.Drawing.Size(784, 1);
		this.cbpline01.TabIndex = 34;
		this.MenuMain.BackColor = System.Drawing.Color.Transparent;
		this.MenuMain.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.TSMI_SM, this.TSMI_RM, this.TSMI_CM, this.TSMI_HC, this.TSMI_DM, this.TSMI_UM, this.TSMI_Item, this.TSMI_HELP });
		this.MenuMain.Location = new System.Drawing.Point(0, 0);
		this.MenuMain.Name = "MenuMain";
		this.MenuMain.Size = new System.Drawing.Size(784, 24);
		this.MenuMain.TabIndex = 0;
		this.MenuMain.Text = "menuStrip1";
		this.TSMI_SM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[15]
		{
			this.TSMIHInfo, this.toolStripSeparator1, this.TSMICer, this.TSMIPaid, this.TSMICurrType, this.toolStripMenuItem2, this.TSMIOther, this.toolStripSeparator17, this.TSMIExportParameters, this.TSMIExportRooms,
			this.TSMIExportGroups, this.TSMIImportRooms, this.toolStripSeparator2, this.TSMILogout, this.TSMIExit
		});
		this.TSMI_SM.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.TSMI_SM.ForeColor = System.Drawing.SystemColors.ControlText;
		this.TSMI_SM.Image = LockSoftware.Properties.Resources.menu_show;
		this.TSMI_SM.Name = "TSMI_SM";
		this.TSMI_SM.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_SM.Size = new System.Drawing.Size(92, 20);
		this.TSMI_SM.Text = "系统菜单";
		this.TSMIHInfo.Name = "TSMIHInfo";
		this.TSMIHInfo.Size = new System.Drawing.Size(174, 22);
		this.TSMIHInfo.Text = "酒店信息";
		this.TSMIHInfo.Click += new System.EventHandler(TSMIHInfo_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
		this.TSMICer.Name = "TSMICer";
		this.TSMICer.Size = new System.Drawing.Size(174, 22);
		this.TSMICer.Text = "证件类型";
		this.TSMICer.Click += new System.EventHandler(TSMICer_Click);
		this.TSMIPaid.Name = "TSMIPaid";
		this.TSMIPaid.Size = new System.Drawing.Size(174, 22);
		this.TSMIPaid.Text = "支付类型";
		this.TSMIPaid.Visible = false;
		this.TSMICurrType.Name = "TSMICurrType";
		this.TSMICurrType.Size = new System.Drawing.Size(174, 22);
		this.TSMICurrType.Text = "币种设置";
		this.TSMICurrType.Click += new System.EventHandler(TSMICurrType_Click);
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
		this.toolStripMenuItem2.Text = "活动设置";
		this.toolStripMenuItem2.Visible = false;
		this.TSMIOther.Name = "TSMIOther";
		this.TSMIOther.Size = new System.Drawing.Size(174, 22);
		this.TSMIOther.Text = "消费设置";
		this.TSMIOther.Click += new System.EventHandler(TSMIOther_Click);
		this.toolStripSeparator17.Name = "toolStripSeparator17";
		this.toolStripSeparator17.Size = new System.Drawing.Size(171, 6);
		this.toolStripSeparator17.Visible = false;
		this.TSMIExportParameters.Name = "TSMIExportParameters";
		this.TSMIExportParameters.Size = new System.Drawing.Size(174, 22);
		this.TSMIExportParameters.Text = "导出发卡器参数";
		this.TSMIExportParameters.Visible = false;
		this.TSMIExportParameters.Click += new System.EventHandler(TSMIExportParameters_Click);
		this.TSMIExportRooms.Name = "TSMIExportRooms";
		this.TSMIExportRooms.Size = new System.Drawing.Size(174, 22);
		this.TSMIExportRooms.Text = "Export Informarion Of Rooms";
		this.TSMIExportRooms.Visible = false;
		this.TSMIExportRooms.Click += new System.EventHandler(TSMIExportRooms_Click);
		this.TSMIExportGroups.Name = "TSMIExportGroups";
		this.TSMIExportGroups.Size = new System.Drawing.Size(174, 22);
		this.TSMIExportGroups.Text = "导出客房分组";
		this.TSMIExportGroups.Visible = false;
		this.TSMIExportGroups.Click += new System.EventHandler(TSMIExportGroups_Click);
		this.TSMIImportRooms.Name = "TSMIImportRooms";
		this.TSMIImportRooms.Size = new System.Drawing.Size(174, 22);
		this.TSMIImportRooms.Text = "导入客房数据";
		this.TSMIImportRooms.Visible = false;
		this.TSMIImportRooms.Click += new System.EventHandler(TSMIImportRooms_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
		this.TSMILogout.Name = "TSMILogout";
		this.TSMILogout.Size = new System.Drawing.Size(174, 22);
		this.TSMILogout.Text = "注销";
		this.TSMILogout.Click += new System.EventHandler(TSMILogout_Click);
		this.TSMIExit.Name = "TSMIExit";
		this.TSMIExit.Size = new System.Drawing.Size(174, 22);
		this.TSMIExit.Text = "退出系统";
		this.TSMIExit.Click += new System.EventHandler(TSMIExit_Click);
		this.TSMI_RM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.TSMIBF, this.toolStripSeparator3, this.TSMIRoomType, this.TSMIRoomSta, this.TSMIRooms, this.toolStripSeparator8, this.TSMIGrp });
		this.TSMI_RM.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.TSMI_RM.Image = LockSoftware.Properties.Resources._05_1_;
		this.TSMI_RM.Name = "TSMI_RM";
		this.TSMI_RM.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_RM.Size = new System.Drawing.Size(92, 20);
		this.TSMI_RM.Text = "客房管理";
		this.TSMIBF.Name = "TSMIBF";
		this.TSMIBF.Size = new System.Drawing.Size(146, 22);
		this.TSMIBF.Text = "楼层管理";
		this.TSMIBF.Click += new System.EventHandler(TSMIBF_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
		this.TSMIRoomType.Name = "TSMIRoomType";
		this.TSMIRoomType.Size = new System.Drawing.Size(146, 22);
		this.TSMIRoomType.Text = "客房类型";
		this.TSMIRoomType.Click += new System.EventHandler(TSMIRoomType_Click);
		this.TSMIRoomSta.Name = "TSMIRoomSta";
		this.TSMIRoomSta.Size = new System.Drawing.Size(146, 22);
		this.TSMIRoomSta.Text = "客房状态";
		this.TSMIRoomSta.Visible = false;
		this.TSMIRoomSta.Click += new System.EventHandler(TSMIRoomSta_Click);
		this.TSMIRooms.Name = "TSMIRooms";
		this.TSMIRooms.Size = new System.Drawing.Size(146, 22);
		this.TSMIRooms.Text = "客房设置";
		this.TSMIRooms.Click += new System.EventHandler(TSMIRooms_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(143, 6);
		this.TSMIGrp.Name = "TSMIGrp";
		this.TSMIGrp.Size = new System.Drawing.Size(146, 22);
		this.TSMIGrp.Text = "客房组管理";
		this.TSMIGrp.Click += new System.EventHandler(TSMIGrp_Click);
		this.TSMI_CM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.TSMICardMgr, this.TSMICardEmp, this.toolStripSeparator5, this.TSMICardRead, this.TSMICardLogout });
		this.TSMI_CM.Image = LockSoftware.Properties.Resources.shared_pictures;
		this.TSMI_CM.Name = "TSMI_CM";
		this.TSMI_CM.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_CM.Size = new System.Drawing.Size(92, 20);
		this.TSMI_CM.Text = "卡片管理";
		this.TSMICardMgr.Name = "TSMICardMgr";
		this.TSMICardMgr.Size = new System.Drawing.Size(132, 22);
		this.TSMICardMgr.Text = "设置卡";
		this.TSMICardMgr.Click += new System.EventHandler(TSMICardMgr_Click);
		this.TSMICardEmp.Name = "TSMICardEmp";
		this.TSMICardEmp.Size = new System.Drawing.Size(132, 22);
		this.TSMICardEmp.Text = "员工卡";
		this.TSMICardEmp.Click += new System.EventHandler(TSMICardEmp_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(129, 6);
		this.TSMICardRead.Name = "TSMICardRead";
		this.TSMICardRead.Size = new System.Drawing.Size(132, 22);
		this.TSMICardRead.Text = "读卡信息";
		this.TSMICardRead.Click += new System.EventHandler(TSMICardRead_Click);
		this.TSMICardLogout.Name = "TSMICardLogout";
		this.TSMICardLogout.Size = new System.Drawing.Size(132, 22);
		this.TSMICardLogout.Text = "注销卡片";
		this.TSMICardLogout.Click += new System.EventHandler(TSMICardLogout_Click);
		this.TSMI_HC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.TSMIRCenter, this.TSMITeamMgr, this.TSMIHR, this.TSMIBR, this.TSMIRCHK, this.toolStripSeparator9, this.TSMIView });
		this.TSMI_HC.Image = LockSoftware.Properties.Resources.TMenu;
		this.TSMI_HC.Name = "TSMI_HC";
		this.TSMI_HC.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_HC.Size = new System.Drawing.Size(92, 20);
		this.TSMI_HC.Text = "接待中心";
		this.TSMIRCenter.Name = "TSMIRCenter";
		this.TSMIRCenter.Size = new System.Drawing.Size(146, 22);
		this.TSMIRCenter.Text = "客房中心";
		this.TSMIRCenter.Click += new System.EventHandler(TSMIRCenter_Click);
		this.TSMITeamMgr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.TSMITeamCI, this.toolStripSeparator16, this.TSMITeamInfo, this.TSMITGLog, this.TSMITeam });
		this.TSMITeamMgr.Name = "TSMITeamMgr";
		this.TSMITeamMgr.Size = new System.Drawing.Size(146, 22);
		this.TSMITeamMgr.Text = "团队办理";
		this.TSMITeamCI.Name = "TSMITeamCI";
		this.TSMITeamCI.Size = new System.Drawing.Size(160, 22);
		this.TSMITeamCI.Text = "团队入住";
		this.TSMITeamCI.Click += new System.EventHandler(TSMITeamCI_Click);
		this.toolStripSeparator16.Name = "toolStripSeparator16";
		this.toolStripSeparator16.Size = new System.Drawing.Size(157, 6);
		this.TSMITeamInfo.Name = "TSMITeamInfo";
		this.TSMITeamInfo.Size = new System.Drawing.Size(160, 22);
		this.TSMITeamInfo.Text = "团队资料查询";
		this.TSMITeamInfo.Click += new System.EventHandler(TSMITeamInfo_Click);
		this.TSMITGLog.Name = "TSMITGLog";
		this.TSMITGLog.Size = new System.Drawing.Size(160, 22);
		this.TSMITGLog.Text = "团队日志查询";
		this.TSMITGLog.Click += new System.EventHandler(TSMITGLog_Click);
		this.TSMITeam.Name = "TSMITeam";
		this.TSMITeam.Size = new System.Drawing.Size(160, 22);
		this.TSMITeam.Text = "团队宾客明细";
		this.TSMITeam.Click += new System.EventHandler(TSMITeam_Click);
		this.TSMIHR.Name = "TSMIHR";
		this.TSMIHR.Size = new System.Drawing.Size(146, 22);
		this.TSMIHR.Text = "钟点房办理";
		this.TSMIHR.Visible = false;
		this.TSMIHR.Click += new System.EventHandler(TSMIHR_Click);
		this.TSMIBR.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.TSMIBMSingle, this.TSMIBRCancel, this.TSMIBRCheckIn });
		this.TSMIBR.Name = "TSMIBR";
		this.TSMIBR.Size = new System.Drawing.Size(146, 22);
		this.TSMIBR.Text = "客房预订";
		this.TSMIBMSingle.Name = "TSMIBMSingle";
		this.TSMIBMSingle.Size = new System.Drawing.Size(132, 22);
		this.TSMIBMSingle.Text = "单人预订";
		this.TSMIBMSingle.Click += new System.EventHandler(TSMIBMSingle_Click);
		this.TSMIBRCancel.Name = "TSMIBRCancel";
		this.TSMIBRCancel.Size = new System.Drawing.Size(132, 22);
		this.TSMIBRCancel.Text = "预订管理";
		this.TSMIBRCancel.Click += new System.EventHandler(TSMIBRCancel_Click);
		this.TSMIBRCheckIn.Name = "TSMIBRCheckIn";
		this.TSMIBRCheckIn.Size = new System.Drawing.Size(132, 22);
		this.TSMIBRCheckIn.Text = "预订入住";
		this.TSMIBRCheckIn.Click += new System.EventHandler(TSMIBRCheckIn_Click);
		this.TSMIRCHK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.TSMIRC_GC, this.TSMIRC_CC });
		this.TSMIRCHK.Name = "TSMIRCHK";
		this.TSMIRCHK.Size = new System.Drawing.Size(146, 22);
		this.TSMIRCHK.Text = "客房检查";
		this.TSMIRC_GC.CheckOnClick = true;
		this.TSMIRC_GC.Name = "TSMIRC_GC";
		this.TSMIRC_GC.Size = new System.Drawing.Size(132, 22);
		this.TSMIRC_GC.Text = "退房检查";
		this.TSMIRC_GC.CheckedChanged += new System.EventHandler(TSMIRC_GC_CheckedChanged);
		this.TSMIRC_CC.CheckOnClick = true;
		this.TSMIRC_CC.Name = "TSMIRC_CC";
		this.TSMIRC_CC.Size = new System.Drawing.Size(132, 22);
		this.TSMIRC_CC.Text = "清洁检查";
		this.TSMIRC_CC.CheckedChanged += new System.EventHandler(TSMIRC_CC_CheckedChanged);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(143, 6);
		this.toolStripSeparator9.Visible = false;
		this.TSMIView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.TSMIView01, this.TSSViewLine, this.TSMIView02 });
		this.TSMIView.Name = "TSMIView";
		this.TSMIView.Size = new System.Drawing.Size(146, 22);
		this.TSMIView.Text = "视图";
		this.TSMIView.Visible = false;
		this.TSMIView01.Name = "TSMIView01";
		this.TSMIView01.Size = new System.Drawing.Size(160, 22);
		this.TSMIView01.Text = "客房中心视图";
		this.TSSViewLine.Name = "TSSViewLine";
		this.TSSViewLine.Size = new System.Drawing.Size(157, 6);
		this.TSMIView02.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.TSMIView02.Checked = true;
		this.TSMIView02.CheckOnClick = true;
		this.TSMIView02.CheckState = System.Windows.Forms.CheckState.Checked;
		this.TSMIView02.Image = LockSoftware.Properties.Resources.GuideUp;
		this.TSMIView02.Name = "TSMIView02";
		this.TSMIView02.Size = new System.Drawing.Size(160, 22);
		this.TSMIView02.Text = "按客房类型";
		this.TSMIView02.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
		this.TSMIView02.Click += new System.EventHandler(TSMIView02_Click);
		this.TSMI_DM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[11]
		{
			this.TSMISGuest, this.TSMISRoom, this.TSMIOtherList, this.toolStripSeparator14, this.TSMISMCard, this.TSMISGSCard, this.toolStripSeparator12, this.TSMILL, this.toolStripSeparator13, this.TSMIDB,
			this.TSMILanMgr
		});
		this.TSMI_DM.Image = LockSoftware.Properties.Resources.mdf_ndf_dbfiles;
		this.TSMI_DM.Name = "TSMI_DM";
		this.TSMI_DM.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_DM.Size = new System.Drawing.Size(92, 20);
		this.TSMI_DM.Text = "数据管理";
		this.TSMISGuest.Name = "TSMISGuest";
		this.TSMISGuest.Size = new System.Drawing.Size(174, 22);
		this.TSMISGuest.Text = "宾客入住明细";
		this.TSMISGuest.Click += new System.EventHandler(TSMISGuest_Click);
		this.TSMISRoom.Name = "TSMISRoom";
		this.TSMISRoom.Size = new System.Drawing.Size(174, 22);
		this.TSMISRoom.Text = "客房消费统计";
		this.TSMISRoom.Click += new System.EventHandler(TSMISRoom_Click);
		this.TSMIOtherList.Name = "TSMIOtherList";
		this.TSMIOtherList.Size = new System.Drawing.Size(174, 22);
		this.TSMIOtherList.Text = "其他消费明细";
		this.TSMIOtherList.Click += new System.EventHandler(TSMIOtherList_Click);
		this.toolStripSeparator14.Name = "toolStripSeparator14";
		this.toolStripSeparator14.Size = new System.Drawing.Size(171, 6);
		this.TSMISMCard.Name = "TSMISMCard";
		this.TSMISMCard.Size = new System.Drawing.Size(174, 22);
		this.TSMISMCard.Text = "发卡记录查询";
		this.TSMISMCard.Click += new System.EventHandler(TSMISMCard_Click);
		this.TSMISGSCard.Name = "TSMISGSCard";
		this.TSMISGSCard.Size = new System.Drawing.Size(174, 22);
		this.TSMISGSCard.Text = "组号设置卡查询";
		this.TSMISGSCard.Click += new System.EventHandler(TSMISGSCard_Click);
		this.toolStripSeparator12.Name = "toolStripSeparator12";
		this.toolStripSeparator12.Size = new System.Drawing.Size(171, 6);
		this.TSMILL.Name = "TSMILL";
		this.TSMILL.Size = new System.Drawing.Size(174, 22);
		this.TSMILL.Text = "开锁日志";
		this.TSMILL.Click += new System.EventHandler(TSMILL_Click);
		this.toolStripSeparator13.Name = "toolStripSeparator13";
		this.toolStripSeparator13.Size = new System.Drawing.Size(171, 6);
		this.TSMIDB.Name = "TSMIDB";
		this.TSMIDB.Size = new System.Drawing.Size(174, 22);
		this.TSMIDB.Text = "数据库管理";
		this.TSMIDB.Click += new System.EventHandler(TSMIDB_Click);
		this.TSMILanMgr.Name = "TSMILanMgr";
		this.TSMILanMgr.Size = new System.Drawing.Size(174, 22);
		this.TSMILanMgr.Text = "语言管理";
		this.TSMILanMgr.Visible = false;
		this.TSMILanMgr.Click += new System.EventHandler(TSMILanMgr_Click);
		this.TSMI_UM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.TSMIUGMgr, this.TSMIUMgr, this.toolStripSeparator10, this.TSMIUPGMgr, this.TSMIUPMgr, this.toolStripSeparator11, this.TSMIUPWD });
		this.TSMI_UM.Image = LockSoftware.Properties.Resources.MgrIcon;
		this.TSMI_UM.Name = "TSMI_UM";
		this.TSMI_UM.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_UM.Size = new System.Drawing.Size(92, 20);
		this.TSMI_UM.Text = "用户管理";
		this.TSMIUGMgr.Name = "TSMIUGMgr";
		this.TSMIUGMgr.Size = new System.Drawing.Size(146, 22);
		this.TSMIUGMgr.Text = "用户组管理";
		this.TSMIUGMgr.Click += new System.EventHandler(TSMIUGMgr_Click);
		this.TSMIUMgr.Name = "TSMIUMgr";
		this.TSMIUMgr.Size = new System.Drawing.Size(146, 22);
		this.TSMIUMgr.Text = "用户管理";
		this.TSMIUMgr.Click += new System.EventHandler(TSMIUMgr_Click);
		this.toolStripSeparator10.Name = "toolStripSeparator10";
		this.toolStripSeparator10.Size = new System.Drawing.Size(143, 6);
		this.TSMIUPGMgr.Name = "TSMIUPGMgr";
		this.TSMIUPGMgr.Size = new System.Drawing.Size(146, 22);
		this.TSMIUPGMgr.Text = "组权限管理";
		this.TSMIUPGMgr.Click += new System.EventHandler(TSMIUPGMgr_Click);
		this.TSMIUPMgr.Name = "TSMIUPMgr";
		this.TSMIUPMgr.Size = new System.Drawing.Size(146, 22);
		this.TSMIUPMgr.Text = "权限管理";
		this.TSMIUPMgr.Click += new System.EventHandler(TSMIUPMgr_Click);
		this.toolStripSeparator11.Name = "toolStripSeparator11";
		this.toolStripSeparator11.Size = new System.Drawing.Size(143, 6);
		this.TSMIUPWD.Name = "TSMIUPWD";
		this.TSMIUPWD.Size = new System.Drawing.Size(146, 22);
		this.TSMIUPWD.Text = "修改密码";
		this.TSMIUPWD.Click += new System.EventHandler(TSMIUPWD_Click);
		this.TSMI_Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.TSMISItem, this.TSMIShop });
		this.TSMI_Item.Image = LockSoftware.Properties.Resources.ShopingBasket;
		this.TSMI_Item.Name = "TSMI_Item";
		this.TSMI_Item.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_Item.Size = new System.Drawing.Size(92, 20);
		this.TSMI_Item.Text = "消费管理";
		this.TSMISItem.Name = "TSMISItem";
		this.TSMISItem.Size = new System.Drawing.Size(132, 22);
		this.TSMISItem.Text = "消费设置";
		this.TSMISItem.Visible = false;
		this.TSMIShop.Name = "TSMIShop";
		this.TSMIShop.Size = new System.Drawing.Size(132, 22);
		this.TSMIShop.Text = "商品购买";
		this.TSMIShop.Click += new System.EventHandler(TSMIShop_Click);
		this.TSMI_HELP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSMISoftIns, this.toolStripSeparator15, this.TSMIAbout, this.TSMIDisplaySet });
		this.TSMI_HELP.Image = LockSoftware.Properties.Resources.help;
		this.TSMI_HELP.Name = "TSMI_HELP";
		this.TSMI_HELP.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
		this.TSMI_HELP.Size = new System.Drawing.Size(64, 20);
		this.TSMI_HELP.Text = "帮助";
		this.TSMISoftIns.Name = "TSMISoftIns";
		this.TSMISoftIns.Size = new System.Drawing.Size(160, 22);
		this.TSMISoftIns.Text = "软件说明书";
		this.TSMISoftIns.Click += new System.EventHandler(TSMISoftIns_Click);
		this.toolStripSeparator15.Name = "toolStripSeparator15";
		this.toolStripSeparator15.Size = new System.Drawing.Size(157, 6);
		this.TSMIAbout.Name = "TSMIAbout";
		this.TSMIAbout.Size = new System.Drawing.Size(160, 22);
		this.TSMIAbout.Text = "关于我们";
		this.TSMIAbout.Click += new System.EventHandler(TSMIAbout_Click);
		this.TSMIDisplaySet.Name = "TSMIDisplaySet";
		this.TSMIDisplaySet.Size = new System.Drawing.Size(160, 22);
		this.TSMIDisplaySet.Text = "Display Option";
		this.TSMIDisplaySet.Visible = false;
		this.TSMIDisplaySet.Click += new System.EventHandler(TSMIDisplaySet_Click);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(784, 562);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.cbpTop);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Times New Roman", 9f);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.IsMdiContainer = true;
		base.MainMenuStrip = this.MenuMain;
		base.Name = "frmMain";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "iGo Software";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmMain_FormClosing);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmMain_FormClosed);
		base.Load += new System.EventHandler(frmMain_Load);
		base.SizeChanged += new System.EventHandler(frmMain_SizeChanged);
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.ToolMain.ResumeLayout(false);
		this.ToolMain.PerformLayout();
		this.cbpTop.ResumeLayout(false);
		this.cbpTop.PerformLayout();
		this.MenuMain.ResumeLayout(false);
		this.MenuMain.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmMain()
	{
		InitializeComponent();
		if (!Program.LoadLogo("MainLOGO.png", pictureBox1))
		{
			pictureBox1.Visible = false;
			tslEmpty.Width = 4;
		}
		if (Program.m_defDiscount == -1)
		{
			if (Program.m_Lan == 1)
			{
				Program.m_defDiscount = 0;
			}
			else
			{
				Program.m_defDiscount = 0;
			}
			Program.SetSingleItem("Discount", Program.m_defDiscount.ToString());
		}
		Program.LocDFmt();
		cur_rnList.Clear();
		Program.m_hPubTab = new Hashtable();
		Program.m_hPubTab = Program.GetControlName(this, m_objName);
		TSSLUser.Text = Program.m_OperName;
		try
		{
			string sql = "Select top 1 * From D_Currency Where curr_Basflag  = 1";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				Program.m_baseCurrCode = dataTable.Rows[0]["curr_code"].ToString().Trim();
				Program.m_baseCurrID = Convert.ToInt32(dataTable.Rows[0]["curr_id"].ToString().Trim());
				Program.m_baseCurrRate = Convert.ToDouble(dataTable.Rows[0]["curr_rate"].ToString().Trim());
				dataTable.Clear();
			}
			else
			{
				Program.MsgBox((string)Program.m_hPubTab["SysInit01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Program.m_baseCurrRate = 1.0;
				Program.m_baseCurrCode = "";
				Program.m_baseCurrID = 1;
			}
			sql = "Select Top 1 * From D_HotelBasic ";
			dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				Text = dataTable.Rows[0]["B_HotelName"].ToString().Trim();
				Program.m_defDay = dataTable.Rows[0]["B_StayDay"].ToString().Trim();
				Program.m_defLeaveTime = dataTable.Rows[0]["B_LeaveTime"].ToString().Trim();
				Program.m_defHalfDay = dataTable.Rows[0]["B_leaveDelay1"].ToString().Trim();
				Program.m_defFullDay = dataTable.Rows[0]["B_leaveDelay2"].ToString().Trim();
				Program.m_defComeTime = dataTable.Rows[0]["B_ComingTime"].ToString().Trim();
				Program.m_defClearTime = ((!(dataTable.Rows[0]["B_CleanTime"].ToString().Trim() == "")) ? Convert.ToInt32(dataTable.Rows[0]["B_CleanTime"].ToString()) : 0);
				Program.m_defHR = ((dataTable.Rows[0]["B_CR_LessHour"] == null || dataTable.Rows[0]["B_CR_LessHour"].ToString().Trim().Length == 0) ? 4 : Convert.ToInt32(dataTable.Rows[0]["B_CR_LessHour"]));
				Program.m_chkGInfo = Convert.ToBoolean(dataTable.Rows[0]["B_GInfo"].ToString());
				Program.m_basMaxGuest = Convert.ToInt32(dataTable.Rows[0]["B_MaxGuest"].ToString());
				if (dataTable.Rows[0]["B_GInfo"].ToString() == "" || dataTable.Rows[0]["B_GInfo"].ToString() == "NULL")
				{
					Program.m_chkGInfo = true;
				}
				else
				{
					Program.m_chkGInfo = Convert.ToBoolean(dataTable.Rows[0]["B_GInfo"].ToString());
				}
				BackgroundImage = null;
				if (dataTable.Rows[0]["B_BackImg"].ToString().Trim() != null)
				{
					string text = dataTable.Rows[0]["B_BackImg"].ToString().Trim();
					if (text == "")
					{
						text = Program.m_bgVal;
					}
					else
					{
						byte[] array = (byte[])dataTable.Rows[0]["B_BackImg"];
						text = Convert.ToBase64String(array);
						try
						{
							MemoryStream stream = new MemoryStream(array);
							BackgroundImage = Image.FromStream(stream);
						}
						catch
						{
						}
						Program.m_bgVal = text;
					}
				}
				dataTable.Clear();
			}
			else
			{
				Text = "iGo Software";
				Program.MsgBox("Get Stay Day and Level Time error, system will use default value them.", "System Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Program.m_basMaxGuest = 10;
				Program.m_defDay = "1";
				Program.m_defLeaveTime = "12:30";
			}
			sql = "";
			for (int i = 1; i < 12; i++)
			{
				string text2 = (string)Program.m_hPubTab["RS_N" + i.ToString("D2")];
				string text3 = sql;
				sql = text3 + "Update D_RoomStatus Set RS_Name000 = N'" + text2 + "', RS_Name001 = N'" + text2 + "', RS_Name002 = N'" + text2 + "', RS_Name003 = '" + text2 + "'";
				sql = sql + " Where RS_ID = " + i + " \n ";
			}
			Program.DBCompExec(sql, "Init Application");
		}
		catch (Exception ex)
		{
			if (Program.m_Lan == 1)
			{
				Program.MsgBox("初始化系统数据错误，请关闭后重试！错误信息：\r\n" + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				Program.MsgBox("Error in initializing system, please close it and try it again ! Error Info:\r\n" + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void frmMain_Load(object sender, EventArgs e)
	{
		try
		{
			MenuMain.CanOverflow = true;
			TSMIHInfo.Enabled = SQLserver.GetUserPermisstion(1001, Program.m_OperID);
			TSMICer.Enabled = SQLserver.GetUserPermisstion(1002, Program.m_OperID);
			TSMICurrType.Enabled = SQLserver.GetUserPermisstion(1003, Program.m_OperID);
			TSMIBF.Enabled = SQLserver.GetUserPermisstion(1004, Program.m_OperID);
			TSMIRoomType.Enabled = SQLserver.GetUserPermisstion(1005, Program.m_OperID);
			TSMIRooms.Enabled = SQLserver.GetUserPermisstion(1006, Program.m_OperID);
			TSMIGrp.Enabled = SQLserver.GetUserPermisstion(1007, Program.m_OperID);
			TSMICardMgr.Enabled = SQLserver.GetUserPermisstion(1011, Program.m_OperID);
			TSMICardEmp.Enabled = SQLserver.GetUserPermisstion(1012, Program.m_OperID);
			TSMICardLogout.Enabled = SQLserver.GetUserPermisstion(1013, Program.m_OperID);
			ToolStripButton toolStripButton = tBtnMain;
			bool enabled = (TSMIRCenter.Enabled = SQLserver.GetUserPermisstion(1014, Program.m_OperID));
			toolStripButton.Enabled = enabled;
			ToolStripButton toolStripButton2 = tBtnTeam;
			bool enabled2 = (TSMITeamCI.Enabled = SQLserver.GetUserPermisstion(1022, Program.m_OperID));
			toolStripButton2.Enabled = enabled2;
			ToolStripButton toolStripButton3 = tBtnBook;
			bool enabled3 = (TSMIBMSingle.Enabled = SQLserver.GetUserPermisstion(1027, Program.m_OperID));
			toolStripButton3.Enabled = enabled3;
			ToolStripMenuItem tSMIBRCheckIn = TSMIBRCheckIn;
			bool enabled4 = (TSMIBRCancel.Enabled = SQLserver.GetUserPermisstion(1029, Program.m_OperID));
			tSMIBRCheckIn.Enabled = enabled4;
			ToolStripButton toolStripButton4 = tBtnSGuest;
			bool enabled5 = (TSMISGuest.Enabled = SQLserver.GetUserPermisstion(1032, Program.m_OperID));
			toolStripButton4.Enabled = enabled5;
			ToolStripButton toolStripButton5 = tBtnSRoom;
			bool enabled6 = (TSMISRoom.Enabled = SQLserver.GetUserPermisstion(1034, Program.m_OperID));
			toolStripButton5.Enabled = enabled6;
			ToolStripMenuItem tSMITeamInfo = TSMITeamInfo;
			bool enabled7 = (TSMITeam.Enabled = SQLserver.GetUserPermisstion(1030, Program.m_OperID));
			tSMITeamInfo.Enabled = enabled7;
			TSMILL.Enabled = SQLserver.GetUserPermisstion(1038, Program.m_OperID);
			TSMISMCard.Enabled = SQLserver.GetUserPermisstion(1040, Program.m_OperID);
			TSMISGSCard.Enabled = SQLserver.GetUserPermisstion(1042, Program.m_OperID);
			TSMIDB.Enabled = SQLserver.GetUserPermisstion(1044, Program.m_OperID);
			TSMIOther.Enabled = SQLserver.GetUserPermisstion(1045, Program.m_OperID);
			TSMIOtherList.Enabled = SQLserver.GetUserPermisstion(1049, Program.m_OperID);
			TSMIUGMgr.Enabled = SQLserver.GetUserPermisstion(17020, Program.m_OperID);
			TSMIUMgr.Enabled = SQLserver.GetUserPermisstion(17021, Program.m_OperID);
			TSMIUPGMgr.Enabled = SQLserver.GetUserPermisstion(17019, Program.m_OperID);
			TSMIUPMgr.Enabled = SQLserver.GetUserPermisstion(17018, Program.m_OperID);
			ToolStripMenuItem tSMI_Item = TSMI_Item;
			bool visible = (TSMIShop.Visible = SQLserver.GetUserPermisstion(1048, Program.m_OperID));
			tSMI_Item.Visible = visible;
			if (Program.m_OperID.ToUpper() == "ADMINS")
			{
				TSMIExportRooms.Visible = true;
				toolStripSeparator17.Visible = true;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)Program.m_hPubTab["InfoPermission"] + ex.Message, MessageBoxIcon.Hand);
		}
		tBtnLogout.Text = TSMIExit.Text;
		tBtnReadCard.Text = TSMICardRead.Text;
		tBtnMain.Text = TSMIRCenter.Text;
		tBtnBook.Text = TSMIBR.Text;
		tBtnTeam.Text = TSMITeamCI.Text;
		tBtnSGuest.Text = TSMISGuest.Text;
		tBtnSRoom.Text = TSMISRoom.Text;
		if (tBtnMain.Enabled)
		{
			tBtnMain_Click(null, null);
		}
		TSSLRState_Click(null, null);
		TSMIRC_CC.Checked = true;
		TSMIRC_GC.CheckedChanged -= TSMIRC_GC_CheckedChanged;
		TSMIRC_GC.Checked = Program.showOldMSG;
		tmChkGuest.Enabled = Program.showOldMSG;
		TSMIRC_GC.CheckedChanged += TSMIRC_GC_CheckedChanged;
	}

	public bool OpenFrm(Form frm)
	{
		Form[] mdiChildren = base.MdiChildren;
		foreach (Form form in mdiChildren)
		{
			if (frm.Name == form.Name)
			{
				form.Activate();
				return false;
			}
		}
		return true;
	}

	private void TSMIHInfo_Click(object sender, EventArgs e)
	{
		frmHotelInfo frmHotelInfo2 = new frmHotelInfo();
		frmHotelInfo2.ShowDialog();
		refreshroom();
	}

	private void refreshroom()
	{
		frmCenter frmCenter2 = null;
		Form[] mdiChildren = base.MdiChildren;
		foreach (Form form in mdiChildren)
		{
			if (form is frmCenter)
			{
				frmCenter2 = (frmCenter)form;
				break;
			}
		}
		frmCenter2?.refreshRoomList();
	}

	private void TSMICer_Click(object sender, EventArgs e)
	{
		frmCerType frmCerType2 = new frmCerType();
		frmCerType2.ShowDialog();
	}

	private void TSMIRoomType_Click(object sender, EventArgs e)
	{
		frmRoomType frmRoomType2 = new frmRoomType();
		frmRoomType2.ShowDialog();
	}

	private void TSMICurrType_Click(object sender, EventArgs e)
	{
		frmCurrency frmCurrency2 = new frmCurrency();
		frmCurrency2.ShowDialog();
	}

	private void TSMIRoomSta_Click(object sender, EventArgs e)
	{
		frmRoomStatus frmRoomStatus2 = new frmRoomStatus();
		frmRoomStatus2.ShowDialog();
	}

	private void TSMIBF_Click(object sender, EventArgs e)
	{
		frmBuildFloor frmBuildFloor2 = new frmBuildFloor();
		frmBuildFloor2.ShowDialog();
	}

	private void TSMIRooms_Click(object sender, EventArgs e)
	{
		frmRooms frmRooms2 = new frmRooms();
		if (OpenFrm(frmRooms2))
		{
			frmRooms2.MdiParent = this;
			frmRooms2.Show();
		}
	}

	private void tBtnMain_Click(object sender, EventArgs e)
	{
		frmCenter frmCenter2 = new frmCenter();
		if (OpenFrm(frmCenter2))
		{
			frmCenter2.MdiParent = this;
			ActivateMdiChild(frmCenter2);
			frmCenter2.Show();
		}
		else
		{
			frmCenter2.Close();
		}
	}

	private void TSSLRSt_Click(object sender, EventArgs e)
	{
		int num = 0;
		if (TSSLRSt.Text == (string)Program.m_hPubTab["InfoDevSC"])
		{
			global::Dev_C_Sharp.Dev_C_Sharp.Instance.ClosePort(Program.m_DevCOM);
			TSSLRSt.Image = Resources.delete;
			TSSLRSt.Text = (string)Program.m_hPubTab["InfoDevSB"];
			return;
		}
		num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.OpenPort(Program.m_DevCOM, Program.m_DevBaud, buzzer: false);
		if (num != 0)
		{
			Program.MsgBox((string)Program.m_hPubTab["InfoDevConn"] + num, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Program.RadioDevBuzzer(1, 2);
		TSSLRSt.Image = Resources.Button_Refresh;
		TSSLRSt.Text = (string)Program.m_hPubTab["InfoDevSC"];
	}

	private void TSMILogout_Click(object sender, EventArgs e)
	{
		if (Program.fpop != null)
		{
			Program.fpop.Close();
			Program.fpop = null;
		}
		Close();
	}

	private void TSMIExit_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)Program.m_hPubTab["InfoExit"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
		{
			Program.m_Exit = true;
			Application.Exit();
		}
	}

	private void TSMIRCenter_Click(object sender, EventArgs e)
	{
		tBtnMain_Click(null, null);
	}

	private void tBtnReadCard_Click(object sender, EventArgs e)
	{
		object[] retdata = new object[256];
		Program.RadioReadCard(retdata, Buzzer: true, 1);
	}

	private void TSMICardMgr_Click(object sender, EventArgs e)
	{
		frmMCMgr frmMCMgr2 = new frmMCMgr();
		frmMCMgr2.ShowDialog();
	}

	private void TSMICardEmp_Click(object sender, EventArgs e)
	{
		frmECMgr frmECMgr2 = new frmECMgr();
		frmECMgr2.ShowDialog();
	}

	private void TSMICardRead_Click(object sender, EventArgs e)
	{
		object[] retdata = new object[256];
		Program.RadioReadCard(retdata, Buzzer: true, 1);
	}

	private void TSMICardLogout_Click(object sender, EventArgs e)
	{
		object[] retdata = new object[256];
		Program.RadioReadCard(retdata, Buzzer: true, 2);
	}

	private void tBtnLogout_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)Program.m_hPubTab["InfoExit"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
		{
			Program.m_Exit = true;
			Application.Exit();
		}
	}

	private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			global::Dev_C_Sharp.Dev_C_Sharp.Instance.ClosePort(Program.m_DevCOM);
			TSSLRState.Image = Resources.delete;
			TSSLRState.Text = (string)Program.m_hPubTab["InfoDevSB"];
			TSSLRState.LinkColor = Color.Maroon;
			TSSLRState.BorderStyle = Border3DStyle.Raised;
			Timer timer = tmChkGuest;
			bool enabled = (tmSys.Enabled = false);
			timer.Enabled = enabled;
			tmChkGuest.Dispose();
			tmSys.Dispose();
		}
		catch
		{
		}
	}

	private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
	{
		Program.m_hPubTab.Clear();
		Program.m_hPubTab = null;
		if (Program.fpop != null && !Program.fpop.Disposing && !Program.fpop.IsDisposed)
		{
			Program.fpop.Close();
		}
		Program.fpop = null;
	}

	private void TSMISGuest_Click(object sender, EventArgs e)
	{
		frmSGuest frmSGuest2 = new frmSGuest();
		if (OpenFrm(frmSGuest2))
		{
			frmSGuest2.MdiParent = this;
			frmSGuest2.Show();
		}
	}

	private void TSMIUPWD_Click(object sender, EventArgs e)
	{
		updatepas updatepas2 = new updatepas();
		updatepas2.ShowDialog();
	}

	private void TSMISRoom_Click(object sender, EventArgs e)
	{
		frmSRoom frmSRoom2 = new frmSRoom();
		if (OpenFrm(frmSRoom2))
		{
			frmSRoom2.MdiParent = this;
			frmSRoom2.Show();
		}
	}

	private void tBtnSGuest_Click(object sender, EventArgs e)
	{
		frmSGuest frmSGuest2 = new frmSGuest();
		if (OpenFrm(frmSGuest2))
		{
			frmSGuest2.MdiParent = this;
			frmSGuest2.Show();
		}
	}

	private void tBtnSRoom_Click(object sender, EventArgs e)
	{
		frmSRoom frmSRoom2 = new frmSRoom();
		if (OpenFrm(frmSRoom2))
		{
			frmSRoom2.MdiParent = this;
			frmSRoom2.Show();
		}
	}

	private void tmSys_Tick(object sender, EventArgs e)
	{
		tmSys.Enabled = false;
		TSSLSystime.Text = Program.GetLocDTime(DateTime.Now, "ss");
		tmSys.Enabled = true;
	}

	private void TSSLRState_Click(object sender, EventArgs e)
	{
		int num = 0;
		if (TSSLRState.Text == (string)Program.m_hPubTab["InfoDevSC"])
		{
			global::Dev_C_Sharp.Dev_C_Sharp.Instance.ClosePort(Program.m_DevCOM);
			TSSLRState.Image = Resources.v_break;
			TSSLRState.Text = (string)Program.m_hPubTab["InfoDevSB"];
			TSSLRState.LinkColor = Color.Maroon;
			TSSLRState.BorderStyle = Border3DStyle.Raised;
			return;
		}
		num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.OpenPort(Program.m_DevCOM, Program.m_DevBaud, buzzer: true);
		if (num < 4)
		{
			Program.MsgBox((string)Program.m_hPubTab["InfoDevConn"] + num, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		TSSLRState.Image = Resources.v_conn;
		TSSLRState.Text = (string)Program.m_hPubTab["InfoDevSC"];
		TSSLRState.LinkColor = Color.DarkGreen;
		TSSLRState.BorderStyle = Border3DStyle.Sunken;
	}

	private void TSSLRState_MouseEnter(object sender, EventArgs e)
	{
		TSSLRState.BackColor = SystemColors.ButtonHighlight;
	}

	private void TSSLRState_MouseLeave(object sender, EventArgs e)
	{
		TSSLRState.BackColor = SystemColors.Control;
	}

	private void TSMIGrp_Click(object sender, EventArgs e)
	{
		frmGrpRoom frmGrpRoom2 = new frmGrpRoom();
		frmGrpRoom2.ShowDialog();
	}

	private void tBtnTeam_Click(object sender, EventArgs e)
	{
		frmTeam frmTeam2 = new frmTeam();
		frmTeam2.Text = TSMITeamCI.Text;
		if (OpenFrm(frmTeam2))
		{
			frmTeam2.MdiParent = this;
			frmTeam2.Show();
		}
	}

	private void TSMITeamCI_Click(object sender, EventArgs e)
	{
		tBtnTeam_Click(null, null);
	}

	private void TSMITeam_Click(object sender, EventArgs e)
	{
		frmSTour frmSTour2 = new frmSTour();
		if (OpenFrm(frmSTour2))
		{
			frmSTour2.MdiParent = this;
			frmSTour2.Show();
		}
	}

	private void TSMIView02_Click(object sender, EventArgs e)
	{
		try
		{
			if (!TSMIView02.Checked)
			{
				TSMIView02.Image = Resources.GuideDown;
			}
			else
			{
				TSMIView02.Image = Resources.GuideUp;
			}
			Program.MDIFrm_Center_Room_Refresh(base.MdiChildren);
		}
		catch
		{
		}
	}

	private void tBtnBook_Click(object sender, EventArgs e)
	{
		frmGBR frmGBR2 = new frmGBR();
		frmGBR2.Text = TSMIBR.Text;
		frmGBR2.ShowDialog();
	}

	private void TSMIBMSingle_Click(object sender, EventArgs e)
	{
		frmGBR frmGBR2 = new frmGBR();
		frmGBR2.Text = TSMIBMSingle.Text;
		frmGBR2.ShowDialog();
	}

	private void TSMIBRCancel_Click(object sender, EventArgs e)
	{
		frmGBRCancel frmGBRCancel2 = new frmGBRCancel();
		frmGBRCancel2.ShowDialog();
	}

	private void TSMIBRCheckIn_Click(object sender, EventArgs e)
	{
		frmGBRCheckIn frmGBRCheckIn2 = new frmGBRCheckIn();
		frmGBRCheckIn2.ShowDialog();
	}

	private void TSMIUPGMgr_Click(object sender, EventArgs e)
	{
		grouppermission grouppermission2 = new grouppermission();
		grouppermission2.ShowDialog();
	}

	private void TSMIUMgr_Click(object sender, EventArgs e)
	{
		updateuser updateuser2 = new updateuser();
		updateuser2.ShowDialog();
	}

	private void TSMIUGMgr_Click(object sender, EventArgs e)
	{
		updategroup updategroup2 = new updategroup();
		updategroup2.ShowDialog();
	}

	private void TSMIUPMgr_Click(object sender, EventArgs e)
	{
		userpermission userpermission2 = new userpermission();
		userpermission2.ShowDialog();
	}

	private void TSMILL_Click(object sender, EventArgs e)
	{
		frmLockLog frmLockLog2 = new frmLockLog();
		frmLockLog2.Text = TSMILL.Text;
		if (OpenFrm(frmLockLog2))
		{
			frmLockLog2.MdiParent = this;
			frmLockLog2.Show();
		}
	}

	private void TSMIDB_Click(object sender, EventArgs e)
	{
		frmDataBaseMgr frmDataBaseMgr2 = new frmDataBaseMgr();
		frmDataBaseMgr2.ShowDialog();
	}

	private void TSMISMCard_Click(object sender, EventArgs e)
	{
		frmSMCard frmSMCard2 = new frmSMCard();
		frmSMCard2.Text = TSMISMCard.Text;
		if (OpenFrm(frmSMCard2))
		{
			frmSMCard2.MdiParent = this;
			frmSMCard2.Show();
		}
	}

	private void TSMISGSCard_Click(object sender, EventArgs e)
	{
		frmSGSCard frmSGSCard2 = new frmSGSCard();
		if (OpenFrm(frmSGSCard2))
		{
			frmSGSCard2.MdiParent = this;
			frmSGSCard2.Show();
		}
	}

	private void TSMIAbout_Click(object sender, EventArgs e)
	{
		frmAbout frmAbout2 = new frmAbout();
		frmAbout2.ShowDialog();
	}

	private void TSMILanMgr_Click(object sender, EventArgs e)
	{
		frmLanModify frmLanModify2 = new frmLanModify();
		frmLanModify2.ShowDialog();
	}

	private void tmChkGuest_Tick(object sender, EventArgs e)
	{
		m_RChkRun = true;
		DataTable dataTable = null;
		try
		{
			tmChkGuest.Enabled = false;
			tmChkGuest.Interval = 600000;
			DateTime now = DateTime.Now;
			string text = "SELECT T_Rooms.*, (Case T_Rooms.TR_stayover When 1 then T_Rooms.TR_SOLTime Else T_Rooms.TR_stand_L_time End) As CurLT, IsNull(T_Team.team_name,'') As team_name, IsNull(T_Team.team_guide,'') As team_guide, IsNull(T_Team.team_tel,'') As team_tel FROM T_Rooms ";
			text += " LEFT OUTER JOIN T_Team ON T_Rooms.team_id = T_Team.team_id";
			object obj = text;
			text = string.Concat(obj, " where T_Rooms.TR_Level = 0 And (Case T_Rooms.TR_stayover When 1 then T_Rooms.TR_SOLTime Else T_Rooms.TR_stand_L_time End) <'", now.AddMinutes(30.0), "'");
			text += " Order by T_Rooms.TR_cometime";
			dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				string text2 = "";
				bool flag = true;
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					text2 = dataTable.Rows[i]["r_name"].ToString().Trim();
					if (!m_PopOldMess && cur_rnList.Contains(text2))
					{
						continue;
					}
					if (flag)
					{
						if (Program.fpop != null)
						{
							Program.fpop.Close();
							Program.fpop = null;
						}
						flag = false;
					}
					if (Program.fpop == null)
					{
						Program.fpop = new frmPop();
						Program.fpop.TopMost = true;
						Program.fpop.Show();
					}
					Program.fpop.BringToFront();
					guestListCls guestListCls2 = new guestListCls();
					bool flag2 = false;
					double num = Convert.ToDouble(dataTable.Rows[i]["TR_Stayhour"]);
					Convert.ToInt32(dataTable.Rows[i]["Tr_sohour"]);
					if (num == 0.0)
					{
						flag2 = true;
					}
					else
					{
						num = Program.CountDay(Convert.ToDateTime(dataTable.Rows[i]["TR_cometime"]), now);
					}
					guestListCls2.c_rn = dataTable.Rows[i]["r_name"].ToString().Trim();
					guestListCls2.c_comedate = dataTable.Rows[i]["TR_cometime"].ToString().Trim();
					guestListCls2.c_hr = flag2;
					guestListCls2.c_leveldate = dataTable.Rows[i]["CurLT"].ToString();
					int num2 = (int)(now - DateTime.Parse(guestListCls2.c_comedate)).TotalHours;
					if (!flag2)
					{
						guestListCls2.c_gsd = num;
						guestListCls2.c_total = Convert.ToDouble(dataTable.Rows[i]["r_price"]) * Convert.ToDouble(dataTable.Rows[i]["tr_discount"]) * num;
					}
					else
					{
						guestListCls2.c_gsd = num2;
						guestListCls2.c_total = ((num2 > Program.m_defHR) ? ((double)Program.m_defHR * Convert.ToDouble(dataTable.Rows[i]["r_price"]) * Convert.ToDouble(dataTable.Rows[i]["tr_discount"]) + (double)(num2 - Program.m_defHR) * Convert.ToDouble(dataTable.Rows[i]["r_price"]) * Convert.ToDouble(dataTable.Rows[i]["tr_discount"])) : ((double)Program.m_defHR * Convert.ToDouble(dataTable.Rows[i]["r_price"]) * Convert.ToDouble(dataTable.Rows[i]["tr_discount"])));
					}
					guestListCls2.c_paid = Convert.ToDouble(dataTable.Rows[i]["TR_Deposit"].ToString());
					guestListCls2.c_tcc = dataTable.Rows[i]["TR_Bascurname"].ToString().Trim();
					guestListCls2.c_pcc = dataTable.Rows[i]["curr_code"].ToString().Trim();
					guestListCls2.date = DateTime.Now;
					if (dataTable.Rows[i]["team_name"].ToString().Trim() != "")
					{
						guestListCls2.c_team = true;
						guestListCls2.c_teamname = dataTable.Rows[i]["team_name"].ToString().Trim();
						guestListCls2.c_teamguide = dataTable.Rows[i]["team_guide"].ToString().Trim();
						guestListCls2.c_teamtel = dataTable.Rows[i]["team_tel"].ToString().Trim();
					}
					Program.fpop.rnlist.Add(guestListCls2);
				}
			}
			if (dataTable != null)
			{
				dataTable.Clear();
				dataTable.Dispose();
			}
		}
		catch (Exception ex)
		{
			dataTable?.Dispose();
			string text3 = string.Format((string)Program.m_hPubTab["ErrRChkG"], "\r\n");
			text3 += ex.Message;
			Console.Write(text3.ToString());
		}
		m_RChkRun = false;
		tmChkGuest.Enabled = TSMIRC_GC.Checked;
	}

	private void tmChkReg_Tick(object sender, EventArgs e)
	{
		tmChkReg.Enabled = false;
		tmChkReg.Interval = 600000;
		string text = "";
		int num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.ChkReg(Program.m_regID, Program.m_regKey, chkid: false);
		if (num <= 0)
		{
			if (Program.MsgBox((string)Program.m_hPubTab["InfoReg03"] + "\r\n" + text, "System Register", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				frmAbout frmAbout2 = new frmAbout();
				frmAbout2.ShowDialog();
			}
			Program.m_Exit = true;
			Application.Exit();
		}
		else if (num <= 10 && m_reginfo)
		{
			Program.MsgBox(string.Format((string)Program.m_hPubTab["InfoReg04"], num), (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		tmChkReg.Enabled = true;
	}

	private void tmChkRS_Tick(object sender, EventArgs e)
	{
		DataTable dataTable = null;
		try
		{
			tmChkRS.Enabled = false;
			tmChkRS.Interval = 3000;
			string standDTime = Program.GetStandDTime(DateTime.Now);
			string sqlquery = "Update D_Rooms Set R_RSID = 1 Where R_RSID = 2 And DATEDIFF(n, R_Updatetime, '" + standDTime + "') >= " + Program.m_defClearTime;
			int num = Program.DBCompExec(sqlquery, "");
			sqlquery = "update D_Rooms set R_RSID = 10 from T_Schedule T,D_Rooms D where T.r_id = D.R_ID and D.R_RSID = 1 and sch_flag = 0 and dateadd(n,-60-" + Program.m_defClearTime + ",g_come_day + ' ' + g_come_time) > GETDATE() \n";
			object obj = sqlquery;
			sqlquery = string.Concat(obj, "update D_Rooms set R_RSID = 3 from T_Schedule T,D_Rooms D where T.r_id = D.R_ID and (D.R_RSID = 10 or D.R_RSID = 1) and sch_flag = 0 and dateadd(n,-60-", Program.m_defClearTime, ",g_come_day + ' ' + g_come_time) < GETDATE() and getdate() <g_come_day + ' ' + g_come_time \n");
			sqlquery += "update D_Rooms set R_RSID = 11 from T_Schedule T,D_Rooms D where T.r_id = D.R_ID and (D.R_RSID = 3 or D.R_RSID = 1 or D.R_RSID = 10) and sch_flag = 0 and getdate()> g_come_day + ' ' + g_come_time";
			if (Program.DBCompExec(sqlquery, "") + num > 0)
			{
				Program.MDIFrm_Center_Room_Refresh(base.MdiChildren);
			}
		}
		catch (Exception ex)
		{
			dataTable?.Dispose();
			string text = string.Format((string)Program.m_hPubTab["ErrRChkS"], "\r\n");
			text += ex.Message;
			Console.Write(text.ToString());
		}
		tmChkRS.Enabled = true;
	}

	private void TSMIRC_CC_CheckedChanged(object sender, EventArgs e)
	{
		tmChkRS.Enabled = false;
		tmChkRS.Enabled = TSMIRC_CC.Checked;
		tmChkRS.Interval = 1000;
	}

	private void TSMIRC_GC_CheckedChanged(object sender, EventArgs e)
	{
		if (TSMIRC_GC.Checked)
		{
			if (!m_RChkRun)
			{
				tmChkGuest.Enabled = false;
				tmChkGuest.Interval = 500;
				tmChkGuest.Enabled = true;
				Program.showOldMSG = true;
			}
		}
		else
		{
			tmChkGuest.Enabled = false;
			Program.showOldMSG = false;
		}
		Program.SetSingleItem("ShowOldMSG", Program.showOldMSG ? "1" : "0");
	}

	private void TSMIOther_Click(object sender, EventArgs e)
	{
		frmOthSetting frmOthSetting2 = new frmOthSetting();
		frmOthSetting2.Text = TSMIOther.Text;
		if (OpenFrm(frmOthSetting2))
		{
			frmOthSetting2.MdiParent = this;
			frmOthSetting2.Show();
		}
	}

	private void TSMIOtherList_Click(object sender, EventArgs e)
	{
		frmSOth frmSOth2 = new frmSOth();
		frmSOth2.Text = TSMIOtherList.Text;
		if (OpenFrm(frmSOth2))
		{
			frmSOth2.MdiParent = this;
			frmSOth2.Show();
		}
	}

	private void TSMITGLog_Click(object sender, EventArgs e)
	{
		frmSTGLogcs frmSTGLogcs2 = new frmSTGLogcs();
		frmSTGLogcs2.Text = TSMITGLog.Text;
		if (OpenFrm(frmSTGLogcs2))
		{
			frmSTGLogcs2.MdiParent = this;
			frmSTGLogcs2.Show();
		}
	}

	private void TSMIHR_Click(object sender, EventArgs e)
	{
		frmHRMgr frmHRMgr2 = new frmHRMgr();
		frmHRMgr2.ShowDialog();
	}

	private void TSMISoftIns_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			text = ((Program.m_Lan == 1) ? "SoftHelp-cn.chm" : ((Program.m_Lan != 2) ? "SoftHelp-en.chm" : "SoftHelp-tc.chm"));
			Process process = new Process();
			process.StartInfo.WorkingDirectory = Program.m_AppPath;
			process.StartInfo.FileName = text;
			process.StartInfo.CreateNoWindow = true;
			process.Start();
		}
		catch (Exception ex)
		{
			Program.MsgCustom(ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void TSMITeamInfo_Click(object sender, EventArgs e)
	{
		frmSTB frmSTB2 = new frmSTB();
		frmSTB2.Text = TSMITeamInfo.Text;
		if (OpenFrm(frmSTB2))
		{
			frmSTB2.MdiParent = this;
			frmSTB2.Show();
		}
	}

	private void TSMIShop_Click(object sender, EventArgs e)
	{
		frmOther frmOther2 = new frmOther();
		frmOther2.Text = TSMI_Item.Text;
		frmOther2.txtRoom.Text = "";
		frmOther2.ShowDialog();
	}

	private void frmMain_SizeChanged(object sender, EventArgs e)
	{
		cbpTop.AutoSize = false;
		cbpTop.AutoSize = true;
	}

	private void TSMIExportParameters_Click(object sender, EventArgs e)
	{
		byte[] array = new byte[10];
		byte[] array2 = new byte[10];
		int saler = 0;
		int hotelid = 0;
		global::Dev_C_Sharp.Dev_C_Sharp.Instance.GetDevParms(array, array2, ref saler, ref hotelid);
		byte[] bytes = Encoding.UTF8.GetBytes(array[0].ToString("X2") + saler.ToString("X2") + hotelid.ToString("X4") + array2[0].ToString("x2") + array2[1].ToString("X2"));
		new FileStream(Environment.CurrentDirectory + "\\Set.dat", FileMode.Create).Write(bytes, 0, bytes.Length);
		MessageBox.Show("ver " + array[0].ToString("X2") + "\nvender " + saler.ToString("X2") + "\nhotelid " + hotelid.ToString("X4") + "\npwd " + array2[0].ToString("x2") + array2[1].ToString("X2"));
	}

	private void TSMIExportRooms_Click(object sender, EventArgs e)
	{
		try
		{
			string sql = "Select R_Name, R_Code, R_SubCode,Build_Name,Build_Code, Floor_Name,Floor_Code, TP_Name, R_TypeID as TP_ID,R_Size,R_SubCodeDai as R_Baseband, R_Memo as R_Comment From v_HotelRooms Where IsNull(Floor_Flag,0) = 0 And IsNull(Build_Flag,0) = 0 And R_flag = 0 Order by Build_Name, Floor_Name, R_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			XmlNodeList elements = new ClassXml(Program.m_lansDt.Rows[Program.m_Lan]["fpath"].ToString(), "Radio").GetElements("Radio/Info_Public/Info_Text");
			foreach (XmlNode item in elements)
			{
				try
				{
					dataTable.Columns[item.Attributes["ColumnName"].Value].ColumnName = item.Attributes["Text"].Value;
				}
				catch
				{
				}
			}
			ClsComm.ExportToExcel(dataTable);
		}
		catch
		{
		}
	}

	private void TSMIExportGroups_Click(object sender, EventArgs e)
	{
		string sql = "Select RGT_name as Group_Name,RGT_code as Group_Code,a.Build_Name,Build_Code, a.Floor_Name,Floor_Code,a.R_Name, R_Code, R_SubCode From v_HotelRooms as a,v_GrpRoom as b Where (IsNull(Build_Flag,0) = 0 And IsNull(Floor_Flag,0) = 0 And IsNull(R_flag,0) = 0 And IsNull(RG_Flag,0) = 0)and a.R_ID=b.r_id Order by Group_Code";
		DataTable dataTable = SQLserver.Data_GetDataTable(sql);
		XmlNodeList elements = new ClassXml(Program.m_lansDt.Rows[Program.m_Lan]["fpath"].ToString(), "Radio").GetElements("Radio/Info_Public/Info_Text");
		foreach (XmlNode item in elements)
		{
			try
			{
				dataTable.Columns[item.Attributes["ColumnName"].Value].ColumnName = item.Attributes["Text"].Value;
			}
			catch
			{
			}
		}
		ClsComm.ExportToExcel(dataTable);
	}

	private void TSMIImportRooms_Click(object sender, EventArgs e)
	{
	}

	private void TSMIDisplaySet_Click(object sender, EventArgs e)
	{
	}

	private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.Cascade);
	}

	private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.TileVertical);
	}

	private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.TileHorizontal);
	}

	private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.ArrangeIcons);
	}

	private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Form[] mdiChildren = base.MdiChildren;
		foreach (Form form in mdiChildren)
		{
			form.Close();
		}
	}

	private void ShowNewForm(object sender, EventArgs e)
	{
		Form form = new Form();
		form.MdiParent = this;
		form.Text = "窗口 " + childFormNumber++;
		form.Show();
	}

	private void OpenFile(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
		if (openFileDialog.ShowDialog(this) == DialogResult.OK)
		{
			_ = openFileDialog.FileName;
		}
	}

	private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
		if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
		{
			_ = saveFileDialog.FileName;
		}
	}
}
