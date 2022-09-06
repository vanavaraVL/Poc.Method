using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Poc.Method.AppYellowAPI.Models;
using Poc.Method.Dal.Sql;
using Poc.Method.Dal.Sql.Entities;

namespace Poc.Method.AppYellowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly StorageContext _dbContext;

        public CompanyController(ILogger<CompanyController> logger, StorageContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateCompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EnableCors]
        [Route("add")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand createCompanyCommand)
        {
            using var scope = _logger.BeginScope("Create company: {Target}", createCompanyCommand);

            if (string.IsNullOrEmpty(createCompanyCommand?.Name))
            {
                throw new BadHttpRequestException("Name is null");
            }

            var newItem = new CompanyEntity()
            {
                AdditionInfo = createCompanyCommand.AdditionInfo,
                Description = createCompanyCommand.Description,
                Name = createCompanyCommand.Name
            };

            await _dbContext.Companies.AddAsync(newItem);
            await _dbContext.SaveChangesAsync();

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new CreateCompanyResponse() { Id = newItem.Id }),
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AssignEmployeeToCompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EnableCors]
        [Route("{companyId:int}/employees/add/{personId:int}")]
        public async Task<IActionResult> AssignEmployeeToCompany(int companyId, int personId)
        {
            using var scope =
                _logger.BeginScope("Assign employee to employer: {Target}, {EmployeId}", companyId, personId);
            
            var companyDb =
                await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
            var personDb =
                await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == personId);

            if (personDb is null)
            {
                throw new BadHttpRequestException("Person doesn't exist in the database");
            }

            if (companyDb is null)
            {
                throw new BadHttpRequestException("Company doesn't exist in the database");
            }

            companyDb.Employees.Add(personDb);
            personDb.EmployerId = companyDb.Id;

            await _dbContext.SaveChangesAsync();

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new AssignEmployeeToCompanyResponse() { IsSuccess = true}),
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
