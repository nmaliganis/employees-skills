using System.Linq;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Extensions;
using employee.skill.common.infrastructure.Helpers.ResourceParameters.Employees;
using employee.skill.common.infrastructure.Paging;
using employee.skill.common.infrastructure.PropertyMappings;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.repository.ContractRepositories;
using employees.Employees.contracts.Employees;
using employees.skills.model.Employees;

namespace employees.skills.services.Employees
{
    public class InquiryAllEmployeesProcessor : IInquiryAllEmployeesProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPropertyMappingService _propertyMappingService;

        public InquiryAllEmployeesProcessor(IAutoMapper autoMapper,
                            IEmployeeRepository EmployeeRepository, IPropertyMappingService propertyMappingService)
        {
            _autoMapper = autoMapper;
            _employeeRepository = EmployeeRepository;
            _propertyMappingService = propertyMappingService;
        }

        public Task<PagedList<Employee>> GetEmployeesAsync(EmployeesResourceParameters employeesResourceParameters)
        {
            var collectionBeforePaging =
                QueryableExtensions.ApplySort(_employeeRepository.FindAllActiveEmployeesPagedOf(employeesResourceParameters.PageIndex,
                            employeesResourceParameters.PageSize), 
                    employeesResourceParameters.OrderBy + " " + employeesResourceParameters.SortDirection, 
                    _propertyMappingService.GetPropertyMapping<EmployeeUiModel, Employee>());


            if (!string.IsNullOrEmpty(employeesResourceParameters.Filter) && !string.IsNullOrEmpty(employeesResourceParameters.SearchQuery))
            {
                var searchQueryForWhereClauseFilterFields = employeesResourceParameters.Filter
                    .Trim().ToLowerInvariant();

                var searchQueryForWhereClauseFilterSearchQuery = employeesResourceParameters.SearchQuery
                    .Trim().ToLowerInvariant();

                collectionBeforePaging.QueriedItems = (IQueryable<Employee>)collectionBeforePaging.QueriedItems
                    .AsEnumerable().FilterData(searchQueryForWhereClauseFilterFields, searchQueryForWhereClauseFilterSearchQuery);
            }

            return Task.Run(() => PagedList<Employee>.Create(collectionBeforePaging,
                employeesResourceParameters.PageIndex,
                employeesResourceParameters.PageSize));
        }

        public Task<int> GetTotalCountEmployeesAsync()
        {
            return Task.Run(() => _employeeRepository.FindCountAllActiveEmployees());
        }
    }
}