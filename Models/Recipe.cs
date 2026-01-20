using System.ComponentModel.DataAnnotations;

namespace NutriSmart.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Ad { get; set; } = string.Empty;

        [Required]
        public string Malzemeler { get; set; } = string.Empty; // Malzeme listesi (virgülle ayrılmış veya JSON)

        [Required]
        public string Talimatlar { get; set; } = string.Empty; // Adım adım talimatlar

        public int HazirlamaSuresi { get; set; } // Dakika cinsinden

        public int PisirmeSuresi { get; set; } // Dakika cinsinden

        public int KisiSayisi { get; set; } = 1;

        public string? ResimYolu { get; set; }

        public int? Kalori { get; set; } // Tahmini kalori (opsiyonel)

        public string Kategori { get; set; } = "Genel"; // Örn: Ana Yemek, Tatlı, Çorba vb.

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
