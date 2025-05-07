using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers
{
	public class GetCustomersQuery
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetCustomersQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<CustomerViewModel> Handle()
		{
			var customers = _context.Customers.OrderBy(c => c.Id).ToList();
			return _mapper.Map<List<CustomerViewModel>>(customers);
		}
	}

	public class CustomerViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
	}
}
