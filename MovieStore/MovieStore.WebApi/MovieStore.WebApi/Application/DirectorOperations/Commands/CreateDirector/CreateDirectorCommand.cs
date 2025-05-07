using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector
{
	public class CreateDirectorCommand
	{
		public CreateDirectorModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public CreateDirectorCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var directorExists = _context.Directors.Any(x =>
				x.FirstName.ToLower() == Model.FirstName.ToLower() &&
				x.LastName.ToLower() == Model.LastName.ToLower());

			if (directorExists)
				throw new InvalidOperationException("Yönetmen zaten mevcut.");

			var director = new Director
			{
				FirstName = Model.FirstName,
				LastName = Model.LastName
			};

			_context.Directors.Add(director);
			_context.SaveChanges();
		}
	}

	public class CreateDirectorModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
