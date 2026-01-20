using Microsoft.AspNetCore.Mvc;

namespace NutriSmart.Controllers
{
    public class CalculatorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BMI()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BMI(double weight, double height)
        {
            if (weight > 0 && height > 0)
            {
                // Height usually comes in cm, convert to meters
                double heightInMeters = height / 100.0;
                double bmi = weight / (heightInMeters * heightInMeters);
                ViewBag.Result = Math.Round(bmi, 2);
                ViewBag.Weight = weight;
                ViewBag.Height = height;

                if (bmi < 18.5) ViewBag.Status = "Zayıf";
                else if (bmi < 25) ViewBag.Status = "Normal";
                else if (bmi < 30) ViewBag.Status = "Fazla Kilolu";
                else ViewBag.Status = "Obez";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Calorie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calorie(string gender, double weight, double height, int age, string activityLevel)
        {
            if (weight > 0 && height > 0 && age > 0)
            {
                // Mifflin-St Jeor Equation
                double bmr;
                if (gender == "male")
                {
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
                }
                else
                {
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) - 161;
                }

                // Parse activity level safely (handles "1.2" regardless of culture)
                double activity = 1.2;
                if (!string.IsNullOrEmpty(activityLevel))
                {
                    double.TryParse(activityLevel, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out activity);
                }

                double tdee = bmr * activity;
                ViewBag.Result = Math.Round(tdee);
                ViewBag.BMR = Math.Round(bmr);
            }
            return View();
        }

        [HttpGet]
        public IActionResult IdealWeight()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IdealWeight(string gender, double height)
        {
            // Robinson Formula (Simple approximation)
            if (height > 0)
            {
                 // Convert cm to inches for formula or use metric approximation
                 // Metric (Lemmens): 22 * height(m)^2
                 double heightInMeters = height / 100.0;
                 double ideal = 22 * (heightInMeters * heightInMeters);
                 
                 ViewBag.Result = Math.Round(ideal, 1);
            }
            return View();
        }
    }
}
