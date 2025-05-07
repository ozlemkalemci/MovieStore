using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.GenreOperations.Queries.GetGenres
{
	public class GetGenresQuery
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetGenresQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<GenreViewModel> Handle()
		{
			var genres = _context.Genres.OrderBy(x => x.Id).ToList();
			return _mapper.Map<List<GenreViewModel>>(genres);
		}
	}

	public class GenreViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
