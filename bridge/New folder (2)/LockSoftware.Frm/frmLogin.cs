using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CommonLib;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;
using ZK.Utils;

namespace LockSoftware.Frm;

public class frmLogin : Form
{
	public string m_objName = "WFLOGIN";

	public Hashtable m_htab;

	private bool m_IniFace;

	private Dictionary<string, string> uPasswords;

	private Dictionary<string, int> uIDs;

	private bool isCreating;

	private Point mousePosition = new Point(0, 0);

	private IContainer components;

	private clsBackPanel bpMain;

	private Label labTitle;

	private Label labVer;

	private ToolsBtn tbtnClose;

	private TextBox txtPwd;

	private TextBox txtUN;

	private Label label3;

	private Label label2;

	private Label labID;

	private GlassBtn gbEnter;

	private GlassBtn gbClose;

	private GlassBtn gbSysSet;

	private Panel plBottom;

	private GlassBtn gbSave;

	private Label label9;

	private ComboBox cobCOM;

	private Label label10;

	private clsBackPanel clsBackPanel1;

	private Label label8;

	private TextBox txtSPwd;

	private TextBox txtSUN;

	private TextBox txtSN;

	private Label label7;

	private Label label6;

	private Label label5;

	private clsBackPanel clsBackPanel2;

	private Label label4;

	private ComboBox cobSector;

	private ComboBox cobUID;

	private ComboBox cobLan;

	private Label labLan;

	private ComboBox cobBaud;

	private Label labBaud;

	private clsBackPanel clsBackPanel3;

	private GlassBtn btnSevr;

	private TextBox txtCDBPwd;

	private Label label1;

	private GlassBtn btnCrDB;

	private CheckBox chkRem;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private SaveFileDialog SaFile;

	private TextBox txtDB;

	private Label label11;

	private OpenFileDialog OpenFile;

	private GlassBtn btnDefault;

	private Panel panTop;

	private PictureBox picWait;

	private void bpMain_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mousePosition.X = e.X + bpMain.Left;
			mousePosition.Y = e.Y + bpMain.Top;
		}
	}

	private void bpMain_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			base.Top = Control.MousePosition.Y - mousePosition.Y;
			base.Left = Control.MousePosition.X - mousePosition.X;
		}
	}

	private void panTop_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mousePosition.X = e.X + panTop.Left + bpMain.Left;
			mousePosition.Y = e.Y + panTop.Top + bpMain.Top;
		}
	}

	private void panTop_MouseMove(object sender, MouseEventArgs e)
	{
		bpMain_MouseMove(sender, e);
	}

	private void labTitle_MouseDown(object sender, MouseEventArgs e)
	{
		labVer_MouseDown(sender, e);
	}

	private void labTitle_MouseMove(object sender, MouseEventArgs e)
	{
		bpMain_MouseMove(sender, e);
	}

	private void labVer_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mousePosition.X = e.X + ((Label)sender).Left + panTop.Left + bpMain.Left;
			mousePosition.Y = e.Y + ((Label)sender).Top + panTop.Top + bpMain.Top;
		}
	}

	private void labVer_MouseMove(object sender, MouseEventArgs e)
	{
		bpMain_MouseMove(sender, e);
	}

	private void plBottom_VisibleChanged(object sender, EventArgs e)
	{
		if (plBottom.Visible)
		{
			base.Height = panTop.Height + plBottom.Height + 5;
		}
		else
		{
			base.Height = panTop.Height + 5;
		}
	}

	public frmLogin()
	{
		InitializeComponent();
		Program.LoadLogo("LoginLOGO.png", pictureBox2);
		labVer.Text = $"Ver{Program.AssemblyVersionMM}";
		Text += labVer.Text.Trim();
	}

	private void frmLogin_Load(object sender, EventArgs e)
	{
		try
		{
			plBottom.VisibleChanged -= plBottom_VisibleChanged;
			plBottom.Visible = false;
			plBottom.VisibleChanged += plBottom_VisibleChanged;
			plBottom_VisibleChanged(null, null);
			int num = Program.ReadLansType();
			if (num <= 0 || Program.m_lansDt == null)
			{
				Application.Exit();
				return;
			}
			m_IniFace = true;
			cobLan.DataSource = Program.m_lansDt;
			cobLan.DisplayMember = "lansName";
			cobLan.ValueMember = "fpath";
			cobLan.SelectedIndex = Program.m_Lan;
			m_IniFace = false;
			InitGUI();
			string config = Program.GetConfig();
			if (Program.m_Lan > num - 1)
			{
				Program.m_Lan = num - 1;
			}
			num = (cobLan.SelectedIndex = Program.m_Lan);
			if (IsHostMachine(Program.m_SqlSN))
			{
				SQLserver.SetConnType(1);
			}
			else
			{
				SQLserver.SetConnType(0);
			}
			SQLserver.SetDebug(Program.m_defDebug == "yes", Program.m_AppPath);
			SQLserver.Data_Set_Connect(Program.m_SqlSN, Program.m_SqlDN, Program.m_SqlUN, Program.m_SqlUPWD);
			if (!string.IsNullOrEmpty(config) && File.Exists(config))
			{
				if (SQLserver.DataBase_Restore(Program.m_SqlSN, Program.m_SqlDN, Program.m_SqlUN, Program.m_SqlUPWD, config) != 1)
				{
					Program.MsgBox((string)m_htab["Error03"], (string)m_htab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					Program.SetSingleItem("RESTOREFILE", "");
					Program.MsgBox((string)m_htab["Info23"], (string)m_htab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			long num2 = 0L;
			if (!SQLserver.sqlConnIsOpen())
			{
				num2 = SQLserver.DataConnect(Program.m_SqlSN, Program.m_SqlDN, Program.m_SqlUN, Program.m_SqlUPWD);
				if (num2 < 0)
				{
					gbSysSet_Click(null, null);
					if (num2 == -1 || (num2 == -2 && !IsHostMachine(txtSN.Text.Trim())) || Program.firstRun != "0")
					{
						string text = "";
						text = (string)m_htab["Info06"];
						Program.MsgBox(text, (string)m_htab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						label1.Visible = true;
						txtCDBPwd.Visible = true;
						btnCrDB.Visible = true;
					}
					else if (num2 == -2)
					{
						Directory.CreateDirectory(Environment.SystemDirectory.Substring(0, 2) + "\\BiolockData");
						string sourcepath = Program.m_AppPath + "\\DB.Bak";
						string text2 = Environment.SystemDirectory.Substring(0, 2) + "\\BiolockData\\" + Program.m_SqlDN + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mdf";
						string savepath = text2;
						picWait.Visible = true;
						CreatDatabase(sourcepath, text2, savepath);
					}
					return;
				}
				label1.Visible = false;
				txtCDBPwd.Visible = false;
				btnCrDB.Visible = false;
				DataTable dataTable = SQLserver.Data_GetDataTable("select count(name) from sysobjects where xtype='u'");
				if (Program.firstRun == "0" && (dataTable == null || dataTable.Rows.Count != 1 || Convert.ToInt32(dataTable.Rows[0][0]) <= 0))
				{
					Directory.CreateDirectory(Environment.SystemDirectory.Substring(0, 2) + "\\BiolockData");
					string sourcepath2 = Program.m_AppPath + "\\DB.Bak";
					string text3 = Environment.SystemDirectory.Substring(0, 2) + "\\BiolockData\\" + Program.m_SqlDN + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mdf";
					string savepath2 = text3;
					picWait.Visible = true;
					CreatDatabase(sourcepath2, text3, savepath2);
				}
				if (Program.firstRun == "0")
				{
					Program.firstRun = "1";
				}
				Program.SaveConfig();
			}
			loadUsers(bind: true);
			cobUID.Select();
			Program.UpdateDB();
		}
		catch (Exception ex)
		{
			SQLserver.Data_Close();
			Program.MsgBox((string)m_htab["Info07"] + "\r\n" + ex.Message, (string)m_htab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
			gbSysSet_Click(null, null);
		}
	}

	private void loadUsers(bool bind)
	{
		if (bind)
		{
			cobUID.Items.Clear();
		}
		if (uPasswords != null)
		{
			uPasswords.Clear();
		}
		else
		{
			uPasswords = new Dictionary<string, string>();
		}
		if (uIDs != null)
		{
			uIDs.Clear();
		}
		else
		{
			uIDs = new Dictionary<string, int>();
		}
		XmlNodeList elements = new ClassXml(Program.pathXml, "SystemConfig").GetElements("SystemConfig/Users");
		if (elements == null)
		{
			return;
		}
		foreach (XmlNode item in elements)
		{
			if (bind)
			{
				cobUID.Items.Add(item.Attributes["Name"].Value);
			}
			uPasswords.Add(item.Attributes["Name"].Value, item.Attributes["Password"].Value);
			try
			{
				uIDs.Add(item.Attributes["Name"].Value, Convert.ToInt32(item.Attributes["ID"].Value));
			}
			catch
			{
				uIDs.Add(item.Attributes["Name"].Value, -1);
			}
		}
	}

	private void tbtnClose_Click(object sender, EventArgs e)
	{
		if (!isCreating)
		{
			string text = "";
			text = (string)m_htab["Info01"];
			if (Program.MsgBox(text, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				Program.m_Exit = true;
				Application.Exit();
			}
		}
	}

	private void gbClose_Click(object sender, EventArgs e)
	{
		if (!isCreating)
		{
			string text = "";
			text = (string)m_htab["Info01"];
			if (Program.MsgBox(text, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				Program.m_Exit = true;
				Application.Exit();
			}
		}
	}

	private void gbEnter_Click(object sender, EventArgs e)
	{
		try
		{
			if (isCreating)
			{
				return;
			}
			string text = "";
			string sql = "select distinct u.User_ID, u.user_name,u.user_no as user_no,u.issys as issys,u.user_password,ug.name as gname from userinfo u,usergroup ug where u.user_no=N'" + cobUID.Text.Trim() + "' and u.Stop_Flag=0 and ug.groupid = (select groupid from userinfo where user_no = N'" + cobUID.Text.Trim() + "')";
			DataTable dataTable = SQLserver.Data_GetDataTable(sql);
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				text = (string)m_htab["Info04"];
				Program.MsgBox(text, Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				txtPwd.Focus();
				return;
			}
			if (dataTable.Rows[0]["user_password"].ToString() != txtPwd.Text.Trim())
			{
				text = (string)m_htab["Info03"];
				Program.MsgBox(text, Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				txtPwd.Focus();
				return;
			}
			SQLserver.Data_ExecuteSql(Program.sqlstrup);
			sql = "";
			SQLserver.UserNo = (Program.m_OperID = dataTable.Rows[0]["User_No"].ToString().Trim());
			SQLserver.UserName = (Program.m_OperName = dataTable.Rows[0]["User_Name"].ToString().Trim());
			SQLserver.UserPassword = (Program.m_OperPwd = dataTable.Rows[0]["user_Password"].ToString().Trim());
			SQLserver.UserGroup = dataTable.Rows[0]["gname"].ToString().Trim();
			SQLserver.IsSys = dataTable.Rows[0]["issys"].ToString().Trim();
			Program.m_opid = Convert.ToInt32(dataTable.Rows[0]["User_ID"].ToString().Trim());
			dataTable.Clear();
			dataTable.Dispose();
			if (chkRem.Checked)
			{
				new ClassXml(Program.pathXml, "SystemConfig").DealUser(Program.m_opid, Program.m_OperID, Rijndael.Instatnce.Encrypt(Program.m_OperPwd), _save: true, ref Program.xd);
			}
			else
			{
				new ClassXml(Program.pathXml, "SystemConfig").DealUser(Program.m_opid, Program.m_OperID, Rijndael.Instatnce.Encrypt(Program.m_OperPwd), _save: false, ref Program.xd);
			}
			if (Program.fm == null || Program.fm.Disposing || Program.fm.IsDisposed)
			{
				Program.fm = new frmMain();
			}
			Hide();
			txtPwd.Text = "";
			chkRem.Checked = false;
			Program.fm.ShowDialog();
			try
			{
				Program.fm.Close();
			}
			catch
			{
			}
			loadUsers(bind: true);
			Show();
		}
		catch (Exception ex)
		{
			string text2 = "";
			text2 = (string)m_htab["Info05"] + "\r\n";
			Program.MsgBox(text2 + ex.Message, (string)m_htab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void gbSysSet_Click(object sender, EventArgs e)
	{
		if (!plBottom.Visible)
		{
			gbSysSet.Text = (string)m_htab["gbSysSet1"];
			plBottom.Visible = true;
			txtSN.Text = Dns.GetHostName() + "\\SQLEXPRESS";
			if (Program.m_SqlSN != "" && Program.m_SqlSN != Dns.GetHostName() + "\\SQLEXPRESS")
			{
				txtSN.Text = Program.m_SqlSN;
			}
			txtDB.Text = Program.m_SqlDN;
			txtSUN.Text = Program.m_SqlUN;
			txtSPwd.Text = Program.m_SqlUPWD;
			cobCOM.SelectedIndex = Program.m_DevCOM;
			cobSector.SelectedIndex = Program.m_CardSector;
			cobBaud.Text = Program.m_DevBaud.ToString();
		}
		else
		{
			gbSysSet.Text = (string)m_htab["gbSysSet"];
			plBottom.Visible = false;
		}
	}

	private void gbSave_Click(object sender, EventArgs e)
	{
		string text = "";
		string text2 = "";
		try
		{
			text = (string)m_htab["Info08"];
			text2 = (string)m_htab["InfoTitle"];
			if (Program.MsgBox(text, text2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.No)
			{
				Program.m_SqlSN = txtSN.Text.Trim();
				Program.m_SqlDN = txtDB.Text.Trim();
				Program.m_SqlUN = txtSUN.Text.Trim();
				Program.m_SqlUPWD = txtSPwd.Text.Trim();
				Program.m_DevCOM = cobCOM.SelectedIndex;
				Program.m_CardSector = cobSector.SelectedIndex;
				Program.m_DevBaud = Convert.ToInt32(cobBaud.Text);
				Program.m_Lan = cobLan.SelectedIndex;
				Program.SaveConfig();
				text = (string)m_htab["Info09"];
				Program.MsgBox(text, text2, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Program.m_Exit = true;
				Program.mutex.Close();
				Application.Exit();
				Application.Restart();
			}
		}
		catch (Exception ex)
		{
			text = (string)m_htab["Info11"];
			Program.MsgBox(text + "\r\n" + ex.Message, (string)m_htab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtPwd_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (txtPwd.Text != "")
			{
				gbEnter_Click(null, null);
				gbEnter.Focus();
			}
			else
			{
				cobUID_KeyDown(sender, e);
			}
		}
	}

	private void cobUID_KeyDown(object sender, KeyEventArgs e)
	{
		frmLogin_KeyDown(sender, e);
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		try
		{
			string text = cobUID.Text.Trim();
			if (!(text == ""))
			{
				Program.m_OperID = text;
				if (uPasswords.ContainsKey(text))
				{
					Program.m_OperPwd = Rijndael.Instatnce.Decrypt(uPasswords[text]);
					chkRem.Checked = true;
					txtPwd.Text = Program.m_OperPwd;
					Program.m_opid = uIDs[text];
				}
				else
				{
					chkRem.Checked = false;
					txtPwd.Text = "";
				}
				txtPwd.Focus();
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox((string)m_htab["Info14"] + "\r\n" + ex.Message, (string)m_htab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cobLan_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!m_IniFace)
		{
			Program.m_Lan = cobLan.SelectedIndex;
			InitGUI();
		}
	}

	private void InitGUI()
	{
		if (cobLan.Text == "ภาษาไทย-TH")
		{
			ClassFont.Instance.enabled = true;
		}
		else
		{
			ClassFont.Instance.enabled = false;
		}
		m_htab = Program.GetControlName(this, m_objName);
		if (m_htab != null)
		{
			Label label = labTitle;
			string text = (Text = (string)m_htab["systemTitle"] + " " + labVer.Text);
			label.Text = text;
			Program.m_M_OK = (string)m_htab["Btn_M_OK"];
			Program.m_M_Cancel = (string)m_htab["Btn_M_Cancel"];
			Program.m_M_Abort = (string)m_htab["Btn_M_Abort"];
			Program.m_M_Retry = (string)m_htab["Btn_M_Retry"];
			Program.m_M_Ignore = (string)m_htab["Btn_M_Ignore"];
			Program.m_M_Yes = (string)m_htab["Btn_M_Yes"];
			Program.m_M_No = (string)m_htab["Btn_M_No"];
			if (plBottom.Visible)
			{
				gbSysSet.Text = (string)m_htab["gbSysSet1"];
			}
			Refresh();
		}
	}

	private void btnSevr_Click(object sender, EventArgs e)
	{
		try
		{
			frmDB frmDB2 = new frmDB();
			if (frmDB2.ShowDialog() == DialogResult.OK)
			{
				txtSN.Text = frmDB2.m_svrname;
				frmDB2.Dispose();
			}
		}
		catch
		{
		}
	}

	private string GetFrontSub(string source, string search)
	{
		_ = string.Empty;
		int num = source.IndexOf(search);
		if (num > 0)
		{
			return source.Substring(0, num);
		}
		return source;
	}

	private string GetBackSub(string source, string search)
	{
		_ = string.Empty;
		int num = source.IndexOf(search);
		if (num >= 0)
		{
			return source.Remove(0, num + search.Length);
		}
		return source;
	}

	private bool IsIPAddress(string IPAddr)
	{
		string pattern = "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$";
		return Regex.IsMatch(IPAddr, pattern);
	}

	private bool IsHostMachine(string server)
	{
		if (string.IsNullOrEmpty(server))
		{
			return true;
		}
		string frontSub = GetFrontSub(server, "\\");
		frontSub = GetFrontSub(frontSub, "/");
		frontSub = GetFrontSub(frontSub, ",").ToUpper();
		if (frontSub == "127.0.0.1")
		{
			return true;
		}
		string text = Environment.MachineName.ToUpper();
		if (IsIPAddress(frontSub))
		{
			IPAddress[] addressList = Dns.GetHostEntry(text).AddressList;
			IPAddress[] array = addressList;
			foreach (IPAddress iPAddress in array)
			{
				if (iPAddress.ToString() == frontSub)
				{
					return true;
				}
			}
			return false;
		}
		if (frontSub == ".")
		{
			return true;
		}
		return frontSub.Equals(text);
	}

	private bool TextCheck()
	{
		string text = txtSN.Text.Trim();
		if (text == "")
		{
			text = string.Format((string)m_htab["ChkNull"], label5.Text.Trim().Substring(0, label5.Text.Trim().Length - 1));
			Program.MsgBox(text, (string)m_htab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		text = txtDB.Text.Trim();
		if (text == "")
		{
			text = string.Format((string)m_htab["ChkNull"], label11.Text.Trim().Substring(0, label11.Text.Trim().Length - 1));
			Program.MsgBox(text, (string)m_htab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		text = txtCDBPwd.Text.Trim();
		if (text == "")
		{
			text = string.Format((string)m_htab["ChkNull"], label1.Text.Trim().Substring(0, label1.Text.Trim().Length - 1));
			Program.MsgBox(text, (string)m_htab["InfoTitle"], MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return false;
		}
		if (text != "!Developer$$")
		{
			Program.MsgBox((string)m_htab["Info15"], btnCrDB.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		return true;
	}

	private void btnCrDB_Click(object sender, EventArgs e)
	{
		try
		{
			if (!TextCheck())
			{
				return;
			}
			if (!IsHostMachine(txtSN.Text.Trim()))
			{
				MessageBox.Show((string)m_htab["Info22"]);
				return;
			}
			SQLserver.SetConnType(1);
			SQLserver.SetDebug(Program.m_defDebug == "yes", Program.m_AppPath);
			string text = "";
			string text2 = "";
			string text3 = "";
			if (Directory.Exists(Program.m_defDBPath))
			{
				OpenFile.Filter = "Mdf Files|*.mdf";
				if (OpenFile.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				text = OpenFile.FileName;
				text3 = text;
			}
			else
			{
				SaFile.Filter = "Mdf Files|*.mdf";
				SaFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				SaFile.FileName = Path.GetFileNameWithoutExtension(txtDB.Text.Trim());
				if (SaFile.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				text2 = SaFile.FileName;
				text3 = text2;
				if (File.Exists(text2))
				{
					MessageBox.Show((string)m_htab["Info21"]);
					return;
				}
				text = Program.m_AppPath + "\\DB.Bak";
			}
			if (!string.IsNullOrEmpty(txtDB.Text.Trim()))
			{
				Program.m_SqlDN = txtDB.Text.Trim();
			}
			else
			{
				Program.m_SqlDN = Path.GetFileNameWithoutExtension(text3);
			}
			string text4 = label5.Text + " " + txtSN.Text.Trim();
			string text5 = text4;
			text4 = text5 + "\r\n\r\n" + (string)m_htab["Info16"] + " " + Program.m_SqlDN.Trim();
			text4 = text4 + "\r\n\r\n" + string.Format((string)m_htab["Info17"], "\r\n\r\n");
			if (Program.MsgBox(text4, btnCrDB.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.No)
			{
				CreatDatabase(text, text2, text3);
			}
		}
		catch (Exception ex)
		{
			string msg = btnCrDB.Text + (string)m_htab["Info20"] + "\r\n" + ex.Message;
			Program.MsgBox(msg, btnCrDB.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void CreatDatabase(string sourcepath, string purpsepath, string savepath)
	{
		ThreadPool.QueueUserWorkItem(delegate
		{
			try
			{
				isCreating = true;
				long num = SQLserver.DataBase_Create(txtSN.Text.Trim(), Program.m_SqlDN, txtSUN.Text.Trim(), txtSPwd.Text.Trim(), sourcepath, purpsepath);
				try
				{
					Invoke((EventHandler)delegate
					{
						picWait.Visible = false;
					});
				}
				catch
				{
				}
				if (num != 1)
				{
					Program.MsgBox((string)m_htab["Info18"], btnCrDB.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				Program.m_defDBPath = Path.GetDirectoryName(savepath);
				if (Program.firstRun == "0")
				{
					Program.firstRun = "1";
				}
				Program.SaveConfig();
				Program.MsgBox((string)m_htab["Info19"], btnCrDB.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Program.m_Exit = true;
				Program.mutex.Close();
				try
				{
					Invoke((EventHandler)delegate
					{
						Application.Exit();
						Application.Restart();
					});
				}
				catch
				{
				}
			}
			catch (Exception ex)
			{
				string msg = btnCrDB.Text + (string)m_htab["Info20"] + "\r\n" + ex.Message;
				Program.MsgBox(msg, btnCrDB.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				isCreating = false;
			}
		});
	}

	private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
	{
		Dispose();
	}

	private void cobUID_TextChanged(object sender, EventArgs e)
	{
		if (chkRem.Checked)
		{
			chkRem.Checked = false;
			txtPwd.Text = "";
		}
	}

	private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkPWDInput(sender, e);
	}

	private string GetSqlString(string name, string key, string value)
	{
		string text = "'Software\\Microsoft\\MSSQLServer\\MSSQLServer" + name + "'";
		return "EXEC xp_instance_regread 'HKEY_LOCAL_MACHINE'," + text + "," + key + ", @result output \nIF @result <> " + value + "\nBEGIN \nEXEC xp_instance_regwrite 'HKEY_LOCAL_MACHINE', " + text + "," + key + ", REG_DWORD, " + value + " \nEND \n";
	}

	private string SetSqlPort()
	{
		string text = "'Software\\Microsoft\\MSSQLServer\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IPAll'";
		return "DECLARE @Value varchar(255) \nEXEC xp_instance_regread 'HKEY_LOCAL_MACHINE'," + text + ",'TcpPort', @Value output \nIF @Value = '0' or @Value = '' \nBEGIN \nEXEC xp_instance_regwrite 'HKEY_LOCAL_MACHINE', " + text + ",'TcpPort', REG_SZ, '1433' \nEND \n";
	}

	private string GetPipeName()
	{
		string text = "'Software\\Microsoft\\MSSQLServer\\MSSQLServer\\SuperSocketNetLib\\Np'";
		return "EXEC xp_instance_regread 'HKEY_LOCAL_MACHINE'," + text + ",'PipeName', @Value output \nSELECT @Value PipeName \n";
	}

	private void btnSaEnabled_Click(object sender, EventArgs e)
	{
		try
		{
			SQLserver.SetDebug(Program.m_defDebug == "yes", Program.m_AppPath);
			SQLserver.WriteLog("");
			string text = "server=" + txtSN.Text.Trim() + ";database=master;Integrated Security=True;Connect Timeout=6";
			SQLserver.WriteLog(text);
			SqlConnection sqlConnection = new SqlConnection(text);
			if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Open();
			}
			SQLserver.WriteLog("Connect OK");
			string text2 = "DECLARE @result int " + GetSqlString("", "'LoginMode'", "2") + GetSqlString("\\SuperSocketNetLib\\Np", "'Enabled'", "1") + GetSqlString("\\SuperSocketNetLib\\Tcp", "'Enabled'", "1") + SetSqlPort() + "Alter LOGIN sa ENABLE \nAlter LOGIN sa WITH PASSWORD = '" + txtSPwd.Text.Trim() + "' \n " + GetPipeName();
			SQLserver.WriteLog(text2);
			DataSet dataSet = SQLserver.Data_GetDataSet(sqlConnection, text2);
			if (dataSet.Tables.Count <= 0)
			{
				Program.MsgBox((string)m_htab["Info04"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			SQLserver.WriteLog("Alter Reg OK");
			string source = dataSet.Tables[0].Rows[0]["PipeName"].ToString().Trim();
			source = GetFrontSub(source, "\\sql\\");
			source = GetBackSub(source, "\\pipe\\");
			SQLserver.WriteLog("PipeName=" + source);
			string text3 = Program.m_AppPath + "\\RestartSql.bat";
			StreamWriter streamWriter = new StreamWriter(text3, append: false, Encoding.Default);
			string value = "@echo off \nnet stop " + source + " \nnet start " + source + " \necho. & pause \n";
			streamWriter.Write(value);
			streamWriter.Flush();
			streamWriter.Close();
			streamWriter = null;
			Process process = new Process();
			process.StartInfo.CreateNoWindow = false;
			process.StartInfo.FileName = text3;
			process.Start();
			process.WaitForExit();
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message ?? "", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void frmLogin_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.Control && e.KeyCode == Keys.R)
		{
			if (Program.IsAdministrator())
			{
				MessageBox.Show("It's admin");
			}
			else
			{
				MessageBox.Show("It's not admin");
			}
		}
	}

	private void picWait_VisibleChanged(object sender, EventArgs e)
	{
		if (picWait.Visible)
		{
			picWait.Width = 50;
			picWait.Height = 50;
			picWait.Left = (base.Width - picWait.Width) / 2;
			picWait.Top = (base.Height - picWait.Height) / 2;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmLogin));
		this.SaFile = new System.Windows.Forms.SaveFileDialog();
		this.OpenFile = new System.Windows.Forms.OpenFileDialog();
		this.bpMain = new LockSoftware.Controls.clsBackPanel(this.components);
		this.panTop = new System.Windows.Forms.Panel();
		this.tbtnClose = new LockSoftware.Controls.ToolsBtn(this.components);
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.gbEnter = new LockSoftware.Controls.GlassBtn(this.components);
		this.clsBackPanel3 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.chkRem = new System.Windows.Forms.CheckBox();
		this.cobUID = new System.Windows.Forms.ComboBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.txtPwd = new System.Windows.Forms.TextBox();
		this.labID = new System.Windows.Forms.Label();
		this.txtUN = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.labTitle = new System.Windows.Forms.Label();
		this.gbSysSet = new LockSoftware.Controls.GlassBtn(this.components);
		this.labVer = new System.Windows.Forms.Label();
		this.gbClose = new LockSoftware.Controls.GlassBtn(this.components);
		this.plBottom = new System.Windows.Forms.Panel();
		this.btnDefault = new LockSoftware.Controls.GlassBtn(this.components);
		this.txtDB = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtCDBPwd = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCrDB = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnSevr = new LockSoftware.Controls.GlassBtn(this.components);
		this.cobBaud = new System.Windows.Forms.ComboBox();
		this.cobLan = new System.Windows.Forms.ComboBox();
		this.labLan = new System.Windows.Forms.Label();
		this.cobSector = new System.Windows.Forms.ComboBox();
		this.gbSave = new LockSoftware.Controls.GlassBtn(this.components);
		this.label9 = new System.Windows.Forms.Label();
		this.cobCOM = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.label8 = new System.Windows.Forms.Label();
		this.txtSN = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.label4 = new System.Windows.Forms.Label();
		this.labBaud = new System.Windows.Forms.Label();
		this.txtSPwd = new System.Windows.Forms.TextBox();
		this.txtSUN = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.picWait = new System.Windows.Forms.PictureBox();
		this.bpMain.SuspendLayout();
		this.panTop.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		this.clsBackPanel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.plBottom.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picWait).BeginInit();
		base.SuspendLayout();
		this.bpMain.Border = false;
		this.bpMain.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.bpMain.BorderBW = 1;
		this.bpMain.BorderColorBottom = System.Drawing.Color.Gray;
		this.bpMain.BorderColorLeft = System.Drawing.Color.Gray;
		this.bpMain.BorderColorRight = System.Drawing.Color.Gray;
		this.bpMain.BorderColorTop = System.Drawing.Color.Gray;
		this.bpMain.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.bpMain.BorderLW = 1;
		this.bpMain.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.bpMain.BorderRW = 1;
		this.bpMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.bpMain.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.bpMain.BorderTW = 1;
		this.bpMain.Color1 = System.Drawing.Color.LightSlateGray;
		this.bpMain.Color2 = System.Drawing.Color.SlateGray;
		this.bpMain.ColorAngle = 90f;
		this.bpMain.Controls.Add(this.panTop);
		this.bpMain.Controls.Add(this.plBottom);
		this.bpMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.bpMain.Location = new System.Drawing.Point(0, 0);
		this.bpMain.Name = "bpMain";
		this.bpMain.Size = new System.Drawing.Size(425, 453);
		this.bpMain.TabIndex = 0;
		this.bpMain.MouseDown += new System.Windows.Forms.MouseEventHandler(bpMain_MouseDown);
		this.bpMain.MouseMove += new System.Windows.Forms.MouseEventHandler(bpMain_MouseMove);
		this.panTop.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panTop.BackColor = System.Drawing.Color.Transparent;
		this.panTop.Controls.Add(this.tbtnClose);
		this.panTop.Controls.Add(this.pictureBox2);
		this.panTop.Controls.Add(this.gbEnter);
		this.panTop.Controls.Add(this.clsBackPanel3);
		this.panTop.Controls.Add(this.labTitle);
		this.panTop.Controls.Add(this.gbSysSet);
		this.panTop.Controls.Add(this.labVer);
		this.panTop.Controls.Add(this.gbClose);
		this.panTop.Location = new System.Drawing.Point(2, 2);
		this.panTop.Margin = new System.Windows.Forms.Padding(2);
		this.panTop.Name = "panTop";
		this.panTop.Size = new System.Drawing.Size(415, 235);
		this.panTop.TabIndex = 11;
		this.panTop.MouseDown += new System.Windows.Forms.MouseEventHandler(panTop_MouseDown);
		this.panTop.MouseMove += new System.Windows.Forms.MouseEventHandler(panTop_MouseMove);
		this.tbtnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.tbtnClose.BackColor = System.Drawing.Color.Transparent;
		this.tbtnClose.Checked = false;
		this.tbtnClose.DefaultBorderColor = System.Drawing.Color.Transparent;
		this.tbtnClose.DefaultColor = System.Drawing.Color.Transparent;
		this.tbtnClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.tbtnClose.ImageNew = LockSoftware.Properties.Resources.close;
		this.tbtnClose.ImageRedrawed = true;
		this.tbtnClose.ImageStyle = 0;
		this.tbtnClose.isButton = true;
		this.tbtnClose.Location = new System.Drawing.Point(393, 0);
		this.tbtnClose.MouseDownBorderColor = System.Drawing.Color.SteelBlue;
		this.tbtnClose.MouseDownEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.tbtnClose.MouseDownStartColor = System.Drawing.Color.White;
		this.tbtnClose.MouseEnterBorderColor = System.Drawing.Color.SteelBlue;
		this.tbtnClose.MouseEnterEndColor = System.Drawing.Color.FromArgb(179, 210, 254);
		this.tbtnClose.MouseEnterStartColor = System.Drawing.Color.White;
		this.tbtnClose.Name = "tbtnClose";
		this.tbtnClose.Size = new System.Drawing.Size(22, 22);
		this.tbtnClose.TabIndex = 5;
		this.tbtnClose.TextImageLocation = 0;
		this.tbtnClose.TextNew = "";
		this.tbtnClose.TextRedrawed = false;
		this.tbtnClose.Click += new System.EventHandler(tbtnClose_Click);
		this.pictureBox2.BackColor = System.Drawing.Color.LightSlateGray;
		this.pictureBox2.Location = new System.Drawing.Point(8, 8);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(60, 50);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 8;
		this.pictureBox2.TabStop = false;
		this.gbEnter.BackColor = System.Drawing.Color.White;
		this.gbEnter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.gbEnter.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.gbEnter.ForeColor = System.Drawing.Color.Black;
		this.gbEnter.GlowColor = System.Drawing.Color.White;
		this.gbEnter.GuidInfo = "&56~01'][Manson]v%#@";
		this.gbEnter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.gbEnter.InnerBorderColor = System.Drawing.Color.Transparent;
		this.gbEnter.Location = new System.Drawing.Point(291, 200);
		this.gbEnter.Name = "gbEnter";
		this.gbEnter.OuterBorderColor = System.Drawing.Color.WhiteSmoke;
		this.gbEnter.Size = new System.Drawing.Size(96, 25);
		this.gbEnter.TabIndex = 8;
		this.gbEnter.Text = "Login";
		this.gbEnter.Click += new System.EventHandler(gbEnter_Click);
		this.clsBackPanel3.Border = true;
		this.clsBackPanel3.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderBW = 1;
		this.clsBackPanel3.BorderColorBottom = System.Drawing.Color.LightSteelBlue;
		this.clsBackPanel3.BorderColorLeft = System.Drawing.Color.SteelBlue;
		this.clsBackPanel3.BorderColorRight = System.Drawing.Color.LightSteelBlue;
		this.clsBackPanel3.BorderColorTop = System.Drawing.Color.SteelBlue;
		this.clsBackPanel3.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderLW = 1;
		this.clsBackPanel3.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderRW = 1;
		this.clsBackPanel3.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel3.BorderTW = 1;
		this.clsBackPanel3.Color1 = System.Drawing.Color.White;
		this.clsBackPanel3.Color2 = System.Drawing.Color.Gainsboro;
		this.clsBackPanel3.ColorAngle = 125f;
		this.clsBackPanel3.Controls.Add(this.chkRem);
		this.clsBackPanel3.Controls.Add(this.cobUID);
		this.clsBackPanel3.Controls.Add(this.pictureBox1);
		this.clsBackPanel3.Controls.Add(this.txtPwd);
		this.clsBackPanel3.Controls.Add(this.labID);
		this.clsBackPanel3.Controls.Add(this.txtUN);
		this.clsBackPanel3.Controls.Add(this.label2);
		this.clsBackPanel3.Controls.Add(this.label3);
		this.clsBackPanel3.Location = new System.Drawing.Point(8, 62);
		this.clsBackPanel3.Name = "clsBackPanel3";
		this.clsBackPanel3.Size = new System.Drawing.Size(400, 132);
		this.clsBackPanel3.TabIndex = 10;
		this.chkRem.AutoSize = true;
		this.chkRem.BackColor = System.Drawing.Color.Transparent;
		this.chkRem.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.chkRem.Location = new System.Drawing.Point(140, 102);
		this.chkRem.Name = "chkRem";
		this.chkRem.Size = new System.Drawing.Size(103, 19);
		this.chkRem.TabIndex = 7;
		this.chkRem.Text = "Remember me...";
		this.chkRem.UseVisualStyleBackColor = false;
		this.cobUID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cobUID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cobUID.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.cobUID.FormattingEnabled = true;
		this.cobUID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cobUID.Location = new System.Drawing.Point(223, 16);
		this.cobUID.Name = "cobUID";
		this.cobUID.Size = new System.Drawing.Size(117, 24);
		this.cobUID.TabIndex = 3;
		this.cobUID.TextChanged += new System.EventHandler(cobUID_TextChanged);
		this.cobUID.KeyDown += new System.Windows.Forms.KeyEventHandler(cobUID_KeyDown);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.BackgroundImage = LockSoftware.Properties.Resources.HotelLock;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(120, 120);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 6;
		this.pictureBox1.TabStop = false;
		this.txtPwd.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtPwd.Location = new System.Drawing.Point(223, 67);
		this.txtPwd.MaxLength = 128;
		this.txtPwd.Name = "txtPwd";
		this.txtPwd.PasswordChar = '*';
		this.txtPwd.Size = new System.Drawing.Size(117, 24);
		this.txtPwd.TabIndex = 5;
		this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPwd_KeyDown);
		this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPwd_KeyPress);
		this.labID.BackColor = System.Drawing.Color.Transparent;
		this.labID.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labID.ForeColor = System.Drawing.Color.Black;
		this.labID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labID.Location = new System.Drawing.Point(121, 10);
		this.labID.Name = "labID";
		this.labID.Size = new System.Drawing.Size(101, 32);
		this.labID.TabIndex = 0;
		this.labID.Text = "LoginName:";
		this.labID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtUN.BackColor = System.Drawing.Color.White;
		this.txtUN.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.txtUN.Location = new System.Drawing.Point(223, 42);
		this.txtUN.Name = "txtUN";
		this.txtUN.ReadOnly = true;
		this.txtUN.Size = new System.Drawing.Size(117, 24);
		this.txtUN.TabIndex = 4;
		this.txtUN.Visible = false;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.Location = new System.Drawing.Point(121, 37);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(101, 32);
		this.label2.TabIndex = 1;
		this.label2.Text = "Username:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.Visible = false;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12f);
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.Location = new System.Drawing.Point(121, 61);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(101, 32);
		this.label3.TabIndex = 2;
		this.label3.Text = "Passwordfg:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labTitle.AutoSize = true;
		this.labTitle.BackColor = System.Drawing.Color.Transparent;
		this.labTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
		this.labTitle.ForeColor = System.Drawing.Color.White;
		this.labTitle.Location = new System.Drawing.Point(70, 25);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(80, 20);
		this.labTitle.TabIndex = 2;
		this.labTitle.Text = "ZKBiolock";
		this.labTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(labTitle_MouseDown);
		this.labTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(labTitle_MouseMove);
		this.gbSysSet.BackColor = System.Drawing.Color.White;
		this.gbSysSet.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.gbSysSet.ForeColor = System.Drawing.Color.Black;
		this.gbSysSet.GlowColor = System.Drawing.Color.White;
		this.gbSysSet.GuidInfo = "&56~01'][Manson]v%#@";
		this.gbSysSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.gbSysSet.InnerBorderColor = System.Drawing.Color.Transparent;
		this.gbSysSet.Location = new System.Drawing.Point(35, 200);
		this.gbSysSet.Name = "gbSysSet";
		this.gbSysSet.OuterBorderColor = System.Drawing.Color.WhiteSmoke;
		this.gbSysSet.Size = new System.Drawing.Size(96, 25);
		this.gbSysSet.TabIndex = 6;
		this.gbSysSet.Text = "Setting↓↑";
		this.gbSysSet.Click += new System.EventHandler(gbSysSet_Click);
		this.labVer.AutoSize = true;
		this.labVer.BackColor = System.Drawing.Color.Transparent;
		this.labVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f);
		this.labVer.ForeColor = System.Drawing.Color.White;
		this.labVer.Location = new System.Drawing.Point(74, 34);
		this.labVer.Name = "labVer";
		this.labVer.Size = new System.Drawing.Size(40, 24);
		this.labVer.TabIndex = 4;
		this.labVer.Text = "Ver";
		this.labVer.Visible = false;
		this.labVer.MouseDown += new System.Windows.Forms.MouseEventHandler(labVer_MouseDown);
		this.labVer.MouseMove += new System.Windows.Forms.MouseEventHandler(labVer_MouseMove);
		this.gbClose.BackColor = System.Drawing.Color.White;
		this.gbClose.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.gbClose.ForeColor = System.Drawing.Color.Black;
		this.gbClose.GlowColor = System.Drawing.Color.White;
		this.gbClose.GuidInfo = "&56~01'][Manson]v%#@";
		this.gbClose.InnerBorderColor = System.Drawing.Color.Transparent;
		this.gbClose.Location = new System.Drawing.Point(137, 200);
		this.gbClose.Name = "gbClose";
		this.gbClose.OuterBorderColor = System.Drawing.Color.WhiteSmoke;
		this.gbClose.Size = new System.Drawing.Size(96, 25);
		this.gbClose.TabIndex = 7;
		this.gbClose.Text = "Exit";
		this.gbClose.Click += new System.EventHandler(gbClose_Click);
		this.plBottom.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.plBottom.BackColor = System.Drawing.Color.White;
		this.plBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.plBottom.Controls.Add(this.btnDefault);
		this.plBottom.Controls.Add(this.txtDB);
		this.plBottom.Controls.Add(this.label11);
		this.plBottom.Controls.Add(this.txtCDBPwd);
		this.plBottom.Controls.Add(this.label1);
		this.plBottom.Controls.Add(this.btnCrDB);
		this.plBottom.Controls.Add(this.btnSevr);
		this.plBottom.Controls.Add(this.cobBaud);
		this.plBottom.Controls.Add(this.cobLan);
		this.plBottom.Controls.Add(this.labLan);
		this.plBottom.Controls.Add(this.cobSector);
		this.plBottom.Controls.Add(this.gbSave);
		this.plBottom.Controls.Add(this.label9);
		this.plBottom.Controls.Add(this.cobCOM);
		this.plBottom.Controls.Add(this.label10);
		this.plBottom.Controls.Add(this.clsBackPanel1);
		this.plBottom.Controls.Add(this.label8);
		this.plBottom.Controls.Add(this.txtSN);
		this.plBottom.Controls.Add(this.label5);
		this.plBottom.Controls.Add(this.clsBackPanel2);
		this.plBottom.Controls.Add(this.label4);
		this.plBottom.Controls.Add(this.labBaud);
		this.plBottom.Controls.Add(this.txtSPwd);
		this.plBottom.Controls.Add(this.txtSUN);
		this.plBottom.Controls.Add(this.label7);
		this.plBottom.Controls.Add(this.label6);
		this.plBottom.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.plBottom.Location = new System.Drawing.Point(2, 246);
		this.plBottom.Margin = new System.Windows.Forms.Padding(2);
		this.plBottom.Name = "plBottom";
		this.plBottom.Size = new System.Drawing.Size(415, 200);
		this.plBottom.TabIndex = 9;
		this.plBottom.VisibleChanged += new System.EventHandler(plBottom_VisibleChanged);
		this.btnDefault.BackColor = System.Drawing.Color.Silver;
		this.btnDefault.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.btnDefault.ForeColor = System.Drawing.Color.Black;
		this.btnDefault.GlowColor = System.Drawing.Color.White;
		this.btnDefault.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnDefault.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnDefault.InnerBorderColor = System.Drawing.Color.Gray;
		this.btnDefault.Location = new System.Drawing.Point(203, 121);
		this.btnDefault.Name = "btnDefault";
		this.btnDefault.OuterBorderColor = System.Drawing.Color.Gainsboro;
		this.btnDefault.Size = new System.Drawing.Size(180, 28);
		this.btnDefault.TabIndex = 57;
		this.btnDefault.Text = "SQL default setting";
		this.btnDefault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnDefault.Click += new System.EventHandler(btnSaEnabled_Click);
		this.txtDB.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.txtDB.Location = new System.Drawing.Point(90, 123);
		this.txtDB.Name = "txtDB";
		this.txtDB.Size = new System.Drawing.Size(108, 21);
		this.txtDB.TabIndex = 55;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label11.Location = new System.Drawing.Point(12, 126);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(56, 15);
		this.label11.TabIndex = 56;
		this.label11.Text = "DataBase:";
		this.txtCDBPwd.Enabled = false;
		this.txtCDBPwd.Location = new System.Drawing.Point(90, 155);
		this.txtCDBPwd.Name = "txtCDBPwd";
		this.txtCDBPwd.PasswordChar = '*';
		this.txtCDBPwd.Size = new System.Drawing.Size(107, 21);
		this.txtCDBPwd.TabIndex = 54;
		this.txtCDBPwd.Text = "!Developer$$";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label1.Location = new System.Drawing.Point(12, 158);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(57, 15);
		this.label1.TabIndex = 53;
		this.label1.Text = "Password:";
		this.btnCrDB.BackColor = System.Drawing.Color.Gainsboro;
		this.btnCrDB.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.btnCrDB.ForeColor = System.Drawing.Color.Black;
		this.btnCrDB.GlowColor = System.Drawing.Color.White;
		this.btnCrDB.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnCrDB.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnCrDB.InnerBorderColor = System.Drawing.Color.DimGray;
		this.btnCrDB.Location = new System.Drawing.Point(203, 153);
		this.btnCrDB.Name = "btnCrDB";
		this.btnCrDB.OuterBorderColor = System.Drawing.Color.Silver;
		this.btnCrDB.Size = new System.Drawing.Size(180, 28);
		this.btnCrDB.TabIndex = 52;
		this.btnCrDB.Text = "Create Database";
		this.btnCrDB.Click += new System.EventHandler(btnCrDB_Click);
		this.btnSevr.BackColor = System.Drawing.Color.Silver;
		this.btnSevr.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.btnSevr.ForeColor = System.Drawing.Color.Black;
		this.btnSevr.GlowColor = System.Drawing.Color.White;
		this.btnSevr.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnSevr.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnSevr.InnerBorderColor = System.Drawing.Color.Gray;
		this.btnSevr.Location = new System.Drawing.Point(301, 61);
		this.btnSevr.Name = "btnSevr";
		this.btnSevr.OuterBorderColor = System.Drawing.Color.Gainsboro;
		this.btnSevr.Size = new System.Drawing.Size(82, 28);
		this.btnSevr.TabIndex = 51;
		this.btnSevr.Text = "...";
		this.btnSevr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSevr.Click += new System.EventHandler(btnSevr_Click);
		this.cobBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobBaud.FormattingEnabled = true;
		this.cobBaud.Items.AddRange(new object[5] { "9600", "19200", "38400", "57600", "115200" });
		this.cobBaud.Location = new System.Drawing.Point(173, 229);
		this.cobBaud.Name = "cobBaud";
		this.cobBaud.Size = new System.Drawing.Size(65, 23);
		this.cobBaud.TabIndex = 6;
		this.cobBaud.Visible = false;
		this.cobLan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobLan.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.cobLan.FormattingEnabled = true;
		this.cobLan.Items.AddRange(new object[3] { "English-EN", "简体中文-CN", "繁體中文-TC" });
		this.cobLan.Location = new System.Drawing.Point(90, 7);
		this.cobLan.Name = "cobLan";
		this.cobLan.Size = new System.Drawing.Size(205, 23);
		this.cobLan.TabIndex = 1;
		this.cobLan.SelectedIndexChanged += new System.EventHandler(cobLan_SelectedIndexChanged);
		this.labLan.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.labLan.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.labLan.Location = new System.Drawing.Point(3, 3);
		this.labLan.Name = "labLan";
		this.labLan.Size = new System.Drawing.Size(81, 28);
		this.labLan.TabIndex = 48;
		this.labLan.Text = "Language:";
		this.labLan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.cobSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobSector.FormattingEnabled = true;
		this.cobSector.Items.AddRange(new object[16]
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10", "11", "12", "13", "14", "15"
		});
		this.cobSector.Location = new System.Drawing.Point(288, 227);
		this.cobSector.Name = "cobSector";
		this.cobSector.Size = new System.Drawing.Size(49, 23);
		this.cobSector.TabIndex = 7;
		this.cobSector.Visible = false;
		this.gbSave.BackColor = System.Drawing.Color.LightGray;
		this.gbSave.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.gbSave.ForeColor = System.Drawing.Color.Black;
		this.gbSave.GlowColor = System.Drawing.Color.White;
		this.gbSave.GuidInfo = "&56~01'][Manson]v%#@";
		this.gbSave.Image = LockSoftware.Properties.Resources.save;
		this.gbSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.gbSave.InnerBorderColor = System.Drawing.Color.DimGray;
		this.gbSave.Location = new System.Drawing.Point(301, 3);
		this.gbSave.Name = "gbSave";
		this.gbSave.OuterBorderColor = System.Drawing.Color.Silver;
		this.gbSave.Size = new System.Drawing.Size(82, 28);
		this.gbSave.TabIndex = 8;
		this.gbSave.Text = "Save";
		this.gbSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.gbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.gbSave.Click += new System.EventHandler(gbSave_Click);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label9.Location = new System.Drawing.Point(247, 235);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(33, 15);
		this.label9.TabIndex = 44;
		this.label9.Text = "Card:";
		this.label9.Visible = false;
		this.cobCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCOM.FormattingEnabled = true;
		this.cobCOM.Items.AddRange(new object[16]
		{
			"USB", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
			"COM10", "COM11", "COM12", "COM13", "COM14", "COM15"
		});
		this.cobCOM.Location = new System.Drawing.Point(68, 228);
		this.cobCOM.Name = "cobCOM";
		this.cobCOM.Size = new System.Drawing.Size(50, 23);
		this.cobCOM.TabIndex = 5;
		this.cobCOM.Visible = false;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label10.Location = new System.Drawing.Point(15, 232);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(43, 15);
		this.label10.TabIndex = 42;
		this.label10.Text = "Reader:";
		this.label10.Visible = false;
		this.clsBackPanel1.Border = false;
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
		this.clsBackPanel1.Color2 = System.Drawing.Color.SlateGray;
		this.clsBackPanel1.ColorAngle = 135f;
		this.clsBackPanel1.Location = new System.Drawing.Point(90, 216);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(242, 1);
		this.clsBackPanel1.TabIndex = 41;
		this.clsBackPanel1.Visible = false;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label8.ForeColor = System.Drawing.Color.SlateGray;
		this.label8.Location = new System.Drawing.Point(8, 209);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(63, 16);
		this.label8.TabIndex = 40;
		this.label8.Text = "Hardware";
		this.label8.Visible = false;
		this.txtSN.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.txtSN.Location = new System.Drawing.Point(90, 65);
		this.txtSN.Name = "txtSN";
		this.txtSN.Size = new System.Drawing.Size(205, 21);
		this.txtSN.TabIndex = 2;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label5.Location = new System.Drawing.Point(12, 68);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(40, 15);
		this.label5.TabIndex = 34;
		this.label5.Text = "Server:";
		this.clsBackPanel2.Border = false;
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
		this.clsBackPanel2.Color2 = System.Drawing.Color.SlateGray;
		this.clsBackPanel2.ColorAngle = 135f;
		this.clsBackPanel2.Location = new System.Drawing.Point(90, 48);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(293, 1);
		this.clsBackPanel2.TabIndex = 33;
		this.label4.Font = new System.Drawing.Font("Times New Roman", 10.5f);
		this.label4.ForeColor = System.Drawing.Color.SlateGray;
		this.label4.Location = new System.Drawing.Point(3, 34);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(81, 28);
		this.label4.TabIndex = 32;
		this.label4.Text = "Database";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labBaud.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.labBaud.Location = new System.Drawing.Point(131, 233);
		this.labBaud.Name = "labBaud";
		this.labBaud.Size = new System.Drawing.Size(58, 12);
		this.labBaud.TabIndex = 50;
		this.labBaud.Text = "Baud:";
		this.labBaud.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.labBaud.Visible = false;
		this.txtSPwd.Location = new System.Drawing.Point(323, 94);
		this.txtSPwd.Name = "txtSPwd";
		this.txtSPwd.PasswordChar = '*';
		this.txtSPwd.Size = new System.Drawing.Size(60, 21);
		this.txtSPwd.TabIndex = 4;
		this.txtSUN.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.txtSUN.Location = new System.Drawing.Point(90, 94);
		this.txtSUN.Name = "txtSUN";
		this.txtSUN.Size = new System.Drawing.Size(60, 21);
		this.txtSUN.TabIndex = 3;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label7.Location = new System.Drawing.Point(200, 97);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 15);
		this.label7.TabIndex = 36;
		this.label7.Text = "Password:";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Times New Roman", 9f);
		this.label6.Location = new System.Drawing.Point(12, 97);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(33, 15);
		this.label6.TabIndex = 35;
		this.label6.Text = "User:";
		this.picWait.BackColor = System.Drawing.Color.Transparent;
		this.picWait.Image = LockSoftware.Properties.Resources.loadpage;
		this.picWait.InitialImage = null;
		this.picWait.Location = new System.Drawing.Point(194, 208);
		this.picWait.Margin = new System.Windows.Forms.Padding(0);
		this.picWait.Name = "picWait";
		this.picWait.Size = new System.Drawing.Size(38, 38);
		this.picWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.picWait.TabIndex = 14;
		this.picWait.TabStop = false;
		this.picWait.Visible = false;
		this.picWait.VisibleChanged += new System.EventHandler(picWait_VisibleChanged);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(425, 453);
		base.Controls.Add(this.picWait);
		base.Controls.Add(this.bpMain);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmLogin";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "用户登录";
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmLogin_FormClosed);
		base.Load += new System.EventHandler(frmLogin_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmLogin_KeyDown);
		this.bpMain.ResumeLayout(false);
		this.panTop.ResumeLayout(false);
		this.panTop.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		this.clsBackPanel3.ResumeLayout(false);
		this.clsBackPanel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.plBottom.ResumeLayout(false);
		this.plBottom.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picWait).EndInit();
		base.ResumeLayout(false);
	}
}
