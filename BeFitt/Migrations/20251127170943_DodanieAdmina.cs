using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFitt.Migrations
{
    /// <inheritdoc />
    public partial class DodanieAdmina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d4869507-2c1b-4f7d-b5df-1234567890ab", null, "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4869507-2c1b-4f7d-b5df-1234567890ab");
        }
    }
}
