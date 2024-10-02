using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display(Name ="First Name")]
		public string FirstName { get; set; } = null!;
		[Display(Name = "Last Name")]

		public string LastName { get; set; } = null!;
		[Required]
		public string UserName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Compare("Password" , ErrorMessage ="Confirm Password Dosen't Match Password")]
		public string ConfirmPassword { get; set; } = null!;

		public bool IsAgree { get; set; }

	}
}
