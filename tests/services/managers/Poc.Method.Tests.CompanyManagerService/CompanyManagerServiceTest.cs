using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using Poc.Method.CompanyManager.Models;
using Poc.Method.ContextStorageAccess;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.ExternalAppRedAccess;
using Poc.Method.ExternalAppRedAccess.Models;
using Poc.Method.ExternalAppYellowAccess;
using Poc.Method.ExternalAppYellowAccess.Models;
using Assert = NUnit.Framework.Assert;
using CompanyListRequest = Poc.Method.CompanyManager.Models.CompanyListRequest;

namespace Poc.Method.Tests.CompanyManagerService
{
    public class CompanyManagerServiceTest
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls_test(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Method.CompanyManagerService.CompanyManagerService).GetConstructors());
        }

        [Test, CustomAutoData]
        public async Task Get_company_list_should_pass([Frozen] IContextStorageAccess contextStorageAccess, 
            Method.CompanyManagerService.CompanyManagerService service,
            CompanyListRequest request,
            CompanyListResponse response)
        {
            // ARRANGE
            Mock.Get(contextStorageAccess)
                .Setup(c => c.GetCompanyList(It.IsAny<ContextStorageAccess.Models.CompanyListRequest>()))
                .ReturnsAsync(response);

            // ACT
            var result = await service.GetCompanyList(request);

            // ASSERTS
            Assert.AreEqual(result.Data.Count, response.Items.Count);
        }

        [Test, CustomAutoData]
        public async Task Get_company_detail_should_pass([Frozen] IContextStorageAccess contextStorageAccess, [Frozen] IExternalAppRedAccess externalAppRedAccess,
            Method.CompanyManagerService.CompanyManagerService service,
            CompanyDetailRequest request,
            CompanyListResponse contextStorageCompanyResponse,
            CompanyEmployeesResponse appEmployeesResponse)
        {
            // ARRANGE
            Mock.Get(contextStorageAccess)
                .Setup(c => c.GetCompanyList(It.IsAny<ContextStorageAccess.Models.CompanyListRequest>()))
                .ReturnsAsync(contextStorageCompanyResponse);

            Mock.Get(externalAppRedAccess)
                .Setup(c => c.GetCompanyEmployees(It.IsAny<CompanyEmployeesRequest>()))
                .ReturnsAsync(appEmployeesResponse);

            request = request with { Id = contextStorageCompanyResponse.Items.First().Id, PageSize = 10 };

            // ACT
            var result = await service.GetCompanyDetail(request);

            // ASSERTS
            Assert.AreEqual(result.Id, contextStorageCompanyResponse.Items.First().Id);
            Assert.AreEqual(result.Name, contextStorageCompanyResponse.Items.First().Name);
            Assert.Greater(result.Persons.Count, 0);
        }

        [Test, CustomAutoData]
        public async Task Create_company_should_pass([Frozen] IContextStorageAccess contextStorageAccess, [Frozen] IExternalAppYellowAccess externalAppYellowAccess,
            Method.CompanyManagerService.CompanyManagerService service,
            CreateCompanyRequest request,
            CompanyListResponse contextStorageCompanyResponse,
            CompanyCreateResponse companyCreateResponse)
        {
            // ARRANGE
            Mock.Get(contextStorageAccess)
                .Setup(c => c.GetCompanyList(It.IsAny<ContextStorageAccess.Models.CompanyListRequest>()))
                .ReturnsAsync(contextStorageCompanyResponse);

            companyCreateResponse = companyCreateResponse with { Id = contextStorageCompanyResponse.Items.First().Id };

            Mock.Get(externalAppYellowAccess)
                .Setup(c => c.CreateCompany(It.IsAny<CompanyCreateRequest>()))
                .ReturnsAsync(companyCreateResponse);

            // ACT
            var result = await service.CreateCompany(request);

            // ASSERTS
            Assert.AreEqual(result.Id, contextStorageCompanyResponse.Items.First().Id);
            Assert.AreEqual(result.Name, contextStorageCompanyResponse.Items.First().Name);
        }
    }
}