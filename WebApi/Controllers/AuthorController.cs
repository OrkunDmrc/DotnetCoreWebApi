using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.DbOperations;
using WebApi.DbOperations.Queries;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAuthors()
        {
            return Ok(new GetAuthorsQuery(_context, _mapper).Handle());
        }
        [HttpGet("{id}")]
        public IActionResult GetSpecificAuthors(int id){
            GetSpecificAuthorsQuery query = new GetSpecificAuthorsQuery(_context,_mapper);
            query.ModelId = id;
            return Ok(query.Handle());
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel model){
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = model;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateAuthor([FromBody] UpdateAuthorModel model){
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.Model = model;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteAuthor([FromBody]DeleteAuthorModel model){
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Model = model;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}