using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.Delete
{
    public class DeleteBook{
        public DeleteBookModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteBook(IBookStoreDbContext context)
        {
            _context = context;
        }
        public bool Handle(){
            if( _context.Books.SingleOrDefault(b => b.Id == Model.Id) is not null){
                _context.Books.Remove(_context.Books.Find(Model.Id));
                _context.SaveChanges();
                return true;
            }
            throw new InvalidOperationException("Data yok.");
        }
    }

    public class DeleteBookModel{
        public int Id { get; set; }
    }
}