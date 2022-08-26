using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetailQuery
{
    public class GetGenresDetailQuery{
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenresDetailViewModel> Handle(){
            var genre = _context.Genres.Where(g => g.IsActive && g.Id == GenreId).OrderBy(g => g.Id);
            if(genre is null){
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            return _mapper.Map<List<GenresDetailViewModel>>(genre);
        }
    }
    public class GenresDetailViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}