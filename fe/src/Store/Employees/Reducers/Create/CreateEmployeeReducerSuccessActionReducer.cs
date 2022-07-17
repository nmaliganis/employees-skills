using employee.skill.fe.Store.Employees.Actions.CreateEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.CreateEmployee
{
  public class CreateEmployeeReducerSuccessActionReducer : Reducer<EmployeeState, CreateEmployeeSuccessAction>
  {
    public override EmployeeState Reduce(EmployeeState state, CreateEmployeeSuccessAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.EmployeeHaveBeenCreated.Message,
        state.IsLoading,
        action.EmployeeHaveBeenCreated,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId
      );
    }
  }
}