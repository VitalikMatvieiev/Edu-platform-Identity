using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity_Infrastructure.Migrations
{
    public partial class ValidationAndHashing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 2431);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 2960);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 3808);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 5481);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 5803);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 6292);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 6343);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 6658);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 6750);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 7188);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 7596);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 8301);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 8683);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 8980);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 9229);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 9252);

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: 9528);

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Identity",
                newName: "PasswordSalt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Identity",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Identity",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Identity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Claim",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Identity");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Identity",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Identity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Identity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Claim",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 31, "DeleteMessage" },
                    { 446, "DownloadFile" },
                    { 600, "EditMessage" },
                    { 2431, "ModerateDiscussions" },
                    { 2960, "ShareContent" },
                    { 3808, "UnbanUser" },
                    { 5481, "DeleteGroup" },
                    { 5803, "ViewUserProfile" },
                    { 6292, "AuditSystemLogs" },
                    { 6343, "EditOwnProfile" },
                    { 6658, "BanUser" },
                    { 6750, "UploadFile" },
                    { 7188, "ViewSensitiveData" },
                    { 7596, "EditGroupSettings" },
                    { 8301, "ApproveRegistrations" },
                    { 8683, "CreateGroup" },
                    { 8980, "DeleteAccount" },
                    { 9229, "PostMessage" },
                    { 9252, "EditSensitiveData" },
                    { 9528, "AccessPrivateForum" }
                });
        }
    }
}
