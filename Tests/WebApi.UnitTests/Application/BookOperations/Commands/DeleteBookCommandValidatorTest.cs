using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.Delete;

namespace Application.BookOperations.Commands
{
    public class DeleteBookCommandValidatorTests : IClassFixture<DeleteBookValidator>{
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(){
            DeleteBook command = new DeleteBook(null);
            command.Model = new DeleteBookModel(){Id = 0};
            DeleteBookValidator validator = new DeleteBookValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors(){
            DeleteBook command = new DeleteBook(null);
            command.Model = new DeleteBookModel(){Id = 5};
            DeleteBookValidator validator = new DeleteBookValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}