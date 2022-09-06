using Microsoft.AspNetCore.Mvc;
using Poc.Method.Core.Dtos.Persons;
using Poc.Method.PersonManager;
using Poc.Method.PersonManager.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonManager _personManager;

        public PersonController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [Route("add")]
        public Task<PersonDto> CreateCompany([FromBody] CreatePersonRequest request)
        {
            return _personManager.CreatePerson(request);
        }
    }
}
