using employee.skill.fe.Store.Employees.Actions.UpdateEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Reducers.UpdateEmployee
{
  public class UpdateEmployeeReducer : Reducer<EmployeeState, UpdateEmployeeAction>
  {
    public override EmployeeState Reduce(EmployeeState state, UpdateEmployeeAction action)
    {
      return new EmployeeState(
        state.EmployeeList,
        "",
        state.IsLoading,
        state.Employee,
        state.EmployeeToBeCreatedPayload,
        action.EmployeeForModificationDto,
        action.EmployeeToBeUpdateId,
        state.CreationStatus,
        state.ModificationStatus,
        state.DeletionStatus
        );
    }
  }
}