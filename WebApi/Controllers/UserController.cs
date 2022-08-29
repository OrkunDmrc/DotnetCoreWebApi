using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperatons.Commands;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public UserController(BookStoreDbContext context, IMapper mapper, IConfiguration configuration){
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel model){
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = model;
            command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login){
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }
        [HttpGet("refleshToken")]
        public ActionResult<Token> RefleshToken([FromQuery] string refleshToken){
            RefleshTokenCommand command = new RefleshTokenCommand(_context, _configuration);
            command.RefleshToken = refleshToken;
            var newToken = command.Handle();
            return newToken;
        }
    }
}