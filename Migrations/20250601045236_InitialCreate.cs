using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertType",
                columns: table => new
                {
                    AlertTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertType", x => x.AlertTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "RiskAreaType",
                columns: table => new
                {
                    RiskAreaTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAreaType", x => x.RiskAreaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SensorModel",
                columns: table => new
                {
                    SensorModelId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Maker = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorModel", x => x.SensorModelId);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CountryId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FullName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UserTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    StateId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Neighbourhood",
                columns: table => new
                {
                    NeighbourhoodId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CityId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighbourhood", x => x.NeighbourhoodId);
                    table.ForeignKey(
                        name: "FK_Neighbourhood_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    StreetId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NeighbourhoodId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.StreetId);
                    table.ForeignKey(
                        name: "FK_Street_Neighbourhood_NeighbourhoodId",
                        column: x => x.NeighbourhoodId,
                        principalTable: "Neighbourhood",
                        principalColumn: "NeighbourhoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StreetId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Complement = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Latitude = table.Column<decimal>(type: "NUMBER(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "NUMBER(9,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RiskAreaTypeId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    StreetId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskAreas_RiskAreaType_RiskAreaTypeId",
                        column: x => x.RiskAreaTypeId,
                        principalTable: "RiskAreaType",
                        principalColumn: "RiskAreaTypeId");
                    table.ForeignKey(
                        name: "FK_RiskAreas_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RiskLevel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    AlertTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RiskAreaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_AlertType_AlertTypeId",
                        column: x => x.AlertTypeId,
                        principalTable: "AlertType",
                        principalColumn: "AlertTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_RiskAreas_RiskAreaId",
                        column: x => x.RiskAreaId,
                        principalTable: "RiskAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Uuid = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RiskAreaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SensorModelId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_RiskAreas_RiskAreaId",
                        column: x => x.RiskAreaId,
                        principalTable: "RiskAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_SensorModel_SensorModelId",
                        column: x => x.SensorModelId,
                        principalTable: "SensorModel",
                        principalColumn: "SensorModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAlert",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AlertId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SendMode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ConfirmedReception = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAlert", x => new { x.UserId, x.AlertId });
                    table.ForeignKey(
                        name: "FK_UserAlert_Alerts_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAlert_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorReading",
                columns: table => new
                {
                    SensorReadingId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SensorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(18,3)", precision: 18, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorReading", x => x.SensorReadingId);
                    table.ForeignKey(
                        name: "FK_SensorReading_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AlertType",
                columns: new[] { "AlertTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "INFO" },
                    { 2, "WARNING" },
                    { 3, "CRITICAL" }
                });

            migrationBuilder.InsertData(
                table: "RiskAreaType",
                columns: new[] { "RiskAreaTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "FLOOD" },
                    { 2, "LANDSLIDE" },
                    { 3, "DAM_BREAK" }
                });

            migrationBuilder.InsertData(
                table: "SensorModel",
                columns: new[] { "SensorModelId", "Maker", "Name" },
                values: new object[,]
                {
                    { 1, "Acme", "ULTRASONIC_WL-X100" },
                    { 2, "Acme", "SOILMOIST-S200" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AlertTypeId",
                table: "Alerts",
                column: "AlertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_RiskAreaId",
                table: "Alerts",
                column: "RiskAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighbourhood_CityId",
                table: "Neighbourhood",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAreas_RiskAreaTypeId",
                table: "RiskAreas",
                column: "RiskAreaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAreas_StreetId",
                table: "RiskAreas",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorReading_SensorId",
                table: "SensorReading",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_RiskAreaId",
                table: "Sensors",
                column: "RiskAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorModelId",
                table: "Sensors",
                column: "SensorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Street_NeighbourhoodId",
                table: "Street",
                column: "NeighbourhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAlert_AlertId",
                table: "UserAlert",
                column: "AlertId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "SensorReading");

            migrationBuilder.DropTable(
                name: "UserAlert");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SensorModel");

            migrationBuilder.DropTable(
                name: "AlertType");

            migrationBuilder.DropTable(
                name: "RiskAreas");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "RiskAreaType");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.DropTable(
                name: "Neighbourhood");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
