using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public  class ExamVM
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public double ExamScore { get; set; }
        public int TotalQuestions { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

       
    }
}
