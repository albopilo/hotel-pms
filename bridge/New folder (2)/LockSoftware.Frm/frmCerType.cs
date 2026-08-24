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

public class frmCerType : Form
{
	public string m_objName = "WFgct";

	public Hashtable m_htab;

	private int _cer_id;

	private IContainer components;

	private ToolsBtn toolsBtn1;

	private clsBackPanel plMain;

	private TextBox txten;

	private Label label1;

	private TextBox txtcn;

	private Label label2;

	private DataGridView dgvList;

	private NGlassBtn btnClose;

	private NGlassBtn btnNew;

	private NGlassBtn btnDis;

	private NGlassBtn btnRes;

	private TableLayoutPanel tableLayoutPanel1;

	private int Cer_id
	{
		get
		{
			return _cer_id;
		}
		set
		{
			if (value > 0)
			{
				btnNew.ButtonText = m_htab["SaveButton"].ToString();
				btnClose.ButtonText = m_htab["CancelButton"].ToString();
				btnNew.Image = Resources.save;
			}
			else
			{
				btnNew.ButtonText = m_htab["btnNew"].ToString();
				btnClose.ButtonText = m_htab["btnClose"].ToString();
				btnNew.Image = Resources.Add;
			}
			_cer_id = value;
		}
	}

	public frmCerType()
	{
		InitializeComponent();
		base.MinimizeBox = (base.MaximizeBox = false);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void frmCerType_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		InitDgvList();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (Cer_id > 0)
		{
			txtcn.Text = "";
			Cer_id = 0;
		}
		else
		{
			Close();
		}
	}

	public void InitDgvList()
	{
		string sql = "Select cer_id, cer_name, cer_flag From D_Cer Order by cer_flag, cer_id desc";
		DataTable dataTable = null;
		try
		{
			dataTable = SQLserver.Data_GetDataTable(sql);
			dgvList.DataSource = dataTable.DefaultView;
			if (dgvList.DataSource != null)
			{
				dgvList.Columns[0].Visible = false;
				dgvList.Columns[1].HeaderText = (string)m_htab["dgvcol1"];
				dgvList.Columns[2].HeaderText = (string)m_htab["dgvcol3"];
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
			if (txtcn.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (Cer_id > 0)
			{
				string sqlstr = $"Update D_Cer Set [cer_name]=N'{txtcn.Text.Trim()}' where [cer_id]=N'{Cer_id}'";
				if (SQLserver.Data_ExecuteSql(sqlstr) < 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					txtcn.Text = "";
					Cer_id = 0;
				}
			}
			else
			{
				string text = "";
				text = "Select * From D_Cer Where cer_name=N'" + txtcn.Text.Trim() + "'";
				DataTable dataTable = SQLserver.Data_GetDataTable(text);
				if (dataTable == null)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (dataTable.Rows.Count > 0)
				{
					Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				text = "Insert into D_Cer values(N'" + txtcn.Text.Trim().Replace("'", "''") + "',0, GetDate()," + Program.m_opid + ",'" + Program.m_OperName + "N',NULL,NULL,NULL)";
				if (SQLserver.Data_ExecuteSql(text) < 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				txtcn.Text = "";
			}
			InitDgvList();
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
			if (dgvList.SelectedRows.Count <= 0 || Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			string text = "";
			for (int i = 0; i < dgvList.SelectedRows.Count; i++)
			{
				text = text + ", " + dgvList.SelectedRows[i].Cells["cer_id"].Value.ToString();
			}
			if (!(text == ""))
			{
				text = text.Substring(1);
				string sqlquery = "Update D_Cer Set cer_flag = 1, updatetime=GetDate(), updatorid=" + Program.m_opid + ", updator=N'" + Program.m_OperName + "' Where cer_id in (" + text + ")  ";
				int num = Program.DBCompExec(sqlquery, btnDis.ButtonText);
				if (num < 0)
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
				if (Convert.ToInt16(dgvList.SelectedRows[i].Cells["cer_flag"].Value) == 1)
				{
					text = text + ", " + dgvList.SelectedRows[i].Cells["cer_id"].Value.ToString();
				}
			}
			if (!(text == "") && Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				text = text.Substring(1);
				string sqlstr = "Update D_Cer Set cer_flag = 0, updatetime=GetDate(), updatorid=" + Program.m_opid + ", updator=N'" + Program.m_OperName + "' Where cer_id in (" + text + ") ";
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
		if (dgvList.Rows.Count <= 0 || dgvList.SelectedRows.Count <= 0)
		{
			return;
		}
		try
		{
			txtcn.Text = dgvList.SelectedRows[0].Cells[1].Value.ToString();
			Cer_id = int.Parse(dgvList.SelectedRows[0].Cells[0].Value.ToString());
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmCerType));
		this.btnRes = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDis = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnNew = new LockSoftware.Controls.NGlassBtn(this.components);
		this.plMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.txtcn = new System.Windows.Forms.TextBox();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.txten = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.plMain.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
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
		this.btnRes.Location = new System.Drawing.Point(312, 38);
		this.btnRes.Name = "btnRes";
		this.btnRes.Size = new System.Drawing.Size(88, 35);
		this.btnRes.TabIndex = 7;
		this.btnRes.Click += new System.EventHandler(btnRes_Click);
		this.btnDis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDis.BackColor = System.Drawing.Color.Transparent;
		this.btnDis.BaseColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDis.ButtonColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.btnDis.ButtonText = "Disable";
		this.btnDis.CornerRadius = 4;
		this.btnDis.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDis.GlowColor = System.Drawing.Color.White;
		this.btnDis.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDis.Location = new System.Drawing.Point(218, 38);
		this.btnDis.Name = "btnDis";
		this.btnDis.Size = new System.Drawing.Size(88, 35);
		this.btnDis.TabIndex = 5;
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
		this.btnClose.Location = new System.Drawing.Point(406, 38);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(83, 35);
		this.btnClose.TabIndex = 6;
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
		this.btnNew.Location = new System.Drawing.Point(102, 38);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(110, 35);
		this.btnNew.TabIndex = 4;
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
		this.plMain.Controls.Add(this.tableLayoutPanel1);
		this.plMain.Controls.Add(this.dgvList);
		this.plMain.Controls.Add(this.txten);
		this.plMain.Controls.Add(this.label2);
		this.plMain.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.plMain.Location = new System.Drawing.Point(3, 81);
		this.plMain.Name = "plMain";
		this.plMain.Size = new System.Drawing.Size(495, 296);
		this.plMain.TabIndex = 3;
		this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtcn, 1, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 9);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(481, 31);
		this.tableLayoutPanel1.TabIndex = 4;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label1.Size = new System.Drawing.Size(74, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "Type Name:";
		this.txtcn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtcn.Location = new System.Drawing.Point(83, 3);
		this.txtcn.MaxLength = 100;
		this.txtcn.Name = "txtcn";
		this.txtcn.Size = new System.Drawing.Size(396, 22);
		this.txtcn.TabIndex = 1;
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Location = new System.Drawing.Point(9, 40);
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(477, 248);
		this.dgvList.TabIndex = 3;
		this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellDoubleClick);
		this.txten.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txten.Location = new System.Drawing.Point(80, 48);
		this.txten.Name = "txten";
		this.txten.Size = new System.Drawing.Size(406, 22);
		this.txten.TabIndex = 2;
		this.txten.Visible = false;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(9, 51);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(60, 14);
		this.label2.TabIndex = 2;
		this.label2.Text = "Name-En:";
		this.label2.Visible = false;
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources.V_Cer;
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
		this.toolsBtn1.Size = new System.Drawing.Size(503, 78);
		this.toolsBtn1.TabIndex = 2;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Certificate Type: Setting guest's certificate.";
		this.toolsBtn1.TextRedrawed = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(503, 382);
		base.Controls.Add(this.btnRes);
		base.Controls.Add(this.btnDis);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.plMain);
		base.Controls.Add(this.toolsBtn1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCerType";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Certificate Type";
		base.Load += new System.EventHandler(frmCerType_Load);
		this.plMain.ResumeLayout(false);
		this.plMain.PerformLayout();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		base.ResumeLayout(false);
	}
}
