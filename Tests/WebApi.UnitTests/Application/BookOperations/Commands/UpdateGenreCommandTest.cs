using AutoMapper;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi;
using WebApi.DbOperations;
using Xunit;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.BookOperations.Commands
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenNoGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(){
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            command.GenreId = 5;
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("bu Id de kategori mevcut değil");
        }
        //Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated(){
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel(){
                Name = "WhenValidInputsAreGiven_Genre_ShouldBeUpdated"
            };
            //act
            //invoke etmezsen çalışmaz
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var genre = _context.Genres.SingleOrDefault(g => g.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}