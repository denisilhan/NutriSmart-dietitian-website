using System.ComponentModel.DataAnnotations;

namespace NutriSmart.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Ad { get; set; }

        public int Kalori { get; set; }

        public string? Icerik { get; set; }

        public string? ResimYolu { get; set; }

        // Navigation Properties
        public ICollection<FoodAllergy> FoodAllergies { get; set; } = new List<FoodAllergy>();
    }
}
