using File_based_expense_analyzer.Data;
using System.Globalization;

string filePath = string.Empty;
List<Expense> expenses = new List<Expense>();
Report report = new Report();
HashSet<DateTime> expenseDates = new();
Dictionary<string, decimal> totalsByCategory = new();

Console.WriteLine("Enter file path:");
filePath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
{
    Console.WriteLine("Invalid file path.");
    return;
}

foreach (string line in File.ReadLines(filePath))
{
    string[] parts = line.Split(';');

    if (parts.Length != 3)
    {
        Console.WriteLine($"Skipping invalid line (wrong format): {line}");
        continue;
    }

    if (!DateTime.TryParse(parts[0], out DateTime date))
    {
        Console.WriteLine($"Skipping invalid line (bad date): {line}");
        continue;
    }

    if (!decimal.TryParse(parts[2], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount))
    {
        Console.WriteLine($"Skipping invalid line (bad amount): {line}");
        continue;
    }

    string category = parts[1];

    // track unique days
    expenseDates.Add(date.Date);

    // Accumulate totals per category
    if (totalsByCategory.ContainsKey(category))
    {
        totalsByCategory[category] += amount;
    }
    else
    {
        totalsByCategory[category] = amount;
    }
}

// Build report details from dictionary
foreach (var kvp in totalsByCategory)
{
    report.Details.Add(new ReportDetails
    {
        Category = kvp.Key,
        TotalSpentByCategory = kvp.Value
    });
}

decimal totalSpent = 0;
foreach (var amount in totalsByCategory.Values)
{
    totalSpent += amount;
}

report.TotalSpent = totalSpent;
report.AveragePerDay = expenseDates.Count == 0 ? 0 : totalSpent / expenseDates.Count;

// Create report file.
string reportFileName = "report.txt";
string directory = Path.GetDirectoryName(filePath);
string reportFilePath = Path.Combine(directory!, reportFileName);

using (StreamWriter sw = File.CreateText(reportFilePath))
{
    sw.WriteLine($"Total Spent: {report.TotalSpent}");
    sw.WriteLine();
    sw.WriteLine("Details by Category:");

    foreach (var detail in report.Details)
    {
        sw.WriteLine($"{detail.Category}: {detail.TotalSpentByCategory}");
    }

    sw.WriteLine();
    sw.WriteLine($"Average Per Day: {report.AveragePerDay}");
}

Console.WriteLine("Report generated successfully.");