using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            var author = _context.Authors.SingleOrDefault(a => a.Id == Model.Id);
            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı.");
            if(_context.Authors.Any(a => a.FirstName == Model.FirstName.Trim() && a.LastName == Model.LastName.Trim()))
                throw new InvalidOperationException("Aynı isim ve soy isimde zaten bir yazar mevcut");
            author.FirstName = Model.FirstName.Trim();
            author.LastName = Model.LastName.Trim();
            _context.SaveChanges();
        }
    }
    public class UpdateAuthorModel{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

    }
}