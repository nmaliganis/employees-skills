using employee.skill.fe.Store.Employees.Actions.Create;
using employee.skill.fe.Store.Employees.Actions.CreateEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.CreateEmployee
{
  public class CreateEmployeeReducer : Reducer<EmployeeState, CreateEmployeeAction>
  {
    public override EmployeeState Reduce(EmployeeState state, CreateEmployeeAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        state.Employee,
        action.EmployeeToBeCreatedPayload,
        state.EmployeeToBeUpdatePayload,
        state.EmployeeId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}