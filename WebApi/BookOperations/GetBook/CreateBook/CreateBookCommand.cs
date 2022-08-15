using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;

namespace WebApi.BookOperations.CreateBook{
    public class CreateBookCommand{
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            if(_context.Books.SingleOrDefault(b => b.Title == Model.Title) is not null){
                throw new InvalidOperationException("Aynı adda kitap mevcut");
            }
            var book = new Book {
                Title = Model.Title,
                GenreId = Model.GenreId,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate
            };
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
    public class CreateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}