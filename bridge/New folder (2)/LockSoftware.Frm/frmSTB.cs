using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmSTB : Form
{
	private IContainer components;

	public GlassBtn btnCl;

	public GlassBtn btnOK;

	private DataGridView dgvList;

	public FlowLayoutPanel flowLayoutPanel1;

	private GlassBtn btnSearch;

	private GlassBtn btnExport;

	private GlassBtn btnReset;

	public Panel panel2;

	private Label label7;

	private TextBox txtTGG;

	private DateTimePicker dtpComeE;

	private Label label10;

	private ComboBox cobTG;

	private Label label9;

	private ComboBox cobTB;

	private DateTimePicker dtpComeS;

	private Label labArr;

	private Label label4;

	private ComboBox cobCer;

	private ComboBox cobUser;

	private TextBox txtCernum;

	private Label label26;

	private Label label6;

	private Label label27;

	public DateTimePicker dtpLevelS;

	public Label label29;

	public Label label5;

	public DateTimePicker dtpLevelE;

	public GlassBtn btnClose;

	public clsBackPanel clsBackPanel1;

	private Panel panel1;

	private Panel panel3;

	public string m_sqlstr = "";

	public string m_objName = "WFsg";

	public Hashtable m_htab;

	public long m_tid = -1L;

	public string m_guide = "";

	public string m_gcer = "";

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmSTB));
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnExport = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.labArr = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpComeE = new System.Windows.Forms.DateTimePicker();
		this.cobUser = new System.Windows.Forms.ComboBox();
		this.dtpComeS = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.dtpLevelE = new System.Windows.Forms.DateTimePicker();
		this.dtpLevelS = new System.Windows.Forms.DateTimePicker();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label9 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.txtTGG = new System.Windows.Forms.TextBox();
		this.cobTB = new System.Windows.Forms.ComboBox();
		this.txtCernum = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.cobTG = new System.Windows.Forms.ComboBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.flowLayoutPanel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 190);
		this.dgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dgvList.MultiSelect = false;
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersVisible = false;
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(1008, 377);
		this.dgvList.TabIndex = 2;
		this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellClick);
		this.dgvList.SelectionChanged += new System.EventHandler(dgvList_SelectionChanged);
		this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanel1.Controls.Add(this.btnSearch);
		this.flowLayoutPanel1.Controls.Add(this.btnExport);
		this.flowLayoutPanel1.Controls.Add(this.btnReset);
		this.flowLayoutPanel1.Controls.Add(this.btnClose);
		this.flowLayoutPanel1.Location = new System.Drawing.Point(507, 126);
		this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(498, 56);
		this.flowLayoutPanel1.TabIndex = 84;
		this.flowLayoutPanel1.Visible = false;
		this.btnSearch.AutoEllipsis = true;
		this.btnSearch.BackColor = System.Drawing.Color.LightGray;
		this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnSearch.ForeColor = System.Drawing.Color.Black;
		this.btnSearch.GlowColor = System.Drawing.Color.White;
		this.btnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSearch.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSearch.Location = new System.Drawing.Point(3, 5);
		this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnSearch.Size = new System.Drawing.Size(103, 44);
		this.btnSearch.TabIndex = 76;
		this.btnSearch.Text = "Search";
		this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
		this.btnExport.AutoSize = true;
		this.btnExport.BackColor = System.Drawing.Color.LightGray;
		this.btnExport.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnExport.ForeColor = System.Drawing.Color.Black;
		this.btnExport.GlowColor = System.Drawing.Color.White;
		this.btnExport.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnExport.Image = LockSoftware.Properties.Resources.xls;
		this.btnExport.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnExport.Location = new System.Drawing.Point(112, 5);
		this.btnExport.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnExport.Name = "btnExport";
		this.btnExport.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnExport.Size = new System.Drawing.Size(149, 44);
		this.btnExport.TabIndex = 77;
		this.btnExport.Text = "Export To Excel";
		this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.btnReset.AutoSize = true;
		this.btnReset.BackColor = System.Drawing.Color.LightGray;
		this.btnReset.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnReset.ForeColor = System.Drawing.Color.Black;
		this.btnReset.GlowColor = System.Drawing.Color.White;
		this.btnReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReset.Image = LockSoftware.Properties.Resources.clear;
		this.btnReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnReset.Location = new System.Drawing.Point(267, 5);
		this.btnReset.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnReset.Name = "btnReset";
		this.btnReset.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnReset.Size = new System.Drawing.Size(84, 44);
		this.btnReset.TabIndex = 78;
		this.btnReset.Text = "Reset";
		this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(357, 5);
		this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnClose.Size = new System.Drawing.Size(79, 44);
		this.btnClose.TabIndex = 79;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.panel2.AutoScroll = true;
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Controls.Add(this.panel1);
		this.panel2.Controls.Add(this.flowLayoutPanel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1008, 190);
		this.panel2.TabIndex = 85;
		this.panel2.Visible = false;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.labArr);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.dtpComeE);
		this.panel3.Controls.Add(this.cobUser);
		this.panel3.Controls.Add(this.dtpComeS);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.label29);
		this.panel3.Controls.Add(this.dtpLevelE);
		this.panel3.Controls.Add(this.dtpLevelS);
		this.panel3.Location = new System.Drawing.Point(507, 3);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(498, 116);
		this.panel3.TabIndex = 89;
		this.labArr.BackColor = System.Drawing.Color.Transparent;
		this.labArr.Location = new System.Drawing.Point(3, 3);
		this.labArr.Name = "labArr";
		this.labArr.Size = new System.Drawing.Size(136, 36);
		this.labArr.TabIndex = 63;
		this.labArr.Text = "Checking In:";
		this.labArr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(296, 14);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(19, 15);
		this.label4.TabIndex = 71;
		this.label4.Text = "→";
		this.label6.Location = new System.Drawing.Point(3, 75);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(136, 36);
		this.label6.TabIndex = 74;
		this.label6.Text = "Operator:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpComeE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeE.Location = new System.Drawing.Point(321, 9);
		this.dtpComeE.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.dtpComeE.Name = "dtpComeE";
		this.dtpComeE.ShowCheckBox = true;
		this.dtpComeE.Size = new System.Drawing.Size(145, 21);
		this.dtpComeE.TabIndex = 66;
		this.cobUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobUser.DropDownWidth = 142;
		this.cobUser.FormattingEnabled = true;
		this.cobUser.Location = new System.Drawing.Point(145, 81);
		this.cobUser.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.cobUser.Name = "cobUser";
		this.cobUser.Size = new System.Drawing.Size(145, 23);
		this.cobUser.TabIndex = 75;
		this.dtpComeS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeS.Location = new System.Drawing.Point(145, 9);
		this.dtpComeS.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.dtpComeS.Name = "dtpComeS";
		this.dtpComeS.ShowCheckBox = true;
		this.dtpComeS.Size = new System.Drawing.Size(145, 21);
		this.dtpComeS.TabIndex = 62;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(296, 50);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(19, 15);
		this.label5.TabIndex = 72;
		this.label5.Text = "→";
		this.label29.BackColor = System.Drawing.Color.Transparent;
		this.label29.Location = new System.Drawing.Point(3, 39);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(136, 36);
		this.label29.TabIndex = 65;
		this.label29.Text = "Checking Out:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpLevelE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelE.Location = new System.Drawing.Point(321, 45);
		this.dtpLevelE.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.dtpLevelE.Name = "dtpLevelE";
		this.dtpLevelE.ShowCheckBox = true;
		this.dtpLevelE.Size = new System.Drawing.Size(145, 21);
		this.dtpLevelE.TabIndex = 73;
		this.dtpLevelS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelS.Location = new System.Drawing.Point(145, 45);
		this.dtpLevelS.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.dtpLevelS.Name = "dtpLevelS";
		this.dtpLevelS.ShowCheckBox = true;
		this.dtpLevelS.Size = new System.Drawing.Size(145, 21);
		this.dtpLevelS.TabIndex = 64;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label9);
		this.panel1.Controls.Add(this.label7);
		this.panel1.Controls.Add(this.cobCer);
		this.panel1.Controls.Add(this.txtTGG);
		this.panel1.Controls.Add(this.cobTB);
		this.panel1.Controls.Add(this.txtCernum);
		this.panel1.Controls.Add(this.label26);
		this.panel1.Controls.Add(this.cobTG);
		this.panel1.Controls.Add(this.label27);
		this.panel1.Controls.Add(this.label10);
		this.panel1.Location = new System.Drawing.Point(3, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(498, 116);
		this.panel1.TabIndex = 88;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label9.Location = new System.Drawing.Point(3, 39);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(136, 36);
		this.label9.TabIndex = 84;
		this.label9.Text = "Tour Group Name:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label7.Location = new System.Drawing.Point(3, 3);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(136, 36);
		this.label7.TabIndex = 82;
		this.label7.Text = "Travel Bureau:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 160;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(379, 11);
		this.cobCer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(104, 23);
		this.cobCer.TabIndex = 59;
		this.txtTGG.Location = new System.Drawing.Point(145, 83);
		this.txtTGG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtTGG.Name = "txtTGG";
		this.txtTGG.Size = new System.Drawing.Size(104, 21);
		this.txtTGG.TabIndex = 87;
		this.cobTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTB.FormattingEnabled = true;
		this.cobTB.Location = new System.Drawing.Point(145, 11);
		this.cobTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobTB.Name = "cobTB";
		this.cobTB.Size = new System.Drawing.Size(104, 23);
		this.cobTB.TabIndex = 83;
		this.cobTB.SelectedIndexChanged += new System.EventHandler(cobTB_SelectedIndexChanged);
		this.txtCernum.Location = new System.Drawing.Point(379, 47);
		this.txtCernum.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.txtCernum.Name = "txtCernum";
		this.txtCernum.Size = new System.Drawing.Size(104, 21);
		this.txtCernum.TabIndex = 58;
		this.label26.BackColor = System.Drawing.Color.Transparent;
		this.label26.Location = new System.Drawing.Point(255, 3);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(118, 36);
		this.label26.TabIndex = 60;
		this.label26.Text = "Certificate:";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobTG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTG.FormattingEnabled = true;
		this.cobTG.Location = new System.Drawing.Point(145, 47);
		this.cobTG.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobTG.Name = "cobTG";
		this.cobTG.Size = new System.Drawing.Size(104, 23);
		this.cobTG.TabIndex = 85;
		this.label27.BackColor = System.Drawing.Color.Transparent;
		this.label27.Location = new System.Drawing.Point(255, 39);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(118, 36);
		this.label27.TabIndex = 61;
		this.label27.Text = "Number:";
		this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label10.Location = new System.Drawing.Point(3, 75);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(136, 36);
		this.label10.TabIndex = 86;
		this.label10.Text = "Tour Group Guide:";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.clsBackPanel1.Border = true;
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.FromArgb(224, 224, 224);
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.btnCl);
		this.clsBackPanel1.Controls.Add(this.btnOK);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 567);
		this.clsBackPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(1008, 51);
		this.clsBackPanel1.TabIndex = 0;
		this.clsBackPanel1.Visible = false;
		this.btnCl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCl.BackColor = System.Drawing.Color.LightGray;
		this.btnCl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCl.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.close;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(908, 8);
		this.btnCl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(86, 35);
		this.btnCl.TabIndex = 8;
		this.btnCl.Text = "取 消";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(815, 8);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(86, 35);
		this.btnOK.TabIndex = 9;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1008, 618);
		base.Controls.Add(this.dgvList);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.clsBackPanel1);
		this.Font = new System.Drawing.Font("Times New Roman", 9f);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "frmSTB";
		this.Text = "Travel Bureau:";
		base.Load += new System.EventHandler(frmSTB_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public frmSTB()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void InitCerType()
	{
		try
		{
			cobCer.Text = "";
			cobCer.DataSource = null;
			string sql = "Select * FROM D_Cer";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["cer_id"] = 0;
				dataRow["cer_name"] = (string)m_htab["cobType"];
				dataTable.Rows.InsertAt(dataRow, 0);
				cobCer.DisplayMember = "cer_name";
				cobCer.ValueMember = "cer_id";
				cobCer.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitOper()
	{
		try
		{
			DataTable dataTable = SQLserver.Data_GetDataTable("Select User_ID, User_Name From  UserInfo Where IsNull(Stop_Flag,0) = 0 Order by User_Name");
			if (dataTable != null)
			{
				cobUser.DisplayMember = "User_Name";
				cobUser.ValueMember = "User_ID";
				cobUser.DataSource = dataTable.DefaultView;
				cobUser.SelectedValue = -1;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)Program.m_hPubTab["ErrInitOper"] + "\r\n" + ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void InitTB()
	{
		try
		{
			string sql = "Select * From D_TraBur Where TB_flag = 0 order by TB_name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				cobTB.DisplayMember = "TB_name";
				cobTB.ValueMember = "TB_id";
				cobTB.DataSource = dataTable.DefaultView;
			}
			cobTB.Text = "";
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, label7.Text.Substring(0, label7.Text.Length - 1));
		}
	}

	private void InitTG()
	{
		try
		{
			if (cobTB.SelectedItem != null)
			{
				long num = (long)((DataRowView)cobTB.SelectedItem).Row.ItemArray[0];
				string text = "Select Team_name";
				text = text + " From v_TeamInfo Where TB_flag = 0 And team_flag=0 And TB_id=" + num;
				text += " Group by Team_name";
				text += " Order by Team_name ";
				DataTable dataTable = SQLserver.Data_GetDataTable(text);
				if (dataTable != null)
				{
					cobTG.DisplayMember = "Team_name";
					cobTG.ValueMember = "Team_name";
					cobTG.DataSource = dataTable.DefaultView;
				}
				cobTG.Text = "";
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, label9.Text.Substring(0, label9.Text.Length - 1));
		}
	}

	public string GetPars()
	{
		try
		{
			string text = "";
			if (cobTB.Text.Trim() != "")
			{
				text = text + " And TB_name like N'" + cobTB.Text.Trim() + "%'";
			}
			if (cobTG.Text.Trim() != "")
			{
				text = text + " And team_name like N'" + cobTG.Text.Trim() + "%'";
			}
			if (txtTGG.Text.Trim() != "")
			{
				text = text + " And team_guide like N'" + txtTGG.Text.Trim() + "%'";
			}
			if (cobCer.DataSource != null && Convert.ToInt32(cobCer.SelectedValue) > 0)
			{
				text = text + " And cer_id=" + Convert.ToInt32(cobCer.SelectedValue);
			}
			if (txtCernum.Text.Trim() != "")
			{
				text = text + " And team_cernum like N'" + txtCernum.Text.Trim() + "%'";
			}
			if (cobUser.DataSource != null && Convert.ToInt32(cobUser.SelectedValue) > 0)
			{
				text = text + " And Creator_id=" + Convert.ToInt32(cobUser.SelectedValue);
			}
			if (dtpComeS.Checked)
			{
				text = text + " And Team_cometime >= '" + Program.GetStandDTime(dtpComeS.Value, "00") + "'";
			}
			if (dtpComeE.Checked)
			{
				text = text + " And Team_cometime <= '" + Program.GetStandDTime(dtpComeE.Value, "59") + "'";
			}
			if (dtpLevelS.Checked)
			{
				text = text + " And Team_leveltime >= '" + Program.GetStandDTime(dtpLevelS.Value, "00") + "'";
			}
			if (dtpLevelE.Checked)
			{
				text = text + " And Team_leveltime <= '" + Program.GetStandDTime(dtpLevelE.Value, "59") + "'";
			}
			return text;
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return "";
		}
	}

	public void InitDgv()
	{
		try
		{
			string text = "Select (Row_Number() OVER (Order by Team_cometime desc, TB_name, Team_name, Team_guide )) AS RowNumber ";
			text += ",team_id, TB_name, team_name, team_guide, cer_name As Team_cername, team_cernum, Team_cometime, team_percount, team_tel, team_fax, team_mail, team_othConn, team_memo ";
			text += " From v_TeamInfo Where TB_flag = 0 And team_flag=0 {0}";
			text = string.Format(text, m_sqlstr);
			text += GetPars();
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			dgvList.DataSource = dataTable.DefaultView;
			if (dataTable != null)
			{
				for (int i = 0; i < dgvList.Columns.Count; i++)
				{
					dgvList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvList.Columns[i].Name];
				}
				dgvList.Columns["team_id"].Visible = false;
				dgvList.AutoResizeColumns();
				if (dgvList.RowCount > 0)
				{
					dgvList.Rows[0].Selected = true;
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom(ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void frmSTB_Load(object sender, EventArgs e)
	{
		dtpComeE.CustomFormat = Program.m_currDateTimeFmt;
		dtpComeS.CustomFormat = Program.m_currDateTimeFmt;
		dtpLevelE.CustomFormat = Program.m_currDateTimeFmt;
		dtpLevelS.CustomFormat = Program.m_currDateTimeFmt;
		DateTime now = DateTime.Now;
		string locDate = Program.GetLocDate(now);
		dtpComeS.Value = Convert.ToDateTime(Program.GetLocDate(now.AddDays(-Convert.ToInt32(Program.m_defDay))) + " " + Program.m_defLeaveTime + ":00");
		dtpComeE.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":59");
		dtpLevelS.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":00");
		dtpLevelE.Value = Convert.ToDateTime(Program.GetLocDate(now.AddDays(Convert.ToInt32(Program.m_defDay))) + " " + Program.m_defLeaveTime + ":59");
		DateTimePicker dateTimePicker = dtpComeE;
		DateTimePicker dateTimePicker2 = dtpLevelS;
		bool flag = (dtpLevelE.Checked = false);
		bool flag3 = (dateTimePicker2.Checked = flag);
		dateTimePicker.Checked = flag3;
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		InitTB();
		InitCerType();
		InitOper();
		FlowLayoutPanel flowLayoutPanel = flowLayoutPanel1;
		bool visible = (panel2.Visible = true);
		flowLayoutPanel.Visible = visible;
		ComboBox comboBox = cobTB;
		string text = (cobTG.Text = "");
		comboBox.Text = text;
		InitDgv();
	}

	private void cobTB_SelectedIndexChanged(object sender, EventArgs e)
	{
		InitTG();
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		InitDgv();
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			if ((dgvList.DataSource == null) | (dgvList.Rows.Count <= 0))
			{
				text = string.Format((string)m_htab["Info01"], Text);
				Program.MsgCustom(text, MessageBoxIcon.Asterisk);
				return;
			}
			ClsComm.ExcelConfig excelConfig = new ClsComm.ExcelConfig();
			excelConfig.Title_Font_Bold = true;
			excelConfig.Title_Font_Size = 13;
			excelConfig.Title_Interior_Color = 37;
			excelConfig.Cell_Font_Size = 11;
			ClsComm.ExportFormDataGridview(dgvList, Text, isShowExcle: true, excelConfig, 0, 1, 0, 0);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["exXlsErr"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		try
		{
			cobUser.SelectedIndex = -1;
			cobUser.Text = "";
			DateTime now = DateTime.Now;
			string locDate = Program.GetLocDate(now);
			dtpComeS.Value = Convert.ToDateTime(Program.GetLocDate(now.AddDays(-Convert.ToInt32(Program.m_defDay))) + " " + Program.m_defLeaveTime + ":00");
			dtpComeE.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":59");
			dtpLevelS.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":00");
			dtpLevelE.Value = Convert.ToDateTime(Program.GetLocDate(now.AddDays(Convert.ToInt32(Program.m_defDay))) + " " + Program.m_defLeaveTime + ":59");
			DateTimePicker dateTimePicker = dtpComeE;
			DateTimePicker dateTimePicker2 = dtpLevelS;
			bool flag = (dtpLevelE.Checked = false);
			bool flag3 = (dateTimePicker2.Checked = flag);
			dateTimePicker.Checked = flag3;
			cobCer.SelectedIndex = 0;
			ComboBox comboBox = cobTB;
			string text = (cobTG.Text = "");
			comboBox.Text = text;
			TextBox textBox = txtCernum;
			string text3 = (txtTGG.Text = "");
			textBox.Text = text3;
		}
		catch
		{
		}
	}

	private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvList_SelectionChanged(object sender, EventArgs e)
	{
		try
		{
			if (clsBackPanel1.Visible && dgvList.SelectedRows.Count > 0)
			{
				Text = dgvList.SelectedRows[0].Cells["Team_name"].Value.ToString();
				m_tid = Convert.ToInt32(dgvList.SelectedRows[0].Cells["team_id"].Value);
				m_guide = dgvList.SelectedRows[0].Cells["team_guide"].Value.ToString();
				m_gcer = dgvList.SelectedRows[0].Cells["team_cernum"].Value.ToString();
			}
		}
		catch
		{
		}
	}
}
