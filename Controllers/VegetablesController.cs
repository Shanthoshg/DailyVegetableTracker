
using DailyVegetableTracker.Data;
using DailyVegetableTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyVegetableTracker.Controllers
{
    public class VegetablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VegetablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all vegetables
        public async Task<IActionResult> Index()
        {
            var vegetables = await _context.Vegetables
                .AsNoTracking()
                .OrderBy(v => v.Name)
                .ToListAsync();

            return View(vegetables);
        }

        // Display the create form
        public IActionResult Create()
        {
            return View();
        }

        // Save a new vegetable
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vegetable vegetable)
        {
            if (ModelState.IsValid)
            {
                var nameExists = await _context.Vegetables
                    .AnyAsync(v => v.Name == vegetable.Name);

                if (nameExists)
                {
                    ModelState.AddModelError(
                        nameof(vegetable.Name),
                        "This vegetable already exists.");
                    return View(vegetable);
                }

                _context.Vegetables.Add(vegetable);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Vegetable added successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(vegetable);
        }

        // Display the edit form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var vegetable = await _context.Vegetables
                .FindAsync(id);

            if (vegetable == null)
                return NotFound();

            return View(vegetable);
        }

        // Save vegetable changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, Vegetable vegetable)
        {
            if (id != vegetable.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(vegetable);

            var nameExists = await _context.Vegetables
                .AnyAsync(v =>
                    v.Name == vegetable.Name &&
                    v.Id != vegetable.Id);

            if (nameExists)
            {
                ModelState.AddModelError(
                    nameof(vegetable.Name),
                    "Another vegetable already uses this name.");
                return View(vegetable);
            }

            _context.Vegetables.Update(vegetable);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Vegetables
                    .AnyAsync(v => v.Id == vegetable.Id))
                    return NotFound();

                throw;
            }

            TempData["Success"] = "Vegetable updated successfully!";
            return RedirectToAction(nameof(Index));
        }

                    // Display details of one vegetable
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var vegetable = await _context.Vegetables
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vegetable == null)
                return NotFound();

            return View(vegetable);
        }



        // Display the delete confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var vegetable = await _context.Vegetables
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vegetable == null)
                return NotFound();

            return View(vegetable);
        }

        // Delete after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vegetable = await _context.Vegetables
                .FindAsync(id);

            if (vegetable == null)
                return NotFound();

            var hasPurchases = await _context.Purchases
                .AnyAsync(p => p.VegetableId == id);

            if (hasPurchases)
            {
                TempData["Error"] =
                    "This vegetable has purchase records and cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            _context.Vegetables.Remove(vegetable);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Vegetable deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
