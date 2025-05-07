using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStore.WebApi.DbOperations;
using static MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using static MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;

namespace MovieStore.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public MoviesController(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetMovies()
		{
			var query = new GetMoviesQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetMovieById(int id)
		{
			var query = new GetMovieDetailQuery(_context, _mapper)
			{
				MovieId = id
			};

			var validator = new GetMovieDetailQueryValidator();
			validator.ValidateAndThrow(query);

			var result = query.Handle();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CreateMovie([FromBody] CreateMovieModel newMovie)
		{
			var command = new CreateMovieCommand(_context)
			{
				Model = newMovie
			};

			var validator = new CreateMovieCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updatedMovie)
		{
			var command = new UpdateMovieCommand(_context)
			{
				MovieId = id,
				Model = updatedMovie
			};

			var validator = new UpdateMovieCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMovie(int id)
		{
			var command = new DeleteMovieCommand(_context)
			{
				MovieId = id
			};

			var validator = new DeleteMovieCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}
	}
}
