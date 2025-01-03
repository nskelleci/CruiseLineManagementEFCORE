using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolestrials1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VesselRoleID",
                table: "PermissionPolicyUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyUser_VesselRoleID",
                table: "PermissionPolicyUser",
                column: "VesselRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyUser_PermissionPolicyRoleBase_VesselRoleID",
                table: "PermissionPolicyUser",
                column: "VesselRoleID",
                principalTable: "PermissionPolicyRoleBase",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_PermissionPolicyRoleBase_VesselRoleID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_VesselRoleID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "VesselRoleID",
                table: "PermissionPolicyUser");
        }
    }
}
