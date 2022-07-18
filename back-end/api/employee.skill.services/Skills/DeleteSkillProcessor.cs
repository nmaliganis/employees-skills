using System;
using System.Threading.Tasks;
using dl.wm.suite.common.infrastructure.Exceptions.Domain.Skills;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.Exceptions.Domain.Skills;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Skills;
using employees.skills.model.Skills;
using Serilog;

namespace employees.skills.services.Skills
{
    public class DeleteSkillProcessor : IDeleteSkillProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly ISkillRepository _skillRepository;
        private readonly IAutoMapper _autoMapper;

        public DeleteSkillProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, ISkillRepository SkillRepository)
        {
            _uOf = uOf;
            _skillRepository = SkillRepository;
            _autoMapper = autoMapper;
        }


        public Task<SkillDeletionUiModel> SoftDeleteSkillAsync(Guid skillToBeDeletedId)
        {
            var response =
                new SkillDeletionUiModel()
                {
                    Message = "START_DELETION"
                };

            if (skillToBeDeletedId == Guid.Empty)
            {
                response.Message = "ERROR_INVALID_SKILL_ID";
                return Task.Run(() => response);
            }

            try
            {
                var skillToBeSoftDeleted = _skillRepository.FindBy(skillToBeDeletedId);

                if (skillToBeSoftDeleted == null)
                    throw new SkillDoesNotExistException(skillToBeDeletedId);

                skillToBeSoftDeleted.SoftDeleted();

                Log.Debug(
                    $"Update-Delete Skill: with Id: {skillToBeDeletedId}" +
                    "--SoftDeleteSkill--  @Ready@ [DeleteSkillProcessor]. " +
                    "Message: Just Before MakeItPersistence");

                MakeSkillPersistent(skillToBeSoftDeleted);

                Log.Debug(
                    $"Update-Delete Skill: with Id: {skillToBeDeletedId}" +
                    "--SoftDeleteSkill--  @Ready@ [DeleteSkillProcessor]. " +
                    "Message: Just After MakeItPersistence");

                response = ThrowExcIfSkillWasNotBeMadePersistent(skillToBeSoftDeleted);
                response.Message = "SUCCESS_DELETION";
            }
            catch (SkillDoesNotExistException e)
            {
                response.Message = "ERROR_SKILL_DOES_NOT_EXIST";
                Log.Error(
                    $"Update-Delete Skill: Id: {skillToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--SoftDeleteSkill--  @NotComplete@ [DeleteSkillProcessor]. " +
                    $"@innerfault:{e?.Message} and {e?.InnerException}");
            }
            catch (SkillDoesNotExistAfterMadePersistentException ex)
            {
                response.Message = "ERROR_SKILL_DOES_NOT_MADE_PERSISTENCE";
                Log.Error(
                    $"Update-Delete Skill: Id: {skillToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--SoftDeleteSkill--  @NotComplete@ [DeleteSkillProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (Exception exx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Update-Delete Skill: Id: {skillToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    $"--SoftDeleteSkill--  @fail@ [DeleteSkillProcessor]. " +
                    $"@innerfault:{exx.Message} and {exx.InnerException}");
            }

            return Task.Run(() => response);
        }


        public Task<SkillDeletionUiModel> HardDeleteSkillAsync(Guid skillToBeDeletedId)
        {
            var response =
                new SkillDeletionUiModel()
                {
                    Message = "START_HARD_DELETION"
                };

            if (skillToBeDeletedId == Guid.Empty)
            {
                response.Message = "ERROR_INVALID_SKILL_ID";
                return Task.Run(() => response);
            }

            try
            {
                var SkillToBeHardDeleted = _skillRepository.FindBy(skillToBeDeletedId);

                if (SkillToBeHardDeleted == null)
                    throw new SkillDoesNotExistException(skillToBeDeletedId);

                Log.Debug(
                    $"Update-Delete Skill: with Id: {skillToBeDeletedId}" +
                    "--HardDeleteSkill--  @Ready@ [DeleteSkillProcessor]. " +
                    "Message: Just Before MakeItPersistence");

                MakeSkillTransient(SkillToBeHardDeleted);

                Log.Debug(
                    $"Update-Delete Skill: with Id: {skillToBeDeletedId}" +
                    "--HardDeleteSkill--  @Ready@ [DeleteSkillProcessor]. " +
                    "Message: Just After MakeItPersistence");

                response.DeletionStatus  = ThrowExcIfSkillWasNotBeMadeTransient(SkillToBeHardDeleted);
                response.Message = "SUCCESS_DELETION";

            }
            catch (SkillDoesNotExistException e)
            {
                response.Message = "ERROR_SKILL_DOES_NOT_EXIST";
                Log.Error(
                    $"Delete Skill: Id: {skillToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--HardDeleteSkill--  @NotComplete@ [DeleteSkillProcessor]. " +
                    $"@innerfault:{e?.Message} and {e?.InnerException}");
            }
            catch (SkillDoesNotExistAfterMadeTransientException ex)
            {
                response.Message = "ERROR_SKILL_DOES_NOT_MADE_TRANSIENT";
                Log.Error(
                    $"Delete Skill: Id: {skillToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--HardDeleteSkill--  @NotComplete@ [DeleteSkillProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (Exception exxx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Delete Skill: Id: {skillToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    $"--HardDeleteSkill--  @fail@ [DeleteSkillProcessor]. " +
                    $"@innerfault:{exxx.Message} and {exxx.InnerException}");
            }

            return Task.Run(() => response);
        }

        private SkillDeletionUiModel ThrowExcIfSkillWasNotBeMadePersistent(Skill skillToBeSoftDeleted)
        {
            var retrievedSkill =
                _skillRepository.FindBy(skillToBeSoftDeleted.Id);
            if (retrievedSkill != null)
                return _autoMapper.Map<SkillDeletionUiModel>(retrievedSkill);
            throw new SkillDoesNotExistAfterMadePersistentException(skillToBeSoftDeleted.Id);
        }

        private bool ThrowExcIfSkillWasNotBeMadeTransient(Skill skillToBeSoftDeleted)
        {
            var retrievedSkill =
                _skillRepository.FindBy(skillToBeSoftDeleted.Id);
            return retrievedSkill != null
                ? throw new SkillDoesNotExistAfterMadePersistentException(skillToBeSoftDeleted.Id)
                : true;
        }

        private void MakeSkillTransient(Skill skillToBeSoftDeleted)
        {
            _skillRepository.Remove(skillToBeSoftDeleted);
            _uOf.Commit();
        }
        
        private void MakeSkillPersistent(Skill skillToBeUpdated)
        {
            _skillRepository.Save(skillToBeUpdated);
            _uOf.Commit();
        }
    }
}
