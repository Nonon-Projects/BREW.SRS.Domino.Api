using BREW.SRS.Domino.Application.Shared.Persons;
using BREW.SRS.Domino.Application.Shared.Persons.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREW.SRS.Domino.Host.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        [HttpGet]
        public async Task<List<PersonDto>> Get()
        {
            var data = await _personService.Get();

            return data;
        }

        [HttpPost("Ensure")]
        public async Task<PersonDto> EnsureAsync(PersonDto personDto)
        {
           return await _personService.Ensure(personDto);
        }

        [HttpDelete("Delete")]
        public async Task<PersonDto> Delete(PersonDto personDto)
        {
            return await _personService.Delete(personDto);
        }
    }
}
