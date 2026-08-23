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

public class updateuser : Form
{
	public string m_objName = "WFus";

	public Hashtable m_htab;

	private string sql;

	private string uuuser_no;

	private string uupassword;

	private string uuname;

	private string text;

	private DataSet myds = new DataSet();

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

	public updateuser()
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
			sql = "select u.user_name as name,u.user_password as password,u.groupid as groupid,u.user_no as user_no,ug.name as ugname from userinfo u,usergroup ug where u.groupid=ug.groupid and ug.name<>N'超级用户组'";
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

	private void updateuser_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		datebind();
		treeView1.ExpandAll();
		for (int i = 1; i < 6; i++)
		{
			toolStrip1.Items["toolStripButton" + i].Text = (string)m_htab["toolStripButton" + i];
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
		createuser createuser2 = new createuser();
		createuser2.ShowDialog();
		if (createuser2.DialogResult == DialogResult.OK)
		{
			myds.Clear();
			datebind();
			treeView1.ExpandAll();
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
			if (treeView1.SelectedNode == null || treeView1.SelectedNode == treeView1.Nodes[0])
			{
				return;
			}
			string text = treeView1.SelectedNode.Text;
			if (text == SQLserver.UserName)
			{
				Program.MsgCustom(text + (string)m_htab["Info01"], MessageBoxIcon.Asterisk);
				return;
			}
			DialogResult dialogResult = Program.MsgBox(string.Format((string)m_htab["Info02"], text), (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult != DialogResult.Yes)
			{
				return;
			}
			try
			{
				sql = "delete from userinfo where user_name = N'" + text + "'";
				SQLserver.Data_ExecuteSql(sql);
				sql = "delete from userpermission where user_no = (select user_no from userinfo where user_name =  N'" + text + "')";
				SQLserver.Data_ExecuteSql(sql);
				treeView1.SelectedNode.Remove();
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

	private void toolStripButton3_Click(object sender, EventArgs e)
	{
		try
		{
			if (treeView1.SelectedNode == null)
			{
				return;
			}
			text = treeView1.SelectedNode.Text;
			if (treeView1.SelectedNode == treeView1.Nodes[0])
			{
				return;
			}
			if (text != SQLserver.UserName)
			{
				try
				{
					myds.Merge(myds);
					int index = treeView1.SelectedNode.Index;
					uuuser_no = myds.Tables[0].Rows[index]["user_no"].ToString();
					uupassword = myds.Tables[0].Rows[index]["password"].ToString();
					uuname = myds.Tables[0].Rows[index]["ugname"].ToString();
					edituser edituser3 = new edituser(text, uuuser_no, uupassword, uuname);
					edituser3.ShowDialog();
					if (edituser3.DialogResult == DialogResult.OK)
					{
						treeView1.SelectedNode.Text = edituser3.uname;
						myds.Tables[0].Rows[index]["password"] = edituser3.upassword;
						myds.Tables[0].Rows[index]["user_no"] = edituser3.uuser_no;
						myds.Tables[0].Rows[index]["ugname"] = edituser3.unname;
						myds.AcceptChanges();
					}
					treeView1.Refresh();
					return;
				}
				catch (Exception ex)
				{
					Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex2)
		{
			Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.updateuser));
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.treeView1 = new System.Windows.Forms.TreeView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.toolStrip1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.toolStripButton2, this.toolStripButton3, this.toolStripButton4, this.toolStripSeparator1, this.toolStripButton5, this.toolStripSeparator2, this.toolStripButton1 });
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.toolStrip1.Size = new System.Drawing.Size(612, 56);
		this.toolStrip1.TabIndex = 2;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
		this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton2.Image = LockSoftware.Properties.Resources.Add;
		this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(102, 53);
		this.toolStripButton2.Text = "toolStripButton2";
		this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
		this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton3.Image = LockSoftware.Properties.Resources.group_edit;
		this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(102, 53);
		this.toolStripButton3.Text = "toolStripButton3";
		this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
		this.toolStripButton4.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton4.Image = LockSoftware.Properties.Resources.delete;
		this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(102, 53);
		this.toolStripButton4.Text = "toolStripButton4";
		this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
		this.toolStripButton5.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton5.Image = LockSoftware.Properties.Resources.application_side_boxes;
		this.toolStripButton5.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.Size = new System.Drawing.Size(102, 53);
		this.toolStripButton5.Text = "toolStripButton5";
		this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 56);
		this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripButton1.Image = LockSoftware.Properties.Resources.close;
		this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(102, 53);
		this.toolStripButton1.Text = "toolStripButton1";
		this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.treeView1.Location = new System.Drawing.Point(0, 56);
		this.treeView1.Name = "treeView1";
		this.treeView1.Size = new System.Drawing.Size(612, 364);
		this.treeView1.TabIndex = 3;
		this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView1_AfterSelect);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "User.gif");
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
		this.clsBackPanel1.Size = new System.Drawing.Size(612, 56);
		this.clsBackPanel1.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(612, 420);
		base.Controls.Add(this.treeView1);
		base.Controls.Add(this.clsBackPanel1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "updateuser";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "用户设置";
		base.Load += new System.EventHandler(updateuser_Load);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
