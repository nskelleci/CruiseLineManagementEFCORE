using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class assigneduserssaddedtovessel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vessels_PermissionPolicyUser_ApplicationUserID",
                table: "Vessels");

            migrationBuilder.DropIndex(
                name: "IX_Vessels_ApplicationUserID",
                table: "Vessels");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Vessels");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserVessel");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserID",
                table: "Vessels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ApplicationUserID",
                table: "Vessels",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vessels_PermissionPolicyUser_ApplicationUserID",
                table: "Vessels",
                column: "ApplicationUserID",
                principalTable: "PermissionPolicyUser",
                principalColumn: "ID");
        }
    }
}
