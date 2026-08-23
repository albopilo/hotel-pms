using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataBase;
using LockSoftware.Controls;
using LockSoftware.Properties;
using Microsoft.Reporting.WinForms;

namespace LockSoftware.Frm;

public class frmBill : Form
{
	public string m_objName = "WFbill";

	public Hashtable m_htab;

	public long m_gid = -1L;

	public long m_trid = -1L;

	public string m_chkIn = "";

	public string m_chkOut = "";

	public double m_FactDay;

	public double m_FactHour;

	public double m_RoomPrice;

	public double m_HourPrice;

	public double m_AddHourPrice;

	public double m_Total;

	public double m_Deposit;

	public double m_Paid;

	public double m_Change;

	public double m_Rate = 1.0;

	public bool m_close;

	public bool m_team;

	public bool m_hr;

	public DateTime m_LeaveTime = default(DateTime);

	public string m_ChangeRoom = "";

	public double m_OtherPaid;

	public double Extrapay;

	public string houses = "";

	public string houseids = "";

	public bool isdis;

	public bool isoldver;

	public double havday;

	public double havhour;

	public double totalCur;

	public List<string> guestsName;

	public DataTable guestsInfoDT;

	private decimal taxPercentage;

	private IContainer components;

	private Panel panel1;

	private clsBackPanel clsBackPanel1;

	public GlassBtn btnCl;

	public GlassBtn btnOK;

	private Label lab09;

	private Label lab08;

	private Label lab07;

	private TableLayoutPanel tableLayoutPanel1;

	private NGlassBtn btnTitle;

	public TextBox txtChange;

	public TextBox txtPaid;

	public TextBox txtTotal;

	public TextBox txt01;

	public TextBox txt02;

	public TextBox txt03;

	public ReportViewer rptbill;

	public CheckBox chkPB;

	private Label lab11;

	public TextBox txt04;

	public TextBox txtDep;

	private Panel panel2;

	private Label label1;

	private ComboBox cobCurrency;

	private Panel panName;

	private Label labName;

	private ComboBox cmbBoxGuestName;

	public TextBox labMsg;

	public TextBox txtBoxPercent;

	private Label lblTaxPercentage;

	private Panel pnlPrintSelect;

	private Panel pnlData;

	private NumericUpDown numUpDownTaxPercent;

	public frmBill()
	{
		InitializeComponent();
		m_htab = Program.GetControlName(this, m_objName);
		Text = (string)m_htab["btnTitle"];
		guestsName = new List<string>();
		guestsInfoDT = new DataTable();
		guestsInfoDT.Columns.Add("GuestID");
		guestsInfoDT.Columns.Add("GuestName");
	}

	private void frmBill_Load(object sender, EventArgs e)
	{
		btnOK.Text = (string)Program.m_hPubTab["btnOK"];
		btnCl.Text = (string)Program.m_hPubTab["btnCl"];
		InitGuestsInfo();
		numUpDownTaxPercent.Value = Program.TaxPercent;
		GetReport();
		txtPaid.Text = ((m_Change < 0.0) ? ((0.0 - m_Change) / m_Rate) : 0.0).ToString("F2");
	}

	private void InitGuestsInfo()
	{
		cmbBoxGuestName.DisplayMember = "GuestName";
		cmbBoxGuestName.ValueMember = "GuestID";
		cmbBoxGuestName.DataSource = guestsInfoDT;
		if (cmbBoxGuestName.Items.Count > 0)
		{
			cmbBoxGuestName.SelectedIndex = 0;
		}
	}

	private void btnCl_Click(object sender, EventArgs e)
	{
		m_close = true;
		Close();
	}

	private string CalcChange()
	{
		return ((m_Deposit + m_Paid) * m_Rate - m_Total - m_Total * (double)(float)taxPercentage / 100.0).ToString("F2");
	}

	private DataTable GetData()
	{
		string text = "";
		text = "Select 0 As rectype,r_name,tp_price as r_price,tp_pricelesshour,tr_id,TR_Bascurname,isnull(a_id,0)/2.0 as havday,Tr_actual_s_hour as havhour, TR_Level,TR_cometime,TR_actual_L_time, TR_mustpay, TR_discount,TR_Roomprice,TP_PriceStandHour,tr_memo From v_Room Where ";
		text = (m_team ? (text + " team_id = " + m_trid) : (text + " TR_ID in " + houseids + " "));
		text += " Union all ";
		text = text + "Select distinct 1 As rectype, (oth_ID + '\r\n' +oth_name) As r_name, oth_price As r_price,0 as tp_pricelesshour,0 as tr_id,'" + Program.m_baseCurrCode + "' As TR_Bascurname, Sum(othp_qty) As havday,0 as havhour";
		text += ", '' As TR_Level, '' As TR_cometime, '' As TR_actual_L_time, sum(othp_apaid) as TR_mustpay";
		text += ",othp_discount as TR_discount, 0 As TR_RoomPrice, 0 As TP_PriceStandHour,'' as tr_memo From v_OtherDetails Where";
		if (!m_team)
		{
			text = text + " tr_id in " + houseids + "and a_id=0 ";
		}
		else
		{
			object obj = text;
			text = string.Concat(obj, " team_id = ", m_trid, "and a_id =-1");
		}
		text += " group by oth_ID, oth_name, oth_price, othp_discount,tr_id";
		return SQLserver.Data_GetDataTable(text);
	}

	private void GetPringTable(DataTable dtSource, DataTable dtPurpose)
	{
		dtPurpose.Columns.Add("r_name");
		dtPurpose.Columns.Add("TR_stayhour");
		dtPurpose.Columns.Add("r_price");
		dtPurpose.Columns.Add("TR_discount");
		dtPurpose.Columns.Add("TR_mustpay");
		double num = 0.0;
		string text = Program.GetFaceDisValue() + "%";
		dtPurpose.Rows.Clear();
		dtPurpose.BeginLoadData();
		for (int i = 0; i < dtSource.Rows.Count; i++)
		{
			double num2 = double.Parse(dtSource.Rows[i]["TR_discount"].ToString());
			text = Program.GetFaceDisValue(num2) + "%";
			if (Convert.ToInt16(dtSource.Rows[i]["rectype"].ToString()) == 0)
			{
				double num3 = 0.0;
				if (m_FactDay + havday > 0.0 || m_team)
				{
					DataRow dataRow = dtPurpose.NewRow();
					dataRow["r_name"] = dtSource.Rows[i]["r_name"];
					dataRow["TR_discount"] = text;
					if (m_team)
					{
						double num4 = double.Parse(dtSource.Rows[i]["r_price"].ToString());
						double num5 = 0.0;
						DateTime dtComeTime = DateTime.Parse(dtSource.Rows[i]["TR_cometime"].ToString());
						dataRow["r_price"] = num4;
						if (dtSource.Rows[i]["TR_Level"].ToString().ToLower() == "true")
						{
							if (dtSource.Rows[i]["TR_memo"].ToString().Contains("->"))
							{
								num5 = Convert.ToDouble(dtSource.Rows[i]["havday"]);
								if (num5 != 0.0)
								{
									dataRow["TR_stayhour"] = num5;
									dataRow["TR_mustpay"] = Convert.ToDouble(dtSource.Rows[i]["TR_mustpay"]);
									dtPurpose.Rows.Add(dataRow);
								}
							}
						}
						else
						{
							num5 = Program.CountDay(dtComeTime, m_LeaveTime);
							if (num5 != 0.0)
							{
								dataRow["TR_stayhour"] = num5;
								dataRow["TR_mustpay"] = num5 * num4 * num2;
								dtPurpose.Rows.Add(dataRow);
							}
						}
					}
					else if (dtSource.Rows[i]["TR_Level"].ToString().ToLower() == "true")
					{
						if (dtSource.Rows[i]["TR_memo"].ToString().Contains("->") && Convert.ToDouble(dtSource.Rows[i]["havday"]) != 0.0)
						{
							dataRow["r_price"] = Convert.ToDouble(dtSource.Rows[i]["r_price"]);
							dataRow["TR_stayhour"] = Convert.ToDouble(dtSource.Rows[i]["havday"]);
							num3 = Convert.ToDouble(dtSource.Rows[i]["havday"]) * Convert.ToDouble(dtSource.Rows[i]["r_price"]) * num2;
							dataRow["TR_mustpay"] = num3;
							dtPurpose.Rows.Add(dataRow);
						}
					}
					else if (m_FactDay + Convert.ToDouble(dtSource.Rows[i]["havday"]) != 0.0)
					{
						dataRow["r_price"] = m_RoomPrice;
						dataRow["TR_stayhour"] = m_FactDay + Convert.ToDouble(dtSource.Rows[i]["havday"]);
						num3 = Convert.ToDouble(dtSource.Rows[i]["havday"]) * m_RoomPrice * num2;
						dataRow["TR_mustpay"] = num3 + m_FactDay * m_RoomPrice * num2;
						dtPurpose.Rows.Add(dataRow);
					}
				}
				if (!(m_FactHour + havhour > 0.0))
				{
					continue;
				}
				DataRow dataRow2 = dtPurpose.NewRow();
				if (m_trid.ToString() == dtSource.Rows[i]["tr_id"].ToString())
				{
					if (m_FactHour + (double)Convert.ToInt32(dtSource.Rows[i]["havhour"]) == 0.0)
					{
						continue;
					}
					if (m_HourPrice == m_AddHourPrice)
					{
						dataRow2["r_name"] = dtSource.Rows[i]["r_name"].ToString() + "\r\n" + (string)m_htab["InfoHR"];
						dataRow2["TR_stayhour"] = (int)m_FactHour + Convert.ToInt32(dtSource.Rows[i]["havhour"]);
						dataRow2["r_price"] = m_HourPrice.ToString("F2");
						dataRow2["TR_discount"] = text;
						dataRow2["TR_mustpay"] = (((m_FactHour > 0.0) ? totalCur : 0.0) + Convert.ToDouble(dtSource.Rows[i]["TR_mustpay"]) - num3).ToString("F2");
						dtPurpose.Rows.Add(dataRow2);
						continue;
					}
					dataRow2["r_name"] = dtSource.Rows[i]["r_name"].ToString() + "\r\n" + (string)m_htab["InfoHR"];
					double num6 = ((!(havhour > 0.0)) ? ((double)Program.m_defHR) : ((havhour > (double)Program.m_defHR) ? ((double)(int)m_FactHour) : ((double)Program.m_defHR - havhour)));
					dataRow2["TR_stayhour"] = num6;
					double num7 = ((havhour < (double)Program.m_defHR) ? m_HourPrice : m_AddHourPrice);
					dataRow2["r_price"] = num7.ToString("F2");
					dataRow2["TR_discount"] = text;
					dataRow2["TR_mustpay"] = (num7 * num6 * num2).ToString("F2");
					dtPurpose.Rows.Add(dataRow2);
					if ((double)(int)m_FactHour + havhour > (double)Program.m_defHR && (double)((int)m_FactHour + Convert.ToInt32(dtSource.Rows[i]["havhour"])) - num6 > 0.0)
					{
						dataRow2 = dtPurpose.NewRow();
						dataRow2["r_name"] = dtSource.Rows[i]["r_name"].ToString() + "\r\n" + (string)m_htab["InfoHR"];
						dataRow2["TR_stayhour"] = (double)((int)m_FactHour + Convert.ToInt32(dtSource.Rows[i]["havhour"])) - num6;
						dataRow2["r_price"] = m_AddHourPrice.ToString("F2");
						dataRow2["TR_discount"] = text;
						dataRow2["TR_mustpay"] = (((m_FactHour > 0.0) ? totalCur : 0.0) + Convert.ToDouble(dtSource.Rows[i]["TR_mustpay"]) - num3 - m_HourPrice * num6 * num2).ToString("F2");
						dtPurpose.Rows.Add(dataRow2);
					}
				}
				else if (Convert.ToInt32(dtSource.Rows[i]["havhour"]) != 0)
				{
					dataRow2["r_name"] = dtSource.Rows[i]["r_name"].ToString() + "\r\n" + (string)m_htab["InfoHR"];
					dataRow2["TR_stayhour"] = Convert.ToInt32(dtSource.Rows[i]["havhour"]);
					dataRow2["r_price"] = dtSource.Rows[i]["tp_pricelesshour"];
					dataRow2["TR_discount"] = text;
					dataRow2["TR_mustpay"] = (Convert.ToDouble(dtSource.Rows[i]["TR_mustpay"]) - num3).ToString("F2");
					dtPurpose.Rows.Add(dataRow2);
				}
			}
			else
			{
				DataRow dataRow3 = dtPurpose.NewRow();
				dataRow3["r_name"] = dtSource.Rows[i]["r_name"].ToString();
				dataRow3["TR_stayhour"] = Convert.ToInt32(dtSource.Rows[i]["havday"]);
				dataRow3["r_price"] = Program.GetLocDecStr(dtSource.Rows[i]["r_price"].ToString());
				dataRow3["TR_discount"] = text;
				dataRow3["TR_mustpay"] = Program.GetLocDecStr(dtSource.Rows[i]["TR_mustpay"].ToString());
				dtPurpose.Rows.Add(dataRow3);
				num += double.Parse(dataRow3["TR_mustpay"].ToString());
			}
		}
		if (Extrapay > 0.0)
		{
			DataRow dataRow4 = dtPurpose.NewRow();
			if (!m_team)
			{
				dataRow4["r_name"] = (string)m_htab["changeroomcost"] + "\n" + houses;
				dataRow4["TR_stayhour"] = ((houses.Split(',').Length > 0) ? (houses.Split(',').Length - 1) : 0);
				dataRow4["r_price"] = Extrapay / (double)((houses.Split(',').Length > 0) ? (houses.Split(',').Length - 1) : 0);
				dataRow4["TR_discount"] = "100%";
				dataRow4["TR_mustpay"] = Extrapay.ToString("F2");
			}
			else
			{
				string[] array = houses.Split('\n');
				int num8 = 0;
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					int num9 = text2.Trim(',').Trim().Split(',')
						.Length;
					if (num9 > 1)
					{
						num8 += num9 - 1;
					}
				}
				dataRow4["r_name"] = (string)m_htab["changeroomcost"] + "\n" + houses;
				dataRow4["TR_stayhour"] = num8;
				dataRow4["r_price"] = Extrapay / (double)num8;
				dataRow4["TR_discount"] = "100%";
				dataRow4["TR_mustpay"] = Extrapay.ToString("F2");
			}
			dtPurpose.Rows.Add(dataRow4);
		}
		dtPurpose.AcceptChanges();
		dtPurpose.EndLoadData();
	}

	private void InitReport(double priceStand)
	{
		if (rptbill.LocalReport.DataSources.Count > 0)
		{
			ReportParameter reportParameter = new ReportParameter("labTitle", (string)m_htab["labTitle"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cHotelID", (string)m_htab["cHotelID"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cAddress", (string)m_htab["cAddress"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cHotelWeb", (string)m_htab["cHotelWeb"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab01", (string)m_htab["lab01"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab02", (string)m_htab["lab02"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab03", (string)m_htab["lab03"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab05", (string)m_htab["lab05"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab04", (string)m_htab["lab04"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("labName", (string)m_htab["labName"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab06", (string)m_htab["lab06"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("tCol01", (string)m_htab["tCol01"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("tCol02", (string)m_htab["tCol02"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("tCol03", (string)m_htab["tCol03"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("tCol04", (string)m_htab["tCol04"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("tCol05", (string)m_htab["tCol05"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("tlabTotal", (string)m_htab["tlabTotal"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab07", (string)m_htab["lab07"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab08", (string)m_htab["lab08"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("lab09", (string)m_htab["lab09"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("labBottom", (string)m_htab["labBottom"]);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cDtNow", Program.GetLocDTime(DateTime.Now, "ss"));
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cGuestName", cmbBoxGuestName.Text);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cOper", Program.m_OperName);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cCheckIn", m_chkIn);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cCheckOut", m_chkOut);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cSD", m_FactDay.ToString("F2"));
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			string text = (string)m_htab["tCol05"];
			if (Program.m_defDiscount == 0)
			{
				text = "(1 - " + text + ")";
			}
			string text2 = "";
			text2 = (m_hr ? string.Format((string)m_htab["labPriDesc_hr"], (string)m_htab["tCol04"], (string)m_htab["tCol02"], (string)m_htab["tCol03"], (string)m_htab["InfoDefSH"] + "(" + Program.m_defHR + ")", (string)m_htab["InfoAH"] + "(" + priceStand.ToString("F2") + ")", text) : string.Format((string)m_htab["labPriDesc"], (string)m_htab["tCol04"], (string)m_htab["tCol02"], (string)m_htab["tCol03"], text));
			reportParameter = new ReportParameter("labPriceDesc", text2);
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = (m_team ? new ReportParameter("cGuestID", "T" + m_gid.ToString("D8")) : new ReportParameter("cGuestID", "G" + Convert.ToInt64(cmbBoxGuestName.SelectedValue).ToString("D8")));
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cTotal", txt01.Text.Trim() + " " + m_Total.ToString("F2"));
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cPaid", txt02.Text.Trim() + " " + (m_Paid + m_Deposit).ToString("F2"));
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("cChange", txt03.Text.Trim() + " " + m_Change.ToString("F2"));
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			reportParameter = new ReportParameter("imgLogo", "file:///" + AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Reports/logo.png");
			rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
			rptbill.LocalReport.SetParameters(new ReportParameter("imgLogoShow", (!File.Exists("Reports/logo.png")).ToString()));
			reportParameter = new ReportParameter("txtTax", string.Format(Program.TaxType, taxPercentage.ToString("F2") + "%"));
			rptbill.LocalReport.SetParameters(reportParameter);
			reportParameter = new ReportParameter("taxValue", txt01.Text.Trim() + " " + (m_Total * (double)(float)taxPercentage / 100.0).ToString("F2"));
			rptbill.LocalReport.SetParameters(reportParameter);
		}
	}

	public void GetReport()
	{
		try
		{
			rptbill.LocalReport.DataSources.Clear();
			string sql = "Select * From D_HotelBasic";
			DataTable dataSourceValue = SQLserver.Data_GetDataTable(sql);
			DataTable data = GetData();
			if (data == null || data.Rows.Count <= 0)
			{
				rptbill.RefreshReport();
				return;
			}
			DataTable dataTable = new DataTable();
			GetPringTable(data, dataTable);
			rptbill.LocalReport.DataSources.Add(new ReportDataSource("RadioLockDataSet_v_Room", dataTable));
			rptbill.LocalReport.DataSources.Add(new ReportDataSource("RPT_DS_HB_D_HotelBasic", dataSourceValue));
			if (File.Exists("Reports\\Bill0.rdlc"))
			{
				rptbill.LocalReport.ReportPath = "Reports\\Bill0.rdlc";
			}
			else
			{
				rptbill.LocalReport.ReportEmbeddedResource = "LockSoftware.Reports.Bill0.rdlc";
			}
			rptbill.LocalReport.EnableExternalImages = true;
			InitReport(m_hr ? Convert.ToDouble(dataTable.Rows[0]["TP_PriceStandHour"]) : 0.0);
			rptbill.RefreshReport();
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtPaid_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r')
		{
			e.Handled = Program.ChkNumInput(sender, e, Integer: false, chkDot: true);
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		btnOKOnClick();
	}

	private void btnOKOnClick()
	{
		try
		{
			m_close = false;
			if (!Program.isValNull(lab08.Text.Trim().Substring(0, lab08.Text.Trim().Length - 1), txtPaid.Text.Trim(), chk: true))
			{
				if (m_Change >= 0.0)
				{
					m_close = true;
				}
				else
				{
					Program.MsgCustom((string)m_htab["Info01"], MessageBoxIcon.Exclamation);
				}
			}
		}
		catch (Exception ex)
		{
			Program.MsgBox(ex.Message, (string)Program.m_hPubTab["ErrTitle"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtChange_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (m_Change < 0.0)
			{
				txtChange.ForeColor = Color.Red;
			}
			else
			{
				txtChange.ForeColor = Color.Black;
			}
		}
		catch
		{
		}
	}

	private void frmBill_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = !m_close;
	}

	private void txtPaid_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		try
		{
			if (txtPaid.Text.Trim() == "")
			{
				txtPaid.Text = Program.GetLocDecStr("0.0");
			}
			m_Paid = Convert.ToDouble(txtPaid.Text.Trim());
			m_Change = Convert.ToDouble(CalcChange());
			txtChange.Text = m_Change.ToString("F2");
			if (rptbill.LocalReport.DataSources.Count > 0)
			{
				ReportParameter reportParameter = new ReportParameter("cPaid", txt02.Text.Trim() + " " + (m_Paid + m_Deposit).ToString("F2"));
				rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
				reportParameter = new ReportParameter("cChange", txt03.Text.Trim() + " " + m_Change.ToString("F2"));
				rptbill.LocalReport.SetParameters(new ReportParameter[1] { reportParameter });
				rptbill.RefreshReport();
				btnOKOnClick();
			}
		}
		catch
		{
		}
	}

	private void cmbBoxGuestName_SelectedIndexChanged(object sender, EventArgs e)
	{
		GetReport();
	}

	private void InitGuestsName()
	{
		foreach (string item in guestsName)
		{
			cmbBoxGuestName.Items.Add(item);
		}
		if (guestsName.Count > 0)
		{
			cmbBoxGuestName.SelectedIndex = 0;
		}
	}

	private void numUpDownTaxPercent_ValueChanged(object sender, EventArgs e)
	{
		taxPercentage = numUpDownTaxPercent.Value;
		m_Change = Convert.ToDouble(CalcChange());
		txtChange.Text = m_Change.ToString("F2");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockSoftware.Frm.frmBill));
		this.rptbill = new Microsoft.Reporting.WinForms.ReportViewer();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pnlData = new System.Windows.Forms.Panel();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.lblTaxPercentage = new System.Windows.Forms.Label();
		this.numUpDownTaxPercent = new System.Windows.Forms.NumericUpDown();
		this.txtBoxPercent = new System.Windows.Forms.TextBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.lab08 = new System.Windows.Forms.Label();
		this.txtTotal = new System.Windows.Forms.TextBox();
		this.lab07 = new System.Windows.Forms.Label();
		this.txt01 = new System.Windows.Forms.TextBox();
		this.txtChange = new System.Windows.Forms.TextBox();
		this.txt03 = new System.Windows.Forms.TextBox();
		this.lab09 = new System.Windows.Forms.Label();
		this.txt02 = new System.Windows.Forms.TextBox();
		this.txt04 = new System.Windows.Forms.TextBox();
		this.txtPaid = new System.Windows.Forms.TextBox();
		this.txtDep = new System.Windows.Forms.TextBox();
		this.lab11 = new System.Windows.Forms.Label();
		this.cobCurrency = new System.Windows.Forms.ComboBox();
		this.pnlPrintSelect = new System.Windows.Forms.Panel();
		this.chkPB = new System.Windows.Forms.CheckBox();
		this.labMsg = new System.Windows.Forms.TextBox();
		this.panName = new System.Windows.Forms.Panel();
		this.cmbBoxGuestName = new System.Windows.Forms.ComboBox();
		this.labName = new System.Windows.Forms.Label();
		this.clsBackPanel1 = new LockSoftware.Controls.clsBackPanel(this.components);
		this.btnCl = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnOK = new LockSoftware.Controls.GlassBtn(this.components);
		this.btnTitle = new LockSoftware.Controls.NGlassBtn(this.components);
		this.panel1.SuspendLayout();
		this.pnlData.SuspendLayout();
		this.tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numUpDownTaxPercent).BeginInit();
		this.panel2.SuspendLayout();
		this.pnlPrintSelect.SuspendLayout();
		this.panName.SuspendLayout();
		this.clsBackPanel1.SuspendLayout();
		base.SuspendLayout();
		this.rptbill.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rptbill.Location = new System.Drawing.Point(0, 0);
		this.rptbill.Name = "rptbill";
		this.rptbill.Size = new System.Drawing.Size(522, 580);
		this.rptbill.TabIndex = 2;
		this.rptbill.TabStop = false;
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.pnlData);
		this.panel1.Controls.Add(this.pnlPrintSelect);
		this.panel1.Controls.Add(this.panName);
		this.panel1.Controls.Add(this.clsBackPanel1);
		this.panel1.Controls.Add(this.btnTitle);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(522, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(278, 580);
		this.panel1.TabIndex = 1;
		this.pnlData.Controls.Add(this.tableLayoutPanel1);
		this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlData.Location = new System.Drawing.Point(0, 97);
		this.pnlData.Margin = new System.Windows.Forms.Padding(0);
		this.pnlData.Name = "pnlData";
		this.pnlData.Size = new System.Drawing.Size(276, 352);
		this.pnlData.TabIndex = 43;
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57f));
		this.tableLayoutPanel1.Controls.Add(this.lblTaxPercentage, 0, 2);
		this.tableLayoutPanel1.Controls.Add(this.numUpDownTaxPercent, 0, 3);
		this.tableLayoutPanel1.Controls.Add(this.txtBoxPercent, 1, 3);
		this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 6);
		this.tableLayoutPanel1.Controls.Add(this.txtTotal, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.lab07, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.txt01, 1, 1);
		this.tableLayoutPanel1.Controls.Add(this.txtChange, 0, 9);
		this.tableLayoutPanel1.Controls.Add(this.txt03, 1, 9);
		this.tableLayoutPanel1.Controls.Add(this.lab09, 0, 8);
		this.tableLayoutPanel1.Controls.Add(this.txt02, 1, 7);
		this.tableLayoutPanel1.Controls.Add(this.txt04, 1, 5);
		this.tableLayoutPanel1.Controls.Add(this.txtPaid, 0, 7);
		this.tableLayoutPanel1.Controls.Add(this.txtDep, 0, 5);
		this.tableLayoutPanel1.Controls.Add(this.lab11, 0, 4);
		this.tableLayoutPanel1.Controls.Add(this.cobCurrency, 0, 10);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
		this.tableLayoutPanel1.RowCount = 11;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30f));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.tableLayoutPanel1.Size = new System.Drawing.Size(276, 352);
		this.tableLayoutPanel1.TabIndex = 41;
		this.lblTaxPercentage.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.lblTaxPercentage, 2);
		this.lblTaxPercentage.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTaxPercentage.Location = new System.Drawing.Point(4, 56);
		this.lblTaxPercentage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.lblTaxPercentage.Name = "lblTaxPercentage";
		this.lblTaxPercentage.Size = new System.Drawing.Size(60, 19);
		this.lblTaxPercentage.TabIndex = 45;
		this.lblTaxPercentage.Text = "税率：";
		this.numUpDownTaxPercent.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.numUpDownTaxPercent.DecimalPlaces = 2;
		System.Windows.Forms.NumericUpDown numericUpDown = this.numUpDownTaxPercent;
		int[] bits = new int[4];
		numericUpDown.Increment = new decimal(bits);
		this.numUpDownTaxPercent.Location = new System.Drawing.Point(4, 78);
		this.numUpDownTaxPercent.Name = "numUpDownTaxPercent";
		this.numUpDownTaxPercent.ReadOnly = true;
		this.numUpDownTaxPercent.Size = new System.Drawing.Size(120, 27);
		this.numUpDownTaxPercent.TabIndex = 44;
		this.numUpDownTaxPercent.TabStop = false;
		this.numUpDownTaxPercent.ValueChanged += new System.EventHandler(numUpDownTaxPercent_ValueChanged);
		this.txtBoxPercent.BackColor = System.Drawing.Color.White;
		this.txtBoxPercent.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtBoxPercent.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtBoxPercent.Location = new System.Drawing.Point(221, 83);
		this.txtBoxPercent.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txtBoxPercent.Name = "txtBoxPercent";
		this.txtBoxPercent.ReadOnly = true;
		this.txtBoxPercent.Size = new System.Drawing.Size(51, 20);
		this.txtBoxPercent.TabIndex = 43;
		this.txtBoxPercent.TabStop = false;
		this.txtBoxPercent.Text = "%";
		this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.lab08);
		this.panel2.Location = new System.Drawing.Point(4, 160);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(268, 62);
		this.panel2.TabIndex = 40;
		this.label1.Font = new System.Drawing.Font("Tahoma", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Red;
		this.label1.Location = new System.Drawing.Point(3, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(262, 36);
		this.label1.TabIndex = 32;
		this.label1.Text = "*按[回车键]确认金额";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lab08.AutoSize = true;
		this.lab08.Dock = System.Windows.Forms.DockStyle.Left;
		this.lab08.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lab08.Location = new System.Drawing.Point(0, 0);
		this.lab08.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.lab08.Name = "lab08";
		this.lab08.Size = new System.Drawing.Size(77, 19);
		this.lab08.TabIndex = 31;
		this.lab08.Text = "支付款：";
		this.txtTotal.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtTotal.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtTotal.Location = new System.Drawing.Point(4, 26);
		this.txtTotal.Name = "txtTotal";
		this.txtTotal.ReadOnly = true;
		this.txtTotal.Size = new System.Drawing.Size(211, 27);
		this.txtTotal.TabIndex = 33;
		this.txtTotal.TabStop = false;
		this.lab07.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.lab07, 2);
		this.lab07.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lab07.Location = new System.Drawing.Point(4, 4);
		this.lab07.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.lab07.Name = "lab07";
		this.lab07.Size = new System.Drawing.Size(77, 19);
		this.lab07.TabIndex = 30;
		this.lab07.Text = "应付款：";
		this.txt01.BackColor = System.Drawing.Color.White;
		this.txt01.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txt01.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txt01.Location = new System.Drawing.Point(221, 31);
		this.txt01.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txt01.Name = "txt01";
		this.txt01.ReadOnly = true;
		this.txt01.Size = new System.Drawing.Size(51, 20);
		this.txt01.TabIndex = 36;
		this.txt01.TabStop = false;
		this.txtChange.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtChange.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtChange.Location = new System.Drawing.Point(4, 288);
		this.txtChange.Name = "txtChange";
		this.txtChange.ReadOnly = true;
		this.txtChange.Size = new System.Drawing.Size(211, 27);
		this.txtChange.TabIndex = 35;
		this.txtChange.TabStop = false;
		this.txtChange.TextChanged += new System.EventHandler(txtChange_TextChanged);
		this.txt03.BackColor = System.Drawing.Color.White;
		this.txt03.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txt03.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txt03.Location = new System.Drawing.Point(221, 293);
		this.txt03.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txt03.Name = "txt03";
		this.txt03.ReadOnly = true;
		this.txt03.Size = new System.Drawing.Size(51, 20);
		this.txt03.TabIndex = 38;
		this.txt03.TabStop = false;
		this.lab09.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.lab09, 2);
		this.lab09.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lab09.Location = new System.Drawing.Point(4, 258);
		this.lab09.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.lab09.Name = "lab09";
		this.tableLayoutPanel1.SetRowSpan(this.lab09, 2);
		this.lab09.Size = new System.Drawing.Size(77, 19);
		this.lab09.TabIndex = 32;
		this.lab09.Text = "应找零：";
		this.txt02.BackColor = System.Drawing.Color.White;
		this.txt02.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txt02.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txt02.Location = new System.Drawing.Point(221, 233);
		this.txt02.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txt02.Name = "txt02";
		this.txt02.ReadOnly = true;
		this.txt02.Size = new System.Drawing.Size(51, 20);
		this.txt02.TabIndex = 37;
		this.txt02.TabStop = false;
		this.txt04.BackColor = System.Drawing.Color.White;
		this.txt04.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txt04.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txt04.Location = new System.Drawing.Point(221, 135);
		this.txt04.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
		this.txt04.Name = "txt04";
		this.txt04.ReadOnly = true;
		this.txt04.Size = new System.Drawing.Size(51, 20);
		this.txt04.TabIndex = 41;
		this.txt04.TabStop = false;
		this.txtPaid.BackColor = System.Drawing.Color.White;
		this.txtPaid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtPaid.Location = new System.Drawing.Point(4, 228);
		this.txtPaid.MaxLength = 15;
		this.txtPaid.Name = "txtPaid";
		this.txtPaid.Size = new System.Drawing.Size(211, 27);
		this.txtPaid.TabIndex = 24;
		this.txtPaid.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPaid_KeyDown);
		this.txtPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPaid_KeyPress);
		this.txtDep.BackColor = System.Drawing.Color.FromArgb(205, 229, 245);
		this.txtDep.Dock = System.Windows.Forms.DockStyle.Fill;
		this.txtDep.Location = new System.Drawing.Point(4, 130);
		this.txtDep.Name = "txtDep";
		this.txtDep.ReadOnly = true;
		this.txtDep.Size = new System.Drawing.Size(211, 27);
		this.txtDep.TabIndex = 40;
		this.txtDep.TabStop = false;
		this.lab11.AutoSize = true;
		this.tableLayoutPanel1.SetColumnSpan(this.lab11, 2);
		this.lab11.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lab11.Location = new System.Drawing.Point(4, 108);
		this.lab11.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.lab11.Name = "lab11";
		this.lab11.Size = new System.Drawing.Size(77, 19);
		this.lab11.TabIndex = 39;
		this.lab11.Text = "已付款：";
		this.cobCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobCurrency.FormattingEnabled = true;
		this.cobCurrency.Location = new System.Drawing.Point(4, 321);
		this.cobCurrency.Name = "cobCurrency";
		this.cobCurrency.Size = new System.Drawing.Size(51, 27);
		this.cobCurrency.TabIndex = 42;
		this.cobCurrency.Visible = false;
		this.pnlPrintSelect.BackColor = System.Drawing.Color.Transparent;
		this.pnlPrintSelect.Controls.Add(this.chkPB);
		this.pnlPrintSelect.Controls.Add(this.labMsg);
		this.pnlPrintSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pnlPrintSelect.Location = new System.Drawing.Point(0, 449);
		this.pnlPrintSelect.Margin = new System.Windows.Forms.Padding(0);
		this.pnlPrintSelect.Name = "pnlPrintSelect";
		this.pnlPrintSelect.Size = new System.Drawing.Size(276, 81);
		this.pnlPrintSelect.TabIndex = 43;
		this.chkPB.AutoSize = true;
		this.chkPB.Checked = true;
		this.chkPB.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPB.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.chkPB.Location = new System.Drawing.Point(8, 4);
		this.chkPB.Name = "chkPB";
		this.chkPB.Size = new System.Drawing.Size(87, 23);
		this.chkPB.TabIndex = 28;
		this.chkPB.Text = "Print Bill";
		this.chkPB.UseVisualStyleBackColor = true;
		this.labMsg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.labMsg.ForeColor = System.Drawing.Color.Red;
		this.labMsg.Location = new System.Drawing.Point(3, 33);
		this.labMsg.Multiline = true;
		this.labMsg.Name = "labMsg";
		this.labMsg.Size = new System.Drawing.Size(270, 48);
		this.labMsg.TabIndex = 42;
		this.panName.BackColor = System.Drawing.Color.Transparent;
		this.panName.Controls.Add(this.cmbBoxGuestName);
		this.panName.Controls.Add(this.labName);
		this.panName.Dock = System.Windows.Forms.DockStyle.Top;
		this.panName.Location = new System.Drawing.Point(0, 43);
		this.panName.Margin = new System.Windows.Forms.Padding(0);
		this.panName.Name = "panName";
		this.panName.Size = new System.Drawing.Size(276, 54);
		this.panName.TabIndex = 0;
		this.cmbBoxGuestName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbBoxGuestName.FormattingEnabled = true;
		this.cmbBoxGuestName.Location = new System.Drawing.Point(12, 25);
		this.cmbBoxGuestName.Name = "cmbBoxGuestName";
		this.cmbBoxGuestName.Size = new System.Drawing.Size(199, 22);
		this.cmbBoxGuestName.TabIndex = 43;
		this.cmbBoxGuestName.SelectedIndexChanged += new System.EventHandler(cmbBoxGuestName_SelectedIndexChanged);
		this.labName.AutoSize = true;
		this.labName.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labName.Location = new System.Drawing.Point(8, 3);
		this.labName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.labName.Name = "labName";
		this.labName.Size = new System.Drawing.Size(94, 19);
		this.labName.TabIndex = 31;
		this.labName.Text = "宾客姓名：";
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
		this.clsBackPanel1.Location = new System.Drawing.Point(0, 530);
		this.clsBackPanel1.Name = "clsBackPanel1";
		this.clsBackPanel1.Size = new System.Drawing.Size(276, 48);
		this.clsBackPanel1.TabIndex = 29;
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
		this.btnCl.Location = new System.Drawing.Point(182, 8);
		this.btnCl.Name = "btnCl";
		this.btnCl.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnCl.Size = new System.Drawing.Size(86, 32);
		this.btnCl.TabIndex = 10;
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
		this.btnOK.Location = new System.Drawing.Point(89, 8);
		this.btnOK.Name = "btnOK";
		this.btnOK.OuterBorderColor = System.Drawing.Color.LightGray;
		this.btnOK.Size = new System.Drawing.Size(86, 32);
		this.btnOK.TabIndex = 9;
		this.btnOK.Text = "确 定";
		this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnTitle.BackColor = System.Drawing.Color.Transparent;
		this.btnTitle.BaseColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.btnTitle.ButtonColor = System.Drawing.Color.FromArgb(255, 192, 128);
		this.btnTitle.ButtonText = "宾客结账";
		this.btnTitle.CornerRadius = 2;
		this.btnTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnTitle.Font = new System.Drawing.Font("Tahoma", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnTitle.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.btnTitle.GlowColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnTitle.GuidInfo = "&56~01'][Manson]v%#@";
		this.btnTitle.Image = LockSoftware.Properties.Resources._018;
		this.btnTitle.ImageSize = new System.Drawing.Size(48, 48);
		this.btnTitle.Location = new System.Drawing.Point(0, 0);
		this.btnTitle.Name = "btnTitle";
		this.btnTitle.Size = new System.Drawing.Size(276, 43);
		this.btnTitle.TabIndex = 37;
		this.btnTitle.TabStop = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.ClientSize = new System.Drawing.Size(800, 580);
		base.Controls.Add(this.rptbill);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmBill";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "宾客结账";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmBill_FormClosing);
		base.Load += new System.EventHandler(frmBill_Load);
		this.panel1.ResumeLayout(false);
		this.pnlData.ResumeLayout(false);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numUpDownTaxPercent).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.pnlPrintSelect.ResumeLayout(false);
		this.pnlPrintSelect.PerformLayout();
		this.panName.ResumeLayout(false);
		this.panName.PerformLayout();
		this.clsBackPanel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
