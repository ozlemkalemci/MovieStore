using System;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
	public class GetDirectorDetailQuery
	{
		public int DirectorId { get; set; }
		private readonly IMovieStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetDirectorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public DirectorDetailViewModel Handle()
		{
			var director = _context.Directors.FirstOrDefault(x => x.Id == DirectorId);
			if (director == null)
				throw new InvalidOperationException("Yönetmen bulunamadı.");

			return _mapper.Map<DirectorDetailViewModel>(director);
		}
	}

	public class DirectorDetailViewModel
	{
		public string FullName { get; set; }
	}
}
