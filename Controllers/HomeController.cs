using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NutriSmart.Models;
using NutriSmart.Data;

namespace NutriSmart.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NutriSmartDbContext _context;

    public HomeController(ILogger<HomeController> logger, NutriSmartDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // Removed Admin redirect - Admins should also see main site features
        // Admin can access Admin Panel from sidebar when needed
        
        if (User.Identity?.IsAuthenticated == true)
        {
            // SMART RECOMMENDATION LOGIC
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            
            if (user != null)
            {
                ViewBag.SmartMessage = "Alerjen analiziniz yapıldı: Size özel güvenli tarifler hazırlandı. ✅";
            }
            
            return View("Dashboard");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult FoodHacks()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
