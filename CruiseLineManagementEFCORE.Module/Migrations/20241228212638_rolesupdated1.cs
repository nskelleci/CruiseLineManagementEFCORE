using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolesupdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrewVesselRole",
                columns: table => new
                {
                    CrewMembersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewVesselRole", x => new { x.CrewMembersID, x.VesselRolesID });
                    table.ForeignKey(
                        name: "FK_CrewVesselRole_PermissionPolicyRoleBase_VesselRolesID",
                        column: x => x.VesselRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewVesselRole_PermissionPolicyUser_CrewMembersID",
                        column: x => x.CrewMembersID,
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
        }
    }
}
