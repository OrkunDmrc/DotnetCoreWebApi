using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.DbOperations{
    public class DataGenerator{
        //service provider inMomoriden gelen bir şey sistemimizi kurmamız yarar
        public static void Initialize(IServiceProvider serviceProvider){
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                //eğer burada herhangi bir veri varsa 
                if(context.Books.Any()){
                    return;
                }
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
                context.Genres.AddRange(
                    new Genre{
                        Name = "Personel Growth"
                    },
                    new Genre{
                        Name = "Science Fiction",
                    },
                    new Genre{
                        Name = "Romance",
                    }
                );
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
                context.SaveChanges();
            }
        }
    }
}