using System.Data.Entity;

namespace KiqtChartApp3
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ReadExcelDbConnectionString") {}
        //public DataContext() : base(AppHelper.ConnectionString) { }

        public DbSet<ExamData> ExamData { get; set; }
    }
}
