using System;
using System.Linq;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.Exceptions.Domain.Skills;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Skills;
using employees.skills.model.Skills;

namespace employees.skills.services.Skills;

public class UpdateSkillProcessor : IUpdateSkillProcessor
{
    private readonly IUnitOfWork _uOf;
    private readonly ISkillRepository _skillRepository;
    private readonly IAutoMapper _autoMapper;

    public UpdateSkillProcessor(IUnitOfWork uOf,
        IAutoMapper autoMapper,
        ISkillRepository skillRepository)
    {
        this._uOf = uOf;
        this._skillRepository = skillRepository;
        this._autoMapper = autoMapper;
    }


    private void ThrowExcIfSkillCannotBeModified(Skill skillToBeCreated)
    {
        bool canBeCreated = !skillToBeCreated.GetBrokenRules().Any();
        if (!canBeCreated)
        {
            throw new InvalidSkillException(skillToBeCreated.GetBrokenRulesAsString());
        }
    }

    private void MakeSkillPersistent(Skill skillToBeMadePersistence)
    {
        this._skillRepository.Save(skillToBeMadePersistence);
        this._uOf.Commit();
    }

    public Task<SkillUiModel> UpdateSkillAsync(Guid skillIdToBeUpdated, SkillModificationUiModel updatedSkill)
    {
        throw new NotImplementedException();
    }
}