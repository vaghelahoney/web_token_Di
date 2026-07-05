using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_token_Di.Migrations
{
    /// <inheritdoc />
    public partial class addPriceCuloumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Employee",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Employee");
        }
    }
}
