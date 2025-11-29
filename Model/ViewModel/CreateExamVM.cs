
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Model;
 



namespace Models.ViewModel
{
    public class CreateExamVM
    {
         
        public Exam Exam { get; set; }
        [ValidateNever]
        public  IEnumerable<SelectListItem> Categories { get; set; }
 
        public  int  QuetionsNumbers { get; set; }
        public string Difficulty { get; set; }

    }
}
   