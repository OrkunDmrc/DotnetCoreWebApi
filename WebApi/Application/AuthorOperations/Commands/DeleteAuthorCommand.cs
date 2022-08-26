using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands{
    public class DeleteAuthorCommand
    {
        public DeleteAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            var author = _context.Authors.SingleOrDefault(a => a.Id == Model.Id);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı.");
            if(_context.Books.Any(b => b.AuthorId == author.Id))
                throw new InvalidOperationException("Yazarı sillmeden önce kitabını siliniz.");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
    public class DeleteAuthorModel{
        public int Id { get; set; }
    }
}