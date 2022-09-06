using Poc.Method.Core.Dtos.Persons;

namespace Poc.Method.Core.Dtos.Companies
{
    public record CompanyInfoDto: CompanyDto
    {
        public IReadOnlyCollection<PersonDto> Persons { get; init; } = new List<PersonDto>();
    }
}
