namespace employee.skill.common.infrastructure.PropertyMappings.TypeHelpers
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}