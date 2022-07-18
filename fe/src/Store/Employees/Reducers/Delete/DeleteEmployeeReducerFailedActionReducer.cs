using employee.skill.fe.Store.Employees.Actions.DeleteEmployee;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.Delete
{
  public class DeleteEmployeeReducerFailedActionReducer : Reducer<EmployeeState, DeleteEmployeeFailedAction>
  {
    public override EmployeeState Reduce(EmployeeState state, DeleteEmployeeFailedAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.ErrorMessage,
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId,
        state.CreationStatus,
        state.ModificationStatus,
        DeletionStatus.Failed
        );
    }
  }
}