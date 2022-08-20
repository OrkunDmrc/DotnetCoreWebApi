using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {  
            RuleFor(command => command.Model.Id).NotEmpty().GreaterThan(0);
        }
    }
}