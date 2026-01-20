using System.ComponentModel.DataAnnotations;

namespace NutriSmart.Models
{
    public enum Ogun
    {
        Kahvalti,
        Ogle,
        Aksam,
        AraOgun
    }

    public class DietProgram
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Danışan
        public User User { get; set; } = null!;

        public int DiyetisyenId { get; set; }
        public User Diyetisyen { get; set; } = null!;

        public DateTime Tarih { get; set; }

        public Ogun Ogun { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;
        
        public string? Notlar { get; set; }
    }
}
