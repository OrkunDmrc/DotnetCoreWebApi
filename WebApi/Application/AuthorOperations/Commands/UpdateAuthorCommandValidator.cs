using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator(){
            RuleFor(command => command.Model.Id).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.BirthDate).LessThan(DateTime.Today);
        }
    }
}