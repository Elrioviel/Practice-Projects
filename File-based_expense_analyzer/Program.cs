using File_based_expense_analyzer.Data;
using System.Globalization;

string filePath = string.Empty;
List<Expense> expenses = new List<Expense>();
Report report = new Report();
List<DateTime> expenseDates = new();
List<string> categories = new();

Console.WriteLine("Enter file path:");
filePath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
{
    Console.WriteLine("Invalid file path.");
    return;
}

foreach (string line in File.ReadLines(filePath))
{
    Expense expense = new Expense()
    {
        Date = DateTime.Parse(line.Split(';')[0]),
        CategoryName = new Category() { Name = line.Split(';')[1] },
        Amount = decimal.Parse(line.Split(';')[2], CultureInfo.InvariantCulture)
    };

    expenses.Add(expense);
}

// Create a list of unique expense dates.
foreach (var exp in expenses)
{
    if (!expenseDates.Contains(exp.Date))
    {
        expenseDates.Add(exp.Date);
    }
}

// Create a list of unique categories.
foreach (var exp in expenses)
{
    if (!categories.Contains(exp.CategoryName.Name))
    {
        categories.Add(exp.CategoryName.Name.ToString());
    }
}

// Seek identical categories and dates and calculate total spent and average per day.
foreach (var category in categories)
{
    decimal totalSpentByCategory = 0;
    foreach (var exp in expenses)
    {
        if (exp.CategoryName.Name.ToString() == category)
        {
            totalSpentByCategory += exp.Amount;
        }
    }

    report.Details.Add(new ReportDetails()
    {
        Category = new Category()
        {
            Name = category,
        },
        //Category = category,
        TotalSpentByCategory = totalSpentByCategory
    });
}

decimal totalSpent = 0;
foreach (var detail in report.Details)
{
    totalSpent += detail.TotalSpentByCategory;
}

report.TotalSpent = totalSpent;
report.AveragePerDay = totalSpent / expenseDates.Count;

// Create report file.
string reportFileName = "report.txt";
string directory = Path.GetDirectoryName(filePath);
string reportFilePath = Path.Combine(directory!, reportFileName);

if (File.Exists(reportFilePath))
{
    File.Delete(reportFilePath);
}

using (StreamWriter sw = File.CreateText(reportFilePath))
{
    sw.WriteLine($"Total Spent: {report.TotalSpent}");
    sw.WriteLine("Details by Category:");
    foreach (var detail in report.Details)
    {
        sw.WriteLine($"{detail.Category.Name}: {detail.TotalSpentByCategory}");
    }
    sw.WriteLine($"Average Per Day: {report.AveragePerDay}");
}