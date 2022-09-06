using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Newtonsoft.Json;
using NUnit.Framework;
using Poc.Method.ExternalAppYellowAccess.Models;
using Poc.Method.Service.ExternalAppYellowAccess;
using RichardSzalay.MockHttp;
using Assert = NUnit.Framework.Assert;

namespace Poc.Method.Tests.ExternalAppYellowAccess
{
    public class ExternalAppYellowAccessServiceTest
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls_test(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ExternalAppYellowAccessService).GetConstructors());
        }

        [Test, CustomAutoData]
        public async Task Create_company_should_pass([Frozen] MockHttpMessageHandler client, ExternalAppYellowAccessService service, CompanyCreateRequest request, CreateCompanyResponse appResponse)
        {
            // ARRANGE
            client.When(HttpMethod.Post, "http://localhost/api/Company/add").Respond(HttpStatusCode.OK,
                new StringContent(JsonConvert.SerializeObject(appResponse)));

            // ACT
            var result = await service.CreateCompany(request);

            // ASSERTS
            Assert.NotNull(result);
            Assert.AreNotEqual(0, result.Id);
        }

        [Test, CustomAutoData]
        public async Task Assign_person_to_company_should_pass([Frozen] MockHttpMessageHandler client, ExternalAppYellowAccessService service, CompanyAssignEmployeeRequest request, AssignEmployeeToCompanyResponse appResponse)
        {
            // ARRANGE
            client.When(HttpMethod.Put, "http://localhost/api/Company/*/employees/add/*").Respond(HttpStatusCode.OK,
                new StringContent(JsonConvert.SerializeObject(appResponse)));

            // ACT
            var result = await service.AssignEmployeeToCompany(request);

            // ASSERTS
            Assert.True(result.IsSuccess);
        }
    }
}