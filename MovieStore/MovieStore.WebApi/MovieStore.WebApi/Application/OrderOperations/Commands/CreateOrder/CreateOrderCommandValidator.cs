using FluentValidation;

namespace MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x => x.CustomerId).GreaterThan(0);
			RuleFor(x => x.MovieId).GreaterThan(0);
		}
	}
}
