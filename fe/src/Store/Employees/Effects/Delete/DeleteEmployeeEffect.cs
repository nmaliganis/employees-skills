using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.Store.Employees.Actions.Delete;
using employee.skill.fe.Store.Employees.Actions.DeleteEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Effects.DeleteEmployee
{
  public class DeleteEmployeeEffect : Effect<DeleteEmployeeAction>
  {
    public IEmployeeDataService EmployeeDataService { get; set; }
    public DeleteEmployeeEffect(IEmployeeDataService employeeDataService)
    {
      EmployeeDataService = employeeDataService;
    }

    public override async Task HandleAsync(DeleteEmployeeAction action, IDispatcher dispatcher)
    {
      try
      {
        var deletedEmployee = await EmployeeDataService.DeleteEmployee(action.EmployeeToBeDeletedId);
        dispatcher.Dispatch(new DeleteEmployeeSuccessAction(deletedEmployee.Id, deletedEmployee.Message));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new DeleteEmployeeFailedAction(e.Message));
      }  
    }
  }
}