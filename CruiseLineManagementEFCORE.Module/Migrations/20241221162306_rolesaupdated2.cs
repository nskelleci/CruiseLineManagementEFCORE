using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolesaupdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrewCrewRole");

            migrationBuilder.CreateTable(
                name: "CrewVesselRole",
                columns: table => new
                {
                    CrewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewVesselRole", x => new { x.CrewsID, x.VesselRolesID });
                    table.ForeignKey(
                        name: "FK_CrewVesselRole_PermissionPolicyRoleBase_VesselRolesID",
                        column: x => x.VesselRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewVesselRole_PermissionPolicyUser_CrewsID",
                        column: x => x.CrewsID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewVesselRole_VesselRolesID",
                table: "CrewVesselRole",
                column: "VesselRolesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrewVesselRole");

            migrationBuilder.CreateTable(
                name: "CrewCrewRole",
                columns: table => new
                {
                    CrewRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewCrewRole", x => new { x.CrewRolesID, x.CrewsID });
                    table.ForeignKey(
                        name: "FK_CrewCrewRole_PermissionPolicyRoleBase_CrewRolesID",
                        column: x => x.CrewRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewCrewRole_PermissionPolicyUser_CrewsID",
                        column: x => x.CrewsID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewCrewRole_CrewsID",
                table: "CrewCrewRole",
                column: "CrewsID");
        }
    }
}
