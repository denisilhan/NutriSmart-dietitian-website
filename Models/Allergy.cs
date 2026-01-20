using System.ComponentModel.DataAnnotations;

namespace NutriSmart.Models
{
    public class Allergy
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Ad { get; set; }

        public ICollection<FoodAllergy> FoodAllergies { get; set; } = new List<FoodAllergy>();
        public ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
    }
}
