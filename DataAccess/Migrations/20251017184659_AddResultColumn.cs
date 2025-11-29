using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddResultColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "StudentExams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e3c0588d-1f62-45ba-8f9d-0a91079f91f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "3cb14f81-f472-487d-a77e-e30bcee1e4a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "4e9fca29-f1b3-412f-a1b1-f9432387e8b9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bf08fe1-51cf-4b7b-8a72-7e6f8d13baf7", "AQAAAAIAAYagAAAAEDNRHyvt2QAbS1fnGIrvjEGaVQjodEI9azUja7LnvgS9CzGKUTirTE6QwzPUDLwINg==", "1d48c0f0-63e7-4698-aebf-652ded1859bd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "StudentExams");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4bcc5fc5-dabc-438b-b6d3-3c902a47410c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ad156952-ab71-4ba8-82e7-34dbb3ff4e52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "cb7a33b9-2a3f-45c8-9e0c-a9d5da2a6b17");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e724f2b-e73b-463f-ac87-03c081719c6b", "AQAAAAIAAYagAAAAEONjRjYSDuNIMA13m7Copryb4IUauCnTAqLxEOrnpKz1RjDnFsypmxealQPfy7zCOg==", "41837c08-ea0e-40f3-90f8-df753975ca18" });
        }
    }
}
