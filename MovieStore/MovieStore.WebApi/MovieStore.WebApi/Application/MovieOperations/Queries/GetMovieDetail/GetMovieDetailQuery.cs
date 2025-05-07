using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
	public class GetMovieDetailQuery
	{
		public int MovieId { get; set; }
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public MovieDetailViewModel Handle()
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == MovieId && m.IsActive);
			if (movie == null)
				throw new InvalidOperationException("Film bulunamadı.");

			return _mapper.Map<MovieDetailViewModel>(movie);
		}
	}

	public class MovieDetailViewModel
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public string Genre { get; set; }
		public string Director { get; set; }
		public decimal Price { get; set; }
		public List<string> Actors { get; set; }
	}
}
