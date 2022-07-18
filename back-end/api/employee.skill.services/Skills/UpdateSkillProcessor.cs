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
using FluentNHibernate.Conventions;
using Serilog;

namespace doc.imagination.services.Skills
{
  public class UpdateSkillProcessor : IUpdateSkillProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly ISkillRepository _skillRepository;
    private readonly IAutoMapper _autoMapper;

    public UpdateSkillProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, ISkillRepository skillRepository)
    {
      _uOf = uOf;
      _skillRepository = skillRepository;
      _autoMapper = autoMapper;
    }

    public Task<SkillUiModel> UpdateSkillAsync(Guid skillIdToBeUpdated,
      SkillModificationUiModel updatedSkill)
    {
      var response =
        new SkillUiModel()
        {
          Message = "START_Skill"
        };

      if (skillIdToBeUpdated == Guid.Empty)
      {
        response.Message = "ERROR_INVALID_Skill_MODEL";
        return Task.Run(() => response);
      }

      try
      {
        var skillToBeUpdated = ThrowExceptionIfSkillDoesNotExist(skillIdToBeUpdated);

        skillToBeUpdated.Name = updatedSkill.Name;
        skillToBeUpdated.Description = updatedSkill.Description;

        ThrowExcIfSkillCanNotBeUpdated(skillToBeUpdated);
        ThrowExcIfThisSkillAlreadyExist(skillToBeUpdated);
        
        Log.Information(
          $"Update Skill: Id: {skillIdToBeUpdated}" + $"Error Message:{response.Message}" +
          "--UpdateRegisterWithSkillAsync--  @Ready@ [UpdateSkillProcessor]. " +
          "Message: Just Before MakeItPersistence");

        MakeSkillPersistent(skillToBeUpdated);

        Log.Information(
          $"Update Skill: Id: {skillIdToBeUpdated}" + $"Error Message:{response.Message}" +
          "--UpdateRegisterWithSkillAsync--  @Ready@ [UpdateSkillProcessor]. " +
          "Message: Just After MakeItPersistence");

        response = ThrowExcIfSkillWasNotBeMadePersistent(skillToBeUpdated);
        response.Message = "SUCCESS_MODIFICATION";
        return Task.Run(() => response);
      }
      catch (SkillDoesNotExistException e)
      {
        response.Message = "ERROR_SKILL_NOT_EXIST";
        Log.Error(
          $"Update Skill: {updatedSkill.Name}" +
          "does not exist -- UpdateSkill--  @NotComplete@ [UpdateSkillProcessor]." +
          $"\nException message:{e.Message}");
      }
      catch (InvalidSkillException ex)
      {
        response.Message = "ERROR_INVALID_SKILL_MODEL";
        Log.Error(
          $"Update Skill: {updatedSkill.Name}" +
          "--UpdateSkill--  @NotComplete@ [UpdateSkillProcessor]. " +
          $"Broken rules: {ex.BrokenRules}");
      }
      catch (SkillDoesNotExistAfterMadePersistentException exx)
      {
        response.Message = "ERROR_SKILL_NOT_MADE_PERSISTENT";
        Log.Error(
          $"Update Skill: {updatedSkill.Name}" +
          $"Error Message:{response.Message}" +
          "--UpdateSkill--  @fail@ [UpdateSkillProcessor]." +
          $" @innerfault:{exx?.Message} and {exx?.InnerException}");
      }
      catch (SkillAlreadyExistsException exx)
      {
        response.Message = "ERROR_SKILL_ALREADY_EXISTS";
        Log.Error(
          $"Update Skill: {updatedSkill.Name}" +
          "already exists --UpdateSkill--  @NotComplete@ [UpdateSkillProcessor]." +
          $"\nException message:{exx.Message}");
      }
      catch (Exception exxx)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Update Skill: Id: {skillIdToBeUpdated}" + $"Error Message:{response.Message}" +
          $"--UpdateRegisterWithSkillAsync--  @fail@ [UpdateSkillProcessor]. " +
          $"@innerfault:{exxx.Message} and {exxx.InnerException}");
      }

      return Task.Run(() => response);
    }

    private void ThrowExcIfSkillCanNotBeUpdated(Skill skillToBeUpdated)
    {
      var canBeUpdated = !skillToBeUpdated.GetBrokenRules().Any();
      if (!canBeUpdated)
        throw new InvalidSkillException(skillToBeUpdated.GetBrokenRulesAsString());
    } 
    
    private void ThrowExcIfThisSkillAlreadyExist(Skill skillToBeUpdated)
    {
      var Skill =
        _skillRepository.FindSkillByName(skillToBeUpdated.Name);
      if (Skill != null && Skill.Id != skillToBeUpdated.Id)
      {
        throw new SkillAlreadyExistsException(skillToBeUpdated.Name);
      }
    }
    
    private Skill ThrowExceptionIfSkillDoesNotExist(Guid idSkill)
    {
      var SkillToBeUpdated = _skillRepository.FindBy(idSkill);
      if (SkillToBeUpdated == null)
        throw new SkillDoesNotExistException(idSkill);
      return SkillToBeUpdated;
    }
    
    private SkillUiModel ThrowExcIfSkillWasNotBeMadePersistent(Skill skillToBeCreated)
    {
      var retrievedSkill =
        _skillRepository.FindSkillByName(skillToBeCreated.Name);
      if (retrievedSkill != null)
        return _autoMapper.Map<SkillUiModel>(retrievedSkill);
      throw new SkillDoesNotExistAfterMadePersistentException(skillToBeCreated.Name);
    }

    private void MakeSkillPersistent(Skill skillToBeUpdated)
    {
      _skillRepository.Save(skillToBeUpdated);
      _uOf.Commit();
    }
  }
}