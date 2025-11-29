using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class TakeExamVM
    {
        public int StudentExamId { get; set; }
        public int ExamId { get; set; }
        public List<Question> Questions { get; set; }
        public  string ExamName { get; set; }
        public int StartTime { get; set; }

        public int EndTime { get; set; }
  
        public int QuestionsCount { get; set; }
       // public  IEnumerable<StudentAnswer> StudentAnswers { get; set; }
        public int CurrentIndex { get; set; }

        // public string? Answer { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
        public List<double> Score { get; set; }

        public  int Duration { get; set; }
        public int AttempID { get; set; }

    }
}
