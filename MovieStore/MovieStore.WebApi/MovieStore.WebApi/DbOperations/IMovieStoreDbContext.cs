using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Entities;
using System.Collections.Generic;

namespace MovieStore.WebApi.DbOperations
{
	public interface IMovieStoreDbContext
	{
		DbSet<Movie> Movies { get; set; }
		DbSet<Genre> Genres { get; set; }
		DbSet<Director> Directors { get; set; }
		DbSet<Actor> Actors { get; set; }
		DbSet<Customer> Customers { get; set; }
		DbSet<Order> Orders { get; set; }
		DbSet<ActorMovie> ActorMovies { get; set; }
		DbSet<CustomerFavoriteGenre> CustomerFavoriteGenres { get; set; }

		int SaveChanges();
	}
}
