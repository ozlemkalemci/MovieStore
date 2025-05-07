using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie
{
	public class DeleteMovieCommand
	{
		public int MovieId { get; set; }
		private readonly IMovieStoreDbContext _context;

		public DeleteMovieCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == MovieId && m.IsActive);
			if (movie == null)
				throw new InvalidOperationException("Film bulunamadı veya zaten silinmiş.");

			movie.IsActive = false; // Fiziksel silme yerine pasif yapıyoruz
			_context.SaveChanges();
		}
	}
}
