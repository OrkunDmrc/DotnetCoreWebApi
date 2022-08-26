using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApi.Application.BookOperations.Queries.GetBook{
    public class GetBooksQuery{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var bookList = _context.Books.Include(b => b.Genre).Include(b => b.Author).OrderBy(b => b.Id).ToList();
            List<BookViewModel> bookViewModels = _mapper.Map<List<BookViewModel>>(bookList);
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
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
    }
}