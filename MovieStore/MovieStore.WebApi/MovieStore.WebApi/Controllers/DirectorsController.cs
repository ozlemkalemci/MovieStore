using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.WebApi.DbOperations;
using static MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;
using static MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace MovieStore.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DirectorsController : ControllerBase
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public DirectorsController(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var query = new GetDirectorsQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var query = new GetDirectorDetailQuery(_context, _mapper) { DirectorId = id };
			new GetDirectorDetailQueryValidator().ValidateAndThrow(query);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateDirectorModel model)
		{
			var command = new CreateDirectorCommand(_context) { Model = model };
			new CreateDirectorCommandValidator().ValidateAndThrow(command);
			command.Handle();
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, [FromBody] UpdateDirectorModel model)
		{
			var command = new UpdateDirectorCommand(_context)
			{
				DirectorId = id,
				Model = model
			};
			new UpdateDirectorCommandValidator().ValidateAndThrow(command);
			command.Handle();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var command = new DeleteDirectorCommand(_context) { DirectorId = id };
			new DeleteDirectorCommandValidator().ValidateAndThrow(command);
			command.Handle();
			return Ok();
		}
	}
}
