using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
	public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
	{
		public GetCustomerDetailQueryValidator()
		{
			RuleFor(query => query.CustomerId).GreaterThan(0);
		}
	}
}
