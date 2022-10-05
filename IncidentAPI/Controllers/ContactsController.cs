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
    public class ContactsController: ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<ContactDto> _validator;

        public ContactsController(DataContext dataContext, IValidator<ContactDto> validator)
        {
            _dataContext = dataContext;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDto request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            if (await _dataContext.Contacts.AnyAsync(c => c.Email == request.Email))
            {
                return BadRequest("Contact with provided email address already exists");
                
            }

            var result = new Contact()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            await _dataContext.AddAsync(result);
            await _dataContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
