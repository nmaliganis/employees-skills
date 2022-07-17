namespace employee.skill.fe.Store.Skills.Actions.Delete
{
  public class DeleteSkillFailedAction
  {
    public string ErrorMessage { get; private set; }
    public DeleteSkillFailedAction(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}