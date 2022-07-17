using System.Threading.Tasks;
using employee.skill.common.infrastructure.Helpers.ResourceParameters.Employees;
using employee.skill.common.infrastructure.Paging;
using employees.skills.model.Employees;

namespace employees.Employees.contracts.Employees;

public interface IInquiryAllEmployeesProcessor {
    Task<PagedList<Employee>> GetEmployeesAsync(EmployeesResourceParameters EmployeesResourceParameters);
}