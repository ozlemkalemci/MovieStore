using FluentValidation;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
	public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
	{
		public UpdateDirectorCommandValidator()
		{
			RuleFor(x => x.DirectorId).GreaterThan(0);
			RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(2);
			RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(2);
		}
	}
}
