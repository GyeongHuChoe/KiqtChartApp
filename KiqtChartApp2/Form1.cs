using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace KiqtChartApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTableCollection tableCollection;

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog=new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TxtFileName.Text = openFileDialog.FileName;
                    using(var stream=File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader=ExcelReaderFactory.CreateReader(stream))
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

        private void CboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[CboSheet.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;

            if (dt != null)
            {
                List<ExamData> examdata = new List<ExamData>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ExamData exam = new ExamData();
                    exam.Num = Convert.ToInt32(dt.Rows[i]["Num"]);
                    exam.Sec = dt.Rows[i]["Sec"].ToString();
                    exam.Xvalue = dt.Rows[i]["x"].ToString();
                    exam.Yvalue = dt.Rows[i]["y"].ToString();
                    examdata.Add(exam);
                }
                examDataBindingSource.DataSource = examdata;
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                DapperPlusManager.Entity<ExamData>().Table("ExamData");
                List<ExamData> examdata = examDataBindingSource.DataSource as List<ExamData>;
                if (examdata != null)
                {
                    using (IDbConnection db = new SqlConnection(AppHelper.ConnectionString))
                    {
                        db.BulkInsert(examdata);
                    }
                }
                MessageBox.Show("Finish~");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
