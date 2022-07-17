using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.Employees.contracts.Employees;

namespace employees.skills.services.Employees
{
    public class DeleteEmployeeProcessor : IDeleteEmployeeProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IEmployeeRepository _EmployeeRepository;

        public DeleteEmployeeProcessor(IUnitOfWork uOf,
            IEmployeeRepository EmployeeRepository)
        {
            _uOf = uOf;
            _EmployeeRepository = EmployeeRepository;
        }

        public Task DeleteEmployeeAsync(Guid employeeToBeDeletedId)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDeletionUiModel> SoftDeleteEmployeeAsync(Guid employeeToBeDeletedId)
        {
            throw new NotImplementedException();
        }
    }
}