
using DailyVegetableTracker.Data;
using DailyVegetableTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DailyVegetableTracker.Controllers;

public class PurchasesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PurchasesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var purchases = await _context.Purchases
            .AsNoTracking()
            .Include(p => p.Vegetable)
            .OrderByDescending(p => p.PurchaseDate)
            .ThenByDescending(p => p.Id)
            .ToListAsync();

        return View(purchases);
    }

    public async Task<IActionResult> Create()
    {
        await LoadVegetablesAsync();

        return View(new Purchase
        {
            PurchaseDate = DateTime.Today,
            Unit = "kg"
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Purchase purchase)
    {
        if (!await _context.Vegetables
                .AnyAsync(v => v.Id == purchase.VegetableId))
        {
            ModelState.AddModelError(
                nameof(purchase.VegetableId),
                "Please select a valid vegetable.");
        }

        if (ModelState.IsValid)
        {
            var vegetable = await _context.Vegetables
                .FirstAsync(v => v.Id == purchase.VegetableId);

            purchase.Unit = vegetable.Unit;

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Purchase added successfully!";

            return RedirectToAction(nameof(Index));
        }

        await LoadVegetablesAsync(purchase.VegetableId);

        return View(purchase);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var purchase = await _context.Purchases.FindAsync(id);

        if (purchase == null)
            return NotFound();

        await LoadVegetablesAsync(purchase.VegetableId);

        return View(purchase);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Purchase purchase)
    {
        if (id != purchase.Id)
            return NotFound();

        if (!await _context.Vegetables
                .AnyAsync(v => v.Id == purchase.VegetableId))
        {
            ModelState.AddModelError(
                nameof(purchase.VegetableId),
                "Please select a valid vegetable.");
        }

        if (ModelState.IsValid)
        {
            var vegetable = await _context.Vegetables
                .FirstAsync(v => v.Id == purchase.VegetableId);

            purchase.Unit = vegetable.Unit;

            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Purchase updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        await LoadVegetablesAsync(purchase.VegetableId);

        return View(purchase);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var purchase = await _context.Purchases
            .AsNoTracking()
            .Include(p => p.Vegetable)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (purchase == null)
            return NotFound();

        return View(purchase);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var purchase = await _context.Purchases.FindAsync(id);

        if (purchase == null)
            return NotFound();

        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Purchase deleted successfully!";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadVegetablesAsync(int? selectedId = null)
    {
        var vegetables = await _context.Vegetables
            .AsNoTracking()
            .OrderBy(v => v.Name)
            .ToListAsync();

        ViewBag.VegetableId = new SelectList(
            vegetables,
            nameof(Vegetable.Id),
            nameof(Vegetable.Name),
            selectedId);
    }
}
