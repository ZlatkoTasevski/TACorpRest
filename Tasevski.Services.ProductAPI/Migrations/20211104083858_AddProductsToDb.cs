using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasevski.Services.ProductAPI.Migrations
{
    public partial class AddProductsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prikazi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price", "Prikazi" },
                values: new object[,]
                {
                    { 1, "Готвени јадења", "Вкусни телешки шницли кои ќе ги обожавате. Добар апетит!", "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/meso-snicla.jpg", "Tелешки шницли во сос", 450.0, false },
                    { 2, "Готвени јадења", "Ароматични компири со рузмарин. Добар апетит!", "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/kompiri-tava.jpg", "Вкусни ароматични компири", 250.0, false },
                    { 3, "Јадења по порачка", "Навистина брз, вкусен и здрав оброк. Добар апетит!", "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/spanak-jajca.jpg", "Здрав оброк од спанаќ и јајца", 180.0, false },
                    { 4, "Entree", "Вкусно и сочно мешано месо. Добар апетит!", "https://dotnettasevski.blob.core.windows.net/tasevskirestaurant/Sliki/pilesko.jpg", "Мешано месо во вкусен сос", 350.0, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
