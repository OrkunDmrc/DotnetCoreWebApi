using FluentAssertions;
using FluentValidation;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.BookOperations.Commands
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CreateGenreCommandValidator>
    {
        //[Fact] : Birkere çalıştırır
        [Theory]
        [InlineData("L","F")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName){
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel(){
                FirstName = firstName,
                LastName = lastName
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidIsGiven_Validator_ShouldNotBeReturnErrors(){
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel(){
                FirstName = "firstName",
                LastName = "lastName"
            };
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}