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

public class edituser2 : Form
{
	public string m_objName = "WFuge";

	public Hashtable m_htab;

	private string groupname;

	public string groupname1;

	private Label label1;

	private Label label2;

	private Label label3;

	private ComboBox comboBox1;

	private GlassBtn btnClose;

	private IContainer components;

	private GlassBtn btnOK;

	public edituser2(string groupname)
	{
		this.groupname = groupname;
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.edituser2));
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		base.SuspendLayout();
		this.label1.Location = new System.Drawing.Point(13, 56);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(103, 38);
		this.label1.TabIndex = 0;
		this.label1.Text = "User Group Name:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label2.Location = new System.Drawing.Point(12, 26);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(103, 30);
		this.label2.TabIndex = 1;
		this.label2.Text = "用  户:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label3.Location = new System.Drawing.Point(121, 24);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(49, 14);
		this.label3.TabIndex = 2;
		this.label3.Text = "label3";
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(124, 56);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(156, 20);
		this.comboBox1.TabIndex = 3;
		this.comboBox1.DropDown += new System.EventHandler(comboBox1_DropDown_1);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(211, 100);
		this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(69, 32);
		this.btnClose.TabIndex = 15;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.AutoSize = true;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(124, 100);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(71, 32);
		this.btnOK.TabIndex = 14;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.ClientSize = new System.Drawing.Size(292, 146);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.comboBox1);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "edituser2";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "编辑用户组";
		base.Load += new System.EventHandler(edituser2_Load_1);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void comboBox1_DropDown_1(object sender, EventArgs e)
	{
	}

	private void edituser2_Load_1(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnClose.Text = (string)Program.m_hPubTab["btnCl"];
		label3.Text = groupname;
		string text = null;
		try
		{
			text = "select distinct name from usergroup where name<>N'超级用户组'";
			DataSet dataSet = new DataSet();
			dataSet.Clear();
			dataSet = SQLserver.Data_GetDataSet(text);
			comboBox1.DataSource = dataSet.Tables[0];
			comboBox1.DisplayMember = "name";
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		groupname1 = comboBox1.Text.Trim();
		base.DialogResult = DialogResult.OK;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}
}
