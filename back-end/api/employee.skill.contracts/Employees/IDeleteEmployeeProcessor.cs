using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;

namespace employees.Employees.contracts.Employees;

public interface IDeleteEmployeeProcessor
{
    Task DeleteEmployeeAsync(Guid employeeToBeDeletedId);
    Task<EmployeeDeletionUiModel> SoftDeleteEmployeeAsync(Guid employeeToBeDeletedId);
}