using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie
{
	public class UpdateMovieCommand
	{
		public int MovieId { get; set; }
		public UpdateMovieModel Model { get; set; }
		private readonly IMovieStoreDbContext _context;

		public UpdateMovieCommand(IMovieStoreDbContext context)
		{
			_context = context;
		}

		public void Handle()
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == MovieId);
			if (movie == null)
				throw new InvalidOperationException("Film bulunamadı.");

			movie.Title = string.IsNullOrWhiteSpace(Model.Title) ? movie.Title : Model.Title;
			movie.Year = Model.Year != default ? Model.Year : movie.Year;
			movie.Price = Model.Price != default ? Model.Price : movie.Price;
			movie.GenreId = Model.GenreId != default ? Model.GenreId : movie.GenreId;
			movie.DirectorId = Model.DirectorId != default ? movie.DirectorId : movie.DirectorId;

			_context.SaveChanges();
		}
	}

	public class UpdateMovieModel
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public int GenreId { get; set; }
		public int DirectorId { get; set; }
		public decimal Price { get; set; }
	}
}
