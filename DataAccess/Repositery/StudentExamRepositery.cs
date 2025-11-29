using DataAccess.Data;
using DataAccess.Repositery.IRepositery;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery
{
    public class StudentExamRepositery : Repositery<StudentExam>, IStudentExamRepositery
    {
        private readonly ExamsPaltFormDbContext _db;
        public StudentExamRepositery(ExamsPaltFormDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StudentExam studentExam)
        {
            _db.StudentExams.Update(studentExam);
        }
    }
}
