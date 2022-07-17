using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.Store.Employees.Actions.FetchEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Effects.FetchEmployee
{
  public class FetchEmployeeEffect : Effect<FetchEmployeeAction>
  {
    public IEmployeeDataService EmployeeDataService { get; set; }
    public FetchEmployeeEffect(IEmployeeDataService employeeDataService)
    {
      EmployeeDataService = employeeDataService;
    }

    public override async Task HandleAsync(FetchEmployeeAction action, IDispatcher dispatcher)
    {
      try
      {
        var Employee = await EmployeeDataService.GetEmployee(action.EmployeeToBeFetchedId);
        dispatcher.Dispatch(new FetchEmployeeSuccessAction(Employee));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchEmployeeFailedAction(e.Message));
      }     
    }
  }
}