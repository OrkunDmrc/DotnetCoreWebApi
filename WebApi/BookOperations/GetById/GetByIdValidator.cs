using FluentValidation;

namespace WebApi.BookOperations.GetByID{
    public class GetByIdValidator : AbstractValidator<GetById>{
        public GetByIdValidator()
        {
            //sadece bir gösterim yapıyor neden bir validasyonu olsun?
        }
    } 
}