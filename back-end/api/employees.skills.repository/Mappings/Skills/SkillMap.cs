using employees.skills.model.Employees;
using employees.skills.model.Skills;
using FluentNHibernate.Mapping;

namespace employees.skills.repository.Mappings.Skills
{
    public class SkillMap : ClassMap<Skill>
    {
        public SkillMap()
        {
            Table(@"skills");

            Id(x => x.Id)
                .Column("id")
                .CustomType("Guid")
                .Access.Property()
                .CustomSqlType("uuid")
                .Not.Nullable()
                .GeneratedBy
                .GuidComb()
                ;

            Map(x => x.Name)
                .Column("name")
                .CustomType("string")
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("varchar(128)")
                .Not.Nullable()
                .Length(128)
                ;

            Map(x => x.Description)
                .Column("description")
                .CustomType("string")
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

            Map(x => x.CreatedDate)
                .Column("created_date")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never()
                .Not.Nullable()
                ;
            
            HasMany<EmployeeSkill>(x => x.Employees)
                .Access.Property()
                .AsSet()
                .Cascade.All()
                .LazyLoad()
                .Inverse()
                .Generic()
                .KeyColumns.Add("skill_id", mapping =>
                    mapping.Name("skill_id")
                        .SqlType("uuid")
                        .Not.Nullable())
                ;
        }
    }
}