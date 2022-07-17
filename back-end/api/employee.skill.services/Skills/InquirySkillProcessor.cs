using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Skills;

namespace employees.skills.services.Skills;

public class InquirySkillProcessor : IInquirySkillProcessor
{
    private readonly IAutoMapper _autoMapper;
    private readonly ISkillRepository _skillRepository;
    public InquirySkillProcessor(ISkillRepository skillRepository, IAutoMapper autoMapper)
    {
        this._skillRepository = skillRepository;
        this._autoMapper = autoMapper;
    }

    public Task<SkillUiModel> GetSkillByIdAsync(Guid id)
    {
        return Task.Run(() => _autoMapper.Map<SkillUiModel>(_skillRepository.FindBy(id)));
    }

    public Task<SkillUiModel> GetSkillByNameAsync(string skillName)
    {
        throw new NotImplementedException();
    }
}