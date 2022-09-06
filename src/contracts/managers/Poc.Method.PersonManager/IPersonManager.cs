using Poc.Method.Core.Dtos.Persons;
using Poc.Method.PersonManager.Models;

namespace Poc.Method.PersonManager
{
    public interface IPersonManager
    {
        Task<PersonDto> CreatePerson(CreatePersonRequest request);
    }
}