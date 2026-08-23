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

public class frmSRoom : Form
{
	private IContainer components;

	private GlassBtn btnCols;

	private ComboBox cobType;

	private Label label1;

	private GlassBtn btnClose;

	private GlassBtn btnReset;

	private GlassBtn btnExport;

	private GlassBtn btnSearch;

	private ComboBox cobUser;

	private Label label6;

	private DateTimePicker dtpLevelE;

	private Label label5;

	private Label label4;

	private ComboBox cobFN;

	private ComboBox cobBN;

	private Label label3;

	private Label label2;

	private DateTimePicker dtpComeE;

	private DateTimePicker dtpLevelS;

	private Label label29;

	private DateTimePicker dtpComeS;

	private Label labArr;

	private clsBackPanel clsBackPanel1;

	private TextBox txtRn;

	private Label label8;

	private ToolStripStatusLabel tssLab1;

	private ToolStripStatusLabel tssLab2;

	private ToolStripStatusLabel tssLab3;

	private ToolStripStatusLabel tssLab4;

	private DataGridView dgvlist;

	private StatusStrip statusStrip1;

	private RadioButton rbDetails;

	private RadioButton rbTotal;

	private GroupBox groupBox1;

	public string m_objName = "WFsr";

	public Hashtable m_htab;

	public string m_tmpVal = "";

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmSRoom));
		this.tssLab1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab3 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab4 = new System.Windows.Forms.ToolStripStatusLabel();
		this.dgvlist = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rbDetails = new System.Windows.Forms.RadioButton();
		this.rbTotal = new System.Windows.Forms.RadioButton();
		this.btnCols = new LockSoftware.Controls.GlassBtn(this.components);
		this.cobType = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnExport = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.cobUser = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpLevelE = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cobFN = new System.Windows.Forms.ComboBox();
		this.cobBN = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpComeE = new System.Windows.Forms.DateTimePicker();
		this.dtpLevelS = new System.Windows.Forms.DateTimePicker();
		this.label29 = new System.Windows.Forms.Label();
		this.dtpComeS = new System.Windows.Forms.DateTimePicker();
		this.labArr = new System.Windows.Forms.Label();
		this.txtRn = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dgvlist).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.tssLab1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 1);
		this.tssLab1.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab1.Name = "tssLab1";
		this.tssLab1.Size = new System.Drawing.Size(680, 18);
		this.tssLab1.Spring = true;
		this.tssLab1.Text = "Total:";
		this.tssLab2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
		this.tssLab2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 1);
		this.tssLab2.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab2.Name = "tssLab2";
		this.tssLab2.Size = new System.Drawing.Size(56, 18);
		this.tssLab2.Text = "合计〓";
		this.tssLab3.AutoSize = false;
		this.tssLab3.BackColor = System.Drawing.Color.Gold;
		this.tssLab3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tssLab3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tssLab3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.tssLab3.ForeColor = System.Drawing.Color.Red;
		this.tssLab3.Name = "tssLab3";
		this.tssLab3.Size = new System.Drawing.Size(160, 18);
		this.tssLab3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tssLab4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 1);
		this.tssLab4.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab4.Name = "tssLab4";
		this.tssLab4.Size = new System.Drawing.Size(0, 18);
		this.dgvlist.AllowUserToAddRows = false;
		this.dgvlist.AllowUserToDeleteRows = false;
		this.dgvlist.BackgroundColor = System.Drawing.Color.White;
		this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvlist.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvlist.Location = new System.Drawing.Point(0, 122);
		this.dgvlist.Name = "dgvlist";
		this.dgvlist.ReadOnly = true;
		this.dgvlist.RowHeadersWidth = 25;
		this.dgvlist.RowTemplate.Height = 23;
		this.dgvlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvlist.Size = new System.Drawing.Size(911, 325);
		this.dgvlist.TabIndex = 5;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.tssLab1, this.tssLab2, this.tssLab3, this.tssLab4 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 447);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(911, 23);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 4;
		this.statusStrip1.Text = "statusStrip1";
		this.clsBackPanel1.AutoScroll = true;
		this.clsBackPanel1.BackColor = System.Drawing.Color.Transparent;
		this.clsBackPanel1.Border = true;
		this.clsBackPanel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderBW = 1;
		this.clsBackPanel1.BorderColorBottom = System.Drawing.SystemColors.GradientInactiveCaption;
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
		this.clsBackPanel1.Controls.Add(this.groupBox1);
		this.clsBackPanel1.Controls.Add(this.btnCols);
		this.clsBackPanel1.Controls.Add(this.cobType);
		this.clsBackPanel1.Controls.Add(this.label1);
		this.clsBackPanel1.Controls.Add(this.btnClose);
		this.clsBackPanel1.Controls.Add(this.btnReset);
		this.clsBackPanel1.Controls.Add(this.btnExport);
		this.clsBackPanel1.Controls.Add(this.btnSearch);
		this.clsBackPanel1.Controls.Add(this.cobUser);
		this.clsBackPanel1.Controls.Add(this.label6);
		this.clsBackPanel1.Controls.Add(this.dtpLevelE);
		this.clsBackPanel1.Controls.Add(this.label5);
		this.clsBackPanel1.Controls.Add(this.label4);
		this.clsBackPanel1.Controls.Add(this.cobFN);
		this.clsBackPanel1.Controls.Add(this.cobBN);
		this.clsBackPanel1.Controls.Add(this.label3);
		this.clsBackPanel1.Controls.Add(this.label2);
		this.clsBackPanel1.Controls.Add(this.dtpComeE);
		this.clsBackPanel1.Controls.Add(this.dtpLevelS);
		this.clsBackPanel1.Controls.Add(this.label29);
		this.clsBackPanel1.Controls.Add(this.dtpComeS);
		this.clsBackPanel1.Controls.Add(this.labArr);
		this.clsBackPanel1.Controls.Add(this.txtRn);
		this.clsBackPanel1.Controls.Add(this.label8);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(911, 122);
		this.clsBackPanel1.TabIndex = 3;
		this.groupBox1.Controls.Add(this.rbDetails);
		this.groupBox1.Controls.Add(this.rbTotal);
		this.groupBox1.Location = new System.Drawing.Point(650, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(110, 55);
		this.groupBox1.TabIndex = 56;
		this.groupBox1.TabStop = false;
		this.rbDetails.AutoSize = true;
		this.rbDetails.Location = new System.Drawing.Point(7, 15);
		this.rbDetails.Name = "rbDetails";
		this.rbDetails.Size = new System.Drawing.Size(65, 16);
		this.rbDetails.TabIndex = 1;
		this.rbDetails.Text = "Details";
		this.rbDetails.UseVisualStyleBackColor = true;
		this.rbTotal.AutoSize = true;
		this.rbTotal.Checked = true;
		this.rbTotal.Location = new System.Drawing.Point(7, 35);
		this.rbTotal.Name = "rbTotal";
		this.rbTotal.Size = new System.Drawing.Size(83, 16);
		this.rbTotal.TabIndex = 0;
		this.rbTotal.TabStop = true;
		this.rbTotal.Text = "Statistics";
		this.rbTotal.UseVisualStyleBackColor = true;
		this.btnCols.BackColor = System.Drawing.Color.LightGray;
		this.btnCols.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCols.ForeColor = System.Drawing.Color.Black;
		this.btnCols.GlowColor = System.Drawing.Color.White;
		this.btnCols.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCols.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.btnCols.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCols.Location = new System.Drawing.Point(762, 9);
		this.btnCols.Name = "btnCols";
		this.btnCols.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCols.Size = new System.Drawing.Size(110, 34);
		this.btnCols.TabIndex = 53;
		this.btnCols.Text = "Disply Fields";
		this.btnCols.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCols.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCols.Visible = false;
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 160;
		this.cobType.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(331, 12);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(140, 23);
		this.cobType.TabIndex = 52;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(225, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 23);
		this.label1.TabIndex = 51;
		this.label1.Text = "Room Type:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(823, 65);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 50;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnReset.BackColor = System.Drawing.Color.LightGray;
		this.btnReset.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReset.ForeColor = System.Drawing.Color.Black;
		this.btnReset.GlowColor = System.Drawing.Color.White;
		this.btnReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReset.Image = LockSoftware.Properties.Resources.clear;
		this.btnReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnReset.Location = new System.Drawing.Point(746, 65);
		this.btnReset.Name = "btnReset";
		this.btnReset.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnReset.Size = new System.Drawing.Size(71, 34);
		this.btnReset.TabIndex = 49;
		this.btnReset.Text = "Reset";
		this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnExport.BackColor = System.Drawing.Color.LightGray;
		this.btnExport.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExport.ForeColor = System.Drawing.Color.Black;
		this.btnExport.GlowColor = System.Drawing.Color.White;
		this.btnExport.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnExport.Image = LockSoftware.Properties.Resources.xls;
		this.btnExport.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnExport.Location = new System.Drawing.Point(766, 19);
		this.btnExport.Name = "btnExport";
		this.btnExport.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnExport.Size = new System.Drawing.Size(128, 34);
		this.btnExport.TabIndex = 48;
		this.btnExport.Text = "Export To Excel";
		this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.btnSearch.BackColor = System.Drawing.Color.LightGray;
		this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSearch.ForeColor = System.Drawing.Color.Black;
		this.btnSearch.GlowColor = System.Drawing.Color.White;
		this.btnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSearch.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSearch.Location = new System.Drawing.Point(650, 65);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSearch.Size = new System.Drawing.Size(90, 34);
		this.btnSearch.TabIndex = 47;
		this.btnSearch.Text = "Search";
		this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
		this.cobUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobUser.DropDownWidth = 160;
		this.cobUser.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobUser.FormattingEnabled = true;
		this.cobUser.Location = new System.Drawing.Point(567, 12);
		this.cobUser.Name = "cobUser";
		this.cobUser.Size = new System.Drawing.Size(77, 23);
		this.cobUser.TabIndex = 46;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(474, 11);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(87, 23);
		this.label6.TabIndex = 45;
		this.label6.Text = "Operator:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpLevelE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelE.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpLevelE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelE.Location = new System.Drawing.Point(504, 77);
		this.dtpLevelE.Name = "dtpLevelE";
		this.dtpLevelE.ShowCheckBox = true;
		this.dtpLevelE.Size = new System.Drawing.Size(140, 21);
		this.dtpLevelE.TabIndex = 44;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(481, 82);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(19, 15);
		this.label5.TabIndex = 43;
		this.label5.Text = "→";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(481, 48);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(19, 15);
		this.label4.TabIndex = 42;
		this.label4.Text = "→";
		this.cobFN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFN.DropDownWidth = 160;
		this.cobFN.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobFN.FormattingEnabled = true;
		this.cobFN.Location = new System.Drawing.Point(107, 44);
		this.cobFN.Name = "cobFN";
		this.cobFN.Size = new System.Drawing.Size(112, 23);
		this.cobFN.TabIndex = 41;
		this.cobBN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBN.DropDownWidth = 160;
		this.cobBN.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobBN.FormattingEnabled = true;
		this.cobBN.Location = new System.Drawing.Point(107, 12);
		this.cobBN.Name = "cobBN";
		this.cobBN.Size = new System.Drawing.Size(112, 23);
		this.cobBN.TabIndex = 40;
		this.cobBN.SelectedIndexChanged += new System.EventHandler(cobBN_SelectedIndexChanged);
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(1, 43);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 23);
		this.label3.TabIndex = 39;
		this.label3.Text = "Floor Name:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(1, 11);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 23);
		this.label2.TabIndex = 38;
		this.label2.Text = "Building Name:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpComeE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeE.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpComeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeE.Location = new System.Drawing.Point(504, 44);
		this.dtpComeE.Name = "dtpComeE";
		this.dtpComeE.ShowCheckBox = true;
		this.dtpComeE.Size = new System.Drawing.Size(140, 21);
		this.dtpComeE.TabIndex = 37;
		this.dtpLevelS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelS.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpLevelS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelS.Location = new System.Drawing.Point(331, 77);
		this.dtpLevelS.Name = "dtpLevelS";
		this.dtpLevelS.ShowCheckBox = true;
		this.dtpLevelS.Size = new System.Drawing.Size(140, 21);
		this.dtpLevelS.TabIndex = 33;
		this.label29.BackColor = System.Drawing.Color.Transparent;
		this.label29.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label29.Location = new System.Drawing.Point(225, 76);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(100, 23);
		this.label29.TabIndex = 36;
		this.label29.Text = "Checking Out:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpComeS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeS.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpComeS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeS.Location = new System.Drawing.Point(331, 44);
		this.dtpComeS.Name = "dtpComeS";
		this.dtpComeS.ShowCheckBox = true;
		this.dtpComeS.Size = new System.Drawing.Size(140, 21);
		this.dtpComeS.TabIndex = 31;
		this.labArr.BackColor = System.Drawing.Color.Transparent;
		this.labArr.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labArr.Location = new System.Drawing.Point(225, 43);
		this.labArr.Name = "labArr";
		this.labArr.Size = new System.Drawing.Size(100, 23);
		this.labArr.TabIndex = 32;
		this.labArr.Text = "Checking In:";
		this.labArr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRn.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtRn.ForeColor = System.Drawing.Color.Black;
		this.txtRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtRn.Location = new System.Drawing.Point(107, 77);
		this.txtRn.Name = "txtRn";
		this.txtRn.Size = new System.Drawing.Size(112, 21);
		this.txtRn.TabIndex = 25;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(1, 76);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(100, 23);
		this.label8.TabIndex = 26;
		this.label8.Text = "Room Name:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(911, 470);
		base.Controls.Add(this.dgvlist);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.statusStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmSRoom";
		this.Text = "frmSRoom";
		base.Load += new System.EventHandler(frmSRoom_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvlist).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmSRoom()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
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

	private void InitType()
	{
		try
		{
			cobType.DataSource = null;
			string sql = "Select TP_ID, TP_Name From D_RoomType Order by TP_ID, TP_Name";
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
			Program.MsgBox((string)m_htab["Err02"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitBuild()
	{
		try
		{
			cobBN.DataSource = null;
			string sql = "Select * From D_Build Order by hotelID, Build_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Build_ID"] = 0;
				dataRow["Build_Name"] = (string)m_htab["cobBN"];
				dataTable.Rows.InsertAt(dataRow, 0);
				cobBN.DisplayMember = "Build_Name";
				cobBN.ValueMember = "Build_ID";
				cobBN.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err03"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitFloor(int bid)
	{
		try
		{
			cobFN.DataSource = null;
			string sql = "Select * From D_Floor Where Build_ID=" + bid + " Order by Build_ID, Floor_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Floor_ID"] = 0;
				dataRow["Floor_Name"] = (string)m_htab["cobFN"];
				dataTable.Rows.InsertAt(dataRow, 0);
				cobFN.DisplayMember = "Floor_Name";
				cobFN.ValueMember = "Floor_ID";
				cobFN.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		try
		{
			dgvlist.DataSource = null;
			string text = "Select r_name, build_name, floor_name, TP_Name";
			if (rbTotal.Checked)
			{
				text += ", Sum(TR_guestcount) As TR_guestcount, Sum(TR_cardcount) As TR_cardcount";
				object obj = text;
				text = string.Concat(obj, ", (cast(cast(isnull(a_id,0)/2.0 as numeric(18,1))as varchar) + N'", Program.m_hPubTab["InfoDay"], "'");
				object obj2 = text;
				text = string.Concat(obj2, " + cast(cast(Tr_actual_s_hour as integer) as varchar) + N'", Program.m_hPubTab["InfoHour"], "') As TR_actual_S_Hour");
				text += ", sum(TR_mustpay) As TR_Roomprice, TR_Bascurname, TR_Basrate, Sum(TR_OthPrice) As TR_OthPrice,cast(tr_memo as nvarchar(max)) as tr_memo ";
				text = text + " From v_Room Where 1 = 1 " + GetPars();
				text += " Group by build_name, floor_name, TP_Name, r_name, TR_Bascurname, TR_actual_S_Hour, TR_Basrate,A_ID,cast(tr_memo as nvarchar(max))";
			}
			else
			{
				text += ", TR_guestcount, TR_cardcount, TR_cometime";
				object obj3 = text;
				text = string.Concat(obj3, ",(Cast(Cast(Tr_stayhour As Integer) As varchar) + N'", Program.m_hPubTab["InfoDay"], "'");
				object obj4 = text;
				text = string.Concat(obj4, "+ Cast(Cast(Tr_sohour As Integer) As varchar) + N'", Program.m_hPubTab["InfoHour"], "') As TR_stayHour");
				text += ",(Case TR_stayover When 1 then TR_SOLTime Else TR_stand_L_time End) As TR_stand_L_time, TR_mustpay As TR_Roomprice, TR_Bascurname, TR_Basrate, TR_OthPrice, TR_stayover";
				text += ", TR_SOLTime, curr_code, curr_rate, TR_Level";
				object obj5 = text;
				text = string.Concat(obj5, ",(cast(cast(isnull(a_id,0)/2.0 as numeric(18,1))as varchar) + N'", Program.m_hPubTab["InfoDay"], "'");
				object obj6 = text;
				text = string.Concat(obj6, "+ cast(cast(Tr_actual_s_hour as integer) as varchar) + N'", Program.m_hPubTab["InfoHour"], "' ) As TR_actual_S_Hour");
				text = text + ",TR_actual_L_time,tr_memo From v_Room Where 1 = 1 " + GetPars();
			}
			text += " Order by build_name, floor_name, TP_Name,r_name";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null)
			{
				dgvlist.DataSource = dataTable.DefaultView;
				for (int i = 0; i < dgvlist.Columns.Count; i++)
				{
					dgvlist.Columns[i].HeaderText = (string)m_htab["dgv" + dgvlist.Columns[i].Name];
				}
				dgvlist.AutoResizeColumns();
			}
			dgvlist.Columns["tr_memo"].Visible = false;
			tssLab1.Text = string.Format((string)m_htab["tssLab1"], dgvlist.Rows.Count);
			double num = 0.0;
			for (int j = 0; j < dgvlist.Rows.Count; j++)
			{
				num += Convert.ToDouble(dataTable.Rows[j]["TR_OthPrice"]);
				num += Convert.ToDouble(dataTable.Rows[j]["TR_Roomprice"]) / Program.m_baseCurrRate;
			}
			tssLab3.Text = num.ToString("F2") + " " + Program.m_baseCurrCode;
		}
		catch
		{
		}
	}

	private void frmSRoom_Load(object sender, EventArgs e)
	{
		tssLab1.Text = "";
		tssLab4.Text = "";
		tssLab2.Text = (string)m_htab["tssLab2"];
		btnExport.Enabled = SQLserver.GetUserPermisstion(1035, Program.m_OperID);
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
		InitType();
		InitOper();
		InitBuild();
		if (m_tmpVal != "")
		{
			dtpComeS.Checked = false;
			txtRn.Text = m_tmpVal;
			btnSearch_Click(null, null);
		}
	}

	private void cobBN_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cobBN.DataSource != null)
			{
				InitFloor(Convert.ToInt32(cobBN.SelectedValue.ToString()));
			}
		}
		catch
		{
		}
	}

	public string GetPars()
	{
		try
		{
			string text = "";
			if (cobBN.DataSource != null && Convert.ToInt32(cobBN.SelectedValue) > 0)
			{
				text = text + " And Build_ID=" + Convert.ToInt32(cobBN.SelectedValue);
			}
			if (cobFN.DataSource != null && Convert.ToInt32(cobFN.SelectedValue) > 0)
			{
				text = text + " And R_FloorID=" + Convert.ToInt32(cobFN.SelectedValue);
			}
			if (txtRn.Text.Trim() != "")
			{
				text = text + " And r_name like N'" + txtRn.Text.Trim() + "%'";
			}
			if (cobType.DataSource != null && Convert.ToInt32(cobType.SelectedValue) > 0)
			{
				text = text + " And R_TypeID=" + Convert.ToInt32(cobType.SelectedValue);
			}
			if (cobUser.DataSource != null && Convert.ToInt32(cobUser.SelectedValue) > 0)
			{
				text = text + " And Creator_id=" + Convert.ToInt32(cobUser.SelectedValue);
			}
			if (dtpComeS.Checked)
			{
				text = text + " And TR_cometime >= '" + Program.GetStandDTime(dtpComeS.Value, "00") + "'";
			}
			if (dtpComeE.Checked)
			{
				text = text + " And TR_cometime <= '" + Program.GetStandDTime(dtpComeE.Value, "59") + "'";
			}
			if (dtpLevelS.Checked)
			{
				text = text + " And TR_actual_l_time >= '" + Program.GetStandDTime(dtpLevelS.Value, "00") + "'";
			}
			if (dtpLevelE.Checked)
			{
				text = text + " And TR_actual_l_time <= '" + Program.GetStandDTime(dtpLevelE.Value, "59") + "'";
			}
			return text;
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return "";
		}
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			if ((dgvlist.DataSource == null) | (dgvlist.Rows.Count <= 0))
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
			ClsComm.ExportFormDataGridview(dgvlist, Text, isShowExcle: true, excelConfig, 0, 1, 0, 0);
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
			txtRn.Text = "";
			if (cobFN.DataSource != null)
			{
				cobFN.SelectedIndex = 0;
			}
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
			ComboBox comboBox = cobBN;
			int selectedIndex = (cobType.SelectedIndex = 0);
			comboBox.SelectedIndex = selectedIndex;
		}
		catch
		{
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}
}
