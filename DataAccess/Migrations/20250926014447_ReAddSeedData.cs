using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReAddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
