using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MovieStore.WebApi.Middlewares
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public RequestLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// Request logu
			Console.WriteLine($"➡ [REQUEST] {context.Request.Method} {context.Request.Path}");

			await _next(context);

			// Response logu
			Console.WriteLine($"⬅ [RESPONSE] {context.Response.StatusCode} {context.Request.Path}");
		}
	}
}
