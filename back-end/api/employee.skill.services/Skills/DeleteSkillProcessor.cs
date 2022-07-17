using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Skills;

namespace employees.skills.services.Skills;

public class DeleteSkillProcessor : IDeleteSkillProcessor
{
    private readonly IUnitOfWork _uOf;
    private readonly ISkillRepository _skillRepository;

    public DeleteSkillProcessor(IUnitOfWork uOf,
        ISkillRepository skillRepository)
    {
        this._uOf = uOf;
        this._skillRepository = skillRepository;
    }

    public Task DeleteSkillAsync(Guid skillToBeDeletedId)
    {
        throw new NotImplementedException();
    }

    public Task<SkillDeletionUiModel> SoftDeleteSkillAsync(Guid skillToBeDeletedId)
    {
        throw new NotImplementedException();
    }
}