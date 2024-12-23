using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class crewandrolesadded5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

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
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

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
