using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using employee.skill.fe.Models.DTOs.Employees;

namespace employee.skill.fe.ServiceAgents.Contracts.Employees
{
    public interface IEmployeeDataService
    {
        Task<List<EmployeeDto>> GetEmployeeList(string authorizationToken = null);
        Task<EmployeeDto> GetEmployee(Guid actionEmployeeId);
        Task<int> GetTotalEmployeeCount();

        Task<EmployeeDto> CreateEmployee(EmployeeForCreationDto employeeToBeCreated);
        Task<EmployeeDto> UpdateEmployee(Guid employeeIdToBeUpdated, EmployeeForModificationDto employeeToBeUpdated);
        Task<EmployeeDto> DeleteEmployee(Guid employeeIdToBeDeleted);
    }
}