using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;

namespace employees.skills.contracts.Skills;

public interface IInquirySkillProcessor
{
    Task<SkillUiModel> GetSkillByIdAsync(Guid id);
    Task<SkillUiModel> GetSkillByNameAsync(string skillName);
}