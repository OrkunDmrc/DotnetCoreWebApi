using WebApi;
using WebApi.DbOperations;

namespace Tests.WebApi.UnitTests.TestSetup{
    public static class Books{
        public static void AddBooks(this BookStoreDbContext context){
            context.Books.AddRange(
                new Book {
                    //Id=1,
                    Title="Learn Startup",
                    GenreId=1,//Personal Growth
                    AuthorId = 1,
                    PageCount=200,
                    PublishDate=new DateTime(2001,06,12)
                },
                new Book {
                    //Id=2,
                    Title="Herland",
                    GenreId=2,//Science Fiction
                    AuthorId = 2,
                    PageCount=240,
                    PublishDate=new DateTime(2001,06,12)
                },
                new Book {
                    //Id=3,
                    Title="Lord of Rings",
                    GenreId=2,//Science Fiction
                    AuthorId = 3,
                    PageCount=540,
                    PublishDate=new DateTime(2001,06,12)
                }
            );
        }
    }
}