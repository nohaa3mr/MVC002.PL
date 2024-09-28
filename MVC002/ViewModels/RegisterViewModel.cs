using System.ComponentModel.DataAnnotations;

namespace MVC002.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "First Name Is Required.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage =  "Last Name Is Required.")]
		public string LastName { get; set; }
		[Required(ErrorMessage ="Password is required.")]
		[DataType(DataType.Password)]
        public string Password { get; set; }
		[Required(ErrorMessage = " Confirming Password is required.")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage ="Not Match.")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage ="Email is required.")]
		[EmailAddress(ErrorMessage ="Email is invalid.")]
        public string Email { get; set; }
        public bool IsAgree { get; set; }


    }
}
