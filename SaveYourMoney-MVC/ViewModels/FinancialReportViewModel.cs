using System;
using System.Collections.Generic;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.ViewModels
{
    public class FinancialReportViewModel
    {
        public double TotalIncome { get; set; }
        public double TotalExpenditure { get; set; }
        public double TotalSavings => TotalIncome - TotalExpenditure;
        public double NetValue => TotalIncome - TotalExpenditure;

        public List<CategorySummary> CategorySummaries { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<Expense> TopExpenses { get; set; }
        public List<Expense> BottomExpenses { get; set; }

        // Monthly breakdown properties
        public List<int> MonthlyLabels { get; set; } // List of months (1-12)
        public List<List<double>> MonthlyCategoryAmounts { get; set; } // Monthly amounts for each category

        // Selected year for the report
        public int SelectedYear { get; set; }

        public class CategorySummary
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public double TotalAmount { get; set; }
            public double Budget { get; set; }
            public bool IsOverBudget { get; set; }
        }
    }
}
