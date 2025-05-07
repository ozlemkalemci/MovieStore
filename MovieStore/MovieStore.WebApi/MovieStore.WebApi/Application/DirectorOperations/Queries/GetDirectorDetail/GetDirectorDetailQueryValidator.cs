using FluentValidation;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
	public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
	{
		public GetDirectorDetailQueryValidator()
		{
			RuleFor(x => x.DirectorId).GreaterThan(0);
		}
	}
}
