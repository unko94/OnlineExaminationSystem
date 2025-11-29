using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByandAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Createdby",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "6c9b3729-9c00-4d19-b65b-6a37b9910086");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7f43db07-e304-4559-b0d8-d311bc140f94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "7a4fa02a-d115-4298-a217-87496d4a8145");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0ff0717-cd4d-4e48-b7c4-1002d50b5bc0", "AQAAAAIAAYagAAAAECQffSDnIiS0LnBd4YEFWaf44xegYtpanxt9k/yteq7hUZgDhs4gcVK5BULFzuq4cw==", "1146d150-b802-4580-af92-386b4efecae3" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Createdby" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Createdby",
                table: "Questions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2fc215ad-e1e4-4aa7-93c2-25daa1a22694");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f7190d75-f769-4a43-84ce-52bfe9656cb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "00250a98-3301-460d-954b-660fa0e3d1d2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6d1edbf-dcb4-48b2-b22d-dc20d365de79", "AQAAAAIAAYagAAAAECenvy/0Lui/b1i/TdeypRdcHtJamNOKT0eynQpTX6vhsCMQBP9GbPo/l42lutlo7g==", "817c9768-f570-4727-be92-dc8614e67307" });
        }
    }
}
