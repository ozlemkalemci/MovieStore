using System;
using System.Linq;
using MovieStore.WebApi.Application.AuthOperations.Models;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.TokenOperations.Interfaces;
using MovieStore.WebApi.TokenOperations.Models;
using MovieStore.WebApi.TokenOperations.Services;

namespace MovieStore.WebApi.Application.AuthOperations.Commands
{
	public class LoginCustomerCommand
	{
		public LoginCustomerModel Model { get; set; }

		private readonly IMovieStoreDbContext _context;
		private readonly ITokenHandler _tokenHandler;

		public LoginCustomerCommand(IMovieStoreDbContext context, ITokenHandler tokenHandler)
		{
			_context = context;
			_tokenHandler = tokenHandler;
		}

		public Token Handle()
		{
			var customer = _context.Customers.SingleOrDefault(x =>
				x.Email.ToLower() == Model.Email.ToLower() &&
				x.Password == Model.Password); // ileride hash kontrolü

			if (customer is null)
				throw new InvalidOperationException("E-posta veya şifre hatalı.");

			return _tokenHandler.CreateAccessToken(customer);
		}
	}
}
