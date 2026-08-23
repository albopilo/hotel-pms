using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LockSoftware.Controls;
using LockSoftware.Properties;
using SQLDMO;

namespace LockSoftware.Frm;

public class frmDB : Form
{
	private IContainer components;

	private Label label1;

	private Label label2;

	private Panel panel1;

	private ListView lvServ;

	private Panel panel2;

	public GlassBtn btnOK;

	public GlassBtn btnCl;

	private ColumnHeader col01;

	public string m_svrname = "";

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmDB));
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lvServ = new System.Windows.Forms.ListView();
		this.col01 = new System.Windows.Forms.ColumnHeader();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(6, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(193, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Please choose your server name:";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(6, 40);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(134, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "请选择您的服务器：";
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(227, 60);
		this.panel1.TabIndex = 2;
		this.lvServ.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.col01 });
		this.lvServ.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvServ.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lvServ.GridLines = true;
		this.lvServ.Location = new System.Drawing.Point(0, 60);
		this.lvServ.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.lvServ.Name = "lvServ";
		this.lvServ.Size = new System.Drawing.Size(227, 224);
		this.lvServ.TabIndex = 3;
		this.lvServ.UseCompatibleStateImageBehavior = false;
		this.lvServ.View = System.Windows.Forms.View.Details;
		this.lvServ.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(lvServ_ItemSelectionChanged);
		this.col01.Text = "Server Name(服务器名称)";
		this.col01.Width = 200;
		this.panel2.BackColor = System.Drawing.SystemColors.Control;
		this.panel2.Controls.Add(this.btnCl);
		this.panel2.Controls.Add(this.btnOK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 284);
		this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(227, 48);
		this.panel2.TabIndex = 4;
		this.btnCl.BackColor = System.Drawing.Color.LightGray;
		this.btnCl.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.search;
		this.btnCl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(8, 8);
		this.btnCl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(82, 35);
		this.btnCl.TabIndex = 8;
		this.btnCl.Text = "Search";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.Click += new System.EventHandler(btnCl_Click);
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(148, 8);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(67, 35);
		this.btnOK.TabIndex = 9;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(227, 332);
		base.Controls.Add(this.lvServ);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmDB";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SQL Server Name";
		base.Load += new System.EventHandler(frmDB_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public frmDB()
	{
		InitializeComponent();
	}

	private void btnCl_Click(object sender, EventArgs e)
	{
		string text = "None SqlServer could be found.";
		text += "\r\n未搜索到可用的 SqlServer 服务器！";
		try
		{
			lvServ.Items.Clear();
			SQLDMO.Application application = new ApplicationClass();
			NameList nameList = application.ListAvailableSQLServers();
			for (int i = 0; i < nameList.Count; i++)
			{
				object obj = nameList.Item(i + 1);
				if (obj != null)
				{
					lvServ.Items.Add(obj.ToString());
				}
			}
			if (lvServ.Items.Count <= 0)
			{
				Program.MsgBox(text, null, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch
		{
			Program.MsgBox(text, null, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void frmDB_Load(object sender, EventArgs e)
	{
		btnCl_Click(null, null);
	}

	private void lvServ_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		try
		{
			if (e.IsSelected)
			{
				m_svrname = e.Item.Text;
			}
		}
		catch
		{
		}
	}
}
