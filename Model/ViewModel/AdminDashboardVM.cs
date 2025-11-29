using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public  class AdminDashboardVM
    {
        public string UserName { get; set; }
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalExams { get; set; }
        public int ExamsInProgress { get; set; }
    }
}
