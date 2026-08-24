using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmHotelInfo : Form
{
	private IContainer components;

	private ToolsBtn toolsBtn1;

	private NGlassBtn btnSave;

	private NGlassBtn btnClose;

	private TextBox txtTel;

	private Label label5;

	private TextBox txtAdd;

	private Label label4;

	private TextBox txtHID;

	private Label label3;

	private TextBox txtHW;

	private Label label2;

	private TextBox txtHN;

	private Label label1;

	private TextBox txtFax;

	private TextBox txtPost;

	private Label label7;

	private Label label8;

	private TextBox txtSD;

	private Label label9;

	private DateTimePicker dtpTime;

	private Label label6;

	private clsBackPanel clsBackPanel1;

	private TextBox txtPath;

	private Label label10;

	private CheckBox chkGInfo;

	private GlassBtn btnBrowse;

	private OpenFileDialog opFDlg;

	private Label label11;

	private TextBox txtMaxG;

	private Label label12;

	private Label label13;

	private DateTimePicker dtpTOL;

	private TextBox txtCTL;

	private PictureBox pbTmp;

	private DateTimePicker dtpLD2;

	private Label label15;

	private Label label16;

	private Label label18;

	private Label label17;

	private DateTimePicker dtpCST;

	private Label label19;

	private RadioButton rbtSelf;

	private RadioButton rbtMove;

	private NumericUpDown numUDMinHours;

	private Label label14;

	private Panel pnlTop;

	private Panel pnlContent;

	private Label lblsign;

	private NumericUpDown numUpDownTaxPercent;

	private Label lblTaxPercent;

	private TextBox txtBoxTaxType;

	private Label lblTaxType;

	private PictureBox picBoxLogo;

	public string m_objName = "WFhi";

	public Hashtable m_htab;

	public bool m_def = true;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmHotelInfo));
		this.opFDlg = new System.Windows.Forms.OpenFileDialog();
		this.numUDMinHours = new System.Windows.Forms.NumericUpDown();
		this.label14 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.rbtSelf = new System.Windows.Forms.RadioButton();
		this.rbtMove = new System.Windows.Forms.RadioButton();
		this.dtpLD2 = new System.Windows.Forms.DateTimePicker();
		this.dtpTOL = new System.Windows.Forms.DateTimePicker();
		this.label18 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.dtpCST = new System.Windows.Forms.DateTimePicker();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txtCTL = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.txtMaxG = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.chkGInfo = new System.Windows.Forms.CheckBox();
		this.txtPath = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtSD = new System.Windows.Forms.TextBox();
		this.txtFax = new System.Windows.Forms.TextBox();
		this.dtpTime = new System.Windows.Forms.DateTimePicker();
		this.txtHW = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtTel = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtHN = new System.Windows.Forms.TextBox();
		this.txtHID = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtAdd = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtPost = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.pbTmp = new System.Windows.Forms.PictureBox();
		this.pnlTop = new System.Windows.Forms.Panel();
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnSave = new LockSoftware.Controls.NGlassBtn(this.components);
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.pnlContent = new System.Windows.Forms.Panel();
		this.picBoxLogo = new System.Windows.Forms.PictureBox();
		this.lblsign = new System.Windows.Forms.Label();
		this.numUpDownTaxPercent = new System.Windows.Forms.NumericUpDown();
		this.lblTaxPercent = new System.Windows.Forms.Label();
		this.txtBoxTaxType = new System.Windows.Forms.TextBox();
		this.lblTaxType = new System.Windows.Forms.Label();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnBrowse = new LockSoftware.Controls.GlassBtn(this.components);
		((System.ComponentModel.ISupportInitialize)this.numUDMinHours).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbTmp).BeginInit();
		this.pnlTop.SuspendLayout();
		this.pnlContent.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picBoxLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numUpDownTaxPercent).BeginInit();
		base.SuspendLayout();
		this.opFDlg.FileName = "Choose Image";
		this.opFDlg.Filter = "*.*|*.*|JPG(.jpg)|*.jpg|BMP(.bmp)|*.bmp|PNG(.png)|*.png|GIF(.gif)|*.gif";
		this.numUDMinHours.Location = new System.Drawing.Point(536, 215);
		this.numUDMinHours.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.numUDMinHours.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numUDMinHours.Name = "numUDMinHours";
		this.numUDMinHours.Size = new System.Drawing.Size(60, 21);
		this.numUDMinHours.TabIndex = 70;
		this.numUDMinHours.Value = new decimal(new int[4] { 4, 0, 0, 0 });
		this.label14.BackColor = System.Drawing.Color.Transparent;
		this.label14.Location = new System.Drawing.Point(410, 208);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(120, 28);
		this.label14.TabIndex = 69;
		this.label14.Text = "minimum-hours:";
		this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label19.BackColor = System.Drawing.Color.Transparent;
		this.label19.Location = new System.Drawing.Point(433, 361);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(107, 17);
		this.label19.TabIndex = 68;
		this.label19.Text = "Discount model:";
		this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label19.Visible = false;
		this.rbtSelf.AutoSize = true;
		this.rbtSelf.BackColor = System.Drawing.SystemColors.Window;
		this.rbtSelf.Location = new System.Drawing.Point(435, 359);
		this.rbtSelf.Name = "rbtSelf";
		this.rbtSelf.Size = new System.Drawing.Size(161, 16);
		this.rbtSelf.TabIndex = 67;
		this.rbtSelf.TabStop = true;
		this.rbtSelf.Text = "Paid = Price * Discount";
		this.rbtSelf.UseVisualStyleBackColor = false;
		this.rbtSelf.Visible = false;
		this.rbtMove.AutoSize = true;
		this.rbtMove.BackColor = System.Drawing.SystemColors.Window;
		this.rbtMove.Location = new System.Drawing.Point(435, 379);
		this.rbtMove.Name = "rbtMove";
		this.rbtMove.Size = new System.Drawing.Size(197, 16);
		this.rbtMove.TabIndex = 66;
		this.rbtMove.TabStop = true;
		this.rbtMove.Text = "Paid = Price * (1 - Discount)";
		this.rbtMove.UseVisualStyleBackColor = false;
		this.rbtMove.Visible = false;
		this.dtpLD2.CustomFormat = "HH:mm";
		this.dtpLD2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpLD2.Location = new System.Drawing.Point(536, 130);
		this.dtpLD2.Name = "dtpLD2";
		this.dtpLD2.ShowUpDown = true;
		this.dtpLD2.Size = new System.Drawing.Size(60, 21);
		this.dtpLD2.TabIndex = 61;
		this.dtpTOL.CustomFormat = "HH:mm";
		this.dtpTOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpTOL.Location = new System.Drawing.Point(536, 74);
		this.dtpTOL.Name = "dtpTOL";
		this.dtpTOL.ShowUpDown = true;
		this.dtpTOL.Size = new System.Drawing.Size(60, 21);
		this.dtpTOL.TabIndex = 11;
		this.dtpTOL.ValueChanged += new System.EventHandler(dtpTOL_ValueChanged);
		this.label18.BackColor = System.Drawing.Color.Transparent;
		this.label18.ForeColor = System.Drawing.Color.Red;
		this.label18.Location = new System.Drawing.Point(604, 130);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(187, 50);
		this.label18.TabIndex = 65;
		this.label18.Text = "*PS:Nedd one day room price.";
		this.label17.BackColor = System.Drawing.Color.Transparent;
		this.label17.ForeColor = System.Drawing.Color.Red;
		this.label17.Location = new System.Drawing.Point(602, 74);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(189, 50);
		this.label17.TabIndex = 64;
		this.label17.Text = "*PS:Need a half day price.";
		this.dtpCST.CustomFormat = "HH:mm";
		this.dtpCST.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpCST.Location = new System.Drawing.Point(536, 18);
		this.dtpCST.Name = "dtpCST";
		this.dtpCST.ShowUpDown = true;
		this.dtpCST.Size = new System.Drawing.Size(60, 21);
		this.dtpCST.TabIndex = 63;
		this.dtpCST.ValueChanged += new System.EventHandler(dtpCST_ValueChanged);
		this.label16.BackColor = System.Drawing.Color.Transparent;
		this.label16.Location = new System.Drawing.Point(410, 12);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(120, 28);
		this.label16.TabIndex = 62;
		this.label16.Text = "Coming Time:";
		this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label15.BackColor = System.Drawing.Color.Transparent;
		this.label15.Location = new System.Drawing.Point(410, 124);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(120, 28);
		this.label15.TabIndex = 60;
		this.label15.Text = "Leave Delay 2:";
		this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtCTL.Location = new System.Drawing.Point(536, 184);
		this.txtCTL.Name = "txtCTL";
		this.txtCTL.Size = new System.Drawing.Size(60, 21);
		this.txtCTL.TabIndex = 10;
		this.txtCTL.Text = "10";
		this.txtCTL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCTL_KeyPress);
		this.label13.BackColor = System.Drawing.Color.Transparent;
		this.label13.Location = new System.Drawing.Point(410, 180);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(120, 28);
		this.label13.TabIndex = 57;
		this.label13.Text = "Cleaning Time Limit(Minute):";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Location = new System.Drawing.Point(410, 68);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(120, 28);
		this.label12.TabIndex = 56;
		this.label12.Text = "Leave Delay 1:";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtMaxG.Location = new System.Drawing.Point(536, 243);
		this.txtMaxG.MaxLength = 2;
		this.txtMaxG.Name = "txtMaxG";
		this.txtMaxG.Size = new System.Drawing.Size(60, 21);
		this.txtMaxG.TabIndex = 12;
		this.txtMaxG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMaxG_KeyPress);
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.Location = new System.Drawing.Point(410, 236);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(120, 28);
		this.label11.TabIndex = 53;
		this.label11.Text = "Room Max Guest:";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkGInfo.BackColor = System.Drawing.Color.Transparent;
		this.chkGInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkGInfo.Checked = true;
		this.chkGInfo.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkGInfo.Location = new System.Drawing.Point(412, 326);
		this.chkGInfo.Name = "chkGInfo";
		this.chkGInfo.Size = new System.Drawing.Size(376, 32);
		this.chkGInfo.TabIndex = 13;
		this.chkGInfo.Text = "Check Input Guest Information";
		this.chkGInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkGInfo.UseVisualStyleBackColor = false;
		this.txtPath.BackColor = System.Drawing.Color.White;
		this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtPath.Location = new System.Drawing.Point(125, 215);
		this.txtPath.Name = "txtPath";
		this.txtPath.ReadOnly = true;
		this.txtPath.Size = new System.Drawing.Size(266, 14);
		this.txtPath.TabIndex = 14;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Location = new System.Drawing.Point(12, 208);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(107, 28);
		this.label10.TabIndex = 18;
		this.label10.Text = "Background:";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Location = new System.Drawing.Point(602, 208);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(120, 28);
		this.label8.TabIndex = 14;
		this.label8.Text = "Default Stay Day:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtSD.Location = new System.Drawing.Point(728, 215);
		this.txtSD.MaxLength = 4;
		this.txtSD.Name = "txtSD";
		this.txtSD.Size = new System.Drawing.Size(60, 21);
		this.txtSD.TabIndex = 8;
		this.txtSD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSD_KeyPress);
		this.txtFax.Location = new System.Drawing.Point(125, 102);
		this.txtFax.Name = "txtFax";
		this.txtFax.Size = new System.Drawing.Size(266, 21);
		this.txtFax.TabIndex = 6;
		this.txtFax.Leave += new System.EventHandler(txtFax_Leave);
		this.dtpTime.CustomFormat = "HH:mm";
		this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpTime.Location = new System.Drawing.Point(536, 46);
		this.dtpTime.Name = "dtpTime";
		this.dtpTime.ShowUpDown = true;
		this.dtpTime.Size = new System.Drawing.Size(60, 21);
		this.dtpTime.TabIndex = 9;
		this.dtpTime.ValueChanged += new System.EventHandler(dtpTime_ValueChanged);
		this.txtHW.Location = new System.Drawing.Point(125, 186);
		this.txtHW.Name = "txtHW";
		this.txtHW.Size = new System.Drawing.Size(266, 21);
		this.txtHW.TabIndex = 2;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Location = new System.Drawing.Point(410, 40);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(120, 28);
		this.label9.TabIndex = 16;
		this.label9.Text = "Leave Time:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtTel.Location = new System.Drawing.Point(125, 74);
		this.txtTel.Name = "txtTel";
		this.txtTel.Size = new System.Drawing.Size(266, 21);
		this.txtTel.TabIndex = 4;
		this.txtTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTel_KeyPress);
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(12, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(107, 28);
		this.label1.TabIndex = 0;
		this.label1.Text = "Hotel Name:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Location = new System.Drawing.Point(12, 124);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(107, 28);
		this.label5.TabIndex = 8;
		this.label5.Text = "Address:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Location = new System.Drawing.Point(12, 68);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(107, 28);
		this.label4.TabIndex = 6;
		this.label4.Text = "Book Phone:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtHN.Location = new System.Drawing.Point(125, 18);
		this.txtHN.Name = "txtHN";
		this.txtHN.Size = new System.Drawing.Size(266, 21);
		this.txtHN.TabIndex = 1;
		this.txtHID.Location = new System.Drawing.Point(125, 46);
		this.txtHID.Name = "txtHID";
		this.txtHID.Size = new System.Drawing.Size(266, 21);
		this.txtHID.TabIndex = 3;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Location = new System.Drawing.Point(12, 152);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(107, 28);
		this.label7.TabIndex = 12;
		this.label7.Text = "Post:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtAdd.Location = new System.Drawing.Point(125, 130);
		this.txtAdd.Name = "txtAdd";
		this.txtAdd.Size = new System.Drawing.Size(266, 21);
		this.txtAdd.TabIndex = 5;
		this.txtAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtAdd_KeyPress);
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Location = new System.Drawing.Point(12, 180);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(107, 28);
		this.label2.TabIndex = 2;
		this.label2.Text = "Hotel Web:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtPost.Location = new System.Drawing.Point(125, 158);
		this.txtPost.Name = "txtPost";
		this.txtPost.Size = new System.Drawing.Size(266, 21);
		this.txtPost.TabIndex = 7;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Location = new System.Drawing.Point(12, 40);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(107, 28);
		this.label3.TabIndex = 4;
		this.label3.Text = "Hotel Number:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Location = new System.Drawing.Point(12, 96);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(107, 28);
		this.label6.TabIndex = 10;
		this.label6.Text = "Fax:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.pbTmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pbTmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbTmp.Location = new System.Drawing.Point(12, 236);
		this.pbTmp.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
		this.pbTmp.Name = "pbTmp";
		this.pbTmp.Size = new System.Drawing.Size(240, 180);
		this.pbTmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbTmp.TabIndex = 59;
		this.pbTmp.TabStop = false;
		this.pnlTop.BackColor = System.Drawing.Color.Transparent;
		this.pnlTop.Controls.Add(this.btnClose);
		this.pnlTop.Controls.Add(this.btnSave);
		this.pnlTop.Controls.Add(this.toolsBtn1);
		this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlTop.Location = new System.Drawing.Point(0, 0);
		this.pnlTop.Margin = new System.Windows.Forms.Padding(0);
		this.pnlTop.Name = "pnlTop";
		this.pnlTop.Size = new System.Drawing.Size(794, 79);
		this.pnlTop.TabIndex = 4;
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnClose.ButtonText = "Close";
		this.btnClose.CornerRadius = 4;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Location = new System.Drawing.Point(699, 29);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(83, 35);
		this.btnClose.TabIndex = 3;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSave.BackColor = System.Drawing.Color.Transparent;
		this.btnSave.BaseColor = System.Drawing.Color.FromArgb(0, 192, 192);
		this.btnSave.ButtonColor = System.Drawing.Color.Teal;
		this.btnSave.ButtonText = "Save Info";
		this.btnSave.CornerRadius = 4;
		this.btnSave.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSave.GlowColor = System.Drawing.Color.White;
		this.btnSave.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSave.Image = LockSoftware.Properties.Resources.save;
		this.btnSave.ImageSize = new System.Drawing.Size(18, 18);
		this.btnSave.Location = new System.Drawing.Point(576, 29);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(105, 35);
		this.btnSave.TabIndex = 2;
		this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomRight;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolsBtn1.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources.OS00;
		this.toolsBtn1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.toolsBtn1.ImageNew = null;
		this.toolsBtn1.ImageRedrawed = false;
		this.toolsBtn1.ImageStyle = 0;
		this.toolsBtn1.isButton = false;
		this.toolsBtn1.Location = new System.Drawing.Point(0, 0);
		this.toolsBtn1.MouseDownBorderColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.toolsBtn1.MouseDownEndColor = System.Drawing.Color.Beige;
		this.toolsBtn1.MouseDownStartColor = System.Drawing.Color.White;
		this.toolsBtn1.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.toolsBtn1.MouseEnterEndColor = System.Drawing.Color.Silver;
		this.toolsBtn1.MouseEnterStartColor = System.Drawing.Color.White;
		this.toolsBtn1.Name = "toolsBtn1";
		this.toolsBtn1.Size = new System.Drawing.Size(794, 79);
		this.toolsBtn1.TabIndex = 1;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Hotel Info: Setting your hotel's information.";
		this.toolsBtn1.TextRedrawed = true;
		this.pnlContent.Controls.Add(this.picBoxLogo);
		this.pnlContent.Controls.Add(this.lblsign);
		this.pnlContent.Controls.Add(this.numUpDownTaxPercent);
		this.pnlContent.Controls.Add(this.lblTaxPercent);
		this.pnlContent.Controls.Add(this.txtBoxTaxType);
		this.pnlContent.Controls.Add(this.lblTaxType);
		this.pnlContent.Controls.Add(this.txtHN);
		this.pnlContent.Controls.Add(this.label19);
		this.pnlContent.Controls.Add(this.clsBackPanel1);
		this.pnlContent.Controls.Add(this.rbtSelf);
		this.pnlContent.Controls.Add(this.numUDMinHours);
		this.pnlContent.Controls.Add(this.txtFax);
		this.pnlContent.Controls.Add(this.rbtMove);
		this.pnlContent.Controls.Add(this.dtpTime);
		this.pnlContent.Controls.Add(this.btnBrowse);
		this.pnlContent.Controls.Add(this.chkGInfo);
		this.pnlContent.Controls.Add(this.txtPath);
		this.pnlContent.Controls.Add(this.txtHW);
		this.pnlContent.Controls.Add(this.label10);
		this.pnlContent.Controls.Add(this.label14);
		this.pnlContent.Controls.Add(this.label11);
		this.pnlContent.Controls.Add(this.label9);
		this.pnlContent.Controls.Add(this.txtMaxG);
		this.pnlContent.Controls.Add(this.txtTel);
		this.pnlContent.Controls.Add(this.label12);
		this.pnlContent.Controls.Add(this.label8);
		this.pnlContent.Controls.Add(this.label1);
		this.pnlContent.Controls.Add(this.pbTmp);
		this.pnlContent.Controls.Add(this.label13);
		this.pnlContent.Controls.Add(this.txtSD);
		this.pnlContent.Controls.Add(this.label5);
		this.pnlContent.Controls.Add(this.label6);
		this.pnlContent.Controls.Add(this.txtCTL);
		this.pnlContent.Controls.Add(this.dtpLD2);
		this.pnlContent.Controls.Add(this.label4);
		this.pnlContent.Controls.Add(this.label3);
		this.pnlContent.Controls.Add(this.label15);
		this.pnlContent.Controls.Add(this.dtpTOL);
		this.pnlContent.Controls.Add(this.txtHID);
		this.pnlContent.Controls.Add(this.txtPost);
		this.pnlContent.Controls.Add(this.label16);
		this.pnlContent.Controls.Add(this.label18);
		this.pnlContent.Controls.Add(this.label7);
		this.pnlContent.Controls.Add(this.label2);
		this.pnlContent.Controls.Add(this.dtpCST);
		this.pnlContent.Controls.Add(this.label17);
		this.pnlContent.Controls.Add(this.txtAdd);
		this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlContent.Location = new System.Drawing.Point(0, 79);
		this.pnlContent.Name = "pnlContent";
		this.pnlContent.Size = new System.Drawing.Size(794, 433);
		this.pnlContent.TabIndex = 71;
		this.picBoxLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picBoxLogo.Location = new System.Drawing.Point(258, 283);
		this.picBoxLogo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
		this.picBoxLogo.Name = "picBoxLogo";
		this.picBoxLogo.Size = new System.Drawing.Size(133, 133);
		this.picBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.picBoxLogo.TabIndex = 76;
		this.picBoxLogo.TabStop = false;
		this.picBoxLogo.Click += new System.EventHandler(picBoxLogo_Click);
		this.lblsign.AutoSize = true;
		this.lblsign.Location = new System.Drawing.Point(604, 301);
		this.lblsign.Name = "lblsign";
		this.lblsign.Size = new System.Drawing.Size(11, 12);
		this.lblsign.TabIndex = 75;
		this.lblsign.Text = "%";
		this.numUpDownTaxPercent.DecimalPlaces = 2;
		this.numUpDownTaxPercent.Location = new System.Drawing.Point(536, 299);
		this.numUpDownTaxPercent.Name = "numUpDownTaxPercent";
		this.numUpDownTaxPercent.Size = new System.Drawing.Size(60, 21);
		this.numUpDownTaxPercent.TabIndex = 74;
		this.numUpDownTaxPercent.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.lblTaxPercent.Location = new System.Drawing.Point(410, 292);
		this.lblTaxPercent.Name = "lblTaxPercent";
		this.lblTaxPercent.Size = new System.Drawing.Size(120, 28);
		this.lblTaxPercent.TabIndex = 73;
		this.lblTaxPercent.Text = "Tax Rate:";
		this.lblTaxPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtBoxTaxType.Location = new System.Drawing.Point(536, 271);
		this.txtBoxTaxType.Name = "txtBoxTaxType";
		this.txtBoxTaxType.Size = new System.Drawing.Size(186, 21);
		this.txtBoxTaxType.TabIndex = 72;
		this.lblTaxType.Location = new System.Drawing.Point(410, 264);
		this.lblTaxType.Name = "lblTaxType";
		this.lblTaxType.Size = new System.Drawing.Size(120, 28);
		this.lblTaxType.TabIndex = 71;
		this.lblTaxType.Text = "Type Of Tax:";
		this.lblTaxType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.clsBackPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel1.ColorAngle = 180f;
		this.clsBackPanel1.Location = new System.Drawing.Point(397, 6);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(1, 422);
		this.clsBackPanel1.TabIndex = 17;
		this.btnBrowse.BackColor = System.Drawing.Color.Silver;
		this.btnBrowse.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBrowse.ForeColor = System.Drawing.Color.Black;
		this.btnBrowse.GlowColor = System.Drawing.Color.White;
		this.btnBrowse.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnBrowse.Image = LockSoftware.Properties.Resources.search;
		this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBrowse.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnBrowse.Location = new System.Drawing.Point(258, 236);
		this.btnBrowse.Name = "btnBrowse";
		this.btnBrowse.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnBrowse.Size = new System.Drawing.Size(91, 33);
		this.btnBrowse.TabIndex = 15;
		this.btnBrowse.Text = "Browse";
		this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(794, 512);
		base.Controls.Add(this.pnlContent);
		base.Controls.Add(this.pnlTop);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmHotelInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Hotel Information";
		base.Load += new System.EventHandler(frmHotelInfo_Load);
		((System.ComponentModel.ISupportInitialize)this.numUDMinHours).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbTmp).EndInit();
		this.pnlTop.ResumeLayout(false);
		this.pnlContent.ResumeLayout(false);
		this.pnlContent.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picBoxLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numUpDownTaxPercent).EndInit();
		base.ResumeLayout(false);
	}

	public frmHotelInfo()
	{
		InitializeComponent();
		base.MinimizeBox = (base.MaximizeBox = false);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void frmHotelInfo_Load(object sender, EventArgs e)
	{
		m_htab = Program.GetControlName(this, m_objName);
		if (Program.m_defDiscount == 1)
		{
			rbtSelf.Checked = true;
		}
		else
		{
			rbtMove.Checked = true;
		}
		string sql = "Select top 1 * From D_HotelBasic Order by B_ID desc";
		DataTable dataTable = null;
		try
		{
			dtpCST.MinDate = DateTime.Now.Date;
			dtpCST.MaxDate = DateTime.Now.Date.AddDays(1.0).AddSeconds(-1.0);
			dtpTime.MinDate = DateTime.Now.Date;
			dtpTime.MaxDate = DateTime.Now.Date.AddDays(1.0).AddSeconds(-1.0);
			dtpTOL.MinDate = DateTime.Now.Date;
			dtpTOL.MaxDate = DateTime.Now.Date.AddDays(1.0).AddSeconds(-1.0);
			dtpLD2.MinDate = DateTime.Now.Date;
			dtpLD2.MaxDate = DateTime.Now.Date.AddDays(1.0).AddSeconds(-1.0);
			dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				txtHN.Text = dataTable.Rows[0]["B_HotelName"].ToString().Trim();
				txtHW.Text = dataTable.Rows[0]["B_HotelWeb"].ToString().Trim();
				txtHID.Text = dataTable.Rows[0]["B_HotelID"].ToString().Trim();
				txtTel.Text = dataTable.Rows[0]["B_Address"].ToString().Trim();
				txtAdd.Text = dataTable.Rows[0]["B_BookTel"].ToString().Trim();
				txtFax.Text = dataTable.Rows[0]["B_Fax"].ToString().Trim();
				txtPost.Text = dataTable.Rows[0]["B_Post"].ToString().Trim();
				txtSD.Text = dataTable.Rows[0]["B_StayDay"].ToString().Trim();
				dtpLD2.Text = dataTable.Rows[0]["B_leaveDelay2"].ToString().Trim();
				dtpTOL.Text = dataTable.Rows[0]["B_leaveDelay1"].ToString().Trim();
				dtpTime.Text = dataTable.Rows[0]["B_LeaveTime"].ToString().Trim();
				dtpCST.Text = dataTable.Rows[0]["B_ComingTime"].ToString().Trim();
				numUDMinHours.Value = ((dataTable.Rows[0]["B_CR_LessHour"] == null || dataTable.Rows[0]["B_CR_LessHour"].ToString().Trim().Length == 0) ? 4 : Convert.ToInt32(dataTable.Rows[0]["B_CR_LessHour"]));
				if (dataTable.Rows[0]["B_CleanTime"].ToString().Trim() == "")
				{
					txtCTL.Text = "10";
				}
				else
				{
					txtCTL.Text = dataTable.Rows[0]["B_CleanTime"].ToString().Trim();
				}
				txtMaxG.Text = dataTable.Rows[0]["B_MaxGuest"].ToString().Trim();
				if (dataTable.Rows[0]["B_GInfo"].ToString() == "" || dataTable.Rows[0]["B_GInfo"].ToString() == "NULL")
				{
					chkGInfo.Checked = true;
				}
				else
				{
					chkGInfo.Checked = Convert.ToBoolean(dataTable.Rows[0]["B_GInfo"].ToString());
				}
				if (dataTable.Rows[0]["B_BackImg"].ToString().Trim() != null)
				{
					string text = dataTable.Rows[0]["B_BackImg"].ToString().Trim();
					if (text == "")
					{
						text = Program.m_bgVal;
					}
					else
					{
						byte[] array = (byte[])dataTable.Rows[0]["B_BackImg"];
						text = Convert.ToBase64String(array);
						try
						{
							MemoryStream stream = new MemoryStream(array);
							pbTmp.Image = Image.FromStream(stream);
						}
						catch
						{
						}
						Program.m_bgVal = text;
					}
				}
				m_def = false;
			}
			else
			{
				txtSD.Text = "1";
				dtpTOL.Text = "14:30";
				dtpTime.Text = "12:30";
			}
			numUpDownTaxPercent.Value = Program.TaxPercent;
			txtBoxTaxType.Text = Program.TaxType;
			Program.LoadImg("Reports\\logo.png", picBoxLogo);
		}
		catch (Exception ex)
		{
			if (dataTable != null)
			{
				dataTable.Clear();
				dataTable.Dispose();
			}
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		string text = "";
		if (txtPath.Text.Trim() != "")
		{
			try
			{
				if (pbTmp.Image != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					pbTmp.Image.Save(memoryStream, pbTmp.Image.RawFormat);
					SQLserver.Data_UpdateImg("Update D_HotelBasic Set B_BackImg = @ImgVal", "@ImgVal", memoryStream.GetBuffer());
					Program.fm.BackgroundImage = pbTmp.Image;
				}
			}
			catch (Exception ex)
			{
				Program.MsgBox((string)m_htab["Err03"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		try
		{
			Program.TaxType = txtBoxTaxType.Text;
			Program.TaxPercent = numUpDownTaxPercent.Value;
			Program.SetSingleItem("TaxType", Program.TaxType);
			Program.SetSingleItem("TaxPercent", Program.TaxPercent.ToString());
			if (Program.isValNull(label8.Text.Substring(0, label8.Text.Length - 1), txtSD.Text.Trim(), chk: true) || Program.isValNull(label11.Text.Substring(0, label11.Text.Length - 1), txtMaxG.Text.Trim(), chk: true) || Program.isValNull(label13.Text.Substring(0, label13.Text.Length - 1), txtCTL.Text.Trim(), chk: true))
			{
				return;
			}
			if (Convert.ToInt32(txtSD.Text) == 0)
			{
				MessageBox.Show((string)m_htab["Err04"]);
				return;
			}
			Program.m_defDay = txtSD.Text;
			if (rbtSelf.Checked)
			{
				Program.m_defDiscount = 1;
			}
			else
			{
				Program.m_defDiscount = 0;
			}
			Program.SetSingleItem("Discount", Program.m_defDiscount.ToString());
			string text2 = "";
			if (!m_def)
			{
				text2 = "Update D_HotelBasic Set B_HotelName=N'" + txtHN.Text.Trim() + "', B_HotelWeb=N'" + txtHW.Text.Trim() + "'";
				string text3 = text2;
				text2 = text3 + ",B_HotelID=N'" + txtHID.Text.Trim() + "',B_BookTel=N'" + txtAdd.Text.Trim() + "'";
				string text4 = text2;
				text2 = text4 + ", B_Address=N'" + txtTel.Text.Trim() + "', B_Fax=N'" + txtFax.Text.Trim() + "'";
				string text5 = text2;
				text2 = text5 + ",B_Post=N'" + txtPost.Text.Trim() + "', B_leaveDelay1='" + dtpTOL.Text.Trim() + "'";
				string text6 = text2;
				text2 = text6 + ", B_leaveDelay2='" + dtpLD2.Text.Trim() + "', B_ComingTime='" + dtpCST.Text.Trim() + "'";
				if (text != "")
				{
					text2 = text2 + ", B_BackImg=0x" + text;
				}
				text2 = text2 + ", B_GInfo=" + Convert.ToInt16(chkGInfo.Checked);
				text2 = text2 + ", B_MaxGuest=" + txtMaxG.Text.Trim();
				object obj = text2;
				text2 = string.Concat(obj, ",B_StayDay=", txtSD.Text.Trim(), ", B_Updatetime=GetDate(),B_Updator_ID=", Program.m_opid, ",B_Updator=N'", Program.m_OperName, "'");
				string text7 = text2;
				text2 = text7 + ", B_CleanTime=" + txtCTL.Text.Trim() + ", B_LeaveTime='" + dtpTime.Text.Trim() + "',B_CR_LessHour=" + Program.GetStandDec(numUDMinHours.Value.ToString());
			}
			else
			{
				text2 = "Insert into D_HotelBasic values(N'" + txtHN.Text.Trim() + "',N'" + txtHW.Text.Trim() + "',N'" + txtHID.Text.Trim() + "'";
				string text8 = text2;
				text2 = text8 + ",N'" + txtAdd.Text.Trim() + "',N'" + txtTel.Text.Trim() + "',N'" + txtFax.Text.Trim() + "'";
				string text9 = text2;
				text2 = text9 + ",N'" + txtPost.Text.Trim() + "'," + txtSD.Text.Trim() + ",'" + dtpTime.Text.Trim() + "','" + dtpLD2.Text.Trim() + "','" + dtpCST.Text.Trim() + "'";
				text2 = ((!(text != "")) ? (text2 + ",Null") : (text2 + ",0x" + text));
				object obj2 = text2;
				text2 = string.Concat(obj2, ",", Convert.ToInt16(chkGInfo.Checked).ToString(), ", ", txtMaxG.Text.Trim(), ",", txtCTL.Text.Trim(), ",'", dtpTOL.Text.Trim(), "',", Program.GetStandDec(numUDMinHours.Value.ToString()), ", GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "')");
			}
			if (SQLserver.Data_ExecuteSql(text2) > 0)
			{
				m_def = false;
				Program.m_chkGInfo = chkGInfo.Checked;
				Program.m_defClearTime = Convert.ToInt32(txtCTL.Text.Trim());
				Program.m_defComeTime = dtpCST.Text.Trim();
				Program.m_defLeaveTime = dtpTime.Text.Trim();
				Program.m_defHalfDay = dtpTOL.Text.Trim();
				Program.m_defFullDay = dtpLD2.Text.Trim();
				Program.m_defHR = (int)numUDMinHours.Value;
				Program.m_basMaxGuest = Convert.ToInt32(txtMaxG.Text.Trim());
				Program.fm.Text = txtHN.Text.Trim();
				Program.MsgBox((string)Program.m_hPubTab["InfoDBOper"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex2)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex2.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtSD_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void btnBrowse_Click(object sender, EventArgs e)
	{
		if (opFDlg.ShowDialog() == DialogResult.OK)
		{
			txtPath.Text = opFDlg.FileName.Trim();
			if (txtPath.Text.Trim() != "")
			{
				pbTmp.Image = Image.FromFile(txtPath.Text.Trim());
			}
			else
			{
				pbTmp.Image = null;
			}
		}
	}

	private void txtMaxG_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtCTL_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtFax_Leave(object sender, EventArgs e)
	{
	}

	private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtAdd_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (txtAdd.Text.Length < 13)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '-' || e.KeyChar == '\b')
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}
		else if (e.KeyChar == '\b')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void dtpCST_ValueChanged(object sender, EventArgs e)
	{
		dtpTime.MinDate = dtpCST.Value;
	}

	private void dtpTime_ValueChanged(object sender, EventArgs e)
	{
		dtpTOL.MinDate = dtpTime.Value;
	}

	private void dtpTOL_ValueChanged(object sender, EventArgs e)
	{
		dtpLD2.MinDate = dtpTOL.Value;
	}

	private void picBoxLogo_Click(object sender, EventArgs e)
	{
		try
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.CheckFileExists = true;
			openFileDialog.Filter = "png |*.png";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (picBoxLogo.Image != null)
				{
					picBoxLogo.Image.Dispose();
				}
				Application.DoEvents();
				File.Copy(openFileDialog.FileName, "Reports\\logo.png", overwrite: true);
				Program.LoadImg("Reports\\logo.png", picBoxLogo);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}
}
