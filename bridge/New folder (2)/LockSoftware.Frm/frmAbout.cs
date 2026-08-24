using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentDll;
using Dev_C_Sharp;
using LockSoftware.Controls;

namespace LockSoftware.Frm;

internal class frmAbout : Form
{
	private IContainer components;

	private PictureBox logoPictureBox;

	private Label labelProductName;

	private Label labelVersion;

	private Label labelCopyright;

	private TextBox textBoxDescription;

	private ComponentDll.GlassBtn okButton;

	private Label label2;

	private Label label1;

	private Label labelCompanyName;

	private TextBox txtKey;

	private Label label4;

	private TextBox txtUID;

	private Label label3;

	private ComponentDll.GlassBtn btnReg;

	private clsBackPanel clsBackPanel1;

	private Label labInfo;

	public string m_objName = "WFab";

	public Hashtable m_htab;

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
		this.labelProductName = new System.Windows.Forms.Label();
		this.labelVersion = new System.Windows.Forms.Label();
		this.labelCopyright = new System.Windows.Forms.Label();
		this.textBoxDescription = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.labelCompanyName = new System.Windows.Forms.Label();
		this.okButton = new ComponentDll.GlassBtn();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.labInfo = new System.Windows.Forms.Label();
		this.btnReg = new ComponentDll.GlassBtn();
		this.logoPictureBox = new System.Windows.Forms.PictureBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtKey = new System.Windows.Forms.TextBox();
		this.txtUID = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.clsBackPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.logoPictureBox).BeginInit();
		base.SuspendLayout();
		this.labelProductName.AutoSize = true;
		this.labelProductName.Location = new System.Drawing.Point(11, 141);
		this.labelProductName.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.labelProductName.MaximumSize = new System.Drawing.Size(0, 20);
		this.labelProductName.Name = "labelProductName";
		this.labelProductName.Size = new System.Drawing.Size(55, 15);
		this.labelProductName.TabIndex = 19;
		this.labelProductName.Text = "产品名称";
		this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelVersion.AutoSize = true;
		this.labelVersion.Location = new System.Drawing.Point(11, 197);
		this.labelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.labelVersion.MaximumSize = new System.Drawing.Size(0, 20);
		this.labelVersion.Name = "labelVersion";
		this.labelVersion.Size = new System.Drawing.Size(31, 15);
		this.labelVersion.TabIndex = 0;
		this.labelVersion.Text = "版本";
		this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelCopyright.AutoSize = true;
		this.labelCopyright.Location = new System.Drawing.Point(11, 169);
		this.labelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
		this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 20);
		this.labelCopyright.Name = "labelCopyright";
		this.labelCopyright.Size = new System.Drawing.Size(31, 15);
		this.labelCopyright.TabIndex = 21;
		this.labelCopyright.Text = "版权";
		this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.textBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBoxDescription.BackColor = System.Drawing.Color.White;
		this.textBoxDescription.Location = new System.Drawing.Point(14, 277);
		this.textBoxDescription.Margin = new System.Windows.Forms.Padding(7, 4, 3, 4);
		this.textBoxDescription.Multiline = true;
		this.textBoxDescription.Name = "textBoxDescription";
		this.textBoxDescription.ReadOnly = true;
		this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.textBoxDescription.Size = new System.Drawing.Size(422, 143);
		this.textBoxDescription.TabIndex = 23;
		this.textBoxDescription.TabStop = false;
		this.textBoxDescription.Text = "说明";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.label2.Location = new System.Drawing.Point(172, 167);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(172, 15);
		this.label2.TabIndex = 14;
		this.label2.Text = "iGo Auto Tech,.Faster your way";
		this.label2.Visible = false;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 18f);
		this.label1.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.label1.Location = new System.Drawing.Point(118, 141);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(228, 27);
		this.label1.TabIndex = 13;
		this.label1.Text = "艾高自动，为您加速";
		this.label1.Visible = false;
		this.labelCompanyName.Location = new System.Drawing.Point(11, 225);
		this.labelCompanyName.Name = "labelCompanyName";
		this.labelCompanyName.Size = new System.Drawing.Size(401, 48);
		this.labelCompanyName.TabIndex = 27;
		this.labelCompanyName.Text = "公司名称";
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.BackColor = System.Drawing.Color.Gainsboro;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.okButton.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.okButton.ForeColor = System.Drawing.Color.Black;
		this.okButton.GlowColor = System.Drawing.Color.White;
		this.okButton.GuidInfo = "&56~01'][Manson]v%#@";
		this.okButton.InnerBorderColor = System.Drawing.Color.Gray;
		this.okButton.Location = new System.Drawing.Point(141, 97);
		this.okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.okButton.Name = "okButton";
		this.okButton.OuterBorderColor = System.Drawing.Color.Gainsboro;
		this.okButton.Size = new System.Drawing.Size(87, 29);
		this.okButton.TabIndex = 24;
		this.okButton.Text = "确定(&O)";
		this.okButton.Visible = false;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.clsBackPanel1.Border = true;
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
		this.clsBackPanel1.Controls.Add(this.labInfo);
		this.clsBackPanel1.Controls.Add(this.btnReg);
		this.clsBackPanel1.Controls.Add(this.logoPictureBox);
		this.clsBackPanel1.Controls.Add(this.label3);
		this.clsBackPanel1.Controls.Add(this.okButton);
		this.clsBackPanel1.Controls.Add(this.txtKey);
		this.clsBackPanel1.Controls.Add(this.txtUID);
		this.clsBackPanel1.Controls.Add(this.label4);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(450, 134);
		this.clsBackPanel1.TabIndex = 28;
		this.labInfo.AutoSize = true;
		this.labInfo.BackColor = System.Drawing.Color.Transparent;
		this.labInfo.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labInfo.Location = new System.Drawing.Point(185, 104);
		this.labInfo.Name = "labInfo";
		this.labInfo.Size = new System.Drawing.Size(0, 14);
		this.labInfo.TabIndex = 31;
		this.btnReg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReg.AutoSize = true;
		this.btnReg.BackColor = System.Drawing.Color.Gainsboro;
		this.btnReg.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReg.ForeColor = System.Drawing.Color.Black;
		this.btnReg.GlowColor = System.Drawing.Color.White;
		this.btnReg.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnReg.InnerBorderColor = System.Drawing.Color.Gray;
		this.btnReg.Location = new System.Drawing.Point(344, 97);
		this.btnReg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.btnReg.Name = "btnReg";
		this.btnReg.OuterBorderColor = System.Drawing.Color.Gainsboro;
		this.btnReg.Size = new System.Drawing.Size(92, 29);
		this.btnReg.TabIndex = 30;
		this.btnReg.Text = "软件注册";
		this.btnReg.Click += new System.EventHandler(btnReg_Click);
		this.logoPictureBox.BackColor = System.Drawing.Color.Transparent;
		this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.logoPictureBox.Location = new System.Drawing.Point(3, 4);
		this.logoPictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.logoPictureBox.Name = "logoPictureBox";
		this.logoPictureBox.Size = new System.Drawing.Size(129, 100);
		this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.logoPictureBox.TabIndex = 12;
		this.logoPictureBox.TabStop = false;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(138, 8);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(60, 16);
		this.label3.TabIndex = 25;
		this.label3.Text = "User ID :";
		this.txtKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtKey.Location = new System.Drawing.Point(141, 69);
		this.txtKey.Name = "txtKey";
		this.txtKey.Size = new System.Drawing.Size(295, 21);
		this.txtKey.TabIndex = 28;
		this.txtUID.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtUID.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtUID.Location = new System.Drawing.Point(141, 27);
		this.txtUID.Name = "txtUID";
		this.txtUID.ReadOnly = true;
		this.txtUID.Size = new System.Drawing.Size(295, 21);
		this.txtUID.TabIndex = 26;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(138, 51);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(67, 16);
		this.label4.TabIndex = 27;
		this.label4.Text = "User Key :";
		base.AcceptButton = this.okButton;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(450, 432);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.labelCompanyName);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.labelProductName);
		base.Controls.Add(this.labelVersion);
		base.Controls.Add(this.labelCopyright);
		base.Controls.Add(this.textBoxDescription);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAbout";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "关 于";
		base.Load += new System.EventHandler(frmAbout_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmAbout_KeyDown);
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.logoPictureBox).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public frmAbout()
	{
		InitializeComponent();
		Program.LoadLogo("AboutLOGO.png", logoPictureBox);
		m_htab = Program.GetControlName(this, m_objName);
		string text = "";
		Text = string.Format((string)m_htab["Info01"], Program.AssemblyTitle);
		labelProductName.Text = string.Format((string)m_htab["Info02"], Program.AssemblyProduct);
		labelVersion.Text = string.Format((string)m_htab["Info03"], Program.AssemblyVersion);
		labelCopyright.Text = string.Format((string)m_htab["Info04"], Program.AssemblyCopyright);
		labelCompanyName.Text = string.Format((string)m_htab["Info05"], Program.AssemblyCompany);
		textBoxDescription.Text = Program.AssemblyDescription + " " + (string)m_htab["Info06"] + "\r\n" + text;
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmAbout_Load(object sender, EventArgs e)
	{
		try
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			StringBuilder stringBuilder2 = new StringBuilder(256);
			global::Dev_C_Sharp.Dev_C_Sharp.Instance.GetRegInfo(stringBuilder, stringBuilder2);
			string regID = (txtUID.Text = stringBuilder.ToString());
			Program.m_regID = regID;
			Program.m_regKey = stringBuilder2.ToString();
			int num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.ChkReg(stringBuilder.ToString(), stringBuilder2.ToString(), chkid: false);
			if (num >= 0)
			{
				labInfo.Text = string.Format((string)m_htab["Info08"], num);
			}
			else
			{
				labInfo.Text = (string)Program.m_hPubTab["Err_C_" + -num];
			}
		}
		catch
		{
		}
	}

	private void btnReg_Click(object sender, EventArgs e)
	{
		try
		{
			if (!Program.isValNull(label3.Text.Substring(0, label3.Text.Length - 1), txtUID.Text.Trim(), chk: true) && !Program.isValNull(label4.Text.Substring(0, label4.Text.Length - 1), txtKey.Text.Trim(), chk: true))
			{
				int num = global::Dev_C_Sharp.Dev_C_Sharp.Instance.WriteKey(txtKey.Text.Trim());
				string text = "";
				if (num < 0)
				{
					text = string.Format((string)Program.m_hPubTab["Err_RegInfo"], num + "\r\n") + (string)Program.m_hPubTab["Err_C_" + -num];
					Program.MsgCustom((string)m_htab["Info09"] + "\r\n" + text, MessageBoxIcon.Hand);
					return;
				}
				text = (string)Program.m_hPubTab["InfoReg01"] + "\r\n" + string.Format((string)m_htab["Info08"], num) + "\r\n" + (string)Program.m_hPubTab["InfoReg02"];
				Program.MsgCustom(text, MessageBoxIcon.Asterisk);
				Program.m_Exit = true;
				Application.Exit();
			}
		}
		catch
		{
		}
	}

	private void frmAbout_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.Control && e.KeyCode == Keys.R)
		{
			txtKey.Text = Program.m_regKey;
		}
	}
}
