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

public class createuser : Form
{
	public string m_objName = "WFuc";

	public Hashtable m_htab;

	public string uname;

	public string unname;

	public string uuser_no;

	public string upassword;

	public string uupassword;

	private IContainer components;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private TextBox textBox2;

	private TextBox textBox3;

	private TextBox textBox4;

	private ComboBox comboBox1;

	private Label label1;

	private TextBox textBox1;

	private GlassBtn btnClose;

	private GlassBtn btnOK;

	public createuser()
	{
		InitializeComponent();
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		string text = textBox2.Text.ToString();
		if (text.Length == 0)
		{
			btnOK.Enabled = false;
		}
		else
		{
			btnOK.Enabled = true;
		}
	}

	private void createuser_Load(object sender, EventArgs e)
	{
		string text = null;
		m_htab = Program.GetControlName(this, m_objName);
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnClose.Text = (string)Program.m_hPubTab["btnCl"];
		btnOK.Enabled = false;
		try
		{
			text = "select distinct name from usergroup where name<>N'超级用户组'";
			DataSet dataSet = new DataSet();
			dataSet = SQLserver.Data_GetDataSet(text);
			comboBox1.DataSource = dataSet.Tables[0];
			comboBox1.DisplayMember = "name";
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, label3.Text.Trim().Substring(0, label3.Text.Trim().Length - 1));
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			string text = null;
			uname = textBox2.Text.Trim();
			unname = comboBox1.Text.Trim();
			uuser_no = textBox1.Text.Trim();
			upassword = textBox3.Text.Trim();
			uupassword = textBox4.Text.Trim();
			if (Program.isValNull(label1.Text.Trim().Substring(0, label1.Text.Trim().Length - 1), uuser_no.Trim(), chk: true) || Program.isValNull(label2.Text.Trim().Substring(0, label2.Text.Trim().Length - 1), uname.Trim(), chk: true) || Program.isValNull(label3.Text.Trim().Substring(0, label3.Text.Trim().Length - 1), unname.Trim(), chk: true))
			{
				return;
			}
			string sql = "select  user_name from userinfo where  user_no = N'" + uuser_no + "'";
			DataSet dataSet = SQLserver.Data_GetDataSet(sql);
			DataTable dataTable = dataSet.Tables[0];
			if (dataSet.Tables[0].Rows.Count != 0)
			{
				Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Asterisk);
				textBox1.SelectAll();
				return;
			}
			sql = "Select user_name from userinfo where user_name = N'" + uname + "'";
			dataTable.Clear();
			dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable.Rows.Count != 0)
			{
				Program.MsgCustom((string)m_htab["Info03"], MessageBoxIcon.Asterisk);
				textBox2.SelectAll();
			}
			else if (upassword == "" || uupassword == "")
			{
				if (Program.isValNull(label4.Text.Trim().Substring(0, label4.Text.Trim().Length - 1), upassword, chk: true))
				{
					textBox3.Select();
				}
				else if (Program.isValNull(label5.Text.Trim().Substring(0, label5.Text.Trim().Length - 1), uupassword, chk: true))
				{
					textBox4.Select();
				}
			}
			else if (upassword == uupassword)
			{
				try
				{
					text = "insert into userinfo(user_name,user_password,groupid,user_no)  select N'" + uname + "',N'" + upassword + "',groupid,N'" + uuser_no + "' from usergroup where name=N'" + unname + "'  ";
					SQLserver.Data_ExecuteSql(text);
					text = "insert into userpermission(user_no,functionid,show,fbrno)  select  user_no=(select user_no from userinfo where user_name=N'" + uname + "'  ),functionid,show,fbrno  from grouppermission where groupid = (select groupid from userinfo where user_name = N'" + uname + "') ";
					SQLserver.Data_ExecuteSql(text);
				}
				catch (Exception ex)
				{
					Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
					base.DialogResult = DialogResult.Cancel;
				}
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				Program.MsgCustom((string)m_htab["Info02"], MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex2)
		{
			Program.MsgCusErrMess(ex2.Message, Text);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkPWDInput(sender, e);
	}

	private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkPWDInput(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.createuser));
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		base.SuspendLayout();
		this.label2.Location = new System.Drawing.Point(5, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(112, 12);
		this.label2.TabIndex = 3;
		this.label2.Text = "用户名称:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label3.Location = new System.Drawing.Point(5, 73);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(112, 12);
		this.label3.TabIndex = 4;
		this.label3.Text = "用 户 组:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label4.Location = new System.Drawing.Point(5, 99);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(112, 12);
		this.label4.TabIndex = 5;
		this.label4.Text = "密    码:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label5.Location = new System.Drawing.Point(5, 126);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(112, 12);
		this.label5.TabIndex = 6;
		this.label5.Text = "密    码:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.textBox2.Location = new System.Drawing.Point(123, 42);
		this.textBox2.MaxLength = 100;
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(235, 21);
		this.textBox2.TabIndex = 8;
		this.textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.textBox3.Location = new System.Drawing.Point(123, 95);
		this.textBox3.MaxLength = 20;
		this.textBox3.Name = "textBox3";
		this.textBox3.PasswordChar = '*';
		this.textBox3.Size = new System.Drawing.Size(235, 21);
		this.textBox3.TabIndex = 9;
		this.textBox3.UseSystemPasswordChar = true;
		this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox3_KeyPress);
		this.textBox4.Location = new System.Drawing.Point(123, 122);
		this.textBox4.MaxLength = 20;
		this.textBox4.Name = "textBox4";
		this.textBox4.PasswordChar = '*';
		this.textBox4.Size = new System.Drawing.Size(235, 21);
		this.textBox4.TabIndex = 10;
		this.textBox4.UseSystemPasswordChar = true;
		this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox4_KeyPress);
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Location = new System.Drawing.Point(123, 69);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(235, 20);
		this.comboBox1.TabIndex = 11;
		this.label1.Location = new System.Drawing.Point(5, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(112, 12);
		this.label1.TabIndex = 12;
		this.label1.Text = "用户编码:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.textBox1.Location = new System.Drawing.Point(123, 15);
		this.textBox1.MaxLength = 20;
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(235, 21);
		this.textBox1.TabIndex = 13;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(289, 151);
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
		this.btnOK.Location = new System.Drawing.Point(195, 151);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(71, 32);
		this.btnOK.TabIndex = 14;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(372, 194);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.comboBox1);
		base.Controls.Add(this.textBox4);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "createuser";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "添加用户";
		base.Load += new System.EventHandler(createuser_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
