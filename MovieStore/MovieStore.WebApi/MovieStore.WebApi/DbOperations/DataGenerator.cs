using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.DbOperations
{
	public class DataGenerator
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>());

			if (context.Genres.Any() || context.Directors.Any() || context.Actors.Any() || context.Movies.Any())
				return;

			// Genres
			var genre1 = new Genre { Name = "Action" };
			var genre2 = new Genre { Name = "Comedy" };
			var genre3 = new Genre { Name = "Drama" };

			context.Genres.AddRange(genre1, genre2, genre3);

			// Directors
			var director1 = new Director { FirstName = "Christopher", LastName = "Nolan" };
			var director2 = new Director { FirstName = "Quentin", LastName = "Tarantino" };

			context.Directors.AddRange(director1, director2);

			// Actors
			var actor1 = new Actor { FirstName = "Leonardo", LastName = "DiCaprio" };
			var actor2 = new Actor { FirstName = "Joseph", LastName = "Gordon-Levitt" };

			context.Actors.AddRange(actor1, actor2);

			context.SaveChanges();

			// Movies
			var movie1 = new Movie
			{
				Title = "Inception",
				Year = 2010,
				GenreId = genre1.Id,
				DirectorId = director1.Id,
				Price = 29.99m,
				IsActive = true
			};

			var movie2 = new Movie
			{
				Title = "Pulp Fiction",
				Year = 1994,
				GenreId = genre3.Id,
				DirectorId = director2.Id,
				Price = 19.99m,
				IsActive = true
			};

			context.Movies.AddRange(movie1, movie2);
			context.SaveChanges();

			// Actor-Movie relations
			context.ActorMovies.AddRange(
				new ActorMovie { MovieId = movie1.Id, ActorId = actor1.Id },
				new ActorMovie { MovieId = movie1.Id, ActorId = actor2.Id },
				new ActorMovie { MovieId = movie2.Id, ActorId = actor1.Id } // örnek olsun
			);

			context.SaveChanges();
		}
	}
}
