using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBook;
using WebApi.DbOperations;

namespace Application.BookOperations.Queries
{
    public class GetBooksQueryTest : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQueryTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenDataIsWanted_Book_ShouldBeGivenThreeDatas(){
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            
            var result = query.Handle();

            result.Should().HaveCount(3);
        }
    }
}