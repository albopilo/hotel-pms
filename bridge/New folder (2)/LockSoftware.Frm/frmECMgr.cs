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

public class frmECMgr : Form
{
	public string m_objName = "WFec";

	public Hashtable m_htab;

	private Label lb_1 = new Label();

	private int m_ct = -1;

	private IContainer components;

	private ComboBox cobCer;

	private TextBox txtCerNum;

	private Label label1;

	private Label label4;

	private Label label2;

	private TextBox txtUser;

	private DateTimePicker dtpCDate;

	private Panel panel1;

	private TabControl tabMain;

	private TabPage tabPage1;

	private TreeView tvList;

	private FlowLayoutPanel flowLayoutPanel1;

	private ComboBox cobCType;

	private CheckBox chkOpL;

	private CheckBox chkOpK;

	private TabPage tabPage2;

	private LockSoftware.Controls.GlassBtn btnCard;

	private LockSoftware.Controls.GlassBtn btnClose;

	private Label label3;

	private DateTimePicker dtpST;

	private Label label5;

	private DateTimePicker dtpET;

	private clsBackPanel clsBackPanel2;

	private Panel panel3;

	private ListView lvGrp;

	private DataGridView dgvGrp;

	private ImageList imglist;

	private clsBackPanel clsBackPanel5;

	private NGlassBtn btnClear;

	private NGlassBtn btnDel;

	private LockSoftware.Controls.GlassBtn btnRead;

	private NGlassBtn btnIDCard;

	public frmECMgr()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		base.Controls.Add(lb_1);
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void InitTreeList()
	{
		try
		{
			tvList.Nodes.Clear();
			string text = "Select B_ID, B_HotelName,Build_ID,Build_Code, Build_Name, IsNull(Build_Flag,0) As Build_Flag, Build_Memo, Floor_ID, Floor_Code, Floor_Name, IsNull(Floor_Flag,0) As Floor_Flag, Floor_Memo From v_HotelBF";
			text += " Where 1=1";
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
					treeNode = new TreeNode(text2, 1, 3);
					treeNode.Name = dataTable.Rows[i]["B_ID"].ToString().Trim();
					tvList.Nodes.Add(treeNode);
				}
				if (!Convert.ToBoolean(dataTable.Rows[i]["Build_Flag"].ToString()))
				{
					if (text3 != dataTable.Rows[i]["Build_Name"].ToString().Trim())
					{
						text3 = dataTable.Rows[i]["Build_Name"].ToString().Trim();
						treeNode2 = new TreeNode(text3, 2, 3);
						treeNode2.Name = dataTable.Rows[i]["Build_ID"].ToString().Trim();
						treeNode.Nodes.Add(treeNode2);
					}
					if (!Convert.ToBoolean(dataTable.Rows[i]["Floor_Flag"].ToString()) && dataTable.Rows[i]["Floor_Name"].ToString().Trim() != "")
					{
						treeNode2?.Nodes.Add(dataTable.Rows[i]["Floor_ID"].ToString().Trim(), dataTable.Rows[i]["Floor_Name"].ToString().Trim(), 2, 3);
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

	private void InitCerType()
	{
		try
		{
			cobCer.Text = "";
			cobCer.DataSource = null;
			string sql = "Select * FROM D_Cer  Where cer_flag = 0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				cobCer.DisplayMember = "cer_name";
				cobCer.ValueMember = "cer_id";
				cobCer.DataSource = dataTable.DefaultView;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void InitGroup()
	{
		try
		{
			dgvGrp.DataSource = null;
			string sql = "Select distinct RGT_id, RGT_name, RGT_code, createtime FROM v_GrpRoom Where RGT_flag=0 And (ISNULL(RG_flag, 1) = 0) Order by RGT_name";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null)
			{
				dgvGrp.DataSource = dataTable.DefaultView;
				DataGridViewColumn dataGridViewColumn = dgvGrp.Columns["RGT_id"];
				bool visible = (dgvGrp.Columns["RGT_code"].Visible = false);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvGrp.Columns.Count; i++)
				{
					dgvGrp.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvGrp.Columns[i].Name];
				}
				dgvGrp.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void frmECMgr_Load(object sender, EventArgs e)
	{
		InitTreeList();
		InitCerType();
		cobCType.Items.Clear();
		for (int i = 0; i < 4; i++)
		{
			cobCType.Items.Add((string)m_htab["cobCT" + i]);
		}
		cobCType.SelectedIndex = 0;
		InitGroup();
		dtpCDate.CustomFormat = Program.m_currDateFmt;
		if (Program.m_Lan == 0)
		{
			btnIDCard.Enabled = false;
		}
	}

	private void btnCard_Click(object sender, EventArgs e)
	{
		try
		{
			if (m_ct == -1)
			{
				return;
			}
			string text = "";
			string text2 = "";
			string text3 = "";
			int num = 0;
			int num2 = 0;
			num = Program.getMaxNumber(1, showError: true);
			if (num < 0)
			{
				return;
			}
			text2 = dtpCDate.Value.ToString("yyyyMMdd") + dtpST.Value.ToString("HHmm");
			if (m_ct != 9)
			{
				if (chkOpK.Checked)
				{
					num2++;
				}
				if (chkOpL.Checked)
				{
					num2 += 2;
				}
			}
			if (txtUser.Text == "" || txtCerNum.Text == "")
			{
				Program.MsgBox((string)m_htab["Error_user"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			object obj = ((cobCer.SelectedValue != null && Convert.ToInt32(cobCer.SelectedValue) >= 0) ? cobCer.SelectedValue : ((object)0));
			text = num2.ToString("D2");
			num2 = -1;
			text += dtpET.Value.ToString("HHmm");
			DataTable dataTable = null;
			if (m_ct != 9)
			{
				TreeNode treeNode = null;
				treeNode = tvList.SelectedNode;
				if (treeNode == null)
				{
					Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				switch (m_ct)
				{
				default:
					return;
				case 10:
				case 11:
					text += "0000";
					text3 = ",Null,'','',Null,'',''";
					break;
				case 13:
					if (treeNode.Level != 1)
					{
						Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					text3 = "Select * From D_Build Where Build_ID=" + treeNode.Name.ToString().Trim();
					dataTable = SQLserver.Data_GetDataTable(text3);
					if (dataTable == null || dataTable.Rows.Count <= 0)
					{
						Program.MsgBox((string)m_htab["Err03"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					text = text + Convert.ToInt32(dataTable.Rows[0]["Build_Code"]).ToString("X2") + "00";
					text3 = "," + dataTable.Rows[0]["Build_ID"].ToString() + ", '" + dataTable.Rows[0]["Build_Code"].ToString() + "', '" + dataTable.Rows[0]["Build_Name"].ToString() + "'";
					text3 += ",Null,'',''";
					break;
				case 12:
				{
					if (treeNode.Level != 2)
					{
						Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					text3 = "Select Build_ID, Build_Code, Build_Name, Floor_ID, Floor_Code, Floor_Name From v_HotelBF Where Floor_ID=" + treeNode.Name.ToString().Trim() + " And Build_ID=" + treeNode.Parent.Name.ToString();
					dataTable = SQLserver.Data_GetDataTable(text3);
					if (dataTable == null || dataTable.Rows.Count <= 0)
					{
						Program.MsgBox((string)m_htab["Err03"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					text = text + Convert.ToInt32(dataTable.Rows[0]["Build_Code"]).ToString("X2") + Convert.ToInt32(dataTable.Rows[0]["Floor_Code"]).ToString("X2");
					text3 = "," + dataTable.Rows[0]["Build_ID"].ToString() + ", '" + dataTable.Rows[0]["Build_Code"].ToString() + "',N'" + dataTable.Rows[0]["Build_Name"].ToString() + "'";
					string text4 = text3;
					text3 = text4 + "," + dataTable.Rows[0]["Floor_ID"].ToString() + ",'" + dataTable.Rows[0]["Floor_Code"].ToString() + "',N'" + dataTable.Rows[0]["Floor_Name"].ToString() + "'";
					break;
				}
				}
			}
			else
			{
				if (lvGrp.Items.Count <= 0)
				{
					Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				text3 = "";
				for (num2 = 0; num2 < lvGrp.Items.Count; num2++)
				{
					text += lvGrp.Items[num2].SubItems[1].Text.ToString();
					text3 = text3 + "#" + lvGrp.Items[num2].SubItems[2].Text.ToString() + ",";
				}
				for (; num2 < 2; num2++)
				{
					text += lvGrp.Items[lvGrp.Items.Count - 1].SubItems[1].Text.ToString();
					text3 = text3 + "#" + lvGrp.Items[lvGrp.Items.Count - 1].SubItems[2].Text.ToString() + ",";
				}
			}
			num2 = -1;
			num++;
			if (Program.RadioWriteCard(m_ct, num, text2, text, text.Length, Buzzer: false) == 0)
			{
				if (m_ct == 9)
				{
					text = text3;
					text3 = ",Null,'','',Null,'',''";
				}
				text2 = Program.GetStandDate(dtpCDate.Value);
				text3 = string.Concat("Insert Into  T_CardManage Values(", m_ct.ToString(), ",", num.ToString(), ",N'", txtUser.Text.Trim(), "',2,", obj, ",N'", txtCerNum.Text.Trim(), "'", text3);
				text3 += ",Null, '', '', NUll";
				if (m_ct == 9)
				{
					text3 += ",0,0";
				}
				else
				{
					string text5 = text3;
					text3 = text5 + ", " + Convert.ToInt16(chkOpL.Checked) + ", " + Convert.ToInt16(chkOpK.Checked);
				}
				object obj2 = text3;
				text3 = string.Concat(obj2, ",'", text2, "',0,'", dtpST.Value.ToString("HH:mm"), "','", dtpET.Value.ToString("HH:mm"), "',GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "'");
				object obj3 = text3;
				text3 = string.Concat(obj3, ",0,NULL, ", Program.m_opid, ",N'", Program.m_OperName, "', GetDate(), '", text, "','')");
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				if (SQLserver.Data_ExecuteSql(text3) <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					Program.RadioDevBuzzer(1, 2);
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void tabMain_Click(object sender, EventArgs e)
	{
		try
		{
			switch (tabMain.SelectedIndex)
			{
			case 0:
			{
				int selectedIndex = cobCType.SelectedIndex;
				cobCType.SelectedIndex = -1;
				cobCType.SelectedIndex = selectedIndex;
				break;
			}
			case 1:
				m_ct = 9;
				break;
			}
		}
		catch
		{
		}
	}

	private void cobCType_SelectedIndexChanged(object sender, EventArgs e)
	{
		CheckBox checkBox = chkOpK;
		bool flag = (chkOpL.Checked = false);
		checkBox.Checked = flag;
		CheckBox checkBox2 = chkOpK;
		bool visible = (chkOpL.Visible = false);
		checkBox2.Visible = visible;
		switch (cobCType.SelectedIndex)
		{
		case 0:
			m_ct = 10;
			break;
		case 1:
			m_ct = 11;
			break;
		case 2:
			m_ct = 13;
			break;
		case 3:
			m_ct = 12;
			break;
		}
		if (m_ct == 10)
		{
			chkOpK.Checked = true;
			CheckBox checkBox3 = chkOpK;
			bool visible2 = (chkOpL.Visible = true);
			checkBox3.Visible = visible2;
		}
		else if (m_ct == 11)
		{
			chkOpL.Checked = true;
			chkOpL.Visible = true;
		}
	}

	private void dgvGrp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex != -1)
			{
				if (lvGrp.Items.Count >= 2)
				{
					lvGrp.Items.RemoveAt(0);
				}
				string[] items = new string[4]
				{
					dgvGrp.Rows[e.RowIndex].Cells[1].Value.ToString(),
					Convert.ToInt32(dgvGrp.Rows[e.RowIndex].Cells[2].Value).ToString("X2"),
					dgvGrp.Rows[e.RowIndex].Cells[0].Value.ToString(),
					dgvGrp.Rows[e.RowIndex].Cells[2].Value.ToString()
				};
				lvGrp.Items.Add(new ListViewItem(items, 0));
			}
		}
		catch
		{
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		try
		{
			lvGrp.Items.Clear();
		}
		catch
		{
		}
	}

	private void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			for (int num = lvGrp.SelectedItems.Count - 1; num >= 0; num--)
			{
				lvGrp.SelectedItems[num].Remove();
			}
		}
		catch
		{
		}
	}

	private void btnRead_Click(object sender, EventArgs e)
	{
		object[] retdata = new object[256];
		Program.RadioReadCard(retdata, Buzzer: true, 1);
	}

	private void btnIDCard_Click(object sender, EventArgs e)
	{
		try
		{
			TextBox textBox = txtCerNum;
			string text = (txtUser.Text = "");
			textBox.Text = text;
			Program.IDCardData CardMsg = default(Program.IDCardData);
			if (Program.Get_IDCardII_Information(ref CardMsg) >= 0)
			{
				txtUser.Text = CardMsg.Name.Trim();
				txtCerNum.Text = CardMsg.IDCardNo;
			}
		}
		catch
		{
		}
	}

	private void btnIDCard_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(clsBackPanel2.Location.X + btnIDCard.Location.X - 30, clsBackPanel2.Location.Y + btnIDCard.Location.Y - 8);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_identity"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnIDCard_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void btnDel_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(btnDel.Location.X + clsBackPanel5.Location.X + tabPage2.Location.X + tabMain.Location.X + panel1.Location.X - 5, btnDel.Location.Y + panel1.Location.Y + tabMain.Location.Y + clsBackPanel5.Location.Y + tabPage2.Location.Y - 12);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_delete"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnDel_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
	}

	private void btnClear_MouseMove(object sender, MouseEventArgs e)
	{
		lb_1.Location = new Point(btnClear.Location.X + clsBackPanel5.Location.X + tabPage2.Location.X + tabMain.Location.X + panel1.Location.X - 5, btnClear.Location.Y + panel1.Location.Y + tabMain.Location.Y + clsBackPanel5.Location.Y + tabPage2.Location.Y - 12);
		lb_1.AutoSize = true;
		lb_1.Text = (string)m_htab["lb_clear"];
		lb_1.BringToFront();
		lb_1.Visible = true;
	}

	private void btnClear_MouseLeave(object sender, EventArgs e)
	{
		lb_1.Visible = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmECMgr));
		this.imglist = new System.Windows.Forms.ImageList(this.components);
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnIDCard = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnRead = new LockSoftware.Controls.GlassBtn(this.components);
		this.dtpET = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpST = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnCard = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.tabMain = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tvList = new System.Windows.Forms.TreeView();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.cobCType = new System.Windows.Forms.ComboBox();
		this.chkOpL = new System.Windows.Forms.CheckBox();
		this.chkOpK = new System.Windows.Forms.CheckBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.panel3 = new System.Windows.Forms.Panel();
		this.dgvGrp = new System.Windows.Forms.DataGridView();
		this.clsBackPanel5 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnClear = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDel = new LockSoftware.Controls.NGlassBtn(this.components);
		this.lvGrp = new System.Windows.Forms.ListView();
		this.dtpCDate = new System.Windows.Forms.DateTimePicker();
		this.txtUser = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtCerNum = new System.Windows.Forms.TextBox();
		this.cobCer = new System.Windows.Forms.ComboBox();
		this.clsBackPanel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.tabMain.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvGrp).BeginInit();
		this.clsBackPanel5.SuspendLayout();
		base.SuspendLayout();
		this.imglist.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imglist.ImageStream");
		this.imglist.TransparentColor = System.Drawing.Color.Transparent;
		this.imglist.Images.SetKeyName(0, "SNOW E AQUA PUBLIC.png");
		this.imglist.Images.SetKeyName(1, "OS00.png");
		this.imglist.Images.SetKeyName(2, "46.png");
		this.imglist.Images.SetKeyName(3, "ok.png");
		this.clsBackPanel2.AutoScroll = true;
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
		this.clsBackPanel2.Color2 = System.Drawing.SystemColors.Control;
		this.clsBackPanel2.ColorAngle = 270f;
		this.clsBackPanel2.Controls.Add(this.btnIDCard);
		this.clsBackPanel2.Controls.Add(this.btnRead);
		this.clsBackPanel2.Controls.Add(this.dtpET);
		this.clsBackPanel2.Controls.Add(this.label5);
		this.clsBackPanel2.Controls.Add(this.dtpST);
		this.clsBackPanel2.Controls.Add(this.label3);
		this.clsBackPanel2.Controls.Add(this.btnClose);
		this.clsBackPanel2.Controls.Add(this.btnCard);
		this.clsBackPanel2.Controls.Add(this.panel1);
		this.clsBackPanel2.Controls.Add(this.dtpCDate);
		this.clsBackPanel2.Controls.Add(this.txtUser);
		this.clsBackPanel2.Controls.Add(this.label2);
		this.clsBackPanel2.Controls.Add(this.label4);
		this.clsBackPanel2.Controls.Add(this.label1);
		this.clsBackPanel2.Controls.Add(this.txtCerNum);
		this.clsBackPanel2.Controls.Add(this.cobCer);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(365, 479);
		this.clsBackPanel2.TabIndex = 7;
		this.btnIDCard.BackColor = System.Drawing.Color.Transparent;
		this.btnIDCard.BaseColor = System.Drawing.Color.White;
		this.btnIDCard.ButtonColor = System.Drawing.Color.Silver;
		this.btnIDCard.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnIDCard.ButtonText = null;
		this.btnIDCard.CornerRadius = 2;
		this.btnIDCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnIDCard.Image = LockSoftware.Properties.Resources.V_Cer;
		this.btnIDCard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnIDCard.Location = new System.Drawing.Point(310, 316);
		this.btnIDCard.Name = "btnIDCard";
		this.btnIDCard.Size = new System.Drawing.Size(30, 26);
		this.btnIDCard.TabIndex = 43;
		this.btnIDCard.Click += new System.EventHandler(btnIDCard_Click);
		this.btnIDCard.MouseLeave += new System.EventHandler(btnIDCard_MouseLeave);
		this.btnIDCard.MouseMove += new System.Windows.Forms.MouseEventHandler(btnIDCard_MouseMove);
		this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnRead.AutoSize = true;
		this.btnRead.BackColor = System.Drawing.Color.LightGray;
		this.btnRead.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRead.ForeColor = System.Drawing.Color.Black;
		this.btnRead.GlowColor = System.Drawing.Color.White;
		this.btnRead.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRead.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnRead.Location = new System.Drawing.Point(24, 436);
		this.btnRead.Name = "btnRead";
		this.btnRead.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnRead.Size = new System.Drawing.Size(84, 30);
		this.btnRead.TabIndex = 14;
		this.btnRead.Text = "读 卡";
		this.btnRead.Click += new System.EventHandler(btnRead_Click);
		this.dtpET.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dtpET.CustomFormat = "HH:mm";
		this.dtpET.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.dtpET.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpET.Location = new System.Drawing.Point(249, 405);
		this.dtpET.Name = "dtpET";
		this.dtpET.ShowUpDown = true;
		this.dtpET.Size = new System.Drawing.Size(84, 23);
		this.dtpET.TabIndex = 13;
		this.dtpET.Value = new System.DateTime(2011, 1, 12, 23, 59, 0, 0);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(262, 411);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(17, 12);
		this.label5.TabIndex = 12;
		this.label5.Text = "→";
		this.dtpST.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dtpST.CustomFormat = "HH:mm";
		this.dtpST.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.dtpST.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpST.Location = new System.Drawing.Point(123, 405);
		this.dtpST.Name = "dtpST";
		this.dtpST.ShowUpDown = true;
		this.dtpST.Size = new System.Drawing.Size(84, 23);
		this.dtpST.TabIndex = 11;
		this.dtpST.Value = new System.DateTime(2011, 1, 12, 0, 0, 0, 0);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label3.Location = new System.Drawing.Point(17, 399);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 28);
		this.label3.TabIndex = 10;
		this.label3.Text = "可用时段：";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(255, 436);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnClose.Size = new System.Drawing.Size(75, 30);
		this.btnClose.TabIndex = 5;
		this.btnClose.Text = "关 闭";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnCard.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCard.AutoSize = true;
		this.btnCard.BackColor = System.Drawing.Color.LightGray;
		this.btnCard.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCard.ForeColor = System.Drawing.Color.Black;
		this.btnCard.GlowColor = System.Drawing.Color.White;
		this.btnCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCard.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCard.Location = new System.Drawing.Point(141, 436);
		this.btnCard.Name = "btnCard";
		this.btnCard.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnCard.Size = new System.Drawing.Size(84, 30);
		this.btnCard.TabIndex = 4;
		this.btnCard.Text = "写 卡";
		this.btnCard.Click += new System.EventHandler(btnCard_Click);
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.Controls.Add(this.tabMain);
		this.panel1.Location = new System.Drawing.Point(10, 12);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(347, 302);
		this.panel1.TabIndex = 2;
		this.tabMain.Controls.Add(this.tabPage1);
		this.tabMain.Controls.Add(this.tabPage2);
		this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabMain.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.tabMain.Location = new System.Drawing.Point(0, 0);
		this.tabMain.Multiline = true;
		this.tabMain.Name = "tabMain";
		this.tabMain.SelectedIndex = 0;
		this.tabMain.Size = new System.Drawing.Size(347, 302);
		this.tabMain.TabIndex = 8;
		this.tabMain.Click += new System.EventHandler(tabMain_Click);
		this.tabPage1.Controls.Add(this.tvList);
		this.tabPage1.Controls.Add(this.flowLayoutPanel1);
		this.tabPage1.Location = new System.Drawing.Point(4, 24);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(339, 274);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Urgent|Control|Building|Floor";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tvList.ImageIndex = 0;
		this.tvList.ImageList = this.imglist;
		this.tvList.Location = new System.Drawing.Point(3, 61);
		this.tvList.Name = "tvList";
		this.tvList.SelectedImageIndex = 3;
		this.tvList.Size = new System.Drawing.Size(333, 210);
		this.tvList.TabIndex = 1;
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.flowLayoutPanel1.Controls.Add(this.cobCType);
		this.flowLayoutPanel1.Controls.Add(this.chkOpL);
		this.flowLayoutPanel1.Controls.Add(this.chkOpK);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(333, 58);
		this.flowLayoutPanel1.TabIndex = 13;
		this.cobCType.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.cobCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCType.DropDownWidth = 200;
		this.cobCType.FormattingEnabled = true;
		this.cobCType.Items.AddRange(new object[4] { "Urgent Card", "Control Card", "Building Card", "Floor Card" });
		this.cobCType.Location = new System.Drawing.Point(3, 3);
		this.cobCType.Name = "cobCType";
		this.cobCType.Size = new System.Drawing.Size(114, 22);
		this.cobCType.TabIndex = 12;
		this.cobCType.SelectedIndexChanged += new System.EventHandler(cobCType_SelectedIndexChanged);
		this.chkOpL.AutoSize = true;
		this.chkOpL.BackColor = System.Drawing.Color.Transparent;
		this.chkOpL.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.chkOpL.Location = new System.Drawing.Point(123, 3);
		this.chkOpL.Name = "chkOpL";
		this.chkOpL.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
		this.chkOpL.Size = new System.Drawing.Size(78, 22);
		this.chkOpL.TabIndex = 11;
		this.chkOpL.Text = "Keep Open";
		this.chkOpL.UseVisualStyleBackColor = false;
		this.chkOpK.AutoSize = true;
		this.chkOpK.BackColor = System.Drawing.Color.Transparent;
		this.chkOpK.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.chkOpK.Location = new System.Drawing.Point(3, 31);
		this.chkOpK.Name = "chkOpK";
		this.chkOpK.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
		this.chkOpK.Size = new System.Drawing.Size(126, 22);
		this.chkOpK.TabIndex = 10;
		this.chkOpK.Text = "Open Inside Lock ";
		this.chkOpK.UseVisualStyleBackColor = false;
		this.tabPage2.Controls.Add(this.panel3);
		this.tabPage2.Controls.Add(this.clsBackPanel5);
		this.tabPage2.Location = new System.Drawing.Point(4, 24);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(339, 274);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Group";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.panel3.Controls.Add(this.dgvGrp);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(3, 3);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(333, 200);
		this.panel3.TabIndex = 55;
		this.dgvGrp.AllowUserToAddRows = false;
		this.dgvGrp.AllowUserToDeleteRows = false;
		this.dgvGrp.BackgroundColor = System.Drawing.Color.White;
		this.dgvGrp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvGrp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvGrp.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvGrp.Location = new System.Drawing.Point(0, 0);
		this.dgvGrp.MultiSelect = false;
		this.dgvGrp.Name = "dgvGrp";
		this.dgvGrp.ReadOnly = true;
		this.dgvGrp.RowHeadersWidth = 25;
		this.dgvGrp.RowTemplate.Height = 23;
		this.dgvGrp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvGrp.Size = new System.Drawing.Size(333, 200);
		this.dgvGrp.TabIndex = 10;
		this.dgvGrp.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvGrp_CellDoubleClick);
		this.clsBackPanel5.Border = true;
		this.clsBackPanel5.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderBW = 1;
		this.clsBackPanel5.BorderColorBottom = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderColorLeft = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderColorRight = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderColorTop = System.Drawing.Color.FromArgb(185, 209, 205);
		this.clsBackPanel5.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderLW = 1;
		this.clsBackPanel5.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderRW = 1;
		this.clsBackPanel5.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel5.BorderTW = 0;
		this.clsBackPanel5.Color1 = System.Drawing.Color.White;
		this.clsBackPanel5.Color2 = System.Drawing.Color.Beige;
		this.clsBackPanel5.ColorAngle = 90f;
		this.clsBackPanel5.Controls.Add(this.btnClear);
		this.clsBackPanel5.Controls.Add(this.btnDel);
		this.clsBackPanel5.Controls.Add(this.lvGrp);
		this.clsBackPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel5.Location = new System.Drawing.Point(3, 203);
		this.clsBackPanel5.Name = "clsBackPanel5";
		this.clsBackPanel5.Padding = new System.Windows.Forms.Padding(1);
		this.clsBackPanel5.Size = new System.Drawing.Size(333, 68);
		this.clsBackPanel5.TabIndex = 12;
		this.btnClear.BackColor = System.Drawing.Color.Transparent;
		this.btnClear.BaseColor = System.Drawing.Color.White;
		this.btnClear.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnClear.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnClear.ButtonText = null;
		this.btnClear.CornerRadius = 2;
		this.btnClear.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClear.Image = LockSoftware.Properties.Resources.clear;
		this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnClear.ImageSize = new System.Drawing.Size(16, 16);
		this.btnClear.Location = new System.Drawing.Point(228, 35);
		this.btnClear.Margin = new System.Windows.Forms.Padding(1);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(24, 24);
		this.btnClear.TabIndex = 8;
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		this.btnClear.MouseLeave += new System.EventHandler(btnClear_MouseLeave);
		this.btnClear.MouseMove += new System.Windows.Forms.MouseEventHandler(btnClear_MouseMove);
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
		this.btnDel.Location = new System.Drawing.Point(228, 9);
		this.btnDel.Margin = new System.Windows.Forms.Padding(1);
		this.btnDel.Name = "btnDel";
		this.btnDel.Size = new System.Drawing.Size(24, 24);
		this.btnDel.TabIndex = 9;
		this.btnDel.Click += new System.EventHandler(btnDel_Click);
		this.btnDel.MouseLeave += new System.EventHandler(btnDel_MouseLeave);
		this.btnDel.MouseMove += new System.Windows.Forms.MouseEventHandler(btnDel_MouseMove);
		this.lvGrp.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lvGrp.Dock = System.Windows.Forms.DockStyle.Left;
		this.lvGrp.LargeImageList = this.imglist;
		this.lvGrp.Location = new System.Drawing.Point(1, 1);
		this.lvGrp.Name = "lvGrp";
		this.lvGrp.Size = new System.Drawing.Size(220, 66);
		this.lvGrp.TabIndex = 3;
		this.lvGrp.UseCompatibleStateImageBehavior = false;
		this.dtpCDate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dtpCDate.CustomFormat = "yyyy-MM-dd";
		this.dtpCDate.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.dtpCDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCDate.Location = new System.Drawing.Point(123, 376);
		this.dtpCDate.Name = "dtpCDate";
		this.dtpCDate.Size = new System.Drawing.Size(217, 23);
		this.dtpCDate.TabIndex = 4;
		this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtUser.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.txtUser.Location = new System.Drawing.Point(123, 319);
		this.txtUser.MaxLength = 50;
		this.txtUser.Name = "txtUser";
		this.txtUser.Size = new System.Drawing.Size(180, 23);
		this.txtUser.TabIndex = 6;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label2.Location = new System.Drawing.Point(17, 371);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 28);
		this.label2.TabIndex = 3;
		this.label2.Text = "卡片有效期：";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label4.Location = new System.Drawing.Point(17, 343);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 28);
		this.label4.TabIndex = 9;
		this.label4.Text = "证件类型：";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label1.Location = new System.Drawing.Point(17, 315);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 28);
		this.label1.TabIndex = 5;
		this.label1.Text = "持卡人：";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtCerNum.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtCerNum.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.txtCerNum.Location = new System.Drawing.Point(196, 348);
		this.txtCerNum.MaxLength = 50;
		this.txtCerNum.Name = "txtCerNum";
		this.txtCerNum.Size = new System.Drawing.Size(144, 23);
		this.txtCerNum.TabIndex = 8;
		this.cobCer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cobCer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCer.DropDownWidth = 150;
		this.cobCer.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.cobCer.FormattingEnabled = true;
		this.cobCer.Location = new System.Drawing.Point(123, 348);
		this.cobCer.Name = "cobCer";
		this.cobCer.Size = new System.Drawing.Size(70, 22);
		this.cobCer.TabIndex = 7;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(365, 479);
		base.Controls.Add(this.clsBackPanel2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "frmECMgr";
		this.Text = "员工卡";
		base.Load += new System.EventHandler(frmECMgr_Load);
		this.clsBackPanel2.ResumeLayout(false);
		this.clsBackPanel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.tabMain.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvGrp).EndInit();
		this.clsBackPanel5.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
