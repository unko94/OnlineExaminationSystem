using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RandID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RandID",
                table: "StudentExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b8d06286-e8a3-41f6-a075-5ba7b5c99797");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "b70ff9c8-ff5d-42d8-aa28-5ccf0f152235");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "fe33da60-7421-4ce6-9ccf-9dbb5e7b692c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c763a6f-27d8-4033-bbe2-05458c7a1744", "AQAAAAIAAYagAAAAECT6L4zG5grcwiD8KNCCKumTDKn7B+zqeW4TG9YpsjO7bycOO1H3zQqnUdgxtGzZeQ==", "7e727a62-a410-43a1-a0d8-e0fe413d7052" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandID",
                table: "StudentExams");

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
        }
    }
}
