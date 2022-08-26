using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public CreateAuthorModel Model { get; set; }
        public CreateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            if(_context.Authors.SingleOrDefault(a => a.FirstName == Model.FirstName && a.LastName == Model.LastName) is not null)
                throw new InvalidOperationException("Yazar kayıtlı.");
            _context.Authors.Add(new Author{FirstName = Model.FirstName,LastName = Model.LastName});
            _context.SaveChanges();
        }
    }
    public class CreateAuthorModel{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

    }

}