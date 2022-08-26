using AutoMapper;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi;
using WebApi.DbOperations;
using Xunit;
using WebApi.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using WebApi.Entities;
using WebApi.Application.AuthorOperations.Commands;

namespace Application.BookOperations.Commands
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrage : Hazırlık
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel(){ FirstName = "Eric", LastName = "Ries" };
            //act : Çalıştırma && assert : Doğrulama
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar kayıtlı.");
        }
        //Happy Path
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            //Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel(){ 
                FirstName = "Emma",
                LastName = "Anderson"
            };
            //act
            //invoke etmezsen çalışmaz
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var author = _context.Authors.SingleOrDefault(a => a.FirstName == command.Model.FirstName && a.LastName == command.Model.LastName);
            author.Should().NotBeNull();
            author.FirstName.Should().Be(command.Model.FirstName);
            author.LastName.Should().Be(command.Model.LastName);
        }
    }
}