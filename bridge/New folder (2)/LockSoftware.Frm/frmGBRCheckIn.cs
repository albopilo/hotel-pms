using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmGBRCheckIn : Form
{
	public string m_objName = "WFbci";

	public Hashtable m_htab;

	private bool m_Init = true;

	private int m_defaultcerid = 1;

	private DataTable m_dtGuest = new DataTable();

	private byte[] m_enableImg;

	private byte[] m_disableImg;

	private IContainer components;

	private clsBackPanel clsBackPanel1;

	private ToolStrip toolStrip1;

	private ToolStripTextBox TSTxtBM;

	private ToolStripButton TSBtnSear;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripButton TSBtnClose;

	private ImageList imglist;

	private Panel panel5;

	private Panel panel7;

	private clsBackPanel cbpline01;

	private ListView lvRoom;

	private Panel panel1;

	private DataGridView dgvGuest;

	private DataGridView dgvList;

	private TableLayoutPanel tableLayoutPanel3;

	private TextBox txtDeposit;

	private Label label21;

	private DateTimePicker dtpCome;

	private NumericUpDown nudDay;

	private Label label28;

	private Label label22;

	private CheckBox chkSync;

	private TextBox txtRoomPrice;

	private Panel panel10;

	private Label label34;

	private Label label29;

	private DateTimePicker dtpLevel;

	private Label labDc;

	private Label label1;

	private Label label2;

	private Timer tSync;

	private TextBox txtPaid;

	private ComboBox cobCurrency;

	private GlassBtn btnCheckIn;

	private NumericUpDown txtDiscount;

	public frmGBRCheckIn()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		dtpCome.CustomFormat = (dtpLevel.CustomFormat = Program.m_currDateTimeFmt);
		m_enableImg = getImageByte(Program.m_AppPath + "\\image\\devEnable.gif");
		m_disableImg = getImageByte(Application.StartupPath + "\\image\\devDisable.gif");
		InitCurrency();
	}

	private byte[] getImageByte(string imagePath)
	{
		try
		{
			if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
			{
				FileStream fileStream = new FileStream(imagePath, FileMode.Open);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				return array;
			}
			return null;
		}
		catch
		{
			return null;
		}
	}

	private static byte[] getImageByte(int iStatus = 0)
	{
		try
		{
			Bitmap bitmap = iStatus switch
			{
				0 => new Bitmap(Resources.empty), 
				1 => new Bitmap(Resources.ArrowRight1), 
				2 => new Bitmap(Resources.SignalOK1), 
				3 => new Bitmap(Resources.FileDelete1), 
				4 => new Bitmap(Resources.idcard), 
				_ => new Bitmap(Resources.empty), 
			};
			using MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Bmp);
			memoryStream.Position = 0L;
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, Convert.ToInt32(memoryStream.Length));
			memoryStream.Flush();
			return array;
		}
		catch
		{
			return null;
		}
	}

	private void frmGBRCancel_Load(object sender, EventArgs e)
	{
		if (m_htab != null)
		{
			TSTxtBM.Text = (string)m_htab["txtBM"];
			TSTxtBM.ForeColor = Color.DarkGray;
			TSTxtBM.ToolTipText = (string)m_htab["txtBM-ttMsg"];
			TSBtnSear.Text = (string)m_htab["TSBtnSear"];
			btnCheckIn.Text = (string)m_htab["TSBtnCheckIn"];
			TSBtnClose.Text = (string)m_htab["TSBtnClose"];
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
			foreach (DataRowView item in cobCurrency.Items)
			{
				if ((bool)item.Row["curr_Basflag"])
				{
					cobCurrency.SelectedItem = item;
					break;
				}
			}
		}
		catch (Exception ex)
		{
			if (dataTable != null)
			{
				dataTable.Clear();
				dataTable.Dispose();
			}
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcolcurr_code"]);
		}
	}

	private void IniGuestHeader()
	{
		if (dgvGuest.Columns.Count > 0)
		{
			return;
		}
		try
		{
			string[] array = new string[11]
			{
				"TP_Name", "TP_Price", "R_Name", "g_name", "cer_id", "g_cernum", "cer_name", "g_GetNum", "g_MadeCard", "g_Operate",
				"r_Index"
			};
			string sql = "select cer_id, cer_name from D_Cer where cer_flag = 0 ";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			for (int i = 0; i < 6; i++)
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
				dataGridViewTextBoxColumn.HeaderText = (string)m_htab["dgv" + array[i]];
				dataGridViewTextBoxColumn.Name = "dgvg" + array[i];
				if (i < 3)
				{
					dataGridViewTextBoxColumn.ReadOnly = true;
				}
				else if (i == 4)
				{
					dataGridViewTextBoxColumn.Visible = false;
				}
				dgvGuest.Columns.Add(dataGridViewTextBoxColumn);
			}
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
			dataGridViewComboBoxColumn.HeaderText = (string)m_htab["dgv" + array[6]];
			dataGridViewComboBoxColumn.Name = "dgvg" + array[6];
			if (dataTable != null)
			{
				dataGridViewComboBoxColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
				dataGridViewComboBoxColumn.DataSource = dataTable.DefaultView;
				dataGridViewComboBoxColumn.DisplayMember = "cer_name";
				dataGridViewComboBoxColumn.ValueMember = "cer_id";
			}
			dgvGuest.Columns.Insert(5, dataGridViewComboBoxColumn);
			m_defaultcerid = int.Parse(dataTable.Rows[0]["cer_id"].ToString());
			DataGridViewImageColumn dataGridViewImageColumn = new DataGridViewImageColumn();
			dataGridViewImageColumn.HeaderText = (string)m_htab["dgv" + array[7]];
			dataGridViewImageColumn.Name = "dgvg" + array[7];
			dataGridViewImageColumn.Visible = Program.m_Lan == 1;
			dgvGuest.Columns.Add(dataGridViewImageColumn);
			DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
			dataGridViewCheckBoxColumn.HeaderText = (string)m_htab["dgv" + array[8]];
			dataGridViewCheckBoxColumn.Name = "dgvg" + array[8];
			dgvGuest.Columns.Add(dataGridViewCheckBoxColumn);
			DataGridViewImageColumn dataGridViewImageColumn2 = new DataGridViewImageColumn();
			dataGridViewImageColumn2.HeaderText = (string)m_htab["dgv" + array[9]];
			dataGridViewImageColumn2.ReadOnly = true;
			dataGridViewImageColumn2.Name = "dgvg" + array[9];
			dgvGuest.Columns.Add(dataGridViewImageColumn2);
			DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2.Name = "dgvgr_Index";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewTextBoxColumn2.Visible = false;
			dgvGuest.Columns.Add(dataGridViewTextBoxColumn2);
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
	}

	public DataTable createTable(DataTable DT)
	{
		DataTable dataTable = new DataTable();
		for (int i = 0; i < DT.Columns.Count; i++)
		{
			if (string.Equals(DT.Columns[i].ColumnName, "t_isTeam"))
			{
				DataColumn column = new DataColumn(DT.Columns[i].ColumnName, Type.GetType("System.Byte[]"));
				dataTable.Columns.Add(column);
			}
			else
			{
				DataColumn column2 = new DataColumn(DT.Columns[i].ColumnName, typeof(string));
				dataTable.Columns.Add(column2);
			}
		}
		foreach (DataRow row in DT.Rows)
		{
			DataRow dataRow2 = dataTable.NewRow();
			foreach (DataColumn column3 in DT.Columns)
			{
				string text = row[column3].ToString();
				if (string.Equals(column3.ColumnName, "t_isTeam"))
				{
					if (text.ToLower() == "true" && m_enableImg != null)
					{
						dataRow2[column3.ColumnName] = m_enableImg;
					}
					else
					{
						dataRow2[column3.ColumnName] = m_disableImg;
					}
				}
				else if (string.Equals(column3.ColumnName, "g_come_day") || string.Equals(column3.ColumnName, "g_level_day"))
				{
					dataRow2[column3.ColumnName] = Program.GetLocDate(DateTime.Parse(text));
				}
				else
				{
					dataRow2[column3.ColumnName] = text;
				}
			}
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}

	private void TSBtnSear_Click(object sender, EventArgs e)
	{
		try
		{
			m_Init = true;
			dgvList.DataSource = null;
			string text = "Select distinct case when g_teamid IS NULL then sch_name else g_name end sch_name, sch_mob, sch_tel, sch_email, case when g_teamid IS NULL then g_name else sch_name end t_name, Case when g_teamid IS null then sch_id else g_teamid end g_teamid, Cast(ISNULL(g_teamid, 0) As bit) g_isTeam, Cast(ISNULL(g_teamid, 0) As bit) t_isTeam, g_come_day, g_come_time, g_level_day, T.cer_id, D.cer_name, g_cernum from T_Schedule T, D_Cer D Where isnull(T.cer_id,1) = D.cer_id and T.sch_flag = 0 ";
			if (TSTxtBM.ForeColor == Color.Black && TSTxtBM.Text.Trim() != "")
			{
				string text2 = TSTxtBM.Text.Trim();
				string text3 = text;
				text = text3 + " And (sch_name like N'" + text2 + "%' or sch_mob  like N'" + text2 + "%' or g_name  like N'" + text2 + "%')";
			}
			text += "Order by g_come_day,g_come_time";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null)
			{
				if (dataTable.Rows.Count > 0)
				{
					dgvList.DataSource = createTable(dataTable).DefaultView;
					for (int i = 0; i < dgvList.Columns.Count; i++)
					{
						dgvList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvList.Columns[i].Name];
						dgvList.Columns[i].ReadOnly = true;
					}
					dgvList.AutoResizeColumns();
					DataGridViewColumn dataGridViewColumn = dgvList.Columns["g_teamid"];
					DataGridViewColumn dataGridViewColumn2 = dgvList.Columns["g_isTeam"];
					bool flag = (dgvList.Columns["cer_id"].Visible = false);
					bool visible = (dataGridViewColumn2.Visible = flag);
					dataGridViewColumn.Visible = visible;
					IniGuestHeader();
					LoadScheduleRoom();
				}
				else
				{
					if (sender != null)
					{
						Program.MsgCustom((string)m_htab["Info1"], MessageBoxIcon.Asterisk);
					}
					lvRoom.Items.Clear();
					dgvGuest.Rows.Clear();
				}
			}
			dgvList.AutoResizeColumns();
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
		m_Init = false;
	}

	private void LoadScheduleRoom()
	{
		lvRoom.Items.Clear();
		bool flag = bool.Parse(dgvList.SelectedRows[0].Cells["g_isTeam"].Value.ToString());
		string text = dgvList.SelectedRows[0].Cells["g_teamid"].Value.ToString();
		string format = "Select R_Name, R_ID, R_Code, R_SubCode, R_FloorID, R_TypeID, R_RSID, R_BedAdd, R_BedSinglePrice, R_Size, R_Memo, build_ID, Build_Name, Floor_Name, TP_Name, TP_BedCount, R_CurGuestCount, R_TotalGuest, R_TotalPrice,TP_Price,TP_deposit, RS_Name000, R_MaxCardNum,Build_Code,Floor_Code, R_SubCodeDai From v_HotelRooms Where (IsNull(R_flag,0) = 0) and (R_RSID <= 3 or R_RSID=10 or R_RSID=11)and R_ID in (select R_ID from T_Schedule where {0} )";
		format = ((!flag) ? string.Format(format, "sch_id = " + text) : string.Format(format, "g_teamid = " + text));
		format += " Order by R_TypeID, Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(format);
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			ListViewItem[] array = new ListViewItem[dataTable.Rows.Count];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string[] array2 = new string[dataTable.Columns.Count];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					array2[j] = dataTable.Rows[i][j].ToString().Trim();
				}
				array[i] = new ListViewItem(array2);
			}
			lvRoom.ItemChecked -= lvRoom_ItemChecked;
			lvRoom.Items.AddRange(array);
			lvRoom.ItemChecked += lvRoom_ItemChecked;
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				switch (Convert.ToInt16(dataTable.Rows[k]["R_RSID"].ToString()))
				{
				case 6:
					lvRoom.Items[k].ImageIndex = 5;
					break;
				case 11:
					lvRoom.Items[k].ImageIndex = 10;
					break;
				case 10:
					lvRoom.Items[k].ImageIndex = 9;
					break;
				case 1:
					lvRoom.Items[k].ImageIndex = 0;
					break;
				case 3:
					lvRoom.Items[k].ImageIndex = 2;
					break;
				default:
					lvRoom.Items[k].ImageIndex = Convert.ToInt16(dataTable.Rows[k]["R_RSID"]) - 1;
					break;
				}
			}
		}
		txtRoomPrice.Text = 0.ToString("F2");
		txtDeposit.Text = 0.ToString("F2");
		dgvGuest.Rows.Clear();
		LoadScheduleInfo();
		if (lvRoom.Items.Count > 0)
		{
			lvRoom.Items[0].Checked = true;
		}
	}

	private void IniRoomGuest()
	{
		dgvGuest.Rows.Clear();
		double num = 0.0;
		double num2 = 0.0;
		if (bool.Parse(dgvList.SelectedRows[0].Cells["g_isTeam"].Value.ToString()))
		{
			int num3 = 0;
			foreach (ListViewItem item in lvRoom.Items)
			{
				if (item.Checked)
				{
					int num4 = int.Parse(item.SubItems[15].Text.Trim());
					dgvGuest.Rows.Add(num4);
					for (int i = 0; i < num4; i++)
					{
						dgvGuest.Rows[num3 + i].Cells[0].Value = item.SubItems[14].Text.Trim();
						dgvGuest.Rows[num3 + i].Cells[1].Value = item.SubItems[19].Text.Trim();
						dgvGuest.Rows[num3 + i].Cells[2].Value = item.SubItems[0].Text.Trim();
						dgvGuest.Rows[num3 + i].Cells[3].Value = "";
						dgvGuest.Rows[num3 + i].Cells[4].Value = m_defaultcerid;
						dgvGuest.Rows[num3 + i].Cells["dgvgcer_name"].Value = m_defaultcerid;
						dgvGuest.Rows[num3 + i].Cells[6].Value = "";
						dgvGuest.Rows[num3 + i].Cells[7].Value = getImageByte(4);
						dgvGuest.Rows[num3 + i].Cells[8].Value = i == 0;
						dgvGuest.Rows[num3 + i].Cells[9].Value = getImageByte();
						dgvGuest.Rows[num3 + i].Cells[10].Value = item.Index;
					}
					num3 += num4;
					num += double.Parse(item.SubItems[19].Text.Trim());
					num2 += double.Parse(item.SubItems[20].Text.Trim());
				}
			}
		}
		else
		{
			int num5 = int.Parse(lvRoom.Items[0].SubItems[15].Text.Trim());
			dgvGuest.Rows.Add(num5);
			for (int j = 0; j < num5; j++)
			{
				dgvGuest.Rows[j].Cells[0].Value = lvRoom.Items[0].SubItems[14].Text.Trim();
				dgvGuest.Rows[j].Cells[1].Value = lvRoom.Items[0].SubItems[19].Text.Trim();
				dgvGuest.Rows[j].Cells[2].Value = lvRoom.Items[0].SubItems[0].Text.Trim();
				if (j == 0)
				{
					dgvGuest.Rows[j].Cells[3].Value = dgvList.SelectedRows[0].Cells["t_name"].Value.ToString();
					string text = dgvList.SelectedRows[0].Cells["cer_id"].Value.ToString();
					dgvGuest.Rows[j].Cells[4].Value = text;
					if (!string.IsNullOrEmpty(text))
					{
						dgvGuest.Rows[j].Cells["dgvgcer_name"].Value = int.Parse(text);
					}
					else
					{
						dgvGuest.Rows[j].Cells["dgvgcer_name"].Value = m_defaultcerid;
					}
					dgvGuest.Rows[j].Cells[6].Value = dgvList.SelectedRows[0].Cells["g_cernum"].Value.ToString();
					dgvGuest.Rows[j].Cells[8].Value = true;
				}
				else
				{
					dgvGuest.Rows[j].Cells[3].Value = "";
					dgvGuest.Rows[j].Cells[4].Value = m_defaultcerid;
					dgvGuest.Rows[j].Cells["dgvgcer_name"].Value = m_defaultcerid;
					dgvGuest.Rows[j].Cells[6].Value = "";
					dgvGuest.Rows[j].Cells[8].Value = false;
				}
				dgvGuest.Rows[j].Cells[7].Value = getImageByte(4);
				dgvGuest.Rows[j].Cells[9].Value = getImageByte();
				dgvGuest.Rows[j].Cells[10].Value = 0;
			}
			num = double.Parse(lvRoom.Items[0].SubItems[19].Text.Trim());
			num2 = double.Parse(lvRoom.Items[0].SubItems[20].Text.Trim());
		}
		dgvGuest.AutoResizeColumns();
		txtRoomPrice.Text = num.ToString("F2");
		txtDeposit.Text = num2.ToString("F2");
	}

	private void AddRoomGuest(ListViewItem item)
	{
		double num = 0.0;
		double num2 = 0.0;
		if (!string.IsNullOrEmpty(txtRoomPrice.Text))
		{
			num = double.Parse(txtRoomPrice.Text);
		}
		if (!string.IsNullOrEmpty(txtDeposit.Text))
		{
			num2 = double.Parse(txtDeposit.Text);
		}
		int count = dgvGuest.Rows.Count;
		if (bool.Parse(dgvList.SelectedRows[0].Cells["g_isTeam"].Value.ToString()))
		{
			int num3 = int.Parse(item.SubItems[15].Text.Trim());
			dgvGuest.Rows.Add(num3);
			for (int i = 0; i < num3; i++)
			{
				dgvGuest.Rows[count + i].Cells[0].Value = item.SubItems[14].Text.Trim();
				dgvGuest.Rows[count + i].Cells[1].Value = item.SubItems[19].Text.Trim();
				dgvGuest.Rows[count + i].Cells[2].Value = item.SubItems[0].Text.Trim();
				dgvGuest.Rows[count + i].Cells[3].Value = "";
				dgvGuest.Rows[count + i].Cells[4].Value = m_defaultcerid;
				dgvGuest.Rows[count + i].Cells["dgvgcer_name"].Value = m_defaultcerid;
				dgvGuest.Rows[count + i].Cells[6].Value = "";
				dgvGuest.Rows[count + i].Cells[7].Value = getImageByte(4);
				dgvGuest.Rows[count + i].Cells[8].Value = i == 0;
				dgvGuest.Rows[count + i].Cells[9].Value = getImageByte();
				dgvGuest.Rows[count + i].Cells[10].Value = item.Index;
			}
			num += double.Parse(item.SubItems[19].Text.Trim());
			num2 += double.Parse(item.SubItems[20].Text.Trim());
		}
		else
		{
			int num4 = int.Parse(item.SubItems[15].Text.Trim());
			dgvGuest.Rows.Add(num4);
			for (int j = 0; j < num4; j++)
			{
				dgvGuest.Rows[count + j].Cells[0].Value = item.SubItems[14].Text.Trim();
				dgvGuest.Rows[count + j].Cells[1].Value = item.SubItems[19].Text.Trim();
				dgvGuest.Rows[count + j].Cells[2].Value = item.SubItems[0].Text.Trim();
				if (j == 0)
				{
					dgvGuest.Rows[count + j].Cells[3].Value = dgvList.SelectedRows[0].Cells["t_name"].Value.ToString();
					string text = dgvList.SelectedRows[0].Cells["cer_id"].Value.ToString();
					dgvGuest.Rows[count + j].Cells[4].Value = text;
					if (!string.IsNullOrEmpty(text))
					{
						dgvGuest.Rows[count + j].Cells["dgvgcer_name"].Value = int.Parse(text);
					}
					else
					{
						dgvGuest.Rows[count + j].Cells["dgvgcer_name"].Value = m_defaultcerid;
					}
					dgvGuest.Rows[count + j].Cells[6].Value = dgvList.SelectedRows[0].Cells["g_cernum"].Value.ToString();
					dgvGuest.Rows[count + j].Cells[8].Value = true;
				}
				else
				{
					dgvGuest.Rows[count + j].Cells[3].Value = "";
					dgvGuest.Rows[count + j].Cells[4].Value = m_defaultcerid;
					dgvGuest.Rows[count + j].Cells["dgvgcer_name"].Value = m_defaultcerid;
					dgvGuest.Rows[count + j].Cells[6].Value = "";
					dgvGuest.Rows[count + j].Cells[8].Value = false;
				}
				dgvGuest.Rows[count + j].Cells[7].Value = getImageByte(4);
				dgvGuest.Rows[count + j].Cells[9].Value = getImageByte();
				dgvGuest.Rows[count + j].Cells[10].Value = item.Index;
			}
			num = double.Parse(item.SubItems[19].Text.Trim());
			num2 = double.Parse(item.SubItems[20].Text.Trim());
		}
		txtRoomPrice.Text = num.ToString("F2");
		txtDeposit.Text = num2.ToString("F2");
	}

	private void DelRoomGuest(ListViewItem item)
	{
		double num = double.Parse(txtRoomPrice.Text) - double.Parse(item.SubItems[19].Text.Trim());
		double num2 = double.Parse(txtDeposit.Text) - double.Parse(item.SubItems[20].Text.Trim());
		string text = item.SubItems[0].Text.Trim();
		bool flag = false;
		bool flag2 = false;
		int num3 = 0;
		while (!flag2 && num3 < dgvGuest.Rows.Count)
		{
			if (text == dgvGuest.Rows[num3].Cells[2].Value.ToString())
			{
				flag = true;
				dgvGuest.Rows.RemoveAt(num3);
				continue;
			}
			if (flag)
			{
				flag2 = true;
			}
			num3++;
		}
		txtRoomPrice.Text = num.ToString("F2");
		txtDeposit.Text = num2.ToString("F2");
	}

	private void LoadScheduleInfo()
	{
		dtpCome.Value = DateTime.Now;
		DateTime dateTime = DateTime.Parse(dgvList.SelectedRows[0].Cells["g_level_day"].Value.ToString() + " " + Program.m_defLeaveTime);
		if (dtpCome.Value < dateTime)
		{
			int num = (int)Program.CountDay(dtpCome.Value, dateTime);
			if ((decimal)num > nudDay.Maximum)
			{
				dtpLevel.Value = DateTime.Now.AddDays((double)nudDay.Maximum);
				nudDay.Value = nudDay.Maximum;
			}
			else
			{
				dtpLevel.Value = dateTime;
				nudDay.Value = num;
			}
		}
		else
		{
			dtpLevel.Value = Program.GetleaveTime(dtpCome.Value, 1);
			nudDay.Value = 1m;
		}
		txtDiscount.Value = decimal.Parse(Program.GetFaceDisValue());
		SetRoomTotal();
	}

	private void SetRoomTotal()
	{
		try
		{
			double num = Convert.ToDouble(txtRoomPrice.Text);
			double num2 = Convert.ToDouble(txtDeposit.Text);
			double num3 = Convert.ToDouble(cobCurrency.SelectedValue);
			if (num3 == 0.0)
			{
				num3 = 1.0;
			}
			double realDisValue = Program.GetRealDisValue(txtDiscount.Value.ToString());
			txtPaid.Tag = num * Convert.ToDouble(nudDay.Value) * realDisValue + num2;
			txtPaid.Text = (double.Parse(txtPaid.Tag.ToString()) / num3).ToString("F2");
		}
		catch
		{
		}
	}

	private void btnCheckIn_Click(object sender, EventArgs e)
	{
		int num = 0;
		int num2 = 4;
		string text = "NULL";
		bool flag = bool.Parse(dgvList.SelectedRows[0].Cells["g_isTeam"].Value.ToString());
		if (flag)
		{
			num2 = 6;
			text = dgvList.SelectedRows[0].Cells["g_teamid"].Value.ToString();
		}
		int num3 = Convert.ToInt32(nudDay.Value);
		if (num3 == 0)
		{
			num3 = 1;
		}
		double num4 = Convert.ToDouble(cobCurrency.SelectedValue);
		double realDisValue = Program.GetRealDisValue(txtDiscount.Value.ToString());
		string standDTime = Program.GetStandDTime(dtpCome.Value, "00");
		string standDTime2 = Program.GetStandDTime(dtpLevel.Value.Date + TimeSpan.Parse(Program.m_defLeaveTime));
		string datetime = dtpLevel.Value.ToString("yyyyMMddHHmm");
		string text2 = string.Empty;
		int num5 = 0;
		int num6 = 0;
		try
		{
			bool flag2 = false;
			for (int i = 0; i < lvRoom.CheckedItems.Count; i++)
			{
				if (flag2)
				{
					break;
				}
				ListViewItem listViewItem = lvRoom.Items[int.Parse(dgvGuest.Rows[num].Cells[10].Value.ToString())];
				int num7 = int.Parse(listViewItem.SubItems[15].Text.Trim());
				int num8 = int.Parse(listViewItem.SubItems[1].Text.Trim());
				int num9 = Convert.ToInt32(listViewItem.SubItems[23].Text.Trim());
				int num10 = Convert.ToInt32(listViewItem.SubItems[24].Text.Trim());
				int num11 = Convert.ToInt32(listViewItem.SubItems[2].Text.Trim());
				int num12 = Convert.ToInt32(listViewItem.SubItems[3].Text.Trim());
				int num13 = Convert.ToInt32(listViewItem.SubItems[25].Text.Trim());
				string text3 = listViewItem.SubItems[0].Text.Trim();
				double num14 = double.Parse(listViewItem.SubItems[19].Text.Trim());
				double num15 = double.Parse(listViewItem.SubItems[20].Text.Trim());
				string text4 = Program.changeValue(num14 * Convert.ToDouble(nudDay.Value) * realDisValue, CultureInfo.InvariantCulture);
				string s = Program.changeValue(Program.changeValue(text4, CultureInfo.InvariantCulture) + num15, CultureInfo.InvariantCulture);
				Program.changeValue(Program.changeValue(s, CultureInfo.InvariantCulture) / num4, CultureInfo.InvariantCulture);
				string standDec = Program.GetStandDec(double.Parse(txtPaid.Text));
				int cardnum = 0;
				int num16 = 0;
				int num17 = 0;
				string text5 = "";
				num13 += 2;
				for (int j = 0; j < num7; j++)
				{
					string value = ((dgvGuest.Rows[num + j].Cells[3].Value == null) ? "" : dgvGuest.Rows[num + j].Cells[3].Value.ToString());
					string value2 = ((dgvGuest.Rows[num + j].Cells[6].Value == null) ? "" : dgvGuest.Rows[num + j].Cells[6].Value.ToString());
					if (string.IsNullOrEmpty(value))
					{
						if (j == 0)
						{
							Program.isValNull(dgvGuest.Columns[3].HeaderText, "", chk: true);
							flag2 = true;
							break;
						}
						continue;
					}
					if (string.IsNullOrEmpty(value2))
					{
						if (j == 0)
						{
							Program.isValNull(dgvGuest.Columns[6].HeaderText, "", chk: true);
							flag2 = true;
							break;
						}
						continue;
					}
					dgvGuest.Rows[num + j].Cells[9].Value = getImageByte(1);
					cardnum = Program.getMaxNumber(1, showError: true);
					if (cardnum < 0)
					{
						return;
					}
					cardnum++;
					int num18 = 0;
					if (bool.Parse(dgvGuest.Rows[num + j].Cells[8].Value.ToString()))
					{
						string msg = (string)m_htab["dgvR_Name"] + ":" + text3 + "\n" + (string)m_htab["dgvTP_Name"] + ":" + dgvGuest.Rows[num + j].Cells[0].Value.ToString() + "\n" + (string)m_htab["dgvg_name"] + ":" + dgvGuest.Rows[num + j].Cells[3].Value.ToString() + "\n\n" + (string)m_htab["Info02"];
						if (Program.MsgBox(msg, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						{
							num17++;
							string text6 = num9.ToString("X2") + num10.ToString("X2") + num11.ToString("X2") + num12.ToString("X2") + ((byte)num13).ToString("X2");
							if (Program.RadioWriteCard(6, cardnum, datetime, text6, text6.Length, Buzzer: true) != 0)
							{
								return;
							}
							num18 = 1;
						}
					}
					if (num16 == 0)
					{
						text5 = "@_ID";
						text2 = "declare @_ID As bigint \n Insert Into T_Rooms Values('',0,0,0," + num8.ToString() + "," + num2.ToString() + ",N'" + text3 + "','" + num11.ToString() + "'," + num12.ToString() + "," + Program.GetStandDec(num14.ToString("F2")) + "," + Program.GetStandDec(realDisValue) + "," + (flag ? "0" : standDec) + ",'" + standDTime + "'," + num3 + ",'" + standDTime2 + "',0, NULL, 0, 0, 0, 0, 0, NULL," + Program.GetStandDec(num15) + ",0,''," + Program.m_baseCurrID + ",N'" + Program.m_baseCurrCode + "'," + Program.GetStandDec(Program.m_baseCurrRate) + ",N'" + cobCurrency.Text.Trim() + "'," + Program.GetStandDec(num4) + ", 0, 0, 0,N'" + btnCheckIn.Text + "',1,NULL," + text + ",GetDate()," + Program.m_opid + ",N'" + Program.m_OperName + "', NULL, NULL, NULL) \n ";
						text2 += "Select @_ID = @@Identity \n ";
					}
					object obj = text2;
					text2 = string.Concat(obj, "Insert Into T_Guest Values(N'", dgvGuest.Rows[num + j].Cells[3].Value.ToString(), "',2,", dgvGuest.Rows[num + j].Cells[4].Value, ",N'", dgvGuest.Rows[num + j].Cells[6].Value.ToString(), "','', ", text5, ",", num8.ToString(), ",'", num9.ToString(), "','", num10.ToString(), "','", num11.ToString(), "',", num12.ToString(), ",", num13.ToString(), ",", cardnum.ToString(), ",N'", text3, "',0,", Program.GetStandDec(num14.ToString("F2")), ",", Program.GetStandDec(realDisValue), ",", Program.GetStandDec(num14 * realDisValue), ",", flag ? "0" : standDec, ",'", standDTime, "',0,'", standDTime2, "',0,NULL,NULL,", num3.ToString(), ",0,0,0,NULL,0,0,'',0,0,0,NULL,0,NULL,", text, ",0,NULL,1,convert(nvarchar(max),", text5, "),0,NULL,0,NULL,", num18.ToString(), ",GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "', NULL, NULL, NULL,NULL, ", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(),0) \n ");
					num6 = Program.DBCompExec(text2, btnCheckIn.Text.Trim());
					if (num6 < 0)
					{
						Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					if (num16 == 0)
					{
						text2 = "select max(TR_ID) TR_ID  from T_Rooms where r_id = " + num8;
						DataTable dataTable = SQLserver.Data_GetDataTable(text2);
						if (dataTable != null && dataTable.Rows.Count > 0)
						{
							text5 = dataTable.Rows[0]["TR_ID"].ToString();
						}
					}
					num16++;
					dgvGuest.Rows[num + j].Cells[9].Value = getImageByte(2);
					text2 = string.Empty;
				}
				if (num16 > 0)
				{
					num5++;
					string text7 = text2;
					text2 = text7 + "Update T_Rooms Set TR_guestcount = TR_guestcount + " + num16 + ",TR_cardcount = TR_cardcount + " + num17 + " Where TR_ID = " + text5 + " \n";
					object obj2 = text2;
					text2 = string.Concat(obj2, "Update D_Rooms Set R_RSID=", num2.ToString(), ", R_CurGuestCount=", num16, ",R_MaxCardNum=", cardnum.ToString(), ", R_SubCodeDai= ", num13.ToString(), ", R_TotalGuest=IsNull(R_TotalGuest,0) + ", num16, ", R_TotalPrice=Isnull(R_TotalPrice,0) + ", text4, ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", num8.ToString());
					string text8 = text2;
					text2 = text8 + " \n Update T_Schedule Set sch_flag = 1, sch_memo = N'" + btnCheckIn.Text + "' Where sch_flag=0 And r_id=" + num8;
					num6 = Program.DBCompExec(text2, btnCheckIn.Text.Trim());
					if (num6 < 0)
					{
						Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				num += num7;
			}
			if (flag)
			{
				int num19 = 1;
				if (lvRoom.Items.Count == num5)
				{
					num19 = 0;
				}
				text2 = "Update T_Team Set Team_perCount = (select count(1) from T_Guest where g_teamid = " + text + "),Team_cometime = '" + standDTime + "',Team_stayHour = " + num3 + ",Team_stand_L_time = '" + standDTime2 + "',Team_roomPrice = Team_roomPrice + " + Program.GetStandDec(txtRoomPrice.Text.Trim()) + ",Team_deposit = Team_deposit + " + Program.GetStandDec(txtDeposit.Text.Trim()) + ",Team_totalprice = Team_totalprice + " + Program.GetStandDec(txtPaid.Tag.ToString()) + ",Team_discount = " + Program.GetStandDec(realDisValue) + ",Team_totalpaid = Team_totalpaid + " + Program.GetStandDec(txtPaid.Text.Trim()) + ",team_sch = " + num19 + ",updatetime = GetDate(),updator_id = " + Program.m_opid + ",updator = N'" + Program.m_OperName + "' Where team_id = " + text;
				num6 = Program.DBCompExec(text2, btnCheckIn.Text.Trim());
				if (num6 < 0)
				{
					Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			if (Program.fm != null)
			{
				Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
			}
			TSBtnSear_Click(null, null);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TSBtnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TSTxtBM_Enter(object sender, EventArgs e)
	{
		if (TSTxtBM.ForeColor == Color.DarkGray)
		{
			TSTxtBM.Text = "";
			TSTxtBM.ForeColor = Color.Black;
		}
	}

	private void TSTxtBM_Leave(object sender, EventArgs e)
	{
		if (TSTxtBM.Text.Trim() == "" || TSTxtBM.ForeColor == Color.DarkGray)
		{
			TSTxtBM.Text = (string)m_htab["txtBM"];
			TSTxtBM.ForeColor = Color.DarkGray;
		}
	}

	private void TSTxtBM_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TSBtnSear_Click(null, null);
		}
	}

	private void dgvList_SelectionChanged(object sender, EventArgs e)
	{
		if (!m_Init && dgvList.SelectedRows.Count > 0)
		{
			LoadScheduleRoom();
		}
	}

	private void dgvGuest_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex == 7)
		{
			Program.IDCardData CardMsg = default(Program.IDCardData);
			if (Program.Get_IDCardII_Information(ref CardMsg) >= 0)
			{
				dgvGuest.Rows[e.RowIndex].Cells["g_cernum"].Value = CardMsg.IDCardNo;
				dgvGuest.Rows[e.RowIndex].Cells["g_name"].Value = CardMsg.Name.Trim();
			}
		}
	}

	private void dgvGuest_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
	{
		if (e.ColumnIndex >= 3 || e.RowIndex < 0)
		{
			return;
		}
		using Brush brush = new SolidBrush(dgvGuest.GridColor);
		using Brush brush2 = new SolidBrush(e.CellStyle.BackColor);
		using Pen pen = new Pen(brush);
		e.Graphics.FillRectangle(brush2, e.CellBounds);
		if (e.RowIndex == dgvGuest.Rows.Count - 1 || dgvGuest.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
		{
			e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
		}
		e.Graphics.DrawLine(pen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
		if (e.Value != null && (e.RowIndex == 0 || dgvGuest.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString()))
		{
			e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, Brushes.Crimson, e.CellBounds.X + 2, e.CellBounds.Y + 2, StringFormat.GenericDefault);
		}
		e.Handled = true;
	}

	private void chkSync_CheckedChanged(object sender, EventArgs e)
	{
		tSync.Enabled = chkSync.Checked;
	}

	private void tSync_Tick(object sender, EventArgs e)
	{
		if (chkSync.Checked && dtpCome.Enabled)
		{
			dtpCome.Value = DateTime.Now;
		}
	}

	private void dtpCome_ValueChanged(object sender, EventArgs e)
	{
		if (m_Init)
		{
			return;
		}
		m_Init = true;
		try
		{
			double num = Convert.ToDouble(nudDay.Value);
			if (dtpCome.Value.ToString("HH:mm:ss").CompareTo(Program.m_defComeTime) < 0)
			{
				num--;
			}
			dtpLevel.Value = dtpCome.Value.AddDays(num);
			SetRoomTotal();
		}
		catch
		{
		}
		finally
		{
			m_Init = false;
		}
	}

	private void txtDiscount_TextChanged(object sender, EventArgs e)
	{
		if (m_Init)
		{
			return;
		}
		m_Init = true;
		try
		{
			SetRoomTotal();
		}
		catch
		{
		}
		finally
		{
			m_Init = false;
		}
	}

	private void lvRoom_ItemChecked(object sender, ItemCheckedEventArgs e)
	{
		try
		{
			if (e.Item != null)
			{
				if (e.Item.Checked)
				{
					AddRoomGuest(e.Item);
				}
				else
				{
					DelRoomGuest(e.Item);
				}
				SetRoomTotal();
			}
		}
		catch
		{
		}
	}

	private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\b' && (e.KeyChar < '0' || e.KeyChar > '9'))
		{
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmGBRCheckIn));
		this.imglist = new System.Windows.Forms.ImageList(this.components);
		this.panel5 = new System.Windows.Forms.Panel();
		this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
		this.nudDay = new System.Windows.Forms.NumericUpDown();
		this.label28 = new System.Windows.Forms.Label();
		this.chkSync = new System.Windows.Forms.CheckBox();
		this.label29 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.txtRoomPrice = new System.Windows.Forms.TextBox();
		this.txtDeposit = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.labDc = new System.Windows.Forms.Label();
		this.panel10 = new System.Windows.Forms.Panel();
		this.txtDiscount = new System.Windows.Forms.NumericUpDown();
		this.label34 = new System.Windows.Forms.Label();
		this.dtpLevel = new System.Windows.Forms.DateTimePicker();
		this.dtpCome = new System.Windows.Forms.DateTimePicker();
		this.txtPaid = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cobCurrency = new System.Windows.Forms.ComboBox();
		this.btnCheckIn = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.cbpline01 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.lvRoom = new System.Windows.Forms.ListView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.dgvGuest = new System.Windows.Forms.DataGridView();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.tSync = new System.Windows.Forms.Timer(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.TSTxtBM = new System.Windows.Forms.ToolStripTextBox();
		this.TSBtnSear = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.TSBtnClose = new System.Windows.Forms.ToolStripButton();
		this.panel5.SuspendLayout();
		this.tableLayoutPanel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).BeginInit();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvGuest).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.clsBackPanel1.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.imglist.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imglist.ImageStream");
		this.imglist.TransparentColor = System.Drawing.Color.Transparent;
		this.imglist.Images.SetKeyName(0, "05(1).png");
		this.imglist.Images.SetKeyName(1, "trashcan_full.png");
		this.imglist.Images.SetKeyName(2, "SyncTime (wormhole).ico");
		this.imglist.Images.SetKeyName(3, "120px-Vista-Login_Manager.png");
		this.imglist.Images.SetKeyName(4, "120px-Vista-Login_Manager.png");
		this.imglist.Images.SetKeyName(5, "35(1).png");
		this.imglist.Images.SetKeyName(6, "Pic_07.png");
		this.imglist.Images.SetKeyName(7, "bgSys.png");
		this.imglist.Images.SetKeyName(8, "v_stop.png");
		this.imglist.Images.SetKeyName(9, "Icon-1.png");
		this.imglist.Images.SetKeyName(10, "Icon-2.png");
		this.panel5.AutoScroll = true;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.tableLayoutPanel3);
		this.panel5.Controls.Add(this.panel7);
		this.panel5.Controls.Add(this.cbpline01);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 595);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(1010, 71);
		this.panel5.TabIndex = 21;
		this.tableLayoutPanel3.ColumnCount = 9;
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111f));
		this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel3.Controls.Add(this.nudDay, 3, 0);
		this.tableLayoutPanel3.Controls.Add(this.label28, 2, 0);
		this.tableLayoutPanel3.Controls.Add(this.chkSync, 0, 0);
		this.tableLayoutPanel3.Controls.Add(this.label29, 0, 1);
		this.tableLayoutPanel3.Controls.Add(this.label21, 4, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtRoomPrice, 5, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtDeposit, 5, 1);
		this.tableLayoutPanel3.Controls.Add(this.label22, 4, 1);
		this.tableLayoutPanel3.Controls.Add(this.labDc, 2, 1);
		this.tableLayoutPanel3.Controls.Add(this.panel10, 3, 1);
		this.tableLayoutPanel3.Controls.Add(this.dtpLevel, 1, 1);
		this.tableLayoutPanel3.Controls.Add(this.dtpCome, 1, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtPaid, 7, 0);
		this.tableLayoutPanel3.Controls.Add(this.label2, 6, 0);
		this.tableLayoutPanel3.Controls.Add(this.label1, 6, 1);
		this.tableLayoutPanel3.Controls.Add(this.cobCurrency, 7, 1);
		this.tableLayoutPanel3.Controls.Add(this.btnCheckIn, 8, 0);
		this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.tableLayoutPanel3.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 11);
		this.tableLayoutPanel3.Name = "tableLayoutPanel3";
		this.tableLayoutPanel3.RowCount = 6;
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel3.Size = new System.Drawing.Size(1008, 58);
		this.tableLayoutPanel3.TabIndex = 55;
		this.nudDay.Location = new System.Drawing.Point(316, 3);
		this.nudDay.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudDay.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.Name = "nudDay";
		this.nudDay.Size = new System.Drawing.Size(62, 21);
		this.nudDay.TabIndex = 3;
		this.nudDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.nudDay.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudDay.ValueChanged += new System.EventHandler(dtpCome_ValueChanged);
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(254, 0);
		this.label28.Name = "label28";
		this.label28.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label28.Size = new System.Drawing.Size(56, 20);
		this.label28.TabIndex = 41;
		this.label28.Text = "Stay Day:";
		this.chkSync.AutoSize = true;
		this.chkSync.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkSync.Location = new System.Drawing.Point(3, 5);
		this.chkSync.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.chkSync.Name = "chkSync";
		this.chkSync.Size = new System.Drawing.Size(105, 18);
		this.chkSync.TabIndex = 52;
		this.chkSync.Text = "System Time";
		this.chkSync.UseVisualStyleBackColor = true;
		this.chkSync.CheckedChanged += new System.EventHandler(chkSync_CheckedChanged);
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(3, 27);
		this.label29.Name = "label29";
		this.label29.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label29.Size = new System.Drawing.Size(64, 20);
		this.label29.TabIndex = 55;
		this.label29.Text = "Leave Date:";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(398, 0);
		this.label21.Name = "label21";
		this.label21.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label21.Size = new System.Drawing.Size(66, 20);
		this.label21.TabIndex = 45;
		this.label21.Text = "Room Price:";
		this.txtRoomPrice.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtRoomPrice.Location = new System.Drawing.Point(470, 3);
		this.txtRoomPrice.Name = "txtRoomPrice";
		this.txtRoomPrice.ReadOnly = true;
		this.txtRoomPrice.Size = new System.Drawing.Size(81, 21);
		this.txtRoomPrice.TabIndex = 9;
		this.txtDeposit.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtDeposit.Location = new System.Drawing.Point(470, 30);
		this.txtDeposit.Name = "txtDeposit";
		this.txtDeposit.ReadOnly = true;
		this.txtDeposit.Size = new System.Drawing.Size(81, 21);
		this.txtDeposit.TabIndex = 10;
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(398, 27);
		this.label22.Name = "label22";
		this.label22.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label22.Size = new System.Drawing.Size(49, 20);
		this.label22.TabIndex = 47;
		this.label22.Text = "Deposit:";
		this.labDc.AutoSize = true;
		this.labDc.Location = new System.Drawing.Point(254, 27);
		this.labDc.Name = "labDc";
		this.labDc.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.labDc.Size = new System.Drawing.Size(54, 20);
		this.labDc.TabIndex = 57;
		this.labDc.Text = "Discount:";
		this.panel10.Controls.Add(this.txtDiscount);
		this.panel10.Controls.Add(this.label34);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel10.Location = new System.Drawing.Point(313, 27);
		this.panel10.Margin = new System.Windows.Forms.Padding(0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(82, 29);
		this.panel10.TabIndex = 54;
		this.txtDiscount.Location = new System.Drawing.Point(3, 3);
		this.txtDiscount.Margin = new System.Windows.Forms.Padding(0);
		this.txtDiscount.Name = "txtDiscount";
		this.txtDiscount.Size = new System.Drawing.Size(42, 21);
		this.txtDiscount.TabIndex = 49;
		this.txtDiscount.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.txtDiscount.ValueChanged += new System.EventHandler(txtDiscount_TextChanged);
		this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiscount_KeyPress);
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label34.ForeColor = System.Drawing.Color.Red;
		this.label34.Location = new System.Drawing.Point(58, 7);
		this.label34.Margin = new System.Windows.Forms.Padding(3, 12, 0, 0);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(21, 14);
		this.label34.TabIndex = 48;
		this.label34.Text = "%";
		this.dtpLevel.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpLevel.Enabled = false;
		this.dtpLevel.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevel.Location = new System.Drawing.Point(114, 30);
		this.dtpLevel.Name = "dtpLevel";
		this.dtpLevel.Size = new System.Drawing.Size(120, 21);
		this.dtpLevel.TabIndex = 56;
		this.dtpCome.CustomFormat = "yyyy-MM-dd HH:mm";
		this.dtpCome.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCome.Location = new System.Drawing.Point(114, 3);
		this.dtpCome.Name = "dtpCome";
		this.dtpCome.Size = new System.Drawing.Size(120, 21);
		this.dtpCome.TabIndex = 1;
		this.dtpCome.ValueChanged += new System.EventHandler(dtpCome_ValueChanged);
		this.txtPaid.Location = new System.Drawing.Point(632, 3);
		this.txtPaid.Name = "txtPaid";
		this.txtPaid.Size = new System.Drawing.Size(91, 21);
		this.txtPaid.TabIndex = 66;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(571, 0);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label2.Size = new System.Drawing.Size(31, 20);
		this.label2.TabIndex = 60;
		this.label2.Text = "Paid:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(571, 27);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label1.Size = new System.Drawing.Size(55, 20);
		this.label1.TabIndex = 59;
		this.label1.Text = "Currency:";
		this.cobCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCurrency.FormattingEnabled = true;
		this.cobCurrency.Location = new System.Drawing.Point(632, 30);
		this.cobCurrency.Name = "cobCurrency";
		this.cobCurrency.Size = new System.Drawing.Size(91, 23);
		this.cobCurrency.TabIndex = 65;
		this.cobCurrency.SelectedValueChanged += new System.EventHandler(txtDiscount_TextChanged);
		this.btnCheckIn.BackColor = System.Drawing.Color.LightGray;
		this.btnCheckIn.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCheckIn.ForeColor = System.Drawing.Color.Black;
		this.btnCheckIn.GlowColor = System.Drawing.Color.White;
		this.btnCheckIn.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCheckIn.Image = LockSoftware.Properties.Resources.UserGroup;
		this.btnCheckIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCheckIn.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCheckIn.Location = new System.Drawing.Point(743, 3);
		this.btnCheckIn.Name = "btnCheckIn";
		this.btnCheckIn.OuterBorderColor = System.Drawing.Color.LightGray;
		this.tableLayoutPanel3.SetRowSpan(this.btnCheckIn, 2);
		this.btnCheckIn.Size = new System.Drawing.Size(182, 50);
		this.btnCheckIn.TabIndex = 64;
		this.btnCheckIn.Text = "Guest Check In";
		this.btnCheckIn.Click += new System.EventHandler(btnCheckIn_Click);
		this.panel7.AutoSize = true;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 69);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(1008, 0);
		this.panel7.TabIndex = 54;
		this.cbpline01.Border = false;
		this.cbpline01.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderBW = 1;
		this.cbpline01.BorderColorBottom = System.Drawing.Color.Gray;
		this.cbpline01.BorderColorLeft = System.Drawing.Color.Gray;
		this.cbpline01.BorderColorRight = System.Drawing.Color.Gray;
		this.cbpline01.BorderColorTop = System.Drawing.Color.Gray;
		this.cbpline01.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderLW = 1;
		this.cbpline01.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderRW = 1;
		this.cbpline01.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.cbpline01.BorderTW = 1;
		this.cbpline01.Color1 = System.Drawing.Color.WhiteSmoke;
		this.cbpline01.Color2 = System.Drawing.Color.Black;
		this.cbpline01.ColorAngle = 135f;
		this.cbpline01.Dock = System.Windows.Forms.DockStyle.Top;
		this.cbpline01.Location = new System.Drawing.Point(0, 0);
		this.cbpline01.Name = "cbpline01";
		this.cbpline01.Size = new System.Drawing.Size(1008, 1);
		this.cbpline01.TabIndex = 36;
		this.lvRoom.CheckBoxes = true;
		this.lvRoom.Dock = System.Windows.Forms.DockStyle.Right;
		this.lvRoom.FullRowSelect = true;
		this.lvRoom.GridLines = true;
		this.lvRoom.LargeImageList = this.imglist;
		this.lvRoom.Location = new System.Drawing.Point(762, 42);
		this.lvRoom.Name = "lvRoom";
		this.lvRoom.Size = new System.Drawing.Size(248, 553);
		this.lvRoom.TabIndex = 22;
		this.lvRoom.UseCompatibleStateImageBehavior = false;
		this.lvRoom.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(lvRoom_ItemChecked);
		this.panel1.Controls.Add(this.dgvGuest);
		this.panel1.Controls.Add(this.dgvList);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 42);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(762, 553);
		this.panel1.TabIndex = 23;
		this.dgvGuest.AllowUserToAddRows = false;
		this.dgvGuest.AllowUserToDeleteRows = false;
		this.dgvGuest.BackgroundColor = System.Drawing.Color.White;
		this.dgvGuest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvGuest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvGuest.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvGuest.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvGuest.Location = new System.Drawing.Point(0, 160);
		this.dgvGuest.Name = "dgvGuest";
		this.dgvGuest.RowHeadersVisible = false;
		this.dgvGuest.RowTemplate.Height = 23;
		this.dgvGuest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvGuest.Size = new System.Drawing.Size(762, 393);
		this.dgvGuest.TabIndex = 13;
		this.dgvGuest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvGuest_CellClick);
		this.dgvGuest.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(dgvGuest_CellPainting);
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Top;
		this.dgvList.Location = new System.Drawing.Point(0, 0);
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersVisible = false;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(762, 160);
		this.dgvList.TabIndex = 10;
		this.dgvList.SelectionChanged += new System.EventHandler(dgvList_SelectionChanged);
		this.tSync.Interval = 500;
		this.tSync.Tick += new System.EventHandler(tSync_Tick);
		this.clsBackPanel1.BackColor = System.Drawing.Color.Transparent;
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.toolStrip1);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(1010, 42);
		this.clsBackPanel1.TabIndex = 8;
		this.toolStrip1.AutoSize = false;
		this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolStrip1.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSTxtBM, this.TSBtnSear, this.toolStripSeparator1, this.TSBtnClose });
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.toolStrip1.Size = new System.Drawing.Size(1010, 42);
		this.toolStrip1.TabIndex = 9;
		this.TSTxtBM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.TSTxtBM.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSTxtBM.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
		this.TSTxtBM.Name = "TSTxtBM";
		this.TSTxtBM.Size = new System.Drawing.Size(180, 42);
		this.TSTxtBM.Enter += new System.EventHandler(TSTxtBM_Enter);
		this.TSTxtBM.Leave += new System.EventHandler(TSTxtBM_Leave);
		this.TSTxtBM.KeyDown += new System.Windows.Forms.KeyEventHandler(TSTxtBM_KeyDown);
		this.TSBtnSear.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSBtnSear.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.TSBtnSear.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSBtnSear.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
		this.TSBtnSear.Name = "TSBtnSear";
		this.TSBtnSear.Size = new System.Drawing.Size(83, 39);
		this.TSBtnSear.Text = "Search";
		this.TSBtnSear.Click += new System.EventHandler(TSBtnSear_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
		this.TSBtnClose.Image = LockSoftware.Properties.Resources.close;
		this.TSBtnClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.TSBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSBtnClose.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
		this.TSBtnClose.Name = "TSBtnClose";
		this.TSBtnClose.Size = new System.Drawing.Size(54, 39);
		this.TSBtnClose.Text = "Close";
		this.TSBtnClose.Click += new System.EventHandler(TSBtnClose_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1010, 666);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.lvRoom);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.clsBackPanel1);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "frmGBRCheckIn";
		this.Text = "预订入住";
		base.Load += new System.EventHandler(frmGBRCancel_Load);
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		this.tableLayoutPanel3.ResumeLayout(false);
		this.tableLayoutPanel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudDay).EndInit();
		this.panel10.ResumeLayout(false);
		this.panel10.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvGuest).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.clsBackPanel1.ResumeLayout(false);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
	}
}
