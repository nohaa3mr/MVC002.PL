using System.ComponentModel.DataAnnotations;

namespace MVC002.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
