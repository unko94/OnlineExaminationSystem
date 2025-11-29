using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface IUnitOfWork
    {
        public IQuestionRepositery question { get; }

        public IExamRepositery exam { get; }

        public ICategoryRepositery category { get; }
        public IExamQuestionRepositery examQuestion { get; }
        public IStudentAnswerRepositery studentAnswer { get; }
        public IStudentExamRepositery studentExam { get; }
        public INotificationRepositery notification { get; }
        public void Save();
    }
}
