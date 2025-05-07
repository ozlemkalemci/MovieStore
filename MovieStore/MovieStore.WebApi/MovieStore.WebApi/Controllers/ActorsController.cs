using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActors;
using MovieStore.WebApi.DbOperations;
using static MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;
using static MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace MovieStore.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ActorsController : ControllerBase
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public ActorsController(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetActors()
		{
			var query = new GetActorsQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetActorById(int id)
		{
			var query = new GetActorDetailQuery(_context, _mapper) { ActorId = id };
			new GetActorDetailQueryValidator().ValidateAndThrow(query);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CreateActor([FromBody] CreateActorModel newActor)
		{
			var command = new CreateActorCommand(_context) { Model = newActor };
			new CreateActorCommandValidator().ValidateAndThrow(command);
			command.Handle();
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateActor(int id, [FromBody] UpdateActorModel updateActor)
		{
			var command = new UpdateActorCommand(_context)
			{
				ActorId = id,
				Model = updateActor
			};
			new UpdateActorCommandValidator().ValidateAndThrow(command);
			command.Handle();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteActor(int id)
		{
			var command = new DeleteActorCommand(_context) { ActorId = id };
			new DeleteActorCommandValidator().ValidateAndThrow(command);
			command.Handle();
			return Ok();
		}
	}
}
