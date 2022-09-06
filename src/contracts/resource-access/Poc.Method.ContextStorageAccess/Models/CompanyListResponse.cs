using Poc.Method.Core.Dtos.Companies;

namespace Poc.Method.ContextStorageAccess.Models
{
    public record CompanyListResponse
    {
        public IReadOnlyCollection<CompanyDto> Items { get; init; } = Array.Empty<CompanyDto>();
    }
}
