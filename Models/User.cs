using System.ComponentModel.DataAnnotations;

namespace NutriSmart.Models
{
    public enum UserRole
    {
        Admin,
        Dietitian,
        Client
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Ad { get; set; }

        [Required]
        [StringLength(50)]
        public required string Soyad { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Sifre { get; set; } // Gerçek uygulamalarda Hashlenmeli!

        public UserRole Rol { get; set; }

        public string? ProfilResmi { get; set; }

        // Navigation Properties
        public ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
        public ICollection<DietProgram> DietPrograms { get; set; } = new List<DietProgram>(); // Danışanın programları
        
        // Diyetisyen ise, danışanları olabilir (veya tam tersi ilişki gereksinimlere göre)
        // Basitlik için şimdilik admin/dietitian tüm clientları görebilir varsayıyoruz. 
        // Veya Dietitian -> Client ilişkisi eklenebilir. 
        // User tablosunda 'AssignedDietitianId' tutulabilir.
        public int? DiyetisyenId { get; set; }
        public User? Diyetisyen { get; set; } // Client ise atanan diyetisyen
        public ICollection<User> Danisanlar { get; set; } = new List<User>(); // Diyetisyen ise danışanları

        // Client specific fields (Requested later but good to add now if simple)
        public float? Boy { get; set; }
        public float? Kilo { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
