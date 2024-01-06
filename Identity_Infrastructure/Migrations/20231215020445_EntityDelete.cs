using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity_Infrastructure.Migrations
{
    public partial class EntityDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimIdentity_Claim_ClaimsId",
                table: "ClaimIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimIdentity_Identity_IdentitiesId",
                table: "ClaimIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimRole_Claim_ClaimsId",
                table: "ClaimRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimRole_Role_RolesId",
                table: "ClaimRole");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_Identity_IdentitiesId",
                table: "IdentityRole");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_Role_RolesId",
                table: "IdentityRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityRole",
                table: "IdentityRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRole_RolesId",
                table: "IdentityRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimRole",
                table: "ClaimRole");

            migrationBuilder.DropIndex(
                name: "IX_ClaimRole_RolesId",
                table: "ClaimRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimIdentity",
                table: "ClaimIdentity");

            migrationBuilder.DropIndex(
                name: "IX_ClaimIdentity_IdentitiesId",
                table: "ClaimIdentity");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "IdentityRole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdentitiesId",
                table: "IdentityRole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "IdentityRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IdentityId",
                table: "IdentityRole",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "IdentityRole",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "ClaimRole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimsId",
                table: "ClaimRole",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClaimRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClaimId",
                table: "ClaimRole",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "ClaimRole",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdentitiesId",
                table: "ClaimIdentity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimsId",
                table: "ClaimIdentity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClaimIdentity",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClaimId",
                table: "ClaimIdentity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdentityId",
                table: "ClaimIdentity",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityRole",
                table: "IdentityRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimRole",
                table: "ClaimRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimIdentity",
                table: "ClaimIdentity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_IdentityId",
                table: "IdentityRole",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_RoleId",
                table: "IdentityRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimRole_ClaimId",
                table: "ClaimRole",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimRole_RoleId",
                table: "ClaimRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimIdentity_ClaimId",
                table: "ClaimIdentity",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimIdentity_IdentityId",
                table: "ClaimIdentity",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimIdentity_Claim_ClaimId",
                table: "ClaimIdentity",
                column: "ClaimId",
                principalTable: "Claim",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimIdentity_Identity_IdentityId",
                table: "ClaimIdentity",
                column: "IdentityId",
                principalTable: "Identity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimRole_Claim_ClaimId",
                table: "ClaimRole",
                column: "ClaimId",
                principalTable: "Claim",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimRole_Role_RoleId",
                table: "ClaimRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_Identity_IdentityId",
                table: "IdentityRole",
                column: "IdentityId",
                principalTable: "Identity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_Role_RoleId",
                table: "IdentityRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimIdentity_Claim_ClaimId",
                table: "ClaimIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimIdentity_Identity_IdentityId",
                table: "ClaimIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimRole_Claim_ClaimId",
                table: "ClaimRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ClaimRole_Role_RoleId",
                table: "ClaimRole");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_Identity_IdentityId",
                table: "IdentityRole");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityRole_Role_RoleId",
                table: "IdentityRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityRole",
                table: "IdentityRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRole_IdentityId",
                table: "IdentityRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityRole_RoleId",
                table: "IdentityRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimRole",
                table: "ClaimRole");

            migrationBuilder.DropIndex(
                name: "IX_ClaimRole_ClaimId",
                table: "ClaimRole");

            migrationBuilder.DropIndex(
                name: "IX_ClaimRole_RoleId",
                table: "ClaimRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimIdentity",
                table: "ClaimIdentity");

            migrationBuilder.DropIndex(
                name: "IX_ClaimIdentity_ClaimId",
                table: "ClaimIdentity");

            migrationBuilder.DropIndex(
                name: "IX_ClaimIdentity_IdentityId",
                table: "ClaimIdentity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClaimRole");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "ClaimRole");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ClaimRole");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClaimIdentity");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "ClaimIdentity");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "ClaimIdentity");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "IdentityRole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdentitiesId",
                table: "IdentityRole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "ClaimRole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClaimsId",
                table: "ClaimRole",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdentitiesId",
                table: "ClaimIdentity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClaimsId",
                table: "ClaimIdentity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityRole",
                table: "IdentityRole",
                columns: new[] { "IdentitiesId", "RolesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimRole",
                table: "ClaimRole",
                columns: new[] { "ClaimsId", "RolesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimIdentity",
                table: "ClaimIdentity",
                columns: new[] { "ClaimsId", "IdentitiesId" });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_RolesId",
                table: "IdentityRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimRole_RolesId",
                table: "ClaimRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimIdentity_IdentitiesId",
                table: "ClaimIdentity",
                column: "IdentitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimIdentity_Claim_ClaimsId",
                table: "ClaimIdentity",
                column: "ClaimsId",
                principalTable: "Claim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimIdentity_Identity_IdentitiesId",
                table: "ClaimIdentity",
                column: "IdentitiesId",
                principalTable: "Identity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimRole_Claim_ClaimsId",
                table: "ClaimRole",
                column: "ClaimsId",
                principalTable: "Claim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimRole_Role_RolesId",
                table: "ClaimRole",
                column: "RolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_Identity_IdentitiesId",
                table: "IdentityRole",
                column: "IdentitiesId",
                principalTable: "Identity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRole_Role_RolesId",
                table: "IdentityRole",
                column: "RolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
