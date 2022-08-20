using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApi.Application.BookOperations.Queries.GetById{
    public class GetById{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        

        public int BookId { get; set; }
        public GetById(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetByIdModel Handle(){
            /*if(_context.Books.SingleOrDefault(b => b.Id == Model.Title) is null){
                throw new InvalidOperationException("Kitap bulunamadı");
            }*/
            var book = _context.Books.Include(b => b.Genre).Include(b => b.Author).SingleOrDefault(b => b.Id == BookId);
            return _mapper.Map<GetByIdModel>(book);
        }
    }
    public class GetByIdModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}