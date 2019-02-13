using FA.Business.DTOs;
using FluentValidation;

namespace FA.Business.Validators
{
    public class PersonValidator : AbstractValidator<PersonDto>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).Matches(".{1,128}");
        }
    }
}
