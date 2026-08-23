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

public class frmGrpRoom : Form
{
	private IContainer components;

	private Panel panel1;

	private DataGridView dgvRList;

	private Panel panel3;

	private ToolsBtn btnMovAll;

	private ToolsBtn btnMovCur;

	private ToolsBtn btnAddCur;

	private ToolsBtn btnAddAll;

	private clsBackPanel clsBackPanel5;

	private ComboBox cobFD;

	private ComboBox cobBD;

	private Panel panel4;

	private clsBackPanel clsBackPanel6;

	private NGlassBtn btnDel;

	private NGlassBtn btnEdit;

	private NGlassBtn btnNew;

	private ComboBox cobGN;

	private TextBox txtGC;

	private Label label13;

	private Label label12;

	private DataGridView dgvGList;

	private TextBox txtSRn;

	private ToolsBtn btnSear;

	private FlowLayoutPanel flowLayoutPanel2;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel TSSLab01;

	private ToolStripStatusLabel TSSLabGrp;

	private ToolStripStatusLabel TSSLab02;

	private ToolStripStatusLabel TSSLabRoom;

	private NGlassBtn btnSG;

	public string m_objName = "WFgrp";

	public Hashtable m_htab;

	private Label label1 = new Label();

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmGrpRoom));
		this.panel1 = new System.Windows.Forms.Panel();
		this.dgvRList = new System.Windows.Forms.DataGridView();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnMovAll = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnMovCur = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnAddCur = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnAddAll = new LockSoftware.Controls.ToolsBtn(this.components);
		this.clsBackPanel5 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.cobFD = new System.Windows.Forms.ComboBox();
		this.cobBD = new System.Windows.Forms.ComboBox();
		this.txtSRn = new System.Windows.Forms.TextBox();
		this.btnSear = new LockSoftware.Controls.ToolsBtn(this.components);
		this.panel4 = new System.Windows.Forms.Panel();
		this.dgvGList = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.TSSLab01 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLabGrp = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLabRoom = new System.Windows.Forms.ToolStripStatusLabel();
		this.clsBackPanel6 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.label12 = new System.Windows.Forms.Label();
		this.cobGN = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtGC = new System.Windows.Forms.TextBox();
		this.btnNew = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnEdit = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDel = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnSG = new LockSoftware.Controls.NGlassBtn(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRList).BeginInit();
		this.panel3.SuspendLayout();
		this.clsBackPanel5.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvGList).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.clsBackPanel6.SuspendLayout();
		this.flowLayoutPanel2.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.dgvRList);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.clsBackPanel5);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(368, 518);
		this.panel1.TabIndex = 15;
		this.dgvRList.AllowUserToAddRows = false;
		this.dgvRList.AllowUserToDeleteRows = false;
		this.dgvRList.BackgroundColor = System.Drawing.Color.White;
		this.dgvRList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Times New Roman", 9f);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvRList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dgvRList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9f);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvRList.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvRList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvRList.Location = new System.Drawing.Point(0, 40);
		this.dgvRList.Name = "dgvRList";
		this.dgvRList.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9f);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvRList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvRList.RowHeadersWidth = 25;
		this.dgvRList.RowTemplate.Height = 23;
		this.dgvRList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRList.Size = new System.Drawing.Size(324, 478);
		this.dgvRList.TabIndex = 14;
		this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
		this.panel3.Controls.Add(this.btnMovAll);
		this.panel3.Controls.Add(this.btnMovCur);
		this.panel3.Controls.Add(this.btnAddCur);
		this.panel3.Controls.Add(this.btnAddAll);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel3.Location = new System.Drawing.Point(324, 40);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(44, 478);
		this.panel3.TabIndex = 16;
		this.btnMovAll.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnMovAll.BackColor = System.Drawing.Color.Transparent;
		this.btnMovAll.Checked = false;
		this.btnMovAll.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnMovAll.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnMovAll.DefaultColor = System.Drawing.Color.Transparent;
		this.btnMovAll.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnMovAll.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.btnMovAll.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnMovAll.ImageNew = null;
		this.btnMovAll.ImageRedrawed = true;
		this.btnMovAll.ImageStyle = 0;
		this.btnMovAll.isButton = true;
		this.btnMovAll.Location = new System.Drawing.Point(6, 275);
		this.btnMovAll.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnMovAll.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnMovAll.MouseDownStartColor = System.Drawing.Color.White;
		this.btnMovAll.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnMovAll.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnMovAll.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnMovAll.Name = "btnMovAll";
		this.btnMovAll.Size = new System.Drawing.Size(32, 25);
		this.btnMovAll.TabIndex = 14;
		this.btnMovAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnMovAll.TextImageLocation = 0;
		this.btnMovAll.TextNew = "|<";
		this.btnMovAll.TextRedrawed = true;
		this.btnMovAll.Click += new System.EventHandler(btnMovAll_Click);
		this.btnMovAll.MouseLeave += new System.EventHandler(btnMovAll_MouseLeave);
		this.btnMovAll.MouseMove += new System.Windows.Forms.MouseEventHandler(btnMovAll_MouseMove);
		this.btnMovCur.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnMovCur.BackColor = System.Drawing.Color.Transparent;
		this.btnMovCur.Checked = false;
		this.btnMovCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnMovCur.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnMovCur.DefaultColor = System.Drawing.Color.Transparent;
		this.btnMovCur.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnMovCur.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.btnMovCur.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnMovCur.ImageNew = null;
		this.btnMovCur.ImageRedrawed = true;
		this.btnMovCur.ImageStyle = 0;
		this.btnMovCur.isButton = true;
		this.btnMovCur.Location = new System.Drawing.Point(6, 241);
		this.btnMovCur.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnMovCur.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnMovCur.MouseDownStartColor = System.Drawing.Color.White;
		this.btnMovCur.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnMovCur.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnMovCur.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnMovCur.Name = "btnMovCur";
		this.btnMovCur.Size = new System.Drawing.Size(32, 25);
		this.btnMovCur.TabIndex = 13;
		this.btnMovCur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnMovCur.TextImageLocation = 0;
		this.btnMovCur.TextNew = "<<";
		this.btnMovCur.TextRedrawed = true;
		this.btnMovCur.Click += new System.EventHandler(btnMovCur_Click);
		this.btnMovCur.MouseLeave += new System.EventHandler(btnMovCur_MouseLeave);
		this.btnMovCur.MouseMove += new System.Windows.Forms.MouseEventHandler(btnMovCur_MouseMove);
		this.btnAddCur.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnAddCur.BackColor = System.Drawing.Color.Transparent;
		this.btnAddCur.Checked = false;
		this.btnAddCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnAddCur.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnAddCur.DefaultColor = System.Drawing.Color.Transparent;
		this.btnAddCur.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnAddCur.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.btnAddCur.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnAddCur.ImageNew = null;
		this.btnAddCur.ImageRedrawed = true;
		this.btnAddCur.ImageStyle = 0;
		this.btnAddCur.isButton = true;
		this.btnAddCur.Location = new System.Drawing.Point(6, 207);
		this.btnAddCur.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnAddCur.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnAddCur.MouseDownStartColor = System.Drawing.Color.White;
		this.btnAddCur.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnAddCur.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnAddCur.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnAddCur.Name = "btnAddCur";
		this.btnAddCur.Size = new System.Drawing.Size(32, 25);
		this.btnAddCur.TabIndex = 12;
		this.btnAddCur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnAddCur.TextImageLocation = 0;
		this.btnAddCur.TextNew = ">>";
		this.btnAddCur.TextRedrawed = true;
		this.btnAddCur.Click += new System.EventHandler(btnAddCur_Click);
		this.btnAddCur.MouseLeave += new System.EventHandler(btnAddCur_MouseLeave);
		this.btnAddCur.MouseMove += new System.Windows.Forms.MouseEventHandler(btnAddCur_MouseMove);
		this.btnAddAll.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.btnAddAll.BackColor = System.Drawing.Color.Transparent;
		this.btnAddAll.Checked = false;
		this.btnAddAll.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnAddAll.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnAddAll.DefaultColor = System.Drawing.Color.Transparent;
		this.btnAddAll.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.btnAddAll.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.btnAddAll.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnAddAll.ImageNew = null;
		this.btnAddAll.ImageRedrawed = true;
		this.btnAddAll.ImageStyle = 0;
		this.btnAddAll.isButton = true;
		this.btnAddAll.Location = new System.Drawing.Point(6, 173);
		this.btnAddAll.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnAddAll.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnAddAll.MouseDownStartColor = System.Drawing.Color.White;
		this.btnAddAll.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnAddAll.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnAddAll.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnAddAll.Name = "btnAddAll";
		this.btnAddAll.Size = new System.Drawing.Size(32, 25);
		this.btnAddAll.TabIndex = 11;
		this.btnAddAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnAddAll.TextImageLocation = 0;
		this.btnAddAll.TextNew = ">|";
		this.btnAddAll.TextRedrawed = true;
		this.btnAddAll.Click += new System.EventHandler(btnAddAll_Click);
		this.btnAddAll.MouseLeave += new System.EventHandler(btnAddAll_MouseLeave);
		this.btnAddAll.MouseMove += new System.Windows.Forms.MouseEventHandler(btnAddAll_MouseMove);
		this.clsBackPanel5.Border = true;
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
		this.clsBackPanel5.Color1 = System.Drawing.Color.White;
		this.clsBackPanel5.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel5.ColorAngle = 90f;
		this.clsBackPanel5.Controls.Add(this.cobFD);
		this.clsBackPanel5.Controls.Add(this.cobBD);
		this.clsBackPanel5.Controls.Add(this.txtSRn);
		this.clsBackPanel5.Controls.Add(this.btnSear);
		this.clsBackPanel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel5.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel5.Name = "clsBackPanel5";
		this.clsBackPanel5.Size = new System.Drawing.Size(368, 40);
		this.clsBackPanel5.TabIndex = 13;
		this.cobFD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFD.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobFD.FormattingEnabled = true;
		this.cobFD.Location = new System.Drawing.Point(121, 10);
		this.cobFD.Name = "cobFD";
		this.cobFD.Size = new System.Drawing.Size(105, 24);
		this.cobFD.TabIndex = 13;
		this.cobBD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBD.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobBD.FormattingEnabled = true;
		this.cobBD.Location = new System.Drawing.Point(12, 10);
		this.cobBD.Name = "cobBD";
		this.cobBD.Size = new System.Drawing.Size(105, 24);
		this.cobBD.TabIndex = 12;
		this.cobBD.SelectedIndexChanged += new System.EventHandler(cobBD_SelectedIndexChanged);
		this.txtSRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSRn.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSRn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtSRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtSRn.Location = new System.Drawing.Point(230, 10);
		this.txtSRn.Name = "txtSRn";
		this.txtSRn.Size = new System.Drawing.Size(95, 24);
		this.txtSRn.TabIndex = 16;
		this.txtSRn.Text = "ROOM NAME...";
		this.txtSRn.Enter += new System.EventHandler(txtSRn_Enter);
		this.txtSRn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSRn_KeyDown);
		this.txtSRn.Leave += new System.EventHandler(txtSRn_Leave);
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
		this.btnSear.Location = new System.Drawing.Point(0, 3);
		this.btnSear.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnSear.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnSear.MouseDownStartColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnSear.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnSear.Name = "btnSear";
		this.btnSear.Size = new System.Drawing.Size(368, 37);
		this.btnSear.TabIndex = 15;
		this.btnSear.TextImageLocation = 0;
		this.btnSear.TextNew = "";
		this.btnSear.TextRedrawed = false;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.btnSear.MouseLeave += new System.EventHandler(btnSear_MouseLeave);
		this.btnSear.MouseMove += new System.Windows.Forms.MouseEventHandler(btnSear_MouseMove);
		this.panel4.Controls.Add(this.dgvGList);
		this.panel4.Controls.Add(this.statusStrip1);
		this.panel4.Controls.Add(this.clsBackPanel6);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(368, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(426, 518);
		this.panel4.TabIndex = 17;
		this.dgvGList.AllowUserToAddRows = false;
		this.dgvGList.AllowUserToDeleteRows = false;
		this.dgvGList.BackgroundColor = System.Drawing.Color.White;
		this.dgvGList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9f);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvGList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvGList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvGList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvGList.Location = new System.Drawing.Point(0, 40);
		this.dgvGList.Name = "dgvGList";
		this.dgvGList.ReadOnly = true;
		this.dgvGList.RowHeadersWidth = 25;
		this.dgvGList.RowTemplate.Height = 23;
		this.dgvGList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvGList.Size = new System.Drawing.Size(426, 452);
		this.dgvGList.TabIndex = 3;
		this.statusStrip1.AutoSize = false;
		this.statusStrip1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSSLab01, this.TSSLabGrp, this.TSSLab02, this.TSSLabRoom });
		this.statusStrip1.Location = new System.Drawing.Point(0, 492);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(426, 26);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 4;
		this.statusStrip1.Text = "statusStrip1";
		this.TSSLab01.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab01.Name = "TSSLab01";
		this.TSSLab01.Size = new System.Drawing.Size(98, 21);
		this.TSSLab01.Text = "Current Group:";
		this.TSSLabGrp.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLabGrp.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLabGrp.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLabGrp.Name = "TSSLabGrp";
		this.TSSLabGrp.Size = new System.Drawing.Size(116, 21);
		this.TSSLabGrp.Spring = true;
		this.TSSLabGrp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab02.Name = "TSSLab02";
		this.TSSLab02.Size = new System.Drawing.Size(81, 21);
		this.TSSLab02.Text = "Total Room:";
		this.TSSLabRoom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLabRoom.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLabRoom.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLabRoom.Name = "TSSLabRoom";
		this.TSSLabRoom.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLabRoom.Size = new System.Drawing.Size(116, 21);
		this.TSSLabRoom.Spring = true;
		this.TSSLabRoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.clsBackPanel6.Border = true;
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
		this.clsBackPanel6.Color1 = System.Drawing.Color.White;
		this.clsBackPanel6.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel6.ColorAngle = 90f;
		this.clsBackPanel6.Controls.Add(this.flowLayoutPanel2);
		this.clsBackPanel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel6.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel6.Name = "clsBackPanel6";
		this.clsBackPanel6.Size = new System.Drawing.Size(426, 40);
		this.clsBackPanel6.TabIndex = 0;
		this.flowLayoutPanel2.AutoScroll = true;
		this.flowLayoutPanel2.AutoSize = true;
		this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel2.Controls.Add(this.label12);
		this.flowLayoutPanel2.Controls.Add(this.cobGN);
		this.flowLayoutPanel2.Controls.Add(this.label13);
		this.flowLayoutPanel2.Controls.Add(this.txtGC);
		this.flowLayoutPanel2.Controls.Add(this.btnNew);
		this.flowLayoutPanel2.Controls.Add(this.btnEdit);
		this.flowLayoutPanel2.Controls.Add(this.btnDel);
		this.flowLayoutPanel2.Controls.Add(this.btnSG);
		this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(2, 6, 0, 0);
		this.flowLayoutPanel2.Size = new System.Drawing.Size(426, 40);
		this.flowLayoutPanel2.TabIndex = 8;
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(5, 11);
		this.label12.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(90, 17);
		this.label12.TabIndex = 0;
		this.label12.Text = "Group Name:";
		this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.cobGN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobGN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobGN.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobGN.FormattingEnabled = true;
		this.cobGN.Location = new System.Drawing.Point(101, 9);
		this.cobGN.MaxLength = 50;
		this.cobGN.Name = "cobGN";
		this.cobGN.Size = new System.Drawing.Size(93, 24);
		this.cobGN.TabIndex = 4;
		this.cobGN.SelectedIndexChanged += new System.EventHandler(cobGN_SelectedIndexChanged);
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.Color.Transparent;
		this.label13.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(200, 11);
		this.label13.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(45, 17);
		this.label13.TabIndex = 2;
		this.label13.Text = "Code:";
		this.txtGC.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtGC.Location = new System.Drawing.Point(251, 9);
		this.txtGC.MaxLength = 3;
		this.txtGC.Name = "txtGC";
		this.txtGC.Size = new System.Drawing.Size(50, 24);
		this.txtGC.TabIndex = 3;
		this.txtGC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGC_KeyPress);
		this.btnNew.BackColor = System.Drawing.Color.Transparent;
		this.btnNew.BaseColor = System.Drawing.Color.White;
		this.btnNew.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnNew.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnNew.ButtonText = null;
		this.btnNew.CornerRadius = 2;
		this.btnNew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNew.Image = LockSoftware.Properties.Resources.Add;
		this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnNew.ImageSize = new System.Drawing.Size(16, 16);
		this.btnNew.Location = new System.Drawing.Point(307, 9);
		this.btnNew.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(24, 24);
		this.btnNew.TabIndex = 5;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.btnNew.MouseLeave += new System.EventHandler(btnNew_MouseLeave);
		this.btnNew.MouseMove += new System.Windows.Forms.MouseEventHandler(btnNew_MouseMove);
		this.btnEdit.BackColor = System.Drawing.Color.Transparent;
		this.btnEdit.BaseColor = System.Drawing.Color.White;
		this.btnEdit.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnEdit.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnEdit.ButtonText = null;
		this.btnEdit.CornerRadius = 2;
		this.btnEdit.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnEdit.Image = LockSoftware.Properties.Resources.table_save;
		this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnEdit.ImageSize = new System.Drawing.Size(16, 16);
		this.btnEdit.Location = new System.Drawing.Point(336, 9);
		this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
		this.btnEdit.Name = "btnEdit";
		this.btnEdit.Size = new System.Drawing.Size(24, 24);
		this.btnEdit.TabIndex = 6;
		this.btnEdit.Click += new System.EventHandler(btnEdit_Click);
		this.btnEdit.MouseLeave += new System.EventHandler(btnEdit_MouseLeave);
		this.btnEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(btnEdit_MouseMove);
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
		this.btnDel.Location = new System.Drawing.Point(365, 9);
		this.btnDel.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
		this.btnDel.Name = "btnDel";
		this.btnDel.Size = new System.Drawing.Size(24, 24);
		this.btnDel.TabIndex = 7;
		this.btnDel.Click += new System.EventHandler(btnDel_Click);
		this.btnDel.MouseLeave += new System.EventHandler(btnDel_MouseLeave);
		this.btnDel.MouseMove += new System.Windows.Forms.MouseEventHandler(btnDel_MouseMove);
		this.btnSG.BackColor = System.Drawing.Color.Transparent;
		this.btnSG.BaseColor = System.Drawing.Color.White;
		this.btnSG.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnSG.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnSG.ButtonText = null;
		this.btnSG.CornerRadius = 2;
		this.btnSG.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSG.Image = LockSoftware.Properties.Resources.search;
		this.btnSG.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnSG.Location = new System.Drawing.Point(394, 9);
		this.btnSG.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
		this.btnSG.Name = "btnSG";
		this.btnSG.Size = new System.Drawing.Size(24, 24);
		this.btnSG.TabIndex = 8;
		this.btnSG.Visible = false;
		this.btnSG.Click += new System.EventHandler(btnSG_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(794, 518);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmGrpRoom";
		this.Text = "Group Setting";
		base.Load += new System.EventHandler(frmGrpRoom_Load);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvRList).EndInit();
		this.panel3.ResumeLayout(false);
		this.clsBackPanel5.ResumeLayout(false);
		this.clsBackPanel5.PerformLayout();
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvGList).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.clsBackPanel6.ResumeLayout(false);
		this.clsBackPanel6.PerformLayout();
		this.flowLayoutPanel2.ResumeLayout(false);
		this.flowLayoutPanel2.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmGrpRoom()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		base.Controls.Add(label1);
		label1.Font = new Font("Times New Roman", 12f, FontStyle.Regular, GraphicsUnit.Point, 1);
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
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

	private void InitRoomListColumn()
	{
		try
		{
			dgvRList.Columns.Clear();
			dgvRList.Columns.Add("R_ID", "");
			dgvRList.Columns.Add("R_Name", (string)m_htab["dgvcolR_Name"]);
			dgvRList.Columns.Add("Build_ID", "");
			dgvRList.Columns.Add("Build_Name", (string)m_htab["dgvcolBuild_Name"]);
			dgvRList.Columns.Add("floor_id", "");
			dgvRList.Columns.Add("Floor_Name", (string)m_htab["dgvcolFloor_Name"]);
			dgvRList.Columns.Add("TP_Name", (string)m_htab["dgvcolTP_Name"]);
			DataGridViewColumn dataGridViewColumn = dgvRList.Columns["r_id"];
			DataGridViewColumn dataGridViewColumn2 = dgvRList.Columns["floor_id"];
			bool flag = (dgvRList.Columns["build_ID"].Visible = false);
			bool visible = (dataGridViewColumn2.Visible = flag);
			dataGridViewColumn.Visible = visible;
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

	private void InitRoomList(int bid, int fid, int gid)
	{
		try
		{
			dgvRList.Rows.Clear();
			string text = "Select R_ID, R_Name, Build_ID, Build_Name, R_FloorID As floor_id, Floor_Name, TP_Name From v_HotelRooms Where 1=1 And R_flag=0";
			if (fid > 0)
			{
				text = text + " And  R_FloorID=" + fid;
			}
			if (bid > 0)
			{
				text = text + " And  Build_ID=" + bid;
			}
			if (txtSRn.ForeColor == Color.Black && txtSRn.Text.Trim() != "")
			{
				text = text + " And R_Name like N'" + txtSRn.Text.Trim() + "%'";
			}
			text = text + " And R_ID not in(select r_id From v_GrpRoom Where RGT_id=" + gid + " And RG_Flag=0)";
			text += " Order by Build_Name, Floor_Name, R_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			object[] array = new object[7];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					array[j] = dataTable.Rows[i][j];
				}
				dgvRList.Rows.Add(array);
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitGroup()
	{
		try
		{
			cobGN.DataSource = null;
			string sql = "Select  RGT_id, RGT_name, RGT_code, createtime FROM D_RoomGroupType Where RGT_flag=0 Order by RGT_name ";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				cobGN.DisplayMember = "RGT_name";
				cobGN.ValueMember = "RGT_id";
				cobGN.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err10"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitGroupListColumn()
	{
		try
		{
			dgvGList.Columns.Clear();
			dgvGList.Columns.Add("R_ID", "");
			dgvGList.Columns.Add("R_Name", (string)m_htab["dgvcolR_Name"]);
			dgvGList.Columns.Add("build_ID", "");
			dgvGList.Columns.Add("Build_Name", (string)m_htab["dgvcolBuild_Name"]);
			dgvGList.Columns.Add("floor_id", "");
			dgvGList.Columns.Add("Floor_Name", (string)m_htab["dgvcolFloor_Name"]);
			dgvGList.Columns.Add("TP_Name", (string)m_htab["dgvcolTP_Name"]);
			DataGridViewColumn dataGridViewColumn = dgvGList.Columns["r_id"];
			DataGridViewColumn dataGridViewColumn2 = dgvGList.Columns["floor_id"];
			bool flag = (dgvGList.Columns["build_ID"].Visible = false);
			bool visible = (dataGridViewColumn2.Visible = flag);
			dataGridViewColumn.Visible = visible;
			for (int i = 0; i < dgvGList.Columns.Count; i++)
			{
				dgvGList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvGList.Columns[i].Name];
			}
			dgvGList.AutoResizeColumns();
		}
		catch
		{
		}
	}

	private void InitGroupList(int gid)
	{
		try
		{
			dgvGList.Rows.Clear();
			string sql = "Select R_ID, r_name As R_Name, build_ID, build_name As Build_Name, floor_id, floor_name As Floor_Name,TP_Name FROM v_GrpRoom Where RG_flag=0 And RGT_ID=" + gid;
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				object[] array = new object[7];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					for (int j = 0; j < dataTable.Columns.Count; j++)
					{
						array[j] = dataTable.Rows[i][j];
					}
					dgvGList.Rows.Add(array);
				}
			}
			TSSLabRoom.Text = dgvGList.Rows.Count.ToString();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err06"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtGC_KeyPress(object sender, KeyPressEventArgs e)
	{
		CheckInfo.NumberKeyPress(sender, e, 0, 255L);
	}

	private bool chkGrp()
	{
		string text = cobGN.Text.Trim();
		if (text == "")
		{
			Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		string text2 = txtGC.Text.Trim();
		if (text2 == "")
		{
			Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		for (int i = 0; i < cobGN.Items.Count; i++)
		{
			if (text == ((DataRowView)cobGN.Items[i]).Row.ItemArray[1].ToString().Trim())
			{
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			if (text2 == ((DataRowView)cobGN.Items[i]).Row.ItemArray[2].ToString().Trim())
			{
				Program.MsgBox((string)m_htab["Info09"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
		}
		return true;
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		try
		{
			if (chkGrp())
			{
				string sqlstr = "Insert Into D_RoomGroupType Values(N'" + cobGN.Text.Trim() + "'," + txtGC.Text.Trim() + ", 0, GetDate(), " + Program.m_opid + ", NULL, NULL)";
				int num = SQLserver.Data_ExecuteSql(sqlstr);
				if (num <= 0)
				{
					Program.MsgBox((string)m_htab["Err03"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				InitGroup();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void frmGrpRoom_Load(object sender, EventArgs e)
	{
		InitRoomListColumn();
		InitGroupListColumn();
		InitBuild();
		InitGroup();
		cobGN.Select();
	}

	private void btnAddAll_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
		{
			dgvRList.SelectAll();
			AddRow();
			TSSLabRoom.Text = dgvGList.Rows.Count.ToString();
		}
	}

	private void AddRow()
	{
		try
		{
			object[] array = new object[7];
			if (cobGN.SelectedItem == null)
			{
				Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			long gid = Convert.ToInt64(cobGN.SelectedValue);
			string gname = ((DataRowView)cobGN.SelectedItem).Row.ItemArray[1].ToString();
			int num = -1;
			for (int num2 = dgvRList.SelectedRows.Count - 1; num2 >= 0; num2--)
			{
				num = dgvRList.SelectedRows[num2].Index;
				for (int i = 0; i < dgvRList.Columns.Count; i++)
				{
					array[i] = dgvRList.Rows[num].Cells[i].Value;
				}
				if (!saveGrpChange(gid, gname, (long)array[0], (string)array[1], (long)array[2], (string)array[3], (long)array[4], (string)array[5], (string)array[6], 0))
				{
					break;
				}
				dgvRList.Rows.RemoveAt(num);
				dgvGList.Rows.Insert(0, array);
				dgvGList.Rows[0].DefaultCellStyle.BackColor = Color.Beige;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void MoveRow()
	{
		try
		{
			object[] array = new object[7];
			long gid = Convert.ToInt64(cobGN.SelectedValue);
			string gname = ((DataRowView)cobGN.SelectedItem).Row.ItemArray[1].ToString();
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			if (cobBD.DataSource != null)
			{
				num2 = Convert.ToInt32(cobBD.SelectedValue);
			}
			if (cobFD.DataSource != null)
			{
				num3 = Convert.ToInt32(cobFD.SelectedValue);
			}
			for (int num4 = dgvGList.SelectedRows.Count - 1; num4 >= 0; num4--)
			{
				num = dgvGList.SelectedRows[num4].Index;
				for (int i = 0; i < dgvGList.Columns.Count; i++)
				{
					array[i] = dgvGList.Rows[num].Cells[i].Value;
				}
				if (!saveGrpChange(gid, gname, (long)array[0], (string)array[1], (long)array[2], (string)array[3], (long)array[4], (string)array[5], (string)array[6], 1))
				{
					break;
				}
				if ((num2 == Convert.ToInt32(dgvGList.Rows[num].Cells["build_ID"].Value) || num2 == 0) && (num3 == Convert.ToInt32(dgvGList.Rows[num].Cells["floor_id"].Value) || num3 == 0))
				{
					dgvRList.Rows.Insert(0, array);
				}
				dgvGList.Rows.RemoveAt(num);
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err07"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			btnSear_Click(null, null);
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
		try
		{
			int num = Convert.ToInt32(cobGN.SelectedValue);
			if (num <= 0)
			{
				Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int num2 = Convert.ToInt32(cobBD.SelectedValue);
			if (num2 == 0)
			{
				InitRoomList(num2, 0, num);
				return;
			}
			int num3 = 0;
			num3 = ((cobFD.DataSource != null) ? Convert.ToInt32(cobFD.SelectedValue) : 0);
			InitRoomList(num2, num3, num);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cobGN_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cobGN.DataSource != null)
			{
				InitGroupList(Convert.ToInt32(cobGN.SelectedValue));
				TSSLabGrp.Text = ((DataRowView)cobGN.SelectedItem).Row.ItemArray[1].ToString();
				txtGC.Text = ((DataRowView)cobGN.SelectedItem).Row.ItemArray[2].ToString();
				btnSear_Click(null, null);
			}
		}
		catch
		{
		}
	}

	private void btnAddCur_Click(object sender, EventArgs e)
	{
		AddRow();
		TSSLabRoom.Text = dgvGList.Rows.Count.ToString();
	}

	private void btnMovCur_Click(object sender, EventArgs e)
	{
		MoveRow();
		TSSLabRoom.Text = dgvGList.Rows.Count.ToString();
	}

	private void btnMovAll_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
		{
			dgvGList.SelectAll();
			MoveRow();
			TSSLabRoom.Text = dgvGList.Rows.Count.ToString();
		}
	}

	private bool saveGrpChange(long gid, string gname, long rid, string rname, long bid, string bname, long fid, string fname, string tpname, int del)
	{
		try
		{
			int num = -1;
			string text = "";
			text = "Select RG_ID From T_RoomGroup Where RGT_ID=" + gid + " And r_id=" + rid;
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			if (dataTable.Rows.Count > 0)
			{
				text = "Update T_RoomGroup Set RG_Flag=" + del + ", updatorid=" + Program.m_opid + ", updatetime=GetDate() where RG_ID=" + dataTable.Rows[0]["RG_ID"].ToString();
				dataTable.Rows.Clear();
				num = SQLserver.Data_ExecuteSql(text);
				if (num != 1)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return false;
				}
			}
			else if (del == 0)
			{
				text = "Insert Into T_RoomGroup Values(" + gid + ", N'" + gname + "', " + rid + ", N'" + rname + "', " + bid;
				string text2 = text;
				text = text2 + ",N'" + bname + "', " + fid + ", N'" + fname.ToString() + "', N'" + tpname + "', 0, GetDate(), " + Program.m_opid + ", NULL, NULL)";
				num = SQLserver.Data_ExecuteSql(text);
				if (num != 1)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return false;
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err08"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		return true;
	}

	private void btnEdit_Click(object sender, EventArgs e)
	{
		try
		{
			if (cobGN.DataSource == null || cobGN.SelectedItem == null)
			{
				return;
			}
			string text = ((DataRowView)cobGN.SelectedItem).Row.ItemArray[1].ToString();
			long num = Convert.ToInt64(cobGN.SelectedValue);
			frmTmpDlg frmTmpDlg2 = new frmTmpDlg();
			int num2 = 2;
			int num3 = 31;
			int num4 = 2;
			int num5 = 20;
			int[] array = new int[num4];
			array[0] = 120;
			array[1] = 150;
			TextBox[] array2 = new TextBox[num2];
			Label[] array3 = new Label[num2];
			frmTmpDlg2.tlpCtls.ColumnCount = num4;
			frmTmpDlg2.tlpCtls.RowCount = num2;
			for (int i = 0; i < num4; i++)
			{
				frmTmpDlg2.tlpCtls.ColumnStyles[i].SizeType = SizeType.Absolute;
				frmTmpDlg2.tlpCtls.ColumnStyles[i].Width = array[i];
				num5 += array[i];
			}
			frmTmpDlg2.Width = num5;
			for (int j = 0; j < num2; j++)
			{
				array3[j] = new Label();
				array3[j].Name = "lab" + (j + 1).ToString("D3");
				array3[j].AutoSize = false;
				array3[j].TextAlign = ContentAlignment.MiddleRight;
				array3[j].Dock = DockStyle.Fill;
				array2[j] = new TextBox();
				array2[j].Name = "txt" + (j + 1).ToString("D3");
				array2[j].Dock = DockStyle.Bottom;
				frmTmpDlg2.Height += num3;
				frmTmpDlg2.tlpCtls.Controls.Add(array3[j]);
				frmTmpDlg2.tlpCtls.Controls.Add(array2[j]);
				frmTmpDlg2.tlpCtls.RowStyles[j].SizeType = SizeType.Absolute;
				frmTmpDlg2.tlpCtls.RowStyles[j].Height = num3;
			}
			frmTmpDlg2.m_htab = m_htab;
			array2[0].ReadOnly = true;
			array2[0].BackColor = Color.FromArgb(205, 229, 245);
			array2[0].Text = text;
			array2[1].Select();
			if (frmTmpDlg2.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}
			string text2 = array2[1].Text.Trim();
			frmTmpDlg2.Dispose();
			if (text2.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			for (int k = 0; k < cobGN.Items.Count; k++)
			{
				if (text2 == ((DataRowView)cobGN.Items[0]).Row.ItemArray[1].ToString().Trim())
				{
					Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
			}
			text = string.Format((string)m_htab["Info06"], text + "\r\n", text2 + "\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				text = "Update D_RoomGroupType Set RGT_Name = N'" + text2 + "', updatetime=GetDate(), updatorid=" + Program.m_opid + " Where RGT_id = " + num;
				int num6 = SQLserver.Data_ExecuteSql(text);
				if (num6 != 1)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num6, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					InitGroup();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err09"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			if (cobGN.DataSource == null || cobGN.SelectedItem == null)
			{
				return;
			}
			string text = ((DataRowView)cobGN.SelectedItem).Row.ItemArray[1].ToString();
			long num = Convert.ToInt64(cobGN.SelectedValue);
			if (Program.MsgBox(label12.Text + " " + text + "\r\n\r\n" + (string)m_htab["Info08"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				text = "Update D_RoomGroupType Set RGT_flag = 1, updatetime=GetDate(), updatorid=" + Program.m_opid + " Where RGT_id = " + num;
				int num2 = SQLserver.Data_ExecuteSql(text);
				if (num2 != 1)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num2, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					((DataRowView)cobGN.SelectedItem).Row.Delete();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnSG_Click(object sender, EventArgs e)
	{
	}

	private void btnAddAll_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnAddAll.Location.X + panel3.Location.X, btnAddAll.Location.Y + panel3.Location.Y - 15);
		label1.Text = (string)m_htab["labeladdall"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnAddAll_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnAddCur_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnAddCur.Location.X + panel3.Location.X, btnAddCur.Location.Y + panel3.Location.Y - 15);
		label1.Text = (string)m_htab["labeladdcur"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnAddCur_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnMovCur_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnMovCur.Location.X + panel3.Location.X, btnMovCur.Location.Y + panel3.Location.Y - 15);
		label1.Text = (string)m_htab["labelmovecur"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnMovCur_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnMovAll_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnMovAll.Location.X + panel3.Location.X, btnMovAll.Location.Y + panel3.Location.Y - 15);
		label1.Text = (string)m_htab["labelmoveall"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnMovAll_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnNew_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnNew.Location.X + flowLayoutPanel2.Location.X + clsBackPanel6.Location.X + panel4.Location.X, btnNew.Location.Y + flowLayoutPanel2.Location.Y - 8 + clsBackPanel6.Location.Y + panel4.Location.Y);
		label1.Text = (string)m_htab["labelnewgrp"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnNew_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnEdit_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnEdit.Location.X + flowLayoutPanel2.Location.X + clsBackPanel6.Location.X + panel4.Location.X, btnEdit.Location.Y + flowLayoutPanel2.Location.Y - 8 + clsBackPanel6.Location.Y + panel4.Location.Y);
		label1.Text = (string)m_htab["labeleditgrp"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnEdit_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnDel_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(btnDel.Location.X + flowLayoutPanel2.Location.X + clsBackPanel6.Location.X + panel4.Location.X, btnDel.Location.Y + flowLayoutPanel2.Location.Y - 8 + clsBackPanel6.Location.Y + panel4.Location.Y);
		label1.Text = (string)m_htab["labeldeletegrp"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnDel_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}

	private void btnSear_MouseMove(object sender, MouseEventArgs e)
	{
		label1.Location = new Point(300, 5);
		label1.Text = (string)m_htab["labelsearch"];
		label1.AutoSize = true;
		label1.BringToFront();
		label1.Visible = true;
	}

	private void btnSear_MouseLeave(object sender, EventArgs e)
	{
		label1.Visible = false;
	}
}
