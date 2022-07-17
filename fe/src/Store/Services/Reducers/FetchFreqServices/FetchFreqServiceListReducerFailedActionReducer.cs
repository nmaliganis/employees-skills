using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchFreqServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchFreqServices
{
  public class FetchFreqServiceListReducerFailedActionReducer : Reducer<ServiceState, FetchFreqServiceListFailedAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchFreqServiceListFailedAction action)
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