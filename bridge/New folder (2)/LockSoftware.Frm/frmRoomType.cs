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

public class frmRoomType : Form
{
	public string m_objName = "WFrt";

	public Hashtable m_htab;

	private IContainer components;

	private ToolsBtn toolsBtn1;

	private clsBackPanel plMain;

	private DataGridView dgvList;

	private TextBox txtPrice;

	private Label label1;

	private TextBox txtName;

	private Label label2;

	private NGlassBtn btnDis;

	private NGlassBtn btnClose;

	private NGlassBtn btnNew;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private TextBox txtSize;

	private TextBox txtOHP;

	private TextBox txtHRP;

	private TextBox txtBed;

	private TextBox txtMemo;

	private Label label7;

	private TextBox txtDepo;

	private Label label8;

	private NGlassBtn btnRes;

	private int _tp_id { get; set; }

	protected int TP_ID
	{
		get
		{
			return _tp_id;
		}
		set
		{
			if (value > 0)
			{
				btnNew.ButtonText = m_htab["SaveText"].ToString();
				btnClose.ButtonText = m_htab["CancelText"].ToString();
				btnNew.Image = Resources.save;
			}
			else
			{
				btnNew.ButtonText = m_htab["btnNew"].ToString();
				btnClose.ButtonText = m_htab["btnClose"].ToString();
				btnNew.Image = Resources.Add;
			}
			_tp_id = value;
		}
	}

	public frmRoomType()
	{
		InitializeComponent();
		base.MinimizeBox = (base.MaximizeBox = false);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.StartPosition = FormStartPosition.CenterScreen;
		m_htab = Program.GetControlName(this, m_objName);
		InitDgvList();
	}

	public void InitDgvList()
	{
		string sql = "Select  TP_ID, TP_Name, TP_Price, TP_deposit, TP_BedCount, TP_PricelessHour, TP_PriceStandHour, TP_RSize, TP_Flag, TP_Memo From D_RoomType Order by  TP_Name";
		DataTable dataTable = null;
		try
		{
			dataTable = SQLserver.Data_GetDataTable(sql);
			dgvList.DataSource = dataTable.DefaultView;
			if (dgvList.DataSource != null)
			{
				dgvList.Columns[0].Visible = false;
				for (int i = 1; i < dgvList.Columns.Count; i++)
				{
					dgvList.Columns[i].HeaderText = (string)m_htab["dgvcol" + dgvList.Columns[i].Name];
				}
				dgvList.AutoResizeColumns();
			}
		}
		catch (Exception ex)
		{
			if (dataTable != null)
			{
				dataTable.Clear();
				dataTable.Dispose();
			}
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtName.Text.Trim() == "" || txtBed.Text.Trim() == "" || txtPrice.Text.Trim() == "" || txtHRP.Text.Trim() == "" || txtOHP.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (TP_ID <= 0)
			{
				Convert.ToDouble("0" + txtPrice.Text.Trim());
				Convert.ToDouble("0" + txtDepo.Text.Trim());
				string text = "";
				text = "Select * From D_RoomType Where TP_Name=N'" + txtName.Text.Trim() + "'";
				DataTable dataTable = SQLserver.Data_GetDataTable(text);
				if (dataTable == null)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (dataTable.Rows.Count > 0)
				{
					Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				text = "Insert into D_RoomType values(N'" + txtName.Text.Trim() + "'," + Program.GetStandDec(txtPrice.Text.Trim()) + "," + Program.GetStandDec(txtDepo.Text.Trim()) + "," + txtBed.Text.Trim() + "," + Program.GetStandDec(txtHRP.Text.Trim()) + "," + Program.GetStandDec(txtOHP.Text.Trim()) + ",N'" + txtSize.Text.Trim() + "',0,GetDate()," + Program.m_opid + ",NULL,NULL,N'" + txtMemo.Text.Trim() + "')";
				if (SQLserver.Data_ExecuteSql(text) < 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				txtName.Text = "";
				InitDgvList();
			}
			else
			{
				string sqlstr = $"update D_RoomType set TP_Name=N'{txtName.Text.Trim()}',TP_Price={Program.GetStandDec(txtPrice.Text.Trim())},TP_deposit={Program.GetStandDec(txtDepo.Text.Trim())},TP_BedCount={txtBed.Text.Trim()},TP_PricelessHour={Program.GetStandDec(txtHRP.Text.Trim())},TP_PriceStandHour={Program.GetStandDec(txtOHP.Text.Trim())},TP_RSize=N'{Program.GetStandDec(txtSize.Text.Trim())}',TP_Memo=N'{txtMemo.Text.Trim()}',[UpdateTime]=GetDate(),[Updator_ID]={Program.m_opid} where TP_ID={TP_ID}";
				int num = SQLserver.Data_ExecuteSql(sqlstr);
				if (num < 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				TP_ID = 0;
				txtName.Text = "";
				InitDgvList();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnDis_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.DataSource == null || dgvList.SelectedRows.Count <= 0 || Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			string text = "";
			for (int i = 0; i < dgvList.SelectedRows.Count; i++)
			{
				text = text + ", " + dgvList.SelectedRows[i].Cells["TP_ID"].Value.ToString();
			}
			if (!(text == ""))
			{
				text = text.Substring(1);
				string sqlquery = "Update D_RoomType Set TP_Flag = 1, UpdateTime=GetDate(), Updator_ID=" + Program.m_opid + " Where TP_ID in (" + text + ") \n Delete From D_RoomType Where TP_ID not in ( select distinct R_TypeID As TP_ID from D_Rooms Where R_TypeID in (" + text + ") ) And TP_ID in(" + text + ")";
				int num = Program.DBCompExec(sqlquery, btnDis.ButtonText);
				if (num <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					InitDgvList();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err03"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (TP_ID > 0)
		{
			txtName.Text = "";
			TP_ID = 0;
		}
		else
		{
			Close();
		}
	}

	private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void txtBed_KeyPress(object sender, KeyPressEventArgs e)
	{
		CheckInfo.NumberKeyPress(sender, e, 1, 20L);
	}

	private void txtHRP_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void txtOHP_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void btnRes_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.SelectedRows.Count <= 0)
			{
				return;
			}
			string text = "";
			for (int i = 0; i < dgvList.SelectedRows.Count; i++)
			{
				if (dgvList.SelectedRows[i].Cells["TP_Flag"].Value.ToString() == "True")
				{
					text = text + ", " + dgvList.SelectedRows[i].Cells["TP_ID"].Value.ToString();
				}
			}
			if (!(text == "") && Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				text = text.Substring(1);
				string sqlstr = "Update D_RoomType Set TP_Flag = 0, UpdateTime=GetDate(), Updator_ID=" + Program.m_opid + " Where TP_ID in (" + text + ") ";
				int num = SQLserver.Data_ExecuteSql(sqlstr);
				if (num <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					InitDgvList();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0)
			{
				TP_ID = int.Parse(dgvList.Rows[e.RowIndex].Cells["TP_ID"].Value.ToString());
				txtName.Text = dgvList.Rows[e.RowIndex].Cells["TP_Name"].Value.ToString();
				txtPrice.Text = dgvList.Rows[e.RowIndex].Cells["TP_Price"].Value.ToString();
				txtDepo.Text = dgvList.Rows[e.RowIndex].Cells["TP_deposit"].Value.ToString();
				txtHRP.Text = dgvList.Rows[e.RowIndex].Cells["TP_PricelessHour"].Value.ToString();
				txtOHP.Text = dgvList.Rows[e.RowIndex].Cells["TP_PriceStandHour"].Value.ToString();
				txtSize.Text = dgvList.Rows[e.RowIndex].Cells["TP_RSize"].Value.ToString();
				txtBed.Text = dgvList.Rows[e.RowIndex].Cells["TP_BedCount"].Value.ToString();
				txtMemo.Text = dgvList.Rows[e.RowIndex].Cells["TP_Memo"].Value.ToString();
			}
		}
		catch
		{
		}
	}

	private void txtDepo_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\b')
		{
			return;
		}
		if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0])
		{
			if ((txtDepo.Text.Trim().Length == 0 || txtDepo.Text.Trim().IndexOf(e.KeyChar) >= 0) && e.KeyChar == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0])
			{
				e.Handled = true;
			}
		}
		else
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmRoomType));
		this.btnRes = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDis = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnNew = new LockSoftware.Controls.NGlassBtn(this.components);
		this.plMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.txtDepo = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtMemo = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtSize = new System.Windows.Forms.TextBox();
		this.txtOHP = new System.Windows.Forms.TextBox();
		this.txtHRP = new System.Windows.Forms.TextBox();
		this.txtBed = new System.Windows.Forms.TextBox();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.txtPrice = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.plMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		base.SuspendLayout();
		this.btnRes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRes.BackColor = System.Drawing.Color.Transparent;
		this.btnRes.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnRes.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnRes.ButtonText = "Restore";
		this.btnRes.CornerRadius = 4;
		this.btnRes.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRes.GlowColor = System.Drawing.Color.White;
		this.btnRes.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRes.Location = new System.Drawing.Point(341, 38);
		this.btnRes.Name = "btnRes";
		this.btnRes.Size = new System.Drawing.Size(88, 35);
		this.btnRes.TabIndex = 11;
		this.btnRes.Click += new System.EventHandler(btnRes_Click);
		this.btnDis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDis.BackColor = System.Drawing.Color.Transparent;
		this.btnDis.BaseColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDis.ButtonColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.btnDis.ButtonText = "Disabled";
		this.btnDis.CornerRadius = 4;
		this.btnDis.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDis.GlowColor = System.Drawing.Color.White;
		this.btnDis.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDis.Location = new System.Drawing.Point(247, 38);
		this.btnDis.Name = "btnDis";
		this.btnDis.Size = new System.Drawing.Size(88, 35);
		this.btnDis.TabIndex = 10;
		this.btnDis.Click += new System.EventHandler(btnDis_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnClose.ButtonText = "Close";
		this.btnClose.CornerRadius = 4;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Location = new System.Drawing.Point(435, 38);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(83, 35);
		this.btnClose.TabIndex = 12;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnNew.BackColor = System.Drawing.Color.Transparent;
		this.btnNew.BaseColor = System.Drawing.Color.FromArgb(0, 192, 192);
		this.btnNew.ButtonColor = System.Drawing.Color.Teal;
		this.btnNew.ButtonText = "New Type";
		this.btnNew.CornerRadius = 4;
		this.btnNew.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNew.GlowColor = System.Drawing.Color.White;
		this.btnNew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNew.Image = LockSoftware.Properties.Resources.Add;
		this.btnNew.Location = new System.Drawing.Point(131, 38);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(110, 35);
		this.btnNew.TabIndex = 9;
		this.btnNew.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
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
		this.plMain.Controls.Add(this.txtDepo);
		this.plMain.Controls.Add(this.label8);
		this.plMain.Controls.Add(this.txtMemo);
		this.plMain.Controls.Add(this.label7);
		this.plMain.Controls.Add(this.label6);
		this.plMain.Controls.Add(this.label5);
		this.plMain.Controls.Add(this.label4);
		this.plMain.Controls.Add(this.label3);
		this.plMain.Controls.Add(this.txtSize);
		this.plMain.Controls.Add(this.txtOHP);
		this.plMain.Controls.Add(this.txtHRP);
		this.plMain.Controls.Add(this.txtBed);
		this.plMain.Controls.Add(this.dgvList);
		this.plMain.Controls.Add(this.txtPrice);
		this.plMain.Controls.Add(this.label1);
		this.plMain.Controls.Add(this.txtName);
		this.plMain.Controls.Add(this.label2);
		this.plMain.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.plMain.Location = new System.Drawing.Point(3, 81);
		this.plMain.Name = "plMain";
		this.plMain.Size = new System.Drawing.Size(524, 378);
		this.plMain.TabIndex = 1;
		this.txtDepo.Location = new System.Drawing.Point(372, 44);
		this.txtDepo.MaxLength = 10;
		this.txtDepo.Name = "txtDepo";
		this.txtDepo.Size = new System.Drawing.Size(143, 22);
		this.txtDepo.TabIndex = 3;
		this.txtDepo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDepo_KeyPress);
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Location = new System.Drawing.Point(230, 44);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(140, 20);
		this.label8.TabIndex = 14;
		this.label8.Text = "Deposit:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtMemo.Location = new System.Drawing.Point(80, 141);
		this.txtMemo.Multiline = true;
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtMemo.Size = new System.Drawing.Size(435, 67);
		this.txtMemo.TabIndex = 8;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(9, 141);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(44, 14);
		this.label7.TabIndex = 12;
		this.label7.Text = "Memo:";
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(9, 103);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(70, 28);
		this.label6.TabIndex = 11;
		this.label6.Text = "Room Size:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(225, 72);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(145, 28);
		this.label5.TabIndex = 10;
		this.label5.Text = "One Hour Price:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(9, 71);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(70, 28);
		this.label4.TabIndex = 9;
		this.label4.Text = "Hour Price:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(225, 104);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(145, 28);
		this.label3.TabIndex = 8;
		this.label3.Text = "Bed Count:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtSize.Location = new System.Drawing.Point(80, 108);
		this.txtSize.MaxLength = 100;
		this.txtSize.Name = "txtSize";
		this.txtSize.Size = new System.Drawing.Size(140, 22);
		this.txtSize.TabIndex = 6;
		this.txtOHP.Location = new System.Drawing.Point(372, 76);
		this.txtOHP.MaxLength = 8;
		this.txtOHP.Name = "txtOHP";
		this.txtOHP.Size = new System.Drawing.Size(143, 22);
		this.txtOHP.TabIndex = 5;
		this.txtOHP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtOHP_KeyPress);
		this.txtHRP.Location = new System.Drawing.Point(80, 76);
		this.txtHRP.MaxLength = 8;
		this.txtHRP.Name = "txtHRP";
		this.txtHRP.Size = new System.Drawing.Size(140, 22);
		this.txtHRP.TabIndex = 4;
		this.txtHRP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtHRP_KeyPress);
		this.txtBed.Location = new System.Drawing.Point(372, 108);
		this.txtBed.MaxLength = 3;
		this.txtBed.Name = "txtBed";
		this.txtBed.Size = new System.Drawing.Size(143, 22);
		this.txtBed.TabIndex = 7;
		this.txtBed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBed_KeyPress);
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Location = new System.Drawing.Point(9, 212);
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(506, 158);
		this.dgvList.TabIndex = 13;
		this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellDoubleClick);
		this.txtPrice.Location = new System.Drawing.Point(80, 44);
		this.txtPrice.MaxLength = 8;
		this.txtPrice.Name = "txtPrice";
		this.txtPrice.Size = new System.Drawing.Size(140, 22);
		this.txtPrice.TabIndex = 2;
		this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrice_KeyPress);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(9, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(42, 14);
		this.label1.TabIndex = 0;
		this.label1.Text = "Name:";
		this.txtName.Location = new System.Drawing.Point(80, 12);
		this.txtName.MaxLength = 100;
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(435, 22);
		this.txtName.TabIndex = 1;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(9, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 14);
		this.label2.TabIndex = 2;
		this.label2.Text = "Price:";
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources._08;
		this.toolsBtn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
		this.toolsBtn1.Size = new System.Drawing.Size(530, 78);
		this.toolsBtn1.TabIndex = 2;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Room Type: Set room's type.";
		this.toolsBtn1.TextRedrawed = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(530, 463);
		base.Controls.Add(this.btnRes);
		base.Controls.Add(this.btnDis);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.plMain);
		base.Controls.Add(this.toolsBtn1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmRoomType";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Room Type";
		this.plMain.ResumeLayout(false);
		this.plMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		base.ResumeLayout(false);
	}
}
