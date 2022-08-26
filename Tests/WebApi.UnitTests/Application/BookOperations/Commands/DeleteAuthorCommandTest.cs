using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.Delete;
using WebApi.Application.AuthorOperations.Commands;

namespace Application.BookOperations.Commands
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<DeleteAuthorCommandValidator>{
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(){
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.Model = new DeleteAuthorModel{
                Id = 0
            };
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors(){
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.Model = new DeleteAuthorModel{
                Id = 5
            };
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}