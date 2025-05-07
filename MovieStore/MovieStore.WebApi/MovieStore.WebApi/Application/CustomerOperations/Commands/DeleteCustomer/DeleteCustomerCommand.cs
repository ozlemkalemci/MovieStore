using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
	public class DeleteCustomerCommand
	{
		public int CustomerId { get; set; }
		private readonly IMovieStoreDbContext _context;

		public DeleteCustomerCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var customer = _context.Customers.FirstOrDefault(x => x.Id == CustomerId);
			if (customer == null)
				throw new InvalidOperationException("Müşteri bulunamadı.");

			_context.Customers.Remove(customer);
			_context.SaveChanges();
		}
	}
}
