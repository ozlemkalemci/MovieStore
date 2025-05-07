using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
	public class UpdateDirectorCommand
	{
		public int DirectorId { get; set; }
		public UpdateDirectorModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public UpdateDirectorCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
			if (director == null)
				throw new InvalidOperationException("Yönetmen bulunamadı.");

			director.FirstName = string.IsNullOrWhiteSpace(Model.FirstName) ? director.FirstName : Model.FirstName;
			director.LastName = string.IsNullOrWhiteSpace(Model.LastName) ? director.LastName : Model.LastName;

			_context.SaveChanges();
		}
	}

	public class UpdateDirectorModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
