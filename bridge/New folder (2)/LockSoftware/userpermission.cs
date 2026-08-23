using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware;

public class userpermission : Form
{
	private IContainer components;

	private TreeView treeView1;

	private Label label1;

	private PictureBox pictureBox1;

	private ImageList imageList1;

	private SplitContainer splitContainer1;

	private clsBackPanel clsBackPanel1;

	private GlassBtn btnClose;

	private GlassBtn btnOK;

	private ToolsBtn btnChAll;

	private DataGridView dgvfunc;

	public string m_objName = "WFups";

	public Hashtable m_htab;

	public Hashtable funtab;

	private string sql;

	private DataSet myds = new DataSet();

	private string text;

	private string wl;

	private string funcid;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.userpermission));
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.dgvfunc = new System.Windows.Forms.DataGridView();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnChAll = new LockSoftware.Controls.ToolsBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.label1 = new System.Windows.Forms.Label();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvfunc).BeginInit();
		this.clsBackPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeView1.Location = new System.Drawing.Point(0, 0);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(234, 426);
		this.treeView1.TabIndex = 0;
		this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Use.gif");
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.treeView1);
		this.splitContainer1.Panel2.Controls.Add(this.dgvfunc);
		this.splitContainer1.Size = new System.Drawing.Size(754, 426);
		this.splitContainer1.SplitterDistance = 234;
		this.splitContainer1.TabIndex = 10;
		this.dgvfunc.AllowUserToAddRows = false;
		this.dgvfunc.AllowUserToDeleteRows = false;
		this.dgvfunc.BackgroundColor = System.Drawing.Color.White;
		this.dgvfunc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.dgvfunc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvfunc.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvfunc.Location = new System.Drawing.Point(0, 0);
		this.dgvfunc.Name = "dgvfunc";
		this.dgvfunc.RowHeadersWidth = 25;
		this.dgvfunc.RowTemplate.Height = 23;
		this.dgvfunc.Size = new System.Drawing.Size(516, 426);
		this.dgvfunc.TabIndex = 0;
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
		this.clsBackPanel1.Controls.Add(this.btnChAll);
		this.clsBackPanel1.Controls.Add(this.btnClose);
		this.clsBackPanel1.Controls.Add(this.btnOK);
		this.clsBackPanel1.Controls.Add(this.pictureBox1);
		this.clsBackPanel1.Controls.Add(this.label1);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel1.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 426);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(754, 86);
		this.clsBackPanel1.TabIndex = 11;
		this.btnChAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnChAll.BackColor = System.Drawing.Color.Transparent;
		this.btnChAll.Checked = false;
		this.btnChAll.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnChAll.DefaultColor = System.Drawing.Color.Transparent;
		this.btnChAll.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnChAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnChAll.ImageNew = LockSoftware.Properties.Resources.application_side_boxes;
		this.btnChAll.ImageRedrawed = true;
		this.btnChAll.ImageStyle = 0;
		this.btnChAll.isButton = true;
		this.btnChAll.Location = new System.Drawing.Point(428, 45);
		this.btnChAll.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnChAll.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnChAll.MouseDownStartColor = System.Drawing.Color.White;
		this.btnChAll.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.btnChAll.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.btnChAll.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnChAll.Name = "btnChAll";
		this.btnChAll.Size = new System.Drawing.Size(120, 25);
		this.btnChAll.TabIndex = 13;
		this.btnChAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnChAll.TextImageLocation = 3;
		this.btnChAll.TextNew = "Choose All";
		this.btnChAll.TextRedrawed = false;
		this.btnChAll.Click += new System.EventHandler(btnChAll_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(651, 41);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(86, 32);
		this.btnClose.TabIndex = 11;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(554, 41);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(86, 32);
		this.btnOK.TabIndex = 10;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.BackgroundImage = LockSoftware.Properties.Resources.v_key;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.pictureBox1.Location = new System.Drawing.Point(12, 19);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(46, 46);
		this.pictureBox1.TabIndex = 5;
		this.pictureBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(72, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(175, 15);
		this.label1.TabIndex = 4;
		this.label1.Text = "设置用户权限，防止越权操作。";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(754, 512);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.clsBackPanel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "userpermission";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "用户权限设置";
		base.Load += new System.EventHandler(userpermission_Load);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvfunc).EndInit();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public userpermission()
	{
		InitializeComponent();
	}

	private void datebind()
	{
		try
		{
			sql = "select u.user_name as name,u.user_password as password,u.groupid as groupid,u.user_no as user_no,ug.name as ugname from userinfo u,usergroup ug where u.groupid=ug.groupid and ug.name<>N'超级用户组' ";
			myds.Clear();
			try
			{
				myds = SQLserver.Data_GetDataSet(sql);
				treeView1.Nodes.Clear();
				treeView1.Nodes.Add((string)m_htab["tAllUsr"]);
				for (int i = 0; i <= myds.Tables[0].Rows.Count - 1; i++)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = myds.Tables[0].Rows[i]["name"].ToString();
					treeView1.ImageList = imageList1;
					treeView1.Nodes[0].Nodes.Add(treeNode);
				}
			}
			catch (Exception ex)
			{
				Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex2)
		{
			Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void datelist()
	{
		string text = null;
		try
		{
			text = "select distinct cast(0 As bit) As f_ch, FunctionID, Caption0 from func ";
			try
			{
				myds = SQLserver.Data_GetDataSet(text);
				if (funtab != null)
				{
					for (int i = 0; i < myds.Tables[0].Rows.Count; i++)
					{
						if (!((string)funtab["F" + myds.Tables[0].Rows[i]["FunctionID"].ToString().Trim()] == ""))
						{
							myds.Tables[0].Rows[i]["Caption0"] = (string)funtab["F" + myds.Tables[0].Rows[i]["FunctionID"].ToString().Trim()];
						}
					}
				}
				dgvfunc.DataSource = myds.Tables[0].DefaultView;
				DataGridViewColumn dataGridViewColumn = dgvfunc.Columns[0];
				string headerText = (dgvfunc.Columns[2].HeaderText = "");
				dataGridViewColumn.HeaderText = headerText;
				dgvfunc.Columns[1].Visible = false;
				dgvfunc.Columns[2].ReadOnly = true;
				dgvfunc.AutoResizeColumns();
			}
			catch (Exception ex)
			{
				Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex2)
		{
			Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void userpermission_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		funtab = Program.GetControlName(null, "FUNC");
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnClose.Text = (string)Program.m_hPubTab["btnCl"];
		datebind();
		datelist();
		treeView1.ExpandAll();
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		if (treeView1.SelectedNode == treeView1.Nodes[0])
		{
			return;
		}
		try
		{
			this.text = treeView1.SelectedNode.Text;
			_ = treeView1.SelectedNode.Index;
			sql = "select distinct user_no,functionid from userpermission where user_no = (select user_no from userinfo where user_name = N'" + this.text + "') ";
			DataSet dataSet = new DataSet();
			dataSet.Clear();
			try
			{
				dataSet = SQLserver.Data_GetDataSet(sql);
				bool flag = false;
				string text = "";
				for (int i = 0; i < dgvfunc.Rows.Count; i++)
				{
					flag = false;
					text = dgvfunc.Rows[i].Cells["FunctionID"].Value.ToString().Trim();
					if (dataSet.Tables[0].Select("FunctionID=" + text).Length > 0)
					{
						flag = true;
					}
					dgvfunc.Rows[i].Cells["f_ch"].Value = flag;
				}
			}
			catch (Exception ex)
			{
				Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			dgvfunc.EndEdit();
			treeView1.Focus();
		}
		catch (Exception ex2)
		{
			Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void SelectIndex()
	{
		funcid = null;
		for (int i = 0; i < dgvfunc.Rows.Count; i++)
		{
			if ((bool)dgvfunc.Rows[i].Cells["f_ch"].Value && funcid == null)
			{
				funcid = dgvfunc.Rows[i].Cells["FunctionID"].Value.ToString();
			}
			else if ((bool)dgvfunc.Rows[i].Cells["f_ch"].Value && funcid != null)
			{
				funcid = funcid + "," + dgvfunc.Rows[i].Cells["FunctionID"].Value.ToString();
			}
		}
	}

	private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
	{
		treeView1.Focus();
	}

	private void tabControl1_StyleChanged(object sender, EventArgs e)
	{
		treeView1.Focus();
	}

	private void listView1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (treeView1.SelectedNode != treeView1.Nodes[0])
		{
			dgvfunc.EndEdit();
			SelectIndex();
			string text = treeView1.SelectedNode.Text;
			if (text == SQLserver.UserName)
			{
				Program.MsgCustom((string)m_htab["Info03"], MessageBoxIcon.Asterisk);
				return;
			}
			sql = "select user_no from userinfo where user_name = N'" + text + "' ";
			DataSet dataSet = SQLserver.Data_GetDataSet(sql);
			wl = null;
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				if (wl == null)
				{
					wl = "'" + dataSet.Tables[0].Rows[i]["user_no"].ToString() + "'";
				}
				else
				{
					wl = wl + ",'" + dataSet.Tables[0].Rows[i]["user_no"].ToString() + "'";
				}
			}
			sql = "delete from userpermission where user_no =  " + wl + " ";
			SQLserver.Data_ExecuteSql(sql);
			if (funcid != null && wl != null)
			{
				sql = "insert into userpermission(user_no,functionid,show) select " + wl + ",functionid,1  from Func where FunctionID in ( " + funcid + ")";
				SQLserver.Data_ExecuteSql(sql);
			}
			Program.MsgCustom((string)m_htab["Info02"], MessageBoxIcon.Asterisk);
		}
		else
		{
			Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Asterisk);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnChAll_Click(object sender, EventArgs e)
	{
		try
		{
			btnChAll.Checked = !btnChAll.Checked;
			for (int i = 0; i < dgvfunc.Rows.Count; i++)
			{
				dgvfunc.Rows[i].Cells["f_ch"].Value = btnChAll.Checked;
			}
			dgvfunc.EndEdit();
		}
		catch
		{
		}
	}
}
