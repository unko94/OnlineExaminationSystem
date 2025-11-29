using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface IQuestionRepositery :IRepositery<Question>
    {
        public void Update(Question question);
    }
}
