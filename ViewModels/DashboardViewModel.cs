
using DailyVegetableTracker.Models;

namespace DailyVegetableTracker.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TodaySpending { get; set; }

        public decimal MonthSpending { get; set; }

        public int TodayPurchaseCount { get; set; }

        public int MonthPurchaseCount { get; set; }

        public List<Purchase> RecentPurchases { get; set; } = new();

public List<Vegetable> Vegetables { get; set; } = new();

public List<VegetableSpendingViewModel> SpendingByVegetable
    { get; set; } = new();
    }
        

    public class VegetableSpendingViewModel
    {
        public string VegetableName { get; set; } = string.Empty;

        public decimal TotalSpent { get; set; }
    }

    
}
