using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string ExamName { get; set; }

        public int Duration { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }
        [ValidateNever]
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        [NotMapped]
        public int QuestionsCount { get; set; }
        [ValidateNever]
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
