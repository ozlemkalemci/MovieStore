using FluentValidation;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector
{
	public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
	{
		public CreateDirectorCommandValidator()
		{
			RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(2);
			RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(2);
		}
	}
}
