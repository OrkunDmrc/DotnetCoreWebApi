using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace Application.BookOperations.Commands
{
    public class UpdateBookCommandValidatorTests : IClassFixture<UpdateBookValidator>
    {
        [Theory]
        [InlineData(1, "Lord Of Rings",0,0)]
        [InlineData(2,"Lord Of Rings",0,1)]
        [InlineData(3,"Lord Of Rings",100,0)]
        [InlineData(2,"",0,0)]
        [InlineData(1,"",100,1)]
        [InlineData(2,"Lord",100,10)]
        [InlineData(2,"Lor",100,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string title, int pageCount, int genreId){
            UpdateBook command = new UpdateBook(null);
            command.Model = new UpdateBookModel(){
                Id = id,
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //act
            UpdateBookValidator validator = new UpdateBookValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors(){
            UpdateBook command = new UpdateBook(null);
            command.Model = new UpdateBookModel(){
                Id = 1,
                Title="Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            //act
            UpdateBookValidator validator = new UpdateBookValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldNotBeReturnErrors(){
            UpdateBook command = new UpdateBook(null);
            command.Model = new UpdateBookModel(){
                Id = 1,
                Title="Title",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 1
            };
            //act
            UpdateBookValidator validator = new UpdateBookValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}