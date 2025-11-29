using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Group_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5c000893-0ee8-49c4-bb73-1a11c08d3a37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "41a81071-095b-4f6f-92fe-2e140f69a88d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "777a9c02-edf6-4a8d-bd37-f1c14203240d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a695c465-8d9f-432c-8855-004ae2e39dd6", null, "AQAAAAIAAYagAAAAEHxaz3drezOSUSsgSd+/45FaUB4tF64nYZQpY382AOIWZdSHPqiaOYRTW7ICPyFEnw==", "7877455f-8268-4ff5-b692-69d11a02db1d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ba6cf050-ff8a-4b21-aac8-1347b54d9ed4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9adc7ca9-4499-4250-a26d-0599f3b6931c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "5d219d17-31e9-44f5-81ad-8cac6dc96714");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "GroupId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2d85b13-40a1-4e49-8e11-73d538e3e242", null, "AQAAAAIAAYagAAAAEIdDGZBEUUcab216YU09I4lK3LBRXzFexby/a+7UVbF1Jdi+M2lSVVdVjDmW5pxAaA==", "ca6e1df8-48fb-4ef4-92f5-cc0efcfc8f3e" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Group_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }
    }
}
