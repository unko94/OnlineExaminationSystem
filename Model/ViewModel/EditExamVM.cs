using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class EditExamVM
    {
        [Required]
        public int ExamId { get; set; }
        [Required]
        public string ExamName { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
      
        public List<int> SelectedQ { get; set; }
        [ValidateNever]
        public  Dictionary<Question,bool> Questions { get; set; }
    }
}
