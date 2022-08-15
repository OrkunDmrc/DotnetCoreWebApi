using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //read only ler sadece constructure içerisindendeiştirilebilirler.
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
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
                CreateBookCommand create = new CreateBookCommand(_context);
                create.Model = bookModel;
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
                updateBook.Handle(updatedBookModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{Id}")]
        public IActionResult DeleteBook(int Id){
            var book = _context.Books.SingleOrDefault(b => b.Id == Id);
            if(book is null){
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }

   
}