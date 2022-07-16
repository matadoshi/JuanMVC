using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class ProductTableSomeColumnDropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
