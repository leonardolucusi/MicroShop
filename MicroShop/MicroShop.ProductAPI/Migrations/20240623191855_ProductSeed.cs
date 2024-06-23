using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stick",
                table: "Products",
                newName: "Stock");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[] { "01J138JZQQT6J3FR6AA06YPRJA", "Camiseta No Internet", 69.9m, 30 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "01J138JZQQT6J3FR6AA06YPRJA");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "Stick");
        }
    }
}
