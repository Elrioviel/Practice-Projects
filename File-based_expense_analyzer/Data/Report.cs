using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_based_expense_analyzer.Data
{
    public class Report
    {
        public decimal TotalSpent { get; set; }
        public List<ReportDetails> Details { get; set; } = new List<ReportDetails>();
        public decimal AveragePerDay { get; set; }
    }

    public class ReportDetails
    {
        public Category Category { get; set; } = new Category();
        public decimal TotalSpentByCategory { get; set; }
    }
}
