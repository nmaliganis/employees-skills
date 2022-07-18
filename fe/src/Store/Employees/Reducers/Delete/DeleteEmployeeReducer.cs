using employee.skill.fe.Store.Employees.Actions.Delete;
using employee.skill.fe.Store.Employees.Actions.DeleteEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.DeleteEmployee
{
  public class DeleteEmployeeReducer : Reducer<EmployeeState, DeleteEmployeeAction>
  {
    public override EmployeeState Reduce(EmployeeState state, DeleteEmployeeAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        action.EmployeeToBeDeletedId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}