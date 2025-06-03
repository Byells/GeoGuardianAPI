using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeoGuardian.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertTypes",
                columns: table => new
                {
                    AlertTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertTypes", x => x.AlertTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "RiskAreaTypes",
                columns: table => new
                {
                    RiskAreaTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAreaTypes", x => x.RiskAreaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SensorModels",
                columns: table => new
                {
                    SensorModelId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Maker = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorModels", x => x.SensorModelId);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CountryId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
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
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    StateId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
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
                        name: "FK_Neighbourhood_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
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
                    CountryId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StateId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CityId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Neighborhood = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    StreetName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Number = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Complement = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Latitude = table.Column<decimal>(type: "NUMBER(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "NUMBER(9,6)", nullable: true),
                    CountryId1 = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    StateId1 = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    CityId1 = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    StreetId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId1",
                        column: x => x.CityId1,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId1",
                        column: x => x.CountryId1,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Addresses_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_States_StateId1",
                        column: x => x.StateId1,
                        principalTable: "States",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK_Addresses_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "StreetId");
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
                    CityId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StreetId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskAreas_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskAreas_RiskAreaTypes_RiskAreaTypeId",
                        column: x => x.RiskAreaTypeId,
                        principalTable: "RiskAreaTypes",
                        principalColumn: "RiskAreaTypeId");
                    table.ForeignKey(
                        name: "FK_RiskAreas_Street_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Street",
                        principalColumn: "StreetId");
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
                        name: "FK_Alerts_AlertTypes_AlertTypeId",
                        column: x => x.AlertTypeId,
                        principalTable: "AlertTypes",
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
                    Uuid = table.Column<string>(type: "NVARCHAR2(36)", maxLength: 36, nullable: false),
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
                        name: "FK_Sensors_SensorModels_SensorModelId",
                        column: x => x.SensorModelId,
                        principalTable: "SensorModels",
                        principalColumn: "SensorModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAlerts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AlertId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SendMode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ConfirmedReception = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAlerts", x => new { x.UserId, x.AlertId });
                    table.ForeignKey(
                        name: "FK_UserAlerts_Alerts_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAlerts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorReadings",
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
                    table.PrimaryKey("PK_SensorReadings", x => x.SensorReadingId);
                    table.ForeignKey(
                        name: "FK_SensorReadings_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AlertTypes",
                columns: new[] { "AlertTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "INFO" },
                    { 2, "WARNING" },
                    { 3, "CRITICAL" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Brasil" });

            migrationBuilder.InsertData(
                table: "RiskAreaTypes",
                columns: new[] { "RiskAreaTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "FLOOD" },
                    { 2, "LANDSLIDE" },
                    { 3, "DAM_BREAK" }
                });

            migrationBuilder.InsertData(
                table: "SensorModels",
                columns: new[] { "SensorModelId", "Maker", "Name" },
                values: new object[,]
                {
                    { 1, "Acme", "ULTRASONIC_WL-X100" },
                    { 2, "Acme", "SOILMOIST-S200" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "UserTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "ADMIN" },
                    { 2, "USER" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Acre" },
                    { 2, 1, "Alagoas" },
                    { 3, 1, "Amapá" },
                    { 4, 1, "Amazonas" },
                    { 5, 1, "Bahia" },
                    { 6, 1, "Ceará" },
                    { 7, 1, "Distrito Federal" },
                    { 8, 1, "Espírito Santo" },
                    { 9, 1, "Goiás" },
                    { 10, 1, "Maranhão" },
                    { 11, 1, "Mato Grosso" },
                    { 12, 1, "Mato Grosso do Sul" },
                    { 13, 1, "Minas Gerais" },
                    { 14, 1, "Pará" },
                    { 15, 1, "Paraíba" },
                    { 16, 1, "Paraná" },
                    { 17, 1, "Pernambuco" },
                    { 18, 1, "Piauí" },
                    { 19, 1, "Rio de Janeiro" },
                    { 20, 1, "Rio Grande do Norte" },
                    { 21, 1, "Rio Grande do Sul" },
                    { 22, 1, "Rondônia" },
                    { 23, 1, "Roraima" },
                    { 24, 1, "Santa Catarina" },
                    { 25, 1, "São Paulo" },
                    { 26, 1, "Sergipe" },
                    { 27, 1, "Tocantins" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Created", "Email", "FullName", "Password", "UserTypeId" },
                values: new object[] { 1, new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@geo.com", "Admin", "Admingeo", 1 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "StateId" },
                values: new object[,]
                {
                    { 1, "Rio Branco", 1 },
                    { 2, "Cruzeiro do Sul", 1 },
                    { 3, "Sena Madureira", 1 },
                    { 4, "Maceió", 2 },
                    { 5, "Arapiraca", 2 },
                    { 6, "Palmeira dos Índios", 2 },
                    { 7, "Macapá", 3 },
                    { 8, "Santana", 3 },
                    { 9, "Laranjal do Jari", 3 },
                    { 10, "Manaus", 4 },
                    { 11, "Parintins", 4 },
                    { 12, "Itacoatiara", 4 },
                    { 13, "Salvador", 5 },
                    { 14, "Feira de Santana", 5 },
                    { 15, "Vitória da Conquista", 5 },
                    { 16, "Fortaleza", 6 },
                    { 17, "Juazeiro do Norte", 6 },
                    { 18, "Sobral", 6 },
                    { 19, "Brasília", 7 },
                    { 20, "Taguatinga", 7 },
                    { 21, "Ceilândia", 7 },
                    { 22, "Vitória", 8 },
                    { 23, "Vila Velha", 8 },
                    { 24, "Serra", 8 },
                    { 25, "Goiânia", 9 },
                    { 26, "Anápolis", 9 },
                    { 27, "Aparecida de Goiânia", 9 },
                    { 28, "São Luís", 10 },
                    { 29, "Imperatriz", 10 },
                    { 30, "Caxias", 10 },
                    { 31, "Cuiabá", 11 },
                    { 32, "Várzea Grande", 11 },
                    { 33, "Rondonópolis", 11 },
                    { 34, "Campo Grande", 12 },
                    { 35, "Dourados", 12 },
                    { 36, "Três Lagoas", 12 },
                    { 37, "Belo Horizonte", 13 },
                    { 38, "Uberlândia", 13 },
                    { 39, "Contagem", 13 },
                    { 40, "Belém", 14 },
                    { 41, "Santarém", 14 },
                    { 42, "Marabá", 14 },
                    { 43, "João Pessoa", 15 },
                    { 44, "Campina Grande", 15 },
                    { 45, "Patos", 15 },
                    { 46, "Curitiba", 16 },
                    { 47, "Londrina", 16 },
                    { 48, "Maringá", 16 },
                    { 49, "Recife", 17 },
                    { 50, "Olinda", 17 },
                    { 51, "Petrolina", 17 },
                    { 52, "Teresina", 18 },
                    { 53, "Parnaíba", 18 },
                    { 54, "Picos", 18 },
                    { 55, "Rio de Janeiro", 19 },
                    { 56, "Niterói", 19 },
                    { 57, "Nova Iguaçu", 19 },
                    { 58, "Natal", 20 },
                    { 59, "Mossoró", 20 },
                    { 60, "Parnamirim", 20 },
                    { 61, "Porto Alegre", 21 },
                    { 62, "Caxias do Sul", 21 },
                    { 63, "Pelotas", 21 },
                    { 64, "Porto Velho", 22 },
                    { 65, "Ji-Paraná", 22 },
                    { 66, "Ariquemes", 22 },
                    { 67, "Boa Vista", 23 },
                    { 68, "Rorainópolis", 23 },
                    { 69, "Caracaraí", 23 },
                    { 70, "Florianópolis", 24 },
                    { 71, "Joinville", 24 },
                    { 72, "Blumenau", 24 },
                    { 73, "São Paulo", 25 },
                    { 74, "Campinas", 25 },
                    { 75, "Santos", 25 },
                    { 76, "Aracaju", 26 },
                    { 77, "Nossa Senhora do Socorro", 26 },
                    { 78, "Itabaiana", 26 },
                    { 79, "Palmas", 27 },
                    { 80, "Araguaína", 27 },
                    { 81, "Gurupi", 27 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId1",
                table: "Addresses",
                column: "CityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId1",
                table: "Addresses",
                column: "CountryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateId",
                table: "Addresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateId1",
                table: "Addresses",
                column: "StateId1");

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
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Neighbourhood_CityId",
                table: "Neighbourhood",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAreas_CityId",
                table: "RiskAreas",
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
                name: "IX_SensorReadings_SensorId",
                table: "SensorReadings",
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
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Street_NeighbourhoodId",
                table: "Street",
                column: "NeighbourhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAlerts_AlertId",
                table: "UserAlerts",
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
                name: "SensorReadings");

            migrationBuilder.DropTable(
                name: "UserAlerts");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SensorModels");

            migrationBuilder.DropTable(
                name: "AlertTypes");

            migrationBuilder.DropTable(
                name: "RiskAreas");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "RiskAreaTypes");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.DropTable(
                name: "Neighbourhood");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
