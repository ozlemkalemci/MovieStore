using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Middlewares;
using MovieStore.WebApi.TokenOperations.Interfaces;
using MovieStore.WebApi.TokenOperations.Models;
using MovieStore.WebApi.TokenOperations.Services;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MovieStore.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			// JWT ayarlarını çek
			services.Configure<TokenOptions>(Configuration.GetSection("Token"));
			var tokenOptions = Configuration.GetSection("Token").Get<TokenOptions>();
			var key = Encoding.UTF8.GetBytes(tokenOptions.SecurityKey);

			// JWT kimlik doğrulama
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(key)
					};
				});

			services.AddAuthorization();

			// Token handler bağla
			services.AddScoped<ITokenHandler, MovieStore.WebApi.TokenOperations.Services.TokenHandler>();

			// DbContext
			services.AddDbContext<MovieStoreDbContext>(options => options.UseInMemoryDatabase("MovieStoreDb"));
			services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());

			// AutoMapper
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			// HTTP Accessor (JWT'den ID almak için)
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// Controllers & Swagger
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieStore.WebApi", Version = "v1" });

				// 🔐 Bearer token desteği
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGci...\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieStore.WebApi v1"));
			}

			app.UseHttpsRedirection();

			// Global Middleware'ler
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<RequestLoggingMiddleware>();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
