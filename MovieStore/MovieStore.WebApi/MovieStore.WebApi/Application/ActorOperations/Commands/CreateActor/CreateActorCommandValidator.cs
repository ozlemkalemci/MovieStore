using FluentValidation;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor
{
	public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
	{
		public CreateActorCommandValidator()
		{
			RuleFor(x => x.Model.FirstName).NotEmpty().MinimumLength(2);
			RuleFor(x => x.Model.LastName).NotEmpty().MinimumLength(2);
		}
	}
}
