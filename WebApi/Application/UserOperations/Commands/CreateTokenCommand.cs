using AutoMapper;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperatons.Commands{
    public class CreateTokenCommand{
        public CreateTokenModel Model {get;set;}
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(BookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle(){
            var user = _context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
            if(user is null)
                throw new InvalidOperationException("Eposta ve şifre uyuşmuyor");
            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(user);
            user.RefleshToken = token.ReffleshToken;
            user.RefleshTokenExpireDate = token.Expiration.AddMinutes(5);
            _context.SaveChanges();
            return token;
        }
    }
    public class CreateTokenModel{
        public string Email { get; set; }
        public string Password { get; set; }
    }
}