using Poc.Method.ExternalAppYellowAccess.Models;

namespace Poc.Method.ExternalAppYellowAccess
{
    public interface IExternalAppYellowAccess
    {
        Task<CompanyCreateResponse> CreateCompany(CompanyCreateRequest request);

        Task<CompanyAssignEmployeeResponse> AssignEmployeeToCompany(CompanyAssignEmployeeRequest request);
    }
}
