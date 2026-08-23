using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmBuildFloor : Form
{
	private IContainer components;

	private ToolsBtn toolsBtn1;

	private clsBackPanel clsBackPanel1;

	private TreeView tvList;

	private Label label4;

	private TextBox txtBCode;

	private TextBox txtBName;

	private Label label3;

	private Label label2;

	private GroupBox grpFloor;

	private TextBox txtFMemo;

	private Label label1;

	private Label label5;

	private Label label6;

	private TextBox txtFCode;

	private TextBox txtFName;

	private GroupBox grpBuild;

	private TextBox txtBMemo;

	private NGlassBtn btnClose;

	private clsBackPanel clsBackPanel2;

	private CheckBox chkSDis;

	private ToolsBtn btnRef;

	private NGlassBtn btnDisB;

	private NGlassBtn btnDisF;

	private ImageList imgListTV;

	private GlassBtn btnBNew;

	private GlassBtn btnFEdit;

	private GlassBtn btnFNew;

	private GlassBtn btnBEdit;

	private FlowLayoutPanel flowLayoutPanel1;

	private GlassBtn btnRT;

	public string m_objName = "WFbf";

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmBuildFloor));
		this.imgListTV = new System.Windows.Forms.ImageList(this.components);
		this.btnDisF = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnDisB = new LockSoftware.Controls.NGlassBtn(this.components);
		this.btnClose = new LockSoftware.Controls.NGlassBtn(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.chkSDis = new System.Windows.Forms.CheckBox();
		this.btnRT = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnRef = new LockSoftware.Controls.ToolsBtn(this.components);
		this.tvList = new System.Windows.Forms.TreeView();
		this.grpFloor = new System.Windows.Forms.GroupBox();
		this.btnFNew = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnFEdit = new LockSoftware.Controls.GlassBtn(this.components);
		this.txtFMemo = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtFCode = new System.Windows.Forms.TextBox();
		this.txtFName = new System.Windows.Forms.TextBox();
		this.grpBuild = new System.Windows.Forms.GroupBox();
		this.btnBEdit = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnBNew = new LockSoftware.Controls.GlassBtn(this.components);
		this.txtBMemo = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtBCode = new System.Windows.Forms.TextBox();
		this.txtBName = new System.Windows.Forms.TextBox();
		this.toolsBtn1 = new LockSoftware.Controls.ToolsBtn(this.components);
		this.clsBackPanel1.SuspendLayout();
		this.clsBackPanel2.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.grpFloor.SuspendLayout();
		this.grpBuild.SuspendLayout();
		base.SuspendLayout();
		this.imgListTV.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imgListTV.ImageStream");
		this.imgListTV.TransparentColor = System.Drawing.Color.Transparent;
		this.imgListTV.Images.SetKeyName(0, "OS00.png");
		this.imgListTV.Images.SetKeyName(1, "46.png");
		this.imgListTV.Images.SetKeyName(2, "ok.png");
		this.btnDisF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDisF.BackColor = System.Drawing.Color.Transparent;
		this.btnDisF.BaseColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDisF.ButtonColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.btnDisF.ButtonText = "Disabled Floor";
		this.btnDisF.CornerRadius = 4;
		this.btnDisF.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDisF.GlowColor = System.Drawing.Color.White;
		this.btnDisF.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDisF.Location = new System.Drawing.Point(377, 39);
		this.btnDisF.Name = "btnDisF";
		this.btnDisF.Size = new System.Drawing.Size(126, 48);
		this.btnDisF.TabIndex = 6;
		this.btnDisF.Click += new System.EventHandler(btnDisF_Click);
		this.btnDisB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDisB.BackColor = System.Drawing.Color.Transparent;
		this.btnDisB.BaseColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDisB.ButtonColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.btnDisB.ButtonText = "Disabled Building";
		this.btnDisB.CornerRadius = 4;
		this.btnDisB.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDisB.GlowColor = System.Drawing.Color.White;
		this.btnDisB.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDisB.Location = new System.Drawing.Point(220, 39);
		this.btnDisB.Name = "btnDisB";
		this.btnDisB.Size = new System.Drawing.Size(149, 48);
		this.btnDisB.TabIndex = 5;
		this.btnDisB.Click += new System.EventHandler(btnDisB_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnClose.BackColor = System.Drawing.Color.Transparent;
		this.btnClose.BaseColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.btnClose.ButtonColor = System.Drawing.Color.FromArgb(0, 64, 0);
		this.btnClose.ButtonText = "Close";
		this.btnClose.CornerRadius = 4;
		this.btnClose.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnClose.GlowColor = System.Drawing.Color.White;
		this.btnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnClose.Location = new System.Drawing.Point(510, 39);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(97, 48);
		this.btnClose.TabIndex = 4;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.clsBackPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.clsBackPanel1.Border = true;
		this.clsBackPanel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderBW = 1;
		this.clsBackPanel1.BorderColorBottom = System.Drawing.Color.LightGray;
		this.clsBackPanel1.BorderColorLeft = System.Drawing.Color.LightGray;
		this.clsBackPanel1.BorderColorRight = System.Drawing.Color.LightGray;
		this.clsBackPanel1.BorderColorTop = System.Drawing.Color.LightGray;
		this.clsBackPanel1.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderLW = 1;
		this.clsBackPanel1.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderRW = 1;
		this.clsBackPanel1.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderTW = 1;
		this.clsBackPanel1.Color1 = System.Drawing.Color.White;
		this.clsBackPanel1.Color2 = System.Drawing.Color.WhiteSmoke;
		this.clsBackPanel1.ColorAngle = 225f;
		this.clsBackPanel1.Controls.Add(this.clsBackPanel2);
		this.clsBackPanel1.Controls.Add(this.grpFloor);
		this.clsBackPanel1.Controls.Add(this.grpBuild);
		this.clsBackPanel1.Location = new System.Drawing.Point(3, 94);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(614, 447);
		this.clsBackPanel1.TabIndex = 3;
		this.clsBackPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.clsBackPanel2.Border = true;
		this.clsBackPanel2.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderBW = 1;
		this.clsBackPanel2.BorderColorBottom = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorLeft = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorRight = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderColorTop = System.Drawing.Color.Gray;
		this.clsBackPanel2.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderLW = 1;
		this.clsBackPanel2.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderRW = 1;
		this.clsBackPanel2.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel2.BorderTW = 1;
		this.clsBackPanel2.Color1 = System.Drawing.Color.White;
		this.clsBackPanel2.Color2 = System.Drawing.Color.Gray;
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.flowLayoutPanel1);
		this.clsBackPanel2.Controls.Add(this.btnRef);
		this.clsBackPanel2.Controls.Add(this.tvList);
		this.clsBackPanel2.Location = new System.Drawing.Point(10, 10);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(302, 426);
		this.clsBackPanel2.TabIndex = 9;
		this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.flowLayoutPanel1.Controls.Add(this.chkSDis);
		this.flowLayoutPanel1.Controls.Add(this.btnRT);
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(251, 30);
		this.flowLayoutPanel1.TabIndex = 4;
		this.chkSDis.AutoSize = true;
		this.chkSDis.BackColor = System.Drawing.Color.Transparent;
		this.chkSDis.Location = new System.Drawing.Point(3, 3);
		this.chkSDis.Name = "chkSDis";
		this.chkSDis.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
		this.chkSDis.Size = new System.Drawing.Size(105, 21);
		this.chkSDis.TabIndex = 3;
		this.chkSDis.Text = "Show Disabled";
		this.chkSDis.UseVisualStyleBackColor = false;
		this.btnRT.AutoSize = true;
		this.btnRT.BackColor = System.Drawing.Color.LightGray;
		this.btnRT.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRT.ForeColor = System.Drawing.Color.Black;
		this.btnRT.GlowColor = System.Drawing.Color.White;
		this.btnRT.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRT.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnRT.Location = new System.Drawing.Point(111, 2);
		this.btnRT.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
		this.btnRT.Name = "btnRT";
		this.btnRT.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnRT.Size = new System.Drawing.Size(60, 28);
		this.btnRT.TabIndex = 5;
		this.btnRT.Text = "Restore";
		this.btnRT.Click += new System.EventHandler(btnRT_Click);
		this.btnRef.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRef.BackColor = System.Drawing.Color.Transparent;
		this.btnRef.Checked = false;
		this.btnRef.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.btnRef.DefaultColor = System.Drawing.Color.Transparent;
		this.btnRef.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnRef.ImageNew = LockSoftware.Properties.Resources.Button_Refresh;
		this.btnRef.ImageRedrawed = true;
		this.btnRef.ImageStyle = 0;
		this.btnRef.isButton = true;
		this.btnRef.Location = new System.Drawing.Point(261, 5);
		this.btnRef.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.btnRef.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.btnRef.MouseDownStartColor = System.Drawing.Color.White;
		this.btnRef.MouseEnterBorderColor = System.Drawing.Color.Silver;
		this.btnRef.MouseEnterEndColor = System.Drawing.Color.White;
		this.btnRef.MouseEnterStartColor = System.Drawing.Color.White;
		this.btnRef.Name = "btnRef";
		this.btnRef.Size = new System.Drawing.Size(27, 27);
		this.btnRef.TabIndex = 2;
		this.btnRef.TextImageLocation = 0;
		this.btnRef.TextNew = "";
		this.btnRef.TextRedrawed = false;
		this.btnRef.Click += new System.EventHandler(btnRef_Click);
		this.tvList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tvList.ImageIndex = 0;
		this.tvList.ImageList = this.imgListTV;
		this.tvList.Location = new System.Drawing.Point(0, 35);
		this.tvList.Name = "tvList";
		this.tvList.SelectedImageIndex = 0;
		this.tvList.Size = new System.Drawing.Size(301, 390);
		this.tvList.TabIndex = 0;
		this.tvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(tvList_AfterSelect);
		this.tvList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvList_NodeMouseDoubleClick);
		this.grpFloor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.grpFloor.BackColor = System.Drawing.Color.Transparent;
		this.grpFloor.Controls.Add(this.btnFNew);
		this.grpFloor.Controls.Add(this.btnFEdit);
		this.grpFloor.Controls.Add(this.txtFMemo);
		this.grpFloor.Controls.Add(this.label1);
		this.grpFloor.Controls.Add(this.label5);
		this.grpFloor.Controls.Add(this.label6);
		this.grpFloor.Controls.Add(this.txtFCode);
		this.grpFloor.Controls.Add(this.txtFName);
		this.grpFloor.Location = new System.Drawing.Point(320, 224);
		this.grpFloor.Name = "grpFloor";
		this.grpFloor.Size = new System.Drawing.Size(283, 211);
		this.grpFloor.TabIndex = 8;
		this.grpFloor.TabStop = false;
		this.grpFloor.Text = "Floor";
		this.btnFNew.BackColor = System.Drawing.Color.LightGray;
		this.btnFNew.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFNew.ForeColor = System.Drawing.Color.Black;
		this.btnFNew.GlowColor = System.Drawing.Color.White;
		this.btnFNew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnFNew.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnFNew.Location = new System.Drawing.Point(186, 21);
		this.btnFNew.Name = "btnFNew";
		this.btnFNew.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnFNew.Size = new System.Drawing.Size(77, 33);
		this.btnFNew.TabIndex = 4;
		this.btnFNew.Text = "New";
		this.btnFNew.Click += new System.EventHandler(btnFNew_Click);
		this.btnFEdit.BackColor = System.Drawing.Color.LightGray;
		this.btnFEdit.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFEdit.ForeColor = System.Drawing.Color.Black;
		this.btnFEdit.GlowColor = System.Drawing.Color.White;
		this.btnFEdit.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnFEdit.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnFEdit.Location = new System.Drawing.Point(187, 63);
		this.btnFEdit.Name = "btnFEdit";
		this.btnFEdit.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnFEdit.Size = new System.Drawing.Size(77, 33);
		this.btnFEdit.TabIndex = 5;
		this.btnFEdit.Text = "Modify";
		this.btnFEdit.Click += new System.EventHandler(btnFEdit_Click);
		this.txtFMemo.Location = new System.Drawing.Point(19, 107);
		this.txtFMemo.Multiline = true;
		this.txtFMemo.Name = "txtFMemo";
		this.txtFMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtFMemo.Size = new System.Drawing.Size(244, 88);
		this.txtFMemo.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 34);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(42, 14);
		this.label1.TabIndex = 2;
		this.label1.Text = "Name:";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(16, 73);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 14);
		this.label5.TabIndex = 6;
		this.label5.Text = "Memo:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(16, 73);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(39, 14);
		this.label6.TabIndex = 3;
		this.label6.Text = "Code:";
		this.label6.Visible = false;
		this.txtFCode.Location = new System.Drawing.Point(76, 69);
		this.txtFCode.MaxLength = 3;
		this.txtFCode.Name = "txtFCode";
		this.txtFCode.Size = new System.Drawing.Size(77, 22);
		this.txtFCode.TabIndex = 2;
		this.txtFCode.Visible = false;
		this.txtFCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFCode_KeyPress);
		this.txtFName.Location = new System.Drawing.Point(76, 29);
		this.txtFName.MaxLength = 40;
		this.txtFName.Name = "txtFName";
		this.txtFName.Size = new System.Drawing.Size(77, 22);
		this.txtFName.TabIndex = 1;
		this.grpBuild.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.grpBuild.BackColor = System.Drawing.Color.Transparent;
		this.grpBuild.Controls.Add(this.btnBEdit);
		this.grpBuild.Controls.Add(this.btnBNew);
		this.grpBuild.Controls.Add(this.txtBMemo);
		this.grpBuild.Controls.Add(this.label2);
		this.grpBuild.Controls.Add(this.label4);
		this.grpBuild.Controls.Add(this.label3);
		this.grpBuild.Controls.Add(this.txtBCode);
		this.grpBuild.Controls.Add(this.txtBName);
		this.grpBuild.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpBuild.Location = new System.Drawing.Point(320, 3);
		this.grpBuild.Name = "grpBuild";
		this.grpBuild.Size = new System.Drawing.Size(283, 213);
		this.grpBuild.TabIndex = 7;
		this.grpBuild.TabStop = false;
		this.grpBuild.Text = "Building";
		this.btnBEdit.BackColor = System.Drawing.Color.LightGray;
		this.btnBEdit.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBEdit.ForeColor = System.Drawing.Color.Black;
		this.btnBEdit.GlowColor = System.Drawing.Color.White;
		this.btnBEdit.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnBEdit.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnBEdit.Location = new System.Drawing.Point(187, 63);
		this.btnBEdit.Name = "btnBEdit";
		this.btnBEdit.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnBEdit.Size = new System.Drawing.Size(77, 33);
		this.btnBEdit.TabIndex = 5;
		this.btnBEdit.Text = "Modify";
		this.btnBEdit.Click += new System.EventHandler(btnBEdit_Click);
		this.btnBNew.BackColor = System.Drawing.Color.LightGray;
		this.btnBNew.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBNew.ForeColor = System.Drawing.Color.Black;
		this.btnBNew.GlowColor = System.Drawing.Color.White;
		this.btnBNew.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnBNew.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnBNew.Location = new System.Drawing.Point(187, 23);
		this.btnBNew.Name = "btnBNew";
		this.btnBNew.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnBNew.Size = new System.Drawing.Size(77, 33);
		this.btnBNew.TabIndex = 4;
		this.btnBNew.Text = "New";
		this.btnBNew.Click += new System.EventHandler(btnBNew_Click);
		this.txtBMemo.Location = new System.Drawing.Point(19, 110);
		this.txtBMemo.Multiline = true;
		this.txtBMemo.Name = "txtBMemo";
		this.txtBMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtBMemo.Size = new System.Drawing.Size(244, 88);
		this.txtBMemo.TabIndex = 3;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(16, 34);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(42, 14);
		this.label2.TabIndex = 2;
		this.label2.Text = "Name:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(16, 73);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(44, 14);
		this.label4.TabIndex = 6;
		this.label4.Text = "Memo:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(16, 73);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 14);
		this.label3.TabIndex = 3;
		this.label3.Text = "Code:";
		this.label3.Visible = false;
		this.txtBCode.Location = new System.Drawing.Point(76, 69);
		this.txtBCode.MaxLength = 3;
		this.txtBCode.Name = "txtBCode";
		this.txtBCode.Size = new System.Drawing.Size(77, 22);
		this.txtBCode.TabIndex = 2;
		this.txtBCode.Visible = false;
		this.txtBCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBCode_KeyPress);
		this.txtBName.Location = new System.Drawing.Point(76, 29);
		this.txtBName.MaxLength = 40;
		this.txtBName.Name = "txtBName";
		this.txtBName.Size = new System.Drawing.Size(77, 22);
		this.txtBName.TabIndex = 1;
		this.toolsBtn1.BackColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Checked = true;
		this.toolsBtn1.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.DefaultColor = System.Drawing.Color.Transparent;
		this.toolsBtn1.Dock = System.Windows.Forms.DockStyle.Top;
		this.toolsBtn1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolsBtn1.ForeColor = System.Drawing.Color.Olive;
		this.toolsBtn1.GuidInfo = "&56~01'][Manson]v%#@";
		this.toolsBtn1.Image = LockSoftware.Properties.Resources._49;
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
		this.toolsBtn1.Size = new System.Drawing.Size(621, 91);
		this.toolsBtn1.TabIndex = 2;
		this.toolsBtn1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.toolsBtn1.TextImageLocation = 0;
		this.toolsBtn1.TextNew = "     〓Building && Floor: Setting hotel's building and floor.";
		this.toolsBtn1.TextRedrawed = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(621, 545);
		base.Controls.Add(this.btnDisF);
		base.Controls.Add(this.btnDisB);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.clsBackPanel1);
		base.Controls.Add(this.toolsBtn1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmBuildFloor";
		this.Text = "frmBuildFloor";
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel2.ResumeLayout(false);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.grpFloor.ResumeLayout(false);
		this.grpFloor.PerformLayout();
		this.grpBuild.ResumeLayout(false);
		this.grpBuild.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmBuildFloor()
	{
		InitializeComponent();
		base.MinimizeBox = (base.MaximizeBox = false);
		base.FormBorderStyle = FormBorderStyle.FixedSingle;
		base.StartPosition = FormStartPosition.CenterScreen;
		m_htab = Program.GetControlName(this, m_objName);
		InitTreeList();
		btnRT.Visible = false;
	}

	private void InitTreeList()
	{
		try
		{
			TreeNode selectedNode = tvList.SelectedNode;
			tvList.Nodes.Clear();
			string text = "Select B_ID, B_HotelName,Build_ID,Build_Code, Build_Name, IsNull(Build_Flag,0) As Build_Flag, Build_Memo, Floor_ID, Floor_Code, Floor_Name, IsNull(Floor_Flag,0) As Floor_Flag, Floor_Memo From v_HotelBF";
			text += " Where 1=1";
			text += " Order by B_ID, Build_ID, Floor_ID ";
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				return;
			}
			TreeNode treeNode = null;
			TreeNode treeNode2 = null;
			string text3;
			string text2 = (text3 = "");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (text2 != dataTable.Rows[i]["B_HotelName"].ToString().Trim())
				{
					text2 = dataTable.Rows[i]["B_HotelName"].ToString().Trim();
					treeNode = new TreeNode(text2, 0, 2);
					treeNode.Name = dataTable.Rows[i]["B_ID"].ToString().Trim();
					tvList.Nodes.Add(treeNode);
				}
				if (!Convert.ToBoolean(dataTable.Rows[i]["Build_Flag"].ToString()) || chkSDis.Checked)
				{
					if (text3 != dataTable.Rows[i]["Build_Name"].ToString().Trim())
					{
						text3 = dataTable.Rows[i]["Build_Name"].ToString().Trim();
						treeNode2 = new TreeNode(text3, 1, 2);
						treeNode2.Name = dataTable.Rows[i]["Build_ID"].ToString().Trim();
						treeNode.Nodes.Add(treeNode2);
					}
					if ((!Convert.ToBoolean(dataTable.Rows[i]["Floor_Flag"].ToString()) || chkSDis.Checked) && dataTable.Rows[i]["Floor_Name"].ToString().Trim() != "")
					{
						treeNode2?.Nodes.Add(dataTable.Rows[i]["Floor_ID"].ToString().Trim(), dataTable.Rows[i]["Floor_Name"].ToString().Trim(), 1, 2);
					}
				}
			}
			tvList.ExpandAll();
			tvList.Select();
			if (selectedNode == null)
			{
				return;
			}
			if (selectedNode.Level == 1 && tvList.Nodes.Count == 1)
			{
				TreeNode[] array = tvList.Nodes[0].Nodes.Find(selectedNode.Name, searchAllChildren: false);
				foreach (TreeNode treeNode3 in array)
				{
					if (selectedNode.Text == treeNode3.Text)
					{
						tvList.SelectedNode = treeNode3;
					}
				}
			}
			else if (selectedNode.Level == 0 && tvList.Nodes.Count == 1)
			{
				tvList.SelectedNode = tvList.Nodes[0].Nodes[tvList.Nodes[0].Nodes.Count - 1];
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err01"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnBNew_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null)
			{
				Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			while (selectedNode.Parent != null)
			{
				selectedNode = selectedNode.Parent;
			}
			text = selectedNode.Name.ToString();
			if (text == "")
			{
				Program.MsgBox((string)m_htab["Info03"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (txtBName.Text.Trim() == "" || txtBCode.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int num = Convert.ToInt16(txtBCode.Text.Trim());
			string text2 = "";
			if (num > 255)
			{
				text2 = string.Format((string)m_htab["Info13"], label3.Text.Trim().Substring(0, label3.Text.Trim().Length - 1), 256);
				Program.MsgCustom(text2, MessageBoxIcon.Asterisk);
				return;
			}
			string sql = "Select * from D_Build Where ((Build_Name=N'" + txtBName.Text.Trim() + "' And Build_Flag = 0) Or Build_Code=" + num + ")";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			sql = "Insert Into D_Build Values(" + text + ",'" + num.ToString() + "',N'" + txtBName.Text.Trim() + "',0,GetDate()," + Program.m_opid + ",NULL,NULL,N'" + txtBMemo.Text.Trim() + "')";
			int num2 = SQLserver.Data_ExecuteSql(sql);
			if (num2 <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num2, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				InitTreeList();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err02"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnBEdit_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtBName.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info09"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null || selectedNode.Level != 1)
			{
				Program.MsgBox((string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			text = string.Format((string)m_htab["grpBuild"] + "\r\n" + (string)m_htab["Info08"], selectedNode.Text.Trim(), "\r\n", txtBName.Text.Trim(), "\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			string sql = "Select * from D_Build Where hotelID=" + selectedNode.Parent.Name.ToString().Trim() + " And Build_Name=N'" + txtBName.Text.Trim() + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				text = string.Format((string)m_htab["Err07"], txtBName.Text.Trim(), (string)m_htab["grpBuild"]);
				Program.MsgBox(text, (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			sql = "Update D_Build Set Build_Name=N'" + txtBName.Text.Trim() + "', Build_Memo=N'" + txtBMemo.Text.Trim() + "', UpdateTime=GetDate(), Updator_ID=" + Program.m_opid;
			sql = sql + " Where Build_ID=" + selectedNode.Name.ToString().Trim();
			int num = SQLserver.Data_ExecuteSql(sql);
			if (num <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				InitTreeList();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err04"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnFNew_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null || selectedNode.Parent == null)
			{
				Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (selectedNode.Level == 2)
			{
				selectedNode = selectedNode.Parent;
			}
			text = selectedNode.Name.ToString().Trim();
			if (text == "")
			{
				Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (txtFName.Text.Trim() == "" || txtFCode.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info02"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			int num = Convert.ToInt16(txtFCode.Text.Trim());
			string text2 = "";
			if (num > 255)
			{
				text2 = string.Format((string)m_htab["Info13"], label6.Text.Trim().Substring(0, label6.Text.Trim().Length - 1), 256);
				Program.MsgCustom(text2, MessageBoxIcon.Asterisk);
				return;
			}
			string sql = "Select * from D_Floor Where Build_ID=" + text + " And ((Floor_Name=N'" + txtFName.Text.Trim() + "' And Floor_Flag = 0) Or Floor_Code=" + num + ")";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				Program.MsgBox((string)m_htab["Info05"], (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			sql = "Insert Into D_Floor Values('" + num.ToString() + "',N'" + txtFName.Text.Trim() + "',0," + text + ",GetDate()," + Program.m_opid + ",NULL,NULL,N'" + txtFMemo.Text.Trim() + "')";
			int num2 = SQLserver.Data_ExecuteSql(sql);
			if (num2 <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num2, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				InitTreeList();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err03"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnFEdit_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtFName.Text.Trim() == "")
			{
				Program.MsgBox((string)m_htab["Info09"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null || selectedNode.Level != 2)
			{
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			text = string.Format((string)m_htab["grpFloor"] + "\r\n" + (string)m_htab["Info08"], selectedNode.Text.Trim(), "\r\n", txtFName.Text.Trim(), "\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			string sql = "Select * from D_Floor Where Build_ID=" + selectedNode.Parent.Name.ToString().Trim() + " And Floor_Name=N'" + txtFName.Text.Trim() + "'";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count > 0)
			{
				if (dataTable != null)
				{
					dataTable.Clear();
					dataTable.Dispose();
				}
				text = string.Format((string)m_htab["Err07"], txtFName.Text.Trim(), (string)m_htab["grpFloor"]);
				Program.MsgBox(text, (string)Program.m_hPubTab["WarnTitle"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			sql = "Update D_Floor Set Floor_Name=N'" + txtFName.Text.Trim() + "', Floor_Memo=N'" + txtFMemo.Text.Trim() + "', UpdateTime=GetDate(), Updator_ID=" + Program.m_opid;
			sql = sql + " Where Floor_ID=" + selectedNode.Name.ToString().Trim();
			int num = SQLserver.Data_ExecuteSql(sql);
			if (num <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOperCode"] + num, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				InitTreeList();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err05"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnRef_Click(object sender, EventArgs e)
	{
		InitTreeList();
	}

	private void txtBCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void txtFCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: true, chkDot: true);
	}

	private void tvList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		try
		{
			TreeNode node = e.Node;
			if (node == null)
			{
				return;
			}
			string text = "Select * From ";
			if (node.Level == 2)
			{
				text = text + " D_Floor Where Floor_ID=" + node.Name.ToString().Trim();
			}
			else
			{
				if (node.Level != 1)
				{
					return;
				}
				text = text + " D_Build Where Build_ID=" + node.Name.ToString().Trim();
			}
			DataTable dataTable = SQLserver.Data_GetDataTable(text);
			bool flag = false;
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				if (node.Level == 1)
				{
					txtBName.Text = dataTable.Rows[0]["Build_Name"].ToString().Trim();
					txtBCode.Text = dataTable.Rows[0]["Build_Code"].ToString().Trim();
					txtBMemo.Text = dataTable.Rows[0]["Build_Memo"].ToString().Trim();
					flag = Convert.ToBoolean(dataTable.Rows[0]["Build_Flag"].ToString());
					btnBEdit.Enabled = !flag;
				}
				else
				{
					txtFName.Text = dataTable.Rows[0]["Floor_Name"].ToString().Trim();
					txtFCode.Text = dataTable.Rows[0]["Floor_Code"].ToString().Trim();
					txtFMemo.Text = dataTable.Rows[0]["Floor_Memo"].ToString().Trim();
					flag = Convert.ToBoolean(dataTable.Rows[0]["Floor_Flag"].ToString());
					btnFEdit.Enabled = !flag;
				}
			}
			btnRT.Visible = flag;
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Err06"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
	{
		btnRT.Visible = false;
		TreeNode node = e.Node;
		if (node == null)
		{
			return;
		}
		string text = string.Empty;
		if (node.Level == 0)
		{
			text = "Select Build_Code From D_Build";
		}
		else if (node.Level == 1)
		{
			text = "Select Floor_Code From D_Floor  Where Build_ID=" + node.Name.ToString().Trim();
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		DataTable dataTable = SQLserver.Data_GetDataTable(text);
		byte b = 1;
		bool flag = false;
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			while (!flag)
			{
				for (int i = 0; i < dataTable.Rows.Count && !(b.ToString() == dataTable.Rows[i][0].ToString()); i++)
				{
					if (i == dataTable.Rows.Count - 1)
					{
						flag = true;
					}
				}
				if (b == byte.MaxValue)
				{
					break;
				}
				if (!flag)
				{
					b++;
				}
			}
		}
		if (node.Level == 0)
		{
			txtBCode.Text = b.ToString();
		}
		else
		{
			txtFCode.Text = b.ToString();
		}
	}

	private void btnDisF_Click(object sender, EventArgs e)
	{
		try
		{
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null || selectedNode.Level != 2)
			{
				Program.MsgBox((string)m_htab["Info07"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			text = string.Format((string)m_htab["Info11"], selectedNode.Text.Trim() + "\r\n\r\n", "\r\n\r\n");
			string sql = "Select R_ID From v_HotelRooms Where R_FloorID=" + selectedNode.Name.ToString().Trim() + " And Floor_Flag = 0 And R_flag =0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable.Rows.Count > 0)
			{
				text = string.Format((string)m_htab["Info12"], selectedNode.Text.Trim());
				Program.MsgCustom(text, MessageBoxIcon.Exclamation);
				return;
			}
			text = string.Format((string)m_htab["Info14"], selectedNode.Text.Trim(), selectedNode.Text.Trim(), "\r\n\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			sql = "Select R_FloorID AS FID From v_Room Where R_FloorID=" + selectedNode.Name.ToString().Trim() + " \n";
			sql += " Union All \n";
			sql = sql + " Select f_id AS FID From T_CardManage Where f_id=" + selectedNode.Name.ToString().Trim() + " \n";
			sql += " Union All \n";
			sql = sql + " Select floor_ID AS FID From T_RoomGroup Where floor_ID=" + selectedNode.Name.ToString().Trim() + " \n";
			sql += " if @@rowcount=0 \n";
			sql = sql + " begin \n Delete From D_Floor Where Floor_ID=" + selectedNode.Name.ToString().Trim() + " end \n";
			object obj = sql;
			sql = string.Concat(obj, " else \n begin \n Update D_Floor Set Floor_Flag = 1, UpdateTime = GetDate(), Updator_ID = ", Program.m_opid, " Where Floor_Flag = 0 And Floor_ID = ", selectedNode.Name.ToString().Trim(), " \n end");
			if (Program.DBCompExec(sql, btnDisB.Text) <= 0)
			{
				Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Program.fm != null)
			{
				Program.MDIFrm_Center_BFR_Ref(Program.fm.MdiChildren);
			}
			InitTreeList();
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnDisB_Click(object sender, EventArgs e)
	{
		try
		{
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null || selectedNode.Level != 1)
			{
				Program.MsgBox((string)m_htab["Info06"], (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			string text = "";
			text = string.Format((string)m_htab["Info10"], selectedNode.Text.Trim() + "\r\n\r\n", "\r\n\r\n");
			string sql = "Select Floor_ID, IsNull(Floor_Flag,0) From D_Floor Where Build_ID=" + selectedNode.Name.ToString().Trim() + " And IsNull(Floor_Flag,0) = 0";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable.Rows.Count > 0)
			{
				text = string.Format((string)m_htab["Info12"], selectedNode.Text.Trim());
				Program.MsgCustom(text, MessageBoxIcon.Exclamation);
				return;
			}
			text = string.Format((string)m_htab["Info14"], selectedNode.Text.Trim(), selectedNode.Text.Trim(), "\r\n\r\n");
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
			{
				sql = "Select Build_ID From D_Floor Where Build_ID=" + selectedNode.Name.ToString().Trim() + " \n Union all \n";
				sql = sql + " Select bl_id As Build_ID From T_CardManage Where bl_id=" + selectedNode.Name.ToString().Trim() + " \n Union all \n";
				sql = sql + " Select Build_ID From T_RoomGroup Where Build_ID=" + selectedNode.Name.ToString().Trim() + " \n";
				sql += " if @@rowcount=0 \n";
				sql = sql + " begin \n Delete From D_Build Where Build_ID=" + selectedNode.Name.ToString().Trim() + " end \n";
				object obj = sql;
				sql = string.Concat(obj, " else \n begin \n Update D_Build Set Build_Flag = 1, UpdateTime = GetDate(), Updator_ID = ", Program.m_opid, " Where Build_Flag = 0 And Build_ID=", selectedNode.Name.ToString().Trim(), " \n end");
				if (Program.DBCompExec(sql, btnDisB.Text) <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					InitTreeList();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnRT_Click(object sender, EventArgs e)
	{
		try
		{
			TreeNode selectedNode = tvList.SelectedNode;
			if (selectedNode == null || selectedNode.Level == 0)
			{
				return;
			}
			string text = "";
			text = string.Format((string)m_htab["Info15"], selectedNode.Text.Trim());
			if (Program.MsgBox(text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
			{
				string sqlquery = "";
				if (selectedNode.Level == 1)
				{
					sqlquery = "Update D_Build Set Build_Flag = 0, UpdateTime = GetDate(), Updator_ID = " + Program.m_opid + " Where Build_ID=" + selectedNode.Name.ToString().Trim();
				}
				else if (selectedNode.Level == 2)
				{
					sqlquery = "Update D_Floor Set Floor_Flag = 0, UpdateTime = GetDate(), Updator_ID = " + Program.m_opid + " Where Floor_ID = " + selectedNode.Name.ToString().Trim();
				}
				if (Program.DBCompExec(sqlquery, btnDisB.Text) <= 0)
				{
					Program.MsgBox((string)Program.m_hPubTab["ErrDBOper"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					InitTreeList();
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)Program.m_hPubTab["ErrOperWithMess"] + ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}
}
