using FluentValidation;
using IncidentAPI.DTOs;

namespace IncidentAPI.Validators
{
    public class IncidentValidator : AbstractValidator<IncidentDto>
    {
        public IncidentValidator()
        {
            RuleFor(x => x.AccountName).Length(1, 20);
            RuleFor(x => x.ContactFirstName).Length(1, 20);
            RuleFor(x => x.ContactLastName).Length(1, 20);
            RuleFor(x => x.ContactEmail).EmailAddress();
            RuleFor(x => x.Description).Length(10, 200);
        }
    }
}
