using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Frm;
using LockSoftware.Properties;

namespace LockSoftware;

public class updategroup : Form
{
	public string m_objName = "WFug";

	public Hashtable m_htab;

	private DataSet myds = new DataSet();

	private string sql;

	private string groupname;

	private string text1;

	public string uuname;

	private IContainer components;

	private ToolStrip toolStrip1;

	private ToolStripButton toolStripButton1;

	private ToolStripButton toolStripButton2;

	private ToolStripButton toolStripButton3;

	private ToolStripButton toolStripButton4;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripButton toolStripButton5;

	private TreeView treeView1;

	private ImageList imageList1;

	private clsBackPanel clsBackPanel1;

	private ToolStripSeparator toolStripSeparator2;

	public updategroup()
	{
		InitializeComponent();
	}

	private void toolStripSeparator1_Click(object sender, EventArgs e)
	{
	}

	private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	private void datebind()
	{
		try
		{
			sql = "select distinct name from usergroup where name<>N'超级用户组' ";
			try
			{
				myds = SQLserver.Data_GetDataSet(sql);
				_ = myds.Tables[0];
				treeView1.Nodes.Clear();
				treeView1.Nodes.Add((string)m_htab["tAllGrp"]);
				for (int i = 0; i <= myds.Tables[0].Rows.Count - 1; i++)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = myds.Tables[0].Rows[i]["name"].ToString();
					treeView1.ImageList = imageList1;
					treeView1.Nodes[0].Nodes.Add(treeNode);
				}
				for (int j = 0; j < treeView1.Nodes[0].Nodes.Count; j++)
				{
					string text = treeView1.Nodes[0].Nodes[j].Text;
					sql = "select user_name from  userinfo where groupid = (select groupid from usergroup where name = N'" + text + "')";
					myds.Clear();
					treeView1.Nodes[0].Nodes[j].Nodes.Clear();
					myds = SQLserver.Data_GetDataSet(sql);
					for (int k = 0; k <= myds.Tables[0].Rows.Count - 1; k++)
					{
						TreeNode treeNode2 = new TreeNode();
						treeNode2.Text = myds.Tables[0].Rows[k]["user_name"].ToString();
						treeView1.ImageList = imageList1;
						treeView1.Nodes[0].Nodes[j].Nodes.Add(treeNode2);
					}
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

	private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void toolStripButton2_Click(object sender, EventArgs e)
	{
		create create2 = new create();
		create2.ShowDialog();
		if (create2.DialogResult == DialogResult.OK)
		{
			myds.Clear();
			datebind();
			treeView1.ExpandAll();
		}
	}

	private void toolStripButton3_Click(object sender, EventArgs e)
	{
		if (treeView1.SelectedNode == null)
		{
			return;
		}
		try
		{
			_ = treeView1.SelectedNode.Index;
			string text = treeView1.SelectedNode.Text;
			if (treeView1.SelectedNode != treeView1.Nodes[0] && treeView1.SelectedNode.Nodes.Count == 0)
			{
				if (!(text != SQLserver.UserName))
				{
					Program.MsgCustom((string)m_htab["Info06"], MessageBoxIcon.Asterisk);
					return;
				}
				try
				{
					groupname = text;
					edituser2 edituser3 = new edituser2(groupname);
					edituser3.ShowDialog();
					if (edituser3.DialogResult == DialogResult.OK)
					{
						text1 = edituser3.groupname1;
						try
						{
							sql = "update userinfo set groupid = ( select groupid from usergroup where name = N'" + text1 + "') where user_name = N'" + text + "' ";
							SQLserver.Data_ExecuteSql(sql);
							sql = "delete from  userpermission  where user_no in (select user_no from userinfo where user_name =N'" + text + "')";
							SQLserver.Data_ExecuteSql(sql);
							sql = "insert into userpermission(user_no,functionid,show) select user_no ,functionid,1  from grouppermission,userinfo where  grouppermission.groupid=(select groupid from usergroup where name = N'" + text1 + "') and user_no in (select user_no from userinfo where user_name = N'" + text + "')";
							SQLserver.Data_ExecuteSql(sql);
							treeView1.SelectedNode.Remove();
						}
						catch (Exception ex)
						{
							Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					treeView1.Refresh();
				}
				catch (Exception ex2)
				{
					Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else if (treeView1.SelectedNode != treeView1.Nodes[0])
			{
				groupname = text;
				if (text == SQLserver.UserGroup)
				{
					Program.MsgCustom(text + (string)m_htab["Info01"], MessageBoxIcon.Asterisk);
					return;
				}
				editusergroup editusergroup2 = new editusergroup(groupname);
				editusergroup2.ShowDialog();
				if (editusergroup2.DialogResult == DialogResult.OK)
				{
					text1 = editusergroup2.groupname1;
					try
					{
						sql = "update usergroup set name = N'" + text1 + "'  where name = N'" + text + "'";
						SQLserver.Data_ExecuteSql(sql);
						treeView1.SelectedNode.Text = editusergroup2.groupname1;
					}
					catch (Exception ex3)
					{
						Program.MsgBox(ex3.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
			datebind();
			treeView1.ExpandAll();
		}
		catch (Exception ex4)
		{
			Program.MsgBox(ex4.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void updategroup_Load_1(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		datebind();
		treeView1.ExpandAll();
		for (int i = 1; i < 6; i++)
		{
			toolStrip1.Items["toolStripButton" + i].Text = (string)m_htab["toolStripButton" + i];
		}
	}

	private void toolStripButton5_Click(object sender, EventArgs e)
	{
		treeView1.ExpandAll();
	}

	private void toolStripButton4_Click(object sender, EventArgs e)
	{
		try
		{
			if (treeView1.SelectedNode == null)
			{
				return;
			}
			string text = treeView1.SelectedNode.Text;
			_ = treeView1.SelectedNode.Index;
			if (treeView1.SelectedNode != treeView1.Nodes[0] && treeView1.SelectedNode.Nodes.Count == 0)
			{
				if (text == SQLserver.UserName)
				{
					Program.MsgCustom(text + (string)m_htab["Info02"], MessageBoxIcon.Asterisk);
					return;
				}
				DialogResult dialogResult = Program.MsgBox(string.Format((string)m_htab["Info03"], text), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					try
					{
						sql = "delete from userinfo where user_name = N'" + text + "'";
						SQLserver.Data_ExecuteSql(sql);
						sql = "delete from userpermission where user_no = (select user_no from userinfo where user_name =  N'" + text + "')";
						SQLserver.Data_ExecuteSql(sql);
						sql = "delete from userGroup where Name =  N'" + text + "'";
						SQLserver.Data_ExecuteSql(sql);
						treeView1.SelectedNode.Remove();
						return;
					}
					catch (Exception ex)
					{
						Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
			}
			else
			{
				if (treeView1.SelectedNode == treeView1.Nodes[0])
				{
					return;
				}
				if (text == SQLserver.UserGroup)
				{
					Program.MsgCustom(text + (string)m_htab["Info04"], MessageBoxIcon.Asterisk);
					return;
				}
				DialogResult dialogResult2 = Program.MsgBox(string.Format((string)m_htab["Info05"], text), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult2 != DialogResult.Yes)
				{
					return;
				}
				try
				{
					sql = "Select user_name from userinfo where groupid = (select groupid from usergroup where name = N'" + text + "') ";
					DataSet dataSet = SQLserver.Data_GetDataSet(sql);
					DataTable dataTable = dataSet.Tables[0];
					if (dataTable.Rows.Count != 0)
					{
						Program.MsgCustom(text + (string)m_htab["Info07"], MessageBoxIcon.Asterisk);
						return;
					}
				}
				catch (Exception ex2)
				{
					Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				try
				{
					sql = "delete from userpermission where user_no in (select user_no from userinfo where groupid=(select groupid from usergroup where name = N'" + text + "'))";
					SQLserver.Data_ExecuteSql(sql);
					sql = "delete from grouppermission where groupid=(select groupid from usergroup where name = N'" + text + "')";
					SQLserver.Data_ExecuteSql(sql);
					sql = "delete from usergroup where name = N'" + text + "'";
					SQLserver.Data_ExecuteSql(sql);
					treeView1.SelectedNode.Remove();
					return;
				}
				catch (Exception ex3)
				{
					Program.MsgBox(ex3.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
		}
		catch (Exception ex4)
		{
			Program.MsgBox(ex4.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		int index = treeView1.SelectedNode.Index;
		try
		{
			if (treeView1.SelectedNode != treeView1.Nodes[0] && treeView1.SelectedNode.Nodes.Count != 0)
			{
				string text = treeView1.SelectedNode.Text;
				sql = "select user_name from  userinfo where groupid = (select groupid from usergroup where name = N'" + text + "')";
				myds.Clear();
				treeView1.Nodes[0].Nodes[index].Nodes.Clear();
				myds = SQLserver.Data_GetDataSet(sql);
				for (int i = 0; i <= myds.Tables[0].Rows.Count - 1; i++)
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = myds.Tables[0].Rows[i]["user_name"].ToString();
					treeView1.ImageList = imageList1;
					treeView1.Nodes[0].Nodes[index].Nodes.Add(treeNode);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.updategroup));
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.clsBackPanel1.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeView1.Location = new System.Drawing.Point(0, 45);
		this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(545, 340);
		this.treeView1.TabIndex = 3;
		this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "UserGroup.gif");
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
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(545, 45);
		this.clsBackPanel1.TabIndex = 4;
		this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.toolStripButton2, this.toolStripButton3, this.toolStripButton4, this.toolStripSeparator1, this.toolStripButton5, this.toolStripSeparator2, this.toolStripButton1 });
		this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.toolStrip1.Size = new System.Drawing.Size(545, 45);
		this.toolStrip1.TabIndex = 2;
		this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
		this.toolStripButton2.Image = LockSoftware.Properties.Resources.Add;
		this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(36, 42);
		this.toolStripButton2.Text = "New";
		this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
		this.toolStripButton3.Image = LockSoftware.Properties.Resources.group_edit;
		this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(32, 42);
		this.toolStripButton3.Text = "Edit";
		this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
		this.toolStripButton4.Image = LockSoftware.Properties.Resources.delete;
		this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(47, 42);
		this.toolStripButton4.Text = "Delete";
		this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
		this.toolStripButton5.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.toolStripButton5.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.Size = new System.Drawing.Size(35, 42);
		this.toolStripButton5.Text = "全部";
		this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
		this.toolStripButton1.Image = LockSoftware.Properties.Resources.close;
		this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(35, 42);
		this.toolStripButton1.Text = "关闭";
		this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(545, 385);
		base.Controls.Add(this.treeView1);
		base.Controls.Add(this.clsBackPanel1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "updategroup";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "用户组设置";
		base.Load += new System.EventHandler(updategroup_Load_1);
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
	}
}
