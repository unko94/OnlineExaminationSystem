
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Model;
using System.ComponentModel.DataAnnotations;
 
namespace Models.ViewModel
{
    public class CreateUserVM
    {
        [Required]
        [Display(Name = "FirstName")]
        public string firstname { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string lastname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
         ErrorMessage = "Password must contain uppercase, lowercase and a number")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }


        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "password and confirmation password must be the same")]
        public string confirmpassword { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string username { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> roles { get; set; }
        public  string roleid { get; set; }
        public string phonenumber { get; set; }
        [ValidateNever]
        public List<SelectListItem> extentions { get; set; }
        public string extention { get; set; }


    }
}
