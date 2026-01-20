using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutriSmart.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Malzemeler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Talimatlar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HazirlamaSuresi = table.Column<int>(type: "int", nullable: false),
                    PisirmeSuresi = table.Column<int>(type: "int", nullable: false),
                    KisiSayisi = table.Column<int>(type: "int", nullable: false),
                    ResimYolu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kalori = table.Column<int>(type: "int", nullable: true),
                    Kategori = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Elma", "100g, C Vitamini, Lif", 52, "apple.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Muz", "100g, Potasyum, B6", 89, "banana.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Portakal", "100g, C Vitamini", 47, "orange.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Çilek", "100g, Antioksidan", 32, "strawberry.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Kivi", "100g, C Vitamini", 61, "kiwi.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Üzüm", "100g, Şekerli", 69, "grapes.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Karpuz", "100g, Su oranı yüksek", 30, "watermelon.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Kavun", "100g, A Vitamini", 34, "melon.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Şeftali", "100g, Lifli", 39, "peach.jpg" });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Armut", "100g, Lifli", 57, "pear.jpg" });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[,]
                {
                    { 11, "Ananas", "100g, Bromelain", 50, "pineapple.jpg" },
                    { 12, "Kiraz", "100g, Antioksidan", 50, "cherry.jpg" },
                    { 20, "Ispanak", "100g, Demir, K Vitamini", 23, "spinach.jpg" },
                    { 21, "Brokoli", "100g, C Vitamini", 34, "broccoli.jpg" },
                    { 22, "Havuç", "100g, A Vitamini", 41, "carrot.jpg" },
                    { 23, "Domates", "100g, Likopen", 18, "tomato.jpg" },
                    { 24, "Salatalık", "100g, Su oranı yüksek", 15, "cucumber.jpg" },
                    { 25, "Biber", "100g, C Vitamini", 20, "pepper.jpg" },
                    { 26, "Patlıcan", "100g, Lifli", 25, "eggplant.jpg" },
                    { 27, "Kabak", "100g, Düşük kalori", 17, "zucchini.jpg" },
                    { 40, "Tavuk Göğsü", "100g, Yüksek Protein", 165, "chicken_breast.jpg" },
                    { 41, "Dana Kıyma", "100g, Protein, Demir", 250, "beef_mince.jpg" },
                    { 42, "Somon", "100g, Omega-3", 208, "salmon.jpg" },
                    { 43, "Yumurta", "100g, Tam Protein", 155, "egg.jpg" },
                    { 44, "Ton Balığı", "100g, Protein", 132, "tuna.jpg" },
                    { 50, "Pirinç", "100g (pişmiş), Karbonhidrat", 130, "rice.jpg" },
                    { 51, "Bulgur", "100g (pişmiş), Lifli", 83, "bulgur.jpg" },
                    { 52, "Mercimek", "100g (pişmiş), Protein, Lif", 116, "lentils.jpg" },
                    { 53, "Nohut", "100g (pişmiş), Protein, Lif", 164, "chickpeas.jpg" },
                    { 54, "Kuru Fasulye", "100g (pişmiş), Protein, Lif", 127, "beans.jpg" },
                    { 55, "Makarna", "100g (pişmiş), Karbonhidrat", 131, "pasta.jpg" },
                    { 60, "Süt (Tam Yağlı)", "100ml, Kalsiyum", 61, "milk.jpg" },
                    { 61, "Yoğurt", "100g, Probiyotik", 59, "yogurt.jpg" },
                    { 62, "Beyaz Peynir", "100g, Protein, Kalsiyum", 310, "cheese_white.jpg" },
                    { 63, "Kaşar Peyniri", "100g, Protein, Yağ", 402, "cheese_cheddar.jpg" },
                    { 64, "Tereyağı", "100g, Yağ", 717, "butter.jpg" },
                    { 70, "Ceviz", "100g, Omega-3", 654, "walnut.jpg" },
                    { 71, "Badem", "100g, E Vitamini", 579, "almond.jpg" },
                    { 72, "Fıstık", "100g, Protein", 567, "peanut.jpg" },
                    { 73, "Çikolata (Bitter)", "100g, Antioksidan", 546, "chocolate_dark.jpg" },
                    { 74, "Cips", "100g, Yağlı", 536, "chips.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Ad", "EklenmeTarihi", "HazirlamaSuresi", "IsDeleted", "Kalori", "Kategori", "KisiSayisi", "Malzemeler", "PisirmeSuresi", "ResimYolu", "Talimatlar" },
                values: new object[,]
                {
                    { 1, "Mevsim Salatası", new DateTime(2026, 1, 12, 15, 50, 52, 52, DateTimeKind.Local).AddTicks(6169), 10, false, 150, "Salata", 2, "Kıvırcık, Domates, Salatalık, Zeytinyağı, Limon", 0, "salad.jpg", "Tüm sebzeleri yıkayıp doğrayın. Sosu ekleyip karıştırın." },
                    { 2, "Izgara Tavuk", new DateTime(2026, 1, 12, 15, 50, 52, 52, DateTimeKind.Local).AddTicks(6184), 15, false, 300, "Ana Yemek", 4, "Tavuk Göğsü, Tuz, Karabiber, Kekik", 20, "grilled_chicken.jpg", "Tavukları baharatlayıp ızgarada her iki tarafını pişirin." },
                    { 3, "Yulaflı Smoothie", new DateTime(2026, 1, 12, 15, 50, 52, 52, DateTimeKind.Local).AddTicks(6188), 5, false, 250, "İçecek", 1, "Yulaf, Süt, Muz, Bal", 0, "smoothie.jpg", "Tüm malzemeleri blenderdan geçirin." }
                });

            migrationBuilder.InsertData(
                table: "FoodAllergies",
                columns: new[] { "AllergyId", "FoodId" },
                values: new object[,]
                {
                    { 5, 43 },
                    { 1, 55 },
                    { 2, 60 },
                    { 2, 61 },
                    { 2, 62 },
                    { 2, 63 },
                    { 2, 64 },
                    { 3, 72 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 5, 43 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 1, 55 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 60 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 61 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 62 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 63 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 2, 64 });

            migrationBuilder.DeleteData(
                table: "FoodAllergies",
                keyColumns: new[] { "AllergyId", "FoodId" },
                keyValues: new object[] { 3, 72 });

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 72);

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

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Tavuk Göğsü (100g)", "Protein zengini, yağsız et", 165, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Yulaf Ezmesi (100g)", "Lif zengini, tam tahıl", 389, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Yumurta (1 adet)", "Protein ve vitamin kaynağı", 78, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Süt (1 bardak)", "Kalsiyum ve D vitamini", 149, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Elma (1 adet)", "Lif ve antioksidan", 95, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Somon (100g)", "Omega-3 yağ asitleri", 208, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Brokoli (100g)", "C vitamini ve lif", 34, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Fıstık Ezmesi (2 tbsp)", "Sağlıklı yağlar, protein", 188, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Ekmek (1 dilim)", "Karbonhidrat kaynağı", 79, null });

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Ad", "Icerik", "Kalori", "ResimYolu" },
                values: new object[] { "Yoğurt (1 kase)", "Probiyotik, kalsiyum", 100, null });
        }
    }
}
