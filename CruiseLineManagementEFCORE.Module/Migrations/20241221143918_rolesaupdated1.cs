using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolesaupdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrewRoleCrewUser");

            migrationBuilder.DropTable(
                name: "Crews");

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
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_CrewCrewRole_CrewsID",
                table: "CrewCrewRole",
                column: "CrewsID");

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
                name: "CrewCrewRole");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "CrewMemberId",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PermissionPolicyUser");

            migrationBuilder.RenameColumn(
                name: "VesselID",
                table: "PermissionPolicyUser",
                newName: "CrewID");

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
                name: "Crews",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewMemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrewUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Crews_PermissionPolicyUser_ID",
                        column: x => x.ID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Crews_Vessels_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewRoleCrewUser_CrewUsersID",
                table: "CrewRoleCrewUser",
                column: "CrewUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_VesselID",
                table: "Crews",
                column: "VesselID");
        }
    }
}
