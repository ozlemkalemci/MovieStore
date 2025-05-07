using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.UpdateCustomer
{
	public class UpdateCustomerCommand
	{
		public int CustomerId { get; set; }
		public UpdateCustomerModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public UpdateCustomerCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var customer = _context.Customers.FirstOrDefault(x => x.Id == CustomerId);
			if (customer == null)
				throw new InvalidOperationException("Müşteri bulunamadı.");

			customer.FirstName = string.IsNullOrWhiteSpace(Model.FirstName) ? customer.FirstName : Model.FirstName;
			customer.LastName = string.IsNullOrWhiteSpace(Model.LastName) ? customer.LastName : Model.LastName;

			_context.SaveChanges();
		}
	}

	public class UpdateCustomerModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
