using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;
using NutriSmart.Models;

namespace NutriSmart.Controllers
{
    public class FoodsController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public FoodsController(NutriSmartDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int pageSize = 12;
            var query = _context.Foods.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(f => f.Ad.ToLower().Contains(search.ToLower()) || f.Icerik.ToLower().Contains(search.ToLower()));
            }

            // FILTER: Show only foods with images
            query = query.Where(f => !string.IsNullOrEmpty(f.ResimYolu));

            int totalItems = await query.CountAsync();
            var foods = await query.OrderBy(f => f.Ad)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .Include(f => f.FoodAllergies)
                                   .ThenInclude(fa => fa.Allergy)
                                   .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.Search = search;

            return View(foods);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Foods
                .Include(f => f.FoodAllergies)
                .ThenInclude(fa => fa.Allergy)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (food == null) return NotFound();

            return View(food);
        }
    }
}
