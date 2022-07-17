using AutoMapper;
using employee.skill.common.dtos.Vms.Employees;
using employees.skills.model.Employees;

namespace employees.skills.api.Configurations.AutoMappingProfiles.Employees
{
    /// <summary>
    /// Class : EmployeeEntityToEmployeeUiAutoMapperProfile
    /// </summary>
    public class EmployeeEntityToEmployeeUiAutoMapperProfile : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public EmployeeEntityToEmployeeUiAutoMapperProfile()
        {
            this.ConfigureMapping();
        }

        /// <summary>
        /// Method : ConfigureMapping
        /// </summary>
        public void ConfigureMapping()
        {
            this.CreateMap<Employee, EmployeeUiModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .MaxDepth(1)
                .ReverseMap()
                ;
        }
    }
}