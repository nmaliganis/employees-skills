namespace employee.skill.common.infrastructure.Serializers
{
  public interface IJsonSerializer
  {
    T DeserializeObject<T>(string json);
    string SerializeObject(object item);
  }
}