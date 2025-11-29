using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface IExamQuestionRepositery : IRepositery<ExamQuestion>
    {
        public void Update(ExamQuestion examQuestion);

    }
}
