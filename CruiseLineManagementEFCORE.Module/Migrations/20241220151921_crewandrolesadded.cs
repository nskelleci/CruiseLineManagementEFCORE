using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class crewandrolesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CruisePort_CruisePortCity_CruisePortCityID",
                table: "CruisePort");

            migrationBuilder.DropForeignKey(
                name: "FK_CruisePortCity_CruisePortCountry_CruisePortCountryID",
                table: "CruisePortCity");

            migrationBuilder.DropForeignKey(
                name: "FK_ItineraryDay_CruisePort_CruisePortID",
                table: "ItineraryDay");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropTable(
                name: "OnBoardUserVesselSpecificRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePortCountry",
                table: "CruisePortCountry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePortCity",
                table: "CruisePortCity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePort",
                table: "CruisePort");

            migrationBuilder.RenameTable(
                name: "CruisePortCountry",
                newName: "CruisePortCountries");

            migrationBuilder.RenameTable(
                name: "CruisePortCity",
                newName: "CruisePortCities");

            migrationBuilder.RenameTable(
                name: "CruisePort",
                newName: "CruisePorts");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePortCity_CruisePortCountryID",
                table: "CruisePortCities",
                newName: "IX_CruisePortCities_CruisePortCountryID");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePort_CruisePortCityID",
                table: "CruisePorts",
                newName: "IX_CruisePorts_CruisePortCityID");

            migrationBuilder.AddColumn<string>(
                name: "CrewMemberId",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePortCountries",
                table: "CruisePortCountries",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePortCities",
                table: "CruisePortCities",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePorts",
                table: "CruisePorts",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "CrewCrewRole",
                columns: table => new
                {
                    CrewRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewCrewRole", x => new { x.CrewRolesID, x.CrewUsersID });
                    table.ForeignKey(
                        name: "FK_CrewCrewRole_PermissionPolicyRoleBase_CrewRolesID",
                        column: x => x.CrewRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewCrewRole_PermissionPolicyUser_CrewUsersID",
                        column: x => x.CrewUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewCrewRole_CrewUsersID",
                table: "CrewCrewRole",
                column: "CrewUsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePortCities_CruisePortCountries_CruisePortCountryID",
                table: "CruisePortCities",
                column: "CruisePortCountryID",
                principalTable: "CruisePortCountries",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePorts_CruisePortCities_CruisePortCityID",
                table: "CruisePorts",
                column: "CruisePortCityID",
                principalTable: "CruisePortCities",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItineraryDay_CruisePorts_CruisePortID",
                table: "ItineraryDay",
                column: "CruisePortID",
                principalTable: "CruisePorts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CruisePortCities_CruisePortCountries_CruisePortCountryID",
                table: "CruisePortCities");

            migrationBuilder.DropForeignKey(
                name: "FK_CruisePorts_CruisePortCities_CruisePortCityID",
                table: "CruisePorts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItineraryDay_CruisePorts_CruisePortID",
                table: "ItineraryDay");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropTable(
                name: "CrewCrewRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePorts",
                table: "CruisePorts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePortCountries",
                table: "CruisePortCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CruisePortCities",
                table: "CruisePortCities");

            migrationBuilder.DropColumn(
                name: "CrewMemberId",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PermissionPolicyUser");

            migrationBuilder.RenameTable(
                name: "CruisePorts",
                newName: "CruisePort");

            migrationBuilder.RenameTable(
                name: "CruisePortCountries",
                newName: "CruisePortCountry");

            migrationBuilder.RenameTable(
                name: "CruisePortCities",
                newName: "CruisePortCity");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePorts_CruisePortCityID",
                table: "CruisePort",
                newName: "IX_CruisePort_CruisePortCityID");

            migrationBuilder.RenameIndex(
                name: "IX_CruisePortCities_CruisePortCountryID",
                table: "CruisePortCity",
                newName: "IX_CruisePortCity_CruisePortCountryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePort",
                table: "CruisePort",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePortCountry",
                table: "CruisePortCountry",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CruisePortCity",
                table: "CruisePortCity",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "OnBoardUserVesselSpecificRole",
                columns: table => new
                {
                    OnBoardUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselSpecificRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnBoardUserVesselSpecificRole", x => new { x.OnBoardUsersID, x.VesselSpecificRolesID });
                    table.ForeignKey(
                        name: "FK_OnBoardUserVesselSpecificRole_PermissionPolicyRoleBase_VesselSpecificRolesID",
                        column: x => x.VesselSpecificRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnBoardUserVesselSpecificRole_PermissionPolicyUser_OnBoardUsersID",
                        column: x => x.OnBoardUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnBoardUserVesselSpecificRole_VesselSpecificRolesID",
                table: "OnBoardUserVesselSpecificRole",
                column: "VesselSpecificRolesID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePort_CruisePortCity_CruisePortCityID",
                table: "CruisePort",
                column: "CruisePortCityID",
                principalTable: "CruisePortCity",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CruisePortCity_CruisePortCountry_CruisePortCountryID",
                table: "CruisePortCity",
                column: "CruisePortCountryID",
                principalTable: "CruisePortCountry",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItineraryDay_CruisePort_CruisePortID",
                table: "ItineraryDay",
                column: "CruisePortID",
                principalTable: "CruisePort",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
