using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface IExamRepositery : IRepositery<Exam>
    {
        public void Update(Exam exam);
    }
}
