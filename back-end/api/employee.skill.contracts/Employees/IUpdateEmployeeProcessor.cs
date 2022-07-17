using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;

namespace employees.skills.contracts.Employees;

public interface IUpdateEmployeeProcessor
{
    Task<EmployeeUiModel> UpdateEmployeeAsync(Guid employeeIdToBeUpdated,
        EmployeeModificationUiModel updatedEmployee);
}