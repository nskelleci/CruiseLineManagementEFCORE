using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class relationchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vessels_Seasons_ActiveSeasonID",
                table: "Vessels");

            migrationBuilder.DropTable(
                name: "SeasonVessel");

            migrationBuilder.RenameColumn(
                name: "ActiveSeasonID",
                table: "Vessels",
                newName: "ActiveSeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Vessels_ActiveSeasonID",
                table: "Vessels",
                newName: "IX_Vessels_ActiveSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vessels_Seasons_ActiveSeasonId",
                table: "Vessels",
                column: "ActiveSeasonId",
                principalTable: "Seasons",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vessels_Seasons_ActiveSeasonId",
                table: "Vessels");

            migrationBuilder.RenameColumn(
                name: "ActiveSeasonId",
                table: "Vessels",
                newName: "ActiveSeasonID");

            migrationBuilder.RenameIndex(
                name: "IX_Vessels_ActiveSeasonId",
                table: "Vessels",
                newName: "IX_Vessels_ActiveSeasonID");

            migrationBuilder.CreateTable(
                name: "SeasonVessel",
                columns: table => new
                {
                    PastSeasonsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonVessel", x => new { x.PastSeasonsID, x.VesselsID });
                    table.ForeignKey(
                        name: "FK_SeasonVessel_Seasons_PastSeasonsID",
                        column: x => x.PastSeasonsID,
                        principalTable: "Seasons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonVessel_Vessels_VesselsID",
                        column: x => x.VesselsID,
                        principalTable: "Vessels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonVessel_VesselsID",
                table: "SeasonVessel",
                column: "VesselsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vessels_Seasons_ActiveSeasonID",
                table: "Vessels",
                column: "ActiveSeasonID",
                principalTable: "Seasons",
                principalColumn: "ID");
        }
    }
}
