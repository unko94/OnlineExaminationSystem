using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public string QuestionText { get; set; }
        
        public string? ImageUrl { get; set; }
        [Required]
        public List<string> Options { get; set; }

        public string Difficulty { get; set; }

        [ForeignKey("CategoryId")]
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }

        public string? Createdby { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
