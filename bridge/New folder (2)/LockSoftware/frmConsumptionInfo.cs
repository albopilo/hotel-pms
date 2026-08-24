using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware;

public class frmConsumptionInfo : Form
{
	public string m_objName = "WFconi";

	public Hashtable m_htab;

	public double rate = 2.0;

	public double dscount = 1.0;

	public bool ischeckcur = true;

	public double totalpay;

	public double depositRemain;

	public double paidextra;

	public double change;

	public string curcode = "￡";

	public string basecode = "￥";

	public bool cheCanUse = true;

	private int memoleft;

	private IContainer components;

	private GlassBtn btnOK;

	private GlassBtn btnClose;

	private clsBackPanel clsBackPanelMain;

	private Label lab1;

	private Label label4;

	private Label labPayextrabase;

	private TextBox texBPayExtracur;

	private Label lab3;

	private Label lab013;

	private Label lab003;

	private Label labdscount;

	private Label lab0;

	private CheckBox cheBCheckCur;

	private Label labchangecur;

	private Label labRemaincur;

	private Label lab2;

	private Label lab4;

	private Label labtotalcur;

	private Label lab014;

	private Label lab004;

	private Label label24;

	private Label labchangebase;

	private Label lab011;

	private Label lab001;

	private Label label20;

	private Label labtotalbase;

	private Label lab012;

	private Label lab002;

	private Label label16;

	private Label labRemainbase;

	public frmConsumptionInfo()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		Text = (string)m_htab["title"];
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnClose.Text = (string)Program.m_hPubTab["btnCl"];
		lab0.Text = (string)m_htab["lab0"];
		lab1.Text = (string)m_htab["lab1"];
		lab2.Text = (string)m_htab["lab2"];
		lab3.Text = (string)m_htab["lab3"];
		cheBCheckCur.Text = (string)m_htab["cheBCheckCur"];
	}

	private void frmConsumptionInfo_Load(object sender, EventArgs e)
	{
		totalpay *= ((dscount > 1.0 || dscount <= 0.0) ? 1.0 : dscount);
		labdscount.Text = Program.GetFaceDisValue(dscount) + "%";
		Label label = lab001;
		Label label2 = lab002;
		Label label3 = lab003;
		string text = (lab004.Text = curcode);
		string text3 = (label3.Text = text);
		string text5 = (label2.Text = text3);
		label.Text = text5;
		Label label4 = lab011;
		Label label5 = lab012;
		Label label6 = lab013;
		string text7 = (lab014.Text = basecode);
		string text9 = (label6.Text = text7);
		string text11 = (label5.Text = text9);
		label4.Text = text11;
		labtotalbase.Text = totalpay.ToString("F2");
		labRemaincur.Text = Program.changeValue(0.0, CultureInfo.CurrentCulture);
		texBPayExtracur.Text = Program.changeValue(0.0, CultureInfo.CurrentCulture);
		cheBCheckCur.Checked = ischeckcur;
		cheBCheckCur.Enabled = cheCanUse;
	}

	private void cheBCheckCur_CheckedChanged(object sender, EventArgs e)
	{
		ischeckcur = cheBCheckCur.Checked;
		if (cheBCheckCur.Checked)
		{
			lab4.Text = (string)m_htab["lab4"];
			labRemaincur.Text = Program.changeValue(0.0, CultureInfo.CurrentCulture);
		}
		else
		{
			lab4.Text = (string)m_htab["lab4_1"];
			labRemaincur.Text = depositRemain.ToString("F2");
		}
	}

	private void labtotalbase_TextChanged(object sender, EventArgs e)
	{
		labtotalcur.Text = (Convert.ToDouble(labtotalbase.Text) / rate).ToString("F2");
	}

	private void texBPayExtracur_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	private void texBPayExtracur_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (texBPayExtracur.Text.Trim().Length != 0)
			{
				paidextra = Convert.ToDouble(texBPayExtracur.Text);
				labPayextrabase.Text = (paidextra * rate).ToString("F2");
				change = Convert.ToDouble(labRemainbase.Text) + Convert.ToDouble(labPayextrabase.Text) - Convert.ToDouble(labtotalbase.Text);
				labchangebase.Text = change.ToString("F2");
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void labRemaincur_TextChanged(object sender, EventArgs e)
	{
		labRemainbase.Text = (Convert.ToDouble(labRemaincur.Text) * rate).ToString("F2");
		texBPayExtracur_TextChanged(null, null);
	}

	private void labchangebase_TextChanged(object sender, EventArgs e)
	{
		labchangecur.Text = (Convert.ToDouble(labchangebase.Text) / rate).ToString("F2");
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (double.Parse(labchangebase.Text) < 0.0)
		{
			Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Exclamation);
		}
		else
		{
			base.DialogResult = DialogResult.OK;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private void cheBCheckCur_SizeChanged(object sender, EventArgs e)
	{
		try
		{
			labdscount.Left = cheBCheckCur.Left - labdscount.Width - 10;
			lab0_SizeChanged(null, null);
		}
		catch
		{
		}
	}

	private void lab0_SizeChanged(object sender, EventArgs e)
	{
		try
		{
			lab0.Left = labdscount.Left - lab0.Width - 5;
		}
		catch
		{
		}
	}

	private void lab2_SizeChanged(object sender, EventArgs e)
	{
		try
		{
			lab2.Left = lab002.Left - lab2.Width - 4;
		}
		catch
		{
		}
	}

	private void lab4_SizeChanged(object sender, EventArgs e)
	{
		try
		{
			lab4.Left = lab004.Left - lab4.Width - 4;
		}
		catch
		{
		}
	}

	private void lab3_SizeChanged(object sender, EventArgs e)
	{
		try
		{
			lab3.Left = lab003.Left - lab3.Width - 4;
		}
		catch
		{
		}
	}

	private void lab1_SizeChanged(object sender, EventArgs e)
	{
		try
		{
			lab1.Left = lab001.Left - lab1.Width - 4;
		}
		catch
		{
		}
	}

	private void lab2_MouseEnter(object sender, EventArgs e)
	{
		memoleft = lab2.Left;
		if (lab2.Left < 5)
		{
			lab2.Left = 5;
		}
	}

	private void lab2_MouseLeave(object sender, EventArgs e)
	{
		lab2.Left = memoleft;
	}

	private void lab4_MouseEnter(object sender, EventArgs e)
	{
		memoleft = lab4.Left;
		if (lab4.Left < 5)
		{
			lab4.Left = 5;
		}
	}

	private void lab4_MouseLeave(object sender, EventArgs e)
	{
		lab4.Left = memoleft;
	}

	private void btnOK_SizeChanged(object sender, EventArgs e)
	{
		btnOK.Left = clsBackPanelMain.Width / 2 - btnOK.Width - 10;
		if (btnOK.Left < 0)
		{
			btnOK.Left = 0;
		}
	}

	private void btnClose_SizeChanged(object sender, EventArgs e)
	{
		btnClose.Left = clsBackPanelMain.Width / 2 + 10;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.frmConsumptionInfo));
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.clsBackPanelMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.lab014 = new System.Windows.Forms.Label();
		this.lab004 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.labchangebase = new System.Windows.Forms.Label();
		this.lab011 = new System.Windows.Forms.Label();
		this.lab001 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.labtotalbase = new System.Windows.Forms.Label();
		this.lab012 = new System.Windows.Forms.Label();
		this.lab002 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.labRemainbase = new System.Windows.Forms.Label();
		this.labchangecur = new System.Windows.Forms.Label();
		this.labRemaincur = new System.Windows.Forms.Label();
		this.lab2 = new System.Windows.Forms.Label();
		this.lab4 = new System.Windows.Forms.Label();
		this.labtotalcur = new System.Windows.Forms.Label();
		this.labdscount = new System.Windows.Forms.Label();
		this.lab0 = new System.Windows.Forms.Label();
		this.cheBCheckCur = new System.Windows.Forms.CheckBox();
		this.lab013 = new System.Windows.Forms.Label();
		this.lab003 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.labPayextrabase = new System.Windows.Forms.Label();
		this.texBPayExtracur = new System.Windows.Forms.TextBox();
		this.lab3 = new System.Windows.Forms.Label();
		this.lab1 = new System.Windows.Forms.Label();
		this.clsBackPanelMain.SuspendLayout();
		base.SuspendLayout();
		this.btnOK.AutoSize = true;
		this.btnOK.BackColor = System.Drawing.Color.Gainsboro;
		this.btnOK.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOK.ForeColor = System.Drawing.Color.Black;
		this.btnOK.GlowColor = System.Drawing.Color.White;
		this.btnOK.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnOK.Image = LockSoftware.Properties.Resources.ok;
		this.btnOK.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnOK.Location = new System.Drawing.Point(65, 188);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnOK.Size = new System.Drawing.Size(86, 30);
		this.btnOK.TabIndex = 10;
		this.btnOK.Text = "OK";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnOK.SizeChanged += new System.EventHandler(btnOK_SizeChanged);
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnClose.AutoSize = true;
		this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
		this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.ForeColor = System.Drawing.Color.Black;
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Image = LockSoftware.Properties.Resources.close;
		this.btnClose.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnClose.Location = new System.Drawing.Point(222, 188);
		this.btnClose.Name = "btnClose";
		this.btnClose.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnClose.Size = new System.Drawing.Size(86, 30);
		this.btnClose.TabIndex = 9;
		this.btnClose.Text = "Close";
		this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnClose.SizeChanged += new System.EventHandler(btnClose_SizeChanged);
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.clsBackPanelMain.Border = true;
		this.clsBackPanelMain.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelMain.BorderBW = 0;
		this.clsBackPanelMain.BorderColorBottom = System.Drawing.Color.YellowGreen;
		this.clsBackPanelMain.BorderColorLeft = System.Drawing.Color.YellowGreen;
		this.clsBackPanelMain.BorderColorRight = System.Drawing.Color.YellowGreen;
		this.clsBackPanelMain.BorderColorTop = System.Drawing.Color.YellowGreen;
		this.clsBackPanelMain.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelMain.BorderLW = 1;
		this.clsBackPanelMain.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelMain.BorderRW = 1;
		this.clsBackPanelMain.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanelMain.BorderTW = 1;
		this.clsBackPanelMain.Color1 = System.Drawing.Color.White;
		this.clsBackPanelMain.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanelMain.ColorAngle = 90f;
		this.clsBackPanelMain.Controls.Add(this.lab014);
		this.clsBackPanelMain.Controls.Add(this.lab004);
		this.clsBackPanelMain.Controls.Add(this.label24);
		this.clsBackPanelMain.Controls.Add(this.labchangebase);
		this.clsBackPanelMain.Controls.Add(this.lab011);
		this.clsBackPanelMain.Controls.Add(this.lab001);
		this.clsBackPanelMain.Controls.Add(this.label20);
		this.clsBackPanelMain.Controls.Add(this.labtotalbase);
		this.clsBackPanelMain.Controls.Add(this.lab012);
		this.clsBackPanelMain.Controls.Add(this.lab002);
		this.clsBackPanelMain.Controls.Add(this.label16);
		this.clsBackPanelMain.Controls.Add(this.labRemainbase);
		this.clsBackPanelMain.Controls.Add(this.labchangecur);
		this.clsBackPanelMain.Controls.Add(this.labRemaincur);
		this.clsBackPanelMain.Controls.Add(this.lab2);
		this.clsBackPanelMain.Controls.Add(this.lab4);
		this.clsBackPanelMain.Controls.Add(this.labtotalcur);
		this.clsBackPanelMain.Controls.Add(this.labdscount);
		this.clsBackPanelMain.Controls.Add(this.lab0);
		this.clsBackPanelMain.Controls.Add(this.cheBCheckCur);
		this.clsBackPanelMain.Controls.Add(this.lab013);
		this.clsBackPanelMain.Controls.Add(this.lab003);
		this.clsBackPanelMain.Controls.Add(this.label4);
		this.clsBackPanelMain.Controls.Add(this.labPayextrabase);
		this.clsBackPanelMain.Controls.Add(this.texBPayExtracur);
		this.clsBackPanelMain.Controls.Add(this.lab3);
		this.clsBackPanelMain.Controls.Add(this.lab1);
		this.clsBackPanelMain.Controls.Add(this.btnClose);
		this.clsBackPanelMain.Controls.Add(this.btnOK);
		this.clsBackPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.clsBackPanelMain.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanelMain.Name = "clsBackPanelMain";
		this.clsBackPanelMain.Size = new System.Drawing.Size(374, 236);
		this.clsBackPanelMain.TabIndex = 11;
		this.lab014.AutoSize = true;
		this.lab014.BackColor = System.Drawing.Color.Transparent;
		this.lab014.Location = new System.Drawing.Point(243, 151);
		this.lab014.Name = "lab014";
		this.lab014.Size = new System.Drawing.Size(17, 12);
		this.lab014.TabIndex = 38;
		this.lab014.Text = "￥";
		this.lab004.AutoSize = true;
		this.lab004.BackColor = System.Drawing.Color.Transparent;
		this.lab004.Location = new System.Drawing.Point(107, 151);
		this.lab004.Name = "lab004";
		this.lab004.Size = new System.Drawing.Size(17, 12);
		this.lab004.TabIndex = 37;
		this.lab004.Text = "￥";
		this.label24.AutoSize = true;
		this.label24.BackColor = System.Drawing.Color.Transparent;
		this.label24.Location = new System.Drawing.Point(220, 151);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(17, 12);
		this.label24.TabIndex = 36;
		this.label24.Text = "<-";
		this.labchangebase.BackColor = System.Drawing.Color.Transparent;
		this.labchangebase.Location = new System.Drawing.Point(266, 151);
		this.labchangebase.Name = "labchangebase";
		this.labchangebase.Size = new System.Drawing.Size(76, 12);
		this.labchangebase.TabIndex = 35;
		this.labchangebase.Text = "100";
		this.labchangebase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labchangebase.TextChanged += new System.EventHandler(labchangebase_TextChanged);
		this.lab011.AutoSize = true;
		this.lab011.BackColor = System.Drawing.Color.Transparent;
		this.lab011.Location = new System.Drawing.Point(243, 67);
		this.lab011.Name = "lab011";
		this.lab011.Size = new System.Drawing.Size(17, 12);
		this.lab011.TabIndex = 34;
		this.lab011.Text = "￥";
		this.lab001.AutoSize = true;
		this.lab001.BackColor = System.Drawing.Color.Transparent;
		this.lab001.Location = new System.Drawing.Point(107, 67);
		this.lab001.Name = "lab001";
		this.lab001.Size = new System.Drawing.Size(17, 12);
		this.lab001.TabIndex = 33;
		this.lab001.Text = "￥";
		this.label20.AutoSize = true;
		this.label20.BackColor = System.Drawing.Color.Transparent;
		this.label20.Location = new System.Drawing.Point(220, 67);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(17, 12);
		this.label20.TabIndex = 32;
		this.label20.Text = "<-";
		this.labtotalbase.BackColor = System.Drawing.Color.Transparent;
		this.labtotalbase.Location = new System.Drawing.Point(266, 67);
		this.labtotalbase.Name = "labtotalbase";
		this.labtotalbase.Size = new System.Drawing.Size(76, 12);
		this.labtotalbase.TabIndex = 31;
		this.labtotalbase.Text = "100";
		this.labtotalbase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labtotalbase.TextChanged += new System.EventHandler(labtotalbase_TextChanged);
		this.lab012.AutoSize = true;
		this.lab012.BackColor = System.Drawing.Color.Transparent;
		this.lab012.Location = new System.Drawing.Point(243, 95);
		this.lab012.Name = "lab012";
		this.lab012.Size = new System.Drawing.Size(17, 12);
		this.lab012.TabIndex = 30;
		this.lab012.Text = "￥";
		this.lab002.AutoSize = true;
		this.lab002.BackColor = System.Drawing.Color.Transparent;
		this.lab002.Location = new System.Drawing.Point(107, 95);
		this.lab002.Name = "lab002";
		this.lab002.Size = new System.Drawing.Size(17, 12);
		this.lab002.TabIndex = 29;
		this.lab002.Text = "￥";
		this.label16.AutoSize = true;
		this.label16.BackColor = System.Drawing.Color.Transparent;
		this.label16.Location = new System.Drawing.Point(220, 95);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(17, 12);
		this.label16.TabIndex = 28;
		this.label16.Text = "->";
		this.labRemainbase.BackColor = System.Drawing.Color.Transparent;
		this.labRemainbase.Location = new System.Drawing.Point(266, 95);
		this.labRemainbase.Name = "labRemainbase";
		this.labRemainbase.Size = new System.Drawing.Size(76, 12);
		this.labRemainbase.TabIndex = 27;
		this.labRemainbase.Text = "100";
		this.labRemainbase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labchangecur.BackColor = System.Drawing.Color.Transparent;
		this.labchangecur.Location = new System.Drawing.Point(132, 151);
		this.labchangecur.Name = "labchangecur";
		this.labchangecur.Size = new System.Drawing.Size(76, 12);
		this.labchangecur.TabIndex = 26;
		this.labchangecur.Text = "10";
		this.labchangecur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labRemaincur.BackColor = System.Drawing.Color.Transparent;
		this.labRemaincur.Location = new System.Drawing.Point(132, 95);
		this.labRemaincur.Name = "labRemaincur";
		this.labRemaincur.Size = new System.Drawing.Size(76, 12);
		this.labRemaincur.TabIndex = 25;
		this.labRemaincur.Text = "100";
		this.labRemaincur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labRemaincur.TextChanged += new System.EventHandler(labRemaincur_TextChanged);
		this.lab2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lab2.AutoSize = true;
		this.lab2.BackColor = System.Drawing.Color.Transparent;
		this.lab2.Location = new System.Drawing.Point(30, 95);
		this.lab2.Name = "lab2";
		this.lab2.Size = new System.Drawing.Size(65, 12);
		this.lab2.TabIndex = 24;
		this.lab2.Text = "押金可用：";
		this.lab2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lab2.SizeChanged += new System.EventHandler(lab2_SizeChanged);
		this.lab2.MouseEnter += new System.EventHandler(lab2_MouseEnter);
		this.lab2.MouseLeave += new System.EventHandler(lab2_MouseLeave);
		this.lab4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lab4.AutoSize = true;
		this.lab4.BackColor = System.Drawing.Color.Transparent;
		this.lab4.Location = new System.Drawing.Point(30, 151);
		this.lab4.Name = "lab4";
		this.lab4.Size = new System.Drawing.Size(65, 12);
		this.lab4.TabIndex = 23;
		this.lab4.Text = "找零金额：";
		this.lab4.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lab4.SizeChanged += new System.EventHandler(lab4_SizeChanged);
		this.lab4.MouseEnter += new System.EventHandler(lab4_MouseEnter);
		this.lab4.MouseLeave += new System.EventHandler(lab4_MouseLeave);
		this.labtotalcur.BackColor = System.Drawing.Color.Transparent;
		this.labtotalcur.Location = new System.Drawing.Point(132, 67);
		this.labtotalcur.Name = "labtotalcur";
		this.labtotalcur.Size = new System.Drawing.Size(76, 12);
		this.labtotalcur.TabIndex = 22;
		this.labtotalcur.Text = "50";
		this.labtotalcur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labdscount.AutoSize = true;
		this.labdscount.BackColor = System.Drawing.Color.Transparent;
		this.labdscount.Location = new System.Drawing.Point(253, 29);
		this.labdscount.Name = "labdscount";
		this.labdscount.Size = new System.Drawing.Size(29, 12);
		this.labdscount.TabIndex = 21;
		this.labdscount.Text = "100%";
		this.lab0.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lab0.AutoSize = true;
		this.lab0.BackColor = System.Drawing.Color.Transparent;
		this.lab0.Location = new System.Drawing.Point(174, 29);
		this.lab0.Name = "lab0";
		this.lab0.Size = new System.Drawing.Size(77, 12);
		this.lab0.TabIndex = 20;
		this.lab0.Text = "优惠百分比：";
		this.lab0.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lab0.SizeChanged += new System.EventHandler(lab0_SizeChanged);
		this.cheBCheckCur.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cheBCheckCur.AutoSize = true;
		this.cheBCheckCur.BackColor = System.Drawing.Color.Transparent;
		this.cheBCheckCur.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cheBCheckCur.Checked = true;
		this.cheBCheckCur.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cheBCheckCur.Location = new System.Drawing.Point(280, 28);
		this.cheBCheckCur.Name = "cheBCheckCur";
		this.cheBCheckCur.Size = new System.Drawing.Size(48, 16);
		this.cheBCheckCur.TabIndex = 19;
		this.cheBCheckCur.Text = "现结";
		this.cheBCheckCur.UseVisualStyleBackColor = false;
		this.cheBCheckCur.CheckedChanged += new System.EventHandler(cheBCheckCur_CheckedChanged);
		this.cheBCheckCur.SizeChanged += new System.EventHandler(cheBCheckCur_SizeChanged);
		this.lab013.AutoSize = true;
		this.lab013.BackColor = System.Drawing.Color.Transparent;
		this.lab013.Location = new System.Drawing.Point(243, 123);
		this.lab013.Name = "lab013";
		this.lab013.Size = new System.Drawing.Size(17, 12);
		this.lab013.TabIndex = 18;
		this.lab013.Text = "￥";
		this.lab003.AutoSize = true;
		this.lab003.BackColor = System.Drawing.Color.Transparent;
		this.lab003.Location = new System.Drawing.Point(107, 123);
		this.lab003.Name = "lab003";
		this.lab003.Size = new System.Drawing.Size(17, 12);
		this.lab003.TabIndex = 17;
		this.lab003.Text = "￥";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(220, 123);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(17, 12);
		this.label4.TabIndex = 16;
		this.label4.Text = "->";
		this.labPayextrabase.BackColor = System.Drawing.Color.Transparent;
		this.labPayextrabase.Location = new System.Drawing.Point(266, 123);
		this.labPayextrabase.Name = "labPayextrabase";
		this.labPayextrabase.Size = new System.Drawing.Size(76, 12);
		this.labPayextrabase.TabIndex = 15;
		this.labPayextrabase.Text = "100";
		this.labPayextrabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.texBPayExtracur.Location = new System.Drawing.Point(132, 120);
		this.texBPayExtracur.Name = "texBPayExtracur";
		this.texBPayExtracur.Size = new System.Drawing.Size(76, 21);
		this.texBPayExtracur.TabIndex = 14;
		this.texBPayExtracur.Text = "100";
		this.texBPayExtracur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.texBPayExtracur.TextChanged += new System.EventHandler(texBPayExtracur_TextChanged);
		this.texBPayExtracur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(texBPayExtracur_KeyPress);
		this.lab3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lab3.AutoSize = true;
		this.lab3.BackColor = System.Drawing.Color.Transparent;
		this.lab3.Location = new System.Drawing.Point(30, 123);
		this.lab3.Name = "lab3";
		this.lab3.Size = new System.Drawing.Size(65, 12);
		this.lab3.TabIndex = 13;
		this.lab3.Text = "加付金额：";
		this.lab3.SizeChanged += new System.EventHandler(lab3_SizeChanged);
		this.lab1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lab1.AutoSize = true;
		this.lab1.BackColor = System.Drawing.Color.Transparent;
		this.lab1.Location = new System.Drawing.Point(30, 67);
		this.lab1.Name = "lab1";
		this.lab1.Size = new System.Drawing.Size(65, 12);
		this.lab1.TabIndex = 11;
		this.lab1.Text = "消费金额：";
		this.lab1.SizeChanged += new System.EventHandler(lab1_SizeChanged);
		base.AcceptButton = this.btnOK;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.CancelButton = this.btnClose;
		base.ClientSize = new System.Drawing.Size(374, 236);
		base.ControlBox = false;
		base.Controls.Add(this.clsBackPanelMain);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		this.MaximumSize = new System.Drawing.Size(390, 275);
		this.MinimumSize = new System.Drawing.Size(390, 275);
		base.Name = "frmConsumptionInfo";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "消费信息";
		base.Load += new System.EventHandler(frmConsumptionInfo_Load);
		this.clsBackPanelMain.ResumeLayout(false);
		this.clsBackPanelMain.PerformLayout();
		base.ResumeLayout(false);
	}
}
