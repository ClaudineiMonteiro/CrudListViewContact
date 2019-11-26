using FluentValidation;
using LanceUpContactList.Models;

namespace LanceUpContactList.Validations
{
	public class ContactValidation : AbstractValidator<Contact>
	{
		public ContactValidation()
		{
			RuleFor(c => c.Phone)
				.NotEmpty().WithMessage("{PropertyName} needs to be provided")
				.Length(10).WithMessage("{PropertyName} needs {MinLength} characters");

			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("{PropertyName} needs to be provided")
				.Length(2, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
		}
	}
}
