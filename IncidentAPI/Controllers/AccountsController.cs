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
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<AccountDto> _validator;

        public AccountsController(DataContext dataContext, IValidator<AccountDto> validator)
        {
            _dataContext = dataContext;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountDto request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            if (await _dataContext.Accounts.AnyAsync(c => c.Name == request.Name))
            {
                return BadRequest("Account with provided name already exists");

            }

            var contact = await _dataContext.Contacts.FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

            if (contact is null)
            {
                return NotFound("Contact with provided email does not exist");
            }            

            var result = new Account()
            {
                Name = request.Name
            };

            contact.Accounts.Add(result);
            _dataContext.Accounts.Add(result);
            await _dataContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
