using employees.skills.model.Employees;
using employees.skills.model.Skills;
using FluentNHibernate.Mapping;

namespace employees.skills.repository.Mappings.Employees
{
    public class EmployeeSkillMap : ClassMap<EmployeeSkill>
    {
        public EmployeeSkillMap()
        {
            Table(@"employeesskills");

            Id(x => x.Id)
                .Column("id")
                .CustomType("Guid")
                .Access.Property()
                .CustomSqlType("uuid")
                .Not.Nullable()
                .GeneratedBy
                .GuidComb()
                ;

            Map(x => x.CreatedDate)
                .Column("created_date")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never()
                .Not.Nullable()
                ;
            
            Map(x => x.Active)
                .Column("active")
                .CustomType("Boolean")
                .Access.Property()
                .Generated.Never()
                .Default("true")
                .CustomSqlType("boolean")
                .Not.Nullable()
                ;

            References(x => x.Skill)
                .Class<Skill>()
                .Access.Property()
                .Cascade.SaveUpdate()
                .LazyLoad()
                .Columns("skill_id")
                ;

            References(x => x.Employee)
                .Class<Employee>()
                .Access.Property()
                .Cascade.SaveUpdate()
                .LazyLoad()
                .Columns("employee_id")
                ;
        }
    }
}