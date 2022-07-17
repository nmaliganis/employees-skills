using System;
using System.Collections.Generic;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.PropertyMappings;
using employees.skills.model.Employees;
using employees.skills.model.Skills;

namespace employees.skills.api.Helpers
{
    /// <summary>
    /// Class : PropertyMappingService
    /// </summary>
    public class PropertyMappingService : BasePropertyMapping
    {

        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", new PropertyMappingValue(new List<string>() { "id" }) },
            };

        private readonly Dictionary<string, PropertyMappingValue> _skillPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", new PropertyMappingValue(new List<string>() { "id" }) },
            };

        private static readonly IList<IPropertyMapping> PropertyMappings = new List<IPropertyMapping>();

        /// <summary>
        /// PropertyMappingService
        /// </summary>
        public PropertyMappingService() : base(PropertyMappings)
        {
            PropertyMappings.Add(new PropertyMapping<EmployeeUiModel, Employee>(this._employeePropertyMapping));
            PropertyMappings.Add(new PropertyMapping<SkillUiModel, Skill>(this._skillPropertyMapping));
        }
    }
}
