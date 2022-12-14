using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook{
    public class UpdateBookValidator : AbstractValidator<UpdateBook>
    {
        public UpdateBookValidator()
        {
            RuleFor(c => c.Model.Id).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}