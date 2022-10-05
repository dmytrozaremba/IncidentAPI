using FluentValidation;
using IncidentAPI.DTOs;

namespace IncidentAPI.Validators
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Name).Length(1, 20);
            RuleFor(x => x.ContactEmail).EmailAddress();
        }
    }
}
