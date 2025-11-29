using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface IStudentExamRepositery : IRepositery<StudentExam>
    {
        public void Update(StudentExam studentExam);
    }
}
