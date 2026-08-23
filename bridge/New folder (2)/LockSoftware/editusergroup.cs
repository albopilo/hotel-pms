using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware;

public class editusergroup : Form
{
	private IContainer components;

	private Label label1;

	private TextBox textBox1;

	private GlassBtn btnClose;

	private GlassBtn btnOK;

	public string m_objName = "WFuge";

	public Hashtable m_htab;

	private string groupname;

	public string groupname1;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.editusergroup));
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		base.SuspendLayout();
		this.label1.Location = new System.Drawing.Point(12, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(101, 34);
		this.label1.TabIndex = 0;
		this.label1.Text = "User Group Name:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.textBox1.Location = new System.Drawing.Point(119, 21);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(156, 21);
		this.textBox1.TabIndex = 1;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.LightGray;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(206, 63);
		this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.Size = new System.Drawing.Size(69, 32);
		this.btnClose.TabIndex = 17;
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
		this.btnOK.Location = new System.Drawing.Point(119, 63);
		this.btnOK.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(71, 32);
		this.btnOK.TabIndex = 16;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(308, 109);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "editusergroup";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "编辑用户组";
		base.Load += new System.EventHandler(editusergroup_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public editusergroup(string groupname)
	{
		this.groupname = groupname;
		InitializeComponent();
	}

	private void editusergroup_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnClose.Text = (string)Program.m_hPubTab["btnCl"];
		textBox1.Text = groupname;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		groupname1 = textBox1.Text.Trim();
		base.DialogResult = DialogResult.OK;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}
}
