using WebApi.DbOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;

namespace Tests.WebApi.UnitTests.TestSetup{
    public class CommonTestFixture{
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            //yaratıldığından emin olmak için 
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}