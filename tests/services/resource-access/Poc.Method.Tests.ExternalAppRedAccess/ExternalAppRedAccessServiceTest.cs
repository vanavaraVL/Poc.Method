using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Newtonsoft.Json;
using NUnit.Framework;
using Poc.Method.ExternalAppRedAccess.Models;
using Poc.Method.Service.ExternalAppRedAccess;
using RichardSzalay.MockHttp;
using Assert = NUnit.Framework.Assert;

namespace Poc.Method.Tests.ExternalAppRedAccess
{
    public class ExternalAppRedAccessServiceTest
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls_test(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ExternalAppRedAccessService).GetConstructors());
        }

        [Test, CustomAutoData]
        public async Task Get_persons_from_company_should_pass([Frozen] MockHttpMessageHandler client, ExternalAppRedAccessService service, CompanyEmployeesRequest request, GetEmployeesInCompanyResponse expectedResult)
        {
            // ARRANGE
            client.When(HttpMethod.Get, "http://localhost/api/Company/*").Respond(HttpStatusCode.OK,
                new StringContent(JsonConvert.SerializeObject(expectedResult)));

            // ACT
            var result = await service.GetCompanyEmployees(request);

            // ASSERTS
            Assert.AreEqual(expectedResult.Employees.Count(), result.Persons.Count);

            foreach (var employeAppModel in expectedResult.Employees)
            {
                var personDto = result.Persons.Single(p => p.Id == employeAppModel.Identity);

                Assert.AreEqual(employeAppModel.EmployeLastName, personDto.LastName);
                Assert.AreEqual(employeAppModel.EmployeFirstName, personDto.FirstName);
                Assert.AreEqual(employeAppModel.SomeMoreInfo, personDto.AdditionInfo);
            }
        }
    }
}