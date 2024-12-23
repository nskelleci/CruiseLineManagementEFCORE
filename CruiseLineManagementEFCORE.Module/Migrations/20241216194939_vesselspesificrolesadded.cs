using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class vesselspesificrolesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "PermissionPolicyUser",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_OnBoardUserVesselSpecificRole_VesselSpecificRolesID",
                table: "OnBoardUserVesselSpecificRole",
                column: "VesselSpecificRolesID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyUser_Vessels_VesselID",
                table: "PermissionPolicyUser",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_Vessels_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropTable(
                name: "OnBoardUserVesselSpecificRole");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "PermissionPolicyUser");
        }
    }
}
