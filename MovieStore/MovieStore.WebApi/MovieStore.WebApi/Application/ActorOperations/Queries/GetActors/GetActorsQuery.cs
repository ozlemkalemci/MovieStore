using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActors
{
	public class GetActorsQuery
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetActorsQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<ActorViewModel> Handle()
		{
			var actors = _context.Actors.OrderBy(x => x.Id).ToList();
			return _mapper.Map<List<ActorViewModel>>(actors);
		}
	}

	public class ActorViewModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
	}
}
