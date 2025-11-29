 using System.ComponentModel.DataAnnotations;
 

namespace Models.ViewModel
{
    public class ForgetPasswordNewPasswordVM
    {
      

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain uppercase, lowercase and a number")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string newpassword { get; set; }


        [Required]
        [Display(Name = "Confirm New Password")]
        [Compare("newpassword", ErrorMessage = "password and confirmation password must be the same")]
        public string confirmnewpassword { get; set; }

    }
}
