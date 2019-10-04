using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiqtChartApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var context = new Entities())
            {
                var query = context.ExamDatas.ToList();
                foreach (var exam in query)
                {
                    chart1.Series[0].Points.AddXY(exam.Xvalue, exam.Yvalue);
                }                
            }
        }
    }
}
