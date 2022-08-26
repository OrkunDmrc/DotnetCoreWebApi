using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBook;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;

namespace Application.BookOperations.Queries
{
    public class GetGenresQueryTest : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQueryTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenDataIsWanted_Genre_ShouldBeGivenThreeDatas(){
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            
            var result = query.Handle();

            result.Should().HaveCount(3);
        }
    }
}