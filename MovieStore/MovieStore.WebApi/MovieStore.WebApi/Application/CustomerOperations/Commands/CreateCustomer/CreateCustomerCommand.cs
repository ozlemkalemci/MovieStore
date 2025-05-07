using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
	public class CreateCustomerCommand
	{
		public CreateCustomerModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public CreateCustomerCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var customerExists = _context.Customers.Any(x =>
				x.FirstName.ToLower() == Model.FirstName.ToLower() &&
				x.LastName.ToLower() == Model.LastName.ToLower());

			if (customerExists)
				throw new InvalidOperationException("Müşteri zaten mevcut.");

			var customer = new Customer
			{
				FirstName = Model.FirstName,
				LastName = Model.LastName,
				Email = Model.Email,
				Password = Model.Password // ileride hashlenebilir
			};

			_context.Customers.Add(customer);
			_context.SaveChanges();
		}
	}

	public class CreateCustomerModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
