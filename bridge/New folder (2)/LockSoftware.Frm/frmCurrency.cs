using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmCurrency : Form
{
	private IContainer components;

	private ToolsBtn toolsBtn1;

	private NGlassBtn btnModify;

	private NGlassBtn btnClose;

	private NGlassBtn btnNew;

	private clsBackPanel plMain;

	private Label label3;

	private TextBox txtSign;

	private DataGridView dgvList;

	private TextBox txtRate;

	private Label label1;

	private TextBox txtName;

	private Label label2;

	private CheckBox chkBase;

	private TextBox txtid;

	private NGlassBtn btnDis;

	private FlowLayoutPanel flowLayoutPanel1;

	public string m_objName = "WFcurrt";

	public Hashtable m_htab;

	private byte[] m_enableImg;

	private byte[] m_disableImg;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmCurrency));
		this.txtid = new System.Windows.Forms.TextBox();
		this.btnDis = new LockSoftware.Controls.NGlassBtn(this.components);
		this.plMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.label1 = new System.Windows.Forms.Label();
		this.txtName = new System.Windows.Forms.TextBox();
		this.chkBase = new System.Windows.Forms.CheckBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtSign = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtRate = new System.Windows.Forms.TextBox();
		this.btnModify = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnNew = new LockSoftware.Controls.NGlassBtn(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.plMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.flowLayoutPanel1.SuspendLayout();
		base.SuspendLayout();
		this.txtid.Location = new System.Drawing.Point(129, 1);
		this.txtid.Name = "txtid";
		this.txtid.Size = new System.Drawing.Size(53, 21);
		this.txtid.TabIndex = 15;
		this.txtid.Visible = false;
		this.btnDis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDis.BackColor = System.Drawing.Color.Transparent;
		this.btnDis.BaseColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDis.ButtonColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.btnDis.ButtonText = "Delete";
		this.btnDis.CornerRadius = 4;
		this.btnDis.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDis.GlowColor = System.Drawing.Color.White;
		this.btnDis.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDis.Location = new System.Drawing.Point(277, 40);
		this.btnDis.Name = "btnDis";
		this.btnDis.Size = new System.Drawing.Size(85, 35);
		this.btnDis.TabIndex = 9;
		this.btnDis.Click += new System.EventHandler(btnDis_Click);
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
		this.plMain.Controls.Add(this.dgvList);
		this.plMain.Controls.Add(this.flowLayoutPanel1);
		this.plMain.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.plMain.Location = new System.Drawing.Point(3, 81);
		this.plMain.Name = "plMain";
		this.plMain.Padding = new System.Windows.Forms.Padding(5);
		this.plMain.Size = new System.Drawing.Size(549, 231);
		this.plMain.TabIndex = 0;
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(5, 36);
		this.dgvList.MultiSelect = false;
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(539, 190);
		this.dgvList.TabIndex = 5;
		this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellClick);
		this.flowLayoutPanel1.AutoSize = true;
		this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel1.Controls.Add(this.label1);
		this.flowLayoutPanel1.Controls.Add(this.txtName);
		this.flowLayoutPanel1.Controls.Add(this.chkBase);
		this.flowLayoutPanel1.Controls.Add(this.label3);
		this.flowLayoutPanel1.Controls.Add(this.txtSign);
		this.flowLayoutPanel1.Controls.Add(this.label2);
		this.flowLayoutPanel1.Controls.Add(this.txtRate);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
		this.flowLayoutPanel1.Size = new System.Drawing.Size(539, 31);
		this.flowLayoutPanel1.TabIndex = 16;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(0, 5);
		this.label1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(42, 14);
		this.label1.TabIndex = 0;
		this.label1.Text = "Name:";
		this.txtName.Location = new System.Drawing.Point(45, 3);
		this.txtName.MaxLength = 20;
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(60, 22);
		this.txtName.TabIndex = 1;
		this.chkBase.AutoSize = true;
		this.chkBase.BackColor = System.Drawing.Color.Transparent;
		this.chkBase.Location = new System.Drawing.Point(111, 3);
		this.chkBase.Name = "chkBase";
		this.chkBase.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
		this.chkBase.Size = new System.Drawing.Size(104, 19);
		this.chkBase.TabIndex = 4;
		this.chkBase.Text = "Basic Currency";
		this.chkBase.UseVisualStyleBackColor = false;
		this.chkBase.CheckedChanged += new System.EventHandler(chkBase_CheckedChanged);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(223, 5);
		this.label3.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(34, 14);
		this.label3.TabIndex = 8;
		this.label3.Text = "Sign:";
		this.txtSign.Location = new System.Drawing.Point(260, 3);
		this.txtSign.MaxLength = 20;
		this.txtSign.Name = "txtSign";
		this.txtSign.Size = new System.Drawing.Size(60, 22);
		this.txtSign.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(328, 5);
		this.label2.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 14);
		this.label2.TabIndex = 2;
		this.label2.Text = "Ex-Rate:";
		this.txtRate.Location = new System.Drawing.Point(384, 3);
		this.txtRate.MaxLength = 10;
		this.txtRate.Name = "txtRate";
		this.txtRate.Size = new System.Drawing.Size(80, 22);
		this.txtRate.TabIndex = 3;
		this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRate_KeyPress);
		this.btnModify.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnModify.BackColor = System.Drawing.Color.Transparent;
		this.btnModify.BaseColor = System.Drawing.Color.FromArgb(0, 192, 192);
		this.btnModify.ButtonColor = System.Drawing.Color.Teal;
		this.btnModify.ButtonText = "Modify";
		this.btnModify.CornerRadius = 4;
		this.btnModify.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModify.GlowColor = System.Drawing.Color.White;
		this.btnModify.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnModify.Location = new System.Drawing.Point(368, 40);
		this.btnModify.Name = "btnModify";
		this.btnModify.Size = new System.Drawing.Size(85, 35);
		this.btnModify.TabIndex = 7;
		this.btnModify.Click += new System.EventHandler(btnModify_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnClose.ButtonText = "Close";
		this.btnClose.CornerRadius = 4;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Location = new System.Drawing.Point(459, 40);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(85, 35);
		this.btnClose.TabIndex = 8;
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
		this.btnNew.Location = new System.Drawing.Point(161, 40);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(110, 35);
		this.btnNew.TabIndex = 6;
		this.btnNew.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources._111;
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
		this.toolsBtn1.Size = new System.Drawing.Size(555, 78);
		this.toolsBtn1.TabIndex = 1;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Currency Type: Set hotel's currency type.";
		this.toolsBtn1.TextRedrawed = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(555, 316);
		base.Controls.Add(this.btnDis);
		base.Controls.Add(this.txtid);
		base.Controls.Add(this.plMain);
		base.Controls.Add(this.btnModify);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.toolsBtn1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmCurrency";
		this.Text = "frmCurrency";
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmCurrency_FormClosed);
		base.Load += new System.EventHandler(frmCurrency_Load);
		this.plMain.ResumeLayout(false);
		this.plMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmCurrency()
	{
		InitializeComponent();
		base.MinimizeBox = (base.MaximizeBox = false);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.StartPosition = FormStartPosition.CenterScreen;
		m_htab = Program.GetControlName(this, m_objName);
		m_enableImg = getImageByte(Program.m_AppPath + "\\image\\devEnable.gif");
		m_disableImg = getImageByte(Application.StartupPath + "\\image\\devDisable.gif");
		InitDgvList();
	}

	private void frmCurrency_Load(object sender, EventArgs e)
	{
	}

	public void InitDgvList()
	{
		string sql = "Select *,curr_Basflag curr_Img From D_Currency Order by curr_id";
		DataTable dataTable = null;
		try
		{
			dataTable = SQLserver.Data_GetDataTable(sql);
			dgvList.DataSource = createTable(dataTable).DefaultView;
			if (dgvList.DataSource != null)
			{
				dgvList.Columns[0].Visible = false;
				dgvList.Columns[1].HeaderText = (string)m_htab["dgvcol1"];
				dgvList.Columns[2].HeaderText = (string)m_htab["dgvcol2"];
				dgvList.Columns[3].HeaderText = (string)m_htab["dgvcol3"];
				dgvList.Columns[4].Visible = false;
				dgvList.Columns[5].HeaderText = (string)m_htab["dgvcol4"];
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

	private void btnNew_Click(object sender, EventArgs e)
	{
		try
		{
			txtid.Text = "";
			if (txtName.Text.Trim() == "" || txtRate.Text.Trim() == "" || txtSign.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			int num = 0;
			text = "Select * from D_Currency Where (curr_code=N'" + txtSign.Text.Trim().Replace("'", "''") + "')";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (chkBase.Checked)
			{
				text = "Update D_Currency Set curr_Basflag=0";
				if (SQLserver.Data_ExecuteSql(text) < 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				num = 1;
			}
			text = "Insert into D_Currency values(N'" + txtSign.Text.Trim().Replace("'", "''") + "',N'" + txtName.Text.Trim().Replace("'", "''") + "'," + Program.GetStandDec(txtRate.Text.Trim()) + "," + num + ")";
			if (SQLserver.Data_ExecuteSql(text) < 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			chkBase.Checked = false;
			TextBox textBox = txtName;
			TextBox textBox2 = txtRate;
			string text2 = (txtSign.Text = "");
			string text4 = (textBox2.Text = text2);
			textBox.Text = text4;
			InitDgvList();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnModify_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtid.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (txtName.Text.Trim() == "" || txtRate.Text.Trim() == "" || txtSign.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			int num = 0;
			if (chkBase.Checked)
			{
				text = "Update D_Currency Set curr_Basflag=0";
				if (SQLserver.Data_ExecuteSql(text) < 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				num = 1;
			}
			text = "Update D_Currency Set curr_code = N'" + txtSign.Text.Trim() + "', curr_name=N'" + txtName.Text.Trim() + "', curr_rate=" + Program.GetStandDec(txtRate.Text.Trim());
			text = text + ", curr_Basflag=" + num;
			text = text + " Where curr_id=" + txtid.Text.Trim();
			int num2 = SQLserver.Data_ExecuteSql(text);
			if (num2 <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num2, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			txtid.Text = "";
			chkBase.Checked = false;
			TextBox textBox = txtName;
			TextBox textBox2 = txtRate;
			string text2 = (txtSign.Text = "");
			string text4 = (textBox2.Text = text2);
			textBox.Text = text4;
			InitDgvList();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err03"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void chkBase_CheckedChanged(object sender, EventArgs e)
	{
		if (chkBase.Checked)
		{
			txtRate.Text = "1";
		}
		txtRate.Enabled = !chkBase.Checked;
	}

	private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvList.DataSource != null && e.RowIndex >= 0)
			{
				txtid.Text = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
				txtSign.Text = dgvList.Rows[e.RowIndex].Cells[1].Value.ToString();
				txtName.Text = dgvList.Rows[e.RowIndex].Cells[2].Value.ToString();
				txtRate.Text = dgvList.Rows[e.RowIndex].Cells[3].Value.ToString();
				chkBase.Checked = Convert.ToBoolean(dgvList.Rows[e.RowIndex].Cells[4].Value);
			}
		}
		catch
		{
		}
	}

	private void btnDis_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.DataSource == null || dgvList.SelectedRows.Count <= 0)
			{
				return;
			}
			if (dgvList.Rows.Count <= 1)
			{
				Program.MsgBox((string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DataGridViewRow dataGridViewRow = dgvList.SelectedRows[0];
			if (bool.Parse(dataGridViewRow.Cells["curr_Basflag"].Value.ToString()))
			{
				Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				string text = dataGridViewRow.Cells["curr_id"].Value.ToString();
				string sqlquery = "Delete From D_Currency Where curr_Basflag = 0 And curr_id = " + text;
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

	private void frmCurrency_FormClosed(object sender, FormClosedEventArgs e)
	{
		try
		{
			string sql = "Select top 1 * From D_Currency Where curr_Basflag  = 1";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				Program.m_baseCurrCode = dataTable.Rows[0]["curr_code"].ToString().Trim();
				Program.m_baseCurrID = Convert.ToInt32(dataTable.Rows[0]["curr_id"].ToString().Trim());
				Program.m_baseCurrRate = Convert.ToDouble(dataTable.Rows[0]["curr_rate"].ToString().Trim());
				dataTable.Clear();
			}
			else
			{
				Program.MsgBox((string)Program.m_hPubTab["SysInit01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Program.m_baseCurrRate = 1.0;
				Program.m_baseCurrCode = "";
				Program.m_baseCurrID = 1;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public DataTable createTable(DataTable DT)
	{
		DataTable dataTable = new DataTable();
		for (int i = 0; i < DT.Columns.Count - 1; i++)
		{
			DataColumn column = new DataColumn(DT.Columns[i].ColumnName, typeof(string));
			dataTable.Columns.Add(column);
		}
		DataColumn column2 = new DataColumn(DT.Columns[DT.Columns.Count - 1].ColumnName, Type.GetType("System.Byte[]"));
		dataTable.Columns.Add(column2);
		foreach (DataRow row in DT.Rows)
		{
			DataRow dataRow2 = dataTable.NewRow();
			foreach (DataColumn column3 in DT.Columns)
			{
				string text = row[column3].ToString();
				if (column3.ColumnName == DT.Columns[DT.Columns.Count - 1].ColumnName)
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
				else
				{
					dataRow2[column3.ColumnName] = text;
				}
			}
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
}
