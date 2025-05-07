using System;

namespace MovieStore.WebApi.TokenOperations.Models
{
	public class Token
	{
		public string AccessToken { get; set; }
		public DateTime Expiration { get; set; }
	}
}
