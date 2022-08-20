using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(BookStoreDbContext contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }
        public void Handle(){
            if(_context.Genres.SingleOrDefault(g => g.Name == Model.Name) is not null)
                throw new InvalidOperationException("bu türde kategori mevcut");
            _context.Genres.Add(new Genre{Name = Model.Name});
            _context.SaveChanges();
        }
        
    }
    public class CreateGenreModel{
        public string Name { get; set; }
    }
}