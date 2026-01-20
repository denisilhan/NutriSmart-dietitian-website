namespace NutriSmart.Models
{
    public class FoodAllergy
    {
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; } = null!;
    }

    public class UserAllergy
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; } = null!;
    }
}
