namespace MovieStore.WebApi.Entities
{
	public class CustomerFavoriteGenre
	{
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		public int GenreId { get; set; }
		public Genre Genre { get; set; }
	}
}
