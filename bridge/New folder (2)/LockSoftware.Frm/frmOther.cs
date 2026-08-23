using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmOther : Form
{
	public string m_objName = "WFOth";

	public Hashtable m_htab;

	private IContainer components;

	private SplitContainer splitContainer1;

	private clsBackPanel clsBackPanel1;

	private TextBox txtIID;

	private ComboBox cobOthType;

	private TextBox txtIN;

	private DataGridView dgvItem;

	private ToolsBtn btnSear;

	private clsBackPanel clsBackPanel2;

	private GlassBtn btnRC;

	private SplitContainer splitContainer2;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel tsslab01;

	private ToolStripStatusLabel tsslabSel;

	private ToolStripStatusLabel tsslab02;

	private ToolStripStatusLabel tsslabGift;

	private ToolStripStatusLabel tsslab03;

	private ToolStripStatusLabel tsslabTotal;

	private clsBackPanel clsBackPanel3;

	private FlowLayoutPanel flowLayoutPanel1;

	private GlassBtn btnClose;

	private GlassBtn btnOK;

	private Label label2;

	private Label label1;

	private DataGridView dGVListHistory;

	private StatusStrip staSHistory;

	private ToolStripStatusLabel tSSLNum;

	private ToolStripStatusLabel tSSLTotalNum;

	private ToolStripStatusLabel tSSLGift;

	private ToolStripStatusLabel tSSLTotalGift;

	private ToolStripStatusLabel tSSLTotal;

	private ToolStripStatusLabel tSSLTotalPrice;

	private DataGridView dgvGuest;

	private DataGridView dgvList;

	private DataGridViewTextBoxColumn g_ID;

	private DataGridViewTextBoxColumn r_id;

	private DataGridViewTextBoxColumn r_name;

	private DataGridViewTextBoxColumn g_name;

	private DataGridViewTextBoxColumn oth_ID;

	private DataGridViewTextBoxColumn oth_name;

	private DataGridViewTextBoxColumn oth_unit;

	private DataGridViewTextBoxColumn oth_price;

	private DataGridViewTextBoxColumn Qty;

	private DataGridViewCheckBoxColumn Gift;

	private DataGridViewTextBoxColumn oth_total;

	private DataGridViewLinkColumn oth_del;

	private DataGridViewTextBoxColumn tr_id;

	private DataGridViewTextBoxColumn team_id;

	private DataGridViewTextBoxColumn curr_code;

	private DataGridViewTextBoxColumn curr_rate;

	private Panel panGuestInfo;

	private LinkLabel linLDetail;

	private Label labDCUse;

	private Label labCerNum;

	private Label labCerType;

	private Label labUName;

	private Label labRName;

	private Label txtRName;

	private Label txtUName;

	private Label txtCerName;

	private Label txtCerNum;

	private Label txtDCUse;

	private Label LabHistory;

	private Panel panel1;

	private ToolsBtn btnRSear;

	private TextBox txtCN;

	private TextBox txtGuest;

	public TextBox txtRoom;

	private GlassBtn gBtnSearch;

	private NumericUpDown txtDC;

	public frmOther()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		tSSLGift.Text = tsslab02.Text;
		tSSLTotal.Text = tsslab03.Text;
		linLDetail.Text = (string)m_htab["linLDetail_0"];
		txtDC.Value = Convert.ToDecimal(Program.GetFaceDisValue());
	}

	private void cobOthType_Enter(object sender, EventArgs e)
	{
		if (cobOthType.ForeColor == Color.DarkGray)
		{
			cobOthType.Text = "";
			cobOthType.ForeColor = Color.Black;
		}
	}

	private void cobOthType_Leave(object sender, EventArgs e)
	{
		if (cobOthType.Text.Trim() == "" || cobOthType.ForeColor == Color.DarkGray)
		{
			cobOthType.Text = (string)m_htab["cobOthType"];
			cobOthType.ForeColor = Color.DarkGray;
		}
	}

	private void txtIID_Enter(object sender, EventArgs e)
	{
		if (txtIID.ForeColor == Color.DarkGray)
		{
			txtIID.Text = "";
			txtIID.ForeColor = Color.Black;
		}
	}

	private void txtIID_Leave(object sender, EventArgs e)
	{
		if (txtIID.Text.Trim() == "" || txtIID.ForeColor == Color.DarkGray)
		{
			txtIID.Text = (string)m_htab["txtIID"];
			txtIID.ForeColor = Color.DarkGray;
		}
	}

	private void txtIN_Enter(object sender, EventArgs e)
	{
		if (txtIN.ForeColor == Color.DarkGray)
		{
			txtIN.Text = "";
			txtIN.ForeColor = Color.Black;
		}
	}

	private void txtIN_Leave(object sender, EventArgs e)
	{
		if (txtIN.Text.Trim() == "" || txtIN.ForeColor == Color.DarkGray)
		{
			txtIN.Text = (string)m_htab["txtIN"];
			txtIN.ForeColor = Color.DarkGray;
		}
	}

	private void txtRoom_Leave(object sender, EventArgs e)
	{
		if (txtRoom.Text.Trim() == "" || txtRoom.ForeColor == Color.DarkGray)
		{
			txtRoom.Text = (string)m_htab["txtRoom"];
			txtRoom.ForeColor = Color.DarkGray;
		}
	}

	private void txtRoom_Enter(object sender, EventArgs e)
	{
		if (txtRoom.ForeColor == Color.DarkGray)
		{
			txtRoom.Text = "";
			txtRoom.ForeColor = Color.Black;
		}
	}

	private void txtGuest_Enter(object sender, EventArgs e)
	{
		if (txtGuest.ForeColor == Color.DarkGray)
		{
			txtGuest.Text = "";
			txtGuest.ForeColor = Color.Black;
		}
	}

	private void txtGuest_Leave(object sender, EventArgs e)
	{
		if (txtGuest.Text.Trim() == "" || txtGuest.ForeColor == Color.DarkGray)
		{
			txtGuest.Text = (string)m_htab["txtGuest"];
			txtGuest.ForeColor = Color.DarkGray;
		}
	}

	private void txtCN_Enter(object sender, EventArgs e)
	{
		if (txtCN.ForeColor == Color.DarkGray)
		{
			txtCN.Text = "";
			txtCN.ForeColor = Color.Black;
		}
	}

	private void txtCN_Leave(object sender, EventArgs e)
	{
		if (txtCN.Text.Trim() == "" || txtCN.ForeColor == Color.DarkGray)
		{
			txtCN.Text = (string)m_htab["txtCN"];
			txtCN.ForeColor = Color.DarkGray;
		}
	}

	private void InitType()
	{
		try
		{
			cobOthType.DataSource = null;
			string sql = "Select OT_ID, OT_Name FROM D_OtherType Where OT_flag = 0 Order by OT_Name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null || dataTable.Rows.Count > 0)
			{
				cobOthType.DisplayMember = "OT_Name";
				cobOthType.ValueMember = "OT_ID";
				cobOthType.DataSource = dataTable.DefaultView;
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
			string text = "Select Row_Number() OVER (Order by OT_ID) AS RowNumber, oth_ID,OT_Name,oth_name,oth_unit,oth_price,oth_memo FROM v_Other Where oth_flag = 0";
			if (cobOthType.Text.Trim() != "" && cobOthType.ForeColor == Color.Black)
			{
				text = text + " And OT_Name like N'" + cobOthType.Text.Trim() + "%'";
			}
			if (txtIID.Text.Trim() != "" && txtIID.ForeColor == Color.Black)
			{
				text = text + " And oth_ID like '" + txtIID.Text.Trim() + "%'";
			}
			if (txtIN.Text.Trim() != "" && txtIN.ForeColor == Color.Black)
			{
				text = text + " And oth_name like N'" + txtIN.Text.Trim() + "%'";
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			dgvItem.DataSource = dataTable.DefaultView;
			if (dgvItem.DataSource != null)
			{
				for (int i = 0; i < dgvItem.Columns.Count; i++)
				{
					dgvItem.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvItem.Columns[i].Name];
				}
				dgvItem.AutoResizeColumns();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["dgvcoloth_name"]);
		}
	}

	private void InitGuest(string[] cardinfo)
	{
		try
		{
			string text = string.Concat("Select top 50 g_id, IsNull(team_id,-1) As team_id, tr_id, r_id, r_name, g_name, cer_name, g_cernum, g_cometime,(Cast(Cast(g_SOTotalDay As Integer) As varchar)+ N'", Program.m_hPubTab["InfoDay"], "'+ Cast(Cast(g_stayHour As Integer) As varchar) + N'", Program.m_hPubTab["InfoHour"], "') as g_stayhour, g_stand_l_time,g_deposit as depos,curr_code,curr_rate,(cast(cast(isnull(a_id,0)/2.0 as numeric(18,1)) as varchar)+N'", Program.m_hPubTab["InfoDay"], "'+cast(cast(g_actual_S_Hour as integer) as varchar)+N'", Program.m_hPubTab["InfoHour"], "') As havStay From v_CardGuest Where g_level = 0 and tr_level=0");
			if (cardinfo != null)
			{
				string text2 = text;
				text = text2 + " And b_code='" + cardinfo[1] + "' And f_code='" + cardinfo[2] + "' And r_code='" + cardinfo[3] + "' And r_subcode=" + cardinfo[4] + " And r_cardnum=" + cardinfo[0];
				text += " And g_logout = 0 And g_loss = 0";
			}
			else
			{
				if (txtRoom.Text.Trim() != "" && txtRoom.ForeColor == Color.Black)
				{
					text = text + " And r_name = N'" + txtRoom.Text.Trim() + "'";
				}
				if (txtCN.Text.Trim() != "" && txtCN.ForeColor == Color.Black)
				{
					text = text + " And r_cardnum = " + txtCN.Text.Trim();
				}
				if (txtGuest.Text.Trim() != "" && txtGuest.ForeColor == Color.Black)
				{
					text = text + " And g_name like N'" + txtGuest.Text.Trim() + "%'";
				}
			}
			text += " Order by g_cometime desc";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			dgvGuest.DataSource = dataTable.DefaultView;
			if (dgvGuest.DataSource != null)
			{
				DataGridViewColumn dataGridViewColumn = dgvGuest.Columns["g_id"];
				DataGridViewColumn dataGridViewColumn2 = dgvGuest.Columns["team_id"];
				DataGridViewColumn dataGridViewColumn3 = dgvGuest.Columns["r_id"];
				bool flag = (dgvGuest.Columns["tr_id"].Visible = false);
				bool flag3 = (dataGridViewColumn3.Visible = flag);
				bool visible = (dataGridViewColumn2.Visible = flag3);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvGuest.Columns.Count; i++)
				{
					dgvGuest.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvGuest.Columns[i].Name];
				}
				dgvGuest.Columns["g_stayhour"].Visible = false;
				dgvGuest.AutoResizeColumns();
				if (dataTable.Rows.Count > 0)
				{
					txtRoom.Text = dataTable.Rows[0]["r_name"].ToString();
				}
			}
			if (dgvGuest.Rows.Count > 0)
			{
				panel1.SendToBack();
				splitContainer1.Enabled = true;
				dgvGuest.Rows[0].Selected = true;
				dgvGuest.CurrentCell = dgvGuest.Rows[0].Cells["g_name"];
			}
			else
			{
				splitContainer1.Enabled = false;
				splitContainer1.SendToBack();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["txtGuest"]);
		}
	}

	private void InitDgvListCol()
	{
		try
		{
			DataGridViewColumn dataGridViewColumn = dgvList.Columns["g_ID"];
			DataGridViewColumn dataGridViewColumn2 = dgvList.Columns["r_id"];
			bool flag = (dgvList.Columns["oth_ID"].Visible = false);
			bool visible = (dataGridViewColumn2.Visible = flag);
			dataGridViewColumn.Visible = visible;
			for (int i = 0; i < dgvList.Columns.Count; i++)
			{
				dgvList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvList.Columns[i].Name];
			}
			dgvList.AutoResizeColumns();
		}
		catch
		{
		}
	}

	private void CountList()
	{
		try
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			for (int i = 0; i < dgvList.Rows.Count; i++)
			{
				bool flag = (bool)dgvList.Rows[i].Cells[9].Value;
				num2 += (double)Convert.ToInt32("0" + dgvList.Rows[i].Cells[8].Value);
				if (flag)
				{
					num3 += Convert.ToDouble("0" + dgvList.Rows[i].Cells[8].Value);
				}
				else
				{
					num += Convert.ToDouble("0" + dgvList.Rows[i].Cells[10].Value);
				}
			}
			tsslabSel.Text = num2.ToString();
			tsslabGift.Text = num3.ToString();
			tsslabTotal.Text = num.ToString("F2");
		}
		catch
		{
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmOther_Load(object sender, EventArgs e)
	{
		InitType();
		InitDgvListCol();
		btnSear_Click(null, null);
	}

	private void btnSear_Click(object sender, EventArgs e)
	{
		InitItem();
	}

	private void btnRSear_Click(object sender, EventArgs e)
	{
		InitGuest(null);
	}

	private void btnRC_Click(object sender, EventArgs e)
	{
		try
		{
			object[] array = new object[256];
			int num = Program.RadioReadCard(array, Buzzer: true, 4);
			if (num >= 0)
			{
				string[] array2 = new string[num - 2];
				array2[0] = array[1].ToString();
				for (int i = 3; i < num; i++)
				{
					array2[i - 2] = (string)array[i];
				}
				InitGuest(array2);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, (string)m_htab["txtGuest"]);
		}
	}

	private void dgvItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			int rowIndex = e.RowIndex;
			if (rowIndex >= 0)
			{
				if (dgvGuest.DataSource == null || dgvGuest.CurrentRow == null)
				{
					Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Asterisk);
					return;
				}
				object[] array = new object[dgvList.Columns.Count];
				array[0] = dgvGuest.CurrentRow.Cells["g_ID"].Value;
				array[1] = dgvGuest.CurrentRow.Cells["r_id"].Value;
				array[2] = dgvGuest.CurrentRow.Cells["r_name"].Value;
				array[3] = dgvGuest.CurrentRow.Cells["g_name"].Value;
				array[4] = dgvItem.Rows[rowIndex].Cells["oth_ID"].Value;
				array[5] = dgvItem.Rows[rowIndex].Cells["oth_name"].Value;
				array[6] = dgvItem.Rows[rowIndex].Cells["oth_unit"].Value;
				array[7] = dgvItem.Rows[rowIndex].Cells["oth_price"].Value;
				array[8] = 1;
				array[9] = false;
				array[10] = Convert.ToDouble(dgvItem.Rows[rowIndex].Cells["oth_price"].Value).ToString("F2");
				array[11] = (string)m_htab["dgvcolDel"];
				array[12] = dgvGuest.CurrentRow.Cells["tr_id"].Value;
				array[13] = dgvGuest.CurrentRow.Cells["team_id"].Value;
				array[14] = dgvGuest.CurrentRow.Cells["curr_code"].Value;
				array[15] = dgvGuest.CurrentRow.Cells["curr_rate"].Value;
				dgvList.Rows.Add(array);
				dgvList.Columns[14].Visible = false;
				dgvList.Columns[15].Visible = false;
				dgvList.AutoResizeColumn(11, DataGridViewAutoSizeColumnMode.DisplayedCells);
				CountList();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)m_htab["Err01"] + "\r\n" + ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			int columnIndex = e.ColumnIndex;
			int rowIndex = e.RowIndex;
			if (columnIndex >= 0 && rowIndex >= 0)
			{
				dgvList.EndEdit();
				if (columnIndex == 9 || columnIndex == 8)
				{
					double num = Convert.ToDouble("0" + dgvList.Rows[rowIndex].Cells[7].Value.ToString());
					double num2 = Convert.ToDouble("0" + dgvList.Rows[rowIndex].Cells[8].Value.ToString());
					num = ((!(bool)dgvList.Rows[rowIndex].Cells[9].Value) ? (num * num2) : 0.0);
					dgvList.Rows[rowIndex].Cells[10].Value = num.ToString("F2");
				}
				CountList();
			}
		}
		catch
		{
		}
	}

	private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			int columnIndex = e.ColumnIndex;
			int rowIndex = e.RowIndex;
			if (columnIndex >= 0 && rowIndex >= 0)
			{
				if (columnIndex == 11)
				{
					dgvList.Rows.RemoveAt(rowIndex);
				}
				CountList();
			}
		}
		catch
		{
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.Rows == null || dgvList.Rows.Count <= 0)
			{
				return;
			}
			double num = Convert.ToDouble(tsslabTotal.Text.Trim());
			string text = tsslab03.Text + tsslabTotal.Text + Program.m_baseCurrCode + "\r\n\r\n";
			double realDisValue = Program.GetRealDisValue(txtDC.Value.ToString());
			text = text + label1.Text + Program.GetFaceDisValue(realDisValue) + "%\r\n\r\n";
			string text2 = text;
			text = text2 + (string)m_htab["Info03"] + (num * realDisValue).ToString("F2") + Program.m_baseCurrCode + "\r\n\r\n";
			frmConsumptionInfo frmConsumptionInfo2 = new frmConsumptionInfo();
			frmConsumptionInfo2.cheCanUse = Convert.ToInt32(dgvList.Rows[0].Cells["team_id"].Value) <= 0;
			frmConsumptionInfo2.basecode = Program.m_baseCurrCode;
			frmConsumptionInfo2.totalpay = num;
			frmConsumptionInfo2.dscount = realDisValue;
			frmConsumptionInfo2.ischeckcur = true;
			frmConsumptionInfo2.curcode = dgvList.Rows[0].Cells["curr_code"].Value.ToString();
			frmConsumptionInfo2.rate = Convert.ToDouble(dgvList.Rows[0].Cells["curr_rate"].Value);
			frmConsumptionInfo2.depositRemain = Program.GetRemainMoney(0, Convert.ToInt32(dgvList.Rows[0].Cells["g_ID"].Value), iscontainfuture: false);
			if (frmConsumptionInfo2.ShowDialog() != DialogResult.Cancel)
			{
				string text3 = "declare @_ID As bigint \n ";
				double num2 = 0.0;
				for (int i = 0; i < dgvList.Rows.Count; i++)
				{
					num2 = Convert.ToDouble(dgvList.Rows[i].Cells["oth_total"].Value.ToString()) * realDisValue;
					num = Convert.ToDouble(dgvList.Rows[i].Cells["oth_price"].Value.ToString()) * Convert.ToDouble(dgvList.Rows[i].Cells["Qty"].Value.ToString());
					string text4 = text3;
					text3 = text4 + "Insert Into T_otherpaid Values('" + dgvList.Rows[i].Cells["oth_ID"].Value.ToString() + "', " + Program.GetStandDec(dgvList.Rows[i].Cells["Qty"].Value.ToString());
					string text5 = text3;
					text3 = text5 + ", " + Program.GetStandDec(num) + ", " + Program.GetStandDec(realDisValue) + "," + Program.GetStandDec(num * realDisValue) + "," + (((bool)dgvList.Rows[i].Cells["Gift"].Value) ? 1 : 0);
					string text6 = text3;
					text3 = text6 + ", " + Program.GetStandDec(num2) + ", Null," + (frmConsumptionInfo2.ischeckcur ? "Null" : "0") + ", " + dgvList.Rows[i].Cells["g_ID"].Value.ToString();
					text3 += string.Format(", {0}, {1}, {2}", dgvList.Rows[i].Cells["team_id"].Value, dgvList.Rows[i].Cells["tr_id"].Value, dgvList.Rows[i].Cells["r_id"].Value);
					object obj = text3;
					text3 = string.Concat(obj, ", '', GetDate(), N'", Program.m_OperName, "', ", Program.m_opid, ", Null, Null, Null) \n ");
					text3 += "Select  @_ID = @@Identity \n ";
					text3 = text3 + "Update T_Rooms Set TR_othp_ID = Cast(IsNull(TR_othp_ID, '') As Varchar) + '#' + Cast(@_ID As varchar), TR_othprice = IsNull(TR_othprice, 0) + " + Program.GetStandDec(num2);
					object obj2 = text3;
					text3 = string.Concat(obj2, ", Updator_id = ", Program.m_opid, ", Updator=N'", Program.m_OperName, "', UpdateTime=GetDate() Where tr_id =", dgvList.Rows[i].Cells["tr_id"].Value.ToString(), " \n ");
					text3 = text3 + "Update T_Guest Set g_othp_ID = Cast(IsNull(g_othp_ID, '') As Varchar) + '#' + Cast(@_ID As varchar), g_othprice = IsNull(g_othprice,0) + " + Program.GetStandDec(num2);
					object obj3 = text3;
					text3 = string.Concat(obj3, ", Updator_id = ", Program.m_opid, ", Updator=N'", Program.m_OperName, "', UpdateTime=GetDate() Where g_id =", dgvList.Rows[i].Cells["g_ID"].Value.ToString(), " \n ");
				}
				string text7 = text3;
				text3 = text7 + "Update T_Rooms Set tr_deposit=tr_deposit+" + Program.GetStandDec(frmConsumptionInfo2.ischeckcur ? 0.0 : frmConsumptionInfo2.paidextra) + " Where tr_id =" + dgvList.Rows[0].Cells["tr_id"].Value.ToString() + " \n ";
				string text8 = text3;
				text3 = text8 + "Update T_Guest Set g_deposit=g_deposit+" + Program.GetStandDec(frmConsumptionInfo2.ischeckcur ? 0.0 : frmConsumptionInfo2.paidextra) + " where tr_id =" + dgvList.Rows[0].Cells["tr_id"].Value.ToString() + " \n ";
				int num3 = Program.DBCompExec(text3, Text);
				if (num3 < 0)
				{
					Program.MsgBox((string)m_htab["Err03"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				dgvList.Rows.Clear();
				Program.MsgCustom((string)m_htab["InfoDBOper"], MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)m_htab["Err02"] + ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void txtDC_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\b' && (e.KeyChar < '0' || e.KeyChar > '9'))
		{
			e.Handled = true;
		}
	}

	private void txtCN_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void linLDetail_Click(object sender, EventArgs e)
	{
		if (dgvGuest.Visible)
		{
			dgvGuest.Visible = false;
			dgvList.Visible = true;
			linLDetail.Text = (string)m_htab["linLDetail_1"];
		}
		else
		{
			dgvGuest.Visible = true;
			dgvList.Visible = false;
			linLDetail.Text = (string)m_htab["linLDetail_0"];
		}
	}

	private void tsslabSel_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (int.Parse(tsslabSel.Text, CultureInfo.CurrentCulture) > 0)
			{
				dgvGuest.Visible = false;
				dgvList.Visible = true;
				linLDetail.Text = (string)m_htab["linLDetail_1"];
			}
			else
			{
				dgvGuest.Visible = true;
				dgvList.Visible = false;
				linLDetail.Text = (string)m_htab["linLDetail_0"];
			}
		}
		catch
		{
		}
	}

	private void dgvGuest_SelectionChanged(object sender, EventArgs e)
	{
		try
		{
			if (dgvGuest.SelectedRows.Count <= 0)
			{
				return;
			}
			if (!(txtCerName.Text == dgvGuest.SelectedRows[0].Cells["cer_name"].Value.ToString()) || !(txtCerNum.Text == dgvGuest.SelectedRows[0].Cells["g_cernum"].Value.ToString()) || !(txtRName.Text == dgvGuest.SelectedRows[0].Cells["r_name"].Value.ToString()))
			{
				dgvList.Rows.Clear();
			}
			string text = "";
			txtRName.Text = dgvGuest.SelectedRows[0].Cells["r_name"].Value.ToString();
			txtUName.Text = dgvGuest.SelectedRows[0].Cells["g_name"].Value.ToString();
			txtCerName.Text = dgvGuest.SelectedRows[0].Cells["cer_name"].Value.ToString();
			txtCerNum.Text = dgvGuest.SelectedRows[0].Cells["g_cernum"].Value.ToString();
			txtDCUse.Text = string.Concat(dgvGuest.SelectedRows[0].Cells["curr_code"].Value, Program.GetRemainMoney(0, Convert.ToInt32(dgvGuest.SelectedRows[0].Cells["g_id"].Value), iscontainfuture: false).ToString("F2", CultureInfo.CurrentCulture));
			text = string.Concat("select oth_ID,OT_Name,oth_name,oth_price,sum(othp_qty) as Qty,oth_unit,sum(othp_total) as oth_total,othp_discount as discount,sum(othp_mpay)as mpay,othp_giving as Gift,sum(othp_apaid) as paid,(case isnull(a_id,-1000) when -1000 then N'", (string)m_htab["Info04"], "' else(case a_id when 0 then N'", (string)m_htab["Info05"], "' else '' end)end)as a_id,CreateTime from v_OtherDetails where g_id =", dgvGuest.SelectedRows[0].Cells["g_id"].Value, " group by oth_ID,OT_Name,oth_name,othp_giving,a_id,CreateTime,oth_price,oth_unit,othp_discount order by createtime asc\r");
			text = text + "select sum(othp_total),sum(case othp_giving when 1 then othp_total else 0 end),sum(othp_apaid) from v_OtherDetails where g_id =" + dgvGuest.SelectedRows[0].Cells["g_id"].Value;
			DataSet dataSet = SQLserver.Data_GetDataSet(text);
			DataTable dataTable = dataSet.Tables[0];
			DataTable dataTable2 = dataSet.Tables[1];
			dGVListHistory.DataSource = dataTable.DefaultView;
			dGVListHistory.ReadOnly = true;
			if (dGVListHistory.DataSource != null)
			{
				for (int i = 0; i < dGVListHistory.Columns.Count; i++)
				{
					dGVListHistory.Columns[i].HeaderText = (string)m_htab["dgvcol" + dGVListHistory.Columns[i].Name];
				}
				dGVListHistory.AutoResizeColumns();
			}
			if (dataTable2 != null && dataTable2.Rows.Count == 1)
			{
				tSSLTotalNum.Text = dataTable2.Rows[0][0].ToString();
				tSSLTotalGift.Text = dataTable2.Rows[0][1].ToString();
				tSSLTotalPrice.Text = dataTable2.Rows[0][2].ToString();
			}
		}
		catch
		{
		}
	}

	private void gBtnSearch_Click(object sender, EventArgs e)
	{
		splitContainer1.Enabled = false;
		splitContainer1.SendToBack();
	}

	private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dgvList.Columns[e.ColumnIndex].Name == "Qty")
		{
			int result = 0;
			if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || !int.TryParse(dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out result) || result < 1)
			{
				dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
			}
			else
			{
				dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = result;
			}
			dgvList_CellValueChanged(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmOther));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.dgvItem = new System.Windows.Forms.DataGridView();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.g_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.r_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.r_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.g_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.oth_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.oth_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.oth_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.oth_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Gift = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.oth_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.oth_del = new System.Windows.Forms.DataGridViewLinkColumn();
		this.tr_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.team_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.curr_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.curr_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvGuest = new System.Windows.Forms.DataGridView();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.tsslab01 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslabSel = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslabGift = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslab03 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslabTotal = new System.Windows.Forms.ToolStripStatusLabel();
		this.dGVListHistory = new System.Windows.Forms.DataGridView();
		this.staSHistory = new System.Windows.Forms.StatusStrip();
		this.tSSLNum = new System.Windows.Forms.ToolStripStatusLabel();
		this.tSSLTotalNum = new System.Windows.Forms.ToolStripStatusLabel();
		this.tSSLGift = new System.Windows.Forms.ToolStripStatusLabel();
		this.tSSLTotalGift = new System.Windows.Forms.ToolStripStatusLabel();
		this.tSSLTotal = new System.Windows.Forms.ToolStripStatusLabel();
		this.tSSLTotalPrice = new System.Windows.Forms.ToolStripStatusLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtCN = new System.Windows.Forms.TextBox();
		this.txtGuest = new System.Windows.Forms.TextBox();
		this.txtRoom = new System.Windows.Forms.TextBox();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.txtIN = new System.Windows.Forms.TextBox();
		this.txtIID = new System.Windows.Forms.TextBox();
		this.cobOthType = new System.Windows.Forms.ComboBox();
		this.btnSear = new LockSoftware.Controls.ToolsBtn(this.components);
		this.clsBackPanel3 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.LabHistory = new System.Windows.Forms.Label();
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.label2 = new System.Windows.Forms.Label();
		this.txtDC = new System.Windows.Forms.NumericUpDown();
		this.label1 = new System.Windows.Forms.Label();
		this.btnRC = new LockSoftware.Controls.GlassBtn(this.components);
		this.gBtnSearch = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.panGuestInfo = new System.Windows.Forms.Panel();
		this.txtDCUse = new System.Windows.Forms.Label();
		this.txtCerNum = new System.Windows.Forms.Label();
		this.txtCerName = new System.Windows.Forms.Label();
		this.txtUName = new System.Windows.Forms.Label();
		this.txtRName = new System.Windows.Forms.Label();
		this.linLDetail = new System.Windows.Forms.LinkLabel();
		this.labDCUse = new System.Windows.Forms.Label();
		this.labCerNum = new System.Windows.Forms.Label();
		this.labCerType = new System.Windows.Forms.Label();
		this.labUName = new System.Windows.Forms.Label();
		this.labRName = new System.Windows.Forms.Label();
		this.btnRSear = new LockSoftware.Controls.ToolsBtn(this.components);
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvItem).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvGuest).BeginInit();
		this.statusStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dGVListHistory).BeginInit();
		this.staSHistory.SuspendLayout();
		this.panel1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		this.clsBackPanel3.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtDC).BeginInit();
		this.clsBackPanel2.SuspendLayout();
		this.panGuestInfo.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.dgvItem);
		this.splitContainer1.Panel1.Controls.Add(this.clsBackPanel1);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
		this.splitContainer1.Panel2.Controls.Add(this.clsBackPanel2);
		this.splitContainer1.Size = new System.Drawing.Size(884, 562);
		this.splitContainer1.SplitterDistance = 367;
		this.splitContainer1.SplitterWidth = 3;
		this.splitContainer1.TabIndex = 0;
		this.dgvItem.AllowUserToAddRows = false;
		this.dgvItem.AllowUserToDeleteRows = false;
		this.dgvItem.BackgroundColor = System.Drawing.Color.White;
		this.dgvItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvItem.Location = new System.Drawing.Point(0, 66);
		this.dgvItem.Name = "dgvItem";
		this.dgvItem.ReadOnly = true;
		this.dgvItem.RowHeadersVisible = false;
		this.dgvItem.RowTemplate.Height = 23;
		this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvItem.Size = new System.Drawing.Size(367, 496);
		this.dgvItem.TabIndex = 1;
		this.dgvItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvItem_CellDoubleClick);
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 66);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.dgvList);
		this.splitContainer2.Panel1.Controls.Add(this.dgvGuest);
		this.splitContainer2.Panel1.Controls.Add(this.statusStrip1);
		this.splitContainer2.Panel1.Controls.Add(this.clsBackPanel3);
		this.splitContainer2.Panel2.Controls.Add(this.dGVListHistory);
		this.splitContainer2.Panel2.Controls.Add(this.staSHistory);
		this.splitContainer2.Size = new System.Drawing.Size(514, 496);
		this.splitContainer2.SplitterDistance = 255;
		this.splitContainer2.SplitterWidth = 3;
		this.splitContainer2.TabIndex = 2;
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Columns.AddRange(this.g_ID, this.r_id, this.r_name, this.g_name, this.oth_ID, this.oth_name, this.oth_unit, this.oth_price, this.Qty, this.Gift, this.oth_total, this.oth_del, this.tr_id, this.team_id, this.curr_code, this.curr_rate);
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvList.Location = new System.Drawing.Point(0, 0);
		this.dgvList.MultiSelect = false;
		this.dgvList.Name = "dgvList";
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(514, 187);
		this.dgvList.TabIndex = 10;
		this.dgvList.Visible = false;
		this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellContentClick);
		this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellEndEdit);
		this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellValueChanged);
		this.g_ID.HeaderText = "Column1";
		this.g_ID.Name = "g_ID";
		this.g_ID.ReadOnly = true;
		this.g_ID.Visible = false;
		this.r_id.HeaderText = "Column1";
		this.r_id.Name = "r_id";
		this.r_id.ReadOnly = true;
		this.r_id.Visible = false;
		this.r_name.HeaderText = "Column1";
		this.r_name.Name = "r_name";
		this.r_name.ReadOnly = true;
		this.g_name.HeaderText = "Column1";
		this.g_name.Name = "g_name";
		this.g_name.ReadOnly = true;
		this.oth_ID.HeaderText = "Column1";
		this.oth_ID.Name = "oth_ID";
		this.oth_ID.ReadOnly = true;
		this.oth_ID.Visible = false;
		this.oth_name.HeaderText = "Column1";
		this.oth_name.Name = "oth_name";
		this.oth_name.ReadOnly = true;
		this.oth_unit.HeaderText = "Column1";
		this.oth_unit.Name = "oth_unit";
		this.oth_unit.ReadOnly = true;
		this.oth_price.HeaderText = "Column1";
		this.oth_price.Name = "oth_price";
		this.oth_price.ReadOnly = true;
		this.Qty.HeaderText = "Column1";
		this.Qty.MaxInputLength = 8;
		this.Qty.Name = "Qty";
		this.Gift.HeaderText = "Column1";
		this.Gift.Name = "Gift";
		this.oth_total.HeaderText = "Column1";
		this.oth_total.Name = "oth_total";
		this.oth_total.ReadOnly = true;
		this.oth_del.ActiveLinkColor = System.Drawing.Color.White;
		this.oth_del.HeaderText = "Column1";
		this.oth_del.LinkColor = System.Drawing.Color.Red;
		this.oth_del.Name = "oth_del";
		this.oth_del.VisitedLinkColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.tr_id.HeaderText = "Column1";
		this.tr_id.Name = "tr_id";
		this.tr_id.ReadOnly = true;
		this.tr_id.Visible = false;
		this.team_id.HeaderText = "Column1";
		this.team_id.Name = "team_id";
		this.team_id.ReadOnly = true;
		this.team_id.Visible = false;
		this.curr_code.HeaderText = "Column1";
		this.curr_code.Name = "curr_code";
		this.curr_code.ReadOnly = true;
		this.curr_rate.HeaderText = "Column2";
		this.curr_rate.Name = "curr_rate";
		this.curr_rate.ReadOnly = true;
		this.dgvGuest.AllowUserToAddRows = false;
		this.dgvGuest.AllowUserToDeleteRows = false;
		this.dgvGuest.BackgroundColor = System.Drawing.Color.White;
		this.dgvGuest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvGuest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvGuest.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvGuest.Location = new System.Drawing.Point(0, 0);
		this.dgvGuest.MultiSelect = false;
		this.dgvGuest.Name = "dgvGuest";
		this.dgvGuest.ReadOnly = true;
		this.dgvGuest.RowHeadersVisible = false;
		this.dgvGuest.RowTemplate.Height = 23;
		this.dgvGuest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvGuest.Size = new System.Drawing.Size(514, 187);
		this.dgvGuest.TabIndex = 9;
		this.dgvGuest.SelectionChanged += new System.EventHandler(dgvGuest_SelectionChanged);
		this.statusStrip1.AutoSize = false;
		this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
		this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.tsslab01, this.tsslabSel, this.tsslab02, this.tsslabGift, this.tsslab03, this.tsslabTotal });
		this.statusStrip1.Location = new System.Drawing.Point(0, 187);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(514, 28);
		this.statusStrip1.TabIndex = 7;
		this.tsslab01.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tsslab01.Name = "tsslab01";
		this.tsslab01.Size = new System.Drawing.Size(63, 23);
		this.tsslab01.Text = "Selected:";
		this.tsslabSel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.tsslabSel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tsslabSel.Name = "tsslabSel";
		this.tsslabSel.Size = new System.Drawing.Size(119, 23);
		this.tsslabSel.Spring = true;
		this.tsslabSel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tsslabSel.TextChanged += new System.EventHandler(tsslabSel_TextChanged);
		this.tsslab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tsslab02.Name = "tsslab02";
		this.tsslab02.Size = new System.Drawing.Size(34, 23);
		this.tsslab02.Text = "Gift:";
		this.tsslabGift.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.tsslabGift.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tsslabGift.Name = "tsslabGift";
		this.tsslabGift.Size = new System.Drawing.Size(119, 23);
		this.tsslabGift.Spring = true;
		this.tsslabGift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tsslab03.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tsslab03.Name = "tsslab03";
		this.tsslab03.Size = new System.Drawing.Size(43, 23);
		this.tsslab03.Text = "Total:";
		this.tsslabTotal.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.tsslabTotal.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tsslabTotal.Name = "tsslabTotal";
		this.tsslabTotal.Size = new System.Drawing.Size(119, 23);
		this.tsslabTotal.Spring = true;
		this.tsslabTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.dGVListHistory.AllowUserToAddRows = false;
		this.dGVListHistory.AllowUserToDeleteRows = false;
		this.dGVListHistory.BackgroundColor = System.Drawing.Color.White;
		this.dGVListHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dGVListHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dGVListHistory.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dGVListHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dGVListHistory.Location = new System.Drawing.Point(0, 0);
		this.dGVListHistory.MultiSelect = false;
		this.dGVListHistory.Name = "dGVListHistory";
		this.dGVListHistory.RowHeadersWidth = 25;
		this.dGVListHistory.RowTemplate.Height = 23;
		this.dGVListHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dGVListHistory.Size = new System.Drawing.Size(514, 210);
		this.dGVListHistory.TabIndex = 6;
		this.staSHistory.AutoSize = false;
		this.staSHistory.BackColor = System.Drawing.Color.Transparent;
		this.staSHistory.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.staSHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.tSSLNum, this.tSSLTotalNum, this.tSSLGift, this.tSSLTotalGift, this.tSSLTotal, this.tSSLTotalPrice });
		this.staSHistory.Location = new System.Drawing.Point(0, 210);
		this.staSHistory.Name = "staSHistory";
		this.staSHistory.Size = new System.Drawing.Size(514, 28);
		this.staSHistory.TabIndex = 5;
		this.tSSLNum.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tSSLNum.Name = "tSSLNum";
		this.tSSLNum.Size = new System.Drawing.Size(39, 23);
		this.tSSLNum.Text = "总数:";
		this.tSSLTotalNum.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.tSSLTotalNum.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tSSLTotalNum.Name = "tSSLTotalNum";
		this.tSSLTotalNum.Size = new System.Drawing.Size(127, 23);
		this.tSSLTotalNum.Spring = true;
		this.tSSLTotalNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tSSLGift.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tSSLGift.Name = "tSSLGift";
		this.tSSLGift.Size = new System.Drawing.Size(34, 23);
		this.tSSLGift.Text = "Gift:";
		this.tSSLTotalGift.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.tSSLTotalGift.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tSSLTotalGift.Name = "tSSLTotalGift";
		this.tSSLTotalGift.Size = new System.Drawing.Size(127, 23);
		this.tSSLTotalGift.Spring = true;
		this.tSSLTotalGift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tSSLTotal.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.tSSLTotal.Name = "tSSLTotal";
		this.tSSLTotal.Size = new System.Drawing.Size(43, 23);
		this.tSSLTotal.Text = "Total:";
		this.tSSLTotalPrice.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.tSSLTotalPrice.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.tSSLTotalPrice.Name = "tSSLTotalPrice";
		this.tSSLTotalPrice.Size = new System.Drawing.Size(127, 23);
		this.tSSLTotalPrice.Spring = true;
		this.tSSLTotalPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.btnRSear);
		this.panel1.Controls.Add(this.txtCN);
		this.panel1.Controls.Add(this.txtGuest);
		this.panel1.Controls.Add(this.txtRoom);
		this.panel1.Location = new System.Drawing.Point(360, 180);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(219, 100);
		this.panel1.TabIndex = 1;
		this.txtCN.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtCN.ForeColor = System.Drawing.Color.DarkGray;
		this.txtCN.Location = new System.Drawing.Point(114, 24);
		this.txtCN.Name = "txtCN";
		this.txtCN.Size = new System.Drawing.Size(90, 22);
		this.txtCN.TabIndex = 4;
		this.txtCN.Enter += new System.EventHandler(txtCN_Enter);
		this.txtCN.Leave += new System.EventHandler(txtCN_Leave);
		this.txtGuest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtGuest.ForeColor = System.Drawing.Color.DarkGray;
		this.txtGuest.Location = new System.Drawing.Point(16, 56);
		this.txtGuest.Name = "txtGuest";
		this.txtGuest.Size = new System.Drawing.Size(90, 22);
		this.txtGuest.TabIndex = 5;
		this.txtGuest.Enter += new System.EventHandler(txtGuest_Enter);
		this.txtGuest.Leave += new System.EventHandler(txtGuest_Leave);
		this.txtRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtRoom.ForeColor = System.Drawing.Color.DarkGray;
		this.txtRoom.Location = new System.Drawing.Point(16, 24);
		this.txtRoom.Name = "txtRoom";
		this.txtRoom.Size = new System.Drawing.Size(90, 22);
		this.txtRoom.TabIndex = 3;
		this.txtRoom.Enter += new System.EventHandler(txtRoom_Enter);
		this.txtRoom.Leave += new System.EventHandler(txtRoom_Leave);
		this.clsBackPanel1.Border = true;
		this.clsBackPanel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderBW = 0;
		this.clsBackPanel1.BorderColorBottom = System.Drawing.Color.YellowGreen;
		this.clsBackPanel1.BorderColorLeft = System.Drawing.Color.YellowGreen;
		this.clsBackPanel1.BorderColorRight = System.Drawing.Color.YellowGreen;
		this.clsBackPanel1.BorderColorTop = System.Drawing.Color.YellowGreen;
		this.clsBackPanel1.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderLW = 1;
		this.clsBackPanel1.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderRW = 1;
		this.clsBackPanel1.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderTW = 1;
		this.clsBackPanel1.Color1 = System.Drawing.Color.White;
		this.clsBackPanel1.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.txtIN);
		this.clsBackPanel1.Controls.Add(this.txtIID);
		this.clsBackPanel1.Controls.Add(this.cobOthType);
		this.clsBackPanel1.Controls.Add(this.btnSear);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(367, 66);
		this.clsBackPanel1.TabIndex = 0;
		this.txtIN.ForeColor = System.Drawing.Color.DarkGray;
		this.txtIN.Location = new System.Drawing.Point(207, 22);
		this.txtIN.Name = "txtIN";
		this.txtIN.Size = new System.Drawing.Size(90, 22);
		this.txtIN.TabIndex = 2;
		this.txtIN.Enter += new System.EventHandler(txtIN_Enter);
		this.txtIN.Leave += new System.EventHandler(txtIN_Leave);
		this.txtIID.ForeColor = System.Drawing.Color.DarkGray;
		this.txtIID.Location = new System.Drawing.Point(111, 22);
		this.txtIID.Name = "txtIID";
		this.txtIID.Size = new System.Drawing.Size(90, 22);
		this.txtIID.TabIndex = 1;
		this.txtIID.Enter += new System.EventHandler(txtIID_Enter);
		this.txtIID.Leave += new System.EventHandler(txtIID_Leave);
		this.cobOthType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobOthType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobOthType.DropDownWidth = 180;
		this.cobOthType.ForeColor = System.Drawing.Color.DarkGray;
		this.cobOthType.FormattingEnabled = true;
		this.cobOthType.Location = new System.Drawing.Point(5, 22);
		this.cobOthType.Name = "cobOthType";
		this.cobOthType.Size = new System.Drawing.Size(100, 22);
		this.cobOthType.TabIndex = 0;
		this.cobOthType.Enter += new System.EventHandler(cobOthType_Enter);
		this.cobOthType.Leave += new System.EventHandler(cobOthType_Leave);
		this.btnSear.BackColor = System.Drawing.Color.Transparent;
		this.btnSear.Checked = false;
		this.btnSear.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnSear.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnSear.DefaultColor = System.Drawing.Color.Transparent;
		this.btnSear.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSear.ImageNew = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnSear.ImageRedrawed = true;
		this.btnSear.ImageStyle = 0;
		this.btnSear.isButton = true;
		this.btnSear.Location = new System.Drawing.Point(0, 0);
		this.btnSear.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnSear.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnSear.MouseDownStartColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnSear.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnSear.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnSear.Name = "btnSear";
		this.btnSear.Size = new System.Drawing.Size(367, 66);
		this.btnSear.TabIndex = 4;
		this.btnSear.TextImageLocation = 0;
		this.btnSear.TextNew = "";
		this.btnSear.TextRedrawed = false;
		this.btnSear.Click += new System.EventHandler(btnSear_Click);
		this.clsBackPanel3.Border = true;
		this.clsBackPanel3.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderBW = 0;
		this.clsBackPanel3.BorderColorBottom = System.Drawing.Color.YellowGreen;
		this.clsBackPanel3.BorderColorLeft = System.Drawing.Color.YellowGreen;
		this.clsBackPanel3.BorderColorRight = System.Drawing.Color.YellowGreen;
		this.clsBackPanel3.BorderColorTop = System.Drawing.Color.YellowGreen;
		this.clsBackPanel3.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderLW = 1;
		this.clsBackPanel3.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderRW = 1;
		this.clsBackPanel3.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderTW = 1;
		this.clsBackPanel3.Color1 = System.Drawing.Color.White;
		this.clsBackPanel3.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel3.ColorAngle = 90f;
		this.clsBackPanel3.Controls.Add(this.flowLayoutPanel1);
		this.clsBackPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel3.Location = new System.Drawing.Point(0, 215);
		this.clsBackPanel3.Name = "clsBackPanel3";
		this.clsBackPanel3.Size = new System.Drawing.Size(514, 40);
		this.clsBackPanel3.TabIndex = 6;
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel1.Controls.Add(this.LabHistory);
		this.flowLayoutPanel1.Controls.Add(this.btnOK);
		this.flowLayoutPanel1.Controls.Add(this.label2);
		this.flowLayoutPanel1.Controls.Add(this.txtDC);
		this.flowLayoutPanel1.Controls.Add(this.label1);
		this.flowLayoutPanel1.Controls.Add(this.btnRC);
		this.flowLayoutPanel1.Controls.Add(this.gBtnSearch);
		this.flowLayoutPanel1.Controls.Add(this.btnClose);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.flowLayoutPanel1.Size = new System.Drawing.Size(514, 40);
		this.flowLayoutPanel1.TabIndex = 7;
		this.LabHistory.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.LabHistory.ForeColor = System.Drawing.Color.Blue;
		this.LabHistory.Location = new System.Drawing.Point(431, 8);
		this.LabHistory.Name = "LabHistory";
		this.LabHistory.Size = new System.Drawing.Size(80, 28);
		this.LabHistory.TabIndex = 10;
		this.LabHistory.TabStop = true;
		this.LabHistory.Text = "↓历史消费";
		this.LabHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.BackColor = System.Drawing.Color.Gainsboro;
		this.btnOK.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(342, 3);
		this.btnOK.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnOK.Size = new System.Drawing.Size(86, 30);
		this.btnOK.TabIndex = 8;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.label2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.Red;
		this.label2.Location = new System.Drawing.Point(318, 12);
		this.label2.Margin = new System.Windows.Forms.Padding(1, 12, 0, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(20, 14);
		this.label2.TabIndex = 9;
		this.label2.Text = "%";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.txtDC.Location = new System.Drawing.Point(275, 8);
		this.txtDC.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.txtDC.Name = "txtDC";
		this.txtDC.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtDC.Size = new System.Drawing.Size(43, 22);
		this.txtDC.TabIndex = 12;
		this.txtDC.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.txtDC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDC_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(213, 12);
		this.label1.Margin = new System.Windows.Forms.Padding(0, 12, 1, 0);
		this.label1.Name = "label1";
		this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label1.Size = new System.Drawing.Size(62, 14);
		this.label1.TabIndex = 0;
		this.label1.Text = "Discount :";
		this.btnRC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRC.BackColor = System.Drawing.Color.Gainsboro;
		this.btnRC.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRC.ForeColor = System.Drawing.Color.Black;
		this.btnRC.GlowColor = System.Drawing.Color.White;
		this.btnRC.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRC.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnRC.Location = new System.Drawing.Point(126, 3);
		this.btnRC.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		this.btnRC.Name = "btnRC";
		this.btnRC.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnRC.Size = new System.Drawing.Size(86, 30);
		this.btnRC.TabIndex = 4;
		this.btnRC.Text = "Read";
		this.btnRC.Click += new System.EventHandler(btnRC_Click);
		this.gBtnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gBtnSearch.BackColor = System.Drawing.Color.Gainsboro;
		this.gBtnSearch.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.gBtnSearch.ForeColor = System.Drawing.Color.Black;
		this.gBtnSearch.GlowColor = System.Drawing.Color.White;
		this.gBtnSearch.GuidInfo = "&56~01'][Manson]v%#@";
		this.gBtnSearch.Image = LockSoftware.Properties.Resources.search;
		this.gBtnSearch.InnerBorderColor = System.Drawing.Color.DimGray;
		this.gBtnSearch.Location = new System.Drawing.Point(93, 3);
		this.gBtnSearch.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		this.gBtnSearch.Name = "gBtnSearch";
		this.gBtnSearch.OuterBorderColor = System.Drawing.Color.LightGray;
		this.gBtnSearch.Size = new System.Drawing.Size(30, 30);
		this.gBtnSearch.TabIndex = 11;
		this.gBtnSearch.Click += new System.EventHandler(gBtnSearch_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(4, 3);
		this.btnClose.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnClose.Size = new System.Drawing.Size(86, 30);
		this.btnClose.TabIndex = 6;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Visible = false;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.clsBackPanel2.Border = true;
		this.clsBackPanel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderBW = 0;
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
		this.clsBackPanel2.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.panGuestInfo);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(514, 66);
		this.clsBackPanel2.TabIndex = 0;
		this.panGuestInfo.BackColor = System.Drawing.Color.Transparent;
		this.panGuestInfo.Controls.Add(this.txtDCUse);
		this.panGuestInfo.Controls.Add(this.txtCerNum);
		this.panGuestInfo.Controls.Add(this.txtCerName);
		this.panGuestInfo.Controls.Add(this.txtUName);
		this.panGuestInfo.Controls.Add(this.txtRName);
		this.panGuestInfo.Controls.Add(this.linLDetail);
		this.panGuestInfo.Controls.Add(this.labDCUse);
		this.panGuestInfo.Controls.Add(this.labCerNum);
		this.panGuestInfo.Controls.Add(this.labCerType);
		this.panGuestInfo.Controls.Add(this.labUName);
		this.panGuestInfo.Controls.Add(this.labRName);
		this.panGuestInfo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panGuestInfo.Location = new System.Drawing.Point(0, 0);
		this.panGuestInfo.Name = "panGuestInfo";
		this.panGuestInfo.Size = new System.Drawing.Size(514, 66);
		this.panGuestInfo.TabIndex = 5;
		this.txtDCUse.AutoSize = true;
		this.txtDCUse.Location = new System.Drawing.Point(350, 7);
		this.txtDCUse.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.txtDCUse.Name = "txtDCUse";
		this.txtDCUse.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtDCUse.Size = new System.Drawing.Size(0, 14);
		this.txtDCUse.TabIndex = 12;
		this.txtCerNum.AutoSize = true;
		this.txtCerNum.Location = new System.Drawing.Point(208, 39);
		this.txtCerNum.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.txtCerNum.Name = "txtCerNum";
		this.txtCerNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCerNum.Size = new System.Drawing.Size(0, 14);
		this.txtCerNum.TabIndex = 11;
		this.txtCerName.AutoSize = true;
		this.txtCerName.Location = new System.Drawing.Point(208, 7);
		this.txtCerName.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.txtCerName.Name = "txtCerName";
		this.txtCerName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtCerName.Size = new System.Drawing.Size(0, 14);
		this.txtCerName.TabIndex = 10;
		this.txtUName.AutoSize = true;
		this.txtUName.Location = new System.Drawing.Point(71, 39);
		this.txtUName.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.txtUName.Name = "txtUName";
		this.txtUName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtUName.Size = new System.Drawing.Size(0, 14);
		this.txtUName.TabIndex = 9;
		this.txtRName.AutoSize = true;
		this.txtRName.Location = new System.Drawing.Point(71, 7);
		this.txtRName.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.txtRName.Name = "txtRName";
		this.txtRName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.txtRName.Size = new System.Drawing.Size(0, 14);
		this.txtRName.TabIndex = 8;
		this.linLDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.linLDetail.Location = new System.Drawing.Point(425, 38);
		this.linLDetail.Name = "linLDetail";
		this.linLDetail.Size = new System.Drawing.Size(88, 28);
		this.linLDetail.TabIndex = 7;
		this.linLDetail.TabStop = true;
		this.linLDetail.Text = "顾客信息↓";
		this.linLDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.linLDetail.Click += new System.EventHandler(linLDetail_Click);
		this.labDCUse.Location = new System.Drawing.Point(278, 0);
		this.labDCUse.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.labDCUse.Name = "labDCUse";
		this.labDCUse.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labDCUse.Size = new System.Drawing.Size(72, 28);
		this.labDCUse.TabIndex = 5;
		this.labDCUse.Text = "可用押金：";
		this.labDCUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCerNum.Location = new System.Drawing.Point(136, 32);
		this.labCerNum.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.labCerNum.Name = "labCerNum";
		this.labCerNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labCerNum.Size = new System.Drawing.Size(72, 28);
		this.labCerNum.TabIndex = 4;
		this.labCerNum.Text = "号码：";
		this.labCerNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCerType.Location = new System.Drawing.Point(132, 0);
		this.labCerType.Margin = new System.Windows.Forms.Padding(0, 12, 3, 0);
		this.labCerType.Name = "labCerType";
		this.labCerType.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labCerType.Size = new System.Drawing.Size(76, 28);
		this.labCerType.TabIndex = 3;
		this.labCerType.Text = "证件类型：";
		this.labCerType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labUName.Location = new System.Drawing.Point(0, 32);
		this.labUName.Margin = new System.Windows.Forms.Padding(0);
		this.labUName.Name = "labUName";
		this.labUName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labUName.Size = new System.Drawing.Size(72, 28);
		this.labUName.TabIndex = 2;
		this.labUName.Text = "顾客姓名：";
		this.labUName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labRName.Location = new System.Drawing.Point(0, 0);
		this.labRName.Margin = new System.Windows.Forms.Padding(0);
		this.labRName.Name = "labRName";
		this.labRName.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.labRName.Size = new System.Drawing.Size(72, 28);
		this.labRName.TabIndex = 1;
		this.labRName.Text = "客房名称：";
		this.labRName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRSear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRSear.BackColor = System.Drawing.Color.Transparent;
		this.btnRSear.Checked = false;
		this.btnRSear.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnRSear.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRSear.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRSear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRSear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRSear.ImageNew = LockSoftware.Properties.Resources.Toolbar_Find;
		this.btnRSear.ImageRedrawed = true;
		this.btnRSear.ImageStyle = 0;
		this.btnRSear.isButton = true;
		this.btnRSear.Location = new System.Drawing.Point(167, 49);
		this.btnRSear.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRSear.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnRSear.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRSear.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRSear.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRSear.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRSear.Name = "btnRSear";
		this.btnRSear.Size = new System.Drawing.Size(37, 40);
		this.btnRSear.TabIndex = 6;
		this.btnRSear.TextImageLocation = 0;
		this.btnRSear.TextNew = "";
		this.btnRSear.TextRedrawed = false;
		this.btnRSear.Click += new System.EventHandler(btnRSear_Click);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(884, 562);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.MinimumSize = new System.Drawing.Size(900, 38);
		base.Name = "frmOther";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmOther";
		base.Load += new System.EventHandler(frmOther_Load);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvItem).EndInit();
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvGuest).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dGVListHistory).EndInit();
		this.staSHistory.ResumeLayout(false);
		this.staSHistory.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		this.clsBackPanel3.ResumeLayout(false);
		this.clsBackPanel3.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtDC).EndInit();
		this.clsBackPanel2.ResumeLayout(false);
		this.panGuestInfo.ResumeLayout(false);
		this.panGuestInfo.PerformLayout();
		base.ResumeLayout(false);
	}
}
