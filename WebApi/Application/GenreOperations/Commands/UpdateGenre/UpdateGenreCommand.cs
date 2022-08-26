using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommand(IBookStoreDbContext contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }
        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("bu Id de kategori mevcut değil");
            
            if(_context.Genres.Any(g => g.Name.ToLower() == Model.Name.ToLower() && g.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut");
            
            genre.Name = Model.Name.Trim();
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel{
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}