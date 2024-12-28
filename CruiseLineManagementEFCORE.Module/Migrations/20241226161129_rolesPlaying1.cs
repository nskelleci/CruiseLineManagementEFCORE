using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolesPlaying1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.DropTable(
                name: "CrewExtendedRole");

            migrationBuilder.DropTable(
                name: "ExtendedRoleGlobalUser");

            migrationBuilder.DropColumn(
                name: "IsGlobalRole",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUser_FirstName",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUser_LastName",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "PermissionPolicyUser",
                type: "int",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "GlobalUser_FirstName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "GlobalUser_LastName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "PermissionPolicyUser");

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobalRole",
                table: "PermissionPolicyRoleBase",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CrewExtendedRole",
                columns: table => new
                {
                    CrewRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewExtendedRole", x => new { x.CrewRolesID, x.CrewsID });
                    table.ForeignKey(
                        name: "FK_CrewExtendedRole_PermissionPolicyRoleBase_CrewRolesID",
                        column: x => x.CrewRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewExtendedRole_PermissionPolicyUser_CrewsID",
                        column: x => x.CrewsID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtendedRoleGlobalUser",
                columns: table => new
                {
                    GlobalRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlobalUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendedRoleGlobalUser", x => new { x.GlobalRolesID, x.GlobalUsersID });
                    table.ForeignKey(
                        name: "FK_ExtendedRoleGlobalUser_PermissionPolicyRoleBase_GlobalRolesID",
                        column: x => x.GlobalRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtendedRoleGlobalUser_PermissionPolicyUser_GlobalUsersID",
                        column: x => x.GlobalUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewExtendedRole_CrewsID",
                table: "CrewExtendedRole",
                column: "CrewsID");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendedRoleGlobalUser_GlobalUsersID",
                table: "ExtendedRoleGlobalUser",
                column: "GlobalUsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyRoleBase_Vessels_VesselID",
                table: "PermissionPolicyRoleBase",
                column: "VesselID",
                principalTable: "Vessels",
                principalColumn: "ID");
        }
    }
}
