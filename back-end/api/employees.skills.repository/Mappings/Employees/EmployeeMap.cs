using employees.skills.model.Employees;
using FluentNHibernate.Mapping;

namespace employees.skills.repository.Mappings.Employees
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table(@"employees");

            Id(x => x.Id)
                .Column("id")
                .CustomType("Guid")
                .Access.Property()
                .CustomSqlType("uuid")
                .Not.Nullable()
                .GeneratedBy
                .GuidComb()
                ;

            Map(x => x.Firstname)
                .Column("firstname")
                .CustomType("string")
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("varchar(128)")
                .Not.Nullable()
                .Length(128)
                ;

            Map(x => x.Lastname)
                .Column("lastname")
                .CustomType("string")
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("varchar(128)")
                .Not.Nullable()
                .Length(128)
                ;

            Map(x => x.Email)
                .Column("email")
                .CustomType("string")
                .Access.Property()
                .Generated.Never()
                .Unique()
                .CustomSqlType("varchar(128)")
                .Not.Nullable()
                .Length(128)
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
            
            Map(x => x.HiredDate)
                .Column("hired_date")
                .CustomType("DateTime")
                .Access.Property()
                .Generated.Never()
                .Not.Nullable()
                ;
            
            HasMany<EmployeeSkill>(x => x.Skills)
                .Access.Property()
                .Cascade.AllDeleteOrphan()
                .LazyLoad()
                .Inverse()
                .Generic()
                .KeyColumns.Add("employee_id", mapping =>
                    mapping.Name("employee_id")
                        .SqlType("uuid")
                        .Nullable())
                ;
        }
    }
}