using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class vesselobjectsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cruises_Vessels_VesselID",
                table: "Cruises");

            migrationBuilder.DropIndex(
                name: "IX_Cruises_VesselID",
                table: "Cruises");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "Cruises");

            migrationBuilder.AddColumn<Guid>(
                name: "DeckID",
                table: "VesselLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VesselLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VesselLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "VesselLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VesselSideID",
                table: "VesselLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DeckNumber",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Decks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "Decks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CabinTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CabinTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "CabinTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CabinBedTypeID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CabinCategoryID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CabinTypeID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeckID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxOccupancy",
                table: "Cabins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VesselSideID",
                table: "Cabins",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CabinCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CabinCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CabinCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CabinCategories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "CabinCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CabinBedTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CabinBedTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "CabinBedTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VesselSides",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesselSides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VesselSides_Vessels_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VesselLocations_DeckID",
                table: "VesselLocations",
                column: "DeckID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselLocations_VesselID",
                table: "VesselLocations",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselLocations_VesselSideID",
                table: "VesselLocations",
                column: "VesselSideID");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_VesselID",
                table: "Decks",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinTypes_VesselID",
                table: "CabinTypes",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_CabinBedTypeID",
                table: "Cabins",
                column: "CabinBedTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_CabinCategoryID",
                table: "Cabins",
                column: "CabinCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_CabinTypeID",
                table: "Cabins",
                column: "CabinTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_DeckID",
                table: "Cabins",
                column: "DeckID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_VesselID",
                table: "Cabins",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Cabins_VesselSideID",
                table: "Cabins",
                column: "VesselSideID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinCategories_VesselID",
                table: "CabinCategories",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinBedTypes_VesselID",
                table: "CabinBedTypes",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselSides_VesselID",
                table: "VesselSides",
                column: "VesselID");

            migrationBuilder.AddForeignKey(
                name: "FK_CabinBedTypes_Vessels_VesselID",
                table: "CabinBedTypes",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CabinCategories_Vessels_VesselID",
                table: "CabinCategories",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_CabinBedTypes_CabinBedTypeID",
                table: "Cabins",
                column: "CabinBedTypeID",
                principalTable: "CabinBedTypes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_CabinCategories_CabinCategoryID",
                table: "Cabins",
                column: "CabinCategoryID",
                principalTable: "CabinCategories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_CabinTypes_CabinTypeID",
                table: "Cabins",
                column: "CabinTypeID",
                principalTable: "CabinTypes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_Decks_DeckID",
                table: "Cabins",
                column: "DeckID",
                principalTable: "Decks",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_VesselSides_VesselSideID",
                table: "Cabins",
                column: "VesselSideID",
                principalTable: "VesselSides",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabins_Vessels_VesselID",
                table: "Cabins",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CabinTypes_Vessels_VesselID",
                table: "CabinTypes",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Vessels_VesselID",
                table: "Decks",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VesselLocations_Decks_DeckID",
                table: "VesselLocations",
                column: "DeckID",
                principalTable: "Decks",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VesselLocations_VesselSides_VesselSideID",
                table: "VesselLocations",
                column: "VesselSideID",
                principalTable: "VesselSides",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VesselLocations_Vessels_VesselID",
                table: "VesselLocations",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CabinBedTypes_Vessels_VesselID",
                table: "CabinBedTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CabinCategories_Vessels_VesselID",
                table: "CabinCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_CabinBedTypes_CabinBedTypeID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_CabinCategories_CabinCategoryID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_CabinTypes_CabinTypeID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_Decks_DeckID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_VesselSides_VesselSideID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabins_Vessels_VesselID",
                table: "Cabins");

            migrationBuilder.DropForeignKey(
                name: "FK_CabinTypes_Vessels_VesselID",
                table: "CabinTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Vessels_VesselID",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_VesselLocations_Decks_DeckID",
                table: "VesselLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_VesselLocations_VesselSides_VesselSideID",
                table: "VesselLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_VesselLocations_Vessels_VesselID",
                table: "VesselLocations");

            migrationBuilder.DropTable(
                name: "VesselSides");

            migrationBuilder.DropIndex(
                name: "IX_VesselLocations_DeckID",
                table: "VesselLocations");

            migrationBuilder.DropIndex(
                name: "IX_VesselLocations_VesselID",
                table: "VesselLocations");

            migrationBuilder.DropIndex(
                name: "IX_VesselLocations_VesselSideID",
                table: "VesselLocations");

            migrationBuilder.DropIndex(
                name: "IX_Decks_VesselID",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_CabinTypes_VesselID",
                table: "CabinTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_CabinBedTypeID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_CabinCategoryID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_CabinTypeID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_DeckID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_VesselID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_Cabins_VesselSideID",
                table: "Cabins");

            migrationBuilder.DropIndex(
                name: "IX_CabinCategories_VesselID",
                table: "CabinCategories");

            migrationBuilder.DropIndex(
                name: "IX_CabinBedTypes_VesselID",
                table: "CabinBedTypes");

            migrationBuilder.DropColumn(
                name: "DeckID",
                table: "VesselLocations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VesselLocations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "VesselLocations");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "VesselLocations");

            migrationBuilder.DropColumn(
                name: "VesselSideID",
                table: "VesselLocations");

            migrationBuilder.DropColumn(
                name: "DeckNumber",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CabinTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CabinTypes");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "CabinTypes");

            migrationBuilder.DropColumn(
                name: "CabinBedTypeID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "CabinCategoryID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "CabinTypeID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "DeckID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "MaxOccupancy",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "VesselSideID",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CabinCategories");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CabinCategories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CabinCategories");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CabinCategories");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "CabinCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CabinBedTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CabinBedTypes");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "CabinBedTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "Cruises",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cruises_VesselID",
                table: "Cruises",
                column: "VesselID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cruises_Vessels_VesselID",
                table: "Cruises",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");
        }
    }
}
