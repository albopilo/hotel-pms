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

public class frmCardRewrite : Form
{
	public string m_objName = "WFcr";

	public Hashtable m_htab;

	public long m_tmpID = -1L;

	public int m_rtype;

	public string btnTxt = "";

	private bool cursel;

	private bool m_Init = true;

	private IContainer components;

	private GlassBtn btnRef;

	private DataGridView dgvList;

	private Panel panel1;

	private GlassBtn btnClose;

	private GlassBtn btnCard;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel TSSLab01;

	private ToolStripDropDownButton TSSBtnChoAll;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private ToolStripStatusLabel TSSLab02;

	public frmCardRewrite()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void InitRoomList()
	{
		try
		{
			m_Init = true;
			dgvList.DataSource = null;
			string text = "Select (Row_Number() OVER (Order by R_Name, g_WCard, g_cometime )) AS RowNumber ";
			text += ", Cast(g_WCard As bit) R_Cho, g_id, g_name, cer_name, g_cernum ,R_Name, g_WCard, g_cometime";
			object obj = text;
			text = string.Concat(obj, ",(Cast(Cast(g_SOTotalDay As Integer) As varchar)+ N'", Program.m_hPubTab["InfoDay"], "'+ Cast(Cast(g_stayHour As Integer) As varchar) + N'", Program.m_hPubTab["InfoHour"], "') as g_stayhour");
			text += ", g_stand_l_time, TR_ID, g_teamid, r_id, b_code, f_code, r_code, r_subcode, r_subDai, r_cardnum";
			text += " From v_CardGuest Where g_level=0 ";
			text = ((m_rtype != 0) ? (text + " And g_teamid = " + m_tmpID) : (text + " And TR_ID = " + m_tmpID));
			text += " order by r_id, r_cardnum ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				dgvList.DataSource = dataTable.DefaultView;
				if (dgvList.DataSource != null)
				{
					DataGridViewColumn dataGridViewColumn = dgvList.Columns["g_id"];
					DataGridViewColumn dataGridViewColumn2 = dgvList.Columns["r_cardnum"];
					DataGridViewColumn dataGridViewColumn3 = dgvList.Columns["TR_ID"];
					DataGridViewColumn dataGridViewColumn4 = dgvList.Columns["g_teamid"];
					bool flag = (dgvList.Columns["r_id"].Visible = false);
					bool flag3 = (dataGridViewColumn4.Visible = flag);
					bool flag5 = (dataGridViewColumn3.Visible = flag3);
					bool visible = (dataGridViewColumn2.Visible = flag5);
					dataGridViewColumn.Visible = visible;
					DataGridViewColumn dataGridViewColumn5 = dgvList.Columns["b_code"];
					DataGridViewColumn dataGridViewColumn6 = dgvList.Columns["f_code"];
					DataGridViewColumn dataGridViewColumn7 = dgvList.Columns["r_code"];
					DataGridViewColumn dataGridViewColumn8 = dgvList.Columns["r_subcode"];
					bool flag8 = (dgvList.Columns["r_subDai"].Visible = false);
					bool flag10 = (dataGridViewColumn8.Visible = flag8);
					bool flag12 = (dataGridViewColumn7.Visible = flag10);
					bool visible2 = (dataGridViewColumn6.Visible = flag12);
					dataGridViewColumn5.Visible = visible2;
					for (int i = 0; i < dgvList.Columns.Count; i++)
					{
						dgvList.Columns[i].ReadOnly = true;
						dgvList.Columns[i].HeaderText = (string)m_htab["dgv" + dgvList.Columns[i].Name];
					}
					dgvList.AutoResizeColumns();
				}
			}
			TSSLab02Val();
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, btnRef.Text);
		}
		m_Init = false;
	}

	private void frmCardRewrite_Load(object sender, EventArgs e)
	{
		btnRef.Text = btnTxt;
		InitRoomList();
		TSSBtnChoAll_Click(new object(), new EventArgs());
	}

	private void btnRef_Click(object sender, EventArgs e)
	{
		cursel = false;
		InitRoomList();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnCard_Click(object sender, EventArgs e)
	{
		try
		{
			dgvList.EndEdit();
			string text = "";
			string text2 = "";
			string text3 = "";
			int num = 0;
			string text4 = "";
			int num2 = 0;
			int num3 = 0;
			bool flag = false;
			int num4 = 0;
			for (int i = 0; i < dgvList.Rows.Count; i++)
			{
				if ((bool)dgvList.Rows[i].Cells["R_Cho"].Value)
				{
					num++;
				}
			}
			if (num <= 0)
			{
				return;
			}
			text3 = string.Format((string)m_htab["Info01"], num + "\r\n");
			if (Program.MsgBox(text3, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
			{
				return;
			}
			for (int j = 0; j < dgvList.Rows.Count; j++)
			{
				if (!(bool)dgvList.Rows[j].Cells["R_Cho"].Value)
				{
					continue;
				}
				text = string.Format((string)m_htab["Info02"], dgvList.Rows[j].Cells["R_Name"].Value.ToString() + "\r\n", dgvList.Rows[j].Cells["g_name"].Value.ToString() + "\r\n", "\r\n", "\r\n");
				switch (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1))
				{
				case DialogResult.Cancel:
					return;
				case DialogResult.No:
					continue;
				}
				flag = num3 != int.Parse(dgvList.Rows[j].Cells["r_id"].Value.ToString());
				if (flag)
				{
					num3 = int.Parse(dgvList.Rows[j].Cells["r_id"].Value.ToString());
					num4 = Convert.ToInt32(dgvList.Rows[j].Cells["r_subDai"].Value);
					num4++;
				}
				if (num4 > 255)
				{
					num4 = 1;
				}
				text = Convert.ToInt32(dgvList.Rows[j].Cells["b_code"].Value).ToString("X2") + Convert.ToInt32(dgvList.Rows[j].Cells["f_code"].Value).ToString("X2");
				text = text + Convert.ToInt32(dgvList.Rows[j].Cells["r_code"].Value).ToString("X2") + Convert.ToInt32(dgvList.Rows[j].Cells["r_subcode"].Value).ToString("X2");
				text += ((byte)num4).ToString("X2");
				num2 = Convert.ToInt32(dgvList.Rows[j].Cells["r_cardnum"].Value);
				if (num2 < 0)
				{
					return;
				}
				text2 = Convert.ToDateTime(dgvList.Rows[j].Cells["g_stand_l_time"].Value).ToString("yyyyMMddHHmm");
				if (Program.RadioWriteCard(6, num2, text2, text, text.Length, Buzzer: false) != 0)
				{
					return;
				}
				text4 = "Update T_Guest Set g_rewrite = 1, g_rwdate = GetDate(), r_subDai = " + num4 + ",Updator = N'" + Program.m_OperName + "', Updator_id =" + Program.m_opid + ", UpdateTime = GetDate() Where g_id =" + dgvList.Rows[j].Cells["g_id"].Value.ToString() + "\n";
				if (flag)
				{
					object obj = text4;
					text4 = string.Concat(obj, "Update D_Rooms Set R_SubCodeDai = ", num4, " Where R_ID = ", num3);
				}
				if (SQLserver.Data_ExecuteSql(text4) <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				dgvList.Rows[j].Cells["R_Cho"].Value = false;
				Program.RadioDevBuzzer(1, 2);
			}
			Program.MsgCustom((string)m_htab["Info03"], MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void TSSBtnChoAll_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvList.DataSource != null)
			{
				cursel = !cursel;
				for (int i = 0; i < dgvList.Rows.Count; i++)
				{
					dgvList.Rows[i].Cells["R_Cho"].Value = cursel;
				}
				if (cursel)
				{
					TSSLab02.Text = dgvList.Rows.Count.ToString();
				}
				else
				{
					TSSLab02.Text = "0";
				}
			}
		}
		catch
		{
		}
	}

	private void TSSLab02Val()
	{
		int num = 0;
		TSSLab02.Text = "";
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
			TSSLab02.Text = num.ToString();
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
			}
		}
		catch
		{
		}
	}

	private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		TSSLab02Val();
	}

	private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex == 1 && e.RowIndex >= 0)
		{
			bool flag = (bool)dgvList.Rows[e.RowIndex].Cells[1].Value;
			string text = dgvList.Rows[e.RowIndex].Cells[6].Value.ToString();
			for (int i = 0; i < dgvList.Rows.Count; i++)
			{
				if (dgvList.Rows[i].Cells[6].Value.ToString() == text)
				{
					dgvList.Rows[i].Cells[1].Value = !flag;
				}
			}
		}
		TSSLab02Val();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmCardRewrite));
		this.dgvList = new System.Windows.Forms.DataGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.TSSBtnChoAll = new System.Windows.Forms.ToolStripDropDownButton();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab01 = new System.Windows.Forms.ToolStripStatusLabel();
		this.TSSLab02 = new System.Windows.Forms.ToolStripStatusLabel();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnCard = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnRef = new LockSoftware.Controls.GlassBtn(this.components);
		((System.ComponentModel.ISupportInitialize)this.dgvList).BeginInit();
		this.panel1.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		base.SuspendLayout();
		this.dgvList.AllowUserToAddRows = false;
		this.dgvList.AllowUserToDeleteRows = false;
		this.dgvList.BackgroundColor = System.Drawing.Color.White;
		this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvList.Location = new System.Drawing.Point(0, 38);
		this.dgvList.Name = "dgvList";
		this.dgvList.RowHeadersVisible = false;
		this.dgvList.RowTemplate.Height = 23;
		this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvList.Size = new System.Drawing.Size(661, 257);
		this.dgvList.TabIndex = 6;
		this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellClick);
		this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellEndEdit);
		this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvList_CellValueChanged);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.btnClose);
		this.panel1.Controls.Add(this.btnCard);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 323);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(661, 42);
		this.panel1.TabIndex = 7;
		this.statusStrip1.AutoSize = false;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.TSSBtnChoAll, this.toolStripStatusLabel1, this.TSSLab01, this.TSSLab02 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 295);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(661, 28);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 8;
		this.statusStrip1.Text = "statusStrip1";
		this.TSSBtnChoAll.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.TSSBtnChoAll.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.TSSBtnChoAll.Name = "TSSBtnChoAll";
		this.TSSBtnChoAll.ShowDropDownArrow = false;
		this.TSSBtnChoAll.Size = new System.Drawing.Size(85, 26);
		this.TSSBtnChoAll.Text = "Choose All";
		this.TSSBtnChoAll.Click += new System.EventHandler(TSSBtnChoAll_Click);
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(427, 23);
		this.toolStripStatusLabel1.Spring = true;
		this.TSSLab01.Name = "TSSLab01";
		this.TSSLab01.Size = new System.Drawing.Size(53, 23);
		this.TSSLab01.Text = "已选择：";
		this.TSSLab02.AutoSize = false;
		this.TSSLab02.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.TSSLab02.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
		this.TSSLab02.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TSSLab02.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.TSSLab02.Name = "TSSLab02";
		this.TSSLab02.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TSSLab02.Size = new System.Drawing.Size(50, 23);
		this.TSSLab02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(573, 6);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(75, 30);
		this.btnClose.TabIndex = 12;
		this.btnClose.Text = "关 闭";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCard.BackColor = System.Drawing.Color.LightGray;
		this.btnCard.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCard.ForeColor = System.Drawing.Color.Black;
		this.btnCard.GlowColor = System.Drawing.Color.White;
		this.btnCard.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCard.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCard.Location = new System.Drawing.Point(492, 6);
		this.btnCard.Name = "btnCard";
		this.btnCard.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCard.Size = new System.Drawing.Size(75, 30);
		this.btnCard.TabIndex = 11;
		this.btnCard.Text = "写 卡";
		this.btnCard.Click += new System.EventHandler(btnCard_Click);
		this.btnRef.AutoSize = true;
		this.btnRef.BackColor = System.Drawing.Color.SteelBlue;
		this.btnRef.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnRef.Font = new System.Drawing.Font("Times New Roman", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRef.ForeColor = System.Drawing.Color.DimGray;
		this.btnRef.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRef.Image = LockSoftware.Properties.Resources.EmployeeQuery;
		this.btnRef.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRef.InnerBorderColor = System.Drawing.Color.WhiteSmoke;
		this.btnRef.Location = new System.Drawing.Point(0, 0);
		this.btnRef.Name = "btnRef";
		this.btnRef.OuterBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRef.Size = new System.Drawing.Size(661, 38);
		this.btnRef.TabIndex = 5;
		this.btnRef.Text = "Room Name";
		this.btnRef.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRef.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnRef.Click += new System.EventHandler(btnRef_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(661, 365);
		base.Controls.Add(this.dgvList);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.btnRef);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.Name = "frmCardRewrite";
		this.Text = "卡片重写";
		base.Load += new System.EventHandler(frmCardRewrite_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvList).EndInit();
		this.panel1.ResumeLayout(false);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
