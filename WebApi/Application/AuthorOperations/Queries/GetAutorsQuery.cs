using AutoMapper;

namespace WebApi.DbOperations.Queries{
    public class GetAuthorsQuery
    {
        public GetAuthorsViewModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorsViewModel> Handle(){
            return _mapper.Map<List<GetAuthorsViewModel>>(_context.Authors.ToList());
        }
    }

    public class GetAuthorsViewModel{
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}