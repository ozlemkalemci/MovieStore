using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
	public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
	{
		public CreateCustomerCommandValidator()
		{
			RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(2);
			RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(2);
		}
	}
}
