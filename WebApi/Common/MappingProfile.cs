using AutoMapper;
using WebApi.Application.BookOperations;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.Application.BookOperations.Queries.GetBook;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresDetailQuery;

using WebApi.Entities;
using WebApi.DbOperations.Queries;

namespace WebApi.Common{
    public class MappingProfile : Profile{
        public MappingProfile()
        {
            //burada CreateBookModel ile book birbirleri arasında dönüşüm yapabilir.İlki source ikincisi Target ır.
            CreateMap<CreateBookModel, Book>();
            //aşağıdaki kod ile mapperı ayarlama yapabiliyoruz.
            //CreateMap<Book, GetByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, GetByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenresDetailViewModel>();

            CreateMap<Author, GetAuthorsViewModel>();
            CreateMap<Author, GetSpecificAuthorViewModel>();
        }
    }
}