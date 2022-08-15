using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;

namespace WebApi.BookOperations.UpdateBook{
    public class UpdateBook{
        private readonly BookStoreDbContext _context;
        public UpdateBook(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(UpdateBookModel updateBookModel){
            if(_context.Books.SingleOrDefault(b => b.Id == updateBookModel.Id) is null){
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");
            }
            var book = _context.Books.Find(updateBookModel.Id);
            book.Title = updateBookModel.Title;
            book.GenreId = updateBookModel.GenreId;
            book.PageCount = updateBookModel.PageCount;
            book.PublishDate = updateBookModel.PublishDate;
            _context.SaveChanges();
        }
    }
    public class UpdateBookModel{
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}