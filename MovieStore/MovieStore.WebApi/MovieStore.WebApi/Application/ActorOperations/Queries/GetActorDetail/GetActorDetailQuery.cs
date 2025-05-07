using AutoMapper;
using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail
{
	public class GetActorDetailQuery
	{
		public int ActorId { get; set; }
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public ActorDetailViewModel Handle()
		{
			var actor = _context.Actors.FirstOrDefault(x => x.Id == ActorId);
			if (actor == null)
				throw new InvalidOperationException("Oyuncu bulunamadı.");

			return _mapper.Map<ActorDetailViewModel>(actor);
		}
	}

	public class ActorDetailViewModel
	{
		public string FullName { get; set; }
	}
}
