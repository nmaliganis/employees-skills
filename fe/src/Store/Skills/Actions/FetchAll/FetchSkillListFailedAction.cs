namespace employee.skill.fe.Store.Skills.Actions.FetchAll
{
  public class FetchSkillListFailedAction
  {
    public string ErrorMessage { get; private set; }
    public FetchSkillListFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}