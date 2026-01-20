using Microsoft.EntityFrameworkCore;
using NutriSmart.Models;

namespace NutriSmart.Data
{
    public class NutriSmartDbContext : DbContext
    {
        public NutriSmartDbContext(DbContextOptions<NutriSmartDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<FoodAllergy> FoodAllergies { get; set; }
        public DbSet<UserAllergy> UserAllergies { get; set; }
        public DbSet<DietProgram> DietPrograms { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FoodAllergy Many-to-Many
            modelBuilder.Entity<FoodAllergy>()
                .HasKey(fa => new { fa.FoodId, fa.AllergyId });

            modelBuilder.Entity<FoodAllergy>()
                .HasOne(fa => fa.Food)
                .WithMany(f => f.FoodAllergies)
                .HasForeignKey(fa => fa.FoodId);

            modelBuilder.Entity<FoodAllergy>()
                .HasOne(fa => fa.Allergy)
                .WithMany(a => a.FoodAllergies)
                .HasForeignKey(fa => fa.AllergyId);

            // UserAllergy Many-to-Many
            modelBuilder.Entity<UserAllergy>()
                .HasKey(ua => new { ua.UserId, ua.AllergyId });

            modelBuilder.Entity<UserAllergy>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAllergies)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAllergy>()
                .HasOne(ua => ua.Allergy)
                .WithMany(a => a.UserAllergies)
                .HasForeignKey(ua => ua.AllergyId);

            // DietProgram Relationships
            modelBuilder.Entity<DietProgram>()
                .HasOne(dp => dp.User)
                .WithMany(u => u.DietPrograms)
                .HasForeignKey(dp => dp.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<DietProgram>()
                .HasOne(dp => dp.Diyetisyen)
                .WithMany() // Assuming we don't need a collection on Diyetisyen for programs assigned
                .HasForeignKey(dp => dp.DiyetisyenId)
                .OnDelete(DeleteBehavior.Restrict);


            // Seed Data: Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Ad = "Admin",
                    Soyad = "System",
                    Email = "admin@nutri.com",
                    Sifre = "Admin123!", // In real app, hash this!
                    Rol = UserRole.Admin,
                    ProfilResmi = "default_admin.png"
                },
                new User
                {
                    Id = 2,
                    Ad = "Ayşe",
                    Soyad = "Yılmaz",
                    Email = "diyetisyen@nutri.com",
                    Sifre = "Diyetisyen123!",
                    Rol = UserRole.Dietitian,
                    ProfilResmi = "default_user.png"
                },
                new User
                {
                    Id = 3,
                    Ad = "Mehmet",
                    Soyad = "Demir",
                    Email = "mehmet@example.com",
                    Sifre = "Danisan123!",
                    Rol = UserRole.Client,
                    Boy = 175,
                    Kilo = 80,
                    ProfilResmi = "default_user.png"
                },
                new User
                {
                    Id = 4,
                    Ad = "Zeynep",
                    Soyad = "Kaya",
                    Email = "zeynep@example.com",
                    Sifre = "Danisan123!",
                    Rol = UserRole.Client,
                    Boy = 165,
                    Kilo = 58,
                    ProfilResmi = "default_user.png"
                }
            );

            // Seed Data: Allergies
            modelBuilder.Entity<Allergy>().HasData(
                new Allergy { Id = 1, Ad = "Gluten" },
                new Allergy { Id = 2, Ad = "Laktoz" },
                new Allergy { Id = 3, Ad = "Fıstık" },
                new Allergy { Id = 4, Ad = "Kabuklu Deniz Ürünleri" },
                new Allergy { Id = 5, Ad = "Yumurta" },
                new Allergy { Id = 6, Ad = "Soya" },
                new Allergy { Id = 7, Ad = "Bal" },
                new Allergy { Id = 8, Ad = "Susam" }
            );

            // Seed Data: Foods
            // --- MEYVELER ---
            modelBuilder.Entity<Food>().HasData(
                new Food { Id = 1, Ad = "Elma", Kalori = 52, Icerik = "100g, C Vitamini, Lif", ResimYolu = "apple.jpg" },
                new Food { Id = 2, Ad = "Muz", Kalori = 89, Icerik = "100g, Potasyum, B6", ResimYolu = "banana.jpg" },
                new Food { Id = 3, Ad = "Portakal", Kalori = 47, Icerik = "100g, C Vitamini", ResimYolu = "orange.jpg" },
                new Food { Id = 4, Ad = "Çilek", Kalori = 32, Icerik = "100g, Antioksidan", ResimYolu = "strawberry.jpg" },
                new Food { Id = 5, Ad = "Kivi", Kalori = 61, Icerik = "100g, C Vitamini", ResimYolu = "kiwi.jpg" },
                new Food { Id = 6, Ad = "Üzüm", Kalori = 69, Icerik = "100g, Şekerli", ResimYolu = "grapes.jpg" },
                new Food { Id = 7, Ad = "Karpuz", Kalori = 30, Icerik = "100g, Su oranı yüksek", ResimYolu = "watermelon.jpg" },
                new Food { Id = 8, Ad = "Kavun", Kalori = 34, Icerik = "100g, A Vitamini", ResimYolu = "melon.jpg" },
                new Food { Id = 9, Ad = "Şeftali", Kalori = 39, Icerik = "100g, Lifli", ResimYolu = "peach.jpg" },
                new Food { Id = 10, Ad = "Armut", Kalori = 57, Icerik = "100g, Lifli", ResimYolu = "pear.jpg" },
                new Food { Id = 11, Ad = "Ananas", Kalori = 50, Icerik = "100g, Bromelain", ResimYolu = "pineapple.jpg" },
                new Food { Id = 12, Ad = "Kiraz", Kalori = 50, Icerik = "100g, Antioksidan", ResimYolu = "cherry.jpg" },
                // --- SEBZELER ---
                new Food { Id = 20, Ad = "Ispanak", Kalori = 23, Icerik = "100g, Demir, K Vitamini", ResimYolu = "spinach.jpg" },
                new Food { Id = 21, Ad = "Brokoli", Kalori = 34, Icerik = "100g, C Vitamini", ResimYolu = "broccoli.jpg" },
                new Food { Id = 22, Ad = "Havuç", Kalori = 41, Icerik = "100g, A Vitamini", ResimYolu = "carrot.jpg" },
                new Food { Id = 23, Ad = "Domates", Kalori = 18, Icerik = "100g, Likopen", ResimYolu = "tomato.jpg" },
                new Food { Id = 24, Ad = "Salatalık", Kalori = 15, Icerik = "100g, Su oranı yüksek", ResimYolu = "cucumber.jpg" },
                new Food { Id = 25, Ad = "Biber", Kalori = 20, Icerik = "100g, C Vitamini", ResimYolu = "pepper.jpg" },
                new Food { Id = 26, Ad = "Patlıcan", Kalori = 25, Icerik = "100g, Lifli", ResimYolu = "eggplant.jpg" },
                new Food { Id = 27, Ad = "Kabak", Kalori = 17, Icerik = "100g, Düşük kalori", ResimYolu = "zucchini.jpg" },
                // --- PROTEİN ---
                new Food { Id = 40, Ad = "Tavuk Göğsü", Kalori = 165, Icerik = "100g, Yüksek Protein", ResimYolu = "chicken_breast.jpg" },
                new Food { Id = 41, Ad = "Dana Kıyma", Kalori = 250, Icerik = "100g, Protein, Demir", ResimYolu = "beef_mince.jpg" },
                new Food { Id = 42, Ad = "Somon", Kalori = 208, Icerik = "100g, Omega-3", ResimYolu = "salmon.jpg" },
                new Food { Id = 43, Ad = "Yumurta", Kalori = 155, Icerik = "100g, Tam Protein", ResimYolu = "egg.jpg" },
                new Food { Id = 44, Ad = "Ton Balığı", Kalori = 132, Icerik = "100g, Protein", ResimYolu = "tuna.jpg" },
                // --- BAKLİYAT ---
                new Food { Id = 50, Ad = "Pirinç", Kalori = 130, Icerik = "100g (pişmiş), Karbonhidrat", ResimYolu = "rice.jpg" },
                new Food { Id = 51, Ad = "Bulgur", Kalori = 83, Icerik = "100g (pişmiş), Lifli", ResimYolu = "bulgur.jpg" },
                new Food { Id = 52, Ad = "Mercimek", Kalori = 116, Icerik = "100g (pişmiş), Protein, Lif", ResimYolu = "lentils.jpg" },
                new Food { Id = 53, Ad = "Nohut", Kalori = 164, Icerik = "100g (pişmiş), Protein, Lif", ResimYolu = "chickpeas.jpg" },
                new Food { Id = 54, Ad = "Kuru Fasulye", Kalori = 127, Icerik = "100g (pişmiş), Protein, Lif", ResimYolu = "beans.jpg" },
                new Food { Id = 55, Ad = "Makarna", Kalori = 131, Icerik = "100g (pişmiş), Karbonhidrat", ResimYolu = "pasta.jpg" },
                // --- SÜT ÜRÜNLERİ ---
                new Food { Id = 60, Ad = "Süt (Tam Yağlı)", Kalori = 61, Icerik = "100ml, Kalsiyum", ResimYolu = "milk.jpg" },
                new Food { Id = 61, Ad = "Yoğurt", Kalori = 59, Icerik = "100g, Probiyotik", ResimYolu = "yogurt.jpg" },
                new Food { Id = 62, Ad = "Beyaz Peynir", Kalori = 310, Icerik = "100g, Protein, Kalsiyum", ResimYolu = "cheese_white.jpg" },
                new Food { Id = 63, Ad = "Kaşar Peyniri", Kalori = 402, Icerik = "100g, Protein, Yağ", ResimYolu = "cheese_cheddar.jpg" },
                new Food { Id = 64, Ad = "Tereyağı", Kalori = 717, Icerik = "100g, Yağ", ResimYolu = "butter.jpg" },
                // --- KURUYEMİŞ & ABUR CUBUR ---
                new Food { Id = 70, Ad = "Ceviz", Kalori = 654, Icerik = "100g, Omega-3", ResimYolu = "walnut.jpg" },
                new Food { Id = 71, Ad = "Badem", Kalori = 579, Icerik = "100g, E Vitamini", ResimYolu = "almond.jpg" },
                new Food { Id = 72, Ad = "Fıstık", Kalori = 567, Icerik = "100g, Protein", ResimYolu = "peanut.jpg" },
                new Food { Id = 73, Ad = "Çikolata (Bitter)", Kalori = 546, Icerik = "100g, Antioksidan", ResimYolu = "chocolate_dark.jpg" },
                new Food { Id = 74, Ad = "Cips", Kalori = 536, Icerik = "100g, Yağlı", ResimYolu = "chips.jpg" }
            );

            // Seed Data: FoodAllergies
             modelBuilder.Entity<FoodAllergy>().HasData(
                new FoodAllergy { FoodId = 60, AllergyId = 2 }, // Süt -> Laktoz
                new FoodAllergy { FoodId = 61, AllergyId = 2 }, // Yoğurt -> Laktoz
                new FoodAllergy { FoodId = 62, AllergyId = 2 }, // Peynir -> Laktoz
                new FoodAllergy { FoodId = 63, AllergyId = 2 }, // Kaşar -> Laktoz
                new FoodAllergy { FoodId = 64, AllergyId = 2 }, // Tereyağı -> Laktoz
                new FoodAllergy { FoodId = 72, AllergyId = 3 }, // Fıstık -> Fıstık
                new FoodAllergy { FoodId = 55, AllergyId = 1 }, // Makarna -> Gluten
                new FoodAllergy { FoodId = 43, AllergyId = 5 }  // Yumurta -> Yumurta
            );

            // Seed Data: UserAllergies (sample allergies for clients)
            modelBuilder.Entity<UserAllergy>().HasData(
                new UserAllergy { UserId = 3, AllergyId = 2 }, // Mehmet -> Laktoz
                new UserAllergy { UserId = 4, AllergyId = 1 }, // Zeynep -> Gluten
                new UserAllergy { UserId = 4, AllergyId = 3 }  // Zeynep -> Fıstık
            );

            // Seed Data: Recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Ad = "Mevsim Salatası",
                    Malzemeler = "Kıvırcık, Domates, Salatalık, Zeytinyağı, Limon",
                    Talimatlar = "Tüm sebzeleri yıkayıp doğrayın. Sosu ekleyip karıştırın.",
                    HazirlamaSuresi = 10,
                    PisirmeSuresi = 0,
                    KisiSayisi = 2,
                    Kalori = 150,
                    Kategori = "Salata",
                    ResimYolu = "salad.jpg"
                },
                new Recipe
                {
                    Id = 2,
                    Ad = "Izgara Tavuk",
                    Malzemeler = "Tavuk Göğsü, Tuz, Karabiber, Kekik",
                    Talimatlar = "Tavukları baharatlayıp ızgarada her iki tarafını pişirin.",
                    HazirlamaSuresi = 15,
                    PisirmeSuresi = 20,
                    KisiSayisi = 4,
                    Kalori = 300,
                    Kategori = "Ana Yemek",
                    ResimYolu = "grilled_chicken.jpg"
                },
                new Recipe
                {
                    Id = 3,
                    Ad = "Yulaflı Smoothie",
                    Malzemeler = "Yulaf, Süt, Muz, Bal",
                    Talimatlar = "Tüm malzemeleri blenderdan geçirin.",
                    HazirlamaSuresi = 5,
                    PisirmeSuresi = 0,
                    KisiSayisi = 1,
                    Kalori = 250,
                    Kategori = "İçecek",
                    ResimYolu = "smoothie.jpg"
                }
            );
        }
    }
}
