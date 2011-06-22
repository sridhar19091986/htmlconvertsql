using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;

namespace Soccer_Score_Forecast
{
    public class ExportToExcel
    {
       public static void DataGridView2Txt(DataGridView dataGridView1, string filename,int column)
        {
            FileStream sr = File.Open(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(sr, System.Text.Encoding.Default);
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count ; i++)
                {
                    strBuilder = new StringBuilder();
                    for (int j = column; j < dataGridView1.Columns.Count; j++)
                    {
                        strBuilder.Append(dataGridView1.Rows[i].Cells[j].Value.ToString() +' ');
                    }
                    strBuilder.Remove(strBuilder.Length - 1, 1);
                    sw.WriteLine(strBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                sr.Close();
            }
        }
        //导出到EXCEL速度比较快的方法
        public static bool ExportForDataGridview(DataGridView gridView, string fileName, bool isShowExcle)
        {
            //建立Excel对象 
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (app == null)
                {
                    return false;
                }

                app.Visible = isShowExcle;
                Workbooks workbooks = app.Workbooks;
                _Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Sheets sheets = workbook.Worksheets;
                _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                if (worksheet == null)
                {
                    return false;
                }
                string sLen = "";
                //取得最后一列列名 
                char H = (char)(64 + gridView.ColumnCount / 26);
                char L = (char)(64 + gridView.ColumnCount % 26);
                if (gridView.ColumnCount < 26)
                {
                    sLen = L.ToString();
                }
                else
                {
                    sLen = H.ToString() + L.ToString();
                }
                //名称
                worksheet.Name = fileName;
                //标题 
                string sTmp = sLen + "1";
                Range ranCaption = worksheet.get_Range(sTmp, "A1");
                string[] asCaption = new string[gridView.ColumnCount];
                for (int i = 0; i < gridView.ColumnCount; i++)
                {
                    asCaption[i] = gridView.Columns[i].HeaderText;
                }
                ranCaption.Value2 = asCaption;
                //数据 
                object[] obj = new object[gridView.Columns.Count];
                for (int r = 0; r < gridView.RowCount ; r++)
                {
                    for (int l = 0; l < gridView.Columns.Count; l++)
                    {
                        if (gridView[l, r].ValueType == typeof(DateTime))
                        {
                            obj[l] = gridView[l, r].Value.ToString();
                        }
                        else
                        {
                            obj[l] = gridView[l, r].Value;
                        }
                    }
                    string cell1 = sLen + ((int)(r + 2)).ToString();
                    string cell2 = "A" + ((int)(r + 2)).ToString();
                    Range ran = worksheet.get_Range(cell1, cell2);
                    ran.Value2 = obj;

                }
                //保存 
                //workbook.SaveCopyAs(fileName);
                //workbook.Saved = true;
            }
            finally
            {
                //关闭 
                //app.UserControl = false;
                //app.Quit();
            }
            return true;
        }
    }
}
