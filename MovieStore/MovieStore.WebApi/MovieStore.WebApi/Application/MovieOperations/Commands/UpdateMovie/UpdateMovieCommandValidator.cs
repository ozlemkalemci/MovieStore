using FluentValidation;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie
{
	public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
	{
		public UpdateMovieCommandValidator()
		{
			RuleFor(command => command.MovieId).GreaterThan(0);
			RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
			RuleFor(command => command.Model.Year).GreaterThan(1900);
			RuleFor(command => command.Model.GenreId).GreaterThan(0);
			RuleFor(command => command.Model.DirectorId).GreaterThan(0);
			RuleFor(command => command.Model.Price).GreaterThan(0);
		}
	}
}
