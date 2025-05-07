using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Queries.GetMovies
{
	public class GetMoviesQuery
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<MovieViewModel> Handle()
		{
			var movies = _context.Movies
				.Where(m => m.IsActive)
				.OrderBy(m => m.Id)
				.ToList();

			return _mapper.Map<List<MovieViewModel>>(movies);
		}
	}

	public class MovieViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public string Genre { get; set; }
		public string Director { get; set; }
		public decimal Price { get; set; }
		public List<string> Actors { get; set; }
	}
}
