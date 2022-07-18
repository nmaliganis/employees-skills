using System;
using System.Linq;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Exceptions.Domain.Employees;
using employee.skill.common.infrastructure.Exceptions.Domain.Skills;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.Employees.contracts.Employees;
using employees.skills.model.Employees;
using employees.skills.model.Skills;
using Serilog;

namespace employees.skills.services.Employees
{
    public class CreateEmployeeProcessor : ICreateEmployeeProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IAutoMapper _autoMapper;

        public CreateEmployeeProcessor(IUnitOfWork uOf, IAutoMapper autoMapper,
            IEmployeeRepository employeeRepository, ISkillRepository skillRepository)
        {
            this._uOf = uOf;
            this._employeeRepository = employeeRepository;
            this._skillRepository = skillRepository;
            this._autoMapper = autoMapper;
        }

        public Task<EmployeeUiModel> CreateEmployeeAsync(EmployeeCreationUiModel newEmployeeUiModel)
        {
            var response =
                new EmployeeUiModel()
                {
                    Message = "START_CREATION"
                };

            if (newEmployeeUiModel == null)
            {
                response.Message = "ERROR_INVALID_EMPLOYEE_MODEL";
                return Task.Run(() => response);
            }

            try
            {
                var employeeToBeCreated = new Employee();

                employeeToBeCreated.InjectWithInitialAttributes(newEmployeeUiModel);

                ThrowExcIfEmployeeCannotBeCreated(employeeToBeCreated);
                ThrowExcIfThisEmployeeAlreadyExist(employeeToBeCreated);

                if (!String.IsNullOrEmpty(newEmployeeUiModel.NonExistingSkill))
                {
                    //Todo: Create Skill And Inject It
                    var skillToBeCreatedAndInjected = _skillRepository.FindSkillByName(newEmployeeUiModel.NonExistingSkill);

                    var employeeSkillToBeInjected = new EmployeeSkill();
                    
                    if (skillToBeCreatedAndInjected != null)
                    {
                        employeeSkillToBeInjected.InjectWithSkill(skillToBeCreatedAndInjected);
                    }
                    else
                    {
                        var skillToBeCreated = new Skill
                        {
                            Name = newEmployeeUiModel.NonExistingSkill,
                            Description = $"Auto-Generated SKill : {newEmployeeUiModel.NonExistingSkill}"
                        };
                        MakeSkillPersistent(skillToBeCreated);
                        employeeSkillToBeInjected.InjectWithSkill(skillToBeCreated);
                    }
                    employeeToBeCreated.InjectWithSkill(employeeSkillToBeInjected);
                }
                
                foreach (var skillId in newEmployeeUiModel.ExistingSkillIds) {
                    var skillToBeInjected = this._skillRepository.FindBy(skillId);

                    if (skillToBeInjected == null) {
                        throw new SkillDoesNotExistException(skillId);
                    }

                    var employeeSkillToBeInjected = new EmployeeSkill();
                    employeeSkillToBeInjected.InjectWithSkill(skillToBeInjected);
                    employeeToBeCreated.InjectWithSkill(employeeSkillToBeInjected);
                }
                
                Log.Debug(
                    $"Create Employee: {newEmployeeUiModel.Email}" +
                    "--CreateEmployee--  @NotComplete@ [CreateEmployeeProcessor]. " +
                    "Message: Just Before MakeItPersistence");

                MakeEmployeePersistent(employeeToBeCreated);

                Log.Debug(
                    $"Create Employee: {newEmployeeUiModel.Email}" +
                    "--CreateEmployee--  @NotComplete@ [CreateEmployeeProcessor]. " +
                    "Message: Just After MakeItPersistence");
                response = ThrowExcIfEmployeeWasNotBeMadePersistent(employeeToBeCreated);
                response.Message = "SUCCESS_CREATION";
            }
            catch (InvalidEmployeeException e)
            {
                response.Message = "ERROR_INVALID_EMPLOYEE_MODEL";
                Log.Error(
                    $"Create Employee: {newEmployeeUiModel.Email}" +
                    $"Error Message:{response.Message}" +
                    "--CreateEmployee--  @NotComplete@ [CreateEmployeeProcessor]. " +
                    $"Broken rules: {e.BrokenRules}");
            }
            catch (EmployeeAlreadyExistsException ex)
            {
                response.Message = "ERROR_EMPLOYEE_ALREADY_EXISTS";
                Log.Error(
                    $"Create Employee: {newEmployeeUiModel.Email}" +
                    $"Error Message:{response.Message}" +
                    "--CreateEmployee--  @fail@ [CreateEmployeeProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (EmployeeDoesNotExistAfterMadePersistentException exx)
            {
                response.Message = "ERROR_EMPLOYEE_NOT_MADE_PERSISTENT";
                Log.Error(
                    $"Create Employee: {newEmployeeUiModel.Email}" +
                    $"Error Message:{response.Message}" +
                    "--CreateEmployee--  @fail@ [CreateEmployeeProcessor]." +
                    $" @innerfault:{exx?.Message} and {exx?.InnerException}");
            }
            catch (Exception exxx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Create Employee: {newEmployeeUiModel.Email}" +
                    $"Error Message:{response.Message}" +
                    $"--CreateEmployee--  @fail@ [CreateEmployeeProcessor]. " +
                    $"@innerfault:{exxx.Message} and {exxx.InnerException}");
            }

            return Task.Run(() => response);
        }

        private void ThrowExcIfThisEmployeeAlreadyExist(Employee employeeToBeCreated)
        {
            var employeeRetrieved = _employeeRepository.FindEmployeeByNameAndEmail(employeeToBeCreated.Lastname, employeeToBeCreated.Firstname, employeeToBeCreated.Email);
            if (employeeRetrieved != null)
            {
                throw new EmployeeAlreadyExistsException(employeeToBeCreated.Lastname,
                    employeeToBeCreated.GetBrokenRulesAsString());
            }
        }

        private EmployeeUiModel ThrowExcIfEmployeeWasNotBeMadePersistent(Employee employeeToBeCreated)
        {
            var retrievedEmployee = _employeeRepository.FindEmployeeByNameAndEmail(employeeToBeCreated.Lastname, employeeToBeCreated.Firstname, employeeToBeCreated.Email);
            if (retrievedEmployee  != null)
                return _autoMapper.Map<EmployeeUiModel>(retrievedEmployee);
            throw new EmployeeDoesNotExistAfterMadePersistentException(employeeToBeCreated.Lastname);
        }

        private void ThrowExcIfEmployeeCannotBeCreated(Employee employeeToBeCreated)
        {
            bool canBeCreated = !employeeToBeCreated.GetBrokenRules().Any();
            if (!canBeCreated)
                throw new InvalidEmployeeException(employeeToBeCreated.GetBrokenRulesAsString());
        }

        private void MakeEmployeePersistent(Employee employeeToBeMadePersistence)
        {
            _employeeRepository.Save(employeeToBeMadePersistence);
            _uOf.Commit();
        }
        
        private void MakeSkillPersistent(Skill skillToBeMadePersistence)
        {
            _skillRepository.Save(skillToBeMadePersistence);
            _uOf.Commit();
        }
    }
}