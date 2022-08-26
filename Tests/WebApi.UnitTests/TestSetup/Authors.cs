using WebApi;
using WebApi.DbOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTests.TestSetup{
    public static class Authors{
        public static void AddAuthors(this BookStoreDbContext context){
            context.Authors.AddRange(
                new Author {
                    FirstName = "Eric",
                    LastName = "Ries",
                    BirthDate = new DateTime(2020, 04, 20)
                },
                new Author {
                    FirstName = "Charlotte Perkins",
                    LastName = "Gilman",
                    BirthDate = new DateTime(2020, 04, 20)
                },
                new Author {
                    FirstName = "J. R. R.",
                    LastName = "Tolkien",
                    BirthDate = new DateTime(2020, 04, 20)
                }
            );
        }
    }
}