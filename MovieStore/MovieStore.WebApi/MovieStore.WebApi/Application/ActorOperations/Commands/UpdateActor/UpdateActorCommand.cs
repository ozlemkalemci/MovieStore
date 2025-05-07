using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor
{
	public class UpdateActorCommand
	{
		public int ActorId { get; set; }
		public UpdateActorModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public UpdateActorCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var actor = _context.Actors.FirstOrDefault(x => x.Id == ActorId);
			if (actor == null)
				throw new InvalidOperationException("Oyuncu bulunamadı.");

			actor.FirstName = string.IsNullOrWhiteSpace(Model.FirstName) ? actor.FirstName : Model.FirstName;
			actor.LastName = string.IsNullOrWhiteSpace(Model.LastName) ? actor.LastName : Model.LastName;

			_context.SaveChanges();
		}
	}

	public class UpdateActorModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
