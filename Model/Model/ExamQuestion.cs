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
    public class ExamQuestion
    {
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        [ValidateNever]
        public Question Question { get; set; }

        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        
        public Exam Exam { get; set; }

    }
}
