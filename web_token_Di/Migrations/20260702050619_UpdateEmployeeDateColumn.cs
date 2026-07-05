using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_token_Di.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Employee",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Employee",
                newName: "DateTime");
        }
    }
}
