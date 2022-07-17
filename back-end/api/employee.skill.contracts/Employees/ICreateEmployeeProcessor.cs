using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;

namespace employees.Employees.contracts.Employees;

public interface ICreateEmployeeProcessor
{
    Task<EmployeeUiModel> CreateEmployeeAsync(EmployeeCreationUiModel newEmployeeUiModel);
}