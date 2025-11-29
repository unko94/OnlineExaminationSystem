using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addduration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Exams");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2d85b13-40a1-4e49-8e11-73d538e3e242", "AQAAAAIAAYagAAAAEIdDGZBEUUcab216YU09I4lK3LBRXzFexby/a+7UVbF1Jdi+M2lSVVdVjDmW5pxAaA==", "ca6e1df8-48fb-4ef4-92f5-cc0efcfc8f3e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Exams");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Exams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
