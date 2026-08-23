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

public class updatepas : Form
{
	public string m_objName = "WFup";

	public Hashtable m_htab;

	private static string name;

	private string strSql;

	private static string password;

	private IContainer components;

	private Label label2;

	private Label label3;

	private TextBox textBox1;

	private TextBox textBox3;

	private TextBox textBox4;

	private Label label5;

	private Label label6;

	private Label label1;

	private GroupBox groupBox1;

	private ToolsBtn toolsBtn2;

	private GlassBtn btnOK;

	private GlassBtn btnCl;

	public updatepas()
	{
		InitializeComponent();
	}

	private void updatepas_Load(object sender, EventArgs e)
	{
		label1.Text = Program.m_OperName;
		name = label1.Text.Trim();
		textBox4.Focus();
		m_htab = Program.GetControlName(this, m_objName);
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnCl.Text = (string)Program.m_hPubTab["btnCl"];
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		password = textBox1.Text.Trim();
		try
		{
			strSql = "select * from userinfo where user_name=N'" + name + "' and user_password=N'" + password + "' ";
			DataSet dataSet = SQLserver.Data_GetDataSet(strSql);
			DataTable dataTable = dataSet.Tables[0];
			if (dataTable.Rows.Count <= 0)
			{
				Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Hand);
				textBox1.Select();
				return;
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			textBox1.Focus();
			return;
		}
		try
		{
			string text = textBox3.Text.Trim();
			string text2 = textBox4.Text.Trim();
			if (Program.isValNull(label6.Text.Trim().Substring(0, label6.Text.Trim().Length - 1), text.Trim(), chk: true))
			{
				textBox3.Select();
				return;
			}
			if (Program.isValNull(label5.Text.Trim().Substring(0, label5.Text.Trim().Length - 1), text2.Trim(), chk: true))
			{
				textBox4.Select();
				return;
			}
			if (text == text2)
			{
				string text3 = null;
				text3 = "update userinfo set user_password = N'" + text + "' where user_name = N'" + Program.m_OperName + "' And user_no=N'" + Program.m_OperID + "'";
				SQLserver.Data_ExecuteSql(text3);
				SQLserver.UserPassword = (Program.m_OperPwd = text);
			}
			else if (text != text2)
			{
				Program.MsgCustom((string)m_htab["Info02"], MessageBoxIcon.Asterisk);
				textBox3.SelectAll();
				textBox4.SelectAll();
				return;
			}
		}
		catch (Exception ex2)
		{
			Program.MsgBox(ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		Program.MsgCustom((string)m_htab["Info03"], MessageBoxIcon.Asterisk);
		Close();
	}

	private void btnCl_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkPWDInput(sender, e);
	}

	private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkPWDInput(sender, e);
	}

	private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.updatepas));
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.toolsBtn2 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.label2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label2.Location = new System.Drawing.Point(12, 85);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(127, 14);
		this.label2.TabIndex = 1;
		this.label2.Text = "用户名:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label3.Location = new System.Drawing.Point(12, 122);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(127, 14);
		this.label3.TabIndex = 2;
		this.label3.Text = "旧密码:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.textBox1.Location = new System.Drawing.Point(160, 119);
		this.textBox1.Name = "textBox1";
		this.textBox1.PasswordChar = '*';
		this.textBox1.Size = new System.Drawing.Size(160, 21);
		this.textBox1.TabIndex = 1;
		this.textBox1.UseSystemPasswordChar = true;
		this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
		this.textBox3.Location = new System.Drawing.Point(160, 189);
		this.textBox3.MaxLength = 20;
		this.textBox3.Name = "textBox3";
		this.textBox3.PasswordChar = '*';
		this.textBox3.Size = new System.Drawing.Size(160, 21);
		this.textBox3.TabIndex = 3;
		this.textBox3.UseSystemPasswordChar = true;
		this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox3_KeyPress);
		this.textBox4.Location = new System.Drawing.Point(160, 154);
		this.textBox4.MaxLength = 20;
		this.textBox4.Name = "textBox4";
		this.textBox4.PasswordChar = '*';
		this.textBox4.Size = new System.Drawing.Size(160, 21);
		this.textBox4.TabIndex = 2;
		this.textBox4.UseSystemPasswordChar = true;
		this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox4_KeyPress);
		this.label5.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label5.Location = new System.Drawing.Point(12, 157);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(127, 14);
		this.label5.TabIndex = 9;
		this.label5.Text = "新密码:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label6.Location = new System.Drawing.Point(12, 192);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(127, 14);
		this.label6.TabIndex = 10;
		this.label6.Text = "新密码:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 1);
		this.label1.Location = new System.Drawing.Point(160, 85);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(49, 14);
		this.label1.TabIndex = 14;
		this.label1.Text = "label1";
		this.groupBox1.Controls.Add(this.btnCl);
		this.groupBox1.Controls.Add(this.btnOK);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 213);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(426, 53);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.btnCl.BackColor = System.Drawing.Color.Gainsboro;
		this.btnCl.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.close;
		this.btnCl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(335, 13);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnCl.Size = new System.Drawing.Size(74, 28);
		this.btnCl.TabIndex = 4;
		this.btnCl.Text = "取 消";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.Click += new System.EventHandler(btnCl_Click);
		this.btnOK.BackColor = System.Drawing.Color.Gainsboro;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(246, 13);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnOK.Size = new System.Drawing.Size(74, 28);
		this.btnOK.TabIndex = 5;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.toolsBtn2.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Checked = true;
		this.toolsBtn2.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn2.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn2.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 1);
		this.toolsBtn2.ForeColor = System.Drawing.Color.Green;
		this.toolsBtn2.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.toolsBtn2.ImageNew = LockSoftware.Properties.Resources.key_64x64;
		this.toolsBtn2.ImageRedrawed = true;
		this.toolsBtn2.ImageStyle = 0;
		this.toolsBtn2.isButton = false;
		this.toolsBtn2.Location = new System.Drawing.Point(0, 0);
		this.toolsBtn2.MouseDownBorderColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.toolsBtn2.MouseDownEndColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn2.MouseEnterEndColor = System.Drawing.Color.White;
		this.toolsBtn2.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn2.Name = "toolsBtn2";
		this.toolsBtn2.Size = new System.Drawing.Size(426, 69);
		this.toolsBtn2.TabIndex = 17;
		this.toolsBtn2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.toolsBtn2.TextImageLocation = 3;
		this.toolsBtn2.TextNew = "          〓设置当前操作人员进入系统口令，当口令改变时必须输入旧口令，在连续输入两次新口令后，并确认。";
		this.toolsBtn2.TextRedrawed = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(426, 266);
		base.Controls.Add(this.toolsBtn2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.textBox4);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "updatepas";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "修改用户密码";
		base.Load += new System.EventHandler(updatepas_Load);
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
