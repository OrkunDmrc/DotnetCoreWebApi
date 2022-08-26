using AutoMapper;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi;
using WebApi.DbOperations;
using Xunit;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;

namespace Application.BookOperations.Commands
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenNoBookIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            UpdateBook command = new UpdateBook(_context);
            command.Model = new UpdateBookModel(){
                Id = 20
            };
            FluentActions
            .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı");
        }
        //Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated(){
            //Arrange
            UpdateBook command = new UpdateBook(_context);
            command.Model = new UpdateBookModel(){
                Id = 1,
                Title = "Title", 
                PageCount = 100,
                GenreId = 1,
                PublishDate = System.DateTime.Now.Date.AddYears(-1),
            };
            //act
            //invoke etmezsen çalışmaz
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var book = _context.Books.SingleOrDefault(b => b.Id == command.Model.Id);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(command.Model.PageCount);
            book.GenreId.Should().Be(command.Model.GenreId);
            book.PublishDate.Should().Be(command.Model.PublishDate);
        }
    }
}