using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre{
    public class DeleteGenreCommand{
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommand(IBookStoreDbContext contex)
        {
            _context = contex;
        }
        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Zaten öyle bir veri yok");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}