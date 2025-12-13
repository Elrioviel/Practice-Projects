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
        public required Category CategoryName { get; set; }
        public decimal AveragePerDay { get; set; }
    }
}
