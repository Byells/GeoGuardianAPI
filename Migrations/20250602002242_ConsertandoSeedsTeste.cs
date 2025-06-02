using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class ConsertandoSeedsTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admingeo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTime(2025, 6, 2, 0, 10, 39, 246, DateTimeKind.Utc).AddTicks(5949), "21232F297A57A5A743894A0E4A801FC3" });
        }
    }
}
