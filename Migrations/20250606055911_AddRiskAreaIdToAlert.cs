using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskAreaIdToAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_RiskAreas_RiskAreaId",
                table: "Alerts");

            migrationBuilder.AlterColumn<int>(
                name: "RiskAreaId",
                table: "Alerts",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_RiskAreas_RiskAreaId",
                table: "Alerts",
                column: "RiskAreaId",
                principalTable: "RiskAreas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_RiskAreas_RiskAreaId",
                table: "Alerts");

            migrationBuilder.AlterColumn<int>(
                name: "RiskAreaId",
                table: "Alerts",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_RiskAreas_RiskAreaId",
                table: "Alerts",
                column: "RiskAreaId",
                principalTable: "RiskAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
