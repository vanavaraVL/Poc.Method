using Poc.Method.ContextStorageAccess.Models;

namespace Poc.Method.ContextStorageAccess
{
    public interface IContextStorageAccess
    {
        Task<CompanyListResponse> GetCompanyList(CompanyListRequest request);

        Task<PersonCreateResponse> CreatePerson(PersonCreateRequest request);

        Task<PersonListResponse> GetPersonList(PersonListRequest request);
    }
}