using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutriSmart.Data;
using NutriSmart.Models;

namespace NutriSmart.Controllers
{
    [Authorize(Roles = "Dietitian,Admin")]
    public class DietitianController : Controller
    {
        private readonly NutriSmartDbContext _context;

        public DietitianController(NutriSmartDbContext context)
        {
            _context = context;
        }

        // GET: Dietitian - Ana Panel
        public async Task<IActionResult> Index()
        {
            // Fix: User.Identity.Name might be mapped to Name claim, retrieve Email specifically
            var currentUserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? User.Identity?.Name;
            
            var dietitian = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
            
            if (dietitian == null) return Forbid();

            // İstatistikler
            var clientCount = await _context.Users.CountAsync(u => u.Rol == UserRole.Client && !u.IsDeleted);
            var foodCount = await _context.Foods.CountAsync();
            var allergyCount = await _context.Allergies.CountAsync();
            
            ViewBag.ClientCount = clientCount;
            ViewBag.FoodCount = foodCount;
            ViewBag.AllergyCount = allergyCount;
            ViewBag.DietitianName = $"{dietitian.Ad} {dietitian.Soyad}";
            
            return View();
        }

        // GET: Dietitian/Clients - Danışan Listesi
        public async Task<IActionResult> Clients()
        {
            var clients = await _context.Users
                .Where(u => u.Rol == UserRole.Client && !u.IsDeleted)
                .Include(u => u.UserAllergies)
                    .ThenInclude(ua => ua.Allergy)
                .OrderBy(u => u.Ad)
                .ThenBy(u => u.Soyad)
                .ToListAsync();
                
            return View(clients);
        }

        // GET: Dietitian/ClientDetails/5
        public async Task<IActionResult> ClientDetails(int? id)
        {
            if (id == null) return NotFound();

            var client = await _context.Users
                .Include(u => u.UserAllergies)
                    .ThenInclude(ua => ua.Allergy)
                .Include(u => u.DietPrograms)
                .FirstOrDefaultAsync(u => u.Id == id && u.Rol == UserRole.Client && !u.IsDeleted);

            if (client == null) return NotFound();

            // Tüm alerjenleri al
            ViewBag.AllAllergies = await _context.Allergies.OrderBy(a => a.Ad).ToListAsync();
            
            return View(client);
        }

        // POST: Dietitian/UpdateClientAllergies - Danışanın alerjenlerini güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClientAllergies(int clientId, int[]? selectedAllergies)
        {
            var client = await _context.Users
                .Include(u => u.UserAllergies)
                .FirstOrDefaultAsync(u => u.Id == clientId && u.Rol == UserRole.Client);

            if (client == null) return NotFound();

            // Mevcut alerjileri kaldır
            _context.UserAllergies.RemoveRange(client.UserAllergies);

            // Yeni alerjileri ekle
            if (selectedAllergies != null && selectedAllergies.Length > 0)
            {
                foreach (var allergyId in selectedAllergies)
                {
                    _context.UserAllergies.Add(new UserAllergy
                    {
                        UserId = clientId,
                        AllergyId = allergyId
                    });
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Alerjenler başarıyla güncellendi!";
            
            return RedirectToAction(nameof(ClientDetails), new { id = clientId });
        }

        // GET: Dietitian/CreateClient
        public IActionResult CreateClient()
        {
            return View();
        }

        // POST: Dietitian/CreateClient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient(User user)
        {
            // Remove validation for fields we will set manually or don't need
            ModelState.Remove("DiyetisyenId");
            ModelState.Remove("ProfilResmi");
            
            if (ModelState.IsValid)
            {
                // Email kontrolü
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kayıtlı.");
                    return View(user);
                }

                user.Rol = UserRole.Client;
                user.ProfilResmi = "default_user.png";
                user.IsDeleted = false;
                
                // Demo olduğu için şifreyi direkt kaydediyoruz. Prod'da hashlenmeli.
                // user.Sifre formdan geliyor.
                
                // İsteğe bağlı: Şu anki diyetisyeni bu danışana ata
                var currentUserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? User.Identity?.Name;
                var dietitian = await _context.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
                if (dietitian != null)
                {
                    user.DiyetisyenId = dietitian.Id;
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Yeni danışan başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Clients));
            }
            return View(user);
        }
        // GET: Dietitian/EditClient/5
        public async Task<IActionResult> EditClient(int? id)
        {
            if (id == null) return NotFound();

            var client = await _context.Users.FindAsync(id);
            if (client == null || client.Rol != UserRole.Client || client.IsDeleted) 
                return NotFound();

            return View(client);
        }

        // POST: Dietitian/EditClient/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(int id, [Bind("Id,Ad,Soyad,Email,Boy,Kilo")] User clientData)
        {
            if (id != clientData.Id) return NotFound();

            var existingClient = await _context.Users.FindAsync(id);
            if (existingClient == null || existingClient.Rol != UserRole.Client)
                return NotFound();

            // Sadece belirli alanları güncelle
            existingClient.Ad = clientData.Ad;
            existingClient.Soyad = clientData.Soyad;
            existingClient.Boy = clientData.Boy;
            existingClient.Kilo = clientData.Kilo;
            // Email güncellemesi riskli olabilir, opsiyonel
            
            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Danışan bilgileri güncellendi!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(ClientDetails), new { id });
        }

        // GET: Dietitian/RecommendFoods/5 - Kullanıcıya uygun yemekleri öner
        public async Task<IActionResult> RecommendFoods(int? id)
        {
            if (id == null) return NotFound();

            var client = await _context.Users
                .Include(u => u.UserAllergies)
                    .ThenInclude(ua => ua.Allergy)
                .FirstOrDefaultAsync(u => u.Id == id && u.Rol == UserRole.Client && !u.IsDeleted);

            if (client == null) return NotFound();

            // Kullanıcının alerjen ID'leri
            var clientAllergyIds = client.UserAllergies.Select(ua => ua.AllergyId).ToList();

            // Tüm besinler ve alerjenleri
            var allFoods = await _context.Foods
                .Include(f => f.FoodAllergies)
                    .ThenInclude(fa => fa.Allergy)
                .ToListAsync();

            // Güvenli besinler (hiç alerjen içermeyen veya kullanıcının alerjeni OLMAYAN)
            var safeFoods = allFoods
                .Where(f => !f.FoodAllergies.Any(fa => clientAllergyIds.Contains(fa.AllergyId)))
                .ToList();

            // Riskli besinler
            var riskyFoods = allFoods
                .Where(f => f.FoodAllergies.Any(fa => clientAllergyIds.Contains(fa.AllergyId)))
                .ToList();

            ViewBag.Client = client;
            ViewBag.SafeFoods = safeFoods;
            ViewBag.RiskyFoods = riskyFoods;

            return View();
        }

        // ========== ALERJEN YÖNETİMİ ==========
        
        // GET: Dietitian/Allergies
        public async Task<IActionResult> Allergies()
        {
            var allergies = await _context.Allergies
                .Include(a => a.UserAllergies)
                .OrderBy(a => a.Ad)
                .ToListAsync();
            return View(allergies);
        }

        // GET: Dietitian/CreateAllergy
        public IActionResult CreateAllergy()
        {
            return View();
        }

        // POST: Dietitian/CreateAllergy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAllergy([Bind("Ad")] Allergy allergy)
        {
            if (ModelState.IsValid)
            {
                // Aynı isimde alerjen var mı kontrol et
                if (await _context.Allergies.AnyAsync(a => a.Ad.ToLower() == allergy.Ad.ToLower()))
                {
                    ModelState.AddModelError("Ad", "Bu alerjen zaten mevcut.");
                    return View(allergy);
                }

                _context.Add(allergy);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Alerjen başarıyla eklendi!";
                return RedirectToAction(nameof(Allergies));
            }
            return View(allergy);
        }

        // POST: Dietitian/DeleteAllergy/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAllergy(int id)
        {
            var allergy = await _context.Allergies.FindAsync(id);
            if (allergy != null)
            {
                _context.Allergies.Remove(allergy);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Alerjen silindi.";
            }
            return RedirectToAction(nameof(Allergies));
        }

        // ========== BESİN YÖNETİMİ ==========
        
        // GET: Dietitian/Foods
        public async Task<IActionResult> Foods()
        {
            var foods = await _context.Foods
                .Include(f => f.FoodAllergies)
                    .ThenInclude(fa => fa.Allergy)
                .OrderBy(f => f.Ad)
                .ToListAsync();
            return View(foods);
        }

        // GET: Dietitian/CreateFood
        public async Task<IActionResult> CreateFood()
        {
            ViewBag.Allergies = await _context.Allergies.OrderBy(a => a.Ad).ToListAsync();
            return View();
        }

        // POST: Dietitian/CreateFood
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFood([Bind("Ad,Kalori,Icerik,ResimYolu")] Food food, int[]? selectedAllergies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();

                // Alerjenleri ekle
                if (selectedAllergies != null && selectedAllergies.Length > 0)
                {
                    foreach (var allergyId in selectedAllergies)
                    {
                        _context.FoodAllergies.Add(new FoodAllergy
                        {
                            FoodId = food.Id,
                            AllergyId = allergyId
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["SuccessMessage"] = "Besin başarıyla eklendi!";
                return RedirectToAction(nameof(Foods));
            }
            
            ViewBag.Allergies = await _context.Allergies.OrderBy(a => a.Ad).ToListAsync();
            return View(food);
        }

        // GET: Dietitian/EditFood/5
        public async Task<IActionResult> EditFood(int? id)
        {
            if (id == null) return NotFound();

            var food = await _context.Foods
                .Include(f => f.FoodAllergies)
                .FirstOrDefaultAsync(f => f.Id == id);
                
            if (food == null) return NotFound();

            ViewBag.Allergies = await _context.Allergies.OrderBy(a => a.Ad).ToListAsync();
            ViewBag.SelectedAllergyIds = food.FoodAllergies.Select(fa => fa.AllergyId).ToList();
            
            return View(food);
        }

        // POST: Dietitian/EditFood/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFood(int id, [Bind("Id,Ad,Kalori,Icerik,ResimYolu")] Food food, int[]? selectedAllergies)
        {
            if (id != food.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFood = await _context.Foods
                        .Include(f => f.FoodAllergies)
                        .FirstOrDefaultAsync(f => f.Id == id);
                    
                    if (existingFood == null) return NotFound();

                    existingFood.Ad = food.Ad;
                    existingFood.Kalori = food.Kalori;
                    existingFood.Icerik = food.Icerik;
                    existingFood.ResimYolu = food.ResimYolu;

                    // Mevcut alerjen ilişkilerini kaldır
                    _context.FoodAllergies.RemoveRange(existingFood.FoodAllergies);

                    // Yeni alerjen ilişkilerini ekle
                    if (selectedAllergies != null && selectedAllergies.Length > 0)
                    {
                        foreach (var allergyId in selectedAllergies)
                        {
                            _context.FoodAllergies.Add(new FoodAllergy
                            {
                                FoodId = id,
                                AllergyId = allergyId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Besin başarıyla güncellendi!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Foods.Any(f => f.Id == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Foods));
            }
            
            ViewBag.Allergies = await _context.Allergies.OrderBy(a => a.Ad).ToListAsync();
            return View(food);
        }

        // POST: Dietitian/DeleteFood/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Besin silindi.";
            }
            return RedirectToAction(nameof(Foods));
        }

        // POST: Dietitian/FixImagePaths
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FixImagePaths()
        {
            var foods = await _context.Foods.ToListAsync();
            int fixedCount = 0;
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var foodsPath = Path.Combine(webRootPath, "images", "foods");

            foreach (var food in foods)
            {
                if (string.IsNullOrEmpty(food.ResimYolu)) continue;

                var currentPath = Path.Combine(foodsPath, food.ResimYolu);
                if (!System.IO.File.Exists(currentPath))
                {
                    // Dosya yok, alternatifi ara (uzantı farkı)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(food.ResimYolu);
                    string[] extensions = { ".jpg", ".png", ".jpeg", ".webp" };
                    bool found = false;

                    foreach (var ext in extensions)
                    {
                        var altPath = Path.Combine(foodsPath, fileNameWithoutExt + ext);
                        if (System.IO.File.Exists(altPath))
                        {
                            food.ResimYolu = fileNameWithoutExt + ext;
                            _context.Update(food);
                            fixedCount++;
                            found = true;
                            break;
                        }
                    }

                    if (!found) 
                    {
                        // Hiçbir yerde yoksa null yapmayalım, belki dosya sonradan gelir.
                        // Ama loglanabilir.
                    }
                }
            }

            if (fixedCount > 0)
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"{fixedCount} adet besin görseli yolu düzeltildi.";
            }
            else
            {
                TempData["SuccessMessage"] = "Düzeltilecek görsel yolu bulunamadı.";
            }

            return RedirectToAction(nameof(Foods));
        }
    }
}
