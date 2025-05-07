using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectors
{
	public class GetDirectorsQuery
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetDirectorsQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<DirectorViewModel> Handle()
		{
			var directors = _context.Directors.OrderBy(x => x.Id).ToList();
			return _mapper.Map<List<DirectorViewModel>>(directors);
		}
	}

	public class DirectorViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
	}
}
