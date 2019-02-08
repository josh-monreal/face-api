using FA.Business.DTOs;
using FluentValidation;

namespace FA.Business.Validators
{
    class PersonGroupValidator : AbstractValidator<PersonGroupDto>
    {
        public PersonGroupValidator()
        {
            RuleFor(pg => pg.PersonGroupId).Matches("^[a-z0-9_-]{1,64}$");
        }
    }
}
