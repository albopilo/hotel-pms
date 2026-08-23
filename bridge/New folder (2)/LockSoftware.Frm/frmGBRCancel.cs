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

public class frmGBRCancel : Form
{
	private IContainer components;

	private clsBackPanel clsBackPanel1;

	private ToolStrip toolStrip1;

	private ToolStripTextBox TSTxtBM;

	private ToolStripButton TSBtnSear;

	private ToolStripButton TSBtnDel;

	private ToolStripSeparator toolStripSeparator1;

	private DataGridView dgvList;

	private StatusStrip sstDR;

	private ToolStripStatusLabel TSSLab01;

	private ToolStripStatusLabel TSSLab02;

	private ToolStripButton TSBtnCA;

	private ToolStripStatusLabel TSSLab03;

	private ToolStripStatusLabel TSSLab04;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton TSBtnClose;

	private ToolStripButton TSBtnPDR;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripStatusLabel TSSLab05;

	private ToolStripStatusLabel TSSLab06;

	public string m_objName = "WFbrc";

	public Hashtable m_htab;

	private bool m_Init = true;

	private int type;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmGBRCancel));
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.sstDR = new System.Windows.Forms.StatusStrip();
		this.TSSLab01 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab03 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab04 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab05 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab06 = new System.Windows.Forms.ToolStripStatusLabel();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.TSTxtBM = new System.Windows.Forms.ToolStripTextBox();
		this.TSBtnSear = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.TSBtnPDR = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.TSBtnCA = new System.Windows.Forms.ToolStripButton();
		this.TSBtnDel = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.TSBtnClose = new System.Windows.Forms.ToolStripButton();
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.sstDR.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 42);
		this.dgvList.Name = "dgvList";
		this.dgvList.RowHeadersVisible = false;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(625, 330);
		this.dgvList.TabIndex = 9;
		this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellClick);
		this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellValueChanged);
		this.sstDR.AutoSize = false;
		this.sstDR.BackColor = System.Drawing.Color.Transparent;
		this.sstDR.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.sstDR.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.TSSLab01, this.TSSLab02, this.TSSLab03, this.TSSLab04, this.TSSLab05, this.TSSLab06 });
		this.sstDR.Location = new System.Drawing.Point(0, 372);
		this.sstDR.Name = "sstDR";
		this.sstDR.Size = new System.Drawing.Size(625, 30);
		this.sstDR.SizingGrip = false;
		this.sstDR.TabIndex = 16;
		this.sstDR.Text = "statusStrip2";
		this.TSSLab01.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab01.Name = "TSSLab01";
		this.TSSLab01.Size = new System.Drawing.Size(43, 25);
		this.TSSLab01.Text = "Total:";
		this.TSSLab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab02.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab02.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab02.Name = "TSSLab02";
		this.TSSLab02.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab02.Size = new System.Drawing.Size(146, 25);
		this.TSSLab02.Spring = true;
		this.TSSLab02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab03.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab03.Name = "TSSLab03";
		this.TSSLab03.Size = new System.Drawing.Size(62, 25);
		this.TSSLab03.Text = "Selected:";
		this.TSSLab04.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab04.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab04.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab04.Name = "TSSLab04";
		this.TSSLab04.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab04.Size = new System.Drawing.Size(146, 25);
		this.TSSLab04.Spring = true;
		this.TSSLab04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TSSLab05.BackColor = System.Drawing.Color.FromArgb(224, 85, 50);
		this.TSSLab05.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom | System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
		this.TSSLab05.ForeColor = System.Drawing.Color.White;
		this.TSSLab05.Name = "TSSLab05";
		this.TSSLab05.Size = new System.Drawing.Size(66, 25);
		this.TSSLab05.Text = "Past Due:";
		this.TSSLab06.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab06.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab06.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab06.Name = "TSSLab06";
		this.TSSLab06.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab06.Size = new System.Drawing.Size(146, 25);
		this.TSSLab06.Spring = true;
		this.TSSLab06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
		this.clsBackPanel1.Size = new System.Drawing.Size(625, 42);
		this.clsBackPanel1.TabIndex = 8;
		this.toolStrip1.AutoSize = false;
		this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolStrip1.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.TSTxtBM, this.TSBtnSear, this.toolStripSeparator1, this.TSBtnPDR, this.toolStripSeparator3, this.TSBtnCA, this.TSBtnDel, this.toolStripSeparator2, this.TSBtnClose });
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.toolStrip1.Size = new System.Drawing.Size(625, 42);
		this.toolStrip1.TabIndex = 9;
		this.TSTxtBM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.TSTxtBM.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSTxtBM.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
		this.TSTxtBM.Name = "TSTxtBM";
		this.TSTxtBM.Size = new System.Drawing.Size(120, 42);
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
		this.TSBtnPDR.Image = LockSoftware.Properties.Resources.history;
		this.TSBtnPDR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.TSBtnPDR.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSBtnPDR.Name = "TSBtnPDR";
		this.TSBtnPDR.Size = new System.Drawing.Size(139, 39);
		this.TSBtnPDR.Text = "Past Due Reservation";
		this.TSBtnPDR.Click += new System.EventHandler(TSBtnPDR_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
		this.TSBtnCA.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSBtnCA.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.TSBtnCA.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.TSBtnCA.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSBtnCA.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
		this.TSBtnCA.Name = "TSBtnCA";
		this.TSBtnCA.Size = new System.Drawing.Size(88, 39);
		this.TSBtnCA.Text = "Choose All";
		this.TSBtnCA.Click += new System.EventHandler(TSBtnCA_Click);
		this.TSBtnDel.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSBtnDel.Image = LockSoftware.Properties.Resources.delete;
		this.TSBtnDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.TSBtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSBtnDel.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
		this.TSBtnDel.Name = "TSBtnDel";
		this.TSBtnDel.Size = new System.Drawing.Size(63, 39);
		this.TSBtnDel.Text = "Delete";
		this.TSBtnDel.Click += new System.EventHandler(TSBtnDel_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
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
		base.ClientSize = new System.Drawing.Size(625, 402);
		base.Controls.Add(this.dgvList);
		base.Controls.Add(this.sstDR);
		base.Controls.Add(this.clsBackPanel1);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "frmGBRCancel";
		this.Text = "预订取消";
		base.Load += new System.EventHandler(frmGBRCancel_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.sstDR.ResumeLayout(false);
		this.sstDR.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmGBRCancel()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void frmGBRCancel_Load(object sender, EventArgs e)
	{
		if (m_htab != null)
		{
			TSTxtBM.Text = (string)m_htab["txtBM"];
			TSTxtBM.ForeColor = Color.DarkGray;
			TSTxtBM.ToolTipText = (string)m_htab["txtBM-ttMsg"];
			TSBtnDel.Text = (string)m_htab["TSBtnDel"];
			TSBtnSear.Text = (string)m_htab["TSBtnSear"];
			TSBtnCA.Text = (string)m_htab["TSBtnCA"];
			TSBtnClose.Text = (string)m_htab["TSBtnClose"];
			TSBtnPDR.Text = (string)m_htab["TSBtnPDR"];
		}
		TSBtnSear_Click(null, null);
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
		_ = e.KeyCode;
		_ = 13;
	}

	private void TSBtnSear_Click(object sender, EventArgs e)
	{
		try
		{
			type = 0;
			m_Init = true;
			TSBtnCA.Checked = false;
			ToolStripStatusLabel tSSLab = TSSLab02;
			ToolStripStatusLabel tSSLab2 = TSSLab04;
			string text = (TSSLab06.Text = "0");
			string text3 = (tSSLab2.Text = text);
			tSSLab.Text = text3;
			dgvList.DataSource = null;
			string text5 = "Select (Row_Number() OVER (Order by V.sch_id)) AS RowNumber, Cast(0 As bit) R_Cho, V.sch_id, case when T.g_teamid IS NULL then V.sch_name else V.g_name end sch_name, V.sch_mob, V.sch_tel, V.sch_email, case when T.g_teamid IS NULL then V.g_name else V.sch_name end g_name, V.r_id, V.R_Name, V.TP_Name, V.TP_Price, V.g_come_day, V.g_come_time,  V.g_level_day, V.Build_Name, V.Floor_Name from v_Reserve V,T_Schedule T Where V.sch_flag = 0 and V.sch_id = T.sch_id ";
			if (TSTxtBM.ForeColor == Color.Black && TSTxtBM.Text.Trim() != "")
			{
				string text6 = TSTxtBM.Text.Trim();
				string text7 = text5;
				text5 = text7 + " And (V.sch_name like N'" + text6 + "%' or V.sch_mob  like N'" + text6 + "%' or V.g_name  like N'" + text6 + "%' or V.TP_Name=N'" + text6 + "')";
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text5);
			if (dataTable != null)
			{
				dgvList.DataSource = dataTable.DefaultView;
				DataGridViewColumn dataGridViewColumn = dgvList.Columns["sch_id"];
				bool visible = (dgvList.Columns["r_id"].Visible = false);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvList.Columns.Count; i++)
				{
					dgvList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvList.Columns[i].Name];
					dgvList.Columns[i].ReadOnly = true;
				}
				dgvList.AutoResizeColumns();
				TSSLab02.Text = dgvList.Rows.Count.ToString();
				dgvList.Columns["R_Cho"].ReadOnly = false;
				int num = 0;
				for (int j = 0; j < dgvList.Rows.Count; j++)
				{
					DateTime dateTime = Convert.ToDateTime(dgvList.Rows[j].Cells["g_come_day"].Value.ToString() + " " + dgvList.Rows[j].Cells["g_come_time"].Value.ToString());
					if (dateTime < DateTime.Now)
					{
						dgvList.Rows[j].DefaultCellStyle.BackColor = Color.FromArgb(224, 85, 50);
						dgvList.Rows[j].DefaultCellStyle.ForeColor = Color.White;
						dgvList.Rows[j].Cells["R_Cho"].Value = true;
						num++;
					}
				}
				ToolStripStatusLabel tSSLab3 = TSSLab04;
				string text8 = (TSSLab06.Text = num.ToString());
				tSSLab3.Text = text8;
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
		m_Init = false;
	}

	private void TSBtnDel_Click(object sender, EventArgs e)
	{
		try
		{
			dgvList.EndEdit();
			int num = 0;
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			for (int num2 = dgvList.Rows.Count - 1; num2 >= 0; num2--)
			{
				if ((bool)dgvList.Rows[num2].Cells["R_Cho"].Value)
				{
					text2 = text2 + dgvList.Rows[num2].Cells["sch_id"].Value.ToString() + ",";
					text3 = text3 + dgvList.Rows[num2].Cells["r_id"].Value.ToString() + ",";
					num++;
				}
			}
			if (num <= 0)
			{
				return;
			}
			text4 = TSSLab03.Text + " " + num + "\r\n" + (string)m_htab["Info01"];
			if (Program.MsgBox(text4, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
			{
				return;
			}
			text = "Update D_Rooms Set R_RSID = 1 Where R_ID in(" + text3.Substring(0, text3.Length - 1) + ") \n";
			text = text + "Update T_Schedule Set sch_flag = 1, sch_memo=N'" + Text + "-";
			if (type == 1)
			{
				text += TSBtnPDR.Text;
			}
			string text5 = text;
			text = text5 + TSBtnDel.Text + "' Where sch_id in(" + text2.Substring(0, text2.Length - 1) + ") \n";
			if (Program.DBCompExec(text, Text) > 0)
			{
				if (type == 0)
				{
					TSBtnSear_Click(null, null);
				}
				else
				{
					TSBtnPDR_Click(null, null);
				}
				if (Program.fm != null)
				{
					Program.MDIFrm_Center_Room_Refresh(Program.fm.MdiChildren);
				}
			}
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

	private void TSBtnCA_Click(object sender, EventArgs e)
	{
		try
		{
			TSBtnCA.Checked = !TSBtnCA.Checked;
			if (dgvList.DataSource != null)
			{
				for (int i = 0; i < dgvList.Rows.Count; i++)
				{
					dgvList.Rows[i].Cells["R_Cho"].Value = TSBtnCA.Checked;
				}
				if (TSBtnCA.Checked)
				{
					TSSLab04.Text = dgvList.Rows.Count.ToString();
				}
				else
				{
					TSSLab04.Text = "0";
				}
			}
		}
		catch
		{
		}
	}

	private void TSSLab04Val()
	{
		int num = 0;
		TSSLab04.Text = "0";
		if (dgvList.DataSource == null)
		{
			return;
		}
		try
		{
			for (int i = 0; i < dgvList.Rows.Count; i++)
			{
				if ((bool)dgvList.Rows[i].Cells["R_Cho"].Value)
				{
					num++;
				}
			}
			TSSLab04.Text = num.ToString();
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
				dgvList.EndEdit();
				TSSLab04Val();
			}
		}
		catch
		{
		}
	}

	private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dgvList.Columns[e.ColumnIndex].Name == "R_Cho")
			{
				dgvList.Rows[e.RowIndex].Cells["R_Cho"].Value = !(bool)dgvList.Rows[e.RowIndex].Cells["R_Cho"].Value;
			}
		}
		catch
		{
		}
	}

	private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void TSBtnPDR_Click(object sender, EventArgs e)
	{
		try
		{
			type = 1;
			m_Init = true;
			TSBtnCA.Checked = false;
			ToolStripStatusLabel tSSLab = TSSLab02;
			ToolStripStatusLabel tSSLab2 = TSSLab04;
			string text = (TSSLab06.Text = "0");
			string text3 = (tSSLab2.Text = text);
			tSSLab.Text = text3;
			dgvList.DataSource = null;
			string text5 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
			string text6 = "Select (Row_Number() OVER (Order by sch_id)) AS RowNumber, Cast(0 As bit) R_Cho";
			text6 += ", sch_id, sch_name, sch_mob, sch_tel, sch_email, g_name";
			text6 += ", r_id, R_Name, TP_Name, TP_Price, g_come_day, g_come_time, g_level_day";
			text6 += ", Build_Name, Floor_Name from v_Reserve Where sch_flag = 0";
			text6 = text6 + " And (g_come_day + ' ' + g_come_time) < '" + text5 + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(text6);
			if (dataTable != null)
			{
				dgvList.DataSource = dataTable.DefaultView;
				DataGridViewColumn dataGridViewColumn = dgvList.Columns["sch_id"];
				bool visible = (dgvList.Columns["r_id"].Visible = false);
				dataGridViewColumn.Visible = visible;
				for (int i = 0; i < dgvList.Columns.Count; i++)
				{
					dgvList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvList.Columns[i].Name];
					dgvList.Columns[i].ReadOnly = true;
				}
				dgvList.AutoResizeColumns();
				TSSLab02.Text = dgvList.Rows.Count.ToString();
				dgvList.Columns["R_Cho"].ReadOnly = false;
				TSSLab06.Text = dgvList.Rows.Count.ToString();
			}
			TSBtnCA_Click(null, null);
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
		m_Init = false;
	}
}
