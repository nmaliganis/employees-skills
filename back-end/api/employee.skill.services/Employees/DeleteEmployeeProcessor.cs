using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Exceptions.Domain.Employees;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.Employees.contracts.Employees;
using employees.skills.model.Employees;
using Serilog;

namespace employees.skills.services.Employees
{
    public class DeleteEmployeeProcessor : IDeleteEmployeeProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAutoMapper _autoMapper;

        public DeleteEmployeeProcessor(IUnitOfWork uOf, IAutoMapper autoMapper, IEmployeeRepository employeeRepository)
        {
            _uOf = uOf;
            _employeeRepository = employeeRepository;
            _autoMapper = autoMapper;
        }


        public Task<EmployeeDeletionUiModel> SoftDeleteEmployeeAsync(Guid employeeToBeDeletedId)
        {
            var response =
                new EmployeeDeletionUiModel()
                {
                    Message = "START_DELETION"
                };

            if (employeeToBeDeletedId == Guid.Empty)
            {
                response.Message = "ERROR_INVALID_Employee_ID";
                return Task.Run(() => response);
            }

            try
            {
                var EmployeeToBeSoftDeleted = _employeeRepository.FindBy(employeeToBeDeletedId);

                if (EmployeeToBeSoftDeleted == null)
                    throw new EmployeeDoesNotExistException(employeeToBeDeletedId);

                EmployeeToBeSoftDeleted.SoftDeleted();

                Log.Debug(
                    $"Update-Delete Employee: with Id: {employeeToBeDeletedId}" +
                    "--SoftDeleteEmployee--  @Ready@ [DeleteEmployeeProcessor]. " +
                    "Message: Just Before MakeItPersistence");

                MakeEmployeePersistent(EmployeeToBeSoftDeleted);

                Log.Debug(
                    $"Update-Delete Employee: with Id: {employeeToBeDeletedId}" +
                    "--SoftDeleteEmployee--  @Ready@ [DeleteEmployeeProcessor]. " +
                    "Message: Just After MakeItPersistence");

                response = ThrowExcIfEmployeeWasNotBeMadePersistent(EmployeeToBeSoftDeleted);
                response.Message = "SUCCESS_DELETION";
            }
            catch (EmployeeDoesNotExistException e)
            {
                response.Message = "ERROR_Employee_DOES_NOT_EXIST";
                Log.Error(
                    $"Update-Delete Employee: Id: {employeeToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--SoftDeleteEmployee--  @NotComplete@ [DeleteEmployeeProcessor]. " +
                    $"@innerfault:{e?.Message} and {e?.InnerException}");
            }
            catch (EmployeeDoesNotExistAfterMadePersistentException ex)
            {
                response.Message = "ERROR_Employee_DOES_NOT_MADE_PERSISTENCE";
                Log.Error(
                    $"Update-Delete Employee: Id: {employeeToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--SoftDeleteEmployee--  @NotComplete@ [DeleteEmployeeProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (Exception exx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Update-Delete Employee: Id: {employeeToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    $"--SoftDeleteEmployee--  @fail@ [DeleteEmployeeProcessor]. " +
                    $"@innerfault:{exx.Message} and {exx.InnerException}");
            }

            return Task.Run(() => response);
        }


        public Task<EmployeeDeletionUiModel> HardDeleteEmployeeAsync(Guid employeeToBeDeletedId)
        {
            var response =
                new EmployeeDeletionUiModel()
                {
                    Message = "START_HARD_DELETION"
                };

            if (employeeToBeDeletedId == Guid.Empty)
            {
                response.Message = "ERROR_INVALID_Employee_ID";
                return Task.Run(() => response);
            }

            try
            {
                var employeeToBeHardDeleted = _employeeRepository.FindBy(employeeToBeDeletedId);

                if (employeeToBeHardDeleted == null)
                    throw new EmployeeDoesNotExistException(employeeToBeDeletedId);

                Log.Debug(
                    $"Update-Delete Employee: with Id: {employeeToBeDeletedId}" +
                    "--HardDeleteEmployee--  @Ready@ [DeleteEmployeeProcessor]. " +
                    "Message: Just Before MakeItPersistence");

                MakeEmployeeTransient(employeeToBeHardDeleted);

                Log.Debug(
                    $"Update-Delete Employee: with Id: {employeeToBeDeletedId}" +
                    "--HardDeleteEmployee--  @Ready@ [DeleteEmployeeProcessor]. " +
                    "Message: Just After MakeItPersistence");

                response.DeletionStatus  = ThrowExcIfEmployeeWasNotBeMadeTransient(employeeToBeHardDeleted);
                response.Message = "SUCCESS_DELETION";

            }
            catch (EmployeeDoesNotExistException e)
            {
                response.Message = "ERROR_Employee_DOES_NOT_EXIST";
                Log.Error(
                    $"Delete Employee: Id: {employeeToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--HardDeleteEmployee--  @NotComplete@ [DeleteEmployeeProcessor]. " +
                    $"@innerfault:{e?.Message} and {e?.InnerException}");
            }
            catch (EmployeeDoesNotExistAfterMadeTransientException ex)
            {
                response.Message = "ERROR_Employee_DOES_NOT_MADE_TRANSIENT";
                Log.Error(
                    $"Delete Employee: Id: {employeeToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    "--HardDeleteEmployee--  @NotComplete@ [DeleteEmployeeProcessor]. " +
                    $"@innerfault:{ex?.Message} and {ex?.InnerException}");
            }
            catch (Exception exxx)
            {
                response.Message = "UNKNOWN_ERROR";
                Log.Error(
                    $"Delete Employee: Id: {employeeToBeDeletedId}" +
                    $"Error Message:{response.Message}" +
                    $"--HardDeleteEmployee--  @fail@ [DeleteEmployeeProcessor]. " +
                    $"@innerfault:{exxx.Message} and {exxx.InnerException}");
            }

            return Task.Run(() => response);
        }

        private EmployeeDeletionUiModel ThrowExcIfEmployeeWasNotBeMadePersistent(Employee employeeToBeSoftDeleted)
        {
            var retrievedEmployee =
                _employeeRepository.FindBy(employeeToBeSoftDeleted.Id);
            if (retrievedEmployee != null)
                return _autoMapper.Map<EmployeeDeletionUiModel>(retrievedEmployee);
            throw new EmployeeDoesNotExistAfterMadePersistentException(employeeToBeSoftDeleted.Id);
        }

        private bool ThrowExcIfEmployeeWasNotBeMadeTransient(Employee employeeToBeSoftDeleted)
        {
            var retrievedEmployee =
                _employeeRepository.FindBy(employeeToBeSoftDeleted.Id);
            return retrievedEmployee != null
                ? throw new EmployeeDoesNotExistAfterMadePersistentException(employeeToBeSoftDeleted.Id)
                : true;
        }

        private void MakeEmployeeTransient(Employee employeeToBeSoftDeleted)
        {
            _employeeRepository.Remove(employeeToBeSoftDeleted);
            _uOf.Commit();
        }
        
        private void MakeEmployeePersistent(Employee employeeToBeUpdated)
        {
            _employeeRepository.Save(employeeToBeUpdated);
            _uOf.Commit();
        }
    }
}
