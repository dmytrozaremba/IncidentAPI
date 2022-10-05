using FluentValidation;
using IncidentAPI.Data;
using IncidentAPI.DTOs;
using IncidentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController: ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<IncidentDto> _validator;

        public IncidentsController(DataContext dataContext, IValidator<IncidentDto> validator)
        {
            _dataContext = dataContext;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncidentDto request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var account = await _dataContext.Accounts.FirstOrDefaultAsync(c => c.Name == request.AccountName);

            if (account is null)
            {
                return NotFound("Account with provided name does not exist");
            }

            var contact = await _dataContext.Contacts.FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

            contact = (contact is not null) ? contact : new Contact() { Email = request.ContactEmail };

            contact.FirstName = request.ContactFirstName;
            contact.LastName = request.ContactLastName;

            if (!contact.Accounts.Contains(account))
            {
                account.Contact = contact;
            }

            var result = new Incident()
            {
                Description = request.Description
            };

            account.Incedents.Add(result);
            _dataContext.Add(result);
            await _dataContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
