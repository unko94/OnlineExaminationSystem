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
    internal class ExamQuestionRepositery : Repositery<ExamQuestion>, IExamQuestionRepositery
    {
        private readonly ExamsPaltFormDbContext _db;
        public ExamQuestionRepositery(ExamsPaltFormDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(ExamQuestion examQuestion)
        {
            _db.ExamQuestions.Update(examQuestion);
        }
    }
}
