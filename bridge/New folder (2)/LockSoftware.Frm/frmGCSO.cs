using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmGCSO : Form
{
	public string m_oldLT = "";

	public bool m_hr;

	private IContainer components;

	private PictureBox pictureBox1;

	private clsBackPanel clsBackPanel1;

	public GlassBtn btnCl;

	public GlassBtn btnOK;

	public Label label1;

	public Label label2;

	public Label label3;

	public TextBox txtMsg;

	public Label label4;

	private TableLayoutPanel tableLayoutPanel1;

	public DateTimePicker dtpLevel;

	public DateTimePicker dtpTime;

	private Label label5;

	public TextBox textBox1;

	public frmGCSO()
	{
		InitializeComponent();
	}

	private void nudDay_ValueChanged(object sender, EventArgs e)
	{
	}

	private void dtpLevel_ValueChanged(object sender, EventArgs e)
	{
	}

	private void frmGCSO_Load(object sender, EventArgs e)
	{
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		dtpLevel.CustomFormat = Program.m_currDateFmt;
		if (m_hr)
		{
			label5.Text = (string)Program.m_hPubTab["InfoHour"];
		}
		else
		{
			label5.Text = (string)Program.m_hPubTab["InfoDay"];
		}
	}

	private void nudDay_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
	}

	private void btnCl_Click(object sender, EventArgs e)
	{
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(textBox1.Text);
			dtpLevel.Value = Convert.ToDateTime(m_oldLT).AddDays(num);
		}
		catch (Exception ex)
		{
			Console.Write(ex.Message.ToString());
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmGCSO));
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpLevel = new System.Windows.Forms.DateTimePicker();
		this.dtpTime = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.txtMsg = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.label5 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.tableLayoutPanel1.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.pictureBox1.Image = LockSoftware.Properties.Resources.Ques;
		this.pictureBox1.Location = new System.Drawing.Point(3, 58);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(35, 47);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 0);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label1.Size = new System.Drawing.Size(35, 20);
		this.label1.TabIndex = 2;
		this.label1.Text = "label1";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(3, 27);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label2.Size = new System.Drawing.Size(35, 20);
		this.label2.TabIndex = 4;
		this.label2.Text = "label2";
		this.dtpLevel.CustomFormat = "yyyy-MM-dd";
		this.dtpLevel.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLevel.Location = new System.Drawing.Point(44, 30);
		this.dtpLevel.Name = "dtpLevel";
		this.dtpLevel.Size = new System.Drawing.Size(95, 21);
		this.dtpLevel.TabIndex = 10;
		this.dtpLevel.ValueChanged += new System.EventHandler(dtpLevel_ValueChanged);
		this.dtpTime.CustomFormat = "HH:mm";
		this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpTime.Location = new System.Drawing.Point(145, 30);
		this.dtpTime.Name = "dtpTime";
		this.dtpTime.ShowUpDown = true;
		this.dtpTime.Size = new System.Drawing.Size(56, 21);
		this.dtpTime.TabIndex = 27;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
		this.label3.Location = new System.Drawing.Point(12, 207);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 16);
		this.label3.TabIndex = 29;
		this.label3.Text = "label3";
		this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtMsg.Location = new System.Drawing.Point(12, 13);
		this.txtMsg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.txtMsg.Multiline = true;
		this.txtMsg.Name = "txtMsg";
		this.txtMsg.ReadOnly = true;
		this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
		this.txtMsg.Size = new System.Drawing.Size(239, 190);
		this.txtMsg.TabIndex = 1;
		this.label4.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.label4, 2);
		this.label4.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.Location = new System.Drawing.Point(3, 109);
		this.label4.Name = "label4";
		this.label4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
		this.label4.Size = new System.Drawing.Size(40, 20);
		this.label4.TabIndex = 30;
		this.label4.Text = "label4";
		this.tableLayoutPanel1.ColumnCount = 3;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124f));
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.dtpTime, 2, 1);
		this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
		this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
		this.tableLayoutPanel1.Controls.Add(this.dtpLevel, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 226);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(239, 109);
		this.tableLayoutPanel1.TabIndex = 31;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(145, 0);
		this.label5.Name = "label5";
		this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
		this.label5.Size = new System.Drawing.Size(35, 20);
		this.label5.TabIndex = 31;
		this.label5.Text = "label5";
		this.textBox1.Location = new System.Drawing.Point(44, 3);
		this.textBox1.MaxLength = 3;
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(95, 21);
		this.textBox1.TabIndex = 32;
		this.textBox1.Text = "1";
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.FromArgb(224, 224, 224);
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.btnCl);
		this.clsBackPanel1.Controls.Add(this.btnOK);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 344);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(268, 40);
		this.clsBackPanel1.TabIndex = 28;
		this.btnCl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.btnCl.BackColor = System.Drawing.Color.LightGray;
		this.btnCl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCl.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.close;
		this.btnCl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(164, 6);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(74, 28);
		this.btnCl.TabIndex = 8;
		this.btnCl.Text = "取 消";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.Click += new System.EventHandler(btnCl_Click);
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(50, 6);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(74, 28);
		this.btnOK.TabIndex = 9;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(268, 384);
		base.Controls.Add(this.tableLayoutPanel1);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.txtMsg);
		this.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGCSO";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmGCSO";
		base.Load += new System.EventHandler(frmGCSO_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
