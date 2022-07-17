using AutoMapper;
using employee.skill.common.dtos.Vms.Skills;
using employees.skills.model.Skills;

namespace employees.skills.api.Configurations.AutoMappingProfiles.Skills;

/// <summary>
/// Class : SkillEntityToSkillUiAutoMapperProfile
/// </summary>
public class SkillEntityToSkillUiAutoMapperProfile : Profile
{
    /// <summary>
    /// Ctor
    /// </summary>
    public SkillEntityToSkillUiAutoMapperProfile()
    {
        this.ConfigureMapping();
    }

    /// <summary>
    /// Method : ConfigureMapping
    /// </summary>
    public void ConfigureMapping()
    {
        this.CreateMap<Skill, SkillUiModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
            .MaxDepth(1)
            .ReverseMap()
            ;
    }
}