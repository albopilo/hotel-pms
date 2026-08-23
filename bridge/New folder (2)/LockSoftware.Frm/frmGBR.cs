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

public class frmGBR : Form
{
	public string m_objName = "WFbr";

	public Hashtable m_htab;

	public bool m_Init;

	public bool m_Del;

	public bool m_chVal;

	private IContainer components;

	public GlassBtn btnCl;

	public GlassBtn btnOK;

	private FlowLayoutPanel flowLayoutPanel1;

	private RadioButton rbGuest;

	private RadioButton rbTbur;

	private ComboBox cobBD;

	private ComboBox cobFD;

	private ComboBox cobType;

	private TextBox txtSRn;

	private Label label19;

	private TextBox txtERn;

	private GlassBtn btnSear;

	private SplitContainer splitContainer1;

	private ListView lvRoom;

	private StatusStrip sstLR;

	private ToolStripStatusLabel TSSLab03;

	private ToolStripStatusLabel TSSLab04;

	private ToolStripStatusLabel TSSLab05;

	private ToolStripStatusLabel TSSLab06;

	private DataGridView dgvRList;

	private StatusStrip sstDR;

	private ToolStripStatusLabel TSSLab07;

	private ToolStripStatusLabel TSSLab08;

	private ToolStripDropDownButton TSSBtnDel;

	private ToolStripDropDownButton TSSBtnRest;

	private clsBackPanel clsBackPanel2;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label1;

	private Label label2;

	private Label label3;

	private TextBox txtCN;

	private TextBox txtCM;

	private Label label4;

	private Label label5;

	private TextBox txtCT;

	private Label label6;

	private TextBox txtGN;

	private DateTimePicker dtpCD;

	private Label label7;

	private DateTimePicker dtpCT;

	private DateTimePicker dtpLD;

	private Label label8;

	private clsBackPanel clsBackPanel1;

	private Label label9;

	private Label label10;

	private Label label11;

	private Label label12;

	private Label label13;

	private ComboBox cobCer;

	private TextBox txtCE;

	private TextBox txtNCernum;

	private Label label15;

	private ImageList imgList;

	private DateTimePicker datTPCD0;

	private DateTimePicker datTPLD0;

	public frmGBR()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void InitCerType()
	{
		try
		{
			cobCer.Text = "";
			cobCer.DataSource = null;
			string sql = "Select * FROM D_Cer  Where cer_flag = 0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				cobCer.DisplayMember = "cer_name";
				cobCer.ValueMember = "cer_id";
				cobCer.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, label3.Text.Substring(0, label3.Text.Length - 1));
		}
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
		if (cobType.SelectedIndex > 0)
		{
			text = text + " And R_TypeID=" + cobType.SelectedValue.ToString();
		}
		return text;
	}

	private void InitRoomList(string sqlStr)
	{
		lvRoom.Items.Clear();
		DateTime dtTime = DateTime.Parse(Program.GetStandDate(dtpCD.Value) + " " + dtpCT.Value.ToString("HH:mm")).AddMinutes(-Program.m_defClearTime);
		DateTime dtTime2 = DateTime.Parse(Program.GetStandDate(dtpLD.Value) + " " + Program.m_defLeaveTime).AddMinutes(Program.m_defClearTime);
		TSSLab04.Text = "";
		string sql = "Select R_Name,R_ID,R_Code,R_SubCode,R_FloorID,R_TypeID,R_RSID,R_BedAdd,R_BedSinglePrice,R_Size, R_Memo, build_ID, Build_Name, Floor_Name, TP_Name , R_CurGuestCount,R_TotalGuest,R_TotalPrice,TP_Price,TP_deposit, RS_Name000, R_MaxCardNum,Build_Code,Floor_Code,TP_BedCount From v_HotelRooms Where r_flag!=1 and R_RSID!=8 and R_RSID!=9 and R_RSID!=7 and R_RSID!=2 and R_ID not in (select r_id from T_Rooms where TR_Level = 0 and not (Tr_cometime >= '" + Program.GetStandDTime(dtTime2, "00") + "' OR Tr_stand_L_time <= '" + Program.GetStandDTime(dtTime, "00") + "')) and R_ID not in (select r_id from T_Schedule where sch_flag = 0 and not (g_come_day + ' ' + g_come_time >= '" + Program.GetStandDTime(dtTime2, "00") + "' OR g_level_day + ' " + Program.m_defLeaveTime + "' <= '" + Program.GetStandDTime(dtTime, "00") + "')) " + sqlStr + "Order by Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(sql);
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
			array[i].ImageIndex = Convert.ToInt16(dataTable.Rows[i]["R_RSID"].ToString()) - 1;
		}
		lvRoom.Items.AddRange(array);
		TSSLab04.Text = lvRoom.Items.Count.ToString();
	}

	private void InitDgvListColumn()
	{
		try
		{
			dgvRList.Rows.Clear();
			dgvRList.Columns.Clear();
			dgvRList.Columns.Add("R_ID", "");
			dgvRList.Columns.Add("R_Name", (string)m_htab["dgvcolR_Name"]);
			dgvRList.Columns.Add("build_ID", "");
			dgvRList.Columns.Add("Build_Name", (string)m_htab["dgvcolBuild_Name"]);
			dgvRList.Columns.Add("floor_id", "");
			dgvRList.Columns.Add("Floor_Name", (string)m_htab["dgvcolFloor_Name"]);
			dgvRList.Columns.Add("TP_Name", (string)m_htab["dgvcolTP_Name"]);
			dgvRList.Columns.Add("TP_BedCount", (string)m_htab["dgvcolTP_BedCount"]);
			dgvRList.Columns.Add("R_CurGuestCount", (string)m_htab["dgvcolR_CurGuestCount"]);
			dgvRList.Columns.Add("TP_Price", (string)m_htab["TP_Price"]);
			dgvRList.Columns.Add("TP_deposit", (string)m_htab["TP_deposit"]);
			dgvRList.Columns.Add("R_BedAdd", (string)m_htab["R_BedAdd"]);
			dgvRList.Columns.Add("R_BedSinglePrice", (string)m_htab["R_BedSinglePrice"]);
			dgvRList.Columns.Add("R_Code", "");
			dgvRList.Columns.Add("R_SubCode", "");
			dgvRList.Columns.Add("Build_Code", "");
			dgvRList.Columns.Add("Floor_Code", "");
			dgvRList.Columns.Add("R_MaxCardNum", "");
			dgvRList.Columns.Add("R_RSID", "");
			DataGridViewColumn dataGridViewColumn = dgvRList.Columns["r_id"];
			DataGridViewColumn dataGridViewColumn2 = dgvRList.Columns["floor_id"];
			bool flag = (dgvRList.Columns["build_ID"].Visible = false);
			bool visible = (dataGridViewColumn2.Visible = flag);
			dataGridViewColumn.Visible = visible;
			DataGridViewColumn dataGridViewColumn3 = dgvRList.Columns["R_Code"];
			DataGridViewColumn dataGridViewColumn4 = dgvRList.Columns["floor_id"];
			bool flag4 = (dgvRList.Columns["R_SubCode"].Visible = false);
			bool visible2 = (dataGridViewColumn4.Visible = flag4);
			dataGridViewColumn3.Visible = visible2;
			DataGridViewColumn dataGridViewColumn5 = dgvRList.Columns["Build_Code"];
			DataGridViewColumn dataGridViewColumn6 = dgvRList.Columns["floor_id"];
			bool flag7 = (dgvRList.Columns["Floor_Code"].Visible = false);
			bool visible3 = (dataGridViewColumn6.Visible = flag7);
			dataGridViewColumn5.Visible = visible3;
			DataGridViewColumn dataGridViewColumn7 = dgvRList.Columns["R_MaxCardNum"];
			DataGridViewColumn dataGridViewColumn8 = dgvRList.Columns["R_BedAdd"];
			bool flag10 = (dgvRList.Columns["R_BedSinglePrice"].Visible = false);
			bool visible4 = (dataGridViewColumn8.Visible = flag10);
			dataGridViewColumn7.Visible = visible4;
			for (int i = 0; i < dgvRList.Columns.Count - 1; i++)
			{
				dgvRList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvRList.Columns[i].Name];
			}
			dgvRList.Columns[dgvRList.Columns.Count - 1].Visible = false;
			dgvRList.AutoResizeColumns();
		}
		catch
		{
		}
	}

	private void frmGBR_Load(object sender, EventArgs e)
	{
		txtERn.Text = (string)m_htab["txtSRn"];
		dtpCD.CustomFormat = Program.m_currDateFmt;
		dtpLD.CustomFormat = Program.m_currDateFmt;
		InitDgvListColumn();
		InitBuild();
		InitCerType();
		InitType();
		try
		{
			btnOK.Text = (string)Program.m_hPubTab["btnOK"];
			btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		}
		catch
		{
		}
		datTPLD0.MaxDate = DateTime.Now.AddDays(9999.0);
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
			txtERn.Select();
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

	private void txtERn_Enter(object sender, EventArgs e)
	{
		if (txtERn.ForeColor == Color.DarkGray)
		{
			txtERn.Text = "";
			txtERn.ForeColor = Color.Black;
		}
	}

	private void txtERn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnSear_Click(null, null);
		}
	}

	private void txtERn_Leave(object sender, EventArgs e)
	{
		if (txtERn.Text.Trim() == "" || txtERn.ForeColor == Color.DarkGray)
		{
			txtERn.Text = (string)m_htab["txtSRn"];
			txtERn.ForeColor = Color.DarkGray;
		}
	}

	private void btnSear_Click(object sender, EventArgs e)
	{
		m_Init = true;
		try
		{
			InitRoomList(getSqlStr());
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnSear.Text);
		}
		m_Init = false;
	}

	private void lvRoom_ItemChecked(object sender, ItemCheckedEventArgs e)
	{
		try
		{
			if (m_Init || m_Del || e.Item == null)
			{
				return;
			}
			if (e.Item.Checked)
			{
				if (rbGuest.Checked && dgvRList.Rows.Count > 0)
				{
					e.Item.Checked = false;
					return;
				}
				for (int i = 0; i < dgvRList.Rows.Count; i++)
				{
					if (e.Item.SubItems[0].Text.Trim() == dgvRList.Rows[i].Cells["R_Name"].Value.ToString().Trim())
					{
						return;
					}
				}
				object[] values = new object[19]
				{
					e.Item.SubItems[1].Text.Trim(),
					e.Item.SubItems[0].Text.Trim(),
					e.Item.SubItems[11].Text.Trim(),
					e.Item.SubItems[12].Text.Trim(),
					e.Item.SubItems[4].Text.Trim(),
					e.Item.SubItems[13].Text.Trim(),
					e.Item.SubItems[14].Text.Trim(),
					e.Item.SubItems[24].Text.Trim(),
					e.Item.SubItems[15].Text.Trim(),
					e.Item.SubItems[18].Text.Trim(),
					e.Item.SubItems[19].Text.Trim(),
					e.Item.SubItems[7].Text.Trim(),
					e.Item.SubItems[8].Text.Trim(),
					e.Item.SubItems[2].Text.Trim(),
					e.Item.SubItems[3].Text.Trim(),
					e.Item.SubItems[22].Text.Trim(),
					e.Item.SubItems[23].Text.Trim(),
					e.Item.SubItems[21].Text.Trim(),
					e.Item.SubItems[6].Text.Trim()
				};
				dgvRList.Rows.Insert(0, values);
				dgvRList.Rows[0].DefaultCellStyle.BackColor = Color.Beige;
			}
			else
			{
				for (int j = 0; j < dgvRList.Rows.Count; j++)
				{
					if (e.Item.SubItems[0].Text.Trim() == dgvRList.Rows[j].Cells["R_Name"].Value.ToString().Trim())
					{
						dgvRList.Rows.RemoveAt(j);
						break;
					}
				}
			}
			TSSLab06.Text = lvRoom.CheckedItems.Count.ToString();
			TSSLab08.Text = dgvRList.Rows.Count.ToString();
			double num = 0.0;
			double num2 = 0.0;
			for (int k = 0; k < dgvRList.Rows.Count; k++)
			{
				if (Convert.ToInt32(dgvRList.Rows[k].Cells["R_CurGuestCount"].Value) <= 0)
				{
					num += Convert.ToDouble(dgvRList.Rows[k].Cells["TP_Price"].Value.ToString());
					num2 += Convert.ToDouble(dgvRList.Rows[k].Cells["TP_deposit"].Value.ToString());
				}
			}
		}
		catch
		{
		}
	}

	private void DelRow()
	{
		if (dgvRList.Rows.Count <= 0)
		{
			return;
		}
		string text = "";
		for (int num = dgvRList.SelectedRows.Count - 1; num >= 0; num--)
		{
			text = dgvRList.SelectedRows[num].Cells[1].Value.ToString().Trim();
			ListViewItem listViewItem = lvRoom.FindItemWithText(text, includeSubItemsInSearch: false, 0);
			if (listViewItem != null)
			{
				listViewItem.Checked = false;
			}
			dgvRList.Rows.RemoveAt(dgvRList.SelectedRows[num].Index);
		}
		TSSLab06.Text = lvRoom.CheckedItems.Count.ToString();
		TSSLab08.Text = dgvRList.Rows.Count.ToString();
		double num2 = 0.0;
		double num3 = 0.0;
		for (int i = 0; i < dgvRList.Rows.Count; i++)
		{
			if (Convert.ToInt32(dgvRList.Rows[i].Cells["R_CurGuestCount"].Value) <= 0)
			{
				num2 += Convert.ToDouble(dgvRList.Rows[i].Cells["TP_Price"].Value.ToString());
				num3 += Convert.ToDouble(dgvRList.Rows[i].Cells["TP_deposit"].Value.ToString());
			}
		}
	}

	private void TSSBtnDel_Click(object sender, EventArgs e)
	{
		m_Del = true;
		try
		{
			DelRow();
		}
		catch
		{
		}
		m_Del = false;
	}

	private void TSSBtnRest_Click(object sender, EventArgs e)
	{
		m_Del = true;
		try
		{
			dgvRList.SelectAll();
			DelRow();
		}
		catch
		{
		}
		m_Del = false;
	}

	private void rbGuest_CheckedChanged(object sender, EventArgs e)
	{
		lvRoom.MultiSelect = rbTbur.Checked;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvRList.Rows.Count <= 0)
			{
				Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Asterisk);
			}
			else if (dtpCD.Value < DateTime.Today)
			{
				Program.MsgBox((string)m_htab["Error_time"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				if (Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtCN.Text.Trim(), chk: true) || Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtCM.Text.Trim(), chk: true) || Program.isValNull(label7.Text.Substring(0, label7.Text.Length - 1), txtGN.Text.Trim(), chk: true) || Program.isValNull(label13.Text.Substring(0, label13.Text.Length - 1), txtNCernum.Text.Trim(), chk: true))
				{
					return;
				}
				for (int num = dgvRList.Rows.Count - 1; num >= 0; num--)
				{
					long num2 = Convert.ToInt64(dgvRList.Rows[num].Cells["R_ID"].Value);
					int iRoomStatus = Convert.ToInt32(dgvRList.Rows[num].Cells["R_RSID"].Value);
					DateTime dtDueCome = DateTime.Parse(Program.GetStandDate(dtpCD.Value) + " " + dtpCT.Value.ToString("HH:mm"));
					string text = "Insert Into T_schedule Values(N'" + txtCN.Text.Trim() + "', N'" + txtCM.Text.Trim() + "', N'" + txtCT.Text.Trim() + "', N'" + txtCE.Text.Trim() + "', N'" + txtGN.Text.Trim() + "', 2," + cobCer.SelectedValue.ToString() + ", N'" + txtNCernum.Text.Trim() + "', NULL, " + num2.ToString() + ", '" + Program.GetStandDate(dtpCD.Value) + "', '" + dtpCT.Value.ToString("HH:mm") + "', '" + Program.GetStandDate(dtpLD.Value) + "', Null, GetDate(), " + Program.m_opid + ",N'" + Program.m_OperName + "', '', 0) \n";
					if (Program.IsScheduleStatus(dtDueCome, iRoomStatus))
					{
						text = text + " Update D_Rooms Set R_RSID = 3 Where R_ID = " + num2;
					}
					if (Program.DBCompExec(text, Text) < 0)
					{
						Program.MsgCustom(txtCN.Text.Trim() + "\r\n" + (string)m_htab["Err01"], MessageBoxIcon.Hand);
						return;
					}
					dgvRList.Rows.Remove(dgvRList.Rows[num]);
				}
				txtGN.Text = "";
				if (Program.fm != null)
				{
					Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
				}
				btnSear_Click(null, null);
				Program.MsgCustom(Program.GetFormatStringShow("Succeed"), MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
	}

	private void btnCl_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dtpCD_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (dtpCD.Value < DateTime.Now)
			{
				datTPCD0.Value = DateTime.Now;
			}
		}
		catch
		{
		}
	}

	private void dtpLD_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (dtpLD.Value < dtpCD.Value)
			{
				datTPLD0.Value = dtpCD.Value.AddDays(1.0);
			}
		}
		catch
		{
		}
	}

	private void btnIDCard_Click(object sender, EventArgs e)
	{
		try
		{
			Program.IDCardData CardMsg = default(Program.IDCardData);
			if (Program.Get_IDCardII_Information(ref CardMsg) >= 0)
			{
				txtNCernum.Text = CardMsg.IDCardNo;
			}
		}
		catch
		{
		}
	}

	private void datTPCD0_ValueChanged(object sender, EventArgs e)
	{
		dtpCD.Value = datTPCD0.Value;
		dtpCT.Value = datTPCD0.Value;
		datTPLD0.MaxDate = datTPCD0.Value.AddDays(9999.0);
	}

	private void datTPLD0_ValueChanged(object sender, EventArgs e)
	{
		dtpLD.Value = datTPLD0.Value;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmGBR));
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.cobBD = new System.Windows.Forms.ComboBox();
		this.cobFD = new System.Windows.Forms.ComboBox();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.datTPCD0 = new System.Windows.Forms.DateTimePicker();
		this.label19 = new System.Windows.Forms.Label();
		this.datTPLD0 = new System.Windows.Forms.DateTimePicker();
		this.btnSear = new LockSoftware.Controls.GlassBtn(this.components);
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.lvRoom = new System.Windows.Forms.ListView();
		this.imgList = new System.Windows.Forms.ImageList(this.components);
		this.sstLR = new System.Windows.Forms.StatusStrip();
		this.TSSLab03 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab04 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab05 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab06 = new System.Windows.Forms.ToolStripStatusLabel();
		this.dgvRList = new System.Windows.Forms.DataGridView();
		this.sstDR = new System.Windows.Forms.StatusStrip();
		this.TSSLab07 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab08 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSBtnDel = new System.Windows.Forms.ToolStripDropDownButton();
		this.TSSBtnRest = new System.Windows.Forms.ToolStripDropDownButton();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.txtCN = new System.Windows.Forms.TextBox();
		this.txtCM = new System.Windows.Forms.TextBox();
		this.txtSRn = new System.Windows.Forms.TextBox();
		this.txtCT = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtERn = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.rbTbur = new System.Windows.Forms.RadioButton();
		this.rbGuest = new System.Windows.Forms.RadioButton();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCE = new System.Windows.Forms.TextBox();
		this.txtNCernum = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.dtpCD = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtGN = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.dtpCT = new System.Windows.Forms.DateTimePicker();
		this.dtpLD = new System.Windows.Forms.DateTimePicker();
		this.label8 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.flowLayoutPanel1.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.sstLR.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRList).BeginInit();
		this.sstDR.SuspendLayout();
		this.clsBackPanel2.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.flowLayoutPanel1.AutoScroll = true;
		this.flowLayoutPanel1.Controls.Add(this.cobBD);
		this.flowLayoutPanel1.Controls.Add(this.cobFD);
		this.flowLayoutPanel1.Controls.Add(this.cobType);
		this.flowLayoutPanel1.Controls.Add(this.datTPCD0);
		this.flowLayoutPanel1.Controls.Add(this.label19);
		this.flowLayoutPanel1.Controls.Add(this.datTPLD0);
		this.flowLayoutPanel1.Controls.Add(this.btnSear);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(784, 47);
		this.flowLayoutPanel1.TabIndex = 0;
		this.cobBD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBD.DropDownWidth = 180;
		this.cobBD.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobBD.FormattingEnabled = true;
		this.cobBD.Location = new System.Drawing.Point(3, 12);
		this.cobBD.Margin = new System.Windows.Forms.Padding(3, 12, 0, 3);
		this.cobBD.Name = "cobBD";
		this.cobBD.Size = new System.Drawing.Size(90, 23);
		this.cobBD.TabIndex = 3;
		this.cobBD.SelectedIndexChanged += new System.EventHandler(cobBD_SelectedIndexChanged);
		this.cobFD.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobFD.DropDownWidth = 180;
		this.cobFD.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobFD.FormattingEnabled = true;
		this.cobFD.Location = new System.Drawing.Point(96, 12);
		this.cobFD.Margin = new System.Windows.Forms.Padding(3, 12, 0, 3);
		this.cobFD.Name = "cobFD";
		this.cobFD.Size = new System.Drawing.Size(90, 23);
		this.cobFD.TabIndex = 4;
		this.cobType.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.DropDownWidth = 180;
		this.cobType.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(189, 12);
		this.cobType.Margin = new System.Windows.Forms.Padding(3, 12, 0, 3);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(90, 23);
		this.cobType.TabIndex = 5;
		this.datTPCD0.CustomFormat = "yyyy-MM-dd HH:mm";
		this.datTPCD0.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.datTPCD0.Location = new System.Drawing.Point(282, 12);
		this.datTPCD0.Margin = new System.Windows.Forms.Padding(3, 12, 0, 3);
		this.datTPCD0.Name = "datTPCD0";
		this.datTPCD0.Size = new System.Drawing.Size(125, 21);
		this.datTPCD0.TabIndex = 17;
		this.datTPCD0.ValueChanged += new System.EventHandler(datTPCD0_ValueChanged);
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(410, 13);
		this.label19.Margin = new System.Windows.Forms.Padding(3, 13, 0, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(22, 17);
		this.label19.TabIndex = 7;
		this.label19.Text = "→";
		this.datTPLD0.CustomFormat = "yyyy-MM-dd";
		this.datTPLD0.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.datTPLD0.Location = new System.Drawing.Point(435, 12);
		this.datTPLD0.Margin = new System.Windows.Forms.Padding(3, 12, 0, 3);
		this.datTPLD0.Name = "datTPLD0";
		this.datTPLD0.Size = new System.Drawing.Size(125, 21);
		this.datTPLD0.TabIndex = 18;
		this.datTPLD0.ValueChanged += new System.EventHandler(datTPLD0_ValueChanged);
		this.btnSear.AutoSize = true;
		this.btnSear.BackColor = System.Drawing.Color.LightGray;
		this.btnSear.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSear.ForeColor = System.Drawing.Color.Black;
		this.btnSear.GlowColor = System.Drawing.Color.White;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.Image = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSear.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSear.Location = new System.Drawing.Point(563, 3);
		this.btnSear.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
		this.btnSear.Name = "btnSear";
		this.btnSear.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSear.Size = new System.Drawing.Size(89, 38);
		this.btnSear.TabIndex = 9;
		this.btnSear.Text = "Search";
		this.btnSear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(0, 47);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.lvRoom);
		this.splitContainer1.Panel1.Controls.Add(this.sstLR);
		this.splitContainer1.Panel1MinSize = 280;
		this.splitContainer1.Panel2.Controls.Add(this.dgvRList);
		this.splitContainer1.Panel2.Controls.Add(this.sstDR);
		this.splitContainer1.Size = new System.Drawing.Size(784, 192);
		this.splitContainer1.SplitterDistance = 300;
		this.splitContainer1.TabIndex = 1;
		this.lvRoom.CheckBoxes = true;
		this.lvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvRoom.FullRowSelect = true;
		this.lvRoom.GridLines = true;
		this.lvRoom.LargeImageList = this.imgList;
		this.lvRoom.Location = new System.Drawing.Point(0, 0);
		this.lvRoom.MultiSelect = false;
		this.lvRoom.Name = "lvRoom";
		this.lvRoom.Size = new System.Drawing.Size(296, 158);
		this.lvRoom.TabIndex = 18;
		this.lvRoom.UseCompatibleStateImageBehavior = false;
		this.lvRoom.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(lvRoom_ItemChecked);
		this.imgList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgList.ImageStream");
		this.imgList.TransparentColor = System.Drawing.Color.Transparent;
		this.imgList.Images.SetKeyName(0, "05(1).png");
		this.imgList.Images.SetKeyName(1, "trashcan_full.ico");
		this.imgList.Images.SetKeyName(2, "synchour.png");
		this.imgList.Images.SetKeyName(3, "120px-Vista-Login_Manager.png");
		this.imgList.Images.SetKeyName(4, "54.png");
		this.imgList.Images.SetKeyName(5, "35(1).png");
		this.imgList.Images.SetKeyName(6, "Pic_07.png");
		this.imgList.Images.SetKeyName(7, "tt.ico");
		this.imgList.Images.SetKeyName(8, "v_stop.png");
		this.imgList.Images.SetKeyName(9, "Icon-1.png");
		this.imgList.Images.SetKeyName(10, "Icon-2.png");
		this.sstLR.AutoSize = false;
		this.sstLR.BackColor = System.Drawing.Color.Transparent;
		this.sstLR.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstLR.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSSLab03, this.TSSLab04, this.TSSLab05, this.TSSLab06 });
		this.sstLR.Location = new System.Drawing.Point(0, 158);
		this.sstLR.Name = "sstLR";
		this.sstLR.Size = new System.Drawing.Size(296, 30);
		this.sstLR.SizingGrip = false;
		this.sstLR.TabIndex = 19;
		this.sstLR.Text = "statusStrip1";
		this.TSSLab03.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab03.Name = "TSSLab03";
		this.TSSLab03.Size = new System.Drawing.Size(43, 25);
		this.TSSLab03.Text = "Total:";
		this.TSSLab04.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab04.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab04.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab04.Name = "TSSLab04";
		this.TSSLab04.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab04.Size = new System.Drawing.Size(88, 25);
		this.TSSLab04.Spring = true;
		this.TSSLab04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab05.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab05.Name = "TSSLab05";
		this.TSSLab05.Size = new System.Drawing.Size(62, 25);
		this.TSSLab05.Text = "Selected:";
		this.TSSLab06.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab06.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab06.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab06.Name = "TSSLab06";
		this.TSSLab06.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab06.Size = new System.Drawing.Size(88, 25);
		this.TSSLab06.Spring = true;
		this.TSSLab06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.dgvRList.AllowUserToAddRows = false;
		this.dgvRList.AllowUserToDeleteRows = false;
		this.dgvRList.BackgroundColor = System.Drawing.Color.White;
		this.dgvRList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvRList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvRList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvRList.Location = new System.Drawing.Point(0, 0);
		this.dgvRList.Name = "dgvRList";
		this.dgvRList.ReadOnly = true;
		this.dgvRList.RowHeadersWidth = 25;
		this.dgvRList.RowTemplate.Height = 23;
		this.dgvRList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRList.Size = new System.Drawing.Size(476, 158);
		this.dgvRList.TabIndex = 14;
		this.sstDR.AutoSize = false;
		this.sstDR.BackColor = System.Drawing.Color.Transparent;
		this.sstDR.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstDR.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSSLab07, this.TSSLab08, this.TSSBtnDel, this.TSSBtnRest });
		this.sstDR.Location = new System.Drawing.Point(0, 158);
		this.sstDR.Name = "sstDR";
		this.sstDR.Size = new System.Drawing.Size(476, 30);
		this.sstDR.SizingGrip = false;
		this.sstDR.TabIndex = 15;
		this.sstDR.Text = "statusStrip2";
		this.TSSLab07.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab07.Name = "TSSLab07";
		this.TSSLab07.Size = new System.Drawing.Size(43, 25);
		this.TSSLab07.Text = "Total:";
		this.TSSLab08.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab08.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab08.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab08.Name = "TSSLab08";
		this.TSSLab08.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab08.Size = new System.Drawing.Size(355, 25);
		this.TSSLab08.Spring = true;
		this.TSSLab08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSBtnDel.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSSBtnDel.Image = LockSoftware.Properties.Resources.delete;
		this.TSSBtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSBtnDel.Name = "TSSBtnDel";
		this.TSSBtnDel.ShowDropDownArrow = false;
		this.TSSBtnDel.Size = new System.Drawing.Size(63, 28);
		this.TSSBtnDel.Text = "Delete";
		this.TSSBtnDel.Click += new System.EventHandler(TSSBtnDel_Click);
		this.TSSBtnRest.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSSBtnRest.Image = LockSoftware.Properties.Resources.clear;
		this.TSSBtnRest.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSBtnRest.Name = "TSSBtnRest";
		this.TSSBtnRest.ShowDropDownArrow = false;
		this.TSSBtnRest.Size = new System.Drawing.Size(59, 28);
		this.TSSBtnRest.Text = "Reset";
		this.TSSBtnRest.Visible = false;
		this.TSSBtnRest.Click += new System.EventHandler(TSSBtnRest_Click);
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
		this.clsBackPanel2.BorderTW = 1;
		this.clsBackPanel2.Color1 = System.Drawing.SystemColors.Control;
		this.clsBackPanel2.Color2 = System.Drawing.SystemColors.Control;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.tableLayoutPanel1);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 239);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(784, 183);
		this.clsBackPanel2.TabIndex = 2;
		this.tableLayoutPanel1.AutoScroll = true;
		this.tableLayoutPanel1.ColumnCount = 8;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.cobCer, 2, 3);
		this.tableLayoutPanel1.Controls.Add(this.txtCN, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtCM, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtSRn, 7, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtCT, 2, 2);
		this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtERn, 7, 0);
		this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.clsBackPanel1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.label9, 3, 0);
		this.tableLayoutPanel1.Controls.Add(this.label10, 3, 1);
		this.tableLayoutPanel1.Controls.Add(this.label12, 1, 3);
		this.tableLayoutPanel1.Controls.Add(this.label4, 4, 4);
		this.tableLayoutPanel1.Controls.Add(this.txtCE, 5, 4);
		this.tableLayoutPanel1.Controls.Add(this.txtNCernum, 2, 4);
		this.tableLayoutPanel1.Controls.Add(this.label13, 1, 4);
		this.tableLayoutPanel1.Controls.Add(this.btnCl, 7, 4);
		this.tableLayoutPanel1.Controls.Add(this.btnOK, 7, 3);
		this.tableLayoutPanel1.Controls.Add(this.dtpCD, 5, 1);
		this.tableLayoutPanel1.Controls.Add(this.label5, 4, 1);
		this.tableLayoutPanel1.Controls.Add(this.label7, 4, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtGN, 5, 0);
		this.tableLayoutPanel1.Controls.Add(this.label11, 6, 0);
		this.tableLayoutPanel1.Controls.Add(this.label15, 3, 4);
		this.tableLayoutPanel1.Controls.Add(this.dtpCT, 5, 2);
		this.tableLayoutPanel1.Controls.Add(this.dtpLD, 5, 3);
		this.tableLayoutPanel1.Controls.Add(this.label8, 4, 2);
		this.tableLayoutPanel1.Controls.Add(this.label6, 4, 3);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
		this.tableLayoutPanel1.RowCount = 5;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 183);
		this.tableLayoutPanel1.TabIndex = 3;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 160;
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(181, 113);
		this.cobCer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(100, 23);
		this.cobCer.TabIndex = 2;
		this.txtCN.Location = new System.Drawing.Point(181, 6);
		this.txtCN.MaxLength = 50;
		this.txtCN.Name = "txtCN";
		this.txtCN.Size = new System.Drawing.Size(100, 21);
		this.txtCN.TabIndex = 3;
		this.txtCM.Location = new System.Drawing.Point(181, 41);
		this.txtCM.MaxLength = 50;
		this.txtCM.Name = "txtCM";
		this.txtCM.Size = new System.Drawing.Size(100, 21);
		this.txtCM.TabIndex = 5;
		this.txtSRn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSRn.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSRn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtSRn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtSRn.Location = new System.Drawing.Point(526, 50);
		this.txtSRn.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.txtSRn.MaxLength = 50;
		this.txtSRn.Name = "txtSRn";
		this.txtSRn.Size = new System.Drawing.Size(90, 21);
		this.txtSRn.TabIndex = 6;
		this.txtSRn.Text = "ROOM NAME...";
		this.txtSRn.Visible = false;
		this.txtSRn.Enter += new System.EventHandler(txtSRn_Enter);
		this.txtSRn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSRn_KeyDown);
		this.txtSRn.Leave += new System.EventHandler(txtSRn_Leave);
		this.txtCT.Location = new System.Drawing.Point(181, 76);
		this.txtCT.MaxLength = 50;
		this.txtCT.Name = "txtCT";
		this.txtCT.Size = new System.Drawing.Size(100, 21);
		this.txtCT.TabIndex = 10;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(108, 8);
		this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "联 系 人：";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(108, 43);
		this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(61, 15);
		this.label2.TabIndex = 1;
		this.label2.Text = "手 机 号：";
		this.txtERn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtERn.ForeColor = System.Drawing.Color.DarkGray;
		this.txtERn.Location = new System.Drawing.Point(526, 15);
		this.txtERn.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
		this.txtERn.MaxLength = 50;
		this.txtERn.Name = "txtERn";
		this.txtERn.Size = new System.Drawing.Size(90, 21);
		this.txtERn.TabIndex = 8;
		this.txtERn.Text = "ROOM NAME...";
		this.txtERn.Visible = false;
		this.txtERn.Enter += new System.EventHandler(txtERn_Enter);
		this.txtERn.KeyDown += new System.Windows.Forms.KeyEventHandler(txtERn_KeyDown);
		this.txtERn.Leave += new System.EventHandler(txtERn_Leave);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(108, 78);
		this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(67, 15);
		this.label3.TabIndex = 2;
		this.label3.Text = "联系电话：";
		this.clsBackPanel1.AutoSize = true;
		this.clsBackPanel1.Border = true;
		this.clsBackPanel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderBW = 1;
		this.clsBackPanel1.BorderColorBottom = System.Drawing.Color.Silver;
		this.clsBackPanel1.BorderColorLeft = System.Drawing.Color.Silver;
		this.clsBackPanel1.BorderColorRight = System.Drawing.Color.Silver;
		this.clsBackPanel1.BorderColorTop = System.Drawing.Color.Silver;
		this.clsBackPanel1.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderLW = 1;
		this.clsBackPanel1.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderRW = 1;
		this.clsBackPanel1.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderTW = 1;
		this.clsBackPanel1.Color1 = System.Drawing.SystemColors.Control;
		this.clsBackPanel1.Color2 = System.Drawing.SystemColors.Control;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.rbTbur);
		this.clsBackPanel1.Controls.Add(this.rbGuest);
		this.clsBackPanel1.Location = new System.Drawing.Point(6, 6);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.tableLayoutPanel1.SetRowSpan(this.clsBackPanel1, 4);
		this.clsBackPanel1.Size = new System.Drawing.Size(96, 75);
		this.clsBackPanel1.TabIndex = 21;
		this.clsBackPanel1.Visible = false;
		this.rbTbur.AutoSize = true;
		this.rbTbur.BackColor = System.Drawing.Color.Transparent;
		this.rbTbur.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.rbTbur.Location = new System.Drawing.Point(10, 46);
		this.rbTbur.Margin = new System.Windows.Forms.Padding(4, 13, 4, 4);
		this.rbTbur.Name = "rbTbur";
		this.rbTbur.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
		this.rbTbur.Size = new System.Drawing.Size(82, 25);
		this.rbTbur.TabIndex = 1;
		this.rbTbur.Text = "团队预订";
		this.rbTbur.UseVisualStyleBackColor = false;
		this.rbGuest.AutoSize = true;
		this.rbGuest.BackColor = System.Drawing.Color.Transparent;
		this.rbGuest.Checked = true;
		this.rbGuest.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.rbGuest.Location = new System.Drawing.Point(10, 9);
		this.rbGuest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
		this.rbGuest.Name = "rbGuest";
		this.rbGuest.Size = new System.Drawing.Size(82, 20);
		this.rbGuest.TabIndex = 0;
		this.rbGuest.TabStop = true;
		this.rbGuest.Text = "单人预订";
		this.rbGuest.UseVisualStyleBackColor = false;
		this.rbGuest.CheckedChanged += new System.EventHandler(rbGuest_CheckedChanged);
		this.label9.AutoSize = true;
		this.label9.ForeColor = System.Drawing.Color.Red;
		this.label9.Location = new System.Drawing.Point(287, 11);
		this.label9.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(13, 15);
		this.label9.TabIndex = 22;
		this.label9.Text = "*";
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.Color.Red;
		this.label10.Location = new System.Drawing.Point(287, 46);
		this.label10.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(13, 15);
		this.label10.TabIndex = 23;
		this.label10.Text = "*";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(108, 113);
		this.label12.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(67, 15);
		this.label12.TabIndex = 25;
		this.label12.Text = "证件类型：";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(328, 148);
		this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(67, 15);
		this.label4.TabIndex = 6;
		this.label4.Text = "电子邮件：";
		this.txtCE.Location = new System.Drawing.Point(401, 146);
		this.txtCE.MaxLength = 50;
		this.txtCE.Name = "txtCE";
		this.txtCE.Size = new System.Drawing.Size(100, 21);
		this.txtCE.TabIndex = 29;
		this.txtNCernum.Location = new System.Drawing.Point(181, 148);
		this.txtNCernum.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.txtNCernum.MaxLength = 50;
		this.txtNCernum.Name = "txtNCernum";
		this.txtNCernum.Size = new System.Drawing.Size(100, 21);
		this.txtNCernum.TabIndex = 28;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(108, 148);
		this.label13.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(67, 15);
		this.label13.TabIndex = 26;
		this.label13.Text = "证件号码：";
		this.btnCl.BackColor = System.Drawing.Color.LightGray;
		this.btnCl.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.close;
		this.btnCl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(527, 147);
		this.btnCl.Margin = new System.Windows.Forms.Padding(4);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(72, 27);
		this.btnCl.TabIndex = 6;
		this.btnCl.Text = "关 闭";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.Click += new System.EventHandler(btnCl_Click);
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(527, 112);
		this.btnOK.Margin = new System.Windows.Forms.Padding(4);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(72, 27);
		this.btnOK.TabIndex = 7;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.dtpCD.CustomFormat = "yyyy-MM-dd";
		this.dtpCD.Enabled = false;
		this.dtpCD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCD.Location = new System.Drawing.Point(401, 41);
		this.dtpCD.Name = "dtpCD";
		this.dtpCD.Size = new System.Drawing.Size(100, 21);
		this.dtpCD.TabIndex = 14;
		this.dtpCD.ValueChanged += new System.EventHandler(dtpCD_ValueChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(328, 43);
		this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(67, 15);
		this.label5.TabIndex = 8;
		this.label5.Text = "入住日期：";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(328, 8);
		this.label7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(61, 15);
		this.label7.TabIndex = 15;
		this.label7.Text = "入 住 人：";
		this.txtGN.Location = new System.Drawing.Point(401, 6);
		this.txtGN.MaxLength = 50;
		this.txtGN.Name = "txtGN";
		this.txtGN.Size = new System.Drawing.Size(100, 21);
		this.txtGN.TabIndex = 13;
		this.label11.AutoSize = true;
		this.label11.ForeColor = System.Drawing.Color.Red;
		this.label11.Location = new System.Drawing.Point(507, 9);
		this.label11.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(13, 15);
		this.label11.TabIndex = 24;
		this.label11.Text = "*";
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.Color.Red;
		this.label15.Location = new System.Drawing.Point(287, 151);
		this.label15.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(13, 15);
		this.label15.TabIndex = 31;
		this.label15.Text = "*";
		this.dtpCT.CustomFormat = "HH:mm";
		this.dtpCT.Enabled = false;
		this.dtpCT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCT.Location = new System.Drawing.Point(401, 76);
		this.dtpCT.Name = "dtpCT";
		this.dtpCT.ShowUpDown = true;
		this.dtpCT.Size = new System.Drawing.Size(100, 21);
		this.dtpCT.TabIndex = 17;
		this.dtpLD.CustomFormat = "yyyy-MM-dd";
		this.dtpLD.Enabled = false;
		this.dtpLD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLD.Location = new System.Drawing.Point(401, 111);
		this.dtpLD.Name = "dtpLD";
		this.dtpLD.Size = new System.Drawing.Size(100, 21);
		this.dtpLD.TabIndex = 16;
		this.dtpLD.ValueChanged += new System.EventHandler(dtpLD_ValueChanged);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(328, 78);
		this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(67, 15);
		this.label8.TabIndex = 18;
		this.label8.Text = "抵店时间：";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(328, 113);
		this.label6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(67, 15);
		this.label6.TabIndex = 11;
		this.label6.Text = "离店日期：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(784, 422);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.flowLayoutPanel1);
		base.Controls.Add(this.clsBackPanel2);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "frmGBR";
		this.Text = "客房预订";
		base.Load += new System.EventHandler(frmGBR_Load);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.sstLR.ResumeLayout(false);
		this.sstLR.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRList).EndInit();
		this.sstDR.ResumeLayout(false);
		this.sstDR.PerformLayout();
		this.clsBackPanel2.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
