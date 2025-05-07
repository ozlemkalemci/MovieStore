using System.Collections.Generic;

namespace MovieStore.WebApi.Entities
{
	public class Customer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public ICollection<Order> Orders { get; set; }
		public ICollection<CustomerFavoriteGenre> FavoriteGenres { get; set; }
	}
}
