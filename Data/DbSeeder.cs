using NutriSmart.Models;

namespace NutriSmart.Data
{
    public static class DbSeeder
    {
        public static void Seed(NutriSmartDbContext context)
        {
            // Veri varsa tekrar ekleme yapma
            if (context.Users.Any() && context.Foods.Any())
            {
                return;
            }

            // 1. Alerjenler
            var allergies = new List<Allergy>
            {
                new Allergy { Ad = "Gluten" },
                new Allergy { Ad = "Laktoz" },
                new Allergy { Ad = "Yer Fıstığı" },
                new Allergy { Ad = "Yumurta" },
                new Allergy { Ad = "Deniz Ürünleri" }
            };

            if (!context.Allergies.Any())
            {
                context.Allergies.AddRange(allergies);
                context.SaveChanges();
            }

             // Yeniden çekelim ki Id'leri gelsin
            var dbAllergies = context.Allergies.ToList();

            // 2. Yemekler
            var foods = new List<Food>
            {
                new Food { Ad = "Izgara Tavuk Salata", Kalori = 350, Icerik = "Tavuk, Marul, Domates, Salatalık", ResimYolu = "salad.jpg" },
                new Food { Ad = "Somon Izgara", Kalori = 450, Icerik = "Somon, Kuşkonmaz, Limon", ResimYolu = "salmon.jpg" },
                new Food { Ad = "Fıstıklı Bar", Kalori = 200, Icerik = "Yulaf, Bal, Yer Fıstığı", ResimYolu = "bar.jpg" },
                new Food { Ad = "Sütlaç", Kalori = 300, Icerik = "Süt, Pirinç, Şeker", ResimYolu = "sutlac.jpg" },
                new Food { Ad = "Kepekli Makarna", Kalori = 400, Icerik = "Tam Buğday Makarna, Domates Sosu", ResimYolu = "pasta.jpg" }
            };

            if (!context.Foods.Any())
            {
                context.Foods.AddRange(foods);
                context.SaveChanges();
            }

            // Yeni eklenen yemekler ve besinler
            var newFoods = new List<Food>
            {
                new Food { Ad = "Kinoa Yemeği", Kalori = 220, Icerik = "Kinoa, Sebzeler, Zeytinyağı", ResimYolu = "kinoa.png" },
                new Food { Ad = "Ananas", Kalori = 50, Icerik = "100g, Bromelain", ResimYolu = "ananas.png" },
                new Food { Ad = "Armut", Kalori = 57, Icerik = "100g, Lifli", ResimYolu = "armut.png" },
                new Food { Ad = "Badem", Kalori = 579, Icerik = "100g, E Vitamini", ResimYolu = "badem.png" },
                new Food { Ad = "Beyaz Peynir", Kalori = 310, Icerik = "100g, Protein, Kalsiyum", ResimYolu = "beyaz_peynir.png" },
                new Food { Ad = "Biber", Kalori = 20, Icerik = "100g, C Vitamini", ResimYolu = "biber.png" },
                new Food { Ad = "Brokoli", Kalori = 34, Icerik = "100g, C Vitamini", ResimYolu = "brokoli.png" },
                new Food { Ad = "Bulgur", Kalori = 83, Icerik = "100g (pişmiş), Lifli", ResimYolu = "bulgur.png" },
                new Food { Ad = "Ceviz", Kalori = 654, Icerik = "100g, Omega-3", ResimYolu = "ceviz.png" },
                new Food { Ad = "Cips", Kalori = 536, Icerik = "100g, Yağlı", ResimYolu = "cips.png" },
                new Food { Ad = "Çikolata (Bitter)", Kalori = 546, Icerik = "100g, Antioksidan", ResimYolu = "cikolata_bitter.png" },
                new Food { Ad = "Çilek", Kalori = 32, Icerik = "100g, Antioksidan", ResimYolu = "strawberry.jpg" },
                new Food { Ad = "Dana Kıyma", Kalori = 250, Icerik = "100g, Protein, Demir", ResimYolu = "dana_kiyma.png" }
            };

            foreach (var food in newFoods)
            {
                if (!context.Foods.Any(f => f.Ad == food.Ad))
                {
                    context.Foods.Add(food);
                }
            }
            context.SaveChanges();
            
            var dbFoods = context.Foods.ToList();

            // 3. Yemek - Alerjen İlişkileri
            // Fıstıklı Bar -> Yer Fıstığı & Gluten (Yulaf)
            if (!context.FoodAllergies.Any())
            {
                var fistikliBar = dbFoods.FirstOrDefault(f => f.Ad == "Fıstıklı Bar");
                var sutlac = dbFoods.FirstOrDefault(f => f.Ad == "Sütlaç");
                var makarna = dbFoods.FirstOrDefault(f => f.Ad == "Kepekli Makarna");

                var fistik = dbAllergies.FirstOrDefault(a => a.Ad == "Yer Fıstığı");
                var gluten = dbAllergies.FirstOrDefault(a => a.Ad == "Gluten");
                var laktoz = dbAllergies.FirstOrDefault(a => a.Ad == "Laktoz");

                var foodAllergies = new List<FoodAllergy>();

                if (fistikliBar != null)
                {
                    if (fistik != null) foodAllergies.Add(new FoodAllergy { FoodId = fistikliBar.Id, AllergyId = fistik.Id });
                    if (gluten != null) foodAllergies.Add(new FoodAllergy { FoodId = fistikliBar.Id, AllergyId = gluten.Id });
                }

                if (sutlac != null && laktoz != null)
                {
                    foodAllergies.Add(new FoodAllergy { FoodId = sutlac.Id, AllergyId = laktoz.Id });
                }

                if (makarna != null && gluten != null)
                {
                    foodAllergies.Add(new FoodAllergy { FoodId = makarna.Id, AllergyId = gluten.Id });
                }

                context.FoodAllergies.AddRange(foodAllergies);
                context.SaveChanges();
            }

            // 4. Kullanıcılar (Diyetisyen ve Danışanlar)
            // Admin zaten DbContext OnModelCreating'de var ama buraya da ek kontroller koyabiliriz.
            
            if (!context.Users.Any(u => u.Email == "diyetisyen1@nutri.com"))
            {
                var diyetisyen1 = new User
                {
                    Ad = "Ayşe",
                    Soyad = "Yılmaz",
                    Email = "diyetisyen1@nutri.com",
                    Sifre = "Diyet123!",
                    Rol = UserRole.Dietitian,
                    ProfilResmi = "dietitian1.jpg"
                };

                context.Users.Add(diyetisyen1);
                context.SaveChanges(); // Id almak için kaydet

                var client1 = new User
                {
                    Ad = "Ahmet",
                    Soyad = "Demir",
                    Email = "client1@nutri.com",
                    Sifre = "Client123!",
                    Rol = UserRole.Client,
                    DiyetisyenId = diyetisyen1.Id,
                    Boy = 180,
                    Kilo = 85,
                    ProfilResmi = "client1.jpg"
                };

                var client2 = new User
                {
                    Ad = "Zeynep",
                    Soyad = "Kaya",
                    Email = "client2@nutri.com",
                    Sifre = "Client123!",
                    Rol = UserRole.Client,
                    DiyetisyenId = diyetisyen1.Id,
                    Boy = 165,
                    Kilo = 60,
                    ProfilResmi = "client2.jpg"
                };

                context.Users.AddRange(client1, client2);
                context.SaveChanges();

                // 5. Danışan Alerjileri
                // Ahmet'in (client1) Glutene alerjisi olsun
                var dbClient1 = context.Users.FirstOrDefault(u => u.Email == "client1@nutri.com");
                var gluten = dbAllergies.FirstOrDefault(a => a.Ad == "Gluten");

                if (dbClient1 != null && gluten != null)
                {
                    context.UserAllergies.Add(new UserAllergy { UserId = dbClient1.Id, AllergyId = gluten.Id });
                    context.SaveChanges();
                }
            }
        }
    }
}
