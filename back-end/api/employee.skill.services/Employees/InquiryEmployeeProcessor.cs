using System;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.repository.ContractRepositories;
using employees.Employees.contracts.Employees;

namespace employees.skills.services.Employees
{
    public class InquiryEmployeeProcessor : IInquiryEmployeeProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IEmployeeRepository _employeeRepository;
        public InquiryEmployeeProcessor(IEmployeeRepository employeeRepository, IAutoMapper autoMapper)
        {
            this._employeeRepository = employeeRepository;
            this._autoMapper = autoMapper;
        }

        public Task<EmployeeUiModel> GetEmployeeByIdAsync(Guid id)
        {
            return Task.Run(() => _autoMapper.Map<EmployeeUiModel>(_employeeRepository.FindBy(id)));
        }

        public Task<EmployeeUiModel> GetEmployeeByNameAsync(string employeeName)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeUiModel> GetEmployeeByEmailAsync(string email)
        {
            var x = _employeeRepository.FindEmployeeByEmail(email);

            return Task.Run(() => _autoMapper.Map<EmployeeUiModel>(_employeeRepository.FindEmployeeByEmail(email)));
        }
    }
}