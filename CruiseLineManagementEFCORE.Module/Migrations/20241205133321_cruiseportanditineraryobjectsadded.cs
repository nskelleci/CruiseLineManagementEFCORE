using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class cruiseportanditineraryobjectsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CruisePassengers_Cruises_CruiseID",
                table: "CruisePassengers");

            migrationBuilder.CreateTable(
                name: "CruisePortCountry",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CruisePortCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CruisePortCountryFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CruisePortCountryLocalCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CruisePortCountryLocalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruisePortCountry", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CruisePortCity",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CruisePortCountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruisePortCity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CruisePortCity_CruisePortCountry_CruisePortCountryID",
                        column: x => x.CruisePortCountryID,
                        principalTable: "CruisePortCountry",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CruisePort",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CruisePortCityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruisePort", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CruisePort_CruisePortCity_CruisePortCityID",
                        column: x => x.CruisePortCityID,
                        principalTable: "CruisePortCity",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ItineraryDay",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    CruiseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CruisePortID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItineraryDay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItineraryDay_CruisePort_CruisePortID",
                        column: x => x.CruisePortID,
                        principalTable: "CruisePort",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ItineraryDay_Cruises_CruiseID",
                        column: x => x.CruiseID,
                        principalTable: "Cruises",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CruisePort_CruisePortCityID",
                table: "CruisePort",
                column: "CruisePortCityID");

            migrationBuilder.CreateIndex(
                name: "IX_CruisePortCity_CruisePortCountryID",
                table: "CruisePortCity",
                column: "CruisePortCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ItineraryDay_CruiseID",
                table: "ItineraryDay",
                column: "CruiseID");

            migrationBuilder.CreateIndex(
                name: "IX_ItineraryDay_CruisePortID",
                table: "ItineraryDay",
                column: "CruisePortID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePassengers_Cruises_CruiseID",
                table: "CruisePassengers",
                column: "CruiseID",
                principalTable: "Cruises",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CruisePassengers_Cruises_CruiseID",
                table: "CruisePassengers");

            migrationBuilder.DropTable(
                name: "ItineraryDay");

            migrationBuilder.DropTable(
                name: "CruisePort");

            migrationBuilder.DropTable(
                name: "CruisePortCity");

            migrationBuilder.DropTable(
                name: "CruisePortCountry");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePassengers_Cruises_CruiseID",
                table: "CruisePassengers",
                column: "CruiseID",
                principalTable: "Cruises",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
