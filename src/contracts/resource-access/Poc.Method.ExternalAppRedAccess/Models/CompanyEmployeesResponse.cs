using Poc.Method.Core.Dtos.Persons;

namespace Poc.Method.ExternalAppRedAccess.Models
{
    public record CompanyEmployeesResponse
    {
        public IReadOnlyCollection<PersonDto> Persons { get; init; } = new List<PersonDto>();
    }
}
