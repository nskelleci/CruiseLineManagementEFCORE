using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class crewandrolesadded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_Vessels_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropTable(
                name: "CrewCrewRole");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyRoleBase_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropColumn(
                name: "CrewMemberId",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.RenameColumn(
                name: "VesselID",
                table: "PermissionPolicyUser",
                newName: "CrewID");

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrewMemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Crews_Vessels_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyUser_CrewID",
                table: "PermissionPolicyUser",
                column: "CrewID",
                unique: true,
                filter: "[CrewID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_VesselID",
                table: "Crews",
                column: "VesselID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyUser_Crews_CrewID",
                table: "PermissionPolicyUser",
                column: "CrewID",
                principalTable: "Crews",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_Crews_CrewID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_CrewID",
                table: "PermissionPolicyUser");

            migrationBuilder.RenameColumn(
                name: "CrewID",
                table: "PermissionPolicyUser",
                newName: "VesselID");

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

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "PermissionPolicyRoleBase",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyRoleBase_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_CrewCrewRole_CrewUsersID",
                table: "CrewCrewRole",
                column: "CrewUsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyUser_Vessels_VesselID",
                table: "PermissionPolicyUser",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");
        }
    }
}
