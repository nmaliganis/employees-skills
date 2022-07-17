using System;
using System.Linq;
using employee.skill.common.infrastructure.Domain.Queries;
using employee.skill.common.infrastructure.Paging;
using employee.skill.repository.ContractRepositories;
using employees.skills.model.Skills;
using employees.skills.repository.Repositories.Base;
using NHibernate;
using NHibernate.Criterion;

namespace employees.skills.repository.Repositories;

public class SkillRepository : RepositoryBase<employees.skills.model.Skills.Skill, Guid>, ISkillRepository
{
    public SkillRepository(ISession session)
        : base(session)
    {
    }

    public QueryResult<Skill> FindAllActiveSkillsPagedOf(int? pageNum, int? pageSize)
    {
        var query = this.Session.QueryOver<Skill>();

        if (pageNum == -1 & pageSize == -1)
        {
            return new QueryResult<Skill>(query?
                .Where(r => r.Active == true)
                .List().AsQueryable());
        }

        return new QueryResult<Skill>(query
                    .Where(r => r.Active == true)
                    .Skip(ResultsPagingUtility.CalculateStartIndex((int)pageNum, (int)pageSize))
                    .Take((int)pageSize).List().AsQueryable(),
                query.ToRowCountQuery().RowCount(),
                (int)pageSize)
            ;
    }

    public Skill FindSkillByName(string name)
    {
        return
            (Skill)
            this.Session.CreateCriteria(typeof(Skill))
                .Add(Expression.Eq("Name", name))
                .SetCacheable(true)
                .SetCacheMode(CacheMode.Normal)
                .UniqueResult()
            ;
    }
}