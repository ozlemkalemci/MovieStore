using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders
{
	public class GetOrdersQuery
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetOrdersQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<OrderViewModel> Handle()
		{
			var orders = _context.Orders.OrderBy(x => x.Id).ToList();
			return _mapper.Map<List<OrderViewModel>>(orders);
		}
	}

	public class OrderViewModel
	{
		public string CustomerName { get; set; }
		public string MovieTitle { get; set; }
		public decimal Price { get; set; }
		public string PurchaseDate { get; set; }
	}
}
