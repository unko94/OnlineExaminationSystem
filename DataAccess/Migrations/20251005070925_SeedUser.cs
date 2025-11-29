using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "4f736fbd-f29e-4f03-9106-590e813676d1", "Admin", "ADMIN" },
                    { "2", "c22663c1-6568-4d5f-99ac-9a00663ef14d", "Student", "STUDENT" },
                    { "3", "478796f4-e6c9-44f7-abeb-2d835d1c384b", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cc2eb532-864e-4d04-b31a-3eba8288b574", 0, "39e4b6ef-c284-40fc-8681-c54428339b83", "ApplicationUser", "ryanabwryan34@gmail.com", true, "Ryyan Abo Ryyan", false, null, "RYANABWRYAN34@GMAIL.COM", "RYYAN2001", "AQAAAAIAAYagAAAAEPAAMXdqpo3DHl/WAimBWzsNmIcGvTQ6RjVNXjcSmhJgMxjW+V7YSI3euz1eVOuYZg==", null, false, "1bc2fb81-41d1-417c-92d1-8d8989507726", false, "ryyan2001" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "cc2eb532-864e-4d04-b31a-3eba8288b574" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "cc2eb532-864e-4d04-b31a-3eba8288b574" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574");
        }
    }
}
