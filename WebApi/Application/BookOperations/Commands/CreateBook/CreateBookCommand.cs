using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Common;
using AutoMapper;

namespace WebApi.Application.BookOperations.Commands.CreateBook{
    public class CreateBookCommand{
        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            if(_context.Books.SingleOrDefault(b => b.Title == Model.Title) is not null){
                throw new InvalidOperationException("Aynı adda kitap mevcut");
            }
            /*var book = new Book {
                Title = Model.Title,
                GenreId = Model.GenreId,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate
            };*/
            //artık yukarıdaki kodlara ihtiyacımız yok çünkü AutoMapper bunu bizim için halledicek. Aşağıdaki kod onu sağlamaktadır.
            var book = _mapper.Map<Book>(Model);
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