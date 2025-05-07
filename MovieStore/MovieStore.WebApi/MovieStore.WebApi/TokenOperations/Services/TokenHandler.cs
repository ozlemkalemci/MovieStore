using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieStore.WebApi.Entities;
using MovieStore.WebApi.TokenOperations.Interfaces;
using MovieStore.WebApi.TokenOperations.Models;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieStore.WebApi.TokenOperations.Services
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IConfiguration _configuration;

		public TokenHandler(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Token CreateAccessToken(Customer customer)
		{
			var tokenOptions = _configuration.GetSection("Token").Get<TokenOptions>();
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
				new Claim(ClaimTypes.Name, $"{customer.FirstName} {customer.LastName}")
			};

			var jwtToken = new JwtSecurityToken(
				issuer: tokenOptions.Issuer,
				audience: tokenOptions.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

			return new Token
			{
				AccessToken = tokenString,
				Expiration = jwtToken.ValidTo
			};
		}
	}
}
