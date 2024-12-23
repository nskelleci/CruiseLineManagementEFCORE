using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class crewandrolesadded3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_Crews_CrewID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_CrewID",
                table: "PermissionPolicyUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Crews_PermissionPolicyUser_ID",
                table: "Crews",
                column: "ID",
                principalTable: "PermissionPolicyUser",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crews_PermissionPolicyUser_ID",
                table: "Crews");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyUser_CrewID",
                table: "PermissionPolicyUser",
                column: "CrewID",
                unique: true,
                filter: "[CrewID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyUser_Crews_CrewID",
                table: "PermissionPolicyUser",
                column: "CrewID",
                principalTable: "Crews",
                principalColumn: "ID");
        }
    }
}
