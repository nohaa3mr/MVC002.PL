using System.ComponentModel.DataAnnotations;

namespace MVC002.PL.ViewModels
{
	public class LoginViewModel
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
