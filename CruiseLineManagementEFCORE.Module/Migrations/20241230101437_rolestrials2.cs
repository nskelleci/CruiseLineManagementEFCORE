using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseLineManagementEFCORE.Module.Migrations
{
    /// <inheritdoc />
    public partial class rolestrials2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalUserVessel_PermissionPolicyUser_GlobalUsersID",
                table: "GlobalUserVessel");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_PermissionPolicyRoleBase_VesselRoleID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionPolicyUser_Vessels_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropIndex(
                name: "IX_PermissionPolicyUser_VesselRoleID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "CrewMemberId",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "GlobalUser_FirstName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "GlobalUser_LastName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "VesselID",
                table: "PermissionPolicyUser");

            migrationBuilder.DropColumn(
                name: "VesselRoleID",
                table: "PermissionPolicyUser");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangePasswordOnFirstLogon = table.Column<bool>(type: "bit", nullable: false),
                    StoredPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crew_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crew_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrewMemberId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VesselID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Vessels_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BaseRoleBaseUser",
                columns: table => new
                {
                    RolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseRoleBaseUser", x => new { x.RolesID, x.UsersID });
                    table.ForeignKey(
                        name: "FK_BaseRoleBaseUser_PermissionPolicyRoleBase_RolesID",
                        column: x => x.RolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseRoleBaseUser_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderUserKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserForeignKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLoginInfos_Users_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseRoleBaseUser_UsersID",
                table: "BaseRoleBaseUser",
                column: "UsersID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginInfos_UserForeignKey",
                table: "UserLoginInfos",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VesselID",
                table: "Users",
                column: "VesselID");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalUserVessel_Users_GlobalUsersID",
                table: "GlobalUserVessel",
                column: "GlobalUsersID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalUserVessel_Users_GlobalUsersID",
                table: "GlobalUserVessel");

            migrationBuilder.DropTable(
                name: "BaseRoleBaseUser");

            migrationBuilder.DropTable(
                name: "UserLoginInfos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CrewMemberId",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
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
                name: "LastName",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PermissionPolicyUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselID",
                table: "PermissionPolicyUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VesselRoleID",
                table: "PermissionPolicyUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyUser_VesselID",
                table: "PermissionPolicyUser",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyUser_VesselRoleID",
                table: "PermissionPolicyUser",
                column: "VesselRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalUserVessel_PermissionPolicyUser_GlobalUsersID",
                table: "GlobalUserVessel",
                column: "GlobalUsersID",
                principalTable: "PermissionPolicyUser",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionPolicyUser_PermissionPolicyRoleBase_VesselRoleID",
                table: "PermissionPolicyUser",
                column: "VesselRoleID",
                principalTable: "PermissionPolicyRoleBase",
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
