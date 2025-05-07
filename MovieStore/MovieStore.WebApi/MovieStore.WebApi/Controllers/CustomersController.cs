using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStore.WebApi.DbOperations;
using static MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;
using static MovieStore.WebApi.Application.CustomerOperations.Commands.UpdateCustomer.UpdateCustomerCommand;

namespace MovieStore.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomersController : ControllerBase
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public CustomersController(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCustomers()
		{
			var query = new GetCustomersQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetCustomerById(int id)
		{
			var query = new GetCustomerDetailQuery(_context, _mapper) { CustomerId = id };
			var validator = new GetCustomerDetailQueryValidator();
			validator.ValidateAndThrow(query);

			var result = query.Handle();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CreateCustomer([FromBody] CreateCustomerModel model)
		{
			var command = new CreateCustomerCommand(_context) { Model = model };
			var validator = new CreateCustomerCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCustomer(int id, [FromBody] UpdateCustomerModel model)
		{
			var command = new UpdateCustomerCommand(_context)
			{
				CustomerId = id,
				Model = model
			};
			var validator = new UpdateCustomerCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCustomer(int id)
		{
			var command = new DeleteCustomerCommand(_context) { CustomerId = id };
			var validator = new DeleteCustomerCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}
	}
}
