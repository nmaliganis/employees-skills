using employee.skill.fe.Store.Employees.Actions.FetchEmployees;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.FetchEmployees
{
  public class FetchEmployeeListReducer : Reducer<EmployeeState, FetchEmployeeListAction>
  {
    public override EmployeeState Reduce(EmployeeState state, FetchEmployeeListAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}