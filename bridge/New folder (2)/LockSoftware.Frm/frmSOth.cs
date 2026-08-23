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

public class frmSOth : Form
{
	public string m_objName = "WFsoth";

	public Hashtable m_htab;

	private IContainer components;

	private GroupBox groupBox1;

	private RadioButton rbDetails;

	private RadioButton rbTotal;

	private Label label1;

	private GlassBtn btnClose;

	private GlassBtn btnReset;

	private GlassBtn btnExport;

	private GlassBtn btnSearch;

	private ComboBox cobUser;

	private Label label6;

	private Label label4;

	private Label label3;

	private DateTimePicker dtpCE;

	private ComboBox cobIT;

	private Label label2;

	private clsBackPanel clsBackPanel1;

	private DateTimePicker dtpCS;

	private Label labCD;

	private TextBox txtIN;

	private Label label8;

	private ToolStripStatusLabel tssLab4;

	private DataGridView dgvlist;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel tssLab1;

	private CheckBox chkFS;

	private TextBox txtIID;

	private TextBox txtGN;

	private Label label5;

	private TextBox txtRN;

	private ToolStripStatusLabel tssLab2;

	private ToolStripStatusLabel tssLab3;

	public frmSOth()
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
			cobIT.DataSource = null;
			string sql = "Select OT_ID, OT_Name FROM D_OtherType Where OT_flag = 0 Order by OT_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null || dataTable.Rows.Count > 0)
			{
				cobIT.DisplayMember = "OT_Name";
				cobIT.ValueMember = "OT_ID";
				cobIT.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcolOT_Name"]);
		}
	}

	public string GetPars()
	{
		try
		{
			string text = "";
			if (cobIT.Text.Trim() != "")
			{
				text = (chkFS.Checked ? (text + " And OT_Name like N'" + cobIT.Text.Trim() + "%'") : (text + " And OT_Name = N'" + cobIT.Text.Trim() + "'"));
			}
			if (txtIID.Text.Trim() != "")
			{
				text = (chkFS.Checked ? (text + " And Oth_ID like '" + txtIID.Text.Trim() + "%'") : (text + " And Oth_ID = '" + txtIID.Text.Trim() + "'"));
			}
			if (txtIN.Text.Trim() != "")
			{
				text = (chkFS.Checked ? (text + " And oth_name like N'" + txtIN.Text.Trim() + "%'") : (text + " And oth_name = N'" + txtIN.Text.Trim() + "'"));
			}
			if (txtRN.Text.Trim() != "")
			{
				text = (chkFS.Checked ? (text + " And r_name like N'" + txtRN.Text.Trim() + "%'") : (text + " And r_name = N'" + txtRN.Text.Trim() + "'"));
			}
			if (txtGN.Text.Trim() != "")
			{
				text = (chkFS.Checked ? (text + " And g_name like N'" + txtGN.Text.Trim() + "%'") : (text + " And g_name = N'" + txtGN.Text.Trim() + "'"));
			}
			if (cobUser.DataSource != null && Convert.ToInt32(cobUser.SelectedValue) > 0)
			{
				text = text + " And Creator_id=" + Convert.ToInt32(cobUser.SelectedValue);
			}
			if (dtpCS.Checked)
			{
				text = text + " And CreateTime >= '" + Program.GetStandDTime(dtpCS.Value, "00") + "'";
			}
			if (dtpCE.Checked)
			{
				text = text + " And CreateTime <= '" + Program.GetStandDTime(dtpCE.Value, "59") + "'";
			}
			return text;
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return "";
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		try
		{
			ToolStripStatusLabel toolStripStatusLabel = tssLab3;
			string text = (tssLab1.Text = "");
			toolStripStatusLabel.Text = text;
			string text3 = "";
			if (rbTotal.Checked)
			{
				text3 = "Select Row_Number() OVER (Order by Oth_ID desc), Oth_ID, OT_Name, oth_name, oth_unit, oth_price";
				text3 += ", Sum(othp_qty) As othp_qty, Sum(othp_total) As othp_total, othp_discount, Sum(othp_mpay) As othp_mpay, othp_giving, Sum(othp_apaid) As othp_apaid";
			}
			else
			{
				text3 = "Select Row_Number() OVER (Order by CreateTime desc), Oth_ID, OT_Name, oth_name, oth_unit, oth_price, othp_qty, othp_total";
				text3 += ", othp_discount, othp_mpay, othp_giving, othp_apaid, CreateTime, Creator";
				text3 += ", g_name, cer_name, g_cernum, r_name, g_cometime, g_stayHour, g_level, cast(isnull(a_id,0)/2.0 as numeric(18,1)) as g_actual_S_Hour , g_actual_L_time, g_level_Card ";
			}
			text3 += " From v_OtherDetails Where 1 = 1";
			text3 += GetPars();
			if (rbTotal.Checked)
			{
				text3 += " Group by Oth_ID, OT_Name, oth_name, oth_unit, oth_price, othp_discount, othp_giving";
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text3);
			dgvlist.DataSource = dataTable.DefaultView;
			if (dataTable != null)
			{
				for (int i = 0; i < dgvlist.Columns.Count; i++)
				{
					dgvlist.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvlist.Columns[i].Name];
				}
				dgvlist.AutoResizeColumns();
				tssLab1.Text = string.Format((string)m_htab["tssLab1"], dgvlist.Rows.Count);
				double num = 0.0;
				for (int j = 0; j < dgvlist.Rows.Count; j++)
				{
					num += Convert.ToDouble(dataTable.Rows[j]["othp_apaid"]);
				}
				tssLab3.Text = num.ToString("F2") + " " + Program.m_baseCurrCode;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
	}

	private void frmSOth_Load(object sender, EventArgs e)
	{
		InitType();
		InitOper();
		dtpCE.CustomFormat = Program.m_currDateTimeFmt;
		dtpCS.CustomFormat = Program.m_currDateTimeFmt;
		btnExport.Enabled = SQLserver.GetUserPermisstion(1050, Program.m_OperID);
		btnReset_Click(null, null);
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

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
		TextBox textBox = txtRN;
		TextBox textBox2 = txtIN;
		TextBox textBox3 = txtGN;
		ComboBox comboBox = cobIT;
		string text = (txtIID.Text = "");
		string text3 = (comboBox.Text = text);
		string text5 = (textBox3.Text = text3);
		string text7 = (textBox2.Text = text5);
		textBox.Text = text7;
		chkFS.Checked = true;
		DateTime now = DateTime.Now;
		dtpCS.Value = Convert.ToDateTime(Program.GetLocDate(now.AddDays(-Convert.ToInt32(Program.m_defDay))) + " " + Program.m_defLeaveTime + ":00");
		dtpCE.Value = Convert.ToDateTime(Program.GetLocDate(now.AddDays(-Convert.ToInt32(Program.m_defDay))) + " " + Program.m_defLeaveTime + ":59");
		dtpCE.Checked = false;
		cobUser.SelectedIndex = -1;
	}

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmSOth));
		this.tssLab4 = new System.Windows.Forms.ToolStripStatusLabel();
		this.dgvlist = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.tssLab1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab3 = new System.Windows.Forms.ToolStripStatusLabel();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.txtRN = new System.Windows.Forms.TextBox();
		this.txtGN = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtIID = new System.Windows.Forms.TextBox();
		this.chkFS = new System.Windows.Forms.CheckBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rbDetails = new System.Windows.Forms.RadioButton();
		this.rbTotal = new System.Windows.Forms.RadioButton();
		this.label1 = new System.Windows.Forms.Label();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnExport = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.cobUser = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cobIT = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpCE = new System.Windows.Forms.DateTimePicker();
		this.dtpCS = new System.Windows.Forms.DateTimePicker();
		this.labCD = new System.Windows.Forms.Label();
		this.txtIN = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dgvlist).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
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
		this.dgvlist.RowHeadersVisible = false;
		this.dgvlist.RowHeadersWidth = 25;
		this.dgvlist.RowTemplate.Height = 23;
		this.dgvlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvlist.Size = new System.Drawing.Size(922, 369);
		this.dgvlist.TabIndex = 8;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.tssLab1, this.tssLab2, this.tssLab3, this.tssLab4 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 491);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(922, 23);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 7;
		this.statusStrip1.Text = "statusStrip1";
		this.tssLab1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 1);
		this.tssLab1.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab1.Name = "tssLab1";
		this.tssLab1.Size = new System.Drawing.Size(691, 18);
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
		this.clsBackPanel1.Controls.Add(this.txtRN);
		this.clsBackPanel1.Controls.Add(this.txtGN);
		this.clsBackPanel1.Controls.Add(this.label5);
		this.clsBackPanel1.Controls.Add(this.txtIID);
		this.clsBackPanel1.Controls.Add(this.chkFS);
		this.clsBackPanel1.Controls.Add(this.groupBox1);
		this.clsBackPanel1.Controls.Add(this.label1);
		this.clsBackPanel1.Controls.Add(this.btnClose);
		this.clsBackPanel1.Controls.Add(this.btnReset);
		this.clsBackPanel1.Controls.Add(this.btnExport);
		this.clsBackPanel1.Controls.Add(this.btnSearch);
		this.clsBackPanel1.Controls.Add(this.cobUser);
		this.clsBackPanel1.Controls.Add(this.label6);
		this.clsBackPanel1.Controls.Add(this.label4);
		this.clsBackPanel1.Controls.Add(this.cobIT);
		this.clsBackPanel1.Controls.Add(this.label3);
		this.clsBackPanel1.Controls.Add(this.label2);
		this.clsBackPanel1.Controls.Add(this.dtpCE);
		this.clsBackPanel1.Controls.Add(this.dtpCS);
		this.clsBackPanel1.Controls.Add(this.labCD);
		this.clsBackPanel1.Controls.Add(this.txtIN);
		this.clsBackPanel1.Controls.Add(this.label8);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(922, 122);
		this.clsBackPanel1.TabIndex = 6;
		this.txtRN.Location = new System.Drawing.Point(349, 44);
		this.txtRN.Name = "txtRN";
		this.txtRN.Size = new System.Drawing.Size(140, 22);
		this.txtRN.TabIndex = 61;
		this.txtGN.Location = new System.Drawing.Point(349, 77);
		this.txtGN.Name = "txtGN";
		this.txtGN.Size = new System.Drawing.Size(140, 22);
		this.txtGN.TabIndex = 60;
		this.label5.Location = new System.Drawing.Point(225, 76);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(118, 23);
		this.label5.TabIndex = 59;
		this.label5.Text = "Guest Name:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtIID.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtIID.Location = new System.Drawing.Point(107, 44);
		this.txtIID.Name = "txtIID";
		this.txtIID.Size = new System.Drawing.Size(112, 22);
		this.txtIID.TabIndex = 58;
		this.chkFS.AutoSize = true;
		this.chkFS.Checked = true;
		this.chkFS.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkFS.Location = new System.Drawing.Point(522, 79);
		this.chkFS.Name = "chkFS";
		this.chkFS.Size = new System.Drawing.Size(96, 18);
		this.chkFS.TabIndex = 57;
		this.chkFS.Text = "Fuzzy Search";
		this.chkFS.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.rbDetails);
		this.groupBox1.Controls.Add(this.rbTotal);
		this.groupBox1.Location = new System.Drawing.Point(668, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(110, 55);
		this.groupBox1.TabIndex = 56;
		this.groupBox1.TabStop = false;
		this.rbDetails.AutoSize = true;
		this.rbDetails.Location = new System.Drawing.Point(7, 15);
		this.rbDetails.Name = "rbDetails";
		this.rbDetails.Size = new System.Drawing.Size(60, 18);
		this.rbDetails.TabIndex = 1;
		this.rbDetails.Text = "Details";
		this.rbDetails.UseVisualStyleBackColor = true;
		this.rbTotal.AutoSize = true;
		this.rbTotal.Checked = true;
		this.rbTotal.Location = new System.Drawing.Point(7, 35);
		this.rbTotal.Name = "rbTotal";
		this.rbTotal.Size = new System.Drawing.Size(73, 18);
		this.rbTotal.TabIndex = 0;
		this.rbTotal.TabStop = true;
		this.rbTotal.Text = "Statistics";
		this.rbTotal.UseVisualStyleBackColor = true;
		this.label1.Location = new System.Drawing.Point(225, 43);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(118, 23);
		this.label1.TabIndex = 51;
		this.label1.Text = "Room Name:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(841, 65);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 50;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnReset.BackColor = System.Drawing.Color.LightGray;
		this.btnReset.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.btnReset.ForeColor = System.Drawing.Color.Black;
		this.btnReset.GlowColor = System.Drawing.Color.White;
		this.btnReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReset.Image = LockSoftware.Properties.Resources.clear;
		this.btnReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnReset.Location = new System.Drawing.Point(764, 65);
		this.btnReset.Name = "btnReset";
		this.btnReset.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnReset.Size = new System.Drawing.Size(71, 34);
		this.btnReset.TabIndex = 49;
		this.btnReset.Text = "Reset";
		this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnExport.BackColor = System.Drawing.Color.LightGray;
		this.btnExport.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.btnExport.ForeColor = System.Drawing.Color.Black;
		this.btnExport.GlowColor = System.Drawing.Color.White;
		this.btnExport.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnExport.Image = LockSoftware.Properties.Resources.xls;
		this.btnExport.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnExport.Location = new System.Drawing.Point(784, 19);
		this.btnExport.Name = "btnExport";
		this.btnExport.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnExport.Size = new System.Drawing.Size(128, 34);
		this.btnExport.TabIndex = 48;
		this.btnExport.Text = "Export To Excel";
		this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.btnSearch.BackColor = System.Drawing.Color.LightGray;
		this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.btnSearch.ForeColor = System.Drawing.Color.Black;
		this.btnSearch.GlowColor = System.Drawing.Color.White;
		this.btnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSearch.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSearch.Location = new System.Drawing.Point(668, 65);
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
		this.cobUser.FormattingEnabled = true;
		this.cobUser.Location = new System.Drawing.Point(585, 44);
		this.cobUser.Name = "cobUser";
		this.cobUser.Size = new System.Drawing.Size(77, 22);
		this.cobUser.TabIndex = 46;
		this.label6.Location = new System.Drawing.Point(492, 43);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(87, 23);
		this.label6.TabIndex = 45;
		this.label6.Text = "Operator:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(499, 15);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(19, 14);
		this.label4.TabIndex = 42;
		this.label4.Text = "→";
		this.cobIT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobIT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobIT.DropDownWidth = 160;
		this.cobIT.FormattingEnabled = true;
		this.cobIT.Location = new System.Drawing.Point(107, 12);
		this.cobIT.Name = "cobIT";
		this.cobIT.Size = new System.Drawing.Size(112, 22);
		this.cobIT.TabIndex = 40;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(1, 43);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 23);
		this.label3.TabIndex = 39;
		this.label3.Text = "Item ID:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(1, 11);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 23);
		this.label2.TabIndex = 38;
		this.label2.Text = "Item Type:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpCE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCE.Location = new System.Drawing.Point(522, 11);
		this.dtpCE.Name = "dtpCE";
		this.dtpCE.ShowCheckBox = true;
		this.dtpCE.Size = new System.Drawing.Size(140, 22);
		this.dtpCE.TabIndex = 37;
		this.dtpCS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCS.Location = new System.Drawing.Point(349, 11);
		this.dtpCS.Name = "dtpCS";
		this.dtpCS.ShowCheckBox = true;
		this.dtpCS.Size = new System.Drawing.Size(140, 22);
		this.dtpCS.TabIndex = 31;
		this.labCD.BackColor = System.Drawing.Color.Transparent;
		this.labCD.Location = new System.Drawing.Point(225, 10);
		this.labCD.Name = "labCD";
		this.labCD.Size = new System.Drawing.Size(118, 23);
		this.labCD.TabIndex = 32;
		this.labCD.Text = "Consumption Date:";
		this.labCD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtIN.ForeColor = System.Drawing.Color.Black;
		this.txtIN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtIN.Location = new System.Drawing.Point(107, 77);
		this.txtIN.Name = "txtIN";
		this.txtIN.Size = new System.Drawing.Size(112, 22);
		this.txtIN.TabIndex = 25;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Location = new System.Drawing.Point(1, 76);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(100, 23);
		this.label8.TabIndex = 26;
		this.label8.Text = "Item Name:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(922, 514);
		base.Controls.Add(this.dgvlist);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.statusStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmSOth";
		this.Text = "frmSOth";
		base.Load += new System.EventHandler(frmSOth_Load);
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
}
