using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MovieStore.WebApi.DbOperations
{
	public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
	{
		public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) { }

		public DbSet<Movie> Movies { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Director> Directors { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<ActorMovie> ActorMovies { get; set; }
		public DbSet<CustomerFavoriteGenre> CustomerFavoriteGenres { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ActorMovie>().HasKey(am => new { am.ActorId, am.MovieId });

			modelBuilder.Entity<CustomerFavoriteGenre>().HasKey(cfg => new { cfg.CustomerId, cfg.GenreId });

			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
