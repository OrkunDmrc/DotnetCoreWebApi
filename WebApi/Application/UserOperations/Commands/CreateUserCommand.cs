using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperatons.Commands{
    public class CreateUserCommand{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserModel Model {get;set;}
        public CreateUserCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            var user = _context.Users.SingleOrDefault(u => u.Email == Model.Email);
            if(user is not null)
                throw new InvalidOperationException("Aynı e-postada kişi var.");
            _context.Users.Add(_mapper.Map<User>(Model));
            _context.SaveChanges();
        }
    }
    public class CreateUserModel{
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}