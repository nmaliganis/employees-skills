using employee.skill.fe.Store.Employees.Actions.CreateEmployee;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.CreateEmployee
{
  public class CreateEmployeeReducerFailedActionReducer : Reducer<EmployeeState, CreateEmployeeFailedAction>
  {
    public override EmployeeState Reduce(EmployeeState state, CreateEmployeeFailedAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.ErrorMessage,
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId,
        CreationStatus.Failed,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}