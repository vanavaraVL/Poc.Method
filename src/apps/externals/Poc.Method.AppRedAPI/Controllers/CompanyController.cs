using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poc.Method.AppRedAPI.Models;
using Poc.Method.Dal.Sql;

namespace Poc.Method.AppRedAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly StorageContext _dbContext;

        public CompanyController(ILogger<CompanyController> logger, StorageContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetEmployeesInCompanyResponse), StatusCodes.Status200OK)]
        [EnableCors]
        [Route("{id:int}")]
        public async Task<GetEmployeesInCompanyResponse> GetAllEmployeesInCompany(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            using var scope = _logger.BeginScope("Get employees : {Target}", id);

            var companyDb = await _dbContext.Companies.Include(c => c.Employees)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (companyDb == null)
            {
                return new GetEmployeesInCompanyResponse() { CompanyId = id, IsSuccess = false};
            }

            return new GetEmployeesInCompanyResponse()
            {
                CompanyId = id,
                IsSuccess = true,
                Employees = companyDb.Employees.Select(e => new EmployeModel()
                {
                    EmployeFirstName = e.FirstName,
                    EmployeLastName = e.LastName,
                    EmployeUpdatedAt = e.UpdatedAt,
                    Identity = e.Id,
                    SomeMoreInfo = e.AdditionInfo
                }).ToList()
            };
        }
    }
}