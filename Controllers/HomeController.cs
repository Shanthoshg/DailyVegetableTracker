
using System.Diagnostics;
using DailyVegetableTracker.Data;
using DailyVegetableTracker.Models;
using DailyVegetableTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyVegetableTracker.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        var monthStart = new DateTime(today.Year, today.Month, 1);
        var nextMonth = monthStart.AddMonths(1);

        var todayPurchases = _context.Purchases
            .AsNoTracking()
            .Where(p => p.PurchaseDate >= today
                     && p.PurchaseDate < tomorrow);

        var monthPurchases = _context.Purchases
            .AsNoTracking()
            .Where(p => p.PurchaseDate >= monthStart
                     && p.PurchaseDate < nextMonth);
        ViewBag.VegetableCount = await _context.Vegetables
    .AsNoTracking()
    .CountAsync();

    
        var model = new DashboardViewModel
        {


            Vegetables = await _context.Vegetables
    .AsNoTracking()
    .OrderBy(v => v.Name)
    .ToListAsync(),


            TodaySpending = await todayPurchases
                .SumAsync(p => (decimal?)(p.Quantity * p.PricePerUnit)) ?? 0m,

            MonthSpending = await monthPurchases
                .SumAsync(p => (decimal?)(p.Quantity * p.PricePerUnit)) ?? 0m,

            TodayPurchaseCount = await todayPurchases.CountAsync(),

            MonthPurchaseCount = await monthPurchases.CountAsync(),

            RecentPurchases = await _context.Purchases
                .AsNoTracking()
                .Include(p => p.Vegetable)
                .OrderByDescending(p => p.PurchaseDate)
                .ThenByDescending(p => p.Id)
                .Take(5)
                .ToListAsync(),

            SpendingByVegetable = await monthPurchases
                .GroupBy(p => p.Vegetable!.Name)
                .Select(g => new VegetableSpendingViewModel
                {
                    VegetableName = g.Key,
                    TotalSpent = g.Sum(p => p.Quantity * p.PricePerUnit)
                })
                .OrderByDescending(x => x.TotalSpent)
                .ToListAsync()
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
        });
    }
}
