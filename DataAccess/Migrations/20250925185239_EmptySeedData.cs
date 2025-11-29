using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmptySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Mathematics" },
                    { 2, "Physics" },
                    { 3, "Chemistry" },
                    { 4, "Computer Science / IT" },
                    { 5, "Biology" },
                    { 6, "History" },
                    { 7, "Geography" },
                    { 8, "Literature / Language Arts" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Difficulty", "ImageUrl", "Options", "QuestionName", "QuestionText", "Score" },
                values: new object[,]
                {
                    { 1, 7, "Easy", null, "[\"Paris\",\"London\",\"Rome\",\"Berlin\"]", "Capital Cities", "What is the capital of France?", 5.0 },
                    { 2, 3, "Med", null, "[\"Oxygen\",\"Gold\",\"Hydrogen\",\"Iron\"]", "Chemistry Basics", "Which element has the chemical symbol 'O'?", 3.0 },
                    { 3, 2, "Hard", null, "[\"Mars\",\"Venus\",\"Jupiter\",\"Saturn\"]", "Astronomy", "Which planet is known as the Red Planet?", 4.0 }
                });
        }
    }
}
