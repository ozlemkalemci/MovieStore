using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor
{
	public class CreateActorCommand
	{
		public CreateActorModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public CreateActorCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var actorExists = _context.Actors.Any(x =>
				x.FirstName.ToLower() == Model.FirstName.ToLower() &&
				x.LastName.ToLower() == Model.LastName.ToLower());

			if (actorExists)
				throw new InvalidOperationException("Oyuncu zaten mevcut.");

			var actor = new Actor
			{
				FirstName = Model.FirstName,
				LastName = Model.LastName
			};

			_context.Actors.Add(actor);
			_context.SaveChanges();
		}
	}

	public class CreateActorModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
