using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public  class EditProfileVM
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }
        [ValidateNever]
        public List<SelectListItem> Extentions { get; set; }
        public string Extention { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

    }
}
