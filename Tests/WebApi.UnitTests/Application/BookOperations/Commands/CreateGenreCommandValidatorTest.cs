using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.BookOperations.Commands
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CreateGenreCommandValidator>
    {
        //[Fact] : Birkere çalıştırır
        [Theory]
        [InlineData("L")]
        [InlineData("Lor")]
        [InlineData("Lo")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name){
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model = new CreateGenreModel(){
                Name = name
            };
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidIsGiven_Validator_ShouldNotBeReturnErrors(){
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model = new CreateGenreModel(){
                Name = "Test"
            };
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}