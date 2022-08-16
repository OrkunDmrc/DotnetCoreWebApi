using AutoMapper;
using WebApi.BookOperations;
using WebApi.BookOperations.CreateBook;

namespace WebApi.Common{
    public class MappingProfile : Profile{
        public MappingProfile()
        {
            //burada CreateBookModel ile book birbirleri arasında dönüşüm yapabilir.İlki source ikincisi Target ır.
            CreateMap<CreateBookModel, Book>();
            //aşağıdaki kod ile mapperı ayarlama yapabiliyoruz.
            //CreateMap<Book, GetByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}