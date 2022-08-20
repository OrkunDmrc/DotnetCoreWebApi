using AutoMapper;

namespace WebApi.DbOperations.Queries{
    public class GetSpecificAuthorsQuery
    {
        public int ModelId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetSpecificAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetSpecificAuthorViewModel Handle(){
            var author = _context.Authors.SingleOrDefault(a => a.Id == ModelId);

            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı.");

            return _mapper.Map<GetSpecificAuthorViewModel>(author);
        }
    }

    public class GetSpecificAuthorViewModel{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}