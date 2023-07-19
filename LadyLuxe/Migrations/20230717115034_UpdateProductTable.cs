using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LadyLuxe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sub_CatehoryId",
                table: "Products",
                newName: "Sub_CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sub_CategoryId",
                table: "Products",
                newName: "Sub_CatehoryId");
        }
    }
}
