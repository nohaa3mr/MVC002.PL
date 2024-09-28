using System.ComponentModel.DataAnnotations;

namespace MVC002.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Password Is Required.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "You Need To Confirm Your New Password.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword" , ErrorMessage ="Doensn't Match")]
        public string ConfirmNewPassword { get; set; }
    }
}
