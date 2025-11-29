using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace DataAccess.Data
{
    public  class ExamsPaltFormDbContext:IdentityDbContext<IdentityUser>
    {
        public ExamsPaltFormDbContext(DbContextOptions<ExamsPaltFormDbContext> options) : base(options)
        {

        }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public  DbSet<Question> Questions { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }

        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            base.OnModelCreating(modelBuilder);
            //Compsite Primary Key for ExamQuestions
            modelBuilder.Entity<ExamQuestion>().HasKey(eq => new { eq.QuestionId, eq.ExamId});
            //modelBuilder.Entity<StudentExam>().HasKey(se => new { se.ExamId, se.ApplicationUserId });

                modelBuilder.Entity<ApplicationUser>().HasData(
                   new ApplicationUser
                   {
                     Id = "cc2eb532-864e-4d04-b31a-3eba8288b574",
                     UserName = "ryyan2001",
                     NormalizedUserName = "RYYAN2001",
                     Email = "ryanabwryan34@gmail.com",
                     NormalizedEmail = "RYANABWRYAN34@GMAIL.COM",
                     EmailConfirmed = true,
                     PasswordHash = hasher.HashPassword(null, "Ryyan2001,"), // replace with your desired password
                     SecurityStamp = Guid.NewGuid().ToString(),
                     FullName = "Ryyan Abo Ryyan"
                   }

                );
            modelBuilder.Entity<IdentityRole>().HasData(
                  new IdentityRole<string> { Id = "1", Name="Admin",NormalizedName= "ADMIN", ConcurrencyStamp= Guid.NewGuid().ToString()},
                  new IdentityRole<string> { Id = "2", Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = Guid.NewGuid().ToString() },
                  new IdentityRole<string> { Id = "3", Name = "Teacher", NormalizedName = "TEACHER", ConcurrencyStamp = Guid.NewGuid().ToString() }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                 new IdentityUserRole<string>
                 {
                     UserId = "cc2eb532-864e-4d04-b31a-3eba8288b574",
                     RoleId = "1"
                 }
                );

            modelBuilder.Entity<Category>().HasData(
                new { Id = 1, CategoryName = "Mathematics" },
                new { Id = 2, CategoryName = "Physics" },
                new { Id = 3, CategoryName = "Chemistry" },
                new { Id = 4, CategoryName = "Computer Science / IT" },
                new { Id = 5, CategoryName = "Biology" },
                new { Id = 6, CategoryName = "History" },
                new { Id = 7, CategoryName = "Geography" },
                new { Id = 8, CategoryName = "Literature / Language Arts" }

                );

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, QuestionText = "What is the capital of France?", QuestionName = "Capital Cities", ImageUrl = null, Score = 5, Options = new List<string> { "Paris", "London", "Rome", "Berlin" }, CorrectAnswer = "Paris", Difficulty = "Easy", CategoryId = 7 },
new Question { Id = 2, QuestionText = "Which element has the chemical symbol 'O'?", QuestionName = "Chemistry Basics", ImageUrl = null, Score = 3, Options = new List<string> { "Oxygen", "Gold", "Hydrogen", "Iron" }, CorrectAnswer = "Oxygen", Difficulty = "Med", CategoryId = 3 },
new Question { Id = 3, QuestionText = "Which planet is known as the Red Planet?", QuestionName = "Astronomy", ImageUrl = null, Score = 4, Options = new List<string> { "Mars", "Venus", "Jupiter", "Saturn" }, CorrectAnswer = "Mars", Difficulty = "Hard", CategoryId = 2 },

// Physics questions
new Question { Id = 4, QuestionText = "An object in motion stays in motion unless acted upon by an external force. Which law is this?", QuestionName = "Newton's First Law", ImageUrl = null, Score = 5, Options = new List<string> { "First Law", "Second Law", "Third Law", "Law of Gravity" }, CorrectAnswer = "First Law", Difficulty = "Easy", CategoryId = 2 },
new Question { Id = 5, QuestionText = "What is the SI unit of acceleration?", QuestionName = "Acceleration Unit", ImageUrl = null, Score = 3, Options = new List<string> { "m/s²", "m/s", "N", "kg" }, CorrectAnswer = "m/s²", Difficulty = "Easy", CategoryId = 2 },
new Question { Id = 6, QuestionText = "Force equals mass times what?", QuestionName = "Force Calculation", ImageUrl = null, Score = 4, Options = new List<string> { "Acceleration", "Velocity", "Time", "Energy" }, CorrectAnswer = "Acceleration", Difficulty = "Medium", CategoryId = 2 },
new Question { Id = 7, QuestionText = "Which force keeps planets in orbit?", QuestionName = "Gravitational Force", ImageUrl = null, Score = 4, Options = new List<string> { "Electromagnetic", "Gravitational", "Nuclear", "Friction" }, CorrectAnswer = "Gravitational", Difficulty = "Medium", CategoryId = 2 },
new Question { Id = 8, QuestionText = "Which energy does a moving object have?", QuestionName = "Kinetic Energy", ImageUrl = null, Score = 3, Options = new List<string> { "Potential", "Kinetic", "Thermal", "Chemical" }, CorrectAnswer = "Kinetic", Difficulty = "Easy", CategoryId = 2 },
new Question { Id = 9, QuestionText = "Work is done when a force is applied over what?", QuestionName = "Work Concept", ImageUrl = null, Score = 4, Options = new List<string> { "Time", "Distance", "Mass", "Speed" }, CorrectAnswer = "Distance", Difficulty = "Medium", CategoryId = 2 },
new Question { Id = 10, QuestionText = "Momentum equals mass times what?", QuestionName = "Momentum", ImageUrl = null, Score = 4, Options = new List<string> { "Velocity", "Acceleration", "Force", "Energy" }, CorrectAnswer = "Velocity", Difficulty = "Medium", CategoryId = 2 },
new Question { Id = 11, QuestionText = "Voltage equals current times resistance. This is which law?", QuestionName = "Ohm's Law", ImageUrl = null, Score = 5, Options = new List<string> { "Ohm's Law", "Newton's Law", "Faraday's Law", "Hooke's Law" }, CorrectAnswer = "Ohm's Law", Difficulty = "Hard", CategoryId = 2 },
new Question { Id = 12, QuestionText = "Bending of light when passing from one medium to another is called?", QuestionName = "Refraction", ImageUrl = null, Score = 3, Options = new List<string> { "Reflection", "Refraction", "Diffraction", "Absorption" }, CorrectAnswer = "Refraction", Difficulty = "Easy", CategoryId = 2 },
new Question { Id = 13, QuestionText = "What is the unit of frequency?", QuestionName = "Frequency Unit", ImageUrl = null, Score = 3, Options = new List<string> { "Hertz", "Newton", "Joule", "Meter" }, CorrectAnswer = "Hertz", Difficulty = "Easy", CategoryId = 2 },

// Other subjects
new Question { Id = 14, QuestionText = "Who wrote 'Romeo and Juliet'?", QuestionName = "Literature", ImageUrl = null, Score = 3, Options = new List<string> { "Shakespeare", "Hemingway", "Dickens", "Austen" }, CorrectAnswer = "Shakespeare", Difficulty = "Easy", CategoryId = 8 },
new Question { Id = 15, QuestionText = "What is the powerhouse of the cell?", QuestionName = "Cell Biology", ImageUrl = null, Score = 4, Options = new List<string> { "Nucleus", "Mitochondria", "Ribosome", "Chloroplast" }, CorrectAnswer = "Mitochondria", Difficulty = "Easy", CategoryId = 5 },
new Question { Id = 16, QuestionText = "Which year did World War II end?", QuestionName = "History", ImageUrl = null, Score = 5, Options = new List<string> { "1940", "1945", "1950", "1939" }, CorrectAnswer = "1945", Difficulty = "Medium", CategoryId = 6 },
new Question { Id = 17, QuestionText = "What is the chemical symbol for gold?", QuestionName = "Chemistry", ImageUrl = null, Score = 3, Options = new List<string> { "Au", "Ag", "Fe", "Go" }, CorrectAnswer = "Au", Difficulty = "Easy", CategoryId = 3 },
new Question { Id = 18, QuestionText = "What is the largest planet in the solar system?", QuestionName = "Astronomy", ImageUrl = null, Score = 4, Options = new List<string> { "Earth", "Jupiter", "Saturn", "Mars" }, CorrectAnswer = "Jupiter", Difficulty = "Medium", CategoryId = 2 },
new Question { Id = 19, QuestionText = "Which language is used for web development?", QuestionName = "Programming", ImageUrl = null, Score = 3, Options = new List<string> { "C#", "Python", "HTML", "Java" }, CorrectAnswer = "HTML", Difficulty = "Easy", CategoryId = 4 },
new Question { Id = 20, QuestionText = "Which continent is known as the 'Dark Continent'?", QuestionName = "Geography", ImageUrl = null, Score = 4, Options = new List<string> { "Africa", "Asia", "Europe", "Australia" }, CorrectAnswer = "Africa", Difficulty = "Medium", CategoryId = 7 }


            );





        }
    }
}
