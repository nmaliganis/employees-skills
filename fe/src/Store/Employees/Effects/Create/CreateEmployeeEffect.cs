using System;
using System.Threading.Tasks;
using employee.skill.fe.ServiceAgents.Base;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.Store.Employees.Actions.Create;
using employee.skill.fe.Store.Employees.Actions.CreateEmployee;
using Fluxor;

namespace employee.skill.fe.Store.Employees.Effects.CreateEmployee
{
  public class CreateEmployeeEffect : Effect<CreateEmployeeAction>
  {
    public IEmployeeDataService EmployeeDataService { get; set; }
    public CreateEmployeeEffect(IEmployeeDataService employeeDataService)
    {
      EmployeeDataService = employeeDataService;
    }

    public override async Task HandleAsync(CreateEmployeeAction action, IDispatcher dispatcher)
    {
      try
      {
        var createdEmployee = await EmployeeDataService.CreateEmployee(action.EmployeeToBeCreatedPayload);
        dispatcher.Dispatch(new CreateEmployeeSuccessAction(createdEmployee));
        //Todo: Logging
      }
      catch (ServiceHttpRequestException<string> e)
      {
        dispatcher.Dispatch(new CreateEmployeeFailedAction(errorMessage: e.Message, e.Content));
      }     
      catch (Exception e)
      {
        dispatcher.Dispatch(new CreateEmployeeFailedAction(errorMessage: e.Message, e.InnerException?.Message));
      }     
    }
  }
}