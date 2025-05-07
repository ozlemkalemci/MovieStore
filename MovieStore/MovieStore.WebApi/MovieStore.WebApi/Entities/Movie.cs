using System;
using System.Collections.Generic;

namespace MovieStore.WebApi.Entities
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public decimal Price { get; set; }

		public int GenreId { get; set; }
		public Genre Genre { get; set; }

		public int DirectorId { get; set; }
		public Director Director { get; set; }

		public ICollection<ActorMovie> ActorMovies { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
