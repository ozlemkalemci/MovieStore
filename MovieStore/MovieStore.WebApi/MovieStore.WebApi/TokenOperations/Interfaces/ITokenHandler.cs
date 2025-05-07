using MovieStore.WebApi.Entities;
using MovieStore.WebApi.TokenOperations.Models;
using MovieStore.WebApi.TokenOperations.Services;

namespace MovieStore.WebApi.TokenOperations.Interfaces
{
	public interface ITokenHandler
	{
		Token CreateAccessToken(Customer customer);
	}
}
