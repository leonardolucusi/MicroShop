using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MicroShop.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "01J138QHTM3NBZ2VHCS8S9D1HQ");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { "01J138T7V73QA71J1D6SVGFHTA", "Camiseta", 15.9m, 23 },
                    { "01J138T7V7HSBWDN1D0S6NB5PV", "Metade de um par de meias", 5.9m, 99 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "01J138T7V73QA71J1D6SVGFHTA");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "01J138T7V7HSBWDN1D0S6NB5PV");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[] { "01J138QHTM3NBZ2VHCS8S9D1HQ", "Metade de um par de meias", 5.9m, 99 });
        }
    }
}
