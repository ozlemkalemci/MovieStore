using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.UpdateCustomer
{
	public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
	{
		public UpdateCustomerCommandValidator()
		{
			RuleFor(command => command.CustomerId).GreaterThan(0);
			RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(2);
			RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2);
		}
	}
}
