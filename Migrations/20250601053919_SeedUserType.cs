using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "UserTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "UserTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "UserTypeId",
                keyValue: 2);
        }
    }
}
