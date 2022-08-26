using AutoMapper;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi;
using WebApi.DbOperations;
using Xunit;
using WebApi.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;

namespace Application.BookOperations.Commands
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrage : Hazırlık
            var book = new Book(){
                Title ="WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990,01,10),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){ Title = book.Title };

            //act : Çalıştırma && assert : Doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı adda kitap mevcut");

            
        }
        //Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            //Arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){ 
                Title = "Title", 
                PageCount = 100,
                GenreId = 1,
                PublishDate = System.DateTime.Now.Date.AddYears(-1),
            };
            //act
            //invoke etmezsen çalışmaz
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var book = _context.Books.SingleOrDefault(b => b.Title == command.Model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(command.Model.PageCount);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.PublishDate.Should().Be(command.Model.PublishDate);
        }
    }
}