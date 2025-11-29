using Microsoft.AspNetCore.Mvc.Rendering;
 

namespace Models.ViewModel
{
    public  class QuestionMangmentVM
    {

        //public List<SelectListItem> categories { get; set; }
        public int examId { get; set; }
        public int categoryid { get; set; }
        public string examname { get; set; }
        public int duration { get; set; }
    }
}
