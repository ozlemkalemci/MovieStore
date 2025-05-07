﻿using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieStore.WebApi.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var result = JsonSerializer.Serialize(new
			{
				status = context.Response.StatusCode,
				error = "Bir hata oluştu.",
				details = exception.Message
			});

			Console.WriteLine($"❌ [ERROR] {exception.Message}");

			return context.Response.WriteAsync(result);
		}
	}
}
