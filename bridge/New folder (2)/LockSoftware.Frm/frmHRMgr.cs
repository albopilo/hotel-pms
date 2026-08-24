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

public class frmHRMgr : Form
{
	private IContainer components;

	private FlowLayoutPanel flowLayoutPanel1;

	private ComboBox cobBD;

	private ComboBox cobFD;

	private ComboBox cobType;

	private TextBox txtSRn;

	private Label label19;

	private TextBox txtERn;

	private LockSoftware.Controls.GlassBtn btnSear;

	private SplitContainer splitContainer1;

	private ListView lvRoom;

	private StatusStrip sstLR;

	private ToolStripStatusLabel TSSLab03;

	private ToolStripStatusLabel TSSLab04;

	private ToolStripStatusLabel TSSLab05;

	private ToolStripStatusLabel TSSLab06;

	private NGlassBtn btnIDCard;

	private TextBox txtRn;

	private TextBox txtGn;

	private ComboBox cobCer;

	private Label label26;

	private Label label17;

	private Label label1;

	private TextBox txtCernum;

	private NumericUpDown nudDay;

	private DateTimePicker dtpCome;

	private Label label29;

	private Label label28;

	private Label label27;

	private Label labArr;

	private TextBox txtDP;

	private TextBox txtRP;

	private TextBox txtMP;

	private CheckBox chkRepl;

	private TextBox txtGDepo;

	private ComboBox cobCurrency;

	private TextBox txtGC;

	private Label label32;

	private Label label31;

	private Label label33;

	private TableLayoutPanel tableLayoutPanel1;

	private Panel panel1;

	private Panel panel3;

	private Panel panel2;

	public LockSoftware.Controls.GlassBtn btnOK;

	public LockSoftware.Controls.GlassBtn btnCl;

	private TextBox textBox1;

	public string m_objName = "WFbr";

	public Hashtable m_htab;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmHRMgr));
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.cobBD = new System.Windows.Forms.ComboBox();
		this.cobFD = new System.Windows.Forms.ComboBox();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.txtSRn = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtERn = new System.Windows.Forms.TextBox();
		this.btnSear = new LockSoftware.Controls.GlassBtn(this.components);
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.lvRoom = new System.Windows.Forms.ListView();
		this.sstLR = new System.Windows.Forms.StatusStrip();
		this.TSSLab03 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab04 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab05 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab06 = new System.Windows.Forms.ToolStripStatusLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.txtRP = new System.Windows.Forms.TextBox();
		this.txtDP = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.cobCurrency = new System.Windows.Forms.ComboBox();
		this.txtGDepo = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.btnIDCard = new LockSoftware.Controls.NGlassBtn(this.components);
		this.label1 = new System.Windows.Forms.Label();
		this.txtRn = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtGC = new System.Windows.Forms.TextBox();
		this.txtMP = new System.Windows.Forms.TextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.txtGn = new System.Windows.Forms.TextBox();
		this.chkRepl = new System.Windows.Forms.CheckBox();
		this.label26 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.txtCernum = new System.Windows.Forms.TextBox();
		this.labArr = new System.Windows.Forms.Label();
		this.dtpCome = new System.Windows.Forms.DateTimePicker();
		this.label28 = new System.Windows.Forms.Label();
		this.nudDay = new System.Windows.Forms.NumericUpDown();
		this.label29 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.flowLayoutPanel1.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.sstLR.SuspendLayout();
		this.panel3.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).BeginInit();
		base.SuspendLayout();
		this.flowLayoutPanel1.AutoScroll = true;
		this.flowLayoutPanel1.Controls.Add(this.cobBD);
		this.flowLayoutPanel1.Controls.Add(this.cobFD);
		this.flowLayoutPanel1.Controls.Add(this.cobType);
		this.flowLayoutPanel1.Controls.Add(this.txtSRn);
		this.flowLayoutPanel1.Controls.Add(this.label19);
		this.flowLayoutPanel1.Controls.Add(this.txtERn);
		this.flowLayoutPanel1.Controls.Add(this.btnSear);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(636, 47);
		this.flowLayoutPanel1.TabIndex = 1;
		this.cobBD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBD.DropDownWidth = 180;
		this.cobBD.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobBD.FormattingEnabled = true;
		this.cobBD.Location = new System.Drawing.Point(8, 12);
		this.cobBD.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.cobBD.Name = "cobBD";
		this.cobBD.Size = new System.Drawing.Size(90, 22);
		this.cobBD.TabIndex = 3;
		this.cobBD.SelectedIndexChanged += new System.EventHandler(cobBD_SelectedIndexChanged);
		this.cobFD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFD.DropDownWidth = 180;
		this.cobFD.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobFD.FormattingEnabled = true;
		this.cobFD.Location = new System.Drawing.Point(104, 12);
		this.cobFD.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.cobFD.Name = "cobFD";
		this.cobFD.Size = new System.Drawing.Size(90, 22);
		this.cobFD.TabIndex = 4;
		this.cobType.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 180;
		this.cobType.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(200, 12);
		this.cobType.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(90, 22);
		this.cobType.TabIndex = 5;
		this.txtSRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSRn.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSRn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtSRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtSRn.Location = new System.Drawing.Point(296, 12);
		this.txtSRn.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.txtSRn.Name = "txtSRn";
		this.txtSRn.Size = new System.Drawing.Size(90, 22);
		this.txtSRn.TabIndex = 6;
		this.txtSRn.Text = "ROOM NAME...";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(392, 13);
		this.label19.Margin = new System.Windows.Forms.Padding(3, 13, 3, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(19, 14);
		this.label19.TabIndex = 7;
		this.label19.Text = "→";
		this.txtERn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtERn.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtERn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtERn.Location = new System.Drawing.Point(417, 12);
		this.txtERn.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.txtERn.Name = "txtERn";
		this.txtERn.Size = new System.Drawing.Size(90, 22);
		this.txtERn.TabIndex = 8;
		this.txtERn.Text = "ROOM NAME...";
		this.btnSear.AutoSize = true;
		this.btnSear.BackColor = System.Drawing.Color.LightGray;
		this.btnSear.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSear.ForeColor = System.Drawing.Color.Black;
		this.btnSear.GlowColor = System.Drawing.Color.White;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSear.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSear.Location = new System.Drawing.Point(513, 3);
		this.btnSear.Name = "btnSear";
		this.btnSear.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSear.Size = new System.Drawing.Size(92, 38);
		this.btnSear.TabIndex = 9;
		this.btnSear.Text = "Search";
		this.btnSear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(0, 47);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.lvRoom);
		this.splitContainer1.Panel1.Controls.Add(this.sstLR);
		this.splitContainer1.Panel1MinSize = 280;
		this.splitContainer1.Panel2.Controls.Add(this.panel3);
		this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
		this.splitContainer1.Size = new System.Drawing.Size(636, 364);
		this.splitContainer1.SplitterDistance = 293;
		this.splitContainer1.TabIndex = 2;
		this.lvRoom.CheckBoxes = true;
		this.lvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvRoom.FullRowSelect = true;
		this.lvRoom.GridLines = true;
		this.lvRoom.Location = new System.Drawing.Point(0, 0);
		this.lvRoom.MultiSelect = false;
		this.lvRoom.Name = "lvRoom";
		this.lvRoom.Size = new System.Drawing.Size(289, 330);
		this.lvRoom.TabIndex = 18;
		this.lvRoom.UseCompatibleStateImageBehavior = false;
		this.sstLR.AutoSize = false;
		this.sstLR.BackColor = System.Drawing.Color.Transparent;
		this.sstLR.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstLR.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSSLab03, this.TSSLab04, this.TSSLab05, this.TSSLab06 });
		this.sstLR.Location = new System.Drawing.Point(0, 330);
		this.sstLR.Name = "sstLR";
		this.sstLR.Size = new System.Drawing.Size(289, 30);
		this.sstLR.SizingGrip = false;
		this.sstLR.TabIndex = 19;
		this.sstLR.Text = "statusStrip1";
		this.TSSLab03.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab03.Name = "TSSLab03";
		this.TSSLab03.Size = new System.Drawing.Size(47, 25);
		this.TSSLab03.Text = "Total:";
		this.TSSLab04.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab04.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab04.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab04.Name = "TSSLab04";
		this.TSSLab04.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab04.Size = new System.Drawing.Size(79, 25);
		this.TSSLab04.Spring = true;
		this.TSSLab04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab05.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab05.Name = "TSSLab05";
		this.TSSLab05.Size = new System.Drawing.Size(68, 25);
		this.TSSLab05.Text = "Selected:";
		this.TSSLab06.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab06.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab06.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab06.Name = "TSSLab06";
		this.TSSLab06.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab06.Size = new System.Drawing.Size(79, 25);
		this.TSSLab06.Spring = true;
		this.TSSLab06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel3.Controls.Add(this.btnOK);
		this.panel3.Controls.Add(this.btnCl);
		this.panel3.Controls.Add(this.txtRP);
		this.panel3.Controls.Add(this.txtDP);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.panel3.Location = new System.Drawing.Point(0, 318);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(335, 42);
		this.panel3.TabIndex = 70;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(82, 7);
		this.btnOK.Margin = new System.Windows.Forms.Padding(4);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(72, 27);
		this.btnOK.TabIndex = 70;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.BackColor = System.Drawing.Color.LightGray;
		this.btnCl.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.close;
		this.btnCl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(162, 7);
		this.btnCl.Margin = new System.Windows.Forms.Padding(4);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(72, 27);
		this.btnCl.TabIndex = 69;
		this.btnCl.Text = "关 闭";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.Click += new System.EventHandler(btnCl_Click);
		this.txtRP.Location = new System.Drawing.Point(6, 3);
		this.txtRP.Name = "txtRP";
		this.txtRP.Size = new System.Drawing.Size(49, 22);
		this.txtRP.TabIndex = 67;
		this.txtRP.Text = "0";
		this.txtRP.Visible = false;
		this.txtDP.Location = new System.Drawing.Point(6, 17);
		this.txtDP.Name = "txtDP";
		this.txtDP.Size = new System.Drawing.Size(49, 22);
		this.txtDP.TabIndex = 68;
		this.txtDP.Text = "0";
		this.txtDP.Visible = false;
		this.tableLayoutPanel1.AutoScroll = true;
		this.tableLayoutPanel1.AutoSize = true;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 10);
		this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtRn, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label17, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtGC, 1, 9);
		this.tableLayoutPanel1.Controls.Add(this.txtMP, 1, 8);
		this.tableLayoutPanel1.Controls.Add(this.label33, 0, 10);
		this.tableLayoutPanel1.Controls.Add(this.label32, 0, 9);
		this.tableLayoutPanel1.Controls.Add(this.txtGn, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.chkRepl, 1, 7);
		this.tableLayoutPanel1.Controls.Add(this.label26, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.label31, 0, 8);
		this.tableLayoutPanel1.Controls.Add(this.label27, 0, 3);
		this.tableLayoutPanel1.Controls.Add(this.txtCernum, 1, 3);
		this.tableLayoutPanel1.Controls.Add(this.labArr, 0, 4);
		this.tableLayoutPanel1.Controls.Add(this.dtpCome, 1, 4);
		this.tableLayoutPanel1.Controls.Add(this.label28, 0, 5);
		this.tableLayoutPanel1.Controls.Add(this.nudDay, 1, 5);
		this.tableLayoutPanel1.Controls.Add(this.label29, 0, 6);
		this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 6);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
		this.tableLayoutPanel1.RowCount = 11;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 318);
		this.tableLayoutPanel1.TabIndex = 69;
		this.panel2.Controls.Add(this.cobCurrency);
		this.panel2.Controls.Add(this.txtGDepo);
		this.panel2.Location = new System.Drawing.Point(98, 283);
		this.panel2.Margin = new System.Windows.Forms.Padding(0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(140, 30);
		this.panel2.TabIndex = 70;
		this.cobCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCurrency.FormattingEnabled = true;
		this.cobCurrency.Location = new System.Drawing.Point(74, 4);
		this.cobCurrency.Name = "cobCurrency";
		this.cobCurrency.Size = new System.Drawing.Size(63, 22);
		this.cobCurrency.TabIndex = 62;
		this.txtGDepo.Location = new System.Drawing.Point(3, 3);
		this.txtGDepo.Name = "txtGDepo";
		this.txtGDepo.Size = new System.Drawing.Size(65, 22);
		this.txtGDepo.TabIndex = 61;
		this.txtGDepo.Text = "0";
		this.panel1.Controls.Add(this.cobCer);
		this.panel1.Controls.Add(this.btnIDCard);
		this.panel1.Location = new System.Drawing.Point(101, 61);
		this.panel1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
		this.panel1.Size = new System.Drawing.Size(137, 30);
		this.panel1.TabIndex = 70;
		this.cobCer.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 180;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(0, 2);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(99, 22);
		this.cobCer.TabIndex = 44;
		this.btnIDCard.BackColor = System.Drawing.Color.Transparent;
		this.btnIDCard.BaseColor = System.Drawing.Color.White;
		this.btnIDCard.ButtonColor = System.Drawing.Color.Silver;
		this.btnIDCard.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnIDCard.ButtonText = null;
		this.btnIDCard.CornerRadius = 2;
		this.btnIDCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIDCard.Image = LockSoftware.Properties.Resources.V_Cer;
		this.btnIDCard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnIDCard.Location = new System.Drawing.Point(102, 1);
		this.btnIDCard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
		this.btnIDCard.Name = "btnIDCard";
		this.btnIDCard.Size = new System.Drawing.Size(30, 26);
		this.btnIDCard.TabIndex = 48;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(8, 5);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label1.Size = new System.Drawing.Size(77, 20);
		this.label1.TabIndex = 45;
		this.label1.Text = "Room Name:";
		this.txtRn.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRn.ForeColor = System.Drawing.Color.Black;
		this.txtRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtRn.Location = new System.Drawing.Point(101, 8);
		this.txtRn.Name = "txtRn";
		this.txtRn.ReadOnly = true;
		this.txtRn.Size = new System.Drawing.Size(136, 22);
		this.txtRn.TabIndex = 42;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(8, 33);
		this.label17.Name = "label17";
		this.label17.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label17.Size = new System.Drawing.Size(78, 20);
		this.label17.TabIndex = 46;
		this.label17.Text = "Guest Name:";
		this.txtGC.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtGC.Location = new System.Drawing.Point(101, 258);
		this.txtGC.Name = "txtGC";
		this.txtGC.ReadOnly = true;
		this.txtGC.Size = new System.Drawing.Size(135, 22);
		this.txtGC.TabIndex = 63;
		this.txtGC.Text = "0";
		this.txtMP.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtMP.Location = new System.Drawing.Point(101, 230);
		this.txtMP.Name = "txtMP";
		this.txtMP.ReadOnly = true;
		this.txtMP.Size = new System.Drawing.Size(135, 22);
		this.txtMP.TabIndex = 60;
		this.txtMP.Text = "0";
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(8, 283);
		this.label33.Name = "label33";
		this.label33.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label33.Size = new System.Drawing.Size(78, 20);
		this.label33.TabIndex = 66;
		this.label33.Text = "Paid Deposit:";
		this.label32.AutoSize = true;
		this.label32.Location = new System.Drawing.Point(8, 255);
		this.label32.Name = "label32";
		this.label32.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label32.Size = new System.Drawing.Size(87, 20);
		this.label32.TabIndex = 65;
		this.label32.Text = "Room Deposit:";
		this.txtGn.Location = new System.Drawing.Point(101, 36);
		this.txtGn.Name = "txtGn";
		this.txtGn.Size = new System.Drawing.Size(136, 22);
		this.txtGn.TabIndex = 43;
		this.chkRepl.AutoSize = true;
		this.chkRepl.Location = new System.Drawing.Point(101, 206);
		this.chkRepl.Name = "chkRepl";
		this.chkRepl.Size = new System.Drawing.Size(73, 18);
		this.chkRepl.TabIndex = 59;
		this.chkRepl.Text = "Null Card";
		this.chkRepl.UseVisualStyleBackColor = true;
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(8, 61);
		this.label26.Name = "label26";
		this.label26.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label26.Size = new System.Drawing.Size(66, 20);
		this.label26.TabIndex = 47;
		this.label26.Text = "Certificate:";
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(8, 227);
		this.label31.Name = "label31";
		this.label31.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label31.Size = new System.Drawing.Size(72, 20);
		this.label31.TabIndex = 64;
		this.label31.Text = "Room Price:";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(8, 91);
		this.label27.Name = "label27";
		this.label27.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label27.Size = new System.Drawing.Size(54, 20);
		this.label27.TabIndex = 54;
		this.label27.Text = "Number:";
		this.txtCernum.Location = new System.Drawing.Point(101, 94);
		this.txtCernum.Name = "txtCernum";
		this.txtCernum.Size = new System.Drawing.Size(136, 22);
		this.txtCernum.TabIndex = 49;
		this.labArr.AutoSize = true;
		this.labArr.Location = new System.Drawing.Point(8, 119);
		this.labArr.Name = "labArr";
		this.labArr.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.labArr.Size = new System.Drawing.Size(73, 20);
		this.labArr.TabIndex = 53;
		this.labArr.Text = "Arrival Date:";
		this.dtpCome.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCome.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCome.Location = new System.Drawing.Point(101, 122);
		this.dtpCome.Name = "dtpCome";
		this.dtpCome.Size = new System.Drawing.Size(136, 22);
		this.dtpCome.TabIndex = 50;
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(8, 147);
		this.label28.Name = "label28";
		this.label28.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label28.Size = new System.Drawing.Size(65, 20);
		this.label28.TabIndex = 55;
		this.label28.Text = "Stay Hour:";
		this.nudDay.Location = new System.Drawing.Point(101, 150);
		this.nudDay.Maximum = new decimal(new int[4] { 23, 0, 0, 0 });
		this.nudDay.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.Name = "nudDay";
		this.nudDay.Size = new System.Drawing.Size(57, 22);
		this.nudDay.TabIndex = 52;
		this.nudDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.nudDay.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(8, 175);
		this.label29.Name = "label29";
		this.label29.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
		this.label29.Size = new System.Drawing.Size(69, 20);
		this.label29.TabIndex = 57;
		this.label29.Text = "Level Date:";
		this.textBox1.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.textBox1.Location = new System.Drawing.Point(101, 178);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(137, 22);
		this.textBox1.TabIndex = 71;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(636, 411);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.flowLayoutPanel1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmHRMgr";
		this.Text = "frmHRMgr";
		base.Load += new System.EventHandler(frmHRMgr_Load);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		this.splitContainer1.ResumeLayout(false);
		this.sstLR.ResumeLayout(false);
		this.sstLR.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.nudDay).EndInit();
		base.ResumeLayout(false);
	}

	public frmHRMgr()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
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
		if (num2 > 0)
		{
			text = text + " And  R_FloorID=" + num2;
		}
		if (num > 0)
		{
			text = text + " And  Build_ID=" + num;
		}
		text += " And RS_Canused=1";
		if (cobType.SelectedIndex > 0)
		{
			text = text + " And R_TypeID=" + cobType.SelectedValue.ToString();
		}
		if (txtSRn.ForeColor == Color.Black && txtSRn.Text.Trim() != "")
		{
			text = text + " And R_Name >= N'" + txtSRn.Text.Trim() + "'";
		}
		if (txtERn.ForeColor == Color.Black && txtERn.Text.Trim() != "")
		{
			text = text + " And R_Name < N'" + txtERn.Text.Trim() + "'";
		}
		return text;
	}

	private void InitRoomList(string sqlStr)
	{
		lvRoom.Items.Clear();
		TSSLab04.Text = "";
		string text = "Select R_Name, R_ID, R_Code, R_SubCode, R_FloorID, R_TypeID, R_RSID, R_BedAdd, R_BedSinglePrice, R_Size, R_Memo";
		text += ", build_ID, Build_Name, Floor_Name, TP_Name , R_CurGuestCount, R_TotalGuest, R_TotalPrice,TP_Price,TP_deposit";
		text += ", RS_Name000, R_MaxCardNum,Build_Code,Floor_Code, TP_BedCount From v_HotelRooms Where (IsNull(R_flag,0) = 0";
		text = text + sqlStr + ")";
		text += " Order by Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(text);
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
			if (Convert.ToInt16(dataTable.Rows[i]["R_RSID"].ToString()) == 6)
			{
				array[i].ImageIndex = 2;
			}
			else
			{
				array[i].ImageIndex = Convert.ToInt16(dataTable.Rows[i]["R_RSID"].ToString()) - 1;
			}
		}
		lvRoom.Items.AddRange(array);
		TSSLab04.Text = lvRoom.Items.Count.ToString();
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

	private void btnCl_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmHRMgr_Load(object sender, EventArgs e)
	{
		txtERn.Text = (string)m_htab["txtSRn"];
		InitBuild();
		InitType();
		InitCerType();
		InitCurrency();
		try
		{
			btnOK.Text = (string)Program.m_hPubTab["btnOK"];
			btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		}
		catch
		{
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
}
