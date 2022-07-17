using System;
using employee.skill.common.infrastructure.Domain;
using employee.skill.common.infrastructure.Domain.Queries;
using employees.skills.model.Employees;

namespace employee.skill.repository.ContractRepositories
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        QueryResult<Employee> FindAllActiveEmployeesPagedOf(int? pageNum, int? pageSize);
        int FindCountAllActiveEmployees();
        Employee FindEmployeeByName(string lastname, string firstname);
        Employee FindEmployeeByNameAndEmail(string lastname, string firstname, string email);

        Employee FindEmployeeByEmail(string email);
    }
}