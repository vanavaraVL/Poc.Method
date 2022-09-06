using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poc.Method.ContextStorageAccess;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.Core.Dtos.Companies;
using Poc.Method.Core.Dtos.Persons;
using Poc.Method.Dal.Sql;
using Poc.Method.Dal.Sql.Entities;

namespace Poc.Method.Service.ContextStorageAccess
{
    public class ContextStorageAccessService: IContextStorageAccess
    {
        private readonly StorageContext _dbContext;
        private readonly IMapper _mapper;

        public ContextStorageAccessService(StorageContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<CompanyListResponse> GetCompanyList(CompanyListRequest request)
        {
            var companyList = await _dbContext.Companies.ToListAsync();

            return new CompanyListResponse()
            {
                Items = _mapper.Map<ICollection<CompanyDto>>(companyList).ToArray()
            };
        }

        public async Task<PersonCreateResponse> CreatePerson(PersonCreateRequest request)
        {
            var entity = _mapper.Map<PersonEntity>(request);

            await _dbContext.Persons.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return new PersonCreateResponse() { Id = entity.Id };
        }

        public async Task<PersonListResponse> GetPersonList(PersonListRequest request)
        {
            var personList = await _dbContext.Persons.ToListAsync();

            return new PersonListResponse()
            {
                Items = _mapper.Map<ICollection<PersonDto>>(personList).ToArray()
            };
        }
    }
}