using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using Poc.Method.ContextStorageAccess;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.ExternalAppYellowAccess;
using Poc.Method.ExternalAppYellowAccess.Models;
using Poc.Method.PersonManager.Models;
using Assert = NUnit.Framework.Assert;

namespace Poc.Method.Tests.PersonManagerService
{
    public class PersonManagerServiceTest
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls_test(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Method.PersonManagerService.PersonManagerService).GetConstructors());
        }

        [Test, CustomAutoData]
        public async Task Create_person_should_pass([Frozen] IContextStorageAccess contextStorageAccess,
            [Frozen] IExternalAppYellowAccess externalAppYellowAccess,
            Method.PersonManagerService.PersonManagerService service,
            CreatePersonRequest request,
            PersonCreateResponse createPersonResponse,
            PersonListResponse personListResponse)
        {
            // ARRANGE
            Mock.Get(contextStorageAccess)
                .Setup(c => c.GetPersonList(It.IsAny<PersonListRequest>()))
                .ReturnsAsync(personListResponse);

            createPersonResponse = createPersonResponse with { Id = personListResponse.Items.First().Id };

            Mock.Get(contextStorageAccess)
                .Setup(c => c.CreatePerson(It.IsAny<PersonCreateRequest>()))
                .ReturnsAsync(createPersonResponse);

            Mock.Get(externalAppYellowAccess)
                .Setup(c => c.AssignEmployeeToCompany(It.IsAny<CompanyAssignEmployeeRequest>()))
                .ReturnsAsync(new CompanyAssignEmployeeResponse() { IsSuccess = true });

            // ACT
            var result = await service.CreatePerson(request);

            // ASSERTS
            Assert.AreEqual(result.Id, personListResponse.Items.First().Id);
            Assert.AreEqual(result.FirstName, personListResponse.Items.First().FirstName);
        }
    }
}