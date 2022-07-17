using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;

namespace employees.skills.contracts.Skills;

public interface IDeleteSkillProcessor
{
    Task DeleteSkillAsync(Guid skillToBeDeletedId);
    Task<SkillDeletionUiModel> SoftDeleteSkillAsync(Guid skillToBeDeletedId);
}