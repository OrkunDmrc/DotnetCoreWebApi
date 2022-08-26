using AutoMapper;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi;
using WebApi.DbOperations;
using Xunit;
using WebApi.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.BookOperations.Commands
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrage : Hazırlık
            var genre = new Genre(){
                Name = "Romance"
            };

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel(){ Name = genre.Name };

            //act : Çalıştırma && assert : Doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("bu türde kategori mevcut");
        }
        //Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated(){
            //Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel(){ 
                Name = "WhenValidInputsAreGiven_Genre_ShouldBeCreated"
            };
            //act
            //invoke etmezsen çalışmaz
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var genre = _context.Genres.SingleOrDefault(b => b.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}