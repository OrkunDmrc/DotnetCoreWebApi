using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.CreateBook;

namespace Application.BookOperations.Commands
{
    public class CreateBookCommandValidatorTests : IClassFixture<CreateBookCommandValidator>
    {
        //[Fact] : Birkere çalıştırır
        [Theory]
        [InlineData("Lord Of Rings",0,0)]
        [InlineData("Lord Of Rings",0,1)]
        [InlineData("Lord Of Rings",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("Lord",100,10)]
        [InlineData("Lor",100,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel(){
                Title=title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]

        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors(){
             CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel(){
                Title="Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        //Happy path : her şey doğru çalışıyor mu diye yazılır
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldNotBeReturnErrors(){
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel(){
                Title="Title",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 1
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}