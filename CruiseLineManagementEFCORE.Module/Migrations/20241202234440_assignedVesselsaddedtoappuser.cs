using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class assignedVesselsaddedtoappuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cruise_SeasonVessel_SeasonVesselID",
                table: "Cruise");

            migrationBuilder.DropForeignKey(
                name: "FK_Cruise_Seasons_SeasonID",
                table: "Cruise");

            migrationBuilder.DropForeignKey(
                name: "FK_Cruise_Vessels_VesselID",
                table: "Cruise");

            migrationBuilder.DropForeignKey(
                name: "FK_CruisePassenger_Cruise_CruiseID",
                table: "CruisePassenger");

            migrationBuilder.DropForeignKey(
                name: "FK_CruisePassenger_Passenger_PassengerID",
                table: "CruisePassenger");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerFolio_CruisePassenger_CruisePassengerID",
                table: "PassengerFolio");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonVessel_Seasons_SeasonID",
                table: "SeasonVessel");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonVessel_Vessels_VesselID",
                table: "SeasonVessel");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_PassengerFolio_PassengerFolioID",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonVessel",
                table: "SeasonVessel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerFolio",
                table: "PassengerFolio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passenger",
                table: "Passenger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePassenger",
                table: "CruisePassenger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cruise",
                table: "Cruise");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "SeasonVessel",
                newName: "SeasonVessels");

            migrationBuilder.RenameTable(
                name: "PassengerFolio",
                newName: "PassengerFolios");

            migrationBuilder.RenameTable(
                name: "Passenger",
                newName: "Passengers");

            migrationBuilder.RenameTable(
                name: "CruisePassenger",
                newName: "CruisePassengers");

            migrationBuilder.RenameTable(
                name: "Cruise",
                newName: "Cruises");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_PassengerFolioID",
                table: "Transactions",
                newName: "IX_Transactions_PassengerFolioID");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonVessel_VesselID",
                table: "SeasonVessels",
                newName: "IX_SeasonVessels_VesselID");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonVessel_SeasonID",
                table: "SeasonVessels",
                newName: "IX_SeasonVessels_SeasonID");

            migrationBuilder.RenameIndex(
                name: "IX_PassengerFolio_CruisePassengerID",
                table: "PassengerFolios",
                newName: "IX_PassengerFolios_CruisePassengerID");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePassenger_PassengerID",
                table: "CruisePassengers",
                newName: "IX_CruisePassengers_PassengerID");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePassenger_CruiseID",
                table: "CruisePassengers",
                newName: "IX_CruisePassengers_CruiseID");

            migrationBuilder.RenameIndex(
                name: "IX_Cruise_VesselID",
                table: "Cruises",
                newName: "IX_Cruises_VesselID");

            migrationBuilder.RenameIndex(
                name: "IX_Cruise_SeasonVesselID",
                table: "Cruises",
                newName: "IX_Cruises_SeasonVesselID");

            migrationBuilder.RenameIndex(
                name: "IX_Cruise_SeasonID",
                table: "Cruises",
                newName: "IX_Cruises_SeasonID");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserID",
                table: "Vessels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonVessels",
                table: "SeasonVessels",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerFolios",
                table: "PassengerFolios",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePassengers",
                table: "CruisePassengers",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cruises",
                table: "Cruises",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ApplicationUserID",
                table: "Vessels",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePassengers_Cruises_CruiseID",
                table: "CruisePassengers",
                column: "CruiseID",
                principalTable: "Cruises",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePassengers_Passengers_PassengerID",
                table: "CruisePassengers",
                column: "PassengerID",
                principalTable: "Passengers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cruises_SeasonVessels_SeasonVesselID",
                table: "Cruises",
                column: "SeasonVesselID",
                principalTable: "SeasonVessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cruises_Seasons_SeasonID",
                table: "Cruises",
                column: "SeasonID",
                principalTable: "Seasons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cruises_Vessels_VesselID",
                table: "Cruises",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerFolios_CruisePassengers_CruisePassengerID",
                table: "PassengerFolios",
                column: "CruisePassengerID",
                principalTable: "CruisePassengers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonVessels_Seasons_SeasonID",
                table: "SeasonVessels",
                column: "SeasonID",
                principalTable: "Seasons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonVessels_Vessels_VesselID",
                table: "SeasonVessels",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PassengerFolios_PassengerFolioID",
                table: "Transactions",
                column: "PassengerFolioID",
                principalTable: "PassengerFolios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vessels_PermissionPolicyUser_ApplicationUserID",
                table: "Vessels",
                column: "ApplicationUserID",
                principalTable: "PermissionPolicyUser",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CruisePassengers_Cruises_CruiseID",
                table: "CruisePassengers");

            migrationBuilder.DropForeignKey(
                name: "FK_CruisePassengers_Passengers_PassengerID",
                table: "CruisePassengers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cruises_SeasonVessels_SeasonVesselID",
                table: "Cruises");

            migrationBuilder.DropForeignKey(
                name: "FK_Cruises_Seasons_SeasonID",
                table: "Cruises");

            migrationBuilder.DropForeignKey(
                name: "FK_Cruises_Vessels_VesselID",
                table: "Cruises");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerFolios_CruisePassengers_CruisePassengerID",
                table: "PassengerFolios");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonVessels_Seasons_SeasonID",
                table: "SeasonVessels");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonVessels_Vessels_VesselID",
                table: "SeasonVessels");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PassengerFolios_PassengerFolioID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vessels_PermissionPolicyUser_ApplicationUserID",
                table: "Vessels");

            migrationBuilder.DropIndex(
                name: "IX_Vessels_ApplicationUserID",
                table: "Vessels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonVessels",
                table: "SeasonVessels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerFolios",
                table: "PassengerFolios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cruises",
                table: "Cruises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePassengers",
                table: "CruisePassengers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Vessels");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "SeasonVessels",
                newName: "SeasonVessel");

            migrationBuilder.RenameTable(
                name: "Passengers",
                newName: "Passenger");

            migrationBuilder.RenameTable(
                name: "PassengerFolios",
                newName: "PassengerFolio");

            migrationBuilder.RenameTable(
                name: "Cruises",
                newName: "Cruise");

            migrationBuilder.RenameTable(
                name: "CruisePassengers",
                newName: "CruisePassenger");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PassengerFolioID",
                table: "Transaction",
                newName: "IX_Transaction_PassengerFolioID");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonVessels_VesselID",
                table: "SeasonVessel",
                newName: "IX_SeasonVessel_VesselID");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonVessels_SeasonID",
                table: "SeasonVessel",
                newName: "IX_SeasonVessel_SeasonID");

            migrationBuilder.RenameIndex(
                name: "IX_PassengerFolios_CruisePassengerID",
                table: "PassengerFolio",
                newName: "IX_PassengerFolio_CruisePassengerID");

            migrationBuilder.RenameIndex(
                name: "IX_Cruises_VesselID",
                table: "Cruise",
                newName: "IX_Cruise_VesselID");

            migrationBuilder.RenameIndex(
                name: "IX_Cruises_SeasonVesselID",
                table: "Cruise",
                newName: "IX_Cruise_SeasonVesselID");

            migrationBuilder.RenameIndex(
                name: "IX_Cruises_SeasonID",
                table: "Cruise",
                newName: "IX_Cruise_SeasonID");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePassengers_PassengerID",
                table: "CruisePassenger",
                newName: "IX_CruisePassenger_PassengerID");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePassengers_CruiseID",
                table: "CruisePassenger",
                newName: "IX_CruisePassenger_CruiseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonVessel",
                table: "SeasonVessel",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passenger",
                table: "Passenger",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerFolio",
                table: "PassengerFolio",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cruise",
                table: "Cruise",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePassenger",
                table: "CruisePassenger",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cruise_SeasonVessel_SeasonVesselID",
                table: "Cruise",
                column: "SeasonVesselID",
                principalTable: "SeasonVessel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cruise_Seasons_SeasonID",
                table: "Cruise",
                column: "SeasonID",
                principalTable: "Seasons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cruise_Vessels_VesselID",
                table: "Cruise",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePassenger_Cruise_CruiseID",
                table: "CruisePassenger",
                column: "CruiseID",
                principalTable: "Cruise",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePassenger_Passenger_PassengerID",
                table: "CruisePassenger",
                column: "PassengerID",
                principalTable: "Passenger",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerFolio_CruisePassenger_CruisePassengerID",
                table: "PassengerFolio",
                column: "CruisePassengerID",
                principalTable: "CruisePassenger",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonVessel_Seasons_SeasonID",
                table: "SeasonVessel",
                column: "SeasonID",
                principalTable: "Seasons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonVessel_Vessels_VesselID",
                table: "SeasonVessel",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_PassengerFolio_PassengerFolioID",
                table: "Transaction",
                column: "PassengerFolioID",
                principalTable: "PassengerFolio",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
