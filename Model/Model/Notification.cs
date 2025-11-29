using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Notification
    {
        public int Id { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string  ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;

    }
}
