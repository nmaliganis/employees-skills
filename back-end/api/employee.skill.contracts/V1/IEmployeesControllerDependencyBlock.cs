using employees.Employees.contracts.Employees;
using employees.skills.contracts.Employees;

namespace employees.skills.contracts.V1;

public interface IEmployeesControllerDependencyBlock
{
    ICreateEmployeeProcessor CreateEmployeeProcessor { get; }
    IInquiryEmployeeProcessor InquiryEmployeeProcessor { get; }
    IUpdateEmployeeProcessor UpdateEmployeeProcessor { get; }
    IInquiryAllEmployeesProcessor InquiryAllEmployeesProcessor { get; }
    IDeleteEmployeeProcessor DeleteEmployeeProcessor { get; }
}