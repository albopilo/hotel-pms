using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmRooms : Form
{
	public string m_objName = "WFr";

	public Hashtable m_htab;

	private bool m_Init = true;

	private int cobStatus_SelectedValue;

	private IContainer components;

	private ToolsBtn toolsBtn1;

	private NGlassBtn btnClose;

	private clsBackPanel plMain;

	private clsBackPanel clsBackPanel2;

	private CheckBox chkSDis;

	private ToolsBtn btnRef;

	private TreeView tvList;

	private SplitContainer splitContainer1;

	private DataGridView dgvList;

	private clsBackPanel clsBackPanel1;

	private ImageList imgListTV;

	private ToolsBtn btnRefR;

	private ToolsBtn btnDel;

	private ToolsBtn btnSave;

	private GroupBox grpRoom;

	private Label label3;

	private Label label2;

	private Label label1;

	private ComboBox cobStatus;

	private ComboBox cobType;

	private TextBox txtFName;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private TextBox txtMemo;

	private TextBox txtSize;

	private TextBox txtBPirce;

	private TextBox txtSubCode;

	private TextBox txtRCode;

	private TextBox txtRName;

	private Panel panel2;

	private GlassBtn btnNew;

	private GlassBtn btnRModify;

	private ToolsBtn toolsBtn2;

	private Label label10;

	private clsBackPanel cbpline01;

	private TextBox txtQty;

	private TextBox txtRFn;

	private Label label12;

	private Label label11;

	private Panel panel3;

	private Panel panel1;

	private TextBox txtFID;

	private Label label17;

	private TextBox txtBACount;

	private TextBox txtRFc;

	private TextBox txtRID;

	private GlassBtn btnAReset;

	private GlassBtn btnANew;

	private GroupBox grpSO;

	private RadioButton rbBehind;

	private RadioButton rbFore;

	private TextBox txtSC;

	private Label label14;

	private clsBackPanel clsBackPanel3;

	private Label label13;

	private CheckBox chk7m;

	private CheckBox chk7l;

	private CheckBox chk4m;

	private CheckBox chk4l;

	private Label label18;

	private Label label19;

	private TextBox txtFCL;

	private Label label20;

	private FlowLayoutPanel flowLayoutPanel1;

	private ToolsBtn btnRes;

	private TextBox label16;

	private TextBox label15;

	public frmRooms()
	{
		InitializeComponent();
		base.WindowState = FormWindowState.Maximized;
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void InitTreeList()
	{
		try
		{
			tvList.Nodes.Clear();
			string text = "Select B_ID, B_HotelName,Build_ID,Build_Code, Build_Name, IsNull(Build_Flag,0) As Build_Flag, Build_Memo, Floor_ID, Floor_Code, Floor_Name, IsNull(Floor_Flag,0) As Floor_Flag, Floor_Memo From v_HotelBF";
			text += " Where 1=1 And IsNull(Floor_Flag,0) = 0 And IsNull(Build_Flag,0) = 0";
			text += " Order by B_ID, Build_ID, Floor_ID ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			TreeNode treeNode = null;
			TreeNode treeNode2 = null;
			string text3;
			string text2 = (text3 = "");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (text2 != dataTable.Rows[i]["B_HotelName"].ToString().Trim())
				{
					text2 = dataTable.Rows[i]["B_HotelName"].ToString().Trim();
					treeNode = new TreeNode(text2, 0, 2);
					treeNode.Name = dataTable.Rows[i]["B_ID"].ToString().Trim();
					tvList.Nodes.Add(treeNode);
				}
				if (!Convert.ToBoolean(dataTable.Rows[i]["Build_Flag"].ToString()) || chkSDis.Checked)
				{
					if (text3 != dataTable.Rows[i]["Build_Name"].ToString().Trim())
					{
						text3 = dataTable.Rows[i]["Build_Name"].ToString().Trim();
						treeNode2 = new TreeNode(text3, 1, 2);
						treeNode2.Name = dataTable.Rows[i]["Build_ID"].ToString().Trim();
						treeNode.Nodes.Add(treeNode2);
					}
					if ((!Convert.ToBoolean(dataTable.Rows[i]["Floor_Flag"].ToString()) || chkSDis.Checked) && dataTable.Rows[i]["Floor_Name"].ToString().Trim() != "")
					{
						treeNode2?.Nodes.Add(dataTable.Rows[i]["Floor_ID"].ToString().Trim(), dataTable.Rows[i]["Floor_Name"].ToString().Trim(), 1, 2);
					}
				}
			}
			tvList.ExpandAll();
			tvList.Select();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
				cobType.DisplayMember = "TP_Name";
				cobType.ValueMember = "TP_ID";
				cobType.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitStatus(string idExtra)
	{
		try
		{
			cobStatus.DataSource = null;
			string sql = "Select RS_ID, RS_Name000 From  D_RoomStatus Where RS_ID in (1,2,7,8,9,4" + ((idExtra == null) ? "" : idExtra) + ") Order by RS_ID, RS_Name000 ";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				cobStatus.DisplayMember = "RS_Name000";
				cobStatus.ValueMember = "RS_ID";
				cobStatus.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err03"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitRoomList(TreeNode selNode)
	{
		m_Init = true;
		if (selNode == null)
		{
			return;
		}
		dgvList.DataSource = null;
		string text = "Select Cast(0 As bit) R_Edit, R_ID, R_Name, R_Code, R_SubCode, R_FloorID, R_TypeID, R_RSID, R_BedAdd, R_BedSinglePrice, R_Size, R_Memo,  Build_Name, Floor_Name, TP_Name, R_flag From v_HotelRooms Where 1=1 And IsNull(Floor_Flag,0) = 0 And IsNull(Build_Flag,0) = 0 ";
		if (selNode.Level == 2)
		{
			text = text + " And  R_FloorID=" + selNode.Name.ToString().Trim();
		}
		else if (selNode.Level == 1)
		{
			text = text + " And  Build_ID=" + selNode.Name.ToString().Trim();
		}
		else
		{
			if (selNode.Level != 0)
			{
				return;
			}
			text = text + " And B_ID=" + selNode.Name.ToString().Trim();
		}
		if (!chkSDis.Checked)
		{
			text += " And R_flag = 0";
		}
		text += " Order by Build_Name, Floor_Name, R_Name";
		DataTable dataTable = SQLserver.Data_GetDataTable(text);
		byte b = 1;
		bool flag = false;
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			dgvList.DataSource = dataTable.DefaultView;
			if (dgvList.DataSource != null)
			{
				if (dgvList.Columns["R_Edit"] == null)
				{
					return;
				}
				dgvList.Columns["R_Edit"].Visible = false;
				DataGridViewColumn dataGridViewColumn = dgvList.Columns["R_BedAdd"];
				DataGridViewColumn dataGridViewColumn2 = dgvList.Columns["R_BedSinglePrice"];
				DataGridViewColumn dataGridViewColumn3 = dgvList.Columns["R_FloorID"];
				DataGridViewColumn dataGridViewColumn4 = dgvList.Columns["R_TypeID"];
				DataGridViewColumn dataGridViewColumn5 = dgvList.Columns["R_RSID"];
				bool flag2 = (dgvList.Columns["R_ID"].Visible = false);
				bool flag4 = (dataGridViewColumn5.Visible = flag2);
				bool flag6 = (dataGridViewColumn4.Visible = flag4);
				bool flag8 = (dataGridViewColumn3.Visible = flag6);
				bool visible = (dataGridViewColumn2.Visible = flag8);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvList.Columns.Count; i++)
				{
					dgvList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvList.Columns[i].Name];
					dgvList.Columns[i].ReadOnly = true;
				}
				dgvList.AutoResizeColumns();
			}
			while (!flag)
			{
				for (int j = 0; j < dataTable.Rows.Count && !(b.ToString() == dataTable.Rows[j]["R_Code"].ToString()); j++)
				{
					if (j == dataTable.Rows.Count - 1)
					{
						flag = true;
					}
				}
				if (b == byte.MaxValue)
				{
					break;
				}
				if (!flag)
				{
					b++;
				}
			}
		}
		if (selNode.Level == 2)
		{
			txtRCode.Text = b.ToString();
		}
		m_Init = false;
	}

	private void frmRooms_Load(object sender, EventArgs e)
	{
		btnRef_Click(null, null);
		InitStatus("");
	}

	private void btnRef_Click(object sender, EventArgs e)
	{
		InitTreeList();
		InitType();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void toolsBtn2_Click(object sender, EventArgs e)
	{
		if (panel1.Visible)
		{
			toolsBtn2.ImageNew = Resources.mini_bottom;
			panel1.Visible = false;
		}
		else
		{
			panel1.Visible = true;
			toolsBtn2.ImageNew = Resources.mini_top;
		}
	}

	private void tvList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		try
		{
			InitRoomList(e.Node);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void tvList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		TextBox textBox = txtFID;
		string text = (txtFName.Text = "");
		textBox.Text = text;
		txtRID.Text = "";
		try
		{
			TreeNode node = e.Node;
			if (node != null && node.Level == 2)
			{
				txtFName.Text = node.Text.Trim();
				txtFID.Text = node.Name.ToString().Trim();
			}
		}
		catch
		{
		}
	}

	private bool chkData(int ctype)
	{
		if (txtFName.Text == "" || txtFID.Text.Trim() == "")
		{
			Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		if (cobType.Text == "")
		{
			Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		if (cobStatus.Text == "")
		{
			Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		if (ctype == 0)
		{
			if (txtRName.Text.Trim() == "" || txtRCode.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			int num = Convert.ToInt16(txtRCode.Text.Trim());
			string text = "";
			if (num > 255)
			{
				text = string.Format((string)m_htab["Info17"], label5.Text.Trim().Substring(0, label5.Text.Trim().Length - 1), 256);
				Program.MsgCustom(text, MessageBoxIcon.Asterisk);
				return false;
			}
			int num2 = Convert.ToInt16(txtSubCode.Text.Trim());
			if (num2 > 15)
			{
				text = string.Format((string)m_htab["Info17"], label6.Text.Trim().Substring(0, label6.Text.Trim().Length - 1), 16);
				Program.MsgCustom(text, MessageBoxIcon.Asterisk);
				return false;
			}
		}
		else
		{
			if (txtRFn.Text.Trim().Length <= Convert.ToInt32(txtFCL.Text.Trim()))
			{
				Program.MsgBox((string)m_htab["Info08"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			if (Convert.ToInt32(txtQty.Text.Trim()) + Convert.ToInt32(txtRFc.Text.Trim()) > 256)
			{
				Program.MsgBox((string)m_htab["Info09"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
		}
		return true;
	}

	private void btnANew_Click(object sender, EventArgs e)
	{
		string text = "";
		try
		{
			if (!chkData(1))
			{
				return;
			}
			int num = Convert.ToInt32(txtRFc.Text.Trim());
			int num2 = Convert.ToInt32(txtFCL.Text.Trim());
			string text2 = txtRFn.Text.Trim();
			int num3 = text2.Length - num2;
			int num4 = Convert.ToInt32(text2.Substring(num2));
			text2 = text2.Substring(0, num2);
			int num5 = Convert.ToInt32(txtQty.Text.Trim());
			if (num5 > 99 && num3 < 3)
			{
				Program.MsgBox((string)m_htab["Info12"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			text = string.Format((string)m_htab["Info10"], text2, "\r\n", txtRFn.Text.Trim(), "\r\n", txtRFc.Text.Trim(), "\r\n", txtQty.Text.Trim(), "\r\n");
			text += string.Format((string)m_htab["Info11"], "--------------\r\n", txtSubCode.Text.Trim(), "\r\n", cobType.Text, "\r\n", cobStatus.Text, "\r\n", txtBACount.Text.Trim(), txtBPirce.Text.Trim(), "\r\n", txtSize.Text.Trim(), "\r\n", txtMemo.Text.Trim(), "\r\n--------------\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			DataTable dataTable = null;
			string text3 = "";
			string text4 = "";
			int num6 = 0;
			int num7 = -1;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			for (int i = 0; i < num5; i++)
			{
				text4 = text2 + (num4 + i).ToString("D" + num3);
				text = (num4 + i).ToString();
				if (!chk4m.Checked)
				{
					num7 = text.IndexOf("4");
					if (num7 >= 0 && num7 < text.Length - 1)
					{
						num10++;
						continue;
					}
				}
				if (!chk7m.Checked)
				{
					num7 = text.IndexOf("7");
					if (num7 >= 0 && num7 < text.Length - 1)
					{
						num10++;
						continue;
					}
				}
				if (!chk4l.Checked && text.Substring(text.Length - 1) == "4")
				{
					num10++;
					continue;
				}
				if (!chk7l.Checked && text.Substring(text.Length - 1) == "7")
				{
					num10++;
					continue;
				}
				text = text4;
				text = ((!rbFore.Checked) ? (text + txtSC.Text.Trim()) : (txtSC.Text.Trim() + text));
				text3 = "Select * From D_Rooms Where R_Name=N'" + text + "' Or (R_FloorID=" + txtFID.Text.Trim() + "  And r_code ='" + (num + i) + "')";
				dataTable = SQLserver.Data_GetDataTable(text3);
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					num9++;
					continue;
				}
				int num12 = int.Parse(cobStatus.SelectedValue.ToString());
				if ((num12 >= 3 && num12 <= 6) || num12 == 10 || num12 == 11)
				{
					num12 = 1;
				}
				text3 = "Insert Into D_Rooms Values('" + (num + i) + "',N'" + text + "',0";
				object obj = text3;
				text3 = string.Concat(obj, ",1,", txtFID.Text.Trim(), ",", (cobType.SelectedValue == null) ? ((object)0) : cobType.SelectedValue, ",", num12.ToString(), ",0,NULL,0,", txtBACount.Text.Trim(), ",", Program.GetStandDec(txtBPirce.Text.Trim()), ",N'", txtSize.Text.Trim(), "',0,0,N'", txtMemo.Text.Trim(), "',0,GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "',NULL,NULL,NULL)");
				int num13 = SQLserver.Data_ExecuteSql(text3);
				if (num13 <= 0)
				{
					text = string.Format((string)m_htab["Err07"], text4, "\r\n");
					num11++;
					Program.MsgBox(text, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				txtRFn.Text = text4;
				num8++;
			}
			text = string.Format((string)m_htab["Info13"], "\r\n", num5 + "\r\n", num8 + "\r\n", num9 + "\r\n", num10 + "\r\n", num11);
			Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			btnRefR_Click(null, null);
		}
		catch (Exception ex)
		{
			text = string.Format((string)m_htab["Err08"], "\r\n");
			Program.MsgBox(text + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		try
		{
			if (!chkData(0))
			{
				return;
			}
			int num = Convert.ToInt16(txtRCode.Text.Trim());
			int num2 = Convert.ToInt16(txtSubCode.Text.Trim());
			string text = "Select * From D_Rooms Where R_Name=N'" + txtRName.Text.Trim() + "' Or (R_FloorID=" + txtFID.Text.Trim() + " And r_code ='" + num + "' And r_subcode = " + num2 + ")";
			text += " ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			int num3 = int.Parse(cobStatus.SelectedValue.ToString());
			if ((num3 >= 3 && num3 <= 6) || num3 == 10 || num3 == 11)
			{
				num3 = 1;
			}
			text = string.Concat("Insert Into D_Rooms Values('", num.ToString(), "',N'", txtRName.Text.Trim(), "',", num2.ToString(), ",1,", txtFID.Text.Trim(), ",", (cobType.SelectedValue == null) ? ((object)0) : cobType.SelectedValue, ",", num3.ToString(), ",0,NULL,0,", txtBACount.Text.Trim(), ",", Program.GetStandDec(txtBPirce.Text.Trim()), ",N'", txtSize.Text.Trim(), "',0,0,N'", txtMemo.Text.Trim(), "',0,GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "',NULL,NULL,NULL)");
			int num4 = SQLserver.Data_ExecuteSql(text);
			if (num4 <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num4, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			string input = txtRName.Text;
			input = Regex.Replace(input, "[^\\d]", "", RegexOptions.IgnoreCase);
			num = 0;
			int num5 = 0;
			if (input.Length >= 2)
			{
				num = Convert.ToInt32("0" + input.Substring(1)) + 1;
				num5 = input.Substring(1).Length;
				if (txtRName.Text.IndexOf(input) > 0)
				{
					txtRName.Text = txtRName.Text.Replace(input, "") + input.Substring(0, 1) + num.ToString("D" + num5);
				}
				else
				{
					txtRName.Text = input.Substring(0, 1) + num.ToString("D" + num5) + txtRName.Text.Replace(input, "");
				}
			}
			else
			{
				txtRName.Text = "";
			}
			num = Convert.ToInt32(txtRCode.Text.Trim());
			txtRCode.Text = ((num < 255) ? (num + 1) : 0).ToString();
			btnRefR_Click(null, null);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err06"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnRModify_Click(object sender, EventArgs e)
	{
		try
		{
			if (!chkData(0))
			{
				return;
			}
			if (txtRID.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				string sqlstr = string.Concat("Update D_Rooms Set R_TypeID=", (cobType.SelectedValue == null) ? ((object)0) : cobType.SelectedValue, ", R_RSID=", cobStatus.SelectedValue, ", R_BedAdd=", txtBACount.Text.Trim(), ", R_BedSinglePrice=", Program.GetStandDec(txtBPirce.Text.Trim()), ",  R_Size=N'", txtSize.Text.Trim(), "', R_Memo=N'", txtMemo.Text.Trim(), "', R_Updatetime=GetDate(), R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", txtRID.Text.Trim());
				int num = SQLserver.Data_ExecuteSql(sqlstr);
				if (num <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				btnRefR_Click(null, null);
			}
		}
		catch (Exception ex)
		{
			Console.Write(ex.Message.ToString());
		}
	}

	private void txtBACount_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtBPirce_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void txtRCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtSubCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		CheckInfo.NumberKeyPress(sender, e, 0, 15L);
	}

	private void txtBPirce_TextChanged(object sender, EventArgs e)
	{
		if (txtBPirce.Text.Trim() == "")
		{
			txtBPirce.Text = "0";
			txtBPirce.SelectionStart = 1;
		}
	}

	private void txtSubCode_TextChanged(object sender, EventArgs e)
	{
		if (txtSubCode.Text.Trim() == "")
		{
			txtSubCode.Text = "0";
			txtSubCode.SelectionStart = 1;
		}
	}

	private void dgvList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		try
		{
			txtFName.Text = dgvList.Rows[e.RowIndex].Cells["Floor_Name"].Value.ToString().Trim();
			txtFID.Text = dgvList.Rows[e.RowIndex].Cells["R_FloorID"].Value.ToString().Trim();
			cobType.SelectedValue = dgvList.Rows[e.RowIndex].Cells["R_TypeID"].Value.ToString().Trim();
			cobStatus.SelectedValue = dgvList.Rows[e.RowIndex].Cells["R_RSID"].Value.ToString().Trim();
			txtBACount.Text = dgvList.Rows[e.RowIndex].Cells["R_BedAdd"].Value.ToString().Trim();
			txtBPirce.Text = dgvList.Rows[e.RowIndex].Cells["R_BedSinglePrice"].Value.ToString().Trim();
			txtRID.Text = dgvList.Rows[e.RowIndex].Cells["R_ID"].Value.ToString().Trim();
			txtRName.Text = dgvList.Rows[e.RowIndex].Cells["R_Name"].Value.ToString().Trim();
			txtRCode.Text = dgvList.Rows[e.RowIndex].Cells["R_Code"].Value.ToString().Trim();
			txtSubCode.Text = dgvList.Rows[e.RowIndex].Cells["R_SubCode"].Value.ToString().Trim();
			txtSize.Text = dgvList.Rows[e.RowIndex].Cells["R_Size"].Value.ToString().Trim();
			txtMemo.Text = dgvList.Rows[e.RowIndex].Cells["R_Memo"].Value.ToString().Trim();
		}
		catch
		{
		}
	}

	private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (!m_Init && dgvList.DataSource != null)
			{
				dgvList.Rows[e.RowIndex].Cells["R_Edit"].Value = true;
				dgvList.EndEdit();
			}
		}
		catch
		{
		}
	}

	private void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		try
		{
			txtFName.Text = dgvList.Rows[e.RowIndex].Cells["Floor_Name"].Value.ToString().Trim();
			txtFID.Text = dgvList.Rows[e.RowIndex].Cells["R_FloorID"].Value.ToString().Trim();
			cobType.SelectedValue = dgvList.Rows[e.RowIndex].Cells["R_TypeID"].Value.ToString().Trim();
			if (cobStatus_SelectedValue != (int)dgvList.Rows[e.RowIndex].Cells["R_RSID"].Value)
			{
				cobStatus_SelectedValue = (int)dgvList.Rows[e.RowIndex].Cells["R_RSID"].Value;
				InitStatus("," + cobStatus_SelectedValue);
			}
			cobStatus.SelectedValue = dgvList.Rows[e.RowIndex].Cells["R_RSID"].Value.ToString().Trim();
			txtBACount.Text = dgvList.Rows[e.RowIndex].Cells["R_BedAdd"].Value.ToString().Trim();
			txtBPirce.Text = dgvList.Rows[e.RowIndex].Cells["R_BedSinglePrice"].Value.ToString().Trim();
			txtRID.Text = dgvList.Rows[e.RowIndex].Cells["R_ID"].Value.ToString().Trim();
			txtRName.Text = dgvList.Rows[e.RowIndex].Cells["R_Name"].Value.ToString().Trim();
			txtRCode.Text = dgvList.Rows[e.RowIndex].Cells["R_Code"].Value.ToString().Trim();
			txtSubCode.Text = dgvList.Rows[e.RowIndex].Cells["R_SubCode"].Value.ToString().Trim();
			txtSize.Text = dgvList.Rows[e.RowIndex].Cells["R_Size"].Value.ToString().Trim();
			txtMemo.Text = dgvList.Rows[e.RowIndex].Cells["R_Memo"].Value.ToString().Trim();
		}
		catch (Exception ex)
		{
			Console.Write(ex.Message.ToString());
		}
	}

	private void btnRefR_Click(object sender, EventArgs e)
	{
		try
		{
			InitRoomList(tvList.SelectedNode);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnAReset_Click(object sender, EventArgs e)
	{
		TextBox textBox = txtSC;
		TextBox textBox2 = txtRFn;
		TextBox textBox3 = txtQty;
		string text = (txtRFc.Text = "");
		string text3 = (textBox3.Text = text);
		string text5 = (textBox2.Text = text3);
		textBox.Text = text5;
		CheckBox checkBox = chk4l;
		CheckBox checkBox2 = chk7l;
		CheckBox checkBox3 = chk4m;
		bool flag = (chk7m.Checked = false);
		bool flag3 = (checkBox3.Checked = flag);
		bool flag5 = (checkBox2.Checked = flag3);
		checkBox.Checked = flag5;
		rbFore.Checked = true;
		txtRFn.Focus();
	}

	private void txtRFn_TextChanged(object sender, EventArgs e)
	{
		string text = txtRFn.Text.Trim();
		int num = 0;
		num = Convert.ToInt32(txtFCL.Text.Trim());
		if (text.Length > num)
		{
			txtRFc.Text = Convert.ToInt32(text.Substring(num)).ToString();
		}
		else
		{
			txtRFc.Text = "";
		}
	}

	private void txtRFn_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtFCL_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtBACount_Leave(object sender, EventArgs e)
	{
		if (txtBACount.Text.Trim() == "")
		{
			txtBACount.Text = "0";
			txtBACount.SelectionStart = 1;
		}
	}

	private void txtQty_Leave(object sender, EventArgs e)
	{
		if (Convert.ToInt32("0" + txtQty.Text.Trim()) == 0)
		{
			txtQty.Text = "1";
			txtQty.SelectionStart = 1;
		}
	}

	private void txtFCL_Leave(object sender, EventArgs e)
	{
		if (Convert.ToInt32("0" + txtFCL.Text.Trim()) == 0)
		{
			txtFCL.Text = "1";
			txtFCL.SelectionStart = 1;
		}
	}

	private void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			string text2 = "";
			if (dgvList.SelectedRows.Count <= 0)
			{
				return;
			}
			text2 = string.Format((string)m_htab["Info14"], "\r\n\r\n", "\r\n\r\n");
			if (Program.MsgBox(text2, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
			{
				return;
			}
			text2 = "";
			for (int i = 0; i < dgvList.SelectedRows.Count; i++)
			{
				text2 = text2 + "," + dgvList.SelectedRows[i].Cells["R_ID"].Value.ToString();
			}
			text2 = text2.Substring(1);
			DataTable dataTable = SQLserver.Data_GetDataTable("select * from D_Rooms where (R_RSID>=3 and R_RSID<=7) AND R_ID in (" + text2 + ")");
			if (dataTable.Rows.Count > 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["RoomIsUsed"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			text = " Delete From D_Rooms Where r_id not in ( select distinct r_id from T_Guest Where r_id in (" + text2 + ") \n union all \n select distinct r_id from T_Schedule Where r_id in (" + text2 + ") \n union all \n select distinct r_id from T_Rooms Where r_id in (" + text2 + ") \n union all \n select distinct VIP_r_id As r_id from T_VIP Where VIP_r_id in (" + text2 + ") \n union all \n select distinct r_id from T_RoomGroup Where r_id in (" + text2 + ") \n  ) And R_ID in(" + text2 + ") \n";
			object obj = text;
			text = string.Concat(obj, "Update D_Rooms Set R_flag = 1,  R_Updator = N'", Program.m_OperName, "', R_Updator_id =", Program.m_opid, ", R_UpdateTime = GetDate() Where R_ID in (", text2, ") And (R_RSID < 3 or R_RSID > 7) And R_flag = 0 ");
			int num = SQLserver.Data_ExecuteSql(text);
			if (num <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Program.fm != null)
			{
				Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
			}
			text2 = string.Format((string)m_htab["Info15"], "\r\n", num.ToString());
			Program.MsgCustom(text2, MessageBoxIcon.Asterisk);
			btnRefR_Click(null, null);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
	{
		try
		{
			if (e != null && e.Node != null)
			{
				InitRoomList(e.Node);
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnRes_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.SelectedRows.Count > 0 && Program.MsgBox((string)m_htab["Info18"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				string text = "";
				string text2 = "";
				for (int i = 0; i < dgvList.SelectedRows.Count; i++)
				{
					text2 = text2 + "," + dgvList.SelectedRows[i].Cells["R_ID"].Value.ToString();
				}
				text2 = text2.Substring(1);
				text = "Update D_Rooms Set R_flag = 0,  R_Updator = N'" + Program.m_OperName + "', R_Updator_id =" + Program.m_opid + ", R_UpdateTime = GetDate() Where R_ID in (" + text2 + ")";
				int num = SQLserver.Data_ExecuteSql(text);
				if (num <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					btnRefR_Click(null, null);
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void chkSDis_CheckedChanged(object sender, EventArgs e)
	{
		btnRes.Visible = chkSDis.Checked;
	}

	private void frmRooms_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			tvList.Dispose();
			tvList = null;
		}
		catch
		{
		}
	}

	private void cobType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cobType.SelectedIndex < 0 && cobType.Items.Count > 0)
		{
			cobType.SelectedIndex = 0;
		}
		string sql = "select tp_rsize from d_roomtype where tp_name=N'" + cobType.Text + "'";
		DataTable dataTable = SQLserver.Data_GetDataTable(sql);
		try
		{
			txtSize.Text = (string)dataTable.Rows[0][0];
		}
		catch (Exception value)
		{
			Console.Write(value);
		}
	}

	private void txtFCL_TextChanged(object sender, EventArgs e)
	{
		try
		{
			int num = int.Parse(txtFCL.Text);
			if (num > 5 || num < 1)
			{
				txtFCL.Text = "1";
			}
		}
		catch (Exception ex)
		{
			txtFCL.Text = "1";
			Console.Write(ex.Message.ToString());
		}
	}

	private void cobStatus_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void label15_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = true;
	}

	private void label15_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Delete)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmRooms));
		this.imgListTV = new System.Windows.Forms.ImageList(this.components);
		this.plMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.grpRoom = new System.Windows.Forms.GroupBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label15 = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtFCL = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtRFc = new System.Windows.Forms.TextBox();
		this.btnAReset = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnANew = new LockSoftware.Controls.GlassBtn(this.components);
		this.grpSO = new System.Windows.Forms.GroupBox();
		this.rbBehind = new System.Windows.Forms.RadioButton();
		this.rbFore = new System.Windows.Forms.RadioButton();
		this.txtSC = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.clsBackPanel3 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.label13 = new System.Windows.Forms.Label();
		this.chk7m = new System.Windows.Forms.CheckBox();
		this.chk7l = new System.Windows.Forms.CheckBox();
		this.chk4m = new System.Windows.Forms.CheckBox();
		this.chk4l = new System.Windows.Forms.CheckBox();
		this.txtQty = new System.Windows.Forms.TextBox();
		this.txtRFn = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.cbpline01 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.label10 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.toolsBtn2 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtRID = new System.Windows.Forms.TextBox();
		this.btnRModify = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnNew = new LockSoftware.Controls.GlassBtn(this.components);
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtSubCode = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtRCode = new System.Windows.Forms.TextBox();
		this.txtRName = new System.Windows.Forms.TextBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label17 = new System.Windows.Forms.Label();
		this.txtBACount = new System.Windows.Forms.TextBox();
		this.txtFID = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtFName = new System.Windows.Forms.TextBox();
		this.txtMemo = new System.Windows.Forms.TextBox();
		this.cobType = new System.Windows.Forms.ComboBox();
		this.txtSize = new System.Windows.Forms.TextBox();
		this.cobStatus = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtBPirce = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnRef = new LockSoftware.Controls.ToolsBtn(this.components);
		this.tvList = new System.Windows.Forms.TreeView();
		this.btnSave = new LockSoftware.Controls.ToolsBtn(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.btnRefR = new LockSoftware.Controls.ToolsBtn(this.components);
		this.chkSDis = new System.Windows.Forms.CheckBox();
		this.btnRes = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnDel = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.plMain.SuspendLayout();
		this.grpRoom.SuspendLayout();
		this.panel2.SuspendLayout();
		this.grpSO.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel3.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.clsBackPanel2.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.flowLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.imgListTV.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgListTV.ImageStream");
		this.imgListTV.TransparentColor = System.Drawing.Color.Transparent;
		this.imgListTV.Images.SetKeyName(0, "OS00.png");
		this.imgListTV.Images.SetKeyName(1, "46.png");
		this.imgListTV.Images.SetKeyName(2, "ok.png");
		this.plMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.plMain.Border = true;
		this.plMain.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderBW = 1;
		this.plMain.BorderColorBottom = System.Drawing.Color.LightGray;
		this.plMain.BorderColorLeft = System.Drawing.Color.LightGray;
		this.plMain.BorderColorRight = System.Drawing.Color.LightGray;
		this.plMain.BorderColorTop = System.Drawing.Color.LightGray;
		this.plMain.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderLW = 1;
		this.plMain.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderRW = 1;
		this.plMain.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.plMain.BorderTW = 1;
		this.plMain.Color1 = System.Drawing.Color.White;
		this.plMain.Color2 = System.Drawing.Color.WhiteSmoke;
		this.plMain.ColorAngle = 225f;
		this.plMain.Controls.Add(this.grpRoom);
		this.plMain.Controls.Add(this.splitContainer1);
		this.plMain.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.plMain.Location = new System.Drawing.Point(3, 57);
		this.plMain.Name = "plMain";
		this.plMain.Size = new System.Drawing.Size(778, 502);
		this.plMain.TabIndex = 5;
		this.grpRoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.grpRoom.BackColor = System.Drawing.Color.Transparent;
		this.grpRoom.Controls.Add(this.panel2);
		this.grpRoom.Controls.Add(this.toolsBtn2);
		this.grpRoom.Controls.Add(this.panel1);
		this.grpRoom.Controls.Add(this.panel3);
		this.grpRoom.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpRoom.Location = new System.Drawing.Point(473, 3);
		this.grpRoom.Name = "grpRoom";
		this.grpRoom.Size = new System.Drawing.Size(296, 490);
		this.grpRoom.TabIndex = 12;
		this.grpRoom.TabStop = false;
		this.grpRoom.Text = "Room Setting";
		this.panel2.AutoScroll = true;
		this.panel2.Controls.Add(this.label15);
		this.panel2.Controls.Add(this.label16);
		this.panel2.Controls.Add(this.label19);
		this.panel2.Controls.Add(this.txtFCL);
		this.panel2.Controls.Add(this.label18);
		this.panel2.Controls.Add(this.txtRFc);
		this.panel2.Controls.Add(this.btnAReset);
		this.panel2.Controls.Add(this.btnANew);
		this.panel2.Controls.Add(this.grpSO);
		this.panel2.Controls.Add(this.txtQty);
		this.panel2.Controls.Add(this.txtRFn);
		this.panel2.Controls.Add(this.label12);
		this.panel2.Controls.Add(this.label11);
		this.panel2.Controls.Add(this.cbpline01);
		this.panel2.Controls.Add(this.label10);
		this.panel2.Controls.Add(this.label20);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(3, 305);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(290, 182);
		this.panel2.TabIndex = 2;
		this.label15.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.label15.ForeColor = System.Drawing.Color.Red;
		this.label15.Location = new System.Drawing.Point(8, 102);
		this.label15.Multiline = true;
		this.label15.Name = "label15";
		this.label15.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.label15.Size = new System.Drawing.Size(260, 32);
		this.label15.TabIndex = 52;
		this.label15.Text = "*Ex:12F,Start: 12001,Create 30 Rooms.";
		this.label15.KeyDown += new System.Windows.Forms.KeyEventHandler(label15_KeyDown);
		this.label15.KeyPress += new System.Windows.Forms.KeyPressEventHandler(label15_KeyPress);
		this.label16.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.label16.ForeColor = System.Drawing.Color.Red;
		this.label16.Location = new System.Drawing.Point(8, 135);
		this.label16.Multiline = true;
		this.label16.Name = "label16";
		this.label16.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.label16.Size = new System.Drawing.Size(260, 32);
		this.label16.TabIndex = 51;
		this.label16.Text = "Start Name = 12001, Quantity = 30";
		this.label16.KeyDown += new System.Windows.Forms.KeyEventHandler(label15_KeyDown);
		this.label16.KeyPress += new System.Windows.Forms.KeyPressEventHandler(label15_KeyPress);
		this.label19.ForeColor = System.Drawing.Color.Red;
		this.label19.Location = new System.Drawing.Point(162, 36);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(110, 35);
		this.label19.TabIndex = 49;
		this.label19.Text = "*Ex:12F,Length=2";
		this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtFCL.Location = new System.Drawing.Point(120, 43);
		this.txtFCL.MaxLength = 1;
		this.txtFCL.Name = "txtFCL";
		this.txtFCL.Size = new System.Drawing.Size(40, 22);
		this.txtFCL.TabIndex = 12;
		this.txtFCL.Text = "1";
		this.txtFCL.TextChanged += new System.EventHandler(txtFCL_TextChanged);
		this.txtFCL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFCL_KeyPress);
		this.txtFCL.Leave += new System.EventHandler(txtFCL_Leave);
		this.label18.Location = new System.Drawing.Point(7, 38);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(112, 28);
		this.label18.TabIndex = 47;
		this.label18.Text = "Floor Code Length:";
		this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtRFc.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtRFc.Location = new System.Drawing.Point(135, 74);
		this.txtRFc.MaxLength = 3;
		this.txtRFc.Name = "txtRFc";
		this.txtRFc.ReadOnly = true;
		this.txtRFc.Size = new System.Drawing.Size(25, 22);
		this.txtRFc.TabIndex = 45;
		this.btnAReset.BackColor = System.Drawing.Color.LightGray;
		this.btnAReset.Font = new System.Drawing.Font("Tahoma", 10.5f);
		this.btnAReset.ForeColor = System.Drawing.Color.Black;
		this.btnAReset.GlowColor = System.Drawing.Color.White;
		this.btnAReset.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnAReset.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnAReset.Location = new System.Drawing.Point(189, 295);
		this.btnAReset.Name = "btnAReset";
		this.btnAReset.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnAReset.Size = new System.Drawing.Size(75, 30);
		this.btnAReset.TabIndex = 23;
		this.btnAReset.Text = "Reset";
		this.btnAReset.Click += new System.EventHandler(btnAReset_Click);
		this.btnANew.BackColor = System.Drawing.Color.LightGray;
		this.btnANew.Font = new System.Drawing.Font("Tahoma", 10.5f);
		this.btnANew.ForeColor = System.Drawing.Color.Black;
		this.btnANew.GlowColor = System.Drawing.Color.White;
		this.btnANew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnANew.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnANew.Location = new System.Drawing.Point(73, 295);
		this.btnANew.Name = "btnANew";
		this.btnANew.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnANew.Size = new System.Drawing.Size(96, 30);
		this.btnANew.TabIndex = 22;
		this.btnANew.Text = "Auto Create";
		this.btnANew.Click += new System.EventHandler(btnANew_Click);
		this.grpSO.Controls.Add(this.rbBehind);
		this.grpSO.Controls.Add(this.rbFore);
		this.grpSO.Controls.Add(this.txtSC);
		this.grpSO.Controls.Add(this.label14);
		this.grpSO.Controls.Add(this.clsBackPanel3);
		this.grpSO.Controls.Add(this.label13);
		this.grpSO.Controls.Add(this.chk7m);
		this.grpSO.Controls.Add(this.chk7l);
		this.grpSO.Controls.Add(this.chk4m);
		this.grpSO.Controls.Add(this.chk4l);
		this.grpSO.Location = new System.Drawing.Point(8, 169);
		this.grpSO.Name = "grpSO";
		this.grpSO.Size = new System.Drawing.Size(264, 122);
		this.grpSO.TabIndex = 3;
		this.grpSO.TabStop = false;
		this.grpSO.Text = "Special Option";
		this.rbBehind.AutoSize = true;
		this.rbBehind.Location = new System.Drawing.Point(201, 95);
		this.rbBehind.Name = "rbBehind";
		this.rbBehind.Size = new System.Drawing.Size(62, 18);
		this.rbBehind.TabIndex = 21;
		this.rbBehind.Text = "Behind";
		this.rbBehind.UseVisualStyleBackColor = true;
		this.rbFore.Checked = true;
		this.rbFore.Location = new System.Drawing.Point(132, 92);
		this.rbFore.Name = "rbFore";
		this.rbFore.Size = new System.Drawing.Size(63, 24);
		this.rbFore.TabIndex = 20;
		this.rbFore.TabStop = true;
		this.rbFore.Text = "Fore";
		this.rbFore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.rbFore.UseVisualStyleBackColor = true;
		this.txtSC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtSC.Location = new System.Drawing.Point(51, 93);
		this.txtSC.MaxLength = 8;
		this.txtSC.Name = "txtSC";
		this.txtSC.Size = new System.Drawing.Size(62, 22);
		this.txtSC.TabIndex = 19;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(6, 97);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(34, 14);
		this.label14.TabIndex = 38;
		this.label14.Text = "Sign:";
		this.clsBackPanel3.Border = false;
		this.clsBackPanel3.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderBW = 1;
		this.clsBackPanel3.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel3.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderLW = 1;
		this.clsBackPanel3.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderRW = 1;
		this.clsBackPanel3.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderTW = 1;
		this.clsBackPanel3.Color1 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel3.Color2 = System.Drawing.Color.Black;
		this.clsBackPanel3.ColorAngle = 135f;
		this.clsBackPanel3.Location = new System.Drawing.Point(7, 87);
		this.clsBackPanel3.Name = "clsBackPanel3";
		this.clsBackPanel3.Size = new System.Drawing.Size(245, 1);
		this.clsBackPanel3.TabIndex = 37;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(6, 72);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(100, 14);
		this.label13.TabIndex = 36;
		this.label13.Text = "Special Character";
		this.chk7m.AutoSize = true;
		this.chk7m.Location = new System.Drawing.Point(118, 48);
		this.chk7m.Name = "chk7m";
		this.chk7m.Size = new System.Drawing.Size(138, 18);
		this.chk7m.TabIndex = 18;
		this.chk7m.Text = "With 7 in the middle";
		this.chk7m.UseVisualStyleBackColor = true;
		this.chk7l.AutoSize = true;
		this.chk7l.Location = new System.Drawing.Point(8, 48);
		this.chk7l.Name = "chk7l";
		this.chk7l.Size = new System.Drawing.Size(100, 18);
		this.chk7l.TabIndex = 17;
		this.chk7l.Text = "With 7 at last";
		this.chk7l.UseVisualStyleBackColor = true;
		this.chk4m.AutoSize = true;
		this.chk4m.Location = new System.Drawing.Point(118, 20);
		this.chk4m.Name = "chk4m";
		this.chk4m.Size = new System.Drawing.Size(138, 18);
		this.chk4m.TabIndex = 16;
		this.chk4m.Text = "With 4 in the middle";
		this.chk4m.UseVisualStyleBackColor = true;
		this.chk4l.AutoSize = true;
		this.chk4l.Location = new System.Drawing.Point(8, 20);
		this.chk4l.Name = "chk4l";
		this.chk4l.Size = new System.Drawing.Size(100, 18);
		this.chk4l.TabIndex = 15;
		this.chk4l.Text = "With 4 at last";
		this.chk4l.UseVisualStyleBackColor = true;
		this.txtQty.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtQty.Location = new System.Drawing.Point(227, 74);
		this.txtQty.MaxLength = 3;
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(33, 22);
		this.txtQty.TabIndex = 14;
		this.txtQty.Text = "1";
		this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtQty_KeyPress);
		this.txtQty.Leave += new System.EventHandler(txtQty_Leave);
		this.txtRFn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRFn.ImeMode = System.Windows.Forms.ImeMode.Disable;
		this.txtRFn.Location = new System.Drawing.Point(78, 74);
		this.txtRFn.MaxLength = 6;
		this.txtRFn.Name = "txtRFn";
		this.txtRFn.Size = new System.Drawing.Size(42, 22);
		this.txtRFn.TabIndex = 13;
		this.txtRFn.TextChanged += new System.EventHandler(txtRFn_TextChanged);
		this.txtRFn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRFn_KeyPress);
		this.label12.Location = new System.Drawing.Point(162, 78);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(65, 18);
		this.label12.TabIndex = 37;
		this.label12.Text = "Quantity:";
		this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label11.Location = new System.Drawing.Point(6, 69);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(73, 28);
		this.label11.TabIndex = 36;
		this.label11.Text = "Start Name:";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
		this.cbpline01.Location = new System.Drawing.Point(6, 34);
		this.cbpline01.Name = "cbpline01";
		this.cbpline01.Size = new System.Drawing.Size(260, 1);
		this.cbpline01.TabIndex = 35;
		this.label10.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label10.ForeColor = System.Drawing.Color.Red;
		this.label10.Location = new System.Drawing.Point(6, 3);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(231, 28);
		this.label10.TabIndex = 0;
		this.label10.Text = "Auto create current floor's room";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(120, 78);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(19, 14);
		this.label20.TabIndex = 50;
		this.label20.Text = "→";
		this.toolsBtn2.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Checked = false;
		this.toolsBtn2.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn2.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn2.ImageNew = LockSoftware.Properties.Resources.mini_top;
		this.toolsBtn2.ImageRedrawed = true;
		this.toolsBtn2.ImageStyle = 0;
		this.toolsBtn2.isButton = true;
		this.toolsBtn2.Location = new System.Drawing.Point(3, 297);
		this.toolsBtn2.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.toolsBtn2.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.toolsBtn2.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn2.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn2.Name = "toolsBtn2";
		this.toolsBtn2.Size = new System.Drawing.Size(290, 8);
		this.toolsBtn2.TabIndex = 21;
		this.toolsBtn2.TextImageLocation = 0;
		this.toolsBtn2.TextNew = "";
		this.toolsBtn2.TextRedrawed = false;
		this.toolsBtn2.Click += new System.EventHandler(toolsBtn2_Click);
		this.panel1.Controls.Add(this.txtRID);
		this.panel1.Controls.Add(this.btnRModify);
		this.panel1.Controls.Add(this.btnNew);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.txtSubCode);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.txtRCode);
		this.panel1.Controls.Add(this.txtRName);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(3, 218);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(290, 79);
		this.panel1.TabIndex = 1;
		this.txtRID.Location = new System.Drawing.Point(201, 8);
		this.txtRID.Name = "txtRID";
		this.txtRID.Size = new System.Drawing.Size(68, 22);
		this.txtRID.TabIndex = 22;
		this.txtRID.Visible = false;
		this.btnRModify.BackColor = System.Drawing.Color.Gainsboro;
		this.btnRModify.Font = new System.Drawing.Font("Tahoma", 10.5f);
		this.btnRModify.ForeColor = System.Drawing.Color.Black;
		this.btnRModify.GlowColor = System.Drawing.Color.White;
		this.btnRModify.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRModify.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnRModify.Location = new System.Drawing.Point(195, 38);
		this.btnRModify.Name = "btnRModify";
		this.btnRModify.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnRModify.Size = new System.Drawing.Size(75, 30);
		this.btnRModify.TabIndex = 11;
		this.btnRModify.Text = "Modify";
		this.btnRModify.Click += new System.EventHandler(btnRModify_Click);
		this.btnNew.BackColor = System.Drawing.Color.Gainsboro;
		this.btnNew.Font = new System.Drawing.Font("Tahoma", 10.5f);
		this.btnNew.ForeColor = System.Drawing.Color.Black;
		this.btnNew.GlowColor = System.Drawing.Color.White;
		this.btnNew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNew.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnNew.Location = new System.Drawing.Point(94, 38);
		this.btnNew.Name = "btnNew";
		this.btnNew.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnNew.Size = new System.Drawing.Size(75, 30);
		this.btnNew.TabIndex = 10;
		this.btnNew.Text = "New";
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.label4.Location = new System.Drawing.Point(5, 7);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(77, 28);
		this.label4.TabIndex = 3;
		this.label4.Text = "Room Name:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.Location = new System.Drawing.Point(5, 59);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(74, 28);
		this.label5.TabIndex = 4;
		this.label5.Text = "Room Code:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.Visible = false;
		this.txtSubCode.Location = new System.Drawing.Point(239, 58);
		this.txtSubCode.MaxLength = 2;
		this.txtSubCode.Name = "txtSubCode";
		this.txtSubCode.Size = new System.Drawing.Size(30, 22);
		this.txtSubCode.TabIndex = 9;
		this.txtSubCode.Text = "0";
		this.txtSubCode.Visible = false;
		this.txtSubCode.TextChanged += new System.EventHandler(txtSubCode_TextChanged);
		this.txtSubCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSubCode_KeyPress);
		this.label6.Location = new System.Drawing.Point(162, 57);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(71, 23);
		this.label6.TabIndex = 5;
		this.label6.Text = "Sub Code:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Visible = false;
		this.txtRCode.Location = new System.Drawing.Point(95, 58);
		this.txtRCode.MaxLength = 3;
		this.txtRCode.Name = "txtRCode";
		this.txtRCode.Size = new System.Drawing.Size(61, 22);
		this.txtRCode.TabIndex = 8;
		this.txtRCode.Visible = false;
		this.txtRCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRCode_KeyPress);
		this.txtRName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtRName.Location = new System.Drawing.Point(95, 8);
		this.txtRName.Name = "txtRName";
		this.txtRName.Size = new System.Drawing.Size(174, 22);
		this.txtRName.TabIndex = 7;
		this.panel3.Controls.Add(this.label17);
		this.panel3.Controls.Add(this.txtBACount);
		this.panel3.Controls.Add(this.txtFID);
		this.panel3.Controls.Add(this.label1);
		this.panel3.Controls.Add(this.txtFName);
		this.panel3.Controls.Add(this.txtMemo);
		this.panel3.Controls.Add(this.cobType);
		this.panel3.Controls.Add(this.txtSize);
		this.panel3.Controls.Add(this.cobStatus);
		this.panel3.Controls.Add(this.label3);
		this.panel3.Controls.Add(this.label2);
		this.panel3.Controls.Add(this.txtBPirce);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add(this.label8);
		this.panel3.Controls.Add(this.label9);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(3, 18);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(290, 200);
		this.panel3.TabIndex = 0;
		this.label17.Location = new System.Drawing.Point(139, 127);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(83, 18);
		this.label17.TabIndex = 21;
		this.label17.Text = "Single Price:";
		this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.txtBACount.Location = new System.Drawing.Point(96, 124);
		this.txtBACount.Name = "txtBACount";
		this.txtBACount.Size = new System.Drawing.Size(43, 22);
		this.txtBACount.TabIndex = 2;
		this.txtBACount.Text = "4";
		this.txtBACount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBACount_KeyPress);
		this.txtBACount.Leave += new System.EventHandler(txtBACount_Leave);
		this.txtFID.Location = new System.Drawing.Point(202, 4);
		this.txtFID.Name = "txtFID";
		this.txtFID.Size = new System.Drawing.Size(67, 22);
		this.txtFID.TabIndex = 19;
		this.txtFID.Visible = false;
		this.label1.Location = new System.Drawing.Point(5, 1);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 28);
		this.label1.TabIndex = 0;
		this.label1.Text = "Floor Name:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtFName.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtFName.ForeColor = System.Drawing.Color.Black;
		this.txtFName.Location = new System.Drawing.Point(95, 4);
		this.txtFName.Name = "txtFName";
		this.txtFName.ReadOnly = true;
		this.txtFName.Size = new System.Drawing.Size(174, 22);
		this.txtFName.TabIndex = 1;
		this.txtMemo.Location = new System.Drawing.Point(96, 154);
		this.txtMemo.Multiline = true;
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtMemo.Size = new System.Drawing.Size(174, 38);
		this.txtMemo.TabIndex = 6;
		this.cobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobType.FormattingEnabled = true;
		this.cobType.Location = new System.Drawing.Point(95, 34);
		this.cobType.Name = "cobType";
		this.cobType.Size = new System.Drawing.Size(174, 22);
		this.cobType.TabIndex = 2;
		this.cobType.SelectedIndexChanged += new System.EventHandler(cobType_SelectedIndexChanged);
		this.txtSize.Location = new System.Drawing.Point(95, 93);
		this.txtSize.Name = "txtSize";
		this.txtSize.ReadOnly = true;
		this.txtSize.Size = new System.Drawing.Size(173, 22);
		this.txtSize.TabIndex = 4;
		this.cobStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobStatus.FormattingEnabled = true;
		this.cobStatus.Location = new System.Drawing.Point(95, 63);
		this.cobStatus.Name = "cobStatus";
		this.cobStatus.Size = new System.Drawing.Size(174, 22);
		this.cobStatus.TabIndex = 3;
		this.cobStatus.SelectedIndexChanged += new System.EventHandler(cobStatus_SelectedIndexChanged);
		this.label3.Location = new System.Drawing.Point(5, 61);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(81, 28);
		this.label3.TabIndex = 2;
		this.label3.Text = "Room Status:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.Location = new System.Drawing.Point(5, 31);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 28);
		this.label2.TabIndex = 1;
		this.label2.Text = "Room Type:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtBPirce.Location = new System.Drawing.Point(228, 124);
		this.txtBPirce.Name = "txtBPirce";
		this.txtBPirce.Size = new System.Drawing.Size(42, 22);
		this.txtBPirce.TabIndex = 5;
		this.txtBPirce.Text = "50.0";
		this.txtBPirce.TextChanged += new System.EventHandler(txtBPirce_TextChanged);
		this.txtBPirce.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBPirce_KeyPress);
		this.label7.Location = new System.Drawing.Point(5, 121);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(82, 28);
		this.label7.TabIndex = 6;
		this.label7.Text = "Can Add Bed:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label8.Location = new System.Drawing.Point(5, 91);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(67, 28);
		this.label8.TabIndex = 7;
		this.label8.Text = "Room Size:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label9.Location = new System.Drawing.Point(3, 159);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(79, 28);
		this.label9.TabIndex = 8;
		this.label9.Text = "Room Memo:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(9, 11);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.clsBackPanel2);
		this.splitContainer1.Panel1MinSize = 100;
		this.splitContainer1.Panel2.Controls.Add(this.clsBackPanel1);
		this.splitContainer1.Panel2MinSize = 100;
		this.splitContainer1.Size = new System.Drawing.Size(458, 482);
		this.splitContainer1.SplitterDistance = 207;
		this.splitContainer1.TabIndex = 11;
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
		this.clsBackPanel2.Color1 = System.Drawing.Color.White;
		this.clsBackPanel2.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.btnRef);
		this.clsBackPanel2.Controls.Add(this.tvList);
		this.clsBackPanel2.Controls.Add(this.btnSave);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(207, 482);
		this.clsBackPanel2.TabIndex = 10;
		this.btnRef.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRef.BackColor = System.Drawing.Color.Transparent;
		this.btnRef.Checked = false;
		this.btnRef.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRef.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRef.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRef.ImageNew = LockSoftware.Properties.Resources.Button_Refresh;
		this.btnRef.ImageRedrawed = true;
		this.btnRef.ImageStyle = 0;
		this.btnRef.isButton = true;
		this.btnRef.Location = new System.Drawing.Point(172, 5);
		this.btnRef.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRef.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnRef.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRef.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRef.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRef.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRef.Name = "btnRef";
		this.btnRef.Size = new System.Drawing.Size(23, 23);
		this.btnRef.TabIndex = 25;
		this.btnRef.TextImageLocation = 0;
		this.btnRef.TextNew = "";
		this.btnRef.TextRedrawed = false;
		this.btnRef.Click += new System.EventHandler(btnRef_Click);
		this.tvList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tvList.ImageIndex = 0;
		this.tvList.ImageList = this.imgListTV;
		this.tvList.Location = new System.Drawing.Point(0, 32);
		this.tvList.Name = "tvList";
		this.tvList.SelectedImageIndex = 0;
		this.tvList.Size = new System.Drawing.Size(207, 450);
		this.tvList.TabIndex = 0;
		this.tvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvList_AfterSelect);
		this.tvList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvList_NodeMouseClick);
		this.tvList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvList_NodeMouseDoubleClick);
		this.btnSave.BackColor = System.Drawing.Color.Transparent;
		this.btnSave.Checked = false;
		this.btnSave.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnSave.DefaultColor = System.Drawing.Color.Transparent;
		this.btnSave.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSave.ImageNew = LockSoftware.Properties.Resources.save;
		this.btnSave.ImageRedrawed = true;
		this.btnSave.ImageStyle = 0;
		this.btnSave.isButton = true;
		this.btnSave.Location = new System.Drawing.Point(117, 5);
		this.btnSave.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnSave.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnSave.MouseDownStartColor = System.Drawing.Color.White;
		this.btnSave.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnSave.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnSave.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(49, 23);
		this.btnSave.TabIndex = 24;
		this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSave.TextImageLocation = 0;
		this.btnSave.TextNew = "Save";
		this.btnSave.TextRedrawed = false;
		this.btnSave.Visible = false;
		this.clsBackPanel1.Border = true;
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
		this.clsBackPanel1.Controls.Add(this.dgvList);
		this.clsBackPanel1.Controls.Add(this.flowLayoutPanel1);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(247, 482);
		this.clsBackPanel1.TabIndex = 1;
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 64);
		this.dgvList.Name = "dgvList";
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(247, 418);
		this.dgvList.TabIndex = 0;
		this.dgvList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvList_CellMouseDoubleClick);
		this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellValueChanged);
		this.dgvList.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvList_RowHeaderMouseClick);
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.Controls.Add(this.btnRefR);
		this.flowLayoutPanel1.Controls.Add(this.chkSDis);
		this.flowLayoutPanel1.Controls.Add(this.btnRes);
		this.flowLayoutPanel1.Controls.Add(this.btnDel);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
		this.flowLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 9f);
		this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(247, 64);
		this.flowLayoutPanel1.TabIndex = 6;
		this.btnRefR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRefR.BackColor = System.Drawing.Color.Transparent;
		this.btnRefR.Checked = false;
		this.btnRefR.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRefR.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRefR.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRefR.ImageNew = LockSoftware.Properties.Resources.Button_Refresh;
		this.btnRefR.ImageRedrawed = true;
		this.btnRefR.ImageStyle = 0;
		this.btnRefR.isButton = true;
		this.btnRefR.Location = new System.Drawing.Point(221, 5);
		this.btnRefR.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRefR.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnRefR.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRefR.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRefR.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRefR.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRefR.Name = "btnRefR";
		this.btnRefR.Size = new System.Drawing.Size(23, 23);
		this.btnRefR.TabIndex = 28;
		this.btnRefR.TextImageLocation = 0;
		this.btnRefR.TextNew = "";
		this.btnRefR.TextRedrawed = false;
		this.btnRefR.Click += new System.EventHandler(btnRefR_Click);
		this.chkSDis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.chkSDis.BackColor = System.Drawing.Color.Transparent;
		this.chkSDis.Location = new System.Drawing.Point(110, 5);
		this.chkSDis.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
		this.chkSDis.Name = "chkSDis";
		this.chkSDis.Size = new System.Drawing.Size(105, 28);
		this.chkSDis.TabIndex = 27;
		this.chkSDis.Text = "Show Disabled";
		this.chkSDis.UseVisualStyleBackColor = false;
		this.chkSDis.CheckedChanged += new System.EventHandler(chkSDis_CheckedChanged);
		this.btnRes.BackColor = System.Drawing.Color.Transparent;
		this.btnRes.Checked = false;
		this.btnRes.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRes.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRes.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRes.ImageNew = LockSoftware.Properties.Resources.v_res;
		this.btnRes.ImageRedrawed = true;
		this.btnRes.ImageStyle = 0;
		this.btnRes.isButton = true;
		this.btnRes.Location = new System.Drawing.Point(29, 5);
		this.btnRes.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRes.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnRes.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRes.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRes.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRes.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRes.Name = "btnRes";
		this.btnRes.Size = new System.Drawing.Size(75, 28);
		this.btnRes.TabIndex = 26;
		this.btnRes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRes.TextImageLocation = 0;
		this.btnRes.TextNew = "Restore";
		this.btnRes.TextRedrawed = false;
		this.btnRes.Visible = false;
		this.btnRes.Click += new System.EventHandler(btnRes_Click);
		this.btnDel.BackColor = System.Drawing.Color.Transparent;
		this.btnDel.Checked = false;
		this.btnDel.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnDel.DefaultColor = System.Drawing.Color.Transparent;
		this.btnDel.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnDel.ImageNew = LockSoftware.Properties.Resources.delete;
		this.btnDel.ImageRedrawed = true;
		this.btnDel.ImageStyle = 0;
		this.btnDel.isButton = true;
		this.btnDel.Location = new System.Drawing.Point(144, 33);
		this.btnDel.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnDel.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnDel.MouseDownStartColor = System.Drawing.Color.White;
		this.btnDel.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnDel.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnDel.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnDel.Name = "btnDel";
		this.btnDel.Size = new System.Drawing.Size(100, 28);
		this.btnDel.TabIndex = 29;
		this.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnDel.TextImageLocation = 0;
		this.btnDel.TextNew = "Disable";
		this.btnDel.TextRedrawed = false;
		this.btnDel.Click += new System.EventHandler(btnDel_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnClose.ButtonText = "Close";
		this.btnClose.CornerRadius = 4;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.5f);
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Location = new System.Drawing.Point(681, 19);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(83, 35);
		this.btnClose.TabIndex = 30;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources._05_1_;
		this.toolsBtn1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
		this.toolsBtn1.ImageNew = null;
		this.toolsBtn1.ImageRedrawed = false;
		this.toolsBtn1.ImageStyle = 0;
		this.toolsBtn1.isButton = false;
		this.toolsBtn1.Location = new System.Drawing.Point(0, 0);
		this.toolsBtn1.MouseDownBorderColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.toolsBtn1.MouseDownEndColor = System.Drawing.Color.Beige;
		this.toolsBtn1.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn1.MouseEnterEndColor = System.Drawing.Color.Silver;
		this.toolsBtn1.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn1.Name = "toolsBtn1";
		this.toolsBtn1.Size = new System.Drawing.Size(784, 54);
		this.toolsBtn1.TabIndex = 3;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Room Setting: Setting hotel's room";
		this.toolsBtn1.TextRedrawed = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		base.ClientSize = new System.Drawing.Size(784, 562);
		base.Controls.Add(this.plMain);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.toolsBtn1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmRooms";
		this.Text = "frmRooms";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmRooms_FormClosing);
		base.Load += new System.EventHandler(frmRooms_Load);
		this.plMain.ResumeLayout(false);
		this.grpRoom.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.grpSO.ResumeLayout(false);
		this.grpSO.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.clsBackPanel2.ResumeLayout(false);
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.flowLayoutPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
