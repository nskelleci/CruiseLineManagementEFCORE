using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class vesselspecificrolesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "PermissionPolicyRoleBase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyRoleBase_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID");

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

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyRoleBase_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "PermissionPolicyRoleBase");
        }
    }
}
