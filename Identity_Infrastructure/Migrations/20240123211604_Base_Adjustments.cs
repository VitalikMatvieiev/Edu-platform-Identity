using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity_Infrastructure.Migrations
{
    public partial class Base_Adjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimsId = table.Column<int>(type: "int", nullable: true),
                    IdentitiesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimIdentity_Claim_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claim",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClaimIdentity_Identity_IdentitiesId",
                        column: x => x.IdentitiesId,
                        principalTable: "Identity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    IdentityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Identity_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "Identity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimsId = table.Column<int>(type: "int", nullable: true),
                    RolesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimRole_Claim_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claim",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClaimRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentitiesId = table.Column<int>(type: "int", nullable: true),
                    RolesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRole_Identity_IdentitiesId",
                        column: x => x.IdentitiesId,
                        principalTable: "Identity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentityRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ReadClaims" },
                    { 2, "WriteClaim" },
                    { 3, "ChangeClaim" },
                    { 4, "DeleteClaim" },
                    { 5, "ReadRoles" },
                    { 6, "WriteRole" },
                    { 7, "ChangeRole" },
                    { 8, "DeleteRole" },
                    { 9, "ReadIdentities" },
                    { 10, "ReadOwnIdentity" },
                    { 11, "WriteIdentity" },
                    { 12, "ChangeIdentity" },
                    { 13, "DeleteIdentity" }
                });

            migrationBuilder.InsertData(
                table: "Identity",
                columns: new[] { "Id", "Email", "LastLogin", "LastLogout", "PasswordHash", "PasswordSalt", "RegistrationDate", "Username" },
                values: new object[] { 1, "eduplatform@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0n2D6RfevH0iAO2odcm6QcsXPxSnTQMeT1VaCpVZ4qc=", "GWJHabKuRfxzdRAMvdjGxQ==", new DateTime(2024, 1, 11, 4, 23, 43, 492, DateTimeKind.Unspecified).AddTicks(5829), "superadmin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "ClaimRole",
                columns: new[] { "Id", "ClaimsId", "RolesId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 11, 1 },
                    { 12, 12, 1 },
                    { 13, 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "IdentitiesId", "RolesId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimIdentity_ClaimsId",
                table: "ClaimIdentity",
                column: "ClaimsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimIdentity_IdentitiesId",
                table: "ClaimIdentity",
                column: "IdentitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimRole_ClaimsId",
                table: "ClaimRole",
                column: "ClaimsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimRole_RolesId",
                table: "ClaimRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_IdentitiesId",
                table: "IdentityRole",
                column: "IdentitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_RolesId",
                table: "IdentityRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdentityId",
                table: "RefreshToken",
                column: "IdentityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimIdentity");

            migrationBuilder.DropTable(
                name: "ClaimRole");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Identity");
        }
    }
}
