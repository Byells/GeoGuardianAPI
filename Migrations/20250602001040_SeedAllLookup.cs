using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllLookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Brasil" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Created", "Email", "FullName", "Password", "UserTypeId" },
                values: new object[] { 1, new DateTime(2025, 6, 2, 0, 10, 39, 246, DateTimeKind.Utc).AddTicks(5949), "admin@geo.com", "Admin", "21232F297A57A5A743894A0E4A801FC3", 1 });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "StateId", "CountryId", "Name" },
                values: new object[] { 1, 1, "São Paulo" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "Name", "StateId" },
                values: new object[] { 1, "São Paulo", 1 });

            migrationBuilder.InsertData(
                table: "Neighbourhood",
                columns: new[] { "NeighbourhoodId", "CityId", "Name" },
                values: new object[] { 1, 1, "Bela Vista" });

            migrationBuilder.InsertData(
                table: "Street",
                columns: new[] { "StreetId", "Name", "NeighbourhoodId" },
                values: new object[] { 1, "Av. Paulista", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Street",
                keyColumn: "StreetId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Neighbourhood",
                keyColumn: "NeighbourhoodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "State",
                keyColumn: "StateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "CountryId",
                keyValue: 1);
        }
    }
}
