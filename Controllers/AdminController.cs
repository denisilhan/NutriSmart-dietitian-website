using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;
using NutriSmart.Models;

namespace NutriSmart.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public AdminController(NutriSmartDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            // deleted users are hidden
            return View(await _context.Users.Where(u => !u.IsDeleted).ToListAsync());
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Check email
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Bu email adresi zaten kayıtlı.");
                    return View(user);
                }

                user.ProfilResmi = "default_user.png"; // Default
                // In production, Hash user.Sifre here!
                
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null || user.IsDeleted) return NotFound();

            return View(user);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FindAsync(id);
                    if (existingUser == null) return NotFound();

                    existingUser.Ad = user.Ad;
                    existingUser.Soyad = user.Soyad;
                    existingUser.Email = user.Email;
                    existingUser.Rol = user.Rol;
                    // Keep existing password if not changed mechanism isn't here, 
                    // simplifying for now assuming full overwrite or careful handling.
                    // Ideally we don't bind Sifre for Edit unless explicitly changed.
                    // For this demo, let's assume we re-save the potentially plain text password
                    // OR better: we should ignore password update here if it's separate. 
                    // But let's keep it simple as per user request scope.
                    if (!string.IsNullOrEmpty(user.Sifre))
                    {
                         existingUser.Sifre = user.Sifre;
                    }
                    
                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null || user.IsDeleted) return NotFound();

            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                // PROTECT ADMIN 
                // Checks: ID 1, specific email, or if they are the last admin? 
                // Simple check: Don't delete self or 'admin@nutri.com'
                var currentEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? User.Identity?.Name;
                if (user.Email == "admin@nutri.com" || user.Id == 1 || (User.Identity?.IsAuthenticated == true && user.Email == currentEmail))
                {
                     TempData["ErrorMessage"] = "Yönetici hesabı silinemez!";
                     return RedirectToAction(nameof(Index));
                }

                // Soft Delete
                user.IsDeleted = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
