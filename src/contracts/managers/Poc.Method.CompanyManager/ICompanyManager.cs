using Poc.Method.CompanyManager.Models;
using Poc.Method.Core.Dtos.Companies;

namespace Poc.Method.CompanyManager
{
    public interface ICompanyManager
    {
        Task<CompanyInfoResponse<CompanyDto>> GetCompanyList(CompanyListRequest request);

        Task<CompanyInfoDto> GetCompanyDetail(CompanyDetailRequest request);

        Task<CompanyInfoDto> CreateCompany(CreateCompanyRequest request);
    }
}