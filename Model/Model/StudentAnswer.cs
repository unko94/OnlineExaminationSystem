using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class StudentAnswer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("StudentExamId")]
        public int StudentExamId { get; set; }
        public StudentExam StudentExam { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string SelectedOption { get; set; }
        public bool IsCorrect { get; set; }
    }
}
