using System;
using System.Threading.Tasks;
using employee.skill.common.infrastructure.Helpers.ResourceParameters.Skills;
using employee.skill.common.infrastructure.Paging;
using employees.skills.model.Skills;

namespace employees.skills.contracts.Skills;

public interface IInquiryAllSkillsProcessor {
    Task<PagedList<Skill>> GetSkillsAsync(SkillsResourceParameters SkillsResourceParameters);
}