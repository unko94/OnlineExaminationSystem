using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Model;

namespace Models.ViewModel
{
    public  class SubmitExamVM
    {
        //  public List<Question> questions { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        [ValidateNever]
        public string  StudentName { get; set; }
        [ValidateNever]
        public List<StudentAnswer> studentAnswers { get; set; }
        public StudentExam studentExam { get; set; }
        [ValidateNever]
        public double score { get; set; }
        [ValidateNever]
        public int RemainingTime { get; set; }
        [ValidateNever]
        public int TotalQuestions { get; set; }
        [ValidateNever]
        public int UnAnsweredQ { get; set; }
        [ValidateNever]
        public int AnsweredQ { get; set; }
        public int AttempID { get; set; }
        public int Duration { get; set; }


    }
}
