﻿using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
	public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
	{
		public DeleteCustomerCommandValidator()
		{
			RuleFor(command => command.CustomerId).GreaterThan(0);
		}
	}
}
