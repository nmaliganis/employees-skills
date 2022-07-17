namespace employee.skill.fe.Store.Skills.Actions.Update
{
  public class UpdateSkillFailedAction
  {
    public string ErrorMessage { get; private set; }
    public string ErrorContent { get; private set; }
    public UpdateSkillFailedAction(string errorMessage, string errorContent)
    {
      ErrorMessage = errorMessage;
    }
  }
}