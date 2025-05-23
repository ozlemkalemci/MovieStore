﻿using FluentValidation;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie
{
	public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
	{
		public DeleteMovieCommandValidator()
		{
			RuleFor(command => command.MovieId).GreaterThan(0);
		}
	}
}
