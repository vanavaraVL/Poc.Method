using Microsoft.AspNetCore.Mvc;
using Poc.Method.CompanyManager;
using Poc.Method.CompanyManager.Models;
using Poc.Method.Core.Dtos.Companies;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyManager _companyManager;

        public CompanyController(ICompanyManager companyManager)
        {
            _companyManager = companyManager;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CompanyInfoResponse<CompanyDto>), StatusCodes.Status200OK)]
        [Route("")]
        public Task<CompanyInfoResponse<CompanyDto>> GetCompanyList()
        {
            return _companyManager.GetCompanyList(new CompanyListRequest());
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CompanyInfoDto), StatusCodes.Status200OK)]
        [Route("{id:int}")]
        public Task<CompanyInfoDto> GetCompanyDetail(int id, [FromQuery] int pageSize)
        {
            return _companyManager.GetCompanyDetail(new CompanyDetailRequest()
            {
                Id = id,
                PageSize = pageSize
            });
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CompanyInfoDto), StatusCodes.Status200OK)]
        [Route("add")]
        public Task<CompanyInfoDto> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            return _companyManager.CreateCompany(request);
        }
    }
}
