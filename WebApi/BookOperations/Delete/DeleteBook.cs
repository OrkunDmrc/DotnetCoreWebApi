using WebApi.DbOperations;

namespace WebApi.BookOperations.Delete
{
    public class DeleteBook{
        public DeleteBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public DeleteBook(BookStoreDbContext context)
        {
            _context = context;
        }
        public bool Handle(){
            if( _context.Books.SingleOrDefault(b => b.Id == Model.Id) is not null){
                _context.Books.Remove(_context.Books.Find(Model.Id));
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

    public class DeleteBookModel{
        public int Id { get; set; }
    }
}