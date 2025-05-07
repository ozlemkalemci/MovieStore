using FluentValidation;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie
{
	public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
	{
		public CreateMovieCommandValidator()
		{
			RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(2);
			RuleFor(x => x.Model.Year).GreaterThan(1900).LessThanOrEqualTo(System.DateTime.Now.Year);
			RuleFor(x => x.Model.GenreId).GreaterThan(0);
			RuleFor(x => x.Model.DirectorId).GreaterThan(0);
			RuleFor(x => x.Model.Price).GreaterThan(0);
		}
	}
}
