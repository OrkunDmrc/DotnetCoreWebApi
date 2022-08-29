using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperatons.Commands{
    public class RefleshTokenCommand{
        public string RefleshToken {get; set;}
        private readonly BookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public RefleshTokenCommand(BookStoreDbContext context, IConfiguration configuration){
            _context = context;
            _configuration = configuration;
        }
        public Token Handle(){
            var user = _context.Users.FirstOrDefault(u => u.RefleshToken == RefleshToken && u.RefleshTokenExpireDate > DateTime.Now);
            if(user is null)
                throw new InvalidOperationException("Valid 1 refleh token bulunamadı");
            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(user);
            
            user.RefleshToken = token.ReffleshToken;
            user.RefleshTokenExpireDate = token.Expiration.AddMinutes(5);
            _context.SaveChanges();
            return token;
        }
    }
}