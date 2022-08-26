using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.BookOperations.Commands
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<UpdateGenreCommandValidator>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(){
            UpdateGenreCommand command = new UpdateGenreCommand(null, null);
            command.Model = new UpdateGenreModel(){
                Name = "a"
            };
            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldNotBeReturnErrors(){
            UpdateGenreCommand command = new UpdateGenreCommand(null, null);
            command.Model = new UpdateGenreModel(){
                Name = "WhenDateTimeEqualNowIsGiven_Validator_ShouldNotBeReturnErrors"
            };
            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}