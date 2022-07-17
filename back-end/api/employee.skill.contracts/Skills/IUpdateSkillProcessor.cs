using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;

namespace employees.skills.contracts.Skills
{
    public interface IUpdateSkillProcessor
    {
        Task<SkillUiModel> UpdateSkillAsync(Guid skillIdToBeUpdated,
            SkillModificationUiModel updatedSkill);
    }
}