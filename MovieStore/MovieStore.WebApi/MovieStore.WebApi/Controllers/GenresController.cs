using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenres;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GenresController : ControllerBase
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GenresController(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetGenres()
		{
			var query = new GetGenresQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetGenreById(int id)
		{
			var query = new GetGenreDetailQuery(_context, _mapper)
			{
				GenreId = id
			};

			var validator = new GetGenreDetailQueryValidator();
			validator.ValidateAndThrow(query);

			var result = query.Handle();
			return Ok(result);
		}
	}
}
