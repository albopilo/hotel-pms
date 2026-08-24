using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmDataBaseMgr : Form
{
	public string m_objName = "WFdbm";

	public Hashtable m_htab;

	private IContainer components;

	private ToolsBtn tbtnRestore;

	private ToolsBtn tbtnBackup;

	private OpenFileDialog OpFile;

	private SaveFileDialog SaFile;

	private ToolsBtn toolsBtn1;

	private Label label1;

	private CheckBox checkBox1;

	public frmDataBaseMgr()
	{
		InitializeComponent();
	}

	private void tbtnBackup_Click(object sender, EventArgs e)
	{
		try
		{
			SaFile.Filter = "SQLServer2005|*.bak|AllFiles|*.*";
			SaFile.FileName = "LockBackup_" + DateTime.Now.ToString("yyyyMMdd");
			if (SaFile.ShowDialog() == DialogResult.OK)
			{
				string fileName = SaFile.FileName;
				if (SQLserver.DataBase_Backup(Program.m_SqlSN, Program.m_SqlDN, Program.m_SqlUN, Program.m_SqlUPWD, fileName) != 1)
				{
					Program.MsgCustom((string)m_htab["Err01"], MessageBoxIcon.Hand);
				}
				else
				{
					Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)m_htab["Err02"] + "\r\n" + ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void tbtnRestore_Click(object sender, EventArgs e)
	{
		if (Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
		{
			return;
		}
		try
		{
			OpFile.Filter = "SQLServer2005|*.bak|AllFiles|*.*";
			OpFile.FileName = "LockBackup";
			if (OpFile.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			string fileName = OpFile.FileName;
			if (File.Exists(fileName))
			{
				if (Program.SetSingleItem("RESTOREFILE", fileName))
				{
					Program.m_Exit = true;
					Program.mutex.Close();
					Application.Exit();
					Application.Restart();
				}
			}
			else
			{
				Program.MsgCustom((string)m_htab["Err04"], MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			Program.MsgCustom((string)m_htab["Err05"] + "\r\n" + ex.Message, MessageBoxIcon.Hand);
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		ToolsBtn toolsBtn = tbtnBackup;
		bool enabled = (tbtnRestore.Enabled = checkBox1.Checked);
		toolsBtn.Enabled = enabled;
	}

	private void frmDataBaseMgr_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmDataBaseMgr));
		this.OpFile = new System.Windows.Forms.OpenFileDialog();
		this.SaFile = new System.Windows.Forms.SaveFileDialog();
		this.label1 = new System.Windows.Forms.Label();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.tbtnBackup = new LockSoftware.Controls.ToolsBtn(this.components);
		this.tbtnRestore = new LockSoftware.Controls.ToolsBtn(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		base.SuspendLayout();
		this.OpFile.FileName = "openFileDialog1";
		this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label1.Font = new System.Drawing.Font("Verdana", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label1.Location = new System.Drawing.Point(168, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(425, 36);
		this.label1.TabIndex = 2;
		this.label1.Text = "*注：备份与还原仅适合在服务器上进行操作。";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Font = new System.Drawing.Font("Verdana", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.checkBox1.ForeColor = System.Drawing.Color.Green;
		this.checkBox1.Location = new System.Drawing.Point(168, 58);
		this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(132, 21);
		this.checkBox1.TabIndex = 4;
		this.checkBox1.Text = "确认数据库操作";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.tbtnBackup.BackColor = System.Drawing.Color.Transparent;
		this.tbtnBackup.Checked = false;
		this.tbtnBackup.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.tbtnBackup.DefaultColor = System.Drawing.Color.Transparent;
		this.tbtnBackup.Enabled = false;
		this.tbtnBackup.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.tbtnBackup.GuidInfo = "&56~01'][Manson]v%#@";
		this.tbtnBackup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.tbtnBackup.ImageNew = LockSoftware.Properties.Resources.mdf_ndf_dbfiles;
		this.tbtnBackup.ImageRedrawed = true;
		this.tbtnBackup.ImageStyle = 0;
		this.tbtnBackup.isButton = true;
		this.tbtnBackup.Location = new System.Drawing.Point(214, 99);
		this.tbtnBackup.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.tbtnBackup.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.tbtnBackup.MouseDownStartColor = System.Drawing.Color.White;
		this.tbtnBackup.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.tbtnBackup.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.tbtnBackup.MouseEnterStartColor = System.Drawing.Color.White;
		this.tbtnBackup.Name = "tbtnBackup";
		this.tbtnBackup.Size = new System.Drawing.Size(140, 54);
		this.tbtnBackup.TabIndex = 0;
		this.tbtnBackup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.tbtnBackup.TextImageLocation = 1;
		this.tbtnBackup.TextNew = "数据库备份";
		this.tbtnBackup.TextRedrawed = false;
		this.tbtnBackup.Click += new System.EventHandler(tbtnBackup_Click);
		this.tbtnRestore.BackColor = System.Drawing.Color.Transparent;
		this.tbtnRestore.Checked = false;
		this.tbtnRestore.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.tbtnRestore.DefaultColor = System.Drawing.Color.Transparent;
		this.tbtnRestore.Enabled = false;
		this.tbtnRestore.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.tbtnRestore.GuidInfo = "&56~01'][Manson]v%#@";
		this.tbtnRestore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.tbtnRestore.ImageNew = LockSoftware.Properties.Resources.Data_Dataset;
		this.tbtnRestore.ImageRedrawed = true;
		this.tbtnRestore.ImageStyle = 0;
		this.tbtnRestore.isButton = true;
		this.tbtnRestore.Location = new System.Drawing.Point(411, 99);
		this.tbtnRestore.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.tbtnRestore.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.tbtnRestore.MouseDownStartColor = System.Drawing.Color.White;
		this.tbtnRestore.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.tbtnRestore.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.tbtnRestore.MouseEnterStartColor = System.Drawing.Color.White;
		this.tbtnRestore.Name = "tbtnRestore";
		this.tbtnRestore.Size = new System.Drawing.Size(140, 54);
		this.tbtnRestore.TabIndex = 1;
		this.tbtnRestore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.tbtnRestore.TextImageLocation = 0;
		this.tbtnRestore.TextNew = "数据库还原";
		this.tbtnRestore.TextRedrawed = false;
		this.tbtnRestore.Click += new System.EventHandler(tbtnRestore_Click);
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources._024;
		this.toolsBtn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.toolsBtn1.ImageNew = null;
		this.toolsBtn1.ImageRedrawed = true;
		this.toolsBtn1.ImageStyle = 0;
		this.toolsBtn1.isButton = false;
		this.toolsBtn1.Location = new System.Drawing.Point(0, 0);
		this.toolsBtn1.MouseDownBorderColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.toolsBtn1.MouseDownEndColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseEnterBorderColor = System.Drawing.Color.FromArgb(37, 199, 0);
		this.toolsBtn1.MouseEnterEndColor = System.Drawing.Color.FromArgb(164, 254, 85);
		this.toolsBtn1.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn1.Name = "toolsBtn1";
		this.toolsBtn1.Size = new System.Drawing.Size(602, 189);
		this.toolsBtn1.TabIndex = 3;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "";
		this.toolsBtn1.TextRedrawed = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(602, 189);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.tbtnBackup);
		base.Controls.Add(this.tbtnRestore);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.toolsBtn1);
		this.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmDataBaseMgr";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "数据库备份与还原";
		base.Load += new System.EventHandler(frmDataBaseMgr_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
