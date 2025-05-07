using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder
{
	public class CreateOrderCommand
	{
		public CreateOrderModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public CreateOrderCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var customer = _context.Customers.FirstOrDefault(x => x.Id == Model.CustomerId);
			var movie = _context.Movies.FirstOrDefault(x => x.Id == Model.MovieId && x.IsActive);

			if (customer == null)
				throw new InvalidOperationException("Müşteri bulunamadı.");

			if (movie == null)
				throw new InvalidOperationException("Film bulunamadı veya pasif durumda.");

			var order = new Order
			{
				CustomerId = Model.CustomerId,
				MovieId = Model.MovieId,
				Price = movie.Price,
				PurchaseDate = DateTime.Now
			};

			_context.Orders.Add(order);
			_context.SaveChanges();
		}
	}

	public class CreateOrderModel
	{
		public int CustomerId { get; set; }
		public int MovieId { get; set; }
	}
}
