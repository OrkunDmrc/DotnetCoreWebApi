using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBook{
    public class GetBooksQuery{
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public List<BookViewModel> Handle(){
            var bookList = _context.Books.OrderBy(b => b.Id).ToList();
            List<BookViewModel> bookViewModels = bookList.Select(b => new BookViewModel{
                Title = b.Title,
                Genre = ((GenreEnum)b.GenreId).ToString(),
                PageCount = b.PageCount,
                PublishDate = b.PublishDate.Date.ToString("dd/mm/yyyy")
            }).ToList();
            return bookViewModels;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }

    public class BookViewModel{
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}