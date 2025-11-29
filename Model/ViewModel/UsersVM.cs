using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
     public  class UsersVM
    {
        [ValidateNever]
        public string id { get; set; }
        public string fullName { get; set; }
        public string userName { get; set; }
        public bool emailConfirmed { get; set; }
        public string email { get; set; }
        public int accessFailedCount { get; set; }
        public bool lockoutEnabled { get; set; }
        //public DateTime lockoutEnd { get; set; }
        public string lockoutEnd { get; set; }
        public string role { get; set; }
        [Required]

        public DateTime LockOutTime { get; set; }
    }
}
