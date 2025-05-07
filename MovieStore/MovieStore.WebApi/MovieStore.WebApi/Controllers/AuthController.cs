using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.AuthOperations.Commands;
using MovieStore.WebApi.Application.AuthOperations.Models;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.TokenOperations.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IMovieStoreDbContext _context;
	private readonly ITokenHandler _tokenHandler;

	public AuthController(IMovieStoreDbContext context, ITokenHandler tokenHandler)
	{
		_context = context;
		_tokenHandler = tokenHandler;
	}

	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginCustomerModel model)
	{
		var command = new LoginCustomerCommand(_context, _tokenHandler) { Model = model };
		var token = command.Handle();
		return Ok(token);
	}
}
