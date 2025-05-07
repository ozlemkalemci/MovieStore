using AutoMapper;
using MovieStore.WebApi.Entities;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActors;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders;
using System.Linq;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenres;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;

namespace MovieStore.WebApi.Common
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Movie
			CreateMap<CreateMovieModel, Movie>();

			CreateMap<Movie, MovieViewModel>()
				.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
				.ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName))
				.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorMovies.Select(am => am.Actor.FirstName + " " + am.Actor.LastName)));

			CreateMap<Movie, MovieDetailViewModel>()
				.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
				.ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName))
				.ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorMovies.Select(am => am.Actor.FirstName + " " + am.Actor.LastName)));

			// Genre
			CreateMap<Genre, GenreViewModel>();
			CreateMap<Genre, GenreDetailViewModel>();

			// Director
			CreateMap<CreateDirectorModel, Director>();

			CreateMap<Director, DirectorViewModel>()
				.ForMember(dest => dest.FullName,
						   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

			CreateMap<Director, DirectorDetailViewModel>()
				.ForMember(dest => dest.FullName,
						   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

			// Actor
			CreateMap<Actor, ActorViewModel>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

			CreateMap<Actor, ActorDetailViewModel>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

			CreateMap<CreateActorModel, Actor>();

			// Customer
			CreateMap<Customer, CustomerViewModel>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

			CreateMap<Customer, CustomerDetailViewModel>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
				.ForMember(dest => dest.FavoriteGenres, opt => opt.MapFrom(src => src.FavoriteGenres.Select(fg => fg.Genre.Name)));

			CreateMap<CreateCustomerModel, Customer>();

			// Order
			CreateMap<Order, OrderViewModel>()
				.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
				.ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
				.ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToShortDateString()));
		}
	}
}
