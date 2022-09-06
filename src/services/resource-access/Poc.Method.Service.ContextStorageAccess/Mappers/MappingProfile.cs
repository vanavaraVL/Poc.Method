using AutoMapper;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.Core.Dtos.Companies;
using Poc.Method.Core.Dtos.Persons;
using Poc.Method.Dal.Sql.Entities;

namespace Poc.Method.Service.ContextStorageAccess.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            PersonProfile();
            CompanyProfile();
        }

        private void PersonProfile()
        {
            CreateMap<PersonEntity, PersonDto>()
                .ForMember(dst => dst.CompanyId, opt => opt.MapFrom(src => src.EmployerId));

            CreateMap<PersonCreateRequest, PersonEntity>();
        }

        private void CompanyProfile()
        {
            CreateMap<CompanyEntity, CompanyDto>();
        }
    }
}
