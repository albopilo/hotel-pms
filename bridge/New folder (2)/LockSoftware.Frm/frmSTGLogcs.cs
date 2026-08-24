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

public class frmSTGLogcs : Form
{
	public string m_objName = "WFstgl";

	public Hashtable m_htab;

	private IContainer components;

	private CheckBox chkFS;

	private GroupBox groupBox1;

	private RadioButton rbDetails;

	private RadioButton rbTotal;

	private ComboBox cobUser;

	private Label label6;

	private DataGridView dgvlist;

	private ToolStripStatusLabel tssLab1;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel tssLab2;

	private ToolStripStatusLabel tssLab3;

	private ToolStripStatusLabel tssLab4;

	public TextBox txtTGG;

	public Label label10;

	public ComboBox cobTG;

	public Label label9;

	public ComboBox cobTB;

	public Label label7;

	public Label label26;

	public Label label27;

	public TextBox txtCernum;

	public ComboBox cobCer;

	public DateTimePicker dtpLevelE;

	public Label label11;

	public Label label12;

	public Label labArr;

	public DateTimePicker dtpComeS;

	public Label label29;

	public DateTimePicker dtpLevelS;

	public DateTimePicker dtpComeE;

	private FlowLayoutPanel flowLayoutPanel1;

	private Panel panel1;

	public FlowLayoutPanel flowLayoutPanel2;

	private GlassBtn btnSearch;

	private GlassBtn btnExport;

	private GlassBtn btnReset;

	private GlassBtn btnClose;

	private clsBackPanel clsBackPanel1;

	private Panel panel2;

	private Panel panel4;

	public frmSTGLogcs()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
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

	private string GetPars()
	{
		try
		{
			string text = "";
			if (cobTB.Text.Trim() != "")
			{
				text = ((!chkFS.Checked) ? (text + " And TB_name = N'" + cobTB.Text.Trim() + "'") : (text + " And TB_name like N'" + cobTB.Text.Trim() + "%'"));
			}
			if (cobTG.Text.Trim() != "")
			{
				text = ((!chkFS.Checked) ? (text + " And team_name = N'" + cobTG.Text.Trim() + "'") : (text + " And team_name like N'" + cobTG.Text.Trim() + "%'"));
			}
			if (txtTGG.Text.Trim() != "")
			{
				text = ((!chkFS.Checked) ? (text + " And team_guide = N'" + txtTGG.Text.Trim() + "'") : (text + " And team_guide like N'" + txtTGG.Text.Trim() + "%'"));
			}
			if (cobCer.DataSource != null && Convert.ToInt32(cobCer.SelectedValue) > 0)
			{
				text = text + " And cer_id=" + Convert.ToInt32(cobCer.SelectedValue);
			}
			if (txtCernum.Text.Trim() != "")
			{
				text = ((!chkFS.Checked) ? (text + " And team_cernum = N'" + txtCernum.Text.Trim() + "'") : (text + " And team_cernum like N'" + txtCernum.Text.Trim() + "%'"));
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
			Program.MsgBox((string)m_htab["Err01"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return "";
		}
	}

	private void frmSTGLogcs_Load(object sender, EventArgs e)
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
		InitTB();
		InitCerType();
		InitOper();
		ComboBox comboBox = cobTB;
		string text = (cobTG.Text = "");
		comboBox.Text = text;
	}

	private void cobTB_SelectedIndexChanged(object sender, EventArgs e)
	{
		InitTG();
	}

	private void btnReset_Click(object sender, EventArgs e)
	{
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
		ComboBox comboBox = cobTB;
		string text = (cobTG.Text = "");
		comboBox.Text = text;
		TextBox textBox = txtCernum;
		string text3 = (txtTGG.Text = "");
		textBox.Text = text3;
		ComboBox comboBox2 = cobCer;
		string text5 = (cobUser.Text = "");
		comboBox2.Text = text5;
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		try
		{
			dgvlist.DataSource = null;
			string text = "";
			if (!rbDetails.Checked)
			{
				text = "Select Row_Number() OVER (Order by team_id), Cast(team_id As Varchar) As team_id, TB_name, team_name, team_guide, Team_cername, team_cernum, team_perCount, team_cometime";
				text += ",team_stayHour, team_stand_L_time, team_deposit";
				text += ",CAST(team_act_sh/2 as numeric(18,1)) as team_act_sh, team_leveltime, team_discount, team_RoomPrice, Sum(g_othprice) As TR_othprice, (IsNull(Sum(g_othprice),0) + team_RoomPrice) As team_totalprice, team_totalpaid";
				text += ", team_getchange, Team_CP, Team_CT, updator, updatetime";
				text = text + " From v_TeamDetails Where 1 = 1 " + GetPars();
				text += " Group by  team_id, TB_name, team_name, team_guide, Team_cername, team_cernum, team_perCount, team_cometime, team_stand_L_time, team_deposit";
				text += ", team_leveltime, team_RoomPrice, team_discount, team_totalprice, team_totalpaid, team_getchange, Team_CP, Team_CT, updator, updatetime, team_stayHour, team_act_sh ";
			}
			else
			{
				text += "Select Row_Number() OVER (Order by team_id), Cast(team_id As Varchar) As team_id , TB_name, team_name, team_guide, Team_cername, team_cernum, team_perCount";
				text += " , R_Name, Build_Name, Floor_Name, TP_Name, Count(g_id) As gCount, Sum(TR_cardcount) As TR_cardcount, TR_cometime,TR_stayhour";
				text += ", TR_Level, TR_actual_L_time, cast(isnull(a_id,0)/2.0 as numeric(18,1)) as TR_actual_S_Hour, TP_Price, TR_discount,tr_mustpay as TR_RoomPrice, TR_othprice";
				text = text + " From v_TeamDetails Where 1 = 1 " + GetPars();
				text += " Group by  a_id,team_id, TB_name, team_name, team_guide, Team_cername, team_cernum, team_perCount";
				text += ", r_name, build_name, floor_name, TP_Name";
				text += ", TR_cometime, TR_stayhour, TR_Level, TR_actual_L_time, TR_actual_S_Hour, TP_Price, TR_discount, tr_mustpay, TR_othprice";
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null)
			{
				double num = 0.0;
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					dataTable.Rows[i]["team_id"] = "T" + Convert.ToInt32(dataTable.Rows[i]["team_id"]).ToString("D8");
					num = (rbDetails.Checked ? (num + (Convert.ToDouble(dataTable.Rows[i]["TR_roomprice"]) + Convert.ToDouble(dataTable.Rows[i]["TR_othprice"]))) : (num + Convert.ToDouble(dataTable.Rows[i]["team_totalprice"])));
				}
				dgvlist.DataSource = dataTable.DefaultView;
				for (int j = 0; j < dgvlist.Columns.Count; j++)
				{
					dgvlist.Columns[j].HeaderText = (string)m_htab["dgvcol" + dgvlist.Columns[j].Name];
				}
				dgvlist.AutoResizeColumns();
				tssLab1.Text = string.Format((string)m_htab["tssLab1"], dgvlist.Rows.Count);
				tssLab3.Text = num.ToString("F2") + " " + Program.m_baseCurrCode;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmSTGLogcs));
		this.dgvlist = new System.Windows.Forms.DataGridView();
		this.tssLab1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.tssLab2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab3 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab4 = new System.Windows.Forms.ToolStripStatusLabel();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.dtpComeS = new System.Windows.Forms.DateTimePicker();
		this.dtpComeE = new System.Windows.Forms.DateTimePicker();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpLevelE = new System.Windows.Forms.DateTimePicker();
		this.cobUser = new System.Windows.Forms.ComboBox();
		this.labArr = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.dtpLevelS = new System.Windows.Forms.DateTimePicker();
		this.label29 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.rbDetails = new System.Windows.Forms.RadioButton();
		this.rbTotal = new System.Windows.Forms.RadioButton();
		this.chkFS = new System.Windows.Forms.CheckBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.cobTB = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtTGG = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cobTG = new System.Windows.Forms.ComboBox();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.label26 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.txtCernum = new System.Windows.Forms.TextBox();
		this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnExport = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		((System.ComponentModel.ISupportInitialize)this.dgvlist).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel4.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.flowLayoutPanel2.SuspendLayout();
		base.SuspendLayout();
		this.dgvlist.AllowUserToAddRows = false;
		this.dgvlist.AllowUserToDeleteRows = false;
		this.dgvlist.BackgroundColor = System.Drawing.Color.White;
		this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvlist.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvlist.Location = new System.Drawing.Point(0, 203);
		this.dgvlist.Name = "dgvlist";
		this.dgvlist.ReadOnly = true;
		this.dgvlist.RowHeadersVisible = false;
		this.dgvlist.RowHeadersWidth = 25;
		this.dgvlist.RowTemplate.Height = 23;
		this.dgvlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvlist.Size = new System.Drawing.Size(1008, 265);
		this.dgvlist.TabIndex = 3;
		this.tssLab1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.tssLab1.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab1.Name = "tssLab1";
		this.tssLab1.Size = new System.Drawing.Size(776, 21);
		this.tssLab1.Spring = true;
		this.tssLab1.Text = "Total:";
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.tssLab1, this.tssLab2, this.tssLab3, this.tssLab4 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 468);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(1008, 26);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 10;
		this.statusStrip1.Text = "statusStrip1";
		this.tssLab2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
		this.tssLab2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.tssLab2.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab2.Name = "tssLab2";
		this.tssLab2.Size = new System.Drawing.Size(57, 21);
		this.tssLab2.Text = "合计〓";
		this.tssLab3.AutoSize = false;
		this.tssLab3.BackColor = System.Drawing.Color.Gold;
		this.tssLab3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tssLab3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tssLab3.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.tssLab3.ForeColor = System.Drawing.Color.Red;
		this.tssLab3.Name = "tssLab3";
		this.tssLab3.Size = new System.Drawing.Size(160, 21);
		this.tssLab3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tssLab4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold);
		this.tssLab4.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab4.Name = "tssLab4";
		this.tssLab4.Size = new System.Drawing.Size(0, 21);
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
		this.clsBackPanel1.Controls.Add(this.panel1);
		this.clsBackPanel1.Controls.Add(this.flowLayoutPanel2);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(1008, 203);
		this.clsBackPanel1.TabIndex = 9;
		this.panel1.AutoSize = true;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.panel4);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 52);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1008, 151);
		this.panel1.TabIndex = 2;
		this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel4.Controls.Add(this.dtpComeS);
		this.panel4.Controls.Add(this.dtpComeE);
		this.panel4.Controls.Add(this.label6);
		this.panel4.Controls.Add(this.dtpLevelE);
		this.panel4.Controls.Add(this.cobUser);
		this.panel4.Controls.Add(this.labArr);
		this.panel4.Controls.Add(this.label12);
		this.panel4.Controls.Add(this.label11);
		this.panel4.Controls.Add(this.dtpLevelS);
		this.panel4.Controls.Add(this.label29);
		this.panel4.Location = new System.Drawing.Point(534, 3);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(471, 103);
		this.panel4.TabIndex = 37;
		this.dtpComeS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeS.Location = new System.Drawing.Point(126, 7);
		this.dtpComeS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpComeS.Name = "dtpComeS";
		this.dtpComeS.ShowCheckBox = true;
		this.dtpComeS.Size = new System.Drawing.Size(145, 22);
		this.dtpComeS.TabIndex = 24;
		this.dtpComeE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeE.Location = new System.Drawing.Point(302, 7);
		this.dtpComeE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpComeE.Name = "dtpComeE";
		this.dtpComeE.ShowCheckBox = true;
		this.dtpComeE.Size = new System.Drawing.Size(145, 22);
		this.dtpComeE.TabIndex = 26;
		this.label6.Location = new System.Drawing.Point(6, 61);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(114, 29);
		this.label6.TabIndex = 31;
		this.label6.Text = "Operator:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpLevelE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelE.Location = new System.Drawing.Point(302, 37);
		this.dtpLevelE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpLevelE.Name = "dtpLevelE";
		this.dtpLevelE.ShowCheckBox = true;
		this.dtpLevelE.Size = new System.Drawing.Size(145, 22);
		this.dtpLevelE.TabIndex = 30;
		this.cobUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobUser.DropDownWidth = 160;
		this.cobUser.FormattingEnabled = true;
		this.cobUser.Location = new System.Drawing.Point(126, 66);
		this.cobUser.Name = "cobUser";
		this.cobUser.Size = new System.Drawing.Size(145, 22);
		this.cobUser.TabIndex = 32;
		this.labArr.BackColor = System.Drawing.Color.Transparent;
		this.labArr.Location = new System.Drawing.Point(3, 3);
		this.labArr.Name = "labArr";
		this.labArr.Size = new System.Drawing.Size(117, 29);
		this.labArr.TabIndex = 23;
		this.labArr.Text = "Checking In:";
		this.labArr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Location = new System.Drawing.Point(277, 10);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(19, 14);
		this.label12.TabIndex = 25;
		this.label12.Text = "→";
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.Location = new System.Drawing.Point(277, 39);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(19, 14);
		this.label11.TabIndex = 29;
		this.label11.Text = "→";
		this.dtpLevelS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelS.Location = new System.Drawing.Point(126, 36);
		this.dtpLevelS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpLevelS.Name = "dtpLevelS";
		this.dtpLevelS.ShowCheckBox = true;
		this.dtpLevelS.Size = new System.Drawing.Size(145, 22);
		this.dtpLevelS.TabIndex = 28;
		this.label29.BackColor = System.Drawing.Color.Transparent;
		this.label29.Location = new System.Drawing.Point(3, 32);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(117, 29);
		this.label29.TabIndex = 27;
		this.label29.Text = "Checking Out:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.groupBox1.AutoSize = true;
		this.groupBox1.Controls.Add(this.flowLayoutPanel1);
		this.groupBox1.Location = new System.Drawing.Point(3, 106);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
		this.groupBox1.Size = new System.Drawing.Size(525, 42);
		this.groupBox1.TabIndex = 34;
		this.groupBox1.TabStop = false;
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.Controls.Add(this.rbDetails);
		this.flowLayoutPanel1.Controls.Add(this.rbTotal);
		this.flowLayoutPanel1.Controls.Add(this.chkFS);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 15);
		this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(521, 25);
		this.flowLayoutPanel1.TabIndex = 0;
		this.rbDetails.AutoSize = true;
		this.rbDetails.Location = new System.Drawing.Point(3, 3);
		this.rbDetails.Name = "rbDetails";
		this.rbDetails.Size = new System.Drawing.Size(60, 18);
		this.rbDetails.TabIndex = 1;
		this.rbDetails.Text = "Details";
		this.rbDetails.UseVisualStyleBackColor = true;
		this.rbTotal.AutoSize = true;
		this.rbTotal.Checked = true;
		this.rbTotal.Location = new System.Drawing.Point(69, 3);
		this.rbTotal.Name = "rbTotal";
		this.rbTotal.Size = new System.Drawing.Size(73, 18);
		this.rbTotal.TabIndex = 0;
		this.rbTotal.TabStop = true;
		this.rbTotal.Text = "Statistics";
		this.rbTotal.UseVisualStyleBackColor = true;
		this.chkFS.AutoSize = true;
		this.chkFS.Checked = true;
		this.chkFS.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkFS.Location = new System.Drawing.Point(148, 3);
		this.chkFS.Name = "chkFS";
		this.chkFS.Size = new System.Drawing.Size(96, 18);
		this.chkFS.TabIndex = 33;
		this.chkFS.Text = "Fuzzy Search";
		this.chkFS.UseVisualStyleBackColor = true;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.cobTB);
		this.panel2.Controls.Add(this.label7);
		this.panel2.Controls.Add(this.label10);
		this.panel2.Controls.Add(this.txtTGG);
		this.panel2.Controls.Add(this.label9);
		this.panel2.Controls.Add(this.cobTG);
		this.panel2.Controls.Add(this.cobCer);
		this.panel2.Controls.Add(this.label26);
		this.panel2.Controls.Add(this.label27);
		this.panel2.Controls.Add(this.txtCernum);
		this.panel2.Location = new System.Drawing.Point(3, 3);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(525, 103);
		this.panel2.TabIndex = 35;
		this.cobTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTB.FormattingEnabled = true;
		this.cobTB.Location = new System.Drawing.Point(126, 7);
		this.cobTB.Name = "cobTB";
		this.cobTB.Size = new System.Drawing.Size(128, 22);
		this.cobTB.TabIndex = 14;
		this.cobTB.SelectedIndexChanged += new System.EventHandler(cobTB_SelectedIndexChanged);
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(3, 3);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(117, 29);
		this.label7.TabIndex = 13;
		this.label7.Text = "Travel Bureau:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label10.Location = new System.Drawing.Point(260, 3);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(117, 29);
		this.label10.TabIndex = 17;
		this.label10.Text = "Tour Group Guide:";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtTGG.Location = new System.Drawing.Point(383, 7);
		this.txtTGG.Name = "txtTGG";
		this.txtTGG.Size = new System.Drawing.Size(128, 22);
		this.txtTGG.TabIndex = 18;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Location = new System.Drawing.Point(3, 32);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(117, 29);
		this.label9.TabIndex = 15;
		this.label9.Text = "Tour Group Name:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobTG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTG.DropDownWidth = 200;
		this.cobTG.FormattingEnabled = true;
		this.cobTG.Location = new System.Drawing.Point(126, 36);
		this.cobTG.Name = "cobTG";
		this.cobTG.Size = new System.Drawing.Size(128, 22);
		this.cobTG.TabIndex = 16;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 160;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(383, 36);
		this.cobCer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(128, 23);
		this.cobCer.TabIndex = 20;
		this.label26.BackColor = System.Drawing.Color.Transparent;
		this.label26.Location = new System.Drawing.Point(260, 32);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(117, 29);
		this.label26.TabIndex = 19;
		this.label26.Text = "Certificate:";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label27.BackColor = System.Drawing.Color.Transparent;
		this.label27.Location = new System.Drawing.Point(260, 61);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(117, 29);
		this.label27.TabIndex = 21;
		this.label27.Text = "Number:";
		this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCernum.Location = new System.Drawing.Point(383, 65);
		this.txtCernum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtCernum.Name = "txtCernum";
		this.txtCernum.Size = new System.Drawing.Size(128, 22);
		this.txtCernum.TabIndex = 22;
		this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlLight;
		this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel2.Controls.Add(this.btnSearch);
		this.flowLayoutPanel2.Controls.Add(this.btnExport);
		this.flowLayoutPanel2.Controls.Add(this.btnReset);
		this.flowLayoutPanel2.Controls.Add(this.btnClose);
		this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel2.Name = "flowLayoutPanel2";
		this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(12, 5, 0, 0);
		this.flowLayoutPanel2.Size = new System.Drawing.Size(1008, 52);
		this.flowLayoutPanel2.TabIndex = 1;
		this.btnSearch.BackColor = System.Drawing.Color.LightGray;
		this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSearch.ForeColor = System.Drawing.Color.Black;
		this.btnSearch.GlowColor = System.Drawing.Color.White;
		this.btnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSearch.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSearch.Location = new System.Drawing.Point(15, 8);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSearch.Size = new System.Drawing.Size(90, 34);
		this.btnSearch.TabIndex = 1;
		this.btnSearch.Text = "Search";
		this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
		this.btnExport.BackColor = System.Drawing.Color.LightGray;
		this.btnExport.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExport.ForeColor = System.Drawing.Color.Black;
		this.btnExport.GlowColor = System.Drawing.Color.White;
		this.btnExport.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnExport.Image = LockSoftware.Properties.Resources.xls;
		this.btnExport.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnExport.Location = new System.Drawing.Point(111, 8);
		this.btnExport.Name = "btnExport";
		this.btnExport.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnExport.Size = new System.Drawing.Size(128, 34);
		this.btnExport.TabIndex = 2;
		this.btnExport.Text = "Export To Excel";
		this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.btnReset.BackColor = System.Drawing.Color.LightGray;
		this.btnReset.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReset.ForeColor = System.Drawing.Color.Black;
		this.btnReset.GlowColor = System.Drawing.Color.White;
		this.btnReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReset.Image = LockSoftware.Properties.Resources.clear;
		this.btnReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnReset.Location = new System.Drawing.Point(245, 8);
		this.btnReset.Name = "btnReset";
		this.btnReset.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnReset.Size = new System.Drawing.Size(71, 34);
		this.btnReset.TabIndex = 3;
		this.btnReset.Text = "Reset";
		this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(322, 8);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(71, 34);
		this.btnClose.TabIndex = 4;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1008, 494);
		base.Controls.Add(this.dgvlist);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.statusStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmSTGLogcs";
		this.Text = "frmSTGLogcs";
		base.Load += new System.EventHandler(frmSTGLogcs_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvlist).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.flowLayoutPanel2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
