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

public class frmRoomStatus : Form
{
	private IContainer components;

	private ToolsBtn toolsBtn1;

	private NGlassBtn btnNew;

	private NGlassBtn btnDis;

	private NGlassBtn btnClose;

	private clsBackPanel plMain;

	private Label label1;

	private CheckBox chkCU;

	private TextBox txtName;

	private DataGridView dgvList;

	private TextBox txtNameen;

	private Label label2;

	public string m_objName = "WFrs";

	public Hashtable m_htab;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmRoomStatus));
		this.plMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.txtNameen = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.chkCU = new System.Windows.Forms.CheckBox();
		this.txtName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDis = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnNew = new LockSoftware.Controls.NGlassBtn(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.plMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		base.SuspendLayout();
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
		this.plMain.Controls.Add(this.txtNameen);
		this.plMain.Controls.Add(this.label2);
		this.plMain.Controls.Add(this.dgvList);
		this.plMain.Controls.Add(this.chkCU);
		this.plMain.Controls.Add(this.txtName);
		this.plMain.Controls.Add(this.label1);
		this.plMain.Location = new System.Drawing.Point(3, 81);
		this.plMain.Name = "plMain";
		this.plMain.Size = new System.Drawing.Size(455, 320);
		this.plMain.TabIndex = 1;
		this.txtNameen.Location = new System.Drawing.Point(229, 11);
		this.txtNameen.Name = "txtNameen";
		this.txtNameen.Size = new System.Drawing.Size(110, 21);
		this.txtNameen.TabIndex = 2;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(151, 15);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(76, 23);
		this.label2.TabIndex = 5;
		this.label2.Text = "Name-En:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Location = new System.Drawing.Point(11, 38);
		this.dgvList.Name = "dgvList";
		this.dgvList.ReadOnly = true;
		this.dgvList.RowHeadersWidth = 25;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(433, 273);
		this.dgvList.TabIndex = 4;
		this.chkCU.AutoSize = true;
		this.chkCU.BackColor = System.Drawing.Color.Transparent;
		this.chkCU.Location = new System.Drawing.Point(345, 14);
		this.chkCU.Name = "chkCU";
		this.chkCU.Size = new System.Drawing.Size(102, 16);
		this.chkCU.TabIndex = 3;
		this.chkCU.Text = "Can for guest";
		this.chkCU.UseVisualStyleBackColor = false;
		this.txtName.Location = new System.Drawing.Point(72, 11);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(73, 21);
		this.txtName.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(9, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "Name-Cn:";
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnClose.ButtonText = "Close";
		this.btnClose.CornerRadius = 4;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Location = new System.Drawing.Point(367, 38);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(83, 35);
		this.btnClose.TabIndex = 6;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnDis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDis.BackColor = System.Drawing.Color.Transparent;
		this.btnDis.BaseColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDis.ButtonColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.btnDis.ButtonText = "Disabled";
		this.btnDis.CornerRadius = 4;
		this.btnDis.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDis.GlowColor = System.Drawing.Color.White;
		this.btnDis.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDis.Location = new System.Drawing.Point(273, 38);
		this.btnDis.Name = "btnDis";
		this.btnDis.Size = new System.Drawing.Size(88, 35);
		this.btnDis.TabIndex = 5;
		this.btnDis.Click += new System.EventHandler(btnDis_Click);
		this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnNew.BackColor = System.Drawing.Color.Transparent;
		this.btnNew.BaseColor = System.Drawing.Color.FromArgb(0, 192, 192);
		this.btnNew.ButtonColor = System.Drawing.Color.Teal;
		this.btnNew.ButtonText = "New Status";
		this.btnNew.CornerRadius = 4;
		this.btnNew.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNew.GlowColor = System.Drawing.Color.White;
		this.btnNew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNew.Image = LockSoftware.Properties.Resources.Add;
		this.btnNew.Location = new System.Drawing.Point(147, 38);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(120, 35);
		this.btnNew.TabIndex = 4;
		this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources._160;
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
		this.toolsBtn1.Size = new System.Drawing.Size(461, 78);
		this.toolsBtn1.TabIndex = 3;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Room Status: Setting room's status.";
		this.toolsBtn1.TextRedrawed = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(461, 404);
		base.Controls.Add(this.plMain);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnDis);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.toolsBtn1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmRoomStatus";
		this.Text = "frmRoomStatus";
		this.plMain.ResumeLayout(false);
		this.plMain.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		base.ResumeLayout(false);
	}

	public frmRoomStatus()
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
		string sql = "Select  * From D_RoomStatus Order by  RS_ID";
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
			if (txtName.Text.Trim() == "" || txtNameen.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			text = "Insert into D_RoomStatus values(N'" + txtNameen.Text.Trim().Replace("'", "''") + "',N'" + txtName.Text.Trim().Replace("'", "''") + "'," + Convert.ToInt16(chkCU.Checked) + ",0,GetDate()," + Program.m_opid.ToString() + ",N'" + Program.m_OperName + "',NULL,NULL,NULL)";
			if (SQLserver.Data_ExecuteSql(text) < 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			TextBox textBox = txtName;
			string text2 = (txtNameen.Text = "");
			textBox.Text = text2;
			chkCU.Checked = false;
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
			if (dgvList.SelectedRows.Count > 0 && Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				string text = "Update D_RoomStatus Set RS_flag = 0, updatetime=GetDate(), updatorid=" + Program.m_opid + ", updator=N'" + Program.m_OperName + "'";
				text += " Where 1 = 1 And (";
				for (int i = 0; i < dgvList.SelectedRows.Count; i++)
				{
					text = text + " TP_ID=" + dgvList.SelectedRows[i].Cells["TP_ID"].Value.ToString() + " or";
				}
				text = text.Substring(0, text.Length - 3) + ")";
				int num = SQLserver.Data_ExecuteSql(text);
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
		Close();
	}
}
