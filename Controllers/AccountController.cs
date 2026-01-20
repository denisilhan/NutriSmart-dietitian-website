using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;
using NutriSmart.Models;

namespace NutriSmart.Controllers
{
    public class AccountController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public AccountController(NutriSmartDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Lütfen e-posta ve şifre giriniz.";
                return View();
            }

            // Find user (In production, use hashed passwords!)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Sifre == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Ad + " " + user.Soyad),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Geçersiz e-posta veya şifre.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string ad, string soyad, string email, string password)
        {
            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Lütfen tüm alanları doldurunuz.";
                return View();
            }

            // Check if email exists
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                ViewBag.Error = "Bu e-posta adresi zaten kullanımda.";
                return View();
            }

            var newUser = new User
            {
                Ad = ad,
                Soyad = soyad,
                Email = email,
                Sifre = password, // In production hash this!
                Rol = UserRole.Client, // Default role
                ProfilResmi = "default_user.png"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Auto login after register
             var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newUser.Ad + " " + newUser.Soyad),
                new Claim(ClaimTypes.Email, newUser.Email),
                new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
                new Claim(ClaimTypes.Role, newUser.Rol.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
