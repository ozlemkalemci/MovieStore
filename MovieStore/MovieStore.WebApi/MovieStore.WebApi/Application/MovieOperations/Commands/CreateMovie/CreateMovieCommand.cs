using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie
{
	public class CreateMovieCommand
	{
		public CreateMovieModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public CreateMovieCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var movieExists = _context.Movies.Any(m => m.Title.ToLower() == Model.Title.ToLower() && m.Year == Model.Year);
			if (movieExists)
				throw new InvalidOperationException("Bu film zaten mevcut.");

			var movie = new Movie
			{
				Title = Model.Title,
				Year = Model.Year,
				GenreId = Model.GenreId,
				DirectorId = Model.DirectorId,
				Price = Model.Price,
				IsActive = true
			};

			_context.Movies.Add(movie);
			_context.SaveChanges();
		}
	}

	public class CreateMovieModel
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public int GenreId { get; set; }
		public int DirectorId { get; set; }
		public decimal Price { get; set; }
	}
}
