﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiqtChartApp2
{
    public class AppHelper
    {
        public static string ConnectionString
            => ConfigurationManager.ConnectionStrings["ReadExcelDbConnectionString"].ConnectionString;
    }
}
