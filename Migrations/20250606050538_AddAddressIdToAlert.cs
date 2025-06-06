using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressIdToAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Alerts",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AddressId",
                table: "Alerts",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Addresses_AddressId",
                table: "Alerts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Addresses_AddressId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_AddressId",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Alerts");
        }
    }
}
