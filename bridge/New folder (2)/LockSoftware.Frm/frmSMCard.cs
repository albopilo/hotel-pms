using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using CommonLib;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmSMCard : Form
{
	private IContainer components;

	private clsBackPanel clsBackPanel1;

	private ComboBox cobType;

	private Label label1;

	private GlassBtn btnClose;

	private GlassBtn btnReset;

	private GlassBtn btnExport;

	private GlassBtn btnSearch;

	private ComboBox cobUser;

	private Label label6;

	private DateTimePicker dtpCVDE;

	private Label label5;

	private Label label4;

	private ComboBox cobFN;

	private ComboBox cobBN;

	private Label label3;

	private Label label2;

	private DateTimePicker dtpCreaE;

	private DateTimePicker dtpCVDS;

	private Label label29;

	private DateTimePicker dtpCreaS;

	private Label label9;

	private ComboBox cobCer;

	private TextBox txtCernum;

	private Label label27;

	private Label label26;

	private TextBox txtRn;

	private Label label8;

	private TextBox txtGn;

	private Label label17;

	private DataGridView dgvList;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel tssLab1;

	private ToolStripStatusLabel tssLab4;

	private Label label7;

	private TextBox txtCE;

	private Label label10;

	private TextBox txtCS;

	public string m_objName = "WFsmc";

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmSMCard));
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.tssLab1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab4 = new System.Windows.Forms.ToolStripStatusLabel();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.txtCE = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtCS = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnExport = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.cobUser = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpCVDE = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cobFN = new System.Windows.Forms.ComboBox();
		this.cobBN = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpCreaE = new System.Windows.Forms.DateTimePicker();
		this.dtpCVDS = new System.Windows.Forms.DateTimePicker();
		this.label29 = new System.Windows.Forms.Label();
		this.dtpCreaS = new System.Windows.Forms.DateTimePicker();
		this.label9 = new System.Windows.Forms.Label();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.txtCernum = new System.Windows.Forms.TextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.txtRn = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtGn = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 151);
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersVisible = false;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(894, 292);
		this.dgvList.TabIndex = 2;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.tssLab1, this.tssLab4 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 443);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(894, 22);
		this.statusStrip1.TabIndex = 3;
		this.statusStrip1.Text = "statusStrip1";
		this.tssLab1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.tssLab1.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab1.Name = "tssLab1";
		this.tssLab1.Size = new System.Drawing.Size(848, 17);
		this.tssLab1.Spring = true;
		this.tssLab1.Text = "Total:";
		this.tssLab4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 1);
		this.tssLab4.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab4.Name = "tssLab4";
		this.tssLab4.Size = new System.Drawing.Size(0, 17);
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
		this.clsBackPanel1.Controls.Add(this.txtCE);
		this.clsBackPanel1.Controls.Add(this.label10);
		this.clsBackPanel1.Controls.Add(this.txtCS);
		this.clsBackPanel1.Controls.Add(this.label7);
		this.clsBackPanel1.Controls.Add(this.cobType);
		this.clsBackPanel1.Controls.Add(this.label1);
		this.clsBackPanel1.Controls.Add(this.btnClose);
		this.clsBackPanel1.Controls.Add(this.btnReset);
		this.clsBackPanel1.Controls.Add(this.btnExport);
		this.clsBackPanel1.Controls.Add(this.btnSearch);
		this.clsBackPanel1.Controls.Add(this.cobUser);
		this.clsBackPanel1.Controls.Add(this.label6);
		this.clsBackPanel1.Controls.Add(this.dtpCVDE);
		this.clsBackPanel1.Controls.Add(this.label5);
		this.clsBackPanel1.Controls.Add(this.label4);
		this.clsBackPanel1.Controls.Add(this.cobFN);
		this.clsBackPanel1.Controls.Add(this.cobBN);
		this.clsBackPanel1.Controls.Add(this.label3);
		this.clsBackPanel1.Controls.Add(this.label2);
		this.clsBackPanel1.Controls.Add(this.dtpCreaE);
		this.clsBackPanel1.Controls.Add(this.dtpCVDS);
		this.clsBackPanel1.Controls.Add(this.label29);
		this.clsBackPanel1.Controls.Add(this.dtpCreaS);
		this.clsBackPanel1.Controls.Add(this.label9);
		this.clsBackPanel1.Controls.Add(this.cobCer);
		this.clsBackPanel1.Controls.Add(this.txtCernum);
		this.clsBackPanel1.Controls.Add(this.label27);
		this.clsBackPanel1.Controls.Add(this.label26);
		this.clsBackPanel1.Controls.Add(this.txtRn);
		this.clsBackPanel1.Controls.Add(this.label8);
		this.clsBackPanel1.Controls.Add(this.txtGn);
		this.clsBackPanel1.Controls.Add(this.label17);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(894, 151);
		this.clsBackPanel1.TabIndex = 1;
		this.txtCE.Location = new System.Drawing.Point(234, 19);
		this.txtCE.Name = "txtCE";
		this.txtCE.Size = new System.Drawing.Size(95, 21);
		this.txtCE.TabIndex = 54;
		this.txtCE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCE_KeyPress);
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Location = new System.Drawing.Point(210, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(19, 15);
		this.label10.TabIndex = 53;
		this.label10.Text = "→";
		this.txtCS.Location = new System.Drawing.Point(109, 19);
		this.txtCS.Name = "txtCS";
		this.txtCS.Size = new System.Drawing.Size(95, 21);
		this.txtCS.TabIndex = 52;
		this.txtCS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCS_KeyPress);
		this.label7.Location = new System.Drawing.Point(3, 18);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(100, 23);
		this.label7.TabIndex = 51;
		this.label7.Text = "Card Number:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 160;
		this.cobType.FormattingEnabled = true;
		this.cobType.ItemHeight = 15;
		this.cobType.Location = new System.Drawing.Point(559, 52);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(140, 23);
		this.cobType.TabIndex = 14;
		this.label1.Location = new System.Drawing.Point(453, 51);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 23);
		this.label1.TabIndex = 13;
		this.label1.Text = "Card Type:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(647, 12);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 50;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnReset.AutoSize = true;
		this.btnReset.BackColor = System.Drawing.Color.LightGray;
		this.btnReset.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReset.ForeColor = System.Drawing.Color.Black;
		this.btnReset.GlowColor = System.Drawing.Color.White;
		this.btnReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReset.Image = LockSoftware.Properties.Resources.clear;
		this.btnReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnReset.Location = new System.Drawing.Point(570, 12);
		this.btnReset.Name = "btnReset";
		this.btnReset.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnReset.Size = new System.Drawing.Size(71, 34);
		this.btnReset.TabIndex = 49;
		this.btnReset.Text = "Reset";
		this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnExport.AutoSize = true;
		this.btnExport.BackColor = System.Drawing.Color.LightGray;
		this.btnExport.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExport.ForeColor = System.Drawing.Color.Black;
		this.btnExport.GlowColor = System.Drawing.Color.White;
		this.btnExport.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnExport.Image = LockSoftware.Properties.Resources.xls;
		this.btnExport.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnExport.Location = new System.Drawing.Point(436, 12);
		this.btnExport.Name = "btnExport";
		this.btnExport.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnExport.Size = new System.Drawing.Size(128, 34);
		this.btnExport.TabIndex = 48;
		this.btnExport.Text = "Export To Excel";
		this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.btnSearch.AutoEllipsis = true;
		this.btnSearch.BackColor = System.Drawing.Color.LightGray;
		this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSearch.ForeColor = System.Drawing.Color.Black;
		this.btnSearch.GlowColor = System.Drawing.Color.White;
		this.btnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSearch.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSearch.Location = new System.Drawing.Point(335, 12);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSearch.Size = new System.Drawing.Size(95, 34);
		this.btnSearch.TabIndex = 47;
		this.btnSearch.Text = "Search";
		this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
		this.cobUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobUser.DropDownWidth = 160;
		this.cobUser.FormattingEnabled = true;
		this.cobUser.ItemHeight = 15;
		this.cobUser.Location = new System.Drawing.Point(795, 52);
		this.cobUser.Name = "cobUser";
		this.cobUser.Size = new System.Drawing.Size(77, 23);
		this.cobUser.TabIndex = 22;
		this.label6.Location = new System.Drawing.Point(702, 51);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(87, 23);
		this.label6.TabIndex = 21;
		this.label6.Text = "Operator:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpCVDE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCVDE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCVDE.Location = new System.Drawing.Point(732, 108);
		this.dtpCVDE.Name = "dtpCVDE";
		this.dtpCVDE.ShowCheckBox = true;
		this.dtpCVDE.Size = new System.Drawing.Size(140, 21);
		this.dtpCVDE.TabIndex = 24;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(709, 113);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(19, 15);
		this.label5.TabIndex = 20;
		this.label5.Text = "→";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(709, 85);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(19, 15);
		this.label4.TabIndex = 19;
		this.label4.Text = "→";
		this.cobFN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFN.DropDownWidth = 160;
		this.cobFN.FormattingEnabled = true;
		this.cobFN.ItemHeight = 15;
		this.cobFN.Location = new System.Drawing.Point(335, 81);
		this.cobFN.Name = "cobFN";
		this.cobFN.Size = new System.Drawing.Size(112, 23);
		this.cobFN.TabIndex = 11;
		this.cobBN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBN.DropDownWidth = 160;
		this.cobBN.FormattingEnabled = true;
		this.cobBN.ItemHeight = 15;
		this.cobBN.Location = new System.Drawing.Point(335, 52);
		this.cobBN.Name = "cobBN";
		this.cobBN.Size = new System.Drawing.Size(112, 23);
		this.cobBN.TabIndex = 10;
		this.cobBN.SelectedIndexChanged += new System.EventHandler(cobBN_SelectedIndexChanged);
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(229, 80);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 23);
		this.label3.TabIndex = 8;
		this.label3.Text = "Floor Name:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(229, 51);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 23);
		this.label2.TabIndex = 7;
		this.label2.Text = "Building Name:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpCreaE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCreaE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCreaE.Location = new System.Drawing.Point(732, 81);
		this.dtpCreaE.Name = "dtpCreaE";
		this.dtpCreaE.ShowCheckBox = true;
		this.dtpCreaE.Size = new System.Drawing.Size(140, 21);
		this.dtpCreaE.TabIndex = 23;
		this.dtpCVDS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCVDS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCVDS.Location = new System.Drawing.Point(559, 108);
		this.dtpCVDS.Name = "dtpCVDS";
		this.dtpCVDS.ShowCheckBox = true;
		this.dtpCVDS.Size = new System.Drawing.Size(140, 21);
		this.dtpCVDS.TabIndex = 18;
		this.label29.BackColor = System.Drawing.Color.Transparent;
		this.label29.Location = new System.Drawing.Point(453, 107);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(100, 23);
		this.label29.TabIndex = 17;
		this.label29.Text = "Card Validdate:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpCreaS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCreaS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCreaS.Location = new System.Drawing.Point(559, 81);
		this.dtpCreaS.Name = "dtpCreaS";
		this.dtpCreaS.ShowCheckBox = true;
		this.dtpCreaS.Size = new System.Drawing.Size(140, 21);
		this.dtpCreaS.TabIndex = 16;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Location = new System.Drawing.Point(453, 80);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(100, 23);
		this.label9.TabIndex = 15;
		this.label9.Text = "Create Date:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 160;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.ItemHeight = 12;
		this.cobCer.Location = new System.Drawing.Point(109, 81);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(110, 20);
		this.cobCer.TabIndex = 4;
		this.txtCernum.Location = new System.Drawing.Point(109, 108);
		this.txtCernum.Name = "txtCernum";
		this.txtCernum.Size = new System.Drawing.Size(110, 21);
		this.txtCernum.TabIndex = 6;
		this.label27.BackColor = System.Drawing.Color.Transparent;
		this.label27.Location = new System.Drawing.Point(3, 107);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(100, 23);
		this.label27.TabIndex = 5;
		this.label27.Text = "Number:";
		this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label26.BackColor = System.Drawing.Color.Transparent;
		this.label26.Location = new System.Drawing.Point(3, 80);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(100, 23);
		this.label26.TabIndex = 3;
		this.label26.Text = "Certificate:";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRn.ForeColor = System.Drawing.Color.Black;
		this.txtRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtRn.Location = new System.Drawing.Point(335, 108);
		this.txtRn.Name = "txtRn";
		this.txtRn.Size = new System.Drawing.Size(112, 21);
		this.txtRn.TabIndex = 12;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Location = new System.Drawing.Point(229, 107);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(100, 23);
		this.label8.TabIndex = 9;
		this.label8.Text = "Room Name:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtGn.Location = new System.Drawing.Point(109, 52);
		this.txtGn.Name = "txtGn";
		this.txtGn.Size = new System.Drawing.Size(110, 21);
		this.txtGn.TabIndex = 2;
		this.label17.BackColor = System.Drawing.Color.Transparent;
		this.label17.Location = new System.Drawing.Point(3, 51);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(100, 23);
		this.label17.TabIndex = 1;
		this.label17.Text = "User Name:";
		this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(894, 465);
		base.Controls.Add(this.dgvList);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.clsBackPanel1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmSMCard";
		this.Text = "frmSMCard";
		base.Load += new System.EventHandler(frmSMCard_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmSMCard()
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
			string sql = "Select * FROM D_Cer ";
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

	private void InitType()
	{
		try
		{
			cobType.DataSource = null;
			XmlNodeList elements = new ClassXml(Program.m_lansDt.Rows[Program.m_Lan]["fpath"].ToString(), "Radio").GetElements("Radio/Info_Public/Info_Bind");
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Value");
			dataTable.Rows.Add("-1", "");
			foreach (XmlNode item in elements)
			{
				string text = item.Attributes["Name"].Value.Split('_')[1];
				if (text != "005" && text != "008" && text != "255")
				{
					dataTable.Rows.Add(text, item.Attributes["Value"].Value);
				}
			}
			cobType.DisplayMember = "Value";
			cobType.ValueMember = "Name";
			cobType.DataSource = dataTable.DefaultView;
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

	public string GetPars()
	{
		try
		{
			string text = "";
			if (cobCer.DataSource != null && Convert.ToInt32(cobCer.SelectedValue) > 0)
			{
				text = text + " And cer_id=" + Convert.ToInt32(cobCer.SelectedValue);
			}
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
			return text;
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return "";
		}
	}

	private string GetParsG()
	{
		string text = "";
		try
		{
			if (txtCS.Text.Trim() != "")
			{
				text = text + " And r_cardnum >= " + txtCS.Text.Trim();
			}
			if (txtCE.Text.Trim() != "")
			{
				text = text + " And r_cardnum <= " + txtCE.Text.Trim();
			}
			if (txtGn.Text.Trim() != "")
			{
				text = text + " And g_name like N'" + txtGn.Text.Trim() + "%'";
			}
			if (txtCernum.Text.Trim() != "")
			{
				text = text + " And g_cernum like N'" + txtCernum.Text.Trim() + "%'";
			}
			if (dtpCreaS.Checked)
			{
				text = text + " And Createtime >= '" + Program.GetStandDTime(dtpCreaS.Value, "00") + "'";
			}
			if (dtpCreaE.Checked)
			{
				text = text + " And Createtime <= '" + Program.GetStandDTime(dtpCreaE.Value, "59") + "'";
			}
			if (dtpCVDS.Checked)
			{
				text = text + " And g_stand_L_time >= '" + Program.GetStandDTime(dtpCVDS.Value, "00") + "'";
			}
			if (dtpCVDE.Checked)
			{
				text = text + " And g_stand_L_time <= '" + Program.GetStandDTime(dtpCVDE.Value, "59") + "'";
			}
			if (cobUser.DataSource != null && Convert.ToInt32(cobUser.SelectedValue) > 0)
			{
				text = text + " And Creator_id=" + Convert.ToInt32(cobUser.SelectedValue);
			}
		}
		catch
		{
		}
		return text;
	}

	private string GetParsM()
	{
		string text = "";
		try
		{
			if (txtCS.Text.Trim() != "")
			{
				text = text + " And cm_cardid >= " + txtCS.Text.Trim();
			}
			if (txtCE.Text.Trim() != "")
			{
				text = text + " And cm_cardid <= " + txtCE.Text.Trim();
			}
			if (txtGn.Text.Trim() != "")
			{
				text = text + " And cm_user like N'" + txtGn.Text.Trim() + "%'";
			}
			if (txtCernum.Text.Trim() != "")
			{
				text = text + " And cm_cernum like N'" + txtCernum.Text.Trim() + "%'";
			}
			if (dtpCreaS.Checked)
			{
				text = text + " And cm_Createtime >= '" + Program.GetStandDTime(dtpCreaS.Value, "00") + "'";
			}
			if (dtpCreaE.Checked)
			{
				text = text + " And cm_Createtime <= '" + Program.GetStandDTime(dtpCreaE.Value, "59") + "'";
			}
			if (dtpCVDS.Checked)
			{
				text = text + " And cm_carddate >= '" + Program.GetStandDTime(dtpCVDS.Value, "00") + "'";
			}
			if (dtpCVDE.Checked)
			{
				text = text + " And cm_carddate <= '" + Program.GetStandDTime(dtpCVDE.Value, "59") + "'";
			}
			if (cobUser.DataSource != null && Convert.ToInt32(cobUser.SelectedValue) > 0)
			{
				text = text + " And cm_Creatorid=" + Convert.ToInt32(cobUser.SelectedValue);
			}
			if (cobType.DataSource != null && Convert.ToInt32(cobType.SelectedValue) >= 0)
			{
				text = text + " And ct_code =" + Convert.ToInt32(cobType.SelectedValue);
			}
		}
		catch
		{
		}
		return text;
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		try
		{
			string pars = GetPars();
			int num = -1;
			if (cobType.DataSource != null && Convert.ToInt32(cobType.SelectedValue) >= 0)
			{
				num = Convert.ToInt32(cobType.SelectedValue);
			}
			string text = "Select (Row_Number() OVER (Order by CardNum, Createtime )) AS RowNumber, * From (";
			if (num == -1 || num == 6)
			{
				string text2 = text;
				text = text2 + "Select r_cardnum As CardNum,6 as ct_code, N'' As CardType, g_name As UserName, cer_name, g_cernum As cernum, (Build_Name + ' ' + Floor_name + ' ' + r_name) As LockAddr, Cast(0 As bit) As r_oplock, Cast(0 As bit) As r_opkeep, Createtime, CONVERT(varchar, g_stand_L_time, 120) As CardDate, Creator, g_logout As Logout, g_logoutdate As LogoutDate, Updator, UpdateTime From v_CardGuest Where g_wcard=1 " + pars + GetParsG() + "  Union all ";
			}
			if (num != 6 && num != 9)
			{
				text += "Select cm_cardid As CardNum,ct_code";
				text = ((num == -1) ? (text + ",'' as CardType,") : (text + ",N''as CardType,"));
				string text3 = text;
				text = text3 + " cm_user As UserName, cer_name, cm_cernum As cernum, (bl_name + ' ' + f_name + ' ' + r_name) As LockAddr, r_oplock, r_opkeep, cm_Createtime As Createtime, (RTrim(cm_carddate) + ' ' + RTrim(cm_carddateST) + '→' + RTrim(cm_carddateET)) As CardDate, cm_Creator As Creator, cm_logout As Logout, cm_logoutdate As LogoutDate, cm_Updator As Updator, cm_updatetime As UpdateTime From v_CardMgr Where 1=1 And ct_code <> 9" + pars + GetParsM() + " Union all ";
			}
			if (num == -1 || num == 9)
			{
				string text4 = text;
				text = text4 + "Select distinct cm_cardid As CardNum,9 as ct_code,N''as CardType, cm_user As UserName, cer_name, cm_cernum As cernum, (dbo.grpJoinStr(cm_id)) As LockAddr, r_oplock, r_opkeep, cm_Createtime As Createtime, (RTrim(cm_carddate) + ' ' + RTrim(cm_carddateST) + '→' + RTrim(cm_carddateET)) As CardDate, cm_Creator As Creator, cm_logout As Logout, cm_logoutdate As LogoutDate, cm_Updator As Updator, cm_updatetime As UpdateTime From v_CardGrp Where ct_code = 9 " + pars + GetParsM() + " Union all ";
			}
			text = text.Remove(text.LastIndexOf("Union all"), 9);
			text += ") As TmpTab";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null)
			{
				dgvList.DataSource = dataTable.DefaultView;
				dgvList.Columns[2].Visible = false;
				for (int i = 0; i < dgvList.Columns.Count; i++)
				{
					dgvList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvList.Columns[i].Name];
				}
				for (int j = 0; j < dgvList.Rows.Count; j++)
				{
					int num2 = Convert.ToInt32(dgvList.Rows[j].Cells[2].Value);
					dgvList.Rows[j].Cells[3].Value = Program.m_hPubTab[(num2 > 9) ? ("devct" + num2) : ("devct0" + num2)];
				}
				dgvList.AutoResizeColumns();
			}
			tssLab1.Text = string.Format((string)m_htab["tssLab1"], dgvList.Rows.Count);
		}
		catch
		{
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

	private void frmSMCard_Load(object sender, EventArgs e)
	{
		tssLab1.Text = "";
		btnExport.Enabled = SQLserver.GetUserPermisstion(1041, Program.m_OperID);
		dtpCreaE.CustomFormat = Program.m_currDateTimeFmt;
		dtpCreaS.CustomFormat = Program.m_currDateTimeFmt;
		dtpCVDE.CustomFormat = Program.m_currDateTimeFmt;
		dtpCVDS.CustomFormat = Program.m_currDateTimeFmt;
		DateTime now = DateTime.Now;
		string locDate = Program.GetLocDate(now);
		dtpCreaS.Value = Convert.ToDateTime(Program.GetLocDate(now.AddMonths(-1)) + " 00:00");
		dtpCreaE.Value = Convert.ToDateTime(locDate + " 23:59");
		dtpCVDS.Value = Convert.ToDateTime(locDate + " 00:00");
		dtpCVDE.Value = Convert.ToDateTime(locDate + " 23:59");
		DateTimePicker dateTimePicker = dtpCreaS;
		DateTimePicker dateTimePicker2 = dtpCreaE;
		DateTimePicker dateTimePicker3 = dtpCVDS;
		bool flag = (dtpCVDE.Checked = false);
		bool flag3 = (dateTimePicker3.Checked = flag);
		bool flag5 = (dateTimePicker2.Checked = flag3);
		dateTimePicker.Checked = flag5;
		InitCerType();
		InitType();
		InitOper();
		InitBuild();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		try
		{
			TextBox textBox = txtCS;
			TextBox textBox2 = txtCE;
			TextBox textBox3 = txtRn;
			TextBox textBox4 = txtGn;
			string text = (txtCernum.Text = "");
			string text3 = (textBox4.Text = text);
			string text5 = (textBox3.Text = text3);
			string text7 = (textBox2.Text = text5);
			textBox.Text = text7;
			if (cobFN.DataSource != null)
			{
				cobFN.SelectedIndex = 0;
			}
			cobUser.SelectedIndex = -1;
			cobUser.Text = "";
			DateTime now = DateTime.Now;
			string locDate = Program.GetLocDate(now);
			dtpCreaS.Value = Convert.ToDateTime(Program.GetLocDate(now.AddMonths(-1)) + " " + Program.m_defLeaveTime + ":00");
			dtpCreaE.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":59");
			dtpCVDS.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":00");
			dtpCVDE.Value = Convert.ToDateTime(locDate + " " + Program.m_defLeaveTime + ":59");
			DateTimePicker dateTimePicker = dtpCreaS;
			DateTimePicker dateTimePicker2 = dtpCreaE;
			DateTimePicker dateTimePicker3 = dtpCVDS;
			bool flag = (dtpCVDE.Checked = false);
			bool flag3 = (dateTimePicker3.Checked = flag);
			bool flag5 = (dateTimePicker2.Checked = flag3);
			dateTimePicker.Checked = flag5;
			ComboBox comboBox = cobCer;
			ComboBox comboBox2 = cobBN;
			int num = (cobType.SelectedIndex = 0);
			int selectedIndex = (comboBox2.SelectedIndex = num);
			comboBox.SelectedIndex = selectedIndex;
		}
		catch
		{
		}
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			if (dgvList.DataSource == null || dgvList.Rows.Count <= 0)
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

	private void txtCS_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtCE_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}
}
