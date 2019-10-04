using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiqtChartApp
{
    class GraphData
    {
        //public int Num { get; set; }
        //public float Sec { get; set; }
        //public double Y { get; set; }
        //public double X { get; set; }

        // 멤버변수들
        private double yvalue;   // y축
        private double xvalue;     // x축

        // 생성자

        public GraphData()
        {
 
        }

        public GraphData(double xvalue, double yvalue)
        {
            this.xvalue = xvalue;
            this.yvalue = yvalue;
        }

        public double Xvalue
        {
            set { xvalue = value; }          get { return xvalue; }
        }

        public double Yvalue
        {
            set { yvalue = value; }
            get { return yvalue; }
        }

    }
}
