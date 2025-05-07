using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder
{
	public class CreateOrderCommand
	{
		public int CustomerId { get; set; }  // Artık burada tutulacak
		public int MovieId { get; set; }     // Sadece movie ID dışarıdan alınacak

		private readonly IMovieStoreDbContext _context;

		public CreateOrderCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var customer = _context.Customers.FirstOrDefault(x => x.Id == CustomerId);
			var movie = _context.Movies.FirstOrDefault(x => x.Id == MovieId && x.IsActive);

			if (customer == null)
				throw new InvalidOperationException("Müşteri bulunamadı.");

			if (movie == null)
				throw new InvalidOperationException("Film bulunamadı veya pasif durumda.");

			var alreadyOrdered = _context.Orders.Any(o => o.CustomerId == CustomerId && o.MovieId == MovieId);
			if (alreadyOrdered)
				throw new InvalidOperationException("Bu film zaten satın alınmış.");

			var order = new Order
			{
				CustomerId = CustomerId,
				MovieId = MovieId,
				Price = movie.Price,
				PurchaseDate = DateTime.Now
			};

			_context.Orders.Add(order);
			_context.SaveChanges();
		}

		public class CreateOrderRequestModel
		{
			public int MovieId { get; set; }
		}
	}
}
