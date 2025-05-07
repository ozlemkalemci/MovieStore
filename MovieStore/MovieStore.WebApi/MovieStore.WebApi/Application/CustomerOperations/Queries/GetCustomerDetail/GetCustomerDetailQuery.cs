using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
	public class GetCustomerDetailQuery
	{
		public int CustomerId { get; set; }
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetCustomerDetailQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public CustomerDetailViewModel Handle()
		{
			var customer = _context.Customers.FirstOrDefault(x => x.Id == CustomerId);
			if (customer == null)
				throw new InvalidOperationException("Müşteri bulunamadı.");

			return _mapper.Map<CustomerDetailViewModel>(customer);
		}
	}

	public class CustomerDetailViewModel
	{
		public string FullName { get; set; }
		public List<string> FavoriteGenres { get; set; }
	}
}
