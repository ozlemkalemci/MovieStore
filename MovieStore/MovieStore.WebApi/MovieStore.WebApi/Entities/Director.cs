using System;
using System.Collections.Generic;

namespace MovieStore.WebApi.Entities
{
	public class Director
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public ICollection<Movie> DirectedMovies { get; set; }
	}
}
