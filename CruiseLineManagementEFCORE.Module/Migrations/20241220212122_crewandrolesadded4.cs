using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class crewandrolesadded4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserVessel");

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "PermissionPolicyRoleBase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CrewRoleCrewUser",
                columns: table => new
                {
                    CrewRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewRoleCrewUser", x => new { x.CrewRolesID, x.CrewUsersID });
                    table.ForeignKey(
                        name: "FK_CrewRoleCrewUser_PermissionPolicyRoleBase_CrewRolesID",
                        column: x => x.CrewRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewRoleCrewUser_PermissionPolicyUser_CrewUsersID",
                        column: x => x.CrewUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalRoleGlobalUser",
                columns: table => new
                {
                    GlobalRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlobalUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalRoleGlobalUser", x => new { x.GlobalRolesID, x.GlobalUsersID });
                    table.ForeignKey(
                        name: "FK_GlobalRoleGlobalUser_PermissionPolicyRoleBase_GlobalRolesID",
                        column: x => x.GlobalRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobalRoleGlobalUser_PermissionPolicyUser_GlobalUsersID",
                        column: x => x.GlobalUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalUserVessel",
                columns: table => new
                {
                    AssignedUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedVesselsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalUserVessel", x => new { x.AssignedUsersID, x.AssignedVesselsID });
                    table.ForeignKey(
                        name: "FK_GlobalUserVessel_PermissionPolicyUser_AssignedUsersID",
                        column: x => x.AssignedUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobalUserVessel_Vessels_AssignedVesselsID",
                        column: x => x.AssignedVesselsID,
                        principalTable: "Vessels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyRoleBase_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_CrewRoleCrewUser_CrewUsersID",
                table: "CrewRoleCrewUser",
                column: "CrewUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalRoleGlobalUser_GlobalUsersID",
                table: "GlobalRoleGlobalUser",
                column: "GlobalUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalUserVessel_AssignedVesselsID",
                table: "GlobalUserVessel",
                column: "AssignedVesselsID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropTable(
                name: "CrewRoleCrewUser");

            migrationBuilder.DropTable(
                name: "GlobalRoleGlobalUser");

            migrationBuilder.DropTable(
                name: "GlobalUserVessel");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyRoleBase_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.CreateTable(
                name: "ApplicationUserVessel",
                columns: table => new
                {
                    AssignedUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedVesselsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserVessel", x => new { x.AssignedUsersID, x.AssignedVesselsID });
                    table.ForeignKey(
                        name: "FK_ApplicationUserVessel_PermissionPolicyUser_AssignedUsersID",
                        column: x => x.AssignedUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserVessel_Vessels_AssignedVesselsID",
                        column: x => x.AssignedVesselsID,
                        principalTable: "Vessels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserVessel_AssignedVesselsID",
                table: "ApplicationUserVessel",
                column: "AssignedVesselsID");
        }
    }
}
