

using FluentValidation;

namespace WebApi.BookOperations.Delete{
    public class DeleteBookValidator : AbstractValidator<DeleteBook>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.Model.Id).GreaterThan(0);
        }
    }
}