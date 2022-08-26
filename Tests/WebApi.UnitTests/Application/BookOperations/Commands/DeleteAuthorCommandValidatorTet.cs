using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.Application.BookOperations.Commands.Delete;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;

namespace Application.BookOperations.Commands
{
    public class DeleteGenreCommandValidtorTest : IClassFixture<DeleteAuthorCommandValidator>{
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandValidtorTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenThereIsNoRecord_InvalidOperationException_ShoulBeReturn(){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 5;
            FluentActions
                .Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Zaten öyle bir veri yok");
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeDeleted(){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            var genre = _context.Genres.SingleOrDefault(b => b.Id == command.GenreId);
            
            genre.Should().BeNull();
        }

    }
}