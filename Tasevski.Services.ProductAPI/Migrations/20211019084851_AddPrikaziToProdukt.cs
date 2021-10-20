using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasevski.Services.ProductAPI.Migrations
{
    public partial class AddPrikaziToProdukt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Prikazi",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prikazi",
                table: "Products");
        }
    }
}
