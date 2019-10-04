using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiqtChartApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTableCollection tableCollection;

        // 엑셀파일 읽기
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TxtFileName.Text = ofd.FileName;
                    using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });
                            tableCollection = result.Tables;
                            CboSheet.Items.Clear();
                            foreach (DataTable table in tableCollection)
                            {
                                CboSheet.Items.Add(table.TableName);    // 콤보에 sheet추가하기
                            }
                        }
                    }
                }
            }
        }

        // 데이터를 챠트에 바인딩
        private void CboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = tableCollection[CboSheet.SelectedItem.ToString()];

            //if (dt != null)
            //{
            //    List<GraphData> Info = new List<GraphData>();

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        GraphData gdata = new GraphData(Convert.ToDouble(dt.Rows[i]["x"]), Convert.ToDouble(dt.Rows[i]["y"]));
            //        info.Add(new GraphData(gdata.Xvalue, gdata.Yvalue));
            //    }
            //    chart1.Series[0].Points.DataBind(info, "Xvalue", "Yvalue", null);
            //}


            DataTable dt = tableCollection[CboSheet.SelectedItem.ToString()];
 
            if (dt != null)
            {
                List<GraphData> Info = new List<GraphData>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GraphData gdata = new GraphData();
                    gdata.Xvalue = Convert.ToDouble(dt.Rows[i]["x"]);
                    gdata.Yvalue = Convert.ToDouble(dt.Rows[i]["y"]);
                    Info.Add(gdata);
                }
                graphDataBindingSource.DataSource = Info;
            }


        }


    }
}
