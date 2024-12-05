using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class newobjectsandrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vessels_Seasons_ActiveSeasonId",
                table: "Vessels");

            migrationBuilder.DropIndex(
                name: "IX_Vessels_ActiveSeasonId",
                table: "Vessels");

            migrationBuilder.DropColumn(
                name: "ActiveSeasonId",
                table: "Vessels");

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SeasonVessel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSeasonActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonVessel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SeasonVessel_Seasons_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Seasons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonVessel_Vessels_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cruise",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeasonVesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cruise", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cruise_SeasonVessel_SeasonVesselID",
                        column: x => x.SeasonVesselID,
                        principalTable: "SeasonVessel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cruise_Seasons_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Seasons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Cruise_Vessels_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CruisePassenger",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CruiseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruisePassenger", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CruisePassenger_Cruise_CruiseID",
                        column: x => x.CruiseID,
                        principalTable: "Cruise",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CruisePassenger_Passenger_PassengerID",
                        column: x => x.PassengerID,
                        principalTable: "Passenger",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerFolio",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CruisePassengerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerFolio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PassengerFolio_CruisePassenger_CruisePassengerID",
                        column: x => x.CruisePassengerID,
                        principalTable: "CruisePassenger",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerFolioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_PassengerFolio_PassengerFolioID",
                        column: x => x.PassengerFolioID,
                        principalTable: "PassengerFolio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cruise_SeasonID",
                table: "Cruise",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_Cruise_SeasonVesselID",
                table: "Cruise",
                column: "SeasonVesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Cruise_VesselID",
                table: "Cruise",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_CruisePassenger_CruiseID",
                table: "CruisePassenger",
                column: "CruiseID");

            migrationBuilder.CreateIndex(
                name: "IX_CruisePassenger_PassengerID",
                table: "CruisePassenger",
                column: "PassengerID");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerFolio_CruisePassengerID",
                table: "PassengerFolio",
                column: "CruisePassengerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeasonVessel_SeasonID",
                table: "SeasonVessel",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonVessel_VesselID",
                table: "SeasonVessel",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PassengerFolioID",
                table: "Transaction",
                column: "PassengerFolioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "PassengerFolio");

            migrationBuilder.DropTable(
                name: "CruisePassenger");

            migrationBuilder.DropTable(
                name: "Cruise");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "SeasonVessel");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveSeasonId",
                table: "Vessels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ActiveSeasonId",
                table: "Vessels",
                column: "ActiveSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vessels_Seasons_ActiveSeasonId",
                table: "Vessels",
                column: "ActiveSeasonId",
                principalTable: "Seasons",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
