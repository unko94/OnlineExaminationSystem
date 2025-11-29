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
    public class StudentAnswerRepositery : Repositery<StudentAnswer>, IStudentAnswerRepositery
    {
        private readonly ExamsPaltFormDbContext _db;
        public StudentAnswerRepositery(ExamsPaltFormDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StudentAnswer studentAnswer)
        {
            _db.Update(studentAnswer);
        }
    }
}
