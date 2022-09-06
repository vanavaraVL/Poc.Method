using Poc.Method.Core.Dtos.Persons;

namespace Poc.Method.ContextStorageAccess.Models
{
    public record PersonListResponse
    {
        public IReadOnlyCollection<PersonDto> Items { get; init; } = Array.Empty<PersonDto>();
    }
}
