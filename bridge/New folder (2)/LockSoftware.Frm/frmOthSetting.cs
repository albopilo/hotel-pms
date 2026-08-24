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

public class frmOthSetting : Form
{
	private IContainer components;

	private SplitContainer splitContainer1;

	private DataGridView dgvItem;

	private FlowLayoutPanel flowLayoutPanel1;

	private Label label1;

	private TextBox txtTPN;

	private NGlassBtn btnNew;

	private NGlassBtn btnEdit;

	private NGlassBtn btnDel;

	private clsBackPanel clsBackPanel1;

	private DataGridView dgvType;

	private clsBackPanel clsBackPanel2;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private TextBox txtIN;

	private TextBox txtIU;

	private TextBox txtUP;

	private TextBox txtIM;

	private Panel panel1;

	private LockSoftware.Controls.GlassBtn btnIModi0;

	private LockSoftware.Controls.GlassBtn btnINew;

	private LockSoftware.Controls.GlassBtn btnClose;

	private Label label6;

	private TextBox txtIID;

	private NGlassBtn btnSear;

	private LockSoftware.Controls.GlassBtn btnIEdit0;

	private NGlassBtn btnCID;

	private CheckBox chkDis;

	private ToolTip toolTip1;

	private ToolTip toolTip2;

	private ToolTip toolTip3;

	private ToolTip toolTip4;

	private ToolTip toolTip5;

	public string m_objName = "WFOthS";

	public Hashtable m_htab;

	public int m_Edit;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmOthSetting));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.dgvItem = new System.Windows.Forms.DataGridView();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnIEdit0 = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnIModi0 = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnINew = new LockSoftware.Controls.GlassBtn(this.components);
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.txtIM = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtUP = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtIU = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtIN = new System.Windows.Forms.TextBox();
		this.txtIID = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.btnCID = new LockSoftware.Controls.NGlassBtn(this.components);
		this.chkDis = new System.Windows.Forms.CheckBox();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.dgvType = new System.Windows.Forms.DataGridView();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.txtTPN = new System.Windows.Forms.TextBox();
		this.btnNew = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnEdit = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDel = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnSear = new LockSoftware.Controls.NGlassBtn(this.components);
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvItem).BeginInit();
		this.clsBackPanel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvType).BeginInit();
		this.flowLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splitContainer1.Location = new System.Drawing.Point(3, 3);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.dgvItem);
		this.splitContainer1.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
		this.splitContainer1.Panel2.Controls.Add(this.clsBackPanel2);
		this.splitContainer1.Panel2.Controls.Add(this.clsBackPanel1);
		this.splitContainer1.Size = new System.Drawing.Size(827, 528);
		this.splitContainer1.SplitterDistance = 462;
		this.splitContainer1.SplitterWidth = 5;
		this.splitContainer1.TabIndex = 1;
		this.dgvItem.AllowUserToAddRows = false;
		this.dgvItem.AllowUserToDeleteRows = false;
		this.dgvItem.BackgroundColor = System.Drawing.Color.White;
		this.dgvItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvItem.Location = new System.Drawing.Point(0, 0);
		this.dgvItem.MultiSelect = false;
		this.dgvItem.Name = "dgvItem";
		this.dgvItem.ReadOnly = true;
		this.dgvItem.RowHeadersVisible = false;
		this.dgvItem.RowTemplate.Height = 23;
		this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvItem.Size = new System.Drawing.Size(462, 528);
		this.dgvItem.TabIndex = 0;
		this.dgvItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvItem_CellDoubleClick);
		this.clsBackPanel2.AutoScroll = true;
		this.clsBackPanel2.Border = true;
		this.clsBackPanel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderBW = 1;
		this.clsBackPanel2.BorderColorBottom = System.Drawing.Color.YellowGreen;
		this.clsBackPanel2.BorderColorLeft = System.Drawing.Color.YellowGreen;
		this.clsBackPanel2.BorderColorRight = System.Drawing.Color.YellowGreen;
		this.clsBackPanel2.BorderColorTop = System.Drawing.Color.YellowGreen;
		this.clsBackPanel2.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderLW = 1;
		this.clsBackPanel2.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderRW = 1;
		this.clsBackPanel2.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderTW = 1;
		this.clsBackPanel2.Color1 = System.Drawing.Color.White;
		this.clsBackPanel2.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.panel1);
		this.clsBackPanel2.Controls.Add(this.tableLayoutPanel1);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 273);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Padding = new System.Windows.Forms.Padding(3);
		this.clsBackPanel2.Size = new System.Drawing.Size(360, 255);
		this.clsBackPanel2.TabIndex = 25;
		this.panel1.BackColor = System.Drawing.Color.Transparent;
		this.panel1.Controls.Add(this.btnIEdit0);
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Controls.Add(this.btnIModi0);
		this.panel1.Controls.Add(this.btnINew);
		this.panel1.Location = new System.Drawing.Point(3, 212);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(323, 43);
		this.panel1.TabIndex = 1;
		this.btnIEdit0.BackColor = System.Drawing.Color.Gainsboro;
		this.btnIEdit0.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnIEdit0.ForeColor = System.Drawing.Color.Black;
		this.btnIEdit0.GlowColor = System.Drawing.Color.White;
		this.btnIEdit0.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIEdit0.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnIEdit0.Location = new System.Drawing.Point(87, 6);
		this.btnIEdit0.Name = "btnIEdit0";
		this.btnIEdit0.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnIEdit0.Size = new System.Drawing.Size(71, 30);
		this.btnIEdit0.TabIndex = 6;
		this.btnIEdit0.Text = "Edit";
		this.btnIEdit0.Click += new System.EventHandler(btnIEdit0_Click);
		this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(241, 6);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(71, 30);
		this.btnClose.TabIndex = 4;
		this.btnClose.Text = "Close";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnIModi0.BackColor = System.Drawing.Color.Gainsboro;
		this.btnIModi0.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnIModi0.ForeColor = System.Drawing.Color.Black;
		this.btnIModi0.GlowColor = System.Drawing.Color.White;
		this.btnIModi0.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIModi0.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnIModi0.Location = new System.Drawing.Point(164, 6);
		this.btnIModi0.Name = "btnIModi0";
		this.btnIModi0.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnIModi0.Size = new System.Drawing.Size(71, 30);
		this.btnIModi0.TabIndex = 2;
		this.btnIModi0.Text = "Delete";
		this.btnIModi0.Click += new System.EventHandler(btnIModi0_Click);
		this.btnINew.BackColor = System.Drawing.Color.Gainsboro;
		this.btnINew.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnINew.ForeColor = System.Drawing.Color.Black;
		this.btnINew.GlowColor = System.Drawing.Color.White;
		this.btnINew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnINew.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnINew.Location = new System.Drawing.Point(10, 6);
		this.btnINew.Name = "btnINew";
		this.btnINew.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnINew.Size = new System.Drawing.Size(71, 30);
		this.btnINew.TabIndex = 1;
		this.btnINew.Text = "New";
		this.btnINew.Click += new System.EventHandler(btnINew_Click);
		this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.tableLayoutPanel1.ColumnCount = 3;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.txtIM, 1, 4);
		this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
		this.tableLayoutPanel1.Controls.Add(this.txtUP, 1, 3);
		this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
		this.tableLayoutPanel1.Controls.Add(this.txtIU, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.txtIN, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtIID, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnCID, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.chkDis, 1, 5);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
		this.tableLayoutPanel1.RowCount = 6;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(354, 209);
		this.tableLayoutPanel1.TabIndex = 0;
		this.tableLayoutPanel1.SetColumnSpan(this.txtIM, 2);
		this.txtIM.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtIM.Location = new System.Drawing.Point(102, 136);
		this.txtIM.Multiline = true;
		this.txtIM.Name = "txtIM";
		this.txtIM.Size = new System.Drawing.Size(244, 37);
		this.txtIM.TabIndex = 5;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(8, 133);
		this.label5.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.label5.Name = "label5";
		this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.label5.Size = new System.Drawing.Size(82, 22);
		this.label5.TabIndex = 3;
		this.label5.Text = "Item Memo:";
		this.tableLayoutPanel1.SetColumnSpan(this.txtUP, 2);
		this.txtUP.Dock = System.Windows.Forms.DockStyle.Top;
		this.txtUP.Location = new System.Drawing.Point(102, 104);
		this.txtUP.MaxLength = 12;
		this.txtUP.Name = "txtUP";
		this.txtUP.Size = new System.Drawing.Size(244, 24);
		this.txtUP.TabIndex = 4;
		this.txtUP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUP_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(8, 101);
		this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
		this.label4.Name = "label4";
		this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.label4.Size = new System.Drawing.Size(70, 27);
		this.label4.TabIndex = 2;
		this.label4.Text = "Unit Price:";
		this.tableLayoutPanel1.SetColumnSpan(this.txtIU, 2);
		this.txtIU.Dock = System.Windows.Forms.DockStyle.Top;
		this.txtIU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtIU.Location = new System.Drawing.Point(102, 72);
		this.txtIU.MaxLength = 120;
		this.txtIU.Name = "txtIU";
		this.txtIU.Size = new System.Drawing.Size(244, 24);
		this.txtIU.TabIndex = 3;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(8, 69);
		this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
		this.label3.Name = "label3";
		this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.label3.Size = new System.Drawing.Size(69, 27);
		this.label3.TabIndex = 1;
		this.label3.Text = "Item Unit:";
		this.tableLayoutPanel1.SetColumnSpan(this.txtIN, 2);
		this.txtIN.Dock = System.Windows.Forms.DockStyle.Top;
		this.txtIN.Location = new System.Drawing.Point(102, 40);
		this.txtIN.MaxLength = 128;
		this.txtIN.Name = "txtIN";
		this.txtIN.Size = new System.Drawing.Size(244, 24);
		this.txtIN.TabIndex = 2;
		this.txtIID.BackColor = System.Drawing.SystemColors.Window;
		this.txtIID.Dock = System.Windows.Forms.DockStyle.Top;
		this.txtIID.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtIID.Location = new System.Drawing.Point(102, 8);
		this.txtIID.MaxLength = 12;
		this.txtIID.Name = "txtIID";
		this.txtIID.Size = new System.Drawing.Size(213, 24);
		this.txtIID.TabIndex = 1;
		this.txtIID.Leave += new System.EventHandler(txtIID_Leave);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(8, 37);
		this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.label2.Size = new System.Drawing.Size(91, 27);
		this.label2.TabIndex = 0;
		this.label2.Text = "Item Name:";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(8, 5);
		this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
		this.label6.Name = "label6";
		this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.label6.Size = new System.Drawing.Size(68, 27);
		this.label6.TabIndex = 8;
		this.label6.Text = "Item ID:";
		this.btnCID.BackColor = System.Drawing.Color.Transparent;
		this.btnCID.BaseColor = System.Drawing.Color.White;
		this.btnCID.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnCID.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnCID.ButtonText = null;
		this.btnCID.CornerRadius = 2;
		this.btnCID.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCID.Image = LockSoftware.Properties.Resources.Left6_32x32x256;
		this.btnCID.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnCID.Location = new System.Drawing.Point(321, 5);
		this.btnCID.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
		this.btnCID.Name = "btnCID";
		this.btnCID.Size = new System.Drawing.Size(28, 28);
		this.btnCID.TabIndex = 12;
		this.btnCID.Click += new System.EventHandler(btnCID_Click);
		this.chkDis.AutoSize = true;
		this.chkDis.Enabled = false;
		this.chkDis.Location = new System.Drawing.Point(102, 179);
		this.chkDis.Name = "chkDis";
		this.chkDis.Size = new System.Drawing.Size(95, 21);
		this.chkDis.TabIndex = 13;
		this.chkDis.Text = "checkBox1";
		this.chkDis.UseVisualStyleBackColor = true;
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.dgvType);
		this.clsBackPanel1.Controls.Add(this.flowLayoutPanel1);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(360, 273);
		this.clsBackPanel1.TabIndex = 24;
		this.dgvType.AllowUserToAddRows = false;
		this.dgvType.AllowUserToDeleteRows = false;
		this.dgvType.BackgroundColor = System.Drawing.Color.White;
		this.dgvType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvType.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvType.Location = new System.Drawing.Point(0, 0);
		this.dgvType.MultiSelect = false;
		this.dgvType.Name = "dgvType";
		this.dgvType.ReadOnly = true;
		this.dgvType.RowHeadersVisible = false;
		this.dgvType.RowTemplate.Height = 23;
		this.dgvType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvType.Size = new System.Drawing.Size(360, 237);
		this.dgvType.TabIndex = 1;
		this.dgvType.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvType_RowEnter);
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel1.Controls.Add(this.label1);
		this.flowLayoutPanel1.Controls.Add(this.txtTPN);
		this.flowLayoutPanel1.Controls.Add(this.btnNew);
		this.flowLayoutPanel1.Controls.Add(this.btnEdit);
		this.flowLayoutPanel1.Controls.Add(this.btnDel);
		this.flowLayoutPanel1.Controls.Add(this.btnSear);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 237);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(360, 36);
		this.flowLayoutPanel1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(8, 10);
		this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(91, 17);
		this.label1.TabIndex = 0;
		this.label1.Text = "Type Name:";
		this.txtTPN.Location = new System.Drawing.Point(105, 8);
		this.txtTPN.MaxLength = 120;
		this.txtTPN.Name = "txtTPN";
		this.txtTPN.Size = new System.Drawing.Size(65, 22);
		this.txtTPN.TabIndex = 1;
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
		this.btnNew.Location = new System.Drawing.Point(176, 5);
		this.btnNew.Margin = new System.Windows.Forms.Padding(3, 0, 2, 3);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(28, 28);
		this.btnNew.TabIndex = 8;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
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
		this.btnEdit.Location = new System.Drawing.Point(209, 5);
		this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 0, 2, 3);
		this.btnEdit.Name = "btnEdit";
		this.btnEdit.Size = new System.Drawing.Size(28, 28);
		this.btnEdit.TabIndex = 9;
		this.btnEdit.Click += new System.EventHandler(btnEdit_Click);
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
		this.btnDel.Location = new System.Drawing.Point(242, 5);
		this.btnDel.Margin = new System.Windows.Forms.Padding(3, 0, 2, 3);
		this.btnDel.Name = "btnDel";
		this.btnDel.Size = new System.Drawing.Size(28, 28);
		this.btnDel.TabIndex = 10;
		this.btnDel.Click += new System.EventHandler(btnDel_Click);
		this.btnSear.BackColor = System.Drawing.Color.Transparent;
		this.btnSear.BaseColor = System.Drawing.Color.White;
		this.btnSear.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnSear.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnSear.ButtonText = null;
		this.btnSear.CornerRadius = 2;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.Image = LockSoftware.Properties.Resources.search;
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnSear.Location = new System.Drawing.Point(275, 5);
		this.btnSear.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
		this.btnSear.Name = "btnSear";
		this.btnSear.Size = new System.Drawing.Size(28, 28);
		this.btnSear.TabIndex = 11;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.toolTip1.ShowAlways = true;
		this.toolTip2.ShowAlways = true;
		this.toolTip3.ShowAlways = true;
		this.toolTip4.ShowAlways = true;
		this.toolTip5.ShowAlways = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.WhiteSmoke;
		base.ClientSize = new System.Drawing.Size(833, 534);
		base.Controls.Add(this.splitContainer1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmOthSetting";
		base.Padding = new System.Windows.Forms.Padding(3);
		this.Text = "Consumer Setting";
		base.Load += new System.EventHandler(frmOthSetting_Load);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvItem).EndInit();
		this.clsBackPanel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvType).EndInit();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmOthSetting()
	{
		InitializeComponent();
		base.WindowState = FormWindowState.Maximized;
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void frmOthSetting_Load(object sender, EventArgs e)
	{
		InitType();
		NGlassBtn nGlassBtn = btnNew;
		NGlassBtn nGlassBtn2 = btnEdit;
		bool flag = (btnDel.Enabled = SQLserver.GetUserPermisstion(1046, Program.m_OperID));
		bool enabled = (nGlassBtn2.Enabled = flag);
		nGlassBtn.Enabled = enabled;
		LockSoftware.Controls.GlassBtn glassBtn = btnIEdit0;
		LockSoftware.Controls.GlassBtn glassBtn2 = btnINew;
		bool flag3 = (btnIModi0.Enabled = SQLserver.GetUserPermisstion(1047, Program.m_OperID));
		bool enabled2 = (glassBtn2.Enabled = flag3);
		glassBtn.Enabled = enabled2;
		toolTip1.SetToolTip(btnNew, (string)m_htab["btnNewT"]);
		toolTip2.SetToolTip(btnEdit, (string)m_htab["btnEditT"]);
		toolTip3.SetToolTip(btnDel, (string)m_htab["btnDelT"]);
		toolTip4.SetToolTip(btnSear, (string)m_htab["btnSearT"]);
		toolTip5.SetToolTip(btnCID, (string)m_htab["btnCIDT"]);
		enableEditBtn(bEna: false);
	}

	private void InitType()
	{
		try
		{
			string sql = "Select Row_Number() OVER (Order by OT_Name) AS RowNumber, OT_ID, OT_Name, OT_Flag,CreateTime,UpdateTime FROM D_OtherType ";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			dgvType.DataSource = dataTable.DefaultView;
			if (dgvType.DataSource != null)
			{
				DataGridViewColumn dataGridViewColumn = dgvType.Columns["OT_ID"];
				bool visible = (dgvType.Columns["OT_Flag"].Visible = false);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvType.Columns.Count; i++)
				{
					dgvType.Columns[i].HeaderText = (string)m_htab["dgvTCol" + i.ToString("D2")];
				}
				dgvType.AutoResizeColumns();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvTCol02"]);
		}
	}

	private void InitItem()
	{
		try
		{
			string text = "Select Row_Number() OVER (Order by OT_ID) AS RowNumber, * FROM v_Other Where 1 = 1";
			if (txtTPN.Text.Trim() != "")
			{
				text = text + " And OT_Name = N'" + txtTPN.Text.Trim() + "'";
			}
			if (txtIID.Text.Trim() != "")
			{
				text = text + " And oth_ID like '" + txtIID.Text.Trim() + "%'";
			}
			if (txtIN.Text.Trim() != "")
			{
				text = text + " And oth_name like N'" + txtIN.Text.Trim() + "%'";
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			dgvItem.DataSource = dataTable.DefaultView;
			if (dgvItem.DataSource != null)
			{
				dgvItem.Columns["OT_ID"].Visible = false;
				for (int i = 0; i < dgvItem.Columns.Count; i++)
				{
					dgvItem.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvItem.Columns[i].Name];
				}
				dgvItem.AutoResizeColumns();
			}
			text = "select min(oth_id) As MAXID from t_otherid where oth_id not in(select oth_id from t_other )";
			DataTable dataTable2 = SQLserver.Data_GetDataTable(text);
			if (dataTable2 != null && dataTable2.Rows.Count > 0)
			{
				txtIID.Text = dataTable2.Rows[0]["MAXID"].ToString().Trim();
				dataTable2.Clear();
			}
			dataTable2?.Dispose();
			enableEditBtn(bEna: false);
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcoloth_name"]);
		}
	}

	private bool chkType(string tpname)
	{
		string sql = "Select * From D_OtherType Where OT_Name=N'" + tpname + "' And OT_Flag = 0";
		DataTable dataTable = SQLserver.Data_GetDataTable(sql);
		if (dataTable == null)
		{
			Program.MsgCustom((string)m_htab["Err03"], MessageBoxIcon.Hand);
			return false;
		}
		if (dataTable.Rows.Count > 0)
		{
			Program.MsgCustom((string)m_htab["Info02"], MessageBoxIcon.Hand);
			return false;
		}
		return true;
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		try
		{
			if (!Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtTPN.Text.Trim(), chk: true) && chkType(txtTPN.Text.Trim()))
			{
				string sqlstr = "Insert Into D_OtherType Values(N'" + txtTPN.Text.Trim() + "', Null, 0, 0, GetDate(), " + Program.m_opid + ", NULL, NULL)";
				int num = SQLserver.Data_ExecuteSql(sqlstr);
				if (num <= 0)
				{
					Program.MsgBox((string)m_htab["Err01"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				InitType();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvType_RowEnter(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0)
			{
				txtTPN.Text = dgvType.Rows[e.RowIndex].Cells["OT_Name"].Value.ToString();
				ClearTextBox();
				enableEditBtn(bEna: false);
				SearchType();
			}
		}
		catch
		{
		}
	}

	private void btnEdit_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvType.DataSource == null || dgvType.Rows.Count < 0)
			{
				return;
			}
			int num = Convert.ToInt32(dgvType.CurrentRow.Cells["OT_ID"].Value);
			string text = dgvType.CurrentRow.Cells["OT_Name"].Value.ToString().Trim();
			if (num < 0 || text == "")
			{
				return;
			}
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
				array2[j].MaxLength = 120;
				frmTmpDlg2.Height += num3;
				frmTmpDlg2.tlpCtls.Controls.Add(array3[j]);
				frmTmpDlg2.tlpCtls.Controls.Add(array2[j]);
				frmTmpDlg2.tlpCtls.RowStyles[j].SizeType = SizeType.Absolute;
				frmTmpDlg2.tlpCtls.RowStyles[j].Height = num3;
			}
			frmTmpDlg2.m_htab = m_htab;
			frmTmpDlg2.Text = (string)m_htab["FrmText"];
			array2[0].ReadOnly = true;
			array2[0].BackColor = Color.FromArgb(205, 229, 245);
			array2[0].Text = text;
			array2[1].Text = "";
			array2[1].Select();
			if (frmTmpDlg2.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}
			string text2 = array2[1].Text.Trim();
			frmTmpDlg2.Dispose();
			if (Program.isValNull(array3[1].Text.Trim().Substring(0, array3[1].Text.Trim().Length - 1), text2, chk: true) || !chkType(text2))
			{
				return;
			}
			text = string.Format((string)m_htab["Info01"], text + "\r\n", text2 + "\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				text = "Update D_OtherType Set OT_Name = N'" + text2 + "', UpdateTime=GetDate(), Updator_ID=" + Program.m_opid + " Where OT_ID = " + num;
				int num6 = SQLserver.Data_ExecuteSql(text);
				if (num6 != 1)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num6, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					InitType();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool chkItem()
	{
		try
		{
			if (Program.isValNull(label6.Text.Substring(0, label6.Text.Length - 1), txtIID.Text.Trim(), chk: true))
			{
				return false;
			}
			if (Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtIN.Text.Trim(), chk: true))
			{
				return false;
			}
			if (Program.isValNull(label3.Text.Substring(0, label3.Text.Length - 1), txtIU.Text.Trim(), chk: true))
			{
				return false;
			}
			if (Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtUP.Text.Trim(), chk: true))
			{
				return false;
			}
			if (dgvType.DataSource == null || dgvType.Rows.Count <= 0)
			{
				Program.MsgCustom((string)m_htab["Info03"], MessageBoxIcon.Asterisk);
				return false;
			}
			if (dgvType.CurrentRow == null)
			{
				Program.MsgCustom((string)m_htab["Info04"], MessageBoxIcon.Asterisk);
				return false;
			}
			string text = txtIID.Text.Trim();
			text = "Select oth_ID From T_Other Where oth_ID = '" + text + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				Program.MsgCustom((string)m_htab["Info07"], MessageBoxIcon.Asterisk);
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return false;
	}

	private void txtUP_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvType.DataSource == null || dgvType.Rows.Count < 0 || dgvType.CurrentRow == null || dgvType.CurrentRow.Cells == null)
			{
				return;
			}
			int num = Convert.ToInt32(dgvType.CurrentRow.Cells["OT_ID"].Value);
			string text = dgvType.CurrentRow.Cells["OT_Name"].Value.ToString().Trim();
			if (num < 0 || text == "")
			{
				return;
			}
			string msg = string.Format((string)m_htab["Info05"], label1.Text + text + "\r\n");
			if (Program.MsgBox(msg, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			string sql = "Select top 1 * From T_Other Where OT_ID=" + num;
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				msg = string.Format((string)m_htab["Info06"], text);
				Program.MsgCustom(msg, MessageBoxIcon.Exclamation);
				return;
			}
			sql = "Delete From D_OtherType Where OT_ID = " + num;
			if (Program.DBCompExec(sql, btnDel.Text) <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			txtTPN.Text = "";
			InitType();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnSear_Click(object sender, EventArgs e)
	{
		SearchType();
	}

	private void SearchType()
	{
		InitItem();
		txtIID.Text = "";
	}

	private void btnIEdit_Click(object sender, EventArgs e)
	{
		try
		{
			btnIEdit0.Enabled = !btnIEdit0.Enabled;
			btnIModi0.Enabled = !btnIEdit0.Enabled;
			FlowLayoutPanel flowLayoutPanel = flowLayoutPanel1;
			bool enabled = (btnINew.Enabled = btnIEdit0.Enabled);
			flowLayoutPanel.Enabled = enabled;
		}
		catch
		{
		}
	}

	private bool ChkItemId()
	{
		try
		{
			string text = txtIID.Text.Trim();
			text = "Select oth_ID From T_Other Where oth_ID = '" + text + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count == 0)
			{
				Program.MsgCustom((string)m_htab["Info10"], MessageBoxIcon.Asterisk);
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return false;
	}

	private void enableEditBtn(bool bEna)
	{
		if (bEna && btnIModi0.Enabled)
		{
			TextBox textBox = txtIID;
			CheckBox checkBox = chkDis;
			bool flag = (btnIEdit0.Enabled = true);
			bool readOnly = (checkBox.Enabled = flag);
			textBox.ReadOnly = readOnly;
			txtIID.BackColor = Color.FromArgb(205, 229, 245);
			btnINew.Enabled = false;
		}
		else
		{
			TextBox textBox2 = txtIID;
			CheckBox checkBox2 = chkDis;
			bool flag4 = (btnIEdit0.Enabled = false);
			bool readOnly2 = (checkBox2.Enabled = flag4);
			textBox2.ReadOnly = readOnly2;
			txtIID.BackColor = SystemColors.Window;
			btnINew.Enabled = true;
		}
	}

	private void dgvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0)
			{
				txtTPN.Text = dgvItem.Rows[e.RowIndex].Cells["OT_Name"].Value.ToString();
				txtIID.Text = dgvItem.Rows[e.RowIndex].Cells["oth_ID"].Value.ToString();
				txtIN.Text = dgvItem.Rows[e.RowIndex].Cells["oth_name"].Value.ToString();
				txtIU.Text = dgvItem.Rows[e.RowIndex].Cells["oth_unit"].Value.ToString();
				txtUP.Text = dgvItem.Rows[e.RowIndex].Cells["oth_price"].Value.ToString();
				txtIM.Text = dgvItem.Rows[e.RowIndex].Cells["oth_memo"].Value.ToString();
				chkDis.Checked = Convert.ToBoolean(dgvItem.Rows[e.RowIndex].Cells["oth_flag"].Value);
				enableEditBtn(bEna: true);
			}
		}
		catch
		{
		}
	}

	private void btnCID_Click(object sender, EventArgs e)
	{
		txtIID.Text = "";
		enableEditBtn(bEna: false);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnINew_Click(object sender, EventArgs e)
	{
		try
		{
			if (!chkItem())
			{
				return;
			}
			int num = Convert.ToInt32(dgvType.CurrentRow.Cells["OT_ID"].Value);
			if (num <= 0)
			{
				Program.MsgCustom((string)m_htab["Info04"], MessageBoxIcon.Asterisk);
				return;
			}
			string text = "Insert Into T_Other Values('" + txtIID.Text.Trim() + "', " + num + ", N'" + txtIN.Text.Trim() + "', N'" + txtIU.Text.Trim() + "'";
			string text2 = text;
			text = text2 + ", " + Program.GetStandDec(txtUP.Text.Trim()) + ", N'" + txtIM.Text.Trim() + "', 0, GetDate(), " + Program.m_opid + ", N'" + Program.m_OperName + "', NULL, NULL, '')";
			int num2 = SQLserver.Data_ExecuteSql(text);
			if (num2 <= 0)
			{
				Program.MsgBox((string)m_htab["Err04"] + num2, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			TextBox textBox = txtIID;
			string text3 = (txtIN.Text = "");
			textBox.Text = text3;
			InitItem();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnIEdit0_Click(object sender, EventArgs e)
	{
		try
		{
			if (!Program.isValNull(label6.Text.Substring(0, label6.Text.Length - 1), txtIID.Text.Trim(), chk: true) && !Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtIN.Text.Trim(), chk: true) && !Program.isValNull(label3.Text.Substring(0, label3.Text.Length - 1), txtIU.Text.Trim(), chk: true) && !Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtUP.Text.Trim(), chk: true) && dgvType.DataSource != null && dgvType.Rows.Count >= 0 && dgvType.CurrentRow != null && dgvType.CurrentRow.Cells != null)
			{
				int num = Convert.ToInt32(dgvType.CurrentRow.Cells["OT_ID"].Value);
				txtTPN.Text.Trim();
				string text = "Update T_Other Set oth_name = N'" + txtIN.Text.Trim() + "', oth_unit = N'" + txtIU.Text.Trim() + "'";
				string text2 = text;
				text = text2 + ", oth_memo = N'" + txtIM.Text.Trim() + "', OT_ID=" + num + ", oth_flag = " + (chkDis.Checked ? 1 : 0);
				string text3 = text;
				text = text3 + ", UpdateTime=GetDate(), Updator_ID=" + Program.m_opid + ", Updator=N'" + Program.m_OperName + "'";
				text = text + ", oth_price=" + Program.GetStandDec(txtUP.Text.Trim());
				text = text + " Where oth_ID = '" + txtIID.Text.Trim() + "'";
				int num2 = SQLserver.Data_ExecuteSql(text);
				if (num2 <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num2, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				TextBox textBox = txtIID;
				string text4 = (txtIN.Text = "");
				textBox.Text = text4;
				TextBox textBox2 = txtUP;
				TextBox textBox3 = txtIM;
				string text6 = (txtIU.Text = "");
				string text8 = (textBox3.Text = text6);
				textBox2.Text = text8;
				InitItem();
			}
		}
		catch
		{
		}
	}

	private void btnIModi0_Click(object sender, EventArgs e)
	{
		try
		{
			if (Program.isValNull(label6.Text.Substring(0, label6.Text.Length - 1), txtIID.Text.Trim(), chk: true) || Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtIN.Text.Trim(), chk: true))
			{
				return;
			}
			string text = "";
			text = label6.Text + txtIID.Text.Trim() + "\r\n";
			text = text + label2.Text + txtIN.Text.Trim();
			if (Program.MsgBox(string.Format((string)m_htab["Info08"], text + "\r\n\r\n"), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			string sql = "Select top 1 oth_ID From T_Otherpaid Where oth_ID='" + txtIID.Text.Trim() + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable.Rows.Count > 0)
			{
				text = string.Format((string)m_htab["Info09"], text);
				Program.MsgCustom(text, MessageBoxIcon.Exclamation);
				return;
			}
			text = txtIID.Text.Trim();
			sql = "Delete From T_Other Where oth_ID = '" + text + "'";
			int num = SQLserver.Data_ExecuteSql(sql);
			if (num < 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (num == 0)
			{
				Program.MsgBox((string)m_htab["Info10"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			TextBox textBox = txtIID;
			string text2 = (txtIN.Text = "");
			textBox.Text = text2;
			InitItem();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtIID_Leave(object sender, EventArgs e)
	{
		try
		{
			int length = txtIID.Text.Trim().Length;
			if (length > 0)
			{
				string text = txtIID.Text.Trim();
				for (int i = length; i < 12; i++)
				{
					text = text.Insert(0, "0");
				}
				txtIID.Text = text;
			}
		}
		catch
		{
		}
	}

	private void ClearTextBox()
	{
		txtIID.Text = "";
		txtIN.Text = "";
		txtIU.Text = "";
		txtUP.Text = "";
		txtIM.Text = "";
	}
}
