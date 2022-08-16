using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using WebApi.BookOperations.Delete;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //read only ler sadece constructure içerisindendeiştirilebilirler.
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context);
            return Ok(query.Handle());
        }
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id){
            try
            {
                return Ok(new GetById(_context).Handle(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel bookModel){
            try{
                CreateBookCommand create = new CreateBookCommand(_context, _mapper);
                create.Model = bookModel;
                //validasyon
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                //bir hata oluştuğunda aşağıdaki catch e gönderir
                validator.ValidateAndThrow(create);
                /*ValidationResult result = validator.Validate(create);
                if(!result.IsValid){
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Özellik : " + item.PropertyName + "~Error Massege: " + item.ErrorMessage);
                    }
                    return BadRequest(Message);
                }else{
                    create.Handle();
                }*/
                create.Handle();
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateBook(UpdateBookModel updatedBookModel){
            try
            {
                UpdateBook updateBook = new UpdateBook(_context);
                updateBook.Model = updatedBookModel;
                UpdateBookValidator validator = new UpdateBookValidator();
                validator.ValidateAndThrow(updateBook);
                updateBook.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteBook([FromBody] DeleteBookModel model){
            try
            {
                DeleteBook deleteBook = new DeleteBook(_context);
                deleteBook.Model = model;
                DeleteBookValidator validator = new DeleteBookValidator();
                validator.ValidateAndThrow(deleteBook);
                if(deleteBook.Handle()){
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }

   
}