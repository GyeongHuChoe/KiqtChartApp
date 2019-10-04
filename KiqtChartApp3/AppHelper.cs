using System.Configuration;

namespace KiqtChartApp3
{
    public class AppHelper
    {
        public static string ConnectionString
            => ConfigurationManager.ConnectionStrings["ReadExcelDbConnectionString"].ConnectionString;
    }
}
