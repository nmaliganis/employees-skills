using employee.skill.fe.Store.Employees.Actions.Delete;
using employee.skill.fe.Store.Employees.Actions.DeleteEmployee;
using employee.skill.fe.Store.Statuses;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.Delete
{
  public class DeleteEmployeeReducerSuccessActionReducer : Reducer<EmployeeState, DeleteEmployeeSuccessAction>
  {
    public override EmployeeState Reduce(EmployeeState state, DeleteEmployeeSuccessAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        action.EmployeeDeletionStatus,
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        action.EmployeeHaveBeenDeletedId,
        state.CreationStatus,
        state.ModificationStatus,
        DeletionStatus.Success
      );
    }
  }
}