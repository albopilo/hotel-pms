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

public class create : Form
{
	private IContainer components;

	private Label label1;

	private TextBox textBox1;

	private GlassBtn btnClose;

	private GlassBtn btnOK;

	public string m_objName = "WFugc";

	public Hashtable m_htab;

	private string textbox1;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.create));
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(70, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "用户组名称:";
		this.textBox1.Location = new System.Drawing.Point(12, 37);
		this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(234, 21);
		this.textBox1.TabIndex = 1;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(175, 67);
		this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(69, 32);
		this.btnClose.TabIndex = 13;
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
		this.btnOK.Location = new System.Drawing.Point(88, 67);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(71, 32);
		this.btnOK.TabIndex = 12;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(256, 113);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnOK);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "create";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "添加用户组";
		base.Load += new System.EventHandler(create_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public create()
	{
		InitializeComponent();
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		textbox1 = textBox1.Text.Trim();
		if (textbox1.Length == 0)
		{
			btnOK.Enabled = false;
		}
		else
		{
			btnOK.Enabled = true;
		}
	}

	private void create_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnClose.Text = (string)Program.m_hPubTab["btnCl"];
		btnOK.Enabled = false;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		string sql = "select distinct name from usergroup where name = N'" + textbox1 + "'";
		DataSet dataSet = SQLserver.Data_GetDataSet(sql);
		_ = dataSet.Tables[0];
		if (int.Parse(dataSet.Tables[0].Rows.Count.ToString()) != 0)
		{
			Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Exclamation);
			textBox1.SelectAll();
			return;
		}
		try
		{
			sql = "insert into usergroup(name,issys) values(N'" + textbox1 + "',0)";
			SQLserver.Data_ExecuteSql(sql);
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			base.DialogResult = DialogResult.Cancel;
		}
		base.DialogResult = DialogResult.OK;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}
}
