using Poc.Method.ExternalAppRedAccess.Models;

namespace Poc.Method.ExternalAppRedAccess
{
    public interface IExternalAppRedAccess
    {
        Task<CompanyEmployeesResponse> GetCompanyEmployees(CompanyEmployeesRequest request);
    }
}
