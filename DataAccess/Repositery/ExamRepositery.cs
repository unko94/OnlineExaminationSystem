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
    public class ExamRepositery : Repositery<Exam>, IExamRepositery
    {
        private readonly ExamsPaltFormDbContext _db;
        public ExamRepositery(ExamsPaltFormDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Exam exam)
        {
            _db.Exams.Update(exam);
        }
    }
}
