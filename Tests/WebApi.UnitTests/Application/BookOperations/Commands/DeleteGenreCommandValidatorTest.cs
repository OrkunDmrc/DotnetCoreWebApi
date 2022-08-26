using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.Delete;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Application.BookOperations.Commands
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<DeleteGenreCommandValidator>{
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(){
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors(){
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 5;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}