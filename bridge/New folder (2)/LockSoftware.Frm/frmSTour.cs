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

public class frmSTour : Form
{
	public string m_objName = "WFsg";

	public Hashtable m_htab;

	public string m_extstr = "";

	public bool m_pars = true;

	public bool m_sum = true;

	public bool m_initctrl = true;

	private IContainer components;

	private ComboBox cobUser;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel tssLab1;

	private ToolStripStatusLabel tssLab2;

	private ToolStripStatusLabel tssLab3;

	private ToolStripStatusLabel tssLab4;

	public Panel panel2;

	private GlassBtn btnCols;

	private GlassBtn btnClose;

	private GlassBtn btnReset;

	private GlassBtn btnExport;

	public FlowLayoutPanel flowLayoutPanel1;

	public DataGridView dgvlist;

	public ComboBox cobType;

	public Label label1;

	public Label label6;

	public DateTimePicker dtpLevelE;

	public Label label5;

	public Label label4;

	public ComboBox cobFN;

	public ComboBox cobBN;

	public Label label3;

	public Label label2;

	public DateTimePicker dtpComeE;

	public DateTimePicker dtpLevelS;

	public Label label29;

	public DateTimePicker dtpComeS;

	public Label labArr;

	public ComboBox cobCer;

	public TextBox txtCernum;

	public Label label27;

	public Label label26;

	public TextBox txtRn;

	public Label label8;

	public TextBox txtGn;

	public Label label17;

	public Label label7;

	public ComboBox cobTG;

	public Label label9;

	public ComboBox cobTB;

	public TextBox txtTGG;

	public Label label10;

	public GlassBtn btnSearch;

	private Panel pnlGroup;

	private Panel pnlPersonal;

	private Panel panel1;

	public frmSTour()
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

	private void frmSTour_Load(object sender, EventArgs e)
	{
		tssLab1.Text = "";
		tssLab4.Text = "";
		tssLab2.Text = (string)m_htab["tssLab2"];
		btnExport.Enabled = SQLserver.GetUserPermisstion(1031, Program.m_OperID);
		if (m_initctrl)
		{
			dtpComeS.CustomFormat = Program.m_currDateTimeFmt;
			dtpComeE.CustomFormat = Program.m_currDateTimeFmt;
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
			InitType();
			InitOper();
			InitBuild();
			FlowLayoutPanel flowLayoutPanel = flowLayoutPanel1;
			bool visible = (panel2.Visible = true);
			flowLayoutPanel.Visible = visible;
			ComboBox comboBox = cobTB;
			string text = (cobTG.Text = "");
			comboBox.Text = text;
		}
		ToolStripStatusLabel toolStripStatusLabel = tssLab2;
		bool visible2 = (tssLab3.Visible = m_sum);
		toolStripStatusLabel.Visible = visible2;
		if (m_extstr != "")
		{
			InitDgvList();
		}
	}

	public void InitDgvList()
	{
		try
		{
			string text = "";
			if (m_extstr == "")
			{
				text = "Select '' As TID, team_id, TB_name, team_name, team_guide, Team_cername, team_cernum, r_cardnum, g_name,  cer_name, g_cernum, r_name, build_name, floor_name, TP_Name";
				text += ", g_cometime,g_sototalday As g_stayDay, g_stand_L_time, g_stayover, g_softime, g_soltime";
				text += ", g_sototalday, g_level, g_actual_l_time, g_level_card, g_othprice";
				text += " From v_TeamDetails Where 1 = 1 {0}";
				text += " Order by g_id desc";
			}
			else
			{
				text = m_extstr;
			}
			if (m_pars)
			{
				text = string.Format(text, GetPars());
			}
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
			dgvlist.Columns["g_sototalday"].Visible = false;
			tssLab1.Text = string.Format((string)m_htab["tssLab1"], dgvlist.Rows.Count);
			double num = 0.0;
			if (m_sum)
			{
				dgvlist.Columns["team_id"].Visible = false;
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					dgvlist.Rows[j].Cells["TID"].Value = "T" + Convert.ToInt32(dataTable.Rows[j]["team_id"]).ToString("D8");
					num += Convert.ToDouble(dataTable.Rows[j]["g_othprice"]);
				}
				tssLab3.Text = num.ToString("F2") + " " + Program.m_baseCurrCode;
			}
		}
		catch
		{
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		InitDgvList();
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
		try
		{
			TextBox textBox = txtRn;
			TextBox textBox2 = txtGn;
			string text = (txtCernum.Text = "");
			string text3 = (textBox2.Text = text);
			textBox.Text = text3;
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
			ComboBox comboBox = cobCer;
			ComboBox comboBox2 = cobBN;
			int num = (cobType.SelectedIndex = 0);
			int selectedIndex = (comboBox2.SelectedIndex = num);
			comboBox.SelectedIndex = selectedIndex;
			ComboBox comboBox3 = cobTB;
			string text5 = (cobTG.Text = "");
			comboBox3.Text = text5;
			txtTGG.Text = "";
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
			if (txtGn.Text.Trim() != "")
			{
				text = text + " And g_name like N'" + txtGn.Text.Trim() + "%'";
			}
			if (cobCer.DataSource != null && Convert.ToInt32(cobCer.SelectedValue) > 0)
			{
				text = text + " And cer_id=" + Convert.ToInt32(cobCer.SelectedValue);
			}
			if (txtCernum.Text.Trim() != "")
			{
				text = text + " And g_cernum like N'" + txtCernum.Text.Trim() + "%'";
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
				text = text + " And g_cometime >= '" + Program.GetStandDTime(dtpComeS.Value, "00") + "'";
			}
			if (dtpComeE.Checked)
			{
				text = text + " And g_cometime <= '" + Program.GetStandDTime(dtpComeE.Value, "59") + "'";
			}
			if (dtpLevelS.Checked)
			{
				text = text + " And g_actual_l_time >= '" + Program.GetStandDTime(dtpLevelS.Value, "00") + "'";
			}
			if (dtpLevelE.Checked)
			{
				text = text + " And g_actual_l_time <= '" + Program.GetStandDTime(dtpLevelE.Value, "59") + "'";
			}
			return text;
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + "\r\n" + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return "";
		}
	}

	private void cobTB_SelectedIndexChanged(object sender, EventArgs e)
	{
		InitTG();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmSTour));
		this.cobType = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
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
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.txtCernum = new System.Windows.Forms.TextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.txtRn = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtGn = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.dgvlist = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.tssLab1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab3 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tssLab4 = new System.Windows.Forms.ToolStripStatusLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pnlPersonal = new System.Windows.Forms.Panel();
		this.pnlGroup = new System.Windows.Forms.Panel();
		this.cobTB = new System.Windows.Forms.ComboBox();
		this.txtTGG = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.cobTG = new System.Windows.Forms.ComboBox();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnExport = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnCols = new LockSoftware.Controls.GlassBtn(this.components);
		((System.ComponentModel.ISupportInitialize)this.dgvlist).BeginInit();
		this.statusStrip1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.pnlPersonal.SuspendLayout();
		this.pnlGroup.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 160;
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(365, 7);
		this.cobType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(72, 23);
		this.cobType.TabIndex = 81;
		this.label1.Location = new System.Drawing.Point(263, 3);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(101, 29);
		this.label1.TabIndex = 80;
		this.label1.Text = "Room Type:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobUser.DropDownWidth = 160;
		this.cobUser.FormattingEnabled = true;
		this.cobUser.Location = new System.Drawing.Point(912, 72);
		this.cobUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobUser.Name = "cobUser";
		this.cobUser.Size = new System.Drawing.Size(72, 23);
		this.cobUser.TabIndex = 75;
		this.label6.Location = new System.Drawing.Point(805, 68);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(101, 29);
		this.label6.TabIndex = 74;
		this.label6.Text = "Operator:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpLevelE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelE.Location = new System.Drawing.Point(766, 111);
		this.dtpLevelE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpLevelE.Name = "dtpLevelE";
		this.dtpLevelE.ShowCheckBox = true;
		this.dtpLevelE.Size = new System.Drawing.Size(146, 21);
		this.dtpLevelE.TabIndex = 73;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(741, 116);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(19, 15);
		this.label5.TabIndex = 72;
		this.label5.Text = "→";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(280, 116);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(19, 15);
		this.label4.TabIndex = 71;
		this.label4.Text = "→";
		this.cobFN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFN.DropDownWidth = 160;
		this.cobFN.FormattingEnabled = true;
		this.cobFN.Location = new System.Drawing.Point(129, 38);
		this.cobFN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobFN.Name = "cobFN";
		this.cobFN.Size = new System.Drawing.Size(128, 23);
		this.cobFN.TabIndex = 70;
		this.cobBN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBN.DropDownWidth = 160;
		this.cobBN.FormattingEnabled = true;
		this.cobBN.Location = new System.Drawing.Point(129, 7);
		this.cobBN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobBN.Name = "cobBN";
		this.cobBN.Size = new System.Drawing.Size(128, 23);
		this.cobBN.TabIndex = 69;
		this.cobBN.SelectedIndexChanged += new System.EventHandler(cobBN_SelectedIndexChanged);
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(3, 34);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(117, 29);
		this.label3.TabIndex = 68;
		this.label3.Text = "Floor Name:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(3, 3);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(117, 29);
		this.label2.TabIndex = 67;
		this.label2.Text = "Building Name:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpComeE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeE.Location = new System.Drawing.Point(305, 111);
		this.dtpComeE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpComeE.Name = "dtpComeE";
		this.dtpComeE.ShowCheckBox = true;
		this.dtpComeE.Size = new System.Drawing.Size(146, 21);
		this.dtpComeE.TabIndex = 66;
		this.dtpLevelS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevelS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevelS.Location = new System.Drawing.Point(593, 111);
		this.dtpLevelS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpLevelS.Name = "dtpLevelS";
		this.dtpLevelS.ShowCheckBox = true;
		this.dtpLevelS.Size = new System.Drawing.Size(142, 21);
		this.dtpLevelS.TabIndex = 64;
		this.label29.BackColor = System.Drawing.Color.Transparent;
		this.label29.Location = new System.Drawing.Point(470, 109);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(117, 29);
		this.label29.TabIndex = 65;
		this.label29.Text = "Checking Out:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dtpComeS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeS.Location = new System.Drawing.Point(132, 111);
		this.dtpComeS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.dtpComeS.Name = "dtpComeS";
		this.dtpComeS.ShowCheckBox = true;
		this.dtpComeS.Size = new System.Drawing.Size(142, 21);
		this.dtpComeS.TabIndex = 62;
		this.labArr.BackColor = System.Drawing.Color.Transparent;
		this.labArr.Location = new System.Drawing.Point(6, 109);
		this.labArr.Name = "labArr";
		this.labArr.Size = new System.Drawing.Size(117, 29);
		this.labArr.TabIndex = 63;
		this.labArr.Text = "Checking In:";
		this.labArr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 160;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(126, 38);
		this.cobCer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(128, 23);
		this.cobCer.TabIndex = 59;
		this.txtCernum.Location = new System.Drawing.Point(126, 69);
		this.txtCernum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtCernum.MaxLength = 50;
		this.txtCernum.Name = "txtCernum";
		this.txtCernum.Size = new System.Drawing.Size(128, 21);
		this.txtCernum.TabIndex = 58;
		this.label27.BackColor = System.Drawing.Color.Transparent;
		this.label27.Location = new System.Drawing.Point(3, 65);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(117, 29);
		this.label27.TabIndex = 61;
		this.label27.Text = "Number:";
		this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label26.BackColor = System.Drawing.Color.Transparent;
		this.label26.Location = new System.Drawing.Point(3, 34);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(117, 29);
		this.label26.TabIndex = 60;
		this.label26.Text = "Certificate:";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRn.ForeColor = System.Drawing.Color.Black;
		this.txtRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtRn.Location = new System.Drawing.Point(129, 69);
		this.txtRn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtRn.MaxLength = 50;
		this.txtRn.Name = "txtRn";
		this.txtRn.Size = new System.Drawing.Size(128, 21);
		this.txtRn.TabIndex = 56;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Location = new System.Drawing.Point(3, 65);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(117, 29);
		this.label8.TabIndex = 57;
		this.label8.Text = "Room Name:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtGn.Location = new System.Drawing.Point(126, 8);
		this.txtGn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtGn.MaxLength = 50;
		this.txtGn.Name = "txtGn";
		this.txtGn.Size = new System.Drawing.Size(128, 21);
		this.txtGn.TabIndex = 54;
		this.label17.BackColor = System.Drawing.Color.Transparent;
		this.label17.Location = new System.Drawing.Point(3, 3);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(117, 29);
		this.label17.TabIndex = 55;
		this.label17.Text = "Guest Name:";
		this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dgvlist.AllowUserToAddRows = false;
		this.dgvlist.AllowUserToDeleteRows = false;
		this.dgvlist.BackgroundColor = System.Drawing.Color.White;
		this.dgvlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvlist.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvlist.Location = new System.Drawing.Point(0, 200);
		this.dgvlist.Name = "dgvlist";
		this.dgvlist.ReadOnly = true;
		this.dgvlist.RowHeadersWidth = 25;
		this.dgvlist.RowTemplate.Height = 23;
		this.dgvlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvlist.Size = new System.Drawing.Size(1008, 436);
		this.dgvlist.TabIndex = 3;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.tssLab1, this.tssLab2, this.tssLab3, this.tssLab4 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 636);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(1008, 26);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 4;
		this.statusStrip1.Text = "statusStrip1";
		this.tssLab1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.tssLab1.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.tssLab1.Name = "tssLab1";
		this.tssLab1.Size = new System.Drawing.Size(776, 21);
		this.tssLab1.Spring = true;
		this.tssLab1.Text = "Total:";
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
		this.panel2.AutoScroll = true;
		this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.panel2.Controls.Add(this.panel1);
		this.panel2.Controls.Add(this.pnlPersonal);
		this.panel2.Controls.Add(this.pnlGroup);
		this.panel2.Controls.Add(this.cobUser);
		this.panel2.Controls.Add(this.label6);
		this.panel2.Controls.Add(this.dtpLevelE);
		this.panel2.Controls.Add(this.label5);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.labArr);
		this.panel2.Controls.Add(this.dtpComeS);
		this.panel2.Controls.Add(this.label29);
		this.panel2.Controls.Add(this.dtpLevelS);
		this.panel2.Controls.Add(this.dtpComeE);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 52);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1008, 148);
		this.panel2.TabIndex = 84;
		this.panel2.Visible = false;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.cobBN);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.cobType);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.label8);
		this.panel1.Controls.Add(this.cobFN);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.txtRn);
		this.panel1.Location = new System.Drawing.Point(559, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(446, 100);
		this.panel1.TabIndex = 90;
		this.pnlPersonal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlPersonal.Controls.Add(this.txtGn);
		this.pnlPersonal.Controls.Add(this.cobCer);
		this.pnlPersonal.Controls.Add(this.label17);
		this.pnlPersonal.Controls.Add(this.txtCernum);
		this.pnlPersonal.Controls.Add(this.label27);
		this.pnlPersonal.Controls.Add(this.label26);
		this.pnlPersonal.Location = new System.Drawing.Point(281, 3);
		this.pnlPersonal.Name = "pnlPersonal";
		this.pnlPersonal.Size = new System.Drawing.Size(272, 100);
		this.pnlPersonal.TabIndex = 89;
		this.pnlGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlGroup.Controls.Add(this.cobTB);
		this.pnlGroup.Controls.Add(this.txtTGG);
		this.pnlGroup.Controls.Add(this.label7);
		this.pnlGroup.Controls.Add(this.label10);
		this.pnlGroup.Controls.Add(this.label9);
		this.pnlGroup.Controls.Add(this.cobTG);
		this.pnlGroup.Location = new System.Drawing.Point(3, 3);
		this.pnlGroup.Name = "pnlGroup";
		this.pnlGroup.Size = new System.Drawing.Size(272, 100);
		this.pnlGroup.TabIndex = 88;
		this.cobTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTB.FormattingEnabled = true;
		this.cobTB.Location = new System.Drawing.Point(129, 7);
		this.cobTB.Name = "cobTB";
		this.cobTB.Size = new System.Drawing.Size(128, 23);
		this.cobTB.TabIndex = 83;
		this.cobTB.SelectedIndexChanged += new System.EventHandler(cobTB_SelectedIndexChanged);
		this.txtTGG.Location = new System.Drawing.Point(129, 69);
		this.txtTGG.MaxLength = 50;
		this.txtTGG.Name = "txtTGG";
		this.txtTGG.Size = new System.Drawing.Size(128, 21);
		this.txtTGG.TabIndex = 87;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(3, 3);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(117, 29);
		this.label7.TabIndex = 82;
		this.label7.Text = "Travel Bureau:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label10.Location = new System.Drawing.Point(3, 65);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(117, 29);
		this.label10.TabIndex = 86;
		this.label10.Text = "Tour Group Guide:";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Location = new System.Drawing.Point(3, 34);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(117, 29);
		this.label9.TabIndex = 84;
		this.label9.Text = "Tour Group Name:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cobTG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cobTG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobTG.FormattingEnabled = true;
		this.cobTG.Location = new System.Drawing.Point(129, 38);
		this.cobTG.Name = "cobTG";
		this.cobTG.Size = new System.Drawing.Size(128, 23);
		this.cobTG.TabIndex = 85;
		this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
		this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel1.Controls.Add(this.btnSearch);
		this.flowLayoutPanel1.Controls.Add(this.btnExport);
		this.flowLayoutPanel1.Controls.Add(this.btnReset);
		this.flowLayoutPanel1.Controls.Add(this.btnClose);
		this.flowLayoutPanel1.Controls.Add(this.btnCols);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(12, 5, 0, 0);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(1008, 52);
		this.flowLayoutPanel1.TabIndex = 83;
		this.flowLayoutPanel1.Visible = false;
		this.btnSearch.AutoEllipsis = true;
		this.btnSearch.BackColor = System.Drawing.Color.Gainsboro;
		this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSearch.ForeColor = System.Drawing.Color.Black;
		this.btnSearch.GlowColor = System.Drawing.Color.White;
		this.btnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSearch.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSearch.Location = new System.Drawing.Point(15, 9);
		this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSearch.Size = new System.Drawing.Size(88, 35);
		this.btnSearch.TabIndex = 76;
		this.btnSearch.Text = "Search";
		this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
		this.btnExport.AutoSize = true;
		this.btnExport.BackColor = System.Drawing.Color.Gainsboro;
		this.btnExport.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExport.ForeColor = System.Drawing.Color.Black;
		this.btnExport.GlowColor = System.Drawing.Color.White;
		this.btnExport.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnExport.Image = LockSoftware.Properties.Resources.xls;
		this.btnExport.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnExport.Location = new System.Drawing.Point(109, 9);
		this.btnExport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnExport.Name = "btnExport";
		this.btnExport.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnExport.Size = new System.Drawing.Size(128, 35);
		this.btnExport.TabIndex = 77;
		this.btnExport.Text = "Export To Excel";
		this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.btnReset.AutoSize = true;
		this.btnReset.BackColor = System.Drawing.Color.Gainsboro;
		this.btnReset.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReset.ForeColor = System.Drawing.Color.Black;
		this.btnReset.GlowColor = System.Drawing.Color.White;
		this.btnReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReset.Image = LockSoftware.Properties.Resources.clear;
		this.btnReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnReset.Location = new System.Drawing.Point(243, 9);
		this.btnReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnReset.Name = "btnReset";
		this.btnReset.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnReset.Size = new System.Drawing.Size(72, 35);
		this.btnReset.TabIndex = 78;
		this.btnReset.Text = "Reset";
		this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReset.Click += new System.EventHandler(btnReset_Click);
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(321, 9);
		this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(68, 35);
		this.btnClose.TabIndex = 79;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnCols.AutoSize = true;
		this.btnCols.BackColor = System.Drawing.Color.Gainsboro;
		this.btnCols.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCols.ForeColor = System.Drawing.Color.Black;
		this.btnCols.GlowColor = System.Drawing.Color.White;
		this.btnCols.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCols.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.btnCols.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCols.Location = new System.Drawing.Point(395, 9);
		this.btnCols.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnCols.Name = "btnCols";
		this.btnCols.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCols.Size = new System.Drawing.Size(111, 35);
		this.btnCols.TabIndex = 82;
		this.btnCols.Text = "Disply Fields";
		this.btnCols.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCols.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCols.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.WhiteSmoke;
		base.ClientSize = new System.Drawing.Size(1008, 662);
		base.Controls.Add(this.dgvlist);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.flowLayoutPanel1);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "frmSTour";
		this.Text = "frmSTour";
		base.Load += new System.EventHandler(frmSTour_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvlist).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.pnlPersonal.ResumeLayout(false);
		this.pnlPersonal.PerformLayout();
		this.pnlGroup.ResumeLayout(false);
		this.pnlGroup.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
