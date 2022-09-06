using Poc.Method.ContextStorageAccess;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.Core.Dtos.Persons;
using Poc.Method.ExternalAppYellowAccess;
using Poc.Method.ExternalAppYellowAccess.Models;
using Poc.Method.PersonManager;
using Poc.Method.PersonManager.Models;

namespace Poc.Method.PersonManagerService
{
    public class PersonManagerService : IPersonManager
    {
        private readonly IContextStorageAccess _contextStorageAccess;
        private readonly IExternalAppYellowAccess _externalAppYellowAccess;

        public PersonManagerService(IContextStorageAccess contextStorageAccess, IExternalAppYellowAccess externalAppYellowAccess)
        {
            _contextStorageAccess = contextStorageAccess ?? throw new ArgumentNullException(nameof(contextStorageAccess));
            _externalAppYellowAccess = externalAppYellowAccess ?? throw new ArgumentNullException(nameof(externalAppYellowAccess));
        }

        public async Task<PersonDto> CreatePerson(CreatePersonRequest request)
        {
            var result = await _contextStorageAccess.CreatePerson(new PersonCreateRequest()
            {
                AdditionInfo = request.AdditionInfo,
                FirstName = request.FirstName,
                LastName = request.LastName
            });

            await _externalAppYellowAccess.AssignEmployeeToCompany(new CompanyAssignEmployeeRequest()
            {
                CompanyId = request.CompanyId,
                PersonId = result.Id
            });

            var personList = await _contextStorageAccess.GetPersonList(new PersonListRequest());

            var person = personList.Items.FirstOrDefault(p => p.Id == result.Id);

            if (person == null)
            {
                throw new Exception("Person not found");
            }

            return person;
        }
    }
}