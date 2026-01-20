using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutriSmart.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Allergies",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Gluten" },
                    { 2, "Laktoz" },
                    { 3, "Fıstık" },
                    { 4, "Kabuklu Deniz Ürünleri" },
                    { 5, "Yumurta" },
                    { 6, "Soya" },
                    { 7, "Bal" },
                    { 8, "Susam" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[,]
                {
                    { 1, "Tavuk Göğsü (100g)", "Protein zengini, yağsız et", 165, null },
                    { 2, "Yulaf Ezmesi (100g)", "Lif zengini, tam tahıl", 389, null },
                    { 3, "Yumurta (1 adet)", "Protein ve vitamin kaynağı", 78, null },
                    { 4, "Süt (1 bardak)", "Kalsiyum ve D vitamini", 149, null },
                    { 5, "Elma (1 adet)", "Lif ve antioksidan", 95, null },
                    { 6, "Somon (100g)", "Omega-3 yağ asitleri", 208, null },
                    { 7, "Brokoli (100g)", "C vitamini ve lif", 34, null },
                    { 8, "Fıstık Ezmesi (2 tbsp)", "Sağlıklı yağlar, protein", 188, null },
                    { 9, "Ekmek (1 dilim)", "Karbonhidrat kaynağı", 79, null },
                    { 10, "Yoğurt (1 kase)", "Probiyotik, kalsiyum", 100, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Ad", "Boy", "DiyetisyenId", "Email", "IsDeleted", "Kilo", "ProfilResmi", "Rol", "Sifre", "Soyad" },
                values: new object[,]
                {
                    { 2, "Ayşe", null, null, "diyetisyen@nutri.com", false, null, "default_user.png", 1, "Diyetisyen123!", "Yılmaz" },
                    { 3, "Mehmet", 175f, null, "mehmet@example.com", false, 80f, "default_user.png", 2, "Danisan123!", "Demir" },
                    { 4, "Zeynep", 165f, null, "zeynep@example.com", false, 58f, "default_user.png", 2, "Danisan123!", "Kaya" }
                });

            migrationBuilder.InsertData(
                table: "FoodAllergies",
                columns: new[] { "AllergyId", "FoodId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 5, 3 },
                    { 2, 4 },
                    { 3, 8 },
                    { 1, 9 },
                    { 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "UserAllergies",
                columns: new[] { "AllergyId", "UserId" },
                values: new object[,]
                {
                    { 2, 3 },
                    { 1, 4 },
                    { 3, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserAllergies",
                keyColumns: new[] { "AllergyId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "UserAllergies",
                keyColumns: new[] { "AllergyId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "UserAllergies",
                keyColumns: new[] { "AllergyId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
