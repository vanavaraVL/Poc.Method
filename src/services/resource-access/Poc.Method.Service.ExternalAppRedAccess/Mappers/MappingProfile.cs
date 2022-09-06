using AutoMapper;
using Poc.Method.Core.Dtos.Persons;

namespace Poc.Method.Service.ExternalAppRedAccess.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            PersonProfile();
        }

        private void PersonProfile()
        {
            CreateMap<EmployeModel, PersonDto>()
                .ForMember(dst => dst.AdditionInfo, opt => opt.MapFrom(src => src.SomeMoreInfo))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Identity))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.EmployeFirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.EmployeLastName))
                .ForMember(dst => dst.UpdatedAt, opt => opt.MapFrom(src => src.EmployeUpdatedAt));
        }
    }
}
