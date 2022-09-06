using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using NUnit.Framework;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.Dal.Sql;
using Poc.Method.Dal.Sql.Entities;
using Poc.Method.Service.ContextStorageAccess;
using Assert = NUnit.Framework.Assert;

namespace Poc.Method.Tests.ContextStorageAccess
{
    public class ContextStorageAccessServiceTest
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls_test(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ContextStorageAccessService).GetConstructors());
        }

        [Test, CustomAutoData]
        public async Task Get_company_list_should_pass([Frozen] StorageContext dbContext,
            ContextStorageAccessService contextAccessService, 
            IFixture fixture)
        {
            // ARRANGE
            var companyList = fixture.CreateMany<CompanyEntity>(10).ToArray();

            await dbContext.Companies.AddRangeAsync(companyList);
            await dbContext.SaveChangesAsync();

            // ACT
            var result = await contextAccessService.GetCompanyList(new CompanyListRequest());

            // ASSERTS
            Assert.AreEqual(result.Items.Count, companyList.Length);
        }

        [Test, CustomAutoData]
        public async Task Create_person_should_pass([Frozen] StorageContext dbContext, ContextStorageAccessService contextAccessService, IFixture fixture)
        {
            // ARRANGE
            var personDb = fixture.Create<PersonEntity>();
            var personRequest = fixture.Create<PersonCreateRequest>();

            await dbContext.Persons.AddAsync(personDb);
            await dbContext.SaveChangesAsync();

            // ACT
            var result = await contextAccessService.CreatePerson(personRequest);

            // ASSERTS
            Assert.AreNotEqual(0, result.Id);
            Assert.AreEqual(2, dbContext.Persons.Count());
        }

        [Test, CustomAutoData]
        public async Task Get_person_list_should_pass([Frozen] StorageContext dbContext, ContextStorageAccessService contextAccessService, IFixture fixture)
        {
            // ARRANGE
            var personList = fixture.CreateMany<PersonEntity>(10).ToArray();

            await dbContext.Persons.AddRangeAsync(personList);
            await dbContext.SaveChangesAsync();

            // ACT
            var result = await contextAccessService.GetPersonList(new PersonListRequest());

            // ASSERTS
            Assert.AreEqual(result.Items.Count, personList.Length);
        }
    }
}