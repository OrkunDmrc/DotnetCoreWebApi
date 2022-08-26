using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Application.BookOperations.Queries.GetBook;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.Delete;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresDetailQuery;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations.Queries;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetGenres(){
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            return Ok(query.Handle());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var query = new GetGenresDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenresDetailQueryValidator validator = new GetGenresDetailQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel model){
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = model;
            command.Handle();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, UpdateGenreModel model){
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            command.GenreId = id;
            command.Model = model;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}