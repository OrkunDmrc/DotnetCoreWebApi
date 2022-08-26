using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.Application.GenreOperations.Queries.GetGenresDetailQuery;
using WebApi.DbOperations;

namespace Application.BookOperations.Queries
{
    public class GetGenreDetilQueryTest : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetilQueryTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeReturn(){
            GetGenresDetailQuery query = new GetGenresDetailQuery(_context, _mapper);
            query.GenreId = 5;
            FluentActions
                .Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }
        [Fact]
        public void WhenValidIdGiven_Genre_ShouldBeBookReturn(){
            GetGenresDetailQuery query = new GetGenresDetailQuery(_context, _mapper);
            query.GenreId = 1;
            var result = query.Handle();
            result.Should().NotBeNull();
        }
    }
}