using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductSeed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "01J138JZQQT6J3FR6AA06YPRJA");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[] { "01J138QHTM3NBZ2VHCS8S9D1HQ", "Metade de um par de meias", 5.9m, 99 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "01J138QHTM3NBZ2VHCS8S9D1HQ");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[] { "01J138JZQQT6J3FR6AA06YPRJA", "Camiseta No Internet", 69.9m, 30 });
        }
    }
}
