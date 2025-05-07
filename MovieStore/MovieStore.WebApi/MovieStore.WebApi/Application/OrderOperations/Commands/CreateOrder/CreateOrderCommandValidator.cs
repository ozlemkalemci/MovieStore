using FluentValidation;

namespace MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x => x.Model.CustomerId).GreaterThan(0);
			RuleFor(x => x.Model.MovieId).GreaterThan(0);
		}
	}
}
