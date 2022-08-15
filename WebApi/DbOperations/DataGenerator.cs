using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DbOperations;

namespace WebApi.DbOperations{
    public class DataGenerator{
        //service provider inMomoriden gelen bir şey sistemimizi kurmamız yarar
        public static void Initialize(IServiceProvider serviceProvider){
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                //eğer burada herhangi bir veri varsa 
                if(context.Books.Any()){
                    return;
                }
                context.Books.AddRange(
                    new Book {
                        //Id=1,
                        Title="Learn Startup",
                        GenreId=1,//Personal Growth
                        PageCount=200,
                        PublishDate=new DateTime(2001,06,12)
                    },
                    new Book {
                        //Id=2,
                        Title="Herland",
                        GenreId=2,//Science Fiction
                        PageCount=240,
                        PublishDate=new DateTime(2001,06,12)
                    },
                    new Book {
                        //Id=3,
                        Title="Learn Startup",
                        GenreId=2,//Science Fiction
                        PageCount=540,
                        PublishDate=new DateTime(2001,06,12)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}