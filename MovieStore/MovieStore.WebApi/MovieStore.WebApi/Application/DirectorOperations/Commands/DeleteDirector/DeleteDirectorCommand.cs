using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
	public class DeleteDirectorCommand
	{
		public int DirectorId { get; set; }
		private readonly IMovieStoreDbContext _context;

		public DeleteDirectorCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
			if (director == null)
				throw new InvalidOperationException("Yönetmen bulunamadı.");

			var hasMovies = _context.Movies.Any(m => m.DirectorId == DirectorId);
			if (hasMovies)
				throw new InvalidOperationException("Yayında filmi olan yönetmen silinemez.");

			_context.Directors.Remove(director);
			_context.SaveChanges();
		}
	}
}
