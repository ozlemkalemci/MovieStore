using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor
{
	public class DeleteActorCommand
	{
		public int ActorId { get; set; }
		private readonly IMovieStoreDbContext _context;

		public DeleteActorCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var actor = _context.Actors.FirstOrDefault(x => x.Id == ActorId);
			if (actor == null)
				throw new InvalidOperationException("Oyuncu bulunamadı.");

			bool isRelatedToMovie = _context.ActorMovies.Any(am => am.ActorId == ActorId);
			if (isRelatedToMovie)
				throw new InvalidOperationException("Filmi olan bir oyuncu silinemez.");

			_context.Actors.Remove(actor);
			_context.SaveChanges();
		}
	}
}
