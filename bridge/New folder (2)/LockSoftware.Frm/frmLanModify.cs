using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataBase;

namespace LockSoftware.Frm;

public class frmLanModify : Form
{
	private IContainer components;

	private Label label1;

	private TextBox txtOPN;

	private Label label2;

	private TextBox txtCtrN;

	private Label label3;

	private TextBox txtEN;

	private Label label4;

	private TextBox txtCN;

	private Button btnNew;

	private Button btnClose;

	private Button button1;

	private ComboBox comboBox1;

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
		this.label1 = new System.Windows.Forms.Label();
		this.txtOPN = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCtrN = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtEN = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCN = new System.Windows.Forms.TextBox();
		this.btnNew = new System.Windows.Forms.Button();
		this.btnClose = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(23, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "界面：";
		this.txtOPN.Location = new System.Drawing.Point(70, 20);
		this.txtOPN.Name = "txtOPN";
		this.txtOPN.Size = new System.Drawing.Size(100, 21);
		this.txtOPN.TabIndex = 1;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(197, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "控件：";
		this.txtCtrN.Location = new System.Drawing.Point(256, 20);
		this.txtCtrN.Name = "txtCtrN";
		this.txtCtrN.Size = new System.Drawing.Size(100, 21);
		this.txtCtrN.TabIndex = 3;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(10, 64);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(41, 12);
		this.label3.TabIndex = 4;
		this.label3.Text = "英文：";
		this.txtEN.Location = new System.Drawing.Point(70, 61);
		this.txtEN.Name = "txtEN";
		this.txtEN.Size = new System.Drawing.Size(498, 21);
		this.txtEN.TabIndex = 5;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(10, 103);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 12);
		this.label4.TabIndex = 6;
		this.label4.Text = "中文：";
		this.txtCN.Location = new System.Drawing.Point(70, 100);
		this.txtCN.Name = "txtCN";
		this.txtCN.Size = new System.Drawing.Size(498, 21);
		this.txtCN.TabIndex = 7;
		this.btnNew.Location = new System.Drawing.Point(381, 137);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(75, 23);
		this.btnNew.TabIndex = 8;
		this.btnNew.Text = "添加";
		this.btnNew.UseVisualStyleBackColor = true;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.btnClose.Location = new System.Drawing.Point(493, 137);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(75, 23);
		this.btnClose.TabIndex = 9;
		this.btnClose.Text = "关闭";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.button1.Location = new System.Drawing.Point(281, 137);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 10;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[3] { "English-EN", "简体中文-CN", "繁体中文-TC" });
		this.comboBox1.Location = new System.Drawing.Point(70, 140);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(121, 20);
		this.comboBox1.TabIndex = 11;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(602, 196);
		base.Controls.Add(this.comboBox1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.txtCN);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.txtEN);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.txtCtrN);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.txtOPN);
		base.Controls.Add(this.label1);
		base.Name = "frmLanModify";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "语言编辑";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmLanModify()
	{
		InitializeComponent();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		try
		{
			if (Program.isValNull(label1.Text.Substring(0, label1.Text.Length - 1), txtOPN.Text.Trim(), chk: true) || Program.isValNull(label2.Text.Substring(0, label2.Text.Length - 1), txtCtrN.Text.Trim(), chk: true) || Program.isValNull(label3.Text.Substring(0, label3.Text.Length - 1), txtEN.Text.Trim(), chk: true) || Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtCN.Text.Trim(), chk: true))
			{
				return;
			}
			DataTable dataTable = SQLserver.Data_GetDataTable("Select * From D_Object Where O_ParentModel = '" + txtOPN.Text.Trim() + "' And O_name = '" + txtCtrN.Text.Trim() + "'");
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				Program.MsgCustom("该界面的控件语言已存在！", MessageBoxIcon.Exclamation);
				return;
			}
			string sqlstr = "Insert into D_Object Values('" + txtOPN.Text.Trim() + "', '" + txtCtrN.Text.Trim() + "', N'" + txtEN.Text.Trim() + "',N'" + txtCN.Text.Trim() + "')";
			if (SQLserver.Data_ExecuteSql(sqlstr) < 0)
			{
				Program.MsgCustom("添加语言失败，请重试！", MessageBoxIcon.Hand);
				return;
			}
			TextBox textBox = txtCtrN;
			TextBox textBox2 = txtEN;
			string text = (txtCN.Text = "");
			string text3 = (textBox2.Text = text);
			textBox.Text = text3;
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (comboBox1.Text == "")
		{
			return;
		}
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		folderBrowserDialog.ShowNewFolderButton = true;
		folderBrowserDialog.SelectedPath = Application.StartupPath;
		folderBrowserDialog.Description = "导出语言";
		if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
		{
			return;
		}
		string text = "";
		string text2 = "";
		text = comboBox1.Text;
		switch (comboBox1.SelectedIndex)
		{
		case 0:
			text2 = "O_en";
			break;
		case 1:
			text2 = "O_cn";
			break;
		case 2:
			text2 = "O_zh";
			break;
		}
		DataTable dataTable = SQLserver.Data_GetDataTable("Select O_ParentModel, O_name, " + text2 + " From D_Object Order by O_ParentModel, O_name");
		if (dataTable.Rows.Count <= 0)
		{
			return;
		}
		string selectedPath = folderBrowserDialog.SelectedPath;
		StreamWriter streamWriter = null;
		StreamWriter streamWriter2 = null;
		try
		{
			string path = selectedPath + "\\lantype" + comboBox1.SelectedIndex.ToString("D2") + ".xml";
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			streamWriter = new StreamWriter(path, append: false);
			streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
			streamWriter.WriteLine("<Radio LanType = '" + text + "'>");
			string text3 = "";
			string text4 = "";
			string text5 = "";
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (text3 != dataTable.Rows[i]["O_ParentModel"].ToString().Trim())
				{
					if (text3 != "")
					{
						streamWriter.WriteLine("</C" + text3.ToUpperInvariant() + ">");
					}
					text4 = dataTable.Rows[i]["O_ParentModel"].ToString().Trim();
					streamWriter.WriteLine("<C" + text4.ToUpperInvariant() + ">");
					text3 = text4;
				}
				text5 = dataTable.Rows[i][text2].ToString().Trim();
				text5 = text5.Replace("&", "&amp;");
				streamWriter.WriteLine("<" + dataTable.Rows[i]["O_name"].ToString().Trim() + " value = \"" + text5 + "\"/>");
			}
			if (text3 != "")
			{
				streamWriter.WriteLine("</C" + text3.ToUpperInvariant() + ">");
			}
			streamWriter.WriteLine("</Radio>");
			streamWriter.Close();
			string text6 = "路徑為：" + selectedPath;
			MessageBox.Show("資料轉出成功！\r\n" + text6, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			streamWriter2?.Close();
			streamWriter?.Close();
			MessageBox.Show("創建轉出文件失敗！錯誤信息：" + ex.Message, "資料轉出錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}
