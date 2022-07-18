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
using Serilog;

namespace employees.skills.services.Skills;

public class CreateSkillProcessor : ICreateSkillProcessor
{
    private readonly IUnitOfWork _uOf;
    private readonly ISkillRepository _skillRepository;
    private readonly IAutoMapper _autoMapper;

    public CreateSkillProcessor(IUnitOfWork uOf, IAutoMapper autoMapper,
        ISkillRepository SkillRepository)
    {
        this._uOf = uOf;
        this._skillRepository = SkillRepository;
        this._autoMapper = autoMapper;
    }

    public Task<SkillUiModel> CreateSkillAsync(SkillCreationUiModel newSkillUiModel)
    {
        var response =
            new SkillUiModel()
            {
                Message = "START_CREATION"
            };

        if (newSkillUiModel == null)
        {
            response.Message = "ERROR_INVALID_SKILL_MODEL";
            return Task.Run(() => response);
        }

        try
        {
            var skillToBeCreated = new Skill();

            skillToBeCreated.InjectWithInitialAttributes(newSkillUiModel);

            ThrowExcIfSkillCannotBeCreated(skillToBeCreated);
            ThrowExcIfThisSkillAlreadyExist(skillToBeCreated);

            Log.Debug(
                $"Create Skill: {newSkillUiModel.Name}" +
                "--CreateSkill--  @NotComplete@ [CreateSkillProcessor]. " +
                "Message: Just Before MakeItPersistence");

            MakeSkillPersistent(skillToBeCreated);

            Log.Debug(
                $"Create Skill: {newSkillUiModel.Name}" +
                "--CreateSkill--  @NotComplete@ [CreateSkillProcessor]. " +
                "Message: Just After MakeItPersistence");
            response = ThrowExcIfSkillWasNotBeMadePersistent(skillToBeCreated);
            response.Message = "SUCCESS_CREATION";
        }
        catch (InvalidSkillException e)
        {
            response.Message = "ERROR_INVALID_SKILL_MODEL";
            Log.Error(
                $"Create Skill: {newSkillUiModel.Name}" +
                $"Error Message:{response.Message}" +
                "--CreateSkill--  @NotComplete@ [CreateSkillProcessor]. " +
                $"Broken rules: {e.BrokenRules}");
        }
        catch (SkillAlreadyExistsException ex)
        {
            response.Message = "ERROR_SKILL_ALREADY_EXISTS";
            Log.Error(
                $"Create Skill: {newSkillUiModel.Name}" +
                $"Error Message:{response.Message}" +
                "--CreateSkill--  @fail@ [CreateSkillProcessor]. " +
                $"@innerfault:{ex?.Message} and {ex?.InnerException}");
        }
        catch (SkillDoesNotExistAfterMadePersistentException exx)
        {
            response.Message = "ERROR_SKILL_NOT_MADE_PERSISTENT";
            Log.Error(
                $"Create Skill: {newSkillUiModel.Name}" +
                $"Error Message:{response.Message}" +
                "--CreateSkill--  @fail@ [CreateSkillProcessor]." +
                $" @innerfault:{exx?.Message} and {exx?.InnerException}");
        }
        catch (Exception exxx)
        {
            response.Message = "UNKNOWN_ERROR";
            Log.Error(
                $"Create Skill: {newSkillUiModel.Name}" +
                $"Error Message:{response.Message}" +
                $"--CreateSkill--  @fail@ [CreateSkillProcessor]. " +
                $"@innerfault:{exxx.Message} and {exxx.InnerException}");
        }

        return Task.Run(() => response);
    }

    private void ThrowExcIfThisSkillAlreadyExist(Skill skillToBeCreated)
    {
        var skillRetrieved = _skillRepository.FindSkillByName(skillToBeCreated.Name);
        if (skillRetrieved != null)
        {
            throw new SkillAlreadyExistsException(skillToBeCreated.Name,
                skillToBeCreated.GetBrokenRulesAsString());
        }
    }

    private SkillUiModel ThrowExcIfSkillWasNotBeMadePersistent(Skill skillToBeCreated)
    {
        var retrievedSkill = _skillRepository.FindSkillByName(skillToBeCreated.Name);
        if (retrievedSkill  != null)
            return _autoMapper.Map<SkillUiModel>(retrievedSkill);
        throw new SkillDoesNotExistAfterMadePersistentException(skillToBeCreated.Name);
    }

    private void ThrowExcIfSkillCannotBeCreated(Skill skillToBeCreated)
    {
        bool canBeCreated = !skillToBeCreated.GetBrokenRules().Any();
        if (!canBeCreated)
            throw new InvalidSkillException(skillToBeCreated.GetBrokenRulesAsString());
    }

    private void MakeSkillPersistent(Skill skillToBeMadePersistence)
    {
        _skillRepository.Save(skillToBeMadePersistence);
        _uOf.Commit();
    }
}