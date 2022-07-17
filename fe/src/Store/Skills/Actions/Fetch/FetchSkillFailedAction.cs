namespace employee.skill.fe.Store.Skills.Actions.Fetch
{
  public class FetchSkillFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchSkillFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}