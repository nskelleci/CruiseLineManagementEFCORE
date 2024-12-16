using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class safetyobjectsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SurvivalCraftsTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SurvivalCraftsTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "SurvivalCraftsTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "SurvivalCrafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SurvivalCrafts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MusterStationID",
                table: "SurvivalCrafts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SurvivalCrafts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SurvivalCraftTypeID",
                table: "SurvivalCrafts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "SurvivalCrafts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VesselLocationID",
                table: "SurvivalCrafts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MusterStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MusterStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "MusterStations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VesselLocationID",
                table: "MusterStations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MusterStationID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SurvivalCraftID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SurvivalCraftsTypes_VesselID",
                table: "SurvivalCraftsTypes",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_SurvivalCrafts_MusterStationID",
                table: "SurvivalCrafts",
                column: "MusterStationID");

            migrationBuilder.CreateIndex(
                name: "IX_SurvivalCrafts_SurvivalCraftTypeID",
                table: "SurvivalCrafts",
                column: "SurvivalCraftTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SurvivalCrafts_VesselID",
                table: "SurvivalCrafts",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_SurvivalCrafts_VesselLocationID",
                table: "SurvivalCrafts",
                column: "VesselLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_MusterStations_VesselID",
                table: "MusterStations",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_MusterStations_VesselLocationID",
                table: "MusterStations",
                column: "VesselLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_MusterStationID",
                table: "Cabins",
                column: "MusterStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_SurvivalCraftID",
                table: "Cabins",
                column: "SurvivalCraftID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_MusterStations_MusterStationID",
                table: "Cabins",
                column: "MusterStationID",
                principalTable: "MusterStations",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_SurvivalCrafts_SurvivalCraftID",
                table: "Cabins",
                column: "SurvivalCraftID",
                principalTable: "SurvivalCrafts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MusterStations_VesselLocations_VesselLocationID",
                table: "MusterStations",
                column: "VesselLocationID",
                principalTable: "VesselLocations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusterStations_Vessels_VesselID",
                table: "MusterStations",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SurvivalCrafts_MusterStations_MusterStationID",
                table: "SurvivalCrafts",
                column: "MusterStationID",
                principalTable: "MusterStations",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SurvivalCrafts_SurvivalCraftsTypes_SurvivalCraftTypeID",
                table: "SurvivalCrafts",
                column: "SurvivalCraftTypeID",
                principalTable: "SurvivalCraftsTypes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SurvivalCrafts_VesselLocations_VesselLocationID",
                table: "SurvivalCrafts",
                column: "VesselLocationID",
                principalTable: "VesselLocations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurvivalCrafts_Vessels_VesselID",
                table: "SurvivalCrafts",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SurvivalCraftsTypes_Vessels_VesselID",
                table: "SurvivalCraftsTypes",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_MusterStations_MusterStationID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_SurvivalCrafts_SurvivalCraftID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_MusterStations_VesselLocations_VesselLocationID",
                table: "MusterStations");

            migrationBuilder.DropForeignKey(
                name: "FK_MusterStations_Vessels_VesselID",
                table: "MusterStations");

            migrationBuilder.DropForeignKey(
                name: "FK_SurvivalCrafts_MusterStations_MusterStationID",
                table: "SurvivalCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_SurvivalCrafts_SurvivalCraftsTypes_SurvivalCraftTypeID",
                table: "SurvivalCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_SurvivalCrafts_VesselLocations_VesselLocationID",
                table: "SurvivalCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_SurvivalCrafts_Vessels_VesselID",
                table: "SurvivalCrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_SurvivalCraftsTypes_Vessels_VesselID",
                table: "SurvivalCraftsTypes");

            migrationBuilder.DropIndex(
                name: "IX_SurvivalCraftsTypes_VesselID",
                table: "SurvivalCraftsTypes");

            migrationBuilder.DropIndex(
                name: "IX_SurvivalCrafts_MusterStationID",
                table: "SurvivalCrafts");

            migrationBuilder.DropIndex(
                name: "IX_SurvivalCrafts_SurvivalCraftTypeID",
                table: "SurvivalCrafts");

            migrationBuilder.DropIndex(
                name: "IX_SurvivalCrafts_VesselID",
                table: "SurvivalCrafts");

            migrationBuilder.DropIndex(
                name: "IX_SurvivalCrafts_VesselLocationID",
                table: "SurvivalCrafts");

            migrationBuilder.DropIndex(
                name: "IX_MusterStations_VesselID",
                table: "MusterStations");

            migrationBuilder.DropIndex(
                name: "IX_MusterStations_VesselLocationID",
                table: "MusterStations");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_MusterStationID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_SurvivalCraftID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SurvivalCraftsTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SurvivalCraftsTypes");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "SurvivalCraftsTypes");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "MusterStationID",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "SurvivalCraftTypeID",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "VesselLocationID",
                table: "SurvivalCrafts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MusterStations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MusterStations");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "MusterStations");

            migrationBuilder.DropColumn(
                name: "VesselLocationID",
                table: "MusterStations");

            migrationBuilder.DropColumn(
                name: "MusterStationID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "SurvivalCraftID",
                table: "Cabins");
        }
    }
}
