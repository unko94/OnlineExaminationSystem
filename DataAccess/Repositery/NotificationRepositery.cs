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
    public class NotificationRepositery : Repositery<Notification>, INotificationRepositery
    {
        private readonly ExamsPaltFormDbContext _db;
        public NotificationRepositery(ExamsPaltFormDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Notification notification)
        {
            _db.Notifications.Update(notification);
        }
    }
}
