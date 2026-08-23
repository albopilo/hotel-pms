using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using Excel;

namespace LockSoftware;

internal class ClsComm
{
	public class ExcelConfig
	{
		public bool Title_Font_Bold;

		public bool Cell_Font_Bold;

		public int Title_Font_Size = 8;

		public int Cell_Font_Size = 8;

		public int Title_Font_Color;

		public int Cell_Font_Color;

		public int Title_Interior_Color;

		public int Cell_Interior_Color;

		private void Set_ExcelConfig(bool Title_Font_Bold, bool Cell_Font_Bold, int Title_Font_Size, int Cell_Font_Size, int Title_Font_Color, int Cell_Font_Color, int Title_Interior_Color, int Cell_Interior_Color)
		{
			this.Title_Font_Bold = Title_Font_Bold;
			this.Title_Font_Color = Title_Font_Color;
			this.Title_Font_Size = Title_Font_Size;
			this.Title_Interior_Color = Title_Interior_Color;
			this.Cell_Font_Bold = Cell_Font_Bold;
			this.Cell_Font_Color = Cell_Font_Color;
			this.Cell_Font_Size = Cell_Font_Size;
			this.Cell_Interior_Color = Cell_Interior_Color;
		}
	}

	public static DataSet ExcelToDataSet(string opnFileName, string sql)
	{
		string text = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + opnFileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2;'";
		OleDbConnection oleDbConnection = null;
		string text2 = "";
		OleDbDataAdapter oleDbDataAdapter = null;
		DataSet dataSet = new DataSet();
		text2 = sql;
		try
		{
			oleDbConnection = new OleDbConnection(text);
			oleDbConnection.Open();
			oleDbDataAdapter = new OleDbDataAdapter(text2, text);
			oleDbDataAdapter.Fill(dataSet, "dtSource");
			return dataSet;
		}
		catch
		{
			oleDbDataAdapter?.Dispose();
			if (oleDbConnection != null)
			{
				if (oleDbConnection.State == ConnectionState.Open)
				{
					oleDbConnection.Close();
				}
				oleDbConnection.Dispose();
			}
			return null;
		}
	}

	public static bool ExportToExcel(System.Data.DataTable dt)
	{
		Excel.Application application = null;
		try
		{
			application = new ApplicationClass();
			application.Visible = true;
			application.Workbooks.Add(true);
			long num = dt.Columns.Count;
			long num2 = dt.Rows.Count;
			for (int i = 0; i < num; i++)
			{
				application.Cells[1, i + 1] = dt.Columns[i].ColumnName.ToString();
			}
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num; k++)
				{
					application.Cells[j + 2, k + 1] = dt.Rows[j][k].ToString().Trim();
				}
			}
			application.UserControl = true;
		}
		catch (Exception ex)
		{
			if (application != null)
			{
				KillProcess("Excel");
				GC.Collect();
			}
			throw new Exception(ex.Message);
		}
		return true;
	}

	public static bool ExportToExcel(System.Data.DataTable dt, string sheetName, ExcelConfig config, bool StatCol, int StatColType)
	{
		Excel.Application application = null;
		Range range = null;
		try
		{
			application = new ApplicationClass();
			application.Visible = true;
			application.Workbooks.Add(true);
			long num = dt.Columns.Count;
			long num2 = dt.Rows.Count;
			for (int i = 0; i < num; i++)
			{
				application.Cells[1, i + 1] = dt.Columns[i].ColumnName.ToString();
				range = (Range)application.Cells[1, i + 1];
				range.Interior.ColorIndex = config.Title_Interior_Color;
				range.Font.Bold = config.Title_Font_Bold;
				range.Font.Size = config.Title_Font_Size;
				range.BorderAround(XlLineStyle.xlContinuous);
				range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
			}
			try
			{
				for (int j = 0; j < num2; j++)
				{
					for (int k = 0; k < num; k++)
					{
						dt.Rows[j][k].GetType();
						application.Cells[j + 2, k + 1] = ((dt.Rows[j][k].GetType().Name == "String") ? ("'" + dt.Rows[j][k].ToString().Trim()) : dt.Rows[j][k].ToString().Trim());
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
			if (num > 1)
			{
				range.Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
			}
			num2++;
			if (StatCol && StatColType == 0)
			{
				application.Cells[num2 + 1, 1] = "总计 : ";
				range = (Range)application.Cells[num2 + 1, num];
				range.FormulaR1C1 = "=sum(R[-" + num2 + "]C:R[-1]C)";
				range.NumberFormatLocal = "￥#,##0.00;￥-#,##0.00";
				range.BorderAround(XlLineStyle.xlContinuous);
				range = (Range)application.Cells[num2 + 1, 1];
				range = range.get_Range(application.Cells[1, 1], application.Cells[1, num - 1]);
				range.MergeCells = true;
				range.BorderAround(XlLineStyle.xlContinuous);
			}
			application = null;
		}
		catch (Exception ex2)
		{
			if (application != null)
			{
				KillProcess("Excel");
				GC.Collect();
			}
			throw new Exception(ex2.Message);
		}
		return true;
	}

	public static bool ExportFormDataGridview(DataGridView gridView, string title, bool isShowExcle, ExcelConfig config, int startExCol, int startExRow, int startGVCol, int startGVRow)
	{
		Excel.Application application = new ApplicationClass();
		try
		{
			if (application == null)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < gridView.ColumnCount; i++)
			{
				if (gridView.Columns[i].Visible)
				{
					num++;
				}
			}
			for (int j = 0; j < gridView.RowCount; j++)
			{
				if (gridView.Rows[j].Visible)
				{
					num2++;
				}
			}
			Workbooks workbooks = application.Workbooks;
			_Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
			Sheets worksheets = workbook.Worksheets;
			_Worksheet worksheet = (_Worksheet)worksheets.get_Item((object)1);
			if (worksheet == null)
			{
				return false;
			}
			string text = "";
			char c = (char)(64 + (num - startGVCol) / 26);
			char c2 = (char)(64 + (num - startGVCol) % 26);
			text = ((num - startGVCol >= 26) ? (c.ToString() + c2) : c2.ToString());
			string cell = text + (startExRow + 1);
			if (title != "")
			{
				application.Cells[1, 1] = title;
				Range range = (Range)application.Cells[1, 1];
				range = range.get_Range(application.Cells[1, 1], application.Cells[1, num - startGVCol]);
				range.MergeCells = true;
				range.Font.Size = 20;
				range.Font.Name = "黑体";
				range.Font.Underline = true;
				range.RowHeight = 30;
			}
			string[] array = new string[num - startGVCol];
			int num3 = 0;
			for (int k = 0; k < gridView.ColumnCount - startGVCol; k++)
			{
				if (gridView.Columns[k + startGVCol].Visible)
				{
					array[num3++] = gridView.Columns[k + startGVCol].HeaderText;
				}
			}
			Range range2 = worksheet.get_Range((object)cell, (object)("A" + (startExRow + 1)));
			range2.Value2 = array;
			range2.Interior.ColorIndex = config.Title_Interior_Color;
			range2.Font.Bold = config.Title_Font_Bold;
			range2.Font.Size = config.Title_Font_Size;
			range2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
			range2.VerticalAlignment = XlVAlign.xlVAlignCenter;
			object[] array2 = new object[num - startGVCol];
			int num4 = 0;
			int num5 = 0;
			for (int l = 0; l < gridView.RowCount - startGVRow; l++)
			{
				if (!gridView.Rows[l].Visible)
				{
					continue;
				}
				num4 = 0;
				for (int m = 0; m < gridView.Columns.Count - startGVCol; m++)
				{
					if (gridView.Columns[m + startGVCol].Visible)
					{
						array2[num4] = null;
						if (gridView[m + startGVCol, l + startGVRow].Value == null)
						{
							num4++;
						}
						else
						{
							array2[num4++] = ((gridView[m + startGVCol, l + startGVRow].ValueType.Name == "String") ? ("'" + gridView[m + startGVCol, l + startGVRow].Value.ToString()) : gridView[m + startGVCol, l + startGVRow].Value.ToString());
						}
					}
				}
				string cell2 = text + (num5 + 2 + startExRow);
				string cell3 = "A" + (num5 + 2 + startExRow);
				Range range3 = worksheet.get_Range((object)cell2, (object)cell3);
				range3.Value2 = array2;
				num5++;
			}
			range2 = worksheet.get_Range(application.Cells[startExRow + 1, startExCol + 1], application.Cells[startExRow + num2 - startGVRow + 1, startExCol + num - startGVCol]);
			range2.Borders.Weight = 2;
			range2.Columns.AutoFit();
			application.Visible = isShowExcle;
		}
		finally
		{
			application.UserControl = false;
			application.Quit();
		}
		return true;
	}

	public static void KillProcess(string processName)
	{
		new Process();
		try
		{
			Process[] processesByName = Process.GetProcessesByName(processName);
			foreach (Process process in processesByName)
			{
				if (!process.CloseMainWindow())
				{
					process.Kill();
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}
