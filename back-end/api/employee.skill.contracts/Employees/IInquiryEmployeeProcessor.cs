using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;

namespace employees.Employees.contracts.Employees;

public interface IInquiryEmployeeProcessor
{
    Task<EmployeeUiModel> GetEmployeeByIdAsync(Guid id);
    Task<EmployeeUiModel> GetEmployeeByNameAsync(string employeeName);
}