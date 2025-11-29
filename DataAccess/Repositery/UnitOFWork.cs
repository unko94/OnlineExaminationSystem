using DataAccess.Data;
using DataAccess.Repositery.IRepositery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly ExamsPaltFormDbContext _db;
        

        public IQuestionRepositery question { get; private set; }

        public IExamRepositery exam { get; private set; }

        public ICategoryRepositery category { get; private set; }

       public  IExamQuestionRepositery examQuestion { get; private set; }

        public IStudentAnswerRepositery studentAnswer { get; private set; }

        public IStudentExamRepositery studentExam { get; private set; }

        public INotificationRepositery notification { get; private set; }



        //public IGroupRepositery group { get; private set; }

        public UnitOFWork(ExamsPaltFormDbContext db)
        {
            _db = db;
            question = new QuestionRepositery(_db);
            exam = new ExamRepositery(_db);
            category = new CategoryRepositery(_db);
            examQuestion = new ExamQuestionRepositery(_db);
            studentAnswer = new StudentAnswerRepositery(_db);
            studentExam = new StudentExamRepositery(_db);
            notification = new NotificationRepositery(_db);
         }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
