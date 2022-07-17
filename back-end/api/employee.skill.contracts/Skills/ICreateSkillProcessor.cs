using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;

namespace employees.skills.contracts.Skills;

public interface ICreateSkillProcessor
{
    Task<SkillUiModel> CreateSkillAsync(SkillCreationUiModel newSkillUiModel);
}