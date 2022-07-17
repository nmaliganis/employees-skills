using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.Store.Employees.Actions.FetchEmployees;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Effects.FetchEmployees
{
  public class FetchEmployeeListEffect : Effect<FetchEmployeeListAction>
  {
    public IEmployeeDataService EmployeeDataService { get; set; }
    public FetchEmployeeListEffect(IEmployeeDataService employeeDataService)
    {
      EmployeeDataService = employeeDataService;
    }

    public override async Task HandleAsync(FetchEmployeeListAction action, IDispatcher dispatcher)
    {
      try
      {
        var Employees = await EmployeeDataService.GetEmployeeList();
        dispatcher.Dispatch(new FetchEmployeeListSuccessAction(Employees));
      }
      catch (Exception e)
      {
        dispatcher.Dispatch(new FetchEmployeeListFailedAction(e.Message));
      }      
    }
  }
}