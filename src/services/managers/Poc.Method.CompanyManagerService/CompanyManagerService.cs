using Poc.Method.CompanyManager;
using Poc.Method.CompanyManager.Models;
using Poc.Method.ContextStorageAccess;
using Poc.Method.Core.Dtos.Companies;
using Poc.Method.ExternalAppRedAccess;
using Poc.Method.ExternalAppRedAccess.Models;
using Poc.Method.ExternalAppYellowAccess;
using Poc.Method.ExternalAppYellowAccess.Models;

namespace Poc.Method.CompanyManagerService
{
    public class CompanyManagerService: ICompanyManager
    {
        private readonly IContextStorageAccess _contextStorageAccess;
        private readonly IExternalAppRedAccess _externalAppRedAccess;
        private readonly IExternalAppYellowAccess _externalAppYellowAccess;

        public CompanyManagerService(IContextStorageAccess contextStorageAccess, 
            IExternalAppRedAccess externalAppRedAccess, 
            IExternalAppYellowAccess externalAppYellowAccess)
        {
            _contextStorageAccess = contextStorageAccess ?? throw new ArgumentNullException(nameof(contextStorageAccess));
            _externalAppRedAccess = externalAppRedAccess ?? throw new ArgumentNullException(nameof(externalAppRedAccess));
            _externalAppYellowAccess = externalAppYellowAccess ?? throw new ArgumentNullException(nameof(externalAppYellowAccess));
        }

        public async Task<CompanyInfoResponse<CompanyDto>> GetCompanyList(CompanyListRequest request)
        {
            var result =
                await _contextStorageAccess.GetCompanyList(new ContextStorageAccess.Models.CompanyListRequest());

            return new CompanyInfoResponse<CompanyDto>(result.Items, true);
        }

        public async Task<CompanyInfoDto> GetCompanyDetail(CompanyDetailRequest request)
        {
            var companyDto = await GetCompany(request.Id);

            var employees = await _externalAppRedAccess.GetCompanyEmployees(new CompanyEmployeesRequest()
            {
                CompanyId = request.Id,
                Page = request.PageSize
            });

            return new CompanyInfoDto()
            {
                AdditionInfo = companyDto.AdditionInfo,
                Description = companyDto.Description,
                Id = companyDto.Id,
                Name = companyDto.Name,
                UpdatedAt = companyDto.UpdatedAt,
                Persons = employees.Persons
            };
        }

        public async Task<CompanyInfoDto> CreateCompany(CreateCompanyRequest request)
        {
            var result = await _externalAppYellowAccess.CreateCompany(new CompanyCreateRequest()
            {
                AdditionInfo = request.AdditionInfo,
                Description = request.Description,
                Name = request.Name
            });

            var companyDto = await GetCompany(result.Id);

            return new CompanyInfoDto()
            {
                AdditionInfo = companyDto.AdditionInfo,
                Description = companyDto.Description,
                Id = companyDto.Id,
                Name = companyDto.Name,
                UpdatedAt = companyDto.UpdatedAt
            };
        }

        private async Task<CompanyDto> GetCompany(int id)
        {
            var companyList =
                await _contextStorageAccess.GetCompanyList(new ContextStorageAccess.Models.CompanyListRequest());

            var companyDto = companyList.Items.FirstOrDefault(c => c.Id == id);

            if (companyDto == null)
            {
                throw new Exception("Company not found");
            }

            return companyDto;
        }
    }
}