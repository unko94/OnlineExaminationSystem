using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery.IRepositery
{
    public interface INotificationRepositery : IRepositery<Notification>
    {
        public void Update(Notification notification);
    }
}
