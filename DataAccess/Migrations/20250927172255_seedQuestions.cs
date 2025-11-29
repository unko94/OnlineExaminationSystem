using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Difficulty", "ImageUrl", "Options", "QuestionName", "QuestionText", "Score" },
                values: new object[,]
                {
                    { 4, 2, "Easy", null, "[\"First Law\",\"Second Law\",\"Third Law\",\"Law of Gravity\"]", "Newton's First Law", "An object in motion stays in motion unless acted upon by an external force. Which law is this?", 5.0 },
                    { 5, 2, "Easy", null, "[\"m/s\\u00B2\",\"m/s\",\"N\",\"kg\"]", "Acceleration Unit", "What is the SI unit of acceleration?", 3.0 },
                    { 6, 2, "Medium", null, "[\"Acceleration\",\"Velocity\",\"Time\",\"Energy\"]", "Force Calculation", "Force equals mass times what?", 4.0 },
                    { 7, 2, "Medium", null, "[\"Electromagnetic\",\"Gravitational\",\"Nuclear\",\"Friction\"]", "Gravitational Force", "Which force keeps planets in orbit?", 4.0 },
                    { 8, 2, "Easy", null, "[\"Potential\",\"Kinetic\",\"Thermal\",\"Chemical\"]", "Kinetic Energy", "Which energy does a moving object have?", 3.0 },
                    { 9, 2, "Medium", null, "[\"Time\",\"Distance\",\"Mass\",\"Speed\"]", "Work Concept", "Work is done when a force is applied over what?", 4.0 },
                    { 10, 2, "Medium", null, "[\"Velocity\",\"Acceleration\",\"Force\",\"Energy\"]", "Momentum", "Momentum equals mass times what?", 4.0 },
                    { 11, 2, "Hard", null, "[\"Ohm\\u0027s Law\",\"Newton\\u0027s Law\",\"Faraday\\u0027s Law\",\"Hooke\\u0027s Law\"]", "Ohm's Law", "Voltage equals current times resistance. This is which law?", 5.0 },
                    { 12, 2, "Easy", null, "[\"Reflection\",\"Refraction\",\"Diffraction\",\"Absorption\"]", "Refraction", "Bending of light when passing from one medium to another is called?", 3.0 },
                    { 13, 2, "Easy", null, "[\"Hertz\",\"Newton\",\"Joule\",\"Meter\"]", "Frequency Unit", "What is the unit of frequency?", 3.0 },
                    { 14, 8, "Easy", null, "[\"Shakespeare\",\"Hemingway\",\"Dickens\",\"Austen\"]", "Literature", "Who wrote 'Romeo and Juliet'?", 3.0 },
                    { 15, 5, "Easy", null, "[\"Nucleus\",\"Mitochondria\",\"Ribosome\",\"Chloroplast\"]", "Cell Biology", "What is the powerhouse of the cell?", 4.0 },
                    { 16, 6, "Medium", null, "[\"1940\",\"1945\",\"1950\",\"1939\"]", "History", "Which year did World War II end?", 5.0 },
                    { 17, 3, "Easy", null, "[\"Au\",\"Ag\",\"Fe\",\"Go\"]", "Chemistry", "What is the chemical symbol for gold?", 3.0 },
                    { 18, 2, "Medium", null, "[\"Earth\",\"Jupiter\",\"Saturn\",\"Mars\"]", "Astronomy", "What is the largest planet in the solar system?", 4.0 },
                    { 19, 4, "Easy", null, "[\"C#\",\"Python\",\"HTML\",\"Java\"]", "Programming", "Which language is used for web development?", 3.0 },
                    { 20, 7, "Medium", null, "[\"Africa\",\"Asia\",\"Europe\",\"Australia\"]", "Geography", "Which continent is known as the 'Dark Continent'?", 4.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
