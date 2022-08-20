
using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetailQuery{
    public class GetGenresDetailQueryValidator : AbstractValidator<GetGenresDetailQuery>
    {
        public GetGenresDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}


