using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;

namespace WebApi.BookOperations{
    public class GetById{
        private readonly BookStoreDbContext _context;
        public GetByIdModel Model { get; set; }
        public GetById(BookStoreDbContext context)
        {
            _context = context;
        }

        public GetByIdModel Handle(){
            if(_context.Books.SingleOrDefault(b => b.Title == Model.Title) is null){
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            var book = _context.Books.SingleOrDefault(b => b.Title == Model.Title);
            return new GetByIdModel{
                Title = book.Title,
                GenreId = book.GenreId,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate
            };
        }
    }
    public class GetByIdModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}