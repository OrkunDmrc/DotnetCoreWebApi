

using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.Delete{
    public class DeleteBookValidator : AbstractValidator<DeleteBook>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.Model.Id).GreaterThan(0);
        }
    }
}