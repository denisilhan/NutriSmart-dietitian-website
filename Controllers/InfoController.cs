using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;
using NutriSmart.Models;

namespace NutriSmart.Controllers
{
    public class InfoController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public InfoController(NutriSmartDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> About()
        {
            var dietitians = await _context.Users
                .Where(u => u.Rol == UserRole.Dietitian && !u.IsDeleted)
                .Take(4)
                .ToListAsync();

            return View(dietitians);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
