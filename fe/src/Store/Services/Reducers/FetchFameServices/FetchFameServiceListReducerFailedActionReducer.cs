using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchFameServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchFameServices
{
  public class FetchFameServiceListReducerFailedActionReducer : Reducer<ServiceState, FetchFameServiceListFailedAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchFameServiceListFailedAction action)
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