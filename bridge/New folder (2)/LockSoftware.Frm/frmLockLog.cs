using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataBase;
using Dev_C_Sharp;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmLockLog : Form
{
	public string m_objName = "WFll";

	public Hashtable m_htab;

	private IContainer components;

	private clsBackPanel clsBackPanelLeft;

	private SplitContainer splContainerData;

	private DataGridView dgvList;

	private TableLayoutPanel tableLayoutPanel1;

	private ListView lvRec;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label2;

	private GlassBtn btnGetLock;

	private ImageList imageList1;

	private DateTimePicker dtpComeS;

	private DateTimePicker dtpComeE;

	private Label label1;

	private clsBackPanel clsBackPanel2;

	private ToolsBtn btnSear;

	private GlassBtn btnEp;

	private Label label6;

	private Label label31;

	private Label label30;

	private Label label35;

	private Label label36;

	private Label label37;

	private Label label12;

	private Label label13;

	private Label label14;

	private Label label32;

	private Label label33;

	private Label label34;

	private PictureBox pictureBox1;

	private Label label8;

	private LinkLabel linklabGrp;

	private SplitContainer splitContainerLeft;

	private Label label7;

	private LinkLabel linklabBl;

	private ListView lvGrp;

	private ListView lvBl;

	private GlassBtn btnClose;

	private TextBox textBox1;

	private ComboBox cobFD;

	private ComboBox cobBD;

	private ComboBox combRD;

	private NumericUpDown numUDTopNum;

	private ComboBox comBPages;

	private Label label9;

	public frmLockLog()
	{
		InitializeComponent();
	}

	private void InitlvRec()
	{
		try
		{
			lvRec.MultiSelect = false;
			lvRec.GridLines = true;
			lvRec.View = View.Details;
			lvRec.Items.Clear();
			lvRec.Columns.Clear();
			lvRec.Columns.Add("", 60);
			lvRec.Columns.Add((string)m_htab["dgvr_cardnum"], 60);
			lvRec.Columns.Add((string)m_htab["dgvl_opentime"], 160);
			lvRec.Columns.Add((string)m_htab["dgvMemo"], 160);
		}
		catch
		{
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

	private void InitRooms(int fid)
	{
		try
		{
			combRD.DataSource = null;
			string text = "Select R_ID,R_Name From D_Rooms ";
			if (fid > 0)
			{
				text = text + " Where R_FloorID=" + fid + " And R_Flag = 0";
			}
			text += " Order by R_ID, R_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["R_ID"] = 0;
				dataRow["R_Name"] = "";
				dataTable.Rows.InsertAt(dataRow, 0);
				combRD.DisplayMember = "R_Name";
				combRD.ValueMember = "R_ID";
				combRD.DataSource = dataTable.DefaultView;
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
				if (Convert.ToInt32(cobBD.SelectedValue) <= 0)
				{
					cobFD.Enabled = false;
				}
				else
				{
					cobFD.Enabled = true;
				}
			}
		}
		catch
		{
		}
	}

	private void cobFD_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cobFD.DataSource != null)
			{
				InitRooms(Convert.ToInt32(cobFD.SelectedValue));
				if (Convert.ToInt32(cobFD.SelectedValue) <= 0)
				{
					combRD.Enabled = false;
				}
				else
				{
					combRD.Enabled = true;
				}
			}
		}
		catch
		{
		}
	}

	private void InitdgvListColumn()
	{
		try
		{
			dgvList.Columns.Clear();
			dgvList.Rows.Clear();
			string sql = "Select top 1 (Row_Number() OVER (Order by cm_cardid, cm_Createtime)) AS RowNumber, cm_cardid As CardNum, '' As l_opentime, '' As Openmemo, cm_id As tabID,N'' As CardType, cm_user As UserName, cer_name, cm_cernum As cernum, bl_name As LockAddr, cm_Createtime As Createtime, cm_carddate As CardDate, cm_Creator As Creator, cm_logout As Logout, cm_logoutdate As LogoutDate, cm_Updator As Updator, cm_updatetime As UpdateTime From v_CardMgr";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				for (int i = 0; i < dataTable.Columns.Count; i++)
				{
					dgvList.Columns.Add(dataTable.Columns[i].ColumnName, (string)m_htab["dgv" + dataTable.Columns[i].ColumnName]);
				}
				dgvList.Columns["RowNumber"].HeaderText = "";
				dgvList.Columns["tabID"].Visible = false;
				dgvList.AutoResizeColumns();
			}
		}
		catch
		{
		}
	}

	private void btnGetLock_Click(object sender, EventArgs e)
	{
		try
		{
			StringBuilder stringBuilder = new StringBuilder(2048);
			StringBuilder stringBuilder2 = new StringBuilder(7680);
			int num = -1;
			num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.ReadCardS70(stringBuilder, stringBuilder2, Buzzer: false);
			textBox1.Text = Program.m_tmpval;
			if (num < 0)
			{
				Program.MsgCustom((string)m_htab["Err01"] + num, MessageBoxIcon.Hand);
				return;
			}
			if (num == 0)
			{
				Program.MsgCustom((string)m_htab["Err02"], MessageBoxIcon.Hand);
				return;
			}
			Program.RadioDevBuzzer(1, 2);
			string[] array = stringBuilder.ToString().Split(';');
			label35.Text = array[1].ToString();
			label36.Text = array[3].ToString();
			label37.Text = array[5].ToString();
			string[] array2 = array[2].ToString().Split(',');
			string[] array3 = array[4].ToString().Split(',');
			string[] array4 = array[6].ToString().Split(',');
			label30.Text = array2[0] + " - " + array2[1] + " - " + array2[2] + " - " + array2[3];
			string sql = "Select * From v_HotelRooms Where Build_Code = " + array2[0] + " And Floor_Code = " + array2[1] + " And R_Code = " + array2[2] + " And R_SubCode = " + array2[3];
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null)
			{
				Program.MsgCustom((string)m_htab["Err03"], MessageBoxIcon.Hand);
			}
			if (dataTable != null && dataTable.Rows.Count <= 0)
			{
				Program.MsgCustom((string)m_htab["Err04"], MessageBoxIcon.Asterisk);
			}
			else if (dataTable != null)
			{
				label31.Text = dataTable.Rows[0]["R_Name"].ToString().Trim();
				label32.Text = dataTable.Rows[0]["Build_Name"].ToString().Trim();
				label33.Text = dataTable.Rows[0]["Floor_Name"].ToString().Trim();
				label34.Text = dataTable.Rows[0]["TP_Name"].ToString().Trim();
			}
			lvGrp.Items.Clear();
			lvBl.Items.Clear();
			for (int i = 0; i < array3.Length; i++)
			{
				if (!(array3[i].Trim() == ""))
				{
					lvGrp.Items.Add(array3[i]);
				}
			}
			for (int j = 0; j < array4.Length; j++)
			{
				if (!(array4[j].Trim() == ""))
				{
					lvBl.Items.Add(array4[j]);
				}
			}
			string[] array5 = stringBuilder2.ToString().Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			try
			{
				sql = "";
				string text = array2[0];
				string text2 = array2[1];
				string text3 = array2[2];
				string text4 = array2[3];
				int num2 = 1;
				string[] array6 = array5;
				foreach (string text5 in array6)
				{
					string text6 = sql;
					sql = text6 + "insert into T_LockRecords values(" + text + "," + text2 + "," + text3 + "," + text4 + ",";
					object obj = sql;
					sql = string.Concat(obj, text5.Replace("#", "").Replace(",", ",'").Trim(), "',", num2, ",getdate())\n");
					num2++;
				}
				SQLserver.Data_ExecuteSql(sql);
			}
			catch
			{
			}
			sql = "";
			ListViewItem[] array7 = new ListViewItem[array5.Length];
			lvRec.Items.Clear();
			for (int l = 0; l < array5.Length; l++)
			{
				if (array5[l].Trim() == "")
				{
					continue;
				}
				string[] array8 = array5[l].Split(',');
				if (array8 != null)
				{
					sql = sql + array8[0].Replace("#", "").Trim() + ",";
					string[] array9 = new string[4]
					{
						(l + 1).ToString(),
						array8[0].Trim(),
						array8[1].Trim(),
						null
					};
					if (array8[0].Trim() == "0")
					{
						array9[3] = (string)m_htab["MachKey"];
					}
					array7[l] = new ListViewItem(array9);
				}
			}
			if (array7.Length > 0)
			{
				lvRec.Items.AddRange(array7);
			}
			if (dgvList.DataSource != null)
			{
				dgvList.DataSource = null;
				InitdgvListColumn();
			}
			else
			{
				dgvList.Rows.Clear();
			}
			if (sql != "")
			{
				initData(sql, array5);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnGetLock.Text);
		}
	}

	private void initData(string sql0, string[] recstr)
	{
		string text = sql0.Substring(0, sql0.Length - 1);
		int num = 0;
		num = ((recstr != null) ? 1000 : ((int)numUDTopNum.Value));
		string text2 = "Select top " + num + " ";
		if (recstr != null)
		{
			text2 += "(Row_Number() OVER (Order by CardNum, Createtime)) AS RowNumber, *";
		}
		else
		{
			text2 += "(Row_Number() OVER (Order by T_LR.OpenTime desc)) AS RowNumber,T_LR.C_Num as CardNum,ct_code,";
			string text3 = text2;
			text2 = text3 + "T_LR.OpenTime as l_opentime,Openmemo=(case when T_LR.C_Num=0 then N'" + (string)m_htab["MachKey"] + "' else N'" + (string)m_htab["NormalKey"] + "' end),tabID,CardType,UserName, cer_name,cernum,LockAddr,Createtime,CardDate, Creator,";
			text2 += "Logout,LogoutDate,Updator,UpdateTime";
		}
		text2 += " From (Select r_cardnum As CardNum,6 as ct_code,";
		text2 += "'' As l_opentime,''As Openmemo, g_id As tabID";
		object obj = text2;
		text2 = string.Concat(obj, ", '", Program.m_hPubTab["devct06"], "' As CardType, g_name As UserName, cer_name, g_cernum As cernum, (Build_Name + ' ' + Floor_name + ' ' + r_name) As LockAddr,  Createtime, CONVERT(varchar, g_stand_L_time, 120) As CardDate, Creator, g_logout As Logout, g_logoutdate As LogoutDate, Updator, UpdateTime From v_CardGuest");
		text2 = ((recstr == null) ? (text2 ?? "") : (text2 + " Where r_cardnum in (" + text + ")"));
		text2 += " Union all Select cm_cardid As CardNum,ct_code,'' As l_opentime,";
		text2 += "''As Openmemo, cm_id As tabID,N'' As CardType, cm_user As UserName, cer_name, cm_cernum As cernum, (bl_name + ' ' + f_name + ' ' + r_name) As LockAddr, cm_Createtime As Createtime, (RTrim(cm_carddate) + ' ' + RTrim(cm_carddateST) + '→' + RTrim(cm_carddateET)) As CardDate, cm_Creator As Creator, cm_logout As Logout, cm_logoutdate As LogoutDate, cm_Updator As Updator, cm_updatetime As UpdateTime From v_CardMgr";
		text2 = ((recstr == null) ? (text2 ?? "") : (text2 + " Where cm_cardid in (" + text + ") And ct_code > 9 And ct_code < 100"));
		text2 += " Union all Select distinct cm_cardid As CardNum,ct_code,'' As l_opentime,";
		text2 += "'' As Openmemo, cm_id As tabID,N'' As CardType, cm_user As UserName, cer_name, cm_cernum As cernum, (dbo.grpJoinStr(cm_id)) As LockAddr, cm_Createtime As Createtime, (RTrim(cm_carddate) + ' ' + RTrim(cm_carddateST) + '→' + RTrim(cm_carddateET)) As CardDate, cm_Creator As Creator, cm_logout As Logout, cm_logoutdate As LogoutDate, cm_Updator As Updator, cm_updatetime As UpdateTime From v_CardGrp";
		text2 = ((recstr == null) ? (text2 ?? "") : (text2 + " Where cm_cardid in (" + text + ") And ct_code = 9"));
		text2 += ") As TmpTab";
		if (recstr != null)
		{
			text2 += "\n";
		}
		else
		{
			text2 += " right join (select * from T_LockRecords ";
			if (Convert.ToInt32(cobBD.SelectedValue) > 0 || dtpComeS.Checked || dtpComeE.Checked)
			{
				text2 += "where 0=0 ";
			}
			if (Convert.ToInt32(cobBD.SelectedValue) > 0)
			{
				text2 = text2 + "and T_LockRecords.B_Code=" + Convert.ToInt32(cobBD.SelectedValue);
			}
			if (Convert.ToInt32(cobFD.SelectedValue) > 0)
			{
				text2 = text2 + "and T_LockRecords.F_Code=" + Convert.ToInt32(cobFD.SelectedValue);
			}
			if (Convert.ToInt32(combRD.SelectedValue) > 0)
			{
				text2 = text2 + "and T_LockRecords.R_Code=" + Convert.ToInt32(combRD.SelectedValue);
			}
			if (dtpComeS.Checked)
			{
				string text4 = "";
				text4 = Program.GetStandDTime(dtpComeS.Value);
				text2 = text2 + "and T_LockRecords.OpenTime>={ts'" + text4 + ":00'}";
			}
			if (dtpComeE.Checked)
			{
				string text5 = "";
				text5 = Program.GetStandDTime(dtpComeE.Value);
				text2 = text2 + "and T_LockRecords.OpenTime<={ts'" + text5 + ":00'}";
			}
			text2 += ")as T_LR on TmpTab.CardNum=T_LR.C_Num";
			text2 += " order by T_LR.OpenTime desc\n";
		}
		DataTable dataTable = SQLserver.Data_GetDataTable(text2);
		if (dataTable != null)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				try
				{
					int num2 = Convert.ToInt32(dataTable.Rows[i]["ct_code"]);
					dataTable.Rows[i]["CardType"] = Program.m_hPubTab[(num2 > 9) ? ("devct" + num2) : ("devct0" + num2)];
				}
				catch
				{
				}
			}
		}
		int num3 = 0;
		if (recstr == null)
		{
			dgvList.Columns["RowNumber"].DataPropertyName = "RowNumber";
			dgvList.Columns["CardNum"].DataPropertyName = "CardNum";
			dgvList.Columns["l_opentime"].DataPropertyName = "l_opentime";
			dgvList.Columns["Openmemo"].DataPropertyName = "Openmemo";
			dgvList.Columns["tabID"].DataPropertyName = "tabID";
			dgvList.Columns["CardType"].DataPropertyName = "CardType";
			dgvList.Columns["UserName"].DataPropertyName = "UserName";
			dgvList.Columns["cer_name"].DataPropertyName = "cer_name";
			dgvList.Columns["cernum"].DataPropertyName = "cernum";
			dgvList.Columns["LockAddr"].DataPropertyName = "LockAddr";
			dgvList.Columns["Createtime"].DataPropertyName = "Createtime";
			dgvList.Columns["CardDate"].DataPropertyName = "CardDate";
			dgvList.Columns["Creator"].DataPropertyName = "Creator";
			dgvList.Columns["Logout"].DataPropertyName = "Logout";
			dgvList.Columns["LogoutDate"].DataPropertyName = "LogoutDate";
			dgvList.Columns["Updator"].DataPropertyName = "Updator";
			dgvList.Columns["UpdateTime"].DataPropertyName = "UpdateTime";
			dgvList.DataSource = dataTable.DefaultView;
			try
			{
				dgvList.Columns["ct_code"].Visible = false;
			}
			catch
			{
			}
		}
		else
		{
			for (int j = 0; j < recstr.Length; j++)
			{
				if (recstr[j].Trim() == "")
				{
					continue;
				}
				string[] array = recstr[j].Split(',');
				object[] array2 = new object[dataTable.Columns.Count];
				if (array == null)
				{
					continue;
				}
				string text6 = array[0].Replace("#", "");
				string text7 = "";
				try
				{
					text7 = Convert.ToDateTime(array[1]).ToString("yyyy-MM-dd HH:mm");
				}
				catch
				{
					continue;
				}
				DataRow[] array3 = dataTable.Select("CardNum=" + text6);
				if (text6 != "0" && array3 != null && array3.Length > 0)
				{
					for (int k = 0; k < array3.Length; k++)
					{
						array2[0] = num3 + 1;
						array2[1] = Convert.ToInt32(text6);
						array2[2] = text7;
						array2[3] = (string)m_htab["NormalKey"];
						for (int l = 5; l < array2.Length; l++)
						{
							if (array3[k][l].ToString().Trim() == "")
							{
								array2[l - 1] = null;
							}
							else
							{
								array2[l - 1] = array3[k][l];
							}
						}
						dgvList.Rows.Add(array2);
						num3++;
					}
				}
				else
				{
					array2[0] = num3 + 1;
					array2[1] = Convert.ToInt32(text6);
					array2[2] = text7;
					array2[3] = (string)m_htab["NormalKey"];
					if (text6 == "0")
					{
						array2[3] = (string)m_htab["MachKey"];
					}
					for (int m = 4; m < array2.Length; m++)
					{
						array2[m] = null;
					}
					dgvList.Rows.Add(array2);
					num3++;
				}
			}
		}
		dgvList.AutoResizeColumns();
	}

	private void frmLockLog_Load(object sender, EventArgs e)
	{
		try
		{
			m_htab = Program.GetControlName(this, m_objName);
			btnEp.Enabled = SQLserver.GetUserPermisstion(1039, Program.m_OperID);
			for (int i = 0; i < 8; i++)
			{
				tableLayoutPanel1.Controls["label" + (30 + i)].Text = "";
			}
			InitlvRec();
			InitdgvListColumn();
			dtpComeE.CustomFormat = Program.m_currDateTimeFmt;
			dtpComeS.CustomFormat = Program.m_currDateTimeFmt;
			DateTime now = DateTime.Now;
			dtpComeS.Value = Convert.ToDateTime(Program.GetLocDate(now.AddMonths(-1)) + " 00:00:00");
			dtpComeE.Value = Convert.ToDateTime(Program.GetLocDate(now) + " 23:59:59");
			DateTimePicker dateTimePicker = dtpComeS;
			bool flag = (dtpComeE.Checked = false);
			dateTimePicker.Checked = flag;
			InitBuild();
		}
		catch
		{
		}
	}

	private void btnEp_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			if (dgvList.Rows.Count <= 0)
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

	private void btnSear_Click(object sender, EventArgs e)
	{
		try
		{
			initData("  ", null);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void label34_TextChanged(object sender, EventArgs e)
	{
		label9.Text = ((Label)sender).Text.Trim();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmLockLog));
		this.splContainerData = new System.Windows.Forms.SplitContainer();
		this.label9 = new System.Windows.Forms.Label();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.comBPages = new System.Windows.Forms.ComboBox();
		this.numUDTopNum = new System.Windows.Forms.NumericUpDown();
		this.combRD = new System.Windows.Forms.ComboBox();
		this.cobFD = new System.Windows.Forms.ComboBox();
		this.cobBD = new System.Windows.Forms.ComboBox();
		this.btnEp = new LockSoftware.Controls.GlassBtn(this.components);
		this.dtpComeE = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpComeS = new System.Windows.Forms.DateTimePicker();
		this.btnSear = new LockSoftware.Controls.ToolsBtn(this.components);
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.lvRec = new System.Windows.Forms.ListView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.clsBackPanelLeft = new LockSoftware.Controls.clsBackPanel(this.components);
		this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
		this.lvGrp = new System.Windows.Forms.ListView();
		this.label7 = new System.Windows.Forms.Label();
		this.linklabGrp = new System.Windows.Forms.LinkLabel();
		this.lvBl = new System.Windows.Forms.ListView();
		this.linklabBl = new System.Windows.Forms.LinkLabel();
		this.label8 = new System.Windows.Forms.Label();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.label12 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label37 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.btnGetLock = new LockSoftware.Controls.GlassBtn(this.components);
		this.splContainerData.Panel1.SuspendLayout();
		this.splContainerData.Panel2.SuspendLayout();
		this.splContainerData.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.clsBackPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numUDTopNum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.clsBackPanelLeft.SuspendLayout();
		this.splitContainerLeft.Panel1.SuspendLayout();
		this.splitContainerLeft.Panel2.SuspendLayout();
		this.splitContainerLeft.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.splContainerData.BackColor = System.Drawing.Color.WhiteSmoke;
		this.splContainerData.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splContainerData.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splContainerData.Location = new System.Drawing.Point(320, 0);
		this.splContainerData.Margin = new System.Windows.Forms.Padding(4);
		this.splContainerData.Name = "splContainerData";
		this.splContainerData.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splContainerData.Panel1.Controls.Add(this.label9);
		this.splContainerData.Panel1.Controls.Add(this.dgvList);
		this.splContainerData.Panel1.Controls.Add(this.textBox1);
		this.splContainerData.Panel1.Controls.Add(this.clsBackPanel2);
		this.splContainerData.Panel2.Controls.Add(this.lvRec);
		this.splContainerData.Panel2Collapsed = true;
		this.splContainerData.Size = new System.Drawing.Size(688, 546);
		this.splContainerData.SplitterDistance = 270;
		this.splContainerData.SplitterWidth = 5;
		this.splContainerData.TabIndex = 1;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.Maroon;
		this.label9.Location = new System.Drawing.Point(7, 89);
		this.label9.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(297, 19);
		this.label9.TabIndex = 22;
		this.label9.Text = "临时组件，临时解决显示异常的怪异问题";
		this.label9.Visible = false;
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 83);
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersVisible = false;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(688, 239);
		this.dgvList.TabIndex = 0;
		this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.textBox1.Location = new System.Drawing.Point(0, 322);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.textBox1.Size = new System.Drawing.Size(688, 224);
		this.textBox1.TabIndex = 2;
		this.textBox1.Visible = false;
		this.clsBackPanel2.Border = true;
		this.clsBackPanel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderBW = 1;
		this.clsBackPanel2.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderLW = 1;
		this.clsBackPanel2.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderRW = 1;
		this.clsBackPanel2.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderTW = 0;
		this.clsBackPanel2.Color1 = System.Drawing.Color.White;
		this.clsBackPanel2.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.comBPages);
		this.clsBackPanel2.Controls.Add(this.numUDTopNum);
		this.clsBackPanel2.Controls.Add(this.combRD);
		this.clsBackPanel2.Controls.Add(this.cobFD);
		this.clsBackPanel2.Controls.Add(this.cobBD);
		this.clsBackPanel2.Controls.Add(this.btnEp);
		this.clsBackPanel2.Controls.Add(this.dtpComeE);
		this.clsBackPanel2.Controls.Add(this.label1);
		this.clsBackPanel2.Controls.Add(this.dtpComeS);
		this.clsBackPanel2.Controls.Add(this.btnSear);
		this.clsBackPanel2.Controls.Add(this.pictureBox1);
		this.clsBackPanel2.Controls.Add(this.btnClose);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(688, 83);
		this.clsBackPanel2.TabIndex = 1;
		this.comBPages.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.comBPages.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.comBPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comBPages.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.comBPages.FormattingEnabled = true;
		this.comBPages.Location = new System.Drawing.Point(564, 27);
		this.comBPages.Name = "comBPages";
		this.comBPages.Size = new System.Drawing.Size(40, 24);
		this.comBPages.TabIndex = 22;
		this.comBPages.Visible = false;
		this.numUDTopNum.Location = new System.Drawing.Point(304, 40);
		this.numUDTopNum.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.numUDTopNum.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numUDTopNum.Name = "numUDTopNum";
		this.numUDTopNum.Size = new System.Drawing.Size(66, 24);
		this.numUDTopNum.TabIndex = 21;
		this.numUDTopNum.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.combRD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.combRD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.combRD.Enabled = false;
		this.combRD.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.combRD.FormattingEnabled = true;
		this.combRD.Location = new System.Drawing.Point(232, 39);
		this.combRD.Name = "combRD";
		this.combRD.Size = new System.Drawing.Size(66, 24);
		this.combRD.TabIndex = 20;
		this.cobFD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFD.Enabled = false;
		this.cobFD.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobFD.FormattingEnabled = true;
		this.cobFD.Location = new System.Drawing.Point(304, 9);
		this.cobFD.Name = "cobFD";
		this.cobFD.Size = new System.Drawing.Size(66, 24);
		this.cobFD.TabIndex = 19;
		this.cobFD.SelectedIndexChanged += new System.EventHandler(cobFD_SelectedIndexChanged);
		this.cobBD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBD.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobBD.FormattingEnabled = true;
		this.cobBD.Location = new System.Drawing.Point(232, 9);
		this.cobBD.Name = "cobBD";
		this.cobBD.Size = new System.Drawing.Size(66, 24);
		this.cobBD.TabIndex = 18;
		this.cobBD.SelectedIndexChanged += new System.EventHandler(cobBD_SelectedIndexChanged);
		this.btnEp.AutoEllipsis = true;
		this.btnEp.BackColor = System.Drawing.Color.Gainsboro;
		this.btnEp.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnEp.ForeColor = System.Drawing.Color.Black;
		this.btnEp.GlowColor = System.Drawing.Color.White;
		this.btnEp.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnEp.Image = LockSoftware.Properties.Resources.xls;
		this.btnEp.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnEp.Location = new System.Drawing.Point(422, 17);
		this.btnEp.Name = "btnEp";
		this.btnEp.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnEp.Size = new System.Drawing.Size(136, 42);
		this.btnEp.TabIndex = 15;
		this.btnEp.Text = "Export To Excel";
		this.btnEp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEp.Click += new System.EventHandler(btnEp_Click);
		this.dtpComeE.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeE.Location = new System.Drawing.Point(70, 39);
		this.dtpComeE.Margin = new System.Windows.Forms.Padding(0);
		this.dtpComeE.Name = "dtpComeE";
		this.dtpComeE.ShowCheckBox = true;
		this.dtpComeE.Size = new System.Drawing.Size(159, 24);
		this.dtpComeE.TabIndex = 12;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(52, 45);
		this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(18, 16);
		this.label1.TabIndex = 11;
		this.label1.Text = "→";
		this.dtpComeS.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpComeS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpComeS.Location = new System.Drawing.Point(55, 9);
		this.dtpComeS.Margin = new System.Windows.Forms.Padding(0);
		this.dtpComeS.Name = "dtpComeS";
		this.dtpComeS.ShowCheckBox = true;
		this.dtpComeS.Size = new System.Drawing.Size(174, 24);
		this.dtpComeS.TabIndex = 10;
		this.btnSear.BackColor = System.Drawing.Color.Transparent;
		this.btnSear.Checked = false;
		this.btnSear.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnSear.DefaultColor = System.Drawing.Color.Transparent;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSear.ImageNew = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSear.ImageRedrawed = true;
		this.btnSear.ImageStyle = 0;
		this.btnSear.isButton = true;
		this.btnSear.Location = new System.Drawing.Point(376, 12);
		this.btnSear.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnSear.MouseDownEndColor = System.Drawing.Color.Silver;
		this.btnSear.MouseDownStartColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterBorderColor = System.Drawing.Color.LightGray;
		this.btnSear.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnSear.Name = "btnSear";
		this.btnSear.Size = new System.Drawing.Size(40, 48);
		this.btnSear.TabIndex = 14;
		this.btnSear.TextImageLocation = 0;
		this.btnSear.TextNew = "";
		this.btnSear.TextRedrawed = false;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.BackgroundImage = LockSoftware.Properties.Resources._052;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(52, 83);
		this.pictureBox1.TabIndex = 16;
		this.pictureBox1.TabStop = false;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(610, 17);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(75, 42);
		this.btnClose.TabIndex = 17;
		this.btnClose.Text = "Close";
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.lvRec.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.lvRec.FullRowSelect = true;
		this.lvRec.Location = new System.Drawing.Point(23, 4);
		this.lvRec.Name = "lvRec";
		this.lvRec.Size = new System.Drawing.Size(127, 55);
		this.lvRec.TabIndex = 0;
		this.lvRec.UseCompatibleStateImageBehavior = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Locker.png");
		this.clsBackPanelLeft.Border = true;
		this.clsBackPanelLeft.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelLeft.BorderBW = 1;
		this.clsBackPanelLeft.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanelLeft.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanelLeft.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanelLeft.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanelLeft.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelLeft.BorderLW = 1;
		this.clsBackPanelLeft.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelLeft.BorderRW = 1;
		this.clsBackPanelLeft.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelLeft.BorderTW = 1;
		this.clsBackPanelLeft.Color1 = System.Drawing.Color.White;
		this.clsBackPanelLeft.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanelLeft.ColorAngle = 90f;
		this.clsBackPanelLeft.Controls.Add(this.splitContainerLeft);
		this.clsBackPanelLeft.Controls.Add(this.tableLayoutPanel1);
		this.clsBackPanelLeft.Controls.Add(this.btnGetLock);
		this.clsBackPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.clsBackPanelLeft.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanelLeft.Margin = new System.Windows.Forms.Padding(4);
		this.clsBackPanelLeft.Name = "clsBackPanelLeft";
		this.clsBackPanelLeft.Size = new System.Drawing.Size(320, 546);
		this.clsBackPanelLeft.TabIndex = 0;
		this.splitContainerLeft.BackColor = System.Drawing.Color.Transparent;
		this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainerLeft.Location = new System.Drawing.Point(0, 293);
		this.splitContainerLeft.Margin = new System.Windows.Forms.Padding(0);
		this.splitContainerLeft.Name = "splitContainerLeft";
		this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainerLeft.Panel1.Controls.Add(this.lvGrp);
		this.splitContainerLeft.Panel1.Controls.Add(this.label7);
		this.splitContainerLeft.Panel1.Controls.Add(this.linklabGrp);
		this.splitContainerLeft.Panel2.Controls.Add(this.lvBl);
		this.splitContainerLeft.Panel2.Controls.Add(this.linklabBl);
		this.splitContainerLeft.Panel2.Controls.Add(this.label8);
		this.splitContainerLeft.Size = new System.Drawing.Size(320, 253);
		this.splitContainerLeft.SplitterDistance = 122;
		this.splitContainerLeft.TabIndex = 26;
		this.lvGrp.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvGrp.Location = new System.Drawing.Point(0, 19);
		this.lvGrp.Name = "lvGrp";
		this.lvGrp.Size = new System.Drawing.Size(320, 83);
		this.lvGrp.TabIndex = 3;
		this.lvGrp.UseCompatibleStateImageBehavior = false;
		this.lvGrp.View = System.Windows.Forms.View.List;
		this.label7.AutoSize = true;
		this.label7.Dock = System.Windows.Forms.DockStyle.Top;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold);
		this.label7.ForeColor = System.Drawing.Color.Teal;
		this.label7.Location = new System.Drawing.Point(0, 0);
		this.label7.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(86, 19);
		this.label7.TabIndex = 22;
		this.label7.Text = "Group List:";
		this.linklabGrp.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.linklabGrp.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.linklabGrp.Location = new System.Drawing.Point(0, 102);
		this.linklabGrp.Name = "linklabGrp";
		this.linklabGrp.Size = new System.Drawing.Size(320, 20);
		this.linklabGrp.TabIndex = 25;
		this.linklabGrp.TabStop = true;
		this.linklabGrp.Text = "Group Details";
		this.linklabGrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.linklabGrp.Visible = false;
		this.lvBl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvBl.Location = new System.Drawing.Point(0, 19);
		this.lvBl.Name = "lvBl";
		this.lvBl.Size = new System.Drawing.Size(320, 80);
		this.lvBl.TabIndex = 3;
		this.lvBl.UseCompatibleStateImageBehavior = false;
		this.lvBl.View = System.Windows.Forms.View.List;
		this.linklabBl.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.linklabBl.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.linklabBl.Location = new System.Drawing.Point(0, 99);
		this.linklabBl.Name = "linklabBl";
		this.linklabBl.Size = new System.Drawing.Size(320, 28);
		this.linklabBl.TabIndex = 2;
		this.linklabBl.TabStop = true;
		this.linklabBl.Text = "Black Details";
		this.linklabBl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.linklabBl.Visible = false;
		this.label8.AutoSize = true;
		this.label8.Dock = System.Windows.Forms.DockStyle.Top;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold);
		this.label8.ForeColor = System.Drawing.Color.Teal;
		this.label8.Location = new System.Drawing.Point(0, 0);
		this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(83, 19);
		this.label8.TabIndex = 0;
		this.label8.Text = "Black List:";
		this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
		this.tableLayoutPanel1.Controls.Add(this.label32, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.label35, 1, 5);
		this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 6);
		this.tableLayoutPanel1.Controls.Add(this.label33, 1, 3);
		this.tableLayoutPanel1.Controls.Add(this.label36, 1, 6);
		this.tableLayoutPanel1.Controls.Add(this.label14, 0, 4);
		this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
		this.tableLayoutPanel1.Controls.Add(this.label34, 1, 4);
		this.tableLayoutPanel1.Controls.Add(this.label37, 1, 7);
		this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.label30, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label31, 1, 1);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Teal;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 53);
		this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 8;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 240);
		this.tableLayoutPanel1.TabIndex = 0;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(4, 66);
		this.label12.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(113, 19);
		this.label12.TabIndex = 16;
		this.label12.Text = "Building Name:";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold);
		this.label5.Location = new System.Drawing.Point(4, 156);
		this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(86, 19);
		this.label5.TabIndex = 5;
		this.label5.Text = "Lock Time:";
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label32.ForeColor = System.Drawing.Color.Maroon;
		this.label32.Location = new System.Drawing.Point(121, 66);
		this.label32.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(53, 19);
		this.label32.TabIndex = 19;
		this.label32.Text = "label15";
		this.label32.TextChanged += new System.EventHandler(label34_TextChanged);
		this.label35.AutoSize = true;
		this.label35.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label35.ForeColor = System.Drawing.Color.Maroon;
		this.label35.Location = new System.Drawing.Point(121, 156);
		this.label35.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(45, 19);
		this.label35.TabIndex = 13;
		this.label35.Text = "label9";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold);
		this.label13.Location = new System.Drawing.Point(4, 96);
		this.label13.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(93, 19);
		this.label13.TabIndex = 17;
		this.label13.Text = "Floor Name:";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold);
		this.label2.Location = new System.Drawing.Point(4, 186);
		this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(104, 19);
		this.label2.TabIndex = 8;
		this.label2.Text = "Lock Opened:";
		this.label33.AutoSize = true;
		this.label33.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label33.ForeColor = System.Drawing.Color.Maroon;
		this.label33.Location = new System.Drawing.Point(121, 96);
		this.label33.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(53, 19);
		this.label33.TabIndex = 20;
		this.label33.Text = "label16";
		this.label33.TextChanged += new System.EventHandler(label34_TextChanged);
		this.label36.AutoSize = true;
		this.label36.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label36.ForeColor = System.Drawing.Color.Maroon;
		this.label36.Location = new System.Drawing.Point(121, 186);
		this.label36.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(53, 19);
		this.label36.TabIndex = 14;
		this.label36.Text = "label10";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(4, 126);
		this.label14.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(91, 19);
		this.label14.TabIndex = 18;
		this.label14.Text = "Room Type:";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(4, 216);
		this.label6.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(107, 19);
		this.label6.TabIndex = 10;
		this.label6.Text = "Limit Number:";
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label34.ForeColor = System.Drawing.Color.Maroon;
		this.label34.Location = new System.Drawing.Point(121, 126);
		this.label34.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(73, 19);
		this.label34.TabIndex = 21;
		this.label34.Text = "豪华客房";
		this.label34.TextChanged += new System.EventHandler(label34_TextChanged);
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.ForeColor = System.Drawing.Color.Maroon;
		this.label37.Location = new System.Drawing.Point(121, 216);
		this.label37.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(52, 19);
		this.label37.TabIndex = 15;
		this.label37.Text = "label11";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(4, 6);
		this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(106, 19);
		this.label4.TabIndex = 4;
		this.label4.Text = "Lock Address:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(4, 36);
		this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(98, 19);
		this.label3.TabIndex = 3;
		this.label3.Text = "Room Name:";
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label30.ForeColor = System.Drawing.Color.Maroon;
		this.label30.Location = new System.Drawing.Point(121, 6);
		this.label30.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(45, 19);
		this.label30.TabIndex = 12;
		this.label30.Text = "label8";
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label31.ForeColor = System.Drawing.Color.Maroon;
		this.label31.Location = new System.Drawing.Point(121, 36);
		this.label31.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(45, 19);
		this.label31.TabIndex = 11;
		this.label31.Text = "label7";
		this.label31.TextChanged += new System.EventHandler(label34_TextChanged);
		this.btnGetLock.AutoEllipsis = true;
		this.btnGetLock.BackColor = System.Drawing.Color.Gainsboro;
		this.btnGetLock.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnGetLock.Font = new System.Drawing.Font("Times New Roman", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGetLock.ForeColor = System.Drawing.Color.Olive;
		this.btnGetLock.GlowColor = System.Drawing.Color.White;
		this.btnGetLock.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnGetLock.ImageIndex = 0;
		this.btnGetLock.ImageList = this.imageList1;
		this.btnGetLock.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnGetLock.Location = new System.Drawing.Point(0, 0);
		this.btnGetLock.Name = "btnGetLock";
		this.btnGetLock.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnGetLock.Size = new System.Drawing.Size(320, 53);
		this.btnGetLock.TabIndex = 9;
		this.btnGetLock.Text = "Get Lock Information";
		this.btnGetLock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGetLock.Click += new System.EventHandler(btnGetLock_Click);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(1008, 546);
		base.Controls.Add(this.splContainerData);
		base.Controls.Add(this.clsBackPanelLeft);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "frmLockLog";
		this.Text = "frmLockLog";
		base.Load += new System.EventHandler(frmLockLog_Load);
		this.splContainerData.Panel1.ResumeLayout(false);
		this.splContainerData.Panel1.PerformLayout();
		this.splContainerData.Panel2.ResumeLayout(false);
		this.splContainerData.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.clsBackPanel2.ResumeLayout(false);
		this.clsBackPanel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numUDTopNum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.clsBackPanelLeft.ResumeLayout(false);
		this.splitContainerLeft.Panel1.ResumeLayout(false);
		this.splitContainerLeft.Panel1.PerformLayout();
		this.splitContainerLeft.Panel2.ResumeLayout(false);
		this.splitContainerLeft.Panel2.PerformLayout();
		this.splitContainerLeft.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
