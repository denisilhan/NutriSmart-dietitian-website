using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;

namespace NutriSmart.Controllers
{
    public class AllergiesController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public AllergiesController(NutriSmartDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Allergies.Include(a => a.FoodAllergies).ThenInclude(fa => fa.Food).ToListAsync());
        }
    }
}
