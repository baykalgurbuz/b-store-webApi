using FluentValidation;
using WebApi.BookOperations.GetDetailQuery;

namespace WebApi.BookOperations.GetDetail
{
    public class  GetDetailQueryValidator :AbstractValidator<GetBookDetailQuery>
    {
        public  GetDetailQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}