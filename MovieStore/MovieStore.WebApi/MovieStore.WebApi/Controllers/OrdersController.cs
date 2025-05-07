using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders;
using MovieStore.WebApi.DbOperations;
using static MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;

namespace MovieStore.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public OrdersController(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetOrders()
		{
			var query = new GetOrdersQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);
		}

		[HttpPost]
		public IActionResult CreateOrder([FromBody] CreateOrderModel model)
		{
			var command = new CreateOrderCommand(_context) { Model = model };
			var validator = new CreateOrderCommandValidator();
			validator.ValidateAndThrow(command);

			command.Handle();
			return Ok();
		}
	}
}
