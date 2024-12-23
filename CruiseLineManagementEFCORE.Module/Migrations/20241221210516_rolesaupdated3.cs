using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolesaupdated3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalUserVessel_PermissionPolicyUser_AssignedUsersID",
                table: "GlobalUserVessel");

            migrationBuilder.DropTable(
                name: "CrewVesselRole");

            migrationBuilder.DropTable(
                name: "GlobalRoleGlobalUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalUserVessel",
                table: "GlobalUserVessel");

            migrationBuilder.DropIndex(
                name: "IX_GlobalUserVessel_AssignedVesselsID",
                table: "GlobalUserVessel");

            migrationBuilder.RenameColumn(
                name: "AssignedUsersID",
                table: "GlobalUserVessel",
                newName: "GlobalUsersID");

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobalRole",
                table: "PermissionPolicyRoleBase",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalUserVessel",
                table: "GlobalUserVessel",
                columns: new[] { "AssignedVesselsID", "GlobalUsersID" });

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
                name: "IX_GlobalUserVessel_GlobalUsersID",
                table: "GlobalUserVessel",
                column: "GlobalUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_CrewExtendedRole_CrewsID",
                table: "CrewExtendedRole",
                column: "CrewsID");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendedRoleGlobalUser_GlobalUsersID",
                table: "ExtendedRoleGlobalUser",
                column: "GlobalUsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalUserVessel_PermissionPolicyUser_GlobalUsersID",
                table: "GlobalUserVessel",
                column: "GlobalUsersID",
                principalTable: "PermissionPolicyUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalUserVessel_PermissionPolicyUser_GlobalUsersID",
                table: "GlobalUserVessel");

            migrationBuilder.DropTable(
                name: "CrewExtendedRole");

            migrationBuilder.DropTable(
                name: "ExtendedRoleGlobalUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalUserVessel",
                table: "GlobalUserVessel");

            migrationBuilder.DropIndex(
                name: "IX_GlobalUserVessel_GlobalUsersID",
                table: "GlobalUserVessel");

            migrationBuilder.DropColumn(
                name: "IsGlobalRole",
                table: "PermissionPolicyRoleBase");

            migrationBuilder.RenameColumn(
                name: "GlobalUsersID",
                table: "GlobalUserVessel",
                newName: "AssignedUsersID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalUserVessel",
                table: "GlobalUserVessel",
                columns: new[] { "AssignedUsersID", "AssignedVesselsID" });

            migrationBuilder.CreateTable(
                name: "CrewVesselRole",
                columns: table => new
                {
                    CrewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewVesselRole", x => new { x.CrewsID, x.VesselRolesID });
                    table.ForeignKey(
                        name: "FK_CrewVesselRole_PermissionPolicyRoleBase_VesselRolesID",
                        column: x => x.VesselRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewVesselRole_PermissionPolicyUser_CrewsID",
                        column: x => x.CrewsID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalRoleGlobalUser",
                columns: table => new
                {
                    GlobalRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlobalUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalRoleGlobalUser", x => new { x.GlobalRolesID, x.GlobalUsersID });
                    table.ForeignKey(
                        name: "FK_GlobalRoleGlobalUser_PermissionPolicyRoleBase_GlobalRolesID",
                        column: x => x.GlobalRolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobalRoleGlobalUser_PermissionPolicyUser_GlobalUsersID",
                        column: x => x.GlobalUsersID,
                        principalTable: "PermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalUserVessel_AssignedVesselsID",
                table: "GlobalUserVessel",
                column: "AssignedVesselsID");

            migrationBuilder.CreateIndex(
                name: "IX_CrewVesselRole_VesselRolesID",
                table: "CrewVesselRole",
                column: "VesselRolesID");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalRoleGlobalUser_GlobalUsersID",
                table: "GlobalRoleGlobalUser",
                column: "GlobalUsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalUserVessel_PermissionPolicyUser_AssignedUsersID",
                table: "GlobalUserVessel",
                column: "AssignedUsersID",
                principalTable: "PermissionPolicyUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
