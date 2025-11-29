using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Model;
 

namespace Models.ViewModel
{
    public class CreateQuestionVM
    {
        public Question Question { get; set; }
        public IEnumerable<SelectListItem> Categries { get; set; }

    }
}
