using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public  class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
