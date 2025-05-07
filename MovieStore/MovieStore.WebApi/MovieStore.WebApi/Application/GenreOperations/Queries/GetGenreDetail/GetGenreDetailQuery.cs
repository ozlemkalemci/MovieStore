using System;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQuery
	{
		public int GenreId { get; set; }
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetGenreDetailQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public GenreDetailViewModel Handle()
		{
			var genre = _context.Genres.FirstOrDefault(g => g.Id == GenreId);
			if (genre == null)
				throw new InvalidOperationException("Tür bulunamadı.");

			return _mapper.Map<GenreDetailViewModel>(genre);
		}
	}

	public class GenreDetailViewModel
	{
		public string Name { get; set; }
	}
}
