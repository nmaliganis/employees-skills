using System;
using System.Linq;
using employee.skill.common.infrastructure.Domain.Queries;
using employee.skill.common.infrastructure.Paging;
using employee.skill.repository.ContractRepositories;
using employees.skills.model.Employees;
using employees.skills.repository.Repositories.Base;
using NHibernate;
using NHibernate.Criterion;

namespace employees.skills.repository.Repositories;

public class EmployeeRepository : RepositoryBase<Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(ISession session)
        : base(session)
    {
    }

    public QueryResult<Employee> FindAllActiveEmployeesPagedOf(int? pageNum, int? pageSize)
    {
        var query = this.Session.QueryOver<Employee>();

        if (pageNum == -1 & pageSize == -1)
        {
            return new QueryResult<Employee>(query?
                .Where(r => r.Active == true)
                .List().AsQueryable());
        }

        return new QueryResult<Employee>(query
                    .Where(r => r.Active == true)
                    .Skip(ResultsPagingUtility.CalculateStartIndex((int)pageNum, (int)pageSize))
                    .Take((int)pageSize).List().AsQueryable(),
                query.ToRowCountQuery().RowCount(),
                (int)pageSize)
            ;
    }

    public int FindCountAllActiveEmployees()
    {
        var count = this.Session
            .CreateCriteria<Employee>()
            .Add(Expression.Eq("Active", true))
            .SetProjection(
                Projections.Count(Projections.Id())
            )
            .UniqueResult<int>();

        return count;
    }

    public Employee FindEmployeeByName(string lastname, string firstname)
    {
        return
            (Employee)
            this.Session.CreateCriteria(typeof(Employee))
                .Add(Expression.Eq("Lastname", lastname))
                .Add(Expression.Eq("Firstname", firstname))
                .SetCacheable(true)
                .SetCacheMode(CacheMode.Normal)
                .UniqueResult()
            ;
    }

    public Employee FindEmployeeByNameAndEmail(string lastname, string firstname, string email)
    {
        return
            (Employee)
            this.Session.CreateCriteria(typeof(Employee))
                .Add(Expression.Eq("Lastname", lastname))
                .Add(Expression.Eq("Firstname", firstname))
                .Add(Expression.Eq("Email", email))
                .SetCacheable(true)
                .SetCacheMode(CacheMode.Normal)
                .UniqueResult()
            ;
    }

    public Employee FindEmployeeByEmail(string email)
    {
        return
            (Employee)
            this.Session.CreateCriteria(typeof(Employee))
                .Add(Expression.Eq("Email", email))
                .SetCacheable(true)
                .SetCacheMode(CacheMode.Normal)
                .UniqueResult()
            ;
    }
}