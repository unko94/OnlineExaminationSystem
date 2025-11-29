using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtAndByToExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e125c333-d337-4f74-bcb5-dbbe2c891c43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "13ebb54d-eb49-4b28-a674-248109c82c07");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "4f38ca2e-4aea-46de-b635-b92f633866cc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cc2eb532-864e-4d04-b31a-3eba8288b574",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df43d8e0-957a-4129-a7d8-2fea46583e2a", "AQAAAAIAAYagAAAAEAD+9FLJxkGcrpt8o2jmOcaIDs7ysIWFxnZ3+JKCHaphLaf9TJMkSWiPwzxiwg4dWQ==", "6e0111ac-1933-40fd-9738-21dab40cf902" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Exams");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a695c465-8d9f-432c-8855-004ae2e39dd6", "AQAAAAIAAYagAAAAEHxaz3drezOSUSsgSd+/45FaUB4tF64nYZQpY382AOIWZdSHPqiaOYRTW7ICPyFEnw==", "7877455f-8268-4ff5-b692-69d11a02db1d" });
        }
    }
}
