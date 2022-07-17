using System.Linq;
using System.Threading.Tasks;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.Extensions;
using employee.skill.common.infrastructure.Helpers.ResourceParameters.Skills;
using employee.skill.common.infrastructure.Paging;
using employee.skill.common.infrastructure.PropertyMappings;
using employee.skill.common.infrastructure.TypeMappings;
using employee.skill.repository.ContractRepositories;
using employees.skills.contracts.Skills;
using employees.skills.model.Skills;

namespace employees.skills.services.Skills;

public class InquiryAllSkillsProcessor : IInquiryAllSkillsProcessor
{
    private readonly IAutoMapper _autoMapper;
    private readonly ISkillRepository _SkillRepository;
    private readonly IPropertyMappingService _propertyMappingService;

    public InquiryAllSkillsProcessor(IAutoMapper autoMapper,
        ISkillRepository SkillRepository, IPropertyMappingService propertyMappingService)
    {
        _autoMapper = autoMapper;
        _SkillRepository = SkillRepository;
        _propertyMappingService = propertyMappingService;
    }

    public Task<PagedList<Skill>> GetSkillsAsync(SkillsResourceParameters SkillsResourceParameters)
    {
        var collectionBeforePaging =
            QueryableExtensions.ApplySort(_SkillRepository.FindAllActiveSkillsPagedOf(SkillsResourceParameters.PageIndex,
                    SkillsResourceParameters.PageSize), 
                SkillsResourceParameters.OrderBy + " " + SkillsResourceParameters.SortDirection, 
                _propertyMappingService.GetPropertyMapping<SkillUiModel, Skill>());


        if (!string.IsNullOrEmpty(SkillsResourceParameters.Filter) && !string.IsNullOrEmpty(SkillsResourceParameters.SearchQuery))
        {
            var searchQueryForWhereClauseFilterFields = SkillsResourceParameters.Filter
                .Trim().ToLowerInvariant();

            var searchQueryForWhereClauseFilterSearchQuery = SkillsResourceParameters.SearchQuery
                .Trim().ToLowerInvariant();

            collectionBeforePaging.QueriedItems = (IQueryable<Skill>)collectionBeforePaging.QueriedItems
                .AsEnumerable().FilterData(searchQueryForWhereClauseFilterFields, searchQueryForWhereClauseFilterSearchQuery);
        }

        return Task.Run(() => PagedList<Skill>.Create(collectionBeforePaging,
            SkillsResourceParameters.PageIndex,
            SkillsResourceParameters.PageSize));
    }
}