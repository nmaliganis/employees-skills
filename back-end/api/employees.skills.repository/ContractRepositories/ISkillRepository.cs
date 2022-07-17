using System;
using employee.skill.common.infrastructure.Domain;
using employee.skill.common.infrastructure.Domain.Queries;

namespace employee.skill.repository.ContractRepositories
{
    public interface ISkillRepository : IRepository<employees.skills.model.Skills.Skill, Guid>
    {
        QueryResult<employees.skills.model.Skills.Skill> FindAllActiveSkillsPagedOf(int? pageNum, int? pageSize);
        employees.skills.model.Skills.Skill FindSkillByName(string name);
    }
}