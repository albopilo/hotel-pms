using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using LockSoftware.Controls;
using LockSoftware.Properties;

namespace LockSoftware.Frm;

public class frmGCR : Form
{
	public DataTable dt;

	public DataTable ndt;

	public string m_objName = "WFgcr";

	public Hashtable m_htab;

	public int m_retst = -1;

	public double stayday;

	public int stayhour;

	public bool isforhour;

	public bool isfordis;

	public double ndp;

	public double nrp;

	public double nrples;

	public double m_discount;

	public double currrate = 1.0;

	public string basesurname = "";

	private DateTime dtnow = DateTime.Now;

	public int othhavhour;

	public int ptype;

	private DateTime comeday;

	private DateTime curday;

	private DateTime LeaveTime;

	private double rplesshour;

	private double rpstandhour;

	private double rp;

	private bool iteam;

	private double extrapay;

	private double mspay;

	private double havday0;

	private double havhour0;

	private double maypay0;

	private double havday1;

	private double havhour1;

	private double maypay1;

	private double havday2;

	private double havhour2;

	private double maypay2;

	private IContainer components;

	public Label labTxtMsg;

	private clsBackPanel clsBackPanel1;

	private clsBackPanel clsBackPanel2;

	public GlassBtn btnCl;

	public GlassBtn btnOK;

	private Label label18;

	private Label label17;

	private Label label16;

	private Label label12;

	private Label label14;

	private Label label13;

	private Label label10;

	private TableLayoutPanel tableLayoutPanel1;

	private Label label21;

	private Label label20;

	private TextBox texBotherpaid1;

	private Label label19;

	public Label label3;

	private PictureBox pictureBox1;

	private Label label9;

	private TextBox txtGPaid;

	public Label label2;

	private Label label8;

	public Label label1;

	public NumericUpDown nudDay;

	private Label label15;

	private Label label11;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label22;

	private TextBox texBotherpaid0;

	private RadioButton radBNew;

	public Label label23;

	private RadioButton radBOld;

	private CheckBox cheBBoth;

	private Panel panel2;

	private FlowLayoutPanel flowLayoutPanel1;

	public TextBox label4;

	public frmGCR()
	{
		base.StartPosition = FormStartPosition.CenterScreen;
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
	}

	private void frmGCR_Load(object sender, EventArgs e)
	{
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		label17.Text = basesurname;
		if (dt != null)
		{
			texBotherpaid0.Text = Program.changeValue(0.0, CultureInfo.CurrentCulture);
			comeday = Convert.ToDateTime(dt.Rows[0]["TR_cometime"].ToString());
			curday = Convert.ToDateTime(Program.GetStandDate(dtnow) + " " + Program.m_defLeaveTime + ":00");
			LeaveTime = Convert.ToDateTime(dt.Rows[0]["TR_stand_L_time"].ToString());
			rplesshour = Convert.ToDouble(dt.Rows[0]["tp_pricelesshour"]);
			rpstandhour = Convert.ToDouble(dt.Rows[0]["tp_pricestandhour"]);
			rp = Convert.ToDouble(dt.Rows[0]["tp_price"]);
			if (Convert.ToInt32(dt.Rows[0]["teamid"]) > 0)
			{
				iteam = true;
			}
			double num = 0.0;
			double num2 = 0.0;
			havedata havedata2 = new havedata();
			havedata2.comedate = comeday;
			havedata2.dtnow = dtnow;
			havedata2.isfordis = isfordis;
			havedata2.isforhour = isforhour;
			havedata2.m_discount = m_discount;
			havedata2.othhavhour = othhavhour;
			havedata2.ptype = ptype;
			havedata2.rp = rp;
			havedata2.rplesshour = rplesshour;
			havedata2.rpstandhour = rpstandhour;
			Program.getdat(havedata2);
			havday0 = havedata2.havday0;
			havday1 = havedata2.havday1;
			havday2 = havedata2.havday2;
			havhour0 = havedata2.havhour0;
			havhour1 = havedata2.havhour1;
			havhour2 = havedata2.havhour2;
			maypay0 = havedata2.maypay0;
			maypay1 = havedata2.maypay1;
			maypay2 = havedata2.maypay2;
			num = havday0;
			num2 = havhour0;
			if ((num >= stayday && stayday > 0.0) || (num2 >= (double)stayhour && stayhour > 0))
			{
				pictureBox1.Image = Resources.Warn01;
				label4.Text = (string)m_htab["Err01"];
				label4.ForeColor = Color.Red;
				nudDay.Enabled = false;
				btnOK.Enabled = false;
			}
			if (isforhour)
			{
				nudDay.Increment = 1m;
				nudDay.ValueChanged -= nudDay_ValueChanged;
				nudDay.Value = Convert.ToDecimal(num2);
				nudDay.ValueChanged += nudDay_ValueChanged;
				nudDay.Maximum = Convert.ToDecimal((num2 > (double)stayhour) ? num2 : ((double)stayhour));
				label1.Text = (string)m_htab["label1_1"];
				label10.Text = (string)m_htab["label10_1"];
				label5.Text = (string)m_htab["label5_1"];
				label6.Text = (string)m_htab["label6_1"];
			}
			else
			{
				nudDay.Increment = 0.5m;
				nudDay.ValueChanged -= nudDay_ValueChanged;
				nudDay.Value = Convert.ToDecimal(num);
				nudDay.ValueChanged += nudDay_ValueChanged;
				nudDay.Maximum = Convert.ToDecimal((num > stayday) ? num : stayday);
				label1.Text = (string)m_htab["label1"];
				label10.Text = (string)m_htab["label10"];
				label5.Text = (string)m_htab["label5"];
				label6.Text = (string)m_htab["label6"];
			}
			cheBBoth.Checked = true;
			nudDay_ValueChanged(new object(), new EventArgs());
			if (ptype == 1 && dtnow < ((comeday.TimeOfDay < TimeSpan.Parse(Program.m_defLeaveTime)) ? (comeday.Date + TimeSpan.Parse(Program.m_defLeaveTime)) : (comeday.Date.AddDays(1.0) + TimeSpan.Parse(Program.m_defLeaveTime))))
			{
				cheBBoth.Checked = false;
				radBOld.Checked = true;
				radBOld.Enabled = true;
				radBNew.Enabled = false;
				cheBBoth.Enabled = false;
				nudDay.Value = 0m;
			}
		}
	}

	private void getdat()
	{
		if (isforhour)
		{
			havday0 = 0.0;
			havday1 = 0.0;
			havday2 = 0.0;
			TimeSpan timeSpan;
			switch (ptype)
			{
			case 1:
				timeSpan = ((comeday.TimeOfDay.Minutes * 60 + comeday.TimeOfDay.Seconds <= TimeSpan.Parse(Program.m_defLeaveTime).Minutes * 60 + TimeSpan.Parse(Program.m_defLeaveTime).Seconds) ? new TimeSpan(comeday.TimeOfDay.Hours, TimeSpan.Parse(Program.m_defLeaveTime).Minutes, TimeSpan.Parse(Program.m_defLeaveTime).Seconds) : new TimeSpan(comeday.TimeOfDay.Hours + 1, TimeSpan.Parse(Program.m_defLeaveTime).Minutes, TimeSpan.Parse(Program.m_defLeaveTime).Seconds));
				havhour0 = (int)(dtnow - (comeday.Date + timeSpan)).TotalHours + 1;
				havhour1 = havhour0;
				havhour2 = havhour1 - 1.0;
				if (havhour2 < 0.0)
				{
					havhour2 = 0.0;
				}
				break;
			case 2:
				timeSpan = ((comeday.TimeOfDay.Minutes * 60 + comeday.TimeOfDay.Seconds <= TimeSpan.Parse(Program.m_defLeaveTime).Minutes * 60 + TimeSpan.Parse(Program.m_defLeaveTime).Seconds) ? new TimeSpan(comeday.TimeOfDay.Hours - 1, TimeSpan.Parse(Program.m_defLeaveTime).Minutes, TimeSpan.Parse(Program.m_defLeaveTime).Seconds) : new TimeSpan(comeday.TimeOfDay.Hours, TimeSpan.Parse(Program.m_defLeaveTime).Minutes, TimeSpan.Parse(Program.m_defLeaveTime).Seconds));
				havhour0 = (int)(dtnow - (comeday.Date + timeSpan)).TotalHours + 1;
				havhour1 = havhour0;
				havhour2 = havhour1 - 1.0;
				if (havhour2 < 0.0)
				{
					havhour2 = 0.0;
				}
				break;
			default:
				havhour0 = (int)(dtnow - comeday).TotalHours + 1;
				havhour1 = havhour0;
				havhour2 = havhour1 - 1.0;
				if (havhour2 < 0.0)
				{
					havhour2 = 0.0;
				}
				break;
			}
			if (isfordis || othhavhour >= Program.m_defHR)
			{
				maypay0 = havhour0 * rpstandhour * m_discount;
				maypay1 = havhour1 * rpstandhour * m_discount;
				maypay2 = havhour2 * rpstandhour * m_discount;
				return;
			}
			int num = Program.m_defHR - othhavhour;
			if (havhour0 < (double)num)
			{
				maypay0 = rplesshour * m_discount * havhour0;
			}
			else
			{
				maypay0 = rplesshour * m_discount * (double)num + (havhour0 - (double)num) * m_discount * rpstandhour;
			}
			if (havhour1 < (double)num)
			{
				maypay1 = rplesshour * m_discount * havhour1;
			}
			else
			{
				maypay1 = rplesshour * m_discount * (double)num + (havhour1 - (double)num) * m_discount * rpstandhour;
			}
			if (havhour2 < (double)num)
			{
				maypay2 = rplesshour * m_discount * havhour2;
			}
			else
			{
				maypay2 = rplesshour * m_discount * (double)num + (havhour2 - (double)num) * m_discount * rpstandhour;
			}
			return;
		}
		havhour0 = 0.0;
		havhour1 = 0.0;
		havhour2 = 0.0;
		havday0 = (int)(dtnow.Date - comeday.Date).TotalDays;
		havday1 = (int)(dtnow.Date - comeday.Date).TotalDays;
		havday2 = (int)(dtnow.Date - comeday.Date).TotalDays;
		if (dtnow.TimeOfDay >= TimeSpan.Parse(Program.m_defLeaveTime))
		{
			havday1++;
		}
		if (dtnow.TimeOfDay > TimeSpan.Parse(Program.m_defHalfDay))
		{
			havday0 += 0.5;
			havday1 += 0.5;
		}
		if (dtnow.TimeOfDay > TimeSpan.Parse(Program.m_defFullDay))
		{
			havday0 += 0.5;
			havday1 += 0.5;
		}
		switch (ptype)
		{
		case 1:
			if (comeday.TimeOfDay >= TimeSpan.Parse(Program.m_defLeaveTime))
			{
				havday0--;
				havday1--;
				havday2--;
			}
			if (havday0 < 0.0)
			{
				havday0 = 0.0;
			}
			if (havday1 < 0.0)
			{
				havday1 = 0.0;
			}
			if (havday2 < 0.0)
			{
				havday2 = 0.0;
			}
			break;
		case 2:
			if (comeday.TimeOfDay < TimeSpan.Parse(Program.m_defLeaveTime))
			{
				havday0++;
				havday1++;
				havday2++;
			}
			break;
		default:
			if (comeday.TimeOfDay < TimeSpan.Parse(Program.m_defComeTime) && (dtnow.Date > comeday.Date || dtnow.TimeOfDay > TimeSpan.Parse(Program.m_defComeTime)))
			{
				havday0++;
				havday1++;
				if (dtnow.Date > comeday.Date)
				{
					havday2++;
				}
			}
			break;
		}
		maypay0 = havday0 * rp * m_discount;
		maypay1 = havday1 * rp * m_discount;
		maypay2 = havday2 * rp * m_discount;
	}

	private void nudDay_ValueChanged(object sender, EventArgs e)
	{
		if ((int)(nudDay.Value * 10m) % 5 != 0)
		{
			nudDay.Value = (decimal)((double)(int)((double)nudDay.Value / 0.5) * 0.5);
		}
		NPrice();
	}

	private void txtGPaid_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
	}

	public void NPrice()
	{
		try
		{
			double num = Math.Round(Convert.ToDouble(nudDay.Value), 1);
			double num2 = 0.0;
			num2 = ((!isforhour) ? Math.Round((stayday - num < 0.0) ? 0.0 : (stayday - num), 1) : Math.Round(((double)stayhour - num < 0.0) ? 0.0 : ((double)stayhour - num), 0));
			label7.Text = num2.ToString();
			double num3 = Convert.ToDouble(dt.Rows[0]["TR_deposit"].ToString().Trim());
			double num4 = Convert.ToDouble(dt.Rows[0]["r_price"]) * m_discount;
			label8.Text = num3.ToString("F2");
			label11.Text = dt.Rows[0]["curr_code"].ToString().Trim();
			if (isforhour)
			{
				if (isfordis || othhavhour >= Program.m_defHR)
				{
					mspay = rpstandhour * m_discount * num;
				}
				else if (num + (double)othhavhour < (double)Program.m_defHR)
				{
					mspay = rplesshour * m_discount * num;
				}
				else
				{
					mspay = (rplesshour * (double)(Program.m_defHR - othhavhour) + rpstandhour * (num - (double)Program.m_defHR + (double)othhavhour)) * m_discount;
				}
			}
			else
			{
				mspay = rp * m_discount * num;
			}
			label14.Text = (num3 - mspay / currrate).ToString("F2");
			Label label = label15;
			string text = (label12.Text = dt.Rows[0]["curr_code"].ToString().Trim());
			label.Text = text;
			if (isforhour)
			{
				double num5 = (int)(LeaveTime - dtnow).TotalHours;
				double num6 = Convert.ToDouble(ndt.Rows[0]["TP_PricelessHour"]);
				if (num6 * m_discount > num4)
				{
					extrapay = (num6 * m_discount - num4) * ((num5 > 0.0) ? num5 : 0.0) / currrate;
				}
			}
			else
			{
				double num7 = Program.CountDay(dtnow, LeaveTime);
				if (nrp * m_discount > num4)
				{
					extrapay = (nrp * m_discount - num4) * num7 / currrate;
				}
			}
			txtGPaid.Text = extrapay.ToString("F2");
		}
		catch
		{
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			if (Program.MsgBox(label4.Text, (string)Program.m_hPubTab["InfoTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			double num = Math.Round(Convert.ToDouble(nudDay.Value), 1);
			double num2 = 0.0;
			double num3 = 0.0;
			if (cheBBoth.Checked)
			{
				num2 = havday0;
				num3 = havhour0;
			}
			else if (radBOld.Checked)
			{
				num2 = havday1;
				num3 = havhour1;
			}
			else if (radBNew.Checked)
			{
				num2 = havday2;
				num3 = havhour2;
			}
			bool flag = false;
			int num4 = Convert.ToInt32(dt.Rows[0]["teamid"]);
			if (num4 > 0)
			{
				flag = true;
			}
			string standDec = Program.GetStandDec(txtGPaid.Text.Trim());
			string standDec2 = Program.GetStandDec(texBotherpaid0.Text);
			string text = "";
			text = ((!isforhour) ? Program.GetStandDec(ndt.Rows[0]["TP_Price"].ToString()) : Program.GetStandDec(ndt.Rows[0]["TP_PricelessHour"].ToString()));
			string text2 = "Declare @_ID As bigint \n";
			text2 = text2 + "Insert Into T_Rooms Select g_id, TR_guestcount, TR_cardcount,0," + ndt.Rows[0]["R_ID"].ToString() + ",RS_ID";
			string text3 = text2;
			text2 = text3 + ", N'" + ndt.Rows[0]["R_Name"].ToString() + "', '" + ndt.Rows[0]["R_Code"].ToString() + "', " + ndt.Rows[0]["R_SubCode"].ToString() + ", " + text;
			string text4 = text2;
			text2 = text4 + ",TR_discount,TR_deposit+" + (flag ? "0" : standDec) + ",getdate(),TR_stayhour-" + Program.GetStandDec(num2) + ",TR_stand_L_time,TR_stayover,TR_SOLTime,TR_SOhour-" + Program.GetStandDec(num3) + ",";
			string text5 = text2;
			text2 = text5 + "TR_SOrp, " + standDec2 + ", TR_Level, 0,TR_actual_L_time," + Program.GetStandDec(ndp) + ",0,'',";
			text2 += " TR_basCurrid, TR_Bascurname, TR_basrate,curr_code,curr_rate,0,0,0,";
			object obj = text2;
			text2 = string.Concat(obj, " '", dt.Rows[0]["tr_id"].ToString(), "->', TR_sch,p_typeid=", (!cheBBoth.Checked) ? (radBOld.Checked ? 1 : 2) : 0, ", team_id");
			object obj2 = text2;
			text2 = string.Concat(obj2, ",GetDate(),", Program.m_opid, ",N'", Program.m_OperName, "'");
			text2 = text2 + ", NULL, NULL, NULL From T_Rooms Where TR_ID = " + dt.Rows[0]["TR_ID"].ToString() + "  And TR_Level = 0 \n";
			text2 += "Select @_ID = @@Identity \n ";
			string text6 = text2;
			text2 = text6 + "Update T_Guest set tr_id=@_ID,r_id=" + ndt.Rows[0]["R_ID"].ToString() + ",b_code='" + ndt.Rows[0]["Build_Code"].ToString();
			string text7 = text2;
			text2 = text7 + "',f_code='" + ndt.Rows[0]["Floor_Code"].ToString() + "',r_code='" + ndt.Rows[0]["R_Code"].ToString() + "',r_subcode=" + ndt.Rows[0]["R_SubCode"].ToString();
			string text8 = text2;
			text2 = text8 + ",R_subdai=" + ndt.Rows[0]["R_SubCodeDai"].ToString() + ",r_price=" + text + ",g_singlepaid=" + Program.GetStandDec(Program.changeValue(text, CultureInfo.InvariantCulture) * m_discount);
			text2 = text2 + ",g_memo=convert(nvarchar(max),g_memo)+'->'+convert(nvarchar(max),@_ID),r_name=N'" + ndt.Rows[0]["R_Name"].ToString() + "' ";
			string text9 = text2;
			text2 = text9 + ",a_id=isnull(a_id,0)+" + Program.GetStandDec(isforhour ? 0.0 : (num * 2.0)) + ",g_actual_S_Hour=g_actual_S_Hour+" + Program.GetStandDec(isforhour ? num : 0.0) + ",g_deposit=g_deposit+" + (flag ? "0" : standDec);
			text2 = text2 + " where TR_ID = " + dt.Rows[0]["TR_ID"].ToString() + " And g_level = 0 \n";
			string text10 = text2;
			text2 = text10 + "Update D_Rooms Set R_RSID=" + dt.Rows[0]["RS_ID"].ToString() + ", R_CurGuestCount=" + dt.Rows[0]["R_CurGuestCount"].ToString();
			string text11 = text2;
			text2 = text11 + ", R_TotalGuest=IsNull(R_TotalGuest,0) + " + dt.Rows[0]["R_CurGuestCount"].ToString() + ", R_TotalPrice=Isnull(R_TotalPrice,0) + " + Program.GetStandDec(label7.Text) + "*" + Program.GetStandDec((nrp * m_discount).ToString("F2"));
			object obj3 = text2;
			text2 = string.Concat(obj3, ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", ndt.Rows[0]["R_ID"].ToString(), "\n");
			string text12 = text2;
			text2 = text12 + "Update T_Rooms Set TR_totalpaid = " + Program.GetStandDec(label8.Text) + ", TR_mustpay=TR_mustpay+" + Program.GetStandDec(mspay.ToString("F2")) + ", TR_getchange=" + Program.GetStandDec(label14.Text) + ", TR_Level=1, TR_actual_S_Hour=TR_actual_S_Hour+" + Program.GetStandDec(isforhour ? num : 0.0) + ",TR_actual_L_time=GetDate()";
			object obj4 = text2;
			text2 = string.Concat(obj4, ",a_id=isnull(a_id,0)+", Program.GetStandDec(isforhour ? 0.0 : (num * 2.0)), ", TR_Memo = '->'+convert(nvarchar(max),@_ID), Updatetime=GetDate(),updator_id=", Program.m_opid, ", updator=N'", Program.m_OperName, "' Where TR_ID=", dt.Rows[0]["TR_ID"].ToString(), " \n ");
			text2 += "Update D_Rooms Set R_RSID = 2, R_CurGuestCount = 0";
			object obj5 = text2;
			text2 = string.Concat(obj5, ", R_Updatetime=GetDate(),R_Updator_ID=", Program.m_opid, ", R_Updator=N'", Program.m_OperName, "' Where R_ID=", dt.Rows[0]["r_id"].ToString(), " \n ");
			if (flag)
			{
				object obj6 = text2;
				text2 = string.Concat(obj6, "update t_team set team_totalpaid=team_totalpaid+", standDec, " where team_id=", num4, "\n");
			}
			text2 += "Select @_ID As TR_ID";
			DataTable dataTable = Program.DBCompGetDT(text2, Text);
			m_retst = -1;
			if (dataTable == null || dataTable.Rows.Count <= 0)
			{
				Program.MsgBox((string)m_htab["Info01"], (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			m_retst = 0;
			frmCardRewrite frmCardRewrite2 = new frmCardRewrite();
			if (dt != null && dt.Rows.Count > 0)
			{
				frmCardRewrite2.m_tmpID = Convert.ToInt64(dataTable.Rows[0]["TR_ID"].ToString());
				frmCardRewrite2.m_rtype = 0;
				frmCardRewrite2.btnTxt = ndt.Rows[0]["R_Name"].ToString();
				frmCardRewrite2.Text = (string)m_htab["fcrText"];
				frmCardRewrite2.ShowDialog();
			}
		}
		catch (Exception ex)
		{
			Program.MsgCusErrMess(ex.Message, Text);
		}
	}

	private void txtGPaid_TextChanged(object sender, EventArgs e)
	{
		try
		{
			double num = Convert.ToDouble(txtGPaid.Text);
			if (num < Convert.ToDouble(texBotherpaid1.Text))
			{
				txtGPaid.Text = texBotherpaid1.Text;
				return;
			}
			label16.Text = (num * currrate).ToString("F2");
			extrapay = num - Convert.ToDouble(texBotherpaid1.Text);
		}
		catch
		{
		}
	}

	private void label15_TextChanged(object sender, EventArgs e)
	{
		label20.Text = label15.Text;
	}

	private void label17_TextChanged(object sender, EventArgs e)
	{
		label22.Text = label17.Text;
	}

	private void texBotherpaid0_TextChanged(object sender, EventArgs e)
	{
		try
		{
			texBotherpaid1.Text = (Convert.ToDouble(texBotherpaid0.Text) / currrate).ToString("F2");
			txtGPaid.Text = (extrapay + Convert.ToDouble(texBotherpaid0.Text) / currrate).ToString("F2");
		}
		catch
		{
		}
	}

	private void cheBBoth_CheckedChanged(object sender, EventArgs e)
	{
		if (iteam && !cheBBoth.Checked)
		{
			cheBBoth.Checked = true;
		}
		else if (cheBBoth.Checked)
		{
			radBOld.Checked = false;
			radBNew.Checked = false;
			radBNew.Enabled = false;
			radBOld.Enabled = false;
			if (isforhour)
			{
				nudDay.Value = Convert.ToDecimal(havhour0);
			}
			else
			{
				nudDay.Value = Convert.ToDecimal(havday0);
			}
		}
		else
		{
			radBOld.Checked = true;
			radBNew.Checked = false;
			radBNew.Enabled = true;
			radBOld.Enabled = true;
		}
	}

	private void radBOld_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (radBOld.Checked)
			{
				if (isforhour)
				{
					nudDay.Value = Convert.ToDecimal(havhour1);
				}
				else
				{
					nudDay.Value = Convert.ToDecimal(havday1);
				}
			}
		}
		catch
		{
		}
	}

	private void radBNew_CheckedChanged(object sender, EventArgs e)
	{
		if (radBNew.Checked)
		{
			if (isforhour)
			{
				nudDay.Value = Convert.ToDecimal(havhour2);
			}
			else
			{
				nudDay.Value = Convert.ToDecimal(havday2);
			}
		}
	}

	private void label23_SizeChanged(object sender, EventArgs e)
	{
		label23.Left = 0;
		radBOld.Left = label23.Left + label23.Width + 2;
		radBOld_SizeChanged(null, null);
	}

	private void radBOld_SizeChanged(object sender, EventArgs e)
	{
		radBNew.Left = radBOld.Left + radBOld.Width + 2;
		radBNew_SizeChanged(null, null);
	}

	private void radBNew_SizeChanged(object sender, EventArgs e)
	{
		cheBBoth.Left = radBNew.Left + radBNew.Width + 2;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmGCR));
		this.label18 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.nudDay = new System.Windows.Forms.NumericUpDown();
		this.label1 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtGPaid = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.texBotherpaid1 = new System.Windows.Forms.TextBox();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.texBotherpaid0 = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.cheBBoth = new System.Windows.Forms.CheckBox();
		this.radBNew = new System.Windows.Forms.RadioButton();
		this.radBOld = new System.Windows.Forms.RadioButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		this.label4 = new System.Windows.Forms.TextBox();
		this.clsBackPanel2 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.labTxtMsg = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.nudDay).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.tableLayoutPanel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.flowLayoutPanel1.SuspendLayout();
		this.clsBackPanel2.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(216, 110);
		this.label18.Name = "label18";
		this.label18.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label18.Size = new System.Drawing.Size(26, 23);
		this.label18.TabIndex = 50;
		this.label18.Text = "-->";
		this.label17.AutoSize = true;
		this.label17.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label17.Location = new System.Drawing.Point(376, 110);
		this.label17.Name = "label17";
		this.label17.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label17.Size = new System.Drawing.Size(47, 23);
		this.label17.TabIndex = 49;
		this.label17.Text = "label17";
		this.label17.TextChanged += new System.EventHandler(label17_TextChanged);
		this.label16.AutoSize = true;
		this.label16.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label16.Location = new System.Drawing.Point(303, 110);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label16.Size = new System.Drawing.Size(47, 23);
		this.label16.TabIndex = 48;
		this.label16.Text = "label16";
		this.label12.AutoSize = true;
		this.label12.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label12.Location = new System.Drawing.Point(376, 87);
		this.label12.Name = "label12";
		this.label12.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label12.Size = new System.Drawing.Size(47, 23);
		this.label12.TabIndex = 44;
		this.label12.Text = "label12";
		this.label14.AutoSize = true;
		this.label14.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label14.Location = new System.Drawing.Point(303, 87);
		this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label14.Name = "label14";
		this.label14.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label14.Size = new System.Drawing.Size(47, 23);
		this.label14.TabIndex = 46;
		this.label14.Text = "label14";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(217, 87);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label13.Size = new System.Drawing.Size(78, 23);
		this.label13.TabIndex = 45;
		this.label13.Text = "剩余金额：";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(376, 58);
		this.label10.Name = "label10";
		this.label10.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label10.Size = new System.Drawing.Size(22, 23);
		this.label10.TabIndex = 42;
		this.label10.Text = "天";
		this.label7.AutoSize = true;
		this.label7.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label7.Location = new System.Drawing.Point(303, 58);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label7.Size = new System.Drawing.Size(40, 23);
		this.label7.TabIndex = 37;
		this.label7.Text = "label7";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(217, 58);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label6.Size = new System.Drawing.Size(78, 23);
		this.label6.TabIndex = 36;
		this.label6.Text = "剩余天数：";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(164, 58);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label5.Size = new System.Drawing.Size(22, 23);
		this.label5.TabIndex = 38;
		this.label5.Text = "天";
		this.label11.AutoSize = true;
		this.label11.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label11.Location = new System.Drawing.Point(163, 87);
		this.label11.Name = "label11";
		this.label11.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label11.Size = new System.Drawing.Size(46, 23);
		this.label11.TabIndex = 43;
		this.label11.Text = "label11";
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label15.Location = new System.Drawing.Point(163, 110);
		this.label15.Name = "label15";
		this.label15.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label15.Size = new System.Drawing.Size(47, 23);
		this.label15.TabIndex = 47;
		this.label15.Text = "label15";
		this.label15.TextChanged += new System.EventHandler(label15_TextChanged);
		this.nudDay.DecimalPlaces = 1;
		this.nudDay.Font = new System.Drawing.Font("Times New Roman", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudDay.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.nudDay.Location = new System.Drawing.Point(90, 62);
		this.nudDay.Margin = new System.Windows.Forms.Padding(4);
		this.nudDay.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudDay.Name = "nudDay";
		this.nudDay.Size = new System.Drawing.Size(60, 21);
		this.nudDay.TabIndex = 3;
		this.nudDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.nudDay.Value = new decimal(new int[4] { 10, 0, 0, 65536 });
		this.nudDay.ValueChanged += new System.EventHandler(nudDay_ValueChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(4, 58);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label1.Size = new System.Drawing.Size(78, 23);
		this.label1.TabIndex = 2;
		this.label1.Text = "已住天数：";
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label8.Location = new System.Drawing.Point(90, 87);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label8.Size = new System.Drawing.Size(40, 23);
		this.label8.TabIndex = 39;
		this.label8.Text = "label8";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(4, 87);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label2.Size = new System.Drawing.Size(78, 23);
		this.label2.TabIndex = 4;
		this.label2.Text = "已付费用：";
		this.txtGPaid.Location = new System.Drawing.Point(89, 113);
		this.txtGPaid.Name = "txtGPaid";
		this.txtGPaid.Size = new System.Drawing.Size(68, 24);
		this.txtGPaid.TabIndex = 41;
		this.txtGPaid.TextChanged += new System.EventHandler(txtGPaid_TextChanged);
		this.txtGPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGPaid_KeyPress);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(4, 110);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label9.Size = new System.Drawing.Size(78, 23);
		this.label9.TabIndex = 40;
		this.label9.Text = "需加收费：";
		this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.pictureBox1.Image = LockSoftware.Properties.Resources.Ques;
		this.pictureBox1.Location = new System.Drawing.Point(33, 175);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(49, 27);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.label3.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.label3, 4);
		this.label3.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.label3.Location = new System.Drawing.Point(0, 0);
		this.label3.Margin = new System.Windows.Forms.Padding(0, 3, 4, 2);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(45, 19);
		this.label3.TabIndex = 33;
		this.label3.Text = "label3";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(4, 140);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label19.Size = new System.Drawing.Size(78, 23);
		this.label19.TabIndex = 52;
		this.label19.Text = "服务费用：";
		this.texBotherpaid1.Location = new System.Drawing.Point(89, 143);
		this.texBotherpaid1.Name = "texBotherpaid1";
		this.texBotherpaid1.ReadOnly = true;
		this.texBotherpaid1.Size = new System.Drawing.Size(68, 24);
		this.texBotherpaid1.TabIndex = 53;
		this.texBotherpaid1.Text = "0.00";
		this.tableLayoutPanel1.ColumnCount = 6;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.texBotherpaid0, 4, 5);
		this.tableLayoutPanel1.Controls.Add(this.label22, 5, 5);
		this.tableLayoutPanel1.Controls.Add(this.label21, 3, 5);
		this.tableLayoutPanel1.Controls.Add(this.label20, 2, 5);
		this.tableLayoutPanel1.Controls.Add(this.texBotherpaid1, 1, 5);
		this.tableLayoutPanel1.Controls.Add(this.label19, 0, 5);
		this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 6);
		this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
		this.tableLayoutPanel1.Controls.Add(this.txtGPaid, 1, 4);
		this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
		this.tableLayoutPanel1.Controls.Add(this.label8, 1, 3);
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.nudDay, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.label15, 2, 4);
		this.tableLayoutPanel1.Controls.Add(this.label11, 2, 3);
		this.tableLayoutPanel1.Controls.Add(this.label5, 2, 2);
		this.tableLayoutPanel1.Controls.Add(this.label6, 3, 2);
		this.tableLayoutPanel1.Controls.Add(this.label7, 4, 2);
		this.tableLayoutPanel1.Controls.Add(this.label10, 5, 2);
		this.tableLayoutPanel1.Controls.Add(this.label13, 3, 3);
		this.tableLayoutPanel1.Controls.Add(this.label14, 4, 3);
		this.tableLayoutPanel1.Controls.Add(this.label12, 5, 3);
		this.tableLayoutPanel1.Controls.Add(this.label16, 4, 4);
		this.tableLayoutPanel1.Controls.Add(this.label17, 5, 4);
		this.tableLayoutPanel1.Controls.Add(this.label18, 3, 4);
		this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.label4, 1, 6);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 7;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 203);
		this.tableLayoutPanel1.TabIndex = 34;
		this.texBotherpaid0.Location = new System.Drawing.Point(302, 143);
		this.texBotherpaid0.Name = "texBotherpaid0";
		this.texBotherpaid0.Size = new System.Drawing.Size(68, 24);
		this.texBotherpaid0.TabIndex = 57;
		this.texBotherpaid0.Text = "0.00";
		this.texBotherpaid0.TextChanged += new System.EventHandler(texBotherpaid0_TextChanged);
		this.label22.AutoSize = true;
		this.label22.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label22.Location = new System.Drawing.Point(376, 140);
		this.label22.Name = "label22";
		this.label22.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label22.Size = new System.Drawing.Size(47, 23);
		this.label22.TabIndex = 56;
		this.label22.Text = "label22";
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(216, 140);
		this.label21.Name = "label21";
		this.label21.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label21.Size = new System.Drawing.Size(26, 23);
		this.label21.TabIndex = 55;
		this.label21.Text = "<--";
		this.label20.AutoSize = true;
		this.label20.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.label20.Location = new System.Drawing.Point(163, 140);
		this.label20.Name = "label20";
		this.label20.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
		this.label20.Size = new System.Drawing.Size(47, 23);
		this.label20.TabIndex = 54;
		this.label20.Text = "label20";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(3, 3);
		this.label23.Margin = new System.Windows.Forms.Padding(3);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(108, 16);
		this.label23.TabIndex = 58;
		this.label23.Text = "跨房时间段收取标准：";
		this.label23.SizeChanged += new System.EventHandler(label23_SizeChanged);
		this.cheBBoth.AutoSize = true;
		this.cheBBoth.Location = new System.Drawing.Point(265, 3);
		this.cheBBoth.Name = "cheBBoth";
		this.cheBBoth.Size = new System.Drawing.Size(83, 20);
		this.cheBBoth.TabIndex = 61;
		this.cheBBoth.Text = "分开运算";
		this.cheBBoth.UseVisualStyleBackColor = true;
		this.cheBBoth.CheckedChanged += new System.EventHandler(cheBBoth_CheckedChanged);
		this.radBNew.AutoSize = true;
		this.radBNew.Checked = true;
		this.radBNew.Location = new System.Drawing.Point(191, 3);
		this.radBNew.Name = "radBNew";
		this.radBNew.Size = new System.Drawing.Size(68, 20);
		this.radBNew.TabIndex = 60;
		this.radBNew.TabStop = true;
		this.radBNew.Text = "新房间";
		this.radBNew.UseVisualStyleBackColor = true;
		this.radBNew.CheckedChanged += new System.EventHandler(radBNew_CheckedChanged);
		this.radBNew.SizeChanged += new System.EventHandler(radBNew_SizeChanged);
		this.radBOld.AutoSize = true;
		this.radBOld.Location = new System.Drawing.Point(117, 3);
		this.radBOld.Name = "radBOld";
		this.radBOld.Size = new System.Drawing.Size(68, 20);
		this.radBOld.TabIndex = 59;
		this.radBOld.Text = "原房间";
		this.radBOld.UseVisualStyleBackColor = true;
		this.radBOld.CheckedChanged += new System.EventHandler(radBOld_CheckedChanged);
		this.radBOld.SizeChanged += new System.EventHandler(radBOld_SizeChanged);
		this.panel2.Controls.Add(this.tableLayoutPanel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 286);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(546, 203);
		this.panel2.TabIndex = 39;
		this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 6);
		this.flowLayoutPanel1.Controls.Add(this.label23);
		this.flowLayoutPanel1.Controls.Add(this.radBOld);
		this.flowLayoutPanel1.Controls.Add(this.radBNew);
		this.flowLayoutPanel1.Controls.Add(this.cheBBoth);
		this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 27);
		this.flowLayoutPanel1.Name = "flowLayoutPanel1";
		this.flowLayoutPanel1.Size = new System.Drawing.Size(540, 28);
		this.flowLayoutPanel1.TabIndex = 62;
		this.label4.BackColor = System.Drawing.SystemColors.Control;
		this.label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tableLayoutPanel1.SetColumnSpan(this.label4, 5);
		this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.label4.Location = new System.Drawing.Point(0, 0);
		this.label4.Multiline = true;
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 21);
		this.label4.TabIndex = 63;
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
		this.clsBackPanel2.Color2 = System.Drawing.Color.FromArgb(224, 224, 224);
		this.clsBackPanel2.ColorAngle = 90f;
		this.clsBackPanel2.Controls.Add(this.btnCl);
		this.clsBackPanel2.Controls.Add(this.btnOK);
		this.clsBackPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.clsBackPanel2.Location = new System.Drawing.Point(0, 489);
		this.clsBackPanel2.Name = "clsBackPanel2";
		this.clsBackPanel2.Size = new System.Drawing.Size(546, 35);
		this.clsBackPanel2.TabIndex = 38;
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
		this.btnCl.Location = new System.Drawing.Point(294, 6);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(74, 23);
		this.btnCl.TabIndex = 8;
		this.btnCl.Text = "取 消";
		this.btnCl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
		this.btnOK.Location = new System.Drawing.Point(180, 6);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(74, 23);
		this.btnOK.TabIndex = 9;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.clsBackPanel1.AutoScroll = true;
		this.clsBackPanel1.Border = true;
		this.clsBackPanel1.BorderBT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderBW = 1;
		this.clsBackPanel1.BorderColorBottom = System.Drawing.Color.DimGray;
		this.clsBackPanel1.BorderColorLeft = System.Drawing.Color.Gainsboro;
		this.clsBackPanel1.BorderColorRight = System.Drawing.Color.Gainsboro;
		this.clsBackPanel1.BorderColorTop = System.Drawing.Color.Gainsboro;
		this.clsBackPanel1.BorderLT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderLW = 1;
		this.clsBackPanel1.BorderRT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderRW = 1;
		this.clsBackPanel1.BorderTT = System.Windows.Forms.ButtonBorderStyle.Solid;
		this.clsBackPanel1.BorderTW = 1;
		this.clsBackPanel1.Color1 = System.Drawing.SystemColors.Control;
		this.clsBackPanel1.Color2 = System.Drawing.SystemColors.Control;
		this.clsBackPanel1.ColorAngle = 90f;
		this.clsBackPanel1.Controls.Add(this.labTxtMsg);
		this.clsBackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 0);
		this.clsBackPanel1.Margin = new System.Windows.Forms.Padding(0);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(546, 286);
		this.clsBackPanel1.TabIndex = 37;
		this.labTxtMsg.AutoSize = true;
		this.labTxtMsg.ForeColor = System.Drawing.SystemColors.ControlText;
		this.labTxtMsg.Location = new System.Drawing.Point(13, 9);
		this.labTxtMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 10);
		this.labTxtMsg.Name = "labTxtMsg";
		this.labTxtMsg.Size = new System.Drawing.Size(0, 16);
		this.labTxtMsg.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(546, 524);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.clsBackPanel2);
		base.Controls.Add(this.clsBackPanel1);
		this.Font = new System.Drawing.Font("Times New Roman", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGCR";
		this.Text = "frmGCR";
		base.Load += new System.EventHandler(frmGCR_Load);
		((System.ComponentModel.ISupportInitialize)this.nudDay).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.flowLayoutPanel1.ResumeLayout(false);
		this.flowLayoutPanel1.PerformLayout();
		this.clsBackPanel2.ResumeLayout(false);
		this.clsBackPanel1.ResumeLayout(false);
		this.clsBackPanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
