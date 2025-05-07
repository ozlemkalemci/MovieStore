using System.Collections.Generic;

namespace MovieStore.WebApi.Entities
{
	public class Actor
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public ICollection<ActorMovie> ActorMovies { get; set; }
	}
}
