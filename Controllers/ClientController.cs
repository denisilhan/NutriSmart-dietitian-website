using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;
using NutriSmart.Models;
using System.Security.Claims;

namespace NutriSmart.Controllers
{
    [Authorize(Roles = "Client,Dietitian,Admin")] // Dietitian/Admin can also view just in case
    public class ClientController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public ClientController(NutriSmartDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MyReport()
        {
            // Get current user ID
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return Forbid();
            if (!int.TryParse(userIdStr, out int userId)) return Forbid();

            var client = await _context.Users
                .Include(u => u.UserAllergies)
                    .ThenInclude(ua => ua.Allergy)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (client == null) return NotFound();

            // Client's Allegy IDs
            var clientAllergyIds = client.UserAllergies.Select(ua => ua.AllergyId).ToList();

            // Fetch All Foods with their allergens
            var allFoods = await _context.Foods
                .Include(f => f.FoodAllergies)
                    .ThenInclude(fa => fa.Allergy)
                .ToListAsync();

            // Safe Foods: Contains NO allergens matching client's list
            var safeFoods = allFoods
                .Where(f => !f.FoodAllergies.Any(fa => clientAllergyIds.Contains(fa.AllergyId)))
                .OrderBy(f => f.Ad)
                .ToList();

            // Risky Foods: Contains AT LEAST ONE of client's allergens
            var riskyFoods = allFoods
                .Where(f => f.FoodAllergies.Any(fa => clientAllergyIds.Contains(fa.AllergyId)))
                .OrderBy(f => f.Ad)
                .ToList();

            ViewBag.SafeFoods = safeFoods;
            ViewBag.RiskyFoods = riskyFoods;

            return View(client);
        }
    }
}
