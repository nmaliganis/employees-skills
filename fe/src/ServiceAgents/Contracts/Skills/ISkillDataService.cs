using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using employee.skill.fe.Models.DTOs.Skills;

namespace employee.skill.fe.ServiceAgents.Contracts.Skills
{
    public interface ISkillDataService
    {
        Task<List<SkillDto>> GetSkillList(string authorizationToken = null);
        Task<SkillDto> GetSkill(Guid actionSkillId);
        Task<SkillDto> CreateSkill(SkillForCreationDto SkillToBeCreated);
        Task<SkillDto> UpdateSkill(Guid SkillIdToBeUpdated, SkillForModificationDto SkillToBeUpdated);
        Task<SkillDto> DeleteSkill(Guid SkillIdToBeDeleted);
    }
}