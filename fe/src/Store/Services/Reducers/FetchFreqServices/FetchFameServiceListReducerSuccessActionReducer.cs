using Fluxor;
using smarthotel.ui.Store.Services.Actions.FetchFreqServices;

namespace smarthotel.ui.Store.Services.Reducers.FetchFreqServices
{
  public class FetchFreqServiceListReducerSuccessActionReducer : Reducer<ServiceState, FetchFreqServiceListSuccessAction>
  {
    public override ServiceState Reduce(ServiceState state, FetchFreqServiceListSuccessAction action)
    {
      return new ServiceState(
        state.ServiceList,
        state.ServiceTypeList,
        state.ServiceFameYearList,
        state.ServiceFameMonthList,
        action.ServiceFreqYearList,
        action.ServiceFreqMonthList,
        "",
        state.IsLoading,
        state.Service,
        state.ServiceToBeCreatedPayload,
        state.ServiceToBeUpdatePayload,
        state.ServiceId
      );
    }
  }
}