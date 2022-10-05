using FluentValidation;
using IncidentAPI.DTOs;

namespace IncidentAPI.Validators
{
    public class ContactValidator: AbstractValidator<ContactDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName).Length(1, 20);
            RuleFor(x => x.LastName).Length(1, 20);
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
