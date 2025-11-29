using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class TeacherHomeVM
    {
        public List<Notification> Notifications { get; set; }
        public List<AvgsVM> Avgs { get; set; }
        public int ExamsNumber { get; set; }
        public int QuestionsNumber { get; set; }

        public double AvgForAllExams { get; set; }
    }

}
