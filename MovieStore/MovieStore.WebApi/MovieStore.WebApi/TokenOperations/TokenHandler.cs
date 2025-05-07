using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieStore.WebApi.Entities;
using MovieStore.WebApi.TokenOperations.Models;

namespace MovieStore.WebApi.TokenOperations
{
	public class TokenHandler
	{
		public IConfiguration Configuration { get; }
		public TokenHandler(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public string CreateAccessToken(Customer customer)
		{
			var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
				new Claim(ClaimTypes.Name, customer.FirstName + " " + customer.LastName),
				new Claim(ClaimTypes.Role, "Customer")
			};

			var token = new JwtSecurityToken(
				issuer: tokenOptions.Issuer,
				audience: tokenOptions.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
