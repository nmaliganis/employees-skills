using System;
using System.Linq;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Exceptions.Domain.Employees;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.common.infrastructure.UnitOfWorks;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Employees;
using employees.skills.model.Employees;

namespace employees.skills.services.Employees
{
    public class UpdateEmployeeProcessor : IUpdateEmployeeProcessor
    {
        private readonly IUnitOfWork _uOf;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IAutoMapper _autoMapper;

        public UpdateEmployeeProcessor(IUnitOfWork uOf,
            IAutoMapper autoMapper,
            IEmployeeRepository EmployeeRepository)
        {
            this._uOf = uOf;
            this._EmployeeRepository = EmployeeRepository;
            this._autoMapper = autoMapper;
        }


        private void ThrowExcIfEmployeeCannotBeModified(Employee employeeToBeCreated)
        {
            bool canBeCreated = !employeeToBeCreated.GetBrokenRules().Any();
            if (!canBeCreated)
            {
                throw new InvalidEmployeeException(employeeToBeCreated.GetBrokenRulesAsString());
            }
        }

        private void MakeEmployeePersistent(Employee employeeToBeMadePersistence)
        {
            this._EmployeeRepository.Save(employeeToBeMadePersistence);
            this._uOf.Commit();
        }

        public Task<EmployeeUiModel> UpdateEmployeeAsync(Guid employeeIdToBeUpdated,
            EmployeeModificationUiModel updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}