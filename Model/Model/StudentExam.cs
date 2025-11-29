using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Models.Model
{
    public class StudentExam
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ExamId")]
        public int ExamId { get; set; }
        [ValidateNever]
        public Exam Exam { get; set; }
        
        public double Score { get; set; }

        public string Status { get; set; }
        public string Result { get; set; }
        public int RandID { get; set; }
    }
}
