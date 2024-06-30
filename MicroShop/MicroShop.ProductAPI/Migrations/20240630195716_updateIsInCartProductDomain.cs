using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateIsInCartProductDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInCart",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInCart",
                table: "Products");
        }
    }
}
