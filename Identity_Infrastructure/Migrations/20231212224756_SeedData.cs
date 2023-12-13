using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity_Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
