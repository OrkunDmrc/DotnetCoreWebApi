using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.DbOperations;

namespace Application.BookOperations.Queries
{
    public class GetBookByIdQueryTest : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookByIdQueryTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn(){
            GetById query = new GetById(_context, _mapper);
            query.BookId = 5;
            FluentActions
                .Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");
        }
        [Fact]
        public void WhenValidIdGiven_Book_ShouldBeBookReturn(){
            GetById query = new GetById(_context, _mapper);
            query.BookId = 1;
            var result = query.Handle();
            result.Should().NotBeNull();
        }
    }
}