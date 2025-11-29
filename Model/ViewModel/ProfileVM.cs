using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ProfileVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; }
        public string Gender { get; set; } = "Male";
        public int TotalExamsTaken { get; set; }
        public double AvgScore { get; set; }
        public int ExamsPassed { get; set; }
        public int ExamsFailed { get; set; }

        //for teacher
        public int TotalCreatedExams { get; set; }
        public int TotalQuestionsUploaded { get; set; }
        public int AverageExamRating { get; set; }

    }
}
