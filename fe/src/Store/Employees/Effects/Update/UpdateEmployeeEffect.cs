using employee.skill.fe.ServiceAgents.Base;
using employee.skill.fe.ServiceAgents.Contracts.Employees;
using employee.skill.fe.Store.Employees.Actions.UpdateEmployee;
using Fluxor;
using System;
using System.Threading.Tasks;

namespace employee.skill.fe.Store.Employees.Effects.UpdateEmployee
{
    public class UpdateEmployeeEffect : Effect<UpdateEmployeeAction>
    {
        public IEmployeeDataService EmployeeDataService { get; set; }
        public UpdateEmployeeEffect(IEmployeeDataService employeeDataService)
        {
            EmployeeDataService = employeeDataService;
        }

        public override async Task HandleAsync(UpdateEmployeeAction action, IDispatcher dispatcher)
        {
            try
            {
                var updatedEmployee = await EmployeeDataService.UpdateEmployee(action.EmployeeToBeUpdateId, action.EmployeeForModificationDto);
                if (updatedEmployee != null)
                    dispatcher.Dispatch(new UpdateEmployeeSuccessAction(updatedEmployee, updatedEmployee.Message));
                //Todo: Logging
            }
            catch (ServiceHttpRequestException<string> e)
            {
                dispatcher.Dispatch(new UpdateEmployeeFailedAction(errorMessage: e.Message, e.Content));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new UpdateEmployeeFailedAction(errorMessage: e.Message, e.InnerException?.Message));
            }
        }
    }
}