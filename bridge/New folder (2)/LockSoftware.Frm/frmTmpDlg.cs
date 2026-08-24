using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ComponentDll;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmTmpDlg : Form
{
	private IContainer components;

	private clsBackPanel clsBackPanel1;

	public TableLayoutPanel tlpCtls;

	private NGlassBtn btnNTxt;

	private NGlassBtn btnDTxt;

	public LockSoftware.Controls.GlassBtn btnOK;

	public LockSoftware.Controls.GlassBtn btnCl;

	public LockSoftware.Controls.GlassBtn btnSkip;

	public string m_objName = "";

	public Hashtable m_htab;

	public int m_type;

	public int m_tmpVal02;

	public ArrayList ctrlList = new ArrayList();

	public bool m_close = true;

	public long m_tmpVal;

	public long m_tmpVal01 = -1L;

	public NumericUpDown nudGC = new NumericUpDown();

	public TextBox[] txtCtrl = new TextBox[100];

	public Label[] lab = new Label[100];

	public ArrayList ctrlcob = new ArrayList();

	public ArrayList ctrlchk = new ArrayList();

	public double m_stayday;

	public double m_userate;

	public DateTime m_TGComeTime = DateTime.Now;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmTmpDlg));
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnSkip = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnDTxt = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnNTxt = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.tlpCtls = new System.Windows.Forms.TableLayoutPanel();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.btnSkip);
		this.clsBackPanel1.Controls.Add(this.btnDTxt);
		this.clsBackPanel1.Controls.Add(this.btnNTxt);
		this.clsBackPanel1.Controls.Add(this.btnCl);
		this.clsBackPanel1.Controls.Add(this.btnOK);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 16);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(303, 42);
		this.clsBackPanel1.TabIndex = 0;
		this.btnSkip.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSkip.BackColor = System.Drawing.Color.LightGray;
		this.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Ignore;
		this.btnSkip.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSkip.ForeColor = System.Drawing.Color.Black;
		this.btnSkip.GlowColor = System.Drawing.Color.White;
		this.btnSkip.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSkip.Image = LockSoftware.Properties.Resources.PanelRight;
		this.btnSkip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSkip.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnSkip.Location = new System.Drawing.Point(65, 7);
		this.btnSkip.Name = "btnSkip";
		this.btnSkip.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnSkip.Size = new System.Drawing.Size(74, 28);
		this.btnSkip.TabIndex = 43;
		this.btnSkip.Text = "跳 过";
		this.btnSkip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSkip.Visible = false;
		this.btnSkip.Click += new System.EventHandler(btnSkip_Click);
		this.btnDTxt.BackColor = System.Drawing.Color.Transparent;
		this.btnDTxt.BaseColor = System.Drawing.Color.White;
		this.btnDTxt.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnDTxt.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnDTxt.ButtonText = null;
		this.btnDTxt.CornerRadius = 2;
		this.btnDTxt.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDTxt.Image = LockSoftware.Properties.Resources.delete;
		this.btnDTxt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnDTxt.ImageSize = new System.Drawing.Size(16, 16);
		this.btnDTxt.Location = new System.Drawing.Point(36, 9);
		this.btnDTxt.Name = "btnDTxt";
		this.btnDTxt.Size = new System.Drawing.Size(24, 24);
		this.btnDTxt.TabIndex = 42;
		this.btnDTxt.Visible = false;
		this.btnNTxt.BackColor = System.Drawing.Color.Transparent;
		this.btnNTxt.BaseColor = System.Drawing.Color.White;
		this.btnNTxt.ButtonColor = System.Drawing.Color.Gainsboro;
		this.btnNTxt.ButtonStyle = ComponentDll.GlassBtn_New.Style.Flat;
		this.btnNTxt.ButtonText = null;
		this.btnNTxt.CornerRadius = 2;
		this.btnNTxt.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnNTxt.Image = LockSoftware.Properties.Resources.Add;
		this.btnNTxt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnNTxt.ImageSize = new System.Drawing.Size(16, 16);
		this.btnNTxt.Location = new System.Drawing.Point(6, 9);
		this.btnNTxt.Name = "btnNTxt";
		this.btnNTxt.Size = new System.Drawing.Size(24, 24);
		this.btnNTxt.TabIndex = 40;
		this.btnNTxt.Visible = false;
		this.btnNTxt.Click += new System.EventHandler(btnNTxt_Click);
		this.btnCl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnCl.BackColor = System.Drawing.Color.LightGray;
		this.btnCl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCl.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCl.ForeColor = System.Drawing.Color.Black;
		this.btnCl.GlowColor = System.Drawing.Color.White;
		this.btnCl.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCl.Image = LockSoftware.Properties.Resources.close;
		this.btnCl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCl.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCl.Location = new System.Drawing.Point(225, 7);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(74, 28);
		this.btnCl.TabIndex = 6;
		this.btnCl.Text = "取 消";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCl.Click += new System.EventHandler(btnCl_Click);
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnOK.BackColor = System.Drawing.Color.LightGray;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(145, 7);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(74, 28);
		this.btnOK.TabIndex = 7;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tlpCtls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tlpCtls.BackColor = System.Drawing.SystemColors.Control;
		this.tlpCtls.ColumnCount = 2;
		this.tlpCtls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120f));
		this.tlpCtls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 306f));
		this.tlpCtls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tlpCtls.Location = new System.Drawing.Point(5, 3);
		this.tlpCtls.Name = "tlpCtls";
		this.tlpCtls.Padding = new System.Windows.Forms.Padding(3);
		this.tlpCtls.RowCount = 8;
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 167f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tlpCtls.Size = new System.Drawing.Size(293, 10);
		this.tlpCtls.TabIndex = 1;
		base.AcceptButton = this.btnOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCl;
		base.ClientSize = new System.Drawing.Size(303, 58);
		base.Controls.Add(this.tlpCtls);
		base.Controls.Add(this.clsBackPanel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(800, 600);
		base.MinimizeBox = false;
		base.Name = "frmTmpDlg";
		this.Text = "frmEdit";
		base.Load += new System.EventHandler(frmEdit_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmTmpDlg_FormClosing);
		this.clsBackPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public frmTmpDlg()
	{
		InitializeComponent();
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void frmEdit_Load(object sender, EventArgs e)
	{
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		btnSkip.Text = (string)Program.m_hPubTab["btnSkip"];
		if (m_htab == null)
		{
			m_htab = Program.GetControlName(this, m_objName);
		}
		else
		{
			Program.InitGUI(this, m_htab);
		}
		if (m_type == 1)
		{
			NGlassBtn nGlassBtn = btnNTxt;
			bool visible = (btnDTxt.Visible = true);
			nGlassBtn.Visible = visible;
		}
		if (base.Height >= Screen.PrimaryScreen.Bounds.Height)
		{
			base.Height = Screen.PrimaryScreen.Bounds.Height - 50;
		}
	}

	private void btnNTxt_Click(object sender, EventArgs e)
	{
		if (m_type == 1)
		{
			int rowCount = tlpCtls.RowCount;
			rowCount++;
			tlpCtls.RowCount = rowCount;
		}
	}

	private void frmTmpDlg_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!m_close)
		{
			e.Cancel = true;
		}
	}

	private void btnCl_Click(object sender, EventArgs e)
	{
		m_close = true;
	}

	private void btnSkip_Click(object sender, EventArgs e)
	{
		m_close = true;
	}
}
