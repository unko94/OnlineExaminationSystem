using DataAccess.Data;
using DataAccess.Repositery.IRepositery;
using Models.Model;
 

namespace DataAccess.Repositery
{
    public class QuestionRepositery : Repositery<Question>, IQuestionRepositery
    {
        private readonly ExamsPaltFormDbContext _db;
        public QuestionRepositery(ExamsPaltFormDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Question question)
        {
            _db.Questions.Update(question);
        }
    }
}
