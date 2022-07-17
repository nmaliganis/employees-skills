using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchServices
{
  public class FetchServiceListReducerFailedActionReducer : Reducer<ServiceState, FetchServiceListFailedAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchServiceListFailedAction action)
    {
      return new ServiceState(
        state.ServiceList,
        state.ServiceTypeList,
        state.ServiceFameYearList,
        state.ServiceFameMonthList,
        state.ServiceFreqYearList,
        state.ServiceFreqMonthList,
        action.ErrorMessage,
        state.IsLoading,
        state.Service,
        state.ServiceToBeCreatedPayload,
        state.ServiceToBeUpdatePayload,
        state.ServiceId
        );
    }
  }
}