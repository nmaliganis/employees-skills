using System;
using System.Linq;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Exceptions.Domain.Employees;
using employee.skill.common.infrastructure.Exceptions.Domain.Skills;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Employees;
using employees.skills.model.Employees;
using employees.skills.model.Skills;
using Serilog;

namespace employees.skills.services.Employees
{
  public class UpdateEmployeeProcessor : IUpdateEmployeeProcessor
  {
    private readonly IUnitOfWork _uOf;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IAutoMapper _autoMapper;

    public UpdateEmployeeProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IEmployeeRepository EmployeeRepository, ISkillRepository skillRepository)
    {
      this._uOf = uOf;
      this._employeeRepository = EmployeeRepository;
      this._skillRepository = skillRepository;
      this._autoMapper = autoMapper;
    }

    public Task<EmployeeUiModel> UpdateEmployeeAsync(Guid employeeIdToBeUpdated,
      EmployeeModificationUiModel updatedEmployee)
    {
      var response =
        new EmployeeUiModel()
        {
          Message = "EMPLOYEE"
        };

      if (employeeIdToBeUpdated == Guid.Empty)
      {
        response.Message = "ERROR_INVALID_EMPLOYEE_MODEL";
        return Task.Run(() => response);
      }

      try
      {
        var employeeToBeUpdated = ThrowExceptionIfEmployeeDoesNotExist(employeeIdToBeUpdated);

        employeeToBeUpdated.Firstname = updatedEmployee.Firstname;
        employeeToBeUpdated.Lastname = updatedEmployee.Lastname;
        employeeToBeUpdated.Email = updatedEmployee.Email;
        employeeToBeUpdated.HiredDate = updatedEmployee.HiredDate;

        ThrowExcIfEmployeeCanNotBeUpdated(employeeToBeUpdated);
        ThrowExcIfThisEmployeeAlreadyExist(employeeToBeUpdated);
        
        if (!String.IsNullOrEmpty(updatedEmployee.NonExistingSkill))
        {
          //Todo: Create Skill And Inject It
          var skillToBeCreatedAndInjected = _skillRepository.FindSkillByName(updatedEmployee.NonExistingSkill);

          var employeeSkillToBeInjected = new EmployeeSkill();
                    
          if (skillToBeCreatedAndInjected != null)
          {
            employeeSkillToBeInjected.InjectWithSkill(skillToBeCreatedAndInjected);
          }
          else
          {
            var skillToBeCreated = new Skill
            {
              Name = updatedEmployee.NonExistingSkill,
              Description = $"Auto-Generated SKill : {updatedEmployee.NonExistingSkill}"
            };
            MakeSkillPersistent(skillToBeCreated);
            employeeSkillToBeInjected.InjectWithSkill(skillToBeCreated);
          }
          employeeToBeUpdated.InjectWithSkill(employeeSkillToBeInjected);
        }

        foreach (var skillId in updatedEmployee.ExistingSkillIds)
        {
          var skillToBeInjected = this._skillRepository.FindBy(skillId);

          if (skillToBeInjected == null)
          {
            throw new SkillDoesNotExistException(skillId);
          }

          var employeeSkillToBeInjected = new EmployeeSkill();

          employeeSkillToBeInjected.InjectWithSkill(skillToBeInjected);

          var existingSkill = employeeToBeUpdated.Skills.FirstOrDefault(x => x.Skill.Id == skillId);
          
          if (existingSkill == null)
            employeeToBeUpdated.InjectWithSkill(employeeSkillToBeInjected);
        }

        Log.Information(
          $"Update Employee: Id: {employeeIdToBeUpdated}" + $"Error Message:{response.Message}" +
          "--UpdateRegisterWithEmployeeAsync--  @Ready@ [UpdateEmployeeProcessor]. " +
          "Message: Just Before MakeItPersistence");

        MakeEmployeePersistent(employeeToBeUpdated);

        Log.Information(
          $"Update Employee: Id: {employeeIdToBeUpdated}" + $"Error Message:{response.Message}" +
          "--UpdateRegisterWithEmployeeAsync--  @Ready@ [UpdateEmployeeProcessor]. " +
          "Message: Just After MakeItPersistence");

        response = ThrowExcIfEmployeeWasNotBeMadePersistent(employeeToBeUpdated);
        response.Message = "SUCCESS_MODIFICATION";
        return Task.Run(() => response);
      }
      catch (EmployeeDoesNotExistException e)
      {
        response.Message = "ERROR_EMPLOYEE_NOT_EXIST";
        Log.Error(
          $"Update Employee: {updatedEmployee.Email}" +
          "does not exist -- UpdateEmployee--  @NotComplete@ [UpdateEmployeeProcessor]." +
          $"\nException message:{e.Message}");
      }
      catch (InvalidEmployeeException ex)
      {
        response.Message = "ERROR_INVALID_EMPLOYEE_MODEL";
        Log.Error(
          $"Update Employee: {updatedEmployee.Email}" +
          "--UpdateEmployee--  @NotComplete@ [UpdateEmployeeProcessor]. " +
          $"Broken rules: {ex.BrokenRules}");
      }
      catch (EmployeeDoesNotExistAfterMadePersistentException exx)
      {
        response.Message = "ERROR_EMPLOYEE_NOT_MADE_PERSISTENT";
        Log.Error(
          $"Update Employee: {updatedEmployee.Email}" +
          $"Error Message:{response.Message}" +
          "--UpdateEmployee--  @fail@ [UpdateEmployeeProcessor]." +
          $" @innerfault:{exx?.Message} and {exx?.InnerException}");
      }
      catch (EmployeeAlreadyExistsException exx)
      {
        response.Message = "ERROR_EMPLOYEE_ALREADY_EXISTS";
        Log.Error(
          $"Update Employee: {updatedEmployee.Email}" +
          "already exists --UpdateEmployee--  @NotComplete@ [UpdateEmployeeProcessor]." +
          $"\nException message:{exx.Message}");
      }
      catch (Exception exxx)
      {
        response.Message = "UNKNOWN_ERROR";
        Log.Error(
          $"Update Employee: Id: {employeeIdToBeUpdated}" + $"Error Message:{response.Message}" +
          $"--UpdateRegisterWithEmployeeAsync--  @fail@ [UpdateEmployeeProcessor]. " +
          $"@innerfault:{exxx.Message} and {exxx.InnerException}");
      }

      return Task.Run(() => response);
    }

    private void ThrowExcIfEmployeeCanNotBeUpdated(Employee employeeToBeUpdated)
    {
      var canBeUpdated = !employeeToBeUpdated.GetBrokenRules().Any();
      if (!canBeUpdated)
        throw new InvalidEmployeeException(employeeToBeUpdated.GetBrokenRulesAsString());
    } 
    
    private void ThrowExcIfThisEmployeeAlreadyExist(Employee employeeToBeUpdated)
    {
      var Employee =
        _employeeRepository.FindEmployeeByNameAndEmail(employeeToBeUpdated.Lastname, employeeToBeUpdated.Firstname, employeeToBeUpdated.Email);
      if (Employee != null && Employee.Id != employeeToBeUpdated.Id)
      {
        throw new EmployeeAlreadyExistsException(employeeToBeUpdated.Email);
      }
    }
    
    private Employee ThrowExceptionIfEmployeeDoesNotExist(Guid idEmployee)
    {
      var EmployeeToBeUpdated = _employeeRepository.FindBy(idEmployee);
      if (EmployeeToBeUpdated == null)
        throw new EmployeeDoesNotExistException(idEmployee);
      return EmployeeToBeUpdated;
    }
    
    private EmployeeUiModel ThrowExcIfEmployeeWasNotBeMadePersistent(Employee employeeToBeHaveBeenUpdated)
    {
      var retrievedEmployee =
        _employeeRepository.FindEmployeeByNameAndEmail(employeeToBeHaveBeenUpdated.Lastname, employeeToBeHaveBeenUpdated.Firstname, employeeToBeHaveBeenUpdated.Email);
      if (retrievedEmployee != null)
        return _autoMapper.Map<EmployeeUiModel>(retrievedEmployee);
      throw new EmployeeDoesNotExistAfterMadePersistentException(employeeToBeHaveBeenUpdated.Email);
    }

    private void MakeEmployeePersistent(Employee employeeToBeUpdated)
    {
      _employeeRepository.Save(employeeToBeUpdated);
      _uOf.Commit();
    }
    
    private void MakeSkillPersistent(Skill skillToBeMadePersistence)
    {
      _skillRepository.Save(skillToBeMadePersistence);
      _uOf.Commit();
    }
  }
}